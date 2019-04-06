using LogixHealth.Eligibility.Models.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogixHealth.Eligibility.Models.Entities
{
    [Table("AuditTrails")]
    public class AuditTrails : DomainEntity<int>
    {
        public AuditTrails()
        {
        }

        public AuditTrails(int id, string tableName,
            int recordId, string field,
            string oldValue, string newValue,
            DateTime changeDateTime, string userAction,
            Guid userId)
        {
            Id = id;
            TableName = tableName;
            RecordId = recordId;
            Field = field;
            OldValue = oldValue;
            NewValue = newValue;
            ChangeDateTime = changeDateTime;
            UserAction = userAction;
            UserId = userId;
        }

        [MaxLength(75)]
        [Required]
        public string TableName { get; set; }

        [Required]
        public long RecordId { get; set; }

        [Required]
        [MaxLength(75)]
        public string Field { get; set; }

        [Required]
        public string OldValue { get; set; }

        [Required]
        public string NewValue { set; get; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime ChangeDateTime { set; get; }

        [Required]
        [MaxLength(255)]
        public string UserAction { set; get; }

        [Required]
        public Guid UserId { set; get; }
    }
}