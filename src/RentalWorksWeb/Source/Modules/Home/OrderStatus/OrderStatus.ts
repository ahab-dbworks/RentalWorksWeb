declare var FwFormField;

class OrderStatus {
    Module: string;

    constructor() {
        this.Module = 'OrderStatus';

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
        
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm() {
        var $form = this.openForm('EDIT');
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
 
}
(window as any).OrderStatusController = new OrderStatus();