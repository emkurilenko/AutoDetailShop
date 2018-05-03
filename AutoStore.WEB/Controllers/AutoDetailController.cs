using AutoMapper;
using AutoStore.BLL.DTO;
using AutoStore.BLL.Infrastructure;
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var cur_user = service.GetCurrentUser();
            if (cur_user != null)
            {
                if (id != null)
                {
                    var _detailDTO = service.GetAutoDetail(id.Value);
                    Mapper.Reset();
                    Mapper.Initialize(cfg => cfg.CreateMap<AutoDetailDTO, AutoDetailViewModel>());
                    var _detail = Mapper.Map<AutoDetailDTO, AutoDetailViewModel>(_detailDTO);
                    if (_detail != null)
                        return View(_detail);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(AutoDetailViewModel detailViewModel)
        {
            var cur_user = service.GetCurrentUser();
            OperationDetails result;
            if (cur_user != null)
            {
                if (ModelState.IsValid)
                {
                    Mapper.Reset();
                    Mapper.Initialize(cfg => cfg.CreateMap<AutoDetailViewModel, AutoDetailDTO>());
                    var _detail = Mapper.Map<AutoDetailViewModel, AutoDetailDTO>(detailViewModel);
                    result = service.EditDetail(_detail);
                    if (result.Succedeed)
                        return RedirectToAction("Index", "Home");
                    else
                        return View(detailViewModel);
                }
                else
                    return View(detailViewModel);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (service.GetCurrentUser() != null)
            {
                if (id != null)
                {
                    service.DeleteDetail(id.Value);
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Login", "Account");     
        }

        public ActionResult Details(int id)
        {
            var _detail = service.GetAutoDetails().FirstOrDefault(c => c.Id == id);
            if (_detail != null)
            {
                return PartialView(_detail);
            }
            return View();
        }
    }
}