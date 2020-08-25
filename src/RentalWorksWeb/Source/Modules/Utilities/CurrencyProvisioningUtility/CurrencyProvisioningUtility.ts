routes.push({ pattern: /^module\/currencyprovisioningutility$/, action: function (match: RegExpExecArray) { return CurrencyProvisioningUtilityController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class CurrencyProvisioningUtility {
    Module: string = 'CurrencyProvisioningUtility';
    caption: string = Constants.Modules.Utilities.children.CurrencyProvisioningUtility.caption;
    apiurl: string = 'api/v1/currencymissingutility';
    nav: string = Constants.Modules.Utilities.children.CurrencyProvisioningUtility.nav;
    id: string = Constants.Modules.Utilities.children.CurrencyProvisioningUtility.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');
        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('[data-datafield="ApplyCurrency"]').on('change', e => {
            const isChecked = FwFormField.getValueByDataField($form, 'ApplyCurrency');
            const $applyCurrencyBtn = $form.find('.apply');
            isChecked ? $applyCurrencyBtn.show() : $applyCurrencyBtn.hide();
        });

        $form.find('.apply').on('click', e => {
            const $grid = $form.find('[data-name="CurrencyMissingGrid"]');
            FwAppData.apiMethod(true, 'POST', 'api/v1/currencymissingutility/applyproposedcurrencies', null, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($grid);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'CurrencyMissingGrid',
            gridSecurityId: 'qH0cLrQVt9avI',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
        });

        FwBrowse.search($form.find('[data-name="CurrencyMissingGrid"]'));
    }
    //----------------------------------------------------------------------------------------------

};
//----------------------------------------------------------------------------------------------
var CurrencyProvisioningUtilityController = new CurrencyProvisioningUtility();