using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrainingDiary.Models;

namespace TrainingDiary.Services
{
    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal claims);
        Task<User> GetUser(ClaimsPrincipal claims);
        Task<User> GetUserByEmail(string email);
        Task<Coach> GetCoachByEmail(string email);
        Task SetUserInStartModel(Start start, ClaimsPrincipal claims);
    }
}
