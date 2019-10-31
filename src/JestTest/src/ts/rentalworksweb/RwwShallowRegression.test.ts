import { BaseTest } from '../shared/BaseTest';
import { ModuleBase } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import {
    //home
    Contact, Customer, Deal, Order, Project, PurchaseOrder, Quote, Vendor,
    Asset, PartsInventory, PhysicalInventory, RentalInventory, RepairOrder, SalesInventory,
    Contract, PickList, Container, Manifest, TransferOrder, TransferReceipt,
    Invoice, Receipt, VendorInvoice,

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
    InventorySettings, LogoSettings, DocumentBarCodeSettings, SystemSettings, TaxOption, Template, UserStatus, Sound, LicenseClass, VehicleColor, VehicleFuelType, VehicleMake, VehicleScheduleStatus, VehicleStatus,
    VehicleType, OrganizationType, VendorCatalog, VendorClass, SapVendorInvoiceStatus, WardrobeCare, WardrobeColor, WardrobeCondition, WardrobeGender, WardrobeLabel,
    WardrobeMaterial, WardrobePattern, WardrobePeriod, WardrobeSource, Warehouse, Widget, WorkWeek,

    //administrator
    Alert, CustomField, CustomForm, DuplicateRule, EmailHistory, Group, Hotfix, User,
} from './modules/AllModules';


export class ShallowRegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    async ShallowRegressionOnModule(module: ModuleBase, registerGlobal?: boolean) {
        let testName: string = "";
        describe(module.moduleCaption, () => {
            const testCollectionName = `Shallow Regression`;
            describe(testCollectionName, () => {
                //---------------------------------------------------------------------------------------
                testName = `Open ${module.moduleCaption} browse`;
                test(testName, async () => {
                    await module.openBrowse()
                        .then(openBrowseResponse => {
                            expect(openBrowseResponse.errorMessage).toBe("");
                            expect(openBrowseResponse.opened).toBeTruthy();
                        });
                }, module.browseOpenTimeout);
                //---------------------------------------------------------------------------------------
                if (module.canView) {       //if the module supports form viewing, try to open the first form, if any
                    testName = `Open first ${module.moduleCaption} form, if any`;
                    test(testName, async () => {
                        await module.openFirstRecordIfAny()
                            .then(openRecordResponse => {
                                expect(openRecordResponse.errorMessage).toBe("");
                                expect(openRecordResponse.opened).toBeTruthy();

                                if (registerGlobal) {
                                    let globalKey = module.moduleName;
                                    for (var key in openRecordResponse.keys) {
                                        globalKey = globalKey + "~" + openRecordResponse.keys[key];
                                    }
                                    //Logging.logInfo(`Global Key: ${globalKey}`);
                                    this.globalScopeRef[globalKey] = openRecordResponse.record;
                                }
                            });

                    }, module.formOpenTimeout);

                    //if the module supports form viewing, try to click all tabs on the form
                    testName = `Click all Tabs on the ${module.moduleCaption} form`;
                    test(testName, async () => {
                        await module.clickAllTabsOnForm()
                            .then(openRecordResponse => {
                                expect(openRecordResponse.errorMessage).toBe("");
                                expect(openRecordResponse.success).toBeTruthy();
                            });
                    }, module.formOpenTimeout);
                }
                //---------------------------------------------------------------------------------------
            });
        });
    }
    //---------------------------------------------------------------------------------------    
    async PerformTests() {

        //Home - Agent
        this.ShallowRegressionOnModule(new Contact());
        this.ShallowRegressionOnModule(new Customer());
        this.ShallowRegressionOnModule(new Deal());
        this.ShallowRegressionOnModule(new Order());
        this.ShallowRegressionOnModule(new Project());
        this.ShallowRegressionOnModule(new PurchaseOrder());
        this.ShallowRegressionOnModule(new Quote());
        this.ShallowRegressionOnModule(new Vendor());

        //Home - Inventory
        this.ShallowRegressionOnModule(new Asset());
        this.ShallowRegressionOnModule(new PartsInventory());
        this.ShallowRegressionOnModule(new PhysicalInventory());
        this.ShallowRegressionOnModule(new RentalInventory());
        this.ShallowRegressionOnModule(new RepairOrder());
        this.ShallowRegressionOnModule(new SalesInventory());

        //Home - Warehouse
        this.ShallowRegressionOnModule(new Contract());
        this.ShallowRegressionOnModule(new PickList());

        //Home - Container
        this.ShallowRegressionOnModule(new Container());

        //Home - Transfer
        this.ShallowRegressionOnModule(new Manifest());
        this.ShallowRegressionOnModule(new TransferOrder());
        this.ShallowRegressionOnModule(new TransferReceipt());

        //Home - Billing
        this.ShallowRegressionOnModule(new Invoice());
        this.ShallowRegressionOnModule(new Receipt());
        this.ShallowRegressionOnModule(new VendorInvoice());

        //Settings
        this.ShallowRegressionOnModule(new AccountingSettings());
        this.ShallowRegressionOnModule(new GlAccount());
        this.ShallowRegressionOnModule(new GlDistribution());
        this.ShallowRegressionOnModule(new Country());
        this.ShallowRegressionOnModule(new State());
        this.ShallowRegressionOnModule(new BillingCycle());
        this.ShallowRegressionOnModule(new Department());
        this.ShallowRegressionOnModule(new ContactEvent());
        this.ShallowRegressionOnModule(new ContactTitle());
        this.ShallowRegressionOnModule(new MailList());
        this.ShallowRegressionOnModule(new Currency());
        this.ShallowRegressionOnModule(new CreditStatus());
        this.ShallowRegressionOnModule(new CustomerCategory());
        this.ShallowRegressionOnModule(new CustomerStatus());
        this.ShallowRegressionOnModule(new CustomerType());
        this.ShallowRegressionOnModule(new DealClassification());
        this.ShallowRegressionOnModule(new DealType());
        this.ShallowRegressionOnModule(new DealStatus());
        this.ShallowRegressionOnModule(new ProductionType());
        this.ShallowRegressionOnModule(new ScheduleType());
        this.ShallowRegressionOnModule(new DiscountTemplate());
        this.ShallowRegressionOnModule(new DocumentType());
        this.ShallowRegressionOnModule(new CoverLetter());
        this.ShallowRegressionOnModule(new TermsConditions());
        this.ShallowRegressionOnModule(new EventCategory());
        this.ShallowRegressionOnModule(new EventType());
        this.ShallowRegressionOnModule(new PersonnelType());
        this.ShallowRegressionOnModule(new PhotographyType());
        this.ShallowRegressionOnModule(new Building());
        this.ShallowRegressionOnModule(new FacilityType());
        this.ShallowRegressionOnModule(new FacilityRate());
        this.ShallowRegressionOnModule(new FacilityScheduleStatus());
        this.ShallowRegressionOnModule(new FacilityStatus());
        this.ShallowRegressionOnModule(new FacilityCategory());
        this.ShallowRegressionOnModule(new SpaceType());
        this.ShallowRegressionOnModule(new FiscalYear());
        this.ShallowRegressionOnModule(new GeneratorFuelType());
        this.ShallowRegressionOnModule(new GeneratorMake());
        this.ShallowRegressionOnModule(new GeneratorRating());
        this.ShallowRegressionOnModule(new GeneratorWatts());
        this.ShallowRegressionOnModule(new GeneratorType());
        this.ShallowRegressionOnModule(new Holiday());
        this.ShallowRegressionOnModule(new BlackoutStatus());
        this.ShallowRegressionOnModule(new BarCodeRange());
        this.ShallowRegressionOnModule(new InventoryAdjustmentReason());
        this.ShallowRegressionOnModule(new Attribute());
        this.ShallowRegressionOnModule(new InventoryCondition());
        this.ShallowRegressionOnModule(new InventoryGroup());
        this.ShallowRegressionOnModule(new InventoryRank());
        this.ShallowRegressionOnModule(new InventoryStatus());
        this.ShallowRegressionOnModule(new InventoryType());
        this.ShallowRegressionOnModule(new PartsCategory());
        this.ShallowRegressionOnModule(new RentalCategory());
        this.ShallowRegressionOnModule(new RetiredReason());
        this.ShallowRegressionOnModule(new SalesCategory());
        this.ShallowRegressionOnModule(new Unit());
        this.ShallowRegressionOnModule(new UnretiredReason());
        this.ShallowRegressionOnModule(new WarehouseCatalog());
        this.ShallowRegressionOnModule(new Crew());
        this.ShallowRegressionOnModule(new LaborRate());
        this.ShallowRegressionOnModule(new LaborPosition());
        this.ShallowRegressionOnModule(new LaborType());
        this.ShallowRegressionOnModule(new LaborCategory());
        this.ShallowRegressionOnModule(new CrewScheduleStatus());
        this.ShallowRegressionOnModule(new CrewStatus());
        this.ShallowRegressionOnModule(new MiscRate());
        this.ShallowRegressionOnModule(new MiscType());
        this.ShallowRegressionOnModule(new MiscCategory());
        this.ShallowRegressionOnModule(new OfficeLocation());
        this.ShallowRegressionOnModule(new OrderType());
        this.ShallowRegressionOnModule(new DiscountReason());
        this.ShallowRegressionOnModule(new MarketSegment());
        this.ShallowRegressionOnModule(new MarketType());
        this.ShallowRegressionOnModule(new OrderSetNo());
        this.ShallowRegressionOnModule(new OrderLocation());
        this.ShallowRegressionOnModule(new PaymentTerms());
        this.ShallowRegressionOnModule(new PaymentType());
        this.ShallowRegressionOnModule(new POApprovalStatus());
        this.ShallowRegressionOnModule(new POApproverRole());
        this.ShallowRegressionOnModule(new POClassification());
        this.ShallowRegressionOnModule(new POImportance());
        this.ShallowRegressionOnModule(new PORejectReason());
        this.ShallowRegressionOnModule(new POType());
        this.ShallowRegressionOnModule(new POApprover());
        this.ShallowRegressionOnModule(new VendorInvoiceApprover());
        this.ShallowRegressionOnModule(new FormDesign());
        this.ShallowRegressionOnModule(new PresentationLayer());
        this.ShallowRegressionOnModule(new ProjectAsBuild());
        this.ShallowRegressionOnModule(new ProjectCommissioning());
        this.ShallowRegressionOnModule(new ProjectDeposit());
        this.ShallowRegressionOnModule(new ProjectDrawings());
        this.ShallowRegressionOnModule(new ProjectDropShipItems());
        this.ShallowRegressionOnModule(new ProjectItemsOrdered());
        this.ShallowRegressionOnModule(new PropsCondition());
        this.ShallowRegressionOnModule(new Region());
        this.ShallowRegressionOnModule(new RepairItemStatus());
        this.ShallowRegressionOnModule(new SetCondition());
        this.ShallowRegressionOnModule(new SetSurface());
        this.ShallowRegressionOnModule(new SetOpening());
        this.ShallowRegressionOnModule(new WallDescription());
        this.ShallowRegressionOnModule(new WallType());
        this.ShallowRegressionOnModule(new ShipVia());
        this.ShallowRegressionOnModule(new Source());
        this.ShallowRegressionOnModule(new AvailabilitySettings());
        this.ShallowRegressionOnModule(new DefaultSettings());
        this.ShallowRegressionOnModule(new EmailSettings());
        this.ShallowRegressionOnModule(new InventorySettings());
        this.ShallowRegressionOnModule(new LogoSettings());
        this.ShallowRegressionOnModule(new DocumentBarCodeSettings());
        this.ShallowRegressionOnModule(new SystemSettings());
        this.ShallowRegressionOnModule(new TaxOption());
        this.ShallowRegressionOnModule(new Template());
        this.ShallowRegressionOnModule(new UserStatus());
        this.ShallowRegressionOnModule(new Sound());
        this.ShallowRegressionOnModule(new LicenseClass());
        this.ShallowRegressionOnModule(new VehicleColor());
        this.ShallowRegressionOnModule(new VehicleFuelType());
        this.ShallowRegressionOnModule(new VehicleMake());
        this.ShallowRegressionOnModule(new VehicleScheduleStatus());
        this.ShallowRegressionOnModule(new VehicleStatus());
        this.ShallowRegressionOnModule(new VehicleType());
        this.ShallowRegressionOnModule(new OrganizationType());
        this.ShallowRegressionOnModule(new VendorCatalog());
        this.ShallowRegressionOnModule(new VendorClass());
        this.ShallowRegressionOnModule(new SapVendorInvoiceStatus());
        this.ShallowRegressionOnModule(new WardrobeCare());
        this.ShallowRegressionOnModule(new WardrobeColor());
        this.ShallowRegressionOnModule(new WardrobeCondition());
        this.ShallowRegressionOnModule(new WardrobeGender());
        this.ShallowRegressionOnModule(new WardrobeLabel());
        this.ShallowRegressionOnModule(new WardrobeMaterial());
        this.ShallowRegressionOnModule(new WardrobePattern());
        this.ShallowRegressionOnModule(new WardrobePeriod());
        this.ShallowRegressionOnModule(new WardrobeSource());
        this.ShallowRegressionOnModule(new Warehouse());
        this.ShallowRegressionOnModule(new Widget());
        this.ShallowRegressionOnModule(new WorkWeek());

        //Administrator
        this.ShallowRegressionOnModule(new Alert());
        this.ShallowRegressionOnModule(new CustomField());
        this.ShallowRegressionOnModule(new CustomForm());
        this.ShallowRegressionOnModule(new DuplicateRule());
        this.ShallowRegressionOnModule(new EmailHistory());
        this.ShallowRegressionOnModule(new Group());
        this.ShallowRegressionOnModule(new Hotfix());
        this.ShallowRegressionOnModule(new User());
    }
    //---------------------------------------------------------------------------------------
}

new ShallowRegressionTest().Run();
