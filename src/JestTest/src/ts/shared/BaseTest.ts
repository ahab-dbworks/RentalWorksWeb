import { FwBaseTest } from '../fwjest/FwBaseTest';
import { TestUtils } from './TestUtils';
import { Logging } from './Logging';

export abstract class BaseTest extends FwBaseTest {
    //---------------------------------------------------------------------------------------
    //note, this method needs to be called within a "test" from the calling scope
    async ChangeOfficeWarehouse(toOffice: string, toWarehouse: string) {

        if (toOffice.toString().toUpperCase().includes("GLOBALSCOPE.")) {
            toOffice = TestUtils.getGlobalScopeValue(toOffice, this.globalScopeRef);
        }
        if (toWarehouse.toString().toUpperCase().includes("GLOBALSCOPE.")) {
            toWarehouse = TestUtils.getGlobalScopeValue(toWarehouse, this.globalScopeRef);
        }

        Logging.logInfo(`Attempting to change Office/Warehouse to ${toOffice} / ${toWarehouse}`);

        //let officeWarehouseSelector = `div.systembarcontrol[data-id="officelocation"]`;
        //await page.waitForSelector(officeWarehouseSelector);
        ////await TestUtils.sleepAsync(5000);  // arbitrary wait to allow this control to get its click event. only necessary when testing this method in rapid succession
        //await page.click(officeWarehouseSelector);

        let officeWarehouseSelector = `div.officelocation.usercontrol`;
        TestUtils.waitForAndClick(officeWarehouseSelector);

        await page.waitForSelector('.advisory', { timeout: 5000 });

        const officeElementHandle = await page.$(`.fwconfirmation .fwformfield[data-datafield="OfficeLocationId"] input.fwformfield-text`);
        await officeElementHandle.click();
        await officeElementHandle.focus();
        await officeElementHandle.click({ clickCount: 3 });
        await officeElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(toOffice);
        await page.keyboard.press('Enter');
        await TestUtils.sleepAsync(500);

        const warehouseElementHandle = await page.$(`.fwconfirmation .fwformfield[data-datafield="WarehouseId"] input.fwformfield-text`);
        await warehouseElementHandle.click();
        await warehouseElementHandle.focus();
        await warehouseElementHandle.click({ clickCount: 3 });
        await warehouseElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(toWarehouse);
        await page.keyboard.press('Enter');
        await TestUtils.sleepAsync(500);

        const options = await page.$$(`.advisory .fwconfirmation-button`);
        await options[0].click(); // click the "Select" button
        await TestUtils.waitForPleaseWait();

        await TestUtils.sleepAsync(8000);  // let the page start to reload

        //let selector = `div.systembarcontrol[data-id="officelocation"] .value`;
        let selector = `div.officelocation.usercontrol .value`;
        await page.waitForSelector(selector);
        const element = await page.$(selector);
        const officeLocation = await page.evaluate(element => element.textContent, element);
        let expectedOfficeLocation = toOffice;
        if (toOffice != toWarehouse) {
            expectedOfficeLocation = toOffice + ' / ' + toWarehouse;
        }
        Logging.logInfo(`Validating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
        expect(officeLocation).toBe(expectedOfficeLocation);

    }
    //---------------------------------------------------------------------------------------
}
