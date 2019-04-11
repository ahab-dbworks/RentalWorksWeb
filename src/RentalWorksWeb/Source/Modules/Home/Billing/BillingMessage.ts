routes.push({ pattern: /^module\/billingmessage$/, action: function (match: RegExpExecArray) { return BillingMessageController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class BillingMessage {
    Module: string = 'BillingMessage';
    apiurl: string = 'api/v1/billingmessage';
    caption: string = 'Billing Message';
    nav: string = 'module/billingmessage';
    id: string = 'B232DF4D-462A-4810-952D-73F8DE66800C';
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
<div data-name="BillingMessage" data-control="FwBrowse" data-type="Browse" id="BillingMessageBrowse" class="fwcontrol fwbrowse" data-controller="BillingMessageController" data-hasinactive="false" data-hasfind="false">
  <div class="column" data-width="0" data-visible="false">
    <div class="field" data-isuniqueid="true" data-datafield="BillingMessageId" data-browsedatatype="key" data-sort="desc"></div>
  </div>
  <div class="column" data-width="auto" data-visible="true">
    <div class="field" data-caption="Message" data-datafield="BillingMessage" data-browsedatatype="text" data-sort="off"></div>
  </div>
</div>
        `;
    }
};
//----------------------------------------------------------------------------------------------
var BillingMessageController = new BillingMessage();