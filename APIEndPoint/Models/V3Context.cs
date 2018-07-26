using Microsoft.EntityFrameworkCore;

namespace APIEndPoint.Models
{
    public class V3Context : DbContext
    {
        public V3Context(DbContextOptions<V3Context> options)
            : base(options)
        {
        }

        public DbSet<V3> V3s { get; set; }
    }
}
