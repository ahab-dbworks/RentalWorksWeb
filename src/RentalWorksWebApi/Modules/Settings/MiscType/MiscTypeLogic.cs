﻿using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.InventoryType;

namespace RentalWorksWebApi.Modules.Settings.MiscType
{
    public class MiscTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        MiscTypeLoader inventoryTypeLoader = new MiscTypeLoader();
        public MiscTypeLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string MiscTypeId { get { return inventoryType.InventoryTypeId; } set { inventoryType.InventoryTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string MiscType { get { return inventoryType.InventoryType; } set { inventoryType.InventoryType = value; } }
        public bool Misc { get { return inventoryType.Misc; } set { inventoryType.Misc = value; } }
        public bool GroupProfitLoss { get { return inventoryType.GroupProfitLoss; } set { inventoryType.GroupProfitLoss = value; } }
        public bool Inactive { get { return inventoryType.Inactive; } set { inventoryType.Inactive = value; } }
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            if (!Misc)
            {
                Misc = true;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
