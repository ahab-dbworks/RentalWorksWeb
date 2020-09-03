using FwStandard.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Containers.Container
{
    [GetRequest(DefaultSort: "Description:asc")]
    public class LookupContainerDescriptionRequest : GetRequest
    {
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Container Identifier [Key|Filter]
        /// </summary>
        [GetRequestProperty(true, false)]
        public string ContainerId { get; set; }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Container Description. [Filter|Sort]
        /// </summary>
        [GetRequestProperty(true, true)]
        public string Description { get; set; }
        ////------------------------------------------------------------------------------------
        ///// <summary>
        ///// Warehouse Identifier . [Filter]
        ///// </summary>
        //[GetRequestProperty(true, false), Required]
        //public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// InventoryId of Scannable Item Barcode. [Filter]
        /// </summary>
        [GetRequestProperty(true, false), Required]
        public string ScannableInventoryId { get; set; }
        //------------------------------------------------------------------------------------
    }

    public class LookupContainerDescriptionResponse
    {
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Container Description Identifier
        /// </summary>
        public string ContainerId { get; set; }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Container Description
        /// </summary>
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
    }
}
