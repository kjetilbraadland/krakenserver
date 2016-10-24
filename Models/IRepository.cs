using System.Collections.Generic;

namespace aspnetcoreapp.Models
{
    public interface IRepository
    {
        void Add(Job item);
        IEnumerable<Job> GetAll();
        Job Find(string key);
        Job Remove(string key);
        void Update(Job item);
        void RemoveAll();

        Job GetNextItem(string client);
        
    }
}