
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Data;
using HotelManagement.Data.Models;
using AutoMapper;
using HotelManagement.Data.Dtos;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HotelsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
          if (_context.Hotels == null)
          {
              return NotFound();
          }
            return await _context.Hotels.ToListAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
          if (_context.Hotels == null)
          {
              return NotFound();
          }
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        [HttpGet("{name}/{rooms}/{checkIn} /{checkOut}")]

        public async Task<ActionResult<List<HotelDto>>> GetHotel(string name, string hotelName, DateTime date)
        {
            if (!(await _context.CheckInOut.AnyAsync(h => h.CheckInTime == date)&&
                await _context.CheckInOut.AnyAsync(h=>h.CheckOutTime ==date)))
                return NotFound();
            
           var locationRefId = await _context.CheckInOut
                .Where(l => l.CheckInTime == date)
                .Select(l=>l.Id).FirstOrDefaultAsync();

            var hotelRefId = await  _context.CheckInOut
                .Where(h=> h.CheckOutTime == date)
                .Select(h=>h.Id).FirstOrDefaultAsync();

            var filterDateFrom = date;                              //25-Jun-2022 00:00:00
            var filterDateTo = date.AddHours(24).AddSeconds(-1);

            var hotelQuery = _context.Hotels
                 .Where(h =>
                 h.CheckInRefId == locationRefId &&
                 h.CheckOutRefId == hotelRefId &&
                 h.CheckIn >= filterDateFrom && h.CheckIn<=filterDateTo);

            var hotelDto = await _mapper.ProjectTo<HotelDto>(hotelQuery).ToListAsync();

            if (!hotelDto.Any())
            
                return NotFound();
            
            return hotelDto;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
          if (_context.Hotels == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Hotels'  is null.");
          }
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
