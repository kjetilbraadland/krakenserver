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
            if (_itemsContext.Items.Count() == 0)
            {
                MyFile f1 = new MyFile() { FilePath = "gg" };
                Job j = new Job() { Name = "Dummy", Files = new List<MyFile> { f1 }, Command = "excute" };
                Add(j);
            }
        }

        public IEnumerable<Job> GetAll()
        {
            return _itemsContext.Items;
        }

        public void Add(Job item)
        {
            try
            {
                if (!_itemsContext.Items.Any(i => i.JobId == item.JobId))
                {
                    _itemsContext.Add(new Job { Name = item.Name, IsComplete = item.IsComplete, Files = item.Files, Command = item.Command });
                    _itemsContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Job Find(string key)
        {
            try
            {
                Job item = _itemsContext.Items.First(i => i.Key == key);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Job Remove(string key)
        {
            Job item = _itemsContext.Items.First(i => i.Key == key);
            _itemsContext.Items.Remove(item);
            _itemsContext.SaveChanges();
            return item;
        }

        public void Update(Job item)
        {
            var orignal = _itemsContext.Items.First(i => i.Key == item.Key);
            orignal.Name = item.Name;
            orignal.IsComplete = item.IsComplete;
            orignal.ReservedBy = item.ReservedBy;
            orignal.Files = item.Files;
            orignal.Command = item.Command;
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

        public Job GetNextItem(string aClient)
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