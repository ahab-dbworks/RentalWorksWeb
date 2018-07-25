routes.push({ pattern: /^module\/checkout$/, action: function (match: RegExpExecArray) { return StagingCheckoutController.getModuleScreen(); } });

class StagingCheckout {
    Module: string = 'StagingCheckout';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Staging / Checkout', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?:any) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-StagingCheckoutForm').html());
        $form = FwModule.openForm($form, mode);

        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        $form.find('[data-datafield="WarehouseId"]').hide();
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        this.getOrder($form);
        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="OrderId"] input.fwformfield-value').val(parentmoduleinfo.OrderId);
            $form.find('div[data-datafield="OrderId"] input.fwformfield-text').val(parentmoduleinfo.OrderNumber);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', parentmoduleinfo.WarehouseId, parentmoduleinfo.Warehouse);
            FwFormField.setValueByDataField($form, 'Description', parentmoduleinfo.description);
            jQuery($form.find('[data-datafield="OrderId"]')).trigger('change');
        }

        //$form.find('.rentalview').hide();
        //$form.find('.salesview').hide();

        //$form.find('div[data-datafield="TaxOptionId"]').data('onchange', function ($tr) {
        //    FwFormField.setValue($form, 'div[data-datafield=""]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
        //});

        this.events($form);
        return $form;
    };

    //----------------------------------------------------------------------------------------------
    getOrder($form: JQuery): void {
        const order = $form.find('[data-datafield="OrderId"]');
        const maxPageSize = 9999;
        order.on('change', function () {
            try {
                let orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();
                let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                FwAppData.apiMethod(true, 'GET', "api/v1/order/" + orderId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Location', response.Location);
                    //FwFormField.setValueByDataField($form, 'WarehouseId', response.WarehouseId);
                    //FwFormField.setValueByDataField($form, 'Status', response.Status);
                    //FwFormField.setValueByDataField($form, 'EstimatedStartDate', response.EstimatedStartDate);
                    //FwFormField.setValueByDataField($form, 'EstimatedStartTime', response.EstimatedStartTime);
                    //FwFormField.setValueByDataField($form, 'EstimatedStopDate', response.EstimatedStopDate);
                    //FwFormField.setValueByDataField($form, 'EstimatedStopTime', response.EstimatedStopTime);

                    //var rental = response.Rental;
                    //var sales = response.Sales;
                    //if (rental === false && sales === false) {
                    //    $form.find('div[data-value="Details"]').hide();
                    //} else {
                    //    $form.find('div[data-value="Details"]').show();
                    //}

                    //if (rental === true) {
                    //    $form.find('.rentalview').show();
                    //} else {
                    //    $form.find('.rentalview').hide();
                    //}

                    //if (sales === true) {
                    //    $form.find('.salesview').show();
                    //} else {
                    //    $form.find('.salesview').hide();
                    //}

                    //$form.find('.details').hide();
                }, null, $form);

                var $stagedItemGridControl: any;
                $stagedItemGridControl = $form.find('[data-name="StagedItemGrid"]');
                $stagedItemGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId,
                        WarehouseId: warehouseId
                    }
                    request.pagesize = maxPageSize;
                })
                FwBrowse.search($stagedItemGridControl);

                //var $orderStatusRentalDetailGridControl: any;
                //$orderStatusRentalDetailGridControl = $form.find('[data-name="OrderStatusRentalDetailGrid"]');
                //$orderStatusRentalDetailGridControl.data('ondatabind', function (request) {
                //    request.uniqueids = {
                //        OrderId: orderId,
                //        RecType: "R"
                //    }
                //    request.pagesize = maxPageSize;
                //})
                //FwBrowse.search($orderStatusRentalDetailGridControl);

                //var $orderStatusSalesDetailGridControl: any;
                //$orderStatusSalesDetailGridControl = $form.find('[data-name="OrderStatusSalesDetailGrid"]');
                //$orderStatusSalesDetailGridControl.data('ondatabind', function (request) {
                //    request.uniqueids = {
                //        OrderId: orderId,
                //        RecType: "S"
                //    }
                //    request.pagesize = maxPageSize;
                //})
                //FwBrowse.search($orderStatusSalesDetailGridControl);

                setTimeout(function () {
                    var $trs = $form.find('.ordersummarygrid tr.viewmode');

                    var $contractpeek = $form.find('.outcontract, .incontract');
                    $contractpeek.attr('data-browsedatafield', 'ContractId');

                    for (var i = 0; i <= $trs.length; i++) {
                        var $rectype = jQuery($trs[i]).find('[data-browsedatafield="RecTypeDisplay"]');
                        var recvalue = $rectype.attr('data-originalvalue');
                        var $validationfield = jQuery($trs[i]).find('[data-browsedatafield="InventoryId"]');

                        switch (recvalue) {
                            case 'RENTAL':
                                $validationfield.attr('data-validationname', 'RentalInventoryValidation');
                                break;
                            case 'SALES':
                                $validationfield.attr('data-validationname', 'SalesInventoryValidation');
                                break;
                        }
                    }
                }, 2000);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let $stagedItemGrid: any;
        let $stagedItemGridControl: any;
        let orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();
        let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        let maxPageSize = 9999;

        $stagedItemGrid = $form.find('div[data-grid="StagedItemGrid"]');
        $stagedItemGridControl = jQuery(jQuery('#tmpl-grids-StagedItemGridBrowse').html());
        $stagedItemGrid.empty().append($stagedItemGridControl);
        $stagedItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: orderId,
                WarehouseId: warehouseId
            };
            request.pagesize = maxPageSize;
        })
        FwBrowse.init($stagedItemGridControl);
        FwBrowse.renderRuntimeHtml($stagedItemGridControl);
        //this.addLegend($form, $stagedItemGrid);
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        let $stagedItemGrid;
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        FwBrowse.search($stagedItemGrid);
    };

    //----------------------------------------------------------------------------------------------
    events($form: any) {

        //BarCode / I-Code change
        $form.find('[data-datafield="Code"] input').on('change', event => {
            $form.find('.error-msg').html('')
            let code, orderId, $stagedItemGrid;
            orderId = FwFormField.getValueByDataField($form, 'OrderId');
            code = FwFormField.getValueByDataField($form, 'Code');
            $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
            let request: any = {};
            request = {
                OrderId: orderId,
                Code: code
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                    FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                    FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                    FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                    FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                    FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                    FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);

                    // selects barcode field if response
                    $form.find('[data-datafield="Code"] input').select();

                    if (response.InventoryStatus.QuantityOrdered === 0) {
                        $form.find('div[data-datafield="Quantity"] input').focus();
                    }
                    FwBrowse.search($stagedItemGrid);
                }
                else {
                    $form.find('div.error-msg').html(`<div style="margin-left:8px;"><span style="font-size:20px;background-color:red;color:white;">${response.msg}</span></div>`)
                }
            }, function onError(response) {
                FwFunc.showError(response);
                }, $form);
        });

        //Quantity change
        $form.find('[data-datafield="Quantity"] input').on('change', event => {
            $form.find('.error-msg').html('')
            let code, orderId, quantity;
            orderId = FwFormField.getValueByDataField($form, 'OrderId');
            code = FwFormField.getValueByDataField($form, 'Code');
            quantity = +FwFormField.getValueByDataField($form, 'Quantity');

            let request: any = {};
            request = {
                OrderId: orderId,
                Code: code,
                Quantity: quantity
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                    FwFormField.setValueByDataField($form, 'Description', response.InventoryStatus.Description);
                    FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                    FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                    FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                    FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                    FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                } else {
                    $form.find('div.error-msg').html(`<div style="margin-left:8px;"><span style="font-size:20px;background-color:red;color:white;">${response.msg}</span></div>`)
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
    };

    //----------------------------------------------------------------------------------------------
    addLegend($form: any, $grid) {
        FwBrowse.addLegend($grid, 'Complete', '#8888ff');
        FwBrowse.addLegend($grid, 'Kit', '#03d337');
        FwBrowse.addLegend($grid, 'Exchange', '#a0cdb4');
        FwBrowse.addLegend($grid, 'Sub Vendor', '#ffb18c');
        FwBrowse.addLegend($grid, 'Consignor', '#8080ff');
        FwBrowse.addLegend($grid, 'Truck', '#ffff00');
        FwBrowse.addLegend($grid, 'Suspended', '#0000a0');
        FwBrowse.addLegend($grid, 'Lost', '#ff8080');
        FwBrowse.addLegend($grid, 'Sales', '#ff0080');
        FwBrowse.addLegend($grid, 'Not Yet Staged or Still Out', '#ff0000');
        FwBrowse.addLegend($grid, 'Too Many Staged', '#00ff80');
    };

    //----------------------------------------------------------------------------------------------
}
var StagingCheckoutController = new StagingCheckout();