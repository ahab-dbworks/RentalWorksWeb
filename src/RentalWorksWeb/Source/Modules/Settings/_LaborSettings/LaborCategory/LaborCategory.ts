class LaborCategory {
    Module:  string = 'LaborCategory';
    apiurl:  string = 'api/v1/laborcategory';
    caption: string = Constants.Modules.Settings.children.LaborSettings.children.LaborCategory.caption;
    nav:     string = Constants.Modules.Settings.children.LaborSettings.children.LaborCategory.nav;
    id:      string = Constants.Modules.Settings.children.LaborSettings.children.LaborCategory.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

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
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.on('change', '.overridecheck input[type=checkbox]', (e) => {
            var $overrideCheck = jQuery(e.currentTarget), $categoryValidation = $form.find('.catvalidation');

            this.toggleEnabled($overrideCheck, $categoryValidation);
        });
    }
    //----------------------------------------------------------------------------------------------
    toggleEnabled($checkbox: JQuery, $validation: JQuery): void {
        if ($checkbox.is(':checked')) {
            $validation.attr('data-enabled', 'true');
        } else {
            $validation.attr('data-enabled', 'false');
        }
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        //const $subCategoryGrid = $form.find('div[data-grid="SubCategoryGrid"]');
        //const $subCategoryControl = FwBrowse.loadGridFromTemplate('SubCategoryGrid');
        //$subCategoryGrid.empty().append($subCategoryControl);
        //$subCategoryControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        CategoryId: FwFormField.getValueByDataField($form, 'CategoryId')
        //    }
        //});
        //$subCategoryControl.data('beforesave', request => {
        //    request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        //})
        //FwBrowse.init($subCategoryControl);
        //FwBrowse.renderRuntimeHtml($subCategoryControl);

        FwBrowse.renderGrid({
            nameGrid: 'SubCategoryGrid',
            gridSecurityId: 'vHMa0l5PUysXo',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CategoryId: FwFormField.getValueByDataField($form, 'CategoryId')
                };
            },
            beforeSave: (request: any) => {
                request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            },
        });
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

        this.toggleEnabled($form.find('.overridecheck input[type=checkbox]'), $form.find('.catvalidation'));

        $form.find('div[data-datafield="IncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="IncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="SubIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="SubIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="ExpenseAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="ExpenseAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CategoryId"] input').val(uniqueids.CategoryId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $laborCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
        FwBrowse.search($laborCategoryGrid);
    }
}
//----------------------------------------------------------------------------------------------
var LaborCategoryController = new LaborCategory();