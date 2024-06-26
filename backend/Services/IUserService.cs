﻿using backend.DTO.User;
using backend.Models;
using backend.Responses;

namespace backend.Services
{
    public interface IUserService
    {
        Task<BaseCommandResponse> Register(CreateUserDTO createUserDTO);

        Task<UserDTO?> GetUserById(int userId);

        Task<bool> CheckUserExists(int id);

        Task<UserDTO?> GetUserByEmail(string email);

        Task<UserDTO?> UpdateUserById(UpdateUserDTO userDTO);

        BasePagingResponse<UserDTO> GetAllPaging(int page, int pageSize);

        BasePagingResponse<UserDTO> SearchUserByEmail(string email, int page, int pageSize);

        Task<SummaryUserDTO> GetSummaryUser();
    }
}
