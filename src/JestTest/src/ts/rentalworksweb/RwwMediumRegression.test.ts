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
    InventorySettings, LogoSettings, DocumentBarCodeSettings, SystemSettings, TaxOption, Template, UserStatus, Sound, LicenseClass, VehicleColor, VehicleFuelType, VehicleMake, VehicleScheduleStatus, VehicleStatus,
    VehicleType, OrganizationType, VendorCatalog, VendorClass, SapVendorInvoiceStatus, WardrobeCare, WardrobeColor, WardrobeCondition, WardrobeGender, WardrobeLabel,
    WardrobeMaterial, WardrobePattern, WardrobePeriod, WardrobeSource, Warehouse, Widget, WorkWeek,

    //administrator
    Alert, CustomField, CustomForm, CustomReportLayout, DuplicateRule, EmailHistory, Group, Hotfix, User,
} from './modules/AllModules';
import { SettingsModule } from '../shared/SettingsModule';

export class MediumRegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------

    async MediumRegressionOnModule(module: ModuleBase) {
        let testName: string = "";
        describe(module.moduleCaption, () => {
            const testCollectionName = `Medium Regression`;
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
                            if (rec.expectedErrorFields) {
                                testName += ` with missing required fields ${JSON.stringify(rec.expectedErrorFields)}`;
                            }
                            test(testName, async () => {
                                await module.populateFormWithRecord(rec.record);
                            }, module.formOpenTimeout);

                            testName = `Save new ${module.moduleCaption}`;
                            if (rec.expectedErrorFields) {
                                testName = `Attempt to save new ${module.moduleCaption}, expect missing required field error`;
                            }
                            test(testName, async () => {
                                let successfulSave: boolean = false;
                                let saveError: string = "";
                                let errorFields: string[] = new Array<string>();

                                await module.saveRecord(true)
                                    .then(saveResponse => {
                                        successfulSave = saveResponse.saved;
                                        saveError = saveResponse.errorMessage;
                                        errorFields = saveResponse.errorFields;
                                    });
                                if (successfulSave) {
                                    await module.closeRecord();  //close the form
                                }
                                else {
                                    await module.closeModifiedRecordWithoutSaving();  //close the form without saving
                                }

                                if (rec.expectedErrorFields) {
                                    expect(errorFields.length).toBeGreaterThan(0);
                                    expect(errorFields.length).toBe(rec.expectedErrorFields.length);
                                    expect(successfulSave).toBe(false);
                                }
                                else {
                                    expect(saveError).toBe("");
                                    expect(errorFields.length).toBe(0);
                                    expect(successfulSave).toBe(true);
                                }

                            }, module.formSaveTimeout);

                            if (rec.seekObject) {
                                testName = `Seek to the newly-created ${module.moduleCaption} record`;
                                test(testName, async () => {
                                    let recordCount = await module.browseSeek(rec.seekObject);//.then().catch(err => this.LogError(testName, err));
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

                                    if (module.grids) {
                                        for (let grid of module.grids) {
                                            if (grid.canNew) {
                                                for (let gridRecord of rec.gridRecords) {
                                                    if (gridRecord.grid === grid) {
                                                        testName = `Add row to Grid: ${grid.gridDisplayName}`;
                                                        if (gridRecord.recordToCreate.expectedErrorFields) {
                                                            testName += ` with missing required fields ${JSON.stringify(gridRecord.recordToCreate.expectedErrorFields)}`;
                                                        }
                                                        test(testName, async () => {
                                                            await grid.addGridRow(gridRecord.recordToCreate.record, true)
                                                                .then(saveResponse => {
                                                                    if (gridRecord.recordToCreate.expectedErrorFields) {
                                                                        expect(saveResponse.errorFields.length).toBeGreaterThan(0);
                                                                        expect(saveResponse.errorFields.length).toBe(gridRecord.recordToCreate.expectedErrorFields.length);
                                                                        expect(saveResponse.saved).toBeFalsy();
                                                                    }
                                                                    else {
                                                                        expect(saveResponse.errorMessage).toBe("");
                                                                        expect(saveResponse.saved).toBeTruthy();
                                                                    }
                                                                });
                                                        }, grid.saveTimeout);

                                                        if (gridRecord.recordToCreate.attemptDuplicate) {
                                                            testName = `Attempt to add duplicate row to Grid: ${grid.gridDisplayName}, expect duplicate error`;
                                                            test(testName, async () => {
                                                                await grid.addGridRow(gridRecord.recordToCreate.record, true)
                                                                    .then(saveResponse => {
                                                                        expect(saveResponse.errorMessage).toContain('Duplicate Rule');
                                                                        expect(saveResponse.saved).toBe(false);
                                                                    });
                                                            }, grid.saveTimeout);
                                                        }

                                                        if (!gridRecord.recordToCreate.expectedErrorFields) {
                                                            if (grid.canDelete) {
                                                                testName = `Confirm that rows exist in the Grid: ${grid.gridDisplayName}`;
                                                                test(testName, async () => {
                                                                    await grid.getRecordCount()
                                                                        .then(gridRowCount => {
                                                                            expect(gridRowCount).toBeGreaterThan(0);
                                                                        });
                                                                }, grid.deleteTimeout);

                                                                testName = `Delete row from Grid: ${grid.gridDisplayName}`;
                                                                test(testName, async () => {
                                                                    let rowIndex: number = 1;

                                                                    if (gridRecord.recordToCreate.seekObject) {
                                                                        await grid.getRecordRowIndex(gridRecord.recordToCreate.seekObject)
                                                                            .then(i => {
                                                                                rowIndex = i;
                                                                            });
                                                                        expect(rowIndex).toBeGreaterThan(0);
                                                                    }

                                                                    await grid.deleteGridRow(rowIndex, true)
                                                                        .then(deleteResponse => {
                                                                            expect(deleteResponse.errorMessage).toBe("");
                                                                            expect(deleteResponse.deleted).toBeTruthy();
                                                                        });
                                                                }, grid.deleteTimeout);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else {
                                                //test to make sure grid New button not accessible
                                                testName = `Confirm no NEW option for Grid: ${grid.gridDisplayName}`;
                                                test(testName, async () => {
                                                    await grid.checkForNewButton()
                                                        .then(optionExists => {
                                                            expect(optionExists).toBeFalsy();
                                                        });
                                                }, grid.deleteTimeout);
                                            }

                                            //if ((grid.canNew) && (!grid.canEdit)) {  // not really a valid scenario.  But to test this, we first need to add a record, then try to edit it
                                            //    //  test to make sure no grid Edit operation 
                                            //    testName = `Confirm no Edit option for Grid: ${grid.gridDisplayName}`;
                                            //    test(testName, async () => {
                                            //        await grid.checkForEditAbility()
                                            //            .then(optionExists => {
                                            //                expect(optionExists).toBeFalsy();
                                            //            });
                                            //    }, grid.deleteTimeout);
                                            //}

                                            if (!grid.canDelete) {
                                                //  test to make sure no grid Delete option 
                                                testName = `Confirm no DELETE option for Grid: ${grid.gridDisplayName}`;
                                                test(testName, async () => {
                                                    await grid.checkForDeleteOption()
                                                        .then(optionExists => {
                                                            expect(optionExists).toBeFalsy();
                                                        });
                                                }, grid.deleteTimeout);
                                            }

                                            if ((!grid.canNew) && (grid.canEdit)) {
                                                //  check to make sure at least one row exists  // doing this because there is now way to check for grid editability of there is not already a row in the grid
                                                testName = `Confirm that a row exists in the Grid, and that EDIT option exists: ${grid.gridDisplayName}`;
                                                test(testName, async () => {
                                                    await grid.getRecordCount()
                                                        .then(async gridRowCount => {
                                                            expect(gridRowCount).toBeGreaterThan(0);

                                                            await grid.checkForEditAbility()
                                                                .then(optionExists => {
                                                                    expect(optionExists).toBeTruthy();
                                                                });

                                                        });
                                                }, grid.editTimeout);
                                            }

                                            if ((!grid.canNew) && (grid.canDelete)) {   // unusual, but possible I guess
                                                //  check to make sure at least one row exists
                                                testName = `Confirm that a row exists in the Grid, and that data can be deleted: ${grid.gridDisplayName}`;
                                                test(testName, async () => {
                                                    await grid.getRecordCount()
                                                        .then(async gridRowCount => {
                                                            expect(gridRowCount).toBeGreaterThan(0);

                                                            await grid.deleteGridRow(1, true)
                                                                .then(deleteResponse => {
                                                                    expect(deleteResponse.errorMessage).toBe("");
                                                                    expect(deleteResponse.deleted).toBeTruthy();
                                                                });

                                                        });
                                                }, grid.deleteTimeout);

                                            }


                                        }
                                    }

                                    testName = `Close the ${module.moduleCaption} record`;
                                    test(testName, async () => {
                                        await module.closeRecord();  //close the form
                                    }, module.formOpenTimeout);
                                }

                                if (rec.attemptDuplicate) {
                                    testName = `Attempt to create duplicate new ${module.moduleCaption}`;
                                    test(testName, async () => {
                                        await module.createNewRecord()
                                            .then(createNewResponse => {
                                                expect(createNewResponse.errorMessage).toBe("");
                                                expect(createNewResponse.success).toBeTruthy();
                                            });
                                    }, module.formOpenTimeout);

                                    testName = `Populate duplicate new ${module.moduleCaption}`;
                                    test(testName, async () => {
                                        await module.populateFormWithRecord(rec.record);
                                    }, module.formOpenTimeout);

                                    testName = `Attempt to save duplicate ${module.moduleCaption}, expect duplicate error`;
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

                                        expect(saveError).toContain('Duplicate Rule');
                                        expect(successfulSave).toBe(false);

                                    }, module.formSaveTimeout);
                                }

                                if (module.canDelete) {
                                    testName = `Seek to the newly-created ${module.moduleCaption} record`;
                                    test(testName, async () => {
                                        let recordCount = await module.browseSeek(rec.seekObject);//.then().catch(err => this.LogError(testName, err));
                                        expect(recordCount).toBe(1);
                                    }, module.browseSeekTimeout);

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
    async RelogAsCopyOfUser() {

        this.LoadMyUserGlobal(new User());
        this.CopyMyUserRegisterGlobal(new User());
        this.DoLogoff();
        this.DoLogin();  // uses new login account

    }
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        //prerequisites

        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        this.OpenSpecificRecord(new InventorySettings(), null, true);

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
        this.MediumRegressionOnModule(new EventType());
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
        this.MediumRegressionOnModule(new OrderSetNo());
        this.MediumRegressionOnModule(new OrderLocation());
        this.MediumRegressionOnModule(new PaymentTerms());
        this.MediumRegressionOnModule(new PaymentType());
        this.MediumRegressionOnModule(new POApprovalStatus());
        this.MediumRegressionOnModule(new POApproverRole());
        this.MediumRegressionOnModule(new POClassification());
        this.MediumRegressionOnModule(new POImportance());
        this.MediumRegressionOnModule(new PORejectReason());
        this.MediumRegressionOnModule(new POType());
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

        //Administrator
        this.MediumRegressionOnModule(new Alert());
        this.MediumRegressionOnModule(new CustomField());
        this.MediumRegressionOnModule(new CustomForm());
        this.MediumRegressionOnModule(new CustomReportLayout());
        this.MediumRegressionOnModule(new DuplicateRule());
        this.MediumRegressionOnModule(new EmailHistory());
        this.MediumRegressionOnModule(new Group());
        this.MediumRegressionOnModule(new Hotfix());
        this.MediumRegressionOnModule(new User());
    }
    //---------------------------------------------------------------------------------------
}

new MediumRegressionTest().Run();
