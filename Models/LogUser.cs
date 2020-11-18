using System;
using System.ComponentModel.DataAnnotations;

namespace ExamThree.Models
{
    public class LogUser
    {
        [Required(ErrorMessage="Information Invalid!")]
        public string LogUserName {get;set;}

        [Required(ErrorMessage="Information Invalid!")]
        [DataType(DataType.Password)]
        public string LogPassword {get; set;}

    }
}