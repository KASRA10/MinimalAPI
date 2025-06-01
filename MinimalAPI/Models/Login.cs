using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.Models
{
    public class Login
    {
        public required string Login_UserName { get; set; }
        public required string Login_PassWord { get; set; }
    }
}
