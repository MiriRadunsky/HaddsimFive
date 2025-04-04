
using BLL.Models;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{


    [Route("[Controller]")]
    [ApiController]
    public class StoreOwnerController : Controller
    {
        IOrderManager orderManager;

        public StoreOwnerController(IOrderManager _orderManager)
        {
            orderManager = _orderManager;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromQuery] Dictionary<string, int> goodsWithQuantities, [FromBody]string compay)
        {
            var idSupplier = await orderManager.GetIdSupplierByCompanyName(compay);

            M_Orders order = new M_Orders() { IdSuppliers = idSupplier };

            var result = await orderManager.CreateOrder(goodsWithQuantities, order.Convert());

            if (result)
            {
                return Ok("Order created successfully.");
            }
            else
            {
                return BadRequest("Failed to create order.");
            }
        }


        [HttpPut("ApproveOrder")]
        public async Task<IActionResult> ApproveOrder([FromQuery] int id)
        {
            var result = await orderManager.ApproveOrder(id);
            if (result)
            {
                return Ok("Order approved successfully.");
            }
            else
            {
                return BadRequest("Failed to approve order.");
            }
        }
    }
}
