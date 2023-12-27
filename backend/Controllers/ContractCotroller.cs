using backend.Attribute;
using backend.DTO;
using backend.Extensions;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IUserService _userService;

        public ContractsController(IContractService contractService, IUserService userService)
        {
            _contractService = contractService;
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Contract> order = await _contractService.GetAll();
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

        //Thêm contract
        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> AddNewContract([FromForm] int registion_id)
        {
            
            if (registion_id <= 0)
            {
                return BadRequest("Request is not valid");
            }
            try
            {
                //Kiểm tra người mua có tồn tại không
                int userId;

                userId = HttpContext.GetUserId();
                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("Policyholder is not found");
                }

                var dto = new ContractDTO();
                dto.user_id = 1;
                dto.registration_id = registion_id;//userId

                // thêm hợp đồng
                var result = await _contractService.AddNewContract(dto);

                // kiểm tra hợp đồng thêm thành công không
                if (result == null)
                {
                    return BadRequest("Created registration is failed");
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // tìm kiếm contract thông qua user id
        [HttpGet("user")]
        [JwtAuthorize]
        public async Task<IActionResult> GetByUserId()
        {
            try
            {
                // Kiểm tra người mua có tồn tại không
                int userId;

                userId = HttpContext.GetUserId();
                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("Policyholder is not found");
                }

                //userId = 1;
                //Lấy danh sách contact theo user id
                var result = await _contractService.GetByUserId(userId);

                // kiểm tra tồn tại
                if (result == null)
                {
                    return BadRequest("Not Found");
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //tìm kiếm theo mã bảo hiềm
        [HttpGet("{insurance_code}")]
        public async Task<IActionResult> GetByInsuranceCode(string insurance_code)
        {
            if (insurance_code == null)
            {
                return BadRequest("please insert your insurance code");
            }
            try
            {
                // lấy contract theo mã bảo hiểm người dùng nhập
                Contract? result = await _contractService.GetByInsuranceCode(insurance_code);

                // kiểm tra tồn tại
                if (result == null)
                {
                    return BadRequest("No result found");
                }

                // đổ kết quả vào dto
                var dto = new ContractDTO
                {
                    contract_id = result.contract_id,
                    insurance_code = result.insurance_code,
                    signing_date = result.signing_date,
                    start_date = result.start_date,
                    end_date = result.end_date,
                    contract_status = result.contract_status,
                    initial_fee_per_turn = result.initial_fee_per_turn,
                    discount = result.discount,
                    total_fee = result.total_fee,
                    total_turn = result.total_turn,
                    periodic_fee = result.periodic_fee,
                    bonus_fee = result.total_fee,
                    beneficial_id = result.beneficial_id,
                    insurance_id = result.insurance_id,
                    user_id = result.user_id,
                    registration_id = result.registration_id,
                };

                return Ok(dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
