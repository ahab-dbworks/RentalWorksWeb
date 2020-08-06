using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.QuikScan
{
    public abstract class QuikScanModule
    {
        protected FwApplicationConfig ApplicationConfig;

        public QuikScanModule(FwApplicationConfig applicationConfig)
        {
            this.ApplicationConfig = applicationConfig;
        }
    }
}
