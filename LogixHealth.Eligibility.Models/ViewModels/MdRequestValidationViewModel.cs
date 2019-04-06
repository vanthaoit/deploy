using System;

namespace LogixHealth.Eligibility.Models.ViewModels
{
    public class MdRequestValidationViewModel
    {
        public int Id { get; set; }
        public string ValidationCode { get; set; }

        public string Description { get; set; }

        public bool IsSelfPay { get; set; }

        public bool IsInsurance { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public Guid ModifiedBy { get; set; }
    }
}