using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Api.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreateOn { get; set; }
       
    }
}