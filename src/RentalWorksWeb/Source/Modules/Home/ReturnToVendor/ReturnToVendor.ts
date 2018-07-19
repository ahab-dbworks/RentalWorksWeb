routes.push({ pattern: /^module\/returntovendor$/, action: function (match: RegExpExecArray) { return ReturnToVendorController.getModuleScreen(); } });

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

        let date = new Date(),
            currentDate = date.toLocaleString(),
            currentTime = date.toLocaleTimeString();

        FwFormField.setValueByDataField($form, 'Date', currentDate);
        FwFormField.setValueByDataField($form, 'Time', currentTime);

        $form.find('.createcontract').on('click', e => {
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
            }, null, $form);
        });

        this.getItems($form);

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'PurchaseOrderId', parentmoduleinfo.PurchaseOrderId, parentmoduleinfo.PurchaseOrderNumber); 
            $form.find('[data-datafield="PurchaseOrderId"] input').change();
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getItems($form) {
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
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

}
var ReturnToVendorController = new ReturnToVendor();