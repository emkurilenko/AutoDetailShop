﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.DAL.Entities
{
   public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile GetClientProfile{get; set;}
    }
}
