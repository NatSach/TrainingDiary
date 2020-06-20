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
    public class CampController : Controller
    {
        private readonly IDbContextService _dbContextService;
        private readonly IUserService _userService;

        public CampController(IDbContextService dbContextService, IUserService userService)
        {
            _dbContextService = dbContextService;
            _userService = userService;
        }

        private bool IsCampRelatedToCurrentUser(Camp camp)
        {
            string userId = _userService.GetUserId(User);
            if (User.IsInRole(Role.Player))
            {
                if (camp.Player == null || camp.Player.Id != userId)
                {
                    return false;
                }
            }
            else if (User.IsInRole(Role.Coach))
            {
                if (camp.Coach == null || camp.Coach.Id != userId)
                {
                    return false;
                }
            }

            return true;
        }

        // GET: Camp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Camp camp = await _dbContextService.GetCampByIdAsync((int)id);

            if (camp == null || !IsCampRelatedToCurrentUser(camp))
            {
                return NotFound();
            }

            return View(camp);
        }

        // GET: Camp/Create
        [Authorize(Roles = Role.Coach)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Camp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Coach)]
        public async Task<IActionResult> Create(Camp camp)
        {
            if (ModelState.IsValid)
            {
                camp.Player = await _userService.GetUserByEmail(camp.PlayerEmail) as Player;
                camp.Coach = await _userService.GetUser(User) as Coach;
                await _dbContextService.AddObjectToDb(camp);
                await _dbContextService.SetConnectionToFullworkout(camp);

                return RedirectToAction("Index", "Events");
            }
            return View(camp);
        }

        // GET: Camp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Camp camp = await _dbContextService.GetCampByIdAsync((int)id);
            if (camp == null || !IsCampRelatedToCurrentUser(camp))
            {
                return NotFound();
            }

            return View(camp);
        }

        // POST: Camp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Location,Id,EventName,StartDate,EndDate,Type")] Camp camp)
        {
            if (id != camp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dbContextService.SetConnectionToFullworkout(camp);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContextService.CampExists(camp.Id))
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
            return View(camp);
        }

        // GET: Camp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camp = await _dbContextService.GetCampByIdAsync((int)id);
            if (camp == null || !IsCampRelatedToCurrentUser(camp))
            {
                return NotFound();
            }

            return View(camp);
        }

        // POST: Camp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camp = await _dbContextService.GetCampByIdAsync(id);
            await _dbContextService.RemoveCampFromDb(camp);

            return RedirectToAction("Index", "Events");
        }
    }
}
