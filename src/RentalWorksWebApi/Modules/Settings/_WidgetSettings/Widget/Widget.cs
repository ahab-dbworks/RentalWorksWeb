using System.Collections.Generic;
using FwStandard.Models;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;

namespace WebApi.Modules.Settings.WidgetSettings.Widget
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
        public bool stacked { get; set; }
        public WidgetAxis()
        {
            ticks = new WidgetAxisTicks();
            stacked = false;
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

        public string apiName { get; set; }
        public string procedureName { get; set; }
        public string counterFieldName { get; set; }
        public string label1FieldName { get; set; }
        public string label2FieldName { get; set; }
        public string backgroundColorFieldName { get; set; } = "backgroundcolor";
        public string borderColorFieldName { get; set; } = "bordercolor";
        public double opacity { get; set; } = 0.75;
        public bool stacked { get { return options.scales.yAxes[0].stacked; } set { options.scales.yAxes[0].stacked = value; options.scales.xAxes[0].stacked = value; } }


        private int counterFieldIndex;
        private int label1FieldIndex;
        private int label2FieldIndex;
        private int backgroundColorFieldIndex;
        private int borderColorFieldIndex;


        //rentalworks-specific values
        public string locationId = "";
        public string warehouseId = "";
        public string departmentId = "";
        public string locationCodes = "";  // for output 

        //date values
        public string dateBehaviorId = "";
        public string dateField = "";
        public DateTime? fromDate = null;
        public DateTime? toDate = null;

        //------------------------------------------------------------------------------------
        public Widget(WidgetLogic l)
        {
            data = new WidgetData();
            options = new WidgetOptions();
            options.title.text = l.Widget;
            options.title.fontSize = 16;
            type = l.DefaultType;
            apiName = l.ApiName;
            procedureName = l.ProcedureName;
            counterFieldName = l.CounterFieldName;
            label1FieldName = l.Label1FieldName;
            label2FieldName = l.Label2FieldName;
        }
        //------------------------------------------------------------------------------------
        public void SetDbConfig(SqlServerConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        protected virtual bool PopulateDataSets(FwJsonDataTable table)
        {
            bool loaded = false;

            List<string> label1List = new List<string>();
            List<string> label2List = new List<string>();


            counterFieldIndex = table.GetColumnNo(counterFieldName);
            label1FieldIndex = table.GetColumnNo(label1FieldName);
            label2FieldIndex = table.GetColumnNo(label2FieldName);
            backgroundColorFieldIndex = table.GetColumnNo(backgroundColorFieldName);
            borderColorFieldIndex = table.GetColumnNo(borderColorFieldName);

            for (int r = 0; r < table.Rows.Count; r++)
            {
                string s = table.Rows[r][label1FieldIndex].ToString();

                if (!label1List.Contains(s))
                {
                    label1List.Add(s);
                }
            }
            if (!string.IsNullOrEmpty(label2FieldName))
            {
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    string s = table.Rows[r][label2FieldIndex].ToString();

                    if (!label2List.Contains(s))
                    {
                        label2List.Add(s);
                    }
                }
            }


            if (label2List.Count == 0)  // 2D chart
            {
                List<decimal> dataList = new List<decimal>();
                List<string> backgroundColor = new List<string>();
                List<string> borderColor = new List<string>();

                data.labels = label1List;

                foreach (string label in label1List)
                {
                    decimal value = 0;
                    string datalabel = "";
                    int colorInt = 0;
                    int borderColorInt = 0;
                    string colorStr = "";
                    string borderColorStr = "";

                    for (int r = 0; r < table.Rows.Count; r++)
                    {
                        datalabel = table.Rows[r][label1FieldIndex].ToString();
                        if (datalabel.Equals(label))
                        {
                            value = Convert.ToDecimal(table.Rows[r][counterFieldIndex]);
                            colorInt = Convert.ToInt32(table.Rows[r][backgroundColorFieldIndex]);
                            colorStr = FwConvert.OleColorToHtmlColor(colorInt, opacity);

                            borderColorInt = Convert.ToInt32(table.Rows[r][borderColorFieldIndex]);
                            borderColorStr = FwConvert.OleColorToHtmlColor(borderColorInt, 1);

                            dataList.Add(value);
                            backgroundColor.Add(colorStr);
                            borderColor.Add(borderColorStr);

                        }
                        loaded = true;
                    }
                }

                WidgetDataSet wds = new WidgetDataSet();
                wds.data = dataList;
                wds.backgroundColor = backgroundColor;
                wds.borderColor = borderColor;
                data.datasets.Add(wds);

            }
            else //3D chart
            {
                data.labels = label2List;

                foreach (string label in label1List)
                {
                    List<decimal> dataList = new List<decimal>();
                    List<string> backgroundColor = new List<string>();
                    List<string> borderColor = new List<string>();

                    foreach (string label2 in label2List)
                    {
                        decimal value = 0;
                        string datalabel = "";
                        string datalabel2 = "";
                        int colorInt = 0;
                        int borderColorInt = 0;
                        string colorStr = "";
                        string borderColorStr = "";

                        for (int r = 0; r < table.Rows.Count; r++)
                        {
                            datalabel = table.Rows[r][label1FieldIndex].ToString();
                            datalabel2 = table.Rows[r][label2FieldIndex].ToString();
                            if ((datalabel.Equals(label)) && (datalabel2.Equals(label2)))
                            {
                                value = Convert.ToDecimal(table.Rows[r][counterFieldIndex]);
                                colorInt = Convert.ToInt32(table.Rows[r][backgroundColorFieldIndex]);
                                borderColorInt = Convert.ToInt32(table.Rows[r][borderColorFieldIndex]);
                                break;
                            }
                        }
                        colorStr = FwConvert.OleColorToHtmlColor(colorInt, opacity);
                        borderColorStr = FwConvert.OleColorToHtmlColor(borderColorInt, 1);

                        dataList.Add(value);
                        backgroundColor.Add(colorStr);
                        borderColor.Add(borderColorStr);

                        loaded = true;
                    }

                    WidgetDataSet wds = new WidgetDataSet();
                    wds.data = dataList;
                    wds.label = label;
                    wds.backgroundColor = backgroundColor;
                    wds.borderColor = borderColor;
                    data.datasets.Add(wds);
                }

                options.legend.display = true;

            }



            return loaded;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync()
        {
            bool loaded = false;

            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                bool paramsAdded = false;
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add("exec " + procedureName);
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

                if (!string.IsNullOrEmpty(dateBehaviorId))
                {
                    if (paramsAdded)
                    {
                        qry.Add(",");
                    }
                    qry.Add(" @datebehaviorid = @datebehaviorid");
                    qry.AddParameter("@datebehaviorid", dateBehaviorId);
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

                if (paramsAdded)
                {
                    qry.Add(",");
                }
                qry.Add(" @outputfromdate = @outputfromdate output");
                qry.AddParameter("@outputfromdate", System.Data.SqlDbType.Date, System.Data.ParameterDirection.Output);
                paramsAdded = true;

                if (paramsAdded)
                {
                    qry.Add(",");
                }
                qry.Add(" @outputtodate = @outputtodate output");
                qry.AddParameter("@outputtodate", System.Data.SqlDbType.Date, System.Data.ParameterDirection.Output);
                paramsAdded = true;

                if (paramsAdded)
                {
                    qry.Add(",");
                }
                qry.Add(" @outputloccodes = @outputloccodes output");
                qry.AddParameter("@outputloccodes", System.Data.SqlDbType.VarChar, System.Data.ParameterDirection.Output);
                paramsAdded = true;

                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);

                if (qry.GetParameter("@outputfromdate").FieldValue == DBNull.Value)
                {
                    fromDate = null;
                    toDate = null;
                }
                else
                {
                    fromDate = qry.GetParameter("@outputfromdate").ToDateTime();
                    toDate = qry.GetParameter("@outputtodate").ToDateTime();
                }
                locationCodes = qry.GetParameter("@outputloccodes").ToString();

                loaded = PopulateDataSets(table);
            }

            return loaded;

        }
        //------------------------------------------------------------------------------------
    }
}
