using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace RwQBO.Source
{
    public class RwReport : FwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override FwSqlConnection GetApplicationSqlConnection()
        {
            return FwSqlConnection.RentalWorks;
        }
        //---------------------------------------------------------------------------------------------
    }
}