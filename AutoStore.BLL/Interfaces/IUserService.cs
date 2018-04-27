using AutoStore.BLL.DTO;
using AutoStore.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AutoStore.BLL.Interfaces
{
    public interface IUserService:IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        UserDTO GetCurrentUser();
    }
}
