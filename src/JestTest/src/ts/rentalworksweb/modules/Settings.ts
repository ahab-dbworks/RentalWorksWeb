import { SettingsModule } from "../../shared/SettingsModule";
import { TestUtils } from "../../shared/TestUtils";
import { GridBase } from "../../shared/GridBase";

//todo: 
//    presentation layer grid - uncomment in v49
//    attempt duplicate on ordersetno - uncomment in v49
//    building space and rate grids (required records to persist in "parent" grids)
//    event type - activity grid - records are created by default, want to selectively delete only our "new" record
//    template grids - need to add "rectype" classes to the OrderItemGrids to differentiate them

//---------------------------------------------------------------------------------------
export class AccountingSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'AccountingSettings';
        this.moduleId = '6EB6300F-1416-42DE-B776-3E418656021D';
        this.moduleCaption = 'Accounting Settings';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GlAccount extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GlAccount';
        this.moduleId = 'F03CA227-99EE-42EF-B615-94540DCB21B3';
        this.moduleCaption = 'Chart of Accounts';

        this.defaultNewRecordToExpect = {
            GlAccountNo: "",
            GlAccountDescription: "",
            GlAccountType: "ASSET",
            Inactive: false
        }

        this.newRecordsToCreate = [
            {
                record: {
                    GlAccountNo: "GlobalScope.TestToken~1.TestToken",
                    GlAccountDescription: "GlobalScope.TestToken~1.TestToken",
                    GlAccountType: "INCOME",
                },
                seekObject: {
                    GlAccountNo: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    GlAccountNo: "",
                    GlAccountDescription: "GlobalScope.TestToken~1.TestToken",
                    GlAccountType: "INCOME",
                },
                expectedErrorFields: ["GlAccountNo"]
            },
            {
                record: {
                    GlAccountNo: "GlobalScope.TestToken~1.TestToken",
                    GlAccountDescription: "",
                    GlAccountType: "INCOME",
                },
                expectedErrorFields: ["GlAccountDescription"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            GlAccountNo: this.newRecordsToCreate[0].record.GlAccountNo.toUpperCase(),
            GlAccountDescription: this.newRecordsToCreate[0].record.GlAccountDescription.toUpperCase(),
            GlAccountType: this.newRecordsToCreate[0].record.GlAccountType.toUpperCase(),
            Inactive: false
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GlDistribution extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GlDistribution';
        this.moduleId = '7C249F59-B5E3-4DAE-933D-38D30858CF7C';
        this.moduleCaption = 'G/L Distribution';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Country extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Country';
        this.moduleId = 'D6E787E6-502B-4D36-B0A6-FA691E6D10CF';
        this.moduleCaption = 'Country';

        this.defaultNewRecordToExpect = {
            Country: "",
            CountryCode: "",
            IsUSA: false,
            Metric: false,
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Country: "GlobalScope.TestToken~1.TestToken",
                    CountryCode: "GlobalScope.TestToken~1.ShortTestToken",
                    IsUSA: true,
                },
                seekObject: {
                    Country: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Country: "",
                    CountryCode: "GlobalScope.TestToken~1.ShortTestToken",
                },
                expectedErrorFields: ["Country"]
            },
            {
                record: {
                    Country: "GlobalScope.TestToken~1.TestToken",
                    CountryCode: "",
                },
                expectedErrorFields: ["CountryCode"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Country: this.newRecordsToCreate[0].record.Country.toUpperCase(),
            CountryCode: this.newRecordsToCreate[0].record.CountryCode.toUpperCase(),
            IsUSA: true,
            Metric: false,
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class State extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'State';
        this.moduleId = 'B70B4B88-51EB-4635-971B-1F676243B810';
        this.moduleCaption = 'State/Province';

        this.defaultNewRecordToExpect = {
            StateCode: "",
            State: "",
        }

        this.newRecordsToCreate = [
            {
                record: {
                    StateCode: "GlobalScope.TestToken~1.TinyTestToken",
                    State: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    State: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    StateCode: "",
                    State: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["StateCode"]
            },
            {
                record: {
                    StateCode: "GlobalScope.TestToken~1.TinyTestToken",
                    State: "",
                },
                expectedErrorFields: ["State"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            StateCode: this.newRecordsToCreate[0].record.StateCode.toUpperCase(),
            State: this.newRecordsToCreate[0].record.State.toUpperCase(),
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class BillingCycle extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'BillingCycle';
        this.moduleId = '5736D549-CEA7-4FCF-86DA-0BCD4C87FA04';
        this.moduleCaption = 'Billing Cycle';

        this.defaultNewRecordToExpect = {
            BillingCycle: "",
            Inactive: false,
            BillingCycleType: "WEEKLY",
            BillOnPeriodStartOrEnd: "END",
            ProrateMonthly: false
        }

        this.newRecordsToCreate = [
            {
                record: {
                    BillingCycle: "GlobalScope.TestToken~1.MediumTestToken",
                    BillingCycleType: "MONTHLY",

                },
                seekObject: {
                    BillingCycle: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    BillingCycle: "",
                    BillingCycleType: "MONTHLY",
                },
                expectedErrorFields: ["BillingCycle"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            BillingCycle: this.newRecordsToCreate[0].record.BillingCycle.toUpperCase(),
            BillingCycleType: this.newRecordsToCreate[0].record.BillingCycleType.toUpperCase(),
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Department extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Department';
        this.moduleId = 'A6CC5F50-F9DE-4158-B627-6FDC1044C22A';
        this.moduleCaption = 'Company Department';


        this.defaultNewRecordToExpect = {
            Department: "",
            DepartmentCode: "",
            DivisionCode: "",
            Inactive: false,
            DisableEditingRentalRate: false,
            DisableEditingSalesRate: false,
            DisableEditingMiscellaneousRate: false,
            DisableEditingLaborRate: false,
            DisableEditingUsedSaleRate: false,
            DisableEditingLossAndDamageRate: false,
            SalesBillingRule: "BILLINGDATE",
            LockLineItemsWhenCustomDiscountUsed: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Department: "GlobalScope.TestToken~1.TestToken",
                    DepartmentCode: "GlobalScope.TestToken~1.ShortTestToken",
                },
                seekObject: {
                    Department: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Department: "",
                    DepartmentCode: "GlobalScope.TestToken~1.ShortTestToken",
                },
                expectedErrorFields: ["Department"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Department: this.newRecordsToCreate[0].record.Department.toUpperCase(),
            DepartmentCode: this.newRecordsToCreate[0].record.DepartmentCode.toUpperCase(),
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ContactEvent extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ContactEvent';
        this.moduleId = '25ad258e-db9d-4e94-a500-0382e7a2024a';
        this.moduleCaption = 'Contact Event';

        this.defaultNewRecordToExpect = {
            ContactEvent: "",
            Inactive: false,
            Recurring: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    ContactEvent: "GlobalScope.TestToken~1.TestToken",
                    Recurring: true,
                },
                seekObject: {
                    ContactEvent: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ContactEvent: "",
                },
                expectedErrorFields: ["ContactEvent"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ContactEvent: this.newRecordsToCreate[0].record.ContactEvent.toUpperCase(),
            Recurring: this.newRecordsToCreate[0].record.Recurring,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ContactTitle extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ContactTitle';
        this.moduleId = '1b9183b2-add9-416d-a5e1-59fe68104e4a';
        this.moduleCaption = 'Contact Title';

        this.defaultNewRecordToExpect = {
            ContactTitle: "",
            Inactive: false,
            AccountsPayable: false,
            AccountsReceivable: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    ContactTitle: "GlobalScope.TestToken~1.TestToken",
                    AccountsReceivable: true,
                },
                seekObject: {
                    ContactTitle: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ContactTitle: "",
                    AccountsReceivable: true,
                },
                expectedErrorFields: ["ContactTitle"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ContactTitle: this.newRecordsToCreate[0].record.ContactTitle.toUpperCase(),
            AccountsReceivable: this.newRecordsToCreate[0].record.AccountsReceivable,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MailList extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MailList';
        this.moduleId = '255ceb68-fb87-4248-ab99-37c18a192300';
        this.moduleCaption = 'Mail List';


        this.defaultNewRecordToExpect = {
            MailList: "",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    MailList: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    MailList: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    MailList: "",
                },
                expectedErrorFields: ["MailList"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            MailList: this.newRecordsToCreate[0].record.MailList.toUpperCase(),
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Currency extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Currency';
        this.moduleId = '672145d0-9b37-4f6f-a216-9ae1e7728168';
        this.moduleCaption = 'Currency';

        this.defaultNewRecordToExpect = {
            Currency: "",
            CurrencyCode: "",
            CurrencySymbol: "",
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Currency: "GlobalScope.TestToken~1.MediumTestToken",
                    CurrencyCode: "GlobalScope.TestToken~1.TinyTestToken",
                    CurrencySymbol: "GlobalScope.TestToken~1.LastCharTestToken",
                },
                seekObject: {
                    Currency: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Currency: "",
                    CurrencyCode: "GlobalScope.TestToken~1.TinyTestToken",
                    CurrencySymbol: "GlobalScope.TestToken~1.LastCharTestToken",
                },
                expectedErrorFields: ["Currency"]
            },
            {
                record: {
                    Currency: "GlobalScope.TestToken~1.MediumTestToken",
                    CurrencyCode: "",
                    CurrencySymbol: "GlobalScope.TestToken~1.LastCharTestToken",
                },
                expectedErrorFields: ["CurrencyCode"]
            },
            {
                record: {
                    Currency: "GlobalScope.TestToken~1.MediumTestToken",
                    CurrencyCode: "GlobalScope.TestToken~1.TinyTestToken",
                    CurrencySymbol: "",
                },
                expectedErrorFields: ["CurrencySymbol"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Currency: this.newRecordsToCreate[0].record.Currency.toUpperCase(),
            CurrencyCode: this.newRecordsToCreate[0].record.CurrencyCode.toUpperCase(),
            CurrencySymbol: this.newRecordsToCreate[0].record.CurrencySymbol.toUpperCase(),
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CreditStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CreditStatus';
        this.moduleId = 'A28D0CC9-B922-4259-BA4A-A5DE474ADFA4';
        this.moduleCaption = 'Credit Status';

        this.defaultNewRecordToExpect = {
            CreditStatus: "",
            CreateContractAllowed: false,
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    CreditStatus: "GlobalScope.TestToken~1.TestToken",
                    CreateContractAllowed: true
                },
                seekObject: {
                    CreditStatus: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    CreditStatus: "",
                    CreateContractAllowed: true
                },
                expectedErrorFields: ["CreditStatus"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            CreditStatus: this.newRecordsToCreate[0].record.CreditStatus.toUpperCase(),
            CreateContractAllowed: this.newRecordsToCreate[0].record.CreateContractAllowed
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CustomerCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CustomerCategory';
        this.moduleId = '8FB6C746-AB6E-4CA5-9BD4-4E9AD88A3BC5';
        this.moduleCaption = 'Customer Category';

        this.defaultNewRecordToExpect = {
            CustomerCategory: "",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    CustomerCategory: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    CustomerCategory: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    CustomerCategory: "",
                },
                expectedErrorFields: ["CustomerCategory"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            CustomerCategory: this.newRecordsToCreate[0].record.CustomerCategory.toUpperCase(),
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CustomerStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CustomerStatus';
        this.moduleId = 'B689A0AA-9FCC-450B-AF0F-AD85483531FA';
        this.moduleCaption = 'Customer Status';

        this.defaultNewRecordToExpect = {
            CustomerStatus: "",
            CreditStatusId: "",
            StatusType: "O",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    CustomerStatus: "GlobalScope.TestToken~1.ShortTestToken",
                    StatusType: "H",
                },
                seekObject: {
                    CustomerStatus: "GlobalScope.TestToken~1.ShortTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    CustomerStatus: "",
                    StatusType: "H",
                },
                expectedErrorFields: ["CustomerStatus"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            CustomerStatus: this.newRecordsToCreate[0].record.CustomerStatus.toUpperCase(),
            CreditStatusId: "",
            StatusType: this.newRecordsToCreate[0].record.StatusType.toUpperCase(),
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CustomerType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CustomerType';
        this.moduleId = '314EDC6F-478A-40E2-B17E-349886A85EA0';
        this.moduleCaption = 'Customer Type';


        this.defaultNewRecordToExpect = {
            CustomerType: "",
            Inactive: false,
            DefaultRentalDiscountPercent: "",
            DefaultSalesDiscountPercent: "",
            DefaultFacilitiesDiscountPercent: "",
        }

        this.newRecordsToCreate = [
            {
                record: {
                    CustomerType: "GlobalScope.TestToken~1.TestToken",
                    DefaultRentalDiscountPercent: "10",
                    DefaultSalesDiscountPercent: "20",
                    DefaultFacilitiesDiscountPercent: "30.33",
                },
                seekObject: {
                    CustomerType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    CustomerType: "",
                },
                expectedErrorFields: ["CustomerType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            CustomerType: this.newRecordsToCreate[0].record.CustomerType.toUpperCase(),
            DefaultRentalDiscountPercent: this.newRecordsToCreate[0].record.DefaultRentalDiscountPercent + " %",
            DefaultSalesDiscountPercent: this.newRecordsToCreate[0].record.DefaultSalesDiscountPercent + " %",
            DefaultFacilitiesDiscountPercent: this.newRecordsToCreate[0].record.DefaultFacilitiesDiscountPercent + " %",
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DealClassification extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DealClassification';
        this.moduleId = 'D1035FCC-D92B-4A3A-B985-C7E02CBE3DFD';
        this.moduleCaption = 'Deal Classification';


        this.defaultNewRecordToExpect = {
            DealClassification: "",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    DealClassification: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    DealClassification: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    DealClassification: "",
                },
                expectedErrorFields: ["DealClassification"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            DealClassification: this.newRecordsToCreate[0].record.DealClassification.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DealType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DealType';
        this.moduleId = 'A021AE67-0F33-4C97-9149-4CD5560EE10A';
        this.moduleCaption = 'Deal Type';

        this.defaultNewRecordToExpect = {
            DealType: "",
            Inactive: false,
            WhiteText: false
        }

        this.newRecordsToCreate = [
            {
                record: {
                    DealType: "GlobalScope.TestToken~1.TestToken",
                    WhiteText: true,
                },
                seekObject: {
                    DealType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    DealType: "",
                    WhiteText: true
                },
                expectedErrorFields: ["DealType"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            DealType: this.newRecordsToCreate[0].record.DealType.toUpperCase(),
            Inactive: false,
            WhiteText: this.newRecordsToCreate[0].record.WhiteText,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DealStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DealStatus';
        this.moduleId = '543F8F83-20AB-4001-8283-1E73A9D795DF';
        this.moduleCaption = 'Deal Status';

        this.defaultNewRecordToExpect = {
            DealStatus: "",
            Inactive: false,
            StatusType: "O",
        }

        this.newRecordsToCreate = [
            {
                record: {
                    DealStatus: "GlobalScope.TestToken~1.MediumTestToken",
                    StatusType: "H",
                },
                seekObject: {
                    DealStatus: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    DealStatus: "",
                    StatusType: "H",
                },
                expectedErrorFields: ["DealStatus"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            DealStatus: this.newRecordsToCreate[0].record.DealStatus.toUpperCase(),
            StatusType: this.newRecordsToCreate[0].record.StatusType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProductionType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProductionType';
        this.moduleId = '993EBF0C-EDF0-47A2-8507-51492502088B';
        this.moduleCaption = 'Production Type';

        this.defaultNewRecordToExpect = {
            ProductionType: "",
            ProductionTypeCode: "",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    ProductionType: "GlobalScope.TestToken~1.TestToken",
                    ProductionTypeCode: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    ProductionType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ProductionType: "",
                    ProductionTypeCode: "GlobalScope.TestToken~1.MediumTestToken",
                },
                expectedErrorFields: ["ProductionType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ProductionType: this.newRecordsToCreate[0].record.ProductionType.toUpperCase(),
            ProductionTypeCode: this.newRecordsToCreate[0].record.ProductionTypeCode.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ScheduleType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ScheduleType';
        this.moduleId = '8646d7bb-9676-4fdd-b9ea-db98045390f4';
        this.moduleCaption = 'Schedule Type';

        this.defaultNewRecordToExpect = {
            ScheduleType: "",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    ScheduleType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    ScheduleType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ScheduleType: "",
                },
                expectedErrorFields: ["ScheduleType"],
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ScheduleType: this.newRecordsToCreate[0].record.ScheduleType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DiscountTemplate extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DiscountTemplate';
        this.moduleId = '258D920E-7024-4F68-BF1F-F07F3613829C';
        this.moduleCaption = 'Discount Template';
        let rentalGrid: GridBase = new GridBase("DiscountItemRentalGrid");
        let salesGrid: GridBase = new GridBase("DiscountItemSalesGrid");
        let laborGrid: GridBase = new GridBase("DiscountItemLaborGrid");
        let miscGrid: GridBase = new GridBase("DiscountItemMiscGrid");
        this.grids.push(rentalGrid);
        this.grids.push(salesGrid);
        this.grids.push(laborGrid);
        this.grids.push(miscGrid);

        this.defaultNewRecordToExpect = {
            DiscountTemplate: "",
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Inactive: false,
            Rental: false,
            Sales: false,
            Labor: false,
            Misc: false,
            RentalDiscountPercent: "",
            SalesDiscountPercent: "",
            SpaceDiscountPercent: "",
            RentalDaysPerWeek: "",
            SpaceDaysPerWeek: "",
        };

        this.newRecordsToCreate = [
            {
                record: {
                    DiscountTemplate: "GlobalScope.TestToken~1.TestToken",
                    Rental: true,
                    Sales: true,
                    Labor: true,
                    Misc: true,
                    RentalDiscountPercent: "10",
                    SalesDiscountPercent: "20",
                    SpaceDiscountPercent: "30",
                    RentalDaysPerWeek: "1.25",
                    SpaceDaysPerWeek: "3.45",
                },
                seekObject: {
                    DiscountTemplate: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    DiscountTemplate: "",
                    Rental: false,
                    Sales: true,
                    Labor: false,
                    Misc: true,
                    RentalDiscountPercent: "10",
                    SalesDiscountPercent: "20",
                    SpaceDiscountPercent: "30",
                    RentalDaysPerWeek: "1.25",
                    SpaceDaysPerWeek: "3.45",
                },
                expectedErrorFields: ["DiscountTemplate"],
            },
        ];
		
		this.newRecordsToCreate[0].gridRecords = [
            {
                grid: rentalGrid,
                recordToCreate: {
                    record: {
                        DiscountPercent: TestUtils.randomIntegerBetween(10, 90).toString(),
                        InventoryTypeId: 1,
                    }
                }
            },
            {
                grid: salesGrid,
                recordToCreate: {
                    record: {
                        DiscountPercent: TestUtils.randomIntegerBetween(10, 90).toString(),
                        InventoryTypeId: 1,
                    }
                }
            },
            {
                grid: laborGrid,
                recordToCreate: {
                    record: {
                        DiscountPercent: TestUtils.randomIntegerBetween(10, 90).toString(),
                        InventoryTypeId: 1,
                    }
                }
            },
            {
                grid: miscGrid,
                recordToCreate: {
                    record: {
                        DiscountPercent: TestUtils.randomIntegerBetween(10, 90).toString(),
                        InventoryTypeId: 1,
                    }
                }
            },
        ];



        this.newRecordsToCreate[0].recordToExpect = {
            DiscountTemplate: this.newRecordsToCreate[0].record.DiscountTemplate.toUpperCase(),
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Inactive: false,
            Rental: this.newRecordsToCreate[0].record.Rental,
            Sales: this.newRecordsToCreate[0].record.Sales,
            Labor: this.newRecordsToCreate[0].record.Labor,
            Misc: this.newRecordsToCreate[0].record.Misc,
            RentalDiscountPercent: this.newRecordsToCreate[0].record.RentalDiscountPercent + " %",
            SalesDiscountPercent: this.newRecordsToCreate[0].record.SalesDiscountPercent + " %",
            SpaceDiscountPercent: this.newRecordsToCreate[0].record.SpaceDiscountPercent + " %",
            RentalDaysPerWeek: this.newRecordsToCreate[0].record.RentalDaysPerWeek,
            SpaceDaysPerWeek: this.newRecordsToCreate[0].record.SpaceDaysPerWeek,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DocumentType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DocumentType';
        this.moduleId = '358fbe63-83a7-4ab4-973b-1a5520573674';
        this.moduleCaption = 'Document Type';

        this.defaultNewRecordToExpect = {
            DocumentType: "",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    DocumentType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    DocumentType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    DocumentType: "",
                },
                expectedErrorFields: ["DocumentType"],
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            DocumentType: this.newRecordsToCreate[0].record.DocumentType.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CoverLetter extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CoverLetter';
        this.moduleId = 'BE13DA09-E3AA-4520-A16C-F43F1A207EA5';
        this.moduleCaption = 'Cover Letter';


        this.defaultNewRecordToExpect = {
            Description: "",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Description: "",
                },
                expectedErrorFields: ["Description"],
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TermsConditions extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TermsConditions';
        this.moduleId = '5C09A4C3-4272-458A-80DA-A5DF6B098D02';
        this.moduleCaption = 'Terms & Conditions';


        this.defaultNewRecordToExpect = {
            Description: "",
            StartOnNewPage: false,
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                    StartOnNewPage: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Description: "",
                },
                expectedErrorFields: ["Description"],
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            StartOnNewPage: this.newRecordsToCreate[0].record.StartOnNewPage,
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class EventCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'EventCategory';
        this.moduleId = '3912b3cc-b35f-434d-aeeb-c45fed537e29';
        this.moduleCaption = 'Event Category';

        this.defaultNewRecordToExpect = {
            EventCategory: "",
            EventCategoryCode: "",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    EventCategory: "GlobalScope.TestToken~1.TestToken",
                    EventCategoryCode: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    EventCategory: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    EventCategory: "",
                },
                expectedErrorFields: ["EventCategory"],
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            EventCategory: this.newRecordsToCreate[0].record.EventCategory.toUpperCase(),
            EventCategoryCode: this.newRecordsToCreate[0].record.EventCategoryCode.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class EventType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'EventType';
        this.moduleId = 'FE501F99-95D4-444C-A7B6-EA20ACE88879';
        this.moduleCaption = 'Event Type';
        let personnelTypeGrid: GridBase = new GridBase("EventTypePersonnelTypeGrid");
        this.grids.push(personnelTypeGrid);

        this.defaultNewRecordToExpect = {
            EventType: "",
            Inactive: false,
        };

        this.newRecordsToCreate = [
            {
                record: {
                    EventType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    EventType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    EventType: "",
                },
                expectedErrorFields: ["EventType"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: personnelTypeGrid,
                recordToCreate: {
                    record: {
                        PersonnelTypeId: 1,
                    }
                }
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            EventType: this.newRecordsToCreate[0].record.EventType.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PersonnelType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PersonnelType';
        this.moduleId = '46339c9c-c663-4041-aeb4-a7f85783996f';
        this.moduleCaption = 'Personnel Type';

        this.defaultNewRecordToExpect = {
            PersonnelType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PersonnelType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    PersonnelType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PersonnelType: "",
                },
                expectedErrorFields: ["PersonnelType"],
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            PersonnelType: this.newRecordsToCreate[0].record.PersonnelType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PhotographyType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PhotographyType';
        this.moduleId = '66bff7f0-8bca-4d32-bd94-6b5f13623bec';
        this.moduleCaption = 'Photography Type';

        this.defaultNewRecordToExpect = {
            PhotographyType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PhotographyType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    PhotographyType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PhotographyType: "",
                },
                expectedErrorFields: ["PhotographyType"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PhotographyType: this.newRecordsToCreate[0].record.PhotographyType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Building extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Building';
        this.moduleId = '2D344845-7E77-40C9-BB9D-04A930D352EB';
        this.moduleCaption = 'Building';
        let floorGrid: GridBase = new GridBase("FloorGrid");
        this.grids.push(floorGrid);

        this.defaultNewRecordToExpect = {
            Building: "",
            BuildingCode: "",
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Inactive: false,
        }

        this.newRecordsToCreate = [
            {
                record: {
                    Building: "GlobalScope.TestToken~1.TestToken",
                    BuildingCode: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    Building: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Building: "",
                    BuildingCode: "GlobalScope.TestToken~1.MediumTestToken",
                },
                expectedErrorFields: ["Building"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: floorGrid,
                recordToCreate: {
                    record: {
                        Floor: "GlobalScope.TestToken~1.ShortTestToken",
                    }
                }
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Building: this.newRecordsToCreate[0].record.Building.toUpperCase(),
            BuildingCode: this.newRecordsToCreate[0].record.BuildingCode.toUpperCase(),
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityType';
        this.moduleId = '197BBE51-28A8-4D00-BD0C-098C0F88DD0E';
        this.moduleCaption = 'Facility Type';

        this.defaultNewRecordToExpect = {
            FacilityType: "",
            Inactive: false,
        };

        this.newRecordsToCreate = [
            {
                record: {
                    FacilityType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    FacilityType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    FacilityType: "",
                },
                expectedErrorFields: ["FacilityType"],
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            FacilityType: this.newRecordsToCreate[0].record.FacilityType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityRate extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityRate';
        this.moduleId = '5D49FC0B-F1BA-4FE4-889D-3C52B6202ACD';
        this.moduleCaption = 'Facility Rate';

        this.defaultNewRecordToExpect = {
            FacilityType: "",
            Category: "",
            ICode: "",
            Description: "",
            Unit: "",
            NonDiscountable: false,
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    FacilityTypeId: 1,
                    CategoryId: 1,
                    ICode: TestUtils.randomAlphanumeric(8),
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    FacilityTypeId: 1,
                    CategoryId: 1,
                    ICode: TestUtils.randomAlphanumeric(8),
                    Description: "",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["Description"]
            },
            {
                record: {
                    FacilityTypeId: 1,
                    ICode: TestUtils.randomAlphanumeric(8),
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["CategoryId"]
            },
            {
                record: {
                    ICode: TestUtils.randomAlphanumeric(8),
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["FacilityTypeId", "CategoryId"]
            },
            {
                record: {
                    FacilityTypeId: 1,
                    CategoryId: 1,
                    ICode: TestUtils.randomAlphanumeric(8),
                    Description: "GlobalScope.TestToken~1.TestToken",
                    NonDiscountable: true,
                },
                expectedErrorFields: ["UnitId"]
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            FacilityType: "|NOTEMPTY|",
            Category: "|NOTEMPTY|",
            ICode: this.newRecordsToCreate[0].record.ICode.toUpperCase(),
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Unit: "|NOTEMPTY|",
            NonDiscountable: true,
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityScheduleStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityScheduleStatus';
        this.moduleId = 'A693C2F7-DF16-4492-9DE5-FC672375C44E';
        this.moduleCaption = 'Facility Schedule Status';

        this.defaultNewRecordToExpect = {
            ScheduleStatus: "",
            StatusAction: "QUIKHOLD",
            Inactive: false,
        };

        this.newRecordsToCreate = [
            {
                record: {
                    ScheduleStatus: "GlobalScope.TestToken~1.MediumTestToken",
                    StatusAction: "HOLD",
                },
                seekObject: {
                    ScheduleStatus: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ScheduleStatus: "",
                    StatusAction: "HOLD",
                },
                expectedErrorFields: ["ScheduleStatus"],
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            ScheduleStatus: this.newRecordsToCreate[0].record.ScheduleStatus.toUpperCase(),
            StatusAction: this.newRecordsToCreate[0].record.StatusAction.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityStatus';
        this.moduleId = 'DB2C8448-9287-4885-952F-BE3D0E4BFEF1';
        this.moduleCaption = 'Facility Status';

        this.defaultNewRecordToExpect = {
            FacilityStatus: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    FacilityStatus: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    FacilityStatus: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    FacilityStatus: "",
                },
                expectedErrorFields: ["FacilityStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            FacilityStatus: this.newRecordsToCreate[0].record.FacilityStatus.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FacilityCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FacilityCategory';
        this.moduleId = '67A9BEC5-4865-409C-9327-B2B8714DDAA8';
        this.moduleCaption = 'Facility Category';

        this.defaultNewRecordToExpect = {
            FacilityType: "",
            Category: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    FacilityTypeId: 1,
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    FacilityTypeId: 1,
                    Category: "",
                },
                expectedErrorFields: ["Category"],
            },
            {
                record: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["FacilityTypeId"],
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            FacilityType: "|NOTEMPTY|",
            Category: this.newRecordsToCreate[0].record.Category.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SpaceType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SpaceType';
        this.moduleId = 'EDF05CFB-9F6B-4771-88EB-6FD254CFE6C6';
        this.moduleCaption = 'Space Type / Activity';


        this.defaultNewRecordToExpect = {
            FacilityType: "",
            SpaceType: "",
            ForReportsOnly: false,
            NonBillable: false,
            AddToDescription: false,
            Inactive: false,
            SpaceTypeClassification: "SPACE TYPE",
            RateICode: "",
        }
        this.newRecordsToCreate = [
            {
                record: {
                    FacilityTypeId: 1,
                    SpaceType: "GlobalScope.TestToken~1.MediumTestToken",
                    ForReportsOnly: false,
                    NonBillable: false,
                    AddToDescription: true,
                    SpaceTypeClassification: "ACTIVITY",
                    RateId: 1,
                },
                seekObject: {
                    SpaceType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    FacilityTypeId: 1,
                    SpaceType: "",
                    ForReportsOnly: false,
                    NonBillable: false,
                    AddToDescription: true,
                    SpaceTypeClassification: "ACTIVITY",
                    RateId: 1,
                },
                expectedErrorFields: ["SpaceType"],
            },
            {
                record: {
                    SpaceType: "GlobalScope.TestToken~1.MediumTestToken",
                    ForReportsOnly: false,
                    NonBillable: false,
                    AddToDescription: true,
                    SpaceTypeClassification: "ACTIVITY",
                    RateId: 1,
                },
                expectedErrorFields: ["FacilityTypeId"],
            },
            {
                record: {
                    FacilityTypeId: 1,
                    SpaceType: "GlobalScope.TestToken~1.MediumTestToken",
                    ForReportsOnly: false,
                    NonBillable: false,
                    AddToDescription: true,
                    SpaceTypeClassification: "ACTIVITY",
                },
                expectedErrorFields: ["RateId"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            FacilityType: "|NOTEMPTY|",
            SpaceType: this.newRecordsToCreate[0].record.SpaceType.toUpperCase(),
            ForReportsOnly: this.newRecordsToCreate[0].record.ForReportsOnly,
            NonBillable: this.newRecordsToCreate[0].record.NonBillable,
            AddToDescription: this.newRecordsToCreate[0].record.AddToDescription,
            SpaceTypeClassification: this.newRecordsToCreate[0].record.SpaceTypeClassification.toUpperCase(),
            RateICode: "|NOTEMPTY|",
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FiscalYear extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FiscalYear';
        this.moduleId = '6F87E62B-F17A-48CB-B673-16D12B6DFFB9';
        this.moduleCaption = 'Fiscal Year';

        this.defaultNewRecordToExpect = {
            Year: "",
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Year: TestUtils.randomIntegerBetween(2051, 2078).toString(),
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Year: "",
                },
                expectedErrorFields: ["Year"],
            },
        ];
        this.newRecordsToCreate[0].seekObject = {
            Year: this.newRecordsToCreate[0].record.Year,
        };
        this.newRecordsToCreate[0].recordToExpect = {
            Year: this.newRecordsToCreate[0].record.Year,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorFuelType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorFuelType';
        this.moduleId = '8A331FE0-B92A-4DD2-8A59-29E4E6D6EA4F';
        this.moduleCaption = 'Generator Fuel Type';

        this.defaultNewRecordToExpect = {
            GeneratorFuelType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    GeneratorFuelType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    GeneratorFuelType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    GeneratorFuelType: "",
                },
                expectedErrorFields: ["GeneratorFuelType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            GeneratorFuelType: this.newRecordsToCreate[0].record.GeneratorFuelType.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorMake extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorMake';
        this.moduleId = 'D7C38A54-A198-4304-8EC2-CE8038D3BE9C';
        this.moduleCaption = 'Generator Make';
        let generatorMakeGrid: GridBase = new GridBase("GeneratorMakeModelGrid");
        this.grids.push(generatorMakeGrid);

        this.defaultNewRecordToExpect = {
            GeneratorMake: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    GeneratorMake: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    GeneratorMake: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    GeneratorMake: "",
                },
                expectedErrorFields: ["GeneratorMake"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: generatorMakeGrid,
                recordToCreate: {
                    record: {
                        GeneratorModel: "GlobalScope.TestToken~1.MediumTestToken",
                    }
                }
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            GeneratorMake: this.newRecordsToCreate[0].record.GeneratorMake.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorRating extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorRating';
        this.moduleId = '140E6997-1BA9-49B7-AA79-CD5EF6444C72';
        this.moduleCaption = 'Generator Rating';

        this.defaultNewRecordToExpect = {
            GeneratorRating: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    GeneratorRating: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    GeneratorRating: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    GeneratorRating: "",
                },
                expectedErrorFields: ["GeneratorRating"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            GeneratorRating: this.newRecordsToCreate[0].record.GeneratorRating.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorWatts extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorWatts';
        this.moduleId = '503349D6-711A-4F45-8891-4B3203008441';
        this.moduleCaption = 'Generator Watts';

        this.defaultNewRecordToExpect = {
            GeneratorWatts: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    GeneratorWatts: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    GeneratorWatts: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    GeneratorWatts: "",
                },
                expectedErrorFields: ["GeneratorWatts"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            GeneratorWatts: this.newRecordsToCreate[0].record.GeneratorWatts.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class GeneratorType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GeneratorType';
        this.moduleId = '95D9D422-DCEB-4150-BCC2-79573B87AC4D';
        this.moduleCaption = 'Generator Type';

        this.defaultNewRecordToExpect = {
            InventoryType: "",
            GeneratorType: "",
            Unit: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryTypeId: 1,
                    GeneratorType: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                },
                seekObject: {
                    GeneratorType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    InventoryTypeId: 1,
                    GeneratorType: "",
                    UnitId: 1,
                },
                expectedErrorFields: ["GeneratorType"],
            },
            {
                record: {
                    GeneratorType: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                },
                expectedErrorFields: ["InventoryTypeId"],
            },
            {
                record: {
                    InventoryTypeId: 1,
                    GeneratorType: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["UnitId"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            InventoryType: "|NOTEMPTY|",
            GeneratorType: this.newRecordsToCreate[0].record.GeneratorType.toUpperCase(),
            Unit: "|NOTEMPTY|",
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Holiday extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Holiday';
        this.moduleId = 'CFFEFF09-A083-478E-913C-945184B5DE94';
        this.moduleCaption = 'Holiday';

        this.defaultNewRecordToExpect = {
            Holiday: "",
            Observed: false,
            Country: "",
            Type: "F",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Holiday: "GlobalScope.TestToken~1.TestToken",
                    Observed: true,
                    CountryId: 1,
                    Type: "O",
                    Adjustment: "2",
                    OffsetHolidayId: 1,
                },
                seekObject: {
                    Holiday: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Holiday: "",
                    Observed: true,
                    CountryId: 1,
                    Type: "O",
                    Adjustment: "2",
                    OffsetHolidayId: 1,
                },
                expectedErrorFields: ["Holiday"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Holiday: this.newRecordsToCreate[0].record.Holiday.toUpperCase(),
            Observed: this.newRecordsToCreate[0].record.Observed,
            Country: "|NOTEMPTY|",
            Type: this.newRecordsToCreate[0].record.Type.toUpperCase(),
            Adjustment: this.newRecordsToCreate[0].record.Adjustment.toUpperCase(),
            OffsetHoliday: "|NOTEMPTY|",
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class BlackoutStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'BlackoutStatus';
        this.moduleId = '43D7C88D-8D8C-424E-94D3-A2C537F0C76E';
        this.moduleCaption = 'Blackout Status';

        this.defaultNewRecordToExpect = {
            BlackoutStatus: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    BlackoutStatus: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    BlackoutStatus: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    BlackoutStatus: "",
                },
                expectedErrorFields: ["BlackoutStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            BlackoutStatus: this.newRecordsToCreate[0].record.BlackoutStatus.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class BarCodeRange extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'BarCodeRange';
        this.moduleId = '9A52C5B8-98AB-49A0-A392-69DB0873F943';
        this.moduleCaption = 'Bar Code Range';

        this.defaultNewRecordToExpect = {
            Prefix: "",
            BarcodeFrom: "",
            BarcodeTo: "",
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Prefix: "GlobalScope.TestToken~1.TinyTestToken",
                    BarcodeFrom: TestUtils.randomIntegerBetween(6000000, 7000000).toString(),
                    BarcodeTo: TestUtils.randomIntegerBetween(8000000, 9000000).toString(),
                },
            },
            {
                record: {
                    Prefix: "GlobalScope.TestToken~1.TinyTestToken",
                    BarcodeFrom: "",
                    BarcodeTo: TestUtils.randomIntegerBetween(8000000, 9000000).toString(),
                },
                expectedErrorFields: ["BarcodeFrom"],
            },
            {
                record: {
                    Prefix: "GlobalScope.TestToken~1.TinyTestToken",
                    BarcodeFrom: TestUtils.randomIntegerBetween(6000000, 7000000).toString(),
                    BarcodeTo: "",
                },
                expectedErrorFields: ["BarcodeTo"],
            },
        ];
        this.newRecordsToCreate[0].seekObject = {
            BarcodeFrom: this.newRecordsToCreate[0].record.BarcodeFrom,
        }
        this.newRecordsToCreate[0].recordToExpect = {
            Prefix: this.newRecordsToCreate[0].record.Prefix.toUpperCase(),
            BarcodeFrom: this.newRecordsToCreate[0].record.BarcodeFrom,
            BarcodeTo: this.newRecordsToCreate[0].record.BarcodeTo,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryAdjustmentReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryAdjustmentReason';
        this.moduleId = 'B3156707-4D41-481C-A66E-8951E5233CDA';
        this.moduleCaption = 'Inventory Adjustment Reason';

        this.defaultNewRecordToExpect = {
            InventoryAdjustmentReason: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryAdjustmentReason: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    InventoryAdjustmentReason: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    InventoryAdjustmentReason: "",
                },
                expectedErrorFields: ["InventoryAdjustmentReason"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            InventoryAdjustmentReason: this.newRecordsToCreate[0].record.InventoryAdjustmentReason.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Attribute extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Attribute';
        this.moduleId = '2777dd37-daca-47ff-aa44-29677b302745';
        this.moduleCaption = 'Inventory Attribute';
        let valueGrid: GridBase = new GridBase("AttributeValueGrid");
        this.grids.push(valueGrid);


        this.defaultNewRecordToExpect = {
            InventoryType: "",
            Attribute: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Attribute: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Attribute: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Attribute: "",
                },
                expectedErrorFields: ["Attribute"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: valueGrid,
                recordToCreate: {
                    record: {
                        AttributeValue: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];


        this.newRecordsToCreate[0].recordToExpect = {
            InventoryType: "(ALL)",
            Attribute: this.newRecordsToCreate[0].record.Attribute.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryCondition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryCondition';
        this.moduleId = 'BF711CAC-1E69-4C92-B509-4CBFA29FAED3';
        this.moduleCaption = 'Inventory Condition';

        this.defaultNewRecordToExpect = {
            InventoryCondition: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryCondition: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    InventoryCondition: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    InventoryCondition: "",
                },
                expectedErrorFields: ["InventoryCondition"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            InventoryCondition: this.newRecordsToCreate[0].record.InventoryCondition.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryGroup extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryGroup';
        this.moduleId = '43AF2FBA-69FB-46A8-8E5A-2712486B66F3';
        this.moduleCaption = 'Inventory Group';
        let invGrid: GridBase = new GridBase("InventoryGroupInvGrid");
        this.grids.push(invGrid);

        this.defaultNewRecordToExpect = {
            InventoryGroup: "",
            RecType: "R",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryGroup: "GlobalScope.TestToken~1.TestToken",
                    RecType: "S",
                },
                seekObject: {
                    InventoryGroup: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    InventoryGroup: "",
                    RecType: "S",
                },
                expectedErrorFields: ["InventoryGroup"],
            },
        ];
		
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: invGrid,
                recordToCreate: {
                    record: {
                        InventoryId: 1,
                    }
                }
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            InventoryGroup: this.newRecordsToCreate[0].record.InventoryGroup.toUpperCase(),
            RecType: this.newRecordsToCreate[0].record.RecType.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryRank extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryRank';
        this.moduleId = '963F5133-29CA-4675-9BE6-E5C47D38789A';
        this.moduleCaption = 'Inventory Rank';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryStatus';
        this.moduleId = 'E8E24D94-A07D-4388-9F2F-58FE028F24BB';
        this.moduleCaption = 'Inventory Status';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventoryType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventoryType';
        this.moduleId = 'D62E0D20-AFF4-46A7-A767-FF32F6EC4617';
        this.moduleCaption = 'Inventory Type';

        this.defaultNewRecordToExpect = {
            InventoryType: "",
            LowAvailabilityPercent: "",
            Rental: false,
            Sales: false,
            Parts: false,
            Wardrobe: false,
            Props: false,
            Sets: false,
            Transportation: false,
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryType: "GlobalScope.TestToken~1.TestToken",
                    LowAvailabilityPercent: TestUtils.randomIntegerBetween(1, 30).toString(),
                    Rental: true,
                    Sales: false,
                    Parts: true,
                    Wardrobe: false,
                    Props: false,
                    Sets: false,
                    Transportation: false,
                },
                seekObject: {
                    InventoryType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    InventoryType: "",
                    LowAvailabilityPercent: TestUtils.randomIntegerBetween(1, 30).toString(),
                    Rental: true,
                    Sales: false,
                    Parts: true,
                    Wardrobe: false,
                    Props: false,
                    Sets: false,
                    Transportation: false,
                },
                expectedErrorFields: ["InventoryType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            InventoryType: this.newRecordsToCreate[0].record.InventoryType.toUpperCase(),
            LowAvailabilityPercent: this.newRecordsToCreate[0].record.LowAvailabilityPercent.toUpperCase() + " %",
            Rental: this.newRecordsToCreate[0].record.Rental,
            Sales: this.newRecordsToCreate[0].record.Sales,
            Parts: this.newRecordsToCreate[0].record.Parts,
            Wardrobe: this.newRecordsToCreate[0].record.Wardrobe,
            Props: this.newRecordsToCreate[0].record.Props,
            Sets: this.newRecordsToCreate[0].record.Sets,
            Transportation: this.newRecordsToCreate[0].record.Transportation,
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PartsCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PartsCategory';
        this.moduleId = '4750DFBD-6C60-41EF-83FE-49C8340D6062';
        this.moduleCaption = 'Parts Category';
        let subCategoryGrid: GridBase = new GridBase("SubCategoryGrid");
        this.grids.push(subCategoryGrid);

        this.defaultNewRecordToExpect = {
            InventoryType: "",
            Category: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryTypeId: 1,
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["InventoryTypeId"],
            },
            {
                record: {
                    InventoryTypeId: 1,
                    Category: "",
                },
                expectedErrorFields: ["Category"],
            },
        ];
		
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: subCategoryGrid,
                recordToCreate: {
                    record: {
                        SubCategory: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];
		
        this.newRecordsToCreate[0].recordToExpect = {
            InventoryType: "|NOTEMPTY|",
            Category: this.newRecordsToCreate[0].record.Category.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RentalCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'RentalCategory';
        this.moduleId = '91079439-A188-4637-B733-A7EF9A9DFC22';
        this.moduleCaption = 'Rental Category';
        let subCategoryGrid: GridBase = new GridBase("SubCategoryGrid");
        this.grids.push(subCategoryGrid);

        this.defaultNewRecordToExpect = {
            InventoryType: "",
            Category: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryTypeId: 1,
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["InventoryTypeId"],
            },
            {
                record: {
                    InventoryTypeId: 1,
                    Category: "",
                },
                expectedErrorFields: ["Category"],
            },
        ];
		
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: subCategoryGrid,
                recordToCreate: {
                    record: {
                        SubCategory: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];
		
        this.newRecordsToCreate[0].recordToExpect = {
            InventoryType: "|NOTEMPTY|",
            Category: this.newRecordsToCreate[0].record.Category.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RetiredReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'RetiredReason';
        this.moduleId = '1DE1DD87-47FD-4079-B7D8-B5DE61FCB280';
        this.moduleCaption = 'Retired Reason';

        this.defaultNewRecordToExpect = {
            RetiredReason: "",
            ReasonType: "SOLD",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    RetiredReason: "GlobalScope.TestToken~1.TestToken",
                    ReasonType: "DONATED",
                },
                seekObject: {
                    RetiredReason: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    RetiredReason: "",
                    ReasonType: "DONATED",
                },
                expectedErrorFields: ["RetiredReason"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            RetiredReason: this.newRecordsToCreate[0].record.RetiredReason.toUpperCase(),
            ReasonType: this.newRecordsToCreate[0].record.ReasonType.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SalesCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SalesCategory';
        this.moduleId = '428619B5-ABDE-48C4-9B2F-CF6D2A3AC574';
        this.moduleCaption = 'Sales Category';
        let subCategoryGrid: GridBase = new GridBase("SubCategoryGrid");
        this.grids.push(subCategoryGrid);

        this.defaultNewRecordToExpect = {
            InventoryType: "",
            Category: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryTypeId: 1,
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["InventoryTypeId"],
            },
            {
                record: {
                    InventoryTypeId: 1,
                    Category: "",
                },
                expectedErrorFields: ["Category"],
            },
        ];
		
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: subCategoryGrid,
                recordToCreate: {
                    record: {
                        SubCategory: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];
		
        this.newRecordsToCreate[0].recordToExpect = {
            InventoryType: "|NOTEMPTY|",
            Category: this.newRecordsToCreate[0].record.Category.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Unit extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Unit';
        this.moduleId = 'EE9F1081-BD9F-4004-A0CA-3813E2360642';
        this.moduleCaption = 'Unit of Measure';

        this.defaultNewRecordToExpect = {
            Unit: "",
            Description: "",
            UnitType: "HOURLY",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Unit: "GlobalScope.TestToken~1.ShortTestToken",
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitType: "WEEKLY",
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Unit: "",
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitType: "WEEKLY",
                },
                expectedErrorFields: ["Unit"],
            },
            {
                record: {
                    Unit: "GlobalScope.TestToken~1.ShortTestToken",
                    Description: "",
                    UnitType: "WEEKLY",
                },
                expectedErrorFields: ["Description"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Unit: this.newRecordsToCreate[0].record.Unit.toUpperCase(),
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            UnitType: this.newRecordsToCreate[0].record.UnitType.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class UnretiredReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'UnretiredReason';
        this.moduleId = 'C8E7F77B-52BC-435C-9971-331CF99284A0';
        this.moduleCaption = 'Unretired Reason';

        this.defaultNewRecordToExpect = {
            UnretiredReason: "",
            ReasonType: "OTHER",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    UnretiredReason: "GlobalScope.TestToken~1.TestToken",
                    ReasonType: "OTHER",
                },
                seekObject: {
                    UnretiredReason: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    UnretiredReason: "",
                    ReasonType: "OTHER",
                },
                expectedErrorFields: ["UnretiredReason"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            UnretiredReason: this.newRecordsToCreate[0].record.UnretiredReason.toUpperCase(),
            ReasonType: this.newRecordsToCreate[0].record.ReasonType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WarehouseCatalog extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WarehouseCatalog';
        this.moduleId = '9045B118-A790-44FB-9867-3E8035EFEE69';
        this.moduleCaption = 'Warehouse Catalog';

        this.defaultNewRecordToExpect = {
            WarehouseCatalog: "",
            CatalogType: "RENTAL",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WarehouseCatalog: "GlobalScope.TestToken~1.TestToken",
                    CatalogType: "SALES",
                },
                seekObject: {
                    WarehouseCatalog: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WarehouseCatalog: "",
                    CatalogType: "SALES",
                },
                expectedErrorFields: ["WarehouseCatalog"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WarehouseCatalog: this.newRecordsToCreate[0].record.WarehouseCatalog.toUpperCase(),
            CatalogType: this.newRecordsToCreate[0].record.CatalogType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Crew extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Crew';
        this.moduleId = 'FF4C0AF2-0984-48FD-A108-68D93CB8FFE6';
        this.moduleCaption = 'Crew';
        let positionGrid: GridBase = new GridBase("CrewPositionGrid");
        let locationGrid: GridBase = new GridBase("CrewLocationGrid");
        let noteGrid: GridBase = new GridBase("ContactNoteGrid");
        this.grids.push(positionGrid);
        this.grids.push(locationGrid);
        this.grids.push(noteGrid);

        this.defaultNewRecordToExpect = {
            Salutation: "",
            FirstName: "",
            MiddleInitial: "",
            LastName: "",
            ActiveDate: "",
            ContactTitle: "",
            Address1: "",
            Address2: "",
            City: "",
            State: "",
            Country: "",
            ZipCode: "",
            OfficePhone: "",
            OfficeExtension: "",
            DirectPhone: "",
            DirectExtension: "",
            Fax: "",
            FaxExtension: "",
            Pager: "",
            PagerPin: "",
            MobilePhone: "",
            HomePhone: "",
            Email: "",
            WebAccess: false,
            LockAccount: false,
            WebAdministrator: false,
            ChangePasswordAtNextLogin: false,
            ExpirePassword: false,
            ExpireDays: "",
            PasswordLastUpdated: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Salutation: "",
                    FirstName: TestUtils.randomFirstName(),
                    MiddleInitial: TestUtils.randomAlphanumeric(1),
                    LastName: "GlobalScope.TestToken~1.TestToken",
                    //ActiveDate: TestUtils.randomRecentDate(100),
                    ContactTitleId: 1,
                    Address1: TestUtils.randomAddress1(),
                    Address2: TestUtils.randomAddress2(),
                    City: TestUtils.randomCity(),
                    State: TestUtils.randomStateCode(),
                    CountryId: 1,
                    ZipCode: TestUtils.randomZipCode5(),
                    OfficePhone: TestUtils.randomPhone(),
                    OfficeExtension: TestUtils.randomPhoneExtension(),
                    DirectPhone: TestUtils.randomPhone(),
                    DirectExtension: TestUtils.randomPhoneExtension(),
                    Fax: TestUtils.randomPhone(),
                    FaxExtension: TestUtils.randomPhoneExtension(),
                    Pager: TestUtils.randomPhone(),
                    PagerPin: TestUtils.randomPhoneExtension(),
                    MobilePhone: TestUtils.randomPhone(),
                    HomePhone: TestUtils.randomPhone(),
                    Email: TestUtils.randomEmail(),
                    WebAccess: false,
                    LockAccount: false,
                    WebAdministrator: true,
                    ChangePasswordAtNextLogin: false,
                    ExpirePassword: true,
                    ExpireDays: TestUtils.randomIntegerBetween(1, 60).toString(),
                },
                seekObject: {
                    LastName: "GlobalScope.TestToken~1.TestToken",
                }
            }
        ];


        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: positionGrid,
                recordToCreate: {
                    record: {
                        LaborTypeId: 1,
                        RateId: 1,
                    }
                }
            },
            {
                grid: locationGrid,
                recordToCreate: {
                    record: {
                        LocationId: 1,
                    }
                }
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        Description: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Salutation: this.newRecordsToCreate[0].record.Salutation.toUpperCase(),
            FirstName: this.newRecordsToCreate[0].record.FirstName.toUpperCase(),
            MiddleInitial: this.newRecordsToCreate[0].record.MiddleInitial.toUpperCase(),
            LastName: this.newRecordsToCreate[0].record.LastName.toUpperCase(),
            //ActiveDate: this.newRecordsToCreate[0].record.ActiveDate,
            ContactTitle: "|NOTEMPTY|",
            Address1: this.newRecordsToCreate[0].record.Address1.toUpperCase(),
            Address2: this.newRecordsToCreate[0].record.Address2.toUpperCase(),
            City: this.newRecordsToCreate[0].record.City.toUpperCase(),
            State: this.newRecordsToCreate[0].record.State.toUpperCase(),
            Country: "|NOTEMPTY|",
            ZipCode: this.newRecordsToCreate[0].record.ZipCode.toUpperCase(),
            OfficePhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.OfficePhone),
            OfficeExtension: this.newRecordsToCreate[0].record.OfficeExtension.toUpperCase(),
            DirectPhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.DirectPhone),
            DirectExtension: this.newRecordsToCreate[0].record.DirectExtension.toUpperCase(),
            Fax: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.Fax),
            FaxExtension: this.newRecordsToCreate[0].record.FaxExtension.toUpperCase(),
            Pager: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.Pager),
            PagerPin: this.newRecordsToCreate[0].record.PagerPin.toUpperCase(),
            MobilePhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.MobilePhone),
            HomePhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.HomePhone),
            Email: this.newRecordsToCreate[0].record.Email,
            WebAccess: this.newRecordsToCreate[0].record.WebAccess,
            LockAccount: this.newRecordsToCreate[0].record.LockAccount,
            WebAdministrator: this.newRecordsToCreate[0].record.WebAdministrator,
            ChangePasswordAtNextLogin: this.newRecordsToCreate[0].record.ChangePasswordAtNextLogin,
            ExpirePassword: this.newRecordsToCreate[0].record.ExpirePassword,
            ExpireDays: this.newRecordsToCreate[0].record.ExpireDays,
            Inactive: false,
        }




    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LaborRate extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LaborRate';
        this.moduleId = '650305EC-0A53-490B-A8FB-E1AF636DA89B';
        this.moduleCaption = 'Labor Rate';

        this.defaultNewRecordToExpect = {
            LaborType: "",
            Category: "",
            ICode: "",
            Description: "",
            Unit: "",
            RateType: "SINGLE",
            NonDiscountable: false,
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    LaborTypeId: 1,
                    CategoryId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                //attemptDuplicate: true,  (duplicate labor desciptions are allowed)
            },
            {
                record: {
                    LaborTypeId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["CategoryId"],
            },
            {
                record: {
                    LaborTypeId: 1,
                    CategoryId: 1,
                    Description: "",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["Description"],
            },
            {
                record: {
                    LaborTypeId: 1,
                    CategoryId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    NonDiscountable: true,
                },
                expectedErrorFields: ["UnitId"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            LaborType: "|NOTEMPTY|",
            Category: "|NOTEMPTY|",
            ICode: "|NOTEMPTY|",
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Unit: "|NOTEMPTY|",
            NonDiscountable: this.newRecordsToCreate[0].record.NonDiscountable,
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LaborPosition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LaborPosition';
        this.moduleId = '6D3B3D4F-2DD8-436F-8942-8FF68B73F3B6';
        this.moduleCaption = 'Position';


        this.defaultNewRecordToExpect = {
            LaborType: "",
            Category: "",
            ICode: "",
            Description: "",
            Unit: "",
            NonDiscountable: false,
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    LaborTypeId: 1,
                    CategoryId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
				attemptDuplicate: false,  // (duplicate crew position descriptions are allowed)
            },
            {
                record: {
                    LaborTypeId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["CategoryId"],
            },
            {
                record: {
                    LaborTypeId: 1,
                    CategoryId: 1,
                    Description: "",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["Description"],
            },
            {
                record: {
                    LaborTypeId: 1,
                    CategoryId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    NonDiscountable: true,
                },
                expectedErrorFields: ["UnitId"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            LaborType: "|NOTEMPTY|",
            Category: "|NOTEMPTY|",
            ICode: "|NOTEMPTY|",
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Unit: "|NOTEMPTY|",
            NonDiscountable: this.newRecordsToCreate[0].record.NonDiscountable,
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LaborType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LaborType';
        this.moduleId = '6757DFC2-360A-450A-B2E8-0B8232E87D6A';
        this.moduleCaption = 'Labor Type';


        this.defaultNewRecordToExpect = {
            LaborType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    LaborType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    LaborType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    LaborType: "",
                },
                expectedErrorFields: ["LaborType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            LaborType: this.newRecordsToCreate[0].record.LaborType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LaborCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LaborCategory';
        this.moduleId = '2A5190B9-B0E8-4B93-897B-C91FC4807FA6';
        this.moduleCaption = 'Labor Category';
        let subCategoryGrid: GridBase = new GridBase("SubCategoryGrid");
        this.grids.push(subCategoryGrid);


        this.defaultNewRecordToExpect = {
            LaborType: "",
            Category: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    LaborTypeId: 1,
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["LaborTypeId"],
            },
            {
                record: {
                    LaborTypeId: 1,
                    Category: "",
                },
                expectedErrorFields: ["Category"],
            },
        ];
		
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: subCategoryGrid,
                recordToCreate: {
                    record: {
                        SubCategory: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];
		
		
        this.newRecordsToCreate[0].recordToExpect = {
            LaborType: "|NOTEMPTY|",
            Category: this.newRecordsToCreate[0].record.Category.toUpperCase(),
            Inactive: false,
        }



    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CrewScheduleStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CrewScheduleStatus';
        this.moduleId = 'E4E11656-0783-4327-A374-161BCFDF0F24';
        this.moduleCaption = 'Crew Schedule Status';

        this.defaultNewRecordToExpect = {
            ScheduleStatus: "",
            StatusAction: "QUIKHOLD",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ScheduleStatus: "GlobalScope.TestToken~1.MediumTestToken",
                    StatusAction: "HOLD",
                },
                seekObject: {
                    ScheduleStatus: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ScheduleStatus: "",
                    StatusAction: "HOLD",
                },
                expectedErrorFields: ["ScheduleStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ScheduleStatus: this.newRecordsToCreate[0].record.ScheduleStatus.toUpperCase(),
            StatusAction: this.newRecordsToCreate[0].record.StatusAction.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CrewStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CrewStatus';
        this.moduleId = '73A6D9E3-E3BE-4B7A-BB3B-0AFE571C944E';
        this.moduleCaption = 'Crew Status';


        this.defaultNewRecordToExpect = {
            CrewStatus: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    CrewStatus: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    CrewStatus: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    CrewStatus: "",
                },
                expectedErrorFields: ["CrewStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            CrewStatus: this.newRecordsToCreate[0].record.CrewStatus.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MiscRate extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MiscRate';
        this.moduleId = '15B5AA83-4C3A-4136-B74B-574BDC0141B2';
        this.moduleCaption = 'Misc Rate';

        this.defaultNewRecordToExpect = {
            MiscType: "",
            Category: "",
            ICode: "",
            Description: "",
            Unit: "",
            RateType: "SINGLE",
            NonDiscountable: false,
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    MiscTypeId: 1,
                    CategoryId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                //attemptDuplicate: true,  (duplicate misc rate descriptions are allowed)
            },
            {
                record: {
                    MiscTypeId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["CategoryId"],
            },
            {
                record: {
                    MiscTypeId: 1,
                    CategoryId: 1,
                    Description: "",
                    UnitId: 1,
                    NonDiscountable: true,
                },
                expectedErrorFields: ["Description"],
            },
            {
                record: {
                    MiscTypeId: 1,
                    CategoryId: 1,
                    Description: "GlobalScope.TestToken~1.TestToken",
                    NonDiscountable: true,
                },
                expectedErrorFields: ["UnitId"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            MiscType: "|NOTEMPTY|",
            Category: "|NOTEMPTY|",
            ICode: "|NOTEMPTY|",
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Unit: "|NOTEMPTY|",
            NonDiscountable: this.newRecordsToCreate[0].record.NonDiscountable,
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MiscType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MiscType';
        this.moduleId = 'EAFEE5C7-84BB-419E-905A-3AE86E18DFAB';
        this.moduleCaption = 'Misc Type';

        this.defaultNewRecordToExpect = {
            MiscType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    MiscType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    MiscType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    MiscType: "",
                },
                expectedErrorFields: ["MiscType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            MiscType: this.newRecordsToCreate[0].record.MiscType.toUpperCase(),
            Inactive: false,
        }



    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MiscCategory extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MiscCategory';
        this.moduleId = 'D5318A2F-ECB8-498A-9D9A-0846F4B9E4DF';
        this.moduleCaption = 'Misc Category';
        let subCategoryGrid: GridBase = new GridBase("SubCategoryGrid");
        this.grids.push(subCategoryGrid);

        this.defaultNewRecordToExpect = {
            MiscType: "",
            Category: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    MiscTypeId: 1,
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Category: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["MiscTypeId"],
            },
            {
                record: {
                    MiscTypeId: 1,
                    Category: "",
                },
                expectedErrorFields: ["Category"],
            },
        ];
		
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: subCategoryGrid,
                recordToCreate: {
                    record: {
                        SubCategory: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];
		
        this.newRecordsToCreate[0].recordToExpect = {
            MiscType: "|NOTEMPTY|",
            Category: this.newRecordsToCreate[0].record.Category.toUpperCase(),
            Inactive: false,
        }



    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OfficeLocation extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OfficeLocation';
        this.moduleId = '8A8EE5CC-458E-4E4B-BA09-9C514588D3BD';
        this.moduleCaption = 'Office Location';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OrderType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OrderType';
        this.moduleId = 'CF3E22CB-F836-4277-9589-998B0BEC3500';
        this.moduleCaption = 'Order Type';
        let contactTitleGrid: GridBase = new GridBase("OrderTypeContactTitleGrid");
        let noteGrid: GridBase = new GridBase("OrderTypeNoteGrid");
        this.grids.push(contactTitleGrid);
        this.grids.push(noteGrid);

        this.defaultNewRecordToExpect = {
            OrderType: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    OrderType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    OrderType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    OrderType: "",
                },
                expectedErrorFields: ["OrderType"],
            },
        ];
		
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: contactTitleGrid,
                recordToCreate: {
                    record: {
                        ContactTitleId: 1,
                    }
                }
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        Description: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];
		
        this.newRecordsToCreate[0].recordToExpect = {
            OrderType: this.newRecordsToCreate[0].record.OrderType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DiscountReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DiscountReason';
        this.moduleId = 'CBBBFA51-DE2D-4A24-A50E-F7F4774016F6';
        this.moduleCaption = 'Discount Reason';

        this.defaultNewRecordToExpect = {
            DiscountReason: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    DiscountReason: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    DiscountReason: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    DiscountReason: "",
                },
                expectedErrorFields: ["DiscountReason"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            DiscountReason: this.newRecordsToCreate[0].record.DiscountReason.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MarketSegment extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MarketSegment';
        this.moduleId = '53B627BE-6AC8-4C1F-BEF4-E8B0A5422E14';
        this.moduleCaption = 'Market Segment';
        let jobGrid: GridBase = new GridBase("MarketSegmentJobGrid");
        this.grids.push(jobGrid);
		
        this.defaultNewRecordToExpect = {
            MarketType: "",
            MarketSegment: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    MarketTypeId: 1,
                    MarketSegment: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    MarketSegment: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    MarketSegment: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["MarketTypeId"],
            },
            {
                record: {
                    MarketTypeId: 1,
                    MarketSegment: "",
                },
                expectedErrorFields: ["MarketSegment"],
            },
        ];
		
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: jobGrid,
                recordToCreate: {
                    record: {
						MarketSegmentJob: "GlobalScope.TestToken~1.TestToken",
                    }
                }
            },
        ];
		
        this.newRecordsToCreate[0].recordToExpect = {
            MarketType: "|NOTEMPTY|",
            MarketSegment: this.newRecordsToCreate[0].record.MarketSegment.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class MarketType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'MarketType';
        this.moduleId = '77D7FD11-EBD2-40A2-A40D-C82D32528A01';
        this.moduleCaption = 'Market Type';

        this.defaultNewRecordToExpect = {
            MarketType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    MarketType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    MarketType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    MarketType: "",
                },
                expectedErrorFields: ["MarketType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            MarketType: this.newRecordsToCreate[0].record.MarketType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OrderSetNo extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OrderSetNo';
        this.moduleId = '4960D9A7-D1E0-4558-B571-DF1CE1BB8245';
        this.moduleCaption = 'Order Set No.';

        this.defaultNewRecordToExpect = {
            OrderSetNo: "",
            Description: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    OrderSetNo: "GlobalScope.TestToken~1.MediumTestToken",
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                //attemptDuplicate: true,
            },
            {
                record: {
                    OrderSetNo: "",
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["OrderSetNo"],
            },
            {
                record: {
                    OrderSetNo: "GlobalScope.TestToken~1.MediumTestToken",
                    Description: "",
                },
                expectedErrorFields: ["Description"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            OrderSetNo: this.newRecordsToCreate[0].record.OrderSetNo.toUpperCase(),
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OrderLocation extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OrderLocation';
        this.moduleId = 'CF58D8C9-95EE-4617-97B9-CAFE200719CC';
        this.moduleCaption = 'Order Location';

        this.defaultNewRecordToExpect = {
            Location: "",
            Description: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Location: "GlobalScope.User~ME.OfficeLocation",
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Location: "",
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["Location"],
            },
            {
                record: {
                    Location: "GlobalScope.User~ME.OfficeLocation",
                    Description: "",
                },
                expectedErrorFields: ["Description"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PaymentTerms extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PaymentTerms';
        this.moduleId = '44FD799A-1572-4B34-9943-D94FFBEF89D4';
        this.moduleCaption = 'Payment Terms';

        this.defaultNewRecordToExpect = {
            PaymentTerms: "",
            PaymentTermsCode: "",
            DueInDays: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PaymentTerms: "GlobalScope.TestToken~1.MediumTestToken",
                    PaymentTermsCode: "GlobalScope.TestToken~1.MediumTestToken",
                    DueInDays: TestUtils.randomIntegerBetween(1, 100).toString(),
                },
                seekObject: {
                    PaymentTerms: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PaymentTerms: "",
                    PaymentTermsCode: "GlobalScope.TestToken~1.MediumTestToken",
                    DueInDays: TestUtils.randomIntegerBetween(1, 100).toString(),
                },
                expectedErrorFields: ["PaymentTerms"],
            },
            {
                record: {
                    PaymentTerms: "GlobalScope.TestToken~1.MediumTestToken",
                    PaymentTermsCode: "",
                    DueInDays: TestUtils.randomIntegerBetween(1, 100).toString(),
                },
                expectedErrorFields: ["PaymentTermsCode"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PaymentTerms: this.newRecordsToCreate[0].record.PaymentTerms.toUpperCase(),
            PaymentTermsCode: this.newRecordsToCreate[0].record.PaymentTermsCode.toUpperCase(),
            DueInDays: this.newRecordsToCreate[0].record.DueInDays,
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PaymentType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PaymentType';
        this.moduleId = 'E88C4957-3A3E-4258-8677-EB6FB61F9BA3';
        this.moduleCaption = 'Payment Type';


        this.defaultNewRecordToExpect = {
            PaymentType: "",
            ShortName: "",
            PaymentTypeType: "CASH",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PaymentType: "GlobalScope.TestToken~1.TestToken",
                    ShortName: "GlobalScope.TestToken~1.MediumTestToken",
                    PaymentTypeType: "CHECK",
                },
                seekObject: {
                    PaymentType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PaymentType: "",
                    ShortName: "GlobalScope.TestToken~1.MediumTestToken",
                    PaymentTypeType: "CHECK",
                },
                expectedErrorFields: ["PaymentType"],
            },
            {
                record: {
                    PaymentType: "GlobalScope.TestToken~1.TestToken",
                    ShortName: "",
                    PaymentTypeType: "CHECK",
                },
                expectedErrorFields: ["ShortName"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PaymentType: this.newRecordsToCreate[0].record.PaymentType.toUpperCase(),
            ShortName: this.newRecordsToCreate[0].record.ShortName.toUpperCase(),
            PaymentTypeType: this.newRecordsToCreate[0].record.PaymentTypeType.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POApprovalStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POApprovalStatus';
        this.moduleId = '22EF1328-FBB1-44D0-A965-4E96675B96CD';
        this.moduleCaption = 'PO Approval Status';


        this.defaultNewRecordToExpect = {
            PoApprovalStatus: "",
            PoApprovalStatusType: "SUBMITTED_FOR_APPROVAL",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PoApprovalStatus: "GlobalScope.TestToken~1.TestToken",
                    PoApprovalStatusType: "SECOND_APPROVAL",
                },
                seekObject: {
                    PoApprovalStatus: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PoApprovalStatus: "",
                    PoApprovalStatusType: "SECOND_APPROVAL",
                },
                expectedErrorFields: ["PoApprovalStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PoApprovalStatus: this.newRecordsToCreate[0].record.PoApprovalStatus.toUpperCase(),
            PoApprovalStatusType: this.newRecordsToCreate[0].record.PoApprovalStatusType.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POApproverRole extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POApproverRole';
        this.moduleId = '992314B6-A24F-468C-A8B6-5EAC8F14BE16';
        this.moduleCaption = 'PO Approver Role';

        this.defaultNewRecordToExpect = {
            PoApproverRole: "",
            PoApproverType: "1",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PoApproverRole: "GlobalScope.TestToken~1.TestToken",
                    PoApproverType: "2",
                },
                seekObject: {
                    PoApproverRole: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PoApproverRole: "",
                    PoApproverType: "2",
                },
                expectedErrorFields: ["PoApproverRole"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PoApproverRole: this.newRecordsToCreate[0].record.PoApproverRole.toUpperCase(),
            PoApproverType: this.newRecordsToCreate[0].record.PoApproverType.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POClassification extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POClassification';
        this.moduleId = '58ef51c5-a97b-43c6-9298-08b064a84a48';
        this.moduleCaption = 'PO Classification';

        this.defaultNewRecordToExpect = {
            PoClassification: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PoClassification: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    PoClassification: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PoClassification: "",
                },
                expectedErrorFields: ["PoClassification"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PoClassification: this.newRecordsToCreate[0].record.PoClassification.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POImportance extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POImportance';
        this.moduleId = '82BF3B3E-0EF8-4A6E-8577-33F23EA9C4FB';
        this.moduleCaption = 'PO Importance';

        this.defaultNewRecordToExpect = {
            PoImportance: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PoImportance: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    PoImportance: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PoImportance: "",
                },
                expectedErrorFields: ["PoImportance"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PoImportance: this.newRecordsToCreate[0].record.PoImportance.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PORejectReason extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PORejectReason';
        this.moduleId = '2C6910A8-51BC-421E-898F-C23938B624B4';
        this.moduleCaption = 'PO Reject Reason';

        this.defaultNewRecordToExpect = {
            PoRejectReason: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PoRejectReason: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    PoRejectReason: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PoRejectReason: "",
                },
                expectedErrorFields: ["PoRejectReason"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PoRejectReason: this.newRecordsToCreate[0].record.PoRejectReason.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POType';
        this.moduleId = 'BB8D68B3-012A-4B05-BE7F-844EB5C96896';
        this.moduleCaption = 'PO Type';


        this.defaultNewRecordToExpect = {
            PoType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PoType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    PoType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PoType: "",
                },
                expectedErrorFields: ["PoType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PoType: this.newRecordsToCreate[0].record.PoType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class POApprover extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'POApprover';
        this.moduleId = '237B99DC-252D-4197-AB4A-01E795076447';
        this.moduleCaption = 'PO Approver';


        this.defaultNewRecordToExpect = {
            Location: "",
            Department: "",
            UserName: "",
            AppRole: "",
            IsBackup: false,
            Limit: false,
            LimitRental: "$ 0.00",
            LimitSales: "$ 0.00",
            LimitParts: "$ 0.00",
            LimitMisc: "$ 0.00",
            LimitLabor: "$ 0.00",
            LimitVehicle: "$ 0.00",
            LimitSubRent: "$ 0.00",
            LimitSubSale: "$ 0.00",
            LimitSubMisc: "$ 0.00",
            LimitSubLabor: "$ 0.00",
            LimitSubVehicle: "$ 0.00",
            LimitRepair: "$ 0.00",
        }
        //this.newRecordsToCreate = [
        //    {
        //        record: {
        //            PoType: "GlobalScope.TestToken~1.TestToken",
        //        },
        //        seekObject: {
        //            PoType: "GlobalScope.TestToken~1.TestToken",
        //        }
        //    }
        //];
        //this.newRecordsToCreate[0].recordToExpect = {
        //    PoType: this.newRecordsToCreate[0].record.PoType.toUpperCase(),
        //    Inactive: false,
        //}


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VendorInvoiceApprover extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VendorInvoiceApprover';
        this.moduleId = '4E34DB8F-84C0-4810-B49E-AE6640DD8E4B';
        this.moduleCaption = 'Vendor Invoice Approver';
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class FormDesign extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'FormDesign';
        this.moduleId = '4DFEC75D-C33A-4358-9EF1-4D1F5F9C5D73';
        this.moduleCaption = 'Form Design';
        this.canNew = false;
        this.canView = false;
        this.canEdit = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PresentationLayer extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PresentationLayer';
        this.moduleId = 'BBEF0AFD-B46A-46B0-8046-113834736060';
        this.moduleCaption = 'Presentation Layer';
        //let activityGrid: GridBase = new GridBase("PresentationLayerActivityGrid");
        let activityOverrideGrid: GridBase = new GridBase("PresentationLayerActivityOverrideGrid");
        //this.grids.push(activityGrid);
        this.grids.push(activityOverrideGrid);

        this.defaultNewRecordToExpect = {
            PresentationLayer: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PresentationLayer: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    PresentationLayer: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PresentationLayer: "",
                },
                expectedErrorFields: ["PresentationLayer"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            //{
            //    grid: activityGrid,
            //    recordToCreate: {
            //        record: {
            //            Activity: "GlobalScope.TestToken~1.TestToken",
            //        }
            //    }
            //},
            {
                grid: activityOverrideGrid,
                recordToCreate: {
                    record: {
                        PresentationLayerActivityId: 1,
                        MasterId: 1,
                    }
                }
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            PresentationLayer: this.newRecordsToCreate[0].record.PresentationLayer.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectAsBuild extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectAsBuild';
        this.moduleId = 'A3BFF1F7-0951-4F3A-A6DE-1A62BEDF45E6';
        this.moduleCaption = 'As Build';

        this.defaultNewRecordToExpect = {
            ProjectAsBuild: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ProjectAsBuild: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    ProjectAsBuild: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ProjectAsBuild: "",
                },
                expectedErrorFields: ["ProjectAsBuild"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ProjectAsBuild: this.newRecordsToCreate[0].record.ProjectAsBuild.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectCommissioning extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectCommissioning';
        this.moduleId = '0EFE9BBA-0685-4046-A7D6-EC3D34AD01AA';
        this.moduleCaption = 'Commissioning';

        this.defaultNewRecordToExpect = {
            ProjectCommissioning: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ProjectCommissioning: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    ProjectCommissioning: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ProjectCommissioning: "",
                },
                expectedErrorFields: ["ProjectCommissioning"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ProjectCommissioning: this.newRecordsToCreate[0].record.ProjectCommissioning.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectDeposit extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectDeposit';
        this.moduleId = '24E6F284-7457-4E75-B77D-25B3A6BE6A4D';
        this.moduleCaption = 'Deposit';


        this.defaultNewRecordToExpect = {
            ProjectDeposit: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ProjectDeposit: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    ProjectDeposit: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ProjectDeposit: "",
                },
                expectedErrorFields: ["ProjectDeposit"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ProjectDeposit: this.newRecordsToCreate[0].record.ProjectDeposit.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectDrawings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectDrawings';
        this.moduleId = '7486859D-243F-4817-8177-6DCB81392C36';
        this.moduleCaption = 'Drawings';


        this.defaultNewRecordToExpect = {
            ProjectDrawings: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ProjectDrawings: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    ProjectDrawings: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ProjectDrawings: "",
                },
                expectedErrorFields: ["ProjectDrawings"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ProjectDrawings: this.newRecordsToCreate[0].record.ProjectDrawings.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectDropShipItems extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectDropShipItems';
        this.moduleId = '20CD34E6-7E35-4EAF-B4D3-587870412C85';
        this.moduleCaption = 'Drop Ship Items';


        this.defaultNewRecordToExpect = {
            ProjectDropShipItems: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ProjectDropShipItems: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    ProjectDropShipItems: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ProjectDropShipItems: "",
                },
                expectedErrorFields: ["ProjectDropShipItems"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ProjectDropShipItems: this.newRecordsToCreate[0].record.ProjectDropShipItems.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ProjectItemsOrdered extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ProjectItemsOrdered';
        this.moduleId = '25507FAD-E140-4A19-8FED-2C381DA653D9';
        this.moduleCaption = 'Items Ordered';


        this.defaultNewRecordToExpect = {
            ProjectItemsOrdered: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ProjectItemsOrdered: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    ProjectItemsOrdered: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ProjectItemsOrdered: "",
                },
                expectedErrorFields: ["ProjectItemsOrdered"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ProjectItemsOrdered: this.newRecordsToCreate[0].record.ProjectItemsOrdered.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PropsCondition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PropsCondition';
        this.moduleId = '86C769E8-F8E6-4C59-BC0B-8F2D563C698F';
        this.moduleCaption = 'Props Condition';


        this.defaultNewRecordToExpect = {
            PropsCondition: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    PropsCondition: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    PropsCondition: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    PropsCondition: "",
                },
                expectedErrorFields: ["PropsCondition"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            PropsCondition: this.newRecordsToCreate[0].record.PropsCondition.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Region extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Region';
        this.moduleId = 'A50C7F59-AF91-44D5-8253-5C4A4D5DFB8B';
        this.moduleCaption = 'Region';


        this.defaultNewRecordToExpect = {
            Region: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Region: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Region: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Region: "",
                },
                expectedErrorFields: ["Region"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Region: this.newRecordsToCreate[0].record.Region.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RepairItemStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'RepairItemStatus';
        this.moduleId = 'D952672A-DCF6-47C8-9B99-47561C79B3F8';
        this.moduleCaption = 'Repair Item Status';


        this.defaultNewRecordToExpect = {
            RepairItemStatus: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    RepairItemStatus: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    RepairItemStatus: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    RepairItemStatus: "",
                },
                expectedErrorFields: ["RepairItemStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            RepairItemStatus: this.newRecordsToCreate[0].record.RepairItemStatus.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SetCondition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SetCondition';
        this.moduleId = '0FFC8940-C060-49E4-BC24-688E25250C5F';
        this.moduleCaption = 'Set Condition';

        this.defaultNewRecordToExpect = {
            SetCondition: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    SetCondition: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    SetCondition: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    SetCondition: "",
                },
                expectedErrorFields: ["SetCondition"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            SetCondition: this.newRecordsToCreate[0].record.SetCondition.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SetSurface extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SetSurface';
        this.moduleId = 'EC55E743-0CB1-4A74-9D10-6C4C6045AAAB';
        this.moduleCaption = 'Set Surface';

        this.defaultNewRecordToExpect = {
            SetSurface: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    SetSurface: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    SetSurface: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    SetSurface: "",
                },
                expectedErrorFields: ["SetSurface"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            SetSurface: this.newRecordsToCreate[0].record.SetSurface.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SetOpening extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SetOpening';
        this.moduleId = '15E52CA3-475D-4BDA-B940-525E5EEAF8CD';
        this.moduleCaption = 'Set Opening';


        this.defaultNewRecordToExpect = {
            SetOpening: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    SetOpening: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    SetOpening: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    SetOpening: "",
                },
                expectedErrorFields: ["SetOpening"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            SetOpening: this.newRecordsToCreate[0].record.SetOpening.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WallDescription extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WallDescription';
        this.moduleId = 'F34F1A9B-53C6-447C-B52B-7FF5BAE38AB5';
        this.moduleCaption = 'Wall Description';


        this.defaultNewRecordToExpect = {
            WallDescription: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WallDescription: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WallDescription: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WallDescription: "",
                },
                expectedErrorFields: ["WallDescription"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WallDescription: this.newRecordsToCreate[0].record.WallDescription.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WallType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WallType';
        this.moduleId = '4C9D2D20-D129-461D-9589-ABC896DD9BC6';
        this.moduleCaption = 'Wall Type';


        this.defaultNewRecordToExpect = {
            WallType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WallType: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WallType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WallType: "",
                },
                expectedErrorFields: ["WallType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WallType: this.newRecordsToCreate[0].record.WallType.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ShipVia extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ShipVia';
        this.moduleId = 'F9E01296-D240-4E16-B267-898787B29509';
        this.moduleCaption = 'Ship Via';


        this.defaultNewRecordToExpect = {
            Vendor: "",
            ShipVia: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    VendorId: 1,
                    ShipVia: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    ShipVia: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ShipVia: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["VendorId"],
            },
            {
                record: {
                    VendorId: 1,
                    ShipVia: "",
                },
                expectedErrorFields: ["ShipVia"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Vendor: "|NOTEMPTY|",
            ShipVia: this.newRecordsToCreate[0].record.ShipVia.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Source extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Source';
        this.moduleId = '6D6165D1-51F2-4616-A67C-DCC803B549AF';
        this.moduleCaption = 'Source';

        this.defaultNewRecordToExpect = {
            Description: "",
            SourceType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                    SourceType: "FTP",
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Description: "",
                    SourceType: "FTP",
                },
                expectedErrorFields: ["Description"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            SourceType: "FTP",
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class AvailabilitySettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'AvailabilitySettings';
        this.moduleId = 'E1C62A69-05B0-4657-AAF0-703F8BDEBC5C';
        this.moduleCaption = 'Availability Settings';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class DefaultSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'DefaultSettings';
        this.moduleId = '3F551271-C157-44F6-B06A-8F835F7A2084';
        this.moduleCaption = 'Default Settings';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class EmailSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'EmailSettings';
        this.moduleId = '8C9613E0-E7E5-4242-9DF6-4F57F59CE2B9';
        this.moduleCaption = 'Email Settings';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class InventorySettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'InventorySettings';
        this.moduleId = '5C7D5BFA-3EA3-42C5-B90A-27A9EA5EA9FC';
        this.moduleCaption = 'Inventory Settings';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LogoSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LogoSettings';
        this.moduleId = 'B3ADDF49-64EB-4740-AB41-4327E6E56242';
        this.moduleCaption = 'Logo Settings';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SystemSettings extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SystemSettings';
        this.moduleId = '6EFC3A8C-E83E-4FE3-BAC8-8E04EBD7F204';
        this.moduleCaption = 'System Settings';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TaxOption extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TaxOption';
        this.moduleId = '5895CA39-5EF8-405B-9E97-2FEB83939EE5';
        this.moduleCaption = 'Tax Option';


        this.defaultNewRecordToExpect = {
            TaxOption: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    TaxOption: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    TaxOption: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    TaxOption: "",
                },
                expectedErrorFields: ["TaxOption"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            TaxOption: this.newRecordsToCreate[0].record.TaxOption.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Template extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Template';
        this.moduleId = 'BDDB1439-F128-4AB7-9657-B1CDFFA12721';
        this.moduleCaption = 'Template';

        this.defaultNewRecordToExpect = {
            Description: "",
            Warehouse: "GlobalScope.User~ME.Warehouse",
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                    Rental: true,
                    Sales: true,
                    Labor: true,
                    Miscellaneous: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Description: "",
                },
                expectedErrorFields: ["Description"],
            },
        ];
        
		this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Warehouse: "GlobalScope.User~ME.Warehouse",
            RateType: "|NOTEMPTY|",
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class UserStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'UserStatus';
        this.moduleId = 'E19916C6-A844-4BD1-A338-FAB0F278122C';
        this.moduleCaption = 'User Status';


        this.defaultNewRecordToExpect = {
            UserStatus: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    UserStatus: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    UserStatus: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    UserStatus: "",
                },
                expectedErrorFields: ["UserStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            UserStatus: this.newRecordsToCreate[0].record.UserStatus.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Sound extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Sound';
        this.moduleId = '29C327DD-7734-4039-9CE2-B25D7CD6F9CB';
        this.moduleCaption = 'Sounds';


        this.defaultNewRecordToExpect = {
            Sound: "",
        }
        this.newRecordsToCreate = [
            {
                record: {
                    Sound: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    Sound: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    Sound: "",
                },
                expectedErrorFields: ["Sound"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            Sound: this.newRecordsToCreate[0].record.Sound.toUpperCase(),
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class LicenseClass extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'LicenseClass';
        this.moduleId = '422F777F-B57F-43DF-8485-F12F3F7BF662';
        this.moduleCaption = 'License Class';


        this.defaultNewRecordToExpect = {
            LicenseClass: "",
            Description: "",
            Regulated: false,
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    LicenseClass: "GlobalScope.TestToken~1.ShortTestToken",
                    Description: "GlobalScope.TestToken~1.TestToken",
                    Regulated: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    LicenseClass: "",
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
                expectedErrorFields: ["LicenseClass"],
            },
            {
                record: {
                    LicenseClass: "GlobalScope.TestToken~1.ShortTestToken",
                    Description: "",
                },
                expectedErrorFields: ["Description"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            LicenseClass: this.newRecordsToCreate[0].record.LicenseClass.toUpperCase(),
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            Regulated: this.newRecordsToCreate[0].record.Regulated,
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleColor extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleColor';
        this.moduleId = 'F7A34B70-509A-422F-BFD1-5F30BE2C8186';
        this.moduleCaption = 'Vehicle Color';


        this.defaultNewRecordToExpect = {
            VehicleColor: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    VehicleColor: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    VehicleColor: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    VehicleColor: "",
                },
                expectedErrorFields: ["VehicleColor"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            VehicleColor: this.newRecordsToCreate[0].record.VehicleColor.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleFuelType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleFuelType';
        this.moduleId = 'D9140FB3-084D-4615-8E7A-95731670E682';
        this.moduleCaption = 'Vehicle Fuel Type';


        this.defaultNewRecordToExpect = {
            VehicleFuelType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    VehicleFuelType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    VehicleFuelType: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    VehicleFuelType: "",
                },
                expectedErrorFields: ["VehicleFuelType"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            VehicleFuelType: this.newRecordsToCreate[0].record.VehicleFuelType.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleMake extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleMake';
        this.moduleId = '299DECA3-B427-49ED-B6AC-2E11F6AA1C4D';
        this.moduleCaption = 'Vehicle Make';
        let vehicleMakeGrid: GridBase = new GridBase("VehicleMakeModelGrid");
        this.grids.push(vehicleMakeGrid);

        this.defaultNewRecordToExpect = {
            VehicleMake: "",
            Inactive: false,
        };
		
        this.newRecordsToCreate = [
            {
                record: {
                    VehicleMake: "GlobalScope.TestToken~1.MediumTestToken",
                },
                seekObject: {
                    VehicleMake: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    VehicleMake: "",
                },
                expectedErrorFields: ["VehicleMake"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: vehicleMakeGrid,
                recordToCreate: {
                    record: {
                        VehicleModel: "GlobalScope.TestToken~1.MediumTestToken",
                    }
                }
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            VehicleMake: this.newRecordsToCreate[0].record.VehicleMake.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleScheduleStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleScheduleStatus';
        this.moduleId = 'A001473B-1FB4-4E85-8093-37A92057CD93';
        this.moduleCaption = 'Vehicle Schedule Status';

        this.defaultNewRecordToExpect = {
            ScheduleStatus: "",
            StatusAction: "QUIKHOLD",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    ScheduleStatus: "GlobalScope.TestToken~1.MediumTestToken",
                    StatusAction: "HOLD",
                },
                seekObject: {
                    ScheduleStatus: "GlobalScope.TestToken~1.MediumTestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    ScheduleStatus: "",
                    StatusAction: "HOLD",
                },
                expectedErrorFields: ["ScheduleStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            ScheduleStatus: this.newRecordsToCreate[0].record.ScheduleStatus.toUpperCase(),
            StatusAction: this.newRecordsToCreate[0].record.StatusAction.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleStatus';
        this.moduleId = 'FB12061D-E6AF-4C09-95A0-8647930C289A';
        this.moduleCaption = 'Vehicle Status';


        this.defaultNewRecordToExpect = {
            VehicleStatus: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    VehicleStatus: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    VehicleStatus: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    VehicleStatus: "",
                },
                expectedErrorFields: ["VehicleStatus"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            VehicleStatus: this.newRecordsToCreate[0].record.VehicleStatus.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VehicleType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VehicleType';
        this.moduleId = '60187072-8990-40BA-8D80-43B451E5BC8B';
        this.moduleCaption = 'Vehicle Type';

        this.defaultNewRecordToExpect = {
            InventoryType: "",
            VehicleType: "",
            LicenseClass: "",
            Unit: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    InventoryTypeId: 1,
                    VehicleType: "GlobalScope.TestToken~1.TestToken",
                    LicenseClassId: 1,
                    UnitId: 1,
                },
                seekObject: {
                    VehicleType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    VehicleType: "GlobalScope.TestToken~1.TestToken",
                    LicenseClassId: 1,
                    UnitId: 1,
                },
                expectedErrorFields: ["InventoryTypeId"],
            },
            {
                record: {
                    InventoryTypeId: 1,
                    VehicleType: "",
                    LicenseClassId: 1,
                    UnitId: 1,
                },
                expectedErrorFields: ["VehicleType"],
            },
            {
                record: {
                    InventoryTypeId: 1,
                    VehicleType: "GlobalScope.TestToken~1.TestToken",
                    UnitId: 1,
                },
                expectedErrorFields: ["LicenseClassId"],
            },
            {
                record: {
                    InventoryTypeId: 1,
                    VehicleType: "GlobalScope.TestToken~1.TestToken",
                    LicenseClassId: 1,
                },
                expectedErrorFields: ["UnitId"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            InventoryType: "|NOTEMPTY|",
            VehicleType: this.newRecordsToCreate[0].record.VehicleType.toUpperCase(),
            LicenseClass: "|NOTEMPTY|",
            Unit: "|NOTEMPTY|",
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class OrganizationType extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'OrganizationType';
        this.moduleId = 'fe3a764c-ab55-4ce5-8d7f-bfc86f174c11';
        this.moduleCaption = 'Organization Type';


        this.defaultNewRecordToExpect = {
            OrganizationType: "",
            OrganizationTypeCode: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    OrganizationType: "GlobalScope.TestToken~1.TestToken",
                    OrganizationTypeCode: "GlobalScope.TestToken~1.ShortTestToken",
                },
                seekObject: {
                    OrganizationType: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    OrganizationType: "",
                    OrganizationTypeCode: "GlobalScope.TestToken~1.ShortTestToken",
                },
                expectedErrorFields: ["OrganizationType"],
            },
            {
                record: {
                    OrganizationType: "GlobalScope.TestToken~1.TestToken",
                    OrganizationTypeCode: "",
                },
                expectedErrorFields: ["OrganizationTypeCode"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            OrganizationType: this.newRecordsToCreate[0].record.OrganizationType.toUpperCase(),
            OrganizationTypeCode: this.newRecordsToCreate[0].record.OrganizationTypeCode.toUpperCase(),
            Inactive: false,
        }


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VendorCatalog extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VendorCatalog';
        this.moduleId = 'BDA5E2DC-0FD2-4227-B80F-8414F3F912B8';
        this.moduleCaption = 'Vendor Catalog';


        this.defaultNewRecordToExpect = {
            VendorCatalog: "",
            CatalogType: "SALES",
            InventoryType: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    VendorCatalog: "GlobalScope.TestToken~1.TestToken",
                    CatalogType: "RENTAL",
                    InventoryTypeId: 1,
                },
                seekObject: {
                    VendorCatalog: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    VendorCatalog: "",
                    CatalogType: "RENTAL",
                    InventoryTypeId: 1,
                },
                expectedErrorFields: ["VendorCatalog"],
            },
            {
                record: {
                    VendorCatalog: "GlobalScope.TestToken~1.TestToken",
                    CatalogType: "RENTAL",
                },
                expectedErrorFields: ["InventoryTypeId"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            VendorCatalog: this.newRecordsToCreate[0].record.VendorCatalog.toUpperCase(),
            CatalogType: this.newRecordsToCreate[0].record.CatalogType.toUpperCase(),
            InventoryType: "|NOTEMPTY|",
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class VendorClass extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'VendorClass';
        this.moduleId = '8B2C9EE3-AE87-483F-A651-8BA633E6C439';
        this.moduleCaption = 'Vendor Class';


        this.defaultNewRecordToExpect = {
            VendorClass: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    VendorClass: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    VendorClass: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    VendorClass: "",
                },
                expectedErrorFields: ["VendorClass"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            VendorClass: this.newRecordsToCreate[0].record.VendorClass.toUpperCase(),
            Inactive: false,
        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SapVendorInvoiceStatus extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SapVendorInvoiceStatus';
        this.moduleId = '1C8E14A3-73A8-4BB6-9B33-65D827B3ED0C';
        this.moduleCaption = 'SAP Vendor Invoice Status';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeCare extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeCare';
        this.moduleId = 'BE6E4F7C-5D81-4437-A343-8F4933DD6545';
        this.moduleCaption = 'Wardrobe Care';


        this.defaultNewRecordToExpect = {
            WardrobeCare: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobeCare: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobeCare: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobeCare: "",
                },
                expectedErrorFields: ["WardrobeCare"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobeCare: this.newRecordsToCreate[0].record.WardrobeCare.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeColor extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeColor';
        this.moduleId = '32238B26-3635-4637-AFE0-0D5B12CAAEE4';
        this.moduleCaption = 'Wardrobe Color';

        this.defaultNewRecordToExpect = {
            WardrobeColor: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobeColor: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobeColor: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobeColor: "",
                },
                expectedErrorFields: ["WardrobeColor"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobeColor: this.newRecordsToCreate[0].record.WardrobeColor.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeCondition extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeCondition';
        this.moduleId = '4EEBE71C-139A-4D09-B589-59DA576C83FD';
        this.moduleCaption = 'Wardrobe Condition';

        this.defaultNewRecordToExpect = {
            WardrobeCondition: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobeCondition: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobeCondition: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobeCondition: "",
                },
                expectedErrorFields: ["WardrobeCondition"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobeCondition: this.newRecordsToCreate[0].record.WardrobeCondition.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeGender extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeGender';
        this.moduleId = '28574D17-D2FF-41A0-8117-5F252013E7B1';
        this.moduleCaption = 'Wardrobe Gender';

        this.defaultNewRecordToExpect = {
            WardrobeGender: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobeGender: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobeGender: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobeGender: "",
                },
                expectedErrorFields: ["WardrobeGender"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobeGender: this.newRecordsToCreate[0].record.WardrobeGender.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeLabel extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeLabel';
        this.moduleId = '9C1B5157-C983-44EE-817F-171B4448401A';
        this.moduleCaption = 'Wardrobe Label';

        this.defaultNewRecordToExpect = {
            WardrobeLabel: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobeLabel: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobeLabel: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobeLabel: "",
                },
                expectedErrorFields: ["WardrobeLabel"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobeLabel: this.newRecordsToCreate[0].record.WardrobeLabel.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeMaterial extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeMaterial';
        this.moduleId = '25895901-C700-4618-9ADA-00A7CB4B83B9';
        this.moduleCaption = 'Wardrobe Material';

        this.defaultNewRecordToExpect = {
            WardrobeMaterial: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobeMaterial: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobeMaterial: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobeMaterial: "",
                },
                expectedErrorFields: ["WardrobeMaterial"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobeMaterial: this.newRecordsToCreate[0].record.WardrobeMaterial.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobePattern extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobePattern';
        this.moduleId = '2BE7072A-5588-4205-8DCD-0FFE6F0C48F7';
        this.moduleCaption = 'Wardrobe Pattern';

        this.defaultNewRecordToExpect = {
            WardrobePattern: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobePattern: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobePattern: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobePattern: "",
                },
                expectedErrorFields: ["WardrobePattern"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobePattern: this.newRecordsToCreate[0].record.WardrobePattern.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobePeriod extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobePeriod';
        this.moduleId = 'BF51623D-ABA6-471A-BC00-4729067C64CF';
        this.moduleCaption = 'Wardrobe Period';

        this.defaultNewRecordToExpect = {
            WardrobePeriod: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobePeriod: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobePeriod: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobePeriod: "",
                },
                expectedErrorFields: ["WardrobePeriod"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobePeriod: this.newRecordsToCreate[0].record.WardrobePeriod.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WardrobeSource extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WardrobeSource';
        this.moduleId = '6709D1A1-3319-435C-BF0E-15D2602575B0';
        this.moduleCaption = 'Wardrobe Source';

        this.defaultNewRecordToExpect = {
            WardrobeSource: "",
            Inactive: false,
        }
        this.newRecordsToCreate = [
            {
                record: {
                    WardrobeSource: "GlobalScope.TestToken~1.TestToken",
                },
                seekObject: {
                    WardrobeSource: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
            },
            {
                record: {
                    WardrobeSource: "",
                },
                expectedErrorFields: ["WardrobeSource"],
            },
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            WardrobeSource: this.newRecordsToCreate[0].record.WardrobeSource.toUpperCase(),
            Inactive: false,
        }

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Warehouse extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Warehouse';
        this.moduleId = '931D3E75-68CB-4280-B12F-9A955444AA0C';
        this.moduleCaption = 'Warehouse';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Widget extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Widget';
        this.moduleId = '0CAF7264-D1FB-46EC-96B9-68D242985812';
        this.moduleCaption = 'Widget';
        this.canNew = false;
        this.canDelete = false;
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class WorkWeek extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'WorkWeek';
        this.moduleId = 'AF91AE34-ADED-4A5A-BD03-113ED817F575';
        this.moduleCaption = 'Work Week';
    }
    //---------------------------------------------------------------------------------------
}

