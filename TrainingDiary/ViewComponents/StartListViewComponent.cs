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
    public class StartListViewComponent : ViewComponent
    {
        private readonly IDbContextService _dbContextService;
        private readonly IUserService _userService;

        public StartListViewComponent(IDbContextService dbContextService, IUserService userService)
        {
            _dbContextService = dbContextService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/Start/Index.cshtml", await GetStartsForCurrentUser());
        }


        public async Task<List<Start>> GetStartsForCurrentUser()
        {
            string userId = _userService.GetUserId(UserClaimsPrincipal);
            List<Start> starts = new List<Start>();

            if (User.IsInRole(Role.Player))
            {
                starts = await _dbContextService.GetStartsForPlayer(userId);
            }
            else if (User.IsInRole(Role.Coach))
            {
                starts = await _dbContextService.GetStartsForCoach(userId);
            }

            return starts;
        }
    }
}
