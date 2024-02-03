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
	public class PromoCodesController : ControllerBase
	{
		//private readonly ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		//public PromoCodesController(ApplicationDbContext context)
		public PromoCodesController(IUnitOfWork unitOfWork)

		{
			//_context = context;
			_unitOfWork = unitOfWork;
		}

		// GET: api/PromoCodes
		[HttpGet]
		//public async Task<ActionResult<IEnumerable<PromoCode>>> GetPromoCodes()
		public async Task<IActionResult> GetPromoCodes()
		{
			var promocodes = await _unitOfWork.PromoCodes.GetAll();

			//if (_context.PromoCodes == null)
			if (promocodes == null)
			{
				return NotFound();
			}
			//return await _context.PromoCodes.ToListAsync();
			return Ok(promocodes);
		}

		// GET: api/PromoCodes/5
		[HttpGet("{id}")]
		//public async Task<ActionResult<PromoCode>> GetPromoCode(int id)
		public async Task<IActionResult> GetPromoCode(int id)
		{

			var promocode = await _unitOfWork.PromoCodes.Get(q => q.Id == id);
			//if (_context.PromoCodes == null)

			if (promocode == null)
			{
				return NotFound();
			}

			return Ok(promocode);
		}

		// PUT: api/PromoCodes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPromoCode(int id, PromoCode promocode)
		{
			if (id != promocode.Id)
			{
				return BadRequest();
			}

			//_context.Entry(promocode).State = EntityState.Modified;
			_unitOfWork.PromoCodes.Update(promocode);

			try
			{
				//await _context.SaveChangesAsync();
				await _unitOfWork.Save(HttpContext);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await PromoCodeExists(id))
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

		// POST: api/PromoCodes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<PromoCode>> PostPromoCode(PromoCode promocode)
		{
			//if (_context.PromoCodes == null)
			//{
			//    return Problem("Entity set 'ApplicationDbContext.PromoCodes'  is null.");
			//}
			//  _context.PromoCodes.Add(promocode);
			//  await _context.SaveChangesAsync();
			await _unitOfWork.PromoCodes.Insert(promocode);
			await _unitOfWork.Save(HttpContext);

			return CreatedAtAction("GetPromoCode", new { id = promocode.Id }, promocode);
		}

		// DELETE: api/PromoCodes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePromoCode(int id)
		{
			//if (_context.PromoCodes == null)
			//{
			//    return NotFound();
			//}
			//var promocode = await _context.PromoCodes.FindAsync(id);
			var promocode = _unitOfWork.PromoCodes.Get(q => q.Id == id);

			if (promocode == null)
			{
				return NotFound();
			}

			//_context.PromoCodes.Remove(promocode);
			//await _context.SaveChangesAsync();
			await _unitOfWork.PromoCodes.Delete(id);
			await _unitOfWork.Save(HttpContext);

			return NoContent();
		}

		private async Task<bool> PromoCodeExists(int id)
		{
			//return (_context.PromoCodes?.Any(e => e.Id == id)).GetValueOrDefault();
			var promocode = await _unitOfWork.PromoCodes.Get(q => q.Id == id);
			return promocode != null;
		}
	}
}