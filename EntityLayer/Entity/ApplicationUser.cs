using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class ApplicationUser: IdentityUser
    {
        public User? User { get; set; }
    }
}
