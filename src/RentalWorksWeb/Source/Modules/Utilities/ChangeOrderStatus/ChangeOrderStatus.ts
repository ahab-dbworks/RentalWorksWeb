routes.push({ pattern: /^module\/changeorderstatus$/, action: function (match: RegExpExecArray) { return ChangeOrderStatusController.getModuleScreen(); } });

class ChangeOrderStatus {
    Module: string = 'ChangeOrderStatus';
    apiurl: string = 'api/v1/changeorderstatus';
    caption: string = Constants.Modules.Utilities.children.ChangeOrderStatus.caption;
    nav: string = Constants.Modules.Utilities.children.ChangeOrderStatus.nav;
    id: string = Constants.Modules.Utilities.children.ChangeOrderStatus.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
            this.afterLoad($form);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('.change-btn').on('click', $tr => {
            //  if (FwModule.validateForm($form)) {
            const request: any = {};
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.NewStatus = FwFormField.getTextByDataField($form, 'OrderStatus');

            FwAppData.apiMethod(true, 'POST', 'api/v1/changeorderstatus/changestatus', request, FwServices.defaultTimeout, response => {
                if (response.success) {
                    FwNotification.renderNotification('SUCCESS', 'Status Updated');
                    $form.find('.fwformfield input').val('');
                    FwModule.refreshForm($form);
                } else {
                }
            }, ex => FwFunc.showError(ex), $form);
            // }
        });

        // Set Description from I-Code validation
        $form.find('[data-datafield="OrderId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="OrderDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        $form.find('.itemid[data-displayfield="BarCode"] input').focus();
    }
    //----------------------------------------------------------------------------------------------
    //beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
    //    const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
    //    switch (datafield) {
    //        case 'ItemId':
    //            request.uniqueids = {
    //                WarehouseId: warehouse.warehouseid
    //            };
    //            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateitem`);
    //            break;
    //        case 'InventoryId':
    //            request.uniqueids = {
    //                WarehouseId: warehouse.warehouseid,
    //                TrackedBy: 'QUANTITY',
    //            };
    //            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
    //    }
    //}
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
            <div id="changeorderstatusform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Change Order Status" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ChangeOrderStatusController">
              <div id="changeorderstatusform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs"></div>
                <div class="tabpages">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Change Status" style="max-width:700px">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order" data-datafield="OrderId" data-displayfield="Order" data-validationname="OrderValidation" style="flex:0 1 200px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Description" data-datafield="OrderDescription" data-enabled="false" style="flex:0 1 400px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order Status" data-datafield="OrderStatus" data-displayfield="OrderStatus" data-validationname="OrderStatusValidation" data-required="true" style="flex:0 1 200px;"></div>
                      </div>
                      <div class="flexrow">
                        <div class="fwformcontrol change-btn" data-type="button" style="flex:0 1 140px;margin:15px 0 0 10px;text-align:center;">Update Status</div>
                      </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var ChangeOrderStatusController = new ChangeOrderStatus();