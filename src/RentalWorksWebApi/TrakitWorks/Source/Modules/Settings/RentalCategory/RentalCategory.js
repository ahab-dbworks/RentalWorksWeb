class RentalCategory {
    constructor() {
        this.Module = 'RentalCategory';
        this.apiurl = 'api/v1/rentalcategory';
        this.caption = 'Rental Category';
        this.nav = 'module/rentalcategory';
        this.id = '91079439-A188-4637-B733-A7EF9A9DFC22';
    }
    getModuleScreen() {
        const screen = {};
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
    events($form) {
        $form.on('change', '.overridecheck input[type=checkbox]', (e) => {
            const $overrideCheck = jQuery(e.currentTarget), $categoryValidation = $form.find('.catvalidation');
            this.toggleEnabled($overrideCheck, $categoryValidation);
        });
    }
    toggleEnabled($checkbox, $validation) {
        if ($checkbox.is(':checked')) {
            $validation.attr('data-enabled', 'true');
        }
        else {
            $validation.attr('data-enabled', 'false');
        }
    }
    renderGrids($form) {
        const $subCategoryGrid = $form.find('div[data-grid="SubCategoryGrid"]');
        const $subCategoryControl = FwBrowse.loadGridFromTemplate('SubCategoryGrid');
        $subCategoryGrid.empty().append($subCategoryControl);
        $subCategoryControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CategoryId: $form.find('div.fwformfield[data-datafield="CategoryId"] input').val()
            };
        });
        $subCategoryControl.data('beforesave', function (request) {
            request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        });
        FwBrowse.init($subCategoryControl);
        FwBrowse.renderRuntimeHtml($subCategoryControl);
    }
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        this.events($form);
        $form.find('[data-datafield="CatalogCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.designer'));
                FwFormField.disable($form.find('.barcodetype'));
            }
            else {
                FwFormField.disable($form.find('.designer'));
                FwFormField.enable($form.find('.barcodetype'));
            }
        });
        this.toggleEnabled($form.find('.overridecheck input[type=checkbox]'), $form.find('.catvalidation'));
        $form.find('div[data-datafield="IncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="IncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="SubIncomeAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="SubIncomeAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="AssetAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="AssetAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
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
        return $form;
    }
    loadForm(uniqueids) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CategoryId"] input').val(uniqueids.CategoryId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
        const $laborCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
        FwBrowse.search($laborCategoryGrid);
        if ($form.find('[data-datafield="CatalogCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('.designer'));
            FwFormField.disable($form.find('.barcodetype'));
        }
        else {
            FwFormField.disable($form.find('.designer'));
            FwFormField.enable($form.find('.barcodetype'));
        }
    }
    beforeValidate($browse, $grid, request) {
        request.uniqueids = {
            Rental: true
        };
    }
}
var RentalCategoryController = new RentalCategory();
//# sourceMappingURL=RentalCategory.js.map