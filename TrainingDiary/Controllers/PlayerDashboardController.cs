using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingDiary.Enums;
using TrainingDiary.Models;
using TrainingDiary.Services;
using TrainingDiary.ViewModels;

namespace TrainingDiary.Controllers
{
    // Player only
    [Authorize(Roles = Role.Player)]
    public class PlayerDashboardController : Controller
    {
        private readonly IDbContextService _dbContextService;
        private readonly IUserService _userService;

        public PlayerDashboardController(IDbContextService dbContextService, IUserService userService)
        {
            _dbContextService = dbContextService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            Player player = await _userService.GetUser(User) as Player;

            if (player == null)
            {
                return NotFound();
            }

            return View();
        }

        public async Task<IActionResult> MyCoachDetails()
        {
            Player player = await _userService.GetUser(User) as Player;

            if (player == null)
            {
                return NotFound();
            }

            if (player.Coach == null)
            {
                if (await _dbContextService.ActiveRequestExists(player.Id))
                {
                    return RedirectToAction(nameof(WaitForApprove));
                }
                else
                {
                    return RedirectToAction(nameof(NoCoach));
                }
            }
            else
            {
                return View(new CoachViewModel(player.Coach));
            }
        }
        public async Task<IActionResult> WaitForApprove()
        {
            string userId = _userService.GetUserId(User);
            Request request = (await _dbContextService.GetRequestsForSender(userId)).FirstOrDefault();

            User coach = request.Receiver;
            ViewData["Coach"] = coach.FirstName + " " + coach.LastName;

            return View();
        }
        public IActionResult NoCoach()
        {
            return View();
        }
    }
}
