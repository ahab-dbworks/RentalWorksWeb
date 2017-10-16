using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RentalWorksWeb.Source.Validations
{
    class OrderValidation : FwValidation 
    {
        //---------------------------------------------------------------------------------------------
        protected override void setBrowseQry(FwSqlSelect selectQry)
        {
            base.setBrowseQry(selectQry);

            selectQry.AddWhere("ordertype = 'O'");
            selectQry.AddWhere("status <> 'SNAPSHOT'");
        }
        //---------------------------------------------------------------------------------------------
    }
}

