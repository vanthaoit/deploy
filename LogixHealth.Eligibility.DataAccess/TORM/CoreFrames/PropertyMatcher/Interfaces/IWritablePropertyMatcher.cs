using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.PropertyMatcher.Interfaces
{
    public interface IWritablePropertyMatcher
    {
        bool Test(Type type);
    }
}
