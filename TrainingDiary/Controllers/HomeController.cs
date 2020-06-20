using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingDiary.Models;
using TrainingDiary.Services;

namespace TrainingDiary.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDbContextService _dbContextService;

        public HomeController(IDbContextService dbContextService, IUserService userService)
        {
            _userService = userService;
            _dbContextService = dbContextService;
        }

        public async Task<IActionResult> Index()
        {
            List<BaseEvent> baseEvents = null;
            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();

            User user = await _userService.GetUser(User);

            if (user is Player)
            {
                baseEvents = await _dbContextService.GetBaseEventsForPlayer(user.Id);
            }
            else if (user is Coach)
            {
                baseEvents = await _dbContextService.GetBaseEventsForCoach(user.Id);
            }

            if (baseEvents != null)
            {
                calendarEvents = baseEvents.Select(x => new CalendarEvent(x)).ToList();
            }

            return View(calendarEvents);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
