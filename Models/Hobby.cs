using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamThree.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId {get;set;}

        [Required(ErrorMessage="Must Provide A Hobby Name!")]
        public string HobbyName {get; set;}

        [Required(ErrorMessage="Description of Hobby Is Required!")]
        
        public string Description {get;set;}

        public int NumLikes {get;set;}

        public DateTime CreatedAt{get;set;}= DateTime.Now;
        public DateTime UpdatedAt{get;set;}= DateTime.Now;
        public int UserId {get;set;}

        public User Creator {get;set;}
        public List<UserHobby> UserHobbies {get;set;}


    }
}