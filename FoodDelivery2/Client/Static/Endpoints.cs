using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery2.Client.Static
{
	public static class Endpoints
	{
		private static readonly string Prefix = "api";

		public static readonly string CustomersEndpoint = $"{Prefix}/customers";
		public static readonly string DriversEndpoint = $"{Prefix}/drivers";
		public static readonly string FAQsEndpoint = $"{Prefix}/faqs";
		public static readonly string FoodOrdersEndpoint = $"{Prefix}/foodorders";
		public static readonly string FoodsEndpoint = $"{Prefix}/foods";
		public static readonly string MenusEndpoint = $"{Prefix}/menus";
		public static readonly string OrdersEndpoint = $"{Prefix}/orders";
		public static readonly string PaymentsEndpoint = $"{Prefix}/payments";
		public static readonly string PromoCodesEndpoint = $"{Prefix}/promocodes";
		public static readonly string RestaurantsEndpoint = $"{Prefix}/restaurants";
		public static readonly string ReviewsEndpoint = $"{Prefix}/reviews";

	}
}
