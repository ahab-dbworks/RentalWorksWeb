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
        FwMenu.addSubMenuItem(options.$groupOptions, 'View Inventory Purchase Sessions', 'S3zkxYNnBXzo', (e: JQuery.ClickEvent) => {
            try {
                this.viewSessions(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
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
        const office = JSON.parse(sessionStorage.getItem('location'));
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);
        FwFormField.setValueByDataField($form, 'Quantity', 1);
        FwFormField.setValueByDataField($form, 'PurchaseDate', today);
        FwFormField.setValueByDataField($form, 'ReceiveDate', today);
        FwFormField.setValueByDataField($form, 'CurrencyId', office.defaultcurrencyid, office.defaultcurrencycode);
        FwFormField.setValueByDataField($form, 'DefaultCurrencyId', office.defaultcurrencyid, office.defaultcurrencycode);
        this.applyCurrencySymbol($form, $form.find('[data-datafield="UnitCost"], [data-datafield="ConvertedUnitCost"]'), office.defaultcurrencysymbol);
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

        $form.find('[data-datafield="PurchaseVendorId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'CurrencyId', FwBrowse.getValueByDataField(null, $tr, 'DefaultCurrencyId'), FwBrowse.getValueByDataField(null, $tr, 'DefaultCurrencyCode'), true);
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
                CurrencyId: FwFormField.getValueByDataField($form, 'CurrencyId'),
                UnitCost: FwFormField.getValueByDataField($form, 'UnitCost'),
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/inventorypurchaseutility/completesession', request, FwServices.defaultTimeout,
                response => {
                    if (response.success) {
                        FwNotification.renderNotification("SUCCESS", "Inventory Purchase Completed Successfully");
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

        $form.find('[data-datafield="CurrencyId"]').on('change', e => {
            const location = JSON.parse(sessionStorage.getItem('location'));
            const defaultCurrencyId = location.defaultcurrencyid;
            const defaultCurrencySymbol = location.defaultcurrencysymbol;
            const currencyId = FwFormField.getValueByDataField($form, 'CurrencyId');

            if ((currencyId != '') && (currencyId != defaultCurrencyId)) {
                const request: any = {};
                request.uniqueids = {
                    FromCurrencyId: defaultCurrencyId,
                    ToCurrencyId: currencyId
                }
                FwAppData.apiMethod(true, 'POST', `api/v1/currencyexchangerate/browse`, request, FwServices.defaultTimeout,
                    response => {
                        if (response.Rows.length > 0) {
                            const currencySymbol = response.Rows[0][response.ColumnIndex.ToCurrencySymbol];
                            const exchangeRate = response.Rows[0][response.ColumnIndex.ExchangeRate];
                            this.applyCurrencySymbol($form, $form.find('[data-datafield="UnitCost"]'), currencySymbol);
                            FwFormField.setValueByDataField($form, 'ExchangeRate', exchangeRate);
                            this.calculateUnitCost($form);
                        }
                    }, ex => FwFunc.showError(ex), $form);
                $form.find('.default-currency').show();
            } else {
                this.applyCurrencySymbol($form, $form.find('[data-datafield="UnitCost"]'), defaultCurrencySymbol);
                $form.find('.default-currency').hide();
            }
        });

        $form.find('[data-datafield="UnitCost"]').on('change', e => {
            this.calculateUnitCost($form);
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
                $browse.on('keydown', '[data-browsedatafield="BarCode"], [data-browsedatafield="SerialNumber"]', e => {
                    const keycode = e.keyCode || e.which;
                    if (keycode === 13) {
                        this.saveBarCodeOnRow($form, $browse, e);
                    }
                });
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    saveBarCodeOnRow($form, $browse, evt) {
        try {
            const $this = jQuery(evt.currentTarget);
            const $tr = $this.parents('tr');
            const datafield = $this.attr('data-browsedatafield');
            let $nextRow = FwBrowse.selectNextRow($browse);
            const nextIndex = FwBrowse.getSelectedIndex($browse);

            FwBrowse.saveRow($browse, $tr)
                .then((value) => {
                    if (nextIndex != -1) {
                        $nextRow = FwBrowse.selectRowByIndex($browse, nextIndex);
                        FwBrowse.setRowEditMode($browse, $nextRow);
                        $browse.data('selectedfield', datafield);
                    }
                });
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    viewSessions($form) {
        const $browse = InventoryPurchaseSessionController.openBrowse();
        const $popup = FwPopup.renderPopup($browse, { ismodal: true }, 'Inventory Purchase Sessions');
        FwPopup.showPopup($popup);
        $browse.data('ondatabind', request => {
            request.uniqueids = {

            }
        });
        FwBrowse.search($browse);

        $browse.on('dblclick', 'tr.viewmode', e => {
            $browse.find('[data-type="DeleteMenuBarButton"]').click();
        });
    }
    //----------------------------------------------------------------------------------------------
    applyCurrencySymbol($form: JQuery, $fields: JQuery, currencySymbol: string) {
        for (let i = 0; i < $fields.length; i++) {
            let $field = jQuery($fields[i]);
            $field.attr('data-currencysymboldisplay', currencySymbol);

            $field
                .find('.fwformfield-value')
                .inputmask('currency', {
                    prefix: currencySymbol + ' ',
                    placeholder: "0.00",
                    min: ((typeof $field.attr('data-minvalue') !== 'undefined') ? $field.attr('data-minvalue') : undefined),
                    max: ((typeof $field.attr('data-maxvalue') !== 'undefined') ? $field.attr('data-maxvalue') : undefined),
                    digits: ((typeof $field.attr('data-digits') !== 'undefined') ? $field.attr('data-digits') : 2),
                    radixPoint: '.',
                    groupSeparator: ','
                });
        }
    }
    //----------------------------------------------------------------------------------------------
    calculateUnitCost($form: JQuery) {
        const cost = parseFloat(FwFormField.getValueByDataField($form, 'UnitCost'));
        const exchangeRate = parseFloat(FwFormField.getValueByDataField($form, 'ExchangeRate'));
        const convertedUnitCost = cost * exchangeRate;
        FwFormField.setValueByDataField($form, 'ConvertedUnitCost', convertedUnitCost);
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