using LogixHealth.Eligibility.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.TORM.CoreFrames.Groups
{
    internal class WhereClauseCondition
    {
        public string Alias { get; set; }
        public string LeftSchema { get; set; }
        public string LeftTable { get; set; }
        public string Left { get; set; }
        public LogicalOperator LocigalOperator { get; set; }
        public string Operator { get; set; }
        public string Right { get; set; }

        public override string ToString()
        {
            var logicalOperatoer = this.LocigalOperator == LogicalOperator.NotSet
                                       ? string.Empty
                                       : this.LocigalOperator.ToString()
                                             .ToUpperInvariant();
            var selector = string.IsNullOrWhiteSpace(this.Alias)
                               ? $"[{this.LeftSchema}].[{this.LeftTable}].[{this.Left}]"
                               : $"[{this.Alias}].[{this.Left}]";
            return $"{logicalOperatoer} {selector} {this.Operator} {this.Right}"
                .Trim();
        }
    }
}
