using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.Common
{
    public enum Comparison
    {
        NotSet = 0,
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Like,
        NotLike
    }
    public enum Aggregation
    {
        None = 0,
        Avg,
        Count,
        Max,
        Min,
        Sum
    }
    public enum JoinType
    {
        None = 0,
        Inner,
        LeftOuter,
        RightOuter,
        Full,
        Cross
    }
    public enum LogicalOperator
    {
        NotSet = 0,
        And,
        Or
    }

    public enum OrderByDirection
    {
        Ascending = 0,
        Descending
    }

    public enum FilterGroupType
    {
        Where = 0,
        And,
        Or
    }
    internal enum WhereClauseGroupType
    {
        Where = 0,
        And,
        Or
    }
}
