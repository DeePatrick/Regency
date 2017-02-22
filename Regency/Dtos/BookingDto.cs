using System.Collections.Generic;

namespace Regency.Dtos
{
    public class BookingDto
    {
        public int CustomerId { get; set; }
        public List<int> ActorIds { get; set; }
        public int Id { get; internal set; }
    }

}