routes.push({ pattern: /^module\/fillcontainer$/, action: function (match: RegExpExecArray) { return FillContainerController.getModuleScreen(); } });

class FillContainer extends StagingCheckoutBase {
    Module: string = 'FillContainer';
    caption: string = Constants.Modules.Container.children.FillContainer.caption;
    nav: string = Constants.Modules.Container.children.FillContainer.nav;
    id: string = Constants.Modules.Container.children.FillContainer.id;
    showAddItemToOrder: boolean = false;
    contractId: string;
    isPendingItemGridView: boolean = false;
    Type: string = 'ContainerItem';
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    afterOpenForm($form: JQuery) {
        $form.find('[data-datafield="QuantitySub"]').hide();
        $form.find('[data-datafield="QuantityOut"] .fwformfield-caption').text('In Container');
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        super.events($form);

        //"Instantiate Container" events
        $form.find('[data-datafield="BarCode"]').on('keydown', e => {
            if (e.which === 13 || e.which === 9) {
                const barcode = FwFormField.getValueByDataField($form, 'BarCode');
                const $errorMsg = $form.find('.error-msg');
                FwAppData.apiMethod(true, 'GET', `api/v1/item/bybarcode?barcode=${barcode}`, null, FwServices.defaultTimeout,
                    response => {
                        if (response.success) {
                            response = response.Item;
                            //check if warehouse matches
                            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
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
                                const $confirmation = FwConfirmation.renderConfirmation(`Create a New Container?`, '');
                                $confirmation.find('.fwconfirmationbox').css('width', '900px');
                                const html = ` <div class="flexpage fwform" style="overflow:hidden;" data-controller="FillContainerController">
                                                <div class="flexrow">
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code" data-datafield="BarCode" data-enabled="false" style="flex:0 1 200px;"></div>
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-enabled="false" style="flex:0 1 200px;"></div>
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:0 1 400px;"></div>
                                                </div>
                                                <div class="flexrow">
                                                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container I-Code" data-datafield="ContainerId" data-displayfield="ICode" data-validationname="ContainerValidation" style="flex:0 1 200px;"></div>
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
                                FwAppData.apiMethod(true, 'POST', `api/v1/rentalinventory/browse`, request, FwServices.defaultTimeout,
                                    response => {
                                        if (response.TotalRows === 1) {
                                            const containerIdIndex = response.ColumnIndex.ContainerId;
                                            const iCodeIndex = response.ColumnIndex.ICode;
                                            const descriptionIndex = response.ColumnIndex.Description;
                                            FwFormField.setValueByDataField($confirmation, 'ContainerId', response.Rows[0][containerIdIndex], response.Rows[0][iCodeIndex]);
                                            FwFormField.setValueByDataField($confirmation, 'ContainerDescription', response.Rows[0][descriptionIndex]);
                                        }
                                    }, ex => {
                                        FwFunc.showError(ex);
                                    }, $confirmation.find('.fwconfirmationbox'));

                                $confirmation.find('[data-datafield="ContainerId"]').data('onchange', $tr => {
                                    $confirmation.find('[data-datafield="ContainerDescription"] input').val($tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
                                });

                                $createContainer.on('click', e => {
                                    const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                                    const request: any = {};
                                    request.ContainerId = FwFormField.getValueByDataField($confirmation, 'ContainerId');
                                    request.ItemId = itemId;
                                    request.WarehouseId = warehouse.warehouseid;
                                    FwAppData.apiMethod(true, 'POST', `api/v1/containeritem/instantiatecontainer`, request, FwServices.defaultTimeout,
                                        response => {
                                            if (response.success) {
                                                FwFormField.disable($form.find('[data-datafield="BarCode"]'));
                                                FwFormField.setValueByDataField($form, 'OrderId', response.ContainerItemId);
                                                FwFormField.setValueByDataField($form, 'ContainerItemId', response.ContainerItemId, barcode, true);
                                                FwNotification.renderNotification('SUCCESS', 'Successfully Created New Container.');
                                                $form.find('[data-datafield="Code"] input').focus();
                                            } else {
                                                $errorMsg.html(`<div><span>${response.msg}</span></div>`);
                                            }
                                            FwConfirmation.destroyConfirmation($confirmation);
                                        }, ex => {
                                            FwFunc.showError(ex);
                                        }, $confirmation.find('.fwconfirmationbox'));
                                });
                            } else {
                                FwFormField.disable($form.find('[data-datafield="BarCode"]'));
                                //FwFormField.setValueByDataField($form, 'OrderId', response.OrderId);
                                FwFormField.setValueByDataField($form, 'OrderId', response.ContainerItemId);
                                FwFormField.setValueByDataField($form, 'ContainerItemId', response.ContainerItemId, barcode, true);
                                FwFormField.setValueByDataField($form, 'ContainerNumber', response.ContainerNumber);
                            }
                        } else {
                            $errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        }
                    }, null, $form);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
}
var FillContainerController = new FillContainer();