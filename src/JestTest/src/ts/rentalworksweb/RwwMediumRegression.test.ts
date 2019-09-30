import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
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
    InventorySettings, LogoSettings, SystemSettings, TaxOption, Template, UserStatus, Sound, LicenseClass, VehicleColor, VehicleFuelType, VehicleMake, VehicleScheduleStatus, VehicleStatus,
    VehicleType, OrganizationType, VendorCatalog, VendorClass, SapVendorInvoiceStatus, WardrobeCare, WardrobeColor, WardrobeCondition, WardrobeGender, WardrobeLabel,
    WardrobeMaterial, WardrobePattern, WardrobePeriod, WardrobeSource, Warehouse, Widget, WorkWeek,

    //administrator
    Alert, CustomField, CustomForm, DuplicateRule, EmailHistory, Group, Hotfix, User,
} from './modules/AllModules';
import { SettingsModule } from '../shared/SettingsModule';

export class MediumRegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------

    async MediumRegressionOnModule(module: ModuleBase) {
        let testName: string = "";
        describe(module.moduleCaption, () => {
            const testCollectionName = `Open browse, create new (if allowed), save, seek, open, validate, close, seek, delete (if allowed)`;
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
                if (module.canNew) {     //if the module supports adding new records, try to add one
                    if (module.newRecordsToCreate) {
                        for (let rec of module.newRecordsToCreate) {

                            testName = `Create new ${module.moduleCaption}`;
                            test(testName, async () => {
                                await module.createNewRecord()
                                    .then(createNewResponse => {
                                        expect(createNewResponse.errorMessage).toBe("");
                                        expect(createNewResponse.success).toBeTruthy();
                                    });
                            }, module.formOpenTimeout);

                            if (module.defaultNewRecordToExpect) {
                                testName = `Validate default values on new ${module.moduleCaption} form, compare with expected values`;
                                test(testName, async () => {
                                    await module.getFormRecord()
                                        .then(defaultObject => {
                                            for (let key in module.defaultNewRecordToExpect) {
                                                let expectingValue = module.defaultNewRecordToExpect[key];
                                                let foundValue = defaultObject[key];

                                                if (expectingValue.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                                                    expectingValue = TestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                }

                                                Logging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                if (expectingValue === "|NOTEMPTY|") {
                                                    expect(foundValue).not.toBe("");
                                                }
                                                else {
                                                    expect(foundValue).not.toBeUndefined();
                                                    expect(foundValue).toBe(expectingValue);
                                                }
                                            }
                                        });
                                }, module.formOpenTimeout);
                            }

                            testName = `Populate new ${module.moduleCaption}`;
                            test(testName, async () => {
                                await module.populateFormWithRecord(rec.record);
                            }, module.formOpenTimeout);

                            testName = `Save new ${module.moduleCaption}`;
                            test(testName, async () => {
                                let successfulSave: boolean = false;
                                let saveError: string = "";
                                await module.saveRecord(true)
                                    .then(saveResponse => {
                                        successfulSave = saveResponse.saved;
                                        saveError = saveResponse.errorMessage;
                                    });
                                if (successfulSave) {
                                    await module.closeRecord();  //close the form
                                }
                                else {
                                    await module.closeModifiedRecordWithoutSaving();  //close the form without saving
                                }
                                expect(saveError).toBe("");
                                expect(successfulSave).toBe(true);

                            }, module.formSaveTimeout);

                            if (rec.seekObject) {
                                testName = `Seek to the newly-created ${module.moduleCaption} record`;
                                test(testName, async () => {
                                    let recordCount = await module.browseSeek(rec.seekObject).then().catch(err => this.LogError(testName, err));
                                    expect(recordCount).toBe(1);
                                }, module.browseSeekTimeout);

                                if (rec.recordToExpect) {
                                    testName = `Open the newly-created ${module.moduleCaption} record, compare values with expected`;
                                    test(testName, async () => {
                                        await module.openFirstRecordIfAny()
                                            .then(openRecordResponse => {
                                                for (let key in rec.recordToExpect) {
                                                    let expectingValue = rec.recordToExpect[key];
                                                    let foundValue = openRecordResponse.record[key];//.toUpperCase();

                                                    if (expectingValue.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                                                        expectingValue = TestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                    }

                                                    Logging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                    if (expectingValue === "|NOTEMPTY|") {
                                                        expect(foundValue).not.toBe("");
                                                    }
                                                    else {
                                                        expect(foundValue).not.toBeUndefined();
                                                        expect(foundValue).toBe(expectingValue);
                                                    }
                                                }
                                            });
                                    }, module.formOpenTimeout);

                                    if ((module instanceof SettingsModule)) {
                                        testName = `Seek to the newly-created ${module.moduleCaption} record`;
                                        test(testName, async () => {
                                            let recordCount = await module.browseSeek(rec.seekObject).then().catch(err => this.LogError(testName, err));
                                            expect(recordCount).toBe(1);
                                        }, module.browseOpenTimeout);
                                    }
                                    else {
                                        testName = `Close the ${module.moduleCaption} record`;
                                        test(testName, async () => {
                                            await module.closeRecord();  //close the form
                                        }, module.formOpenTimeout);
                                    }
                                }

                                if (module.canDelete) {
                                    testName = `Delete the ${module.moduleCaption} record`;
                                    test(testName, async () => {
                                        let successfulDelete: boolean = false;
                                        let deleteError: string = "";
                                        let rowCountBefore = await module.browseGetRowsDisplayed();
                                        await module.deleteRecord(1, true)
                                            .then(deleteResponse => {
                                                successfulDelete = deleteResponse.deleted;
                                                deleteError = deleteResponse.errorMessage;
                                            });
                                        let rowCountAfter = await module.browseGetRowsDisplayed();
                                        expect(deleteError).toBe("");
                                        expect(successfulDelete).toBe(true);
                                        expect(rowCountAfter).toBe(rowCountBefore - 1);
                                    }, module.deleteTimeout);
                                }
                            }
                        }
                    }
                }
                else {
                    // make sure that the New button is not available
                    testName = `Make sure no New button exists on ${module.moduleCaption} browse`;
                    test(testName, async () => {
                        let buttonExists = await module.findNewButton();
                        expect(buttonExists).toBeFalsy();
                    }, module.browseOpenTimeout);
                }

                if (!module.canDelete) {
                    // make sure that the Delete button is not available
                    testName = `Make sure no Delete button exists on ${module.moduleCaption} browse`;
                    test(testName, async () => {
                        let buttonExists = await module.findDeleteButton();
                        expect(buttonExists).toBeFalsy();
                    }, module.browseOpenTimeout);
                }


                //---------------------------------------------------------------------------------------
            });
        });
    }
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        //prerequisites
        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);

        let warehouseToSeek: any = {
            Warehouse: "GlobalScope.User~ME.Warehouse",
        }
        this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        //Home - Agent
        this.MediumRegressionOnModule(new Contact());
        this.MediumRegressionOnModule(new Customer());
        this.MediumRegressionOnModule(new Deal());
        this.MediumRegressionOnModule(new Order());
        this.MediumRegressionOnModule(new Project());
        this.MediumRegressionOnModule(new PurchaseOrder());
        this.MediumRegressionOnModule(new Quote());
        this.MediumRegressionOnModule(new Vendor());

        //Home - Inventory
        //this.MediumRegressionOnModule(new Asset());
        this.MediumRegressionOnModule(new PartsInventory());
        this.MediumRegressionOnModule(new PhysicalInventory());
        this.MediumRegressionOnModule(new RentalInventory());
        //this.MediumRegressionOnModule(new RepairOrder());
        this.MediumRegressionOnModule(new SalesInventory());

        //Home - Warehouse
        //this.MediumRegressionOnModule(new Contract());
        //this.MediumRegressionOnModule(new PickList());

        //Home - Container
        //this.MediumRegressionOnModule(new Container());

        //Home - Transfer
        //this.MediumRegressionOnModule(new Manifest());
        this.MediumRegressionOnModule(new TransferOrder());
        //this.MediumRegressionOnModule(new TransferReceipt());

        //Home - Billing
        this.MediumRegressionOnModule(new Invoice());
        this.MediumRegressionOnModule(new Receipt());
        this.MediumRegressionOnModule(new VendorInvoice());

        //Settings
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
        //this.MediumRegressionOnModule(new CustomerType());
        //this.MediumRegressionOnModule(new DealClassification());
        //this.MediumRegressionOnModule(new DealType());
        //this.MediumRegressionOnModule(new DealStatus());
        //this.MediumRegressionOnModule(new ProductionType());
        //this.MediumRegressionOnModule(new ScheduleType());
        //this.MediumRegressionOnModule(new DiscountTemplate());
        //this.MediumRegressionOnModule(new DocumentType());
        //this.MediumRegressionOnModule(new CoverLetter());
        //this.MediumRegressionOnModule(new TermsConditions());
        //this.MediumRegressionOnModule(new EventCategory());
        //this.MediumRegressionOnModule(new EventType());
        //this.MediumRegressionOnModule(new PersonnelType());
        //this.MediumRegressionOnModule(new PhotographyType());
        //this.MediumRegressionOnModule(new Building());
        //this.MediumRegressionOnModule(new FacilityType());
        //this.MediumRegressionOnModule(new FacilityRate());
        //this.MediumRegressionOnModule(new FacilityScheduleStatus());
        //this.MediumRegressionOnModule(new FacilityStatus());
        //this.MediumRegressionOnModule(new FacilityCategory());
        //this.MediumRegressionOnModule(new SpaceType());
        //this.MediumRegressionOnModule(new FiscalYear());
        //this.MediumRegressionOnModule(new GeneratorFuelType());
        //this.MediumRegressionOnModule(new GeneratorMake());
        //this.MediumRegressionOnModule(new GeneratorRating());
        //this.MediumRegressionOnModule(new GeneratorWatts());
        //this.MediumRegressionOnModule(new GeneratorType());
        //this.MediumRegressionOnModule(new Holiday());
        //this.MediumRegressionOnModule(new BlackoutStatus());
        //this.MediumRegressionOnModule(new BarCodeRange());
        //this.MediumRegressionOnModule(new InventoryAdjustmentReason());
        //this.MediumRegressionOnModule(new Attribute());
        //this.MediumRegressionOnModule(new InventoryCondition());
        //this.MediumRegressionOnModule(new InventoryGroup());
        //this.MediumRegressionOnModule(new InventoryRank());
        //this.MediumRegressionOnModule(new InventoryStatus());
        //this.MediumRegressionOnModule(new InventoryType());
        //this.MediumRegressionOnModule(new PartsCategory());
        //this.MediumRegressionOnModule(new RentalCategory());
        //this.MediumRegressionOnModule(new RetiredReason());
        //this.MediumRegressionOnModule(new SalesCategory());
        //this.MediumRegressionOnModule(new Unit());
        //this.MediumRegressionOnModule(new UnretiredReason());
        //this.MediumRegressionOnModule(new WarehouseCatalog());
        //this.MediumRegressionOnModule(new Crew());
        //this.MediumRegressionOnModule(new LaborRate());
        //this.MediumRegressionOnModule(new LaborPosition());
        //this.MediumRegressionOnModule(new LaborType());
        //this.MediumRegressionOnModule(new LaborCategory());
        //this.MediumRegressionOnModule(new CrewScheduleStatus());
        //this.MediumRegressionOnModule(new CrewStatus());
        //this.MediumRegressionOnModule(new MiscRate());
        //this.MediumRegressionOnModule(new MiscType());
        //this.MediumRegressionOnModule(new MiscCategory());
        //this.MediumRegressionOnModule(new OfficeLocation());
        //this.MediumRegressionOnModule(new OrderType());
        //this.MediumRegressionOnModule(new DiscountReason());
        //this.MediumRegressionOnModule(new MarketSegment());
        //this.MediumRegressionOnModule(new MarketType());
        //this.MediumRegressionOnModule(new OrderSetNo());
        //this.MediumRegressionOnModule(new OrderLocation());
        //this.MediumRegressionOnModule(new PaymentTerms());
        //this.MediumRegressionOnModule(new PaymentType());
        //this.MediumRegressionOnModule(new POApprovalStatus());
        //this.MediumRegressionOnModule(new POApproverRole());
        //this.MediumRegressionOnModule(new POClassification());
        //this.MediumRegressionOnModule(new POImportance());
        //this.MediumRegressionOnModule(new PORejectReason());
        //this.MediumRegressionOnModule(new POType());
        //this.MediumRegressionOnModule(new POApprover());
        //this.MediumRegressionOnModule(new VendorInvoiceApprover());
        //this.MediumRegressionOnModule(new FormDesign());
        //this.MediumRegressionOnModule(new PresentationLayer());
        //this.MediumRegressionOnModule(new ProjectAsBuild());
        //this.MediumRegressionOnModule(new ProjectCommissioning());
        //this.MediumRegressionOnModule(new ProjectDeposit());
        //this.MediumRegressionOnModule(new ProjectDrawings());
        //this.MediumRegressionOnModule(new ProjectDropShipItems());
        //this.MediumRegressionOnModule(new ProjectItemsOrdered());
        //this.MediumRegressionOnModule(new PropsCondition());
        //this.MediumRegressionOnModule(new Region());
        //this.MediumRegressionOnModule(new RepairItemStatus());
        //this.MediumRegressionOnModule(new SetCondition());
        //this.MediumRegressionOnModule(new SetSurface());
        //this.MediumRegressionOnModule(new SetOpening());
        //this.MediumRegressionOnModule(new WallDescription());
        //this.MediumRegressionOnModule(new WallType());
        //this.MediumRegressionOnModule(new ShipVia());
        //this.MediumRegressionOnModule(new Source());
        //this.MediumRegressionOnModule(new AvailabilitySettings());
        //this.MediumRegressionOnModule(new DefaultSettings());
        //this.MediumRegressionOnModule(new EmailSettings());
        //this.MediumRegressionOnModule(new InventorySettings());
        //this.MediumRegressionOnModule(new LogoSettings());
        //this.MediumRegressionOnModule(new SystemSettings());
        //this.MediumRegressionOnModule(new TaxOption());
        //this.MediumRegressionOnModule(new Template());
        //this.MediumRegressionOnModule(new UserStatus());
        //this.MediumRegressionOnModule(new Sound());
        //this.MediumRegressionOnModule(new LicenseClass());
        //this.MediumRegressionOnModule(new VehicleColor());
        //this.MediumRegressionOnModule(new VehicleFuelType());
        //this.MediumRegressionOnModule(new VehicleMake());
        //this.MediumRegressionOnModule(new VehicleScheduleStatus());
        //this.MediumRegressionOnModule(new VehicleStatus());
        //this.MediumRegressionOnModule(new VehicleType());
        //this.MediumRegressionOnModule(new OrganizationType());
        //this.MediumRegressionOnModule(new VendorCatalog());
        //this.MediumRegressionOnModule(new VendorClass());
        //this.MediumRegressionOnModule(new SapVendorInvoiceStatus());
        //this.MediumRegressionOnModule(new WardrobeCare());
        //this.MediumRegressionOnModule(new WardrobeColor());
        //this.MediumRegressionOnModule(new WardrobeCondition());
        //this.MediumRegressionOnModule(new WardrobeGender());
        //this.MediumRegressionOnModule(new WardrobeLabel());
        //this.MediumRegressionOnModule(new WardrobeMaterial());
        //this.MediumRegressionOnModule(new WardrobePattern());
        //this.MediumRegressionOnModule(new WardrobePeriod());
        //this.MediumRegressionOnModule(new WardrobeSource());
        //this.MediumRegressionOnModule(new Warehouse());
        //this.MediumRegressionOnModule(new Widget());
        //this.MediumRegressionOnModule(new WorkWeek());

        ////Administrator
        //this.MediumRegressionOnModule(new Alert());
        //this.MediumRegressionOnModule(new CustomField());
        //this.MediumRegressionOnModule(new CustomForm());
        //this.MediumRegressionOnModule(new DuplicateRule());
        //this.MediumRegressionOnModule(new EmailHistory());
        //this.MediumRegressionOnModule(new Group());
        //this.MediumRegressionOnModule(new Hotfix());
        //this.MediumRegressionOnModule(new User());

    }
    //---------------------------------------------------------------------------------------
}

new MediumRegressionTest().Run();
