using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace TrainingDiary.Models
{
    public class Coach : User
    {
        public List<Player> PlayersList { get; set; } = new List<Player>();
    }
}