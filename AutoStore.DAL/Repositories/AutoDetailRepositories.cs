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
    public class AutoDetailRepositories : IRepository<AutoDetail>
    {

        private DetailContext db;

        public AutoDetailRepositories(DetailContext context)
        {
            this.db = context;
        }

        public void Create(AutoDetail item)
        {
            db.AutoDetails.Add(item);
        }

        public void Delete(int id)
        {
            AutoDetail detail = db.AutoDetails.Find(id);
            if (detail != null)
                db.AutoDetails.Remove(detail);
        }

        public IEnumerable<AutoDetail> Find(Func<AutoDetail, Boolean> predicate)
        {
            return db.AutoDetails.Where(predicate).ToList();
        }

        public AutoDetail Get(int id)
        {
            return db.AutoDetails.Find(id);
        }

        public IEnumerable<AutoDetail> GetAll()
        {
            return db.AutoDetails;
        }

        public void Update(AutoDetail item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
