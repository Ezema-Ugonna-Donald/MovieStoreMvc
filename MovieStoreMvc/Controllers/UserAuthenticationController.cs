using Microsoft.AspNetCore.Mvc;
using MovieStoreMvc.Models.DTO;
using MovieStoreMvc.Repositories.Abstract;

namespace MovieStoreMvc.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService _authService)
        {
            this.authService= _authService;
        }

        //public async Task<IActionResult> Register()
        //{
        //    var model = new RegistrationModel
        //    {
        //        Email = "ugoeze@gmail.com",
        //        Username = "admin",
        //        Name = "Ugonna",
        //        Password = "Password@123",
        //        PasswordConfirm = "Password@123",
        //        Role = "Admin"
        //    };

        //    var result = await authService.RegisterAsync(model);

        //    return Ok(result.Message);
        //}

        public async Task<IActionResult> Login ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Could not login..";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
