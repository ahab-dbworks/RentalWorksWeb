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


export class ShallowRegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        //Home - Agent
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Contact());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Customer());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Deal());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Order());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Project());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PurchaseOrder());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Quote());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Vendor());

        //Home - Inventory
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Asset());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PartsInventory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PhysicalInventory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new RentalInventory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new RepairOrder());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new SalesInventory());

        //Home - Warehouse
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Contract());

        //Settings
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new AccountingSettings());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new GlAccount());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new GlDistribution());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Country());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new State());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new BillingCycle());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Department());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ContactEvent());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ContactTitle());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new MailList());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Currency());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new CreditStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new CustomerCategory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new CustomerStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new CustomerType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new DealClassification());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new DealType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new DealStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ProductionType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ScheduleType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new DiscountTemplate());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new DocumentType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new CoverLetter());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new TermsConditions());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new EventCategory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new EventType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PersonnelType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PhotographyType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Building());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new FacilityType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new FacilityRate());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new FacilityScheduleStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new FacilityStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new FacilityCategory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new SpaceType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new FiscalYear());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new GeneratorFuelType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new GeneratorMake());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new GeneratorRating());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new GeneratorWatts());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new GeneratorType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Holiday());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new BlackoutStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new BarCodeRange());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new InventoryAdjustmentReason());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Attribute());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new InventoryCondition());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new InventoryGroup());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new InventoryRank());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new InventoryStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new InventoryType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PartsCategory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new RentalCategory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new RetiredReason());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new SalesCategory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Unit());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new UnretiredReason());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WarehouseCatalog());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Crew());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new LaborRate());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new LaborPosition());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new LaborType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new LaborCategory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new CrewScheduleStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new CrewStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new MiscRate());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new MiscType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new MiscCategory());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new OfficeLocation());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new OrderType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new DiscountReason());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new MarketSegment());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new MarketType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new OrderSetNo());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new OrderLocation());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PaymentTerms());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PaymentType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new POApprovalStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new POApproverRole());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new POClassification());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new POImportance());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PORejectReason());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new POType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new POApprover());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VendorInvoiceApprover());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new FormDesign());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PresentationLayer());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ProjectAsBuild());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ProjectCommissioning());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ProjectDeposit());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ProjectDrawings());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ProjectDropShipItems());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ProjectItemsOrdered());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new PropsCondition());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Region());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new RepairItemStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new SetCondition());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new SetSurface());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new SetOpening());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WallDescription());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WallType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new ShipVia());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Source());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new AvailabilitySettings());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new DefaultSettings());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new EmailSettings());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new InventorySettings());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new LogoSettings());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new SystemSettings());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new TaxOption());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Template());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new UserStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Sound());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new LicenseClass());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VehicleColor());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VehicleFuelType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VehicleMake());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VehicleScheduleStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VehicleStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VehicleType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new OrganizationType());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VendorCatalog());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new VendorClass());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new SapVendorInvoiceStatus());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobeCare());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobeColor());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobeCondition());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobeGender());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobeLabel());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobeMaterial());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobePattern());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobePeriod());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WardrobeSource());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Warehouse());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new Widget());
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new WorkWeek());

        //Administrator
        this.TestModule_OpenBrowse_OpenFirstFormIfAny_ClickAllTabs(new User());
    }
    //---------------------------------------------------------------------------------------
}

new ShallowRegressionTest().Run();
