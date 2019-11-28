using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.ViewModels
{
     public class ProductViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public int CompanyId { get; set; }
        public string CompanyTitle { get; set; }

        public int BrandId { get; set; }
        public string BrandTitle { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
