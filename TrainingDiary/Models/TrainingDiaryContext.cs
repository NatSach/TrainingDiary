using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrainingDiary.Models
{
    public class TrainingDiaryContext : IdentityDbContext
    {
        public TrainingDiaryContext (DbContextOptions<TrainingDiaryContext> options)
            :base(options)
            {

            }
        
        public DbSet<BaseEvent> BaseEvent {get;set;}
        public DbSet<Coach> Coach {get;set;}
        public DbSet<FullWorkout> FullWorkout {get;set;}
        public DbSet<Message> Message {get;set;}
        public DbSet<Player> Player {get;set;}
        public DbSet<Start> Start {get;set;}
        public DbSet<User> User {get;set;}
        public DbSet<Workout> Workout {get;set;}
        public DbSet<Camp> Camp {get;set; }
        public DbSet<Request> Request { get; set; }
    }
}