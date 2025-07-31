1) SELECT DISTINCT City
FROM Customers
WHERE City IN (
    SELECT DISTINCT City FROM Employees
)

2a) SELECT DISTINCT City
FROM Customers
WHERE City NOT IN (
    SELECT DISTINCT City FROM Employees
)

2b) SELECT DISTINCT c.City
FROM Customers c
LEFT JOIN Employees e ON c.City = e.City
WHERE e.City IS NULL

3) SELECT p.ProductName, SUM(od.Quantity) AS TotalQuantity
FROM Products p
JOIN [Order Details] od ON p.ProductID = od.ProductID
GROUP BY p.ProductName

4) SELECT c.City, SUM(od.Quantity) AS TotalOrdered
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY c.City

5) SELECT City, COUNT(*) AS NumCustomers
FROM Customers
GROUP BY City
HAVING COUNT(*) >= 2

6) SELECT c.City
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY c.City
HAVING COUNT(DISTINCT od.ProductID) >= 2

7) SELECT DISTINCT c.CustomerID, c.CompanyName, o.ShipCity, c.City AS CustomerCity
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
WHERE o.ShipCity <> c.City

8) WITH ProductTotals AS (
    SELECT od.ProductID, SUM(od.Quantity) AS TotalOrdered
    FROM [Order Details] od
    GROUP BY od.ProductID
),
TopProducts AS (
    SELECT TOP 5 pt.ProductID, pt.TotalOrdered
    FROM ProductTotals pt
    ORDER BY pt.TotalOrdered DESC
),
CityProductTotals AS (
    SELECT c.City, od.ProductID, SUM(od.Quantity) AS CityQuantity
    FROM Customers c
    JOIN Orders o ON c.CustomerID = o.CustomerID
    JOIN [Order Details] od ON o.OrderID = od.OrderID
    GROUP BY c.City, od.ProductID
),
TopCityPerProduct AS (
    SELECT ProductID, City, CityQuantity,
           RANK() OVER (PARTITION BY ProductID ORDER BY CityQuantity DESC) AS rnk
    FROM CityProductTotals
)
SELECT p.ProductName, AVG(od.UnitPrice) AS AvgPrice, t.City
FROM TopProducts tp
JOIN Products p ON p.ProductID = tp.ProductID
JOIN [Order Details] od ON od.ProductID = tp.ProductID
JOIN TopCityPerProduct t ON t.ProductID = tp.ProductID AND t.rnk = 1
GROUP BY p.ProductName, t.City

9a) SELECT DISTINCT e.City
FROM Employees e
WHERE e.City NOT IN (
    SELECT DISTINCT o.ShipCity
    FROM Orders o
)

9b) SELECT DISTINCT e.City
FROM Employees e
LEFT JOIN Orders o ON e.City = o.ShipCity
WHERE o.OrderID IS NULL

10) WITH EmployeeSales AS (
    SELECT e.City, COUNT(o.OrderID) AS NumOrders
    FROM Employees e
    JOIN Orders o ON e.EmployeeID = o.EmployeeID
    GROUP BY e.City
),
TopEmpCity AS (
    SELECT TOP 1 City
    FROM EmployeeSales
    ORDER BY NumOrders DESC
),
CityQuantities AS (
    SELECT c.City, SUM(od.Quantity) AS TotalQty
    FROM Customers c
    JOIN Orders o ON c.CustomerID = o.CustomerID
    JOIN [Order Details] od ON o.OrderID = od.OrderID
    GROUP BY c.City
),
TopOrderCity AS (
    SELECT TOP 1 City
    FROM CityQuantities
    ORDER BY TotalQty DESC
)
SELECT e.City
FROM TopEmpCity e
JOIN TopOrderCity o ON e.City = o.City

11) WITH CTE AS (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY Column1, Column2, Column3 ORDER BY (SELECT NULL)) AS rn
    FROM YourTable
)
DELETE FROM CTE WHERE rn > 1

