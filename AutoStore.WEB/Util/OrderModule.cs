using AutoStore.BLL.Interfaces;
using AutoStore.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStore.WEB.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IService>().To<Service>();
        }
    }
}