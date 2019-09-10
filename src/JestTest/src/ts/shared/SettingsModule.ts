import { ModuleBase } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from './TestUtils';

//---------------------------------------------------------------------------------------
export class SettingsModule extends ModuleBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
    }
    //---------------------------------------------------------------------------------------
    async openBrowse(timeout?: number, sleepafteropening?: number): Promise<void> {
        if (!timeout) {
            timeout = 3000;  //if we can't find the settings module header bar on the Settings Page within 3 seconds, then timeout the test
        }

        let settingsGearSelector = `i.material-icons.dashboard.systembarcontrol[title="Settings"]`;
        await page.waitForSelector(settingsGearSelector);
        await page.click(settingsGearSelector);

        let moduleHeadingSelector = `.panel-group[id="${this.moduleName}"]`;
        await page.waitForSelector(moduleHeadingSelector, { timeout: timeout });
        await page.click(moduleHeadingSelector);

        let moduleLegendBarSelector = `.panel-group[id="${this.moduleName}"] .legend`;
        await page.waitForSelector(moduleLegendBarSelector)
            .then(async done => {
                Logging.logInfo(`Opened ${this.moduleCaption} module`);
            })
        if (sleepafteropening > 0) {
            await TestUtils.sleepAsync(sleepafteropening);  // wait x seconds to allow other queries to complete
        }
    }
    //---------------------------------------------------------------------------------------
    async openRecord(index?: number, sleepAfterOpening?: number): Promise<void> {
        if (index == undefined) {
            index = 1;
        }

        //<tr tabindex="0" class="viewmode selected" > <td class="column" data - visible="false" style = "display:none;" > <div class="field" data - isuniqueid="true" data - browsedatatype="key" data - sort="off" data - formreadonly="true" data - browsedatafield="DealId" data - formdatafield="DealId" data - cssclass="DealId" data - originalvalue="F003RPX5" > F003RPX5 < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Deal" data - browsedatatype="text" data - sort="asc" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="Deal" data - formdatafield="Deal" data - cssclass="Deal" data - originalvalue="02 CREATIVE SOLUTONS" > 02 CREATIVE SOLUTONS < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Deal Number" data - browsedatatype="text" data - sort="off" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="DealNumber" data - formdatafield="DealNumber" data - cssclass="DealNumber" data - originalvalue="L12309" > L12309 < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Deal Type" data - browsedatatype="text" data - sort="off" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="DealType" data - formdatafield="DealType" data - cssclass="DealType" data - originalvalue="INDUSTRIAL/PROD" > INDUSTRIAL / PROD < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Deal Status" data - browsedatatype="text" data - sort="off" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="DealStatus" data - formdatafield="DealStatus" data - cssclass="DealStatus" data - originalvalue="OPEN" > OPEN < /div></td > <td class="column" data - visible="true" style = "" > <div class="field" data - caption="Customer" data - browsedatatype="text" data - sort="off" data - isuniqueid="false" data - formreadonly="true" data - browsedatafield="Customer" data - formdatafield="Customer" data - cssclass="Customer" data - originalvalue="02 CREATIVE SOLUTONS" > 02 CREATIVE SOLUTONS < /div></td > <td class="column" data - visible="true" style = "" > </td></tr >
        //await page.waitForSelector(`.fwbrowse tbody tr.viewmode:nth-child(${index})`);

        //<div class="panel-record" id = "C0016A6Z" > <div class="panel panel-info container-fluid" > <div class="row-heading" > <i class="material-icons record-selector" > keyboard_arrow_down < /i>      <div style="width:100%;padding-left: inherit;">        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">          <label style="font-weight:800;">Ship Via</label > </div>    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">    <label data-datafield="ShipVia" style="color:#31708f">1ST PRIORITY OVN</label > </div>      </div > <div style="width:100%;padding-left: inherit;" > <div class="fwcontrol fwcontainer fwform-fieldrow" data - type="fieldrow" > <label style="font-weight:800;" > Carrier < /label>        </div > <div class="fwcontrol fwcontainer fwform-fieldrow" data - type="fieldrow" > <label data - datafield="Vendor" style = "color:#31708f" > FEDERAL EXPRESS < /label>        </div > </div>    </div > </div>  <div class="panel-body data-panel" style="display:none;" id="C0016A6Z" data-type="settings-row"></div > </div>
        //await page.waitForSelector(`.panel-group[id="${this.moduleName}"].panel-primary.panel-collapse.panel-body.panel-record:nth-child(${index})`);

        //let records = await page.$$eval(`.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record`, (e: any) => { return e.children; });
        //let records = await page.$$eval(`.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body`, (e: any) => { return e.children; });
        //Logging.logger.info(`records: ${JSON.stringify(records)}`);
        //let elementHandle = records[0];
        //await page.click(elementHandle, { clickCount: 1 });

        let selector = `.panel-group[id="${this.moduleName}"] .panel-primary .panel-collapse .panel-body .panel-record`;  // this only works for the first record
        await page.waitForSelector(selector);
        await page.click(selector);
        await ModuleBase.wait(300); // let the row render

        if (sleepAfterOpening > 0) {
            await ModuleBase.wait(sleepAfterOpening);
        }
    }
    //---------------------------------------------------------------------------------------
}