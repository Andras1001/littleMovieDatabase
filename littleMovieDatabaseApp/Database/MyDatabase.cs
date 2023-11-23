using littleMovieDatabaseApp.Models;
using Microsoft.EntityFrameworkCore;
namespace littleMovieDatabaseApp.Database
{
    public class MyDatabase:DbContext
    {
        public MyDatabase(DbContextOptions<MyDatabase> options) : base(options)
        { 
        
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}
