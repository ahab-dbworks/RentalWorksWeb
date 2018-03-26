using System.Collections.Generic;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetQuotesByStatus : Widget
    {
        //------------------------------------------------------------------------------------
        public WidgetQuotesByStatus() : base()
        {
            type = "bar";
            options.title.text = "Quotes By Status";
            options.title.fontSize = 20;

            sql = "exec widgetquotesbystatus";
            counterFieldName = "quotecount";
            labelFieldName = "status";
        }
        //------------------------------------------------------------------------------------
    }
}
