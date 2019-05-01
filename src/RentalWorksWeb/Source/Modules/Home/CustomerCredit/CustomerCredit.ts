routes.push({ pattern: /^module\/customercredit$/, action: function (match: RegExpExecArray) { return CustomerCreditController.getModuleScreen(); } });

class CustomerCredit {
    Module: string = 'CustomerCredit';
    apiurl: string = 'api/v1/customercredit';
    caption: string = Constants.Modules.Home.CustomerCredit.caption;
	nav: string = Constants.Modules.Home.CustomerCredit.nav;
	id: string = Constants.Modules.Home.CustomerCredit.id;
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

        FwBrowse.addLegend($browse, 'Credit Memo', '#ABABD6');
        FwBrowse.addLegend($browse, 'Depleting Deposit', '#37D303');
        FwBrowse.addLegend($browse, 'Overpayment', '#FFFFFF');
        FwBrowse.addLegend($browse, 'Refund Check', '#6F6FFF');

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
        //let $form = jQuery(AssetController.getFormTemplate());

        //$form = FwModule.openForm($form, mode);
        //return $form;
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        //const $form = this.openForm('EDIT');
        //FwFormField.setValueByDataField($form, 'ItemId', uniqueids.ItemId);
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
           <div data-name="CustomerCredit" data-control="FwBrowse" data-type="Browse" id="CustomerCreditBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="CustomerCreditController" data-hasinactive="false">
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-isuniqueid="true" data-datafield="ReceiptId" data-browsedatatype="key"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Date" data-datafield="ReceiptDate" data-browsedatatype="date" data-sort="desc"></div>
             </div>
             <div class="column flexcolumn" max-width="250px" data-visible="true">
               <div class="field" data-caption="Customer" data-datafield="Customer" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Payment Type" data-datafield="PaymentType" data-cellcolor="RecTypeColor" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Ref/Check" data-datafield="CheckNumber" data-browsedatatype="text" data-sort="desc"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Amount" data-datafield="Amount" data-browsedatatype="money" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Applied" data-datafield="Applied" data-browsedatatype="money" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Refunded" data-datafield="Refunded" data-browsedatatype="money" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Remaining" data-datafield="Remaining" data-browsedatatype="money" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
             </div>
             <div class="column spacer" data-width="auto" data-visible="true"></div>
           </div>`;
    };
    //---------------------------------------------------------------------------------------------
}

var CustomerCreditController = new CustomerCredit();