using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) {

        }

        public DbSet<Book> Book { get; set; }
    }
}