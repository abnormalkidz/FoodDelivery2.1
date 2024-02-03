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
	public class DriversController : ControllerBase
	{
		//private readonly ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		//public DriversController(ApplicationDbContext context)
		public DriversController(IUnitOfWork unitOfWork)

		{
			//_context = context;
			_unitOfWork = unitOfWork;
		}

		// GET: api/Drivers
		[HttpGet]
		//public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
		public async Task<IActionResult> GetDrivers()
		{
			var drivers = await _unitOfWork.Drivers.GetAll();

			//if (_context.Drivers == null)
			if (drivers == null)
			{
				return NotFound();
			}
			//return await _context.Drivers.ToListAsync();
			return Ok(drivers);
		}

		// GET: api/Drivers/5
		[HttpGet("{id}")]
		//public async Task<ActionResult<Driver>> GetDriver(int id)
		public async Task<IActionResult> GetDriver(int id)
		{

			var driver = await _unitOfWork.Drivers.Get(q => q.Id == id);
			//if (_context.Drivers == null)

			if (driver == null)
			{
				return NotFound();
			}

			return Ok(driver);
		}

		// PUT: api/Drivers/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutDriver(int id, Driver driver)
		{
			if (id != driver.Id)
			{
				return BadRequest();
			}

			//_context.Entry(driver).State = EntityState.Modified;
			_unitOfWork.Drivers.Update(driver);

			try
			{
				//await _context.SaveChangesAsync();
				await _unitOfWork.Save(HttpContext);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await DriverExists(id))
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

		// POST: api/Drivers
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Driver>> PostDriver(Driver driver)
		{
			//if (_context.Drivers == null)
			//{
			//    return Problem("Entity set 'ApplicationDbContext.Drivers'  is null.");
			//}
			//  _context.Drivers.Add(driver);
			//  await _context.SaveChangesAsync();
			await _unitOfWork.Drivers.Insert(driver);
			await _unitOfWork.Save(HttpContext);

			return CreatedAtAction("GetDriver", new { id = driver.Id }, driver);
		}

		// DELETE: api/Drivers/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDriver(int id)
		{
			//if (_context.Drivers == null)
			//{
			//    return NotFound();
			//}
			//var driver = await _context.Drivers.FindAsync(id);
			var driver = _unitOfWork.Drivers.Get(q => q.Id == id);

			if (driver == null)
			{
				return NotFound();
			}

			//_context.Drivers.Remove(driver);
			//await _context.SaveChangesAsync();
			await _unitOfWork.Drivers.Delete(id);
			await _unitOfWork.Save(HttpContext);

			return NoContent();
		}

		private async Task<bool> DriverExists(int id)
		{
			//return (_context.Drivers?.Any(e => e.Id == id)).GetValueOrDefault();
			var driver = await _unitOfWork.Drivers.Get(q => q.Id == id);
			return driver != null;
		}
	}
}
