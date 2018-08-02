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
            FwModule.openModuleTab($form, 'Staging / Check-Out', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form: JQuery = this.openForm('EDIT');
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);
        
        return $form;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?:any) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-StagingCheckoutForm').html());
        $form = FwModule.openForm($form, mode);

        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        $form.find('[data-datafield="WarehouseId"]').hide();
        $form.find('.orderstatus').hide();
        $form.find('.createcontract').hide();

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
        $form.find('div[data-datafield="OrderId"] input').focus();
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
                    FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
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
            FwFormField.disable($form.find('div[data-datafield="OrderId"]'));
            $form.find('.orderstatus').show();
            $form.find('.createcontract').show();


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
        let errorBeep = new Audio('./theme/audio/errorBeep1.wav');

        // BarCode / I-Code change
        $form.find('[data-datafield="Code"] input').bind('keypress change', function (e) {
            if (e.type === 'change' || e.keyCode == 13) {
                $form.find('.error-msg').html('');
                $form.find('div.AddItemToOrder').html('');
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
                    if (response.success === true && response.status != 107) {
                        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);

                        FwBrowse.search($stagedItemGrid);
                        $form.find('[data-datafield="Code"] input').select();
                    } if (response.status === 107) {
                        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);

                        FwFormField.setValueByDataField($form, 'Quantity', 0)
                        $form.find('div[data-datafield="Quantity"] input').select();
                    } if (response.ShowAddItemToOrder === true) {
                        errorBeep.play();
                        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`)
                    } if (response.ShowAddCompleteToOrder === true) {
                        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                    } if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                        errorBeep.play();
                        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                        $form.find('[data-datafield="Code"] input').select();
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                    FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                    FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                    FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                    FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                    FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                    FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                    $form.find('[data-datafield="Code"] input').select();
                }, $form);
            }
        })​;​

        //Quantity change
        $form.find('[data-datafield="Quantity"] input').bind('keypress change', function (e) {
            if (e.type === 'change' || e.keyCode == 13) {
                $form.find('.error-msg').html('');
                $form.find('div.AddItemToOrder').html('');
                let code, orderId, quantity, $stagedItemGrid;
                orderId = FwFormField.getValueByDataField($form, 'OrderId');
                code = FwFormField.getValueByDataField($form, 'Code');
                quantity = +FwFormField.getValueByDataField($form, 'Quantity');
                $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');

                let request: any = {};
                request = {
                    OrderId: orderId,
                    Code: code,
                    Quantity: quantity
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

                        FwBrowse.search($stagedItemGrid);
                        FwFormField.setValueByDataField($form, 'Quantity', 0)
                        $form.find('[data-datafield="Code"] input').select();
                    } if (response.ShowAddItemToOrder === true) {
                        errorBeep.play();
                        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`)
                    } if (response.ShowAddCompleteToOrder === true) {
                        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                    } if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                        errorBeep.play();
                        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                        $form.find('[data-datafield="Code"] input').select();
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    $form.find('[data-datafield="Code"] input').select();
                }, $form);
            }
        });

        // Order Status
        $form.find('.orderstatus').on('click', e => {
            let $orderStatusForm;
            try {
                var mode = 'EDIT';
                var orderInfo: any = {};
                orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                orderInfo.OrderNumber = FwFormField.getTextByDataField($form, 'OrderId');
                $orderStatusForm = OrderStatusController.openForm(mode, orderInfo);
                FwModule.openSubModuleTab($form, $orderStatusForm);
                jQuery('.tab.submodule.active').find('.caption').html('Order Status');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

        // Create Contract
        $form.find('.createcontract').on('click', e => {
            let orderId = FwFormField.getValueByDataField($form, 'OrderId');
            let requestBody: any = {}, $stagedItemGrid;
            $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
            //let automaticallyCreateCheckOut = FwFormField.getValueByDataField($form, 'AutomaticallyCreateCheckOut');
            //let date = new Date(),
            //    currentDate = date.toLocaleString(),
            //    currentTime = date.toLocaleTimeString();

            //if (automaticallyCreateCheckOut == 'T') {
            //    requestBody = {
            //        CreateOutContracts: true
            //    }
            //}
            requestBody.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', "api/v1/checkout/checkoutallstaged/", requestBody, FwServices.defaultTimeout, function onSuccess(response) {
                let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                try {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = response.ContractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);
                    $form.find('.fwformfield').not('[data-type="date"], [data-type="time"]').find('input').val('');
                    FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
                    $form.find('.orderstatus').hide();
                    $form.find('.createcontract').hide();
                    FwFormField.enable($form.find('div[data-datafield="OrderId"]'));
                    $form.find('[data-datafield="Code"] input').select();
                    $form.find('div[data-name="StagedItemGrid"] tr.viewmode').empty();
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, null);

        });

    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        switch (validationName) {
            case 'OrderValidation':
                request.miscfields = {
                    Staging: true,
                    StagingWarehouseId: warehouse.warehouseid,
                };
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
    addItemToOrder(element: any) {
        let code, $form, $element, orderId, quantity, $stagedItemGrid, request: any = {};
        $element = jQuery(element);
        $form = jQuery($element).closest('.fwform'); 
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        code = FwFormField.getValueByDataField($form, 'Code');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');

        request = {
            OrderId: orderId,
            Code: code,
            AddItemToOrder: true
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                FwBrowse.search($stagedItemGrid);
                $form.find('.error-msg').html('');
                $form.find('div.AddItemToOrder').html('');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
        $form.find('[data-datafield="Code"] input').select();
    }
    //----------------------------------------------------------------------------------------------
    addCompleteToOrder(element: any) {
        let code, $form, $element, orderId, quantity, $stagedItemGrid, request: any = {};
        $element = jQuery(element);
        $form = jQuery($element).closest('.fwform');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        code = FwFormField.getValueByDataField($form, 'Code');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');

        request = {
            OrderId: orderId,
            Code: code,
            AddCompleteToOrder: true
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                FwBrowse.search($stagedItemGrid);
                $form.find('.error-msg').html('');
                $form.find('div.AddItemToOrder').html('');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
        $form.find('[data-datafield="Code"] input').select();
    }
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