

using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericRepositoryDemo
{
    // Base entity: reference type with an Id
    public abstract class Entity
    {
        public int Id { get; set; }
    }

    // Repository contract (as requested)
    public interface IRepository<T> where T : Entity
    {
        void Add(T item);
        void Remove(T item);
        void Save();
        IEnumerable<T> GetAll();
        T GetById(int id);
    }

    // Pluggable data store contract (can be InMemory, SQL, Oracle, etc.)
    public interface IDataStore<T> where T : Entity
    {
        void Add(T item);
        void Remove(T item);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void SaveChanges();
    }

    // In-memory data store implementation
    public class InMemoryDataStore<T> : IDataStore<T> where T : Entity, new()
    {
        private readonly Dictionary<int, T> _data = new();
        private int _nextId = 1;

        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (item.Id == 0) item.Id = _nextId++; // auto-assign Id if not set
            _data[item.Id] = item;
        }

        public void Remove(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _data.Remove(item.Id);
        }

        public IEnumerable<T> GetAll()
        {
            // Return a snapshot to avoid external mutation issues
            return _data.Values.ToList();
        }

        public T GetById(int id)
        {
            return _data.TryGetValue(id, out var value) ? value : null;
        }

        public void SaveChanges()
        {
            // No-op for in-memory; DB-backed stores would persist here
        }
    }

    // Generic repository using any IDataStore<T>
    public class GenericRepository<T> : IRepository<T> where T : Entity
    {
        private readonly IDataStore<T> _store;

        public GenericRepository(IDataStore<T> store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public void Add(T item) => _store.Add(item);

        public void Remove(T item) => _store.Remove(item);

        public void Save() => _store.SaveChanges();

        public IEnumerable<T> GetAll() => _store.GetAll();

        public T GetById(int id) => _store.GetById(id);
    }

    // Example entity
    public class Customer : Entity
    {
        public string Name { get; set; }
        public override string ToString() => $"Customer {{ Id = {Id}, Name = {Name} }}";
    }

    class Program
    {
        static void Main()
        {
            // Swap this with a different IDataStore<T> to change the backend
            IDataStore<Customer> store = new InMemoryDataStore<Customer>();
            IRepository<Customer> repo = new GenericRepository<Customer>(store);

            // Create
            var alice = new Customer { Name = "Alice" };
            var bob   = new Customer { Name = "Bob" };
            repo.Add(alice);
            repo.Add(bob);
            repo.Save();

            // Read all
            Console.WriteLine("All customers after add:");
            foreach (var c in repo.GetAll())
                Console.WriteLine(c);

            // Read by Id
            var fetched = repo.GetById(alice.Id);
            Console.WriteLine($"\nFetched by Id {alice.Id}: {fetched}");

            // Delete
            repo.Remove(bob);
            repo.Save();

            Console.WriteLine("\nAll customers after removing Bob:");
            foreach (var c in repo.GetAll())
                Console.WriteLine(c);

            // Done
            Console.WriteLine("\nDone.");
        }
    }
}


