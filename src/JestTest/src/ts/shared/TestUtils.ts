import { Logging } from '../shared/Logging';

export class TestUtils {
    //-----------------------------------------------------------------------------------------------------------------
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
                    Logging.logger.error(`Error in login: ${evaluateLogin}`);
                    await browser.close(); // abort test by closing window. Probably a better way
                    continueTest = false;
                } else if (evaluateLogin === '') {
                    Logging.logger.info(`Successful login for user ${process.env.RW_EMAIL}`);
                    continueTest = true;
                }
            })
        if (continueTest) {
            await page.waitForSelector('.appmenu', { visible: true, timeout: 20000 }) // Upper left 'hamburger menu'
                .then(done => {
                    if (done) {
                        Logging.logger.info(`Successful authentication process for user ${process.env.RW_EMAIL}`);
                        continueTest = true;
                    }
                }).catch(ex => {
                    Logging.logger.error(`Unsuccessful authentication process for user ${process.env.RW_EMAIL}. Test aborted`);
                    browser.close();
                    continueTest = false;
                })
        }
        await TestUtils.sleepAsync(750);
        return continueTest;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static async logoff(): Promise<void> {
        await page.click('.usermenu');
        await page.waitForSelector('#master-header > div > div.user-controls > div > div.user-dropdown > div.menuitems > div:nth-child(2)');
        await page.click('#master-header > div > div.user-controls > div > div.user-dropdown > div.menuitems > div:nth-child(2)');
        await page.waitForSelector(`.programlogo`);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static async sleepAsync(timeout: number): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            try {
                setTimeout(() => {
                    resolve();
                }, timeout);
            } catch (ex) {
                reject(ex);
            }
        });
    }
    //-----------------------------------------------------------------------------------------------------------------
    static getDateTimeToken(): string {
        const date = new Date();
        const hours = date.getHours();
        const minutes = date.getMinutes();
        const seconds = date.getSeconds();
        const dateTimeToken = `${date.toLocaleDateString().replace(/\//g, '')}${hours}${minutes}${seconds}`;
        return dateTimeToken;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static getTestToken(): string {
        return this.getDateTimeToken();
    }
    //-----------------------------------------------------------------------------------------------------------------
}