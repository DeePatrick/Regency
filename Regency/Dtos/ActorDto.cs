using System;
using System.ComponentModel.DataAnnotations;

namespace Regency.Dtos
{
    public class ActorDto
    {
        public int Id { get; set; }
        public byte[] ActorImage { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }


        public DateTime? DateActive { get; set; }
        public DateTime DateJoined { get; set; }
    }
}