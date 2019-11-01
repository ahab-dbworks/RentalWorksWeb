import { GlobalScope } from '../shared/GlobalScope';
import { ModuleBase, NewRecordToCreate } from './ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from './TestUtils';

export class AddGridRowResponse {
    saved: boolean;
    errorMessage: string;
    errorFields: string[];
}

export class DeleteGridRowResponse {
    deleted: boolean;
    errorMessage: string;
}

export class GridRecordToCreate {
    grid: GridBase;
    recordToCreate: NewRecordToCreate;
}

//---------------------------------------------------------------------------------------
export class GridBase {
    gridDisplayName: string;
    gridName: string;
    gridClass: string[];
    gridSelector: string;
    deleteTimeout: number;
    saveTimeout: number;

    canNew: boolean;
    canEdit: boolean;
    canDelete: boolean;

    defaultNewRecordToExpect?: any;
    newRecordsToCreate?: NewRecordToCreate[];

    globalScopeRef?= GlobalScope;

    //---------------------------------------------------------------------------------------
    constructor(gridDisplayName: string, gridName: string, gridClass?: string[]) {
        if ((gridClass == undefined) || (gridClass == null)) {
            gridClass = new Array();
        }
        this.gridDisplayName = gridDisplayName;
        this.gridName = gridName;
        this.gridClass = gridClass;

        this.gridSelector = `div[data-grid="${this.gridName}"]`;
        if (this.gridClass) {
            for (let cl of this.gridClass) {
                this.gridSelector += `.${cl}`;
            }
        }

        this.canNew = true;
        this.canEdit = true;
        this.canDelete = true;
        this.deleteTimeout = 120000; // 120 seconds
        this.saveTimeout = 120000; // 120 seconds
        this.globalScopeRef = GlobalScope;
    }
    //---------------------------------------------------------------------------------------
    async getGridDataType(fieldName: string): Promise<string> {
        let datatype;
        let gridFieldSelector = `${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedatafield="${fieldName}"]`;
        const field = await page.$(gridFieldSelector);
        if (field != null) {
            datatype = await page.$eval(gridFieldSelector, el => el.getAttribute('data-browsedatatype'));
        } else {
            gridFieldSelector = `${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedisplayfield="${fieldName}"]`;
            const isDisplayField = await page.$(gridFieldSelector);
            if (isDisplayField != null) {
                datatype = 'displayfield';
            }
        }
        Logging.logInfo(`${fieldName} datatype is ${datatype}`);
        return datatype;
    }
    //---------------------------------------------------------------------------------------
    async populateGridValidationField(dataField: string, validationName: string, recordToSelect?: number): Promise<void> {
        if (recordToSelect === undefined) {
            recordToSelect = 1;
        }
        await page.click(`${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedatafield="${dataField}"] .btnvalidate`);
        await ModuleBase.wait(500);  // wait for validation to open
        await page.waitForSelector(`div[data-name="${validationName}"] tr.viewmode:nth-child(1)`, { visible: true });
        await page.click(`div[data-name="${validationName}"] tr.viewmode:nth-child(${recordToSelect})`, { clickCount: 2 });
    }
    //---------------------------------------------------------------------------------------
    async populateGridValidationTextField(dataField: string, value: string): Promise<void> {
        let gridFieldSelector: string = `${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedatafield="${dataField}"] input.text`;
        let currentValue = await page.$eval(gridFieldSelector, (e: any) => e.value);
        const elementHandle = await page.$(gridFieldSelector);
        await elementHandle.click();
        await elementHandle.focus();
        if (currentValue != '') {
            await elementHandle.click({ clickCount: 3 });
            await elementHandle.press('Backspace');
        }
        await page.keyboard.sendCharacter(value);
        await page.keyboard.press('Tab');
    }
    //---------------------------------------------------------------------------------------
    async getRecordCount(): Promise<number> {
        Logging.logInfo(`About to check number of rows in grid: ${this.gridName}`);

        const tabId = await page.$eval(this.gridSelector, el => el.closest('[data-type="tabpage"]').getAttribute('data-tabid'));
        const tabIsActive = await page.$eval(`#${tabId}`, el => el.classList.contains('active'));
        if (!tabIsActive) {
            Logging.logInfo(`Clicking tab ${tabId} in getRecordCount`);
            await page.click(`#${tabId}`);
            await ModuleBase.wait(1500); // wait for the grid to refresh if any
        }

        await page.waitForSelector(this.gridSelector);
        let records = await page.$$eval(`${this.gridSelector} tbody tr`, (e: any) => { return e; });
        let recordCount = records.length;
        Logging.logInfo(`Record Count: ${recordCount}`);
        return recordCount;

    }
    //---------------------------------------------------------------------------------------
    async checkForNewButton(): Promise<boolean> {
        let buttonExists: boolean = false;
        Logging.logInfo(`About to check for new button on grid: ${this.gridName}`);

        const tabId = await page.$eval(this.gridSelector, el => el.closest('[data-type="tabpage"]').getAttribute('data-tabid'));
        const tabIsActive = await page.$eval(`#${tabId}`, el => el.classList.contains('active'));
        if (!tabIsActive) {
            Logging.logInfo(`Clicking tab ${tabId} in addGridRow`);
            await page.click(`#${tabId}`);
            await ModuleBase.wait(500); // wait for the grid to refresh if any
        }

        let gridNewButtonSelector = `${this.gridSelector} .buttonbar [data-type="NewButton"] i`;
        var newButton;
        try {
            newButton = await page.waitForSelector(gridNewButtonSelector, { timeout: 1000 });
        } catch (error) { } // not found
        buttonExists = (newButton !== undefined);
        return buttonExists;
    }
    //---------------------------------------------------------------------------------------
    async checkForDeleteOption(): Promise<boolean> {
        let deleteExists: boolean = false;
        Logging.logInfo(`About to check for delete option on grid: ${this.gridName}`);

        const tabId = await page.$eval(this.gridSelector, el => el.closest('[data-type="tabpage"]').getAttribute('data-tabid'));
        const tabIsActive = await page.$eval(`#${tabId}`, el => el.classList.contains('active'));
        if (!tabIsActive) {
            Logging.logInfo(`Clicking tab ${tabId} in addGridRow`);
            await page.click(`#${tabId}`);
            await ModuleBase.wait(500); // wait for the grid to refresh if any
        }

        let gridMenuSelector = `${this.gridSelector} .submenubutton i`;
        await page.waitForSelector(gridMenuSelector, { visible: true });
        await ModuleBase.wait(200); // wait for the grid hamburger to get its events
        await page.click(gridMenuSelector);
        Logging.logInfo(`clicked grid menu button on grid: ${this.gridName}`);


        let deleteOptionSelector = `${this.gridSelector} .submenu-btn .deleteoption`;  // need to add "delete" class to this menu option
        var deleteOption;
        try {
            deleteOption = await page.waitForSelector(deleteOptionSelector, { timeout: 1000 });
        } catch (error) { } // not found
        deleteExists = (deleteOption !== undefined);
        return deleteExists;
    }
    //---------------------------------------------------------------------------------------
    async addGridRow(record: any, closeUnexpectedErrors: boolean = false): Promise<AddGridRowResponse> {

        let response = new AddGridRowResponse();
        response.saved = false;
        response.errorMessage = "not saved";
        response.errorFields = new Array<string>();

        Logging.logInfo(`About to add a new row to grid: ${this.gridName}`);

        const tabId = await page.$eval(this.gridSelector, el => el.closest('[data-type="tabpage"]').getAttribute('data-tabid'));
        const tabIsActive = await page.$eval(`#${tabId}`, el => el.classList.contains('active'));
        if (!tabIsActive) {
            Logging.logInfo(`Clicking tab ${tabId} in addGridRow`);
            await page.click(`#${tabId}`);
            await ModuleBase.wait(1500); // wait for the grid to refresh if any
        }

        let gridNewButtonSelector = `${this.gridSelector} .buttonbar [data-type="NewButton"] i`;
        await page.waitForSelector(gridNewButtonSelector, { visible: true });
        await ModuleBase.wait(1000); // wait for the New button to get its events
        await page.click(gridNewButtonSelector);
        Logging.logInfo(`clicked New button on grid: ${this.gridName}`);


        let gridNewRowSelector = `${this.gridSelector} tbody tr`;
        await page.waitForSelector(gridNewRowSelector);
        Logging.logInfo(`found new row on grid: ${this.gridName}`);

        for (let key in record) {
            let fieldToPopulate = key;
            let valueToPopulate = record[key];

            if (valueToPopulate.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                valueToPopulate = TestUtils.getGlobalScopeValue(valueToPopulate, this.globalScopeRef);
            }
            Logging.logInfo(`About to populate grid FieldName: "${fieldToPopulate}"   Value: "${valueToPopulate}"`);

            const datatype = await this.getGridDataType(fieldToPopulate);
            if (datatype === 'displayfield') {
                let displayfield = fieldToPopulate;
                fieldToPopulate = await page.$eval(`${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedisplayfield="${displayfield}"]`, el => el.getAttribute('data-browsedatafield'));
                Logging.logInfo(`About to populate grid FieldName: "${fieldToPopulate}"   Value: "${valueToPopulate}"`);
            }

            var currentValue = "";
            let gridFieldSelector: string = "";
            switch (datatype) {
                case 'phone':
                case 'email':
                case 'zipcode':
                case 'percent':
                case 'number':
                case 'password':
                case 'date':
                case 'text':
                    gridFieldSelector = `${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedatafield="${fieldToPopulate}"] input`;
                    currentValue = await page.$eval(gridFieldSelector, (e: any) => e.value);
                    const elementHandle = await page.$(gridFieldSelector);
                    await elementHandle.click();
                    await elementHandle.focus();
                    if (currentValue != '') {
                        await elementHandle.click({ clickCount: 3 });
                        await elementHandle.press('Backspace');
                    }
                    await page.keyboard.sendCharacter(valueToPopulate);
                    await page.keyboard.press('Tab');
                    break;
                case 'checkbox':
                    break;
                case 'radio':
                    break;
                case 'validation':
                    Logging.logInfo(`about to populate field ${fieldToPopulate}`);
                    let displayField = await page.$eval(`${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedatafield="${fieldToPopulate}"]`, el => el.getAttribute('data-browsedisplayfield'));
                    Logging.logInfo(`displayField is ${displayField}`);
                    gridFieldSelector = `${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedatafield="${fieldToPopulate}"] input.text`;
                    Logging.logInfo(`gridFieldSelector is ${gridFieldSelector}`);
                    currentValue = await page.$eval(gridFieldSelector, (e: any) => e.value);
                    Logging.logInfo(`currentValue is ${currentValue}`);
                    if (currentValue != "") {
                        await this.populateGridValidationTextField(fieldToPopulate, "");
                    }
                    if (valueToPopulate !== 0) {
                        const validationname = await page.$eval(`${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedatafield="${fieldToPopulate}"]`, el => el.getAttribute('data-validationname'));
                        Logging.logInfo(`validationname is ${validationname}`);
                        await this.populateGridValidationField(fieldToPopulate, validationname, valueToPopulate);
                    }
                    break;
                case 'select':
                    break;
                case 'displayfield':
                    gridFieldSelector = `${this.gridSelector} .tablewrapper table tbody tr td div[data-browsedatafield="${fieldToPopulate}"] input`;
                    currentValue = await page.$eval(gridFieldSelector, (e: any) => e.value);
                    if (currentValue != "") {
                        // clear out existing value
                        const elementHandle = await page.$(gridFieldSelector);
                        await elementHandle.click();
                        await elementHandle.focus();
                        await elementHandle.click({ clickCount: 3 });
                        await elementHandle.press('Backspace');
                    }
                    await page.keyboard.sendCharacter(valueToPopulate);
                    await page.keyboard.press('Tab');
                    break;
                default:
                    break;
            }
            await ModuleBase.wait(1500);  // wait for blur/validation events to finish
        }

        Logging.logInfo(`about to find the Save button on grid: ${this.gridName}`);
        let gridRowSaveButtonSelector = `${this.gridSelector} .tablewrapper table tbody tr td div.divsaverow i`;
        await page.waitForSelector(gridRowSaveButtonSelector, { visible: true });
        Logging.logInfo(`found the Save button on grid: ${this.gridName}.  About to click.`);
        await page.click(gridRowSaveButtonSelector);

        Logging.logInfo(`about to wait for the Please Wait dialog after saving grid: ${this.gridName}.`);
        // wait 300 milliseconds for a Please Wait dialog
        var pleaseWaitDialog;
        try {
            pleaseWaitDialog = await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 300 });
        } catch (error) { } // assume that we missed the Please Wait dialog

        // if Please Wait dialog found, wait for it to go away
        if (pleaseWaitDialog !== undefined) {
            await page.waitFor(() => !document.querySelector('.pleasewait'), { timeout: this.saveTimeout });
        }
        Logging.logInfo(`Please Wait dialog is gone after saving grid: ${this.gridName}.`);

        Logging.logInfo(`about to check for error pop-up after saving grid: ${this.gridName}.`);
        // wait 300 milliseconds for any errors
        var popUp;
        try {
            popUp = await page.waitForSelector('.advisory', { timeout: 300 });
        } catch (error) { }  // no error pop-up

        if (popUp !== undefined) {
            Logging.logInfo(`error pop-up found saving grid: ${this.gridName}.`);
            let selector: string = ``;
            let errorMessage = await page.$eval('.advisory', el => el.textContent);
            response.saved = false;
            response.errorMessage = errorMessage;
            response.errorFields = await page.$$eval(`div.field.error`, fields => fields.map((field) => field.getAttribute('data-browsedatafield')));
            Logging.logInfo(`${this.gridDisplayName} Record not saved: ${errorMessage}`);
            Logging.logInfo(`Error Fields: ${JSON.stringify(response.errorFields)}`);

            if (closeUnexpectedErrors) {
                //check for any error message pop-ups and click them to make error messages go away
                Logging.logInfo(`About to check for error prompt and close it`);
                selector = '.advisory .fwconfirmation-button';
                const elementHandle = await page.$(selector);
                if (elementHandle != null) {
                    Logging.logInfo(`Found button on prompt, about to click`);
                    await elementHandle.click();
                    await page.waitFor(() => !document.querySelector('.advisory'));
                }
            }

            //check for the "record not saved" toaster message and make it go away
            selector = `.advisory .messageclose`;
            const elementHandle = await page.$(selector);
            if (elementHandle != null) {
                Logging.logInfo(`Found toaster button, about to click`);
                await elementHandle.click();
                await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
            }

            // cancel the insert row
            let gridRowCancelButtonSelector = `${this.gridSelector} .tablewrapper table tbody tr td div.divcancelsaverow i`;
            await page.waitForSelector(gridRowCancelButtonSelector, { visible: true });
            await page.click(gridRowCancelButtonSelector);
        }
        else {
            Logging.logInfo(`no error pop-up found saving grid: ${this.gridName}.`);
            response.saved = true;
            response.errorMessage = "";
        }
        return response;
    }
    //---------------------------------------------------------------------------------------
    async deleteGridRow(rowToDelete?: number, closeUnexpectedErrors?: boolean): Promise<DeleteGridRowResponse> {
        let response = new DeleteGridRowResponse();
        response.deleted = false;
        response.errorMessage = "record not deleted";

        Logging.logInfo(`About to delete row from grid: ${this.gridName}`);

        const tabId = await page.$eval(this.gridSelector, el => el.closest('[data-type="tabpage"]').getAttribute('data-tabid'));
        const tabIsActive = await page.$eval(`#${tabId}`, el => el.classList.contains('active'));
        if (!tabIsActive) {
            Logging.logInfo(`Clicking tab ${tabId} in deleteGridRow`);
            await page.click(`#${tabId}`);
        }

        let gridContextMenuSelector = `${this.gridSelector} .tablewrapper table tbody tr .browsecontextmenu i`;
        Logging.logInfo(`About to wait for row context menu: ${gridContextMenuSelector}`);
        await page.waitForSelector(gridContextMenuSelector, { visible: true });
        await page.click(gridContextMenuSelector);
        Logging.logInfo(`clicked the row context menu`);

        let gridContextMenuDeleteOptionSelector = `${this.gridSelector} .tablewrapper table tbody tr .browsecontextmenu .deleteoption`;
        Logging.logInfo(`About to wait for delete option: ${gridContextMenuDeleteOptionSelector}`);
        await page.waitForSelector(gridContextMenuDeleteOptionSelector, { visible: true });
        await page.click(gridContextMenuDeleteOptionSelector);
        Logging.logInfo(`clicked the delete option`);


        const popupText = await page.$eval('.advisory', el => el.textContent);
        if (popupText.includes('Delete Record')) {
            Logging.logInfo(`Delete record, confirmation prompt detected.`);

            const options = await page.$$('.advisory .fwconfirmation-button');
            await options[0].click() // click "Yes" option
                .then(() => {
                    Logging.logInfo(`Clicked the "Yes" button.`);
                })
            await page.waitFor(() => !document.querySelector('.advisory'));

            try {
                await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 3000 });
            } catch (error) { } // assume that we missed the Please Wait dialog

            await page.waitFor(() => !document.querySelector('.pleasewait'));
            Logging.logInfo(`Finished waiting for the Please Wait dialog.`);

            let afterDeleteMsg: string = "";
            try {
                await page.waitFor(() => document.querySelector('.advisory'), { timeout: 500 });
                afterDeleteMsg = await page.$eval('.advisory', el => el.textContent);
            } catch (error) { } // assume that no error occurred

            if (afterDeleteMsg.includes('Error')) {
                Logging.logInfo(`${this.gridDisplayName} Record not deleted: ${afterDeleteMsg}`);
                response.deleted = false;
                response.errorMessage = afterDeleteMsg;

                if (closeUnexpectedErrors) {
                    //check for any error message pop-ups and click them to make error messages go away
                    Logging.logInfo(`About to check for error prompt and close it`);
                    let selector = '.advisory .fwconfirmation-button';
                    const elementHandle = await page.$(selector);
                    if (elementHandle != null) {
                        Logging.logInfo(`Found button on prompt, about to click`);
                        await elementHandle.click();
                        await page.waitFor(() => !document.querySelector('.advisory'));
                    }
                }
            } else {
                Logging.logInfo(`${this.gridDisplayName} Record deleted: ${afterDeleteMsg}`);
                response.deleted = true;
                response.errorMessage = "";
            }
        }
        return response;
    }
    //---------------------------------------------------------------------------------------
}