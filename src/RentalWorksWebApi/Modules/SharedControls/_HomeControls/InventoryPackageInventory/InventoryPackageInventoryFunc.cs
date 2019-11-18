using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.InventoryPackageInventory
{
    public class SortInventoryPackageInventorysRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> InventoryPackageInventoryIds { get; set; } = new List<string>();
    }

    public static class InventoryPackageInventoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortInventoryPackageInventorys(FwApplicationConfig appConfig, FwUserSession userSession, SortInventoryPackageInventorysRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "packageitem";
            r2.IdFieldNames.Add("packageitemid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            SortItemsResponse response = new SortItemsResponse();

            bool proceedWithSort = true;

            if (proceedWithSort)
            {
                if (request.InventoryPackageInventoryIds.Count == 0)
                {
                    response.success = false;
                    response.msg = "Nothing to sort.";
                    proceedWithSort = false;
                }
            }

            if (proceedWithSort)
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    int i = 0;
                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("select pi.packageitemid, pi.primaryflg, pi.orderby     ");
                    qry.Add(" from  packageitem pi with (nolock)                    ");
                    qry.Add(" where pi.packageitemid in                             ");
                    qry.Add("        (");
                    foreach (string itemId in request.InventoryPackageInventoryIds)
                    {
                        if (i > 0)
                        {
                            qry.Add(",");
                        }
                        qry.Add("@packageitemid" + i.ToString());
                        qry.AddParameter("@packageitemid" + i.ToString(), itemId);
                        i++;
                    }
                    qry.Add("        )");
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                    foreach (List<object> row in dt.Rows)
                    {
                        string packageItemId = row[dt.GetColumnNo("packageitemid")].ToString();
                        bool isPrimary = FwConvert.ToBoolean(row[dt.GetColumnNo("primaryflg")].ToString());
                        int orderby = FwConvert.ToInt32(row[dt.GetColumnNo("orderby")].ToString());
                        if (isPrimary || orderby.Equals(1))
                        {
                            request.InventoryPackageInventoryIds.Remove(packageItemId);
                            request.InventoryPackageInventoryIds.Insert(0, packageItemId);
                            //response.success = false;
                            //response.msg = "Cannot re-sort the Primary Item.";
                            //proceedWithSort = false;
                            //break;

                        }
                    }
                }
            }

            if (proceedWithSort)
            {
                if (request.InventoryPackageInventoryIds.Count == 0)
                {
                    response.success = false;
                    response.msg = "Nothing to sort.";
                    proceedWithSort = false;
                }
            }


            if (proceedWithSort)
            {
                foreach (string itemId in request.InventoryPackageInventoryIds)
                {
                    List<string> idCombo = new List<string>();
                    idCombo.Add(itemId);
                    r2.Ids.Add(idCombo);
                }
                response = await AppFunc.SortItems(appConfig, userSession, r2);
            }

            return response;
        }
    }
    //-------------------------------------------------------------------------------------------------------    
}

