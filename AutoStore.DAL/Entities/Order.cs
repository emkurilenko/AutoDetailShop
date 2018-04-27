using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public int AutoDetailId { get; set; }
        public virtual AutoDetail AutoDetail { get; set; }

        [ForeignKey("ClientProfile")]
        public string ClientProfileId { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }

    }
}
