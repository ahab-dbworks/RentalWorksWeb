class VehicleType {
    Module: string = 'VehicleType';
    apiurl: string = 'api/v1/vehicletype';
    caption: string = Constants.Modules.Settings.children.VehicleSettings.children.VehicleType.caption;
    nav: string = Constants.Modules.Settings.children.VehicleSettings.children.VehicleType.nav;
    id: string = Constants.Modules.Settings.children.VehicleSettings.children.VehicleType.id;
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
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="VehicleTypeId"] input').val(uniqueids.VehicleTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    disableFields(): void {
        jQuery('.disablefield').attr('data-required', 'false');
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $vehicleTypeWarehouseGrid = $form.find('div[data-grid="VehicleTypeWarehouseGrid"]');
        const $vehicleTypeWarehouseControl = FwBrowse.loadGridFromTemplate('VehicleTypeWarehouseGrid');
        $vehicleTypeWarehouseGrid.empty().append($vehicleTypeWarehouseControl);
        $vehicleTypeWarehouseControl.data('ondatabind', request => {
            request.uniqueids = {
                VehicleTypeId: FwFormField.getValueByDataField($form, 'VehicleTypeId')
            }
            request.miscfields = {
                UserWarehouseId: warehouse.warehouseid
            };
        });
        FwBrowse.init($vehicleTypeWarehouseControl);
        FwBrowse.renderRuntimeHtml($vehicleTypeWarehouseControl);
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $vehicleTypeWarehouseGrid = $form.find('[data-name="VehicleTypeWarehouseGrid"]');
        FwBrowse.search($vehicleTypeWarehouseGrid);

        this.disableFields();
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.find('div[data-datafield="AssetAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="AssetAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="IncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="IncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="SubIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="SubIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="EquipmentSaleIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="EquipmentSaleIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="LdIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="LdIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="CostOfGoodsSoldExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="CostOfGoodsSoldExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="CostOfGoodsRentedExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="CostOfGoodsRentedExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="DepreciationExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="DepreciationExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="AccumulatedDepreciationExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="AccumulatedDepreciationExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
    }
    //----------------------------------------------------------------------------------------------
    //beforeValidateInventoryType = function ($browse, $grid, request) {
    //    request.uniqueids = {
    //        Vehicle: true
    //    };
    //}
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InventoryTypeId':
                request.uniqueids = {
                    Vehicle: true
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'LicenseClassId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelicenseclass`);
                break;
            case 'UnitId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateunit`);
                break;
            case 'AssetAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateassetaccount`);
                break;
            case 'IncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateincomeaccount`);
                break;
            case 'SubIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubincomeaccount`);
                break;
            case 'EquipmentSaleIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateequipmentsaleincomeaccount`);
                break;
            case 'LdIncomeAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateldincomeaccount`);
                break;
            case 'CostOfGoodsSoldExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodssoldexpenseaccount`);
                break;
            case 'CostOfGoodsRentedExpenseAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecostofgoodsrentedexpenseaccount`);
                break;
        }
    }
}

var VehicleTypeController = new VehicleType();