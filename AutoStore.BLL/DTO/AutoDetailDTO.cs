using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.BLL.DTO
{
    public class AutoDetailDTO
    {
        public int IdAutoDetail { get; set; }
        public string Article { get; set; }
        public string NameDetail { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }

    }
}
