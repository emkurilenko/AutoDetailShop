using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public int AutoDetailId { get; set; }
        public string ClientProfileId { get; set; }
        public DateTime? Date { get; set; }
    }
}
