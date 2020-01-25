class SpaceType {
    Module: string = 'SpaceType';
    apiurl: string = 'api/v1/spacetype';
    caption: string = Constants.Modules.Settings.children.FacilitySettings.children.SpaceType.caption;
    nav: string = Constants.Modules.Settings.children.FacilitySettings.children.SpaceType.nav;
    id: string = Constants.Modules.Settings.children.FacilitySettings.children.SpaceType.id;
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

        $form.find('[data-datafield="ForReportsOnly"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NonBillable"]'))
            } else {
                FwFormField.disable($form.find('[data-datafield="NonBillable"]'))
            }
        });

        $form.find('div[data-datafield="RateId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="RateDescription"]', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="RateUnit"]', $tr.find('.field[data-browsedatafield="Unit"]').attr('data-originalvalue'));
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="SpaceTypeId"] input').val(uniqueids.SpaceTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form: any) {
        //const $spaceWarehouseRateGrid = $form.find('div[data-grid="SpaceWarehouseRateGrid"]');
        //const $spaceWarehouseRateGridControl = FwBrowse.loadGridFromTemplate('SpaceWarehouseRateGrid');
        //$spaceWarehouseRateGrid.empty().append($spaceWarehouseRateGridControl);
        //$spaceWarehouseRateGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        RateId: FwFormField.getValueByDataField($form, 'RateId')
        //    };
        //});
        //FwBrowse.init($spaceWarehouseRateGridControl);
        //FwBrowse.renderRuntimeHtml($spaceWarehouseRateGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'SpaceWarehouseRateGrid',
            gridSecurityId: 'oVjmeqXtHEJCm',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    RateId: FwFormField.getValueByDataField($form, 'RateId')
                };
            }
        });
    }

    afterLoad($form: any) {
        const $spaceWarehouseRateGrid = $form.find('[data-name="SpaceWarehouseRateGrid"]');
        FwBrowse.search($spaceWarehouseRateGrid);
    }
    //-----------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'FacilityTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatefacilitytype`);
                break;
            case 'RateId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterate`);
                break;
        }
    }
}

var SpaceTypeController = new SpaceType();