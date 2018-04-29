using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoStore.WEB.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public int AutoDetailId { get; set; }
        public string ClientProfileId { get; set; }
        public DateTime Date { get; set; }
    }
}