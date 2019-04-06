using LogixHealth.Eligibility.Models.Infrastructure.Enums;

namespace LogixHealth.Eligibility.Models.ViewModels
{
    public class PayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Status Status { set; get; }
    }
}