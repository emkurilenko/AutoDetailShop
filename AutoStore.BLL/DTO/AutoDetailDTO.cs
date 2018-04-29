using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.BLL.DTO
{
    public class AutoDetailDTO
    {
        public int Id { get; set; }
        public string Article { get; set; }
        public string Brend { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }

    }
}
