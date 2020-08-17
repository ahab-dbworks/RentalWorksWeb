using FwStandard.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Containers.Container
{
    [GetRequest(DefaultSort: "ContainerDescription:asc")]
    public class LookupContainerDescriptionRequest : GetRequest
    {
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Container Description Identifier [Key|Filter]
        /// </summary>
        [GetRequestProperty(true, false)]
        public string ContainerDescriptionId { get; set; }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Reason for retiring an item. [Filter|Sort]
        /// </summary>
        [GetRequestProperty(true, true)]
        public string ContainerDescription { get; set; }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Warehouse identifier . [Filter]
        /// </summary>
        [GetRequestProperty(true, false), Required]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// MasterId of Scannable Item Barcode. [Filter]
        /// </summary>
        [GetRequestProperty(true, false), Required]
        public string ScannableMasterId { get; set; }
        //------------------------------------------------------------------------------------
    }

    public class LookupContainerDescriptionResponse
    {
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Container Description Identifier
        /// </summary>
        public string ContainerDescriptionId { get; set; }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Reason for retiring an item
        /// </summary>
        public string ContainerDescription { get; set; }
        //------------------------------------------------------------------------------------
    }
}
