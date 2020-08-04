import { BaseTest } from '../shared/BaseTest';
import { ModuleBase } from '../shared/ModuleBase';
import { User } from './modules/AllModules';

export class ShallowRegressionBaseTest extends BaseTest {
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
                        await module.openRecord()
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
    async RelogAsCopyOfUser() {
        this.LoadMyUserGlobal(new User());
        this.CopyMyUserRegisterGlobal(new User());
        this.DoLogoff();
        this.DoLogin();
    }
    //---------------------------------------------------------------------------------------
}

