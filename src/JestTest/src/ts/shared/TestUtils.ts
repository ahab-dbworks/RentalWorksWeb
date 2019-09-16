import { Logging } from '../shared/Logging';
import faker from 'faker';
import { GlobalScope } from '../shared/GlobalScope';

export class LoginResponse {
    success: boolean;
    errorMessage: string;
}

export class LogoutResponse {
    success: boolean;
}

export class TestUtils {

    //-----------------------------------------------------------------------------------------------------------------
    static async authenticate(): Promise<LoginResponse> {
        let loginReponse: LoginResponse = new LoginResponse();
        loginReponse.success = false;
        loginReponse.errorMessage = "could not login";

        let baseUrl: string = process.env.RW_URL;  //ie http://localhost/rentalworksweb
        let login = process.env.RW_LOGIN;
        let password = process.env.RW_PASSWORD;

        await page.goto(`${baseUrl}/#/login`);
        await page.waitForNavigation();

        let selector = `.btnLogin`;
        await page.waitForSelector(selector, { visible: true });
        await page.click(selector);

        selector = `#email`;
        await page.waitForSelector('#email', { visible: true });
        await page.type('#email', <string>login);

        selector = `#password`;
        await page.click(selector);
        await page.type(selector, <string>password);

        selector = `.btnLogin`;
        await page.waitForSelector(selector, { visible: true });
        await page.click(selector);

        selector = `div.fwoverlay`;
        await page.waitForSelector(selector, { visible: true, timeout: 20000 });

        await page.waitForFunction(() => !document.querySelector(`div.fwoverlay`), { polling: 'mutation' })
            .then(async done => {
                const errorMessage = await page.evaluate(() => {
                    const errorMessageText = jQuery('body').find('.errormessage').text();
                    return errorMessageText;
                })
                if (errorMessage.includes('Invalid')) {
                    loginReponse.errorMessage = errorMessage;
                    Logging.logError(`Unsuccessful authentication for user ${login}: ${errorMessage}`);
                    //await browser.close(); // abort test by closing window. Probably a better way
                    //justin 09/12/2019 - I would like to avoid browser.close() here because it kills the test without producing a report
                } else if (errorMessage === '') {
                    Logging.logInfo(`Successful authentication for user ${login}`);

                    await page.waitForSelector('.appmenu', { visible: true, timeout: 20000 }) // Upper left 'hamburger menu'
                        .then(done => {
                            if (done) {
                                Logging.logInfo(`Successful login process for user ${login}`);
                                loginReponse.success = true;
                                loginReponse.errorMessage = "";
                            }
                        })
                        .catch(ex => {
                            Logging.logError(`Unsuccessful login process for user ${login}. Could not reach application main menu.`);
                            loginReponse.errorMessage = 'cannot find main menu button';
                            //browser.close();
                            //justin 09/12/2019 - I would like to avoid browser.close() here because it kills the test without producing a report
                        })
                    await TestUtils.sleepAsync(750);
                }
            })
            .catch(ex => {
                Logging.logError(`Login Error: ${ex}`);
            })

        return loginReponse;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static async logoff(): Promise<LogoutResponse> {
        let logoutResponse: LogoutResponse = new LogoutResponse();
        logoutResponse.success = false;

        let selector = `.usermenu`;
        await page.waitForSelector(selector, { timeout: 1000 });
        await page.click(selector);

        selector = `#master-header > div > div.user-controls > div > div.user-dropdown > div.menuitems > div:nth-child(2)`;
        await page.waitForSelector(selector, { timeout: 1000 });
        await page.click(selector);

        selector = `.programlogo`;
        await page.waitForSelector(selector);

        logoutResponse.success = true;

        return logoutResponse;
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
        const theDate = new Date();
        const year = theDate.getFullYear().toString();
        const month = (theDate.getMonth() + 1).toString();
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
        Logging.logInfo(`Test Token: ${testToken}`);
        return testToken;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static getGlobalScopeValue(valueIn: string, globalScope: GlobalScope): string {
        let valueOut: string = valueIn;
        if (valueIn.toString().includes("GlobalScope.")) {
            //example1: "GlobalScope.DefaultSettings~1.DefaultUnit",
            //example2: "Product124 GlobalScope.DefaultSettings~1.DefaultUnit",
            //example2: "GlobalScope.DefaultSettings~1.DefaultUnit SomeOtherValue",

            let globalScopeKeyString = "";
            let rightOfGlobalScopeKeyString = valueIn.substring(valueIn.indexOf("GlobalScope."));
            let endOfOfGlobalScopeKeyString = rightOfGlobalScopeKeyString.indexOf(" ");
            if (endOfOfGlobalScopeKeyString >= 0) {   // string contains a space after the GlobalScope key
                globalScopeKeyString = valueIn.substring(valueIn.indexOf("GlobalScope."), valueIn.indexOf("GlobalScope.") + endOfOfGlobalScopeKeyString);
            }
            else {
                globalScopeKeyString = valueIn.substring(valueIn.indexOf("GlobalScope."));
            }
            valueIn = valueIn.replace(globalScopeKeyString, '!!!!!!!!');
            let globalScopeKey = globalScopeKeyString.toString().split('.');
            let globalScopeValue = globalScope[globalScopeKey[1].toString()][globalScopeKey[2].toString()];
            valueOut = valueIn.replace('!!!!!!!!', globalScopeValue);
        }
        return valueOut;
    }
    //---------------------------------------------------------------------------------------
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
