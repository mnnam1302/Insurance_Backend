using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirebaseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly FirebaseStorage _firebaseStorage;
        private readonly FirebaseAuthProvider _authProvider;

        public FirebaseController(IConfiguration configuration, 
                                    FirebaseStorage firebaseStorage,
                                    FirebaseAuthProvider authProvider)
        {
            _configuration = configuration;
            _firebaseStorage = firebaseStorage;
            _authProvider = authProvider;
        }


        private async Task AuthenticateFirebase()
        {
            await _authProvider.SignInWithEmailAndPasswordAsync(
                _configuration["Firebase:AuthEmail"],
                _configuration["Firebase:AuthPassword"]);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0) {
                return BadRequest("File is not valid");
            }
            try
            {
                // Clean code
                var stream = file.OpenReadStream();

                await AuthenticateFirebase();

                // Get properties file
                var name = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);

                string fileName = name + DateTime.Now.Ticks.ToString() + extension;

                var imageUrl = await _firebaseStorage
                    .Child("images")
                    .Child(fileName)
                    .PutAsync(stream);

                return Ok(imageUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
