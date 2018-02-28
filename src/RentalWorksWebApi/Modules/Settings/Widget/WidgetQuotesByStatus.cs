﻿using System.Collections.Generic;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetQuotesByStatus : Widget
    {
        public WidgetQuotesByStatus() : base()
        {
            type = "bar";
            options.title.text = "Quotes By Status";
            options.title.fontSize = 20;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> LoadAsync()
        {
            bool loaded = false;
            List<int> dataList = new List<int>();
            List<string> backgroundColor = new List<string>();
            List<string> borderColor = new List<string>();

            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add("exec widgetquotesbystatus");
                qry.AddColumn("quotecount");
                qry.AddColumn("status");
                qry.AddColumn("backgroundcolor");
                qry.AddColumn("bordercolor");
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    int statusCount = Convert.ToInt32(table.Rows[r][0]);
                    string quoteStatus = table.Rows[r][1].ToString();
                    int statusColorInt = Convert.ToInt32(table.Rows[r][2]);
                    double opacity = 0.2;
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
    }
}
