class FiscalYear {
    Module: string = 'FiscalYear';
    apiurl: string = 'api/v1/fiscalyear';
    caption: string = Constants.Modules.Settings.children.FiscalYearSettings.children.FiscalYear.caption;
    nav: string = Constants.Modules.Settings.children.FiscalYearSettings.children.FiscalYear.nav;
    id: string = Constants.Modules.Settings.children.FiscalYearSettings.children.FiscalYear.id;
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

    renderGrids($form: any) {
        const $fiscalYearGrid = $form.find('div[data-grid="FiscalMonthGrid"]');
        const $fiscalYearControl = FwBrowse.loadGridFromTemplate('FiscalMonthGrid');
        $fiscalYearGrid.empty().append($fiscalYearControl);
        $fiscalYearControl.data('ondatabind', request => {
            request.uniqueids = {
                FiscalYearId: FwFormField.getValueByDataField($form, 'FiscalYearId')
            }
        });
        FwBrowse.init($fiscalYearControl);
        FwBrowse.renderRuntimeHtml($fiscalYearControl);
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="FiscalYearId"] input').val(uniqueids.FiscalYearId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
        const $fiscalYearGrid = $form.find('[data-name="FiscalMonthGrid"]');
        FwBrowse.search($fiscalYearGrid);
    }
}

var FiscalYearController = new FiscalYear();