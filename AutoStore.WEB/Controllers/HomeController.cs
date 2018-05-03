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
    public class HomeController : Controller
    {
        IService service;

        public HomeController(IService serv)
        {
            service = serv;
            //service.CreateDetail(new AutoDetailDTO
            //{
            //    Article = "9333029",
            //    Name = "Ступицы колес",
            //    Brend = "Tech-as",
            //    Price = 124.5
            //}

        }


        public ActionResult Index()
        {
            IEnumerable<AutoDetailDTO> autoDetails = service.GetAutoDetails();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<AutoDetailDTO, AutoDetailViewModel>());
            var details = Mapper.Map <IEnumerable<AutoDetailDTO>,  List<AutoDetailViewModel>>(autoDetails);
            return View(details);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}