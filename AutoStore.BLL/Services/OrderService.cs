using AutoMapper;
using AutoStore.BLL.DTO;
using AutoStore.BLL.Infrastructure;
using AutoStore.BLL.Interfaces;
using AutoStore.DAL.Entities;
using AutoStore.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public AutoDetailDTO GetAutoDetail(int? id)
        {
            if (id == null)
                throw new ValidationException("Id товара не выбрано", "");
            var detail = Database.AutoDetails.Get(id.Value);
            if (detail == null)
                throw new ValidationException("Делать не найдена", "");
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<AutoDetail, AutoDetailDTO>());
            return Mapper.Map<AutoDetail, AutoDetailDTO>(detail);
        }

        public IEnumerable<AutoDetailDTO> GetAutoDetails()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<AutoDetail, AutoDetail>());
            return Mapper.Map<IEnumerable<AutoDetail>, List<AutoDetailDTO>>(Database.AutoDetails.GetAll());
        }

        public OperationDetails MakeOrder(OrderDTO orderDTO)
        {
            var autoDetail = Database.AutoDetails.Get(orderDTO.Id);
            var client = Database.UserManager.FindById(orderDTO.ClientProfileId.ToString());

            if (autoDetail == null)
                return new OperationDetails(false, "Деталь не найдена", "");
            if (client == null)
                return new OperationDetails(false, "Пользавтель не найден", "");

            Order order = new Order
            {
                Date = DateTime.Now,
                AutoDetailId = autoDetail.IdAutoDetail,
                Sum = autoDetail.Price,
                ClientProfileId = client.GetClientProfile.Id
            };
            Database.Orders.Create(order);
            Database.Save();

            return new OperationDetails(true, "Заказ успешно выполнен", "");
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());
            return Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(Database.Orders.GetAll());
        }
        public OperationDetails CreateDetail(AutoDetailDTO autoDetailDTO)
        {
            if(autoDetailDTO != null)
            {
                Database.AutoDetails.Create(new AutoDetail
                {
                    Price = autoDetailDTO.Price,
                    Article = autoDetailDTO.Article,
                    NameDetail = autoDetailDTO.NameDetail,
                    Company = autoDetailDTO.Company,
                    Type = autoDetailDTO.Type
                    
                });
                Database.Save();
                return new OperationDetails(true, "Деталь добавлена в базу", "");
            }
            return new OperationDetails(false, "Параметры пусты", "");
        }

        public OperationDetails EditDetail(AutoDetailDTO autoDetailDTO)
        {
            try
            {
                Database.AutoDetails.Update(new AutoDetail
                {
                    IdAutoDetail = autoDetailDTO.IdAutoDetail,
                    Price = autoDetailDTO.Price,
                    Article = autoDetailDTO.Article,
                    NameDetail = autoDetailDTO.NameDetail,
                    Company = autoDetailDTO.Company,
                    Type = autoDetailDTO.Type
                });
            Database.Save();
                return new OperationDetails(true, "Деталь изменена", "");
            }
            catch
            {
                return new OperationDetails(false, "Ошибка изменения детали", "");
            }
        }

        public OperationDetails DeleteOrder(int id)
        {
            try
            {
                Database.Orders.Delete(id);
                Database.Save();
                return new OperationDetails(true, "Заказ успешно удален!", "");
            }
            catch
            {
                return new OperationDetails(false, "Невозможно удалить заказ", "");

            }
        }

        public OperationDetails DeleteDetail(int id)
        {
            try
            {
                Database.AutoDetails.Delete(id);
                Database.Save();
                return new OperationDetails(false, "Деталь удалена с базы", "");
            }
            catch
            {
                return new OperationDetails(false, "Невозможно удалить деталь", "");
            }
        }
    }
}
