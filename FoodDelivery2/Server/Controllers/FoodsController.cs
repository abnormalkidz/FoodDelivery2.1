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
	public class FoodsController : ControllerBase
	{
		//private readonly ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		//public FoodsController(ApplicationDbContext context)
		public FoodsController(IUnitOfWork unitOfWork)

		{
			//_context = context;
			_unitOfWork = unitOfWork;
		}

		// GET: api/Foods
		[HttpGet]
		//public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
		public async Task<IActionResult> GetFoods()
		{
			var foods = await _unitOfWork.Foods.GetAll();

			//if (_context.Foods == null)
			if (foods == null)
			{
				return NotFound();
			}
			//return await _context.Foods.ToListAsync();
			return Ok(foods);
		}

		// GET: api/Foods/5
		[HttpGet("{id}")]
		//public async Task<ActionResult<Food>> GetFood(int id)
		public async Task<IActionResult> GetFood(int id)
		{

			var food = await _unitOfWork.Foods.Get(q => q.Id == id);
			//if (_context.Foods == null)

			if (food == null)
			{
				return NotFound();
			}

			return Ok(food);
		}

		// PUT: api/Foods/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFood(int id, Food food)
		{
			if (id != food.Id)
			{
				return BadRequest();
			}

			//_context.Entry(food).State = EntityState.Modified;
			_unitOfWork.Foods.Update(food);

			try
			{
				//await _context.SaveChangesAsync();
				await _unitOfWork.Save(HttpContext);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await FoodExists(id))
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

		// POST: api/Foods
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Food>> PostFood(Food food)
		{
			//if (_context.Foods == null)
			//{
			//    return Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
			//}
			//  _context.Foods.Add(food);
			//  await _context.SaveChangesAsync();
			await _unitOfWork.Foods.Insert(food);
			await _unitOfWork.Save(HttpContext);

			return CreatedAtAction("GetFood", new { id = food.Id }, food);
		}

		// DELETE: api/Foods/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFood(int id)
		{
			//if (_context.Foods == null)
			//{
			//    return NotFound();
			//}
			//var food = await _context.Foods.FindAsync(id);
			var food = _unitOfWork.Foods.Get(q => q.Id == id);

			if (food == null)
			{
				return NotFound();
			}

			//_context.Foods.Remove(food);
			//await _context.SaveChangesAsync();
			await _unitOfWork.Foods.Delete(id);
			await _unitOfWork.Save(HttpContext);

			return NoContent();
		}

		private async Task<bool> FoodExists(int id)
		{
			//return (_context.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
			var food = await _unitOfWork.Foods.Get(q => q.Id == id);
			return food != null;
		}
	}
}
