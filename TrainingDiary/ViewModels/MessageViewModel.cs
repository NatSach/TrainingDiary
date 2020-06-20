using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Models;

namespace TrainingDiary.ViewModels
{
    public class MessageViewModel
    {
        public List<Message> Inbox { get; set; }
        public List<Message> Outbox { get; set; }
        public List<Request> Requests { get; set; }
    }
}
