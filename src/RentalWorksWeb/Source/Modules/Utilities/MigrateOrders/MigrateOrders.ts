routes.push({ pattern: /^module\/migrateorders/, action: function (match: RegExpExecArray) { return MigrateOrdersController.getModuleScreen(); } });
class MigrateOrders {
    Module: string = 'MigrateOrders';
    apiurl: string = 'api/v1/migrateorders';
    caption: string = Constants.Modules.Utilities.MigrateOrders.caption;
    nav: string = Constants.Modules.Utilities.MigrateOrders.nav;
    id: string = Constants.Modules.Utilities.MigrateOrders.id;
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

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);

        FwFormField.setValueByDataField($form, 'CopyLineItemNotes', true);
        FwFormField.setValueByDataField($form, 'CopyRentalRates', true);

        //add order browse
        const $orderBrowse = this.addOrderBrowse($form);
        $form.find('.orderBrowse').append($orderBrowse);

        $form.find('[data-datafield="DealId"]').on('change', e => {
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            const departmentId = FwFormField.getValueByDataField($form, 'DepartmentId');

            const onDataBind = $orderBrowse.data('ondatabind');
            if (typeof onDataBind == 'function') {
                $orderBrowse.data('ondatabind', request => {
                    onDataBind(request);
                    request.uniqueids.DealId = dealId;
                    request.uniqueids.DepartmentId = departmentId;
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
        });

        //toggle billing stop date
        $form.find('[data-datafield="UpdateBillingStopDate"]').on('change', e => {
            const isChecked = FwFormField.getValueByDataField($form, 'UpdateBillingStopDate');
            if (isChecked) {
                FwFormField.enable($form.find('.migrate-options[data-datafield="BillingStopDate"]'));
            } else {
                FwFormField.disable($form.find('.migrate-options[data-datafield="BillingStopDate"]'));
            }
        });
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