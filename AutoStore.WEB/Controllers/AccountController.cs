using AutoMapper;
using AutoStore.BLL.DTO;
using AutoStore.BLL.Infrastructure;
using AutoStore.BLL.Interfaces;
using AutoStore.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AutoStore.WEB.Controllers
{
    public class AccountController : Controller
    {


        private IService Service
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { UserName = model.UserName, Password = model.Password };
                ClaimsIdentity claim = await Service.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Address = model.Address,
                    UserName = model.UserName,
                    Role = "user"
                };
                OperationDetails operationDetails = await Service.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await Service.SetInitialData(new UserDTO
            {
                Email = "kurilenko.e.m@outlook.com",
                Password = "123qwe",
                Name = "Евгений Куриленко",
                UserName = "emkurilenko",
                Address = "Novopolotsk",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }

        public ActionResult GetName()
        {
            var user = Service.GetCurrentUser();
            if (user != null)
            {
                ViewBag.Name = user.Name;
            }
            else
            {
                ViewBag.Name = "Гость";

            }
            return PartialView();
        }

        [Authorize]
        public ActionResult Index()
        {
            var user = Service.GetCurrentUser();
            if (user == null)
            {
                Logout();
            }
            return View(user);
        }



        [Authorize]
        public ActionResult _PartialUserOrders()
        {
            var user = Service.GetCurrentUser();
            var orderDtos = Service.GetOrders().Where(o => o.ClientProfileId == user.IdUser);
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>());
            var orders = Mapper.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(orderDtos);
            return PartialView(orders);
        }

        [Authorize]
        public ActionResult Account()
        {
            var _user = Service.GetCurrentUser();
            if (_user == null) {
                Logout();
            }
            var user = Mapper.Map<UserDTO, UserViewModel>(_user);
            return View(user);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AccountDetail(string id)
        {
            var _user = Service.GetUser(id);
            return View(_user);
        }

        [Authorize]
        public ActionResult EditUser()
        {
            var _user = Service.GetCurrentUser();
            var user = Mapper.Map<UserDTO, UserViewModel>(_user);
            return View(user);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EditUser(UserViewModel user)
        {
            var cur_user = Service.GetCurrentUser();
            if (cur_user != null)
            {
                if (ModelState.IsValid)
                {
                    var _user = Mapper.Map<UserViewModel, UserDTO>(user);
                    OperationDetails operationDetails = await Service.EditUser(_user);
                    if (operationDetails.Succedeed)
                        return RedirectToAction("Account", "Account");
                    else
                        ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
                else
                    return View(user);              
            }
            return RedirectToAction("Login","Account");
        }
    }
}