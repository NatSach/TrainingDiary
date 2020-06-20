using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Controllers;
using TrainingDiary.Enums;
using TrainingDiary.Models;
using TrainingDiary.Services;

namespace TrainingDiary.ViewComponents
{
    public class CampListViewComponent : ViewComponent
    {
        private readonly IDbContextService _dbContextService;
        private readonly IUserService _userService;

        public CampListViewComponent(IDbContextService dbContextService, IUserService userService)
        {
            _dbContextService = dbContextService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/Camp/Index.cshtml", await GetCampsForCurrentUser());
        }

        public async Task<List<Camp>> GetCampsForCurrentUser()
        {
            string userId = _userService.GetUserId(UserClaimsPrincipal);
            List<Camp> camps = new List<Camp>();

            if (User.IsInRole(Role.Player))
            {
                camps = await _dbContextService.GetCampsForPlayer(userId);
            }
            else if (User.IsInRole(Role.Coach))
            {
                camps = await _dbContextService.GetCampsForCoach(userId);
            }

            return camps;
        }

    }
}
