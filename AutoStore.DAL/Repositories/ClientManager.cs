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
    class ClientManager : IClientManager
    {
        public DetailContext db { get; set; }

        public ClientManager(DetailContext db)
        {
            this.db = db;
        }

        public void Create(ClientProfile item)
        {
            db.ClientProfiles.Add(item);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public ClientProfile Get(string id)
        {
            return db.ClientProfiles.Find(id);
        }
    }
}
