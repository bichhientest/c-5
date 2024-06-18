using Microsoft.EntityFrameworkCore;

namespace NET105_ASM.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderComboDetail> OrderComboDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ComboFoodItem> ComboFoodItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComboFoodItem>()
                .HasKey(cf => new { cf.ComboId, cf.FoodItemId });

            modelBuilder.Entity<ComboFoodItem>()
                .HasOne(cf => cf.Combo)
                .WithMany(c => c.ComboFoodItems)
                .HasForeignKey(cf => cf.ComboId)
                .OnDelete(DeleteBehavior.Restrict); // or Cascade if you prefer

            modelBuilder.Entity<ComboFoodItem>()
                .HasOne(cf => cf.FoodItem)
                .WithMany(f => f.ComboFoodItems)
                .HasForeignKey(cf => cf.FoodItemId)
                .OnDelete(DeleteBehavior.Restrict); // or Cascade if you prefer

            // Ensuring ComboId and FoodItemId are required (not nullable)
            modelBuilder.Entity<ComboFoodItem>()
                .Property(cf => cf.ComboId)
                .IsRequired();

            modelBuilder.Entity<ComboFoodItem>()
                .Property(cf => cf.FoodItemId)
                .IsRequired();
        }
    }
}
