class OrderType {
    constructor() {
        this.Module = 'OrderType';
        this.apiurl = 'api/v1/ordertype';
        this.caption = Constants.Modules.Settings.children.OrderSettings.children.OrderType.caption;
        this.nav = Constants.Modules.Settings.children.OrderSettings.children.OrderType.nav;
        this.id = Constants.Modules.Settings.children.OrderSettings.children.OrderType.id;
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
    openForm(mode) {
        var $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        $form.find('[data-datafield="QuikPayDiscount"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.discount'));
            }
            else {
                FwFormField.disable($form.find('.discount'));
            }
        });
        $form.find('[data-datafield="AddInstallationAndStrikeFee"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.installation'));
            }
            else {
                FwFormField.disable($form.find('.installation'));
            }
        });
        $form.find('[data-datafield="AddManagementAndServiceFee"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.management'));
            }
            else {
                FwFormField.disable($form.find('.management'));
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
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="OrderTypeId"] input').val(uniqueids.OrderTypeId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeInvoiceExportGrid',
            gridSecurityId: 'acguZNBoT1XC',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeNoteGrid',
            gridSecurityId: 'DZwS6DaO7Ed8',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeCoverLetterGrid',
            gridSecurityId: 'acguZNBoT1XC',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeActivityDatesGrid',
            gridSecurityId: 'oMijD9WAL6Bl',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeTermsAndConditionsGrid',
            gridSecurityId: 'acguZNBoT1XC',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeContactTitleGrid',
            gridSecurityId: 'HzNQkWcZ8vEC',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
    }
    afterLoad($form) {
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
            FwFormField.enable($form.find('.discount'));
        }
        else {
            FwFormField.disable($form.find('.discount'));
        }
        ;
        if ($form.find('[data-datafield="AddInstallationAndStrikeFee"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('.installation'));
        }
        else {
            FwFormField.disable($form.find('.installation'));
        }
        ;
        if ($form.find('[data-datafield="AddManagementAndServiceFee"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('.management'));
        }
        else {
            FwFormField.disable($form.find('.management'));
        }
        ;
        const $resaleGrid = $form.find('[data-name="OrderTypeContactTitleGrid"]');
        FwBrowse.search($resaleGrid);
    }
}
var OrderTypeController = new OrderType();
//# sourceMappingURL=OrderType.js.map