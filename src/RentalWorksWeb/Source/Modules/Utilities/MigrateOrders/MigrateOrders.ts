routes.push({ pattern: /^module\/migrateorders/, action: function (match: RegExpExecArray) { return MigrateOrdersController.getModuleScreen(); } });
class MigrateOrders {
    Module: string = 'MigrateOrders';
    apiurl: string = 'api/v1/migrateorders';
    caption: string = Constants.Modules.Utilities.MigrateOrders.caption;
    nav: string = Constants.Modules.Utilities.MigrateOrders.nav;
    id: string = Constants.Modules.Utilities.MigrateOrders.id;
    successSoundFileName: string;
    errorSoundFileName: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables "modified" asterisk
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.getSoundUrls($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSoundUrls = ($form): void => {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        const errorSound = new Audio(this.errorSoundFileName);
        const successSound = new Audio(this.successSoundFileName);
        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        const rateType = JSON.parse(sessionStorage.getItem('defaultlocation')).ratetype;
        FwFormField.setValueByDataField($form, 'RateType', rateType, rateType);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'FromDate', today);
        FwFormField.setValueByDataField($form, 'ToDate', today);
        FwFormField.setValueByDataField($form, 'CopyLineItemNotes', true);
        FwFormField.setValueByDataField($form, 'CopyRentalRates', true);

        //add order browse
        const $orderBrowse = this.addOrderBrowse($form);
        $form.find('.orderBrowse').append($orderBrowse);

        $form.find('[data-datafield="DealId"], [data-datafield="DepartmentId"]').data('onchange', e => {
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            const departmentId = FwFormField.getValueByDataField($form, 'DepartmentId');
            const onDataBind = $orderBrowse.data('ondatabind');
            if (typeof onDataBind == 'function') {
                $orderBrowse.data('ondatabind', request => {
                    onDataBind(request);
                    request.miscfields.DealId = dealId;
                    request.miscfields.DepartmentId = departmentId;
                    request.miscfields.Migrate = true;
                });
            }
            FwBrowse.search($orderBrowse);
        });

        //toggle destination fields
        $form.find('[data-datafield="CreateNewOrder"], [data-datafield="MigrateToExistingOrder"]').on('change', e => {
            const datafield = jQuery(e.currentTarget).attr('data-datafield');
            let datafield2;
            if (datafield === 'CreateNewOrder') {
                datafield2 = 'MigrateToExistingOrder';
            } else {
                datafield2 = 'CreateNewOrder';
            }
            const isChecked = FwFormField.getValueByDataField($form, datafield);
            if (isChecked) {
                FwFormField.setValueByDataField($form, datafield2, false);
                FwFormField.disable($form.find(`.${datafield2}`));
                FwFormField.enable($form.find(`.${datafield}`));
            } else {
                FwFormField.setValueByDataField($form, datafield2, true);
                FwFormField.enable($form.find(`.${datafield2}`));
                FwFormField.disable($form.find(`.${datafield}`));
            }

            const isCreateNewOrder = FwFormField.getValueByDataField($form, 'CreateNewOrder');
            if (isCreateNewOrder) {
                $form.find('[data-datafield="PendingPO"]').change();
            }
        });
        FwFormField.setValueByDataField($form, 'CreateNewOrder', true, undefined, true);

        //toggle billing stop date
        $form.find('[data-datafield="UpdateBillingStopDate"]').on('change', e => {
            const isChecked = FwFormField.getValueByDataField($form, 'UpdateBillingStopDate');
            if (isChecked) {
                FwFormField.enable($form.find('[data-datafield="MigrateBillingStopDate"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="MigrateBillingStopDate"]'));
            }
        });

        //Toggle PO fields
        $form.find('[data-datafield="PendingPO"]').on('change', e => {
            const isChecked = FwFormField.getValueByDataField($form, 'PendingPO');
            if (isChecked) {
                FwFormField.disable($form.find('[data-datafield="FlatPO"], [data-datafield="PurchaseOrderNumber"], [data-datafield="PurchaseOrderAmount"]'));
            } else {
                FwFormField.enable($form.find('[data-datafield="FlatPO"], [data-datafield="PurchaseOrderNumber"], [data-datafield="PurchaseOrderAmount"]'));
            }
        });
        FwFormField.setValueByDataField($form, 'PendingPO', true, undefined, true);

        //default description and deal values
        $form.find('[data-datafield="OrderId"]').data('onchange', $tr => {
            const description = $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue');
            const dealId = $tr.find('[data-browsedatafield="DealId"]').attr('data-originalvalue');
            const deal = $tr.find('[data-browsedatafield="Deal"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'Description', description);
            FwFormField.setValueByDataField($form, 'MigrateDealId', dealId, deal);
        });

        //render grid upon clicking Items tab
        $form.find('[data-type="tab"][data-caption="Items"]').on('click', e => {
            this.renderMigrateItemGrid($form);  //starts session
        });


        const $migrateItemGrid = $form.find('div[data-grid="MigrateItemGrid"]');
        // Select All
        $form.find('.select-all').on('click', e => {
            const $migrateItemGridControl = $form.find('[data-name="MigrateItemGrid"]');
            const request: any = {};
            request.SessionId = $migrateItemGrid.data('sessionId');
            FwAppData.apiMethod(true, 'POST', `api/v1/migrate/selectall`, request, FwServices.defaultTimeout,
                response => {
                    $form.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        $form.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                        FwBrowse.search($migrateItemGridControl);
                    }
                },
                ex => FwFunc.showError(ex)
                , $migrateItemGrid);
        });
        // Select None
        $form.find('.select-none').on('click', e => {
            const $migrateItemGridControl = $form.find('[data-name="MigrateItemGrid"]');
            const request: any = {};
            request.SessionId = $migrateItemGrid.data('sessionId');
            FwAppData.apiMethod(true, 'POST', `api/v1/migrate/selectnone`, request, FwServices.defaultTimeout,
                response => {
                    $form.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        $form.find('div.error-msg').html(`<div><span">${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                    }
                    FwBrowse.search($migrateItemGridControl);
                }, ex => FwFunc.showError(ex) 
                , $migrateItemGrid);
        });

        //finalize migration
        $form.find('.finalize-migration').on('click', e => {
            const $migrateItemGridControl = $form.find('[data-name="MigrateItemGrid"]');
            const request: any = {};
            request.SessionId = $migrateItemGrid.data('sessionId');

            FwAppData.apiMethod(true, 'POST', `api/v1/migrate/completesession`, request, FwServices.defaultTimeout,
                response => {
                    $form.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        $form.find('div.error-msg').html(`<div><span">${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                        //will produce array of contractlogic objects
                        //open each contract as a separate tab
                        //reset migrate orders tab
                    }
                }, ex => FwFunc.showError(ex)
                , $migrateItemGrid);
        });
    }
    //----------------------------------------------------------------------------------------------
    renderMigrateItemGrid($form) {
        const $migrateItemGrid = $form.find('div[data-grid="MigrateItemGrid"]');
        const $migrateItemGridControl = FwBrowse.loadGridFromTemplate('MigrateItemGrid');
        $migrateItemGrid.empty().append($migrateItemGridControl);
        FwBrowse.init($migrateItemGridControl);
        FwBrowse.renderRuntimeHtml($migrateItemGridControl);

        const $orderBrowse = $form.find('.orderBrowse .fwbrowse');
        const $selectedCheckboxes = $orderBrowse.find('tbody .cbselectrow:checked');

        let orderIds: any = [];
        let sessionId;
        if ($selectedCheckboxes.length > 0) {
            for (let i = 0; i < $selectedCheckboxes.length; i++) {
                const orderId = $selectedCheckboxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                if (orderId !== '') {
                    orderIds.push(orderId);
                }
            }
            orderIds = orderIds.join(',');

            const request: any = {};
            request.OrderIds = orderIds;
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            request.DepartmentId = JSON.parse(sessionStorage.getItem('department')).departmentid;

            FwAppData.apiMethod(true, 'POST', `api/v1/migrate/startsession`, request, FwServices.defaultTimeout,
                response => {
                    sessionId = response.SessionId;
                    if (sessionId) {
                        $migrateItemGrid.data('sessionId', sessionId);
                        $migrateItemGridControl.data('ondatabind', request => {
                            request.uniqueids = {
                                SessionId: sessionId
                            };
                        });
                        FwBrowse.search($migrateItemGridControl);
                    }
                }, ex => FwFunc.showError(ex), $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    addOrderBrowse($form) {
        let $browse = jQuery(OrderController.getBrowseTemplate());
        $browse.attr('data-hasmultirowselect', 'true');
        $browse.attr('data-type', 'Browse');
        $browse.attr('data-showsearch', 'false');

        $browse = FwModule.openBrowse($browse);
        $browse.find('.fwbrowse-menu').hide();

        $browse.data('ondatabind', request => {
            request.ActiveViewFields = OrderController.ActiveViewFields;
            request.pagesize = 15;
            request.orderby = 'OrderDate desc';
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
}
var MigrateOrdersController = new MigrateOrders();