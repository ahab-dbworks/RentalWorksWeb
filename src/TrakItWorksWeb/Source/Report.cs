using Fw.Json.Services;
using Fw.Json.SqlServer;

namespace TrakitWorksWeb.Source
{
    public class Report : FwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override FwSqlConnection GetApplicationSqlConnection()
        {
            return FwSqlConnection.RentalWorks;
        }
        //---------------------------------------------------------------------------------------------
    }
}