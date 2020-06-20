using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainingDiary.Models;
using TrainingDiary.Services;
using TrainingDiary.ViewModels;

namespace TrainingDiary.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        private readonly IUserService _userService;
        private readonly IDbContextService _dbContextService;

        public IndexModel(
            SignInManager<User> signInManager,
            IUserService userService,
            IDbContextService dbContextService)
        {
            _signInManager = signInManager;
            _userService = userService;
            _dbContextService = dbContextService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public PlayerViewModel PlayerVM { get; set; }
        
        [BindProperty]
        public CoachViewModel CoachVM { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userService.GetUser(User);
            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            if (user is Player player)
            {
                PlayerVM = new PlayerViewModel(player);
            }
            else if (user is Coach coach)
            {
                CoachVM = new CoachViewModel(coach);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userService.GetUser(User);
            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            if (user is Player player && PlayerVM != null)
            {
                player.FirstName = PlayerVM.FirstName;
                player.LastName = PlayerVM.LastName;
                player.BirthDate = PlayerVM.BirthDate;
                player.LicenceNo = PlayerVM.LicenceNo;
                player.StartNo = PlayerVM.StartNo;
                player.Weight = PlayerVM.Weight;
                player.Height = PlayerVM.Height;
                player.ShoeSize = PlayerVM.ShoeSize;
                player.Size = PlayerVM.Size;
                player.ExaminationDeadline = PlayerVM.ExaminationDeadline;
                player.Injured = PlayerVM.Injured;
            }
            else if (user is Coach coach && CoachVM != null)
            {
                coach.FirstName = CoachVM.FirstName;
                coach.LastName = CoachVM.LastName;
            }

            await _dbContextService.UpdateObjectInDb(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
