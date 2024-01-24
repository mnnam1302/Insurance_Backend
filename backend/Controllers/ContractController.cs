using backend.Attribute;
using backend.DTO.Contract;
using backend.Extensions;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
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

        /// <summary>
        /// Get all contracts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
        {
            try
            {
                var contracts = await _contractService.GetListContracts();
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        /// <summary>
        /// Get contract base on ContractId
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById([FromRoute] int id)
        {
            try
            {
                var contracts = await _contractService.GetContractById(id);
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        /// <summary>
        /// Get contracts by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetByUserId([FromQuery] int userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("Policyholder is not found");
                }

                var result = await _contractService.GetByUserId(userId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        ///// <summary>
        ///// Get Insurance base on Insurance code
        ///// </summary>
        ///// <param name="insurance_code"></param>
        ///// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetByInsuranceCode([FromQuery] string insurance_code)
        {
            if (insurance_code == null)
            {
                return BadRequest("please insert your insurance code");
            }
            try
            {
                var result = await _contractService.GetByInsuranceCode(insurance_code);

                if (result == null)
                {
                    return NotFound("Contract is not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        /// <summary>
        /// Create contract
        /// </summary>
        /// <param name="addContract"></param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> CreateContract([FromBody] CreateContractDTO addContract)
        {
            if (addContract == null)
            {
                return BadRequest("Request is not valid");
            }
            try
            {
                //Kiểm tra người mua có tồn tại không
                int userId = HttpContext.GetUserId();
                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("Policyholder is not found");
                }

                var contract_dto = new ContractDTO();
                contract_dto.UserId = userId;
                contract_dto.RegistrationId = addContract.Registration_Id;

                // thêm hợp đồng
                var result = await _contractService.CreateContract(contract_dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        /// <summary>
        /// Get summary totalFee contract over year, month
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummaryContract()
        {
            try
            {
                var result = await _contractService.GetSummaryContract();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}