import { FwModuleBase, FwDeleteResponse } from '../fwjest/FwModuleBase';
import { FwLogging } from '../fwjest/FwLogging';
import { FwTestUtils } from './FwTestUtils';
import { FwSaveResponse, FwOpenBrowseResponse, FwOpenRecordResponse, FwCreateNewResponse } from '../fwjest/FwModuleBase';

export class FwClickRecordResponse {
    clicked: boolean;
    recordId: string;
    recordsVisible: number;
}

//---------------------------------------------------------------------------------------
export class FwSettingsModule extends FwModuleBase {
    waitAfterClickingToOpenBrowseBeforeCheckingForErrors: number = 600;
    waitAfterClickingToOpenRecordBeforeCheckingForErrors: number = 400;
    waitAfterClickingToOpenRecordToCheckForErrors: number = 300;
    waitForButtonToGetEvents: number = 2500;
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
    }
    //---------------------------------------------------------------------------------------
    getBrowseSelector(): string {
        return `.panel-group[id="${this.moduleName}"]`;
    }
    //---------------------------------------------------------------------------------------
    getNewButtonSelector(): string {
        return `.panel-group[id="${this.moduleName}"] i.material-icons.new-row-menu`;
    }
    //---------------------------------------------------------------------------------------
    async openBrowse(): Promise<FwOpenBrowseResponse> {
        let openBrowseResponse: FwOpenBrowseResponse = new FwOpenBrowseResponse();
        openBrowseResponse.opened = false;
        openBrowseResponse.recordCount = 0;
        openBrowseResponse.errorMessage = "browse not opened";

        let settingsGearSelector = `i.material-icons.dashboard.systembarcontrol[title="Settings"]`;
        await page.waitForSelector(settingsGearSelector, { visible: true });
        await page.click(settingsGearSelector);

        let moduleHeadingSelector = `.panel-group[id="${this.moduleName}"]`;
        await page.waitForSelector(moduleHeadingSelector, { visible: true });
        await page.click(moduleHeadingSelector);

        // wait for the module to try to open, then check for errors
        var popUp;
        try {
            popUp = await page.waitForSelector('.advisory', { timeout: this.waitAfterClickingToOpenBrowseBeforeCheckingForErrors });
        } catch (error) { } // no error pop-up

        if (popUp !== undefined) {
            let errorMessage = await page.$eval('.advisory', el => el.textContent);
            openBrowseResponse.opened = false;
            openBrowseResponse.recordCount = 0;
            openBrowseResponse.errorMessage = errorMessage;

            FwLogging.logError(`Error opening ${this.moduleCaption} browse: ` + errorMessage);

            const options = await page.$$('.advisory .fwconfirmation-button');
            await options[0].click() // click "OK" option
                .then(() => {
                    FwLogging.logInfo(`Clicked the "OK" button.`);
                })
        }
        else {
            let moduleLegendBarSelector = `.panel-group[id="${this.moduleName}"] .legend`;
            await page.waitForSelector(moduleLegendBarSelector)
                .then(async done => {
                    FwLogging.logInfo(`Opened ${this.moduleCaption} module`);
                    //await ModuleBase.wait(200); // let the rows render, if any
                })

            //let rowCount: number | void = this.browseGetRowsDisplayed() as unknown as number;
            openBrowseResponse.opened = true;
            //openBrowseResponse.recordCount = rowCount;
            openBrowseResponse.errorMessage = "";


            if (this.waitAfterClickingToOpenBrowseToAllowOtherQueries > 0) {
                await FwModuleBase.wait(this.waitAfterClickingToOpenBrowseToAllowOtherQueries);
            }
        }
        return openBrowseResponse;
    }
    //---------------------------------------------------------------------------------------
    async browseGetRowsDisplayed(): Promise<number> {
        FwLogging.logInfo(`about to count rows in the ${this.moduleName} module`);
        await page.waitForSelector(this.getBrowseSelector(), { visible: true });

        let recordSelector = `.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record:not(.inactive-panel)`;
        const records = await page.$$(recordSelector);

        var recordCount;
        if (records == undefined) {
            recordCount = 0;
        }
        else {
            recordCount = 0;
            for (let record of records) {
                let styleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), record);
                if ((styleAttributeValue === undefined) || (styleAttributeValue == null)) {
                    styleAttributeValue = "";
                }
                if (!styleAttributeValue.replace(' ', '').includes("display:none")) {  // only count the record if it is displayed


                    let childSelector = `.panel-info .row-heading:not(.inactive-panel)`;
                    const childDiv = await record.$(childSelector);

                    let childStyleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), childDiv);
                    if ((childStyleAttributeValue === undefined) || (childStyleAttributeValue == null)) {
                        childStyleAttributeValue = "";
                    }
                    if (!childStyleAttributeValue.replace(' ', '').includes("display:none")) {

                        recordCount++;
                    }
                }
            }
        }

        FwLogging.logInfo(`Record Count: ${recordCount}`);
        return recordCount;
    }
    //---------------------------------------------------------------------------------------
    async browseSeek(seekObject: any): Promise<number> {
        await page.waitForSelector(this.getBrowseSelector(), { visible: true });

        let refreshButtonSelector = `.panel-group[id="${this.moduleName}"] .refresh`;
        await page.waitForSelector(refreshButtonSelector, { visible: true });
        await page.click(refreshButtonSelector);
        await FwModuleBase.wait(this.waitAfterClickingToOpenBrowseBeforeCheckingForErrors); // let the refresh occur, or at least start

        let searchFieldSelector = `.panel-group[id="${this.moduleName}"] input#recordSearch`;
        await page.waitForSelector(searchFieldSelector, { visible: true });

        let keyField = "";
        for (var key in seekObject) {
            keyField = key;  // For "Settings" modules, there is only one Search field on the page.  Here we just get the first key field name to search by
            break;
        }

        let seekValue = seekObject[keyField];
        if (seekValue.toString().toUpperCase().includes("GLOBALSCOPE.")) {
            seekValue = FwTestUtils.getGlobalScopeValue(seekValue, this.globalScopeRef);
        }

        FwLogging.logInfo(`About to add search for ${seekValue}`);

        let elementHandle = await page.$(searchFieldSelector);
        await elementHandle.click();
        await page.keyboard.sendCharacter(seekValue);
        await page.keyboard.press('Enter');
        await FwModuleBase.wait(this.waitAfterHittingEnterToSearch); // let the rows render  // #stresstest s/b 2000+

        let recordCount = await this.browseGetRowsDisplayed();

        return recordCount;

    }
    //---------------------------------------------------------------------------------------
    async clickRecord(index?: number): Promise<FwClickRecordResponse> {
        let clickRecordResponse: FwClickRecordResponse = new FwClickRecordResponse();
        clickRecordResponse.clicked = false;
        clickRecordResponse.recordId = "";
        clickRecordResponse.recordsVisible = 0;

        if (index == undefined) {
            index = 1;
        }

        let moduleLegendBarSelector = `.panel-group[id="${this.moduleName}"] .legend`;
        await page.waitForSelector(moduleLegendBarSelector);

        let recordSelector = `.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record:not(.inactive-panel)`;
        //let recordSelector = `.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record:not(.inactive-panel) .panel-info .row-heading:not(.inactive-panel)`;

        const records = await page.$$(recordSelector);

        if (records == undefined) {
            clickRecordResponse.recordsVisible = 0;
        }
        else {
            clickRecordResponse.recordsVisible = 0;
            let recordToClick: any;
            let rowcounter: number = 0;
            for (let record of records) {
                let styleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), record);
                if ((styleAttributeValue === undefined) || (styleAttributeValue == null)) {
                    styleAttributeValue = "";
                }
                if (!styleAttributeValue.replace(' ', '').includes("display:none")) {

                    let childSelector = `.panel-info .row-heading:not(.inactive-panel)`;
                    const childDiv = await record.$(childSelector);
                    if (childDiv != null) {
                        let recordId = await page.evaluate(el => el.getAttribute('id'), record);

                        let childStyleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), childDiv);
                        if ((childStyleAttributeValue === undefined) || (childStyleAttributeValue == null)) {
                            childStyleAttributeValue = "";
                        }
                        if (!childStyleAttributeValue.replace(' ', '').includes("display:none")) {

                            rowcounter++;
                            if (rowcounter === index) {
                                clickRecordResponse.recordId = recordId;
                                recordToClick = record;
                                //await record.click(); // click the row
                                //clickRecordResponse.clicked = true;
                            }
                            clickRecordResponse.recordsVisible++;
                        }
                    }
                }
            }
            if (recordToClick != null) {
                await recordToClick.click(); // click the row
                clickRecordResponse.clicked = true;
                //await ModuleBase.wait(1000); // let the form render or collapse  // #stresstest s/b 1500+
                await FwModuleBase.wait(this.waitAfterClickingToOpenRecordBeforeCheckingForErrors); // let the form render or collapse  // #stresstest s/b 1500+
            }

        }

        return clickRecordResponse;
    }
    //---------------------------------------------------------------------------------------
    async openRecord(index?: number): Promise<FwOpenRecordResponse> {
        let openRecordResponse: FwOpenRecordResponse = new FwOpenRecordResponse();
        openRecordResponse.opened = false;
        openRecordResponse.record = null;
        openRecordResponse.errorMessage = "form not opened";

        let formCountBefore = await this.countOpenForms();

        if (index == undefined) {
            index = 1;
        }


        let clickRecordResponse: FwClickRecordResponse = await this.clickRecord(index);

        if (clickRecordResponse.clicked) {
            //try {
            //    await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 3000 });
            //} catch (error) { } // assume that we missed the Please Wait dialog
            //
            //await page.waitFor(() => !document.querySelector('.pleasewait'));
            //Logging.logInfo(`Finished waiting for the Please Wait dialog.`);

            var popUp;
            try {
                popUp = await page.waitForSelector('.advisory', { timeout: this.waitAfterClickingToOpenRecordToCheckForErrors });
            } catch (error) { } // no error pop-up

            if (popUp !== undefined) {
                let errorMessage = await page.$eval('.advisory', el => el.textContent);
                openRecordResponse.opened = false;
                openRecordResponse.record = null;
                openRecordResponse.errorMessage = errorMessage;

                FwLogging.logError(`Error opening ${this.moduleCaption} form: ` + errorMessage);

                const options = await page.$$('.advisory .fwconfirmation-button');
                await options[0].click() // click "OK" option
                    .then(() => {
                        FwLogging.logInfo(`Clicked the "OK" button.`);
                    })
            }
            else {

                let formCountAfter = await this.countOpenForms();
                if (formCountAfter == formCountBefore + 1) {

                    openRecordResponse.opened = true;
                    openRecordResponse.errorMessage = "";
                    openRecordResponse.record = await this.getFormRecord();
                    openRecordResponse.keys = await this.getFormKeys();

                    FwLogging.logInfo(`Form Record: ${JSON.stringify(openRecordResponse.record)}`);
                    FwLogging.logInfo(`Form Keys: ${JSON.stringify(openRecordResponse.keys)}`);

                    if (this.waitAfterClickingToOpenFormToAllowOtherQueries > 0) {
                        await FwModuleBase.wait(this.waitAfterClickingToOpenFormToAllowOtherQueries);
                    }
                }
                else {
                    openRecordResponse.errorMessage = `${formCountAfter} forms opened`;
                }
            }
        }

        return openRecordResponse;

    }
    //---------------------------------------------------------------------------------------
    //async openFirstRecordIfAny(): Promise<OpenRecordResponse> {
    //
    //    let openRecordResponse: OpenRecordResponse = new OpenRecordResponse();
    //    openRecordResponse.opened = false;
    //    openRecordResponse.record = null;
    //    openRecordResponse.errorMessage = "form not opened";
    //
    //    let formCountBefore = await this.countOpenForms();
    //
    //    let clickRecordResponse: ClickRecordResponse = await this.clickRecord();
    //
    //    if (clickRecordResponse.recordsVisible === 0) {
    //        openRecordResponse.opened = true;
    //        openRecordResponse.record = null;
    //        openRecordResponse.errorMessage = "";
    //    }
    //    else {
    //
    //
    //        if (clickRecordResponse.clicked) {
    //            try {
    //                await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 1000 });
    //            } catch (error) { } // assume that we missed the Please Wait dialog
    //
    //            await page.waitFor(() => !document.querySelector('.pleasewait'));
    //            Logging.logInfo(`Finished waiting for the Please Wait dialog.`);
    //
    //            var popUp;
    //            try {
    //                popUp = await page.waitForSelector('.advisory', { timeout: 500 });
    //            } catch (error) { } // no error pop-up
    //
    //            if (popUp !== undefined) {
    //                let errorMessage = await page.$eval('.advisory', el => el.textContent);
    //                openRecordResponse.opened = false;
    //                openRecordResponse.record = null;
    //                openRecordResponse.errorMessage = errorMessage;
    //
    //                Logging.logError(`Error opening ${this.moduleCaption} form: ` + errorMessage);
    //
    //                const options = await page.$$('.advisory .fwconfirmation-button');
    //                await options[0].click() // click "OK" option
    //                    .then(() => {
    //                        Logging.logInfo(`Clicked the "OK" button.`);
    //                    })
    //            }
    //            else {
    //
    //                let formCountAfter = await this.countOpenForms();
    //                if (formCountAfter == formCountBefore + 1) {
    //
    //                    openRecordResponse.opened = true;
    //                    openRecordResponse.errorMessage = "";
    //                    openRecordResponse.record = await this.getFormRecord();
    //                    openRecordResponse.keys = await this.getFormKeys();
    //
    //                    Logging.logInfo(`Form Record: ${JSON.stringify(openRecordResponse.record)}`);
    //                    Logging.logInfo(`Form Keys: ${JSON.stringify(openRecordResponse.keys)}`);
    //
    //                    if (this.waitAfterClickingToOpenFormToAllowOtherQueries > 0) {
    //                        await ModuleBase.wait(this.waitAfterClickingToOpenFormToAllowOtherQueries);
    //                    }
    //                }
    //                else {
    //                    openRecordResponse.errorMessage = `${formCountAfter} forms opened`;
    //                }
    //            }
    //
    //        }
    //    }
    //
    //    return openRecordResponse;
    //}
    //---------------------------------------------------------------------------------------
    async createNewRecord(count?: number): Promise<FwCreateNewResponse> {
        let createNewResponse: FwCreateNewResponse = new FwCreateNewResponse()
        createNewResponse.success = false;
        createNewResponse.errorMessage = "could not create new";

        if (count === undefined) {
            count = 1;
        }

        let moduleLegendBarSelector = `.panel-group[id="${this.moduleName}"] .legend`;
        await page.waitForSelector(moduleLegendBarSelector);

        let openFormCountBefore = await this.countOpenForms();

        //let newButtonSelector = `.panel-group[id="${this.moduleName}"] i.material-icons.new-row-menu`;

        await page.waitForSelector(this.getNewButtonSelector(), { visible: true, timeout: 10000 });
        await FwModuleBase.wait(this.waitForButtonToGetEvents); // let the events get associated to the new button
        await page.click(this.getNewButtonSelector(), { clickCount: count });

        let formSelector = `.fwform`;
        await page.waitForSelector(formSelector, { visible: true, timeout: 10000 })
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

                    FwLogging.logInfo(`Form Record: ${JSON.stringify(createNewResponse.defaultRecord)}`);

                    FwLogging.logInfo(`New ${this.moduleCaption} Created`);
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
    async closeRecord(index?: number): Promise<void> {
        // need to check to see if the record is open first
        await this.clickRecord(index);
        FwLogging.logInfo(`Record closed.`);
    }
    //---------------------------------------------------------------------------------------
    async closeModifiedRecordWithoutSaving(): Promise<void> {
        //Logging.logInfo(`about to close form without saving`);

        let cancelSelector = `.panel-group[id="${this.moduleName}"] i.material-icons.cancel`;
        let cancelButtonFound: boolean = false;
        try {
            await page.waitForSelector(cancelSelector, { visible: true, timeout: 1000 });
            cancelButtonFound = true;
        } catch (error) { } // there is no Cancel button, so it must not be a NEW record

        if (cancelButtonFound) {
            FwLogging.logInfo(`new record "cancel" button found`);
            FwModuleBase.wait(this.waitForButtonToGetEvents);  // wait for the cancel button to get its events
            await page.click(cancelSelector);
            FwLogging.logInfo(`Record closed without saving.`);
        }
        else {
            FwLogging.logInfo(`new record "cancel" button NOT found`);
            this.closeRecord();
        }

        // temporary work-around to allow regression test to run while Settings "New" function is under construction

        //Logging.logInfo(`about to refresh module`);
        //let refreshButtonSelector = `.panel-group[id="${this.moduleName}"] .refresh`;
        //await page.waitForSelector(refreshButtonSelector, { visible: true });
        //await page.click(refreshButtonSelector);
        //await ModuleBase.wait(this.waitAfterClickingToOpenBrowseBeforeCheckingForErrors); // let the refresh occur, or at least start


    }
    //---------------------------------------------------------------------------------------
    async deleteRecord(index?: number, closeUnexpectedErrors: boolean = false): Promise<FwDeleteResponse> {
        let response: FwDeleteResponse = new FwDeleteResponse();
        response.deleted = false;
        response.errorMessage = "record not deleted";


        if (index == undefined) {
            index = 1;
        }

        let clickRecordResponse: FwClickRecordResponse = await this.clickRecord(index);
        if (clickRecordResponse.clicked) {

            let deleteButtonSelector = `div .panel-record[id="${clickRecordResponse.recordId}"] .btn-delete[data-type="DeleteMenuBarButton"]`;
            await page.waitForSelector(deleteButtonSelector, { visible: true });
            FwModuleBase.wait(this.waitForButtonToGetEvents); // wait for the button to get its events
            await page.click(deleteButtonSelector, { clickCount: 1 });  // click the delete button

            await page.waitFor(() => document.querySelector('.advisory'));
            const popupText = await page.$eval('.advisory', el => el.textContent);
            if (popupText.includes('Delete this record')) {
                FwLogging.logInfo(`Delete record, confirmation prompt detected.`);

                const options = await page.$$('.advisory .fwconfirmation-button');
                await options[0].click() // click "Yes" option
                    .then(() => {
                        FwLogging.logInfo(`Clicked the "Yes" button.`);
                    })
                await page.waitFor(() => !document.querySelector('.advisory'));

                //try {
                //    await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 3000 });
                //} catch (error) { } // assume that we missed the Please Wait dialog
                //
                //await page.waitFor(() => !document.querySelector('.pleasewait'));
                //Logging.logInfo(`Finished waiting for the Please Wait dialog.`);

                //let selector = `.advisory .messageclose`;
                //const elementHandle = await page.$(selector);
                //if (elementHandle != null) {
                //    await elementHandle.click();
                //    await page.waitFor(() => !document.querySelector('.advisory'));  // wait for "record deleted" toaster to go away
                //}
                //
                //

                let selector = `.advisory`;
                await page.waitForSelector(selector);

                const afterDeleteMsg = await page.$eval('.advisory', el => el.textContent);
                if ((afterDeleteMsg.includes('deleted')) && (!afterDeleteMsg.includes('Error'))) {
                    FwLogging.logInfo(`${this.moduleCaption} Record deleted: ${afterDeleteMsg}`);

                    //make the "record deleted" toaster message go away
                    await page.waitForSelector('.advisory .messageclose');
                    await page.click(`.advisory .messageclose`);
                    await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away

                    response.deleted = true;
                    response.errorMessage = "";
                } else if (afterDeleteMsg.includes('Error')) {
                    FwLogging.logInfo(`${this.moduleCaption} Record not deleted: ${afterDeleteMsg}`);
                    response.deleted = false;
                    response.errorMessage = afterDeleteMsg;

                    if (closeUnexpectedErrors) {
                        //check for any error message pop-ups and click them to make error messages go away
                        FwLogging.logInfo(`About to check for error prompt and close it`);
                        let selector = '.advisory .fwconfirmation-button';
                        const elementHandle = await page.$(selector);
                        if (elementHandle != null) {
                            //await page.waitForSelector('.advisory .fwconfirmation-button');
                            FwLogging.logInfo(`Found button on prompt, about to click`);
                            //await page.waitForSelector(selector);
                            //await page.click(selector);
                            await elementHandle.click();
                            await page.waitFor(() => !document.querySelector('.advisory'));
                        }
                    }
                }


            }
            FwLogging.logInfo(`Record deleted.`);

            //if (sleepAfterDeleting > 0) {
            //    await ModuleBase.wait(sleepAfterDeleting);
            //}
        }

        return response;
    }
    //---------------------------------------------------------------------------------------
}