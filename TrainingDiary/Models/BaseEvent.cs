using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingDiary.Enums;

namespace TrainingDiary.Models
{
    public abstract class BaseEvent : IEntity
    {
        public int Id {get; set;}

        [DisplayName("Event name")]
        public string EventName {get; set;}

        [DisplayName("Start date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime StartDate {get; set;}

        [DisplayName("End date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime? EndDate {get; set;}
        public Player Player {get; set;}
        public Coach Coach {get; set;}
        public EventType Type { get; set; }

        [NotMapped]
        public string CalendarColor { get; set; }

        [NotMapped]
        public string PlayerEmail { get; set; }
    }
}