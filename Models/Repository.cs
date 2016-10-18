using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

using aspnetcoreapp.Database;
using System.Linq;

namespace aspnetcoreapp.Models
{
    public class Repository : IRepository
    {
        private readonly ItemsContext _itemsContext = new ItemsContext();

        public Repository()
        {

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
            orignal.ReservedBy = item.ReservedBy;
            orignal.FilePath = item.FilePath;
            _itemsContext.SaveChanges();
        }

        public void RemoveAll()
        {
            foreach (var row in _itemsContext.Items)
            {
                _itemsContext.Items.Remove(row);
            }
            _itemsContext.SaveChanges();
        }

        public Item GetNextItem(string aClient)
        {
            var item = _itemsContext.Items.FirstOrDefault(i => i.ReservedBy == aClient);
            if (item == null)
            {
                item = _itemsContext.Items.FirstOrDefault(i => String.IsNullOrEmpty(i.ReservedBy));
                if (item != null)
                {
                    item.ReservedBy = aClient;
                    Update(item);
                }
            }

            return item;
        }
    }
}