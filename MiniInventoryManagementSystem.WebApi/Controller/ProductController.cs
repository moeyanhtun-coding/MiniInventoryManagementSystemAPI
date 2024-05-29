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
        private readonly DapperService _dapperService = new DapperService(
            ConnectionStrings.sqlConnectionStringBuilder.ConnectionString
        );

        //product list
        [HttpGet("ProductList")]
        public IActionResult ProductList()
        {
            List<ProductModel> lst = _dapperService.Query<ProductModel>(ProductQuery.ProductList);
            return Ok(lst);
        }

        //product create
        [HttpPost("ProductCreate")]
        public IActionResult ProductCreate(ProductModel product)
        {
            int result = _dapperService.Execute(ProductQuery.ProductCreate, product);
            var message = result > 0 ? "Product Create Success" : "Product Create Fail";
            return Ok(message);
        }

        //product Edit
        [HttpGet("ProductEdit/{id}")]
        public IActionResult ProductGet(int id)
        {
            ProductModel item = _dapperService.QueryFirstOrDefault<ProductModel>(
                ProductQuery.ProductGet,
                new ProductModel { ProductId = id }
            );
            if (item is null)
                return NotFound("Data Not Found");
            return Ok(item);
        }

        //product Update
        [HttpPatch("ProductUpdate/{id}")]
        public IActionResult ProductUpdate(int id, ProductModel product)
        {
            var itemFind = _dapperService.QueryFirstOrDefault<ProductModel>(
                ProductQuery.ProductGet,
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

            int result = _dapperService.Execute(query, item);

            string message = result > 0 ? "Product Update Success" : "Product Update Fail";

            return Ok(message);
        }

        //product Delete
        [HttpDelete("ProductDelete/{id}")]
        public IActionResult ProductDelete(int id)
        {
            ProductModel item = _dapperService.QueryFirstOrDefault<ProductModel>(
                ProductQuery.ProductGet,
                new ProductModel { ProductId = id }
            );
            if (item is null)
                return NotFound("Data Not Found");
            int result = _dapperService.Execute(ProductQuery.ProductDelete, item);
            string message = result > 0 ? "Product Delete Success" : "Product Delete Fail";
            return Ok(message);
        }

        //product search
        [HttpGet("getProductItemsFiltered/{productName}")]
        public IActionResult ProductSearch(string productName)
        {
            List<ProductModel> lst = _dapperService.Query<ProductModel>(
                ProductQuery.ProductSearch,
                new ProductModel { ProductName = $"%{productName}%" }
            );
            if (lst.Count == 0)
                return NotFound("No Data Found");
            return Ok(lst);
        }
    }
}
