import { HomeModule } from "../../shared/HomeModule";
import { TestUtils } from "../../shared/TestUtils";
import { Logging } from '../../shared/Logging';
import { GridBase } from "../../shared/GridBase";

//---------------------------------------------------------------------------------------
export class Quote extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Quote';
        this.moduleId = '4D785844-BE8A-4C00-B1FA-2AA5B05183E5';
        this.moduleCaption = 'Quote';
        this.canDelete = false;

        this.defaultNewRecordToExpect = {
            QuoteNumber: "",
            Department: "GlobalScope.User~ME.PrimaryDepartment",
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Warehouse: "GlobalScope.User~ME.Warehouse",
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            PendingPo: true,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    //Deal: dealInputs.Deal,
                    DealId: 1,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8)
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            QuoteNumber: "|NOTEMPTY|",
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Deal: "|NOTEMPTY|",
            Location: this.newRecordsToCreate[0].record.Location.toUpperCase(),
            ReferenceNumber: this.newRecordsToCreate[0].record.ReferenceNumber.toUpperCase(),
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Warehouse: "GlobalScope.User~ME.Warehouse",
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            PendingPo: true,
        }

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


        this.defaultNewRecordToExpect = {
            OrderNumber: "",
            Department: "GlobalScope.User~ME.PrimaryDepartment",
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Warehouse: "GlobalScope.User~ME.Warehouse",
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            PendingPo: true,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    //Deal: dealInputs.Deal,
                    DealId: 1,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8)
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            OrderNumber: "|NOTEMPTY|",
            Deal: "|NOTEMPTY|",
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Location: this.newRecordsToCreate[0].record.Location.toUpperCase(),
            ReferenceNumber: this.newRecordsToCreate[0].record.ReferenceNumber.toUpperCase(),
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Warehouse: "GlobalScope.User~ME.Warehouse",
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            PendingPo: true,
        }

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
                    Customer: TestUtils.randomCompanyName() + " GlobalScope.TestToken~1.TestToken",
                    CustomerNumber: "GlobalScope.TestToken~1.TestToken",
                    Address1: TestUtils.randomAddress1(),
                    Address2: TestUtils.randomAddress2(),
                    City: TestUtils.randomCity(),
                    State: TestUtils.randomState(),
                    ZipCode: TestUtils.randomZipCode(),
                    Phone: TestUtils.randomPhone(),
                    Fax: TestUtils.randomPhone(),
                    WebAddress: TestUtils.randomUrl(),
                    CustomerTypeId: 1,
                    CreditStatusId: 1
                },
                seekObject: {
                    Customer: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Customer: this.newRecordsToCreate[0].record.Customer.toUpperCase(),
            CustomerNumber: this.newRecordsToCreate[0].record.CustomerNumber,
            Address1: this.newRecordsToCreate[0].record.Address1.toUpperCase(),
            Address2: this.newRecordsToCreate[0].record.Address2.toUpperCase(),
            City: this.newRecordsToCreate[0].record.City.toUpperCase(),
            State: this.newRecordsToCreate[0].record.State.toUpperCase(),
            ZipCode: this.newRecordsToCreate[0].record.ZipCode.toUpperCase(),
            Phone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.Phone),
            Fax: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.Fax),
            WebAddress: this.newRecordsToCreate[0].record.WebAddress,
        }
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
                    Deal: TestUtils.randomCompanyName() + " GlobalScope.TestToken~1.TestToken",
                    DealNumber: "GlobalScope.TestToken~1.TestToken",
                    CustomerId: 1,
                    Address2: TestUtils.randomAddress2(),
                    Fax: TestUtils.randomPhone(),
                    DealTypeId: 1
                },
                seekObject: {
                    Deal: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            //Customer: this.newRecordsToCreate[0].record.Customer.toUpperCase(),
            Deal: this.newRecordsToCreate[0].record.Deal.toUpperCase(),
            DealNumber: this.newRecordsToCreate[0].record.DealNumber.toUpperCase(),
            //Address1: customerInputs.Address1.toUpperCase(),
            Address2: this.newRecordsToCreate[0].record.Address2.toUpperCase(),
            //City: customerInputs.City.toUpperCase(),
            //State: customerInputs.State.toUpperCase(),
            //ZipCode: customerInputs.ZipCode.toUpperCase(),
            //Phone: TestUtils.formattedPhone(customerInputs.Phone),
            Fax: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.Fax),
        }


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
                    Vendor: TestUtils.randomCompanyName() + " GlobalScope.TestToken~1.TestToken",
                    VendorNumber: "GlobalScope.TestToken~1.MediumTestToken",
                    Address1: TestUtils.randomAddress1(),
                    Address2: TestUtils.randomAddress2(),
                    City: TestUtils.randomCity(),
                    State: TestUtils.randomState(),
                    ZipCode: TestUtils.randomZipCode(),
                    Phone: TestUtils.randomPhone(),
                    Fax: TestUtils.randomPhone(),
                    WebAddress: TestUtils.randomUrl(),
                    //OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
                },
                seekObject: {
                    VendorDisplayName: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Vendor: this.newRecordsToCreate[0].record.Vendor.toUpperCase(),
            VendorNumber: this.newRecordsToCreate[0].record.VendorNumber.toUpperCase(),
            Address1: this.newRecordsToCreate[0].record.Address1.toUpperCase(),
            Address2: this.newRecordsToCreate[0].record.Address2.toUpperCase(),
            City: this.newRecordsToCreate[0].record.City.toUpperCase(),
            State: this.newRecordsToCreate[0].record.State.toUpperCase(),
            ZipCode: this.newRecordsToCreate[0].record.ZipCode.toUpperCase(),
            Phone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.Phone),
            Fax: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.Fax),
            WebAddress: this.newRecordsToCreate[0].record.WebAddress,
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation"
        }
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
        this.grids.push(new GridBase("ContactCompanyGrid"));
        this.grids.push(new GridBase("ContactNoteGrid"));

        this.newRecordsToCreate = [
            {
                record: {
                    FirstName: TestUtils.randomFirstName(),
                    LastName: `${TestUtils.randomLastName()} GlobalScope.TestToken~1.TestToken`,
                    Email: TestUtils.randomEmail(),
                    Address1: TestUtils.randomAddress1(),
                    Address2: TestUtils.randomAddress2(),
                    City: TestUtils.randomCity(),
                    State: TestUtils.randomStateCode(),
                    ZipCode: TestUtils.randomZipCode(),
                    OfficePhone: TestUtils.randomPhone(),
                    OfficeExtension: TestUtils.randomPhoneExtension(),
                    DirectPhone: TestUtils.randomPhone(),
                    MobilePhone: TestUtils.randomPhone(),
                    HomePhone: TestUtils.randomPhone(),
                    Fax: TestUtils.randomPhone(),
                },
                seekObject: {
                    LastName: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
        this.newRecordsToCreate[0].gridRecords = [
            {
                gridSelector: "ContactCompanyGrid",
                recordToCreate: {
                    record: {
                        CompanyId: 5,
                        ContactTitleId: 4,
                        OfficePhone: TestUtils.randomPhone(),
                    }
                }
            },
            {
                gridSelector: "ContactNoteGrid",
                recordToCreate: {
                    record: {
                        Description: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            FirstName: this.newRecordsToCreate[0].record.FirstName.toUpperCase(),
            LastName: this.newRecordsToCreate[0].record.LastName.toUpperCase(),
            Email: this.newRecordsToCreate[0].record.Email,
            Address1: this.newRecordsToCreate[0].record.Address1.toUpperCase(),
            Address2: this.newRecordsToCreate[0].record.Address2.toUpperCase(),
            City: this.newRecordsToCreate[0].record.City.toUpperCase(),
            State: this.newRecordsToCreate[0].record.State.toUpperCase(),
            ZipCode: this.newRecordsToCreate[0].record.ZipCode.toUpperCase(),
            OfficePhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.OfficePhone),
            OfficeExtension: this.newRecordsToCreate[0].record.OfficeExtension,
            DirectPhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.DirectPhone),
            MobilePhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.MobilePhone),
            HomePhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.HomePhone),
            Fax: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.Fax),
        }

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

        this.newRecordsToCreate = [
            {
                record: {
                    VendorId: 2,
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    ReferenceNumber: TestUtils.randomAlphanumeric(8)
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            PurchaseOrderNumber: "|NOTEMPTY|",
            ReferenceNumber: this.newRecordsToCreate[0].record.ReferenceNumber.toUpperCase()
        }


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


        this.newRecordsToCreate = [
            {
                record: {
                    Project: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    //Deal: dealInputs.Deal,
                    DealId: 1,
                    ProjectDescription: TestUtils.randomLoremWords(10),
                },
                seekObject: {
                    Project: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            //Deal: this.newRecordsToCreate[0].record.Deal.toUpperCase(),
            Project: this.newRecordsToCreate[0].record.Project.toUpperCase(),
            ProjectNumber: "|NOTEMPTY|",
            ProjectDescription: this.newRecordsToCreate[0].record.ProjectDescription,
        }

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
            ICode: "",
            Description: "",
            Unit: "GlobalScope.DefaultSettings~1.DefaultUnit",   // ie. "EA"
        }

        this.newRecordsToCreate = [
            {
                record: {
                    ICode: "GlobalScope.RentalInventory~NEWICODE.newICode",
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                    TrackedBy: "QUANTITY",
                    IsFixedAsset: true
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            ManufacturerPartNumber: this.newRecordsToCreate[0].record.ManufacturerPartNumber.toUpperCase(),
            TrackedBy: this.newRecordsToCreate[0].record.TrackedBy.toUpperCase(),
            IsFixedAsset: this.newRecordsToCreate[0].record.IsFixedAsset,
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

        this.newRecordsToCreate = [
            {
                record: {
                    ICode: TestUtils.randomAlphanumeric(7),
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ICode: this.newRecordsToCreate[0].record.ICode.toUpperCase(),
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            ManufacturerPartNumber: this.newRecordsToCreate[0].record.ManufacturerPartNumber.toUpperCase(),
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

        this.newRecordsToCreate = [
            {
                record: {
                    ICode: TestUtils.randomAlphanumeric(7),
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ICode: this.newRecordsToCreate[0].record.ICode.toUpperCase(),
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            ManufacturerPartNumber: this.newRecordsToCreate[0].record.ManufacturerPartNumber.toUpperCase(),
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
        this.canNew = false;
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

        this.newRecordsToCreate = [
            {
                record: {
                    Description: `${TestUtils.randomProductName().substring(0, 20)} GlobalScope.TestToken~1.TestToken`,
                    InventoryTypeId: 2,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
        }


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
        this.canNew = false;
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
        this.canNew = false;
        this.canDelete = false;
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
        this.canNew = false;
        this.canDelete = false;
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
        this.canDelete = false;

        this.newRecordsToCreate = [
            {
                record: {
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    FromWarehouseCode: "GlobalScope.Warehouse~MINE.WarehouseCode",
                    ToWarehouseId: 2,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
        }


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
        this.canNew = false;
        this.canDelete = false;
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
        this.canDelete = false;

        this.defaultNewRecordToExpect = {
            InvoiceNumber: "",
            InvoiceDescription: "",
            Department: "GlobalScope.User~ME.PrimaryDepartment",
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            Status: "NEW",
        }

        this.newRecordsToCreate = [
            {
                record: {
                    InvoiceDescription: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    DealId: 2,
                    RateType: 1,
                },
                seekObject: {
                    InvoiceDescription: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            InvoiceNumber: "|NOTEMPTY|",
            Deal: "|NOTEMPTY|",
            RateType: "|NOTEMPTY|",
            InvoiceDescription: this.newRecordsToCreate[0].record.InvoiceDescription.toUpperCase(),
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
        }



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

