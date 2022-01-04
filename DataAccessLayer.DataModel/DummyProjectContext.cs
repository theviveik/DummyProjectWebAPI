using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataModel
{
    public class DummyProjectContext : DbContext
    {
        public DummyProjectContext(DbContextOptions<DummyProjectContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }

    }
}
