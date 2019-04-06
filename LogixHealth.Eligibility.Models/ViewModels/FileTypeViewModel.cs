using LogixHealth.Eligibility.Models.Infrastructure.Enums;

namespace LogixHealth.Eligibility.Models.ViewModels
{
    public class FileTypeViewModel
    {
        public int Id { get; set; }
        public string TypeCode { set; get; }

        public string Content { set; get; }
        public Status Status { set; get; }
    }
}