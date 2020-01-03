using FwStandard.Models;
using System.ComponentModel.DataAnnotations;

namespace FwCore.Modules.Administrator.Group
{
    public class CopySecurityNodeRequest
    {
        /// <summary>
        /// ID of Group to copy from
        /// </summary>
        [MaxLength(8)]
        public string FromGroupId { get; set; } = string.Empty;

        /// <summary>
        /// ID of Group to copy to
        /// </summary>
        [MaxLength(8)]
        public string ToGroupIds { get; set; } = string.Empty;

        /// <summary>
        /// ID of security node to copy from the FromGroupId to the ToGroupId
        /// </summary>
        [MaxLength(8)]
        public string SecurityId { get; set; } = string.Empty;
    }

    [GetRequest(DefaultSort: "Name:asc")]
    public class LookupGroupRequest: GetRequest
    {
        /// <summary>
        /// Identifier [Key|Filter|Sort]
        /// </summary>
        [GetRequestProperty(true, true), MaxLength(8)]
        public string GroupId { get; set; } = string.Empty;

        /// <summary>
        /// Name of Group [Filter|Sort]
        /// </summary>
        [GetRequestProperty(true, true), MaxLength(30)]
        public string Name { get; set; } = string.Empty;
    }
    
    public class LookupGroupResponse
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public string GroupId { get; set; } = string.Empty;
        
        /// <summary>
        /// Name of Group
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
