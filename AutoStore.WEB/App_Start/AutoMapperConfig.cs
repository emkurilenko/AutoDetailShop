using AutoMapper;
using AutoStore.BLL.DTO;
using AutoStore.DAL.Entities; //Так нельзя делать. Но Должен решить проблему с ошибкой
//"'Mapper already initialized. You must call Initialize once per application domain/process"
using AutoStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStore.WEB.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserDTO, UserViewModel>();
                cfg.CreateMap<UserViewModel, UserDTO>();
                cfg.CreateMap<AutoDetailDTO, AutoDetailViewModel>();
                cfg.CreateMap<AutoDetailViewModel, AutoDetailDTO>();
                cfg.CreateMap<OrderDTO, OrderViewModel>();
                cfg.CreateMap<AutoDetail, AutoDetailDTO>();
                cfg.CreateMap<Order, OrderDTO>();
            }
            );
        }
    }
}