routes.push({ pattern: /^module\/fillcontainer$/, action: function (match: RegExpExecArray) { return FillContainerController.getModuleScreen(); } });
class FillContainer extends StagingCheckoutBase {
    Module: string = 'FillContainer';
    caption: string = 'Fill Container';
    nav: string = 'module/fillcontainer';
    id: string = '0F1050FB-48DF-41D7-A969-37300B81B7B5';
    showAddItemToOrder: boolean = false;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    contractId: string;
    isPendingItemGridView: boolean = false;
    Type: string = 'ContainerItem';
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        super.events($form);

        //instantiate container
        $form.find('[data-datafield="BarCode"]').on('keydown', e => {
            if (e.which == 13) {
                const barcode = FwFormField.getValueByDataField($form, 'BarCode');
                FwAppData.apiMethod(true, 'GET', `api/v1/item/bybarcode?barcode=${barcode}`, null, FwServices.defaultTimeout,
                    response => {
                        if (response.ContainerItemId === "") {
                            const inventoryId = response.InventoryId;
                            const itemId = response.ItemId;
                            const $confirmation = FwConfirmation.renderConfirmation(`Instantiate a New Container?`, '');
                            $confirmation.find('.fwconfirmationbox').css('width', '650px');
                            const html = `<div class="flexrow fwform" style="overflow:hidden;" data-controller="FillContainerController">
                                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container I-Code" data-datafield="ContainerId" data-displayfield="ICode" data-formbeforevalidate="beforeValidate" data-validationname="ContainerValidation" style="flex:0 1 200px;"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:0 1 400px;"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="InventoryId" data-datafield="InventoryId" style="display:none;"></div>
                                          </div>`;
                            FwConfirmation.addControls($confirmation, html);
                            FwFormField.setValueByDataField($confirmation, 'InventoryId', inventoryId);

                            const $createContainer = FwConfirmation.addButton($confirmation, 'Create Container', false);
                            FwConfirmation.addButton($confirmation, 'Cancel');

                            $confirmation.find('[data-datafield="ContainerId"]').data('onchange', $tr => {
                                $confirmation.find('[data-datafield="Description"] input').val($tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
                            });

                            $createContainer.on('click', e => {
                                const request: any = {};
                                request.ContainerId = FwFormField.getValueByDataField($confirmation, 'ContainerId');
                                request.ItemId = itemId;
                                FwAppData.apiMethod(true, 'POST', `api/v1/containeritem/instantiatecontainer`, request, FwServices.defaultTimeout,
                                    response => {
                                        FwFormField.setValueByDataField($form, 'ContainerItemId', response.ContainerItemId, '', true);
                                    }, ex => {
                                        FwFunc.showError(ex);
                                    }, $form);
                            });
                        }
                }, ex => {
                    FwFunc.showError(ex);
                 
                }, $form);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
}
var FillContainerController = new FillContainer();