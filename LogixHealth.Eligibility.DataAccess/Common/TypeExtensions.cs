using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.Common
{
    public static class TypeExtensions
    {
        public static bool IsSimpleType(
            this Type type)
        {
            return
                type.IsPrimitive ||
                new[]
                {
                    typeof(string),
                    typeof(decimal)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}
