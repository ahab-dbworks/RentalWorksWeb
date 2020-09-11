routes.push({ pattern: /^module\/inventorysummary/, action: function (match: RegExpExecArray) { return InventorySummaryController.getModuleScreen(); } });

class InventorySummary {
    Module: string = 'InventorySummary';
    apiurl: string = 'api/v1/inventorysummary';
    caption: string = Constants.Modules.Inventory.children.InventorySummary.caption;
    nav: string = Constants.Modules.Inventory.children.InventorySummary.nav;
    id: string = Constants.Modules.Inventory.children.InventorySummary.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables "modified" asterisk
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form) {

    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
    //    const officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
    //    switch (datafield) {
    //        case 'CreateNewDealId':
    //        case 'DealId':
    //            const shareDeals = JSON.parse(sessionStorage.getItem('controldefaults')).sharedealsacrossofficelocations;
    //            if (!shareDeals) {
    //                request.uniqueids = {
    //                    LocationId: officeLocationId
    //                }
    //            }
    //            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
    //            break;
    //        case 'DepartmentId':
    //            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
    //            break;
    //        case 'CreateNewDealId':
    //            request.uniqueids = {
    //                LocationId: officeLocationId
    //            }
    //            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecreatenewdeal`);
    //            break;
    //        case 'RateType':
    //            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateratetype`);
    //            break;
    //    }
    }
}
var InventorySummaryController = new InventorySummary();