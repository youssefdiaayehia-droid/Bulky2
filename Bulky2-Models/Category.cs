using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bulky2_Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Required]
        [Range(1,100,ErrorMessage ="Out of rang must be between 1 and 100")]
        public int DisplayOrder { get; set; }
    }
}
