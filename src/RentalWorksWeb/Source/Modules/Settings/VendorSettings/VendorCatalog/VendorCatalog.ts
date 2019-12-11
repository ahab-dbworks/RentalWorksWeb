class VendorCatalog {
    Module: string = 'VendorCatalog';
    apiurl: string = 'api/v1/vendorcatalog';
    caption: string = Constants.Modules.Settings.children.VendorSettings.children.VendorCatalog.caption;
    nav: string = Constants.Modules.Settings.children.VendorSettings.children.VendorCatalog.nav;
    id: string = Constants.Modules.Settings.children.VendorSettings.children.VendorCatalog.id;
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

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');

        $form.find('div.fwformfield[data-datafield="VendorCatalogId"] input').val(uniqueids.VendorCatalogId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    //beforeValidateInventoryType($browse, $grid, request) {
    //    var value = $grid.find('input[type="radio"]:checked').val();
    //    switch (value) {
    //        case 'RENTAL':
    //            request.uniqueids = {
    //                Rental: true
    //            }
    //            break;
    //        case 'SALES':
    //            request.uniqueids = {
    //                Sales: true
    //            }
    //            break;
    //        case 'PARTS':
    //            request.uniqueids = {
    //                Parts: true
    //            }
    //            break;
    //    }
    //}

    //beforeValidateShipVia($browse, $grid, request) {
    //    var validationName = request.module;
    //    var VendorIdValue = jQuery($grid.find('[data-datafield="CarrierId"] input')).val();

    //    switch (validationName) {
    //        case 'ShipViaValidation':
    //            request.uniqueids = {
    //                VendorId: VendorIdValue
    //            };
    //            break;
    //    }
    //}

    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const VendorIdValue = jQuery($form.find('[data-datafield="CarrierId"] input')).val();
        const value = $form.find('input[type="radio"]:checked').val();
        switch (datafield) {
            case 'InventoryTypeId':
                switch (value) {
                    case 'RENTAL':
                        request.uniqueids = {
                            Rental: true
                        }
                        break;
                    case 'SALES':
                        request.uniqueids = {
                            Sales: true
                        }
                        break;
                    case 'PARTS':
                        request.uniqueids = {
                            Parts: true
                        }
                        break;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'VendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatevendor`);
                break;
            case 'CarrierId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecarrier`);
                break;
            case 'ShipViaId':
                request.uniqueids = {
                    VendorId: VendorIdValue
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateshipvia`);
                break;
        }
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

var VendorCatalogController = new VendorCatalog();