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

    renderGrids($form: any) {
        // ----------
        const $orderTypeInvoiceExportGrid = $form.find('div[data-grid="OrderTypeInvoiceExportGrid"]');
        const $orderTypeInvoiceExportGridControl = FwBrowse.loadGridFromTemplate('OrderTypeInvoiceExportGrid');
        $orderTypeInvoiceExportGrid.empty().append($orderTypeInvoiceExportGridControl);
        $orderTypeInvoiceExportGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            };
        });
        $orderTypeInvoiceExportGridControl.data('beforesave', request => {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($orderTypeInvoiceExportGridControl);;
        FwBrowse.renderRuntimeHtml($orderTypeInvoiceExportGridControl);
        // ----------
        const $orderTypeNoteGrid = $form.find('div[data-grid="OrderTypeNoteGrid"]');
        const $orderTypeNoteGridControl = FwBrowse.loadGridFromTemplate('OrderTypeNoteGrid');
        $orderTypeNoteGrid.empty().append($orderTypeNoteGridControl);
        $orderTypeNoteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            };
        });
        $orderTypeNoteGridControl.data('beforesave', request => {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($orderTypeNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeNoteGridControl);
        //----------
        const $orderTypeCoverLetterGrid = $form.find('div[data-grid="OrderTypeCoverLetterGrid"]');
        const $orderTypeCoverLetterGridControl = FwBrowse.loadGridFromTemplate('OrderTypeCoverLetterGrid');
        $orderTypeCoverLetterGrid.empty().append($orderTypeCoverLetterGridControl);
        $orderTypeCoverLetterGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            };
        })
        $orderTypeCoverLetterGridControl.data('beforesave', request => {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        });
        FwBrowse.init($orderTypeCoverLetterGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeCoverLetterGridControl);
        //----------
        const $orderTypeActivityDatesGrid = $form.find('div[data-grid="OrderTypeActivityDatesGrid"]');
        const $orderTypeActivityDatesGridControl = FwBrowse.loadGridFromTemplate('OrderTypeActivityDatesGrid');
        $orderTypeActivityDatesGrid.empty().append($orderTypeActivityDatesGridControl);
        $orderTypeActivityDatesGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            };
        })
        $orderTypeActivityDatesGridControl.data('beforesave', request => {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        });
        FwBrowse.init($orderTypeActivityDatesGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeActivityDatesGridControl);
        // -----------
        const $orderTypeTermsAndConditionsGrid = $form.find('div[data-grid="OrderTypeTermsAndConditionsGrid"]');
        const $orderTypeTermsAndConditionsGridControl = FwBrowse.loadGridFromTemplate('OrderTypeTermsAndConditionsGrid');
        $orderTypeTermsAndConditionsGrid.empty().append($orderTypeTermsAndConditionsGridControl);
        $orderTypeTermsAndConditionsGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            };
        })
        $orderTypeTermsAndConditionsGridControl.data('beforesave', request => {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        });
        FwBrowse.init($orderTypeTermsAndConditionsGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeTermsAndConditionsGridControl);
        //----------
        const $resaleGrid = $form.find('div[data-grid="ContactTitleGrid"]');
        const $resaleControl = FwBrowse.loadGridFromTemplate('ContactTitleGrid');
        $resaleGrid.empty().append($resaleControl);
        $resaleControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            }
        });
        $resaleControl.data('beforesave', request => {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($resaleControl);
        FwBrowse.renderRuntimeHtml($resaleControl);
        // ----------
        const $orderTypeContactTitleGrid = $form.find('div[data-grid="OrderTypeContactTitleGrid"]');
        const $orderTypeContactTitleControl = FwBrowse.loadGridFromTemplate('OrderTypeContactTitleGrid');
        $orderTypeContactTitleGrid.empty().append($orderTypeContactTitleControl);
        $orderTypeContactTitleControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
            }
        });
        $orderTypeContactTitleControl.data('beforesave', request => {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
        });
        FwBrowse.init($orderTypeContactTitleControl);
        FwBrowse.renderRuntimeHtml($orderTypeContactTitleControl);
        // -----------
    }

    afterLoad($form: any) {
        const $orderTypeInvoiceExportGrid = $form.find('[data-name="OrderTypeInvoiceExportGrid"]');
        FwBrowse.search($orderTypeInvoiceExportGrid);

        const $orderTypeNoteGrid = $form.find('[data-name="OrderTypeNoteGrid"]');
        FwBrowse.search($orderTypeNoteGrid);

        const $orderTypeCoverLetterGrid = $form.find('[data-name="OrderTypeCoverLetterGrid"]');
        FwBrowse.search($orderTypeCoverLetterGrid);

        const $orderTypeTermsAndConditionsGrid = $form.find('[data-name="OrderTypeTermsAndConditionsGrid"]');
        FwBrowse.search($orderTypeTermsAndConditionsGrid);

        const $orderTypeActivityDatesGrid = $form.find('[data-name="OrderTypeActivityDatesGrid"]');
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

        const $resaleGrid = $form.find('[data-name="OrderTypeContactTitleGrid"]');
        FwBrowse.search($resaleGrid);
    }
}

var OrderTypeController = new OrderType();