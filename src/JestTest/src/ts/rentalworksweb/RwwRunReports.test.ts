import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';

export class RunReportsTest extends BaseTest {
    //---------------------------------------------------------------------------------------

    async PerformTests() {
        // ----------
        async function goToReportsPage() {
            Logging.logInfo(`About to click on report icon`);
            const reportIcon = `div.systembar i[title="Reports"]`;
            await page.waitForSelector(reportIcon, { visible: true });
            await page.click(reportIcon);
        }
        // ----------
        async function runReport(reportName: string) {
            let closeUnexpectedErrors = false;
            Logging.logInfo(`About to click on ${reportName}`);
            const reportPanel = `#${reportName} .panel .panel-heading`;
            await page.waitForSelector(reportPanel, { visible: true });
            await page.click(reportPanel);

            Logging.logInfo(`About to click on report preview`);
            const reportMenu = `#${reportName} .fwform-menu .buttonbar`;
            await ModuleBase.wait(1000); // wait for prior report settings to be loaded
            await page.waitForSelector(reportMenu, { visible: true });

            Logging.logInfo(`About to preview report`);
            const previewBtn = `${reportMenu} .btn .btn-text.preview-btn`;
            await page.click(previewBtn);
            let popUp;
            try {
                popUp = await page.waitForSelector('.advisory', { timeout: 750 });
            } catch (error) { }  // no error pop-up

            if (popUp !== undefined) {
                Logging.logInfo(`error pop-up found previewing ${reportName}.`);
                closeUnexpectedErrors = true;
                let selector: string = ``;

                const errorFields = await page.$$eval(`div.field.error`, fields => fields.map((field) => field.getAttribute('data-datafield')));
                const errorMessage = await page.$eval('.advisory', el => el.textContent);
                Logging.logInfo(`${reportName} Report not generated: ${errorMessage}`);
                Logging.logInfo(`Error Fields: ${JSON.stringify(errorFields)}`);

                if (closeUnexpectedErrors) {
                    //check for the "record not saved" toaster message and make it go away
                    selector = `.advisory .messageclose`;
                    const elementHandle = await page.$(selector);
                    if (elementHandle != null) {
                        Logging.logInfo(`Found toaster button, about to click`);
                        await elementHandle.click();
                        await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
                    }
                }
            }
            else {
                Logging.logInfo(`no error pop-up found previewing: ${reportName}.`);
                const pages = await browser.pages();
                await console.log('PAGES: ', pages.length)
                if (pages.length === 3) {
                    Logging.logInfo(`Waiting for ${reportName} to load`);
                    await pages[2].waitForSelector('.preview', { visible: true });
                    await ModuleBase.wait(1000); // only for developer to be able to see the report
                    Logging.logInfo(`${reportName} rendered`);
                    Logging.logInfo(`About to close ${reportName}`);
                    await pages[2].close();
                }
            }
        }
        //---------------------------------------------------------------------------------------
        describe('Go to Reports page and run particular reports', () => {
            // ----------
            let testName: string = 'Navigate to report section';
            test(testName, async () => {
                await goToReportsPage();
            }, this.testTimeout);
            // ----------
            testName = 'Preview ArAgingReport';
            test(testName, async () => {
                await runReport('ArAgingReport');
            }, this.testTimeout);
            // ----------
            testName = 'Preview CreateInvoiceProcessReport';
            test(testName, async () => {
                await runReport('CreateInvoiceProcessReport');
            }, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
}

new RunReportsTest().Run();
