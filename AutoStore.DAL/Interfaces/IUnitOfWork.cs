using AutoStore.DAL.Entities;
using AutoStore.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace AutoStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();

        IRepository<AutoDetail> AutoDetails { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
