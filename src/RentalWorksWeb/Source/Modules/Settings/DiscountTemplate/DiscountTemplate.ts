class DiscountTemplate {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountTemplate';
        this.apiurl = 'api/v1/discounttemplate';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Discount Template', false, 'BROWSE', true);
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
        const $discountItemRentalGrid = $form.find('div[data-grid="DiscountItemRentalGrid"]');
        const $discountItemRentalGridControl = FwBrowse.loadGridFromTemplate('DiscountItemRentalGrid');
        $discountItemRentalGrid.empty().append($discountItemRentalGridControl);
        $discountItemRentalGridControl.data('ondatabind', request => {
            request.uniqueids = {
                DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                RecType: "R"
            };
        })
        $discountItemRentalGridControl.data('beforesave', request => {
            request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
            request.RecType = "R";
        })
        FwBrowse.init($discountItemRentalGridControl);
        FwBrowse.renderRuntimeHtml($discountItemRentalGridControl);

        const $discountItemSalesGrid = $form.find('div[data-grid="DiscountItemSalesGrid"]');
        const $discountItemSalesGridControl = FwBrowse.loadGridFromTemplate('DiscountItemSalesGrid');
        $discountItemSalesGrid.empty().append($discountItemSalesGridControl);
        $discountItemSalesGridControl.data('ondatabind', request => {
            request.uniqueids = {
                DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                RecType: "S"
            };
        })
        $discountItemSalesGridControl.data('beforesave', request => {
            request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
            request.RecType = "S";
        })
        FwBrowse.init($discountItemSalesGridControl);
        FwBrowse.renderRuntimeHtml($discountItemSalesGridControl);

        const $discountItemLaborGrid = $form.find('div[data-grid="DiscountItemLaborGrid"]');
        const $discountItemLaborGridControl = FwBrowse.loadGridFromTemplate('DiscountItemLaborGrid');
        $discountItemLaborGrid.empty().append($discountItemLaborGridControl);
        $discountItemLaborGridControl.data('ondatabind', request => {
            request.uniqueids = {
                DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                RecType: "L"
            };
        })
        $discountItemLaborGridControl.data('beforesave', request => {
            request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
            request.RecType = "L";
        })
        FwBrowse.init($discountItemLaborGridControl);
        FwBrowse.renderRuntimeHtml($discountItemLaborGridControl);

        const $discountItemMiscGrid = $form.find('div[data-grid="DiscountItemMiscGrid"]');
        const $discountItemMiscGridControl = FwBrowse.loadGridFromTemplate('DiscountItemMiscGrid');
        $discountItemMiscGrid.empty().append($discountItemMiscGridControl);
        $discountItemMiscGridControl.data('ondatabind', request => {
            request.uniqueids = {
                DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                RecType: "M"
            };
        })
        $discountItemMiscGridControl.data('beforesave', request => {
            request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
            request.RecType = "M";
        })
        FwBrowse.init($discountItemMiscGridControl);
        FwBrowse.renderRuntimeHtml($discountItemMiscGridControl);
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const office = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
        }
        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DiscountTemplateId"] input').val(uniqueids.DiscountTemplateId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
        const $discountItemControl = $form.find('[data-name="DiscountItemGrid"]');
        FwBrowse.search($discountItemControl);

        const $discountItemRentalControl = $form.find('[data-name="DiscountItemRentalGrid"]');
        FwBrowse.search($discountItemRentalControl);

        const $discountItemSalesControl = $form.find('[data-name="DiscountItemSalesGrid"]');
        FwBrowse.search($discountItemSalesControl);

        const $discountItemLaborControl = $form.find('[data-name="DiscountItemLaborGrid"]');
        FwBrowse.search($discountItemLaborControl);

        const $discountItemMiscControl = $form.find('[data-name="DiscountItemMiscGrid"]');
        FwBrowse.search($discountItemMiscControl);

        //const rentalDays = parseFloat(FwFormField.getValueByDataField($form, 'RentalDaysPerWeek'));
        //const rentalDecimals = rentalDays.toFixed(3);
        //FwFormField.setValueByDataField($form, 'RentalDaysPerWeek', rentalDecimals);

        //const spaceDays = parseFloat(FwFormField.getValueByDataField($form, 'SpaceDaysPerWeek'));
        //const spaceDecimals = spaceDays.toFixed(3);
        //FwFormField.setValueByDataField($form, 'SpaceDaysPerWeek', spaceDecimals);
    }
}

var DiscountTemplateController = new DiscountTemplate();