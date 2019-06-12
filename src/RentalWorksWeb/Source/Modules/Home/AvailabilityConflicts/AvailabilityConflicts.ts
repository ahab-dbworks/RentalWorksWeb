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
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);

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
                    this.loadAvailabilityTable($form, response);
                },
                ex => FwFunc.showError(ex), $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    loadAvailabilityTable($form, data) {
        const html = `
                <table>
                    <thead>
                        <tr>
                            <th>Warehouse</th>
                            <th>Type</th>
                            <th>Category</th>
                            <th>Sub Category</th>
                            <th>I-Code</th>
                            <th>Item Description</th>
                            <th>Order No.</th>
                            <th>Order Description</th>
                            <th>Deal</th>
                            <th>Ordered</th>
                            <th>Sub</th>
                            <th>Available</th>
                            <th>Late</th>
                            <th>In</th>
                            <th>QC</th>
                            <th>From</th>
                            <th>To</th>
                        <tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>`;

        $form.find('#availabilityTable').append(html);

        const $rows: any = [];
        for (let i = 0; i < data.length; i++) {
            const row = `
                    <tr>
                        <td>${data[i].Warehouse}</td>
                        <td>${data[i].InventoryType}</td>
                        <td>${data[i].Category}</td>
                        <td>${data[i].SubCategory}</td>
                        <td>${data[i].ICode}</td>
                        <td>${data[i].ItemDescription}</td>
                        <td>${data[i].OrderNumber}</td>
                        <td>${data[i].OrderDescription}</td>
                        <td>${data[i].Deal}</td>
                        <td>${data[i].QuantityOrdered}</td>
                        <td>${data[i].QuantitySub}</td>
                        <td>${data[i].QuantityAvailable}</td>
                        <td>${data[i].QuantityLate}</td>
                        <td>${data[i].QuantityIn}</td>
                        <td>${data[i].QuantityQc}</td>
                        <td>${data[i].FromDateTime}</td>
                        <td>${data[i].ToDateTime}</td>
                    </tr>
                    `;
            $rows.push(row);
        }

        $form.find('tbody').append($rows);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        //const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        //request.uniqueids = {
        //    WarehouseId: warehouse.warehouseid
        //};
    }
    //----------------------------------------------------------------------------------------------
}
var AvailabilityConflictsController = new AvailabilityConflicts();