using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniInventoryManagementSystem.WebApi.Model
{
    [Table("Tbl_Product")]
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductQuantity { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}
