using Jinkan.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinkan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<log> logs { get; set; }
    }
}
