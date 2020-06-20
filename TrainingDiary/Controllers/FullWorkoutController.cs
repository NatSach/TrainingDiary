using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingDiary.Enums;
using TrainingDiary.Models;
using TrainingDiary.Services;

namespace TrainingDiary.Controllers
{
    [Authorize]
    public class FullWorkoutController : Controller
    {
        private readonly IDbContextService _dbContextService;
        private readonly IUserService _userService;

        public FullWorkoutController(IDbContextService dbContextService,IUserService userService)
        {
            _dbContextService = dbContextService;
            _userService = userService;
        }

        private bool IsFullWorkoutRelatedToCurrentUser(FullWorkout fullWorkout)
        {
            string userId = _userService.GetUserId(User);
            if (User.IsInRole(Role.Player))
            {
                if (fullWorkout.Player == null || fullWorkout.Player.Id != userId)
                {
                    return false;
                }
            }
            else if (User.IsInRole(Role.Coach))
            {
                if (fullWorkout.Coach == null || fullWorkout.Coach.Id != userId)
                {
                    return false;
                }
            }

            return true;
        }

        // GET: FullWorkouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fullWorkout = await _dbContextService.GetFullWorkoutByIdAsync((int)id);
            if (fullWorkout == null || !IsFullWorkoutRelatedToCurrentUser(fullWorkout))
            {
                return NotFound();
            }

            return View(fullWorkout);
        }

        // GET: FullWorkouts/Create
        [Authorize(Roles = Role.Coach)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FullWorkouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Coach)]
        public async Task<IActionResult> Create(FullWorkout fullWorkout)
        {
            if (ModelState.IsValid)
            {
                fullWorkout.Player = await _userService.GetUserByEmail(fullWorkout.PlayerEmail) as Player;
                fullWorkout.Coach = await _userService.GetUser(User) as Coach;

                await _dbContextService.AddObjectToDb(fullWorkout);
                await _dbContextService.SetConnectionToCamp(fullWorkout);

                return RedirectToAction("Index", "Events");
            }
            return View(fullWorkout);
        }

        // GET: FullWorkouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fullWorkout = await _dbContextService.GetFullWorkoutByIdAsync((int)id);
            if (fullWorkout == null || !IsFullWorkoutRelatedToCurrentUser(fullWorkout))
            {
                return NotFound();
            }
            return View(fullWorkout);
        }

        // POST: FullWorkouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventName,StartDate,EndDate, Plan, Realization")] FullWorkout fullWorkout)
        {
            if (id != fullWorkout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dbContextService.UpdateObjectInDb(fullWorkout);
                    //_dbContextService.SetConnectionToCamp(fullWorkout);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContextService.FullWorkoutExists(fullWorkout.Id))
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
            return View(fullWorkout);
        }

        // GET: FullWorkouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fullWorkout = await _dbContextService.GetFullWorkoutByIdAsync((int)id);
            if (fullWorkout == null || !IsFullWorkoutRelatedToCurrentUser(fullWorkout))
            {
                return NotFound();
            }

            return View(fullWorkout);
        }

        // POST: FullWorkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fullWorkout = await _dbContextService.GetFullWorkoutByIdAsync(id);
            await _dbContextService.RemoveFullWorkoutFromDb(fullWorkout);

            return RedirectToAction("Index", "Events");
        }
    }
}
