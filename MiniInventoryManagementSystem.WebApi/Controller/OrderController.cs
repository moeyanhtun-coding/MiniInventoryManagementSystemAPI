using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniInventoryManagementSystem.Shared;
using MiniInventoryManagementSystem.WebApi.Db;
using MiniInventoryManagementSystem.WebApi.Model;
using MiniInventoryManagementSystem.WebApi.Query;

namespace MiniInventoryManagementSystem.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _appDbContext = new AppDbContext();

        private readonly DapperService _dapperService = new DapperService(
            ConnectionStrings.sqlConnectionStringBuilder.ConnectionString
        );

        //order Create
        [HttpPost]
        public IActionResult OrderCreate(OrderRequest orderRequest)
        {
            var product = _appDbContext.Products.FirstOrDefault(x =>
                x.ProductId == orderRequest.ProductId
            );

            decimal total = (decimal)(product.ProductPrice * orderRequest.Quantity)!;

            var invoiceNo = DateTime.Now.ToString("yyMMddHHmmss");

            if (orderRequest.Quantity > product.ProductQuantity)
            {
                return NotFound($"{product.ProductName} " + "isn't enough left");
            }

            ProductModel productModel = new ProductModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductQuantity = product.ProductQuantity - orderRequest.Quantity,
                ProductPrice = product.ProductPrice,
            };

            OrderModel orderModel = new OrderModel()
            {
                ProductId = product.ProductId,
                OrderInvoiceNo = invoiceNo,
                TotalAmount = total
            };

            OrderDetailModel orderDetailModel = new OrderDetailModel()
            {
                OrderInvoiceNo = orderModel.OrderInvoiceNo,
                ProductId = orderModel.ProductId,
                Quantity = orderRequest.Quantity,
                Amount = (decimal)product.ProductPrice!,
                TotalAmount = (decimal)orderModel.TotalAmount
            };

            _appDbContext.Orders.Add(orderModel);
            _appDbContext.OrderDetails.Add(orderDetailModel);
            _appDbContext.SaveChanges();

            _dapperService.Execute(ProductQuery.ProductUpdate, productModel);

            OrderResponse orderResponse = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank you for your order!",
                TotalAmount = total,
            };
            return Ok(orderResponse);
        }

        [HttpGet("{invoiceNo}")]
        public IActionResult GetOrder(string invoiceNo)
        {
            var model = _dapperService
                .Query<GetOrderDetail>(
                    OrderQuery.OrderDetailGetQuery,
                    new OrderDetailModel { OrderInvoiceNo = invoiceNo }
                )
                .ToList();
            return Ok(model);
        }
    }
}
