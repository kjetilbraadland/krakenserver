using System.Collections.Generic;

namespace aspnetcoreapp.Models
{
    public interface IRepository
    {
        void Add(Item item);
        IEnumerable<Item> GetAll();
        Item Find(string key);
        Item Remove(string key);
        void Update(Item item);
    }
}