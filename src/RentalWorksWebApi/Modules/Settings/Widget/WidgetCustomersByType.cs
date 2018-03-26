using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetCustomersByType : Widget
    {
        //------------------------------------------------------------------------------------
        public WidgetCustomersByType() : base()
        {
            type = "horizontalBar";
            options.title.text = "Customers by Type";
            options.title.fontSize = 20;

            sql = "exec widgetcustomersbytype";
            counterFieldName = "customercount";
            labelFieldName = "customertype";
        }
        //------------------------------------------------------------------------------------
    }
};