using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ExamThree.Models
{
    public class User 
    {
        [Key]
        public int UserId{get;set;}

        [Required(ErrorMessage="Must Provide A First Name!")]
        [MinLength(2, ErrorMessage= "Name Must Be At Least 2 Characters!")]
        public string FirstName {get;set;}
        
        [Required(ErrorMessage="Must Provide A Last Name!")]
        [MinLength(2, ErrorMessage="Name Must Be At Least 2 Characters!")]
        public string LastName {get;set;}

        [Required(ErrorMessage="Must Provide A User Name!")]

        [MinLength(3, ErrorMessage="User Name Must Be At Least 3 Characters!")]
        [MaxLength(15,ErrorMessage="User Name Cannot Be Longer Than 15 Characters!")]
        public string UserName {get;set;}
        
        // [Required(ErrorMessage="Must Provide A Valid Email!")]
        // [EmailAddress(ErrorMessage="Email Must Be Valid")]
        // public string Email {get;set;}
        
        // [Required(ErrorMessage="Must Provide a Date Of Birth!")]
        // [DataType(DataType.Date, ErrorMessage="Must Provide A Date of Birth!")]
        // public DateTime DoB {get;set;}
        
        [Required(ErrorMessage="Must Provide A Password!")]
        [DataType(DataType.Password, ErrorMessage="Must Provide A Password")]
        [MinLength(8, ErrorMessage="Password Must Be At Least 8 Characters!")]
        public string Password {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;}= DateTime.Now;


        public List<Hobby> Hobbies{get;set;}
        public List<UserHobby> UserHobbies{get;set;}

        [NotMapped]
        [Required(ErrorMessage="Passwords Must Match!")]
        [DataType(DataType.Password, ErrorMessage="Passwords Must Match!")]
        [MinLength(8, ErrorMessage="Password Must Be At Least 8 Characters!")]
        [Compare("Password", ErrorMessage="Passwords Must Match!")]
        public string ConfirmPassword {get;set;}
    }
}