import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';

export class RunReportsTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        describe('Run a specific Report', () => {
            //---------------------------------------------------------------------------------------
            let testName: string = 'Navigate to report section';
            let closeUnexpectedErrors = false;
            test(testName, async () => {
                async function goToReportsPage() {
                    Logging.logInfo(`About to click on report icon`);
                    const reportIcon = `div.systembar i[title="Reports"]`;
                    await page.waitForSelector(reportIcon, { visible: true });
                    await page.click(reportIcon);
                }
                await goToReportsPage();
            }, this.testTimeout);

            testName = 'Click on a particular report';
            test(testName, async () => {

                async function runReport(reportName: string) {
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

                        const errorFields = await page.$$eval(`div.field.error`, fields => fields.map((field) => field.getAttribute('data-browsedatafield')));
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
                        await ModuleBase.wait(5000); // wait for report to load
                        //await page.waitForSelector('html .preview', { visible: true });
                        const pages = await browser.pages();
                        if (pages.length === 3) {
                            Logging.logInfo(`About to close ${reportName}`);
                            await pages[2].close();
                        }
                    }

                }
                await runReport('ArAgingReport');
                await runReport('CreateInvoiceProcessReport');

            }, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
}

new RunReportsTest().Run();
