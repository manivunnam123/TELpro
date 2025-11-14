using Microsoft.EntityFrameworkCore;
using TELpro.Models;

namespace TELpro.DATA
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }


    }
}
