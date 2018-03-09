using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetCustomersByType : Widget
    {
        public WidgetCustomersByType() : base()
        {
            type = "horizontalBar";
            options.title.text = "Customers by Type";
            options.title.fontSize = 20;
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
                qry.Add("exec widgetcustomersbytype");
                qry.AddColumn("customercount");
                qry.AddColumn("customertype");
                qry.AddColumn("backgroundcolor");
                qry.AddColumn("bordercolor");
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    int customerCount = Convert.ToInt32(table.Rows[r][0]);
                    string customerType = table.Rows[r][1].ToString();
                    int customerTypeColorInt = Convert.ToInt32(table.Rows[r][2]);
                    double opacity = 0.2;
                    string customerTypeColorStr = FwConvert.OleColorToHtmlColor(customerTypeColorInt, opacity);
                    string borderColorStr = FwConvert.OleColorToHtmlColor(customerTypeColorInt, 1);



                    data.labels.Add(customerType);
                    dataList.Add(customerCount);
                    backgroundColor.Add(customerTypeColorStr);
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