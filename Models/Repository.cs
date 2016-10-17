using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

using aspnetcoreapp.Database;
using System.Linq;

namespace aspnetcoreapp.Models
{
    public class Repository : IRepository
    {
        //private static ConcurrentDictionary<string, Item> _todos = new ConcurrentDictionary<string, Item>();
        private readonly ItemsContext _itemsContext = new ItemsContext();

        public Repository()
        {
            //Add(new Item { Name = "Item1" });
            //Add(new Item { Name = "Item2" });

            // try
            // {
            //     using (var db = new ItemsContext())
            //     {
            //         db.Items.Add(new Item { Name = "Item1" });
            //         db.Items.Add(new Item { Name = "Item2" });
            //         var count = db.SaveChanges();
            //         Console.WriteLine("{0} records saved to database", count);

            //         Console.WriteLine();
            //         Console.WriteLine("All blogs in database:");
            //         foreach (var item in db.Items)
            //         {
            //             Console.WriteLine(" - {0}", item.Key);
            //         }
            //     }

            // }
            // catch (Exception ex)
            // {

            // }
        }

        public IEnumerable<Item> GetAll()
        {
            return _itemsContext.Items;
        }

        public void Add(Item item)
        {
            item.Key = Guid.NewGuid().ToString();

            if (!_itemsContext.Items.Any(i => i.Key == item.Key || i.ItemId == item.ItemId))
            {
                _itemsContext.Add(new Item { Name = item.Name, IsComplete = item.IsComplete });
                _itemsContext.SaveChanges();
            }
        }

        public Item Find(string key)
        {
            try
            {
                Item item = _itemsContext.Items.First(i => i.Key == key);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Item Remove(string key)
        {
            Item item = _itemsContext.Items.First(i => i.Key == key);
            _itemsContext.Items.Remove(item);
            _itemsContext.SaveChanges();
            return item;
        }

        public void Update(Item item)
        {
            var orignal = _itemsContext.Items.First(i => i.Key == item.Key);
            orignal.Name = item.Name;
            orignal.IsComplete = item.IsComplete;
            _itemsContext.SaveChanges();
        }

        public void RemoveAll()
        {
            foreach(var row in _itemsContext.Items)
            {
                _itemsContext.Items.Remove(row);
            }
            _itemsContext.SaveChanges();
        }
    }
}