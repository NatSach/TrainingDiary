using Newtonsoft.Json;
using System;

namespace TrainingDiary.Models
{
    public class CalendarEvent
    {
        public CalendarEvent()
        {

        }

        public CalendarEvent(BaseEvent source)
        {
            Title = source.EventName;
            StartDate = source.StartDate;
            EndDate = source.EndDate;
            Url = $"/{source.GetType().Name}/Details/{source.Id}";
            Color = source.CalendarColor;
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("start")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("end")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        public override string ToString()
        {
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm";

            return JsonConvert.SerializeObject(this, jsonSettings);
        }
    }
}
