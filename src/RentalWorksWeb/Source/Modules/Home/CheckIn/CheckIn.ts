//routes.push({ pattern: /^module\/checkin$/, action: function (match: RegExpExecArray) { return CheckInController.getModuleScreen(); } });

class CheckIn {
    Module: string = 'CheckIn';
    caption: string = 'Check-In';
    nav: string = 'module/checkin';
    id: string = '77317E53-25A2-4C12-8DAD-7541F9A09436';
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="OrderId"] input.fwformfield-value').val(parentmoduleinfo.OrderId);
            $form.find('div[data-datafield="OrderId"] input.fwformfield-text').val(parentmoduleinfo.OrderNumber);
            jQuery($form.find('[data-datafield="OrderId"] input')).trigger('change');
            $form.attr('data-showsuspendedsessions', 'false');
        }
        this.getSoundUrls($form);
        this.events($form);
        this.getSuspendedSessions($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSuspendedSessions($form) {
        let showSuspendedSessions = $form.attr('data-showsuspendedsessions');
        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', 'api/v1/checkin/suspendedsessionsexist', null, FwServices.defaultTimeout, function onSuccess(response) {
                $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
            }, null, $form);

            $form.on('click', '.suspendedsession', e => {
                let html = `<div>
                 <div style="background-color:white; padding-right:10px; text-align:right;" class="close-modal"><i style="cursor:pointer;" class="material-icons">clear</i></div>
<div id="suspendedSessions" style="max-width:1400px; max-height:750px; overflow:auto;"></div>
            </div>`;

                let $popup = FwPopup.renderPopup(jQuery(html), { ismodal: true });

                let $browse = SuspendedSessionController.openBrowse();
                let officeLocationId = JSON.parse(sessionStorage.getItem('location'));
                officeLocationId = officeLocationId.locationid;
                $browse.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OfficeLocationId: officeLocationId
                        , SessionType: 'IN'
                        , OrderType: 'O'
                    }
                });

                FwPopup.showPopup($popup);
                jQuery('#suspendedSessions').append($browse);
                FwBrowse.search($browse);

                $popup.find('.close-modal > i').one('click', function (e) {
                    FwPopup.destroyPopup($popup);
                    jQuery(document).find('.fwpopup').off('click');
                    jQuery(document).off('keydown');
                });

                $browse.on('dblclick', 'tr.viewmode', e => {
                    let $this = jQuery(e.currentTarget);
                    let id = $this.find(`[data-browsedatafield="OrderId"]`).attr('data-originalvalue');
                    let orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
                    let dealId = $this.find(`[data-browsedatafield="DealId"]`).attr('data-originalvalue');
                    let dealNumber = $this.find(`[data-browsedatafield="DealNumber"]`).attr('data-originalvalue');
                    if (dealId !== "") {
                        FwFormField.setValueByDataField($form, 'DealId', dealId, dealNumber);
                    }
                    FwFormField.setValueByDataField($form, 'OrderId', id, orderNumber);
                    FwPopup.destroyPopup($popup);
                    $form.find('[data-datafield="OrderId"] input').change();
                    $form.find('.suspendedsession').hide();
                });

            });
        }
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let $checkedInItemsGrid: any,
            $checkedInItemsGridControl: any;

        $checkedInItemsGrid = $form.find('div[data-grid="CheckedInItemGrid"]');
        $checkedInItemsGridControl = jQuery(jQuery('#tmpl-grids-CheckedInItemGridBrowse').html());
        $checkedInItemsGrid.empty().append($checkedInItemsGridControl);
        $checkedInItemsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkedInItemsGridControl);
        FwBrowse.renderRuntimeHtml($checkedInItemsGridControl);

        let $checkInExceptionGrid: any,
            $checkInExceptionGridControl: any;

        $checkInExceptionGrid = $form.find('div[data-grid="CheckInExceptionGrid"]');
        $checkInExceptionGridControl = jQuery(jQuery('#tmpl-grids-CheckInExceptionGridBrowse').html());
        $checkInExceptionGrid.empty().append($checkInExceptionGridControl);
        $checkInExceptionGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInExceptionGridControl);
        FwBrowse.renderRuntimeHtml($checkInExceptionGridControl);

        let $checkInOrderGrid: any,
            $checkInOrderGridControl: any;

        $checkInOrderGrid = $form.find('div[data-grid="CheckInOrderGrid"]');
        $checkInOrderGridControl = jQuery(jQuery('#tmpl-grids-CheckInOrderGridBrowse').html());
        $checkInOrderGrid.empty().append($checkInOrderGridControl);
        $checkInOrderGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInOrderGridControl);
        FwBrowse.renderRuntimeHtml($checkInOrderGridControl);

        let $checkInSwapGrid: any,
            $checkInSwapGridControl: any;

        $checkInSwapGrid = $form.find('div[data-grid="CheckInSwapGrid"]');
        $checkInSwapGridControl = jQuery(jQuery('#tmpl-grids-CheckInSwapGridBrowse').html());
        $checkInSwapGrid.empty().append($checkInSwapGridControl);
        $checkInSwapGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkInSwapGridControl);
        FwBrowse.renderRuntimeHtml($checkInSwapGridControl);

        let $checkInQuantityItemsGrid: any,
            $checkInQuantityItemsGridControl: any;
        $checkInQuantityItemsGrid = $form.find('div[data-grid="CheckInQuantityItemsGrid"]');
        $checkInQuantityItemsGridControl = jQuery(jQuery('#tmpl-grids-CheckInQuantityItemsGridBrowse').html());
        $checkInQuantityItemsGrid.empty().append($checkInQuantityItemsGridControl);
        $checkInQuantityItemsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                AllOrdersForDeal: FwFormField.getValueByDataField($form, 'AllOrdersForDeal'),
                OrderId: FwFormField.getValueByDataField($form, 'SpecificOrderId'),
                OutOnly: FwFormField.getValueByDataField($form, 'ShowQuantityOut')
            }
        });
        FwBrowse.init($checkInQuantityItemsGridControl);
        FwBrowse.renderRuntimeHtml($checkInQuantityItemsGridControl);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse: any, $form: any, request: any) {
        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.miscfields = {
            CheckIn: true
            , CheckInWarehouseId: warehouseId
        }
    };
    //----------------------------------------------------------------------------------------------
    beforeValidateSpecificOrder($browse: any, $form: any, request: any) {
        let dealId = FwFormField.getValueByDataField($form, 'DealId');
        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.uniqueids = {
            DealId: dealId
        }
        request.miscfields = {
            CheckIn: true,
            CheckInWarehouseId: warehouseId
        }
    }
    //----------------------------------------------------------------------------------------------
    getSoundUrls($form): void {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let errorMsg, errorSound, successSound, department, self = this;
        department = JSON.parse(sessionStorage.getItem('department'));
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        errorMsg = $form.find('.error-msg:not(.qty)');
        const $checkInQuantityItemsGridControl = $form.find('div[data-name="CheckInQuantityItemsGrid"]');
        const allActiveOrders = $form.find('[data-datafield="AllOrdersForDeal"] input');
        const specificOrder = $form.find('[data-datafield="SpecificOrder"] input');
        const specificOrderValidation = $form.find('div[data-datafield="SpecificOrderId"]');

        //Default Department
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        //Order selection
        $form.find('[data-datafield="OrderId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'DealId', $tr.find('[data-browsedatafield="DealId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Deal"]').attr('data-originalvalue'));
            FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));

            let request: any = {};
            request = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
                , OrderId: FwFormField.getValueByDataField($form, 'OrderId')
                , DepartmentId: FwFormField.getValueByDataField($form, 'DepartmentId')
            }
            FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/startcheckincontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                $form.find('[data-datafield="BarCode"] input').focus();
            }, null, null);

            $form.find('.suspendedsession').hide();
        });
        //Deal selection
        $form.find('[data-datafield="DealId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
            let request: any = {};
            request = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
                , DepartmentId: FwFormField.getValueByDataField($form, 'DepartmentId')
            }
            FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/startcheckincontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                $form.find('[data-datafield="BarCode"] input').focus();
            }, null, null);
        });
        //BarCode input
        $form.find('[data-datafield="BarCode"] input').on('keydown', e => {
            if (e.which === 13) {
                errorMsg.html('');
                this.checkInItem($form);
            }
        });
        //Quantity input
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (e.which === 13) {
                errorMsg.html('');
                let type = 'Quantity';
                this.checkInItem($form, type);
            }
        });
        //Add Order to Contract
        $form.find('.addordertocontract').on('click', e => {
            errorMsg.html('');
            let type = 'AddOrderToContract';
            this.checkInItem($form, type);
        });
        //Swap Item
        $form.find('.swapitem').on('click', e => {
            errorMsg.html('');
            let type = 'SwapItem';
            this.checkInItem($form, type);
        });
        //Create Contract
        $form.find('.createcontract').on('click', e => {
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId) {
                FwAppData.apiMethod(true, 'POST', `api/v1/checkin/completecheckincontract/${contractId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = response.ContractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);
                    $form.find('.fwformfield').not('[data-datafield="DepartmentId"]').find('input').val('');
                    $form.find('div[data-name="CheckedInItemGrid"] tr.viewmode').empty();
                    errorMsg.html('');
                    FwFormField.enable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));
                }, null, $form);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order, Deal, BarCode, or I-Code.')
            }
        });
        //Refresh grids on tab click
        $form.find('div.exceptionstab').on('click', e => {
            //Disable clicking Exception tab w/o a ContractId
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId) {
                let $checkInExceptionGridControl = $form.find('div[data-name="CheckInExceptionGrid"]');
                FwBrowse.search($checkInExceptionGridControl);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order, Deal, BarCode, or I-Code.')
            }
        });
        $form.find('div.quantityitemstab').on('click', e => {
            //Disable clicking Quantity Items tab w/o a ContractId
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            let orderId = FwFormField.getValueByDataField($form, 'OrderId');
            if (contractId) {
                FwBrowse.search($checkInQuantityItemsGridControl);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order, Deal, BarCode, or I-Code.')
            }
            if (orderId === '') {
                if ($form.find('.optionlist').css('display') === 'none') {
                    $form.find('.optionlist').toggle();
                }
                allActiveOrders.prop('checked', true);
                FwBrowse.search($checkInQuantityItemsGridControl);
            }
        });
        $form.find('div.orderstab').on('click', e => {
            let $checkInOrderGridControl = $form.find('div[data-name="CheckInOrderGrid"]');
            FwBrowse.search($checkInOrderGridControl);
        });
        $form.find('div.swapitemtab').on('click', e => {
            let $checkInSwapGridControl = $form.find('div[data-name="CheckInSwapGrid"]');
            FwBrowse.search($checkInSwapGridControl);
        });

        //Options button
        $form.find('.optionsbutton').on('click', e => {
            $form.find('.optionlist').toggle();
        });

        //AllOrdersForDeal Checkbox functionality
        allActiveOrders.on('change', e => {
            if (allActiveOrders.prop('checked')) {
                FwFormField.disable(specificOrderValidation);
                specificOrder.prop('checked', false);
                FwBrowse.search($checkInQuantityItemsGridControl);
            } else {
                FwFormField.enable(specificOrderValidation);
                specificOrder.prop('checked', true);
                FwBrowse.search($checkInQuantityItemsGridControl);
            }
        });

        specificOrder.on('change', e => {
            if (specificOrder.prop('checked')) {
                if (specificOrderValidation.find('input').val() !== '') {
                    $checkInQuantityItemsGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                            AllOrdersForDeal: 'F',
                            OrderId: FwFormField.getValueByDataField($form, 'SpecificOrderId'),
                            OutOnly: FwFormField.getValueByDataField($form, 'ShowQuantityOut')
                        }
                    });
                    FwBrowse.search($checkInQuantityItemsGridControl);
                }
                FwFormField.enable(specificOrderValidation);
                allActiveOrders.prop('checked', false);
            } else {
                FwFormField.disable(specificOrderValidation);
                allActiveOrders.prop('checked', true);
                FwBrowse.search($checkInQuantityItemsGridControl);
            }
        });

        specificOrderValidation.data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'SpecificDescription', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $checkInQuantityItemsGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                    AllOrdersForDeal: FwFormField.getValueByDataField($form, 'AllOrdersForDeal'),
                    OrderId: FwFormField.getValueByDataField($form, 'SpecificOrderId'),
                    OutOnly: FwFormField.getValueByDataField($form, 'ShowQuantityOut')
                }
            })
            FwBrowse.search($checkInQuantityItemsGridControl);
        });
        specificOrderValidation.focusout(function () {
            if (jQuery(this).find('input.fwformfield-text').val() === '') {
                FwFormField.setValueByDataField($form, 'SpecificDescription', '');
            }
        })

        $form.find('[data-datafield="ShowQuantityOut"] input').on('change', e => {
            FwBrowse.search($checkInQuantityItemsGridControl);
        });

        //Order Status Button
        $form.find('.orderstatus').on('click', e => {
            let orderInfo: any = {}, $orderStatusForm;
            orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            orderInfo.OrderNumber = FwFormField.getTextByDataField($form, 'OrderId');
            $orderStatusForm = OrderStatusController.openForm('EDIT', orderInfo);
            FwModule.openSubModuleTab($form, $orderStatusForm);
            jQuery('.tab.submodule.active').find('.caption').html('Order Status');
        });
        //Refresh grid on Check-In tab click
        $form.find('.checkintab').on('click', e => {
            const $checkedInItemsGrid = $form.find('div[data-name="CheckedInItemGrid"]');
            FwBrowse.search($checkedInItemsGrid);
        });
    };
    //----------------------------------------------------------------------------------------------
    checkInItem($form, type?: string) {
        let errorSound, successSound, notificationSound;
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        notificationSound = new Audio(this.notificationSoundFileName);

        $form.find('.swapitem').hide();
        let contractId = FwFormField.getValueByDataField($form, 'ContractId');
        let request: any = {};
        request = {
            Code: FwFormField.getValueByDataField($form, 'BarCode')
        }
        if (contractId) {
            request.ContractId = contractId;
        }

        switch (type) {
            case 'Quantity':
                request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
                break;
            case 'AddOrderToContract':
                $form.find('[data-type="section"][data-caption="Check-In"] .fwform-section-title').text('Multi-Order Check-In');
                $form.find('.orderstab').show();
                $form.find('[data-datafield="OrderId"]').parents('.flexcolumn').hide();
                request.AddOrderToContract = true;
                break;
            case 'SwapItem':
                $form.find('.swapitemtab').show();
                request.SwapItem = true;
                break;
        }

        FwAppData.apiMethod(true, 'POST', 'api/v1/checkin/checkinitem', request, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.success) {
                successSound.play();
                FwFormField.setValueByDataField($form, 'ContractId', response.ContractId);
                FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
                FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
                FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
                FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
                FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
                FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
                FwFormField.setValueByDataField($form, 'QuantityIn', response.InventoryStatus.QuantityIn);
                FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
                FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                if (type !== 'SwapItem') {
                    FwFormField.setValueByDataField($form, 'OrderId', response.OrderId, response.OrderNumber);
                    FwFormField.setValueByDataField($form, 'Description', response.OrderDescription);
                }
                FwFormField.disable($form.find('[data-datafield="OrderId"], [data-datafield="DealId"]'));

                let $checkedInItemsGridControl = $form.find('div[data-name="CheckedInItemGrid"]');
                FwBrowse.search($checkedInItemsGridControl);
                $form.find('[data-datafield="BarCode"] input').select();

                if (response.status === 107) {
                    successSound.play();
                    $form.find('[data-datafield="Quantity"] input').select();
                }

                if (type === 'Quantity') {
                    FwFormField.setValueByDataField($form, 'Quantity', 0);
                    $form.find('[data-datafield="BarCode"] input').select();
                }
            }
            else if (!response.success) {
                if (response.ShowSwap) {
                    notificationSound.play();
                    $form.find('.swapitem').show();
                } else {
                    errorSound.play();
                    $form.find('.swapitem').hide();
                }
                $form.find('.error-msg:not(.qty)').html(`<div><span>${response.msg}</span></div>`);
                $form.find('[data-datafield="BarCode"] input').select();
            }
            response.ShowNewOrder ? $form.find('.addordertocontract').show() : $form.find('.addordertocontract').hide();
        }, null, contractId ? null : $form);
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="checkinform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="${this.caption}" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="${this.Module}Controller">
          <div id="checkinform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="checkintab" class="checkintab tab" data-tabpageid="checkintabpage" data-caption="${this.caption}"></div>
              <div data-type="tab" id="orderstab" class="orderstab tab" data-tabpageid="orderstabpage" data-caption="Orders" style="display:none;"></div>
              <div data-type="tab" id="quantityitemstab" class="quantityitemstab tab" data-tabpageid="quantityitemstabpage" data-caption="Quantity Items"></div>
              <div data-type="tab" id="swapitemtab" class="swapitemtab tab" data-tabpageid="swapitemtabpage" data-caption="Swapped Items" style="display:none;"></div>
              <div data-type="tab" id="exceptionstab" class="exceptionstab tab" data-tabpageid="exceptionstabpage" data-caption="Exceptions"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="checkintabpage" class="tabpage" data-tabid="checkintab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Check-In">
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:1 1 450px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:1 1 250px;"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-formbeforevalidate="beforeValidate" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 250px;" data-enabled="false"></div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:1 1 450px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="flex:0 1 350px;"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:0 1 200px;" data-enabled="false"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items">
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:1 1 850px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code / I-Code" data-datafield="BarCode" style="flex:1 1 300px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" style="flex:1 1 300px;" data-enabled="false"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="InventoryDescription" style="flex:1 1 400px;" data-enabled="false"></div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:1 1 850px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" style="flex:0 1 100px; margin-right:256px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ordered" data-datafield="QuantityOrdered" style="flex:0 1 100px;" data-enabled="false"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub" data-datafield="QuantitySub" style="flex:0 1 100px;" data-enabled="false"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Out" data-datafield="QuantityOut" style="flex:0 1 100px;" data-enabled="false"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Staged" data-datafield="QuantityStaged" style="flex:0 1 100px;" data-enabled="false"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="In" data-datafield="QuantityIn" style="flex:0 1 100px;" data-enabled="false"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="QuantityRemaining" style="flex:0 1 100px;" data-enabled="false"></div>
                          </div>
                        </div>
                      </div>
                      <div class="error-msg" style="margin-top:8px;"></div>
                      <div class="fwformcontrol addordertocontract" data-type="button" style="display:none; flex:0 1 150px;margin:15px 0 0 10px;text-align:center;">Add Order To Contract</div>
                      <div class="fwformcontrol swapitem" data-type="button" style="display:none; flex:0 1 150px;margin:15px 0 0 10px;text-align:center;">Swap Item</div>
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CheckedInItemGrid" data-securitycaption=""></div>
                      </div>
                      <div class="formrow">
                        <div class="fwformcontrol orderstatus" data-type="button" style="float:left; margin-left:10px;">Order Status</div>
                        <div class="fwformcontrol createcontract" data-type="button" style="float:right;">Create Contract</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="orderstabpage" class="tabpage" data-tabid="orderstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckInOrderGrid" data-securitycaption=""></div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="quantityitemstabpage" class="tabpage" data-tabid="quantityitemstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckInQuantityItemsGrid" data-securitycaption=""></div>
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol optionsbutton" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                    <div class="fwformcontrol selectall" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                    <div class="fwformcontrol selectnone" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                  </div>
                  <div class="flexrow optionlist" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show all ACTIVE Orders for this Deal" data-datafield="AllOrdersForDeal" style="flex:0 1 350px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Specific Order" data-datafield="SpecificOrder" style="flex:0 1 150px;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="SpecificOrderId" data-displayfield="SpecificOrderNumber" data-validationname="OrderValidation" data-formbeforevalidate="beforeValidateSpecificOrder" style="flex:0 1 175px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="SpecificDescription" style="flex:1 1 250px;" data-enabled="false"></div>
                  </div>
                  <div class="flexrow optionlist" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only show items with Quantity Out" data-datafield="ShowQuantityOut"></div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="swapitemtabpage" class="tabpage" data-tabid="swapitemtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckInSwapGrid" data-securitycaption=""></div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="exceptionstabpage" class="tabpage" data-tabid="exceptionstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckInExceptionGrid" data-securitycaption=""></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        `;
      }
    //----------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------
}
var CheckInController = new CheckIn();