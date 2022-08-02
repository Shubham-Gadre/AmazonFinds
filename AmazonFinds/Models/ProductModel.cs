using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonFinds.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Link")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ProductLink { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public IFormFile ProductImage { get; set; }

        public string ProductImageUrl { get; set; }

    }
}
