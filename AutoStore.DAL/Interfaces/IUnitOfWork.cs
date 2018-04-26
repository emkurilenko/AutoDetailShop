using AutoStore.DAL.Entities;
using System;

namespace AutoStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AutoDetail> AutoDetails { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
