require('dotenv').config()
import { FwLogging } from './FwLogging';
import { FwTestUtils, FwLoginResponse, FwLogoutResponse } from './FwTestUtils';
import { FwModuleBase } from './FwModuleBase';
import { FwSaveResponse, FwOpenBrowseResponse, FwCreateNewResponse, FwOpenRecordResponse, FwClickAllTabsResponse } from './FwModuleBase';
import { FwGlobalScope } from './FwGlobalScope';

export abstract class FwBaseTest {
    continueTest: boolean | void = true;
    testTimeout: number = 120000; // 120 seconds
    testToken = FwTestUtils.getTestToken();
    globalScopeRef = FwGlobalScope;
    //---------------------------------------------------------------------------------------
    LogError(testName: string, err: any) {
        FwLogging.logError("Error in " + testName + ": " + err);
    }
    //---------------------------------------------------------------------------------------
    CheckDependencies() {
        if (process.env.RW_URL === undefined) throw 'Please add a line to the .env file such as RW_URL=\'http://localhost/rentalworksweb\'';
        if (process.env.RW_LOGIN === undefined) throw 'Please add a line to the .env file such as RW_LOGIN=\'TEST\'';
        if (process.env.RW_PASSWORD === undefined) throw 'Please add a line to the .env file such as RW_PASSWORD=\'TEST\'';
    }
    //---------------------------------------------------------------------------------------
    DoBeforeAll() {
        beforeAll(async () => {
            await page.setViewport({ width: 1600, height: 1080 })
                .then()
                .catch(err => FwLogging.logError('Error in BeforeAll: ' + err));
        });
    }
    //---------------------------------------------------------------------------------------
    async VerifyTestToken() {
        let testName: string = "";
        const testCollectionName = `Test Token`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            testName = `Test Token ${this.testToken}`;
            test(testName, async () => {
                expect(this.testToken).not.toBe("");
                //GlobalScope.TestToken~1.TestToken
                this.globalScopeRef["TestToken~1"] = {
                    TestToken: this.testToken,                                                     // 16 characters
                    MediumTestToken: this.testToken.substring(this.testToken.length - 8),          //  8 characters - not guaranteed to be unique
                    ShortTestToken: this.testToken.substring(this.testToken.length - 3),           //  3 characters - not guaranteed to be unique
                    TinyTestToken: this.testToken.substring(this.testToken.length - 2),            //  2 characters - not guaranteed to be unique
                    LastCharTestToken: this.testToken.substring(this.testToken.length - 1),        //  1 character  - not guaranteed to be unique
                };
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async DoLogin() {
        let testName: string = "";
        const testCollectionName = `Login`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            testName = 'Login';
            test(testName, async () => {
                await FwTestUtils.authenticate().
                    then(loginResponse => {
                        expect(loginResponse.errorMessage).toBe("");
                        expect(loginResponse.success).toBeTruthy();
                    });
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async DoLogoff() {
        let testName: string = "";
        const testCollectionName = `Logoff`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            testName = 'Logoff';
            test(testName, async () => {
                await FwTestUtils.logoff()
                    .then(logoutResponse => {
                        expect(logoutResponse.success).toBeTruthy();
                    });
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async OpenSpecificRecord(module: FwModuleBase, seekObject?: any, registerGlobal?: boolean, globalKeyValue?: string, closeRecordWhenDone?: boolean) {
        let testName: string = "";
        const testCollectionName = `Open a specific ${module.moduleCaption}`;// + registerGlobal ? `, register global values` : ``;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            testName = `Open ${module.moduleCaption} browse`;
            test(testName, async () => {
                await module.openBrowse()
                    .then((openBrowseResponse) => {
                        expect(openBrowseResponse.errorMessage).toBe("");
                        expect(openBrowseResponse.opened).toBeTruthy();
                    });
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            if (seekObject) {   // if seekObject supplied, use it.  Otherwise skip this and just open the first record in the browse
                testName = `Use column headers to seek a specific ${module.moduleCaption} record`;
                test(testName, async () => {
                    let recordCount = await module.browseSeek(seekObject).then().catch(err => this.LogError(testName, err));
                    expect(recordCount).toBe(1);
                    this.continueTest = (recordCount == 1);
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            testName = `Open the ${module.moduleCaption} record`;
            test(testName, async () => {
                await module.openRecord()
                    .then(openRecordResponse => {
                        expect(openRecordResponse.errorMessage).toBe("");
                        expect(openRecordResponse.opened).toBeTruthy();

                        if (registerGlobal) {
                            let globalKey = module.moduleName;
                            if (globalKeyValue === undefined) {
                                for (var key in openRecordResponse.keys) {
                                    globalKey = globalKey + "~" + openRecordResponse.keys[key];
                                }
                            }
                            else {
                                globalKey = globalKey + "~" + globalKeyValue;
                            }
                            this.globalScopeRef[globalKey] = openRecordResponse.record;
                        }
                    });
                if (closeRecordWhenDone) {
                    await module.closeRecord();
                }
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async LoadMyUserGlobal(userModule: FwModuleBase) {
        let testName: string = `Load my User account data into global scope`;
        const testCollectionName = `Load my User account data into global scope`;
        describe(testCollectionName, () => {
            test(testName, async () => {

                var findUserInputs: any = {
                    LoginName: process.env.RW_LOGIN
                }

                let myAccount: any = this.globalScopeRef["User~ME"];  // check GlobalScope for myAccount to use (can be used to re-log during the middle of a test)
                if (myAccount != undefined) {  // if myAccount was established, then use it
                    findUserInputs.LoginName = myAccount.LoginName;
                }

                //await this.OpenSpecificRecord(userModule, findUserInputs, true, "ME", true);
                await userModule.openBrowse();
                await userModule.browseSeek(findUserInputs);
                await userModule.openRecord()
                    .then(openRecordResponse => {
                        this.globalScopeRef["User~ME"] = openRecordResponse.record;
                        //    }
                    });
                await userModule.closeRecord();
            }, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
    async CopyMyUserRegisterGlobal(userModule: FwModuleBase) {
        //var findUserInputs: any = {
        //    LoginName: "GlobalScope.TestToken~1.TestToken"
        //}

        let testName: string = "";
        const testCollectionName = `Copy User to create a new Test User`;
        describe(testCollectionName, () => {
            testName = `Copy User to create a new Test User`;
            test(testName, async () => {
                let me: any = this.globalScopeRef["User~ME"];
                let newMe: any = {};
                newMe.FirstName = FwTestUtils.randomFirstName();
                newMe.LastName = FwTestUtils.randomLastName();
                newMe.LoginName = this.globalScopeRef["TestToken~1"].TestToken;
                let newPassword: string = FwTestUtils.randomAlphanumeric(20);
                newMe.Password = newPassword;
                newMe.GroupName = me.GroupName;
                newMe.UserTitle = me.UserTitle;
                newMe.Email = "GlobalScope.TestToken~1.MediumTestToken_" + me.Email;
                newMe.OfficeLocation = me.OfficeLocation;
                newMe.Warehouse = me.Warehouse;
                newMe.DefaultDepartmentType = me.DefaultDepartmentType;
                newMe.RentalDepartment = me.RentalDepartment;
                newMe.SalesDepartment = me.SalesDepartment;
                newMe.MiscDepartment = me.MiscDepartment;
                newMe.LaborDepartment = me.LaborDepartment;

                newMe.SuccessSound = me.SuccessSound;
                newMe.ErrorSound = me.ErrorSound;
                newMe.NotificationSound = me.NotificationSound;

                await userModule.createNewRecord();
                await userModule.populateFormWithRecord(newMe);
                await userModule.saveRecord(true);

                this.globalScopeRef["User~ME"] = newMe;

                FwLogging.logInfo(`end of CopyMyUserRegisterGlobal`);

            }, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
    async ValidateUserAndEnvironment() {
        let testName: string = "";
        const testCollectionName = `Validate User and Environment`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            testName = `Validate User Name`;
            test(testName, async () => {
                let selector = `div.systembarcontrol[data-id="username"]`;
                await page.waitForSelector(selector);
                const element = await page.$(selector);
                const userName = await page.evaluate(element => element.textContent, element);
                let expectedUserName = this.globalScopeRef["User~ME"]["FirstName"] + " " + this.globalScopeRef["User~ME"]["LastName"];
                FwLogging.logInfo(`Validating User Name on toolbar:\n     Expecting: "${expectedUserName}"\n     Found:     "${userName}"`);
                expect(userName).toBe(expectedUserName);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = `Validate Office Location`;
            test(testName, async () => {
                let selector = `div.systembarcontrol[data-id="officelocation"] .value`;
                await page.waitForSelector(selector);
                const element = await page.$(selector);
                const officeLocation = await page.evaluate(element => element.textContent, element);
                let expectedOfficeLocation = this.globalScopeRef["User~ME"]["OfficeLocation"];
                FwLogging.logInfo(`Validating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
                expect(officeLocation).toBe(expectedOfficeLocation);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    // this method can be overridden to implement copying your User as a new login for to run this test
    async RelogAsCopyOfUser() { }
    //---------------------------------------------------------------------------------------
    // this method can be overridden in sub classes for each test collection we want to perform
    async ValidateEnvironment() { }
    //---------------------------------------------------------------------------------------
    // this method will be overridden in sub classes for each test collection we want to perform
    async PerformTests() { }
    //---------------------------------------------------------------------------------------
    async Run() {
        try {
            this.DoBeforeAll();
            this.VerifyTestToken();
            this.CheckDependencies();
            this.DoLogin()
            this.RelogAsCopyOfUser();
            this.ValidateEnvironment();
            this.PerformTests();
            this.DoLogoff();
        } catch (ex) {
            FwLogging.logError('Error in Run.' + ex);
        }
    }
    //---------------------------------------------------------------------------------------
    async ShallowRegressionOnModule(module: FwModuleBase, registerGlobal?: boolean) {
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
                        await module.openRecord()
                            .then(openRecordResponse => {
                                expect(openRecordResponse.errorMessage).toBe("");
                                expect(openRecordResponse.opened).toBeTruthy();

                                if (registerGlobal) {
                                    let globalKey = module.moduleName;
                                    for (var key in openRecordResponse.keys) {
                                        globalKey = globalKey + "~" + openRecordResponse.keys[key];
                                    }
                                    //FwLogging.logInfo(`Global Key: ${globalKey}`);
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
    async MediumRegressionOnModule(module: FwModuleBase) {
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
                                                    expectingValue = FwTestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                }

                                                FwLogging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                if (expectingValue === FwModuleBase.NOTEMPTY) {
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
                                                        expectingValue = FwTestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                    }

                                                    FwLogging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                    if (expectingValue === FwModuleBase.NOTEMPTY) {
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
                                                            expectingValue = FwTestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                        }

                                                        FwLogging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                        if (expectingValue === FwModuleBase.NOTEMPTY) {
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
                                                        expectingValue = FwTestUtils.getGlobalScopeValue(expectingValue, this.globalScopeRef);
                                                    }

                                                    FwLogging.logInfo(`Comparing: ${key}\n     Expecting: "${expectingValue}"\n     Found:     "${foundValue}"`);
                                                    if (expectingValue === FwModuleBase.NOTEMPTY) {
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
}
