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

    renderGrids($form: any) {
        var $resaleGrid,
            $resaleControl;

        $resaleGrid = $form.find('div[data-grid="ContactTitleGrid"]');
        $resaleControl = jQuery(jQuery('#tmpl-grids-ContactTitleGridBrowse').html());
        $resaleGrid.empty().append($resaleControl);
        $resaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeContactTitleId: $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val()
            }
        });
        $resaleControl.data('beforesave', function (request) {
            request.OrderTypeContactTitleId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($resaleControl);
        FwBrowse.renderRuntimeHtml($resaleControl);
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
        // ----------
        var nameOrderTypeNoteGrid: string = 'OrderTypeNoteGrid';
        var $orderTypeNoteGrid: any = $orderTypeNoteGrid = $form.find('div[data-grid="' + nameOrderTypeNoteGrid + '"]');
        var $orderTypeNoteGridControl: any = FwBrowse.loadGridFromTemplate(nameOrderTypeNoteGrid);

        $orderTypeNoteGrid.empty().append($orderTypeNoteGridControl);
        $orderTypeNoteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            };
        });
        $orderTypeNoteGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($orderTypeNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeNoteGridControl);


        var $coverLetterGrid: any;
        var $coverLetterGridControl: any;

        $coverLetterGrid = $form.find('div[data-grid="CoverLetterGrid"]');
        $coverLetterGridControl = jQuery(jQuery('#tmpl-grids-CoverLetterGridBrowse').html());
        $coverLetterGrid.empty().append($coverLetterGridControl);
        $coverLetterGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val()
            };
        })
        $coverLetterGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        });
        FwBrowse.init($coverLetterGridControl);
        FwBrowse.renderRuntimeHtml($coverLetterGridControl);
    }


    afterLoad($form: any) {
        var $orderTypeInvoiceExportGrid: any = $form.find('[data-name="OrderTypeInvoiceExportGrid"]');
        FwBrowse.search($orderTypeInvoiceExportGrid);

        var $orderTypeNoteGrid: any = $form.find('[data-name="OrderTypeNoteGrid"]');
        FwBrowse.search($orderTypeNoteGrid);

        var $coverLetterGrid: any;

        $coverLetterGrid = $form.find('[data-name="CoverLetterGrid"]');
        FwBrowse.search($coverLetterGrid);


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

        var $resaleGrid;            

        $resaleGrid = $form.find('[data-name="ContactTitleGrid"]');
        FwBrowse.search($resaleGrid);
    }
}

(<any>window).OrderTypeController = new OrderType();