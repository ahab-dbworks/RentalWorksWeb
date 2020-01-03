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
    static async wait(milliseconds: number): Promise<void> {
        await page.waitFor(milliseconds);
    }
    //---------------------------------------------------------------------------------------
    async openModule(): Promise<FwOpenModuleResponse> {
        let openModuleResponse: FwOpenModuleResponse = new FwOpenModuleResponse();
        openModuleResponse.opened = false;
        openModuleResponse.errorMessage = "module not opened";

        let mainMenuSelector = `.appmenu`;
        await page.waitForSelector(mainMenuSelector);

        await page.click(mainMenuSelector);
        let menuButtonId = '#btnModule' + this.moduleId;
        await expect(page).toClick(menuButtonId);

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