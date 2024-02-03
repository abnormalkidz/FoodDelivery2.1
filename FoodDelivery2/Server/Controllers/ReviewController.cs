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
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var Reviews = await _unitOfWork.Reviews.GetAll(includes: q => q.Include(x => x.Driver).Include(x => x.Order.FoodOrder1).Include(x => x.Order.FoodOrder1.Food).Include(x => x.Order.FoodOrder2).Include(x => x.Order.FoodOrder2.Food).Include(x => x.Order.FoodOrder3).Include(x => x.Order.FoodOrder3.Food));
            //return await _context.Makes.ToListAsync();
            return Ok(Reviews);

        //if (_context.Reviews == null)
        //{
        //return NotFound();
        //}
        //return await _context.Reviews.ToListAsync();
    }

    // GET: api/Reviews/5
    [HttpGet("{id}")]
        public async Task<ActionResult> GetReview(int id)
        {
            var review = await _unitOfWork.Reviews.Get(q => q.Id == id);

            if(review == null)
            {
                return NotFound();
            }

            return Ok(review);


          //if (_context.Reviews == null)
          //{
              //return NotFound();
          //}
            //var review = await _context.Reviews.FindAsync(id);

           // if (review == null)
            //{
              //  return NotFound();
            //}

            //return review;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            //_context.Entry(review).State = EntityState.Modified;
            _unitOfWork.Reviews.Update(review);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ReviewExists(id))
                if(!await ReviewExists (id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            //if (_context.Reviews == null)
            //{
            //return Problem("Entity set 'ApplicationDbContext.Reviews'  is null.");
            //}
            //_context.Reviews.Add(review);
            //await _context.SaveChangesAsync();

            await _unitOfWork.Reviews.Insert(review);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            //if (_context.Reviews == null)
            //{
            //return NotFound();
            //}

            // var review = await _context.Reviews.FindAsync(id);
            var review = await _unitOfWork.Reviews.Get(q => q.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            //_context.Reviews.Remove(review);
            //await _context.SaveChangesAsync();

            await _unitOfWork.Reviews.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool ReviewExists(int id)
        //{
           // return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private async Task<bool> ReviewExists(int id)
        {
            var review = await _unitOfWork.Reviews.Get(q => q.Id == id);
            return review != null;
        }
    }
}
