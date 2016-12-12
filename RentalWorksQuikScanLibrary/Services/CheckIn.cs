using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalWorksQuikScanLibrary.Services
{
    class CheckIn
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void GetShowCreateContract(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select showcreatecontract = (case when exists (select *");
                qry.Add("                                               from ordertran with (nolock)");
                qry.Add("                                               where inreturncontractid = @contractid)");
                qry.Add("                                  then 'T'");
                qry.Add("                                  else 'F'");
                qry.Add("                             end)");
                qry.AddParameter("@contractid", request.contractid);
                qry.Execute();
                response.showcreatecontract = qry.GetField("showcreatecontract").ToBoolean();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
