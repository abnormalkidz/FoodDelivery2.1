using CarRentalManagement.Server.Configurations.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using FoodDelivery2.Server.Configurations.Entities;
using FoodDelivery2.Server.Models;
using FoodDelivery2.Shared.Domain;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FoodDelivery2.Server.Data
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Driver> Drivers { get; set; }
		public DbSet<FAQ> FAQs { get; set; }
		public DbSet<Food> Foods { get; set; }
		public DbSet<FoodOrder> FoodOrders { get; set; }
		public DbSet<Menu> Menus { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<PromoCode> PromoCodes { get; set; }
		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<Review> Reviews { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new RestaurantSeedConfiguration());
			builder.ApplyConfiguration(new FoodSeedConfiguration());
			builder.ApplyConfiguration(new FoodOrderSeedConfiguration());
			builder.ApplyConfiguration(new RoleSeedConfiguration());
			builder.ApplyConfiguration(new UserSeedConfiguration());
			builder.ApplyConfiguration(new UserRoleSeedConfiguration());
			builder.ApplyConfiguration(new PromoCodeSeedConfiguration());
            builder.ApplyConfiguration(new MenuSeedConfiguration());

        }

	}

}
