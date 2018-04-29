using AutoStore.BLL.Interfaces;
using AutoStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.BLL.Services
{
    public class AutoDetailService
    {
        public IService CreateService(string connection)
        {
            return new Service(new EFUnitOfWork(connection));
        }
    }
}
