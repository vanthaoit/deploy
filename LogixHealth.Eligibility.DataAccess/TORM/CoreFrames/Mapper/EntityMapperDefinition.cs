using LogixHealth.Eligibility.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Mapper
{
    public class EntityMapperDefinition<TEntity> where TEntity : class, new()
    {
        public EntityMapperDefinition()
        {
            PropertySetters = new Dictionary<string, Action<TEntity, object>>();
            ColumnTypeMappings = new Dictionary<string, Type>();
        }

        public Dictionary<string, Action<TEntity, object>> PropertySetters { get; set; }
        public Dictionary<string, Type> ColumnTypeMappings { get; set; }
        public EntityActivator<TEntity> Activator { get; set; }
    }
}
