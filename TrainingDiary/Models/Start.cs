using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using TrainingDiary.Enums;

namespace TrainingDiary.Models
{
    public class Start : BaseEvent
    {
        [DisplayName("Place")]
        public string Place {get; set;}
        [DisplayName("Result")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm\\:ss\\,FFF}")]
        public TimeSpan Result {get; set;}
        [DisplayName("Distance")]
        public float Distance {get; set;}
        [DisplayName("Location")]
        public string Location {get; set;}
        [DisplayName("Coach comment")]
        public string CoachComment {get;set;}
        [DisplayName("Player comment")]
        public string PlayerComment {get;set;}

        public Start()
        {
            Type = EventType.Start;
            CalendarColor = ColorTranslator.ToHtml(Color.Red);
        }
    }
}