import { FwLogging } from './FwLogging';
import { FwTestUtils } from './FwTestUtils';
import { FwGlobalScope } from './FwGlobalScope';
import { FwGridBase } from './FwGridBase';

export class FwOpenModuleResponse {
    opened: boolean;
    errorMessage: string;
}

//---------------------------------------------------------------------------------------
export class FwFrontEndBase {
    moduleName: string;
    moduleId: string;
    moduleGroupName: string;
    moduleCaption: string;
    grids?: FwGridBase[];
    globalScopeRef = FwGlobalScope;

    moduleOpenTimeout: number = 20000;
    waitAfterOpeningModuleToAllowOtherQueries: number = 500;

    //---------------------------------------------------------------------------------------
    constructor() {
        this.moduleName = 'UnknownModule';
        this.moduleId = '99999999-9999-9999-9999-999999999999';
        this.moduleCaption = 'UnknownModule';
        this.grids = new Array();
    }
    //---------------------------------------------------------------------------------------
    getFormMenuSelector(): string {
        return `div .fwform-menu .submenubutton i`;
    }
    //---------------------------------------------------------------------------------------
    async clickMenuWithConfirmation(securityId: string) {
        let moduleMenuSelector: string = this.getFormMenuSelector();
        let functionMenuSelector = `div .fwform [data-securityid="${securityId}"]`;
        await page.waitForSelector(moduleMenuSelector);
        await page.click(moduleMenuSelector);
        await page.waitForSelector(functionMenuSelector);
        await page.click(functionMenuSelector);
        await page.waitForSelector('.advisory');
        const options = await page.$$('.advisory .fwconfirmation-button');
        await options[0].click();
        await FwTestUtils.waitForPleaseWait();
        try {
            let toasterCloseSelector = `.advisory .messageclose`;
            await page.waitForSelector(toasterCloseSelector, { timeout: 2000 });
            await page.click(toasterCloseSelector);
            await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
        } catch (error) { } // assume that we missed the toaster
    }
    //---------------------------------------------------------------------------------------
    static async wait(milliseconds: number): Promise<void> {
        await page.waitFor(milliseconds);
    }
    //---------------------------------------------------------------------------------------
    async openModule(): Promise<FwOpenModuleResponse> {
        let openModuleResponse: FwOpenModuleResponse = new FwOpenModuleResponse();
        openModuleResponse.opened = false;
        openModuleResponse.errorMessage = "module not opened";

        //let mainMenuSelector = `.appmenu`;
        //await page.waitForSelector(mainMenuSelector);
        //
        //await page.click(mainMenuSelector);
        //let menuButtonId = '#btnModule' + this.moduleId;
        //await expect(page).toClick(menuButtonId);


        //let mainMenuSelector = `.app-menu-button`;
        ////await page.waitForSelector(mainMenuSelector);
        ////await page.click(mainMenuSelector);
        //FwTestUtils.waitForAndClick(mainMenuSelector, 0, 2000);

        let menuGroupSelector = `i[title="${this.moduleGroupName}"]`;
        //await page.waitForSelector(menuGroupSelector);
        //await expect(page).toClick(menuGroupSelector);
        FwTestUtils.waitForAndClick(menuGroupSelector, 0, 2000);

        let menuItemSelector = `div[data-securityid="${this.moduleId}"]`;
        //await page.waitForSelector(menuItemSelector);
        //await expect(page).toClick(menuItemSelector);
        FwTestUtils.waitForAndClick(menuItemSelector);

        // wait for the module to open and load
        //await page.waitFor(() => document.querySelector('.pleasewait'));
        try {
            await page.waitFor(() => document.querySelector('.pleasewait'), { timeout: 5000 });
        } catch (error) { } // assume that we missed the Please Wait dialog
        await page.waitFor(() => !document.querySelector('.pleasewait'), { timeout: this.moduleOpenTimeout });

        // find the browse tab
        let moduleTabSelector = `div.tab.active[data-tabtype="FORM"]`;
        await page.waitForSelector(moduleTabSelector);

        // make sure that we are getting the module we want
        let moduleTabCaptionSelector = moduleTabSelector + ` div.caption`;
        const moduleTabCaption = await page.$eval(moduleTabCaptionSelector, el => el.textContent);
        while (moduleTabCaption !== this.moduleCaption) {
            await FwFrontEndBase.wait(100);
        }

        // wait for the module to try to open, then check for errors
        var popUp;
        try {
            popUp = await page.waitForSelector('.advisory', { timeout: 1500 });
        } catch (error) { } // no error pop-up

        if (popUp !== undefined) {
            let errorMessage = await page.$eval('.advisory', el => el.textContent);
            openModuleResponse.opened = false;
            openModuleResponse.errorMessage = errorMessage;

            FwLogging.logError(`Error opening ${this.moduleCaption} browse: ` + errorMessage);

            const options = await page.$$('.advisory .fwconfirmation-button');
            await options[0].click() // click "OK" option
                .then(() => {
                    FwLogging.logInfo(`Clicked the "OK" button.`);
                })
        }
        else {
            openModuleResponse.opened = true;
            openModuleResponse.errorMessage = "";

            if (this.waitAfterOpeningModuleToAllowOtherQueries > 0) {
                await FwFrontEndBase.wait(this.waitAfterOpeningModuleToAllowOtherQueries);
            }
        }
        return openModuleResponse;
    }
    //---------------------------------------------------------------------------------------
}