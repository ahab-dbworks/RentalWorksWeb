import { Logging } from '../shared/Logging';
import { TestUtils } from './TestUtils';

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
    async openModule(timeout?: number, sleepafteropening?: number): Promise<void> {
        if (!timeout) {
            timeout = 5000;
        }
        await ModuleBase.wait(750);
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
            await page.type(`.fwformfield[data-datafield="${dataField}"] input`, value);
        }
    }
    //---------------------------------------------------------------------------------------
    async populateValidationTextField(dataField: string, value: string): Promise<void> {
        await page.type(`.fwformfield[data-datafield="${dataField}"] .fwformfield-text`, value);
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
    async saveRecord(): Promise<boolean> {
        let successfulSave: boolean = false;
        await page.click('.btn[data-type="SaveMenuBarButton"]');
        await page.waitForSelector('.advisory');
        await page.waitForFunction(() => document.querySelector('.advisory'), { polling: 'mutation' })
            .then(async done => {
                const afterSaveMsg = await page.$eval('.advisory', el => el.textContent);
                if ((afterSaveMsg.includes('saved')) && (!afterSaveMsg.includes('Error'))) {
                    Logging.logger.info(`${this.moduleCaption} Record saved: ${afterSaveMsg}`);
                    successfulSave = true;
                } else if (afterSaveMsg.includes('Error') || afterSaveMsg.includes('resolve')) {
                    Logging.logger.info(`${this.moduleCaption} Record not saved: ${afterSaveMsg}`);
                    successfulSave = false;
                }
            })
        return successfulSave;
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