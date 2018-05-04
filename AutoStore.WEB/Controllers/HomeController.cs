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
            //Mapper.Reset();
            //Mapper.Initialize(cfg => cfg.CreateMap<AutoDetailDTO, AutoDetailViewModel>());
            var details = Mapper.Map <IEnumerable<AutoDetailDTO>,  List<AutoDetailViewModel>>(autoDetails);
            return View(details);
        }

        public ActionResult DetailSearch(string name)
        {
            try
            {
                if (!name.Equals(""))
                {
                    var _details = service.GetAutoDetails().Where(a => a.Article.Contains(name)).ToList();
                    //Mapper.Reset();
                    //Mapper.Initialize(cfg => cfg.CreateMap<AutoDetailDTO, AutoDetailViewModel>());
                    var details = Mapper.Map<IEnumerable<AutoDetailDTO>, List<AutoDetailViewModel>>(_details);
                    return PartialView(details);
                }
                return PartialView();
            }catch(Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}