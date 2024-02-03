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
	public class FoodOrdersController : ControllerBase
	{
		//private readonly ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		//public FoodOrdersController(ApplicationDbContext context)
		public FoodOrdersController(IUnitOfWork unitOfWork)

		{
			//_context = context;
			_unitOfWork = unitOfWork;
		}

		// GET: api/FoodOrders
		[HttpGet]
		//public async Task<ActionResult<IEnumerable<FoodOrder>>> GetFoodOrders()
		public async Task<IActionResult> GetFoodOrders()
		{
			var foodorders = await _unitOfWork.FoodOrders.GetAll(includes: q => q.Include(x => x.Food));

			//if (_context.FoodOrders == null)
			if (foodorders == null)
			{
				return NotFound();
			}
			//return await _context.FoodOrders.ToListAsync();
			return Ok(foodorders);
		}

		// GET: api/FoodOrders/5
		[HttpGet("{id}")]
		//public async Task<ActionResult<FoodOrder>> GetFoodOrder(int id)
		public async Task<IActionResult> GetFoodOrder(int id)
		{

			var foodorder = await _unitOfWork.FoodOrders.Get(q => q.Id == id);
			//if (_context.FoodOrders == null)

			if (foodorder == null)
			{
				return NotFound();
			}

			return Ok(foodorder);
		}

		// PUT: api/FoodOrders/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFoodOrder(int id, FoodOrder foodorder)
		{
			if (id != foodorder.Id)
			{
				return BadRequest();
			}

			//_context.Entry(foodorder).State = EntityState.Modified;
			_unitOfWork.FoodOrders.Update(foodorder);

			try
			{
				//await _context.SaveChangesAsync();
				await _unitOfWork.Save(HttpContext);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await FoodOrderExists(id))
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

		// POST: api/FoodOrders
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<FoodOrder>> PostFoodOrder(FoodOrder foodorder)
		{
			//if (_context.FoodOrders == null)
			//{
			//    return Problem("Entity set 'ApplicationDbContext.FoodOrders'  is null.");
			//}
			//  _context.FoodOrders.Add(foodorder);
			//  await _context.SaveChangesAsync();
			await _unitOfWork.FoodOrders.Insert(foodorder);
			await _unitOfWork.Save(HttpContext);

			return CreatedAtAction("GetFoodOrder", new { id = foodorder.Id }, foodorder);
		}

		// DELETE: api/FoodOrders/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFoodOrder(int id)
		{
			//if (_context.FoodOrders == null)
			//{
			//    return NotFound();
			//}
			//var foodorder = await _context.FoodOrders.FindAsync(id);
			var foodorder = _unitOfWork.FoodOrders.Get(q => q.Id == id);

			if (foodorder == null)
			{
				return NotFound();
			}

			//_context.FoodOrders.Remove(foodorder);
			//await _context.SaveChangesAsync();
			await _unitOfWork.FoodOrders.Delete(id);
			await _unitOfWork.Save(HttpContext);

			return NoContent();
		}

		private async Task<bool> FoodOrderExists(int id)
		{
			//return (_context.FoodOrders?.Any(e => e.Id == id)).GetValueOrDefault();
			var foodorder = await _unitOfWork.FoodOrders.Get(q => q.Id == id);
			return foodorder != null;
		}
	}
}
