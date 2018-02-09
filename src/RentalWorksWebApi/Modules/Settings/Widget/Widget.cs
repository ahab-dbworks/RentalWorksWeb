using System.Collections.Generic;
using FwStandard.Models;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Widget
{
      
    //------------------------------------------------------------------------------------
    public class WidgetDataSet
    {
        public string label { get; set; }
        public List<int> data { get; set; }
        public List<string> backgroundColor { get; set; }
        public List<string> borderColor { get; set; }
        public int borderWidth { get; set; }
        public WidgetDataSet()
        {
            data = new List<int>();
            backgroundColor = new List<string>();
            borderColor = new List<string>();
            borderWidth = 1;
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetData
    {
        public List<string> labels { get; set; }
        public List<WidgetDataSet> datasets { get; set; }
        public WidgetData()
        {
            labels = new List<string>();
            datasets = new List<WidgetDataSet>();
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetTitle
    {
        public bool display { get; set; }
        public string text { get; set; }
        public WidgetTitle()
        {
            display = true;
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetLegend
    {
        public bool display { get; set; }
        public WidgetLegend()
        {
            display = false;
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetAxisTicks
    {
        public bool beginAtZero { get; set; }
        public WidgetAxisTicks()
        {
            beginAtZero = true;
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetAxis
    {
        public WidgetAxisTicks ticks { get; set; }
        public WidgetAxis()
        {
            ticks = new WidgetAxisTicks();
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetScales
    {
        public List<WidgetAxis> xAxes { get; set; }

        public List<WidgetAxis> yAxes { get; set; }
        public WidgetScales()
        {
            yAxes = new List<WidgetAxis>();
            yAxes.Add(new WidgetAxis());
            xAxes = new List<WidgetAxis>();
            xAxes.Add(new WidgetAxis());
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetOptions
    {
        public WidgetTitle title { get; set; }
        public WidgetLegend legend { get; set; }
        public WidgetScales scales { get; set; }
        public bool responsive { get; set; }
        public bool maintainAspectRatio { get; set; }
        public WidgetOptions()
        {
            title = new WidgetTitle();
            legend = new WidgetLegend();
            scales = new WidgetScales();
            responsive = true;
            maintainAspectRatio = true;
        }
    }
    //------------------------------------------------------------------------------------
    public /*abstract*/ class Widget
    {
        protected SqlServerConfig _dbConfig { get; set; }

        public string type { get; set; }
        public WidgetData data { get; set; }
        public WidgetOptions options { get; set; }

        public Widget()
        {
            data = new WidgetData();
            options = new WidgetOptions();
        }

        public void SetDbConfig(SqlServerConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //public abstract Task<bool> LoadAsync();
        //------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------
    }
}
