using AutoMapper;
using Regency.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Regency.Dtos;

namespace Regency.Controllers.Api
{
    public class BookingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public BookingsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetBookings(string query = null)
        {
            var bookingsQuery = _context.Bookings
                .Include(c => c.Customer)
                .Include(c => c.Actor);

            //if (!String.IsNullOrWhiteSpace(query))
            //    bookingsQuery = bookingsQuery.Where(c => c.Customer.Name.Contains(query));

            var bookingDtos = bookingsQuery
                .ToList()
                .Select(Mapper.Map<Booking, BookingDto>);

            return Ok(bookingDtos);
        }


        // GET /api/bookings/1
        public IHttpActionResult GetBooking(int id)
        {
            var booking = _context.Bookings.SingleOrDefault(c => c.Id == id);

            if (booking == null)
                return NotFound();

            return Ok(Mapper.Map<Booking, BookingDto>(booking));
        }

        // POST /api/bookings
        [HttpPost]
        public IHttpActionResult CreateBooking(BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var booking = Mapper.Map<BookingDto, Booking>(bookingDto);
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            bookingDto.Id = booking.Id;
            return Created(new Uri(Request.RequestUri + "/" + booking.Id), bookingDto);
        }

        // PUT /api/bookings/1
        [HttpPut]
        public IHttpActionResult UpdateBooking(int id, BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var bookingInDb = _context.Bookings.SingleOrDefault(c => c.Id == id);

            if (bookingInDb == null)
                return NotFound();

            Mapper.Map(bookingDto, bookingInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/bookings/1
        [HttpDelete]
        public IHttpActionResult DeleteBooking(int id)
        {
            var bookingInDb = _context.Bookings.SingleOrDefault(c => c.Id == id);

            if (bookingInDb == null)
                return NotFound();

            _context.Bookings.Remove(bookingInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
