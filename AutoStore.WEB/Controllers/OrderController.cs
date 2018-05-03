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
    public class OrderController : Controller
    {
        IService service;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public OrderController(IService serv)
        {
            service = serv;
        }

        public ActionResult Index()
        {
            var orderDtos = service.GetOrders();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>());
            var orders = Mapper.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(orderDtos);
            return View(orders);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var cur_user = service.GetCurrentUser();
            OperationDetails result;
            if (cur_user != null)
            {
                if (id != null)
                {
                    result = service.DeleteOrder(id.Value);
                    logger.Info(result.Message);
                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        public ActionResult Buy(int id)
        {
            var cur_user = service.GetCurrentUser();
            var order = new OrderDTO
            {
                Id = id,
                ClientProfileId = cur_user.IdUser,
                Date = DateTime.Now,
                Sum = service.GetAutoDetails().FirstOrDefault(c => c.Id == id).Price
            };
            OperationDetails result = service.MakeOrder(order);
            logger.Info(result.Message);
            return RedirectToAction("Index", "Home");
        }

    }
}