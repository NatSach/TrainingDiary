using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Enums;

namespace TrainingDiary.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TrainingDiaryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TrainingDiaryContext>>()))
            {
                var coach = new Coach()
                {
                    FirstName = "Jan",
                    LastName = "Trenerowy",
                };
                var player = new Player()
                {
                    FirstName = "Mariusz",
                    LastName = "Zawodnikowy",
                    Coach = coach,
                    Height = 178,
                    BirthDate = DateTime.Now,
                    LicenceNo = 1234444,
                    StartNo = 12,
                    Weight = 76,
                    ShoeSize = 40,
                    Size = 36,
                    ExaminationDeadline = DateTime.Now,
                    Injured = false
                };
                var start = new Start()
                {
                    EventName = "Super zawodyyyy",
                    StartDate = new DateTime(2019, 10, 25, 15, 00, 00),
                    EndDate = new DateTime(2019, 10, 25, 18, 00, 00),
                    Player = player,
                    Coach = coach,
                    Type = EventType.Start,
                    Place = "najpierwsze",
                    Result = TimeSpan.Zero,
                    Distance = 5000,
                    Location = "Hajnówka",
                    CoachComment = "jesteś super, zawodnik!",
                    PlayerComment = "dzięki, trener! jestem super zawodnik!"
                };
                var workout = new Workout()
                {
                    Content = "to będzie ciężki trening",
                    OWB1 = (float)1.9,
                    OWB2 = 7,
                    WB3 = 8,
                    ZB = 3,
                    KR = 33,
                    GL = 2,
                    DL = 12,
                    Rhythm = 33,
                    RelativeSpeed = 12,
                    MaxSpeed = 12333,
                    Skip = 3,
                    MultipleJump = 99,
                    MS = 123,
                    BPG = 333,
                    KMStart = 1234,
                    Scamper = 44,
                    Comment = "ale się zmęczysz, marny zawodniku"
                };
                var workoutPlayer = new Workout()
                {                
                    Content = "to będzie ciężki trening",
                    OWB1 = (float)1.9,
                    OWB2 = 7,
                    WB3 = 8,
                    ZB = 3,
                    KR = 33,
                    GL = 2,
                    DL = 12,
                    Rhythm = 33,
                    RelativeSpeed = 12,
                    MaxSpeed = 12333,
                    Skip = 3,
                    MultipleJump = 99,
                    MS = 123,
                    BPG = 333,
                    KMStart = 1234,
                    Scamper = 44,
                    Comment = "dałem radę, super trenerze"
                };
                var fullworkout = new FullWorkout()
                {
                    EventName = "trening numer 007",
                    StartDate = new DateTime(2019, 10, 23, 15, 00, 00),
                    EndDate = new DateTime(2019, 10, 23, 18, 00, 00),
                    Player = player,
                    Coach = coach,
                    Type = EventType.Fullworkout,
                    Plan = workout,
                    Realization = workoutPlayer
                };

                var camp = new Camp()
                {
                    EventName = "Bardzo długi campus",
                    StartDate = new DateTime(2019, 11, 11, 11, 11, 11),
                    EndDate = new DateTime(2019, 11, 11, 11, 11, 11),
                    Player = player,
                    Coach = coach,
                    Type = EventType.Camp,
                    Location = "Super miejsce na obóz"
                };

                camp.TrainingList.Add(fullworkout);

                context.Player.Add(player);
                context.Coach.Add(coach);
                context.Start.Add(start);
                context.Workout.Add(workout);
                context.Workout.Add(workoutPlayer);
                context.FullWorkout.Add(fullworkout);
                context.Camp.Add(camp);


                context.SaveChanges();
            }
        }
    }
}
