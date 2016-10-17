using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

using aspnetcoreapp.Models;

namespace aspnetcoreapp.Database
{
    public class ItemsContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        //public DbSet<Item> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Items.db");          
    
        }
    }
}