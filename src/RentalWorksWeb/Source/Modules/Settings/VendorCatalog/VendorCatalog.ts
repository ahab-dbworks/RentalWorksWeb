declare var FwModule: any;
declare var FwBrowse: any;

class VendorCatalog {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'VendorCatalog';
        this.apiurl = 'api/v1/vendorcatalog';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Vendor Catalog', false, 'BROWSE', true);
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

        return $browse;
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

        $form.find('div.fwformfield[data-datafield="VendorCatalogId"] input').val(uniqueids.VendorCatalogId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="VendorCatalogId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var value = $form.find('input[type="radio"]:checked').val();
        var hideAll = function () {
            $form.find('.rentalcategory').hide();
            $form.find('.salescategory').hide();
            $form.find('.partscategory').hide();
        }

        if (value === 'RENTAL') {
            $form.find('.rentalcategory').show();
        }
        if (value === 'SALES') {
            $form.find('.salescategory').show();
        }
        if (value === 'PARTS') {
            $form.find('.partscategory').show();
        }

        $form.find('[data-datafield="CatalogType"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this).val();
            if ($this === 'RENTAL') {
                hideAll();
                $form.find('.rentalcategory').show();
            }
            if ($this === 'SALES') {
                hideAll();
                $form.find('.salescategory').show();
            }
            if ($this === 'PARTS') {
                hideAll();
                $form.find('.partscategory').show();
            }
        })
    }
}

(<any>window).VendorCatalogController = new VendorCatalog();

