using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;

namespace TrainingDiary.Models
{
    public class User : IdentityUser
    {
        [DisplayName("First name")]
        public string FirstName {get; set;}
        [DisplayName("Last name")]
        public string LastName {get; set;}
    }
}