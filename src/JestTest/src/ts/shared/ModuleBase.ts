import { Logging } from '../shared/Logging';
import { TestUtils } from './TestUtils';
import { GlobalScope } from '../shared/GlobalScope';

export class OpenBrowseResponse {
    opened: boolean;
    recordCount: number;
    errorMessage: string;
}

export class OpenRecordResponse {
    opened: boolean;
    keys: any;
    record: any;
    errorMessage: string;
}

export class CreateNewResponse {
    success: boolean;
    defaultRecord: any;
    errorMessage: string;
}

export class ClickAllTabsResponse {
    success: boolean;
    tabCount: number;
    errorMessage: string;
}

export class SaveResponse {
    saved: boolean;
    errorMessage: string;
    errorFields: any;
}

export class NewRecordToCreate {
    record: any;
    seekObject?: any;
    recordToExpect?: any;
}

//---------------------------------------------------------------------------------------
export class ModuleBase {
    moduleName: string;
    moduleId: string;
    moduleCaption: string;
    browseOpenTimeout: number = 30000; // 30 seconds
    deleteTimeout: number = 30000; // 30 seconds
    formOpenTimeout: number = 30000; // 30 seconds
    formSaveTimeout: number = 30000; // 30 seconds

    canNew: boolean = true;
    canView: boolean = true;
    canEdit: boolean = true;
    canDelete: boolean = true;

    defaultNewRecordToExpect: any;
    newRecordsToCreate: NewRecordToCreate[];

    globalScopeRef = GlobalScope;

    //---------------------------------------------------------------------------------------
    constructor() {
        this.moduleName = 'UnknownModule';
        this.moduleId = '99999999-9999-9999-9999-999999999999';
        this.moduleCaption = 'UnknownModule';
    }
    //---------------------------------------------------------------------------------------
    getBrowseSelector(): string {
        return `.fwbrowse tbody`;
    }
    //---------------------------------------------------------------------------------------
    getNewButtonSelector(): string {
        return `.addnewtab i.material-icons`;
    }
    //---------------------------------------------------------------------------------------
    getDeleteButtonSelector(): string {
        return `.btn[data-type="DeleteMenuBarButton"]`;
    }
    //---------------------------------------------------------------------------------------
    static async wait(milliseconds: number): Promise<void> {
        await page.waitFor(milliseconds)
    }
    //---------------------------------------------------------------------------------------
    async openBrowse(timeout?: number, sleepafteropening?: number): Promise<OpenBrowseResponse> {
        let openBrowseResponse: OpenBrowseResponse = new OpenBrowseResponse();
        openBrowseResponse.opened = false;
        openBrowseResponse.recordCount = 0;
        openBrowseResponse.errorMessage = "browse not opened";
        if (!timeout) {
            timeout = 2000;  //if we can't find the button on the main menu within 2 seconds, then timeout the test
        }
        let mainMenuSelector = `.appmenu`;
        await page.waitForSelector(mainMenuSelector, { timeout: timeout });
        await page.click(mainMenuSelector);
        let menuButtonId = '#btnModule' + this.moduleId;
        await expect(page).toClick(menuButtonId);
        //await ModuleBase.wait(300); // wait for the previously-open module to go away.  may need a way to go back to a blank/home screen before attempting to get to this browse

        // wait for the data to come in
        await page.waitFor(() => document.querySelector('.pleasewait'));
        await page.waitFor(() => !document.querySelector('.pleasewait'));

        // find the browse tab
        let browseTabSelector = `div.tab.active[data-tabtype="BROWSE"]`;
        await page.waitForSelector(browseTabSelector);

        // make sure that we are getting the browse we want
        let browseTabCaptionSelector = browseTabSelector + ` div.caption`;
        const browserTabCaption = await page.$eval(browseTabCaptionSelector, el => el.textContent);
        while (browserTabCaption !== this.moduleCaption) {
            await ModuleBase.wait(100);
        }

        // find the browse column headers
        Logging.logInfo(`Opened ${this.moduleCaption} module`);
        await page.waitForSelector(`.fwbrowse .fieldnames`);

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
            let rowCount: number | void = this.browseGetRowsDisplayed() as unknown as number;
            openBrowseResponse.opened = true;
            openBrowseResponse.recordCount = rowCount;
            openBrowseResponse.errorMessage = "";

            if (sleepafteropening > 0) {
                await TestUtils.sleepAsync(sleepafteropening);  // wait x seconds to allow other queries to complete
            }
        }
        return openBrowseResponse;
    }
    //---------------------------------------------------------------------------------------
    async browseGetRowsDisplayed(): Promise<number> {
        await page.waitForSelector(`.fwbrowse .fieldnames`);
        let records = await page.$$eval(`.fwbrowse tbody tr`, (e: any) => { return e; });
        let recordCount = records.length;
        Logging.logInfo(`Record Count: ${recordCount}`);
        return recordCount;
    }
    //---------------------------------------------------------------------------------------
    async browseSeek(seekObject: any): Promise<number> {
        await page.waitForSelector(`.fwbrowse .fieldnames`);
        for (var key in seekObject) {

            let seekValue = seekObject[key];
            if (seekValue.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                seekValue = TestUtils.getGlobalScopeValue(seekValue, this.globalScopeRef);
            }

            Logging.logInfo(`About to seek on field ${key} with ${seekValue}`);
            let selector = `.fwbrowse .field[data-browsedatafield="${key}"] .search input`;
            await page.waitForSelector(selector);
            let elementHandle = await page.$(selector);
            await elementHandle.click();
            await page.keyboard.sendCharacter(seekValue);
            await page.keyboard.press('Enter');
            await page.waitForFunction(() => document.querySelector('.pleasewait'), { polling: 'mutation' });
        }
        await ModuleBase.wait(300); // let the rows render
        let records = await page.$$eval(`.fwbrowse tbody tr`, (e: any) => { return e; });
        let recordCount = records.length;
        Logging.logInfo(`Record Count: ${recordCount}`);
        return recordCount;

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
        let selector = `.fwbrowse tbody tr.viewmode:nth-child(${index})`;
        await page.waitForSelector(selector);
        selector = `.fwbrowse tbody tr.viewmode`;
        await page.click(selector, { clickCount: 2 });
        //await page.waitFor(() => document.querySelector('.pleasewait'));
        //await page.waitFor(() => !document.querySelector('.pleasewait'));

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

                Logging.logInfo(`Form Record: ${JSON.stringify(openRecordResponse.record)}`);
                Logging.logInfo(`Form Keys: ${JSON.stringify(openRecordResponse.keys)}`);

                if (sleepAfterOpening > 0) {
                    await ModuleBase.wait(sleepAfterOpening);
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

        let selector = `.fwbrowse`;
        await page.waitForSelector(selector, { timeout: 3000 });

        selector = `.fwbrowse tbody tr.viewmode`;
        let records = await page.$$eval(selector, (e: any) => { return e; });
        var recordCount;
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
            selector += `:nth-child(1)`;
            await page.waitForSelector(selector);
            Logging.logInfo(`About to double-click the first row.`);
            await page.click(selector, { clickCount: 2 });
            //await page.waitFor(() => document.querySelector('.pleasewait'));

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

                    Logging.logInfo(`Form Record: ${JSON.stringify(openRecordResponse.record)}`);
                    Logging.logInfo(`Form Keys: ${JSON.stringify(openRecordResponse.keys)}`);

                    if (sleepAfterOpening > 0) {
                        await ModuleBase.wait(sleepAfterOpening);
                    }
                }
            }
        }
        return openRecordResponse;
    }
    //---------------------------------------------------------------------------------------
    async clickAllTabsOnForm(): Promise<ClickAllTabsResponse> {
        let clickAllTabsResponse: ClickAllTabsResponse = new ClickAllTabsResponse();
        clickAllTabsResponse.success = false;
        clickAllTabsResponse.tabCount = 0;
        clickAllTabsResponse.errorMessage = "tabs not clicked";

        let errorFound = false;

        let openFormCount = await this.countOpenForms();
        if (openFormCount > 0) {

            let tabSelector = `div .fwform .fwtabs .tab`;
            const tabs = await page.$$(tabSelector);
            clickAllTabsResponse.tabCount = tabs.length;

            if (clickAllTabsResponse.tabCount > 0) {
                for (let tab of tabs) {
                    let styleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), tab);
                    if ((styleAttributeValue === undefined) || (styleAttributeValue == null)) {
                        styleAttributeValue = "";
                    }
                    if (!styleAttributeValue.replace(' ', '').includes("display:none")) {  // only try to click on the tab if it is visible
                        await tab.click(); // click the tab

                        // wait 300 milliseconds, then check for a Please Wait dialog
                        var pleaseWaitDialog;
                        try {
                            pleaseWaitDialog = await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 300 });
                        } catch (error) { } // assume that we missed the Please Wait dialog

                        // if Please Wait dialog found, wait for it to go away
                        if (pleaseWaitDialog !== undefined) {
                            await page.waitFor(() => !document.querySelector('.pleasewait'));
                        }

                        // wait 300 milliseconds, then check for any errors
                        var popUp;
                        try {
                            popUp = await page.waitForSelector('.advisory', { timeout: 300 });
                        } catch (error) { } // no error pop-up

                        if (popUp !== undefined) {
                            errorFound = true;
                            let errorMessage = await page.$eval('.advisory', el => el.textContent);
                            clickAllTabsResponse.errorMessage = errorMessage;

                            Logging.logError(`Error clicking ${tab} tab: ` + errorMessage);

                            const options = await page.$$('.advisory .fwconfirmation-button');
                            await options[0].click() // click "OK" option
                                .then(() => {
                                    Logging.logInfo(`Clicked the "OK" button.`);
                                })
                        }
                    }
                }
            }
        }
        if (!errorFound) {
            clickAllTabsResponse.success = true;
            clickAllTabsResponse.errorMessage = "";
        }
        return clickAllTabsResponse;
    }
    //---------------------------------------------------------------------------------------
    async findDeleteButton(): Promise<boolean> {
        let foundDeleteButton: boolean = false;
        await page.waitForSelector(this.getBrowseSelector(), { timeout: 1000 });
        var newButton;
        try {
            newButton = await page.waitForSelector(this.getDeleteButtonSelector(), { timeout: 1000 });
        } catch (error) { } // not found
        foundDeleteButton = (newButton !== undefined);
        return foundDeleteButton;
    }
    //---------------------------------------------------------------------------------------
    async deleteRecord(index?: number, sleepAfterDeleting?: number): Promise<void> {
        if (index == undefined) {
            index = 1;
        }
        await page.waitForSelector(`.fwbrowse tbody tr.viewmode:nth-child(${index})`);
        await page.click('.fwbrowse tbody tr.viewmode', { clickCount: 1 });   // click the row
        await page.waitForSelector(this.getDeleteButtonSelector());
        await page.click(this.getDeleteButtonSelector(), { clickCount: 1 });  // click the delete button

        const popupText = await page.$eval('.advisory', el => el.textContent);
        if (popupText.includes('delete this record')) {
            Logging.logInfo(`Delete record, confirmation prompt detected.`);

            const options = await page.$$('.advisory .fwconfirmation-button');
            await options[0].click() // click "Yes" option
                .then(() => {
                    Logging.logInfo(`Clicked the "Yes" button.`);
                })
            await page.waitFor(() => !document.querySelector('.advisory'));
            await page.waitFor(() => document.querySelector('.pleasewait'));
            await page.waitFor(() => !document.querySelector('.pleasewait'));
        }
        Logging.logInfo(`Record deleted.`);

        if (sleepAfterDeleting > 0) {
            await ModuleBase.wait(sleepAfterDeleting);
        }
    }
    //---------------------------------------------------------------------------------------
    async countOpenForms(): Promise<number> {
        let formSelector = `.fwform .fwform-body`;
        let forms = await page.$$eval(formSelector, (e: any) => { return e; });
        var formCount;
        if (forms == undefined) {
            formCount = 0;
        }
        else {
            formCount = forms.length;
        }
        //Logging.logInfo(`Open Form Count: ${formCount}`);
        return formCount;
    }
    //---------------------------------------------------------------------------------------
    async findNewButton(): Promise<boolean> {
        let foundNewButton: boolean = false;
        await page.waitForSelector(this.getBrowseSelector(), { timeout: 1000 });
        var newButton;
        try {
            newButton = await page.waitForSelector(this.getNewButtonSelector(), { timeout: 1000 });
        } catch (error) { } // not found
        foundNewButton = (newButton !== undefined);
        return foundNewButton;
    }
    //---------------------------------------------------------------------------------------
    async createNewRecord(count?: number): Promise<CreateNewResponse> {
        let createNewResponse: CreateNewResponse = new CreateNewResponse()
        createNewResponse.success = false;
        createNewResponse.errorMessage = "could not create new";

        if (count === undefined) {
            count = 1;
        }

        await page.waitForSelector(this.getBrowseSelector(), { timeout: 1000 });

        let openFormCountBefore = await this.countOpenForms();

        //await page.waitForSelector(this.newButtonSelector, { timeout: 1000 });
        //await page.click(this.newButtonSelector, { clickCount: count });
        if (await this.findNewButton()) {
            await page.click(this.getNewButtonSelector(), { clickCount: count });
        }

        let formSelector = `.fwform`;
        await page.waitForSelector(formSelector, { timeout: 5000 })
            .then(async done => {
                let openFormCountAfter = await this.countOpenForms();

                if (openFormCountAfter === (openFormCountBefore + count)) {

                    const formCaption = await page.evaluate(() => {
                        const caption = jQuery('body').find('div.tab.active').attr('data-caption');
                        return caption;
                    })
                    if (formCaption === `New ${this.moduleCaption}`) {
                        createNewResponse.success = true;
                        createNewResponse.errorMessage = "";
                        createNewResponse.defaultRecord = await this.getFormRecord();
                        Logging.logInfo(`New ${this.moduleCaption} Created`);
                    } else {
                        createNewResponse.errorMessage = `New ${this.moduleCaption} not Created`;
                        Logging.logError(createNewResponse.errorMessage);
                    }
                }
                else {
                    createNewResponse.errorMessage = `Incorrect number of New ${this.moduleCaption} forms created. (${openFormCountAfter}, but expected ${openFormCountBefore + count})`;
                }
            });

        return createNewResponse;
    }
    //---------------------------------------------------------------------------------------
    async getDataType(fieldName: string): Promise<string> {
        let datatype;
        const field = await page.$(`.fwformfield[data-datafield="${fieldName}"]`);
        if (field != null) {
            datatype = await page.$eval(`.fwformfield[data-datafield="${fieldName}"]`, el => el.getAttribute('data-type'));
        } else {
            const isDisplayField = await page.$(`.fwformfield[data-displayfield="${fieldName}"]`);
            if (isDisplayField != null) {
                datatype = 'displayfield';
            }
        }
        //Logging.logInfo(`${fieldName} datatype is ${datatype}`);
        return datatype;
    }
    //---------------------------------------------------------------------------------------
    async populateFormWithRecord(record: any): Promise<any> {
        var currentValue = "";
        let newValue = "";
        for (var key in record) {
            //Logging.logInfo(`About to populate ${key} with ${record[key]}`);

            let fieldToPopulate = key;
            let valueToPopulate = record[key];

            if (valueToPopulate.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                valueToPopulate = TestUtils.getGlobalScopeValue(valueToPopulate, this.globalScopeRef);
            }
            Logging.logInfo(`About to populate ${fieldToPopulate} with ${valueToPopulate}`);

            let displayfield;
            const datatype = await this.getDataType(fieldToPopulate);
            if (datatype === 'displayfield') {
                displayfield = fieldToPopulate;
                fieldToPopulate = await page.$eval(`.fwformfield[data-displayfield="${fieldToPopulate}"]`, el => el.getAttribute('data-datafield'));
                Logging.logInfo(`About to populate ${fieldToPopulate} with ${valueToPopulate}`);
            }
            const tabId = await page.$eval(`.fwformfield[data-datafield="${fieldToPopulate}"]`, el => el.closest('[data-type="tabpage"]').getAttribute('data-tabid'));
            //Logging.logInfo(`Found ${key} field on tab ${tabId}`);
            const tabIsActive = await page.$eval(`#${tabId}`, el => el.classList.contains('active'));
            if (!tabIsActive) {
                Logging.logInfo(`Clicking tab ${tabId}`);
                await page.click(`#${tabId}`);
            }
            switch (datatype) {
                case 'phone':
                case 'email':
                case 'zipcode':
                case 'text':
                case 'textarea':
                    currentValue = await this.getDataFieldValue(fieldToPopulate);
                    if (currentValue != "") {
                        await this.clearInputField(fieldToPopulate);
                    }
                    await this.populateTextField(fieldToPopulate, valueToPopulate);
                    break;
                case 'validation':
                    const validationname = await page.$eval(`.fwformfield[data-datafield="${fieldToPopulate}"]`, el => el.getAttribute('data-validationname'));
                    await this.populateValidationField(fieldToPopulate, validationname, valueToPopulate);
                    break;
                case 'displayfield':
                    currentValue = await this.getDataFieldText(fieldToPopulate);
                    if (currentValue != "") {
                        await this.clearInputField(fieldToPopulate);
                    }
                    await this.populateValidationTextField(fieldToPopulate, valueToPopulate);
                    await ModuleBase.wait(750);  // allow "after validate" methods to finish
                    break;
                default:
                    break;
            }
        }
        await ModuleBase.wait(500);
    }
    //---------------------------------------------------------------------------------------
    async getFormKeys(): Promise<any> {
        let keys: any = {};
        const datafields = await page.$$eval(`.fwform .fwformfield[data-type="key"]`, fields => fields.map((field) => field.getAttribute('data-datafield')));
        for (let i = 0; i < datafields.length; i++) {
            if (datafields[i] != '') {
                let value = await this.getDataFieldValue(datafields[i]);
                keys[datafields[i]] = value;
            }
        }
        //Logging.logInfo(`Form Keys: ${JSON.stringify(record)}`);
        return keys;
    }
    //---------------------------------------------------------------------------------------
    async getFormRecord(): Promise<any> {
        let record: any = {};
        //Logging.logInfo(`About to gather form record for : ${this.moduleName}`);
        const datafields = await page.$$eval(`.fwform .fwformfield`, fields => fields.map((field) => field.getAttribute('data-datafield')));
        for (let i = 0; i < datafields.length; i++) {
            let dataField = datafields[i];
            if (dataField != '') {
                //Logging.logInfo(`About to gather field "${dataField}"`);
                let value;
                const datatype = await this.getDataType(dataField);
                switch (datatype) {
                    case 'phone':
                    case 'email':
                    case 'zipcode':
                    case 'text':
                    case 'textarea':
                    case 'key':
                        value = await this.getDataFieldValue(dataField);
                        record[dataField] = value;
                        break;
                    case 'validation':
                        value = await this.getDataFieldText(dataField);
                        const displayFieldName = await page.$eval(`.fwformfield[data-datafield="${dataField}"]`, el => el.getAttribute('data-displayfield'));
                        const displayValue = await this.getDataFieldValue(dataField);
                        record[dataField] = displayValue;
                        record[displayFieldName] = value;
                        break;
                    case 'togglebuttons':
                        value = ""; // un-handled field type
                        record[dataField] = value;
                        break;
                    default:
                        value = ""; // un-handled field type
                        record[dataField] = value;
                        break;
                }
            }
        }
        //Logging.logInfo(`Form Record: ${JSON.stringify(record)}`);
        return record;
    }
    //---------------------------------------------------------------------------------------
    async clearInputField(dataField: string): Promise<void> {
        const elementHandle = await page.$(`.fwformfield[data-datafield="${dataField}"] input`);
        await elementHandle.click();
        await elementHandle.focus();
        // click three times to select all
        await elementHandle.click({ clickCount: 3 });
        await elementHandle.press('Backspace');
    }
    //---------------------------------------------------------------------------------------
    async clickTab(tabCaption: string): Promise<void> {
        await page.click(`.fwform .tabs [data-caption="${tabCaption}"]`);
    }
    //---------------------------------------------------------------------------------------
    async populateTextField(dataField: string, value: string): Promise<void> {
        if (value === '') {
            await this.clearInputField(dataField);
        }
        else {
            //await page.type(`.fwformfield[data-datafield="${dataField}"] input`, value);
            const elementHandle = await page.$(`.fwformfield[data-datafield="${dataField}"] input`);
            await elementHandle.click();
            await page.keyboard.sendCharacter(value);
        }
    }
    //---------------------------------------------------------------------------------------
    async populateValidationTextField(dataField: string, value: string): Promise<void> {
        //await page.type(`.fwformfield[data-datafield="${dataField}"] .fwformfield-text`, value);
        //await page.keyboard.press('Enter');
        const elementHandle = await page.$(`.fwformfield[data-datafield="${dataField}"] .fwformfield-text`);
        await elementHandle.click();
        await page.keyboard.sendCharacter(value);
        await page.keyboard.press('Enter');
    }
    //---------------------------------------------------------------------------------------
    async populateValidationField(dataField: string, validationName: string, recordToSelect?: number): Promise<void> {
        if (recordToSelect === undefined) {
            recordToSelect = 1;
        }
        await page.click(`.fwformfield[data-datafield="${dataField}"] i.btnvalidate`);
        //await page.waitFor(500);
        await ModuleBase.wait(500);
        await page.waitForSelector(`div[data-name="${validationName}"] tr.viewmode:nth-child(1)`, { visible: true });
        await page.click(`div[data-name="${validationName}"] tr.viewmode:nth-child(${recordToSelect})`, { clickCount: 2 });
        await ModuleBase.wait(750);  // allow "after validate" methods to finish
    }
    //---------------------------------------------------------------------------------------
    async getDataFieldValue(dataField: string): Promise<string> {
        const selector = `div[data-datafield="${dataField}"] .fwformfield-value`;
        const val = await page.$eval(selector, (e: any) => {
            return e.value
        })
        return val;
    }
    //---------------------------------------------------------------------------------------
    async getDataFieldText(dataField: string): Promise<string> {
        const selector = `div[data-datafield="${dataField}"] .fwformfield-text`;
        const val = await page.$eval(selector, (e: any) => {
            return e.value
        })
        return val;
    }
    //---------------------------------------------------------------------------------------
    async getValueWithEvaluate(dataField: string) { //retaining this for reference for a different method of retrieving values in the browser
        const selector = `div[data-datafield="${dataField}"] .fwformfield-text`;
        const value = await page.evaluate((selector) => {
            const $form = jQuery('body').find('.fwform');
            const val = $form.find(selector).val();

            return val;
        }, selector)
        return value;
    }
    //---------------------------------------------------------------------------------------
    async addGridRow(gridController: string, className?: string, numberOfRows?: number, fieldObject?: any) {
        if (numberOfRows === undefined) {
            numberOfRows = 1;
        }
        // fieldobject is object of datafields and values to fill in
        let grid;
        if (className) {
            grid = `div[data-grid="${gridController}"].${className}`;
        } else {
            grid = `div[data-grid="${gridController}"]`;
        }

        await page.waitForSelector(`${grid} .buttonbar [data-type="NewButton"] i`);
        await ModuleBase.wait(1000);
        await page.click(`${grid} .buttonbar [data-type="NewButton"] i`); // add new row
        await page.waitForSelector(`${grid} tbody tr`);
        async function fillValues() {
            if (fieldObject !== null && (typeof fieldObject === 'object' && !Array.isArray(fieldObject))) {

                for (let key in fieldObject) {
                    console.log(`Adding to grid: `, `FieldName: "${key}"   Value: "${fieldObject[key]}"`);
                    const gridLineSelector = `${grid} .tablewrapper table tbody tr td div[data-browsedatafield="${key}"] input`;
                    const fieldValue = await page.$eval(gridLineSelector, (e: any) => e.value);
                    if (fieldValue != '') {
                        // clear out existing value
                        const elementHandle = await page.$(gridLineSelector);
                        await elementHandle.click();
                        await elementHandle.focus();
                        await elementHandle.click({ clickCount: 3 });
                        await elementHandle.press('Backspace');
                        // assign value
                        page.type(gridLineSelector, fieldObject[key]);
                    } else {
                        page.type(gridLineSelector, fieldObject[key]);
                    }
                    await ModuleBase.wait(3000);
                    await page.keyboard.press('Enter');
                }
                await ModuleBase.wait(1500);

                await page.keyboard.press('Enter');
                await ModuleBase.wait(1000);
                await page.click(`${grid} .tablewrapper table tbody tr td div.divsaverow i`);
                await ModuleBase.wait(3000);
            }
        }
        await fillValues();
    }
    //---------------------------------------------------------------------------------------
    async saveRecord(closeUnexpectedErrors: boolean = false): Promise<SaveResponse> {
        var selector = "";
        let response = new SaveResponse();
        response.saved = false;
        response.errorMessage = "not saved";

        let savingObject = await this.getFormRecord();
        Logging.logInfo(`About to try to save ${this.moduleCaption} Record: ${JSON.stringify(savingObject)}`);

        await page.click('.btn[data-type="SaveMenuBarButton"]');
        await page.waitForSelector('.advisory');
        await page.waitForFunction(() => document.querySelector('.advisory'), { polling: 'mutation' })
            .then(async done => {
                const afterSaveMsg = await page.$eval('.advisory', el => el.textContent);
                if ((afterSaveMsg.includes('saved')) && (!afterSaveMsg.includes('Error'))) {
                    Logging.logInfo(`${this.moduleCaption} Record saved: ${afterSaveMsg}`);

                    //make the "record saved" toaster message go away
                    await page.waitForSelector('.advisory .messageclose');
                    await page.click(`.advisory .messageclose`);
                    await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away

                    response.saved = true;
                    response.errorMessage = "";
                } else if (afterSaveMsg.includes('Error') || afterSaveMsg.includes('resolve')) {
                    Logging.logInfo(`${this.moduleCaption} Record not saved: ${afterSaveMsg}`);
                    response.saved = false;
                    response.errorMessage = afterSaveMsg;
                    response.errorFields = await page.$$eval(`.fwformfield.error`, fields => fields.map((field) => field.getAttribute('data-datafield')));
                    Logging.logInfo(`Error Fields: ${JSON.stringify(response.errorFields)}`);

                    if (closeUnexpectedErrors) {
                        //check for any error message pop-ups and click them to make error messages go away
                        selector = '.advisory .fwconfirmation-button';
                        const elementHandle = await page.$(selector);
                        if (elementHandle != null) {
                            //await page.waitForSelector('.advisory .fwconfirmation-button');
                            await page.waitForSelector(selector);
                            await page.click(`.fwconfirmation-button`);
                            await page.waitFor(() => !document.querySelector('.advisory'));
                        }
                    }

                    //check for the "record not saved" toaster message and make it go away
                    selector = `.advisory .messageclose`;
                    const elementHandle = await page.$(selector);
                    if (elementHandle != null) {
                        await page.waitForSelector(selector);
                        await page.click(selector);
                        await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
                    }


                }
            })
        return response;
    }
    //---------------------------------------------------------------------------------------
    async checkForDuplicatePrompt(): Promise<void> {
        await page.waitForSelector('.advisory .message');
        const popupText = await page.$eval('.advisory', el => el.textContent);
        expect(popupText).toContain('Duplicate Rule');
    }
    //---------------------------------------------------------------------------------------
    async closeDuplicatePrompt(): Promise<void> {
        await page.waitForSelector('.advisory .fwconfirmation-button');
        await page.click(`.fwconfirmation-button`);
        await page.waitFor(() => !document.querySelector('.advisory'));
    }
    //---------------------------------------------------------------------------------------
    async closeRecord(): Promise<void> {
        Logging.logInfo(`about to close form tab`);
        await page.click('div.delete');
        Logging.logInfo(`Record closed.`);
        //await page.waitForNavigation();
    }
    //---------------------------------------------------------------------------------------
    async closeModifiedRecordWithoutSaving(): Promise<void> {
        Logging.logInfo(`about to close form tab without saving`);
        await page.click('div.delete');
        const popupText = await page.$eval('.advisory', el => el.textContent);
        if (popupText.includes('save your changes')) {
            Logging.logInfo(`Close tab, save changes prompt detected.`);

            const options = await page.$$('.advisory .fwconfirmation-button');
            await options[1].click() // clicks "Don't Save" option
                .then(() => {
                    Logging.logInfo(`Clicked the "Don't Save" button.`);
                })
            await page.waitFor(() => !document.querySelector('.advisory'));
        }
        Logging.logInfo(`Record closed without saving.`);
        //await page.waitForNavigation();
    }
    //---------------------------------------------------------------------------------------
}