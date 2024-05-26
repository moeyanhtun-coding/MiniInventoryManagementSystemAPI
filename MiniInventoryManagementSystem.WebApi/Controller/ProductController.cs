using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniInventoryManagementSystem.WebApi.Model;

namespace MiniInventoryManagementSystem.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDbConnection _db = new SqlConnection(
            ConnectionStrings.sqlConnectionStringBuilder.ConnectionString
        );

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {

        }
    }
}
