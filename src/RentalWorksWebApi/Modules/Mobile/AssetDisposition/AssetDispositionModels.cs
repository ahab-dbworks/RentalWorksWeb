using FwStandard.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Mobile.AssetDisposition
{
    [GetRequest(DefaultSort: "RetiredReason:asc")]
    public class LookupRetiredReasonRequest : GetRequest
    {
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Retired Reason Identifier [Key|Filter]
        /// </summary>
        [GetRequestProperty(true, false)/*, MaxLength(8)*/]
        public string RetiredReasonId { get; set; }

        /// <summary>
        /// Reason for retiring an item. [Filter|Sort]
        /// </summary>
        [GetRequestProperty(true, true)/*, MaxLength(20)*/]
        public string RetiredReason { get; set; }

        /// <summary>
        /// Category to use for filtering Retired Reasons for different purposes. [Filter|Sort] {OTHER|INVENTORY|CHANGECODE|LOST|STOLEN|DONATED|SOLD}
        /// </summary>
        [GetRequestProperty(true, true)/*, MaxLength(10)*/, Required]
        public string ReasonType { get; set; }

    }

    public class LookupRetiredReasonResponse
    {
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Retired Reason Identifier
        /// </summary>
        public string RetiredReasonId { get; set; }

        //------------------------------------------------------------------------------------
        /// <summary>
        /// Reason for retiring an item
        /// </summary>
        public string RetiredReason { get; set; }

        //------------------------------------------------------------------------------------
        ///// <summary>
        ///// Category to use for filtering Retired Reasons for different purposes. {OTHER|INVENTORY|CHANGECODE|LOST|STOLEN|DONATED|SOLD}
        ///// </summary>
        //public string ReasonType { get; set; }
    }
}
