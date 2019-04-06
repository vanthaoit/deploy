using LogixHealth.Eligibility.Models.Infrastructure.Enums;
using LogixHealth.Eligibility.Models.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogixHealth.Eligibility.Models.Entities.System
{
    [Table("ApplicationUsers")]
    public class ApplicationUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(Guid id, string fullName, string userName,
            string email, string phoneNumber, string avatar, Status status)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            Status = status;
        }

        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Status Status { get; set; }
    }
}