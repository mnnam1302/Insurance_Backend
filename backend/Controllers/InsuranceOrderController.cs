using backend.Attribute;
using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InsuranceOrderController : ControllerBase
    {
        private readonly IInsuranceOrderService _orderService;

        public InsuranceOrderController(IInsuranceOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                List<InsuranceOrder> order = await _orderService.GetAll();
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message});
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                InsuranceOrder? order = await _orderService.GetById(id);

                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> AddInsuranceOrder([FromBody] InsuranceOrderDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("invalid request");
            }
            try
            {
                InsuranceOrder? order = await _orderService.AddInsuranceOrder(dto);
                
                if (order == null)
                {
                    return BadRequest("Order not created, Please check your request!");
                }

                var o_dto = new InsuranceOrderDTO
                {
                    ContractId = order.ContractId,
                    TotalCost = order.TotalCost,
                    Description = order.Description,
                };

                return Ok(o_dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
