using LogixHealth.Eligibility.Models.Infrastructure.Enums;
using LogixHealth.Eligibility.Models.Infrastructure.Interfaces;
using LogixHealth.Eligibility.Models.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogixHealth.Eligibility.Models.Entities
{
    [Table("Vouchers")]
    public class Voucher : DomainEntity<int>, ISwitchable
    {
        public Voucher()
        {
        }

        public Voucher(int id, string content, Status status)
        {
            Id = id;
            Content = content;
            Status = status;
        }

        public string Content { get; set; }

        public Status Status { get; set; }
    }
}