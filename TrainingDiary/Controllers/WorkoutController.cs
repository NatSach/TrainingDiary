using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingDiary.Models;
using TrainingDiary.Services;

namespace TrainingDiary.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly IDbContextService _dbContextService;

        public WorkoutController(IDbContextService dbContextService)
        {
            _dbContextService = dbContextService;
        }

        // GET: Workout
        public async Task<IActionResult> Index()
        {
            return View(await _dbContextService.GetWorkoutsAsync());
        }

        // GET: Workout/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _dbContextService.GetWorkoutByIdAsync((int)id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workout/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workout/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,OWB1,OWB2,WB3,ZB,KR,GL,DL,Rhythm,RelativeSpeed,MaxSpeed,Skip,MultipleJump,MS,BPG,KMStart,Scamper,Comment")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                await _dbContextService.AddObjectToDb(workout);
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workout/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _dbContextService.FindWorkoutByIdAsync((int)id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }

        // POST: Workout/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,OWB1,OWB2,WB3,ZB,KR,GL,DL,Rhythm,RelativeSpeed,MaxSpeed,Skip,MultipleJump,MS,BPG,KMStart,Scamper,Comment")] Workout workout)
        {
            if (id != workout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dbContextService.UpdateObjectInDb(workout);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContextService.WorkoutExists(workout.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workout/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _dbContextService.FindWorkoutByIdAsync((int)id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _dbContextService.FindWorkoutByIdAsync((int)id);
            await _dbContextService.RemoveWorkoutFromDb(workout);
            return RedirectToAction(nameof(Index));
        }

    }
}
