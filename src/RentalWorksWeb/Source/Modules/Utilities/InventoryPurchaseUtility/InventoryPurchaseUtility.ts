routes.push({ pattern: /^module\/inventorypurchaseutility$/, action: function (match: RegExpExecArray) { return InventoryPurchaseUtilityController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class InventoryPurchaseUtility {
    Module: string = 'InventoryPurchaseUtility';
    caption: string = Constants.Modules.Utilities.children.InventoryPurchaseUtility.caption;
    apiurl: string = 'api/v1/inventorypurchaseutility';
    nav: string = Constants.Modules.Utilities.children.InventoryPurchaseUtility.nav;
    id: string = Constants.Modules.Utilities.children.InventoryPurchaseUtility.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');
        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $manufacturerValidation = $form.find('[data-datafield="ManufacturerVendorId"]');
        $manufacturerValidation.data('beforevalidate', ($form, $manufacturerValidation, request) => {
            request.uniqueids = {
                'Manufacturer': true,
            }
        });

        const $purchaseValidation = $form.find('[data-datafield="PurchaseVendorId"]');
        $purchaseValidation.data('beforevalidate', ($form, $purchaseValidation, request) => {
            request.uniqueids = {
                'RentalInventory': true,
            }
        });

        const $rentalInventoryValidation = $form.find('.icode[data-datafield="InventoryId"]');
        $rentalInventoryValidation.data('beforevalidate', ($form, $rentalInventoryValidation, request) => {
            request.uniqueids = {
                'WarehouseId': warehouse.warehouseid,
                'Classification': 'I,A',
            }
        });

        const $rentalInventoryDescValidation = $form.find('.description[data-datafield="InventoryId"]');
        $rentalInventoryDescValidation.data('beforevalidate', ($form, $rentalInventoryDescValidation, request) => {
            request.uniqueids = {
                'WarehouseId': warehouse.warehouseid,
                'Classification': 'I,A',
            }
        });

        this.setDefaults($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    setDefaults($form) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);
        FwFormField.setValueByDataField($form, 'Quantity', 1);
        FwFormField.setValueByDataField($form, 'PurchaseDate', today);
        FwFormField.setValueByDataField($form, 'ReceiveDate', today);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        const $itemGridControl = $form.find('[data-name="InventoryPurchaseItemGrid"]');

        $form.find('.description[data-datafield="InventoryId"]').data('onchange', $tr => {
            FwFormField.setValue2($form.find('.icode[data-datafield="InventoryId"]'), FwBrowse.getValueByDataField(null, $tr, 'InventoryId'), FwBrowse.getValueByDataField(null, $tr, 'ICode'))
            $form.find('.icode[data-datafield="InventoryId"]').data('onchange')($tr);
        });

        $form.find('.icode[data-datafield="InventoryId"]').data('onchange', $tr => {
            const trackedBy = FwBrowse.getValueByDataField(null, $tr, 'TrackedBy');
            if (trackedBy === 'QUANTITY') {
                $form.find('.tracked-by').hide();
            } else {
                $form.find('.tracked-by').show();
            }
            $form.find('.additems').show();
            const inventoryId = FwBrowse.getValueByDataField(null, $tr, 'InventoryId');
            const description = FwBrowse.getValueByDataField(null, $tr, 'Description');
            FwFormField.setValue2($form.find('.description[data-datafield="InventoryId"]'), inventoryId, description, false);
            const unitVal = FwBrowse.getValueByDataField(null, $tr, 'UnitValue');
            FwFormField.setValueByDataField($form, 'UnitCost', unitVal);
            const aisleLoc = FwBrowse.getValueByDataField(null, $tr, 'AisleLocation');
            FwFormField.setValueByDataField($form, 'AisleLocation', aisleLoc);
            const shelfLoc = FwBrowse.getValueByDataField(null, $tr, 'ShelfLocation');
            FwFormField.setValueByDataField($form, 'ShelfLocation', shelfLoc);

            const request: any = {};
            request.InventoryId = inventoryId;
            request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
            if (typeof $form.data('sessionid') === 'string') {
                request.SessionId = $form.data('sessionid');
                FwAppData.apiMethod(true, 'POST', `api/v1/inventorypurchaseutility/updatesession`, request, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.search($itemGridControl);
                    }, ex => FwFunc.showError(ex), $form);
            } else {
                FwAppData.apiMethod(true, 'POST', `api/v1/inventorypurchaseutility/startsession`, request, FwServices.defaultTimeout,
                    response => {
                        $form.data("sessionid", response.SessionId);
                        FwBrowse.search($itemGridControl);
                    }, ex => FwFunc.showError(ex), $form);
            }
        });

        $form.find('[data-datafield="WarrantyDays"]').on('change', e => {
            const days = FwFormField.getValueByDataField($form, 'WarrantyDays');
            const today = FwFunc.getDate();
            const expiration = FwFunc.getDate(today, parseInt(days));
            FwFormField.setValueByDataField($form, 'WarrantyExpiration', expiration);
        });

        $form.find('[data-datafield="ManufacturerVendorId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'CountryId', FwBrowse.getValueByDataField(null, $tr, 'CountryId'), FwBrowse.getValueByDataField(null, $tr, 'Country'));
        });

        $form.find('[data-datafield="Quantity"]').on('change', e => {
            const request: any = {};
            request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
            if (typeof $form.data('sessionid') === 'string') {
                request.SessionId = $form.data('sessionid');
                FwAppData.apiMethod(true, 'POST', `api/v1/inventorypurchaseutility/updatesession`, request, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.search($itemGridControl);
                    }, ex => FwFunc.showError(ex), $form);
            }
        });

        //Add items button
        $form.find('.additems').on('click', e => {
            let request: any = {
                SessionId: $form.data('sessionid'),
                InventoryId: FwFormField.getValueByDataField($form, 'InventoryId'),
                Quantity: FwFormField.getValueByDataField($form, 'Quantity'),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId'),
                AisleLocation: FwFormField.getValueByDataField($form, 'AisleLocation'),
                ShelfLocation: FwFormField.getValueByDataField($form, 'ShelfLocation'),
                ManufacturerVendorId: FwFormField.getValueByDataField($form, 'ManufacturerVendorId'),
                ManufacturerModelNumber: FwFormField.getValueByDataField($form, 'ManufacturerModelNumber'),
                ManufacturerPartNumber: FwFormField.getValueByDataField($form, 'ManufacturerPartNumber'),
                CountryId: FwFormField.getValueByDataField($form, 'CountryId'),
                WarrantyDays: FwFormField.getValueByDataField($form, 'WarrantyDays'),
                WarrantyExpiration: FwFormField.getValueByDataField($form, 'WarrantyExpiration'),
                PurchaseVendorId: FwFormField.getValueByDataField($form, 'PurchaseVendorId'),
                OutsidePoNumber: FwFormField.getValueByDataField($form, 'OutsidePoNumber'),
                PurchaseDate: FwFormField.getValueByDataField($form, 'PurchaseDate'),
                ReceiveDate: FwFormField.getValueByDataField($form, 'ReceiveDate'),
                VendorPartNumber: FwFormField.getValueByDataField($form, 'VendorPartNumber'),
                UnitCost: FwFormField.getValueByDataField($form, 'UnitCost'),
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/inventorypurchaseutility/completesession', request, FwServices.defaultTimeout,
                response => {
                    if (response.success) {
                        FwNotification.renderNotification("SUCCESS", "Purchase(s) Successfully Created");
                        $form.find('.fwformfield input').val('');
                        $itemGridControl.find('tr.viewmode').empty();
                        $form.removeData('sessionid');
                        this.setDefaults($form);
                        //    const uniqueids: any = {};
                        //    uniqueids.PurchaseId = 
                        //    const $control = PurchaseController.loadForm(uniqueids);
                        //    FwModule.openModuleTab($control, "", true, 'FORM', true);
                    } else {
                        FwNotification.renderNotification("ERROR", response.msg);
                    }
                }, ex => FwFunc.showError(ex), $form);
        });

        //Assign Bar Codes button
        $form.find('.assignbarcodes').on('click', e => {
            if (typeof $form.data('sessionid') === 'string') {
                const request: any = {};
                request.SessionId = $form.data('sessionid');
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                FwAppData.apiMethod(true, 'POST', `api/v1/inventorypurchaseutility/assignbarcodes`, request, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.search($itemGridControl);
                    }, ex => FwFunc.showError(ex), $form);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'InventoryPurchaseItemGrid',
            gridSecurityId: 'qH0cLrQVt9avI',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    SessionId: $form.data('sessionid')
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.on('keydown', '[data-browsedatafield="BarCode"]', e => {
                    const keycode = e.keyCode || e.which;
                    if (keycode === 13) {
                        let $tr = jQuery(e.currentTarget).parents('tr');
                        FwBrowse.saveRow($browse, $tr)
                            .then((value) => {
                                $tr = FwBrowse.selectRowByIndex($browse, 0);
                                FwBrowse.setRowEditMode($browse, $tr);
                                $browse.data('selectedfield', 'BarCode');
                            });
                    }
                });
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InventoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'ManufacturerVendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemanufacturervendor`);
                break;
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
            case 'PurchaseVendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepurchasevendor`);
                break;
        }
    }
};
//----------------------------------------------------------------------------------------------
var InventoryPurchaseUtilityController = new InventoryPurchaseUtility();