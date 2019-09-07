import { Logging } from '../shared/Logging';
import faker from 'faker';

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
        //const date = new Date();
        //const hours = date.getHours();
        //const minutes = date.getMinutes();
        //const seconds = date.getSeconds();
        //const dateTimeToken = `${date.toLocaleDateString().replace(/\//g, '')}${hours}${minutes}${seconds}`;
        const theDate = new Date();
        const year = theDate.getFullYear().toString();
        const month = (theDate.getMonth()+1).toString();
        const date = theDate.getDate().toString();
        const hours = theDate.getHours().toString();
        const minutes = theDate.getMinutes().toString();
        const seconds = theDate.getSeconds().toString();
        const dateTimeToken = year.padStart(4, '0') + month.padStart(2, '0') + date.padStart(2, '0') + hours.padStart(2, '0') + minutes.padStart(2, '0') + seconds.padStart(2, '0');;
        return dateTimeToken;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static getTestToken(): string {
        let testToken = this.getDateTimeToken();
        Logging.logger.info(`Test Token: ${testToken}`);
        return testToken;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomAlphanumeric(length: number): string {
        return faker.random.alphaNumeric(length);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomNumeric(length: number): string {
        let symbol: string = "#";
        let format: string = symbol.repeat(length);
        return faker.helpers.replaceSymbolWithNumber(format, symbol);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomFirstName(): string {
        return faker.name.firstName();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomLastName(): string {
        return faker.name.lastName();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomEmail(): string {
        return faker.internet.email();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomUrl(): string {
        return faker.internet.url();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomAddress1(): string {
        return faker.address.streetAddress();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomAddress2(): string {
        return faker.address.secondaryAddress();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomStreetName(): string {
        return faker.address.streetName();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomCity(): string {
        return faker.address.city();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomState(): string {
        return faker.address.state();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomStateCode(): string {
        return faker.address.stateAbbr();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomZipCode5(): string {
        return faker.address.zipCode("#####");
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomZipCode(): string {
        return this.randomZipCode5();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomZipCode9(): string {
        return faker.address.zipCode("#####-####");
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomPhone(): string {
        //return faker.phone.phoneNumber();
        let phone: string = faker.phone.phoneNumberFormat(0);
        phone = phone.split('-').join("");  // remove the dashes, we just want the numbers
        return phone;
        /*
        faker.phone.phoneNumberFormat(format);
        Formats:
        0 = "587-753-7028
        1 = "(116) 239-1938
        2 = "1-878-758-7353
        3 = "343.578.4788
        */
    }
    //-----------------------------------------------------------------------------------------------------------------
    static formattedPhone(phone: string): string {  //phone should be just numbers like 7142038800, returns (714) 203-8800
        let formattedPhone: string = "";
        //formattedPhone = '(' + phone.substring(0, 3) + ') ' + phone.substring(3, 6) + '-' + phone.substring(6); 
        formattedPhone = '(' + phone.substr(0, 3) + ') ' + phone.substr(3, 3) + '-' + phone.substr(6);
        return formattedPhone;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomPhoneExtension(): string {
        return this.randomNumeric(4);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomCompanyName(): string {
        return faker.company.companyName();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomJobTitle(): string {
        return faker.name.jobTitle();
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomProductName(): string {
        return faker.commerce.productName();
    }
    //-----------------------------------------------------------------------------------------------------------------
}
