using AspNetCore.Email;
using backend.DTO;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VerificationPasswordController: ControllerBase
    {
        private readonly IVerificationPasswordService _verificationPasswordService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public VerificationPasswordController(IVerificationPasswordService verificationPasswordService,
                                                IUserService userService,
                                                IEmailService emailService)
        {
            _verificationPasswordService = verificationPasswordService;
            _userService = userService;
            _emailService = emailService;
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailRequestDTO request)
        {
            if (request.Email == null)
            {
                return BadRequest("Request is not valid");
            }
            try
            {
                var user = await _userService.GetUserByEmail(request.Email);

                if (user == null)
                {
                    return NotFound("User is not valid!");
                }

                string otp = await _verificationPasswordService.GenerateOTP(user.UserId);

                var EmailMessage = new EmailRequestDTO
                {
                    Email = user.Email,
                    Message = otp,
                };
                // Send email to gmail using Memikit
                await _emailService.SendEmaiAsync(EmailMessage);

                return Ok("Sucess. Let's check your email to get otp code");
            } catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">
        /// 1. Email
        /// 2. OtpCode
        /// </param>
        /// <returns></returns>
        [HttpPost("verify-password")]
        public async Task<IActionResult> VerificationOtpCode([FromBody] EmailRequestDTO request)
        {
            if (request.Email == null)
            {
                return BadRequest("Request is not valid");
            }
            try
            {
                var user = await _userService.GetUserByEmail(request.Email);

                if (user == null)
                {
                    return NotFound("User is not valid!");
                }

                // Kiểm tra mã OTP
                // 1. Check thời gian expired
                // 2. Check mã OTP gửi có chính xác
                await _verificationPasswordService.VerificationOTP(user.UserId, request.Message);

                return Ok("Sucess. Verify OTP code right, let's reset password");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">
        /// 1. Email
        /// 2. OtpCode
        /// </param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] EmailRequestDTO request)
        {
            if (request == null || request.Email == null || request.Message == null)
            {
                return BadRequest("Request is not valid");
            }
            try
            {
                var user = await _userService.GetUserByEmail(request.Email);

                if (user == null)
                {
                    return NotFound("User is not valid!");
                }

                // reset password mới
                await _verificationPasswordService.ResetPassword(user.UserId, request.Message);

                return Ok("Sucess. Reset password success");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
