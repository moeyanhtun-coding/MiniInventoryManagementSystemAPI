namespace MiniInventoryManagementSystem.WebApi.Query
{
    public class ProductQuery
    {
        public static string productCreate { get; } =
            @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[ProductQuantity]
           ,[ProductPrice])
     VALUES
           (@ProductName
           ,@ProductQuantity
           ,@ProductPrice)";
    }
}
