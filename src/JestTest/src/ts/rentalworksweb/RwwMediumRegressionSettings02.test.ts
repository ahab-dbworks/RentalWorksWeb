import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import {
/*
 //home
    Contact, Customer, Deal, Order, Project, PurchaseOrder, Quote, Vendor,
    Asset, PartsInventory, PhysicalInventory, RentalInventory, RepairOrder, SalesInventory,
    Contract, PickList, Container, Manifest, TransferOrder, TransferReceipt,
    Invoice, Receipt, VendorInvoice,
        */
    //settings
    ActivityType, AccountingSettings, GlAccount, GlDistribution, Country, State, BillingCycle, Department, ContactEvent, ContactTitle, MailList, Currency,
    CreditStatus, CustomerCategory, CustomerStatus, CustomerType, DealClassification, DealType, DealStatus, ProductionType, ScheduleType, DiscountTemplate,
    DocumentType, CoverLetter, TermsConditions, EventCategory, EventType, PersonnelType, PhotographyType, Building, FacilityType, FacilityRate, FacilityScheduleStatus,
    FacilityStatus, FacilityCategory, SpaceType, FiscalYear, GeneratorFuelType, GeneratorMake, GeneratorRating, GeneratorWatts, GeneratorType, Holiday,
    BlackoutStatus, BarCodeRange, InventoryAdjustmentReason, Attribute, InventoryCondition, InventoryGroup, InventoryRank, InventoryStatus, InventoryType,
    PartsCategory, RentalCategory, RetiredReason, SalesCategory, Unit, UnretiredReason, WarehouseCatalog, Crew, LaborRate, LaborPosition, LaborType, LaborCategory,
    CrewScheduleStatus, CrewStatus, MiscRate, MiscType, MiscCategory, OfficeLocation, OrderType, DiscountReason, MarketSegment, MarketType, OrderSetNo,
    OrderLocation, PaymentTerms, PaymentType, POApprovalStatus, POApproverRole, POClassification, POImportance, PORejectReason, POType, POApprover, VendorInvoiceApprover,
    FormDesign, PresentationLayer, ProjectAsBuild, ProjectCommissioning, ProjectDeposit, ProjectDrawings, ProjectDropShipItems, ProjectItemsOrdered, PropsCondition,
    Region, RepairItemStatus, SetCondition, SetSurface, SetOpening, WallDescription, WallType, ShipVia, Source, AvailabilitySettings, DefaultSettings, EmailSettings,
    InventorySettings, LogoSettings, DocumentBarCodeSettings, SystemSettings, TaxOption, Template, UserStatus, Sound, LicenseClass, VehicleColor, VehicleFuelType, VehicleMake, VehicleScheduleStatus, VehicleStatus,
    VehicleType, OrganizationType, VendorCatalog, VendorClass, SapVendorInvoiceStatus, WardrobeCare, WardrobeColor, WardrobeCondition, WardrobeGender, WardrobeLabel,
    WardrobeMaterial, WardrobePattern, WardrobePeriod, WardrobeSource, Warehouse, Widget, WorkWeek,

    //administrator
    //Alert, CustomField, CustomForm, CustomReportLayout, DuplicateRule, EmailHistory, Group, Hotfix,
    User,
} from './modules/AllModules';
//import { SettingsModule } from '../shared/SettingsModule';
import { MediumRegressionBaseTest } from './RwwMediumRegressionBase';

export class MediumRegressionSettingsTest extends MediumRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        this.OpenSpecificRecord(new InventorySettings(), null, true);

        let warehouseToSeek: any = {
            Warehouse: "GlobalScope.User~ME.Warehouse",
        }
        this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        /*
        //Home - Agent
        this.MediumRegressionOnModule(new Contact());
        this.MediumRegressionOnModule(new Customer());
        this.MediumRegressionOnModule(new Deal());
        this.MediumRegressionOnModule(new Order());
        this.MediumRegressionOnModule(new Project());
        this.MediumRegressionOnModule(new PurchaseOrder());
        this.MediumRegressionOnModule(new Quote());
        this.MediumRegressionOnModule(new Vendor());

        describe('Setup new Rental I-Codes', () => {
            //---------------------------------------------------------------------------------------
            let testName: string = 'Create new Rental I-Code using i-Code mask, if any';
            test(testName, async () => {

                let iCodeMask: string = this.globalScopeRef["InventorySettings~1"].ICodeMask;  // ie. "aaaaa-"  or "aaaaa-aa"
                iCodeMask = iCodeMask.toUpperCase();
                let newICode: string = TestUtils.randomAlphanumeric((iCodeMask.split("A").length - 1)); // count the A's
                iCodeMask = iCodeMask.trim();
                let maskedICode: string = newICode;

                if ((iCodeMask.includes("-")) && (!iCodeMask.endsWith("-"))) {
                    let hyphenIndex: number = iCodeMask.indexOf("-");
                    let iCodeStart: string = newICode.toUpperCase().substr(0, hyphenIndex);
                    let iCodeEnd: string = newICode.toUpperCase().substr(hyphenIndex);
                    maskedICode = iCodeStart + '-' + iCodeEnd;
                }

                let newICodeObject: any = {};
                newICodeObject.newICode = newICode.toUpperCase();
                newICodeObject.maskedICode = maskedICode.toUpperCase();
                this.globalScopeRef["RentalInventory~NEWICODE"] = newICodeObject;

                expect(1).toBe(1);
            }, this.testTimeout);
        });

        //Home - Inventory
        this.MediumRegressionOnModule(new Asset());
        this.MediumRegressionOnModule(new PartsInventory());
        this.MediumRegressionOnModule(new PhysicalInventory());
        this.MediumRegressionOnModule(new RentalInventory());
        //this.MediumRegressionOnModule(new RepairOrder());    // this module cannot be tested because we cannot search on a unique field. also the bar code validation allows all statuses, so we can't be sure that a record we pick there will be allowable in repair
        this.MediumRegressionOnModule(new SalesInventory());

        //Home - Warehouse
        this.MediumRegressionOnModule(new Contract());
        this.MediumRegressionOnModule(new PickList());

        //Home - Container
        this.MediumRegressionOnModule(new Container());

        //Home - Transfer
        this.MediumRegressionOnModule(new Manifest());
        this.MediumRegressionOnModule(new TransferOrder());
        this.MediumRegressionOnModule(new TransferReceipt());

        //Home - Billing
        this.MediumRegressionOnModule(new Invoice());
        this.MediumRegressionOnModule(new Receipt());
        this.MediumRegressionOnModule(new VendorInvoice());
        */

        /*
        //Settings
        this.MediumRegressionOnModule(new ActivityType());
        this.MediumRegressionOnModule(new AccountingSettings());
        this.MediumRegressionOnModule(new GlAccount());
        this.MediumRegressionOnModule(new GlDistribution());
        this.MediumRegressionOnModule(new Country());
        this.MediumRegressionOnModule(new State());
        this.MediumRegressionOnModule(new BillingCycle());
        this.MediumRegressionOnModule(new Department());
        this.MediumRegressionOnModule(new ContactEvent());
        this.MediumRegressionOnModule(new ContactTitle());
        this.MediumRegressionOnModule(new MailList());
        this.MediumRegressionOnModule(new Currency());
        this.MediumRegressionOnModule(new CreditStatus());
        this.MediumRegressionOnModule(new CustomerCategory());
        this.MediumRegressionOnModule(new CustomerStatus());
        this.MediumRegressionOnModule(new CustomerType());
        this.MediumRegressionOnModule(new DealClassification());
        this.MediumRegressionOnModule(new DealType());
        this.MediumRegressionOnModule(new DealStatus());
        this.MediumRegressionOnModule(new ProductionType());
        this.MediumRegressionOnModule(new ScheduleType());
        this.MediumRegressionOnModule(new DiscountTemplate());
        this.MediumRegressionOnModule(new DocumentType());
        //this.MediumRegressionOnModule(new CoverLetter());
        //this.MediumRegressionOnModule(new TermsConditions());
        this.MediumRegressionOnModule(new EventCategory());
        this.MediumRegressionOnModule(new PersonnelType());
        this.MediumRegressionOnModule(new PhotographyType());
        this.MediumRegressionOnModule(new Building());
        this.MediumRegressionOnModule(new FacilityType());
        this.MediumRegressionOnModule(new FacilityRate());
        this.MediumRegressionOnModule(new FacilityScheduleStatus());
        this.MediumRegressionOnModule(new FacilityStatus());
        this.MediumRegressionOnModule(new FacilityCategory());
        this.MediumRegressionOnModule(new SpaceType());
        this.MediumRegressionOnModule(new FiscalYear());
        */
        this.MediumRegressionOnModule(new ActivityType());

        this.MediumRegressionOnModule(new EventType());

        this.MediumRegressionOnModule(new GeneratorFuelType());
        this.MediumRegressionOnModule(new GeneratorMake());
        this.MediumRegressionOnModule(new GeneratorRating());
        this.MediumRegressionOnModule(new GeneratorWatts());
        this.MediumRegressionOnModule(new GeneratorType());
        //this.MediumRegressionOnModule(new Holiday()); // module cannot be tested becuase data fields repeat and become invisible based on holiday.Type
        this.MediumRegressionOnModule(new BlackoutStatus());
        this.MediumRegressionOnModule(new BarCodeRange());
        this.MediumRegressionOnModule(new InventoryAdjustmentReason());
        this.MediumRegressionOnModule(new Attribute());
        this.MediumRegressionOnModule(new InventoryCondition());
        this.MediumRegressionOnModule(new InventoryGroup());
        //this.MediumRegressionOnModule(new InventoryRank());  // module cannot be tested because there is no unique field that can be searched to validate or delete the record
        //this.MediumRegressionOnModule(new InventoryStatus());  // module cannot be tested because of unique index on the "statustype" field. no adds allowed
        this.MediumRegressionOnModule(new InventoryType());
        this.MediumRegressionOnModule(new PartsCategory());
        this.MediumRegressionOnModule(new RentalCategory());
        this.MediumRegressionOnModule(new RetiredReason());
        this.MediumRegressionOnModule(new SalesCategory());
        this.MediumRegressionOnModule(new Unit());
        this.MediumRegressionOnModule(new UnretiredReason());
        this.MediumRegressionOnModule(new WarehouseCatalog());
        this.MediumRegressionOnModule(new Crew());
        this.MediumRegressionOnModule(new LaborRate());
        this.MediumRegressionOnModule(new LaborPosition());
        this.MediumRegressionOnModule(new LaborType());
        this.MediumRegressionOnModule(new LaborCategory());
        this.MediumRegressionOnModule(new CrewScheduleStatus());
        this.MediumRegressionOnModule(new CrewStatus());
        this.MediumRegressionOnModule(new MiscRate());
        this.MediumRegressionOnModule(new MiscType());
        this.MediumRegressionOnModule(new MiscCategory());
        this.MediumRegressionOnModule(new OfficeLocation());
        this.MediumRegressionOnModule(new OrderType());
        this.MediumRegressionOnModule(new DiscountReason());
        this.MediumRegressionOnModule(new MarketSegment());
        this.MediumRegressionOnModule(new MarketType());

        this.MediumRegressionOnModule(new POType());

        /*
        this.MediumRegressionOnModule(new OrderSetNo());
        this.MediumRegressionOnModule(new OrderLocation());
        this.MediumRegressionOnModule(new PaymentTerms());
        this.MediumRegressionOnModule(new PaymentType());
        this.MediumRegressionOnModule(new POApprovalStatus());
        this.MediumRegressionOnModule(new POApproverRole());
        this.MediumRegressionOnModule(new POClassification());
        this.MediumRegressionOnModule(new POImportance());
        this.MediumRegressionOnModule(new PORejectReason());
        //this.MediumRegressionOnModule(new POApprover());                // module cannot be tested because there is no unique field that can be searched to validate or delete the record
        //this.MediumRegressionOnModule(new VendorInvoiceApprover());     // module cannot be tested because there is no unique field that can be searched to validate or delete the record
        this.MediumRegressionOnModule(new FormDesign());
        this.MediumRegressionOnModule(new PresentationLayer());
        this.MediumRegressionOnModule(new ProjectAsBuild());
        this.MediumRegressionOnModule(new ProjectCommissioning());
        this.MediumRegressionOnModule(new ProjectDeposit());
        this.MediumRegressionOnModule(new ProjectDrawings());
        this.MediumRegressionOnModule(new ProjectDropShipItems());
        this.MediumRegressionOnModule(new ProjectItemsOrdered());
        this.MediumRegressionOnModule(new PropsCondition());
        this.MediumRegressionOnModule(new Region());
        this.MediumRegressionOnModule(new RepairItemStatus());
        this.MediumRegressionOnModule(new SetCondition());
        this.MediumRegressionOnModule(new SetSurface());
        this.MediumRegressionOnModule(new SetOpening());
        this.MediumRegressionOnModule(new WallDescription());
        this.MediumRegressionOnModule(new WallType());
        this.MediumRegressionOnModule(new ShipVia());
        this.MediumRegressionOnModule(new Source());
        this.MediumRegressionOnModule(new AvailabilitySettings());
        this.MediumRegressionOnModule(new DefaultSettings());
        this.MediumRegressionOnModule(new EmailSettings());
        this.MediumRegressionOnModule(new InventorySettings());
        this.MediumRegressionOnModule(new LogoSettings());
        this.MediumRegressionOnModule(new DocumentBarCodeSettings());
        this.MediumRegressionOnModule(new SystemSettings());
        this.MediumRegressionOnModule(new TaxOption());
        this.MediumRegressionOnModule(new Template());
        this.MediumRegressionOnModule(new UserStatus());
        this.MediumRegressionOnModule(new Sound());
        this.MediumRegressionOnModule(new LicenseClass());
        this.MediumRegressionOnModule(new VehicleColor());
        this.MediumRegressionOnModule(new VehicleFuelType());
        this.MediumRegressionOnModule(new VehicleMake());
        this.MediumRegressionOnModule(new VehicleScheduleStatus());
        this.MediumRegressionOnModule(new VehicleStatus());
        this.MediumRegressionOnModule(new VehicleType());
        this.MediumRegressionOnModule(new OrganizationType());
        this.MediumRegressionOnModule(new VendorCatalog());
        this.MediumRegressionOnModule(new VendorClass());
        this.MediumRegressionOnModule(new SapVendorInvoiceStatus());
        this.MediumRegressionOnModule(new WardrobeCare());
        this.MediumRegressionOnModule(new WardrobeColor());
        this.MediumRegressionOnModule(new WardrobeCondition());
        this.MediumRegressionOnModule(new WardrobeGender());
        this.MediumRegressionOnModule(new WardrobeLabel());
        this.MediumRegressionOnModule(new WardrobeMaterial());
        this.MediumRegressionOnModule(new WardrobePattern());
        this.MediumRegressionOnModule(new WardrobePeriod());
        this.MediumRegressionOnModule(new WardrobeSource());
        this.MediumRegressionOnModule(new Warehouse());
        this.MediumRegressionOnModule(new Widget());
        //this.MediumRegressionOnModule(new WorkWeek());     // module cannot be tested because there is no unique field that can be searched to validate or delete the record

*/
        /*
        //Administrator
        //this.MediumRegressionOnModule(new Alert());
        //this.MediumRegressionOnModule(new CustomField());
        this.MediumRegressionOnModule(new CustomForm());
        this.MediumRegressionOnModule(new CustomReportLayout());
        //this.MediumRegressionOnModule(new DuplicateRule());
        this.MediumRegressionOnModule(new EmailHistory());
        this.MediumRegressionOnModule(new Group());
        this.MediumRegressionOnModule(new Hotfix());
        this.MediumRegressionOnModule(new User());
        */

    }
    //---------------------------------------------------------------------------------------
}

describe('MediumRegressionSettingsTest', () => {
    try {
        new MediumRegressionSettingsTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
