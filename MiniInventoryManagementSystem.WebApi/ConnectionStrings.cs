using System.Data.SqlClient;

namespace MiniInventoryManagementSystem.WebApi
{
    public class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "MYTDotNetCoreMiniInventoryManagementSystem",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,
        };
    }
}