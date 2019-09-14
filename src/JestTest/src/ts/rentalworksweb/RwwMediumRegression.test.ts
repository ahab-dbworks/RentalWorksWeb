import { BaseTest } from '../shared/BaseTest';
import {
    //home
    Contact, Customer, Deal, Order, Project, PurchaseOrder, Quote, Vendor,
    Asset, PartsInventory, PhysicalInventory, RentalInventory, RepairOrder, SalesInventory, 
    Contract,

    //settings
    AccountingSettings, GlAccount, GlDistribution, Country, State, BillingCycle, Department, ContactEvent, ContactTitle, MailList, Currency,
    CreditStatus, CustomerCategory, CustomerStatus, CustomerType, DealClassification, DealType, DealStatus, ProductionType, ScheduleType, DiscountTemplate,
    DocumentType, CoverLetter, TermsConditions, EventCategory, EventType, PersonnelType, PhotographyType, Building, FacilityType, FacilityRate, FacilityScheduleStatus,
    FacilityStatus, FacilityCategory, SpaceType, FiscalYear, GeneratorFuelType, GeneratorMake, GeneratorRating, GeneratorWatts, GeneratorType, Holiday,
    BlackoutStatus, BarCodeRange, InventoryAdjustmentReason, Attribute, InventoryCondition, InventoryGroup, InventoryRank, InventoryStatus, InventoryType,
    PartsCategory, RentalCategory, RetiredReason, SalesCategory, Unit, UnretiredReason, WarehouseCatalog, Crew, LaborRate, LaborPosition, LaborType, LaborCategory,
    CrewScheduleStatus, CrewStatus, MiscRate, MiscType, MiscCategory, OfficeLocation, OrderType, DiscountReason, MarketSegment, MarketType, OrderSetNo,
    OrderLocation, PaymentTerms, PaymentType, POApprovalStatus, POApproverRole, POClassification, POImportance, PORejectReason, POType, POApprover, VendorInvoiceApprover,
    FormDesign, PresentationLayer, ProjectAsBuild, ProjectCommissioning, ProjectDeposit, ProjectDrawings, ProjectDropShipItems, ProjectItemsOrdered, PropsCondition,
    Region, RepairItemStatus, SetCondition, SetSurface, SetOpening, WallDescription, WallType, ShipVia, Source, AvailabilitySettings, DefaultSettings, EmailSettings,
    InventorySettings, LogoSettings, SystemSettings, TaxOption, Template, UserStatus, Sound, LicenseClass, VehicleColor, VehicleFuelType, VehicleMake, VehicleScheduleStatus, VehicleStatus,
    VehicleType, OrganizationType, VendorCatalog, VendorClass, SapVendorInvoiceStatus, WardrobeCare, WardrobeColor, WardrobeCondition, WardrobeGender, WardrobeLabel,
    WardrobeMaterial, WardrobePattern, WardrobePeriod, WardrobeSource, Warehouse, Widget, WorkWeek,

    //administrator
    User
} from './modules/AllModules';


export class MediumRegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        ////Home - Agent
        this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Contact());
        this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Customer());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Deal());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Order());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Project());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PurchaseOrder());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Quote());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Vendor());

        ////Home - Inventory
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Asset());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PartsInventory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PhysicalInventory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new RentalInventory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new RepairOrder());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new SalesInventory());

        ////Home - Warehouse
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Contract());

        ////Settings
        this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new AccountingSettings());
        this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new GlAccount());
        this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new GlDistribution());
        this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Country());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new State());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new BillingCycle());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Department());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ContactEvent());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ContactTitle());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new MailList());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Currency());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new CreditStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new CustomerCategory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new CustomerStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new CustomerType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new DealClassification());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new DealType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new DealStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ProductionType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ScheduleType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new DiscountTemplate());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new DocumentType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new CoverLetter());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new TermsConditions());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new EventCategory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new EventType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PersonnelType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PhotographyType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Building());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new FacilityType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new FacilityRate());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new FacilityScheduleStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new FacilityStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new FacilityCategory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new SpaceType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new FiscalYear());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new GeneratorFuelType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new GeneratorMake());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new GeneratorRating());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new GeneratorWatts());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new GeneratorType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Holiday());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new BlackoutStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new BarCodeRange());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new InventoryAdjustmentReason());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Attribute());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new InventoryCondition());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new InventoryGroup());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new InventoryRank());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new InventoryStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new InventoryType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PartsCategory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new RentalCategory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new RetiredReason());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new SalesCategory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Unit());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new UnretiredReason());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WarehouseCatalog());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Crew());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new LaborRate());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new LaborPosition());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new LaborType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new LaborCategory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new CrewScheduleStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new CrewStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new MiscRate());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new MiscType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new MiscCategory());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new OfficeLocation());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new OrderType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new DiscountReason());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new MarketSegment());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new MarketType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new OrderSetNo());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new OrderLocation());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PaymentTerms());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PaymentType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new POApprovalStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new POApproverRole());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new POClassification());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new POImportance());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PORejectReason());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new POType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new POApprover());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VendorInvoiceApprover());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new FormDesign());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PresentationLayer());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ProjectAsBuild());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ProjectCommissioning());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ProjectDeposit());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ProjectDrawings());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ProjectDropShipItems());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ProjectItemsOrdered());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new PropsCondition());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Region());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new RepairItemStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new SetCondition());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new SetSurface());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new SetOpening());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WallDescription());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WallType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new ShipVia());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Source());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new AvailabilitySettings());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new DefaultSettings());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new EmailSettings());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new InventorySettings());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new LogoSettings());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new SystemSettings());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new TaxOption());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Template());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new UserStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Sound());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new LicenseClass());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VehicleColor());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VehicleFuelType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VehicleMake());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VehicleScheduleStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VehicleStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VehicleType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new OrganizationType());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VendorCatalog());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new VendorClass());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new SapVendorInvoiceStatus());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobeCare());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobeColor());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobeCondition());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobeGender());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobeLabel());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobeMaterial());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobePattern());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobePeriod());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WardrobeSource());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Warehouse());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new Widget());
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new WorkWeek());

        ////Administrator
        //this.TestModule_OpenBrowse_CreateNew_Save_Edit_Save_Delete(new User());
    }
    //---------------------------------------------------------------------------------------
}

new MediumRegressionTest().Run();
