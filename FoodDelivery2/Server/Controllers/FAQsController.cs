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
	public class FAQsController : ControllerBase
	{
		//private readonly ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		//public FAQsController(ApplicationDbContext context)
		public FAQsController(IUnitOfWork unitOfWork)

		{
			//_context = context;
			_unitOfWork = unitOfWork;
		}

		// GET: api/FAQs
		[HttpGet]
		//public async Task<ActionResult<IEnumerable<FAQ>>> GetFAQs()
		public async Task<IActionResult> GetFAQs()
		{
			var faqs = await _unitOfWork.FAQs.GetAll();

			//if (_context.FAQs == null)
			if (faqs == null)
			{
				return NotFound();
			}
			//return await _context.FAQs.ToListAsync();
			return Ok(faqs);
		}

		// GET: api/FAQs/5
		[HttpGet("{id}")]
		//public async Task<ActionResult<FAQ>> GetFAQ(int id)
		public async Task<IActionResult> GetFAQ(int id)
		{

			var faq = await _unitOfWork.FAQs.Get(q => q.Id == id);
			//if (_context.FAQs == null)

			if (faq == null)
			{
				return NotFound();
			}

			return Ok(faq);
		}

		// PUT: api/FAQs/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFAQ(int id, FAQ faq)
		{
			if (id != faq.Id)
			{
				return BadRequest();
			}

			//_context.Entry(faq).State = EntityState.Modified;
			_unitOfWork.FAQs.Update(faq);

			try
			{
				//await _context.SaveChangesAsync();
				await _unitOfWork.Save(HttpContext);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await FAQExists(id))
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

		// POST: api/FAQs
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<FAQ>> PostFAQ(FAQ faq)
		{
			//if (_context.FAQs == null)
			//{
			//    return Problem("Entity set 'ApplicationDbContext.FAQs'  is null.");
			//}
			//  _context.FAQs.Add(faq);
			//  await _context.SaveChangesAsync();
			await _unitOfWork.FAQs.Insert(faq);
			await _unitOfWork.Save(HttpContext);

			return CreatedAtAction("GetFAQ", new { id = faq.Id }, faq);
		}

		// DELETE: api/FAQs/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFAQ(int id)
		{
			//if (_context.FAQs == null)
			//{
			//    return NotFound();
			//}
			//var faq = await _context.FAQs.FindAsync(id);
			var faq = _unitOfWork.FAQs.Get(q => q.Id == id);

			if (faq == null)
			{
				return NotFound();
			}

			//_context.FAQs.Remove(faq);
			//await _context.SaveChangesAsync();
			await _unitOfWork.FAQs.Delete(id);
			await _unitOfWork.Save(HttpContext);

			return NoContent();
		}

		private async Task<bool> FAQExists(int id)
		{
			//return (_context.FAQs?.Any(e => e.Id == id)).GetValueOrDefault();
			var faq = await _unitOfWork.FAQs.Get(q => q.Id == id);
			return faq != null;
		}
	}
}
