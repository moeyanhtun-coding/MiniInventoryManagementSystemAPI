namespace MiniInventoryManagementSystem.WebApi.Query
{
    public class ProductQuery
    {
        public static string ProductList { get; } =
            @"SELECT [ProductId]
              ,[ProductName]
              ,[ProductQuantity]
              ,[ProductPrice]
              FROM [dbo].[Tbl_Product]";

        public static string ProductCreate { get; } =
            @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[ProductQuantity]
           ,[ProductPrice])
     VALUES
           (@ProductName
           ,@ProductQuantity
           ,@ProductPrice)";

        public static string ProductUpdate { get; } =
            @"UPDATE [dbo].[Tbl_Product]
               SET [ProductName] = @ProductName
                  ,[ProductQuantity] = @ProductQuantity
                  ,[ProductPrice] = @ProductPrice
             WHERE ProductId = @ProductId";

        public static string ProductGet { get; } =
            @"SELECT [ProductId]
              ,[ProductName]
              ,[ProductQuantity]
              ,[ProductPrice]
              FROM [dbo].[Tbl_Product]
              WHERE ProductId = @ProductId";

        public static string ProductDelete { get; } =
            @"DELETE FROM [dbo].[Tbl_Product]
            WHERE ProductId = @ProductId";

        public static string ProductSearch { get; } =
            @"SELECT * FROM [dbo].[Tbl_Product]
                WHERE ProductName LIKE @ProductName";
    }
}
