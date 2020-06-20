using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Enums;
using TrainingDiary.Models;
using TrainingDiary.Services;

namespace TrainingDiary.ViewComponents
{
    public class FullWorkoutListViewComponent : ViewComponent
    {
        private readonly IDbContextService _dbContextService;
        private readonly IUserService _userService;

        public FullWorkoutListViewComponent(IDbContextService dbContextService, IUserService userService)
        {
            _dbContextService = dbContextService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/FullWorkout/Index.cshtml", await GetFullWorkoutsForCurrentUser());
        }

        public async Task<List<FullWorkout>> GetFullWorkoutsForCurrentUser()
        {
            string userId = _userService.GetUserId(UserClaimsPrincipal);
            List<FullWorkout> fullWorkouts = new List<FullWorkout>();

            if (User.IsInRole(Role.Player))
            {
                fullWorkouts = await _dbContextService.GetFullWorkoutsForPlayer(userId);
            }
            else if (User.IsInRole(Role.Coach))
            {
                fullWorkouts = await _dbContextService.GetFullWorkoutsForCoach(userId);
            }

            return fullWorkouts;
        }
    }
}
