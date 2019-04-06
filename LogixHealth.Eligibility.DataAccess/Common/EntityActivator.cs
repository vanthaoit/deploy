using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.Common
{
    public delegate T EntityActivator<T>();

    public static class EntityActivator
    {
        public static EntityActivator<T> GetActivator<T>()
        {
            var type = typeof(T);
            var ctor = type.GetConstructor(Type.EmptyTypes);

            var lambda =
                Expression.Lambda(typeof(EntityActivator<T>), Expression.New(ctor));

            var compiled = (EntityActivator<T>)lambda.Compile();
            return compiled;
        }
    }
}
