class Template {
    Module: string = 'Template';
    apiurl: string = 'api/v1/Template';
    caption: string = Constants.Modules.Settings.children.TemplateSettings.children.Template.caption;
    nav: string = Constants.Modules.Settings.children.TemplateSettings.children.Template.nav;
    id: string = Constants.Modules.Settings.children.TemplateSettings.children.Template.id;
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

        if (mode === 'NEW') {
            const office = JSON.parse(sessionStorage.getItem('location'));
            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValueByDataField($form, 'RateType', office.ratetype, office.ratetype);
        }
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        // ----------
        const $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridRentalControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridRentalControl.find('.column').attr('data-visible', 'false');
        $orderItemGridRentalControl.find('.template').parent().attr('data-visible', 'true');
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'R'
            };
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId');
            request.RecType = 'R';
        }
        );
        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);
        // ----------
        const $orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridSalesControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridSalesControl.find('.column').attr('data-visible', 'false');
        $orderItemGridSalesControl.find('.template').parent().attr('data-visible', 'true');
        $orderItemGridSales.empty().append($orderItemGridSalesControl);
        $orderItemGridSalesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'S'
            };
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId');
            request.RecType = 'S';
        });
        FwBrowse.init($orderItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);
        // ----------
        const $orderItemGridFacilities = $form.find('.facilitiesgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridFacilitiesControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridFacilitiesControl.find('.column').attr('data-visible', 'false');
        $orderItemGridFacilitiesControl.find('.template').parent().attr('data-visible', 'true');
        $orderItemGridFacilities.empty().append($orderItemGridFacilitiesControl);
        $orderItemGridFacilitiesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'SP'
            };
        });
        $orderItemGridFacilitiesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId');
            request.RecType = 'SP';
        }
        );
        FwBrowse.init($orderItemGridFacilitiesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridFacilitiesControl);

        //var $orderItemGridTransportation;
        //var $orderItemGridTransportationControl;
        //$orderItemGridTransportation = $form.find('.transportationgrid div[data-grid="OrderItemGrid"]');
        //$orderItemGridTransportationControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
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
        // ----------
        const $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridLaborControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridLaborControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        $orderItemGridLaborControl.find('.column').attr('data-visible', 'false');
        $orderItemGridLaborControl.find('.template').parent().attr('data-visible', 'true');
        $orderItemGridLabor.empty().append($orderItemGridLaborControl);
        $orderItemGridLaborControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'L'
            };
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId');
            request.RecType = 'L';
        });
        FwBrowse.init($orderItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);
        // ----------
        const $orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridMiscControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridMiscControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        $orderItemGridMiscControl.find('.column').attr('data-visible', 'false');
        $orderItemGridMiscControl.find('.template').parent().attr('data-visible', 'true');
        $orderItemGridMisc.empty().append($orderItemGridMiscControl);
        $orderItemGridMiscControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TemplateId'),
                RecType: 'M'
            };
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'TemplateId');
            request.RecType = 'M';
        }
        );
        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);
        // ----------
        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.facilitiesgrid .valtype')).attr('data-validationname', 'FacilityRateValidation');
        //jQuery($form.find('.transportationgrid .valtype')).attr('data-validationname', 'FacilityRateValidation');
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'RateType':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatederate`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TemplateId"] input').val(uniqueids.TemplateId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $orderItemGridRental;
        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridRental);

        var $orderItemGridSales;
        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridSales);

        var $orderItemGridFacilities;
        $orderItemGridFacilities = $form.find('.facilitiesgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridFacilities);

        //var $orderItemGridTransportation;
        //$orderItemGridTransportation = $form.find('.transportationgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridTransportation);

        var $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridLabor);

        const $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridMisc);


        //Click Event on tabs to load grids/browses
        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
        $form.on('click', '[data-type="tab"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabname = $tab.attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            if ($tab.hasClass('audittab') == false) {
                const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        try {
                            const $gridcontrol = jQuery($gridControls[i]);
                            FwBrowse.search($gridcontrol);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }

                const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
            $tab.addClass('tabGridsLoaded');
        });

        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridFacilities.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();

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

        let rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
            , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
            , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
            , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');

        $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
        $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
        $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
        $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();

        $form.find('.rectype [data-datafield="Rental"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                $form.find('[data-type="tab"][data-caption="Rental"]').show();
            } else {
                $form.find('[data-type="tab"][data-caption="Rental"]').hide();
            }
        });
        $form.find('[data-datafield="Sales"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                $form.find('[data-type="tab"][data-caption="Sales"]').show();
            } else {
                $form.find('[data-type="tab"][data-caption="Sales"]').hide();
            }
        });
        $form.find('[data-datafield="Miscellaneous"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                $form.find('[data-type="tab"][data-caption="Misc"]').show();
            } else {
                $form.find('[data-type="tab"][data-caption="Misc"]').hide();
            }
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                $form.find('[data-type="tab"][data-caption="Labor"]').show();
            } else {
                $form.find('[data-type="tab"][data-caption="Labor"]').hide();
            }
        });
    }
}

var TemplateController = new Template();

FwApplicationTree.clickEvents[Constants.Modules.Settings.children.TemplateSettings.children.Template.form.menuItems.Search.id] = function (e: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        const orderId = FwFormField.getValueByDataField($form, 'TemplateId');
        if (orderId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function.');
        } else {
            const search = new SearchInterface();
            search.renderSearchPopup($form, orderId, 'Template');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};