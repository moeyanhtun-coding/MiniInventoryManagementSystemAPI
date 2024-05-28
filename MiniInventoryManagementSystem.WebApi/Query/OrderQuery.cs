namespace MiniInventoryManagementSystem.WebApi.Query
{
    public class OrderQuery
    {
        public static string OrderDetailGetQuery =
            @"SELECT od.OrderInvoiceNo,p.ProductName,od.Quantity,
            p.ProductPrice,od.TotalAmount FROM  [dbo].[Tbl_OrderDetail] od
            INNER JOIN [dbo].[Tbl_Product] p on p.ProductId = od.ProductId
            Where od.OrderInvoiceNo = @OrderInvoiceNo";
    }
}
