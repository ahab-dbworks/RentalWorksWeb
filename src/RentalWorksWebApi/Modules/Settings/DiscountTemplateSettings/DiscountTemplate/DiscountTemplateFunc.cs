using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Settings.DiscountItem;

namespace WebApi.Modules.Settings.DiscountTemplateSettings.DiscountTemplate
{
    public class AddAllDiscountTemplateItemsRequest
    {
        [Required]
        public string DiscountTemplateId { get; set; }
        [Required]
        public string WarehouseId { get; set; }  // for default rates
        [Required]
        public string RecType { get; set; }
    }

    public class AddAllDiscountTemplateItemsResponse : TSpStatusResponse { }

    public static class DiscountTemplateFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<AddAllDiscountTemplateItemsResponse> AddAllItems(FwApplicationConfig appConfig, FwUserSession userSession, AddAllDiscountTemplateItemsRequest request)
        {
            AddAllDiscountTemplateItemsResponse response = new AddAllDiscountTemplateItemsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {

                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select m.masterid, m.categoryid, m.subcategoryid, c.inventorydepartmentid, m.availfor, ");
                qry.Add("       m.master,                                                                       ");
                qry.Add("       mw.hourlyrate, mw.dailyrate,                                                    ");
                qry.Add("       mw.weeklyrate, mw.week2rate, mw.week3rate, mw.week4rate, mw.week5rate,          ");
                qry.Add("       mw.monthlyrate,                                                                 ");
                qry.Add("       mw.price                                                                        ");
                qry.Add(" from  master m with (nolock)                                                          ");
                qry.Add("           join masterwh  mw with (nolock) on (m.masterid     = mw.masterid)           ");
                qry.Add("           join warehouse w  with (nolock) on (mw.warehouseid = w.warehouseid)         ");
                qry.Add("           join category  c  with (nolock) on (m.categoryid   = c.categoryid)          ");
                qry.Add(" where m.availfor     = @rectype                                                       ");
                qry.Add(" and   mw.warehouseid = @warehouseid                                                   ");
                qry.Add(" and   m.inactive    <> 'T'                                                            ");
                qry.Add(" and   w.inactive    <> 'T'                                                            ");
                qry.Add(" and   not exists (select *                                                            ");
                qry.Add("                    from  discountitem di with(nolock)                                 ");
                qry.Add("                    where di.discounttemplateid = @discounttemplateid                  ");
                qry.Add("                    and   di.masterid           = m.masterid)                          ");
                qry.AddParameter("@rectype", request.RecType);
                qry.AddParameter("@discounttemplateid", request.DiscountTemplateId);
                qry.AddParameter("@warehouseid", request.WarehouseId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                if (dt.TotalRows > 0)
                {
                    await FwSqlData.StartMeter(conn, appConfig.DatabaseSettings, request.DiscountTemplateId, "Adding Items to Discount Template", dt.TotalRows);
                    foreach (List<object> row in dt.Rows)
                    {
                        string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                        string description = row[dt.GetColumnNo("master")].ToString();
                        string inventoryTypeId = row[dt.GetColumnNo("inventorydepartmentid")].ToString();
                        string categoryId = row[dt.GetColumnNo("categoryid")].ToString();
                        string subCategoryId = row[dt.GetColumnNo("subcategoryid")].ToString();
                        string recType = row[dt.GetColumnNo("availfor")].ToString();
                        decimal hourlyRate = FwConvert.ToDecimal(row[dt.GetColumnNo("hourlyrate")].ToString());
                        decimal dailyRate = FwConvert.ToDecimal(row[dt.GetColumnNo("dailyrate")].ToString());
                        decimal weeklyRate = FwConvert.ToDecimal(row[dt.GetColumnNo("weeklyrate")].ToString());
                        decimal week2Rate = FwConvert.ToDecimal(row[dt.GetColumnNo("week2rate")].ToString());
                        decimal week3Rate = FwConvert.ToDecimal(row[dt.GetColumnNo("week3rate")].ToString());
                        decimal week4Rate = FwConvert.ToDecimal(row[dt.GetColumnNo("week4rate")].ToString());
                        decimal week5Rate = FwConvert.ToDecimal(row[dt.GetColumnNo("week5rate")].ToString());
                        decimal monthlyRate = FwConvert.ToDecimal(row[dt.GetColumnNo("monthlyrate")].ToString());
                        decimal price = FwConvert.ToDecimal(row[dt.GetColumnNo("price")].ToString());

                        await FwSqlData.StepMeter(conn, appConfig.DatabaseSettings, request.DiscountTemplateId, newCaption: "Adding " + description, steps: 0);
                        DiscountItemLogic item = new DiscountItemLogic();
                        item.SetDependencies(appConfig, userSession);
                        item.DiscountTemplateId = request.DiscountTemplateId;
                        item.InventoryId = inventoryId;
                        item.InventoryTypeId = inventoryTypeId;
                        item.CategoryId = categoryId;
                        item.SubCategoryId = subCategoryId;
                        item.RecType = recType;
                        //item.HourlyRate = hourlyRate;  //future
                        item.DailyRate = dailyRate;
                        item.WeeklyRate = weeklyRate;
                        item.Week2Rate = week2Rate;
                        item.Week3Rate = week3Rate;
                        item.Week4Rate = week4Rate;
                        item.Week5Rate = week5Rate;
                        item.MonthlyRate = monthlyRate;
                        //item.Price = price;  //future
                        await item.SaveAsync();
                        await FwSqlData.StepMeter(conn, appConfig.DatabaseSettings, request.DiscountTemplateId);
                    }
                    await FwSqlData.FinishMeter(conn, appConfig.DatabaseSettings, request.DiscountTemplateId);
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
