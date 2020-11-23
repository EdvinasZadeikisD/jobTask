using Microsoft.EntityFrameworkCore;

namespace JobTask.Data
{
    public class BundleContext : DbContext
    {
        public BundleContext(DbContextOptions<BundleContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=device.db");
    }
}
