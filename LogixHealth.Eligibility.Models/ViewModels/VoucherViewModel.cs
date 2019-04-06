using LogixHealth.Eligibility.Models.Infrastructure.Enums;

namespace LogixHealth.Eligibility.Models.ViewModels
{
    public class VoucherViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public Status Status { get; set; }
    }
}