using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace RentalWorksWebLibrary.Modules
{
    class OrderType : FwModule
    {
        //---------------------------------------------------------------------------------------------
        //protected override void setBrowseQry(FwSqlSelect selectQry)
        //{
        //    base.setBrowseQry(selectQry);
        //    selectQry.AddWhere("ordertype = 'C'");
        //}
        //---------------------------------------------------------------------------------------------
        protected override string getTabName()
        {
            return form.Tables["ordertype"].GetField("ordertype").Value;
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeInsert()
        {
            form.Tables["ordertype"].GetField("ordertypeid").UniqueIdentifier = true;
            form.Tables["ordertype"].GetField("ordertypeid").Value = FwSqlData.GetNextId(this.form.DatabaseConnection);
        }
        //---------------------------------------------------------------------------------------------
        protected override string getFormUniqueId()
        {
            return form.Tables["ordertype"].GetUniqueId("ordertypeid").Value; 
        }
        //---------------------------------------------------------------------------------------------
    }
}