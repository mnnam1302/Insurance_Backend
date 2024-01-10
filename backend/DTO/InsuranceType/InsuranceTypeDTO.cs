using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.DTO.InsuranceType
{
    public class InsuranceTypeDTO
    {
        public int InsuranceTypeId { get; set; }
        public string NameInsuranceType { get; set; }
    }
}
