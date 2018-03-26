using System.Collections.Generic;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetOrdersByStatus : Widget
    {
        //------------------------------------------------------------------------------------
        public WidgetOrdersByStatus() : base()
        {
            type = "bar";
            options.title.text = "Orders By Status";
            options.title.fontSize = 20;

            sql = "exec widgetordersbystatus";
            counterFieldName = "ordercount";
            labelFieldName = "status";
        }
        //------------------------------------------------------------------------------------
    }
}
