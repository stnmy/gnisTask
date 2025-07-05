using System.ComponentModel.DataAnnotations;

namespace gnisTask.Models
{
    public class MeetingDetail
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        public string Unit { get; set; }
    }
}
