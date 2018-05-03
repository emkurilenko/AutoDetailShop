using AutoStore.BLL.DTO;
using AutoStore.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.BLL.Interfaces
{
    public interface IService : IDisposable
    {
        OperationDetails MakeOrder(OrderDTO orderDTO);
        AutoDetailDTO GetAutoDetail(int? id);
        IEnumerable<AutoDetailDTO> GetAutoDetails();
        IEnumerable<OrderDTO> GetOrders();
        OperationDetails DeleteOrder(int id);

        OperationDetails CreateDetail(AutoDetailDTO autoDetailDTO);
        OperationDetails EditDetail(AutoDetailDTO autoDetailDTO);
        OperationDetails DeleteDetail(int id);

        Task<OperationDetails> Create(UserDTO userDto);
        Task<OperationDetails> EditUser(UserDTO userDTO);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        UserDTO GetCurrentUser();
        UserDTO GetUser(string id);
    }
}
