class OrderType {
    Module: string = 'OrderType';
    apiurl: string = 'api/v1/ordertype';
    caption: string = Constants.Modules.Settings.children.OrderSettings.children.OrderType.caption;
    nav: string = Constants.Modules.Settings.children.OrderSettings.children.OrderType.nav;
    id: string = Constants.Modules.Settings.children.OrderSettings.children.OrderType.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
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
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeInvoiceExportGrid',
            gridSecurityId: 'acguZNBoT1XC',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeNoteGrid',
            gridSecurityId: 'DZwS6DaO7Ed8',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId')
            }
        });
        //----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeCoverLetterGrid',
            gridSecurityId: 'acguZNBoT1XC',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
        //----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeActivityDatesGrid',
            gridSecurityId: 'oMijD9WAL6Bl',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
        // -----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeTermsAndConditionsGrid',
            gridSecurityId: 'acguZNBoT1XC',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeContactTitleGrid',
            gridSecurityId: 'HzNQkWcZ8vEC',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
            }
        });
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