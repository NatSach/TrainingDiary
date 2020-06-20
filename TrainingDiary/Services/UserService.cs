using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrainingDiary.Models;

namespace TrainingDiary.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserId(ClaimsPrincipal claims)
        {
            return _userManager.GetUserId(claims);
        }

        public async Task<User> GetUser(ClaimsPrincipal claims)
        {
           return await _userManager.GetUserAsync(claims);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<Coach> GetCoachByEmail(string email)
        {
            var user = await GetUserByEmail(email);
            if(user is Coach coach)
            {
                return coach;
            }

            return null;
        }

        public async Task SetUserInStartModel(Start start, ClaimsPrincipal claims)
        {
            User user = await GetUser(claims);

            if (user is Player)
            {
                start.Player = (Player)user;
            }
            else if (user is Coach)
            {
                start.Coach = (Coach)user;
            }
        }

    }
}
