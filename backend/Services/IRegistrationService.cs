﻿using backend.DTO.Registration;
using backend.Models;
using backend.Responses;

namespace backend.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationDTO> GetRegistrationById(int id);
        Task<BaseCommandResponse> CreateRegistrationInsurance(CreateRegistrationDTO registrationDTO);
        Task<RegistrationDTO> ChangeStatusRegistration(int id, UpdateStatusRegistrationDTO updateRegistration);

    }
}
