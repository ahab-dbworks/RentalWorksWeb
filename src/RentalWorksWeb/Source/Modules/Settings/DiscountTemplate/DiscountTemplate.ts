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
        var $discountItemGrid, $discountItemControl;

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
        var $discountItemControl;

        $discountItemControl = $form.find('[data-name="DiscountItemGrid"]');
        FwBrowse.search($discountItemControl);
    }
}

(<any>window).DiscountTemplateController = new DiscountTemplate();

