using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Models;

namespace TrainingDiary.Services
{
    public interface IDbContextService
    {
        Task<List<BaseEvent>> GetBaseEvents();
        Task<List<BaseEvent>> GetBaseEventsForPlayer(string id);
        Task<List<BaseEvent>> GetBaseEventsForCoach(string id);
        Task<List<IdentityRole>> GetRoles();
        Task<List<Player>> GetPlayersForCoach(string id);
        Task<List<Start>> GetStarts();
        Task<List<Start>> GetStartsForCoach(string id);
        Task<List<Start>> GetStartsForPlayer(string id);

        Task<List<Workout>> GetWorkoutsAsync();
        Task<List<Camp>> GetCampsAsync();
        Task<List<Camp>> GetCampsForCoach(string id);
        Task<List<Camp>> GetCampsForPlayer(string id);
        Task<List<FullWorkout>> GetFullWorkoutsAsync();
        Task<List<FullWorkout>> GetFullWorkoutsForCoach(string id);
        Task<List<FullWorkout>> GetFullWorkoutsForPlayer(string id);
        Task<BaseEvent> GetBaseEventByIdAsync(int id);
        Task<Start> GetStartByIdAsync(int id);
        Task<Message> GetMessageByIdAsync(int id);
        Task<Request> GetRequestByIdAsync(int id);
        Task<Workout> GetWorkoutByIdAsync(int id);
        Task<Camp> GetCampByIdAsync(int id);
        Task<FullWorkout> GetFullWorkoutByIdAsync(int id);
        Task AddObjectToDb(object newObject);
        Task UpdateObjectInDb(object editedObject);
        Task CreateRelationBetweenCoachAndPlayer(Request request);
        Task DeleteRelationBetweenCoachAndPlayer(User user);
        Task<Message> FindMessageById(int id);
        Task<Start> FindStartById(int id);
        Task<Workout> FindWorkoutByIdAsync(int id);
        Task<Camp> FindCampByIdAsync(int id);
        Task<FullWorkout> FindFullWorkoutByIdAsync(int id);
        Task RemoveMessageFromDb(Message message);
        Task RemoveStartFromDb(Start start);
        Task RemoveWorkoutFromDb(Workout workout);
        Task RemoveCampFromDb(Camp camp);
        Task RemoveFullWorkoutFromDb(FullWorkout fullWorkout);
        bool MessageExists(int id);
        bool StartExists(int id);
        bool WorkoutExists(int id);
        bool CampExists(int id);
        bool FullWorkoutExists(int id);
        Task<bool> ActiveRequestExists(string id);
        Task SetConnectionToCamp(FullWorkout fullWorkout);
        Task SetConnectionToFullworkout(Camp camp);
        Task<List<Message>> GetInbox(string id);
        Task<List<Message>> GetOutbox(string id);
        Task<List<Request>> GetRequestsForSender(string id);
        Task<List<Request>> GetRequestsForReceiver(string id);
    }
}
