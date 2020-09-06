using Microsoft.EntityFrameworkCore;

namespace dockerDemoApp.Models
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }

        public DbSet<CustomerModel> CustomerItems { get; set; }
    }
}
