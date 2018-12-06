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
        public List<decimal> data { get; set; }
        public List<string> backgroundColor { get; set; }
        public List<string> borderColor { get; set; }
        public int borderWidth { get; set; }
        public WidgetDataSet()
        {
            data = new List<decimal>();
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

        public string sql { get; set; }
        public string counterFieldName { get; set; }
        public string labelFieldName { get; set; }
        public string backgroundColorFieldName { get; set; } = "backgroundcolor";
        public string borderColorFieldName { get; set; } = "bordercolor";
        public double opacity { get; set; } = 0.4;

        //rentalworks-specific values
        public string locationId = "";
        public string warehouseId = "";
        public string departmentId = "";

        //date values
        public string dateBehavior = "";
        public string dateField = "";
        public DateTime? fromDate = null;
        public DateTime? toDate = null;

        public Widget()
        {
            data = new WidgetData();
            options = new WidgetOptions();
        }

        public Widget(string sql, string counterFieldName, string labelFieldName) : base()
        {
            this.sql = sql;
            this.counterFieldName = counterFieldName;
            this.labelFieldName = labelFieldName;
        }

        public void SetDbConfig(SqlServerConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public virtual async Task<bool> LoadAsync()
        {
            bool loaded = false;
            List<decimal> dataList = new List<decimal>();
            List<string> backgroundColor = new List<string>();
            List<string> borderColor = new List<string>();

            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                bool paramsAdded = false;
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add(sql);
                if (dataPoints != 0)
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @datapoints = @datapoints");
                    qry.AddParameter("@datapoints", dataPoints);
                    paramsAdded = true;
                }

                if (!string.IsNullOrEmpty(locationId))
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @locationid = @locationid");
                    qry.AddParameter("@locationid", locationId);
                    paramsAdded = true;
                }


                if (!string.IsNullOrEmpty(warehouseId))
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @warehouseid = @warehouseid");
                    qry.AddParameter("@warehouseid", warehouseId);
                    paramsAdded = true;
                }

                if (!string.IsNullOrEmpty(departmentId))
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @departmentid = @departmentid");
                    qry.AddParameter("@departmentid", departmentId);
                    paramsAdded = true;
                }

                if (!string.IsNullOrEmpty(dateBehavior))
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @datebehavior = @datebehavior");
                    qry.AddParameter("@datebehavior", dateBehavior);
                    paramsAdded = true;
                }

                if (!string.IsNullOrEmpty(dateField))
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @datefield = @datefield");
                    qry.AddParameter("@datefield", dateField);
                    paramsAdded = true;
                }

                if (fromDate != null)
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @fromdate = @fromdate");
                    qry.AddParameter("@fromdate", fromDate);
                    paramsAdded = true;
                }

                if (toDate != null)
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @todate = @todate");
                    qry.AddParameter("@todate", toDate);
                    paramsAdded = true;
                }

                qry.AddColumn(counterFieldName);
                qry.AddColumn(labelFieldName);
                qry.AddColumn(backgroundColorFieldName);
                qry.AddColumn(borderColorFieldName);
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    //int statusCount = Convert.ToInt32(table.Rows[r][0]);
                    //string quoteStatus = table.Rows[r][1].ToString();
                    //int statusColorInt = Convert.ToInt32(table.Rows[r][2]);
                    //string statusColorStr = FwConvert.OleColorToHtmlColor(statusColorInt, opacity);
                    //string borderColorStr = FwConvert.OleColorToHtmlColor(statusColorInt, 1);

                    //data.labels.Add(quoteStatus);
                    //dataList.Add(statusCount);
                    //backgroundColor.Add(statusColorStr);
                    //borderColor.Add(borderColorStr);


                    decimal value = Convert.ToDecimal(table.Rows[r][0]);
                    string label = table.Rows[r][1].ToString();
                    int colorInt = Convert.ToInt32(table.Rows[r][2]);
                    string colorStr = FwConvert.OleColorToHtmlColor(colorInt, opacity);
                    string borderColorStr = FwConvert.OleColorToHtmlColor(colorInt, 1);

                    data.labels.Add(label);
                    dataList.Add(value);
                    backgroundColor.Add(colorStr);
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
