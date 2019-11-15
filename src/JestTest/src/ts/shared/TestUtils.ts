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
    static globalScopeRef = GlobalScope;
    //-----------------------------------------------------------------------------------------------------------------
    static async authenticate(): Promise<LoginResponse> {
        let loginReponse: LoginResponse = new LoginResponse();
        loginReponse.success = false;
        loginReponse.errorMessage = "could not login";
        let myAccount: any = TestUtils.globalScopeRef["User~ME"];  // check GlobalScope for myAccount to use (can be used to re-log during the middle of a test)
        let baseUrl: string = process.env.RW_URL;  //ie http://localhost/rentalworksweb
        let login = process.env.RW_LOGIN;
        let password = process.env.RW_PASSWORD;

        if (myAccount != undefined) {  // if myAccount was established, then use it
            login = myAccount.LoginName;
            password = myAccount.Password;
        }
        else {
            await page.goto(`${baseUrl}/#/login`);
            await page.waitForNavigation({timeout: 120000});
        }

        let selector = `.btnLogin`;
        await page.waitForSelector(selector, { visible: true });
        await page.click(selector);

        selector = `#email`;
        let loginFieldHandle = await page.$(selector);
        await loginFieldHandle.click();
        await loginFieldHandle.focus();
        // click three times to select all
        await loginFieldHandle.click({ clickCount: 3 });
        await loginFieldHandle.press('Backspace');
        await page.keyboard.sendCharacter(<string>login);

        selector = `#password`;
        let passwordFieldHandle = await page.$(selector);
        await passwordFieldHandle.click();
        await passwordFieldHandle.focus();
        // click three times to select all
        await passwordFieldHandle.click({ clickCount: 3 });
        await passwordFieldHandle.press('Backspace');
        await page.keyboard.sendCharacter(<string>password);

        selector = `.btnLogin`;
        await page.waitForSelector(selector, { visible: true });
        await page.click(selector);

        selector = `div.fwoverlay`;
        await page.waitForSelector(selector, { visible: true, timeout: 120000 });

        await page.waitForFunction(() => !document.querySelector(`div.fwoverlay`), { polling: 'mutation', timeout: 120000 })
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

                    await page.waitForSelector('.appmenu', { visible: true, timeout: 120000 }) // Upper left 'hamburger menu'
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
                    await TestUtils.sleepAsync(1250);  // wait for the menu items to get built and get click all events 
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
        await page.waitForSelector(selector, { visible: true });
        await page.click(selector);

        selector = `#master-header > div > div.user-controls > div > div.user-dropdown > div.menuitems > div:nth-child(2)`;
        await page.waitForSelector(selector, { visible: true });
        await page.click(selector);

        selector = `.programlogo`;
        await page.waitForSelector(selector, { visible: true });

        logoutResponse.success = true;

        return logoutResponse;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static async sleepAsync(timeout: number): Promise<void> {
        //return new Promise<void>((resolve, reject) => {
        //    try {
        //        setTimeout(() => {
        //            resolve();
        //        }, timeout);
        //    } catch (ex) {
        //        reject(ex);
        //    }
        //});
        await page.waitFor(timeout);
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
        const random1 = this.randomAlphanumeric(1).toUpperCase();
        const dateTimeToken = year.padStart(4, '0') + month.padStart(2, '0') + date.padStart(2, '0') + hours.padStart(2, '0') + minutes.padStart(2, '0') + seconds.padStart(2, '0') + random1;
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
        if (valueIn.toString().toUpperCase().includes("GLOBALSCOPE.")) {
            //example1: "GlobalScope.DefaultSettings~1.DefaultUnit",
            //example2: "Product124 GlobalScope.DefaultSettings~1.DefaultUnit",
            //example2: "GlobalScope.DefaultSettings~1.DefaultUnit SomeOtherValue",

            let globalScopeKeyString = "";
            let rightOfGlobalScopeKeyString = valueIn.substring(valueIn.toUpperCase().indexOf("GLOBALSCOPE."));
            let endOfOfGlobalScopeKeyString = rightOfGlobalScopeKeyString.indexOf(" ");
            if (endOfOfGlobalScopeKeyString < 0) {
                endOfOfGlobalScopeKeyString = rightOfGlobalScopeKeyString.indexOf("_");
            }
            if (endOfOfGlobalScopeKeyString >= 0) {   // string contains a space or underscore after the GlobalScope key
                globalScopeKeyString = valueIn.substring(valueIn.toUpperCase().indexOf("GLOBALSCOPE."), valueIn.toUpperCase().indexOf("GLOBALSCOPE.") + endOfOfGlobalScopeKeyString);
            }
            else {
                globalScopeKeyString = valueIn.substring(valueIn.toUpperCase().indexOf("GLOBALSCOPE."));
            }

            const placeHolderString = '!x!x!x!x!x!';
            valueIn = valueIn.replace(globalScopeKeyString, placeHolderString);
            let globalScopeKey = globalScopeKeyString.toString().split('.');
            let globalScopeKeyPart1 = globalScopeKey[1].toString();
            let globalScopeKeyPart2 = globalScopeKey[2].toString();

            //let globalScopeValue = globalScope[globalScopeKeyPart1][globalScopeKeyPart2];

            let globalScopeObject = globalScope[Object.keys(globalScope).find(key => key.toLowerCase() === globalScopeKeyPart1.toLowerCase())];
            let globalScopeValue = globalScopeObject[Object.keys(globalScopeObject).find(key => key.toLowerCase() === globalScopeKeyPart2.toLowerCase())];
            valueOut = valueIn.replace(placeHolderString, globalScopeValue);
        }
        return valueOut;
    }
    //---------------------------------------------------------------------------------------
    static async waitForPleaseWait(timeout?: number) {
        if (timeout == undefined) {
            timeout = 30000;
        }

        try {
            await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 5000 });
        } catch (error) { } // assume that we missed the Please Wait dialog
        await page.waitFor(() => !document.querySelector('.pleasewait'), { timeout: timeout });
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
    static randomIntegerBetween(minValue: number, maxValue: number): number {
        return faker.random.number({ min: minValue, max: maxValue });
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
    static randomLoremSentences(sentenceCount?: number): string {
        return faker.lorem.sentences(sentenceCount);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomLoremWords(wordCount?: number): string {
        return faker.lorem.words(wordCount)
    }
    //-----------------------------------------------------------------------------------------------------------------
    static dateMDY(theDate: Date, separator: string = "/"): string {
        const year = theDate.getFullYear().toString();
        const month = (theDate.getMonth() + 1).toString();
        const date = theDate.getDate().toString();
        const recentDateStr = month.padStart(2, '0') + separator + date.padStart(2, '0') + separator + year.padStart(4, '0');
        return recentDateStr;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomRecentDateMDY(withinNumberOfDays?: number, separator: string = "/"): string {
        let recentDate = faker.date.recent(withinNumberOfDays);
        return TestUtils.dateMDY(recentDate, separator);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static randomFutureDateMDY(withinNumberOfDays?: number, separator: string = "/"): string {
        let minusOne: number = -1;
        let daysAhead: number = (withinNumberOfDays * minusOne);
        let futureDate = faker.date.recent(daysAhead);
        return TestUtils.dateMDY(futureDate, separator);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static futureDate(numberOfDays: number = 0): Date {
        let today: Date = new Date();
        let futureDate: Date = new Date(today.getTime() + (numberOfDays * 1000 * 60 * 60 * 24));
        return futureDate;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static futureDateMDY(numberOfDays: number = 0, separator: string = "/"): string {
        return TestUtils.dateMDY(TestUtils.futureDate(numberOfDays), separator);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static pastDate(numberOfDays: number = 0): Date {
        let today: Date = new Date();
        let minusOne: number = -1;
        let daysPast: number = (numberOfDays * minusOne);
        let pastDate: Date = new Date(today.getTime() + (daysPast * 1000 * 60 * 60 * 24));
        return pastDate;
    }
    //-----------------------------------------------------------------------------------------------------------------
    static pastDateMDY(numberOfDays: number = 0, separator: string = "/"): string {
        let today: Date = new Date();
        let minusOne: number = -1;
        let daysPast: number = (numberOfDays * minusOne);
        let pastDate: Date = new Date(today.getTime() + (daysPast * 1000 * 60 * 60 * 24));
        return TestUtils.dateMDY(TestUtils.pastDate(numberOfDays), separator);
    }
    //-----------------------------------------------------------------------------------------------------------------
}
