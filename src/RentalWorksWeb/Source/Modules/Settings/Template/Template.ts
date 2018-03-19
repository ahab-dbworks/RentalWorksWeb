class Template {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Template';
        this.apiurl = 'api/v1/Template';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Template', false, 'BROWSE', true);
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

    renderGrids($form) {
        var $orderItemGridRental;
        var $orderItemGridRentalControl;
        $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'R'
            };

        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId')
        }
        );
        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);

        var $orderItemGridSales;
        var $orderItemGridSalesControl;
        $orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridSalesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridSales.empty().append($orderItemGridSalesControl);
        $orderItemGridSalesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'S'
            };

        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId')
        });
        FwBrowse.init($orderItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);


        var $orderItemGridFacilities;
        var $orderItemGridFacilitiesControl;
        $orderItemGridFacilities = $form.find('.facilitiesgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridFacilitiesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridFacilities.empty().append($orderItemGridFacilitiesControl);
        $orderItemGridFacilitiesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'F'
            };
        });
        $orderItemGridFacilitiesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId')
        }
        );
        FwBrowse.init($orderItemGridFacilitiesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridFacilitiesControl);


        //var $orderItemGridTransportation;
        //var $orderItemGridTransportationControl;
        //$orderItemGridTransportation = $form.find('.transportationgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridTransportationControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        //$orderItemGridTransportation.empty().append($orderItemGridTransportationControl);
        //$orderItemGridTransportationControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
        //        RecType: 'T'
        //    };
        //});
        //$orderItemGridTransportationControl.data('beforesave', function (request) {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId')
        //}
        //);
        //FwBrowse.init($orderItemGridTransportationControl);
        //FwBrowse.renderRuntimeHtml($orderItemGridTransportationControl);

        var $orderItemGridLabor;
        var $orderItemGridLaborControl;
        $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridLaborControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridLabor.empty().append($orderItemGridLaborControl);
        $orderItemGridLaborControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'L'
            };
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId')
        });
        FwBrowse.init($orderItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);


        var $orderItemGridMisc;
        var $orderItemGridMiscControl;
        $orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridMiscControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridMisc.empty().append($orderItemGridMiscControl);
        $orderItemGridMiscControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'M'
            };
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId')
        }
        );
        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);


        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.facilitiesgrid .valtype')).attr('data-validationname', 'FacilityRateValidation');
        //jQuery($form.find('.transportationgrid .valtype')).attr('data-validationname', 'FacilityRateValidation');

    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TemplateId"] input').val(uniqueids.TemplateId);
        FwModule.loadForm(this.Module, $form);

        $form.find('.rectype .fwformfield').on('change', function (e) {
            var rectype = jQuery(e.currentTarget).attr('data-datafield');
            switch (rectype) {
                case 'Rental':
                    $form.find('.rental').toggle();
                    break;
                case 'Sales':
                    $form.find('.sales').toggle();
                    break;
                case 'Facilities':
                    $form.find('.facilities').toggle();
                    break;
                case 'Transportation':
                    $form.find('.transportation').toggle();
                    break;
                case 'Labor':
                    $form.find('.labor').toggle();
                    break;
                case 'Miscellaneous':
                    $form.find('.miscellaneous').toggle();
                    break;

            }
        })

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="TemplateId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $orderItemGridRental;
        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridRental);

        var $orderItemGridSales;
        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridSales);

        var $orderItemGridFacilities;
        $orderItemGridFacilities = $form.find('.facilitiesgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridFacilities);

        //var $orderItemGridTransportation;
        //$orderItemGridTransportation = $form.find('.transportationgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridTransportation);

        var $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridLabor);

        var $orderItemGridMisc;
        $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridMisc);

        var checkboxes = $form.find('.rectype .fwformfield')
        for (var i = 0; i < checkboxes.length; i++) {
            var type = jQuery(checkboxes[i]).attr('data-datafield');
            var isChecked = FwFormField.getValueByDataField($form, type);
            var typeLowerCase = type.toLowerCase()

            if (isChecked === true) {
                $form.find('.' + typeLowerCase).show();
            } else {
                $form.find('.' + typeLowerCase).hide();
            }
        }

        jQuery($form.find('[data-grid="OrderItemGrid"] [data-browsedatafield="FromDate"], [data-browsedatafield="ToDate"], [data-browsedatafield="BillablePeriods"], [data-browsedatafield="SubQuantity"], [data-browsedatafield="AvailableQuantity"]')).parent().hide();
    }
}

var TemplateController = new Template();