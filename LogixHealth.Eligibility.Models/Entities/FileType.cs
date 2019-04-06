using LogixHealth.Eligibility.Models.Infrastructure.Enums;
using LogixHealth.Eligibility.Models.Infrastructure.Interfaces;
using LogixHealth.Eligibility.Models.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogixHealth.Eligibility.Models.Entities
{
    [Table("FileTypes")]
    public class FileType : DomainEntity<int>, ISwitchable
    {
        public FileType()
        {
        }

        public FileType(int id, string typeCode, string content, Status status)
        {
            Id = id;
            TypeCode = typeCode;
            Content = content;
            Status = status;
        }

        [Required]
        [MaxLength(256)]
        public string TypeCode { set; get; }

        public string Content { set; get; }
        public Status Status { set; get; }
    }
}