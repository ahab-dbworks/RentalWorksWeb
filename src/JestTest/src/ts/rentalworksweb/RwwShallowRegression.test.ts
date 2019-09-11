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
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Contact());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Customer());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Deal());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Order());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Project());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PurchaseOrder());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Quote());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Vendor());

        //Home - Inventory
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Asset());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PartsInventory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PhysicalInventory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new RentalInventory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new RepairOrder());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new SalesInventory());

        //Home - Warehouse
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Contract());

        //Settings
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new AccountingSettings());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new GlAccount());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new GlDistribution());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Country());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new State());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new BillingCycle());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Department());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ContactEvent());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ContactTitle());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new MailList());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Currency());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new CreditStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new CustomerCategory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new CustomerStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new CustomerType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new DealClassification());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new DealType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new DealStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ProductionType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ScheduleType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new DiscountTemplate());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new DocumentType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new CoverLetter());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new TermsConditions());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new EventCategory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new EventType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PersonnelType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PhotographyType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Building());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new FacilityType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new FacilityRate());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new FacilityScheduleStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new FacilityStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new FacilityCategory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new SpaceType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new FiscalYear());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new GeneratorFuelType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new GeneratorMake());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new GeneratorRating());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new GeneratorWatts());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new GeneratorType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Holiday());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new BlackoutStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new BarCodeRange());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new InventoryAdjustmentReason());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Attribute());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new InventoryCondition());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new InventoryGroup());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new InventoryRank());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new InventoryStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new InventoryType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PartsCategory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new RentalCategory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new RetiredReason());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new SalesCategory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Unit());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new UnretiredReason());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WarehouseCatalog());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Crew());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new LaborRate());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new LaborPosition());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new LaborType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new LaborCategory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new CrewScheduleStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new CrewStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new MiscRate());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new MiscType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new MiscCategory());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new OfficeLocation());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new OrderType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new DiscountReason());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new MarketSegment());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new MarketType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new OrderSetNo());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new OrderLocation());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PaymentTerms());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PaymentType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new POApprovalStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new POApproverRole());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new POClassification());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new POImportance());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PORejectReason());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new POType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new POApprover());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VendorInvoiceApprover());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new FormDesign());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PresentationLayer());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ProjectAsBuild());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ProjectCommissioning());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ProjectDeposit());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ProjectDrawings());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ProjectDropShipItems());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ProjectItemsOrdered());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new PropsCondition());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Region());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new RepairItemStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new SetCondition());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new SetSurface());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new SetOpening());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WallDescription());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WallType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new ShipVia());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Source());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new AvailabilitySettings());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new DefaultSettings());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new EmailSettings());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new InventorySettings());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new LogoSettings());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new SystemSettings());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new TaxOption());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Template());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new UserStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Sound());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new LicenseClass());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VehicleColor());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VehicleFuelType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VehicleMake());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VehicleScheduleStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VehicleStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VehicleType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new OrganizationType());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VendorCatalog());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new VendorClass());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new SapVendorInvoiceStatus());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobeCare());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobeColor());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobeCondition());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobeGender());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobeLabel());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobeMaterial());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobePattern());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobePeriod());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WardrobeSource());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Warehouse());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new Widget());
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new WorkWeek());

        //Administrator
        this.TestModuleOpenBrowseOpenFirstFormIfAny(new User());
    }
    //---------------------------------------------------------------------------------------
}

new ShallowRegressionTest().Run();
