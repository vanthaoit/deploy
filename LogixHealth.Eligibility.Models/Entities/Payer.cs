using LogixHealth.Eligibility.Models.Infrastructure.Enums;
using LogixHealth.Eligibility.Models.Infrastructure.Interfaces;
using LogixHealth.Eligibility.Models.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogixHealth.Eligibility.Models.Entities
{
    [Table("Payers")]
    public class Payer : DomainEntity<int>, ISwitchable
    {
        public Payer()
        {
        }

        public Payer(int id, string name, Status status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public Status Status { set; get; }
    }
}