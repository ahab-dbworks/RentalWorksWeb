require('dotenv').config()
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { ModuleBase } from '../shared/ModuleBase';
import { SaveResponse } from '../shared/ModuleBase';

export abstract class BaseTest {

    continueTest: boolean | void = true;
    testTimeout: number = 45000; // 45 seconds
    testToken = TestUtils.getTestToken();

    //---------------------------------------------------------------------------------------
    LogError(testName: string, err: any) {
        Logging.logger.error(testName, err);
    }
    //---------------------------------------------------------------------------------------
    CheckDependencies() {
        if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
        if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
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
    VerifyTestToken() {
        let testName: string = "";
        const testCollectionName = `Verify Test Token`;
        describe(testCollectionName, () => {
            testName = `Verify Test Token ${this.testToken}`;
            test(testName, async () => {
                expect(this.testToken).not.toBe("");
            }, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
    DoLogin() {
        let testName: string = "";
        const testCollectionName = `Login`;
        describe(testCollectionName, () => {
            testName = 'Login';
            test(testName, async () => {
                this.continueTest = await TestUtils.authenticate()
                    .then((data) => { })
                    .catch(err => Logging.logger.error('Error in DoLogin: ', err));
            }, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
    DoLogoff() {
        let testName: string = "";
        const testCollectionName = `Logoff`;
        describe(testCollectionName, () => {
            testName = 'Logoff';
            test(testName, async () => {
                this.continueTest = await TestUtils.logoff()
                    .then((data) => { })
                    .catch(err => Logging.logger.error('Error in DoLogoff: ', err));
            }, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
    TestModule(module: ModuleBase, inputObject: any, expectedObject: any) {
        let testName: string = "";
        const testCollectionName = `Create new ${module.moduleName}, fill out form, save record`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} Browse`;
                test(testName, async () => {
                    await module.openBrowse().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} Form in New mode`;
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
                    expect(strictSaveResponse.errorMessage).toBeUndefined();
                    expect(strictSaveResponse.saved).toBe(true);
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Validate saved ${module.moduleName}`;
                test(testName, async () => {
                    let savedObject = await module.getFormRecord().then().catch(err => this.LogError(testName, err));
                    for (let key in expectedObject) {
                        console.log(`Comparing : "${key}": `, `"${savedObject[key]}"`, `"${expectedObject[key]}"`);
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
    TestModuleForDuplicate(module: ModuleBase, inputObject: any, duplicatedFieldsForTestName: string = "") {
        let testName: string = "";
        const testCollectionName = `Attempt to create a duplicate ${module.moduleName} ${duplicatedFieldsForTestName ? " using " + duplicatedFieldsForTestName : ""}`;
        describe(testCollectionName, () => {
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} Browse`;
                test(testName, async () => {
                    await module.openBrowse().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
            if (this.continueTest) {
                testName = `Open ${module.moduleName} Form in New mode`;
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
                    //let saved: boolean | void = await module.saveRecord().then().catch(err => this.LogError(testName, err));
                    //expect(saved).toBe(false);
                    //this.continueTest = !saved;

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
                testName = 'Close Record';
                test(testName, async () => {
                    await module.closeModifiedRecordWithoutSaving().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
            }
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    // this method will be overridden in sub classes for each test collection we want to perform
    PerformTests() { }
    //---------------------------------------------------------------------------------------
    Run() {
        try {
            this.DoBeforeAll();
            this.VerifyTestToken();
            this.CheckDependencies();
            this.DoLogin();
            this.PerformTests();
            this.DoLogoff();
        } catch (ex) {
            Logging.logger.error('Error in Run.', ex);
        }
    }
    //---------------------------------------------------------------------------------------
}
