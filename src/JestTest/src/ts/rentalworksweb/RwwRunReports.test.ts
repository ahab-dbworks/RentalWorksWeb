import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { User } from './modules/AllModules';

export class RunReportsTest extends BaseTest {
    //---------------------------------------------------------------------------------------

    async MediumRegressionOnModule(module: ModuleBase) { }
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        //prerequisites

        //this.LoadMyUserGlobal(new User());


        describe('Run a specific Report', () => {
            //---------------------------------------------------------------------------------------
            let testName: string = 'Navigate to report section';
            test(testName, async () => {
                Logging.logInfo(`About to click on report icon`);
                const reportIcon = `div.systembar i[title="Reports"]`;
                await page.waitForSelector(reportIcon, { visible: true });
                await page.click(reportIcon);
            }, this.testTimeout);

            testName = 'Click on a particular report';
            test(testName, async () => {
                const reportName = 'ArAgingReport';
                Logging.logInfo(`About to click on ${reportName}`);
                const reportPanel = `#${reportName} .panel .panel-heading`;
                await page.waitForSelector(reportPanel, { visible: true });
                await page.click(reportPanel);
                Logging.logInfo(`About to click on report preview`);
                const reportMenu = `#${reportName} .fwform-menu .buttonbar`;
                await ModuleBase.wait(1000); // wait for prior report settings to be loaded
                await page.waitForSelector(reportMenu, { visible: true });
                Logging.logInfo(`About to preview report`);
                const previewBtn = `${reportMenu} .btn .btn-text.Preview`;
                await page.click(previewBtn);
                await ModuleBase.wait(5000); // wait for report to load
                const pages = await browser.pages();
                Logging.logInfo(`About to close ${reportName}`);
                await pages[2].close();


            }, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
}

new RunReportsTest().Run();
