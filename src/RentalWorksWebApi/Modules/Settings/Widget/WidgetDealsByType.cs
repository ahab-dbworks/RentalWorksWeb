using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetDealsByType : Widget
    {
        public WidgetDealsByType() : base()
        {
            type = "horizontalBar";
            options.title.text = "Deals by Type";
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
                qry.Add("exec widgetdealsbytype");
                qry.AddColumn("dealcount");
                qry.AddColumn("dealtype");
                qry.AddColumn("backgroundcolor");
                qry.AddColumn("bordercolor");
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true, doParse: false);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    int dealCount = Convert.ToInt32(table.Rows[r][0]);
                    string dealType = table.Rows[r][1].ToString();
                    int dealTypeColorInt = Convert.ToInt32(table.Rows[r][2]);
                    double opacity = 0.2;
                    string dealTypeColorStr = FwConvert.OleColorToHtmlColor(dealTypeColorInt, opacity);
                    string borderColorStr = FwConvert.OleColorToHtmlColor(dealTypeColorInt, 1);



                    data.labels.Add(dealType);
                    dataList.Add(dealCount);
                    backgroundColor.Add(dealTypeColorStr);
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