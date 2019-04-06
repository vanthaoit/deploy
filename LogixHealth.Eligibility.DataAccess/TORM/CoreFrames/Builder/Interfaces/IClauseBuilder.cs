using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Builder.Interfaces
{
    public interface IClauseBuilder
    {
        string Sql();
        bool IsClean { get; set; }
    }
}
