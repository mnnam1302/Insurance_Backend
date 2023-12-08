using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceOrderController : ControllerBase
    {
        private readonly InsuranceDbContext insuranceDbContext;

        public InsuranceOrderController(InsuranceDbContext insuranceDbContext)
        {
            this.insuranceDbContext = insuranceDbContext;
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var orders = insuranceDbContext.InsuranceOrders.ToList();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var order = insuranceDbContext.InsuranceOrders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDTO = new InsuranceOrderDTO();
            orderDTO.Id = order.Id;
            orderDTO.ContractId = order.ContractId;
            orderDTO.TotalCost = order.TotalCost;
            orderDTO.TotalPayment = order.TotalPayment;
            orderDTO.Description = order.Description;
            orderDTO.Status = order.Status;
            orderDTO.PaymentDate = order.PaymentDate;

            return Ok(orderDTO);
        }

        [HttpPost]
        public IActionResult CreateInsuranceOrder([FromBody] CreateInsuranceOrder dto)
        {
            var orderDomain = new InsuranceOrder
            {
                ContractId = dto.ContractId,
                TotalCost = dto.TotalCost,
                TotalPayment = dto.TotalPayment,
                Description = dto.Description,
                Status = dto.Status,
                PaymentDate = null,
            };

            insuranceDbContext.InsuranceOrders.Add(orderDomain);
            insuranceDbContext.SaveChanges();

            InsuranceOrderDTO o_dto = new InsuranceOrderDTO()
            {
                Id = orderDomain.Id,
                ContractId = dto.ContractId,
                TotalCost = dto.TotalCost,
                TotalPayment = dto.TotalPayment,
                Description = dto.Description,
                Status = dto.Status,
                PaymentDate = null,
            };

            return CreatedAtAction(nameof(GetById), new { Id = o_dto.Id }, o_dto);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateOrder([FromRoute] int id, [FromBody] InsuranceOrderDTO dto)
        {
            var orderDomain = insuranceDbContext.InsuranceOrders.FirstOrDefault(x => x.Id == id);
            if (orderDomain == null)
            {
                return NotFound();
            }
            orderDomain.Status = dto.Status;
            insuranceDbContext.SaveChanges();

            var updated_order_dto = new UpdateInsuranceOrder()
            {
                Status = orderDomain.Status,
                PaymentDate = dto.PaymentDate,
            };
            return Ok(updated_order_dto);
        }
    }
}
