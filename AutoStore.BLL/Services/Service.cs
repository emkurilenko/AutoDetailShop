using AutoMapper;
using AutoStore.BLL.DTO;
using AutoStore.BLL.Infrastructure;
using AutoStore.BLL.Interfaces;
using AutoStore.DAL.Entities;
using AutoStore.DAL.Interfaces;
using AutoStore.DAL.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.BLL.Services
{
    public class Service : IService
    {
        IUnitOfWork Database { get; set; }

        public Service(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Service(string connectionString)
        {
            Database = new EFUnitOfWork(connectionString);
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
                AutoDetailId = autoDetail.Id,
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
                    Brend = autoDetailDTO.Brend,
                    Name = autoDetailDTO.Name
                    
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
                    Id = autoDetailDTO.Id,
                    Price = autoDetailDTO.Price,
                    Article = autoDetailDTO.Article,
                    Brend = autoDetailDTO.Brend,
                    Name = autoDetailDTO.Name
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
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            ApplicationUser user = await Database.UserManager.FindAsync(userDto.UserName, userDto.Password);

            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.UserName };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name, UserName = userDto.UserName };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();

                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public UserDTO GetCurrentUser()
        {
            ApplicationUser appUser = Database.UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (appUser == null)
                return null;
            else
            {
                var user = Database.ClientManager.Find(u => u.Id == appUser.Id);
                
                return new UserDTO
                {
                    Address = user.Address,
                    Name = user.Name,
                    Email = user.ApplicationUser.Email,
                    UserName = user.ApplicationUser.UserName,
                    IdUser = user.Id
                };
            }
        }

        public UserDTO GetUser(string id)
        {
            ApplicationUser appUser = Database.UserManager.FindById(id);
            if (appUser == null)
                return null;
            else
            {
                var user = Database.ClientManager.Find(u => u.Id == appUser.Id);

                return new UserDTO
                {
                    Address = user.Address,
                    Name = user.Name,
                    Email = user.ApplicationUser.Email,
                    UserName = user.ApplicationUser.UserName,
                    IdUser = user.Id
                };
            }

        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public async Task<OperationDetails> EditUser(UserDTO userDTO)
        {
                ApplicationUser user = await Database.UserManager.FindByIdAsync(System.Web.HttpContext.Current.User.Identity.GetUserId());
                if (user != null)
                {
                    user.UserName = userDTO.UserName;
                    user.GetClientProfile.Name = userDTO.Name;
                    user.Email = userDTO.Email;
                    user.GetClientProfile.Address = userDTO.Address;
                    IdentityResult result = await Database.UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return new OperationDetails(true, "Профиль успешно изменен!", "");
                    }else
                        return new OperationDetails(false, "Ошибка изменения профиля!", "");
                }else
                    return new OperationDetails(false, "Пользователь не найден","");
        }
}
}
