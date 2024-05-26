using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniInventoryManagementSystem.Shared;
using MiniInventoryManagementSystem.WebApi.Model;
using MiniInventoryManagementSystem.WebApi.Query;

namespace MiniInventoryManagementSystem.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DapperService _db = new DapperService(
            ConnectionStrings.sqlConnectionStringBuilder.ConnectionString
        );

        //product create
        [HttpPost]
        public IActionResult ProductCreate(ProductModel product)
        {
            int result = _db.Execute(ProductQuery.productCreate, product);
            var message = result > 0 ? "Product Create Success" : "Product Create Fail";
            return Ok(message);
        }

        //product Edit
        [HttpGet("{id}")]
        public IActionResult ProductGet(int id)
        {
            ProductModel item = _db.QueryFirstOrDefault<ProductModel>(
                ProductQuery.productGet,
                new ProductModel { ProductId = id }
            );
            if (item is null)
                return NotFound("Data Not Found");
            return Ok(item);
        }

        //product Update
        [HttpPatch]
    }
}
