using FoodDelivery2.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery2.Server.Configurations.Entities
{
	public class FoodSeedConfiguration : IEntityTypeConfiguration<Food>
	{
		public void Configure(EntityTypeBuilder<Food> builder)
		{
			builder.HasData(
				new Food
				{
					Id = 1,
					FoodItem = "No Food Item",
					Size = "Nil",
					Price = 0,
					Halal = true,
					Calories = 0,
					Protein = 0,
					Fats = 0,
					Allergies = "Nil",
					Remarks = "Nil",
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now,
					CreatedBy = "System",
					UpdatedBy = "System"
				},
				new Food
				{
					Id = 2,
					FoodItem = "Burger",
					Size = "Small",
					Price = 3.50,
					Halal = true,
					Calories = 5000,
					Protein = 0.5,
					Fats = 0.55,
					Allergies = "Nil",
					Remarks = "Nil",
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now,
					CreatedBy = "System",
					UpdatedBy = "System"
				},
				new Food
				{
					Id = 3,
					FoodItem = "Mala",
					Size = "Large",
					Price = 3.50,
					Halal = true,
					Calories = 3000,
					Protein = 0.3,
					Fats = 0.15,
					Allergies = "Peanuts",
					Remarks = "Nil",
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now,
					CreatedBy = "System",
					UpdatedBy = "System"
				}
			);
		}
	}
}
