using System.Collections.Generic;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System;

namespace WebApi.Modules.Settings.Widget
{
    public class WidgetBillingByAgentByMonth : Widget
    {
        public WidgetBillingByAgentByMonth() : base()
        {
            type = "bar";
            options.title.text = "Billing By Agent By Month";
            options.title.fontSize = 20;
        }
        //------------------------------------------------------------------------------------
        public override async Task<bool> LoadAsync()
        {
            bool loaded = false;
            List<string> monthYearList = new List<string>();
            List<string> agentList = new List<string>();

            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add("exec widgetbillingbyagentbymonth");
                if (dataPoints != 0)
                {
                    qry.Add(" @datapoints = @datapoints");
                    qry.AddParameter("@datapoints", dataPoints);
                }
                qry.AddColumn("billingamount");       //00
                qry.AddColumn("agent");               //01
                qry.AddColumn("year");                //02
                qry.AddColumn("month");               //03
                qry.AddColumn("monthyear");           //04
                qry.AddColumn("backgroundcolor");     //05
                qry.AddColumn("bordercolor");         //06
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    string monthYear = table.Rows[r][4].ToString();
                    if (!monthYearList.Contains(monthYear))
                    {
                        monthYearList.Add(monthYear);
                    }
                }
                data.labels = monthYearList;
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    string agent = table.Rows[r][1].ToString();
                    if (!agentList.Contains(agent))
                    {
                        agentList.Add(agent);
                    }
                }
                foreach (string agent in agentList)
                {
                    loaded = true;

                    WidgetDataSet ds = new WidgetDataSet();

                    double opacity = 0.2;
                    int statusColorInt = 0;
                    string statusColorStr = FwConvert.OleColorToHtmlColor(statusColorInt, opacity);
                    string borderColorStr = FwConvert.OleColorToHtmlColor(statusColorInt, 1);

                    List<int> dataList = new List<int>();
                    List<string> backgroundColor = new List<string>();
                    List<string> borderColor = new List<string>();

                    foreach (string monthYear in monthYearList)
                    {
                        bool agentMonthYearFound = false;


                        for (int r = 0; r < table.Rows.Count; r++)
                        {
                            string dataAgent = table.Rows[r][1].ToString();
                            string dataMonthYear = table.Rows[r][4].ToString();
                            if (dataAgent.Equals(agent))
                            {
                                statusColorInt = Convert.ToInt32(table.Rows[r][5]);
                                statusColorStr = FwConvert.OleColorToHtmlColor(statusColorInt, opacity);
                                borderColorStr = FwConvert.OleColorToHtmlColor(statusColorInt, 1);

                                if (dataMonthYear.Equals(monthYear))
                                {
                                    agentMonthYearFound = true;
                                    decimal billingAmountDec = Convert.ToDecimal(table.Rows[r][0]);
                                    int billingAmountInt = Convert.ToInt32(billingAmountDec);

                                    dataList.Add(billingAmountInt);
                                    backgroundColor.Add(statusColorStr);
                                    borderColor.Add(borderColorStr);

                                }
                            }
                        }


                        if (!agentMonthYearFound)
                        {
                            dataList.Add(0);
                            backgroundColor.Add(statusColorStr);
                            borderColor.Add(borderColorStr);
                        }
                    }


                    ds.label = agent;
                    ds.data = dataList;
                    ds.backgroundColor = backgroundColor;
                    ds.borderColor = borderColor;
                    data.datasets.Add(ds);

                }
            }

            options.legend.display = true;

            return loaded;

        }
        //------------------------------------------------------------------------------------
    }
}
