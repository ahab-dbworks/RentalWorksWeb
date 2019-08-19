import { TestUtils } from './TestUtils';
const fs = require('fs');
const { createLogger, format, transports } = require('winston');
const { combine, timestamp, label, printf } = format;
const myFormat: any = printf(({ level, message, label, timestamp }) => `${timestamp} ${level}: ${message}`);
const sgMail = require('@sendgrid/mail');
const logger = createLogger({
    format: combine(
        timestamp(),
        myFormat
    ),
    defaultMeta: { service: 'user-service' },
    transports: [
        new transports.Console(),
        new transports.File({ filename: 'error.log', level: 'error' }),
        new transports.File({ filename: 'combined.log', level: 'info' }),
    ]
});

beforeAll(async () => {
    await page.setViewport({ width: 1600, height: 1080 })
        .then()
        .catch(err => logger.error('Error in BeforeAll: ', err));
});

export class ModuleBase {
    moduleName: string;
    moduleBtnId: string;
    moduleCaption: string;


    //constructor(moduleName: string, moduleBtnId: string, moduleCaption: string) {
    //    this.moduleName = moduleName;
    //    this.moduleBtnId = moduleBtnId;
    //    this.moduleCaption = moduleCaption;
    //}

    constructor() { }

    static async authenticate(): Promise<void> {
        let baseUrl: string = "http://localhost/rentalworksweb";
        let continueTest;
        await page.goto(`${baseUrl}/#/login`);
        await page.waitForNavigation();
        await page.waitForSelector('.btnLogin', { visible: true });
        await page.click('.btnLogin');
        await page.waitForSelector('#email', { visible: true });
        await page.type('#email', <string>process.env.RW_EMAIL);
        await page.click('#password');
        await page.type('#password', <string>process.env.RW_PASSWORD);
        await page.click('.btnLogin');
        await page.waitForSelector('div.fwoverlay', { visible: true, timeout: 20000 });
        await page.waitForFunction(() => !document.querySelector('div.fwoverlay'), { polling: 'mutation' })
            .then(async done => {
                const evaluateLogin = await page.evaluate(() => {
                    const errorMessageText = jQuery('body').find('.errormessage').text();
                    return errorMessageText;
                })
                if (evaluateLogin.includes('Invalid')) {
                    logger.error(`Error in login: ${evaluateLogin}`);
                    await browser.close(); // abort test by closing window. Probably a better way
                    continueTest = false;
                } else if (evaluateLogin === '') {
                    logger.info(`Successful login for user ${process.env.RW_EMAIL}`);
                    continueTest = true;
                }
            })
        if (continueTest) {
            await page.waitForSelector('.appmenu', { visible: true, timeout: 20000 }) // Upper left 'hamburger menu'
                .then(done => {
                    if (done) {
                        logger.info(`Successful authentication process for user ${process.env.RW_EMAIL}`);
                        continueTest = true;
                    }
                }).catch(ex => {
                    logger.error(`Unsuccessful authentication process for user ${process.env.RW_EMAIL}. Test aborted`);
                    browser.close();
                    continueTest = false;
                })
        }
        return continueTest;
    }

    static async wait(milliseconds: number): Promise<void> {
        await page.waitFor(milliseconds)
    }

    async openModule(timeout?: number, sleepafteropening?: number): Promise<void> {
        if (!timeout) {
            timeout = 5000;
        }
        //await this.delay(1000);
        //await page.waitFor(750)
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
                    logger.info(`Opened ${this.moduleCaption} module`);
                } else {
                    logger.error(`Error opening ${this.moduleCaption} module`);
                    await browser.close();
                }
            })
        if (sleepafteropening > 0) {
            await TestUtils.sleepAsync(sleepafteropening);  // wait x seconds to allow other queries to complete
        }
    }

    async openRecord(index: number, sleepafteropening?: number): Promise<void> {
        await page.waitForSelector(`.fwbrowse tbody tr.viewmode:nth-child(${index})`);
        await page.click('.fwbrowse tbody tr.viewmode', { clickCount: 2 });
        if (sleepafteropening > 0) {
            await TestUtils.sleepAsync(sleepafteropening);
        }
    }

    async createNewRecord(count: number): Promise<void> {
        await page.waitForSelector(`.fwbrowse tbody`);
        await page.click('.addnewtab i.material-icons', { clickCount: count });
        await page.waitForSelector(`.fwform`)
            .then(async done => {
                const formCaption = await page.evaluate(() => {
                    const caption = jQuery('body').find('div.tab.active').attr('data-caption');
                    return caption;
                })
                if (formCaption !== '') {
                    logger.info(`New ${this.moduleCaption} Created`);
                } else {
                    logger.error(`New ${this.moduleCaption} not Created`);
                    await browser.close();
                }
            })
    }

    async clearInputField(dataField: string): Promise<void> {
        const elementHandle = await page.$(`.fwformfield[data-datafield="${dataField}"] input`);
        await elementHandle.click();
        await elementHandle.focus();
        // click three times to select all
        await elementHandle.click({ clickCount: 3 });
        await elementHandle.press('Backspace');
    }

    async clickTab(tabCaption: string): Promise<void> {
        await page.click(`.fwform .tabs [data-caption="${tabCaption}"]`);
    }

    async populateTextField(dataField: string, value: string): Promise<void> {
        if (value === '') {
            await this.clearInputField(dataField);
        }
        else {
            await page.type(`.fwformfield[data-datafield="${dataField}"] input`, value);
        }
    }

    async populateValidationTextField(dataField: string, value: string): Promise<void> {
        await page.type(`.fwformfield[data-datafield="${dataField}"] .fwformfield-text`, value);
        await page.keyboard.press('Enter');
    }

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
        //await page.waitFor(1000);
        await ModuleBase.wait(1000);
        await page.click(`${grid} .buttonbar [data-type="NewButton"] i`); // add new row
        await page.waitForSelector(`${grid} tbody tr`);
        async function fillValues() {
            if (fieldObject !== null && (typeof fieldObject === 'object' && !Array.isArray(fieldObject))) {

                for (let key in fieldObject) {
                    //// differentiate b/w datafield inputs and validations - structure is different
                    // console.log('selector', `${grid} .tablewrapper table tbody tr td div[data-browsedatafield="${key}"] input.text`)
                    page.type(`${grid} .tablewrapper table tbody tr td div[data-browsedatafield="${key}"] input.text`, fieldObject[key])
                }
                //await page.waitFor(1500); //  need better wait that all values have been assigned
                await ModuleBase.wait(1500);

                await page.keyboard.press('Enter');
                //await page.waitFor(1000);
                await ModuleBase.wait(1000);
                await page.click(`${grid} .tablewrapper table tbody tr td div.divsaverow i`);
                //await page.waitFor(3000);
                await ModuleBase.wait(3000);
            }
        }
        await fillValues();
        //await page.waitFor(3000)
    }

    async saveRecord(): Promise<void> {
        let continueTest;
        await page.click('.btn[data-type="SaveMenuBarButton"]');
        await page.waitForSelector('.advisory');
        //await page.screenshot({ path: 'PageAfterVendorSave.png', fullPage: true });
        await page.waitForFunction(() => document.querySelector('.advisory'), { polling: 'mutation' })
            .then(async done => {
                const afterSaveMsg = await page.evaluate(() => {
                    const messageText = jQuery('body').find('.advisory').text();
                    return messageText;
                })
                if (afterSaveMsg.includes('saved')) {
                    logger.info(`${this.moduleCaption} Record saved: ${afterSaveMsg}`);
                    continueTest = true;
                } else if (afterSaveMsg.includes('Error') || afterSaveMsg.includes('resolve')) {
                    logger.error(`${this.moduleCaption} Record not saved: ${afterSaveMsg}`);
                    continueTest = false;
                }
            })
        return continueTest;
    }

    async closeRecord(): Promise<void> {
        await page.click('div.delete');
        //await page.waitForNavigation();
    }

    static async logoff(): Promise<void> {
        //let baseUrl: string = "http://localhost/rentalworksweb";
        //await page.goto(`${baseUrl}/#/logoff`);
        //await page.waitForSelector(`.programlogo`);

        await page.click('.usermenu');
        await page.waitForSelector('#master-header > div > div.user-controls > div > div.user-dropdown > div.menuitems > div:nth-child(2)');
        await page.click('#master-header > div > div.user-controls > div > div.user-dropdown > div.menuitems > div:nth-child(2)');
        await page.waitForSelector(`.programlogo`);

    }

    static async emailResults(): Promise<void> {
        let buffer = '';
        const combinedLog = fs.createReadStream("./combined.log", "utf8");
        combinedLog.on('data', chunk => {
            buffer += chunk;
        });
        combinedLog.on('end', () => {
            const result = buffer;
            console.log('result:', result);
            //  console.log('TYPEOF:', typeof result);
            //  result = result.toString();
            sgMail.setApiKey(process.env.SENDGRID_API_KEY);
            const msg = {
                to: 'joshpace@gmail.com',
                from: 'jpace@4wall.com',
                subject: 'SendGrid Sending logfile',
                text: 'result',
                html: '<strong>and easy to do anywhere, even with Node.js</strong>',
                attachments: [
                    {
                        content: 'Some base 64 encoded attachment content',
                        filename: 'some-attachment.txt',
                        type: 'plain/text',
                        disposition: 'attachment',
                        contentId: 'mytext'
                    },
                ],
            };
            sgMail.send(msg)
                .then((res) => console.log('RES: ', res))
                .catch((err) => console.log('ERR: ', err))
        });
    }
}