namespace Day6;


public class Ball
    {
        private int size;
        private Color color;
        private int throwCount;

        // Constructor
        public Ball(int size, Color color)
        {
            this.size = size;
            this.color = color;
            throwCount = 0;
        }

        // Pop method sets size to 0
        public void Pop()
        {
            size = 0;
        }

        // Throw method increments only if not popped
        public void Throw()
        {
            if (size > 0)
            {
                throwCount++;
            }
        }

        // Get the throw count
        public int GetThrowCount()
        {
            return throwCount;
        }

        public override string ToString()
        {
            return $"Ball(Size: {size}, Color: {color}, Thrown: {throwCount} times)";
        }
    }

    