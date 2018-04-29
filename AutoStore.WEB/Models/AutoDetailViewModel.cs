using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStore.WEB.Models
{
    public class AutoDetailViewModel
    {
        public int Id { get; set; }
        public string Article { get; set; }
        public string Brend { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
    }
}