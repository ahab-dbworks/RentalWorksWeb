declare var FwFormField;

class OrderStatus {
    Module: string;
    //apiurl: string;

    constructor() {
        this.Module = 'OrderStatus';
        //this.apiurl = 'api/v1/order';

    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.loadForm();

        screen.load = function () {
            FwModule.openModuleTab($form, 'Order Status', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-OrderStatusForm').html());
        $form = FwModule.openForm($form, mode);

        this.getOrder($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm() {
        var $form = this.openForm('EDIT');
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getOrder($form: JQuery): void {
        var order = $form.find('[data-datafield="OrderId"]');
        order.on('change', function () {
            try {
                var orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();
                FwAppData.apiMethod(true, 'GET', "api/v1/order/" + orderId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Deal', response.Deal);
                    FwFormField.setValueByDataField($form, 'Status', response.Status);
                    FwFormField.setValueByDataField($form, 'Warehouse', response.Warehouse);
                    FwFormField.setValueByDataField($form, 'PickDate', response.PickDate);
                    FwFormField.setValueByDataField($form, 'PickTime', response.PickTime);
                    FwFormField.setValueByDataField($form, 'EstimatedStartDate', response.EstimatedStartDate);
                    FwFormField.setValueByDataField($form, 'EstimatedStartTime', response.EstimatedStartTime);
                    FwFormField.setValueByDataField($form, 'EstimatedStopDate', response.EstimatedStopDate);
                    FwFormField.setValueByDataField($form, 'EstimatedStopTime', response.EstimatedStopTime);
                });

                var $orderStatusSummaryGridControl: any;
                $orderStatusSummaryGridControl = $form.find('[data-name="OrderStatusSummaryGrid"]');
                $orderStatusSummaryGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId
                    }
                })
                FwBrowse.search($orderStatusSummaryGridControl);
            }
            catch(ex) {
                    FwFunc.showError(ex);
                }
            });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        var $orderStatusSummaryGrid: any;
        var $orderStatusSummaryGridControl: any;
        var orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();

        $orderStatusSummaryGrid = $form.find('div[data-grid="OrderStatusSummaryGrid"]');
        $orderStatusSummaryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusSummaryGridBrowse').html());
        $orderStatusSummaryGrid.empty().append($orderStatusSummaryGridControl);
        $orderStatusSummaryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: orderId
            };
        })
   
        FwBrowse.init($orderStatusSummaryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusSummaryGridControl);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $orderStatusSummaryGrid: any;

        $orderStatusSummaryGrid = $form.find('[data-name="OrderStatusSummaryGrid"]');
        FwBrowse.search($orderStatusSummaryGrid);

    }
}
(window as any).OrderStatusController = new OrderStatus();