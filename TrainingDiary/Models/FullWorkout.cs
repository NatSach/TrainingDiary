using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Drawing;
using TrainingDiary.Enums;

namespace TrainingDiary.Models
{
    public class FullWorkout : BaseEvent
    {
        private readonly ILazyLoader lazyLoader;
        private Workout plan;
        private Workout realization;

        public FullWorkout()
        {
            Type = EventType.Fullworkout;
            CalendarColor = ColorTranslator.ToHtml(Color.Blue);
        }

        FullWorkout(ILazyLoader lazyLoader) : this()
        {
            this.lazyLoader = lazyLoader;
        }

        public Workout Plan
        {
            get => lazyLoader.Load(this, ref plan);
            set => plan = value;
        }
        public Workout Realization
        {
            get => lazyLoader.Load(this, ref realization);
            set => realization = value;
        }

    }
}