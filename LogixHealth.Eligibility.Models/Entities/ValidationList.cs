using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogixHealth.Eligibility.Models.Entities
{
    [Table("ValidationList")]
    public class ValidationList
    {
        public ValidationList()
        {

        }
        public ValidationList(string validationCode)
        {
            ValidationCode = validationCode;
        }
        [Key]
        [MaxLength(15)]
        public string ValidationCode { get; set; }
    }
}
