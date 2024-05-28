using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace MiniInventoryManagementSystem.WebApi.Model
{
    [Table("Tbl_Order")]
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string OrderInvoiceNo { get; set; }
        public decimal TotalAmount { get; set; }
    }

    [Table("Tbl_OrderDetail")]
    public class OrderDetailModel
    {
        [Key]
        public int OrderDetailId { get; set; }
        public string OrderInvoiceNo { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

public class OrderRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderResponse
{
    public string Message { get; set; }
    public string InvoiceNo { get; set; }
    public decimal TotalAmount { get; set; }

    [NotMapped]
    public string TotalAmountStr
    {
        get { return "$ " + TotalAmount; }
    }
}

public class GetOrderDetail
{
    public string OrderInvoiceNo { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string TotalAmountStr { get { return "$ " + TotalAmount; } }
}
