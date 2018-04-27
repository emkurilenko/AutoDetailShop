using AutoStore.DAL.EF;
using AutoStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.DAL.Interfaces
{
    public interface IClientManager:IDisposable
    { 
        void Create(ClientProfile item);
        ClientProfile Get(string id);
    }
}
