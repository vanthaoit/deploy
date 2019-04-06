using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Mapper
{
    public interface IEntityMapper
    {
        IEnumerable<TEntity> Map<TEntity>(IDataReader reader) where TEntity : class, new();
    }
}
