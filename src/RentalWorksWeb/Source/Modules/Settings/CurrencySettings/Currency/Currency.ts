class Currency {
    Module: string = 'Currency';
    apiurl: string = 'api/v1/currency';
    caption: string = Constants.Modules.Settings.children.CurrencySettings.children.Currency.caption;
    nav: string = Constants.Modules.Settings.children.CurrencySettings.children.Currency.nav;
    id: string = Constants.Modules.Settings.children.CurrencySettings.children.Currency.id;
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

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CurrencyId"] input').val(uniqueids.CurrencyId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        FwBrowse.renderGrid({
            nameGrid: 'CurrencyExchangeRateGrid',
            gridSecurityId: 'UfURKoOaUi87C',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    FromCurrencyId: FwFormField.getValueByDataField($form, 'CurrencyId')
                };
            },
            beforeSave: (request: any) => {
                request.FromCurrencyId = FwFormField.getValueByDataField($form, 'CurrencyId');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        FwBrowse.search($form.find('[data-name="CurrencyExchangeRateGrid"]'));
    }
}
//----------------------------------------------------------------------------------------------
var CurrencyController = new Currency();