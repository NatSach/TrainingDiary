using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Enums;
using TrainingDiary.Models;

namespace TrainingDiary.Services
{
    public class DbContextService : IDbContextService
    {
        private readonly TrainingDiaryContext _context;
        private readonly Func<BaseEvent, string, bool> GetByCoachId = (baseEvent, id) => baseEvent.Coach != null && baseEvent.Coach.Id == id;
        private readonly Func<BaseEvent, string, bool> GetByPlayerId = (baseEvent, id) => baseEvent.Player != null && baseEvent.Player.Id == id;

        public DbContextService(TrainingDiaryContext context)
        {
            _context = context;
        }

        public async Task AddObjectToDb(object newObject)
        {
            _context.Add(newObject);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateObjectInDb(object editedObject)
        {
            _context.Update(editedObject);
            await _context.SaveChangesAsync();
        }
        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<List<Player>> GetPlayersForCoach(string id)
        {
            List<Player> players = await _context.Player.Include(x => x.Coach).ToListAsync();

            if(players == null)
            {
                return null;
            }

            return players.Where(x => x.Coach != null && x.Coach.Id == id).ToList();
        }

        public async Task<List<BaseEvent>> GetBaseEvents()
        {
            return await _context.BaseEvent.Include(x => x.Coach).Include(x => x.Player).ToListAsync();
        }

        public async Task<List<BaseEvent>> GetBaseEventsForPlayer(string id)
        {
            List<BaseEvent> baseEvents = await GetBaseEvents();

            if (baseEvents == null)
            {
                return null;
            }

            return baseEvents.Where(x => GetByPlayerId(x, id)).ToList();
        }

        public async Task<List<BaseEvent>> GetBaseEventsForCoach(string id)
        {
            List<BaseEvent> baseEvents = await GetBaseEvents();

            if (baseEvents == null)
            {
                return null;
            }

            return baseEvents.Where(x => GetByCoachId(x, id)).ToList();
        }
        public async Task<List<Start>> GetStarts()
        {
            return await _context.Start.Include(x => x.Coach).Include(x => x.Player).ToListAsync();
        }
        public async Task<List<Start>> GetStartsForCoach(string id)
        {
            List<Start> starts = await GetStarts();

            if (starts == null)
            {
                return null;
            }

            return starts.Where(x => GetByCoachId(x, id)).ToList();
        }

        public async Task<List<Start>> GetStartsForPlayer(string id)
        {
            List<Start> starts = await GetStarts();

            if (starts == null)
            {
                return null;
            }

            return starts.Where(x => GetByPlayerId(x, id)).ToList();
        }

        public async Task<List<Workout>> GetWorkoutsAsync()
        {
            return await _context.Workout.ToListAsync();
        }

        public async Task<List<Camp>> GetCampsAsync()
        {
            return await _context.Camp.Include(x => x.Coach).Include(x => x.Player).ToListAsync();
        }

        public async Task<List<Camp>> GetCampsForCoach(string id)
        {
            List<Camp> camps = await GetCampsAsync();

            if (camps == null)
            {
                return null;
            }

            return camps.Where(x => GetByCoachId(x, id)).ToList();
        }

        public async Task<List<Camp>> GetCampsForPlayer(string id)
        {
            List<Camp> camps = await GetCampsAsync();

            if (camps == null)
            {
                return null;
            }

            return camps.Where(x => GetByPlayerId(x, id)).ToList();
        }

        public async Task<List<FullWorkout>> GetFullWorkoutsAsync()
        {
            return await _context.FullWorkout.Include(x => x.Coach).Include(x => x.Player).ToListAsync();
        }

        public async Task<List<FullWorkout>> GetFullWorkoutsForCoach(string id)
        {
            List<FullWorkout> fullWorkouts = await GetFullWorkoutsAsync();

            if (fullWorkouts == null)
            {
                return null;
            }

            return fullWorkouts.Where(x => GetByCoachId(x, id)).ToList();
        }

        public async Task<List<FullWorkout>> GetFullWorkoutsForPlayer(string id)
        {
            List<FullWorkout> fullWorkouts = await GetFullWorkoutsAsync();

            if (fullWorkouts == null)
            {
                return null;
            }

            return fullWorkouts.Where(x => GetByPlayerId(x, id)).ToList();
        }

        public async Task<BaseEvent> GetBaseEventByIdAsync(int id)
        {
            return await _context.BaseEvent.Include(x => x.Coach).Include(x => x.Player).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Start> GetStartByIdAsync(int id)
        {
            return await _context.Start.Include(x => x.Coach).Include(x => x.Player).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Workout> GetWorkoutByIdAsync(int id)
        {
            return await _context.Workout.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Camp> GetCampByIdAsync(int id)
        {
            return await _context.Camp.Include(x => x.Coach).Include(x => x.Player).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _context.Message.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Request> GetRequestByIdAsync(int id)
        {
            return await _context.Request.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<FullWorkout> GetFullWorkoutByIdAsync(int id)
        {
            return await _context.FullWorkout.Include(x => x.Coach).Include(x => x.Player).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateRelationBetweenCoachAndPlayer(Request request)
        {
            Player player = null;
            Coach coach = null;

            if (request.Sender is Player newPlayer)
            {
                player = newPlayer;
            }

            if (request.Receiver is Coach newcoach)
            {
                coach = newcoach;
            }

            if (player == null || coach == null)
            {
                return;
            }

            player.Coach = coach;
            coach.PlayersList.Add(player);
            await UpdateObjectInDb(player);
            await UpdateObjectInDb(coach);
        }
        public async Task DeleteRelationBetweenCoachAndPlayer(User user)
        {
            Player player = null;
            Coach coach = null;

            if (user is Player newPlayer)
            {
                player = newPlayer;
            }

            if (player != null && player.Coach is Coach newcoach)
            {
                coach = newcoach;
            }

            if (player == null || coach == null)
            {
                return;
            }

            player.Coach = null;
            coach.PlayersList.Remove(player);
            await UpdateObjectInDb(player);
            await UpdateObjectInDb(coach);
        }
        public async Task<Message> FindMessageById(int id)
        {
            return await _context.Message.FindAsync(id);
        }
        public async Task<Start> FindStartById(int id)
        {
            return await _context.Start.FindAsync(id);
        }
        public async Task<Workout> FindWorkoutByIdAsync(int id)
        {
            return await _context.Workout.FindAsync(id);
        }
        public async Task<Camp> FindCampByIdAsync(int id)
        {
            return await _context.Camp.FindAsync(id);
        }
        public async Task<FullWorkout> FindFullWorkoutByIdAsync(int id)
        {
            return await _context.FullWorkout.FindAsync(id);
        }
        public async Task RemoveMessageFromDb(Message message)
        {
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveStartFromDb(Start start)
        {
            _context.Start.Remove(start);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveWorkoutFromDb(Workout workout)
        {
            _context.Workout.Remove(workout);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveCampFromDb(Camp camp)
        {
            _context.Camp.Remove(camp);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveFullWorkoutFromDb(FullWorkout fullWorkout)
        {
            _context.FullWorkout.Remove(fullWorkout);
            await _context.SaveChangesAsync();
        }
        public bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
        public bool StartExists(int id)
        {
            return _context.Start.Any(e => e.Id == id);
        }
        public bool WorkoutExists(int id)
        {
            return _context.Workout.Any(e => e.Id == id);
        }
        public bool CampExists(int id)
        {
            return _context.Camp.Any(e => e.Id == id);
        }
        public bool FullWorkoutExists(int id)
        {
            return _context.FullWorkout.Any(e => e.Id == id);
        }
        public async Task<bool> ActiveRequestExists(string id)
        {
            List<Request> requests = await GetRequestsForSender(id);
            Request req = requests.FirstOrDefault(x => x.RequestStatus == RequestStatus.Awaiting);
            if (req != null)
            {
                return true;
            }
            return false;
        }
        public async Task SetConnectionToCamp(FullWorkout fullWorkout)
        {
            List<Camp> camps = await GetCampsAsync();
            camps = camps.Where(x => fullWorkout.StartDate >= x.StartDate  && fullWorkout.EndDate <= x.EndDate).ToList();
            camps.ForEach(x =>
            {
                x.TrainingList.Add(fullWorkout);
                _context.Update(x);
            });

            await _context.SaveChangesAsync();
        }
        public async Task SetConnectionToFullworkout(Camp camp)
        {
            List<FullWorkout> fullWorkouts = await GetFullWorkoutsAsync();
            fullWorkouts = fullWorkouts.Where(x => x.StartDate >= camp.StartDate && x.EndDate <= camp.EndDate).ToList();
            camp.TrainingList = new List<FullWorkout>(fullWorkouts);
            await UpdateObjectInDb(camp);
        }
        public async Task<List<Message>> GetInbox(string id)
        {
            return await _context.Message.Where(x => x.Receiver.Id == id && !(x is Request)).OrderByDescending(x => x.SendDate).ToListAsync();
        }
        public async Task<List<Message>> GetOutbox(string id)
        {
            return await _context.Message.Where(x => x.Sender.Id == id && !(x is Request)).OrderByDescending(x => x.SendDate).ToListAsync();
        }
        public async Task<List<Request>> GetRequestsForSender(string id)
        {
            return await _context.Request.Where(x => x.Sender.Id == id).OrderByDescending(x => x.SendDate).ToListAsync();
        }
        public async Task<List<Request>> GetRequestsForReceiver(string id)
        {
            return await _context.Request.Where(x => x.Receiver.Id == id).OrderByDescending(x => x.SendDate).ToListAsync();
        }
    }
}
