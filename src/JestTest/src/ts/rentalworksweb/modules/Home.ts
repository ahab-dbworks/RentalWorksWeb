import { HomeModule } from "../../shared/HomeModule";
import { TestUtils } from "../../shared/TestUtils";
import { Logging } from '../../shared/Logging';
import { GridBase } from "../../shared/GridBase";
import { ModuleBase } from "../../shared/ModuleBase";

//---------------------------------------------------------------------------------------
export class Quote extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Quote';
        this.moduleGroupName = 'Agent';
        this.moduleId = 'jFkSBEur1dluU';
        this.moduleCaption = 'Quote';
        this.populateNewFormTimeout = 300000; // 300 seconds = 5 minutes
        this.canDelete = false;
        let rentalGrid: GridBase = new GridBase("Rental Item Grid", "OrderItemGrid", ["R"]);
        rentalGrid.waitAfterSavingToReloadGrid = 1500;
        rentalGrid.multiRowSave = true;
        let salesGrid: GridBase = new GridBase("Sales Item Grid", "OrderItemGrid", ["S"]);
        salesGrid.waitAfterSavingToReloadGrid = 1500;
        salesGrid.multiRowSave = true;
        let miscGrid: GridBase = new GridBase("Miscellaneous Item Grid", "OrderItemGrid", ["M"]);
        miscGrid.waitAfterSavingToReloadGrid = 1500;
        miscGrid.multiRowSave = true;
        let laborGrid: GridBase = new GridBase("Labor Item Grid", "OrderItemGrid", ["L"]);
        laborGrid.waitAfterSavingToReloadGrid = 1500;
        laborGrid.multiRowSave = true;
        let contactGrid: GridBase = new GridBase("Contact Grid", "OrderContactGrid");
        let noteGrid: GridBase = new GridBase("Note Grid", "OrderNoteGrid");
        this.grids.push(rentalGrid);
        this.grids.push(salesGrid);
        this.grids.push(miscGrid);
        this.grids.push(laborGrid);
        this.grids.push(contactGrid);
        this.grids.push(noteGrid);

        this.defaultNewRecordToExpect = {
            //quote tab
            QuoteNumber: "",
            Department: "GlobalScope.User~ME.PrimaryDepartment",
            //rate?
            //order type?
            ReferenceNumber: "",
            Location: "",
            PickDate: TestUtils.todayMDY(),
            EstimatedStartDate: TestUtils.todayMDY(),
            EstimatedStopDate: TestUtils.todayMDY(),
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Warehouse: "GlobalScope.User~ME.Warehouse",
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            OutsideSalesRepresentative: "",
            PendingPo: true,
            FlatPo: false,
            PoNumber: "",
            PoAmount: "",
            MarketType: "",
            MarketSegment: "",
            MarketSegmentJob: "",

            //billing tab
            BillingCycle: "GlobalScope.DefaultSettings~1.DefaultDealBillingCycle",
        }

        this.newRecordsToCreate = [
            {
                record: {
                    //quote tab
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    DealId: 1,  //TestUtils.randomIntegerBetween(1, 10), this won't work because not all Deals have contacts, 
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    RentalSale: false,
                    //Facilities
                    //LossAndDamage: false
                    EstimatedStopDate: TestUtils.futureDateMDY(10),
                    EstimatedStartDate: TestUtils.futureDateMDY(5),
                    PickDate: TestUtils.futureDateMDY(3),
                    PickTime: TestUtils.randomTimeHHMM(),
                    EstimatedStartTime: TestUtils.randomTimeHHMM(),
                    EstimatedStopTime: TestUtils.randomTimeHHMM(),
                    //ActivityDatesAndTimes: [{ _Custom: [], OrderId: "A0002QCM", OrderTypeId: "000000S7", OrderTypeDateTypeId: "00012XCG", … }, …]
                    OutsideSalesRepresentativeId: TestUtils.randomIntegerBetween(1, 5),
                    CoverLetterId: 1,
                    TermsConditionsId: 1,
                    MarketTypeId: 1,
                    MarketSegmentId: 1,
                    //MarketSegmentJobId: 1,
                    PendingPo: false,
                    FlatPo: false,
                    PoNumber: "GlobalScope.TestToken~1.MediumTestToken",
                    PoAmount: TestUtils.randomIntegerBetween(100, 999).toString() + "." + TestUtils.randomIntegerBetween(10, 99).toString(),

                    //billing tab
                    BillingStartDate: TestUtils.futureDateMDY(10),
                    BillingEndDate: TestUtils.futureDateMDY(20),
                    //BillingMonths: 0
                    //BillingWeeks: 0
                    DelayBillingSearchUntil: TestUtils.futureDateMDY(5),
                    LockBillingDates: TestUtils.randomBoolean(),
                    SpecifyBillingDatesByType: false,
                    //FacilitiesBillingEndDate: "",
                    //FacilitiesBillingStartDate: "",
                    //LaborBillingEndDate: "",
                    //LaborBillingStartDate: "",
                    //MiscellaneousBillingEndDate: "",
                    //MiscellaneousBillingStartDate: "",
                    //RentalBillingEndDate: "",
                    //RentalBillingStartDate: "",
                    //VehicleBillingEndDate: "",
                    //VehicleBillingStartDate: "",
                    //DetermineQuantitiesToBillBasedOn: "",
                    BillingCycleId: TestUtils.randomIntegerBetween(1, 5),
                    PaymentTermsId: TestUtils.randomIntegerBetween(1, 5),
                    PaymentTypeId: TestUtils.randomIntegerBetween(1, 5),
                    //CurrencyId: TestUtils.randomIntegerBetween(1, 5),
                    CurrencyCode: "USD",
                    TaxOptionId: TestUtils.randomIntegerBetween(1, 5),
                    //PrintIssuedToAddressFrom: "CUSTOMER",
                    IssuedToName: TestUtils.randomJobTitle(),
                    IssuedToAttention: TestUtils.randomFullName(),
                    IssuedToAttention2: TestUtils.randomFullName(),
                    IssuedToAddress1: TestUtils.randomAddress1(),
                    IssuedToAddress2: TestUtils.randomAddress2(),
                    IssuedToCity: TestUtils.randomCity(),
                    IssuedToState: TestUtils.randomStateCode(),
                    IssuedToZipCode: TestUtils.randomZipCode(),
                    IssuedToCountryId: TestUtils.randomIntegerBetween(1, 5),
                    DiscountReasonId: TestUtils.randomIntegerBetween(1, 5),
                    RoundTripRentals: TestUtils.randomBoolean(),
                    RequireContactConfirmation: TestUtils.randomBoolean(),
                    BillToAddressDifferentFromIssuedToAddress: true,
                    BillToName: TestUtils.randomJobTitle(),
                    BillToAttention: TestUtils.randomFullName(),
                    BillToAttention2: TestUtils.randomFullName(),
                    BillToAddress1: TestUtils.randomAddress1(),
                    BillToAddress2: TestUtils.randomAddress2(),
                    BillToCity: TestUtils.randomCity(),
                    BillToState: TestUtils.randomStateCode(),
                    BillToZipCode: TestUtils.randomZipCode(),
                    BillToCountryId: TestUtils.randomIntegerBetween(1, 5),
                    NoCharge: TestUtils.randomBoolean(),
                    NoChargeReason: TestUtils.randomLoremWords(20),
                    //InGroup: false,
                    //GroupNumber: 0,
                    //HiatusDiscountFrom: ""


                    //deliver / ship
                    OutDeliveryTargetShipDate: TestUtils.randomFutureDateMDY(5),
                    OutDeliveryRequiredDate: TestUtils.randomFutureDateMDY(6),
                    OutDeliveryRequiredTime: TestUtils.randomTimeHHMM(),
                    OutDeliveryToContact: TestUtils.randomFullName(),
                    OutDeliveryToContactPhone: TestUtils.randomPhone(),
                    OutDeliveryCarrierId: 1,
                    OutDeliveryShipViaId: 1,
                    OutDeliveryFreightTrackingNumber: "GlobalScope.TestToken~1.TestToken",
                    //OutDeliveryAddressType: "WAREHOUSE"
                    OutDeliveryToLocation: TestUtils.randomJobTitle(),
                    OutDeliveryToAttention: TestUtils.randomFullName(),
                    OutDeliveryToAddress1: TestUtils.randomAddress1(),
                    OutDeliveryToAddress2: TestUtils.randomAddress2(),
                    OutDeliveryToCity: TestUtils.randomCity(),
                    OutDeliveryToState: TestUtils.randomStateCode(),
                    OutDeliveryToZipCode: TestUtils.randomZipCode(),
                    OutDeliveryToCountryId: TestUtils.randomIntegerBetween(1, 5),
                    OutDeliveryToCrossStreets: TestUtils.randomLoremWords(5),
                    OutDeliveryDeliveryNotes: TestUtils.randomLoremWords(20),
                    OutDeliveryOnlineOrderNumber: "GlobalScope.TestToken~1.TestToken",
                    OutDeliveryOnlineOrderStatus: TestUtils.randomIntegerBetween(2, 3),
                    InDeliveryTargetShipDate: TestUtils.randomFutureDateMDY(5),
                    InDeliveryRequiredDate: TestUtils.randomFutureDateMDY(6),
                    InDeliveryRequiredTime: TestUtils.randomTimeHHMM(),
                    InDeliveryToContact: TestUtils.randomFullName(),
                    InDeliveryToContactPhone: TestUtils.randomPhone(),
                    InDeliveryCarrierId: 1,
                    InDeliveryShipViaId: 1,
                    InDeliveryFreightTrackingNumber: "GlobalScope.TestToken~1.TestToken",
                    //InDeliveryAddressType: "WAREHOUSE",
                    InDeliveryToLocation: TestUtils.randomJobTitle(),
                    InDeliveryToAttention: TestUtils.randomFullName(),
                    InDeliveryToAddress1: TestUtils.randomAddress1(),
                    InDeliveryToAddress2: TestUtils.randomAddress2(),
                    InDeliveryToCity: TestUtils.randomCity(),
                    InDeliveryToState: TestUtils.randomStateCode(),
                    InDeliveryToZipCode: TestUtils.randomZipCode(),
                    InDeliveryToCountryId: TestUtils.randomIntegerBetween(1, 5),
                    InDeliveryToCrossStreets: TestUtils.randomLoremWords(5),
                    InDeliveryDeliveryNotes: TestUtils.randomLoremWords(20),


                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: rentalGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: salesGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: miscGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: laborGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: contactGrid,
                recordToCreate: {
                    record: {
                        ContactId: 1,
                        ContactTitleId: 1,
                    },
                    editRecord: {
                        record: {
                            OfficeExtension: "GlobalScope.TestToken~1.ShortTestToken",
                        },
                        recordToExpect: {
                            OfficeExtension: "GlobalScope.TestToken~1.ShortTestToken",
                        },
                    },
                },
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        NotesDescription: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            NotesDescription: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            NotesDescription: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
        ];



        this.newRecordsToCreate[0].recordToExpect = {
            //quote tab
            QuoteId: ModuleBase.NOTEMPTY,
            QuoteNumber: ModuleBase.NOTEMPTY,
            //QuoteDate: TestUtils.todayMDY(),
            //RateType: "DAILY"
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            DealId: ModuleBase.NOTEMPTY,
            Deal: ModuleBase.NOTEMPTY,
            DealNumber: ModuleBase.NOTEMPTY,
            CustomerId: ModuleBase.NOTEMPTY,
            Customer: ModuleBase.NOTEMPTY,
            CustomerNumber: ModuleBase.NOTEMPTY,
            DepartmentId: ModuleBase.NOTEMPTY,
            Department: ModuleBase.NOTEMPTY,
            Location: this.newRecordsToCreate[0].record.Location.toUpperCase(),
            OrderTypeId: ModuleBase.NOTEMPTY,
            Rental: this.newRecordsToCreate[0].record.Rental,
            Sales: this.newRecordsToCreate[0].record.Sales,
            Miscellaneous: this.newRecordsToCreate[0].record.Miscellaneous,
            Labor: this.newRecordsToCreate[0].record.Labor,
            RentalSale: this.newRecordsToCreate[0].record.RentalSale,
            //Facilities: this.newRecordsToCreate[0].record.Facilities,
            ReferenceNumber: this.newRecordsToCreate[0].record.ReferenceNumber.toUpperCase(),
            Status: "ACTIVE",
            StatusDate: TestUtils.todayMDY(),
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Warehouse: "GlobalScope.User~ME.Warehouse",
            PendingPo: this.newRecordsToCreate[0].record.PendingPo,
            FlatPo: this.newRecordsToCreate[0].record.FlatPo,
            PoNumber: "GlobalScope.TestToken~1.MediumTestToken",
            PoAmount: "$ " + this.newRecordsToCreate[0].record.PoAmount,
            //justin hoffman 02/21/2020 temporary work-around.  To fix this properly, we need to add an attribute to dynamic fields such as [data-testdatafield="PickDate"] 
            //PickDate: this.newRecordsToCreate[0].record.PickDate,
            //EstimatedStartDate: this.newRecordsToCreate[0].record.EstimatedStartDate,
            //EstimatedStopDate: this.newRecordsToCreate[0].record.EstimatedStopDate,
            //PickTime: this.newRecordsToCreate[0].record.PickTime,
            //EstimatedStartTime: this.newRecordsToCreate[0].record.EstimatedStartTime,
            //EstimatedStopTime: this.newRecordsToCreate[0].record.EstimatedStopTime,
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            OutsideSalesRepresentative: ModuleBase.NOTEMPTY,
            CoverLetter: ModuleBase.NOTEMPTY,
            TermsConditions: ModuleBase.NOTEMPTY,
            MarketType: ModuleBase.NOTEMPTY,
            MarketSegment: ModuleBase.NOTEMPTY,
            //MarketSegmentJob: ModuleBase.NOTEMPTY,

            //billing tab
            BillingStartDate: this.newRecordsToCreate[0].record.BillingStartDate,
            BillingEndDate: this.newRecordsToCreate[0].record.BillingEndDate,
            //BillingMonths: 0
            //BillingWeeks: 0
            DelayBillingSearchUntil: this.newRecordsToCreate[0].record.DelayBillingSearchUntil,
            LockBillingDates: this.newRecordsToCreate[0].record.LockBillingDates,
            SpecifyBillingDatesByType: this.newRecordsToCreate[0].record.SpecifyBillingDatesByType,
            BillingCycle: ModuleBase.NOTEMPTY,
            PaymentTerms: ModuleBase.NOTEMPTY,
            PaymentType: ModuleBase.NOTEMPTY,
            CurrencyCode: ModuleBase.NOTEMPTY,
            Currency: ModuleBase.NOTEMPTY,
            TaxOption: ModuleBase.NOTEMPTY,
            TaxId: ModuleBase.NOTEMPTY,
            DiscountReason: ModuleBase.NOTEMPTY,
            IssuedToName: this.newRecordsToCreate[0].record.IssuedToName.toUpperCase(),
            IssuedToAttention: this.newRecordsToCreate[0].record.IssuedToAttention.toUpperCase(),
            IssuedToAttention2: this.newRecordsToCreate[0].record.IssuedToAttention2.toUpperCase(),
            IssuedToAddress1: this.newRecordsToCreate[0].record.IssuedToAddress1.toUpperCase(),
            IssuedToAddress2: this.newRecordsToCreate[0].record.IssuedToAddress2.toUpperCase(),
            IssuedToCity: this.newRecordsToCreate[0].record.IssuedToCity.toUpperCase(),
            IssuedToState: this.newRecordsToCreate[0].record.IssuedToState.toUpperCase(),
            IssuedToZipCode: this.newRecordsToCreate[0].record.IssuedToZipCode.toUpperCase(),
            IssuedToCountry: ModuleBase.NOTEMPTY,
            RoundTripRentals: this.newRecordsToCreate[0].record.RoundTripRentals,
            RequireContactConfirmation: this.newRecordsToCreate[0].record.RequireContactConfirmation,
            BillToAddressDifferentFromIssuedToAddress: this.newRecordsToCreate[0].record.BillToAddressDifferentFromIssuedToAddress,
            BillToName: this.newRecordsToCreate[0].record.BillToName.toUpperCase(),
            BillToAttention: this.newRecordsToCreate[0].record.BillToAttention.toUpperCase(),
            BillToAttention2: this.newRecordsToCreate[0].record.BillToAttention2.toUpperCase(),
            BillToAddress1: this.newRecordsToCreate[0].record.BillToAddress1.toUpperCase(),
            BillToAddress2: this.newRecordsToCreate[0].record.BillToAddress2.toUpperCase(),
            BillToCity: this.newRecordsToCreate[0].record.BillToCity.toUpperCase(),
            BillToState: this.newRecordsToCreate[0].record.BillToState.toUpperCase(),
            BillToZipCode: this.newRecordsToCreate[0].record.BillToZipCode.toUpperCase(),
            BillToCountry: ModuleBase.NOTEMPTY,
            NoCharge: this.newRecordsToCreate[0].record.NoCharge,
            NoChargeReason: this.newRecordsToCreate[0].record.NoChargeReason,

            //deliver / ship
            OutDeliveryTargetShipDate: this.newRecordsToCreate[0].record.OutDeliveryTargetShipDate,
            OutDeliveryRequiredDate: this.newRecordsToCreate[0].record.OutDeliveryRequiredDate,
            OutDeliveryRequiredTime: this.newRecordsToCreate[0].record.OutDeliveryRequiredTime,
            OutDeliveryToContact: this.newRecordsToCreate[0].record.OutDeliveryToContact.toUpperCase(),
            OutDeliveryToContactPhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.OutDeliveryToContactPhone),
            OutDeliveryCarrier: ModuleBase.NOTEMPTY,
            OutDeliveryShipVia: ModuleBase.NOTEMPTY,
            OutDeliveryFreightTrackingNumber: "GlobalScope.TestToken~1.TestToken",
            OutDeliveryToLocation: this.newRecordsToCreate[0].record.OutDeliveryToLocation.toUpperCase(),
            OutDeliveryToAttention: this.newRecordsToCreate[0].record.OutDeliveryToAttention.toUpperCase(),
            OutDeliveryToAddress1: this.newRecordsToCreate[0].record.OutDeliveryToAddress1.toUpperCase(),
            OutDeliveryToAddress2: this.newRecordsToCreate[0].record.OutDeliveryToAddress2.toUpperCase(),
            OutDeliveryToCity: this.newRecordsToCreate[0].record.OutDeliveryToCity.toUpperCase(),
            OutDeliveryToState: this.newRecordsToCreate[0].record.OutDeliveryToState.toUpperCase(),
            OutDeliveryToZipCode: this.newRecordsToCreate[0].record.OutDeliveryToZipCode.toUpperCase(),
            OutDeliveryToCountry: ModuleBase.NOTEMPTY,
            OutDeliveryToCrossStreets: this.newRecordsToCreate[0].record.OutDeliveryToCrossStreets,
            OutDeliveryDeliveryNotes: this.newRecordsToCreate[0].record.OutDeliveryDeliveryNotes,
            OutDeliveryOnlineOrderNumber: "GlobalScope.TestToken~1.TestToken",
            OutDeliveryOnlineOrderStatus: ModuleBase.NOTEMPTY,
            InDeliveryTargetShipDate: this.newRecordsToCreate[0].record.InDeliveryTargetShipDate,
            InDeliveryRequiredDate: this.newRecordsToCreate[0].record.InDeliveryRequiredDate,
            InDeliveryRequiredTime: this.newRecordsToCreate[0].record.InDeliveryRequiredTime,
            InDeliveryToContact: this.newRecordsToCreate[0].record.InDeliveryToContact.toUpperCase(),
            InDeliveryToContactPhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.InDeliveryToContactPhone),
            InDeliveryCarrierId: ModuleBase.NOTEMPTY,
            InDeliveryShipViaId: ModuleBase.NOTEMPTY,
            InDeliveryFreightTrackingNumber: "GlobalScope.TestToken~1.TestToken",
            InDeliveryToLocation: this.newRecordsToCreate[0].record.InDeliveryToLocation.toUpperCase(),
            InDeliveryToAttention: this.newRecordsToCreate[0].record.InDeliveryToAttention.toUpperCase(),
            InDeliveryToAddress1: this.newRecordsToCreate[0].record.InDeliveryToAddress1.toUpperCase(),
            InDeliveryToAddress2: this.newRecordsToCreate[0].record.InDeliveryToAddress2.toUpperCase(),
            InDeliveryToCity: this.newRecordsToCreate[0].record.InDeliveryToCity.toUpperCase(),
            InDeliveryToState: this.newRecordsToCreate[0].record.InDeliveryToState.toUpperCase(),
            InDeliveryToZipCode: this.newRecordsToCreate[0].record.InDeliveryToZipCode.toUpperCase(),
            InDeliveryToCountry: ModuleBase.NOTEMPTY,
            InDeliveryToCrossStreets: this.newRecordsToCreate[0].record.InDeliveryToCrossStreets,
            InDeliveryDeliveryNotes: this.newRecordsToCreate[0].record.InDeliveryDeliveryNotes,

            //HasFacilitiesItem: false,
            //HasLaborItem: true,
            //HasLossAndDamageItem: false,
            //HasMiscellaneousItem: false,
            //HasNotes: false,
            //HasRentalItem: true,
            //HasRentalSaleItem: false,
            //HasRepair: false,
            //HasSalesItem: true,







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
        this.moduleGroupName = 'Agent';
        this.moduleId = 'U8Zlahz3ke9i';
        this.moduleCaption = 'Order';
        this.populateNewFormTimeout = 300000; // 300 seconds = 5 minutes
        this.canDelete = false;
        let rentalGrid: GridBase = new GridBase("Rental Item Grid", "OrderItemGrid", ["R"]);
        rentalGrid.waitAfterSavingToReloadGrid = 1500;
        rentalGrid.multiRowSave = true;
        let salesGrid: GridBase = new GridBase("Sales Item Grid", "OrderItemGrid", ["S"]);
        salesGrid.waitAfterSavingToReloadGrid = 1500;
        salesGrid.multiRowSave = true;
        let miscGrid: GridBase = new GridBase("Miscellaneous Item Grid", "OrderItemGrid", ["M"]);
        miscGrid.waitAfterSavingToReloadGrid = 1500;
        miscGrid.multiRowSave = true;
        let laborGrid: GridBase = new GridBase("Labor Item Grid", "OrderItemGrid", ["L"]);
        laborGrid.waitAfterSavingToReloadGrid = 1500;
        laborGrid.multiRowSave = true;
        let contactGrid: GridBase = new GridBase("Contact Grid", "OrderContactGrid");
        let noteGrid: GridBase = new GridBase("Note Grid", "OrderNoteGrid");
        this.grids.push(rentalGrid);
        this.grids.push(salesGrid);
        this.grids.push(miscGrid);
        this.grids.push(laborGrid);
        this.grids.push(contactGrid);
        this.grids.push(noteGrid);


        this.defaultNewRecordToExpect = {
            OrderNumber: "",
            Department: "GlobalScope.User~ME.PrimaryDepartment",
            //rate?
            //order type?
            ReferenceNumber: "",
            Location: "",
            PickDate: TestUtils.todayMDY(),
            EstimatedStartDate: TestUtils.todayMDY(),
            EstimatedStopDate: TestUtils.todayMDY(),
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Warehouse: "GlobalScope.User~ME.Warehouse",
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            OutsideSalesRepresentative: "",
            PendingPo: true,
            FlatPo: false,
            PoNumber: "",
            PoAmount: "",
            MarketType: "",
            MarketSegment: "",
            MarketSegmentJob: "",

            //billing tab
            BillingCycle: "GlobalScope.DefaultSettings~1.DefaultDealBillingCycle",
        }

        this.newRecordsToCreate = [
            {
                record: {
                    //order tab
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    DealId: 1, //TestUtils.randomIntegerBetween(1, 10), this won't work because not all Deals have contacts, 
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    RentalSale: false,
                    //Facilities
                    LossAndDamage: false,

                    EstimatedStopDate: TestUtils.futureDateMDY(10),
                    EstimatedStartDate: TestUtils.futureDateMDY(5),
                    PickDate: TestUtils.futureDateMDY(3),
                    PickTime: TestUtils.randomTimeHHMM(),
                    EstimatedStartTime: TestUtils.randomTimeHHMM(),
                    EstimatedStopTime: TestUtils.randomTimeHHMM(),
                    //ActivityDatesAndTimes: [{ _Custom: [], OrderId: "A0002QCM", OrderTypeId: "000000S7", OrderTypeDateTypeId: "00012XCG", … }, …]
                    OutsideSalesRepresentativeId: TestUtils.randomIntegerBetween(1, 5),
                    CoverLetterId: 1,
                    TermsConditionsId: 1,
                    MarketTypeId: 1,
                    MarketSegmentId: 1,
                    //MarketSegmentJobId: 1,
                    PendingPo: false,
                    FlatPo: false,
                    PoNumber: "GlobalScope.TestToken~1.MediumTestToken",
                    PoAmount: TestUtils.randomIntegerBetween(100, 999).toString() + "." + TestUtils.randomIntegerBetween(10, 99).toString(),

                    //billing tab
                    BillingStartDate: TestUtils.futureDateMDY(10),
                    BillingEndDate: TestUtils.futureDateMDY(20),
                    //BillingMonths: 0
                    //BillingWeeks: 0
                    DelayBillingSearchUntil: TestUtils.futureDateMDY(5),
                    LockBillingDates: TestUtils.randomBoolean(),
                    SpecifyBillingDatesByType: false,
                    //FacilitiesBillingEndDate: "",
                    //FacilitiesBillingStartDate: "",
                    //LaborBillingEndDate: "",
                    //LaborBillingStartDate: "",
                    //MiscellaneousBillingEndDate: "",
                    //MiscellaneousBillingStartDate: "",
                    //RentalBillingEndDate: "",
                    //RentalBillingStartDate: "",
                    //VehicleBillingEndDate: "",
                    //VehicleBillingStartDate: "",
                    //DetermineQuantitiesToBillBasedOn: "",
                    BillingCycleId: TestUtils.randomIntegerBetween(1, 5),
                    PaymentTermsId: TestUtils.randomIntegerBetween(1, 5),
                    PaymentTypeId: TestUtils.randomIntegerBetween(1, 5),
                    //CurrencyId: TestUtils.randomIntegerBetween(1, 5),
                    CurrencyCode: "USD",
                    TaxOptionId: TestUtils.randomIntegerBetween(1, 5),
                    //PrintIssuedToAddressFrom: "CUSTOMER",
                    IssuedToName: TestUtils.randomJobTitle(),
                    IssuedToAttention: TestUtils.randomFullName(),
                    IssuedToAttention2: TestUtils.randomFullName(),
                    IssuedToAddress1: TestUtils.randomAddress1(),
                    IssuedToAddress2: TestUtils.randomAddress2(),
                    IssuedToCity: TestUtils.randomCity(),
                    IssuedToState: TestUtils.randomStateCode(),
                    IssuedToZipCode: TestUtils.randomZipCode(),
                    IssuedToCountryId: TestUtils.randomIntegerBetween(1, 5),
                    DiscountReasonId: TestUtils.randomIntegerBetween(1, 5),
                    RoundTripRentals: TestUtils.randomBoolean(),
                    RequireContactConfirmation: TestUtils.randomBoolean(),
                    BillToAddressDifferentFromIssuedToAddress: true,
                    BillToName: TestUtils.randomJobTitle(),
                    BillToAttention: TestUtils.randomFullName(),
                    //BillToAttention2: TestUtils.randomFullName(),  //jh add back once fixed
                    //BillToAddress1: TestUtils.randomAddress1(),
                    BillToAddress2: TestUtils.randomAddress2(),
                    BillToCity: TestUtils.randomCity(),
                    BillToState: TestUtils.randomStateCode(),
                    BillToZipCode: TestUtils.randomZipCode(),
                    BillToCountryId: TestUtils.randomIntegerBetween(1, 5),
                    NoCharge: TestUtils.randomBoolean(),
                    NoChargeReason: TestUtils.randomLoremWords(20),
                    //InGroup: false,
                    //GroupNumber: 0,
                    //HiatusDiscountFrom: ""


                    //deliver / ship
                    OutDeliveryTargetShipDate: TestUtils.randomFutureDateMDY(5),
                    OutDeliveryRequiredDate: TestUtils.randomFutureDateMDY(6),
                    OutDeliveryRequiredTime: TestUtils.randomTimeHHMM(),
                    OutDeliveryToContact: TestUtils.randomFullName(),
                    OutDeliveryToContactPhone: TestUtils.randomPhone(),
                    OutDeliveryCarrierId: 1,
                    OutDeliveryShipViaId: 1,
                    OutDeliveryFreightTrackingNumber: "GlobalScope.TestToken~1.TestToken",
                    //OutDeliveryAddressType: "WAREHOUSE"
                    OutDeliveryToLocation: TestUtils.randomJobTitle(),
                    OutDeliveryToAttention: TestUtils.randomFullName(),
                    OutDeliveryToAddress1: TestUtils.randomAddress1(),
                    OutDeliveryToAddress2: TestUtils.randomAddress2(),
                    OutDeliveryToCity: TestUtils.randomCity(),
                    OutDeliveryToState: TestUtils.randomStateCode(),
                    OutDeliveryToZipCode: TestUtils.randomZipCode(),
                    OutDeliveryToCountryId: TestUtils.randomIntegerBetween(1, 5),
                    OutDeliveryToCrossStreets: TestUtils.randomLoremWords(5),
                    OutDeliveryDeliveryNotes: TestUtils.randomLoremWords(20),
                    OutDeliveryOnlineOrderNumber: "GlobalScope.TestToken~1.TestToken",
                    OutDeliveryOnlineOrderStatus: TestUtils.randomIntegerBetween(2, 3),
                    InDeliveryTargetShipDate: TestUtils.randomFutureDateMDY(5),
                    InDeliveryRequiredDate: TestUtils.randomFutureDateMDY(6),
                    InDeliveryRequiredTime: TestUtils.randomTimeHHMM(),
                    InDeliveryToContact: TestUtils.randomFullName(),
                    InDeliveryToContactPhone: TestUtils.randomPhone(),
                    InDeliveryCarrierId: 1,
                    InDeliveryShipViaId: 1,
                    InDeliveryFreightTrackingNumber: "GlobalScope.TestToken~1.TestToken",
                    //InDeliveryAddressType: "WAREHOUSE",
                    InDeliveryToLocation: TestUtils.randomJobTitle(),
                    InDeliveryToAttention: TestUtils.randomFullName(),
                    InDeliveryToAddress1: TestUtils.randomAddress1(),
                    InDeliveryToAddress2: TestUtils.randomAddress2(),
                    InDeliveryToCity: TestUtils.randomCity(),
                    InDeliveryToState: TestUtils.randomStateCode(),
                    InDeliveryToZipCode: TestUtils.randomZipCode(),
                    InDeliveryToCountryId: TestUtils.randomIntegerBetween(1, 5),
                    InDeliveryToCrossStreets: TestUtils.randomLoremWords(5),
                    InDeliveryDeliveryNotes: TestUtils.randomLoremWords(20),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: rentalGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: salesGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: miscGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: laborGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: contactGrid,
                recordToCreate: {
                    record: {
                        ContactId: 1,
                        ContactTitleId: 1,
                    },
                    editRecord: {
                        record: {
                            OfficeExtension: "GlobalScope.TestToken~1.ShortTestToken",
                        },
                        recordToExpect: {
                            OfficeExtension: "GlobalScope.TestToken~1.ShortTestToken",
                        },
                    },
                },
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        NotesDescription: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            NotesDescription: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            NotesDescription: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
        ];


        this.newRecordsToCreate[0].recordToExpect = {
            //order tab
            OrderId: ModuleBase.NOTEMPTY,
            OrderNumber: ModuleBase.NOTEMPTY,
            //OrderDate: TestUtils.todayMDY(),
            //RateType: "DAILY"
            DealId: ModuleBase.NOTEMPTY,
            Deal: ModuleBase.NOTEMPTY,
            DealNumber: ModuleBase.NOTEMPTY,
            CustomerId: ModuleBase.NOTEMPTY,
            Customer: ModuleBase.NOTEMPTY,
            CustomerNumber: ModuleBase.NOTEMPTY,
            DepartmentId: ModuleBase.NOTEMPTY,
            Department: ModuleBase.NOTEMPTY,
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            OrderTypeId: ModuleBase.NOTEMPTY,
            Location: this.newRecordsToCreate[0].record.Location.toUpperCase(),
            Rental: this.newRecordsToCreate[0].record.Rental,
            Sales: this.newRecordsToCreate[0].record.Sales,
            Miscellaneous: this.newRecordsToCreate[0].record.Miscellaneous,
            Labor: this.newRecordsToCreate[0].record.Labor,
            RentalSale: this.newRecordsToCreate[0].record.RentalSale,
            //Facilities: this.newRecordsToCreate[0].record.Facilities,
            LossAndDamage: this.newRecordsToCreate[0].record.LossAndDamage,
            ReferenceNumber: this.newRecordsToCreate[0].record.ReferenceNumber.toUpperCase(),
            Status: "CONFIRMED",
            StatusDate: TestUtils.todayMDY(),
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",
            Warehouse: "GlobalScope.User~ME.Warehouse",
            PendingPo: this.newRecordsToCreate[0].record.PendingPo,
            FlatPo: this.newRecordsToCreate[0].record.FlatPo,
            PoNumber: "GlobalScope.TestToken~1.MediumTestToken",
            PoAmount: "$ " + this.newRecordsToCreate[0].record.PoAmount,
            //justin hoffman 02/21/2020 temporary work-around.  To fix this properly, we need to add an attribute to dynamic fields such as [data-testdatafield="PickDate"] 
            //PickDate: this.newRecordsToCreate[0].record.PickDate,
            //EstimatedStartDate: this.newRecordsToCreate[0].record.EstimatedStartDate,
            //EstimatedStopDate: this.newRecordsToCreate[0].record.EstimatedStopDate,
            //PickTime: this.newRecordsToCreate[0].record.PickTime,
            //EstimatedStartTime: this.newRecordsToCreate[0].record.EstimatedStartTime,
            //EstimatedStopTime: this.newRecordsToCreate[0].record.EstimatedStopTime,
            Agent: "GlobalScope.User~ME.Name",
            ProjectManager: "GlobalScope.User~ME.Name",
            OutsideSalesRepresentative: ModuleBase.NOTEMPTY,
            CoverLetter: ModuleBase.NOTEMPTY,
            TermsConditions: ModuleBase.NOTEMPTY,
            MarketType: ModuleBase.NOTEMPTY,
            MarketSegment: ModuleBase.NOTEMPTY,
            //MarketSegmentJob: ModuleBase.NOTEMPTY,

            //billing tab
            BillingStartDate: this.newRecordsToCreate[0].record.BillingStartDate,
            BillingEndDate: this.newRecordsToCreate[0].record.BillingEndDate,
            //BillingMonths: 0
            //BillingWeeks: 0
            DelayBillingSearchUntil: this.newRecordsToCreate[0].record.DelayBillingSearchUntil,
            LockBillingDates: this.newRecordsToCreate[0].record.LockBillingDates,
            SpecifyBillingDatesByType: this.newRecordsToCreate[0].record.SpecifyBillingDatesByType,
            BillingCycle: ModuleBase.NOTEMPTY,
            PaymentTerms: ModuleBase.NOTEMPTY,
            PaymentType: ModuleBase.NOTEMPTY,
            CurrencyCode: ModuleBase.NOTEMPTY,
            Currency: ModuleBase.NOTEMPTY,
            TaxOption: ModuleBase.NOTEMPTY,
            TaxId: ModuleBase.NOTEMPTY,
            DiscountReason: ModuleBase.NOTEMPTY,
            IssuedToName: this.newRecordsToCreate[0].record.IssuedToName.toUpperCase(),
            IssuedToAttention: this.newRecordsToCreate[0].record.IssuedToAttention.toUpperCase(),
            IssuedToAttention2: this.newRecordsToCreate[0].record.IssuedToAttention2.toUpperCase(),
            IssuedToAddress1: this.newRecordsToCreate[0].record.IssuedToAddress1.toUpperCase(),
            IssuedToAddress2: this.newRecordsToCreate[0].record.IssuedToAddress2.toUpperCase(),
            IssuedToCity: this.newRecordsToCreate[0].record.IssuedToCity.toUpperCase(),
            IssuedToState: this.newRecordsToCreate[0].record.IssuedToState.toUpperCase(),
            IssuedToZipCode: this.newRecordsToCreate[0].record.IssuedToZipCode.toUpperCase(),
            IssuedToCountry: ModuleBase.NOTEMPTY,
            RoundTripRentals: this.newRecordsToCreate[0].record.RoundTripRentals,
            RequireContactConfirmation: this.newRecordsToCreate[0].record.RequireContactConfirmation,
            BillToAddressDifferentFromIssuedToAddress: this.newRecordsToCreate[0].record.BillToAddressDifferentFromIssuedToAddress,
            BillToName: this.newRecordsToCreate[0].record.BillToName.toUpperCase(),
            BillToAttention: this.newRecordsToCreate[0].record.BillToAttention.toUpperCase(),
            //BillToAttention2: this.newRecordsToCreate[0].record.BillToAttention2.toUpperCase(),  // add back when fixed
            //BillToAddress1: this.newRecordsToCreate[0].record.BillToAddress1.toUpperCase(),
            BillToAddress2: this.newRecordsToCreate[0].record.BillToAddress2.toUpperCase(),
            BillToCity: this.newRecordsToCreate[0].record.BillToCity.toUpperCase(),
            BillToState: this.newRecordsToCreate[0].record.BillToState.toUpperCase(),
            BillToZipCode: this.newRecordsToCreate[0].record.BillToZipCode.toUpperCase(),
            BillToCountry: ModuleBase.NOTEMPTY,
            NoCharge: this.newRecordsToCreate[0].record.NoCharge,
            NoChargeReason: this.newRecordsToCreate[0].record.NoChargeReason,

            //deliver / ship
            OutDeliveryTargetShipDate: this.newRecordsToCreate[0].record.OutDeliveryTargetShipDate,
            OutDeliveryRequiredDate: this.newRecordsToCreate[0].record.OutDeliveryRequiredDate,
            OutDeliveryRequiredTime: this.newRecordsToCreate[0].record.OutDeliveryRequiredTime,
            OutDeliveryToContact: this.newRecordsToCreate[0].record.OutDeliveryToContact.toUpperCase(),
            OutDeliveryToContactPhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.OutDeliveryToContactPhone),
            OutDeliveryCarrier: ModuleBase.NOTEMPTY,
            OutDeliveryShipVia: ModuleBase.NOTEMPTY,
            OutDeliveryFreightTrackingNumber: "GlobalScope.TestToken~1.TestToken",
            OutDeliveryToLocation: this.newRecordsToCreate[0].record.OutDeliveryToLocation.toUpperCase(),
            OutDeliveryToAttention: this.newRecordsToCreate[0].record.OutDeliveryToAttention.toUpperCase(),
            OutDeliveryToAddress1: this.newRecordsToCreate[0].record.OutDeliveryToAddress1.toUpperCase(),
            OutDeliveryToAddress2: this.newRecordsToCreate[0].record.OutDeliveryToAddress2.toUpperCase(),
            OutDeliveryToCity: this.newRecordsToCreate[0].record.OutDeliveryToCity.toUpperCase(),
            OutDeliveryToState: this.newRecordsToCreate[0].record.OutDeliveryToState.toUpperCase(),
            OutDeliveryToZipCode: this.newRecordsToCreate[0].record.OutDeliveryToZipCode.toUpperCase(),
            OutDeliveryToCountry: ModuleBase.NOTEMPTY,
            OutDeliveryToCrossStreets: this.newRecordsToCreate[0].record.OutDeliveryToCrossStreets,
            OutDeliveryDeliveryNotes: this.newRecordsToCreate[0].record.OutDeliveryDeliveryNotes,
            OutDeliveryOnlineOrderNumber: "GlobalScope.TestToken~1.TestToken",
            OutDeliveryOnlineOrderStatus: ModuleBase.NOTEMPTY,
            InDeliveryTargetShipDate: this.newRecordsToCreate[0].record.InDeliveryTargetShipDate,
            InDeliveryRequiredDate: this.newRecordsToCreate[0].record.InDeliveryRequiredDate,
            InDeliveryRequiredTime: this.newRecordsToCreate[0].record.InDeliveryRequiredTime,
            InDeliveryToContact: this.newRecordsToCreate[0].record.InDeliveryToContact.toUpperCase(),
            InDeliveryToContactPhone: TestUtils.formattedPhone(this.newRecordsToCreate[0].record.InDeliveryToContactPhone),
            InDeliveryCarrierId: ModuleBase.NOTEMPTY,
            InDeliveryShipViaId: ModuleBase.NOTEMPTY,
            InDeliveryFreightTrackingNumber: "GlobalScope.TestToken~1.TestToken",
            InDeliveryToLocation: this.newRecordsToCreate[0].record.InDeliveryToLocation.toUpperCase(),
            InDeliveryToAttention: this.newRecordsToCreate[0].record.InDeliveryToAttention.toUpperCase(),
            InDeliveryToAddress1: this.newRecordsToCreate[0].record.InDeliveryToAddress1.toUpperCase(),
            InDeliveryToAddress2: this.newRecordsToCreate[0].record.InDeliveryToAddress2.toUpperCase(),
            InDeliveryToCity: this.newRecordsToCreate[0].record.InDeliveryToCity.toUpperCase(),
            InDeliveryToState: this.newRecordsToCreate[0].record.InDeliveryToState.toUpperCase(),
            InDeliveryToZipCode: this.newRecordsToCreate[0].record.InDeliveryToZipCode.toUpperCase(),
            InDeliveryToCountry: ModuleBase.NOTEMPTY,
            InDeliveryToCrossStreets: this.newRecordsToCreate[0].record.InDeliveryToCrossStreets,
            InDeliveryDeliveryNotes: this.newRecordsToCreate[0].record.InDeliveryDeliveryNotes,

            //HasFacilitiesItem: false,
            //HasLaborItem: true,
            //HasLossAndDamageItem: false,
            //HasMiscellaneousItem: false,
            //HasNotes: false,
            //HasRentalItem: true,
            //HasRentalSaleItem: false,
            //HasRepair: false,
            //HasSalesItem: true,



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
        this.moduleGroupName = 'Agent';
        this.moduleId = 'InSfo1f2lbFV';
        this.moduleCaption = 'Customer';
        let contactGrid: GridBase = new GridBase("Contact Grid", "CompanyContactGrid");
        //taxgrid
        let resaleGrid: GridBase = new GridBase("Resale Grid", "CompanyResaleGrid");
        let noteGrid: GridBase = new GridBase("Note Grid", "CustomerNoteGrid");
        this.grids.push(contactGrid);
        this.grids.push(resaleGrid);
        this.grids.push(noteGrid);


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
                },
                attemptDuplicate: true,
                editRecord: {
                    record: {
                        Address1: "GlobalScope.TestToken~1.TestToken",
                    },
                    seekObject: {
                        Customer: "GlobalScope.TestToken~1.TestToken",
                    },
                    recordToExpect: {
                        Address1: "GlobalScope.TestToken~1.TestToken",
                    },
                },
            },
            {
                record: {
                    Customer: "",
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
                expectedErrorFields: ["Customer"],
            },
            {
                record: {
                    Customer: TestUtils.randomCompanyName() + " GlobalScope.TestToken~1.TestToken",
                    CustomerNumber: "",
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
                expectedErrorFields: ["CustomerNumber"],
            },
            {
                record: {
                    Customer: TestUtils.randomCompanyName() + " GlobalScope.TestToken~1.TestToken",
                    CustomerNumber: "GlobalScope.TestToken~1.TestToken",
                    OfficeLocationId: 0,
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
                expectedErrorFields: ["OfficeLocationId"],
            },
            {
                record: {
                    Customer: TestUtils.randomCompanyName() + " GlobalScope.TestToken~1.TestToken",
                    CustomerNumber: "GlobalScope.TestToken~1.TestToken",
                    CustomerStatus: "",
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
                expectedErrorFields: ["CustomerStatusId"],
            },
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
                    CustomerType: "",
                    CreditStatusId: 1
                },
                expectedErrorFields: ["CustomerTypeId"],
            },
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
                    CreditStatus: "",
                },
                expectedErrorFields: ["CreditStatusId"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: contactGrid,
                recordToCreate: {
                    record: {
                        ContactId: 1,
                        ContactTitleId: 1,
                        ActiveDate: TestUtils.randomRecentDateMDY(30, ""),
                    },
                    editRecord: {
                        record: {
                            ActiveDate: "01/01/2001",
                        },
                        recordToExpect: {
                            ActiveDate: "01/01/2001",
                        },
                    },
                }
            },
            {
                grid: contactGrid,
                recordToCreate: {
                    record: {
                        ContactId: 1,
                        ContactTitleId: 0,
                    },
                    expectedErrorFields: ["ContactTitleId"],
                }
            },
            {
                grid: resaleGrid,
                recordToCreate: {
                    record: {
                        StateId: 1,
                        ResaleNumber: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            ResaleNumber: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            ResaleNumber: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
            {
                grid: resaleGrid,
                recordToCreate: {
                    record: {
                        ResaleNumber: "GlobalScope.TestToken~1.TestToken",
                    },
                    expectedErrorFields: ["StateId"],
                },
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        Description: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            Description: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            Description: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
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
        this.moduleGroupName = 'Agent';
        this.moduleId = '8WdRib388fFF';
        this.moduleCaption = 'Deal';
        let contactGrid: GridBase = new GridBase("Contact Grid", "CompanyContactGrid");
        //taxgrid
        let resaleGrid: GridBase = new GridBase("Resale Grid", "CompanyResaleGrid");
        let shipperGrid: GridBase = new GridBase("Shipper Grid", "DealShipperGrid");
        let noteGrid: GridBase = new GridBase("Note Grid", "DealNoteGrid");
        this.grids.push(contactGrid);
        this.grids.push(resaleGrid);
        this.grids.push(shipperGrid);
        this.grids.push(noteGrid);

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
                    DealTypeId: 1,
                    UseCustomerTax: false,
                    Address2: TestUtils.randomAddress2(),
                    Fax: TestUtils.randomPhone(),
                },
                seekObject: {
                    Deal: "GlobalScope.TestToken~1.TestToken",
                },
                attemptDuplicate: true,
                editRecord: {
                    record: {
                        Address1: "GlobalScope.TestToken~1.TestToken",
                    },
                    seekObject: {
                        Deal: "GlobalScope.TestToken~1.TestToken",
                    },
                    recordToExpect: {
                        Address1: "GlobalScope.TestToken~1.TestToken",
                    },
                }
            },
            {
                record: {
                    Deal: TestUtils.randomCompanyName() + " GlobalScope.TestToken~1.TestToken",
                    DealNumber: "GlobalScope.TestToken~1.TestToken",
                    Address2: TestUtils.randomAddress2(),
                    Fax: TestUtils.randomPhone(),
                    DealTypeId: 1,
                    UseCustomerTax: false
                },
                expectedErrorFields: ["CustomerId"],
            },
            {
                record: {
                    Deal: TestUtils.randomCompanyName() + " GlobalScope.TestToken~1.TestToken",
                    DealNumber: "GlobalScope.TestToken~1.TestToken",
                    CustomerId: 1,
                    Address2: TestUtils.randomAddress2(),
                    Fax: TestUtils.randomPhone(),
                    UseCustomerTax: false
                },
                expectedErrorFields: ["DealTypeId"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: contactGrid,
                recordToCreate: {
                    record: {
                        ContactId: 1,
                        ContactTitleId: 1,
                        ActiveDate: TestUtils.randomRecentDateMDY(30, ""),
                    },
                    editRecord: {
                        record: {
                            ActiveDate: "01/01/2001",
                        },
                        recordToExpect: {
                            ActiveDate: "01/01/2001",
                        },
                    },
                },
            },
            {
                grid: contactGrid,
                recordToCreate: {
                    record: {
                        ContactId: 1,
                        ContactTitleId: 0,
                    },
                    expectedErrorFields: ["ContactTitleId"],
                }
            },
            {
                grid: resaleGrid,
                recordToCreate: {
                    record: {
                        StateId: 1,
                        ResaleNumber: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            ResaleNumber: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            ResaleNumber: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
            {
                grid: resaleGrid,
                recordToCreate: {
                    record: {
                        ResaleNumber: "GlobalScope.TestToken~1.TestToken",
                    },
                    expectedErrorFields: ["StateId"],
                },
            },
            {
                grid: shipperGrid,
                recordToCreate: {
                    record: {
                        CarrierId: 1,
                        ShipViaId: 1,
                        ShipperAcct: "GlobalScope.TestToken~1.MediumTestToken",
                    },
                    editRecord: {
                        record: {
                            ShipperAcct: "GlobalScope.TestToken~1.ShortTestToken",
                        },
                        recordToExpect: {
                            ShipperAcct: "GlobalScope.TestToken~1.ShortTestToken",
                        },
                    },
                },
            },
            {
                grid: shipperGrid,
                recordToCreate: {
                    record: {
                        ShipperAcct: "GlobalScope.TestToken~1.MediumTestToken",
                    },
                    expectedErrorFields: ["CarrierId", "ShipViaId"],
                },
            },
            {
                grid: shipperGrid,
                recordToCreate: {
                    record: {
                        CarrierId: 1,
                        ShipperAcct: "GlobalScope.TestToken~1.MediumTestToken",
                    },
                    expectedErrorFields: ["ShipViaId"],
                },
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        Description: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            Description: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            Description: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
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
        this.moduleGroupName = 'Agent';
        this.moduleId = 'cwytGLEcUzJdn';
        this.moduleCaption = 'Vendor';
        let taxGrid: GridBase = new GridBase("Location Tax Grid", "CompanyTaxOptionGrid");
        taxGrid.canNew = false;
        taxGrid.canDelete = false;
        let contactGrid: GridBase = new GridBase("Contact Grid", "CompanyContactGrid");
        let noteGrid: GridBase = new GridBase("Note Grid", "VendorNoteGrid");
        //this.grids.push(taxGrid);  //todo #1410
        this.grids.push(contactGrid);
        this.grids.push(noteGrid);

        this.defaultNewRecordToExpect = {
            OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
        };

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
                },
                //attemptDuplicate: true,
                editRecord: {
                    record: {
                        Address2: "ABCD",
                    },
                    seekObject: {
                        VendorDisplayName: "GlobalScope.TestToken~1.TestToken",
                    },
                    recordToExpect: {
                        Address2: "ABCD",
                    },
                },
            },
            {
                record: {
                    VendorNumber: TestUtils.randomAlphanumeric(8),
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
                expectedErrorFields: ["Vendor"],
            },
            {
                record: {
                    Vendor: TestUtils.randomCompanyName(),
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
                expectedErrorFields: ["VendorNumber"],
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: contactGrid,
                recordToCreate: {
                    record: {
                        ContactId: 1,
                        ContactTitleId: 1,
                        ActiveDate: TestUtils.randomRecentDateMDY(30, ""),
                    },
                    editRecord: {
                        record: {
                            ActiveDate: "01/01/2001",
                        },
                        recordToExpect: {
                            ActiveDate: "01/01/2001",
                        },
                    },
                },
            },
            {
                grid: taxGrid,
                recordToEdit: {
                    seekObject: {
                        Location: "GlobalScope.User~ME.OfficeLocation",
                    },
                    record: {
                        TaxOptionId: TestUtils.randomIntegerBetween(1, 5),
                    },
                }
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        Description: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            Description: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            Description: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
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
        this.moduleGroupName = 'Agent';
        this.moduleId = '9ykTwUXTet46';
        this.moduleCaption = 'Contact';
        let companyGrid: GridBase = new GridBase("Company Grid", "ContactCompanyGrid");
        let eventGrid: GridBase = new GridBase("Personal Event Grid", "ContactPersonalEventGrid");
        let noteGrid: GridBase = new GridBase("Note Grid", "ContactNoteGrid");
        this.grids.push(companyGrid);
        this.grids.push(eventGrid);
        this.grids.push(noteGrid);

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
                editRecord: {
                    record: {
                        Address1: "GlobalScope.TestToken~1.TestToken",
                    },
                    seekObject: {
                        LastName: "GlobalScope.TestToken~1.TestToken",
                    },
                    recordToExpect: {
                        Address1: "GlobalScope.TestToken~1.TestToken",
                    },
                }
            }
        ];
        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: companyGrid,
                recordToCreate: {
                    record: {
                        CompanyId: 5,
                        ContactTitleId: 4,
                        OfficePhone: TestUtils.randomPhone(),
                        ActiveDate: TestUtils.randomRecentDateMDY(30, ""),
                    },
                    editRecord: {
                        record: {
                            ActiveDate: "01/01/2001",
                        },
                        recordToExpect: {
                            ActiveDate: "01/01/2001",
                        },
                    },
                },
            },
            {
                grid: companyGrid,
                recordToCreate: {
                    record: {
                        CompanyId: 5,
                        ContactTitleId: 0,
                        OfficePhone: TestUtils.randomPhone(),
                    },
                    expectedErrorFields: ["ContactTitleId"],
                }
            },
            {
                grid: eventGrid,
                recordToCreate: {
                    record: {
                        ContactEventId: 1,
                        EventDate: TestUtils.randomRecentDateMDY(30, ""),
                    },
                    editRecord: {
                        record: {
                            EventDate: "01/01/2001",
                        },
                        recordToExpect: {
                            EventDate: "01/01/2001",
                        },
                    },
                },
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        Description: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            Description: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            Description: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
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
    rentalGrid: GridBase;
    salesGrid: GridBase;
    partsGrid: GridBase;
    miscGrid: GridBase;
    laborGrid: GridBase;
    noteGrid: GridBase;
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PurchaseOrder';
        this.moduleGroupName = 'Agent';
        this.moduleId = '9a0xOMvBM7Uh9';
        this.moduleCaption = 'Purchase Order';
        this.canDelete = false;
        this.rentalGrid = new GridBase("Rental Inventory Grid", "OrderItemGrid", ["R", "purchase"]);
        this.rentalGrid.waitAfterSavingToReloadGrid = 1500;
        this.rentalGrid.multiRowSave = true;
        this.salesGrid = new GridBase("Sales Inventory Grid", "OrderItemGrid", ["S", "purchase"]);
        this.salesGrid.waitAfterSavingToReloadGrid = 1500;
        this.salesGrid.multiRowSave = true;
        this.partsGrid = new GridBase("Parts Inventory Grid", "OrderItemGrid", ["P", "purchase"]);
        this.partsGrid.waitAfterSavingToReloadGrid = 1500;
        this.partsGrid.multiRowSave = true;
        this.miscGrid = new GridBase("Miscellaneous Items Grid", "OrderItemGrid", ["M", "purchase"]);
        this.miscGrid.waitAfterSavingToReloadGrid = 1500;
        this.miscGrid.multiRowSave = true;
        this.laborGrid = new GridBase("Labor Items Grid", "OrderItemGrid", ["L", "purchase"]);
        this.laborGrid.waitAfterSavingToReloadGrid = 1500;
        this.laborGrid.multiRowSave = true;
        //let contactGrid: GridBase = new GridBase("OrderContactGrid");
        this.noteGrid = new GridBase("Note Grid", "OrderNoteGrid");
        this.grids.push(this.rentalGrid);
        this.grids.push(this.salesGrid);
        this.grids.push(this.partsGrid);
        this.grids.push(this.miscGrid);
        this.grids.push(this.laborGrid);
        //this.grids.push(contactGrid);
        this.grids.push(this.noteGrid);

        this.newRecordsToCreate = [
            {
                record: {
                    VendorId: 2,
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Parts: true,
                    Miscellaneous: true,
                    Labor: true,
                    RateType: 2,
                    CurrencyCode: "USD",
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                }

            }
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: this.rentalGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: this.salesGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: this.partsGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: this.miscGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            {
                grid: this.laborGrid,
                recordToCreate: {
                    record: {
                        InventoryId: TestUtils.randomIntegerBetween(1, 3),
                        QuantityOrdered: TestUtils.randomIntegerBetween(1, 3).toString(),
                    },
                    editRecord: {
                        record: {
                            QuantityOrdered: "5",
                        },
                        recordToExpect: {
                            QuantityOrdered: "5",
                        },
                    },
                },
            },
            //{
            //    grid: contactGrid,
            //    recordToCreate: {
            //        record: {
            //            ContactId: 1,
            //            ContactTitleId: 1,
            //        }
            //    }
            //},
            {
                grid: this.noteGrid,
                recordToCreate: {
                    record: {
                        NotesDescription: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            NotesDescription: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            NotesDescription: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
        ];


        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            PurchaseOrderNumber: ModuleBase.NOTEMPTY,
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
        this.moduleGroupName = 'Agent';
        this.moduleId = 'k7bYJRoHkf9Jr';
        this.moduleCaption = 'Project';
        this.canDelete = false;
        let contactGrid: GridBase = new GridBase("Contact Grid", "ProjectContactGrid");
        let poApproverGrid: GridBase = new GridBase("PO Approver Grid", "POApproverGrid");
        let noteGrid: GridBase = new GridBase("Note Grid", "ProjectNoteGrid");
        this.grids.push(contactGrid);
        this.grids.push(poApproverGrid);
        this.grids.push(noteGrid);

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

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: contactGrid,
                recordToCreate: {
                    record: {
                        ContactId: 1,
                        ContactTitleId: 1,
                    },
                    editRecord: {
                        record: {
                            OfficeExtension: "GlobalScope.TestToken~1.ShortTestToken",
                        },
                        recordToExpect: {
                            OfficeExtension: "GlobalScope.TestToken~1.ShortTestToken",
                        },
                    },
                },
            },
            {
                grid: poApproverGrid,
                recordToCreate: {
                    record: {
                        UsersId: 1,
                        AppRoleId: 1,
                    },
                    editRecord: {
                        record: {
                            IsBackup: true,
                        },
                        recordToExpect: {
                            IsBackup: true,
                        },
                    },
                },
            },
            {
                grid: noteGrid,
                recordToCreate: {
                    record: {
                        NotesDescription: "GlobalScope.TestToken~1.TestToken",
                    },
                    editRecord: {
                        record: {
                            NotesDescription: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                        recordToExpect: {
                            NotesDescription: "GlobalScope.TestToken~1.MediumTestToken",
                        },
                    },
                },
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            //Deal: this.newRecordsToCreate[0].record.Deal.toUpperCase(),
            Project: this.newRecordsToCreate[0].record.Project.toUpperCase(),
            ProjectNumber: ModuleBase.NOTEMPTY,
            ProjectDescription: this.newRecordsToCreate[0].record.ProjectDescription,
        }

    }
    //---------------------------------------------------------------------------------------
}

//---------------------------------------------------------------------------------------
export class InventoryModule extends HomeModule { }
//---------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------
export class RentalInventory extends InventoryModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'RentalInventory';
        this.moduleGroupName = 'Inventory';
        this.moduleId = '3ICuf6pSeBh6G';
        this.moduleCaption = 'Rental Inventory';
        let whInvGrid: GridBase = new GridBase("Warehouse Inventory Grid", "RentalInventoryWarehouseGrid");
        whInvGrid.canNew = false;
        whInvGrid.canDelete = false;

        let akaGrid: GridBase = new GridBase("AKA Grid", "AlternativeDescriptionGrid");

        let completeKitGrid: GridBase = new GridBase("Complete/Kit Grid", "InventoryCompleteKitGrid");
        completeKitGrid.canNew = false;
        completeKitGrid.canEdit = false;
        completeKitGrid.canDelete = false;

        let substituteGrid: GridBase = new GridBase("Substitute Grid", "RentalInventorySubstituteGrid");
        let compatibilityGrid: GridBase = new GridBase("Compatibility Grid", "RentalInventoryCompatibilityGrid");

        let purchaseVendorGrid: GridBase = new GridBase("Purchase Vendor Grid", "PurchaseVendorGrid");
        purchaseVendorGrid.canNew = false;
        purchaseVendorGrid.canEdit = false;
        purchaseVendorGrid.canDelete = false;

        let prepGrid: GridBase = new GridBase("Prep Grid", "InventoryPrepGrid");
        let attributeGrid: GridBase = new GridBase("Attribute Grid", "InventoryAttributeValueGrid");
        let taxGrid: GridBase = new GridBase("Tax Grid", "InventoryLocationTaxGrid");
        taxGrid.canNew = false;
        taxGrid.canDelete = false;

        this.grids.push(whInvGrid);
        this.grids.push(akaGrid);
        this.grids.push(completeKitGrid);
        this.grids.push(substituteGrid);
        this.grids.push(compatibilityGrid);
        this.grids.push(purchaseVendorGrid);
        this.grids.push(prepGrid);
        this.grids.push(attributeGrid);
        this.grids.push(taxGrid);

        this.defaultNewRecordToExpect = {
            ICode: "",
            Description: "",
            Unit: "GlobalScope.DefaultSettings~1.DefaultUnit",   // ie. "EA"
        }

        this.newRecordsToCreate = [
            {
                record: {
                    //inventory tab
                    ICode: "GlobalScope.RentalInventory~NEWICODE.newICode",
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                    TrackedBy: "QUANTITY",
                    NonDiscountable: TestUtils.randomBoolean(),
                    IsHazardousMaterial: TestUtils.randomBoolean(),
                    IsFixedAsset: TestUtils.randomBoolean(),
                    ExcludeImageFromQuoteOrderPrint: TestUtils.randomBoolean(),

                    //availability tab
                    NoAvailabilityCheck: TestUtils.randomBoolean(),
                    //SendAvailabilityAlert: TestUtils.randomBoolean(),
                    //AvailabilityManuallyResolveConflicts: TestUtils.randomBoolean(),

                    //completes/kits
                    DisplayInSummaryModeWhenRateIsZero: TestUtils.randomBoolean(),

                    //weights and dims
                    PrimaryDimensionDescription: "GlobalScope.TestToken~1.TestToken PRIMARY",
                    PrimaryDimensionHeightCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionHeightFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionHeightIn: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionHeightM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionLengthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionLengthFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionLengthIn: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionLengthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWidthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionWidthFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionWidthIn: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWidthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionDescription: "GlobalScope.TestToken~1.TestToken SECONDARY",
                    SecondaryDimensionHeightCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    CountryOfOriginId: TestUtils.randomIntegerBetween(1, 5),

                    //prep/qc
                    //QcRequired: TestUtils.randomBoolean(),
                    QcTime: "00:05",

                    //attribute / usage
                    TrackAssetUsage: TestUtils.randomBoolean(),
                    TrackLampUsage: TestUtils.randomBoolean(),
                    TrackStrikes: TestUtils.randomBoolean(),
                    TrackCandles: TestUtils.randomBoolean(),
                    LampCount: TestUtils.randomIntegerBetween(1, 4),
                    MinimumFootCandles: TestUtils.randomIntegerBetween(1, 20).toString(),
                    TrackSoftware: true,
                    SoftwareEffectiveDate: TestUtils.randomRecentDateMDY(60),
                    SoftwareVersion: "GlobalScope.TestToken~1.MediumTestToken",
                    CopyAttributesAsNote: TestUtils.randomBoolean(),

                    //notes
                    Note: TestUtils.randomLoremWords(20),
                    AutoCopyNotesToQuoteOrder: TestUtils.randomBoolean(),
                    PrintNoteOnInContract: TestUtils.randomBoolean(),
                    PrintNoteOnInvoice: TestUtils.randomBoolean(),
                    PrintNoteOnOrder: TestUtils.randomBoolean(),
                    PrintNoteOnOutContract: TestUtils.randomBoolean(),
                    PrintNoteOnPO: TestUtils.randomBoolean(),
                    PrintNoteOnPickList: TestUtils.randomBoolean(),
                    PrintNoteOnPoReceiveList: TestUtils.randomBoolean(),
                    PrintNoteOnPoReturnList: TestUtils.randomBoolean(),
                    PrintNoteOnQuote: TestUtils.randomBoolean(),
                    PrintNoteOnReceiveContract: TestUtils.randomBoolean(),
                    PrintNoteOnReturnContract: TestUtils.randomBoolean(),
                    PrintNoteOnReturnList: TestUtils.randomBoolean(),
                    WebDetailedDescription: TestUtils.randomLoremWords(20),

                    //profit/loss
                    OverrideProfitAndLossCategory: true,
                    ProfitAndLossCategoryId: 1,
                    //ExcludeFromReturnOnAsset: TestUtils.randomBoolean(),

                    //g/l accounts
                    CostOfGoodsRentedExpenseAccountId: TestUtils.randomIntegerBetween(1, 5),
                    CostOfGoodsSoldExpenseAccountId: TestUtils.randomIntegerBetween(1, 5),
                    EquipmentSaleIncomeAccountId: TestUtils.randomIntegerBetween(1, 5),
                    IncomeAccountId: TestUtils.randomIntegerBetween(1, 5),
                    //ExpenseAccountId: TestUtils.randomIntegerBetween(1, 5),
                    LdIncomeAccountId: TestUtils.randomIntegerBetween(1, 5),
                    SubIncomeAccountId: TestUtils.randomIntegerBetween(1, 5),
                    AssetAccountId: TestUtils.randomIntegerBetween(1, 5),


                    /*
     
    AllocateRevenueForAccessories: false
    AutomaticallyRebuildContainerAtCheckIn: false
    AutomaticallyRebuildContainerAtTransferIn: false
    CleaningFeeAmount: 0
    ContainerId: ""
    ContainerPackingListBehavior: ""
    ContainerScannableDescription: ""
    ContainerScannableICode: ""
    ContainerScannableInventoryId: ""
    ContainerStagingRule: ""
    Dyed: false
    ExcludeContainedItemsFromAvailability: false
    ExcludeImageFromQuoteOrderPrint: false
    Gender: ""
    GenderId: ""
    IsHazardousMaterial: false
    Label: ""
    LabelId: ""
    Manufacturer: ""
    ManufacturerId: ""
    ManufacturerPartNumber: ""
    ManufacturerUrl: ""
    Material: ""
    MaterialId: ""
    NonDiscountable: false
    OverrideSystemDefaultRevenueAllocationBehavior: false
    PackagePrice: ""
    PackageRevenueCalculationFormula: ""
    Pattern: ""
    PatternId: ""
    Period: ""
    PeriodId: ""
    SeparatePackageOnQuoteOrder: false
    UseContainerNumber: false
    WardrobeCare: ""
    WardrobeCareId: ""
    WardrobeDetailedDescription: ""
    WardrobePieceCount: 0
    WardrobeSize: ""
    WardrobeSource: ""
    WardrobeSourceId: ""
    WarehouseSpecificPackage: false
                    */

                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];

        let aka1: string = TestUtils.randomAlphanumeric(20).toUpperCase();
        let aka2: string = TestUtils.randomAlphanumeric(20).toUpperCase();

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: akaGrid,
                recordToCreate: {
                    record: {
                        AKA: aka1,
                    },
                    seekObject: {
                        AKA: aka1,
                    },
                    attemptDuplicate: true,
                    //editRecord: {
                    //    seekObject: {
                    //        AKA: aka1,
                    //    },
                    //    record: {
                    //        AKA: aka2,
                    //    },
                    //    recordToExpect: {
                    //        AKA: aka2,
                    //    },
                    //},
                },
            },
            {
                grid: substituteGrid,
                recordToCreate: {
                    record: {
                        SubstituteInventoryId: TestUtils.randomIntegerBetween(1, 3),
                    },
                    editRecord: {
                        record: {
                            SubstituteInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        },
                        //recordToExpect: {
                        //    SubstituteInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        //},
                    },
                },
            },
            // can't test this scenario because the grid save is cancelled when all fields are blank
            //{
            //    grid: substituteGrid,
            //    recordToCreate: {
            //        record: {
            //        },
            //        expectedErrorFields: ["SubstituteInventoryId"],
            //    },
            //},
            {
                grid: compatibilityGrid,
                recordToCreate: {
                    record: {
                        CompatibleWithInventoryId: TestUtils.randomIntegerBetween(1, 3),
                    },
                    editRecord: {
                        record: {
                            CompatibleWithInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        },
                        //recordToExpect: {
                        //    CompatibleWithInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        //},
                    },
                },
            },
            {
                grid: prepGrid,
                recordToCreate: {
                    record: {
                        PrepRateId: 1,
                        //PrepRate: "25.25",
                        PrepRate: TestUtils.randomIntegerBetween(10, 99) + "." + TestUtils.randomIntegerBetween(10, 99),
                        PrepTime: "1",
                    },
                    editRecord: {
                        record: {
                            PrepRate: "50.50",
                        },
                        recordToExpect: {
                            PrepRate: "50.50",
                        },
                    },
                },
            },
            {
                grid: attributeGrid,
                recordToCreate: {
                    record: {
                        AttributeId: 1,
                        AttributeValueId: 1,
                    },
                    editRecord: {
                        record: {
                            AttributeValueId: 2,
                        },
                        //recordToExpect: {
                        //    AttributeValueId: 2,
                        //},
                    },
                },
            },
            {
                grid: taxGrid,
                recordToEdit: {
                    seekObject: {
                        Location: "GlobalScope.User~ME.OfficeLocation",
                    },
                    record: {
                        Taxable: false,
                    },
                },
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            //inventory tab
            ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            ManufacturerPartNumber: this.newRecordsToCreate[0].record.ManufacturerPartNumber.toUpperCase(),
            TrackedBy: this.newRecordsToCreate[0].record.TrackedBy.toUpperCase(),
            NonDiscountable: this.newRecordsToCreate[0].record.NonDiscountable,
            IsHazardousMaterial: this.newRecordsToCreate[0].record.IsHazardousMaterial,
            IsFixedAsset: this.newRecordsToCreate[0].record.IsFixedAsset,
            InventoryType: ModuleBase.NOTEMPTY,
            Category: ModuleBase.NOTEMPTY,
            Unit: ModuleBase.NOTEMPTY,
            Rank: ModuleBase.NOTEMPTY,
            ExcludeImageFromQuoteOrderPrint: this.newRecordsToCreate[0].record.ExcludeImageFromQuoteOrderPrint,

            //availability tab
            NoAvailabilityCheck: this.newRecordsToCreate[0].record.NoAvailabilityCheck,
            //SendAvailabilityAlert: this.newRecordsToCreate[0].record.SendAvailabilityAlert,
            //AvailabilityManuallyResolveConflicts: this.newRecordsToCreate[0].record.AvailabilityManuallyResolveConflicts,

            //completes/kits
            DisplayInSummaryModeWhenRateIsZero: this.newRecordsToCreate[0].record.DisplayInSummaryModeWhenRateIsZero,

            //weights and dims
            PrimaryDimensionDescription: "GlobalScope.TestToken~1.TestToken PRIMARY",
            PrimaryDimensionHeightCm: this.newRecordsToCreate[0].record.PrimaryDimensionHeightCm.toUpperCase(),
            //PrimaryDimensionHeightFt: this.newRecordsToCreate[0].record.PrimaryDimensionHeightFt.toUpperCase(),
            //PrimaryDimensionHeightIn: this.newRecordsToCreate[0].record.PrimaryDimensionHeightIn.toUpperCase(),
            PrimaryDimensionHeightM: this.newRecordsToCreate[0].record.PrimaryDimensionHeightM.toUpperCase(),
            PrimaryDimensionLengthCm: this.newRecordsToCreate[0].record.PrimaryDimensionLengthCm.toUpperCase(),
            //PrimaryDimensionLengthFt: this.newRecordsToCreate[0].record.PrimaryDimensionLengthFt.toUpperCase(),
            //PrimaryDimensionLengthIn: this.newRecordsToCreate[0].record.PrimaryDimensionLengthIn.toUpperCase(),
            PrimaryDimensionLengthM: this.newRecordsToCreate[0].record.PrimaryDimensionLengthM.toUpperCase(),
            PrimaryDimensionShipWeightG: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightG.toUpperCase(),
            PrimaryDimensionShipWeightKg: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightKg.toUpperCase(),
            PrimaryDimensionShipWeightLbs: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightLbs.toUpperCase(),
            PrimaryDimensionShipWeightOz: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightOz.toUpperCase(),
            PrimaryDimensionWeightInCaseG: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseG.toUpperCase(),
            PrimaryDimensionWeightInCaseKg: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseKg.toUpperCase(),
            PrimaryDimensionWeightInCaseLbs: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseLbs.toUpperCase(),
            PrimaryDimensionWeightInCaseOz: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseOz.toUpperCase(),
            PrimaryDimensionWidthCm: this.newRecordsToCreate[0].record.PrimaryDimensionWidthCm.toUpperCase(),
            //PrimaryDimensionWidthFt: this.newRecordsToCreate[0].record.PrimaryDimensionWidthFt.toUpperCase(),
            //PrimaryDimensionWidthIn: this.newRecordsToCreate[0].record.PrimaryDimensionWidthIn.toUpperCase(),
            PrimaryDimensionWidthM: this.newRecordsToCreate[0].record.PrimaryDimensionWidthM.toUpperCase(),
            SecondaryDimensionDescription: "GlobalScope.TestToken~1.TestToken SECONDARY",
            SecondaryDimensionHeightCm: this.newRecordsToCreate[0].record.SecondaryDimensionHeightCm.toUpperCase(),
            SecondaryDimensionHeightFt: this.newRecordsToCreate[0].record.SecondaryDimensionHeightFt.toUpperCase(),
            SecondaryDimensionHeightIn: this.newRecordsToCreate[0].record.SecondaryDimensionHeightIn.toUpperCase(),
            SecondaryDimensionHeightM: this.newRecordsToCreate[0].record.SecondaryDimensionHeightM.toUpperCase(),
            SecondaryDimensionLengthCm: this.newRecordsToCreate[0].record.SecondaryDimensionLengthCm.toUpperCase(),
            SecondaryDimensionLengthFt: this.newRecordsToCreate[0].record.SecondaryDimensionLengthFt.toUpperCase(),
            SecondaryDimensionLengthIn: this.newRecordsToCreate[0].record.SecondaryDimensionLengthIn.toUpperCase(),
            SecondaryDimensionLengthM: this.newRecordsToCreate[0].record.SecondaryDimensionLengthM.toUpperCase(),
            SecondaryDimensionShipWeightG: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightG.toUpperCase(),
            SecondaryDimensionShipWeightKg: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightKg.toUpperCase(),
            SecondaryDimensionShipWeightLbs: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightLbs.toUpperCase(),
            SecondaryDimensionShipWeightOz: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightOz.toUpperCase(),
            SecondaryDimensionWeightInCaseG: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseG.toUpperCase(),
            SecondaryDimensionWeightInCaseKg: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseKg.toUpperCase(),
            SecondaryDimensionWeightInCaseLbs: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseLbs.toUpperCase(),
            SecondaryDimensionWeightInCaseOz: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseOz.toUpperCase(),
            SecondaryDimensionWidthCm: this.newRecordsToCreate[0].record.SecondaryDimensionWidthCm.toUpperCase(),
            SecondaryDimensionWidthFt: this.newRecordsToCreate[0].record.SecondaryDimensionWidthFt.toUpperCase(),
            SecondaryDimensionWidthIn: this.newRecordsToCreate[0].record.SecondaryDimensionWidthIn.toUpperCase(),
            SecondaryDimensionWidthM: this.newRecordsToCreate[0].record.SecondaryDimensionWidthM.toUpperCase(),
            CountryOfOrigin: ModuleBase.NOTEMPTY,

            //prep/qc
            //QcRequired: this.newRecordsToCreate[0].record.QcRequired,
            QcTime: this.newRecordsToCreate[0].record.QcTime.toUpperCase(),

            //attribute / usage
            TrackAssetUsage: this.newRecordsToCreate[0].record.TrackAssetUsage,
            TrackLampUsage: this.newRecordsToCreate[0].record.TrackLampUsage,
            TrackStrikes: this.newRecordsToCreate[0].record.TrackStrikes,
            TrackCandles: this.newRecordsToCreate[0].record.TrackCandles,
            //LampCount: TestUtils.randomIntegerBetween(1, 4),
            MinimumFootCandles: this.newRecordsToCreate[0].record.MinimumFootCandles,
            TrackSoftware: this.newRecordsToCreate[0].record.TrackSoftware,
            SoftwareEffectiveDate: this.newRecordsToCreate[0].record.SoftwareEffectiveDate,
            SoftwareVersion: "GlobalScope.TestToken~1.MediumTestToken",
            CopyAttributesAsNote: this.newRecordsToCreate[0].record.CopyAttributesAsNote,

            //notes
            Note: this.newRecordsToCreate[0].record.Note,
            AutoCopyNotesToQuoteOrder: this.newRecordsToCreate[0].record.AutoCopyNotesToQuoteOrder,
            PrintNoteOnInContract: this.newRecordsToCreate[0].record.PrintNoteOnInContract,
            PrintNoteOnInvoice: this.newRecordsToCreate[0].record.PrintNoteOnInvoice,
            PrintNoteOnOrder: this.newRecordsToCreate[0].record.PrintNoteOnOrder,
            PrintNoteOnOutContract: this.newRecordsToCreate[0].record.PrintNoteOnOutContract,
            PrintNoteOnPO: this.newRecordsToCreate[0].record.PrintNoteOnPO,
            PrintNoteOnPickList: this.newRecordsToCreate[0].record.PrintNoteOnPickList,
            PrintNoteOnPoReceiveList: this.newRecordsToCreate[0].record.PrintNoteOnPoReceiveList,
            PrintNoteOnPoReturnList: this.newRecordsToCreate[0].record.PrintNoteOnPoReturnList,
            PrintNoteOnQuote: this.newRecordsToCreate[0].record.PrintNoteOnQuote,
            PrintNoteOnReceiveContract: this.newRecordsToCreate[0].record.PrintNoteOnReceiveContract,
            PrintNoteOnReturnContract: this.newRecordsToCreate[0].record.PrintNoteOnReturnContract,
            PrintNoteOnReturnList: this.newRecordsToCreate[0].record.PrintNoteOnReturnList,
            WebDetailedDescription: this.newRecordsToCreate[0].record.WebDetailedDescription,

            //profit/loss
            OverrideProfitAndLossCategory: this.newRecordsToCreate[0].record.OverrideProfitAndLossCategory,
            ProfitAndLossCategory: ModuleBase.NOTEMPTY,
            //ExcludeFromReturnOnAsset: this.newRecordsToCreate[0].record.ExcludeFromReturnOnAsset,  // api not saving correctly

            //g/l accounts
            CostOfGoodsRentedExpenseAccountNo: ModuleBase.NOTEMPTY,
            CostOfGoodsRentedExpenseAccountDescriptption: ModuleBase.NOTEMPTY,
            CostOfGoodsSoldExpenseAccountNo: ModuleBase.NOTEMPTY,
            CostOfGoodsSoldExpenseAccountDescription: ModuleBase.NOTEMPTY,
            EquipmentSaleIncomeAccountNo: ModuleBase.NOTEMPTY,
            EquipmentSaleIncomeAccountDescription: ModuleBase.NOTEMPTY,
            IncomeAccountNo: ModuleBase.NOTEMPTY,
            IncomeAccountDescription: ModuleBase.NOTEMPTY,
            LdIncomeAccountNo: ModuleBase.NOTEMPTY,
            LdIncomeAccountDescription: ModuleBase.NOTEMPTY,
            SubIncomeAccountNo: ModuleBase.NOTEMPTY,
            SubIncomeAccountDescription: ModuleBase.NOTEMPTY,
            AssetAccountNo: ModuleBase.NOTEMPTY,
            AssetAccountDescription: ModuleBase.NOTEMPTY,





        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class SalesInventory extends InventoryModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'SalesInventory';
        this.moduleGroupName = 'Inventory';
        this.moduleId = 'ShjGAzM2Pq3kk';
        this.moduleCaption = 'Sales Inventory';

        let whInvGrid: GridBase = new GridBase("Warehouse Inventory Grid", "SalesInventoryWarehouseGrid");
        whInvGrid.canNew = false;
        whInvGrid.canDelete = false;

        let akaGrid: GridBase = new GridBase("AKA Grid", "AlternativeDescriptionGrid");

        let completeKitGrid: GridBase = new GridBase("Complete/Kit Grid", "InventoryCompleteKitGrid");
        completeKitGrid.canNew = false;
        completeKitGrid.canEdit = false;
        completeKitGrid.canDelete = false;

        let substituteGrid: GridBase = new GridBase("Substitute Grid", "SalesInventorySubstituteGrid");
        let compatibilityGrid: GridBase = new GridBase("Compatibility Grid", "SalesInventoryCompatibilityGrid");

        let purchaseVendorGrid: GridBase = new GridBase("Purchase Vendor Grid", "PurchaseVendorGrid");
        purchaseVendorGrid.canNew = false;
        purchaseVendorGrid.canEdit = false;
        purchaseVendorGrid.canDelete = false;

        let prepGrid: GridBase = new GridBase("Prep Grid", "InventoryPrepGrid");
        let attributeGrid: GridBase = new GridBase("Attribute Grid", "InventoryAttributeValueGrid");
        let taxGrid: GridBase = new GridBase("Tax Grid", "InventoryLocationTaxGrid");
        taxGrid.canNew = false;
        taxGrid.canDelete = false;

        this.grids.push(whInvGrid);
        this.grids.push(akaGrid);
        this.grids.push(completeKitGrid);
        this.grids.push(substituteGrid);
        this.grids.push(compatibilityGrid);
        this.grids.push(purchaseVendorGrid);
        this.grids.push(prepGrid);
        this.grids.push(attributeGrid);
        this.grids.push(taxGrid);

        this.defaultNewRecordToExpect = {
            Unit: "GlobalScope.DefaultSettings~1.DefaultUnit",   // ie. "EA"
        }

        this.newRecordsToCreate = [
            {
                record: {
                    //inventory tab
                    ICode: TestUtils.randomAlphanumeric(7),
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    TrackedBy: "QUANTITY",
                    NonDiscountable: TestUtils.randomBoolean(),
                    IsHazardousMaterial: TestUtils.randomBoolean(),
                    //IsFixedAsset: TestUtils.randomBoolean(),
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                    ExcludeImageFromQuoteOrderPrint: TestUtils.randomBoolean(),


                    //availability tab
                    NoAvailabilityCheck: TestUtils.randomBoolean(),
                    //SendAvailabilityAlert: TestUtils.randomBoolean(),
                    //AvailabilityManuallyResolveConflicts: TestUtils.randomBoolean(),

                    //completes/kits
                    DisplayInSummaryModeWhenRateIsZero: TestUtils.randomBoolean(),

                    //weights and dims
                    PrimaryDimensionDescription: "GlobalScope.TestToken~1.TestToken PRIMARY",
                    PrimaryDimensionHeightCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionHeightFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionHeightIn: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionHeightM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionLengthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionLengthFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionLengthIn: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionLengthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWidthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionWidthFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionWidthIn: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWidthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionDescription: "GlobalScope.TestToken~1.TestToken SECONDARY",
                    SecondaryDimensionHeightCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    CountryOfOriginId: TestUtils.randomIntegerBetween(1, 5),

                    //attribute / usage
                    CopyAttributesAsNote: TestUtils.randomBoolean(),

                    //notes
                    Note: TestUtils.randomLoremWords(20),
                    AutoCopyNotesToQuoteOrder: TestUtils.randomBoolean(),
                    PrintNoteOnInContract: TestUtils.randomBoolean(),
                    PrintNoteOnInvoice: TestUtils.randomBoolean(),
                    PrintNoteOnOrder: TestUtils.randomBoolean(),
                    PrintNoteOnOutContract: TestUtils.randomBoolean(),
                    PrintNoteOnPO: TestUtils.randomBoolean(),
                    PrintNoteOnPickList: TestUtils.randomBoolean(),
                    PrintNoteOnPoReceiveList: TestUtils.randomBoolean(),
                    PrintNoteOnPoReturnList: TestUtils.randomBoolean(),
                    PrintNoteOnQuote: TestUtils.randomBoolean(),
                    PrintNoteOnReceiveContract: TestUtils.randomBoolean(),
                    PrintNoteOnReturnContract: TestUtils.randomBoolean(),
                    PrintNoteOnReturnList: TestUtils.randomBoolean(),
                    WebDetailedDescription: TestUtils.randomLoremWords(20),

                    //profit/loss
                    OverrideProfitAndLossCategory: true,
                    ProfitAndLossCategoryId: 1,

                    //g/l accounts
                    AssetAccountId: TestUtils.randomIntegerBetween(1, 5),
                    IncomeAccountId: TestUtils.randomIntegerBetween(1, 5),
                    SubIncomeAccountId: TestUtils.randomIntegerBetween(1, 5),
                    CostOfGoodsSoldExpenseAccountId: TestUtils.randomIntegerBetween(1, 5),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];

        let aka1: string = TestUtils.randomAlphanumeric(20).toUpperCase();
        let aka2: string = TestUtils.randomAlphanumeric(20).toUpperCase();

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: akaGrid,
                recordToCreate: {
                    record: {
                        AKA: aka1,
                    },
                    seekObject: {
                        AKA: aka1,
                    },
                    attemptDuplicate: true,
                    //editRecord: {
                    //    seekObject: {
                    //        AKA: aka1,
                    //    },
                    //    record: {
                    //        AKA: aka2,
                    //    },
                    //    recordToExpect: {
                    //        AKA: aka2,
                    //    },
                    //},
                },
            },
            {
                grid: substituteGrid,
                recordToCreate: {
                    record: {
                        SubstituteInventoryId: 1,
                    },
                    editRecord: {
                        record: {
                            SubstituteInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        },
                        //recordToExpect: {
                        //    SubstituteInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        //},
                    },
                },
            },
            // can't test this scenario because the grid save is cancelled when all fields are blank
            //{
            //    grid: substituteGrid,
            //    recordToCreate: {
            //        record: {
            //        },
            //        expectedErrorFields: ["SubstituteInventoryId"],
            //    },
            //},
            {
                grid: compatibilityGrid,
                recordToCreate: {
                    record: {
                        CompatibleWithInventoryId: 1,
                    },
                    editRecord: {
                        record: {
                            CompatibleWithInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        },
                        //recordToExpect: {
                        //    CompatibleWithInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        //},
                    },
                },
            },
            {
                grid: prepGrid,
                recordToCreate: {
                    record: {
                        PrepRateId: 1,
                        //PrepRate: "25.25",
                        PrepRate: TestUtils.randomIntegerBetween(10, 99) + "." + TestUtils.randomIntegerBetween(10, 99),
                        PrepTime: "1",
                    },
                    editRecord: {
                        record: {
                            PrepRate: "50.50",
                        },
                        recordToExpect: {
                            PrepRate: "50.50",
                        },
                    },
                },
            },
            {
                grid: attributeGrid,
                recordToCreate: {
                    record: {
                        AttributeId: 1,
                        AttributeValueId: 1,
                    },
                    editRecord: {
                        record: {
                            AttributeValueId: 2,
                        },
                        //recordToExpect: {
                        //    AttributeValueId: 2,
                        //},
                    },
                },
            },
            {
                grid: taxGrid,
                recordToEdit: {
                    seekObject: {
                        Location: "GlobalScope.User~ME.OfficeLocation",
                    },
                    record: {
                        Taxable: false,
                    },
                },
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            //inventory tab
            ICode: this.newRecordsToCreate[0].record.ICode.toUpperCase(),
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            ManufacturerPartNumber: this.newRecordsToCreate[0].record.ManufacturerPartNumber.toUpperCase(),
            TrackedBy: this.newRecordsToCreate[0].record.TrackedBy.toUpperCase(),
            NonDiscountable: this.newRecordsToCreate[0].record.NonDiscountable,
            IsHazardousMaterial: this.newRecordsToCreate[0].record.IsHazardousMaterial,
            //IsFixedAsset: this.newRecordsToCreate[0].record.IsFixedAsset,
            InventoryType: ModuleBase.NOTEMPTY,
            Category: ModuleBase.NOTEMPTY,
            Unit: ModuleBase.NOTEMPTY,
            Rank: ModuleBase.NOTEMPTY,
            ExcludeImageFromQuoteOrderPrint: this.newRecordsToCreate[0].record.ExcludeImageFromQuoteOrderPrint,

            //availability tab
            NoAvailabilityCheck: this.newRecordsToCreate[0].record.NoAvailabilityCheck,
            //SendAvailabilityAlert: this.newRecordsToCreate[0].record.SendAvailabilityAlert,
            //AvailabilityManuallyResolveConflicts: this.newRecordsToCreate[0].record.AvailabilityManuallyResolveConflicts,

            //completes/kits
            DisplayInSummaryModeWhenRateIsZero: this.newRecordsToCreate[0].record.DisplayInSummaryModeWhenRateIsZero,

            //weights and dims
            PrimaryDimensionDescription: "GlobalScope.TestToken~1.TestToken PRIMARY",
            PrimaryDimensionHeightCm: this.newRecordsToCreate[0].record.PrimaryDimensionHeightCm.toUpperCase(),
            //PrimaryDimensionHeightFt: this.newRecordsToCreate[0].record.PrimaryDimensionHeightFt.toUpperCase(),
            //PrimaryDimensionHeightIn: this.newRecordsToCreate[0].record.PrimaryDimensionHeightIn.toUpperCase(),
            PrimaryDimensionHeightM: this.newRecordsToCreate[0].record.PrimaryDimensionHeightM.toUpperCase(),
            PrimaryDimensionLengthCm: this.newRecordsToCreate[0].record.PrimaryDimensionLengthCm.toUpperCase(),
            //PrimaryDimensionLengthFt: this.newRecordsToCreate[0].record.PrimaryDimensionLengthFt.toUpperCase(),
            //PrimaryDimensionLengthIn: this.newRecordsToCreate[0].record.PrimaryDimensionLengthIn.toUpperCase(),
            PrimaryDimensionLengthM: this.newRecordsToCreate[0].record.PrimaryDimensionLengthM.toUpperCase(),
            PrimaryDimensionShipWeightG: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightG.toUpperCase(),
            PrimaryDimensionShipWeightKg: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightKg.toUpperCase(),
            PrimaryDimensionShipWeightLbs: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightLbs.toUpperCase(),
            PrimaryDimensionShipWeightOz: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightOz.toUpperCase(),
            PrimaryDimensionWeightInCaseG: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseG.toUpperCase(),
            PrimaryDimensionWeightInCaseKg: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseKg.toUpperCase(),
            PrimaryDimensionWeightInCaseLbs: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseLbs.toUpperCase(),
            PrimaryDimensionWeightInCaseOz: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseOz.toUpperCase(),
            PrimaryDimensionWidthCm: this.newRecordsToCreate[0].record.PrimaryDimensionWidthCm.toUpperCase(),
            //PrimaryDimensionWidthFt: this.newRecordsToCreate[0].record.PrimaryDimensionWidthFt.toUpperCase(),
            //PrimaryDimensionWidthIn: this.newRecordsToCreate[0].record.PrimaryDimensionWidthIn.toUpperCase(),
            PrimaryDimensionWidthM: this.newRecordsToCreate[0].record.PrimaryDimensionWidthM.toUpperCase(),
            SecondaryDimensionDescription: "GlobalScope.TestToken~1.TestToken SECONDARY",
            SecondaryDimensionHeightCm: this.newRecordsToCreate[0].record.SecondaryDimensionHeightCm.toUpperCase(),
            SecondaryDimensionHeightFt: this.newRecordsToCreate[0].record.SecondaryDimensionHeightFt.toUpperCase(),
            SecondaryDimensionHeightIn: this.newRecordsToCreate[0].record.SecondaryDimensionHeightIn.toUpperCase(),
            SecondaryDimensionHeightM: this.newRecordsToCreate[0].record.SecondaryDimensionHeightM.toUpperCase(),
            SecondaryDimensionLengthCm: this.newRecordsToCreate[0].record.SecondaryDimensionLengthCm.toUpperCase(),
            SecondaryDimensionLengthFt: this.newRecordsToCreate[0].record.SecondaryDimensionLengthFt.toUpperCase(),
            SecondaryDimensionLengthIn: this.newRecordsToCreate[0].record.SecondaryDimensionLengthIn.toUpperCase(),
            SecondaryDimensionLengthM: this.newRecordsToCreate[0].record.SecondaryDimensionLengthM.toUpperCase(),
            SecondaryDimensionShipWeightG: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightG.toUpperCase(),
            SecondaryDimensionShipWeightKg: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightKg.toUpperCase(),
            SecondaryDimensionShipWeightLbs: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightLbs.toUpperCase(),
            SecondaryDimensionShipWeightOz: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightOz.toUpperCase(),
            SecondaryDimensionWeightInCaseG: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseG.toUpperCase(),
            SecondaryDimensionWeightInCaseKg: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseKg.toUpperCase(),
            SecondaryDimensionWeightInCaseLbs: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseLbs.toUpperCase(),
            SecondaryDimensionWeightInCaseOz: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseOz.toUpperCase(),
            SecondaryDimensionWidthCm: this.newRecordsToCreate[0].record.SecondaryDimensionWidthCm.toUpperCase(),
            SecondaryDimensionWidthFt: this.newRecordsToCreate[0].record.SecondaryDimensionWidthFt.toUpperCase(),
            SecondaryDimensionWidthIn: this.newRecordsToCreate[0].record.SecondaryDimensionWidthIn.toUpperCase(),
            SecondaryDimensionWidthM: this.newRecordsToCreate[0].record.SecondaryDimensionWidthM.toUpperCase(),
            CountryOfOrigin: ModuleBase.NOTEMPTY,

            //attribute / usage
            CopyAttributesAsNote: this.newRecordsToCreate[0].record.CopyAttributesAsNote,

            //notes
            Note: this.newRecordsToCreate[0].record.Note,
            AutoCopyNotesToQuoteOrder: this.newRecordsToCreate[0].record.AutoCopyNotesToQuoteOrder,
            PrintNoteOnInContract: this.newRecordsToCreate[0].record.PrintNoteOnInContract,
            PrintNoteOnInvoice: this.newRecordsToCreate[0].record.PrintNoteOnInvoice,
            PrintNoteOnOrder: this.newRecordsToCreate[0].record.PrintNoteOnOrder,
            PrintNoteOnOutContract: this.newRecordsToCreate[0].record.PrintNoteOnOutContract,
            PrintNoteOnPO: this.newRecordsToCreate[0].record.PrintNoteOnPO,
            PrintNoteOnPickList: this.newRecordsToCreate[0].record.PrintNoteOnPickList,
            PrintNoteOnPoReceiveList: this.newRecordsToCreate[0].record.PrintNoteOnPoReceiveList,
            PrintNoteOnPoReturnList: this.newRecordsToCreate[0].record.PrintNoteOnPoReturnList,
            PrintNoteOnQuote: this.newRecordsToCreate[0].record.PrintNoteOnQuote,
            PrintNoteOnReceiveContract: this.newRecordsToCreate[0].record.PrintNoteOnReceiveContract,
            PrintNoteOnReturnContract: this.newRecordsToCreate[0].record.PrintNoteOnReturnContract,
            PrintNoteOnReturnList: this.newRecordsToCreate[0].record.PrintNoteOnReturnList,
            WebDetailedDescription: this.newRecordsToCreate[0].record.WebDetailedDescription,

            //profit/loss
            OverrideProfitAndLossCategory: this.newRecordsToCreate[0].record.OverrideProfitAndLossCategory,
            ProfitAndLossCategory: ModuleBase.NOTEMPTY,

            //g/l accounts
            AssetAccountNo: ModuleBase.NOTEMPTY,
            AssetAccountDescription: ModuleBase.NOTEMPTY,
            IncomeAccountNo: ModuleBase.NOTEMPTY,
            IncomeAccountDescription: ModuleBase.NOTEMPTY,
            SubIncomeAccountNo: ModuleBase.NOTEMPTY,
            SubIncomeAccountDescription: ModuleBase.NOTEMPTY,
            CostOfGoodsSoldExpenseAccountNo: ModuleBase.NOTEMPTY,
            CostOfGoodsSoldExpenseAccountDescription: ModuleBase.NOTEMPTY,

        }
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PartsInventory extends InventoryModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PartsInventory';
        this.moduleGroupName = 'Inventory';
        this.moduleId = '2WDCohbQV6GU';
        this.moduleCaption = 'Parts Inventory';

        let whInvGrid: GridBase = new GridBase("Warehouse Inventory Grid", "PartsInventoryWarehouseGrid");
        whInvGrid.canNew = false;
        whInvGrid.canDelete = false;

        let akaGrid: GridBase = new GridBase("AKA Grid", "AlternativeDescriptionGrid");

        let completeKitGrid: GridBase = new GridBase("Complete/Kit Grid", "InventoryCompleteKitGrid");
        completeKitGrid.canNew = false;
        completeKitGrid.canEdit = false;
        completeKitGrid.canDelete = false;

        let substituteGrid: GridBase = new GridBase("Substitute Grid", "PartsInventorySubstituteGrid");
        let compatibilityGrid: GridBase = new GridBase("Compatibility Grid", "PartsInventoryCompatibilityGrid");

        let purchaseVendorGrid: GridBase = new GridBase("Purchase Vendor Grid", "PurchaseVendorGrid");
        purchaseVendorGrid.canNew = false;
        purchaseVendorGrid.canEdit = false;
        purchaseVendorGrid.canDelete = false;

        let prepGrid: GridBase = new GridBase("Prep Grid", "InventoryPrepGrid");
        let attributeGrid: GridBase = new GridBase("Attribute Grid", "InventoryAttributeValueGrid");
        let taxGrid: GridBase = new GridBase("Tax Grid", "InventoryLocationTaxGrid");
        taxGrid.canNew = false;
        taxGrid.canDelete = false;

        this.grids.push(whInvGrid);
        this.grids.push(akaGrid);
        this.grids.push(completeKitGrid);
        this.grids.push(substituteGrid);
        this.grids.push(compatibilityGrid);
        this.grids.push(purchaseVendorGrid);
        this.grids.push(prepGrid);
        this.grids.push(attributeGrid);
        this.grids.push(taxGrid);

        this.defaultNewRecordToExpect = {
            Unit: "GlobalScope.DefaultSettings~1.DefaultUnit",   // ie. "EA"
        }

        this.newRecordsToCreate = [
            {
                record: {
                    //inventory tab
                    ICode: TestUtils.randomAlphanumeric(7),
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    NonDiscountable: TestUtils.randomBoolean(),
                    IsHazardousMaterial: TestUtils.randomBoolean(),
                    //IsFixedAsset: TestUtils.randomBoolean(),
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                    ExcludeImageFromQuoteOrderPrint: TestUtils.randomBoolean(),

                    //completes/kits
                    DisplayInSummaryModeWhenRateIsZero: TestUtils.randomBoolean(),

                    //weights and dims
                    PrimaryDimensionDescription: "GlobalScope.TestToken~1.TestToken PRIMARY",
                    PrimaryDimensionHeightCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionHeightFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionHeightIn: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionHeightM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionLengthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionLengthFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionLengthIn: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionLengthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionShipWeightOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWeightInCaseOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWidthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionWidthFt: "0",//TestUtils.randomIntegerBetween(1, 20).toString(),
                    //PrimaryDimensionWidthIn: "0",//tUtils.randomIntegerBetween(1, 20).toString(),
                    PrimaryDimensionWidthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionDescription: "GlobalScope.TestToken~1.TestToken SECONDARY",
                    SecondaryDimensionHeightCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionHeightM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionLengthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionShipWeightOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseG: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseKg: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseLbs: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWeightInCaseOz: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthCm: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthFt: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthIn: TestUtils.randomIntegerBetween(1, 20).toString(),
                    SecondaryDimensionWidthM: TestUtils.randomIntegerBetween(1, 20).toString(),
                    CountryOfOriginId: TestUtils.randomIntegerBetween(1, 5),

                    //attribute / usage
                    CopyAttributesAsNote: TestUtils.randomBoolean(),

                    //notes
                    Note: TestUtils.randomLoremWords(20),
                    AutoCopyNotesToQuoteOrder: TestUtils.randomBoolean(),
                    PrintNoteOnInContract: TestUtils.randomBoolean(),
                    PrintNoteOnInvoice: TestUtils.randomBoolean(),
                    PrintNoteOnOrder: TestUtils.randomBoolean(),
                    PrintNoteOnOutContract: TestUtils.randomBoolean(),
                    PrintNoteOnPO: TestUtils.randomBoolean(),
                    PrintNoteOnPickList: TestUtils.randomBoolean(),
                    PrintNoteOnPoReceiveList: TestUtils.randomBoolean(),
                    PrintNoteOnPoReturnList: TestUtils.randomBoolean(),
                    PrintNoteOnQuote: TestUtils.randomBoolean(),
                    PrintNoteOnReceiveContract: TestUtils.randomBoolean(),
                    PrintNoteOnReturnContract: TestUtils.randomBoolean(),
                    PrintNoteOnReturnList: TestUtils.randomBoolean(),
                    WebDetailedDescription: TestUtils.randomLoremWords(20),

                    //profit/loss
                    OverrideProfitAndLossCategory: true,
                    ProfitAndLossCategoryId: 1,

                    //g/l accounts
                    AssetAccountId: TestUtils.randomIntegerBetween(1, 5),
                    IncomeAccountId: TestUtils.randomIntegerBetween(1, 5),
                    CostOfGoodsSoldExpenseAccountId: TestUtils.randomIntegerBetween(1, 5),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];

        let aka1: string = TestUtils.randomAlphanumeric(20).toUpperCase();
        let aka2: string = TestUtils.randomAlphanumeric(20).toUpperCase();

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: akaGrid,
                recordToCreate: {
                    record: {
                        AKA: aka1,
                    },
                    seekObject: {
                        AKA: aka1,
                    },
                    attemptDuplicate: true,
                    //editRecord: {
                    //    seekObject: {
                    //        AKA: aka1,
                    //    },
                    //    record: {
                    //        AKA: aka2,
                    //    },
                    //    recordToExpect: {
                    //        AKA: aka2,
                    //    },
                    //},
                },
            },
            {
                grid: substituteGrid,
                recordToCreate: {
                    record: {
                        SubstituteInventoryId: 1,
                    },
                    editRecord: {
                        record: {
                            SubstituteInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        },
                        //recordToExpect: {
                        //    SubstituteInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        //},
                    },
                },
            },
            // can't test this scenario because the grid save is cancelled when all fields are blank
            //{
            //    grid: substituteGrid,
            //    recordToCreate: {
            //        record: {
            //        },
            //        expectedErrorFields: ["SubstituteInventoryId"],
            //    },
            //},
            {
                grid: compatibilityGrid,
                recordToCreate: {
                    record: {
                        CompatibleWithInventoryId: 1,
                    },
                    editRecord: {
                        record: {
                            CompatibleWithInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        },
                        //recordToExpect: {
                        //    CompatibleWithInventoryId: TestUtils.randomIntegerBetween(4, 6),
                        //},
                    },
                },
            },
            {
                grid: prepGrid,
                recordToCreate: {
                    record: {
                        PrepRateId: 1,
                        //PrepRate: "25.25",
                        PrepRate: TestUtils.randomIntegerBetween(10, 99) + "." + TestUtils.randomIntegerBetween(10, 99),
                        PrepTime: "1",
                    },
                    editRecord: {
                        record: {
                            PrepRate: "50.50",
                        },
                        recordToExpect: {
                            PrepRate: "50.50",
                        },
                    },
                },
            },
            {
                grid: attributeGrid,
                recordToCreate: {
                    record: {
                        AttributeId: 1,
                        AttributeValueId: 1,
                    },
                    editRecord: {
                        record: {
                            AttributeValueId: 2,
                        },
                        //recordToExpect: {
                        //    AttributeValueId: 2,
                        //},
                    },
                },
            },
            {
                grid: taxGrid,
                recordToEdit: {
                    seekObject: {
                        Location: "GlobalScope.User~ME.OfficeLocation",
                    },
                    record: {
                        Taxable: false,
                    },
                },
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            //inventory tab
            ICode: this.newRecordsToCreate[0].record.ICode.toUpperCase(),
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
            ManufacturerPartNumber: this.newRecordsToCreate[0].record.ManufacturerPartNumber.toUpperCase(),
            TrackedBy: "QUANTITY",
            NonDiscountable: this.newRecordsToCreate[0].record.NonDiscountable,
            IsHazardousMaterial: this.newRecordsToCreate[0].record.IsHazardousMaterial,
            //IsFixedAsset: this.newRecordsToCreate[0].record.IsFixedAsset,
            InventoryType: ModuleBase.NOTEMPTY,
            Category: ModuleBase.NOTEMPTY,
            Unit: ModuleBase.NOTEMPTY,
            Rank: ModuleBase.NOTEMPTY,
            ExcludeImageFromQuoteOrderPrint: this.newRecordsToCreate[0].record.ExcludeImageFromQuoteOrderPrint,

            //completes/kits
            DisplayInSummaryModeWhenRateIsZero: this.newRecordsToCreate[0].record.DisplayInSummaryModeWhenRateIsZero,

            //weights and dims
            PrimaryDimensionDescription: "GlobalScope.TestToken~1.TestToken PRIMARY",
            PrimaryDimensionHeightCm: this.newRecordsToCreate[0].record.PrimaryDimensionHeightCm.toUpperCase(),
            //PrimaryDimensionHeightFt: this.newRecordsToCreate[0].record.PrimaryDimensionHeightFt.toUpperCase(),
            //PrimaryDimensionHeightIn: this.newRecordsToCreate[0].record.PrimaryDimensionHeightIn.toUpperCase(),
            PrimaryDimensionHeightM: this.newRecordsToCreate[0].record.PrimaryDimensionHeightM.toUpperCase(),
            PrimaryDimensionLengthCm: this.newRecordsToCreate[0].record.PrimaryDimensionLengthCm.toUpperCase(),
            //PrimaryDimensionLengthFt: this.newRecordsToCreate[0].record.PrimaryDimensionLengthFt.toUpperCase(),
            //PrimaryDimensionLengthIn: this.newRecordsToCreate[0].record.PrimaryDimensionLengthIn.toUpperCase(),
            PrimaryDimensionLengthM: this.newRecordsToCreate[0].record.PrimaryDimensionLengthM.toUpperCase(),
            PrimaryDimensionShipWeightG: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightG.toUpperCase(),
            PrimaryDimensionShipWeightKg: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightKg.toUpperCase(),
            PrimaryDimensionShipWeightLbs: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightLbs.toUpperCase(),
            PrimaryDimensionShipWeightOz: this.newRecordsToCreate[0].record.PrimaryDimensionShipWeightOz.toUpperCase(),
            PrimaryDimensionWeightInCaseG: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseG.toUpperCase(),
            PrimaryDimensionWeightInCaseKg: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseKg.toUpperCase(),
            PrimaryDimensionWeightInCaseLbs: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseLbs.toUpperCase(),
            PrimaryDimensionWeightInCaseOz: this.newRecordsToCreate[0].record.PrimaryDimensionWeightInCaseOz.toUpperCase(),
            PrimaryDimensionWidthCm: this.newRecordsToCreate[0].record.PrimaryDimensionWidthCm.toUpperCase(),
            //PrimaryDimensionWidthFt: this.newRecordsToCreate[0].record.PrimaryDimensionWidthFt.toUpperCase(),
            //PrimaryDimensionWidthIn: this.newRecordsToCreate[0].record.PrimaryDimensionWidthIn.toUpperCase(),
            PrimaryDimensionWidthM: this.newRecordsToCreate[0].record.PrimaryDimensionWidthM.toUpperCase(),
            SecondaryDimensionDescription: "GlobalScope.TestToken~1.TestToken SECONDARY",
            SecondaryDimensionHeightCm: this.newRecordsToCreate[0].record.SecondaryDimensionHeightCm.toUpperCase(),
            SecondaryDimensionHeightFt: this.newRecordsToCreate[0].record.SecondaryDimensionHeightFt.toUpperCase(),
            SecondaryDimensionHeightIn: this.newRecordsToCreate[0].record.SecondaryDimensionHeightIn.toUpperCase(),
            SecondaryDimensionHeightM: this.newRecordsToCreate[0].record.SecondaryDimensionHeightM.toUpperCase(),
            SecondaryDimensionLengthCm: this.newRecordsToCreate[0].record.SecondaryDimensionLengthCm.toUpperCase(),
            SecondaryDimensionLengthFt: this.newRecordsToCreate[0].record.SecondaryDimensionLengthFt.toUpperCase(),
            SecondaryDimensionLengthIn: this.newRecordsToCreate[0].record.SecondaryDimensionLengthIn.toUpperCase(),
            SecondaryDimensionLengthM: this.newRecordsToCreate[0].record.SecondaryDimensionLengthM.toUpperCase(),
            SecondaryDimensionShipWeightG: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightG.toUpperCase(),
            SecondaryDimensionShipWeightKg: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightKg.toUpperCase(),
            SecondaryDimensionShipWeightLbs: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightLbs.toUpperCase(),
            SecondaryDimensionShipWeightOz: this.newRecordsToCreate[0].record.SecondaryDimensionShipWeightOz.toUpperCase(),
            SecondaryDimensionWeightInCaseG: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseG.toUpperCase(),
            SecondaryDimensionWeightInCaseKg: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseKg.toUpperCase(),
            SecondaryDimensionWeightInCaseLbs: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseLbs.toUpperCase(),
            SecondaryDimensionWeightInCaseOz: this.newRecordsToCreate[0].record.SecondaryDimensionWeightInCaseOz.toUpperCase(),
            SecondaryDimensionWidthCm: this.newRecordsToCreate[0].record.SecondaryDimensionWidthCm.toUpperCase(),
            SecondaryDimensionWidthFt: this.newRecordsToCreate[0].record.SecondaryDimensionWidthFt.toUpperCase(),
            SecondaryDimensionWidthIn: this.newRecordsToCreate[0].record.SecondaryDimensionWidthIn.toUpperCase(),
            SecondaryDimensionWidthM: this.newRecordsToCreate[0].record.SecondaryDimensionWidthM.toUpperCase(),
            CountryOfOrigin: ModuleBase.NOTEMPTY,

            //attribute / usage
            CopyAttributesAsNote: this.newRecordsToCreate[0].record.CopyAttributesAsNote,

            //notes
            Note: this.newRecordsToCreate[0].record.Note,
            AutoCopyNotesToQuoteOrder: this.newRecordsToCreate[0].record.AutoCopyNotesToQuoteOrder,
            PrintNoteOnInContract: this.newRecordsToCreate[0].record.PrintNoteOnInContract,
            PrintNoteOnInvoice: this.newRecordsToCreate[0].record.PrintNoteOnInvoice,
            PrintNoteOnOrder: this.newRecordsToCreate[0].record.PrintNoteOnOrder,
            PrintNoteOnOutContract: this.newRecordsToCreate[0].record.PrintNoteOnOutContract,
            PrintNoteOnPO: this.newRecordsToCreate[0].record.PrintNoteOnPO,
            PrintNoteOnPickList: this.newRecordsToCreate[0].record.PrintNoteOnPickList,
            PrintNoteOnPoReceiveList: this.newRecordsToCreate[0].record.PrintNoteOnPoReceiveList,
            PrintNoteOnPoReturnList: this.newRecordsToCreate[0].record.PrintNoteOnPoReturnList,
            PrintNoteOnQuote: this.newRecordsToCreate[0].record.PrintNoteOnQuote,
            PrintNoteOnReceiveContract: this.newRecordsToCreate[0].record.PrintNoteOnReceiveContract,
            PrintNoteOnReturnContract: this.newRecordsToCreate[0].record.PrintNoteOnReturnContract,
            PrintNoteOnReturnList: this.newRecordsToCreate[0].record.PrintNoteOnReturnList,
            WebDetailedDescription: this.newRecordsToCreate[0].record.WebDetailedDescription,

            //profit/loss
            OverrideProfitAndLossCategory: this.newRecordsToCreate[0].record.OverrideProfitAndLossCategory,
            ProfitAndLossCategory: ModuleBase.NOTEMPTY,

            //g/l accounts
            AssetAccountNo: ModuleBase.NOTEMPTY,
            AssetAccountDescription: ModuleBase.NOTEMPTY,
            IncomeAccountNo: ModuleBase.NOTEMPTY,
            IncomeAccountDescription: ModuleBase.NOTEMPTY,
            CostOfGoodsSoldExpenseAccountNo: ModuleBase.NOTEMPTY,
            CostOfGoodsSoldExpenseAccountDescription: ModuleBase.NOTEMPTY,
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
        this.moduleGroupName = 'Inventory';
        this.moduleId = 'kSugPLvkuNsH';
        this.moduleCaption = 'Asset';
        this.canNew = false;
        this.canDelete = false;

        let vendorInvoiceGrid: GridBase = new GridBase("Vendor Invoice Grid", "PurchaseVendorInvoiceItemGrid");
        vendorInvoiceGrid.canNew = false;
        vendorInvoiceGrid.canEdit = false;
        vendorInvoiceGrid.canDelete = false;

        //let attributeGrid: GridBase = new GridBase("Attribute Grid", "ItemAttributeValueGrid");

        let qcGrid: GridBase = new GridBase("QC Grid", "ItemQcGrid");
        qcGrid.canNew = false;
        qcGrid.canEdit = false;
        qcGrid.canDelete = false;

        this.grids.push(vendorInvoiceGrid);
        //this.grids.push(attributeGrid);
        this.grids.push(qcGrid);


        this.recordsToEdit = [
            {
                record: {
                    InspectionNo: "GlobalScope.TestToken~1.MediumTestToken",
                },
                recordToExpect: {
                    InspectionNo: "GlobalScope.TestToken~1.MediumTestToken",
                },
            }
        ];

        //this.recordsToEdit[0].gridRecords = [
        //    {
        //        grid: attributeGrid,
        //        recordToCreate: {
        //            record: {
        //                AttributeId: 1,
        //                AttributeValueId: 1,
        //            },
        //            editRecord: {
        //                record: {
        //                    AttributeValueId: 2,
        //                },
        //                //recordToExpect: {
        //                //    AttributeValueId: 2,
        //                //},
        //            },
        //        },
        //    },
        //];

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class RepairOrder extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Repair';
        this.moduleGroupName = 'Inventory';
        this.moduleId = 't4gfyzLkSZhyc';
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
        this.moduleGroupName = 'Inventory';
        this.moduleId = 'JIuxFUWTLDC6';
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
                editRecord: {
                    seekObject: {
                        Description: "GlobalScope.TestToken~1.TestToken",
                    },
                    record: {
                        ScheduleDate: "01/01/2001",
                    },
                    recordToExpect: {
                        ScheduleDate: "01/01/2001",
                    },
                },
            }
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
        };

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class PickList extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'PickList';
        this.moduleGroupName = 'Warehouse';
        this.moduleId = 'bggVQOivrIgi';
        this.moduleCaption = 'Pick List';
        this.canNew = false;

        let itemsGrid: GridBase = new GridBase("Items Grid", "PickListItemGrid");
        itemsGrid.canNew = false;
        itemsGrid.canEdit = false;
        itemsGrid.canDelete = false;

        this.grids.push(itemsGrid);

        this.recordsToEdit = [
            {
                record: {
                    DueTime: TestUtils.randomIntegerBetween(10, 23) + ":" + TestUtils.randomIntegerBetween(10, 50),
                },
            },
        ];

        this.recordsToEdit[0].gridRecords = [
            {
                grid: itemsGrid,
                recordToEdit: {
                    record: {
                        Quantity: TestUtils.randomIntegerBetween(1, 50).toString(),
                    }
                }
            }
        ];


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Contract extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Contract';
        this.moduleGroupName = 'Warehouse';
        this.moduleId = 'Z8MlDQp7xOqu';
        this.moduleCaption = 'Contract';
        this.canNew = false;
        this.canDelete = false;

        let summaryItemGrid: GridBase = new GridBase("Summary Item Grid", "ContractSummaryGrid");
        let rentalItemGrid: GridBase = new GridBase("Rental Item Grid", "ContractDetailGrid", ["R"]);
        let salesItemGrid: GridBase = new GridBase("Sales Item Grid", "ContractDetailGrid", ["S"]);

        this.grids.push(summaryItemGrid);
        this.grids.push(rentalItemGrid);
        this.grids.push(salesItemGrid);
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Container extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Container';
        this.moduleGroupName = 'Warehouse';
        this.moduleId = 'bSQsBVDvo86X1';
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
        this.moduleGroupName = 'Inventory';
        this.moduleId = 'tc2HgrtvGDJ5';
        this.moduleCaption = 'Transfer Manifest';
        this.canNew = false;
        this.canDelete = false;

        let summaryItemGrid: GridBase = new GridBase("Summary Item Grid", "ContractSummaryGrid");
        let rentalItemGrid: GridBase = new GridBase("Rental Item Grid", "ContractDetailGrid", ["R"]);
        let salesItemGrid: GridBase = new GridBase("Sales Item Grid", "ContractDetailGrid", ["S"]);

        this.grids.push(summaryItemGrid);
        this.grids.push(rentalItemGrid);
        this.grids.push(salesItemGrid);

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TransferOrder extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TransferOrder';
        this.moduleGroupName = 'Inventory';
        this.moduleId = 'tWkLbjsVHH6N';
        this.moduleCaption = 'Transfer Order';
        this.canDelete = false;

        let rentalItemGrid: GridBase = new GridBase("Rental Item Grid", "TransferOrderItemGrid", ["R"]);
        rentalItemGrid.waitAfterSavingToReloadGrid = 1500;
        let salesItemGrid: GridBase = new GridBase("Sales Item Grid", "TransferOrderItemGrid", ["S"]);
        salesItemGrid.waitAfterSavingToReloadGrid = 1500;

        this.grids.push(rentalItemGrid);
        this.grids.push(salesItemGrid);


        this.newRecordsToCreate = [
            {
                record: {
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    FromWarehouseCode: "GlobalScope.Warehouse~MINE.WarehouseCode",
                    ToWarehouseId: 2,
                    Rental: true,
                    Sales: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            },
        ];

        this.newRecordsToCreate[0].gridRecords = [
            {
                grid: rentalItemGrid,
                recordToCreate: {
                    record: {
                        InventoryId: 1,
                    },
                },
            },
            {
                grid: salesItemGrid,
                recordToCreate: {
                    record: {
                        InventoryId: 1,
                    },
                },
            },
        ];

        this.newRecordsToCreate[0].recordToExpect = {
            Description: this.newRecordsToCreate[0].record.Description.toUpperCase(),
        };


    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TransferReceipt extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TransferReceipt';
        this.moduleGroupName = 'Inventory';
        this.moduleId = 'VSn3weoOHqLc';
        this.moduleCaption = 'Transfer Receipt';
        this.canNew = false;
        this.canDelete = false;

        let summaryItemGrid: GridBase = new GridBase("Summary Item Grid", "ContractSummaryGrid");
        let rentalItemGrid: GridBase = new GridBase("Rental Item Grid", "ContractDetailGrid", ["R"]);
        let salesItemGrid: GridBase = new GridBase("Sales Item Grid", "ContractDetailGrid", ["S"]);

        this.grids.push(summaryItemGrid);
        this.grids.push(rentalItemGrid);
        this.grids.push(salesItemGrid);

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Invoice extends HomeModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Invoice';
        this.moduleGroupName = 'Billing';
        this.moduleId = 'cZ9Z8aGEiDDw';
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
                    //CurrencyId: TestUtils.randomIntegerBetween(1, 5),
                    CurrencyCode: "USD",
                    TaxOptionId: TestUtils.randomIntegerBetween(1, 5),
                },
                seekObject: {
                    InvoiceDescription: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
        this.newRecordsToCreate[0].recordToExpect = {
            InvoiceNumber: ModuleBase.NOTEMPTY,
            Deal: ModuleBase.NOTEMPTY,
            RateType: ModuleBase.NOTEMPTY,
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
        this.moduleGroupName = 'Billing';
        this.moduleId = 'q4PPGLusbFw';
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
        this.moduleGroupName = 'Billing';
        this.moduleId = 'Fq9aOe0yWfY';
        this.moduleCaption = 'Vendor Invoice';
    }
    //---------------------------------------------------------------------------------------
}

