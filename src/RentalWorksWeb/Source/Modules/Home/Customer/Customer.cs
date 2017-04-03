using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    class Customer : FwModule
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);
            //selectQry.AddWhere("ordertype = 'C'");
        }
        //---------------------------------------------------------------------------------------------
        protected override string getTabName()
        {
            return form.Tables["customer"].GetField("customerid").Value;
        }
        //---------------------------------------------------------------------------------------------
        protected override void beforeInsert()
        {
            form.Tables["customer"].GetField("customerid").UniqueIdentifier = true;
            form.Tables["customer"].GetField("customerid").Value = FwSqlData.GetNextId(this.form.DatabaseConnection);
        }
        //---------------------------------------------------------------------------------------------
        protected override string getFormUniqueId()
        {
            return form.Tables["customer"].GetUniqueId("customerid").Value;
        }
        //---------------------------------------------------------------------------------------------
    }
}