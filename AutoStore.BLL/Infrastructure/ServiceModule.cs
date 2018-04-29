using AutoStore.DAL.Interfaces;
using AutoStore.DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.BLL.Infrastructure
{
    public class ServiceModule: NinjectModule
    {
        private string connection;
        public ServiceModule(string conn)
        {
            connection = conn;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connection);
        }
    }
}
