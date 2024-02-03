using FoodDelivery2.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery2.Server.Configurations.Entities
{
	public class PromoCodeSeedConfiguration : IEntityTypeConfiguration<PromoCode>
	{
		public void Configure(EntityTypeBuilder<PromoCode> builder)
		{
			builder.HasData(
				new PromoCode
				{
					Id = 1,
					PromoName = "NIL",
					Amount = 0,
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now,
					CreatedBy = "System",
					UpdatedBy = "System"
				}

			); ;
		}
	}
}