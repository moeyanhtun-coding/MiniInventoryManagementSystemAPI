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

        //product list
        [HttpGet]
        public IActionResult ProductList()
        {
            List<ProductModel> lst = _db.Query<ProductModel>(ProductQuery.productList);
            return Ok(lst);
        }

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
        [HttpPatch("{id}")]
        public IActionResult ProductUpdate(int id, ProductModel product)
        {
            var itemFind = _db.QueryFirstOrDefault<ProductModel>(
                ProductQuery.productGet,
                new ProductModel { ProductId = id }
            );
            if (itemFind is null)
            {
                return NotFound("No Data Found");
            }
            string conditions = string.Empty;
    

            ProductModel item = new ProductModel();
            if (!string.IsNullOrEmpty(product.ProductName))
            {
                item.ProductName = product.ProductName;
                conditions += " [ProductName] = @ProductName, ";
            }
            if (product.ProductQuantity.HasValue)
            {
                item.ProductQuantity = product.ProductQuantity;
                conditions += " [ProductQuantity] = @ProductQuantity, ";
            }
            if (product.ProductPrice.HasValue)
            {
                item.ProductPrice = product.ProductPrice;
                conditions += " [ProductPrice] = @ProductPrice, ";
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            item.ProductId = id;
            string query =
        $@"UPDATE [dbo].[Tbl_Product]
                   SET {conditions}
                 WHERE ProductId = @ProductId";

            int result = _db.Execute(query, item);
            string message = result > 0 ? "Product Update Success" : "Product Update Fail";
            return Ok(message);
        }


    }
}
