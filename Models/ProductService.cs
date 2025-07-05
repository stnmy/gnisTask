using System.ComponentModel.DataAnnotations;

namespace gnisTask.Models
{
    public class ProductService
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        public string Unit { get; set; }
    }
}
