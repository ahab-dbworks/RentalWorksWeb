using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.Widgets
{
    public class WidgetOrdersByAgent : Widget
    {
        public WidgetOrdersByAgent() : base()
        {
            type = "pie";
            options.title.text = "Orders By Agent";
        }

        public async Task<bool> LoadAsync()
        {
            bool loaded = false;
            List<int> dataList = new List<int>();
            List<string> backgroundColor = new List<string>();
            List<string> borderColor = new List<string>();

            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add("exec widgetordersbyagent");
                qry.AddColumn("ordercount");
                qry.AddColumn("status");
                qry.AddColumn("backgroundcolor");
                qry.AddColumn("bordercolor");
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true, doParse: false);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    int statusCount = Convert.ToInt32(table.Rows[r][0]);
                    string orderStatus = table.Rows[r][1].ToString();

                    data.labels.Add(orderStatus);
                    dataList.Add(statusCount);
                    //need to load backgroundColor here
                    //need to load borderColor here

                    loaded = true;
                }
            }

            data.datasets.Add(new WidgetDataSet());
            data.datasets[0].data = dataList;

            backgroundColor.Add("rgba(153, 102, 255, 0.2)");
            backgroundColor.Add("rgba(75, 192, 192, 0.2)");
            backgroundColor.Add("rgba(54, 162, 235, 0.2)");
            backgroundColor.Add("rgba(75, 192, 192, 0.2)");
            backgroundColor.Add("rgba(153, 102, 255, 0.2)");
            backgroundColor.Add("rgba(255, 159, 64, 0.2)");

            borderColor.Add("rgba(153, 102, 255, 1");
            borderColor.Add("rgba(75, 192, 192, 1)");
            borderColor.Add("rgba(54, 162, 235, 1)");
            borderColor.Add("rgba(75, 192, 192, 1)");
            borderColor.Add("rgba(153, 102, 255, 1)");
            borderColor.Add("rgba(255, 159, 64, 1)");

            data.datasets[0].backgroundColor = backgroundColor;
            data.datasets[0].borderColor = borderColor;

            return loaded;
        }
    }
};