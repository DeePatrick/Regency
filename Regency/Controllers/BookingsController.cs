using Regency.Models;
using Regency.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Regency.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Create()
        {
            var customers = _context.Customers.ToList();
            var actors = _context.Actors.ToList();

            var viewModel = new BookingFormViewModel
            {
                Customers = customers,
                Actors = actors,
                Booking = new Booking()
            };
            return View("New", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BookingFormViewModel
                {
                    Booking = booking,
                    Customers = _context.Customers.ToList(),
                    Actors = _context.Actors.ToList()

                };
                return View("New", viewModel);
            }
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bookings
                    .Single(c => c.Id == booking.Id);

                bookingInDb.CustomerId = booking.CustomerId;
                bookingInDb.ActorId = booking.ActorId;
                bookingInDb.Customer = booking.Customer;
                bookingInDb.Actor = booking.Actor;

                bookingInDb.DateBooked = booking.DateBooked;


            }
            _context.SaveChanges();
            return RedirectToAction("CompletedBooking", "Bookings");
        }

        [Authorize(Roles = RoleName.CanManageActors)]
        public ActionResult Index()
        {
            var bookings = _context.Bookings
                .Include(c => c.Customer)
                .Include(a => a.Actor)
                .ToList();
            //if (!User.IsInRole(RoleName.CanManageActors))
            //    return View("Index", bookings);

            return View("Index", bookings);
        }


        public ActionResult Edit(int id)
        {
            var booking = _context.Bookings
                .Include(c => c.Customer)
                .Include(c => c.Actor)
                .SingleOrDefault(c => c.Id == id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            var viewModel = new BookingFormViewModel
            {
                Booking = booking,
                Customers = _context.Customers.ToList(),
                Actors = _context.Actors.ToList()

            };

            return View("New", viewModel);
        }

        public ActionResult CompletedBooking()
        {
            return View();
        }
    }
}







