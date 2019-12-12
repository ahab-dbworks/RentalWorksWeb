using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventorySettings.InventoryStatus
{
    [GetRequest(DefaultSort: "InventoryStatus:asc")]
    public class GetInventoryStatusRequest: GetRequest
    {
        [GetRequestProperty(true, true), MaxLength(20)]
        public string InventoryStatus { get; set; }
    }
}
