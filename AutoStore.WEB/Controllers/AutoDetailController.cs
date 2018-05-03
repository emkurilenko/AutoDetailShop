using AutoMapper;
using AutoStore.BLL.DTO;
using AutoStore.BLL.Interfaces;
using AutoStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoStore.WEB.Controllers
{
    public class AutoDetailController : Controller
    {

        private IService service;

        public AutoDetailController(IService service)
        {
            this.service = service;
        }
        // GET: AutoDetail
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var cur_user = service.GetCurrentUser();
            if (cur_user != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(AutoDetailViewModel detail)
        {
            var cur_user = service.GetCurrentUser();
            if (cur_user != null)
            {
                if (ModelState.IsValid)
                {
                    Mapper.Reset();
                    Mapper.Initialize(cfg => cfg.CreateMap<AutoDetailViewModel, AutoDetailDTO>());
                    var _detail = Mapper.Map<AutoDetailViewModel, AutoDetailDTO>(detail);
                    var result = service.CreateDetail(_detail);
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View(detail);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}