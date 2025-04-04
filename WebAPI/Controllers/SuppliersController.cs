using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using BLL.Services;

namespace WebAPI.Controllers
{

    [Route("[Controller]")]
    [ApiController]
    public class SuppliersController : Controller
    {
        IOrderManager orderManager;

        public SuppliersController(IOrderManager _orderManager)
        {
            orderManager = _orderManager;
        }

        [HttpPost ("AddSupplier")]
        public IActionResult AddSupplier([FromBody] M_Suppliers supplier)
        {
           if (supplier == null) {
                throw new ArgumentNullException(nameof(supplier));
            }
            if(supplier.Convert() == null)
            {
                return BadRequest();
            }

            orderManager.creatSupplierAsync(supplier.Convert());
            return Ok();

        }
        [HttpPost("LoginSupplier")]
        public async Task<IActionResult> LoginSupplier([FromBody] string company,  string phone)
        {
            if (company == null || phone == null)
            {
                throw new ArgumentNullException("Company or phone cannot be null");
            }

            bool loginResult = await orderManager.loginSupplierAsync(company, phone);

            if (loginResult)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("AllOrdersForSupplier")]
        public async Task<IActionResult> AllOrders(string company)
        {
            var orders = await orderManager.GetAllOrdersWithGoodsForSupplier(company);
            return Ok(orders);
        }

        [HttpPost("AddGoodsToSupplier")]
        public async Task<IActionResult> AddGoodsToSupplier([FromQuery] Dictionary<string, float> goodsWithQuantities,[FromBody]string company,int quantity)
        {
            var result = await orderManager.AddGoodsToSupplier(company, goodsWithQuantities, quantity);
            if (result)
            {
                return Ok("Goods added successfully.");
            }
            else
            {
                return BadRequest("Failed to add goods.");
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
