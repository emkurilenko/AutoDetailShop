using AutoStore.DAL.EF;
using AutoStore.DAL.Entities;
using AutoStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.DAL.Repositories
{
    public class EFUnitOfWork:IUnitOfWork
    {
        private DetailContext db;
        private AutoDetailRepositories autoDetailRepositories;
        private OrderRepository orderRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new DetailContext(connectionString);
        }

        public IRepository<AutoDetail> AutoDetails
        {
            get
            {
                if (autoDetailRepositories == null)
                {
                    autoDetailRepositories = new AutoDetailRepositories(db);
                }
                return autoDetailRepositories;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
