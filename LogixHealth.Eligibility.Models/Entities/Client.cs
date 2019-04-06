using LogixHealth.Eligibility.Models.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogixHealth.Eligibility.Models.Entities
{
    [Table("Clients")]
    public class Client : DomainEntity<int>
    {
        public Client()
        {
        }

        public Client(int id, string name, string content)
        {
            Id = id;
            Name = name;
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
    }
}