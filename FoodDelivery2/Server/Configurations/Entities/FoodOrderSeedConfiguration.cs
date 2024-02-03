using FoodDelivery2.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery2.Server.Configurations.Entities
{
	public class FoodOrderSeedConfiguration : IEntityTypeConfiguration<FoodOrder>
	{
		public void Configure(EntityTypeBuilder<FoodOrder> builder)
		{
			builder.HasData(
				new FoodOrder
				{
					Id = 1,
					FoodId = 1,
					Qty = 0,
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now,
					CreatedBy = "System",
					UpdatedBy = "System"
				}

			); ;
		}
	}
}