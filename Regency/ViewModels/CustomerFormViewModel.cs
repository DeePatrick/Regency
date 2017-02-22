using Regency.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Regency.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        [Required]
        public byte MembershipType { get; set; }
        public Customer Customer { get; set; }

    }
}


