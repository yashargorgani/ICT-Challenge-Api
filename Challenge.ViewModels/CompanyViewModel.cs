using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.ViewModels
{
    public class CompanyViewModel
    {
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
