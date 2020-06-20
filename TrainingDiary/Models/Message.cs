using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainingDiary.Models
{
    public class Message : IEntity
    {
        private readonly ILazyLoader lazyLoader;
        private User sender;
        private User receiver;

        public Message()
        {

        }

        public Message(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }

        public int Id {get; set;}
        public string Title { get; set; }
        public string Content {get; set;}
        public bool IsReadBySender { get; set; }
        public bool IsReadByReceiver { get; set; }

        [DisplayName("Send date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy hh:mm}")]
        public DateTime SendDate {get; set;}

        /// <summary>
        /// Delete this
        /// </summary>
        [DisplayName("Receive date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy hh:mm}")]
        public DateTime ReceiveDate {get; set;}
        public User Sender
        {
            get => lazyLoader.Load(this, ref sender);
            set => sender = value;
        }
        public User Receiver
        {
            get => lazyLoader.Load(this, ref receiver);
            set => receiver = value;
        }
    }
}