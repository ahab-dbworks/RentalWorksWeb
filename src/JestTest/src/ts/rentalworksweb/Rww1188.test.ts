import { BaseTest } from '../shared/BaseTest';
import { ModuleBase } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { Contact, Order, DefaultSettings, User } from './modules/AllModules';

export class IssueRww1188Test extends BaseTest {
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

                                    if (module.grids) {
                                        for (let grid of module.grids) {
                                            for (let gridRecord of rec.gridRecords) {
                                                if (gridRecord.grid === grid) {
                                                    if (grid.canNew) {
                                                        testName = `Add row to Grid: ${grid.gridName}`;
                                                        test(testName, async () => {
                                                            await module.addGridRow(grid.gridName, grid.gridClass, gridRecord.recordToCreate.record, true)
                                                                .then(saveResponse => {
                                                                    expect(saveResponse.errorMessage).toBe("");
                                                                    expect(saveResponse.saved).toBeTruthy();
                                                                });
                                                        }, grid.saveTimeout);
                                                    }

                                                    if (grid.canDelete) {
                                                        testName = `Delete row from Grid: ${grid.gridName}`;
                                                        test(testName, async () => {
                                                            await module.deleteGridRow(grid.gridName, grid.gridClass, 1, true)
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
                                        let recordCount = await module.browseSeek(rec.seekObject).then().catch(err => this.LogError(testName, err));
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
        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        this.MediumRegressionOnModule(new Contact());
        this.MediumRegressionOnModule(new Order());
    }
    //---------------------------------------------------------------------------------------
}

new IssueRww1188Test().Run();
