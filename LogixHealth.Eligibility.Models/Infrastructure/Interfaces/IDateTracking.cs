using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.Models.Infrastructure.Interfaces
{
    public interface IDateTracking
    {
        DateTime CreatedDate { set; get; }

        DateTime ModifiedDate { set; get; }
    }
}
