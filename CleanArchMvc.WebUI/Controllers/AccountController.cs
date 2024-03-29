﻿using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticate;
        public AccountController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authenticate.Authenticate(model.Email, model.Password);

            if (result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.(Password must be strong).");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Registre()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registre(RegisterViewModel model)
        {
            var result = await _authenticate.RegistrerUser(model.Email, model.Password);
            if (result)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Registre attempt (Password must be strong)");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();
            return RedirectToAction("/Account/Login");
        }
    }
}
