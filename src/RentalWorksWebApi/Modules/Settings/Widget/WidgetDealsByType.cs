using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetDealsByType : Widget
    {
        //------------------------------------------------------------------------------------
        public WidgetDealsByType() : base()
        {
            type = "horizontalBar";
            options.title.text = "Deals by Type";
            options.title.fontSize = 20;

            sql = "exec widgetdealsbytype";
            counterFieldName = "dealcount";
            labelFieldName = "dealtype";
        }
        //------------------------------------------------------------------------------------
    }
};