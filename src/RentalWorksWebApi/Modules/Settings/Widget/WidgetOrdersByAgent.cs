using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.Widget
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
                qry.AddColumn("agent");
                qry.AddColumn("backgroundcolor");
                qry.AddColumn("bordercolor");
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true, doParse: false);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    int orderCount = Convert.ToInt32(table.Rows[r][0]);
                    string agent = table.Rows[r][1].ToString();
                    int agentColorInt = Convert.ToInt32(table.Rows[r][2]);
                    double opacity = 0.2;
                    string agentColorStr = FwConvert.OleColorToHtmlColor(agentColorInt, opacity);
                    string borderColorStr = FwConvert.OleColorToHtmlColor(agentColorInt, 1);

                    data.labels.Add(agent);
                    dataList.Add(orderCount);
                    backgroundColor.Add(agentColorStr);
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
    }
};