using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using RentalWorksAPI.api.v1.Models;
using System.Collections.Generic;
using System.Data;

namespace RentalWorksAPI.api.v1.Data
{
    public class InventoryData
    {
        //----------------------------------------------------------------------------------------------------
        public static List<RentalItem> GetRentalItemsAsOf(FwDateTime asofdate)
        {
            FwSqlCommand qry;
            List<RentalItem> result = new List<RentalItem>();
            DataTable dt = new DataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select a.*");
            qry.Add("  from apirest_rentalinventoryview a with (nolock)");
            qry.Add(" where a.datestamp > @asofdate");
            //qry.Add("order by inventorydepartmentorderby, categoryorderby, subcategoryorderby"); //2016-09-28: Ahab: need to remove
            qry.AddParameter("@asofdate", asofdate.GetSqlDate());
            dt = qry.QueryToTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RentalItem newItem = new RentalItem();

                newItem.masterid                   = dt.Rows[i]["masterid"].ToString().TrimEnd();
                newItem.masterno                   = dt.Rows[i]["masterno"].ToString().TrimEnd();
                newItem.master                     = dt.Rows[i]["master"].ToString().TrimEnd();
                newItem.inactive                   = new FwDatabaseField(dt.Rows[i]["inactive"]).ToBoolean();
                newItem.availfor                   = dt.Rows[i]["availfor"].ToString().TrimEnd();
                newItem.availfrom                  = dt.Rows[i]["availfrom"].ToString().TrimEnd();
                newItem.manufacturer               = dt.Rows[i]["manufacturer"].ToString().TrimEnd();
                newItem.category                   = dt.Rows[i]["category"].ToString().TrimEnd();
                newItem.inventorydepartmentid      = dt.Rows[i]["inventorydepartmentid"].ToString().TrimEnd();
                newItem.inventorydepartment        = dt.Rows[i]["inventorydepartment"].ToString().TrimEnd();
                newItem.unit                       = dt.Rows[i]["unit"].ToString().TrimEnd();
                newItem.unitid                     = dt.Rows[i]["unitid"].ToString().TrimEnd();
                newItem.itemclass                  = dt.Rows[i]["itemclass"].ToString().TrimEnd();
                newItem.subcategory                = dt.Rows[i]["subcategory"].ToString().TrimEnd();
                newItem.rank                       = dt.Rows[i]["rank"].ToString().TrimEnd();
                newItem.defaultcost                = dt.Rows[i]["defaultcost"].ToString().TrimEnd();
                newItem.dailyrate                  = dt.Rows[i]["dailyrate"].ToString().TrimEnd();
                newItem.weeklyrate                 = dt.Rows[i]["weeklyrate"].ToString().TrimEnd();
                newItem.week2rate                  = dt.Rows[i]["week2rate"].ToString().TrimEnd();
                newItem.week3rate                  = dt.Rows[i]["week3rate"].ToString().TrimEnd();
                newItem.week4rate                  = dt.Rows[i]["week4rate"].ToString().TrimEnd();
                newItem.week5rate                  = dt.Rows[i]["week5rate"].ToString().TrimEnd();
                newItem.monthlyrate                = dt.Rows[i]["monthlyrate"].ToString().TrimEnd();
                newItem.partnumber                 = dt.Rows[i]["partnumber"].ToString().TrimEnd();
                newItem.aisleloc                   = dt.Rows[i]["aisleloc"].ToString().TrimEnd();
                newItem.shelfloc                   = dt.Rows[i]["shelfloc"].ToString().TrimEnd();
                newItem.nodiscount                 = dt.Rows[i]["nodiscount"].ToString().TrimEnd();
                newItem.replacementcost            = dt.Rows[i]["replacementcost"].ToString().TrimEnd();
                newItem.trackedby                  = dt.Rows[i]["trackedby"].ToString().TrimEnd();
                newItem.notes                      = dt.Rows[i]["notes"].ToString().TrimEnd();
                newItem.cost                       = dt.Rows[i]["cost"].ToString().TrimEnd();
                newItem.price                      = dt.Rows[i]["price"].ToString().TrimEnd();
                newItem.dailycost                  = dt.Rows[i]["dailycost"].ToString().TrimEnd();
                newItem.weeklycost                 = dt.Rows[i]["weeklycost"].ToString().TrimEnd();
                newItem.monthlycost                = dt.Rows[i]["monthlycost"].ToString().TrimEnd();
                //newItem.originalicode              = dt.Rows[i]["originalicode"].ToString().TrimEnd();
                newItem.manifestvalue              = dt.Rows[i]["manifestvalue"].ToString().TrimEnd();
                newItem.shipweightlbs              = dt.Rows[i]["shipweightlbs"].ToString().TrimEnd();
                newItem.shipweightoz               = dt.Rows[i]["shipweightoz"].ToString().TrimEnd();
                newItem.weightwcaselbs             = dt.Rows[i]["weightwcaselbs"].ToString().TrimEnd();
                newItem.weightwcaseoz              = dt.Rows[i]["weightwcaseoz"].ToString().TrimEnd();
                newItem.widthft                    = dt.Rows[i]["widthft"].ToString().TrimEnd();
                newItem.widthin                    = dt.Rows[i]["widthin"].ToString().TrimEnd();
                newItem.heightft                   = dt.Rows[i]["heightft"].ToString().TrimEnd();
                newItem.heightin                   = dt.Rows[i]["heightin"].ToString().TrimEnd();
                newItem.lengthft                   = dt.Rows[i]["lengthft"].ToString().TrimEnd();
                newItem.lengthin                   = dt.Rows[i]["lengthin"].ToString().TrimEnd();
                newItem.shipweightkg               = dt.Rows[i]["shipweightkg"].ToString().TrimEnd();
                newItem.shipweightg                = dt.Rows[i]["shipweightg"].ToString().TrimEnd();
                newItem.weightwcasekg              = dt.Rows[i]["weightwcasekg"].ToString().TrimEnd();
                newItem.weightwcaseg               = dt.Rows[i]["weightwcaseg"].ToString().TrimEnd();
                newItem.widthm                     = dt.Rows[i]["widthm"].ToString().TrimEnd();
                newItem.widthcm                    = dt.Rows[i]["widthcm"].ToString().TrimEnd();
                newItem.heightm                    = dt.Rows[i]["heightm"].ToString().TrimEnd();
                newItem.heightcm                   = dt.Rows[i]["heightcm"].ToString().TrimEnd();
                newItem.lengthm                    = dt.Rows[i]["lengthm"].ToString().TrimEnd();
                newItem.lengthcm                   = dt.Rows[i]["lengthcm"].ToString().TrimEnd();
                newItem.categoryid                 = dt.Rows[i]["categoryid"].ToString().TrimEnd();
                newItem.dw                         = dt.Rows[i]["dw"].ToString().TrimEnd();
                newItem.metered                    = dt.Rows[i]["metered"].ToString().TrimEnd();
                newItem.qty                        = dt.Rows[i]["qty"].ToString().TrimEnd();
                newItem.qtyin                      = dt.Rows[i]["qtyin"].ToString().TrimEnd();
                newItem.datestamp                  = dt.Rows[i]["datestamp"].ToString().TrimEnd();
                newItem.warehouseid                = dt.Rows[i]["warehouseid"].ToString().TrimEnd();
                newItem.inventorydepartmentorderby = dt.Rows[i]["inventorydepartmentorderby"].ToString().TrimEnd();
                newItem.categoryorderby            = dt.Rows[i]["categoryorderby"].ToString().TrimEnd();
                newItem.subcategoryorderby         = dt.Rows[i]["subcategoryorderby"].ToString().TrimEnd();
                newItem.orderby                    = dt.Rows[i]["orderby"].ToString().TrimEnd();

                newItem.itemaka                    = InventoryData.GetItemAkas(newItem.masterid);

                result.Add(newItem);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<SaleItem> GetSalesItemsAsOf(FwDateTime asofdate)
        {
            FwSqlCommand qry;
            List<SaleItem> result = new List<SaleItem>();
            DataTable dt = new DataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_salesinventoryview");
            qry.Add(" where datestamp > @asofdate");
            //qry.Add("order by inventorydepartmentorderby, categoryorderby, subcategoryorderby"); //2016-09-28: Ahab: need to remove
            qry.AddParameter("@asofdate", asofdate.GetSqlDate());
            dt = qry.QueryToTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SaleItem newItem = new SaleItem();

                newItem.masterid                   = dt.Rows[i]["masterid"].ToString().TrimEnd();
                newItem.masterno                   = dt.Rows[i]["masterno"].ToString().TrimEnd();
                newItem.master                     = dt.Rows[i]["master"].ToString().TrimEnd();
                newItem.inactive                   = new FwDatabaseField(dt.Rows[i]["inactive"]).ToBoolean();
                newItem.availfor                   = dt.Rows[i]["availfor"].ToString().TrimEnd();
                newItem.availfrom                  = dt.Rows[i]["availfrom"].ToString().TrimEnd();
                newItem.manufacturer               = dt.Rows[i]["manufacturer"].ToString().TrimEnd();
                newItem.category                   = dt.Rows[i]["category"].ToString().TrimEnd();
                newItem.inventorydepartmentid      = dt.Rows[i]["inventorydepartmentid"].ToString().TrimEnd();
                newItem.inventorydepartment        = dt.Rows[i]["inventorydepartment"].ToString().TrimEnd();
                newItem.unit                       = dt.Rows[i]["unit"].ToString().TrimEnd();
                newItem.unitid                     = dt.Rows[i]["unitid"].ToString().TrimEnd();
                newItem.itemclass                  = dt.Rows[i]["itemclass"].ToString().TrimEnd();
                newItem.subcategory                = dt.Rows[i]["subcategory"].ToString().TrimEnd();
                newItem.rank                       = dt.Rows[i]["rank"].ToString().TrimEnd();
                newItem.defaultcost                = dt.Rows[i]["defaultcost"].ToString().TrimEnd();
                newItem.notes                      = dt.Rows[i]["notes"].ToString().TrimEnd();
                newItem.cost                       = dt.Rows[i]["cost"].ToString().TrimEnd();
                newItem.price                      = dt.Rows[i]["price"].ToString().TrimEnd();
                //newItem.originalicode              = dt.Rows[i]["originalicode"].ToString().TrimEnd();
                newItem.manifestvalue              = dt.Rows[i]["manifestvalue"].ToString().TrimEnd();
                newItem.shipweightlbs              = dt.Rows[i]["shipweightlbs"].ToString().TrimEnd();
                newItem.shipweightoz               = dt.Rows[i]["shipweightoz"].ToString().TrimEnd();
                newItem.weightwcaselbs             = dt.Rows[i]["weightwcaselbs"].ToString().TrimEnd();
                newItem.weightwcaseoz              = dt.Rows[i]["weightwcaseoz"].ToString().TrimEnd();
                newItem.widthft                    = dt.Rows[i]["widthft"].ToString().TrimEnd();
                newItem.widthin                    = dt.Rows[i]["widthin"].ToString().TrimEnd();
                newItem.heightft                   = dt.Rows[i]["heightft"].ToString().TrimEnd();
                newItem.heightin                   = dt.Rows[i]["heightin"].ToString().TrimEnd();
                newItem.lengthft                   = dt.Rows[i]["lengthft"].ToString().TrimEnd();
                newItem.lengthin                   = dt.Rows[i]["lengthin"].ToString().TrimEnd();
                newItem.shipweightkg               = dt.Rows[i]["shipweightkg"].ToString().TrimEnd();
                newItem.shipweightg                = dt.Rows[i]["shipweightg"].ToString().TrimEnd();
                newItem.weightwcasekg              = dt.Rows[i]["weightwcasekg"].ToString().TrimEnd();
                newItem.weightwcaseg               = dt.Rows[i]["weightwcaseg"].ToString().TrimEnd();
                newItem.widthm                     = dt.Rows[i]["widthm"].ToString().TrimEnd();
                newItem.widthcm                    = dt.Rows[i]["widthcm"].ToString().TrimEnd();
                newItem.heightm                    = dt.Rows[i]["heightm"].ToString().TrimEnd();
                newItem.heightcm                   = dt.Rows[i]["heightcm"].ToString().TrimEnd();
                newItem.lengthm                    = dt.Rows[i]["lengthm"].ToString().TrimEnd();
                newItem.lengthcm                   = dt.Rows[i]["lengthcm"].ToString().TrimEnd();
                newItem.categoryid                 = dt.Rows[i]["categoryid"].ToString().TrimEnd();
                newItem.qty                        = dt.Rows[i]["qty"].ToString().TrimEnd();
                newItem.datestamp                  = dt.Rows[i]["datestamp"].ToString().TrimEnd();
                newItem.warehouseid                = dt.Rows[i]["warehouseid"].ToString().TrimEnd();
                newItem.inventorydepartmentorderby = dt.Rows[i]["inventorydepartmentorderby"].ToString().TrimEnd();
                newItem.categoryorderby            = dt.Rows[i]["categoryorderby"].ToString().TrimEnd();
                newItem.subcategoryorderby         = dt.Rows[i]["subcategoryorderby"].ToString().TrimEnd();
                newItem.orderby                    = dt.Rows[i]["orderby"].ToString().TrimEnd();

                newItem.itemaka                    = InventoryData.GetItemAkas(newItem.masterid);

                result.Add(newItem);
            }

            return result;
        }
        //------------------------------------------------------------------------------
        public static List<RentalItem> HighlyUsedInventory(string dealid, string departmentid)
        {
            FwSqlCommand qry;
            List<RentalItem> result = new List<RentalItem>();
            DataTable dt = new DataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from dbo.apirest_funchiglyusedinventory(@dealid, @departmentid)");
            qry.AddParameter("@dealid", dealid);
            qry.AddParameter("@departmentid", departmentid);
            dt = qry.QueryToTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RentalItem newitem = new RentalItem();

                newitem.masterid              = dt.Rows[i]["masterid"].ToString().TrimEnd();
                newitem.masterno              = dt.Rows[i]["masterno"].ToString().TrimEnd();
                newitem.master                = dt.Rows[i]["master"].ToString().TrimEnd();
                newitem.availfor              = dt.Rows[i]["availfor"].ToString().TrimEnd();
                newitem.availfrom             = dt.Rows[i]["availfrom"].ToString().TrimEnd();
                newitem.manufacturer          = dt.Rows[i]["manufacturer"].ToString().TrimEnd();
                newitem.category              = dt.Rows[i]["category"].ToString().TrimEnd();
                newitem.inventorydepartmentid = dt.Rows[i]["inventorydepartmentid"].ToString().TrimEnd();
                newitem.inventorydepartment   = dt.Rows[i]["inventorydepartment"].ToString().TrimEnd();
                newitem.unit                  = dt.Rows[i]["unit"].ToString().TrimEnd();
                newitem.unitid                = dt.Rows[i]["unitid"].ToString().TrimEnd();
                newitem.itemclass             = dt.Rows[i]["itemclass"].ToString().TrimEnd();
                newitem.subcategory           = dt.Rows[i]["subcategory"].ToString().TrimEnd();
                newitem.rank                  = dt.Rows[i]["rank"].ToString().TrimEnd();
                newitem.defaultcost           = dt.Rows[i]["defaultcost"].ToString().TrimEnd();
                newitem.dailyrate             = dt.Rows[i]["dailyrate"].ToString().TrimEnd();
                newitem.weeklyrate            = dt.Rows[i]["weeklyrate"].ToString().TrimEnd();
                newitem.week2rate             = dt.Rows[i]["week2rate"].ToString().TrimEnd();
                newitem.week3rate             = dt.Rows[i]["week3rate"].ToString().TrimEnd();
                newitem.week4rate             = dt.Rows[i]["week4rate"].ToString().TrimEnd();
                newitem.week5rate             = dt.Rows[i]["week5rate"].ToString().TrimEnd();
                newitem.monthlyrate           = dt.Rows[i]["monthlyrate"].ToString().TrimEnd();
                newitem.partnumber            = dt.Rows[i]["partnumber"].ToString().TrimEnd();
                //newitem.aisleloc              = dt.Rows[i]["aisleloc"].ToString().TrimEnd();
                //newitem.shelfloc              = dt.Rows[i]["shelfloc"].ToString().TrimEnd();
                newitem.nodiscount            = dt.Rows[i]["nodiscount"].ToString().TrimEnd();
                newitem.replacementcost       = dt.Rows[i]["replacementcost"].ToString().TrimEnd();
                newitem.trackedby             = dt.Rows[i]["trackedby"].ToString().TrimEnd();
                newitem.notes                 = dt.Rows[i]["notes"].ToString().TrimEnd();
                newitem.cost                  = dt.Rows[i]["cost"].ToString().TrimEnd();
                newitem.price                 = dt.Rows[i]["price"].ToString().TrimEnd();
                newitem.dailycost             = dt.Rows[i]["dailycost"].ToString().TrimEnd();
                newitem.weeklycost            = dt.Rows[i]["weeklycost"].ToString().TrimEnd();
                newitem.monthlycost           = dt.Rows[i]["monthlycost"].ToString().TrimEnd();
                newitem.originalicode         = dt.Rows[i]["originalicode"].ToString().TrimEnd();
                newitem.manifestvalue         = dt.Rows[i]["manifestvalue"].ToString().TrimEnd();
                newitem.shipweightlbs         = dt.Rows[i]["shipweightlbs"].ToString().TrimEnd();
                newitem.shipweightoz          = dt.Rows[i]["shipweightoz"].ToString().TrimEnd();
                newitem.weightwcaselbs        = dt.Rows[i]["weightwcaselbs"].ToString().TrimEnd();
                newitem.weightwcaseoz         = dt.Rows[i]["weightwcaseoz"].ToString().TrimEnd();
                newitem.widthft               = dt.Rows[i]["widthft"].ToString().TrimEnd();
                newitem.widthin               = dt.Rows[i]["widthin"].ToString().TrimEnd();
                newitem.heightft              = dt.Rows[i]["heightft"].ToString().TrimEnd();
                newitem.heightin              = dt.Rows[i]["heightin"].ToString().TrimEnd();
                newitem.lengthft              = dt.Rows[i]["lengthft"].ToString().TrimEnd();
                newitem.lengthin              = dt.Rows[i]["lengthin"].ToString().TrimEnd();
                newitem.shipweightkg          = dt.Rows[i]["shipweightkg"].ToString().TrimEnd();
                newitem.shipweightg           = dt.Rows[i]["shipweightg"].ToString().TrimEnd();
                newitem.weightwcasekg         = dt.Rows[i]["weightwcasekg"].ToString().TrimEnd();
                newitem.weightwcaseg          = dt.Rows[i]["weightwcaseg"].ToString().TrimEnd();
                newitem.widthm                = dt.Rows[i]["widthm"].ToString().TrimEnd();
                newitem.widthcm               = dt.Rows[i]["widthcm"].ToString().TrimEnd();
                newitem.heightm               = dt.Rows[i]["heightm"].ToString().TrimEnd();
                newitem.heightcm              = dt.Rows[i]["heightcm"].ToString().TrimEnd();
                newitem.lengthm               = dt.Rows[i]["lengthm"].ToString().TrimEnd();
                newitem.lengthcm              = dt.Rows[i]["lengthcm"].ToString().TrimEnd();
                newitem.categoryid            = dt.Rows[i]["categoryid"].ToString().TrimEnd();
                newitem.dw                    = dt.Rows[i]["dw"].ToString().TrimEnd();
                newitem.metered               = dt.Rows[i]["metered"].ToString().TrimEnd();
                newitem.qty                   = dt.Rows[i]["qty"].ToString().TrimEnd();
                newitem.qtyin                 = dt.Rows[i]["qtyin"].ToString().TrimEnd();
                newitem.datestamp             = dt.Rows[i]["datestamp"].ToString().TrimEnd();
                newitem.warehouseid           = dt.Rows[i]["warehouseid"].ToString().TrimEnd();

                result.Add(newitem);
            }

            return result;
        }
        //------------------------------------------------------------------------------
        public static List<CompleteKit> GetCompletesAndKits(string packageid, string warehouseid)
        {
            FwSqlCommand qry;
            DataTable dt = new DataTable();
            List<CompleteKit> response = new List<CompleteKit>();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_packageitemview");
            qry.Add(" where (warehouseid = '' or warehouseid = @warehouseid)");
            if (!string.IsNullOrEmpty(packageid))
            {
                qry.Add("and packageid = @packageid");
                qry.AddParameter("@packageid", packageid);
            }
            qry.Add("order by orderby");
            qry.AddParameter("@warehouseid", warehouseid);
            dt = qry.QueryToTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CompleteKit newCompleteKit = new CompleteKit();

                newCompleteKit.masterno         = dt.Rows[i]["masterno"].ToString().TrimEnd();
                newCompleteKit.description      = dt.Rows[i]["description"].ToString().TrimEnd();
                newCompleteKit.masterid         = dt.Rows[i]["masterid"].ToString().TrimEnd();
                newCompleteKit.packageitemid    = dt.Rows[i]["packageitemid"].ToString().TrimEnd();
                newCompleteKit.packageid        = dt.Rows[i]["packageid"].ToString().TrimEnd();
                newCompleteKit.primaryflg       = dt.Rows[i]["primaryflg"].ToString().TrimEnd();
                newCompleteKit.defaultqty       = dt.Rows[i]["defaultqty"].ToString().TrimEnd();
                newCompleteKit.isoption         = dt.Rows[i]["isoption"].ToString().TrimEnd();
                newCompleteKit.charge           = dt.Rows[i]["charge"].ToString().TrimEnd();
                newCompleteKit.datestamp        = dt.Rows[i]["datestamp"].ToString().TrimEnd();
                newCompleteKit.required         = dt.Rows[i]["required"].ToString().TrimEnd();
                newCompleteKit.optioncolor      = dt.Rows[i]["optioncolor"].ToString().TrimEnd();
                newCompleteKit.itemclass        = dt.Rows[i]["itemclass"].ToString().TrimEnd();
                newCompleteKit.availfor         = dt.Rows[i]["availfor"].ToString().TrimEnd();
                newCompleteKit.availfrom        = dt.Rows[i]["availfrom"].ToString().TrimEnd();
                newCompleteKit.categoryorderby  = dt.Rows[i]["categoryorderby"].ToString().TrimEnd();
                newCompleteKit.orderby          = dt.Rows[i]["orderby"].ToString().TrimEnd();
                newCompleteKit.itemcolor        = dt.Rows[i]["itemcolor"].ToString().TrimEnd();
                newCompleteKit.isnestedcomplete = dt.Rows[i]["isnestedcomplete"].ToString().TrimEnd();
                newCompleteKit.iteminactive     = dt.Rows[i]["iteminactive"].ToString().TrimEnd();
                newCompleteKit.warehouseid      = dt.Rows[i]["warehouseid"].ToString().TrimEnd();
                newCompleteKit.parentid         = dt.Rows[i]["parentid"].ToString().TrimEnd();
                newCompleteKit.packageitemclass = dt.Rows[i]["packageitemclass"].ToString().TrimEnd();

                newCompleteKit.itemaka                 = InventoryData.GetItemAkas(newCompleteKit.masterid);

                response.Add(newCompleteKit);
            }
            
            return response;
        }
        //----------------------------------------------------------------------------------------------------
        public static string[] GetItemAkas(string masterid)
        {
            FwSqlCommand qry;
            string[] result;
            DataTable dt = new DataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from masteraka");
            qry.Add(" where masterid = @masterid");
            qry.AddParameter("@masterid", masterid);
            dt = qry.QueryToTable();

            result = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result[i] = dt.Rows[i]["aka"].ToString().TrimEnd();
            }

            return result;
        }
        //------------------------------------------------------------------------------
    }
}