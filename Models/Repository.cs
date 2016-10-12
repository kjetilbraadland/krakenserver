using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace aspnetcoreapp.Models
{
    public class Repository : IRepository
    {
        private static ConcurrentDictionary<string, Item> _todos =
              new ConcurrentDictionary<string, Item>();

        public Repository()
        {
            Add(new Item { Name = "Item1" });
        }

        public IEnumerable<Item> GetAll()
        {
            return _todos.Values;
        }

        public void Add(Item item)
        {
            item.Key = Guid.NewGuid().ToString();
            _todos[item.Key] = item;
        }

        public Item Find(string key)
        {
            Item item;
            _todos.TryGetValue(key, out item);
            return item;
        }

        public Item Remove(string key)
        {
            Item item;
            _todos.TryRemove(key, out item);
            return item;
        }

        public void Update(Item item)
        {
            _todos[item.Key] = item;
        }
    }
}