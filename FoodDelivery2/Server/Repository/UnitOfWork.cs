using FoodDelivery2.Server.Repository;
using FoodDelivery2.Server.Data;
using FoodDelivery2.Server.IRepository;
using FoodDelivery2.Server.Models;
using FoodDelivery2.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodDelivery2.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
		private IGenericRepository<Customer> _customers;
		private IGenericRepository<Driver> _drivers;
        private IGenericRepository<FAQ> _faqs;
        private IGenericRepository<Food> _foods;
        private IGenericRepository<FoodOrder> _foodorders;
        private IGenericRepository<Menu> _menus;
        private IGenericRepository<Order> _orders;
		private IGenericRepository<Payment> _payments;
		private IGenericRepository<PromoCode> _promocodes;
		private IGenericRepository<Restaurant> _restaurants;
		private IGenericRepository<Review> _reviews;

		private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

		public IGenericRepository<Customer> Customers
	=> _customers ??= new GenericRepository<Customer>(_context);
		public IGenericRepository<Driver> Drivers
            => _drivers ??= new GenericRepository<Driver>(_context);
        public IGenericRepository<FAQ> FAQs
            => _faqs ??= new GenericRepository<FAQ>(_context);
        public IGenericRepository<Food> Foods
            => _foods ??= new GenericRepository<Food>(_context);
        public IGenericRepository<FoodOrder> FoodOrders
            => _foodorders ??= new GenericRepository<FoodOrder>(_context);
        public IGenericRepository<Menu> Menus
            => _menus ??= new GenericRepository<Menu>(_context);
        public IGenericRepository<Order> Orders
            => _orders ??= new GenericRepository<Order>(_context);
		public IGenericRepository<Payment> Payments
	=> _payments ??= new GenericRepository<Payment>(_context);
		public IGenericRepository<PromoCode> PromoCodes
	=> _promocodes ??= new GenericRepository<PromoCode>(_context);
		public IGenericRepository<Restaurant> Restaurants
	=> _restaurants ??= new GenericRepository<Restaurant>(_context);
		public IGenericRepository<Review> Reviews
	=> _reviews ??= new GenericRepository<Review>(_context);
		public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}