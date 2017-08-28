using FwStandard.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace RentalWorksWebApi
{
    public class ApplicationConfig
    {
        public SqlServerOptions DatabaseSettings { get; set; }
    }
}
