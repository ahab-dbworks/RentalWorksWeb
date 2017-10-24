declare var FwModule: any;
declare var FwBrowse: any;

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
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    renderGrids($form: any) {
        var $discountItemGrid
            , $discountItemControl
            , $discountItemRentalGrid
            , $discountItemRentalGridControl
            , $discountItemSalesGrid
            , $discountItemSalesGridControl
            , $discountItemLaborGrid
            , $discountItemLaborGridControl
            , $discountItemMiscGrid
            , $discountItemMiscGridControl;

        // load companytax Grid
        $discountItemGrid = $form.find('div[data-grid="DiscountItemGrid"]');
        $discountItemControl = jQuery(jQuery('#tmpl-grids-DiscountItemGridBrowse').html());
        $discountItemGrid.empty().append($discountItemControl);
        $discountItemControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DiscountItemId: $form.find('div.fwformfield[data-datafield="DiscountItemId"] input').val()
            }
        });
        FwBrowse.init($discountItemControl);
        FwBrowse.renderRuntimeHtml($discountItemControl);

        $discountItemRentalGrid = $form.find('div[data-grid="DiscountItemRentalGrid"]');
        $discountItemRentalGridControl = jQuery(jQuery('#tmpl-grids-DiscountItemRentalGridBrowse').html());
        $discountItemRentalGrid.empty().append($discountItemRentalGridControl);
        $discountItemRentalGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DiscountItemId: $form.find('div.fwformfield[data-datafield="DiscountItemId"] input').val(),
                RecType: "R"
            };
        })
        $discountItemRentalGridControl.data('beforesave', function (request) {
            request.DiscountItemId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        })
        FwBrowse.init($discountItemRentalGridControl);
        FwBrowse.renderRuntimeHtml($discountItemRentalGridControl);

        $discountItemSalesGrid = $form.find('div[data-grid="DiscountItemSalesGrid"]');
        $discountItemSalesGridControl = jQuery(jQuery('#tmpl-grids-DiscountItemSalesGridBrowse').html());
        $discountItemSalesGrid.empty().append($discountItemSalesGridControl);
        $discountItemSalesGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DiscountItemId: $form.find('div.fwformfield[data-datafield="DiscountItemId"] input').val(),
                RecType: "S"
            };
        })
        $discountItemSalesGridControl.data('beforesave', function (request) {
            request.DiscountItemId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        })
        FwBrowse.init($discountItemSalesGridControl);
        FwBrowse.renderRuntimeHtml($discountItemSalesGridControl);

        $discountItemLaborGrid = $form.find('div[data-grid="DiscountItemLaborGrid"]');
        $discountItemLaborGridControl = jQuery(jQuery('#tmpl-grids-DiscountItemLaborGridBrowse').html());
        $discountItemLaborGrid.empty().append($discountItemLaborGridControl);
        $discountItemLaborGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DiscountItemId: $form.find('div.fwformfield[data-datafield="DiscountItemId"] input').val(),
                RecType: "L"
            };
        })
        $discountItemLaborGridControl.data('beforesave', function (request) {
            request.DiscountItemId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        })
        FwBrowse.init($discountItemLaborGridControl);
        FwBrowse.renderRuntimeHtml($discountItemLaborGridControl);

        $discountItemMiscGrid = $form.find('div[data-grid="DiscountItemMiscGrid"]');
        $discountItemMiscGridControl = jQuery(jQuery('#tmpl-grids-DiscountItemMiscGridBrowse').html());
        $discountItemMiscGrid.empty().append($discountItemMiscGridControl);
        $discountItemMiscGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DiscountItemId: $form.find('div.fwformfield[data-datafield="DiscountItemId"] input').val(),
                RecType: "M"
            };
        })
        $discountItemMiscGridControl.data('beforesave', function (request) {
            request.DiscountItemId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        })
        FwBrowse.init($discountItemMiscGridControl);
        FwBrowse.renderRuntimeHtml($discountItemMiscGridControl);
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DiscountTemplateId"] input').val(uniqueids.DiscountTemplateId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="DiscountTemplateId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $discountItemControl
            , $discountItemRentalControl
            , $discountItemSalesControl
            , $discountItemLaborControl
            , $discountItemMiscControl;

        $discountItemControl = $form.find('[data-name="DiscountItemGrid"]');
        FwBrowse.search($discountItemControl);

        $discountItemRentalControl = $form.find('[data-name="DiscountItemRentalGrid"]');
        FwBrowse.search($discountItemRentalControl);

        $discountItemSalesControl = $form.find('[data-name="DiscountItemSalesGrid"]');
        FwBrowse.search($discountItemSalesControl);

        $discountItemLaborControl = $form.find('[data-name="DiscountItemLaborGrid"]');
        FwBrowse.search($discountItemLaborControl);

        $discountItemLaborControl = $form.find('[data-name="DiscountItemMiscGrid"]');
        FwBrowse.search($discountItemLaborControl);

        var rentalDays = parseFloat($form.find('div.fwformfield[data-datafield="RentalDaysPerWeek"] input').val());
        var rentalDecimals = rentalDays.toFixed(3);
        $form.find('div.fwformfield[data-datafield="RentalDaysPerWeek"] input').val(rentalDecimals);

        var spaceDays = parseFloat($form.find('div.fwformfield[data-datafield="SpaceDaysPerWeek"] input').val());
        var spaceDecimals = spaceDays.toFixed(3);
        $form.find('div.fwformfield[data-datafield="SpaceDaysPerWeek"] input').val(spaceDecimals);
 
    }

}

(<any>window).DiscountTemplateController = new DiscountTemplate();

