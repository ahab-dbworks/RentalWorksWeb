﻿routes.push({ pattern: /^module\/inventorysummary/, action: function (match: RegExpExecArray) { return InventorySummaryController.getModuleScreen(); } });

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

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, `InventoryId`, parentmoduleinfo.InventoryId, parentmoduleinfo.ICode);
            FwFormField.setValueByDataField($form, `Description`, parentmoduleinfo.Description);
            if (parentmoduleinfo.InventoryId !== '') {
                    $form.find(`[data-datafield="InventoryId"]`).change();
                    const $inventorySummaryOutGrid = $form.find('div[data-name="InventorySummaryOutItemsGrid"]');
                    FwBrowse.search($inventorySummaryOutGrid);

                    $form.find('.out-row').show();
            }
        }
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        // Set Description from I-Code validation
        $form.find('[data-datafield="InventoryId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="Description"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
            const $inventorySummaryOutGrid = $form.find('div[data-name="InventorySummaryOutItemsGrid"]');
            FwBrowse.search($inventorySummaryOutGrid);

            $form.find('.out-row').show();
        });

        // changing Warehouse - refresh grid
        $form.find('[data-datafield="WarehouseId"]').data('onchange', $tr => {
            const $inventorySummaryOutGrid = $form.find('div[data-name="InventorySummaryOutItemsGrid"]');
            FwBrowse.search($inventorySummaryOutGrid);
            $form.find('.out-row').show();
        });


        $form.find('div[data-type="tab"]').on('click', e => {
            //Disable clicking Quantity Items tab w/o an OrderId
            const inventoryId = FwFormField.getValueByDataField($form, `InventoryId`);
            if (inventoryId === '') {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Item.')
            }
        });
        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const tabname = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        //const totalFields = ["LineTotalWithTax", "Tax", "Tax2", "LineTotal", "LineTotalBeforeDiscount", "DiscountAmount"];
        //                               Total               Tax   SubTotal      GrossTotal                 Discount

        FwBrowse.renderGrid({
            nameGrid: 'InventorySummaryOutItemsGrid',
            gridSelector: `div[data-grid="InventorySummaryOutItemsGrid"]`,
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
            nameGrid: 'InventorySummaryRetiredHistoryGrid',
            gridSelector: `div[data-grid="InventorySummaryRetiredHistoryGrid"]`,
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
                    Classification: 'I,A',
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
        }
    }
}
var InventorySummaryController = new InventorySummary();