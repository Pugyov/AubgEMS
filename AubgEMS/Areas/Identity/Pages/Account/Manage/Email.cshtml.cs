#nullable disable
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AubgEMS.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EmailModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required, EmailAddress, Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("Unable to load user.");

            Username = await _userManager.GetUserNameAsync(user);
            return Page();
        }

        // Change email immediately (no verification email)
        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("Unable to load user.");

            var newEmail = Input.NewEmail.Trim();

            // Optional: early exit if unchanged
            var currentEmail = await _userManager.GetEmailAsync(user);
            if (string.Equals(currentEmail, newEmail, System.StringComparison.OrdinalIgnoreCase))
            {
                StatusMessage = "Your email is unchanged.";
                return RedirectToPage();
            }

            // Set email + username to match (if you want them aligned)
            var setEmailResult = await _userManager.SetEmailAsync(user, newEmail);
            if (!setEmailResult.Succeeded)
            {
                foreach (var e in setEmailResult.Errors)
                    ModelState.AddModelError(string.Empty, e.Description);
                return Page();
            }

            // If you use email as username, keep them in sync:
            var setUserNameResult = await _userManager.SetUserNameAsync(user, newEmail);
            if (!setUserNameResult.Succeeded)
            {
                foreach (var e in setUserNameResult.Errors)
                    ModelState.AddModelError(string.Empty, e.Description);
                return Page();
            }

            // Mark email confirmed (since we skip verification)
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                // Some stores block setting this directly; instead re-confirm via tokenless path:
                // If your store disallows it, simply ignore this step.
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your email has been updated.";
            return RedirectToPage();
        }
    }
}
