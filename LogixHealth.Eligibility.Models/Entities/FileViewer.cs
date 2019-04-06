using LogixHealth.Eligibility.Models.Infrastructure.Enums;
using LogixHealth.Eligibility.Models.Infrastructure.Interfaces;
using LogixHealth.Eligibility.Models.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogixHealth.Eligibility.Models.Entities
{
    [Table("FileViewers")]
    public class FileViewer : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public FileViewer()
        {
        }

        public FileViewer(int id, string name,
            string lineCount, string ptCount,
            string legend, DateTime modifiedDate,
            string modifiedBy, DateTime createdDate,
            string createdBy,
            int auditTrailId, int noteId, int clientId, int fileTypeId, int voucherId,
            Status status
            )
        {
            Id = id;
            Name = name;
            Legend = legend;
            LineCount = lineCount;
            PTCount = ptCount;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            AuditTrailId = auditTrailId;
            NoteId = noteId;
            ClientId = clientId;
            FileTypeId = fileTypeId;
            VoucherId = voucherId;
            Status = status;
        }

        public string Name { set; get; }

        [MaxLength(256)]
        public string LineCount { get; set; }

        [MaxLength(256)]
        public string PTCount { get; set; }

        [MaxLength(256)]
        public string Legend { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { set; get; }
        public string CreatedBy { set; get; }

        [Required]
        public int AuditTrailId { get; set; }

        [ForeignKey("AuditTrailId")]
        public virtual AuditTrails AuditTrail { get; set; }

        [Required]
        public int NoteId { get; set; }

        [ForeignKey("NoteId")]
        public virtual Note Note { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [Required]
        public int FileTypeId { get; set; }

        [ForeignKey("FileTypeId")]
        public virtual FileType FileType { get; set; }

        [Required]
        public int VoucherId { get; set; }

        [ForeignKey("VoucherId")]
        public virtual Voucher Voucher { get; set; }

        public Status Status { set; get; }
    }
}