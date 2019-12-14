import { GlobalScope } from '../shared/GlobalScope';
import { ModuleBase, NewRecordToCreate, RecordToEdit } from './ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from './TestUtils';

export class SaveGridRowResponse {
    saved: boolean;
    errorMessage: string;
    errorFields: string[];
}

export class AddGridRowResponse extends SaveGridRowResponse { }

export class EditGridRowResponse extends SaveGridRowResponse { }

export class DeleteGridRowResponse {
    deleted: boolean;
    errorMessage: string;
}

export class GridRecordToCreate {
    grid: GridBase;
    recordToCreate?: NewRecordToCreate;
    recordToEdit?: RecordToEdit;
}

//---------------------------------------------------------------------------------------
export class GridBase {
    gridDisplayName: string;
    gridName: string;
    gridClass: string[];
    gridSelector: string;
    deleteTimeout: number;
    editTimeout: number;
    saveTimeout: number;

    canNew: boolean;
    canEdit: boolean;
    canDelete: boolean;
    multiRowSave: boolean;  // ie (order grids can save multiple rows at one time)

    newButtonSelector: string = "";

    waitAfterSavingToReloadGrid: number = 0;
    waitAfterInputtingEachCellValueOnNewRow: number = 500;
    waitBeforeClickingSaveButtonOnNewRow: number = 1000;
    waitBeforeClickingSaveButtonOnEditRow: number = 1000;
    waitForGridSubMenuToGetEvents: number = 250;

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

        this.newButtonSelector = `${this.gridSelector} .buttonbar [data-type="NewButton"] i`;


        this.canNew = true;
        this.canEdit = true;
        this.canDelete = true;
        this.deleteTimeout = 120000; // 120 seconds
        this.editTimeout = 120000; // 120 seconds
        this.saveTimeout = 120000; // 120 seconds
        this.globalScopeRef = GlobalScope;
    }
    //---------------------------------------------------------------------------------------
    async clickGridTab() {
        const tabId = await page.$eval(this.gridSelector, el => el.closest('[data-type="tabpage"]').getAttribute('data-tabid'));
        const tabIsActive = await page.$eval(`#${tabId}`, el => el.classList.contains('active'));
        if (!tabIsActive) {
            Logging.logInfo(`Clicking tab ${tabId}`);
            await page.click(`#${tabId}`);
            await ModuleBase.wait(1500); // wait for the grid to refresh if any
        }
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

        await this.clickGridTab();

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

        await this.clickGridTab();

        //let gridNewButtonSelector = `${this.gridSelector} .buttonbar [data-type="NewButton"] i`;
        var newButton;
        try {
            newButton = await page.waitForSelector(this.newButtonSelector, { timeout: 1000 });
        } catch (error) { } // not found
        buttonExists = (newButton !== undefined);
        return buttonExists;
    }
    //---------------------------------------------------------------------------------------
    async clickFirstEditableCell(rowNumber: number): Promise<boolean> {
        let foundEditableCell: boolean = false;

        let editableCellSelector = `${this.gridSelector} .tablewrapper table tbody tr:nth-child(${rowNumber}) .column[data-visible="true"] .editablefield`;
        const editableCells = await page.$$(editableCellSelector);

        if (editableCells) {
            for (let editableCell of editableCells) {
                let styleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), editableCell);
                if ((styleAttributeValue === undefined) || (styleAttributeValue == null)) {
                    styleAttributeValue = "";
                }
                if (!styleAttributeValue.replace(' ', '').includes("display:none")) {  // only consider the cell if it is displayed
                    //found a non-hidden cell
                    const cellColumn = (await editableCell.$x('..'))[0]; // parent column element

                    let styleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), cellColumn);
                    if ((styleAttributeValue === undefined) || (styleAttributeValue == null)) {
                        styleAttributeValue = "";
                    }
                    if (!styleAttributeValue.replace(' ', '').includes("display:none")) {  // only consider the column if it is displayed
                        //found a non-hidden cell

                        foundEditableCell = true;
                        await editableCell.click();
                        break; // exit the loop, cell was clicked
                    }
                }
            }
        }
        return foundEditableCell;
    }
    //---------------------------------------------------------------------------------------
    async checkForEditAbility(): Promise<boolean> {
        let canEdit: boolean = false;
        Logging.logInfo(`About to check for Edit ability on grid: ${this.gridName}`);

        await this.clickGridTab();

        //let editableCellSelector = `${this.gridSelector} .tablewrapper table tbody tr:nth-child(1) .column[data-visible="true"] .editablefield`;
        //const editableCells = await page.$$(editableCellSelector);
        //
        //let foundEditableCell: boolean = false;
        //if (editableCells) {
        //    for (let editableCell of editableCells) {
        //        let styleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), editableCell);
        //        if ((styleAttributeValue === undefined) || (styleAttributeValue == null)) {
        //            styleAttributeValue = "";
        //        }
        //        if (!styleAttributeValue.replace(' ', '').includes("display:none")) {  // only consider the cell if it is displayed
        //            //found a non-hidden cell
        //            const cellColumn = (await editableCell.$x('..'))[0]; // parent column element
        //
        //            let styleAttributeValue: string = await page.evaluate(el => el.getAttribute('style'), cellColumn);
        //            if ((styleAttributeValue === undefined) || (styleAttributeValue == null)) {
        //                styleAttributeValue = "";
        //            }
        //            if (!styleAttributeValue.replace(' ', '').includes("display:none")) {  // only consider the column if it is displayed
        //                //found a non-hidden cell
        //
        //                foundEditableCell = true;
        //                await editableCell.click();
        //                break; // exit the loop, cell was clicked
        //            }
        //        }
        //    }
        //}

        let foundEditableCell: boolean = await this.clickFirstEditableCell(1);

        if (foundEditableCell) {
            var editRow;
            try {
                let editModeRowSelector = `${this.gridSelector} .tablewrapper table tbody tr.editrow`;
                editRow = await page.waitForSelector(editModeRowSelector, { timeout: 3000 });
                Logging.logInfo(`found row in EDIT mode on grid: ${this.gridName}`);

                let cancelEditModeButtonSelector = `${this.gridSelector} .tablewrapper table tbody tr.editrow .divcancelsaverow i`;
                Logging.logInfo(`looking for grid row edit cancel button ${cancelEditModeButtonSelector}`);
                await page.waitForSelector(cancelEditModeButtonSelector);
                Logging.logInfo(`found grid row edit cancel button, about to click`);
                await page.click(cancelEditModeButtonSelector);
                Logging.logInfo(`clicked grid row edit cancel button`);

            } catch (error) { } // not found
            canEdit = (editRow !== undefined);
        }
        return canEdit;
    }
    //---------------------------------------------------------------------------------------
    async checkForDeleteOption(): Promise<boolean> {
        let deleteExists: boolean = false;
        Logging.logInfo(`About to check for delete option on grid: ${this.gridName}`);

        await this.clickGridTab();

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


        await page.waitForSelector(gridMenuSelector, { visible: true });
        await page.click(gridMenuSelector);
        await ModuleBase.wait(200); // wait for the grid hamburger to get its events
        Logging.logInfo(`clicked grid menu button to close menu on grid: ${this.gridName}`);


        return deleteExists;
    }
    //---------------------------------------------------------------------------------------
    async populateRowWithRecord(record: any): Promise<boolean> {
        let success: boolean = false;
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
                case 'money':
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
            if (this.waitAfterInputtingEachCellValueOnNewRow > 0) {
                await ModuleBase.wait(this.waitAfterInputtingEachCellValueOnNewRow);
            }
        }
        success = true;
        return success;
    }
    //---------------------------------------------------------------------------------------
    async saveRow(closeUnexpectedErrors: boolean = false): Promise<SaveGridRowResponse> {

        let response = new SaveGridRowResponse();
        response.saved = false;
        response.errorMessage = "not saved";
        response.errorFields = new Array<string>();

        Logging.logInfo(`about to find the Save button on grid: ${this.gridName}`);
        let gridRowSaveButtonSelector = `${this.gridSelector} .tablewrapper table tbody tr td div.divsaverow i`;

        if (this.multiRowSave) {
            var multiSaveButton;
            try {
                let gridMultiRowSaveButtonSelector = `${this.gridSelector} .grid-multi-save i`;
                multiSaveButton = await page.waitForSelector(gridMultiRowSaveButtonSelector, { timeout: 500 });
                if (multiSaveButton) {
                    gridRowSaveButtonSelector = gridMultiRowSaveButtonSelector;
                }
            } catch (error) { } // assume that there is no multi-save button
        }



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
            popUp = await page.waitForSelector('.advisory', { timeout: 750 });
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
            Logging.logInfo(`Found grid row cancel button. about to click`);
            await page.click(gridRowCancelButtonSelector);
        }
        else {
            Logging.logInfo(`no error pop-up found saving grid: ${this.gridName}.`);
            response.saved = true;
            response.errorMessage = "";
        }

        if (this.waitAfterSavingToReloadGrid > 0) {
            await ModuleBase.wait(this.waitAfterSavingToReloadGrid);
        }

        return response;

    }
    //---------------------------------------------------------------------------------------
    async addGridRow(record: any, closeUnexpectedErrors: boolean = false): Promise<AddGridRowResponse> {

        let response = new AddGridRowResponse();
        response.saved = false;
        response.errorMessage = "not saved";
        response.errorFields = new Array<string>();

        Logging.logInfo(`About to add a new row to grid: ${this.gridName}`);

        await this.clickGridTab();

        await page.waitForSelector(this.newButtonSelector, { visible: true });
        await ModuleBase.wait(1000); // wait for the New button to get its events
        await page.click(this.newButtonSelector);
        Logging.logInfo(`clicked New button on grid: ${this.gridName}`);

        let gridNewRowSelector = `${this.gridSelector} tbody tr`;
        await page.waitForSelector(gridNewRowSelector);
        Logging.logInfo(`found new row on grid: ${this.gridName}`);

        await this.populateRowWithRecord(record);

        if (this.waitBeforeClickingSaveButtonOnNewRow > 0) {
            await ModuleBase.wait(this.waitBeforeClickingSaveButtonOnNewRow);
        }

        response = await this.saveRow(closeUnexpectedErrors);

        return response;
    }
    //---------------------------------------------------------------------------------------
    async editGridRow(rowToEdit: number, record: any, closeUnexpectedErrors: boolean = false): Promise<EditGridRowResponse> {

        let response = new EditGridRowResponse();
        response.saved = false;
        response.errorMessage = "not saved";
        response.errorFields = new Array<string>();

        Logging.logInfo(`About to edit row in grid: ${this.gridName}`);

        await this.clickGridTab();

        //let editableCellSelector = `${this.gridSelector} .tablewrapper table tbody tr:nth-child(${rowToEdit}) .editablefield`;
        //Logging.logInfo(`editableCellSelector: ${editableCellSelector}`);
        //await page.waitForSelector(editableCellSelector);
        //Logging.logInfo(`found editable cell on grid: ${this.gridName}`);
        //await ModuleBase.wait(200); // wait for the grid row to get its events
        //
        //Logging.logInfo(`about to click editable cell on grid: ${this.gridName}`);
        //await page.click(editableCellSelector);

        await this.clickFirstEditableCell(1);

        let editModeRowSelector = `${this.gridSelector} .tablewrapper table tbody tr.editrow`;
        await page.waitForSelector(editModeRowSelector);
        Logging.logInfo(`found row in edit mode on grid: ${this.gridName}`);

        await this.populateRowWithRecord(record);

        if (this.waitBeforeClickingSaveButtonOnEditRow > 0) {
            await ModuleBase.wait(this.waitBeforeClickingSaveButtonOnEditRow);
        }

        response = await this.saveRow(closeUnexpectedErrors);
        return response;
    }
    //---------------------------------------------------------------------------------------
    async deleteGridRow(rowToDelete?: number, closeUnexpectedErrors?: boolean): Promise<DeleteGridRowResponse> {
        let response = new DeleteGridRowResponse();
        response.deleted = false;
        response.errorMessage = "record not deleted";

        if (rowToDelete === undefined) {
            rowToDelete = 1;
        }

        Logging.logInfo(`About to delete row from grid: ${this.gridName}`);

        await this.clickGridTab();

        await ModuleBase.wait(this.waitForGridSubMenuToGetEvents);  // wait for the grid sub menu to get its events
        let gridContextMenuSelector = `${this.gridSelector} .tablewrapper table tbody tr:nth-child(${rowToDelete}) .browsecontextmenu i`;
        Logging.logInfo(`About to wait for row context menu: ${gridContextMenuSelector}`);
        await page.waitForSelector(gridContextMenuSelector);
        await page.click(gridContextMenuSelector);
        Logging.logInfo(`clicked the row context menu`);

        //await ModuleBase.wait(250);  // wait for the grid sub menu to open
        let gridContextMenuDeleteOptionSelector = `${this.gridSelector} .tablewrapper table tbody tr:nth-child(${rowToDelete}) .browsecontextmenu .deleteoption`;
        Logging.logInfo(`About to wait for delete option: ${gridContextMenuDeleteOptionSelector}`);
        await page.waitForSelector(gridContextMenuDeleteOptionSelector);
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

            if (afterDeleteMsg.includes('Error') || afterDeleteMsg.includes('Not Found')) {
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
    async getRecordRowIndex(record: any): Promise<number> {
        let rowIndex: number = 0;

        Logging.logInfo(`About to try to find row for object: ${JSON.stringify(record)}`);

        await this.clickGridTab();

        Logging.logInfo(`About to wait for grid: ${this.gridSelector}`);
        await page.waitForSelector(this.gridSelector, { visible: true, timeout: 10000 });

        // if there are more records in the grid than shown, increase the rowcount 

        let rowSelector = this.gridSelector + ` tr.viewmode`;
        const rows = await page.$$(rowSelector);
        let rowCount = rows.length;
        Logging.logInfo(`Found ${rowCount} rows in grid: ${this.gridSelector}`);

        let recordFound: boolean = false;
        for (let r = 1; r <= rowCount; r++) {
            let rowSelector = this.gridSelector + ` tr.viewmode:nth-child(${r})`;

            for (let key in record) {
                let fieldToFind = key;
                let valueToFind = record[key];
                if (valueToFind.toString().toUpperCase().includes("GLOBALSCOPE.")) {
                    valueToFind = TestUtils.getGlobalScopeValue(valueToFind, this.globalScopeRef);
                }

                const datatype = await this.getGridDataType(fieldToFind);
                Logging.logInfo(`Looking for value of ${valueToFind} in field ${fieldToFind} in grid: ${this.gridSelector}`);

                let cellSelector = "";
                var cellValue = "";
                switch (datatype) {
                    case 'checkbox':
                        //cellSelector = rowSelector + ` td.column .field[data-browsedatafield="${fieldToFind}"] input`;
                        //cellValue = await page.$eval(cellSelector, (e: any) => e.value);
                        break;
                    default:
                        cellSelector = rowSelector + ` td.column .field[data-browsedatafield="${fieldToFind}"]`;
                        cellValue = await page.$eval(cellSelector, (e: any) => e.textContent);
                        break;
                }
                Logging.logInfo(`Found value of ${cellValue} in field ${fieldToFind} in grid: ${this.gridSelector}`);

                if (cellValue.toUpperCase() == valueToFind.toUpperCase()) {
                    recordFound = true;
                }
                else {
                    recordFound = false;
                    break;  // exit the column loop
                }
            }

            if (recordFound) {
            }

            if (recordFound) {
                rowIndex = r;
                break;  // exit the row loop
            }
        }

        return rowIndex;
    }
    //---------------------------------------------------------------------------------------
}