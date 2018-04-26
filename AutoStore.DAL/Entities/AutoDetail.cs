using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.DAL.Entities
{
    public class AutoDetail
    {
        
        public int IdAutoDetail { get; set; }
        //артикул детали
        public string Article { get; set; }
        //наименование детали
        public string NameDetail { get; set; }
        //компания
        public string Company { get; set; }
        //цена
        public decimal Price { get; set; }
        //тип
        public string Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
