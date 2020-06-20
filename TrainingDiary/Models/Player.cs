using System;
using System.ComponentModel;

namespace TrainingDiary.Models
{
    public class Player : User
    {
        public Coach Coach {get; set;}
        public int Height {get; set;}
        [DisplayName("Date of birth")]
        public DateTime BirthDate {get; set;}
        [DisplayName("Licence number")]
        public int LicenceNo {get; set;}
        [DisplayName("Start number")]
        public int StartNo {get; set;}
        public float Weight {get; set;}
        [DisplayName("Shoe size")]
        public int ShoeSize{get; set;}
        public int Size {get; set;}
        [DisplayName("Exmination deadline")]
        public DateTime ExaminationDeadline {get; set;}
        public bool Injured {get; set;}
    }
}