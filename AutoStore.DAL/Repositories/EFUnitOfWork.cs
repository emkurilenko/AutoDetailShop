using AutoStore.DAL.EF;
using AutoStore.DAL.Entities;
using AutoStore.DAL.Identity;
using AutoStore.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
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

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public EFUnitOfWork(string connectionString)
        {
            db = new DetailContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
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

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
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
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
           await db.SaveChangesAsync();
        }
    }
}
