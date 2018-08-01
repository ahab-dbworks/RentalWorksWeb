﻿routes.push({ pattern: /^module\/returntovendor$/, action: function (match: RegExpExecArray) { return ReturnToVendorController.getModuleScreen(); } });

class ReturnToVendor {
    Module: string = 'ReturnToVendor';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Return To Vendor', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-ReturnToVendorForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');
        //$form.find('div[data-datafield="PurchaseOrderId"] fwformfield-text input').focus();
        //$form.find('div[data-datafield="PurchaseOrderId"] input fwformfield-text').focus();
        //$form.find('div[data-displayfield="PurchaseOrderNumber"] input').focus();

        let date = new Date(),
            currentDate = date.toLocaleString(),
            currentTime = date.toLocaleTimeString();

        FwFormField.setValueByDataField($form, 'Date', currentDate);
        FwFormField.setValueByDataField($form, 'Time', currentTime);

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'PurchaseOrderId', parentmoduleinfo.PurchaseOrderId, parentmoduleinfo.PurchaseOrderNumber); 
            $form.find('[data-datafield="PurchaseOrderId"] input').change();
        }

        this.getItems($form);
        this.events($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getItems($form) {
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));

            let purchaseOrderId = $tr.find('[data-browsedatafield="PurchaseOrderId"]').attr('data-originalvalue');

            FwFormField.setValueByDataField($form, 'VendorId', $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Vendor"]').attr('data-originalvalue')); 
            //FwFormField.setValueByDataField($form, 'ReferenceNumber', $tr.find('[data-browsedatafield="ReferenceNumber"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));

            let request = {
                PurchaseOrderId: purchaseOrderId
            }

            FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/startreturncontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                let contractId = response.ContractId,
                    $pOReturnItemGridControl: any,
                    max = 9999;

                FwFormField.setValueByDataField($form, 'ContractId', contractId);

                $pOReturnItemGridControl = $form.find('div[data-name="POReturnItemGrid"]');
                $pOReturnItemGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        ContractId: contractId,
                        PurchaseOrderId: purchaseOrderId
                    }
                    request.pagesize = max;
                })
                FwBrowse.search($pOReturnItemGridControl);
            }, null, null);
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //console.log('po', $form.find('div[data-datafield="PurchaseOrderId"] input'))
        //$form.find('div[data-datafield="PurchaseOrderId"] fwformfield-control input').focus();
        //$form.find('div[data-datafield="PurchaseOrderId"] fwformfield-control ').focus();
        //$form.find('div[data-datafield="PurchaseOrderId"] input').focus();
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form:any) {
        let $pOReturnItemGrid: any,
            $pOReturnItemGridControl: any;

        $pOReturnItemGrid = $form.find('div[data-grid="POReturnItemGrid"]');
        $pOReturnItemGridControl = jQuery(jQuery('#tmpl-grids-POReturnItemGridBrowse').html());
        $pOReturnItemGrid.empty().append($pOReturnItemGridControl);
        $pOReturnItemGridControl.data('ondatabind', function (request) {
        
        })
        FwBrowse.init($pOReturnItemGridControl);
        FwBrowse.renderRuntimeHtml($pOReturnItemGridControl);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        // Create Contract
        $form.find('.createcontract').on('click', e => {
            let date = new Date(),
                currentDate = date.toLocaleString(),
                currentTime = date.toLocaleTimeString();
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorder/completereturncontract/${contractId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = contractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);

                    $form.find('.fwformfield').not('[data-type="date"], [data-type="time"]').find('input').val('');
                    let $pOReturnItemGridControl = $form.find('div[data-name="POReturnItemGrid"]');
                    $pOReturnItemGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: contractId,
                            PurchaseOrderId: ''
                        }
                    })
                    FwBrowse.search($pOReturnItemGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
                FwFormField.setValueByDataField($form, 'Date', currentDate);
                FwFormField.setValueByDataField($form, 'Time', currentTime);
            }, null, $form);
        });

        // Select None
        $form.find('.selectnone').on('click', e => {
            let request: any = {}, quantity;
            const $returnItemsGridControl = $form.find('div[data-name="POReturnItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            const purchaseOrderItemIdColumn: any = $form.find('.POReturnItemGrid [data-browsedatafield="PurchaseOrderItemId"]');
            const QuantityColumn: any = $form.find('.POReturnItemGrid [data-browsedatafield="Quantity"]');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;

            for (let i = 1; i < purchaseOrderItemIdColumn.length; i++) {

                if (QuantityColumn.eq(i).attr('data-originalvalue') != 0) {
                    request.PurchaseOrderItemId = purchaseOrderItemIdColumn.eq(i).attr('data-originalvalue')
                    quantity = QuantityColumn.eq(i).attr('data-originalvalue');
                    request.Quantity = -quantity
                    FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreturnitem/returnitems`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form);
                }
            }
            setTimeout(() => { FwBrowse.search($returnItemsGridControl); }, 1500);
        });
        // Select All
        $form.find('.selectall').on('click', e => {
            let request: any = {};
            const $returnItemsGridControl = $form.find('div[data-name="POReturnItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            const purchaseOrderItemIdColumn: any = $form.find('.POReturnItemGrid [data-browsedatafield="PurchaseOrderItemId"]');
            const QuantityReturnableColumn: any = $form.find('.POReturnItemGrid [data-browsedatafield="QuantityReturnable"]');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            for (let i = 1; i < purchaseOrderItemIdColumn.length; i++) {
                if (QuantityReturnableColumn.eq(i).attr('data-originalvalue') != 0) {
                    request.PurchaseOrderItemId = purchaseOrderItemIdColumn.eq(i).attr('data-originalvalue')
                    request.Quantity = QuantityReturnableColumn.eq(i).attr('data-originalvalue')
                    FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreturnitem/returnitems`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form);
                }
            }
            setTimeout(() => { FwBrowse.search($returnItemsGridControl); }, 1500);
        });
    }
    //----------------------------------------------------------------------------------------------
}
var ReturnToVendorController = new ReturnToVendor();