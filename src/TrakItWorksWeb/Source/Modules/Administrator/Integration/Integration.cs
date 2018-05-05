using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.SqlServer.Entities;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace TrakItWorksWeb.Modules
{
    class Integration : FwModule
    {
        //---------------------------------------------------------------------------------------------
        protected override string getTabName()
        {
            return "RentalWorks Integration";
        }
        //---------------------------------------------------------------------------------------------
    }
}
