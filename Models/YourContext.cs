using Microsoft.EntityFrameworkCore;
//CHANGE THIS
namespace Products_And_Categories.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        // "users" table is represented by this DbSet "Users"
        public DbSet<Products> Products { get; set; }
        public DbSet<Associations> Associations { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
