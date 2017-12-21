using System.Collections.Generic;

namespace WebApi.Modules.Settings.Widgets
{
    //------------------------------------------------------------------------------------
    public class WidgetDataSet
    {
        public List<int> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
        public List<string> BorderColor { get; set; }
        public int BorderWidth { get; set; }
        public WidgetDataSet()
        {
            Data = new List<int>();
            BackgroundColor = new List<string>();
            BorderColor = new List<string>();
            BorderWidth = 1;
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetData
    {
        public List<string> Labels { get; set; }
        public List<WidgetDataSet> DataSets { get; set; }
        public WidgetData()
        {
            Labels = new List<string>();
            DataSets = new List<WidgetDataSet>();
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetTitle
    {
        public bool Display { get; set; }
        public string Text { get; set; }
        public WidgetTitle()
        {
            Display = true;
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetLegend
    {
        public bool Display { get; set; }
        public WidgetLegend()
        {
            Display = true;
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetAxisTicks
    {
        public bool BeginAtZero { get; set; }
        public WidgetAxisTicks()
        {
            BeginAtZero = true;
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetAxis
    {
        public WidgetAxisTicks Ticks { get; set; }
        public WidgetAxis()
        {
            Ticks = new WidgetAxisTicks();
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetScales
    {
        public List<WidgetAxis> YAxes { get; set; }
        public WidgetScales()
        {
            YAxes = new List<WidgetAxis>();
            YAxes.Add(new WidgetAxis());
        }
    }
    //------------------------------------------------------------------------------------
    public class WidgetOptions
    {
        public WidgetTitle Title { get; set; }
        public WidgetLegend Legend { get; set; }
        public WidgetScales Scales { get; set; }
        public bool Responsive { get; set; }
        public bool MaintainAspectRatio { get; set; }
        public WidgetOptions()
        {
            Title = new WidgetTitle();
            Legend = new WidgetLegend();
            Scales = new WidgetScales();
        }
    }
    //------------------------------------------------------------------------------------
    public class Widget
    {
        public string ChartType { get; set; }
        public WidgetData Data { get; set; }
        public WidgetOptions Options { get; set; }

        public Widget()
        {
            Data = new WidgetData();
            Options = new WidgetOptions();
        }
    }
    //------------------------------------------------------------------------------------

}
