﻿import { HomeModule } from "../../shared/HomeModule";

//---------------------------------------------------------------------------------------
export class Quote extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Quote';
        this.moduleId = '4D785844-BE8A-4C00-B1FA-2AA5B05183E5';
        this.moduleCaption = 'Quote';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Order extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Order';
        this.moduleId = '64C46F51-5E00-48FA-94B6-FC4EF53FEA20';
        this.moduleCaption = 'Order';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Customer extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Customer';
        this.moduleId = '214C6242-AA91-4498-A4CC-E0F3DCCCE71E';
        this.moduleCaption = 'Customer';
        this.newRecordsToCreate = [
            {
                record: {
                    Customer: "GlobalScope.TestToken~1.TestToken",
                    CustomerNumber: "GlobalScope.TestToken~1.TestToken",
                    CustomerTypeId: 1,
                    CreditStatusId: 1,
                },
                seekObject: {
                    Customer: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Deal extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Deal';
        this.moduleId = 'C67AD425-5273-4F80-A452-146B2008B41C';
        this.moduleCaption = 'Deal';

        this.defaultNewRecordToExpect = {
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
            DealStatus: "GlobalScope.DefaultSettings~1.DefaultDealStatus",   // ie. "ACTIVE"
        }


        this.newRecordsToCreate = [
            {
                record: {
                    Deal: "GlobalScope.TestToken~1.TestToken",
                    DealNumber: "GlobalScope.TestToken~1.TestToken",
                    CustomerId: 1,
                    DealTypeId: 1
                },
                seekObject: {
                    Deal: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Vendor extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Vendor';
        this.moduleId = 'AE4884F4-CB21-4D10-A0B5-306BD0883F19';
        this.moduleCaption = 'Vendor';

        this.defaultNewRecordToExpect = {
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Vendor: "GlobalScope.TestToken~1.TestToken",
                    VendorNumber: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    VendorDisplayName: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Contact extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Contact';
        this.moduleId = '3F803517-618A-41C0-9F0B-2C96B8BDAFC4';
        this.moduleCaption = 'Contact';
        this.newRecordsToCreate = [
            {
                record: {
                    FirstName: "GlobalScope.TestToken~1.TestToken",
                    LastName: "GlobalScope.TestToken~1.TestToken",
                    Email: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    LastName: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PurchaseOrder extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PurchaseOrder';
        this.moduleId = '67D8C8BB-CF55-4231-B4A2-BB308ADF18F0';
        this.moduleCaption = 'Purchase Order';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Project extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Project';
        this.moduleId = 'C6C8167A-C3B5-4915-8290-4520AF7EDB35';
        this.moduleCaption = 'Project';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RentalInventory extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'RentalInventory';
        this.moduleId = 'FCDB4C86-20E7-489B-A8B7-D22EE6F85C06';
        this.moduleCaption = 'Rental Inventory';
        this.defaultNewRecordToExpect = {
            Unit: "GlobalScope.DefaultSettings~1.DefaultUnit",   // ie. "EA"
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SalesInventory extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SalesInventory';
        this.moduleId = 'B0CF2E66-CDF8-4E58-8006-49CA68AE38C2';
        this.moduleCaption = 'Sales Inventory';
        this.defaultNewRecordToExpect = {
            Unit: "GlobalScope.DefaultSettings~1.DefaultUnit",   // ie. "EA"
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PartsInventory extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PartsInventory';
        this.moduleId = '351B8A09-7778-4F06-A6A2-ED0920A5C360';
        this.moduleCaption = 'Parts Inventory';

        this.defaultNewRecordToExpect = {
            Unit: "GlobalScope.DefaultSettings~1.DefaultUnit",   // ie. "EA"
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Asset extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Asset';
        this.moduleId = '1C45299E-F8DB-4AE4-966F-BE142295E3D6';
        this.moduleCaption = 'Asset';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RepairOrder extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Repair';
        this.moduleId = '2BD0DC82-270E-4B86-A9AA-DD0461A0186A';
        this.moduleCaption = 'Repair Order';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PhysicalInventory extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PhysicalInventory';
        this.moduleId = 'BABFE80E-8A52-49D4-81D9-6B6EBB518E89';
        this.moduleCaption = 'Physical Inventory';
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PickList extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PickList';
        this.moduleId = '7B04E5D4-D079-4F3A-9CB0-844F293569ED';
        this.moduleCaption = 'Pick List';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Contract extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Contract';
        this.moduleId = '6BBB8A0A-53FA-4E1D-89B3-8B184B233DEA';
        this.moduleCaption = 'Contract';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Container extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Container';
        this.moduleId = '28A49328-FFBD-42D5-A492-EDF540DF7011';
        this.moduleCaption = 'Container';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ContainerStatus extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ContainerStatus';
        this.moduleId = '0CD07ACF-D9A4-42A3-A288-162398683F8A';
        this.moduleCaption = 'Container Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Manifest extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Manifest';
        this.moduleId = '1643B4CE-D368-4D64-8C05-6EF7C7D80336';
        this.moduleCaption = 'Transfer Manifest';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TransferOrder extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TransferOrder';
        this.moduleId = 'F089C9A9-554D-40BF-B1FA-015FEDE43591';
        this.moduleCaption = 'Transfer Order';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TransferReceipt extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TransferReceipt';
        this.moduleId = '2B60012B-ED6A-430B-B2CB-C1287FD4CE8B';
        this.moduleCaption = 'Transfer Receipt';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Invoice extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Invoice';
        this.moduleId = '9B79D7D8-08A1-4F6B-AC0A-028DFA9FE10F';
        this.moduleCaption = 'Invoice';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Receipt extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Receipt';
        this.moduleId = '57E34535-1B9F-4223-AD82-981CA34A6DEC';
        this.moduleCaption = 'Receipts';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VendorInvoice extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VendorInvoice';
        this.moduleId = '854B3C59-7040-47C4-A8A3-8A336FC970FE';
        this.moduleCaption = 'Vendor Invoice';
    }
    //---------------------------------------------------------------------------------------
}

