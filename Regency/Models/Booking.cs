using System;

namespace Regency.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Actor Actor { get; set; }
        public int ActorId { get; set; }
        public DateTime DateBooked { get; set; }


    }
}






