using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoStore.WEB.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}