using BLL.Interfaces;
using BLL.Interfaces.Models;
using ChatSignalR.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ChatSignalR.Controllers
{
    public class AccountController : BaseController
    {
        private IAuthorizeService authService;

        static AccountController()
        {
        }

        public AccountController(IAuthorizeService authService)
        {
            this.authService = authService;
        }

        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var user = authService.GetUser(model.Username, model.Password);
                if (user != null)
                {
                    Modules.Authentication.AuthenticationHelper.AuthUser(Response, user.UserName, user.ID, user.Roles.ToArray(), model.RememberMe);

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Chat", "Home");
                    }

                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                DetailedUserData user = null;
                if (!authService.UserNameExist(model.Username) && !authService.EmailExist(model.Email))
                {
                    try
                    {
                        user = new DetailedUserData()
                        {
                            UserName = model.Username,
                            Email = model.Email,
                            Roles = new[] {"user"}, //make it better if you will have enough time
                        };
                        authService.CreateNewUser(user, model.Password);
                        Modules.Authentication.AuthenticationHelper.AuthUser(Response, user.UserName, user.ID, user.Roles.ToArray(), false);


                        if (Request.IsAjaxRequest())
                        {
                            // ajax request :)
                        }

                        return RedirectToAction("Chat", "Home");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Ошибка при регистрации");
                    }
                }
            }
            ModelState.AddModelError("", "Ошибка при регистрации");
            if (Request.IsAjaxRequest())
            {
                // ajax request :)
                //Response.Write(Url.Action("Register"));
                //return new HttpStatusCodeResult(400);
            }
            return View(model);
        }

        public ActionResult SignOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }
    }
}