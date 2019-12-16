import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { User } from './modules/AllModules';

export class RunReportsTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    async RelogAsCopyOfUser() {
        this.LoadMyUserGlobal(new User());
        this.CopyMyUserRegisterGlobal(new User());
        this.DoLogoff();
        this.DoLogin();  // uses new login account
    }
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

        const reportNames = ['ArAgingReport',
            //'DailyReceiptsReport',
            //'GlDistributionReport',
            //'AgentBillingReport',
            //'BillingProgressReport',
            //'BillingStatementReport',
            'CreateInvoiceProcessReport',
            //'InvoiceDiscountReport',
            //'InvoiceReport',
            //'InvoiceSummaryReport',
            //'ProfitLossReport',
            //'ProjectManagerBillingReport',
            //'SalesQuoteBillingReport',
            //'SalesRepresentativeBillingReport',
            //'SalesTaxCanadaReport',
            //'SalesTaxUSAReport',
            //'ChangeAuditReport',
            //'DealInvoiceBatchReport',
            //'ReceiptBatchReport',
            //'VendorInvoiceBatchReport',
            //'ContractRevisionReport',
            //'OutContractReport',
            //'CrewSignInReport',
            //'CreditsOnAccountReport',
            //'CustomerRevenueByMonthReport',
            //'CustomerRevenueByTypeReport',
            //'DealInvoiceDetailReport',
            //'DealOutstandingItemsReport',
            //'OrdersByDealReport',
            //'ReturnReceiptReport',
            //'TransferReport',
            //'LateReturnsReport',
            //'OrderConflictReport',
            //'OrderReport',
            //'PickListReport',
            //'QuikActivityReport',
            //'QuoteReport',
            //'QuoteOrderMasterReport',
            //'PartsInventoryAttributesReport',
            //'PartsInventoryCatalogReport',
            //'PartsInventoryPurchaseHistoryReport',
            //'PartsInventoryReorderReport',
            //'PartsInventoryTransactionReport',
            //'RentalInventoryActivityByDateReport',
            //'RentalInventoryAttributesReport',
            //'RentalInventoryAvailabilityReport',
            //'RentalInventoryCatalogReport',
            //'RentalInventoryChangeReport',
            //'RentalInventoryMasterReport',
            //'RentalInventoryMovementReport',
            //'RentalInventoryPurchaseHistoryReport',
            //'RentalInventoryQCRequiredReport',
            //'RentalInventoryStatusAndRevenueReport',
            //'RentalInventoryUnusedItemsReport',
            //'RentalInventoryUsageReport',
            //'RentalInventoryValueReport',
            //'RentalLostAndDamagedBillingHistoryReport',
            //'RetiredRentalInventoryReport',
            //'ReturnedToInventoryReport',
            //'ReturnOnAssetReport',
            //'UnretiredRentalInventoryReport',
            //'ValueOfOutRentalInventoryReport',
            //'RepairOrderStatusReport',
            //'SalesBackorderReport',
            //'SalesHistoryReport',
            //'SalesInventoryAttributesReport',
            //'SalesInventoryCatalogReport',
            //'SalesInventoryMasterReport',
            //'SalesInventoryPurchaseHistoryReport',
            //'SalesInventoryReorderReport',
            //'SalesInventoryTransactionReport',
            //'PurchaseOrderMasterReport',
            //'SubItemStatusReport',
            //'SubRentalBillingAnalysisReport',
            'VendorInvoiceSummaryReport']
        // ----------
        async function getReportNames() {
            return await page.evaluate(() => {
                const reports = jQuery('.well .panel-group');
                const names: Array<string> = [];
                reports.each((i, el) => {
                    names.push(jQuery(el).attr('id'));
                })
                return names;
            })
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

                const errorFields = await page.$$eval(`div[data-control="FwFormField"].error`, fields => fields.map((field) => field.getAttribute('data-datafield')));
                const errorMessage = await page.$eval('.advisory', el => el.textContent);
                Logging.logInfo(`${reportName} Report not generated: ${errorMessage}`);
                Logging.logInfo(`Error Fields: ${JSON.stringify(errorFields)}`);
                expect(errorFields.length).toBe(0);
                errorFields.length = 0;

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
                await pages[2].setViewport({ width: 1600, height: 1080 })
                if (pages.length === 3) {
                    Logging.logInfo(`Waiting for ${reportName} to load`);
                    await pages[2].waitForSelector('body', { visible: true, timeout: 5000 });
                    const preview = await pages[2].$('.preview');
                    await ModuleBase.wait(5000); // only for developer to be able to see the report
                    if (preview) {
                        Logging.logInfo(`${reportName} rendered`);
                    } else {
                        const html = await pages[2].$('html');
                        Logging.logInfo(`${reportName} was not rendered`);
                        Logging.logInfo(`Error: ${html}`);
                    }
                    expect(preview).toBeTruthy();
                    Logging.logInfo(`About to close ${reportName}`);
                    await pages[2].close();
                }
            }
        }
        //---------------------------------------------------------------------------------------
        describe('Go to Reports page and run reports', () => {
            // ----------
            let testName: string = 'Navigate to report section';
            test(testName, async () => {
                await goToReportsPage();
            }, this.testTimeout);
            // ----------
            //testName = 'Get ALL Reports';
            //test(testName, async () => {
            //    Logging.logInfo(`About to get Report Names`);
            //    //reportNames = await getReportNames();
            //    //await console.log('reportNames: ', reportNames);
            //}, this.testTimeout);


            for (let i = 0; i < reportNames.length; i++) {
                testName = `Running ${reportNames[i]}`;
                test(testName, async () => {
                    Logging.logInfo(`About to run ${reportNames[i]}`);
                    await runReport(reportNames[i]);
                }, this.testTimeout);
            }

            // ----------
            //testName = 'Preview ArAgingReport';
            //test(testName, async () => {
            //    await runReport('ArAgingReport');
            //}, this.testTimeout);
            //// ----------
            //testName = 'Preview CreateInvoiceProcessReport';
            //test(testName, async () => {
            //    await runReport('CreateInvoiceProcessReport');
            //}, this.testTimeout);
        });
    }
    //---------------------------------------------------------------------------------------
}

new RunReportsTest().Run();
