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
	public class MenusController : ControllerBase
	{
		//private readonly ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		//public MenusController(ApplicationDbContext context)
		public MenusController(IUnitOfWork unitOfWork)

		{
			//_context = context;
			_unitOfWork = unitOfWork;
		}

		// GET: api/Menus
		[HttpGet]
		//public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
		public async Task<IActionResult> GetMenus()
		{
			var menus = await _unitOfWork.Menus.GetAll();

			//if (_context.Menus == null)
			if (menus == null)
			{
				return NotFound();
			}
			//return await _context.Menus.ToListAsync();
			return Ok(menus);
		}

		// GET: api/Menus/5
		[HttpGet("{id}")]
		//public async Task<ActionResult<Menu>> GetMenu(int id)
		public async Task<IActionResult> GetMenu(int id)
		{

			var menu = await _unitOfWork.Menus.Get(q => q.Id == id);
			//if (_context.Menus == null)

			if (menu == null)
			{
				return NotFound();
			}

			return Ok(menu);
		}

		// PUT: api/Menus/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutMenu(int id, Menu menu)
		{
			if (id != menu.Id)
			{
				return BadRequest();
			}

			//_context.Entry(menu).State = EntityState.Modified;
			_unitOfWork.Menus.Update(menu);

			try
			{
				//await _context.SaveChangesAsync();
				await _unitOfWork.Save(HttpContext);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await MenuExists(id))
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

		// POST: api/Menus
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Menu>> PostMenu(Menu menu)
		{
			//if (_context.Menus == null)
			//{
			//    return Problem("Entity set 'ApplicationDbContext.Menus'  is null.");
			//}
			//  _context.Menus.Add(menu);
			//  await _context.SaveChangesAsync();
			await _unitOfWork.Menus.Insert(menu);
			await _unitOfWork.Save(HttpContext);

			return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
		}

		// DELETE: api/Menus/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMenu(int id)
		{
			//if (_context.Menus == null)
			//{
			//    return NotFound();
			//}
			//var menu = await _context.Menus.FindAsync(id);
			var menu = _unitOfWork.Menus.Get(q => q.Id == id);

			if (menu == null)
			{
				return NotFound();
			}

			//_context.Menus.Remove(menu);
			//await _context.SaveChangesAsync();
			await _unitOfWork.Menus.Delete(id);
			await _unitOfWork.Save(HttpContext);

			return NoContent();
		}

		private async Task<bool> MenuExists(int id)
		{
			//return (_context.Menus?.Any(e => e.Id == id)).GetValueOrDefault();
			var menu = await _unitOfWork.Menus.Get(q => q.Id == id);
			return menu != null;
		}
	}
}