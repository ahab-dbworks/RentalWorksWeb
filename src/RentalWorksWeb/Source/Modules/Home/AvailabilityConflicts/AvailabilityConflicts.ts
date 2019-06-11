routes.push({ pattern: /^module\/availabilityconflicts/, action: function (match: RegExpExecArray) { return AvailabilityConflictsController.getModuleScreen(); } });
class AvailabilityConflicts {
    Module: string = 'AvailabilityConflicts';
    caption: string = Constants.Modules.Home.AvailabilityConflicts.caption;
    nav: string = Constants.Modules.Home.AvailabilityConflicts.nav;
    id: string = Constants.Modules.Home.AvailabilityConflicts.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
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

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.loadItems($form.find('div[data-datafield="Rank"]'), [
            { value: "A", text: "A", selected: "T" },
            { value: "B", text: "B", selected: "T" },
            { value: "C", text: "C", selected: "T" },
            { value: "D", text: "D", selected: "T" },
            { value: "E", text: "E", selected: "T" },
            { value: "F", text: "F", selected: "T" },
            { value: "G", text: "G", selected: "T" }
        ]);

        FwFormField.setValueByDataField($form, 'ConflictType', 'N');

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValueByDataField($form, 'Warehouse', warehouse.warehouseid, warehouse.warehouse);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('.refresh').on('click', e => {
            const request: any = {};
            request.AvailableFor = FwFormField.getValueByDataField($form, 'AvailableFor');
            request.ConflictType = FwFormField.getValueByDataField($form, 'ConflictType');
            request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            request.InventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            request.SubCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
            request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            request.Description = FwFormField.getValueByDataField($form, 'Description');
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            request.Ranks = FwFormField.getValueByDataField($form, 'Rank');

            FwAppData.apiMethod(true, 'POST', 'api/v1/inventoryavailability/conflicts', request, FwServices.defaultTimeout,
                response => {
               
                },
                ex => FwFunc.showError(ex), $form);
        });
    }
    beforeValidate($browse, $grid, request) {
        //const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        //request.uniqueids = {
        //    WarehouseId: warehouse.warehouseid
        //};
    }
    //----------------------------------------------------------------------------------------------
}
var AvailabilityConflictsController = new AvailabilityConflicts();