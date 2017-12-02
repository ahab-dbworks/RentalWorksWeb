using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using FwStandard.Models;

namespace RentalWorksWebApi
{
    public class ApplicationConfig
    {
        public SqlServerConfig DatabaseSettings { get; set; }
    }
}
