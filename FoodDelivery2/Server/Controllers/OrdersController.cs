using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery2.Server.Data;
using FoodDelivery2.Shared.Domain;
using FoodDelivery2.Server.IRepository;

namespace FoodDelivery2.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		//private readonly ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		//public OrdersController(ApplicationDbContext context)
		public OrdersController(IUnitOfWork unitOfWork)

		{
			//_context = context;
			_unitOfWork = unitOfWork;
		}

		//GET: api/Orders
	   [HttpGet]
		//public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
		public async Task<IActionResult> GetOrders()
		{
			var orders = await _unitOfWork.Orders.GetAll(includes: q => q.Include(x => x.Customer).Include(x => x.Driver).Include(x => x.FoodOrder1).Include(x => x.FoodOrder1.Food).Include(x => x.FoodOrder2).Include(x => x.FoodOrder2.Food).Include(x => x.FoodOrder3).Include(x => x.FoodOrder3.Food));

			//if (_context.Orders == null)
			if (orders == null)
			{
				return NotFound();
			}
			//return await _context.Orders.ToListAsync();
			return Ok(orders);
		}
		//[HttpGet]
		//public async Task<IActionResult> GetOrders()
		//{
		//	var orders = await _unitOfWork.Orders.GetAll(includes: q =>
		//		q.Include(x => x.Customer)
		//		.Include(x => x.Driver)
		//		.Include(x => x.Review)
		//		.Include(x => x.FoodOrder1)
		//		.Include(x => x.FoodOrder2)
		//		.Include(x => x.FoodOrder3));

		//	if (orders == null)
		//	{
		//		return Ok(new List<Order>());
		//	}

		//	return Ok(orders);
		//}

		// GET: api/Orders/5
		[HttpGet("{id}")]
		//public async Task<ActionResult<Order>> GetOrder(int id)
		public async Task<IActionResult> GetOrder(int id)
		{

			var order = await _unitOfWork.Orders.Get(q => q.Id == id);
			//if (_context.Orders == null)

			if (order == null)
			{
				return NotFound();
			}

			return Ok(order);
		}

		// PUT: api/Orders/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrder(int id, Order order)
		{
			if (id != order.Id)
			{
				return BadRequest();
			}

			//_context.Entry(order).State = EntityState.Modified;
			_unitOfWork.Orders.Update(order);

			try
			{
				//await _context.SaveChangesAsync();
				await _unitOfWork.Save(HttpContext);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await OrderExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Orders
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Order>> PostOrder(Order order)
		{
			//if (_context.Orders == null)
			//{
			//    return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
			//}
			//  _context.Orders.Add(order);
			//  await _context.SaveChangesAsync();
			await _unitOfWork.Orders.Insert(order);
			await _unitOfWork.Save(HttpContext);

			return CreatedAtAction("GetOrder", new { id = order.Id }, order);
		}

		// DELETE: api/Orders/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			//if (_context.Orders == null)
			//{
			//    return NotFound();
			//}
			//var order = await _context.Orders.FindAsync(id);
			var order = _unitOfWork.Orders.Get(q => q.Id == id);

			if (order == null)
			{
				return NotFound();
			}

			//_context.Orders.Remove(order);
			//await _context.SaveChangesAsync();
			await _unitOfWork.Orders.Delete(id);
			await _unitOfWork.Save(HttpContext);

			return NoContent();
		}

		private async Task<bool> OrderExists(int id)
		{
			//return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
			var order = await _unitOfWork.Orders.Get(q => q.Id == id);
			return order != null;
		}
	}
}