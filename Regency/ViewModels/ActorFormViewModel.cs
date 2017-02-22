using System;
using System.ComponentModel.DataAnnotations;

namespace Regency.ViewModels
{
    public class ActorFormViewModel
    {
        public int? Id { get; set; }
        public byte[] ActorImage { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }


        public DateTime? DateActive { get; set; }

        [Required(ErrorMessage = "Date Joined is required")]
        public DateTime DateJoined { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Actor Details" : "Add Actor Details";
            }
        }

        public ActorFormViewModel()
        {
            Id = 0;
        }

    }
}



