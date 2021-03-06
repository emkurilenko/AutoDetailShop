﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.DAL.Entities
{
    public class AutoDetail
    {
        
        public int Id { get; set; }
        //артикул детали
        public string Article { get; set; }
        public string Brend { get; set; }
        //цена
        public double Price { get; set; }
        //тип
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
