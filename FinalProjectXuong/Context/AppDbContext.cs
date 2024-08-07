using FinalProjectXuong.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectXuong.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Navigation(u => u.Cart)
                .AutoInclude();

            modelBuilder.Entity<Cart>()
                .Navigation(c => c.User)
                .AutoInclude();

            modelBuilder.Entity<Cart>()
                .Navigation(c => c.CartItems)
                .AutoInclude();

            //modelBuilder.Entity<Book>()
            //    .Navigation(b => b.CartItems)
            //    .AutoInclude();

            modelBuilder.Entity<CartItem>()
                .Navigation(ci => ci.Book)
                .AutoInclude();

            modelBuilder.Entity<CartItem>()
                .Navigation(ci => ci.Cart)
                .AutoInclude();

            base.OnModelCreating(modelBuilder);
        }
    }
}
