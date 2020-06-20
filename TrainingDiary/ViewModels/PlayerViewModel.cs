using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Models;

namespace TrainingDiary.ViewModels
{
    public class PlayerViewModel
    {
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Licence number")]
        public int LicenceNo { get; set; }
        [DisplayName("Start number")]
        public int StartNo { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        [DisplayName("Shoe size")]
        public int ShoeSize { get; set; }
        public int Size { get; set; }
        [DisplayName("Exmination deadline")]
        public DateTime ExaminationDeadline { get; set; }
        public bool Injured { get; set; }

        public PlayerViewModel()
        {

        }
        public PlayerViewModel(Player player)
        {
            FirstName = player.FirstName;
            LastName = player.LastName;
            Email = player.Email;
            BirthDate = player.BirthDate;
            LicenceNo = player.LicenceNo;
            StartNo = player.StartNo;
            Weight = player.Weight;
            Height = player.Height;
            ShoeSize = player.ShoeSize;
            Size = player.Size;
            ExaminationDeadline = player.ExaminationDeadline;
            Injured = player.Injured;
        }
    }
}

