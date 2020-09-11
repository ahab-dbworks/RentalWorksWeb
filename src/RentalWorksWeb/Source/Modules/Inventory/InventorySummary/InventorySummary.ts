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
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        // Set Description from I-Code validation
        $form.find('[data-datafield="InventoryId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        //const totalFields = ["LineTotalWithTax", "Tax", "Tax2", "LineTotal", "LineTotalBeforeDiscount", "DiscountAmount"];
        //                               Total               Tax   SubTotal      GrossTotal                 Discount

        FwBrowse.renderGrid({
            nameGrid: 'InventorySummaryOutGrid',
            gridSelector: `div[data-grid="InventorySummaryOutGrid"]`,
            gridSecurityId: '0LZv8tP11itN2',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId'),
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId'),
                };
                //request.totalfields = totalFields;
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {

            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateGridTotals($form, 'out', dt.Totals);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'InventorySummaryRetiredGrid',
            gridSelector: `div[data-grid="InventorySummaryRetiredGrid"]`,
            gridSecurityId: '5LpDkxSK6jqMz',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId'),
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId'),
                };
                //request.totalfields = totalFields;
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {

            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateGridTotals($form, 'retired', dt.Totals);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'InventorySummaryPhysicalInventoryGrid',
            gridSelector: `div[data-grid="InventorySummaryPhysicalInventoryGrid"]`,
            gridSecurityId: '3ZMKqWS2A4CDO',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryId: FwFormField.getValueByDataField($form, 'InventoryId'),
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId'),
                };
                //request.totalfields = totalFields;
            },
            beforeSave: (request: any) => {
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {

            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateGridTotals($form, 'physical', dt.Totals);
            },
        });
        // ----------
    }
    //----------------------------------------------------------------------------------------------
    calculateGridTotals($form: JQuery, gridType: string, totals?): void {
        if (totals) {
            const grossTotal = totals.LineTotalBeforeDiscount;
            const discount = totals.DiscountAmount;
            const subTotal = totals.LineTotal;
            const salesTax = totals.Tax;
            const salesTax2 = totals.Tax2;
            const total = totals.LineTotalWithTax;

            FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="SubTotal"]`), subTotal);
            FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="Discount"]`), discount);
            FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="Tax"]`), salesTax);
            FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="Tax2"]`), salesTax2);
            FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="GrossTotal"]`), grossTotal);
            FwFormField.setValue2($form.find(`.${gridType}-totals [data-totalfield="Total"]`), total);
        }
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        switch (datafield) {
            case 'InventoryId':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid,
                    //TrackedBy: 'QUANTITY',
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
        }
    }
}
var InventorySummaryController = new InventorySummary();