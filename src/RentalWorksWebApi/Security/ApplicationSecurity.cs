using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Security
{
    public class ApplicationSecurity
    {
        [SecurityApplicationNode(Guid: "")] 
        public object Orders;
    }
}
