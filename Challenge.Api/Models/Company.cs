using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Api.Models
{
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string UniqueName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime CreateOn { get; set; }
    }
}