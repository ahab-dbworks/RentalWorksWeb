require('dotenv').config()
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { ModuleBase } from '../shared/ModuleBase';
import { SaveResponse } from '../shared/ModuleBase';
import { GlobalScope } from '../shared/GlobalScope';

export abstract class BaseTest {

    continueTest: boolean | void = true;
    testTimeout: number = 45000; // 45 seconds
    testToken = TestUtils.getTestToken();

    globalScopeRef = GlobalScope;

    //---------------------------------------------------------------------------------------
    LogError(testName: string, err: any) {
        Logging.logger.error(testName, err);
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
                .catch(err => Logging.logger.error('Error in BeforeAll: ', err));
        });
    }
    //---------------------------------------------------------------------------------------
    async VerifyTestToken() {
        let testName: string = "";
        const testCollectionName = `Verify Test Token`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            testName = `Verify Test Token ${this.testToken}`;
            test(testName, async () => {
                expect(this.testToken).not.toBe("");
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
                this.continueTest = await TestUtils.authenticate()
                    .then((data) => { })
                    .catch(err => Logging.logger.error('Error in DoLogin: ', err));
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
                this.continueTest = await TestUtils.logoff()
                    .then((data) => { })
                    .catch(err => Logging.logger.error('Error in DoLogoff: ', err));
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async TestModuleOpenBrowse(module: ModuleBase) {
        let testName: string = "";
        const testCollectionName = `Open ${module.moduleName} browse`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            testName = `Open ${module.moduleName} browse`;
            test(testName, async () => {
                await module.openBrowse().then().catch(err => this.LogError(testName, err));
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------

    async LoadUserGlobal(userModule: ModuleBase) {
        var findUserInputs: any = {
            LoginName: process.env.RW_LOGIN
        }
        await this.TestModuleOpenSpecificRecord(userModule, findUserInputs, true, "ME");
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
                Logging.logger.info(`Valiating User Name on toolbar:\n     Expecting: "${expectedUserName}"\n     Found:     "${userName}"`);
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
                Logging.logger.info(`Valiating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
                expect(officeLocation).toBe(expectedOfficeLocation);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async TestModuleOpenBrowseOpenForm(module: ModuleBase, index?: number, registerGlobal?: boolean) {
        let testName: string = "";
        const testCollectionName = `Open ${module.moduleName} browse and form`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            testName = `Open ${module.moduleName} browse`;
            test(testName, async () => {
                await module.openBrowse().then().catch(err => this.LogError(testName, err));
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = `Open ${module.moduleName} form`;
            test(testName, async () => {
                let formCountBefore = await module.countOpenForms();
                await module.openRecord(index).then().catch(err => this.LogError(testName, err));
                let formCountAfter = await module.countOpenForms();
                expect(formCountAfter).toBe(formCountBefore + 1);
                if (registerGlobal) {
                    let formObject = await module.getFormRecord().then().catch(err => this.LogError(testName, err));
                    Logging.logger.info(`Form Record: ${JSON.stringify(formObject)}`);

                    let formKeys = await module.getFormKeys().then().catch(err => this.LogError(testName, err));
                    Logging.logger.info(`Form Keys: ${JSON.stringify(formKeys)}`);

                    let globalKey = module.moduleName;
                    for (var key in formKeys) {
                        globalKey = globalKey + "~" + formKeys[key];
                    }
                    Logging.logger.info(`Global Key: ${globalKey}`);
                    this.globalScopeRef[globalKey] = formObject;
                }
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async TestModuleDefaultsOnNewForm(module: ModuleBase, expectedObject: any) {
        let testName: string = "";
        const testCollectionName = `Start new ${module.moduleName}, check form for expected default values`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} browse`;
                test(testName, async () => {
                    await module.openBrowse().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} form in new mode`;
                test(testName, async () => {
                    await module.createNewRecord().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Validate default values on new ${module.moduleName} form, compare with expected values`;
                test(testName, async () => {
                    let defaultObject = await module.getFormRecord().then().catch(err => this.LogError(testName, err));
                    Logging.logger.info(`Form Record: ${JSON.stringify(defaultObject)}`);

                    for (let key in expectedObject) {
                        console.log(`Comparing: ${key}\n     Expecting: "${expectedObject[key]}"\n     Found:     "${defaultObject[key]}"`);

                        if (expectedObject[key].toString().startsWith("GlobalScope")) {
                            //example: "GlobalScope.DefaultSettings~1.DefaultUnit",
                            let globalScopeKey = expectedObject[key].toString().split('.');
                            expectedObject[key] = this.globalScopeRef[globalScopeKey[1].toString()][globalScopeKey[2].toString()];
                            console.log(`Comparing: ${key}\n     Expecting: "${expectedObject[key]}"\n     Found:     "${defaultObject[key]}"`);
                        }

                        if (expectedObject[key] === "|NOTEMPTY|") {
                            expect(defaultObject[key]).not.toBe("");
                        }
                        else {
                            expect(defaultObject[key]).not.toBeUndefined();
                            expect(defaultObject[key]).toBe(expectedObject[key]);
                        }
                    }
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Close new ${module.moduleName} form without saving`;
                test(testName, async () => {
                    await module.closeModifiedRecordWithoutSaving().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async TestModuleCreateNewRecord(module: ModuleBase, inputObject: any, expectedObject: any) {
        let testName: string = "";
        const testCollectionName = `Create new ${module.moduleName}, fill out form, save record`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} browse`;
                test(testName, async () => {
                    await module.openBrowse().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} form in new mode`;
                test(testName, async () => {
                    await module.createNewRecord().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Fill in ${module.moduleName} form data`;
                test(testName, async () => {
                    await module.populateFormWithRecord(inputObject).then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Save new ${module.moduleName}`;
                test(testName, async () => {
                    let saveResponse: SaveResponse | void = await module.saveRecord(true).then().catch(err => this.LogError(testName, err));
                    let strictSaveResponse = saveResponse as SaveResponse;
                    this.continueTest = strictSaveResponse.saved;
                    expect(strictSaveResponse.errorMessage).toBe("");
                    expect(strictSaveResponse.saved).toBe(true);
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Validate all values on saved ${module.moduleName}, compare with expected values`;
                test(testName, async () => {
                    let savedObject = await module.getFormRecord().then().catch(err => this.LogError(testName, err));
                    Logging.logger.info(`Form Record: ${JSON.stringify(savedObject)}`);
                    for (let key in expectedObject) {
                        console.log(`Comparing: ${key}\n     Expecting: "${expectedObject[key]}"\n     Found:     "${savedObject[key]}"`);

                        if (expectedObject[key].toString().startsWith("GlobalScope")) {
                            //example: "GlobalScope.DefaultSettings~1.DefaultUnit",
                            let globalScopeKey = expectedObject[key].toString().split('.');
                            expectedObject[key] = this.globalScopeRef[globalScopeKey[1].toString()][globalScopeKey[2].toString()];
                            console.log(`Comparing: ${key}\n     Expecting: "${expectedObject[key]}"\n     Found:     "${savedObject[key]}"`);
                        }

                        if (expectedObject[key] === "|NOTEMPTY|") {
                            expect(savedObject[key]).not.toBe("");
                        }
                        else {
                            expect(savedObject[key]).not.toBeUndefined();
                            expect(savedObject[key]).toBe(expectedObject[key]);
                        }
                    }
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Close ${module.moduleName} record`;
                test(testName, async () => {
                    await module.closeRecord().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async TestModulePreventDuplicate(module: ModuleBase, inputObject: any, duplicatedFieldsForTestName: string = "") {
        let testName: string = "";
        const testCollectionName = `Attempt to create a duplicate ${module.moduleName} ${duplicatedFieldsForTestName ? "using " + duplicatedFieldsForTestName : ""}`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} browse`;
                test(testName, async () => {
                    await module.openBrowse().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} form in new mode`;
                test(testName, async () => {
                    await module.createNewRecord().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Fill in ${module.moduleName} form data`;
                test(testName, async () => {
                    await module.populateFormWithRecord(inputObject).then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Attempt to save new ${module.moduleName}, expect Duplicate Error from system`;
                test(testName, async () => {
                    let saveResponse: SaveResponse | void = await module.saveRecord().then().catch(err => this.LogError(testName, err));
                    let strictSaveResponse = saveResponse as SaveResponse;
                    this.continueTest = !strictSaveResponse.saved;
                    expect(strictSaveResponse.saved).toBe(false);
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = 'Detect error message notification popup';
                test(testName, async () => {
                    await module.checkForDuplicatePrompt().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = 'Close duplicate notification popup';
                test(testName, async () => {
                    await module.closeDuplicatePrompt().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Close new ${module.moduleName} form without saving`;
                test(testName, async () => {
                    await module.closeModifiedRecordWithoutSaving().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async TestModuleForMissingRequiredField(module: ModuleBase, inputObject: any, missingRequiredFieldName: string = "") {
        let testName: string = "";
        const testCollectionName = `Attempt to create a ${module.moduleName} without a required ${missingRequiredFieldName}`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} browse`;
                test(testName, async () => {
                    await module.openBrowse().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} form in new mode`;
                test(testName, async () => {
                    await module.createNewRecord().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Fill in ${module.moduleName} form data`;
                test(testName, async () => {
                    await module.populateFormWithRecord(inputObject).then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Attempt to save new ${module.moduleName}, expect missing required field error from system`;
                test(testName, async () => {
                    let saveResponse: SaveResponse | void = await module.saveRecord(true).then().catch(err => this.LogError(testName, err));
                    let strictSaveResponse = saveResponse as SaveResponse;
                    this.continueTest = !strictSaveResponse.saved;
                    expect(strictSaveResponse.saved).toBe(false);
                    expect(strictSaveResponse.errorFields).toContain(missingRequiredFieldName);
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Close new ${module.moduleName} form without saving`;
                test(testName, async () => {
                    await module.closeModifiedRecordWithoutSaving().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async TestModuleOpenSpecificRecord(module: ModuleBase, seekObject: any, registerGlobal?: boolean, globalKeyValue?: string) {
        let testName: string = "";
        const testCollectionName = `Attempt to seek to and open a ${module.moduleName}`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} browse`;
                test(testName, async () => {
                    await module.openBrowse().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Use column headers to seek a specific ${module.moduleName} record`;
                test(testName, async () => {
                    let recordCount = await module.browseSeek(seekObject).then().catch(err => this.LogError(testName, err));
                    expect(recordCount).toBe(1);
                    this.continueTest = (recordCount == 1);
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open the ${module.moduleName} record`;
                test(testName, async () => {
                    let formCountBefore = await module.countOpenForms();
                    await module.openRecord();
                    let formCountAfter = await module.countOpenForms();
                    expect(formCountAfter).toBe(formCountBefore + 1);


                    if (registerGlobal) {
                        let formObject = await module.getFormRecord().then().catch(err => this.LogError(testName, err));
                        Logging.logger.info(`Form Record: ${JSON.stringify(formObject)}`);

                        let formKeys = await module.getFormKeys().then().catch(err => this.LogError(testName, err));
                        Logging.logger.info(`Form Keys: ${JSON.stringify(formKeys)}`);

                        let globalKey = module.moduleName;
                        if (globalKeyValue === undefined) {
                            for (var key in formKeys) {
                                globalKey = globalKey + "~" + formKeys[key];
                            }
                        }
                        else {
                            globalKey = globalKey + "~" + globalKeyValue;
                        }
                        Logging.logger.info(`Global Key: ${globalKey}`);
                        this.globalScopeRef[globalKey] = formObject;
                    }

                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async TestModuleDeleteSpecificRecord(module: ModuleBase, seekObject: any) {
        let testName: string = "";
        const testCollectionName = `Attempt to seek to and delete a ${module.moduleName}`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} browse`;
                test(testName, async () => {
                    await module.openBrowse().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Use column headers to seek a specific ${module.moduleName} record`;
                test(testName, async () => {
                    let recordCount = await module.browseSeek(seekObject).then().catch(err => this.LogError(testName, err));
                    expect(recordCount).toBe(1);
                    this.continueTest = (recordCount == 1);
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Delete the ${module.moduleName} record`;
                test(testName, async () => {
                    let rowCountBefore = await module.browseGetRowsDisplayed();
                    await module.deleteRecord();
                    let rowCountAfter = await module.browseGetRowsDisplayed();
                    expect(rowCountAfter).toBe(rowCountBefore - 1);
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
        });
    }
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
            this.DoLogin();
            this.ValidateEnvironment();
            this.PerformTests();
            this.DoLogoff();
        } catch (ex) {
            Logging.logger.error('Error in Run.', ex);
        }
    }
    //---------------------------------------------------------------------------------------
}
