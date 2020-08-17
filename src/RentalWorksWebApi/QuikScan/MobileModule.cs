using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.QuikScan
{
    public abstract class MobileModule
    {
        protected FwApplicationConfig ApplicationConfig;

        public MobileModule(FwApplicationConfig applicationConfig)
        {
            this.ApplicationConfig = applicationConfig;
        }
    }
}
