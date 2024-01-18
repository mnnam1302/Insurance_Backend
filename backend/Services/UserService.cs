using AutoMapper;
using backend.DTO.User;
using backend.IRepositories;
using backend.Models;
using backend.Responses;
using System.Runtime.CompilerServices;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<bool> CheckUserExists(int id)       
        {
            return _userRepository.Exists(id);
        }

        public BasePagingResponse<UserDTO> GetAllPaging(int page, int pageSize)
        {
            var result = _userRepository.GetMultiPaging(x => x.Status, 
                                                        out int totalRowSelected, 
                                                        out int totalRow, 
                                                        out int totalPage, 
                                                        page, 
                                                        pageSize);

            var users = _mapper.Map<List<UserDTO>>(result);

            var response = new BasePagingResponse<UserDTO>
            {
                Data = users,                           // Danh sách user
                TotalItemSelected = totalRowSelected,   // Số lượng record trả về
                TotalItems = totalRow,                  // Tổng số record trong db
                PageSize = pageSize,                    // Page size
                CurrentPage = page,                       // Current page
                TotalPages = totalPage                   // Total page
            };
            return response;
        }

        public async Task<UserDTO?> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            var result = _mapper.Map<UserDTO>(user);
            return result;
        }

        public async Task<UserDTO?> GetUserById(int userId)
        {
            //return await _userRepository.GetUserById(userId);
            var result = await _userRepository.Get(userId);

            var response = _mapper.Map<UserDTO>(result);
            return response;
        }

        public async Task<BaseCommandResponse> Register(CreateUserDTO userDTO)
        {
            // Convert DTO -> models

            // Check các điều kiện cần thiết
            // 1. User must be equal or greater than 18 years old
            // 2. Email tồn tại
            // 3. CardIdentification tồn tại
            var response = new BaseCommandResponse();

            if (userDTO.DateOfBirth > DateTime.Now.AddYears(-18))
            {
                response.Success = false;
                response.Message = "Creation failed.";
                response.Errors = new List<string> { "User must be equal or greater than 18 years old." };
                return response;
            }

            var userExists_ByEmail = await _userRepository.GetUserByEmail(userDTO.Email);
            if (userExists_ByEmail != null)
            {
                response.Success = false;
                response.Message = "Creation failed.";
                response.Errors = new List<string> { "Email already exists." };
                return response;
            }

            var userExists_ByIdentification = await _userRepository.GetUserByCardIdentication(userDTO.CardIdentification);
            if(userExists_ByIdentification != null)
            {
                response.Success = false;
                response.Message = "Creation failed.";
                response.Errors = new List<string> { "Card identification already exists." };
                return response;
            }

            var user = _mapper.Map<User>(userDTO);
            var result = await _userRepository.CreateUser(user);

            if (result != null)
            {
                response.Success = true;
                response.Message = "Creation successful";
                response.Id = result.UserId;
            }

            return response;
        }

        public async Task<UserDTO?> UpdateUserById(UpdateUserDTO userDTO) 
        {
            var user = await _userRepository.Get(userDTO.UserId);

            _mapper.Map(userDTO, user);

            var result = await _userRepository.Update(user);

            var response = _mapper.Map<UserDTO>(result);
            return response;
        }
    }
}
 