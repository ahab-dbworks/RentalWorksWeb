using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    public class QC
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void ItemScan(dynamic request, dynamic response, dynamic session)
        {
            response.qcitem     = WebCompleteQCItem(request.code, session.security.webUser.usersid);
            response.itemstatus = RwAppData.WebGetItemStatus(FwSqlConnection.RentalWorks, session.security.webUser.usersid, request.code);
            response.conditions = GetRentalConditions(response.itemstatus.masterId);
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic WebCompleteQCItem(string code, string usersid)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.webcompleteqcitem");
            sp.AddParameter("@code",           code);
            sp.AddParameter("@usersid",        usersid);
            sp.AddParameter("@masterno",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@rentalitemid",   SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@rentalitemqcid", SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@description",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",         SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",            SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();
            result = new ExpandoObject();
            result.masterNo       = sp.GetParameter("@masterno").ToString().Trim();
            result.rentalitemid   = sp.GetParameter("@rentalitemid").ToString().Trim();
            result.rentalitemqcid = sp.GetParameter("@rentalitemqcid").ToString().Trim();
            result.description    = sp.GetParameter("@description").ToString().Trim();
            result.status         = sp.GetParameter("@status").ToInt32();
            result.msg            = sp.GetParameter("@msg").ToString().Trim();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic GetRentalConditions(string masterid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("conditionid", false);
            qry.AddColumn("condition", false);
            qry.Add("select value = c.conditionid, text = c.condition");
            qry.Add("from   condition c with (nolock)");
            qry.Add("where  c.rental = 'T'");
            qry.Add("  and  c.conditionid in (select conditionid from dbo.funcconditionfilter(@masterid))");
            qry.AddParameter("@masterid", masterid);
            qry.Execute();
            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void UpdateQCItem(dynamic request, dynamic response, dynamic session)
        {
            byte[] image;

            if (!string.IsNullOrEmpty(request.conditionid) || !string.IsNullOrEmpty(request.note))
            {
                UpdateRentalItemQC(request.qcitem.rentalitemid, request.qcitem.rentalitemqcid, request.conditionid, request.note);
            }

            for (int i = 0; i < request.images.Length; i++)
            {
                image = Convert.FromBase64String(request.images[i]);
                FwSqlData.InsertAppImage(FwSqlConnection.RentalWorks, request.qcitem.rentalitemqcid, string.Empty, string.Empty, string.Empty, string.Empty, "JPG", image);
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic UpdateRentalItemQC(string rentalitemid, string rentalitemqcid, string conditionid, string note)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.webupdaterentalitemqc");
            sp.AddParameter("@rentalitemid",    rentalitemid);
            sp.AddParameter("@rentalitemqcid",  rentalitemqcid);
            sp.AddParameter("@conditionid",     conditionid);
            sp.AddParameter("@note",            note);
            sp.AddParameter("@errno",           SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@errmsg",          SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();
            result        = new ExpandoObject();
            result.errno  = sp.GetParameter("@errno").ToInt32();
            result.errmsg = sp.GetParameter("@errmsg").ToString().Trim();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
