﻿using backend.DTO.BenefitDetail;
using backend.Models;

namespace backend.Services
{
    public interface IBenefitDetailService
    {
        Task<List<BenefitDetailDTO>> GetBenefitDetailByInsuranceId(int insuranceId);
    }
}
