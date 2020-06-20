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
    // Coach only
    [Authorize(Roles = Role.Coach)]
    public class CoachDashboardController : Controller
    {
        private readonly IDbContextService _dbContextService;
        private readonly IUserService _userService;

        public CoachDashboardController(IDbContextService dbContextService, IUserService userService)
        {
            _dbContextService = dbContextService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            Coach coach = await _userService.GetUser(User) as Coach;

            if (coach == null)
            {
                return NotFound();
            }

            coach.PlayersList = await _dbContextService.GetPlayersForCoach(coach.Id);

            if (coach.PlayersList.Any())
            {
                IEnumerable<PlayerViewModel> playersViewModels = coach.PlayersList.Select(x => new PlayerViewModel(x)).ToList();
                return View(playersViewModels);
            }
            else
            {
                return RedirectToAction(nameof(NoPlayers));
            }
        }

        //TODO: Details, Delete

        public IActionResult NoPlayers()
        {
            return View();
        }

        public async Task<IActionResult> PlayerDetails(string id)
        {
            if(id == null)
            {
                return NotFound("Unable to load user.");
            }
            var user = await _userService.GetUserByEmail(id);
            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            if (user is Player player)
            {
                PlayerViewModel playerVM = new PlayerViewModel(player);
                return View(playerVM);
            }
            else
            {
                return NotFound("Unable to load user.");
            }
        }
        public async Task<IActionResult> DeleteConnection(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _userService.GetUserByEmail(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user is Player player)
            {
                PlayerViewModel playerVM = new PlayerViewModel(player);
                return View(playerVM);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost, ActionName("DeleteConnection")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            User user = await _userService.GetUserByEmail(id);
            await _dbContextService.DeleteRelationBetweenCoachAndPlayer(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
