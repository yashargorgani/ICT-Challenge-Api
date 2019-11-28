using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Api.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public DateTime CreateOn { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}