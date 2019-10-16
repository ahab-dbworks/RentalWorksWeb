using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryType;

namespace WebApi.Modules.Settings.MiscType
{
    [FwLogic(Id:"BYBJEtQsBtKE")]
    public class MiscTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        MiscTypeLoader inventoryTypeLoader = new MiscTypeLoader();
        public MiscTypeLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"Ejl551QbmUez", IsPrimaryKey:true)]
        public string MiscTypeId { get { return inventoryType.InventoryTypeId; } set { inventoryType.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"Ejl551QbmUez", IsRecordTitle:true)]
        public string MiscType { get { return inventoryType.InventoryType; } set { inventoryType.InventoryType = value; } }

        [FwLogicProperty(Id:"zEiC53NMcUX")]
        public bool? Misc { get { return inventoryType.Misc; } set { inventoryType.Misc = value; } }

        [FwLogicProperty(Id:"kghDICt9Toc")]
        public bool? GroupProfitLoss { get { return inventoryType.GroupProfitLoss; } set { inventoryType.GroupProfitLoss = value; } }

        [FwLogicProperty(Id: "DyE4LuDuuJghH", IsReadOnly: true)]
        public int? CategoryCount { get; set; }

        [FwLogicProperty(Id:"XuPH5cERxMW")]
        public bool? Inactive { get { return inventoryType.Inactive; } set { inventoryType.Inactive = value; } }

        [FwLogicProperty(Id:"xD2KVyILBRC")]
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            Misc = true;
        }
        //------------------------------------------------------------------------------------
    }

}
