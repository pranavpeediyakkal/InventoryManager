using InventoryManager.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InventoryManager.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("InventoryConnectionString")
        {

        }

        public DbSet<Inventory> Items { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}