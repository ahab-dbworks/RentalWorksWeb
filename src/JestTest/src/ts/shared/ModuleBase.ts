import { Logging } from '../shared/Logging';
import { TestUtils } from './TestUtils';


export class SaveResponse {
    saved: boolean;
    errorMessage: string;
    errorFields: any;
}

//---------------------------------------------------------------------------------------
export class ModuleBase {
    moduleName: string;
    moduleBtnId: string;
    moduleCaption: string;

    //---------------------------------------------------------------------------------------
    constructor() { }
    //---------------------------------------------------------------------------------------
    static async wait(milliseconds: number): Promise<void> {
        await page.waitFor(milliseconds)
    }
    //---------------------------------------------------------------------------------------
    async openBrowse(timeout?: number, sleepafteropening?: number): Promise<void> {
        if (!timeout) {
            timeout = 5000;
        }
        await page.waitForSelector('.appmenu')
        //await ModuleBase.wait(250);
        await page.click('.appmenu');
        await expect(page).toClick(this.moduleBtnId, { timeout: timeout });
        await page.waitForSelector('div.tab.active[data-tabtype="BROWSE"]')
            .then(async done => {
                const evaluateBrowse = await page.evaluate(() => {
                    const moduleTabText = jQuery('body').find('div.tab.active[data-tabtype="BROWSE"]').text();
                    return moduleTabText;
                })
                if (evaluateBrowse.includes(`${this.moduleCaption}`)) {
                    Logging.logger.info(`Opened ${this.moduleCaption} module`);
                } else {
                    Logging.logger.error(`Error opening ${this.moduleCaption} module`);
                    await browser.close();
                }
            })
        if (sleepafteropening > 0) {
            await TestUtils.sleepAsync(sleepafteropening);  // wait x seconds to allow other queries to complete
        }
    }
    //---------------------------------------------------------------------------------------
    async openRecord(index: number, sleepafteropening?: number): Promise<void> {
        await page.waitForSelector(`.fwbrowse tbody tr.viewmode:nth-child(${index})`);
        await page.click('.fwbrowse tbody tr.viewmode', { clickCount: 2 });
        if (sleepafteropening > 0) {
            await TestUtils.sleepAsync(sleepafteropening);
        }
    }
    //---------------------------------------------------------------------------------------
    async createNewRecord(count?: number): Promise<void> {

        if (count === undefined) {
            count = 1;
        }

        await page.waitForSelector(`.fwbrowse tbody`);
        await page.click('.addnewtab i.material-icons', { clickCount: count });
        await page.waitForSelector(`.fwform`)
            .then(async done => {
                const formCaption = await page.evaluate(() => {
                    const caption = jQuery('body').find('div.tab.active').attr('data-caption');
                    return caption;
                })
                if (formCaption !== '') {
                    Logging.logger.info(`New ${this.moduleCaption} Created`);
                } else {
                    Logging.logger.error(`New ${this.moduleCaption} not Created`);
                    await browser.close();
                }
            })
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
        Logging.logger.info(`${fieldName} datatype is ${datatype}`);
        return datatype;
    }
    //---------------------------------------------------------------------------------------
    async populateFormWithRecord(record: any): Promise<any> {
        var currentValue = "";
        for (var key in record) {
            Logging.logger.info(`About to populate ${key} with ${record[key]}`);
            let displayfield;
            const datatype = await this.getDataType(key);
            if (datatype === 'displayfield') {
                displayfield = key;
                key = await page.$eval(`.fwformfield[data-displayfield="${key}"]`, el => el.getAttribute('data-datafield'));
                Logging.logger.info(`About to populate ${key} with ${record[key]}`);
            }
            const tabId = await page.$eval(`.fwformfield[data-datafield="${key}"]`, el => el.closest('[data-type="tabpage"]').getAttribute('data-tabid'));
            Logging.logger.info(`Found ${key} field on tab ${tabId}`);
            const tabIsActive = await page.$eval(`#${tabId}`, el => el.classList.contains('active'));
            if (!tabIsActive) {
                Logging.logger.info(`Clicking tab ${tabId}`);
                await page.click(`#${tabId}`);
            }
            switch (datatype) {
                case 'phone':
                case 'email':
                case 'zipcode':
                case 'text':
                    currentValue = await this.getDataFieldValue(key);
                    if (currentValue != "") {
                        await this.clearInputField(key);
                    }
                    await this.populateTextField(key, record[key]);
                    break;
                case 'validation':
                    const validationname = await page.$eval(`.fwformfield[data-datafield="${key}"]`, el => el.getAttribute('data-validationname'));
                    await this.populateValidationField(key, validationname, record[key]);
                    break;
                case 'displayfield':
                    //await this.clearInputField(key);
                    await this.populateValidationTextField(key, record[displayfield]);
                    await ModuleBase.wait(750);  // allow "after validate" methods to finish
                    break;
                default:
                    break;
            }
        }
        await ModuleBase.wait(500);
    }
    //---------------------------------------------------------------------------------------
    async getFormRecord(): Promise<any> {
        let record: any = {};
        const datafields = await page.$$eval(`.fwform .fwformfield`, fields => fields.map((field) => field.getAttribute('data-datafield')));
        for (let i = 0; i < datafields.length; i++) {
            if (datafields[i] != '') {
                let value;
                const datatype = await this.getDataType(datafields[i]);
                switch (datatype) {
                    case 'phone':
                    case 'email':
                    case 'zipcode':
                    case 'text':
                        value = await this.getDataFieldValue(datafields[i]);
                        record[datafields[i]] = value;
                        break;
                    case 'validation':
                        value = await this.getDataFieldText(datafields[i]);
                        const displayFieldName = await page.$eval(`.fwformfield[data-datafield="${datafields[i]}"]`, el => el.getAttribute('data-displayfield'));
                        const displayValue = await this.getDataFieldValue(datafields[i]);
                        record[datafields[i]] = displayValue;
                        record[displayFieldName] = value;
                        break;
                    case 'togglebuttons':
                        value = ""; // un-handled field type
                        record[datafields[i]] = value;
                        break;
                    default:
                        value = ""; // un-handled field type
                        record[datafields[i]] = value;
                        break;
                }
            }
        }
        //Logging.logger.info(`Form Record: ${JSON.stringify(record)}`);
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
        Logging.logger.info(`About to try to save ${this.moduleCaption} Record: ${JSON.stringify(savingObject)}`);

        await page.click('.btn[data-type="SaveMenuBarButton"]');
        await page.waitForSelector('.advisory');
        await page.waitForFunction(() => document.querySelector('.advisory'), { polling: 'mutation' })
            .then(async done => {
                const afterSaveMsg = await page.$eval('.advisory', el => el.textContent);
                if ((afterSaveMsg.includes('saved')) && (!afterSaveMsg.includes('Error'))) {
                    Logging.logger.info(`${this.moduleCaption} Record saved: ${afterSaveMsg}`);

                    //make the "record saved" toaster message go away
                    await page.waitForSelector('.advisory .messageclose');
                    await page.click(`.advisory .messageclose`);
                    await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away

                    response.saved = true;
                    response.errorMessage = "";
                } else if (afterSaveMsg.includes('Error') || afterSaveMsg.includes('resolve')) {
                    Logging.logger.info(`${this.moduleCaption} Record not saved: ${afterSaveMsg}`);
                    response.saved = false;
                    response.errorMessage = afterSaveMsg;
                    response.errorFields = await page.$$eval(`.fwformfield.error`, fields => fields.map((field) => field.getAttribute('data-datafield')));
                    Logging.logger.info(`Error Fields: ${JSON.stringify(response.errorFields)}`);

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
        await page.click('div.delete');
        Logging.logger.info(`Record closed.`);
        //await page.waitForNavigation();
    }
    //---------------------------------------------------------------------------------------
    async closeModifiedRecordWithoutSaving(): Promise<void> {
        await page.click('div.delete');
        const popupText = await page.$eval('.advisory', el => el.textContent);
        if (popupText.includes('save your changes')) {
            Logging.logger.info(`Close tab, save changes propmt detected.`);

            const options = await page.$$('.advisory .fwconfirmation-button');
            await options[1].click() // clicks "Don't Save" option
                .then(() => {
                    Logging.logger.info(`Clicked the "Don't Save" button.`);
                })
            await page.waitFor(() => !document.querySelector('.advisory'));
        }
        Logging.logger.info(`Record closed without saving.`);
        //await page.waitForNavigation();
    }
    //---------------------------------------------------------------------------------------
}