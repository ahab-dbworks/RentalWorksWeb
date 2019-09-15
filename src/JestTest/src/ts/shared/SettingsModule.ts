import { ModuleBase } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from './TestUtils';
import { SaveResponse, OpenBrowseResponse, OpenRecordResponse, CreateNewResponse } from '../shared/ModuleBase';

//---------------------------------------------------------------------------------------
export class SettingsModule extends ModuleBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
    }
    //---------------------------------------------------------------------------------------
    getBrowseSelector(): string {
        return `.panel-group[id="${this.moduleName}"] .legend`;
    }
    //---------------------------------------------------------------------------------------
    getNewButtonSelector(): string {
        return `.panel-group[id="${this.moduleName}"] i.material-icons.new-row-menu`;
    }
    //---------------------------------------------------------------------------------------
    async openBrowse(timeout?: number, sleepafteropening?: number): Promise<OpenBrowseResponse> {
        let openBrowseResponse: OpenBrowseResponse = new OpenBrowseResponse();
        openBrowseResponse.opened = false;
        openBrowseResponse.recordCount = 0;
        openBrowseResponse.errorMessage = "browse not opened";
        if (!timeout) {
            timeout = 3000;  //if we can't find the settings module header bar on the Settings Page within 3 seconds, then timeout the test
        }

        let settingsGearSelector = `i.material-icons.dashboard.systembarcontrol[title="Settings"]`;
        await page.waitForSelector(settingsGearSelector);
        await page.click(settingsGearSelector);

        let moduleHeadingSelector = `.panel-group[id="${this.moduleName}"]`;
        await page.waitForSelector(moduleHeadingSelector, { timeout: timeout });
        await page.click(moduleHeadingSelector);

        // wait for the module to try to open, then check for errors
        var popUp;
        try {
            popUp = await page.waitForSelector('.advisory', { timeout: 500 });
        } catch (error) { } // no error pop-up

        if (popUp !== undefined) {
            let errorMessage = await page.$eval('.advisory', el => el.textContent);
            openBrowseResponse.opened = false;
            openBrowseResponse.recordCount = 0;
            openBrowseResponse.errorMessage = errorMessage;

            Logging.logError(`Error opening ${this.moduleCaption} browse: ` + errorMessage);

            const options = await page.$$('.advisory .fwconfirmation-button');
            await options[0].click() // click "OK" option
                .then(() => {
                    Logging.logInfo(`Clicked the "OK" button.`);
                })
        }
        else {
            let moduleLegendBarSelector = `.panel-group[id="${this.moduleName}"] .legend`;
            await page.waitForSelector(moduleLegendBarSelector)
                .then(async done => {
                    Logging.logInfo(`Opened ${this.moduleCaption} module`);
                    await ModuleBase.wait(200); // let the rows render, if any
                })

            //let rowCount: number | void = this.browseGetRowsDisplayed() as unknown as number;
            openBrowseResponse.opened = true;
            //openBrowseResponse.recordCount = rowCount;
            openBrowseResponse.errorMessage = "";


            if (sleepafteropening > 0) {
                await TestUtils.sleepAsync(sleepafteropening);  // wait x seconds to allow other queries to complete
            }
        }
        return openBrowseResponse;
    }
    //---------------------------------------------------------------------------------------
    async openRecord(index?: number, sleepAfterOpening?: number): Promise<OpenRecordResponse> {
        let openRecordResponse: OpenRecordResponse = new OpenRecordResponse();
        openRecordResponse.opened = false;
        openRecordResponse.record = null;
        openRecordResponse.errorMessage = "form not opened";

        let formCountBefore = await this.countOpenForms();

        if (index == undefined) {
            index = 1;
        }

        //<tr tabindex="0" class="viewmode selected" > <td class="column" data - visible="false" style = "display:none;" > <div class="field" data - isuniqueid="true" data - browsedatatype="key" data - sort="off" data - formreadonly="true" data - browsedatafield="DealId" data - formdatafield="DealId" data - cssclass="DealId" data - originalvalue="F003RPX5" > F003RPX5 < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Deal" data - browsedatatype="text" data - sort="asc" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="Deal" data - formdatafield="Deal" data - cssclass="Deal" data - originalvalue="02 CREATIVE SOLUTONS" > 02 CREATIVE SOLUTONS < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Deal Number" data - browsedatatype="text" data - sort="off" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="DealNumber" data - formdatafield="DealNumber" data - cssclass="DealNumber" data - originalvalue="L12309" > L12309 < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Deal Type" data - browsedatatype="text" data - sort="off" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="DealType" data - formdatafield="DealType" data - cssclass="DealType" data - originalvalue="INDUSTRIAL/PROD" > INDUSTRIAL / PROD < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Deal Status" data - browsedatatype="text" data - sort="off" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="DealStatus" data - formdatafield="DealStatus" data - cssclass="DealStatus" data - originalvalue="OPEN" > OPEN < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Customer" data - browsedatatype="text" data - sort="off" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="Customer" data - formdatafield="Customer" data - cssclass="Customer" data - originalvalue="02 CREATIVE SOLUTONS" > 02 CREATIVE SOLUTONS < /div></td > <td class="column" data - visible="true" style = "" > </td></tr >
        //await page.waitForSelector(`.fwbrowse tbody tr.viewmode:nth-child(${index})`);

        //<div class="panel-record" id = "C0016A6Z" > <div class="panel panel-info container-fluid" > <div class="row-heading" > <i class="material-icons record-selector" > keyboard_arrow_down < /i>      <div style="width:100%;padding-left: inherit;">        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">          <label style="font-weight:800;">Ship Via</label > </div>    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">    <label data-datafield="ShipVia" style="color:#31708f">1ST PRIORITY OVN</label > </div>      </div > <div style="width:100%;padding-left: inherit;" > <div class="fwcontrol fwcontainer fwform-fieldrow" data - type="fieldrow" > <label style="font-weight:800;" > Carrier < /label>        </div > <div class="fwcontrol fwcontainer fwform-fieldrow" data - type="fieldrow" > <label data - datafield="Vendor" style = "color:#31708f" > FEDERAL EXPRESS < /label>        </div > </div>    </div > </div>  <div class="panel-body data-panel" style="display:none;" id="C0016A6Z" data-type="settings-row"></div > </div>
        //await page.waitForSelector(`.panel-group[id="${this.moduleName}"].panel-primary.panel-collapse.panel-body.panel-record:nth-child(${index})`);

        //let records = await page.$$eval(`.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record`, (e: any) => { return e.children; });
        //let records = await page.$$eval(`.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body`, (e: any) => { return e.children; });
        //Logging.logInfo(`records: ${JSON.stringify(records)}`);
        //let elementHandle = records[0];
        //await page.click(elementHandle, { clickCount: 1 });

        //let selector = `.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record`;
        let selector = `.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record .row-heading:not(.inactive-panel)`;

        let records = await page.$$eval(selector, (e: any) => { return e; });
        var recordCount;
        if (records == undefined) {
            recordCount = 0;
        }
        else {
            recordCount = records.length;
        }

        if (recordCount == 0) {
            openRecordResponse.opened = true;
            openRecordResponse.record = null;
            openRecordResponse.errorMessage = "";
        }
        else {
            await page.waitForSelector(selector);
            Logging.logInfo(`About to click the first row.`);
            await page.click(selector);

            try {
                await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 3000 });
            } catch (error) { } // assume that we missed the Please Wait dialog

            await page.waitFor(() => !document.querySelector('.pleasewait'));
            Logging.logInfo(`Finished waiting for the Please Wait dialog.`);


            var popUp;
            try {
                popUp = await page.waitForSelector('.advisory', { timeout: 500 });
            } catch (error) { } // no error pop-up

            if (popUp !== undefined) {
                let errorMessage = await page.$eval('.advisory', el => el.textContent);
                openRecordResponse.opened = false;
                openRecordResponse.record = null;
                openRecordResponse.errorMessage = errorMessage;

                Logging.logError(`Error opening ${this.moduleCaption} form: ` + errorMessage);

                const options = await page.$$('.advisory .fwconfirmation-button');
                await options[0].click() // click "OK" option
                    .then(() => {
                        Logging.logInfo(`Clicked the "OK" button.`);
                    })
            }
            else {

                let formCountAfter = await this.countOpenForms();
                if (formCountAfter == formCountBefore + 1) {

                    openRecordResponse.opened = true;
                    openRecordResponse.errorMessage = "";
                    openRecordResponse.record = await this.getFormRecord();
                    openRecordResponse.keys = await this.getFormKeys();

                    if (sleepAfterOpening > 0) {
                        await ModuleBase.wait(sleepAfterOpening);
                    }
                }
            }
        }

        return openRecordResponse;

    }
    //---------------------------------------------------------------------------------------
    async openFirstRecordIfAny(sleepAfterOpening?: number): Promise<OpenRecordResponse> {

        let openRecordResponse: OpenRecordResponse = new OpenRecordResponse();
        openRecordResponse.opened = false;
        openRecordResponse.record = null;
        openRecordResponse.errorMessage = "form not opened";

        let formCountBefore = await this.countOpenForms();

        //let selector = `.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record .row-heading`;
        let selector = `.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record .row-heading:not(.inactive-panel)`;
        var records;
        var recordCount;
        try {
            Logging.logInfo(`About to check for records in the ${this.moduleName} module`);
            records = await page.$$eval(selector, (e: any) => { return e; });
        } catch (error) { } // no records found

        if (records == undefined) {
            recordCount = 0;
        }
        else {
            recordCount = records.length;
        }
        Logging.logInfo(`Record Count: ${recordCount}`);

        if (recordCount == 0) {
            openRecordResponse.opened = true;
            openRecordResponse.record = null;
            openRecordResponse.errorMessage = "";
        }
        else {
            //selector += `:nth-child(1)`;
            //await page.waitForSelector(`.fwbrowse tbody tr.viewmode:nth-child(1)`);
            await page.waitForSelector(selector);
            Logging.logInfo(`About to click the first row.`);
            await page.click(selector);

            try {
                await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 3000 });
            } catch (error) { } // assume that we missed the Please Wait dialog

            await page.waitFor(() => !document.querySelector('.pleasewait'));
            Logging.logInfo(`Finished waiting for the Please Wait dialog.`);


            var popUp;
            try {
                popUp = await page.waitForSelector('.advisory', { timeout: 500 });
            } catch (error) { } // no error pop-up

            if (popUp !== undefined) {
                let errorMessage = await page.$eval('.advisory', el => el.textContent);
                openRecordResponse.opened = false;
                openRecordResponse.record = null;
                openRecordResponse.errorMessage = errorMessage;

                Logging.logError(`Error opening ${this.moduleCaption} form: ` + errorMessage);

                const options = await page.$$('.advisory .fwconfirmation-button');
                await options[0].click() // click "OK" option
                    .then(() => {
                        Logging.logInfo(`Clicked the "OK" button.`);
                    })
            }
            else {

                let formCountAfter = await this.countOpenForms();
                if (formCountAfter == formCountBefore + 1) {

                    openRecordResponse.opened = true;
                    openRecordResponse.errorMessage = "";
                    openRecordResponse.record = await this.getFormRecord();
                    openRecordResponse.keys = await this.getFormKeys();

                    if (sleepAfterOpening > 0) {
                        await ModuleBase.wait(sleepAfterOpening);
                    }
                }
            }
        }
        return openRecordResponse;
    }
    //---------------------------------------------------------------------------------------
    async createNewRecord(count?: number): Promise<CreateNewResponse> {
        let createNewResponse: CreateNewResponse = new CreateNewResponse()
        createNewResponse.success = false;
        createNewResponse.errorMessage = "could not create new";

        if (count === undefined) {
            count = 1;
        }

        let moduleLegendBarSelector = `.panel-group[id="${this.moduleName}"] .legend`;
        await page.waitForSelector(moduleLegendBarSelector, { timeout: 1000 });

        let openFormCountBefore = await this.countOpenForms();

        //let newButtonSelector = `.panel-group[id="${this.moduleName}"] i.material-icons.new-row-menu`;

        await page.waitForSelector(this.getNewButtonSelector(), { timeout: 1000 });
        await page.click(this.getNewButtonSelector(), { clickCount: count });

        let formSelector = `.fwform`;
        await page.waitForSelector(formSelector, { timeout: 5000 })
            .then(async done => {
                let openFormCountAfter = await this.countOpenForms();

                if (openFormCountAfter === (openFormCountBefore + count)) {

                    const formCaption = await page.evaluate(() => {
                        const caption = jQuery('body').find('div.tab.active').attr('data-caption');
                        return caption;
                    })
                    //if (formCaption === `New ${this.moduleCaption}`) {
                    createNewResponse.success = true;
                    createNewResponse.errorMessage = "";
                    createNewResponse.defaultRecord = await this.getFormRecord();
                    Logging.logInfo(`New ${this.moduleCaption} Created`);
                    //} else {
                    //    createNewResponse.errorMessage = `New ${this.moduleCaption} not Created`;
                    //    Logging.logError(createNewResponse.errorMessage);
                    //}
                }
                else {
                    createNewResponse.errorMessage = `Incorrect number of New ${this.moduleCaption} forms created. (${openFormCountAfter}, but expected ${openFormCountBefore + count})`;
                }
            });

        return createNewResponse;
    }
    //---------------------------------------------------------------------------------------
}