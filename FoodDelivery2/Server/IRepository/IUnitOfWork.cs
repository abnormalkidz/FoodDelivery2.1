using FoodDelivery2.Server.IRepository;
using FoodDelivery2.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery2.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Driver> Drivers { get; }
        IGenericRepository<FAQ> FAQs { get; }
        IGenericRepository<Food> Foods { get; }
        IGenericRepository<FoodOrder> FoodOrders { get; }
        IGenericRepository<Menu> Menus { get; }
		IGenericRepository<Order> Orders { get; }
		IGenericRepository<Payment> Payments { get; }
		IGenericRepository<PromoCode> PromoCodes { get; }
		IGenericRepository<Restaurant> Restaurants { get; }
		IGenericRepository<Review> Reviews { get; }
	}
}