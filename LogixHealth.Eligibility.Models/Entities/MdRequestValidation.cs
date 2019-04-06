using LogixHealth.Eligibility.Models.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogixHealth.Eligibility.Models.Entities
{
    [Table("MdRequestValidation")]
    public class MdRequestValidation : DomainEntity<int>
    {
        public MdRequestValidation()
        {
        }

        public MdRequestValidation(int id, string validationCode
            , string description, bool isSelfPay
            , bool isInsurance, DateTime createdDateTime
            , DateTime modifiedDateTime, Guid modifiedBy)
        {
            Id = id;
            ValidationCode = validationCode;
            Description = description;
            IsSelfPay = isSelfPay;
            IsInsurance = isInsurance;
            CreatedDateTime = createdDateTime;
            ModifiedDateTime = modifiedDateTime;
            ModifiedBy = modifiedBy;
        }

        [Required]
        [MaxLength(15)]
        public string ValidationCode { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public bool IsSelfPay { get; set; }

        [Required]
        public bool IsInsurance { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDateTime { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDateTime { get; set; }

        [Required]
        public Guid ModifiedBy { get; set; }
    }
}