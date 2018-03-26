using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetOrdersByAgent : Widget
    {
        //------------------------------------------------------------------------------------
        public WidgetOrdersByAgent() : base()
        {
            type = "pie";
            options.title.text = "Orders By Agent";
            options.title.fontSize = 20;

            sql = "exec widgetordersbyagent";
            counterFieldName = "ordercount";
            labelFieldName = "agent";
        }
        //------------------------------------------------------------------------------------
    }
};