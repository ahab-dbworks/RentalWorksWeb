class FacilityRate {
    Module: string = 'FacilityRate';
    apiurl: string = 'api/v1/facilityrate';
    caption: string = Constants.Modules.Settings.children.FacilitySettings.children.FacilityRate.caption;
    nav: string = Constants.Modules.Settings.children.FacilitySettings.children.FacilityRate.nav;
    id: string = Constants.Modules.Settings.children.FacilitySettings.children.FacilityRate.id;

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Facility Rate', false, 'BROWSE', true);
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

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true')
        }

        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.category [data-type="validation"]'))
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategory"]'))
            }
            else {
                FwFormField.disable($form.find('.category [data-type="validation"]'))
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategory"]'))
            }
        });

        $form.find('[data-datafield="ProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('[data-datafield="OverrideProfitAndLossCategory"]'))
            }
            else {
                FwFormField.enable($form.find('[data-datafield="OverrideProfitAndLossCategory"]'))
            }
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="RateId"] input').val(uniqueids.RateId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form: any) {
       //const $rateLocationTaxGrid = $form.find('div[data-grid="RateLocationTaxGrid"]');
       //const $rateLocationTaxGridControl = FwBrowse.loadGridFromTemplate('RateLocationTaxGrid');
       // $rateLocationTaxGrid.empty().append($rateLocationTaxGridControl);
       // $rateLocationTaxGridControl.data('ondatabind', request => {
       //     request.uniqueids = {
       //         RateId: FwFormField.getValueByDataField($form, 'RateId')
       //     };
       // })
       // FwBrowse.init($rateLocationTaxGridControl);
       // FwBrowse.renderRuntimeHtml($rateLocationTaxGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'RateLocationTaxGrid',
            gridSecurityId: 'Bm6TN9A4IRIuT',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    RateId: FwFormField.getValueByDataField($form, 'RateId'),
                };
            },
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'RateId');
            //},
        });

        //const $rateWarehouseGrid = $form.find('div[data-grid="RateWarehouseGrid"]');
        //const $rateWarehouseGridControl = FwBrowse.loadGridFromTemplate('RateWarehouseGrid');
        //$rateWarehouseGrid.empty().append($rateWarehouseGridControl);
        //$rateWarehouseGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        RateId: FwFormField.getValueByDataField($form, 'RateId')
        //    };
        //})
        //FwBrowse.init($rateWarehouseGridControl);
        //FwBrowse.renderRuntimeHtml($rateWarehouseGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'RateWarehouseGrid',
            gridSecurityId: 'oVjmeqXtHEJCm',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    RateId: FwFormField.getValueByDataField($form, 'RateId'),
                };
            },
            //beforeSave: (request: any) => {
            //    request.RateId = FwFormField.getValueByDataField($form, 'RateId');
            //},
        });
    }

    afterLoad($form: any) {
        var $limit = $form.find('div.fwformfield[data-datafield="OverrideProfitAndLossCategory"] input').prop('checked');

        const $rateLocationTaxGrid = $form.find('[data-name="RateLocationTaxGrid"]');
        FwBrowse.search($rateLocationTaxGrid);

        const $rateWarehouseGrid = $form.find('[data-name="RateWarehouseGrid"]');
        FwBrowse.search($rateWarehouseGrid);

        if ($limit === true) {
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategory"]'));
        }
    }

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        var FacilityTypeValue = jQuery($form.find('[data-validationname="FacilityTypeValidation"] input')).val();

        request.uniqueids = {
            FacilityTypeId: FacilityTypeValue
        };
    }
}

var FacilityRateController = new FacilityRate();