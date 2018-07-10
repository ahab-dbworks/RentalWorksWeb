routes.push({ pattern: /^module\/receivefromvendor$/, action: function (match: RegExpExecArray) { return ReceiveFromVendorController.getModuleScreen(); } });

class ReceiveFromVendor {
    Module: string;

    constructor() {
        this.Module = 'ReceiveFromVendor';
    }
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

        $form.find('.createcontract').on('click', e => {
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            FwAppData.apiMethod(true, 'POST', "api/v1/purchaseorder/completereceivecontract/" + contractId, null, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = contractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, null);
        });

        this.getItems($form);

        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="PurchaseOrderId"] input.fwformfield-value').val(parentmoduleinfo.PurchaseOrderId);
            $form.find('div[data-datafield="PurchaseOrderId"] input.fwformfield-text').val(parentmoduleinfo.PurchaseOrderNumber);
            $form.find('[data-datafield="PurchaseOrderId"] input').change();
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getItems($form) {
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            let purchaseOrderId = $tr.find('[data-browsedatafield="PurchaseOrderId"]').attr('data-originalvalue');

            FwFormField.setValueByDataField($form, 'Vendor', $tr.find('[data-browsedatafield="Vendor"]').attr('data-originalvalue'));
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
    renderGrids($form:any) {
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
    }
    //----------------------------------------------------------------------------------------------

}
var ReceiveFromVendorController = new ReceiveFromVendor();