namespace MiniInventoryManagementSystem.WebApi.Query
{
    public class ProductQuery
    {
        public static string productList { get; } = 
            @"SELECT * FROM [dbo].[Tbl_Product]";

        public static string productDelete { get; } =
            @"DELETE FROM [dbo].[Tbl_Product]
            WHERE ProductId = @ProductId";

        public static string productCreate { get; } =
            @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[ProductQuantity]
           ,[ProductPrice])
     VALUES
           (@ProductName
           ,@ProductQuantity
           ,@ProductPrice)";

        public static string productGet { get; } =
            @"SELECT [ProductId]
              ,[ProductName]
              ,[ProductQuantity]
              ,[ProductPrice]
              FROM [dbo].[Tbl_Product]
              WHERE ProductId = @ProductId";
    }
}
