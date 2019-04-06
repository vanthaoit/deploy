using LogixHealth.Eligibility.Models.Infrastructure.Enums;
using System;

namespace LogixHealth.Eligibility.Models.ViewModels
{
    public class FileViewerViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; }

        public string LineCount { get; set; }

        public string PTCount { get; set; }

        public string Legend { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { set; get; }
        public string CreatedBy { set; get; }

        public Status Status { set; get; }
    }
}