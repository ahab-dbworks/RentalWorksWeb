class FiscalYear {
    Module: string = 'FiscalYear';
    apiurl: string = 'api/v1/fiscalyear';
    caption: string = Constants.Modules.Settings.children.FiscalYearSettings.children.FiscalYear.caption;
    nav: string = Constants.Modules.Settings.children.FiscalYearSettings.children.FiscalYear.nav;
    id: string = Constants.Modules.Settings.children.FiscalYearSettings.children.FiscalYear.id;


    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
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

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
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