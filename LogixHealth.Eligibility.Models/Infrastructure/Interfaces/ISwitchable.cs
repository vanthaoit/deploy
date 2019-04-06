using LogixHealth.Eligibility.Models.Infrastructure.Enums;

namespace LogixHealth.Eligibility.Models.Infrastructure.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}