routes.push({ pattern: /^module\/dealcredit$/, action: function (match: RegExpExecArray) { return DealCreditController.getModuleScreen(); } });

class DealCredit {
    Module: string = 'DealCredit';
    apiurl: string = 'api/v1/dealcredit';
    caption: string = 'Deal Credit';
    nav: string = 'module/dealcredit';
    id: string = '3DD1BA32-0213-472E-ADA8-E54D531464CC';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        //try {
        //    FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
        //        for (var key in response) {
        //            FwBrowse.addLegend($browse, key, response[key]);
        //        }
        //    }, function onError(response) {
        //        FwFunc.showError(response);
        //    }, $browse)
        //} catch (ex) {
        //    FwFunc.showError(ex);
        //}

        return $browse;
    };
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        //const location = JSON.parse(sessionStorage.getItem('location'));
        //const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        //const $userLocation: JQuery = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
        //if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
        //    this.ActiveViewFields.LocationId = [location.locationid];
        //}
        //const viewSubitems: Array<JQuery> = [];
        //viewSubitems.push($userLocation, $all);
        //FwMenu.addViewBtn($menuObject, 'Location', viewSubitems, true, "LocationId");

        //return $menuObject;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        // var $form = FwModule.loadFormFromTemplate(this.Module);
        // let $form = jQuery(AssetController.getFormTemplate());

        //$form = FwModule.openForm($form, mode);
        //return $form;
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        //const $form = this.openForm('EDIT');
        //FwFormField.setValueByDataField($form, 'ReceiptId', uniqueids.ItemId);
        //FwModule.loadForm(this.Module, $form);

        //return $form;
    };
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //---------------------------------------------------------------------------------------------
    loadAudit($form: JQuery) {
        const uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    };
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
           <div data-name="DealCredit" data-control="FwBrowse" data-type="Browse" id="DealCreditBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="DealCreditController" data-hasinactive="false">
            <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-isuniqueid="true" data-datafield="ReceiptId" data-browsedatatype="key"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Date" data-datafield="ReceiptDate" data-browsedatatype="date" data-sort="asc"></div>
             </div>
             <div class="column flexcolumn" max-width="250px" data-visible="true">
               <div class="field" data-caption="Customer" data-datafield="Customer" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="250px" data-visible="true">
               <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Payment Type" data-datafield="PaymentType" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Ref/Check" data-datafield="CheckNumber" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Amount" data-datafield="Amount" data-browsedatatype="number" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Applied" data-datafield="Applied" data-browsedatatype="number" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Refunded" data-datafield="Refunded" data-browsedatatype="number" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Remaining" data-datafield="Remaining" data-browsedatatype="number" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
             </div>
             <div class="column spacer" data-width="auto" data-visible="true"></div>
           </div>`;
    };
    //---------------------------------------------------------------------------------------------
}

var DealCreditController = new DealCredit();