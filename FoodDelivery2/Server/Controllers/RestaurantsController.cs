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
    public class RestaurantsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurant = await _unitOfWork.Restaurants.GetAll();
            //return await _context.Makes.ToListAsync();
            return Ok(restaurant);

            //if (_context.Reviews == null)
            //{
            //return NotFound();
            //}
            //return await _context.Reviews.ToListAsync();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRestaurant(int id)
        {
            var restaurant = await _unitOfWork.Restaurants.Get(q => q.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);


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

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            //_context.Entry(review).State = EntityState.Modified;
            _unitOfWork.Restaurants.Update(restaurant);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ReviewExists(id))
                if (!await RestaurantExists(id))
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

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            //if (_context.Reviews == null)
            //{
            //return Problem("Entity set 'ApplicationDbContext.Reviews'  is null.");
            //}
            //_context.Reviews.Add(review);
            //await _context.SaveChangesAsync();

            await _unitOfWork.Restaurants.Insert(restaurant);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            //if (_context.Reviews == null)
            //{
            //return NotFound();
            //}

            // var review = await _context.Reviews.FindAsync(id);
            var restaurant = await _unitOfWork.Restaurants.Get(q => q.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            //_context.Reviews.Remove(review);
            //await _context.SaveChangesAsync();

            await _unitOfWork.Restaurants.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool ReviewExists(int id)
        //{
        // return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        private async Task<bool> RestaurantExists(int id)
        {
            var restaurant = await _unitOfWork.Restaurants.Get(q => q.Id == id);
            return restaurant != null;
        }
    }
}
