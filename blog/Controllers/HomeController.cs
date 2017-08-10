using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.ViewModels;
using Data.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBlogContext _blogContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(IBlogContext blogContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _blogContext = blogContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Index()
        {
            var posts = _blogContext.Posts.ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var signIn = await _signInManager.PasswordSignInAsync(viewModel.Login, viewModel.Password, false, false);
                if (signIn.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Användaren existerar inte");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                IdentityResult identityResult;
                var user = new User()
                { Name = viewModel.Login, UserName = viewModel.Login };
                try
                {
                    identityResult = await _userManager.CreateAsync(user, viewModel.Password);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Fuck!");
                    return View(viewModel);
                }

                if (identityResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var identityResultError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityResultError.Description);
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
