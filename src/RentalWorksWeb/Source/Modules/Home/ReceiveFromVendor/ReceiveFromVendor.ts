routes.push({ pattern: /^module\/receivefromvendor$/, action: function (match: RegExpExecArray) { return ReceiveFromVendorController.getModuleScreen(); } });

class ReceiveFromVendor {
    Module: string = 'ReceiveFromVendor';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Receive From Vendor', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-ReceiveFromVendorForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

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
    };
    //----------------------------------------------------------------------------------------------
    getItems($form) {
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));

            let purchaseOrderId = $tr.find('[data-browsedatafield="PurchaseOrderId"]').attr('data-originalvalue');

            FwFormField.setValueByDataField($form, 'VendorId', $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Vendor"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'ReferenceNumber', $tr.find('[data-browsedatafield="ReferenceNumber"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));

            let request = {
                PurchaseOrderId: purchaseOrderId
            }

            FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/startreceivecontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                let contractId = response.ContractId,
                    $receiveItemsGridControl: any,
                    max = 9999;

                FwFormField.setValueByDataField($form, 'ContractId', contractId);

                $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
                $receiveItemsGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        ContractId: contractId
                        , PurchaseOrderId: purchaseOrderId
                    }
                    request.pagesize = max;
                })
                FwBrowse.search($receiveItemsGridControl);
            }, null, null);
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let $receiveItemsGrid: any,
            $receiveItemsGridControl: any;

        $receiveItemsGrid = $form.find('div[data-grid="POReceiveItemGrid"]');
        $receiveItemsGridControl = jQuery(jQuery('#tmpl-grids-POReceiveItemGridBrowse').html());
        $receiveItemsGrid.empty().append($receiveItemsGridControl);
        $receiveItemsGridControl.data('ondatabind', function (request) {

        })
        FwBrowse.init($receiveItemsGridControl);
        FwBrowse.renderRuntimeHtml($receiveItemsGridControl);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        // Create Contract
        $form.find('.createcontract').on('click', e => {
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            let automaticallyCreateCheckOut = FwFormField.getValueByDataField($form, 'AutomaticallyCreateCheckOut');
            let date = new Date(),
                currentDate = date.toLocaleString(),
                currentTime = date.toLocaleTimeString();

            let requestBody: any = {};
            if (automaticallyCreateCheckOut == 'T') {
                requestBody = {
                    CreateOutContracts: true
                }
            }
            FwAppData.apiMethod(true, 'POST', "api/v1/purchaseorder/completereceivecontract/" + contractId, requestBody, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    for (let i = 0; i < response.length; i++) {
                        let contractInfo: any = {}, $contractForm;
                        contractInfo.ContractId = response[i].ContractId;
                        $contractForm = ContractController.loadForm(contractInfo);
                        FwModule.openSubModuleTab($form, $contractForm);
                    }
                    $form.find('.fwformfield').not('[data-type="date"], [data-type="time"]').find('input').val('');
                    let $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
                    $receiveItemsGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: contractId
                            , PurchaseOrderId: ''
                        }
                    })
                    FwBrowse.search($receiveItemsGridControl);
                    FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
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
            const $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            const purchaseOrderItemIdColumn: any = $form.find('.POReceiveItemGrid [data-browsedatafield="PurchaseOrderItemId"]');
            const QuantityColumn: any = $form.find('.POReceiveItemGrid [data-browsedatafield="Quantity"]');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;

            for (let i = 1; i < purchaseOrderItemIdColumn.length; i++) {

                if (QuantityColumn.eq(i).attr('data-originalvalue') != 0) {
                    request.PurchaseOrderItemId = purchaseOrderItemIdColumn.eq(i).attr('data-originalvalue')
                    quantity = QuantityColumn.eq(i).attr('data-originalvalue');
                    request.Quantity = -quantity
                    FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreceiveitem/receiveitems`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form);
                }
            }
            setTimeout(() => { FwBrowse.search($receiveItemsGridControl); }, 1500);
        });
        // Select All
        $form.find('.selectall').on('click', e => {
            let request: any = {};
            const $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            const purchaseOrderItemIdColumn: any = $form.find('.POReceiveItemGrid [data-browsedatafield="PurchaseOrderItemId"]');
            const QuantityRemainingColumn: any = $form.find('.POReceiveItemGrid [data-browsedatafield="QuantityRemaining"]');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            for (let i = 1; i < purchaseOrderItemIdColumn.length; i++) {
                if (QuantityRemainingColumn.eq(i).attr('data-originalvalue') != 0) {
                    request.PurchaseOrderItemId = purchaseOrderItemIdColumn.eq(i).attr('data-originalvalue')
                    request.Quantity = QuantityRemainingColumn.eq(i).attr('data-originalvalue')
                    FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreceiveitem/receiveitems`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form);
                }
            }
            setTimeout(() => { FwBrowse.search($receiveItemsGridControl); }, 1500);
        });
        //Hide/Show Options
        var $optionToggle = $form.find('.optiontoggle');
        $form.find('.options').toggle();
        $optionToggle.on('click', function () {
            $form.find('.options').toggle();
        });
    };
    //----------------------------------------------------------------------------------------------
}
var ReceiveFromVendorController = new ReceiveFromVendor();