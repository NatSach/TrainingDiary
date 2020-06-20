using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingDiary.Enums;
using TrainingDiary.Models;
using TrainingDiary.Services;

namespace TrainingDiary.Controllers
{
    [Authorize]
    public class StartController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDbContextService _dbContextService;

        public StartController(IUserService userService, IDbContextService dbContextService)
        {
            _userService = userService;
            _dbContextService = dbContextService;
        }

        private bool IsStartRelatedToCurrentUser(Start start)
        {
            string userId = _userService.GetUserId(User);
            if (User.IsInRole(Role.Player))
            {
                if (start.Player == null || start.Player.Id != userId)
                {
                    return false;
                }
            }
            else if (User.IsInRole(Role.Coach))
            {
                if (start.Coach == null || start.Coach.Id != userId)
                {
                    return false;
                }
            }

            return true;
        }

        // GET: Start/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var start = await _dbContextService.GetStartByIdAsync((int)id);
            if (start == null || !IsStartRelatedToCurrentUser(start))
            {
                return NotFound();
            }

            return View(start);
        }

        // GET: Start/Create
        [Authorize(Roles = Role.Coach)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Start/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Coach)]
        public async Task<IActionResult> Create(Start start)
        {
            if (ModelState.IsValid)
            {
                start.Player = await _userService.GetUserByEmail(start.PlayerEmail) as Player;
                start.Coach = await _userService.GetUser(User) as Coach;

                await _userService.SetUserInStartModel(start, User);
                await _dbContextService.AddObjectToDb(start);

                return RedirectToAction("Index", "Events");
            }
            return View(start);
        }

        // GET: Start/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var start = await _dbContextService.GetStartByIdAsync((int)id);
            if (start == null || !IsStartRelatedToCurrentUser(start))
            {
                return NotFound();
            }
            return View(start);
        }

        // POST: Start/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Place,Result,Distance,Location,CoachComment,PlayerComment,Id,EventName,StartDate,EndDate,Type")] Start start)
        {
            if (id != start.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dbContextService.UpdateObjectInDb(start);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContextService.StartExists(start.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Events");
            }
            return View(start);
        }

        // GET: Start/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var start = await _dbContextService.GetStartByIdAsync((int)id);
            if (start == null || !IsStartRelatedToCurrentUser(start))
            {
                return NotFound();
            }

            return View(start);
        }

        // POST: Start/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var start = await _dbContextService.GetStartByIdAsync(id);
            await _dbContextService.RemoveStartFromDb(start);

            return RedirectToAction("Index", "Events");
        }
    }
}
