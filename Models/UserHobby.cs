using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ExamThree.Models
{
    public class UserHobby
    {
        [Key]
        public int UserHobbyId{get;set;}
        public int UserId{get;set;}

        public int HobbyId{get;set;}
        public DateTime CreatedAt {get;set;}=DateTime.Now;
        public DateTime UpdatedAt {get;set;} =DateTime.Now;

        public User Creator{get;set;}
        public Hobby Hobby{get;set;}
    }
}