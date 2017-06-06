using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusModels
{
    public class ItemStatusResponse
    {
        public ItemStatus item { get; set; } = new ItemStatus();
    }
}