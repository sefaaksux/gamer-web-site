using Gallery.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Data
{
    public class MyContext : DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=mobilya; TrustServerCertificate=True; Integrated Security=true;");
        }

      
        public DbSet<Product> products {get; set;}
        public DbSet<Image> images {get; set;}
        public DbSet<User> users {get; set;}

        public DbSet<Category> categories {get; set;}

    } 
    
}