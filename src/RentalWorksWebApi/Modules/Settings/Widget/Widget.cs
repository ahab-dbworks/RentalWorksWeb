using System.Collections.Generic;
using FwStandard.Models;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;

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
        public int fontSize { get; set; }
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
        public int dataPoints { get; set; }

        protected string sql { get; set; }
        protected string counterFieldName { get; set; }
        protected string labelFieldName { get; set; }
        protected string backgroundColorFieldName { get; set; } = "backgroundcolor";
        protected string borderColorFieldName { get; set; } = "bordercolor";
        protected double opacity { get; set; } = 0.4;


        public Widget()
        {
            data = new WidgetData();
            options = new WidgetOptions();
        }

        public void SetDbConfig(SqlServerConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public virtual async Task<bool> LoadAsync()
        {
            bool loaded = false;
            List<int> dataList = new List<int>();
            List<string> backgroundColor = new List<string>();
            List<string> borderColor = new List<string>();

            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add(sql);
                if (dataPoints != 0)
                {
                    qry.Add(" @datapoints = @datapoints");
                    qry.AddParameter("@datapoints", dataPoints);
                }
                qry.AddColumn(counterFieldName);
                qry.AddColumn(labelFieldName);
                qry.AddColumn(backgroundColorFieldName);
                qry.AddColumn(borderColorFieldName);
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    int statusCount = Convert.ToInt32(table.Rows[r][0]);
                    string quoteStatus = table.Rows[r][1].ToString();
                    int statusColorInt = Convert.ToInt32(table.Rows[r][2]);
                    string statusColorStr = FwConvert.OleColorToHtmlColor(statusColorInt, opacity);
                    string borderColorStr = FwConvert.OleColorToHtmlColor(statusColorInt, 1);

                    data.labels.Add(quoteStatus);
                    dataList.Add(statusCount);
                    backgroundColor.Add(statusColorStr);
                    borderColor.Add(borderColorStr);

                    loaded = true;
                }
            }

            data.datasets.Add(new WidgetDataSet());
            data.datasets[0].data = dataList;
            data.datasets[0].backgroundColor = backgroundColor;
            data.datasets[0].borderColor = borderColor;

            return loaded;

        }
        //------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------
    }
}
