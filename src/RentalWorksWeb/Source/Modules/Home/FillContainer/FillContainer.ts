﻿routes.push({ pattern: /^module\/fillcontainer$/, action: function (match: RegExpExecArray) { return FillContainerController.getModuleScreen(); } });
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
                        //check if warehouse matches
                        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                        const $errorMsg = $form.find('.error-msg');
                        if (response.WarehouseId !== warehouse.warehouseid) {
                            $errorMsg.html(`<div><span>Bar Code ${barcode} is from the ${response.Warehouse} Warehouse.</span></div>`);
                            return;
                        }

                        //must match one condition
                        const statusType = response.StatusType;
                        let conditionMet = false;
                        if (statusType === "IN" || statusType === "INCONTAINER" || (statusType === "STAGED" && response.ContainerItemId === response.OrderId)) conditionMet = true;
                        if (!conditionMet) {
                            $errorMsg.html(`<div><span>Bar Code ${barcode} is currently ${response.InventoryStatus}.</span></div>`);
                            return;
                        }

                        //create confirmation popup
                        if (response.ContainerItemId === "") {
                            $errorMsg.html('');
                            const inventoryId = response.InventoryId;
                            const itemId = response.ItemId;
                            const $confirmation = FwConfirmation.renderConfirmation(`Instantiate a New Container?`, '');
                            $confirmation.find('.fwconfirmationbox').css('width', '900px');
                            const html = ` <div class="flexpage fwform" style="overflow:hidden;" data-controller="FillContainerController">
                                                <div class="flexrow">
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code" data-datafield="BarCode" data-enabled="false" style="flex:0 1 200px;"></div>
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-enabled="false" style="flex:0 1 200px;"></div>
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:0 1 400px;"></div>
                                                </div>
                                                <div class="flexrow">
                                                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container I-Code" data-datafield="ContainerId" data-displayfield="ICode" data-formbeforevalidate="beforeValidate" data-validationname="ContainerValidation" style="flex:0 1 200px;"></div>
                                                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Container Description" data-datafield="ContainerDescription" data-enabled="false" style="flex:0 1 400px;"></div>
                                                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="InventoryId" data-datafield="InventoryId" style="display:none;"></div>
                                                </div>
                                            </div>`;
                            FwConfirmation.addControls($confirmation, html);
                            FwFormField.setValueByDataField($confirmation, 'InventoryId', inventoryId);
                            FwFormField.setValueByDataField($confirmation, 'BarCode', response.BarCode);
                            FwFormField.setValueByDataField($confirmation, 'ICode', response.ICode);
                            FwFormField.setValueByDataField($confirmation, 'Description', response.Description);

                            const $createContainer = FwConfirmation.addButton($confirmation, 'Create Container', false);
                            FwConfirmation.addButton($confirmation, 'Cancel');

                            //populate fields if the validation only has one result
                            const request: any = {};
                            request.uniqueids = {
                                ScannableInventoryId: inventoryId
                            };
                            FwAppData.apiMethod(true, 'POST', `api/v1/container/browse`, request, FwServices.defaultTimeout,
                                response => {
                                    if (response.TotalRows === 1) {
                                        FwFormField.setValueByDataField($confirmation, 'ContainerId', response.ContainerId, response.ICode);
                                        FwFormField.setValueByDataField($confirmation, 'Description', response.Description);
                                    }
                                }, ex => {
                                    FwFunc.showError(ex);
                                }, $confirmation.find('.fwconfirmationbox'));

                            $confirmation.find('[data-datafield="ContainerId"]').data('onchange', $tr => {
                                $confirmation.find('[data-datafield="ContainerDescription"] input').val($tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
                            });

                            $createContainer.on('click', e => {
                                const request: any = {};
                                request.ContainerId = FwFormField.getValueByDataField($confirmation, 'ContainerId');
                                request.ItemId = itemId;
                                FwAppData.apiMethod(true, 'POST', `api/v1/containeritem/instantiatecontainer`, request, FwServices.defaultTimeout,
                                    response => {
                                        FwFormField.disable($form.find('[data-datafield="BarCode"]'));
                                        FwFormField.setValueByDataField($form, 'ContainerItemId', response.ContainerItemId, barcode, true);
                                        FwNotification.renderNotification('SUCCESS', 'Successfully Instantiated New Container.');
                                        FwConfirmation.destroyConfirmation($confirmation);
                                        $form.find('[data-datafield="Code"] input').focus();
                                    }, ex => {
                                        FwFunc.showError(ex);
                                    }, $confirmation.find('.fwconfirmationbox'));
                            });
                        } else {
                            FwFormField.disable($form.find('[data-datafield="BarCode"]'));
                            FwFormField.setValueByDataField($form, 'ContainerItemId', response.ContainerItemId, barcode, true);
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