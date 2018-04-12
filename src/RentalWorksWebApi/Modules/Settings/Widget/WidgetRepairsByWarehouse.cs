using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetRepairsByWarehouse : Widget
    {
        //------------------------------------------------------------------------------------
        public WidgetRepairsByWarehouse() : base()
        {
            type = "horizontalBar";
            options.title.text = "Repairs By Warehouse";
            options.title.fontSize = 20;

            sql = "exec widgetrepairsbywarehouse";
            counterFieldName = "repaircount";
            labelFieldName = "warehouse";
        }
        //------------------------------------------------------------------------------------
    }
};