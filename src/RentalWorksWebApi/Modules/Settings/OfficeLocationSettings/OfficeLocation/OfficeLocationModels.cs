using FwStandard.Models;

namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    //------------------------------------------------------------------------------------
    public class GetManyOfficeLocationRequest : GetRequest
    {
        /// <summary>
        /// Filter Expression
        /// </summary>
        [GetRequestProperty(true, false)]
        public string LocationId { get; set; } = null;
        /// <summary>
        /// Filter Expression
        /// </summary>
        [GetRequestProperty(true, true)]
        public string Location { get; set; } = null;
    }
    //------------------------------------------------------------------------------------
    public class GetManyOfficeLocationModel
    {
        public string LocationId { get; set; } = null;
        public string Location { get; set; } = null;
    }
    //------------------------------------------------------------------------------------
}
