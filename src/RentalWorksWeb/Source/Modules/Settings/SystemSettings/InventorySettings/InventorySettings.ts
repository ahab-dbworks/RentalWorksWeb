routes.push({ pattern: /^module\/inventorysettings$/, action: function (match: RegExpExecArray) { return InventorySettingsController.getModuleScreen(); } });

class InventorySettings {
    Module: string = 'InventorySettings';
    apiurl: string = 'api/v1/inventorysettings';
    caption: string = Constants.Modules.Settings.children.SystemSettings.children.InventorySettings.caption;
    nav: string = Constants.Modules.Settings.children.SystemSettings.children.InventorySettings.nav;
    id: string = Constants.Modules.Settings.children.SystemSettings.children.InventorySettings.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
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


        //Toggle Buttons
        FwFormField.loadItems($form.find('div[data-datafield="DefaultRentalQuantityInventoryCostCalculation"]'), [
            { value: 'FIFO', caption: 'First In, First Out' },
            { value: 'LIFO', caption: 'Last In, First Out' },
            { value: 'AVERAGEVALUE', caption: 'Average Value' }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="DefaultSalesQuantityInventoryCostCalculation"]'), [
            { value: 'FIFO', caption: 'First In, First Out' },
            { value: 'LIFO', caption: 'Last In, First Out' },
            { value: 'AVERAGEVALUE', caption: 'Average Value' }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="DefaultPartsQuantityInventoryCostCalculation"]'), [
            { value: 'FIFO', caption: 'First In, First Out' },
            { value: 'LIFO', caption: 'Last In, First Out' },
            { value: 'AVERAGEVALUE', caption: 'Average Value' }
        ]);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventorySettingsId"] input').val(uniqueids.InventorySettingsId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const userAssignedICodes = FwFormField.getValueByDataField($form, 'UserAssignedICodes');
        if (userAssignedICodes) {
            FwFormField.disable($form.find('[data-datafield="LastICode"]'));
            FwFormField.disable($form.find('[data-datafield="ICodePrefix"]'));
        }
        else {
            FwFormField.enable($form.find('[data-datafield="LastICode"]'));
            FwFormField.enable($form.find('[data-datafield="ICodePrefix"]'));
        }
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        $form.find('[data-datafield="UserAssignedICodes"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                FwFormField.disable($form.find('[data-datafield="LastICode"]'));
                FwFormField.disable($form.find('[data-datafield="ICodePrefix"]'));
            }
            else {
                FwFormField.enable($form.find('[data-datafield="LastICode"]'));
                FwFormField.enable($form.find('[data-datafield="ICodePrefix"]'));
            }
        });

        $form.find('[data-datafield="IncludeTaxInOriginalEquipmentCost"] input').on('change', e => {
            const originalVal = ($form.find('[data-datafield="IncludeTaxInOriginalEquipmentCost"]').attr('data-originalvalue') === "true");
            const newVal = FwFormField.getValue2($form.find('[data-datafield="IncludeTaxInOriginalEquipmentCost"]'));

            if (originalVal == newVal) {
                $form.find('.depreciationoecwarning').hide();
            }
            else {
                $form.find('.depreciationoecwarning').show();
            }
        });


        $form.find('[data-datafield="StartDepreciatingFixedAssetsTheMonthAfterTheyAreReceived"] input').on('change', e => {
            const originalVal = ($form.find('[data-datafield="StartDepreciatingFixedAssetsTheMonthAfterTheyAreReceived"]').attr('data-originalvalue') === "true");
            const newVal = FwFormField.getValue2($form.find('[data-datafield="StartDepreciatingFixedAssetsTheMonthAfterTheyAreReceived"]'));

            if (originalVal == newVal) {
                $form.find('.depreciationmonthwarning').hide();
            }
            else {
                $form.find('.depreciationmonthwarning').show();
            }
        });

        $form.find('[data-datafield="DefaultRentalQuantityInventoryCostCalculation"]').on('change', e => {
            const originalVal = $form.find('[data-datafield="DefaultRentalQuantityInventoryCostCalculation"]').attr('data-originalvalue');
            const newVal = FwFormField.getValue2($form.find('[data-datafield="DefaultRentalQuantityInventoryCostCalculation"]'));
        
            if (originalVal == newVal) {
                $form.find('.rentalcostwarning').hide();
            }
            else {
                $form.find('.rentalcostwarning').show();
            }
        });
        
        $form.find('[data-datafield="DefaultSalesQuantityInventoryCostCalculation"]').on('change', e => {
            const originalVal = $form.find('[data-datafield="DefaultSalesQuantityInventoryCostCalculation"]').attr('data-originalvalue');
            const newVal = FwFormField.getValue2($form.find('[data-datafield="DefaultSalesQuantityInventoryCostCalculation"]'));
        
            if (originalVal == newVal) {
                $form.find('.salescostwarning').hide();
            }
            else {
                $form.find('.salescostwarning').show();
            }
        });
        
        $form.find('[data-datafield="DefaultPartsQuantityInventoryCostCalculation"]').on('change', e => {
            const originalVal = $form.find('[data-datafield="DefaultPartsQuantityInventoryCostCalculation"]').attr('data-originalvalue');
            const newVal = FwFormField.getValue2($form.find('[data-datafield="DefaultPartsQuantityInventoryCostCalculation"]'));
        
            if (originalVal == newVal) {
                $form.find('.partscostwarning').hide();
            }
            else {
                $form.find('.partscostwarning').show();
            }
        });



    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var InventorySettingsController = new InventorySettings();