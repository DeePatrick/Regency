﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Regency.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }

        [Min18YearsIfMember]

        [Display(Name = "Membership Type")]
        [Required]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

    }
}