using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using TrainingDiary.Enums;

namespace TrainingDiary.Models
{
    public class Camp : BaseEvent
    {
        private readonly ILazyLoader lazyLoader;
        private List<FullWorkout> trainingList = new List<FullWorkout>();

        public string Location { get; set; }
		public List<FullWorkout> TrainingList 
        {
            get => lazyLoader.Load(this, ref trainingList);
            set => trainingList = value;
        }

        public Camp()
        {
            Type = EventType.Camp;
            CalendarColor = ColorTranslator.ToHtml(Color.Green);
        }

        Camp(ILazyLoader lazyLoader) : this()
        {
            this.lazyLoader = lazyLoader;
        }
    }
}