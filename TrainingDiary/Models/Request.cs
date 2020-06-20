using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Enums;

namespace TrainingDiary.Models
{
    public class Request : Message
    {
        [DisplayName("Status")]
        public RequestStatus RequestStatus { get; set; }

        public Request()
        {

        }
        public Request(string firstname, string lastname)
        {
            Title = "Request to join as a player.";
            Content = $"{firstname} {lastname} asks you to join as your player. " +
                "This will allow cooperation between you to be established. You can accept or reject the request.";
        }

        public Request(ILazyLoader lazyLoader) : base(lazyLoader)
        {

        }
    }
}
