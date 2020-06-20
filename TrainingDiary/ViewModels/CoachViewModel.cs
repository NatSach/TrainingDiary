using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Models;

namespace TrainingDiary.ViewModels
{
    public class CoachViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public CoachViewModel()
        {

        }
        public CoachViewModel(Coach coach)
        {
            FirstName = coach.FirstName;
            LastName = coach.LastName;
            Email = coach.Email;
        }
    }
}
