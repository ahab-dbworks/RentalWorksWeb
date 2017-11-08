declare var FwModule: any;
declare var FwBrowse: any;

class OrderType {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderType';
        this.apiurl = 'api/v1/ordertype';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Order Type', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="QuikPayDiscount"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.discount'))
            }
            else {
                FwFormField.disable($form.find('.discount'))
            }
        });

        $form.find('[data-datafield="AddInstallationAndStrikeFee"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.installation'))
            }
            else {
                FwFormField.disable($form.find('.installation'))
            }
        });

        $form.find('[data-datafield="AddManagementAndServiceFee"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.management'))
            }
            else {
                FwFormField.disable($form.find('.management'))
            }
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val(uniqueids.OrderTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        // ----------
        var nameOrderTypeInvoiceExportGrid: string = 'OrderTypeInvoiceExportGrid';
        var $orderTypeInvoiceExportGrid: any = $orderTypeInvoiceExportGrid = $form.find('div[data-grid="' + nameOrderTypeInvoiceExportGrid + '"]');
        var $orderTypeInvoiceExportGridControl: any = FwBrowse.loadGridFromTemplate(nameOrderTypeInvoiceExportGrid);

        $orderTypeInvoiceExportGrid.empty().append($orderTypeInvoiceExportGridControl);
        $orderTypeInvoiceExportGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            };
        });
        $orderTypeInvoiceExportGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($orderTypeInvoiceExportGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeInvoiceExportGridControl);
    }

    afterLoad($form: any) {
        var $orderTypeInvoiceExportGrid: any = $form.find('[data-name="OrderTypeInvoiceExportGrid"]');
        FwBrowse.search($orderTypeInvoiceExportGrid);

        if ($form.find('[data-datafield="QuikPayDiscount"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('.discount'))
        }
        else {
            FwFormField.disable($form.find('.discount'))
        };

        if ($form.find('[data-datafield="AddInstallationAndStrikeFee"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('.installation'))
        }
        else {
            FwFormField.disable($form.find('.installation'))
        };

        if ($form.find('[data-datafield="AddManagementAndServiceFee"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('.management'))
        }
        else {
            FwFormField.disable($form.find('.management'))
        };
    }
}

(<any>window).OrderTypeController = new OrderType();