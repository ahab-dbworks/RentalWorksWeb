import { BaseTest } from '../shared/BaseTest';
import { ModuleBase } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { User } from './modules/AllModules';

export class MediumRegressionBaseTest extends BaseTest {
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
                                                if (expectingValue === ModuleBase.NOTEMPTY) {
                                                    let expectedStr = `${key} value is ""`;
                                                    let actualStr = `${key} value is "${foundValue}"`;
                                                    expect(actualStr).not.toBe(expectedStr);
                                                }
                                                else {
                                                    let expectedStr = `${key} value is undefined`;
                                                    let actualStr = `${key} value is ${foundValue == undefined ? "undefined" : foundValue}`;
                                                    expect(actualStr).not.toBe(expectedStr);

                                                    expectedStr = `${key} value is ${expectingValue}`;
                                                    actualStr = `${key} value is ${foundValue}`;
                                                    expect(actualStr).toBe(expectedStr);
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
                                        await module.openRecord()
                                            .then(openRecordResponse => {
                                                for (let key in rec.recordToExpect) {
                                                    let expectingValue = rec.recordToExpect[key];
                                                    let foundValue = openRecordResponse.record[key];//.toUpperCase();

                                                    if (expectingValue.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                                                        expectingValue = TestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                    }

                                                    Logging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                    if (expectingValue === ModuleBase.NOTEMPTY) {
                                                        let expectedStr = `${key} value is ""`;
                                                        let actualStr = `${key} value is "${foundValue}"`;
                                                        expect(actualStr).not.toBe(expectedStr);
                                                    }
                                                    else {
                                                        let expectedStr = `${key} value is undefined`;
                                                        let actualStr = `${key} value is ${foundValue == undefined ? "undefined" : foundValue}`;
                                                        expect(actualStr).not.toBe(expectedStr);

                                                        expectedStr = `${key} value is ${expectingValue}`;
                                                        actualStr = `${key} value is ${foundValue}`;
                                                        expect(actualStr).toBe(expectedStr);
                                                    }
                                                }
                                            });
                                    }, module.formOpenTimeout);

                                    if (module.grids) {
                                        for (let grid of module.grids) {
                                            if (grid.canNew) {
                                                if (rec.gridRecords) {
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

                                                                //attempto edit the row
                                                                if (grid.canEdit) {

                                                                    //  check to make sure at least one row exists, and that it can be put into Edit mode
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

                                                                    if (gridRecord.recordToCreate.editRecord) {

                                                                        //  attempt to edit the row
                                                                        testName = `Apply edits to row in: ${grid.gridDisplayName}`;
                                                                        test(testName, async () => {
                                                                            let rowIndex: number = 1;

                                                                            if (gridRecord.recordToCreate.seekObject) {
                                                                                await grid.getRecordRowIndex(gridRecord.recordToCreate.seekObject)
                                                                                    .then(i => {
                                                                                        rowIndex = i;
                                                                                    });
                                                                                expect(rowIndex).toBeGreaterThan(0);
                                                                            }

                                                                            await grid.editGridRow(rowIndex, gridRecord.recordToCreate.editRecord.record, true)
                                                                                .then(editResponse => {
                                                                                    expect(editResponse.errorMessage).toBe("");
                                                                                    expect(editResponse.saved).toBeTruthy();
                                                                                });

                                                                        }, grid.editTimeout);

                                                                        // todo: find the row again and compare with expected
                                                                    }
                                                                }

                                                                //if (grid.canDelete) {
                                                                if ((grid.canDelete) && (!gridRecord.recordToCreate.persistData)) {
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


                                                // check each record 
                                                if (rec.gridRecords) {
                                                    for (let gridRecord of rec.gridRecords) {
                                                        if (gridRecord.grid === grid) {
                                                            if ((gridRecord.recordToEdit) && (!gridRecord.recordToCreate)) {

                                                                //  attempt to edit the row
                                                                testName = `Apply edits to row in: ${grid.gridDisplayName}`;
                                                                test(testName, async () => {
                                                                    let rowIndex: number = 1;

                                                                    if (gridRecord.recordToEdit.seekObject) {
                                                                        await grid.getRecordRowIndex(gridRecord.recordToEdit.seekObject)
                                                                            .then(i => {
                                                                                rowIndex = i;
                                                                            });
                                                                        expect(rowIndex).toBeGreaterThan(0);
                                                                    }

                                                                    await grid.editGridRow(rowIndex, gridRecord.recordToEdit.record, true)
                                                                        .then(editResponse => {
                                                                            expect(editResponse.errorMessage).toBe("");
                                                                            expect(editResponse.saved).toBeTruthy();
                                                                        });

                                                                }, grid.editTimeout);

                                                                // todo: find the row again and compare with expected

                                                            }
                                                        }
                                                    }
                                                }

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

                                if (rec.editRecord) {
                                    testName = `Seek to the newly-created ${module.moduleCaption} record`;
                                    test(testName, async () => {
                                        let recordCount = await module.browseSeek(rec.seekObject);//.then().catch(err => this.LogError(testName, err));
                                        expect(recordCount).toBe(1);
                                    }, module.browseSeekTimeout);

                                    // open 
                                    testName = `Open the newly-created ${module.moduleCaption} record for editing`;
                                    test(testName, async () => {
                                        await module.openRecord()
                                            .then(openRecordResponse => {
                                                expect(openRecordResponse.errorMessage).toBe("");
                                                expect(openRecordResponse.record).toBeDefined();
                                                expect(openRecordResponse.opened).toBeTruthy();
                                            });

                                    }, module.formOpenTimeout);

                                    //edit
                                    testName = `Modify/Edit ${module.moduleCaption}`;
                                    if (rec.editRecord.expectedErrorFields) {
                                        testName += ` with missing required fields ${JSON.stringify(rec.editRecord.expectedErrorFields)}`;
                                    }
                                    test(testName, async () => {
                                        await module.populateFormWithRecord(rec.editRecord.record);
                                    }, module.formOpenTimeout);

                                    //save
                                    testName = `Save edited ${module.moduleCaption}`;
                                    if (rec.editRecord.expectedErrorFields) {
                                        testName = `Attempt to save edited ${module.moduleCaption}, expect missing required field error`;
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

                                        if (rec.editRecord.expectedErrorFields) {
                                            expect(errorFields.length).toBeGreaterThan(0);
                                            expect(errorFields.length).toBe(rec.editRecord.expectedErrorFields.length);
                                            expect(successfulSave).toBe(false);
                                        }
                                        else {
                                            expect(saveError).toBe("");
                                            expect(errorFields.length).toBe(0);
                                            expect(successfulSave).toBe(true);
                                        }

                                    }, module.formSaveTimeout);


                                    testName = `Seek to the edited ${module.moduleCaption} record`;
                                    test(testName, async () => {
                                        let recordCount = await module.browseSeek(rec.editRecord.seekObject);//.then().catch(err => this.LogError(testName, err));
                                        expect(recordCount).toBe(1);
                                    }, module.browseSeekTimeout);

                                    if (rec.editRecord.recordToExpect) {
                                        testName = `Open the edited ${module.moduleCaption} record, compare values with expected`;
                                        test(testName, async () => {
                                            await module.openRecord()
                                                .then(openRecordResponse => {
                                                    for (let key in rec.editRecord.recordToExpect) {
                                                        let expectingValue = rec.editRecord.recordToExpect[key];
                                                        let foundValue = openRecordResponse.record[key];//.toUpperCase();

                                                        if (expectingValue.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                                                            expectingValue = TestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                        }

                                                        Logging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                        if (expectingValue === ModuleBase.NOTEMPTY) {
                                                            let expectedStr = `${key} value is ""`;
                                                            let actualStr = `${key} value is "${foundValue}"`;
                                                            expect(actualStr).not.toBe(expectedStr);
                                                        }
                                                        else {
                                                            let expectedStr = `${key} value is undefined`;
                                                            let actualStr = `${key} value is ${foundValue == undefined ? "undefined" : foundValue}`;
                                                            expect(actualStr).not.toBe(expectedStr);

                                                            expectedStr = `${key} value is ${expectingValue}`;
                                                            actualStr = `${key} value is ${foundValue}`;
                                                            expect(actualStr).toBe(expectedStr);
                                                        }
                                                    }
                                                });
                                        }, module.formOpenTimeout);
                                    }

                                    testName = `Close the ${module.moduleCaption} record`;
                                    test(testName, async () => {
                                        await module.closeRecord();  //close the form
                                    }, module.formOpenTimeout);

                                }

                                //if (module.canDelete) {
                                if ((module.canDelete) && (!rec.persistData)) {
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

                    if (module.canEdit) {
                        if (module.recordsToEdit) {
                            for (let rec of module.recordsToEdit) {
                                if (rec.seekObject) {
                                    testName = `Seek to the ${module.moduleCaption} record to edit`;
                                    test(testName, async () => {
                                        let recordCount = await module.browseSeek(rec.seekObject);//.then().catch(err => this.LogError(testName, err));
                                        expect(recordCount).toBe(1);
                                    }, module.browseSeekTimeout);
                                }

                                // open 
                                testName = `Open the ${module.moduleCaption} record for editing`;
                                test(testName, async () => {
                                    await module.openRecord();
                                }, module.formOpenTimeout);

                                //edit
                                testName = `Modify/Edit ${module.moduleCaption}`;
                                if (rec.expectedErrorFields) {
                                    testName += ` with missing required fields ${JSON.stringify(rec.expectedErrorFields)}`;
                                }
                                test(testName, async () => {
                                    await module.populateFormWithRecord(rec.record);
                                }, module.formOpenTimeout);

                                //save
                                testName = `Save edited ${module.moduleCaption}`;
                                if (rec.expectedErrorFields) {
                                    testName = `Attempt to save edited ${module.moduleCaption}, expect missing required field error`;
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
                                    if (rec.seekObject) {
                                        testName = `Seek to the edited ${module.moduleCaption} record`;
                                        test(testName, async () => {
                                            let recordCount = await module.browseSeek(rec.seekObject);//.then().catch(err => this.LogError(testName, err));
                                            expect(recordCount).toBe(1);
                                        }, module.browseSeekTimeout);
                                    }
                                }

                                testName = `Open the edited ${module.moduleCaption} record`;
                                if (rec.recordToExpect) {
                                    testName += `, compare values with expected`;
                                }

                                test(testName, async () => {
                                    await module.openRecord()
                                        .then(openRecordResponse => {
                                            if (rec.recordToExpect) {
                                                for (let key in rec.recordToExpect) {
                                                    let expectingValue = rec.recordToExpect[key];
                                                    let foundValue = openRecordResponse.record[key];//.toUpperCase();

                                                    if (expectingValue.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                                                        expectingValue = TestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                    }

                                                    Logging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                    if (expectingValue === ModuleBase.NOTEMPTY) {
                                                        let expectedStr = `${key} value is ""`;
                                                        let actualStr = `${key} value is "${foundValue}"`;
                                                        expect(actualStr).not.toBe(expectedStr);
                                                    }
                                                    else {
                                                        let expectedStr = `${key} value is undefined`;
                                                        let actualStr = `${key} value is ${foundValue == undefined ? "undefined" : foundValue}`;
                                                        expect(actualStr).not.toBe(expectedStr);

                                                        expectedStr = `${key} value is ${expectingValue}`;
                                                        actualStr = `${key} value is ${foundValue}`;
                                                        expect(actualStr).toBe(expectedStr);
                                                    }
                                                }
                                            }
                                        });
                                }, module.formOpenTimeout);

                                if (module.grids) {
                                    for (let grid of module.grids) {
                                        if (grid.canNew) {
                                            if (rec.gridRecords) {
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

                                                            //attempto edit the row
                                                            if (grid.canEdit) {

                                                                //  check to make sure at least one row exists, and that it can be put into Edit mode
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

                                                                if (gridRecord.recordToCreate.editRecord) {

                                                                    //  attempt to edit the row
                                                                    testName = `Apply edits to row in: ${grid.gridDisplayName}`;
                                                                    test(testName, async () => {
                                                                        let rowIndex: number = 1;

                                                                        if (gridRecord.recordToCreate.seekObject) {
                                                                            await grid.getRecordRowIndex(gridRecord.recordToCreate.seekObject)
                                                                                .then(i => {
                                                                                    rowIndex = i;
                                                                                });
                                                                            expect(rowIndex).toBeGreaterThan(0);
                                                                        }

                                                                        await grid.editGridRow(rowIndex, gridRecord.recordToCreate.editRecord.record, true)
                                                                            .then(editResponse => {
                                                                                expect(editResponse.errorMessage).toBe("");
                                                                                expect(editResponse.saved).toBeTruthy();
                                                                            });

                                                                    }, grid.editTimeout);

                                                                    // todo: find the row again and compare with expected
                                                                }
                                                            }

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


                                            // check each record 
                                            if (rec.gridRecords) {
                                                for (let gridRecord of rec.gridRecords) {
                                                    if (gridRecord.grid === grid) {
                                                        if ((gridRecord.recordToEdit) && (!gridRecord.recordToCreate)) {

                                                            //  attempt to edit the row
                                                            testName = `Apply edits to row in: ${grid.gridDisplayName}`;
                                                            test(testName, async () => {
                                                                let rowIndex: number = 1;

                                                                if (gridRecord.recordToEdit.seekObject) {
                                                                    await grid.getRecordRowIndex(gridRecord.recordToEdit.seekObject)
                                                                        .then(i => {
                                                                            rowIndex = i;
                                                                        });
                                                                    expect(rowIndex).toBeGreaterThan(0);
                                                                }

                                                                await grid.editGridRow(rowIndex, gridRecord.recordToEdit.record, true)
                                                                    .then(editResponse => {
                                                                        expect(editResponse.errorMessage).toBe("");
                                                                        expect(editResponse.saved).toBeTruthy();
                                                                    });

                                                            }, grid.editTimeout);

                                                            // todo: find the row again and compare with expected

                                                        }
                                                    }
                                                }
                                            }

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

                        }
                    }

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
}
