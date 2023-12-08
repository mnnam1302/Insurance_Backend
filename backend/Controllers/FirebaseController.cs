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
                //var stream = file.OpenReadStream();

                //var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_configuration["Firebase:ApiKey"]));
                //var auth = await authProvider.SignInWithEmailAndPasswordAsync(
                //                                            _configuration["Firebase:AuthEmail"], 
                //                                            _configuration["Firebase:AuthPassword"]);

                //var firebaseStorageOptions = new FirebaseStorageOptions
                //{
                //    AuthTokenAsyncFactory = () => Task.FromResult(_configuration["Firebase:ApiKey"]),
                //    ThrowOnCancel = true,
                //    // có thể set thêm timeout ở đây
                //};

                //// Replace 'your-firebase-storage-bucket-url' with your actual Firebase Storage bucket URL
                //var firebaseStorage = new FirebaseStorage(_configuration["Firebase:StorageBucket"]);

                //// Get name && extension
                //var name = file.FileName.Split(".")[0];
                //var extension = "." + file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
                //// Generate a unique name for the file or use the original name
                //// nên gắn thêm fileName cho dễ nhìn
                //string fileName = name + DateTime.Now.Ticks.ToString() + extension;

                //var imageUrl = await firebaseStorage
                //    .Child("images")
                //    .Child(fileName)
                //    .PutAsync(stream);
                //    //.DeleteAsync(stream);

                //// You can use imageUrl to store in your database or return to the client
                //return Ok(imageUrl);

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
