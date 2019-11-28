using System;
using System.ComponentModel.DataAnnotations;

namespace Challenge.ViewModels
{
    public class BrandViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
