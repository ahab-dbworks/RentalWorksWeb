import { BaseTest } from '../shared/BaseTest';
import { ModuleBase } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { Alert } from './modules/AllModules';
import { SettingsModule } from '../shared/SettingsModule';

export class Issue1141Test extends BaseTest {
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
        this.MediumRegressionOnModule(new Alert());
    }
    //---------------------------------------------------------------------------------------
}
new Issue1141Test().Run();
