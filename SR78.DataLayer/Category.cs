using System;
using System.ComponentModel.DataAnnotations;

namespace SR78.DataLayer
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name field is required!")]
        [MaxLength(50)]
        public string CategoryName { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
