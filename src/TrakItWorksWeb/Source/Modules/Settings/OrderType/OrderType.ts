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
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
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

        $form.find('div[data-datafield="InstallationAndStrikeFeeRateId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="InstallationAndStrikeFeeDescription"]', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="ManagementAndServiceFeeRateId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="ManagementAndServiceFeeDescription"]', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
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

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
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
        FwBrowse.init($orderTypeInvoiceExportGridControl);;
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
        //----------
        var $orderTypeCoverLetterGrid: any;
        var $orderTypeCoverLetterGridControl: any;

        $orderTypeCoverLetterGrid = $form.find('div[data-grid="OrderTypeCoverLetterGrid"]');
        $orderTypeCoverLetterGridControl = jQuery(jQuery('#tmpl-grids-OrderTypeCoverLetterGridBrowse').html());
        $orderTypeCoverLetterGrid.empty().append($orderTypeCoverLetterGridControl);
        $orderTypeCoverLetterGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val()
            };
        })
        $orderTypeCoverLetterGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        });
        FwBrowse.init($orderTypeCoverLetterGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeCoverLetterGridControl);
        //----------
        var $orderTypeActivityDatesGrid: any;
        var $orderTypeActivityDatesGridControl: any;

        $orderTypeActivityDatesGrid = $form.find('div[data-grid="OrderTypeActivityDatesGrid"]');
        $orderTypeActivityDatesGridControl = jQuery(jQuery('#tmpl-grids-OrderTypeActivityDatesGridBrowse').html());
        $orderTypeActivityDatesGrid.empty().append($orderTypeActivityDatesGridControl);
        $orderTypeActivityDatesGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val()
            };
        })
        $orderTypeActivityDatesGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        });
        FwBrowse.init($orderTypeActivityDatesGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeActivityDatesGridControl);
        // -----------
        var $orderTypeTermsAndConditionsGrid: any;
        var $orderTypeTermsAndConditionsGridControl: any;

        $orderTypeTermsAndConditionsGrid = $form.find('div[data-grid="OrderTypeTermsAndConditionsGrid"]');
        $orderTypeTermsAndConditionsGridControl = jQuery(jQuery('#tmpl-grids-OrderTypeTermsAndConditionsGridBrowse').html());
        $orderTypeTermsAndConditionsGrid.empty().append($orderTypeTermsAndConditionsGridControl);
        $orderTypeTermsAndConditionsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val()
            };
        })
        $orderTypeTermsAndConditionsGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        });
        FwBrowse.init($orderTypeTermsAndConditionsGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeTermsAndConditionsGridControl);
        //----------
        var $resaleGrid,
            $resaleControl;

        $resaleGrid = $form.find('div[data-grid="ContactTitleGrid"]');
        $resaleControl = jQuery(jQuery('#tmpl-grids-ContactTitleGridBrowse').html());
        $resaleGrid.empty().append($resaleControl);
        $resaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val()
            }
        });
        $resaleControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($resaleControl);
        FwBrowse.renderRuntimeHtml($resaleControl);
        // ----------
        var $orderTypeContactTitleGrid,
            $orderTypeContactTitleControl;

        $orderTypeContactTitleGrid = $form.find('div[data-grid="OrderTypeContactTitleGrid"]');
        $orderTypeContactTitleControl = jQuery(jQuery('#tmpl-grids-OrderTypeContactTitleGridBrowse').html());
        $orderTypeContactTitleGrid.empty().append($orderTypeContactTitleControl);
        $orderTypeContactTitleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val()
            }
        });
        $orderTypeContactTitleControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($orderTypeContactTitleControl);
        FwBrowse.renderRuntimeHtml($orderTypeContactTitleControl);
        // -----------
    }

    afterLoad($form: any) {
        var $orderTypeInvoiceExportGrid: any = $form.find('[data-name="OrderTypeInvoiceExportGrid"]');
        FwBrowse.search($orderTypeInvoiceExportGrid);

        var $orderTypeNoteGrid: any = $form.find('[data-name="OrderTypeNoteGrid"]');
        FwBrowse.search($orderTypeNoteGrid);

        var $orderTypeCoverLetterGrid: any;
        $orderTypeCoverLetterGrid = $form.find('[data-name="OrderTypeCoverLetterGrid"]');
        FwBrowse.search($orderTypeCoverLetterGrid);

        var $orderTypeTermsAndConditionsGrid: any;
        $orderTypeTermsAndConditionsGrid = $form.find('[data-name="OrderTypeTermsAndConditionsGrid"]');
        FwBrowse.search($orderTypeTermsAndConditionsGrid);

        var $orderTypeActivityDatesGrid: any;
        $orderTypeActivityDatesGrid = $form.find('[data-name="OrderTypeActivityDatesGrid"]');
        FwBrowse.search($orderTypeActivityDatesGrid);

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

        $resaleGrid = $form.find('[data-name="OrderTypeContactTitleGrid"]');
        FwBrowse.search($resaleGrid);
    }
}

var OrderTypeController = new OrderType();