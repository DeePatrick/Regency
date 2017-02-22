using Regency.Models;
using System.Collections.Generic;

namespace Regency.ViewModels
{
    public class BookingFormViewModel
    {

        public IEnumerable<Customer> Customers { get; set; }


        public IEnumerable<Actor> Actors { get; set; }

        public Booking Booking { get; set; }
    }
}


