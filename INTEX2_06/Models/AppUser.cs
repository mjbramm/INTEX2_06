using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace INTEX2_06.Models
{
    public class AppUser : IdentityUser
    {
        public int? CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? ResidenceCountry { get; set; }
        public string? Gender { get; set; }
        public float? Age { get; set; }
    }
}