using Microsoft.EntityFrameworkCore;
using FoodDelivery2.Shared.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Drawing;

namespace FoodDelivery2.Server.Configurations.Entities
{
	public class RestaurantSeedConfiguration : IEntityTypeConfiguration<Restaurant>
	{
		public void Configure(EntityTypeBuilder<Restaurant> builder)
		{
			builder.HasData(
				new Restaurant
				{
					Id = 1,
					RestoName = "DTF",
					Location = "Tampines Mall",
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now,
					CreatedBy = "System",
					UpdatedBy = "System"
				},
				new Restaurant
				{
					Id = 2,
					RestoName = "Mala Wok",
					Location = "Bedok Corner",
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now,
					CreatedBy = "System",
					UpdatedBy = "System"
				}
			);
		}
	}
}
