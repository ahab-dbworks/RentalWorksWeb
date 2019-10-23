require('dotenv').config()
import { Logging } from '../shared/Logging';
import { TestUtils, LoginResponse, LogoutResponse } from '../shared/TestUtils';
import { ModuleBase } from '../shared/ModuleBase';
import { SaveResponse, OpenBrowseResponse, CreateNewResponse, OpenRecordResponse, ClickAllTabsResponse } from '../shared/ModuleBase';
import { GlobalScope } from '../shared/GlobalScope';

export abstract class BaseTest {
    continueTest: boolean | void = true;
    testTimeout: number = 120000; // 120 seconds
    testToken = TestUtils.getTestToken();
    globalScopeRef = GlobalScope;
    //---------------------------------------------------------------------------------------
    LogError(testName: string, err: any) {
        Logging.logError("Error in " + testName + ": " + err);
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
                .catch(err => Logging.logError('Error in BeforeAll: ' + err));
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
                await TestUtils.authenticate().
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
                await TestUtils.logoff()
                    .then(logoutResponse => {
                        expect(logoutResponse.success).toBeTruthy();
                    });
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
    async OpenSpecificRecord(module: ModuleBase, seekObject?: any, registerGlobal?: boolean, globalKeyValue?: string, closeRecordWhenDone?: boolean) {
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
    async LoadMyUserGlobal(userModule: ModuleBase) {
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
    async CopyMyUserRegisterGlobal(userModule: ModuleBase) {
        var findUserInputs: any = {
            LoginName: "GlobalScope.TestToken~1.TestToken"
        }

        let testName: string = "";
        const testCollectionName = `Copy User to create a new Test User`;
        describe(testCollectionName, () => {
            testName = `Copy User to create a new Test User`;
            test(testName, async () => {
                let me: any = this.globalScopeRef["User~ME"];
                let newMe: any = {};
                newMe.FirstName = TestUtils.randomFirstName();
                newMe.LastName = TestUtils.randomLastName();
                //newMe.LoginName = "GlobalScope.TestToken~1.TestToken";
                newMe.LoginName = this.globalScopeRef["TestToken~1"].TestToken;
                let newPassword: string = TestUtils.randomAlphanumeric(20);
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
                await userModule.createNewRecord();
                await userModule.populateFormWithRecord(newMe);
                await userModule.saveRecord(true);

                // this is done to bypass the potential chrome prompt to save password
                //await ModuleBase.wait(2000);
                //let selector = `div.systembarcontrol[data-id="username"]`;
                //await page.waitForSelector(selector);
                //await page.click(selector);

                //Logging.logInfo(`about to try to find "home" logo`);
                ////click back to "home"
                //let homeSelector = `div .logo`;
                //await page.waitForSelector(homeSelector);
                //Logging.logInfo(`"home" logo found`);
                //await page.click(homeSelector);
                //Logging.logInfo(`clicked on "home" logo`);

                //await userModule.openBrowse();
                //Logging.logInfo(`user browse opened`);
                
				//await userModule.browseSeek(findUserInputs);
                //Logging.logInfo(`user browse seeked upon`);

                //await userModule.openRecord()
                //    .then(openRecordResponse => {
                //        openRecordResponse.record.Password = newPassword;
                //        this.globalScopeRef["User~ME"] = openRecordResponse.record;
                //    });
				
                this.globalScopeRef["User~ME"] = newMe;

                Logging.logInfo(`end of CopyMyUserRegisterGlobal`);

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
                Logging.logInfo(`Valiating User Name on toolbar:\n     Expecting: "${expectedUserName}"\n     Found:     "${userName}"`);
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
                Logging.logInfo(`Valiating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
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
            Logging.logError('Error in Run.' + ex);
        }
    }
    //---------------------------------------------------------------------------------------
}
