using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace TELpro.Models
{
    public class Product
    {
        
        [Required]
        [Key]
         public int Id { get; set; }    
      
        [Required(ErrorMessage = "name should not be empty")]
        public string Name { get; set; }
        [StringLength(500,ErrorMessage ="the para should not exceed 500 letter")]
        public string Description { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string ImagePath { get; set; }


    }
}
