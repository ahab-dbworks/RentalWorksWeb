import { BaseTest } from '../shared/BaseTest';
import { ModuleBase } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { User } from './modules/AllModules';
import { TestUtils } from '../shared/TestUtils';


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
        //this.testTimeout = 300000; // 5 minutes
        // ----------
        async function goToReportsPage() {
            Logging.logInfo(`About to click on report icon`);
            //const reportIcon = `div.systembar i[title="Reports"]`;   //needs new selector
            //await page.waitForSelector(reportIcon, { visible: true });
            //await page.click(reportIcon);  

            //await FwTestUtils.sleepAsync(2000);

            let mainMenuSelector = `.app-menu-button`;
            await TestUtils.waitForAndClick(mainMenuSelector, 0, 2000);

            let reportsIconSelector = `div.menu-lv1object i[title="Reports"]`;
            await TestUtils.waitForAndClick(reportsIconSelector, 0, 2000);

        }
        // ----------

        const reportNames = [
            'ArAgingReport',
            'DailyReceiptsReport',
            'GlDistributionReport',
            'AgentBillingReport',
            'BillingProgressReport',
            'BillingStatementReport',
            'CreateInvoiceProcessReport',
            'InvoiceDiscountReport',
            'InvoiceReport',
            'InvoiceSummaryReport',
            'ProfitLossReport',
            'ProjectManagerBillingReport',
            'SalesQuoteBillingReport',
            'SalesRepresentativeBillingReport',
            'SalesTaxCanadaReport',
            'SalesTaxUSAReport',
            'ChangeAuditReport',
            'DealInvoiceBatchReport',
            'ReceiptBatchReport',
            'VendorInvoiceBatchReport',
            'ContractRevisionReport',
            'ExchangeContractReport',
            'LostContractReport',
            'OutContractReport',
            'InContractReport',
            'ReceiveContractReport',
            'ReturnContractReport',
            'TransferManifestReport',
            'TransferReceiptReport',
            'CrewSignInReport',
            'CreditsOnAccountReport',
            'CustomerRevenueByMonthReport',
            'CustomerRevenueByTypeReport',
            'DealInvoiceDetailReport',
            'DealOutstandingItemsReport',
            'OrdersByDealReport',
            'ReturnReceiptReport',
            'TransferReport',
            'LateReturnsReport',
            'OrderConflictReport',
            'OrderReport',
            'PickListReport',
            'QuikActivityReport',
            'QuoteReport',
            'QuoteOrderMasterReport',
            'PartsInventoryAttributesReport',
            'PartsInventoryCatalogReport',
            'PartsInventoryPurchaseHistoryReport',
            'PartsInventoryReorderReport',
            'PartsInventoryTransactionReport',
            'RentalInventoryActivityByDateReport',
            'RentalInventoryAttributesReport',
            'RentalInventoryAvailabilityReport',
            'RentalInventoryCatalogReport',
            //'RentalInventoryChangeReport',  //justin hoffman 02/26/2020 this report requires the data warehouse. not yet installed in the testing environment
            'RentalInventoryMasterReport',
            'RentalInventoryMovementReport',
            'RentalInventoryPurchaseHistoryReport',
            'RentalInventoryQCRequiredReport',
            'RentalInventoryStatusAndRevenueReport',
            'RentalInventoryUnusedItemsReport',
            'RentalInventoryUsageReport',
            'RentalInventoryValueReport',
            'RentalLostAndDamagedBillingHistoryReport',
            'RetiredRentalInventoryReport',
            'ReturnedToInventoryReport',
            //'ReturnOnAssetReport',  //justin hoffman 02/26/2020 this report requires the data warehouse. not yet installed in the testing environment
            'UnretiredRentalInventoryReport',
            'ValueOfOutRentalInventoryReport',
            'RepairOrderStatusReport',
            'SalesBackorderReport',
            'SalesHistoryReport',
            'SalesInventoryAttributesReport',
            'SalesInventoryCatalogReport',
            'SalesInventoryMasterReport',
            'SalesInventoryPurchaseHistoryReport',
            'SalesInventoryReorderReport',
            'SalesInventoryTransactionReport',
            'PurchaseOrderMasterReport',
            'SubItemStatusReport',
            'SubRentalBillingAnalysisReport',
            'VendorInvoiceSummaryReport'
        ]
        // ----------
        //async function getReportNamesDynamic() {
        //    return await page.evaluate(() => {
        //        const reports = jQuery('.well .panel-group');
        //        const names: Array<string> = [];
        //        reports.each((i, el) => {
        //            names.push(jQuery(el).attr('id'));
        //        })
        //        return names;
        //    })
        //}
        // ----------

        //---------------------------------------------------------------------------------------
        async function populateValidationField(reportName: string, dataField: string, validationName: string, recordToSelect?: number) {
            if (recordToSelect === undefined) {
                recordToSelect = 1;
            }
            Logging.logInfo(`About to click validation button on ${dataField}`);
            await ModuleBase.wait(1000);
            await page.click(`#${reportName} .fwformfield[data-datafield="${dataField}"] i.btnvalidate`);
            var popUp;
            try {
                popUp = await page.waitForSelector('.advisory', { timeout: 750 });
            } catch (error) { } // no error pop-up

            if (popUp !== undefined) {
                let errorMessage = await page.$eval('.advisory', el => el.textContent);
                Logging.logError(`Error opening validation ${validationName}: ` + errorMessage);
                const options = await page.$$('.advisory .fwconfirmation-button');
                await options[0].click() // click "OK" option
                    .then(() => {
                        Logging.logInfo(`Clicked the "OK" button.`);
                    })
            }
            else {
                Logging.logInfo(`No errors found when clicking validation button`);
                Logging.logInfo(`About to wait for the validation pop-up`);
                await page.waitForSelector(`div[data-name="${validationName}"] tr.viewmode:nth-child(1)`);
                Logging.logInfo(`validation pop-up found,  about to click ${recordToSelect} record`);
                await page.click(`div[data-name="${validationName}"] tr.viewmode:nth-child(${recordToSelect})`, { clickCount: 2 });
                Logging.logInfo(`validation record clicked, about to wait 750 milliseconds`);
                await ModuleBase.wait(750);
            }
        }
        //---------------------------------------------------------------------------------------
        async function runReport(reportName: string) {
            let closeUnexpectedErrors = false;
            let testError = null;
            Logging.logInfo(`About to click on ${reportName}`);
            //const reportPanel = `#${reportName} .panel .panel-heading`;
            const reportPanel = `#${reportName} .panel-heading`;
            await page.waitForSelector(reportPanel, { visible: true });
            await page.click(reportPanel);

            //justin hoffman 02/25/2020 - if any validation fields have data-savesetting=false, then auto-select the first entry in the validation to allow report to run
            let noSaveFieldSelector = `#${reportName} [data-type="validation"][data-savesetting="false"][data-required="true"]`;
            const noSaveFields = await page.$$(noSaveFieldSelector);

            if (noSaveFields.length > 0) {
                for (let noSaveField of noSaveFields) {
                    let styleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), noSaveField);
                    if ((styleAttributeValue === undefined) || (styleAttributeValue == null)) {
                        styleAttributeValue = "";
                    }
                    if (!styleAttributeValue.replace(' ', '').includes("display:none")) {  // only try to input a value if the field is visible
                        let dataFieldName: string = await page.evaluate(el => el.getAttribute('data-datafield'), noSaveField);
                        let validationName: string = await page.evaluate(el => el.getAttribute('data-validationname'), noSaveField);
                        Logging.logInfo(`Found a no-save field: ${dataFieldName}   validation name: ${validationName}`);
                        await populateValidationField(reportName, dataFieldName, validationName, 1);
                    }
                }
            }

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
                testError = errorMessage;
                Logging.logInfo(`${reportName} Report not generated: ${errorMessage}`);
                Logging.logInfo(`Error Fields: ${JSON.stringify(errorFields)}`);
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
                Logging.logInfo(`NO error pop-up found previewing: ${reportName}.`);
                const pages = await browser.pages();
                await pages[2].setViewport({ width: 1600, height: 1080 })
                if (pages.length === 3) {
                    Logging.logInfo(`Waiting for ${reportName} to load`);

                    var preview;
                    var footer;
                    try {
                        preview = await pages[2].waitForSelector('.preview', { visible: true, timeout: 300000 });
                        footer = await pages[2].waitForSelector('#pageFooter', { visible: true, timeout: 300000 });
                        await ModuleBase.wait(1000); // only for developer to be able to see the report
                    } catch (error) { } // preview not found

                    if (preview !== undefined) {
                        Logging.logInfo(`${reportName} rendered`);
                    } else {
                        //const html = await pages[2].$eval('html', el => el.textContent);
                        let html: string = await pages[2].$eval('html', el => el.textContent);
                        let htmls = html.match(/.{1,100}/g); // split into an array of 100-character-length strings.  Required in order for the full text to appear in the test report pdf
                        testError = htmls;
                        Logging.logInfo(`${reportName} was not rendered`);
                        Logging.logInfo(`Error: ${html}`);
                    }
                    Logging.logInfo(`About to close ${reportName}`);
                    await pages[2].close();
                    //await ModuleBase.wait(1500); // wait to allow more time for the tab to close before starting the next report
                }
            }

            //close the repoort section 
            await page.waitForSelector(reportPanel, { visible: true });
            await page.click(reportPanel);

            expect(testError).toBeNull();
        }
        //---------------------------------------------------------------------------------------
        describe('Run all Reports', () => {
            // ----------
            let testName: string = 'Navigate to report section';
            test(testName, async () => {
                await goToReportsPage();
            }, this.testTimeout);
            // ----------
            // Iterate through all Reports
            for (let i = 0; i < reportNames.length; i++) {
                testName = `Run ${reportNames[i]}`;
                test(testName, async () => {
                    Logging.logInfo(`About to run ${reportNames[i]}`);
                    await runReport(reportNames[i]);
                }, this.testTimeout);
            }
        });
    }
    //---------------------------------------------------------------------------------------
}

describe('RunReportsTest', () => {
    try {
        new RunReportsTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
