routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return QuoteController.getModuleScreen(filter); } });

class Quote extends OrderBase {
    Module:             string = 'Quote';
    apiurl:             string = 'api/v1/quote';
    caption:            string = 'Quote';
    nav:                string = 'module/quote';
    id:                 string = '9213AF53-6829-4276-9DF9-9DAA704C2CCF';
    ActiveViewFields:   any    = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var screen: any = {};
        screen.$view    = FwModule.getModuleControl(this.Module + 'Controller');

        var $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Quote', false, 'BROWSE', true);

            if (typeof filter !== 'undefined') {
                filter.search = filter.search.replace(/%20/, ' ');
                var datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
            }

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
        //var self = this;
        //var $browse;
        //$browse = FwBrowse.loadBrowseFromTemplate(this.Module);

        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                $tr.css('color', '#aaaaaa');
            }
        });

        const self = this;
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        var department = JSON.parse(sessionStorage.getItem('department'));;
        var location   = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultOrderType = response.DefaultOrderType;
            self.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        const $all:       JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $prospect:  JQuery = FwMenu.generateDropDownViewBtn('Prospect', true, "PROSPECT");
        const $active:    JQuery = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $reserved:  JQuery = FwMenu.generateDropDownViewBtn('Reserved', false, "RESERVED");
        const $ordered:   JQuery = FwMenu.generateDropDownViewBtn('Ordered', false, "ORDERED");
        const $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed:    JQuery = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        FwMenu.addVerticleSeparator($menuObject);

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $prospect, $active, $reserved, $ordered, $cancelled, $closed);
        FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");

        return $menuObject;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        //var $form;
        var self = this;

        //$form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            $form.find('.combinedtab').hide();

            var today = FwFunc.getDate();
            var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            var office = JSON.parse(sessionStorage.getItem('location'));
            var department = JSON.parse(sessionStorage.getItem('department'));

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);

            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
            FwFormField.setValueByDataField($form, 'BillingMonths', '0');
            FwFormField.setValueByDataField($form, 'VersionNumber', 1);

            $form.find('div[data-datafield="DealId"]').attr('data-required', 'false');
            $form.find('div[data-datafield="PickTime"]').attr('data-required', 'false');

            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            $form.find('div[data-datafield="PendingPo"] input').prop('checked', true);
            $form.find('div[data-datafield="Rental"] input').prop('checked', true);
            $form.find('div[data-datafield="Sales"] input').prop('checked', true);
            $form.find('div[data-datafield="Miscellaneous"] input').prop('checked', true);
            $form.find('div[data-datafield="Labor"] input').prop('checked', true);
            FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
            $form.find('[data-type="tab"][data-caption="Used Sale"]').hide();
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));

            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);

            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();
        }

        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', 'false');
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', 'false');
        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        FwFormField.loadItems($form.find('.outtype'), [
            { value: 'DELIVER', text: 'Deliver to Customer' },
            { value: 'SHIP', text: 'Ship to Customer' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);

        FwFormField.loadItems($form.find('.intype'), [
            { value: 'DELIVER', text: 'Customer Deliver' },
            { value: 'SHIP', text: 'Customer Ship' },
            { value: 'PICK UP', text: 'Pick Up from Customer' }
        ], true);

        FwFormField.loadItems($form.find('.online'), [
            { value: 'PARTIAL', text: 'Partial' },
            { value: 'COMPLETE', text: 'Complete' }
        ], true);

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
            FwFormField.setValue($form, 'div[data-datafield="RateType"]', parentModuleInfo.RateTypeId, parentModuleInfo.RateType);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', parentModuleInfo.BillingCycleId, parentModuleInfo.BillingCycle);
        }

        this.events($form);
        this.activityCheckboxEvents($form, mode);
        if (typeof parentModuleInfo !== 'undefined' && mode !== 'NEW') {
            this.renderFrames($form, parentModuleInfo.QuoteId);
            //this.dynamicColumns($form, parentModuleInfo.OrderTypeId);
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let self        = this;
        var totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]

        var $orderStatusHistoryGrid        = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        var $orderStatusHistoryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusHistoryGridBrowse').html());
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: $form.find('div.fwformfield[data-datafield="QuoteId"] input').val()
            };
        })
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);

        var $orderItemGridRental        = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        var $orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRental.addClass('R');
        $orderItemGridRentalControl.data('isSummary', false);

        $orderItemGridRentalControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'R'
            };
            request.totalfields = totalFields;
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'R';
        });
        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', ($control, dt) => {
            let rentalItems = $form.find('.rentalgrid tbody').children();
            rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });
        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);

        var $orderItemGridUsedSale        = $form.find('.usedsalegrid div[data-grid="OrderItemGrid"]');
        var $orderItemGridUsedSaleControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridUsedSale.empty().append($orderItemGridUsedSaleControl);
        $orderItemGridUsedSale.addClass('RS');
        $orderItemGridUsedSaleControl.data('isSummary', false);
        $orderItemGridUsedSaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'RS'
            };
            request.totalfields = totalFields;
        });
        $orderItemGridUsedSaleControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'RS';
        });
        FwBrowse.init($orderItemGridUsedSaleControl);
        FwBrowse.renderRuntimeHtml($orderItemGridUsedSaleControl);

        var $allOrderItemGrid        = $form.find('.combinedgrid div[data-grid="OrderItemGrid"]');
        var $allOrderItemGridControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $allOrderItemGridControl.find('.combined').attr('data-visible', 'true');
        $allOrderItemGridControl.find('.individual').attr('data-visible', 'false');
        $allOrderItemGrid.empty().append($allOrderItemGridControl);
        $allOrderItemGrid.addClass('A');
        $allOrderItemGridControl.data('isSummary', false);
        $allOrderItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId')
            };
            request.totalfields = totalFields;
        });
        $allOrderItemGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
        }
        );
        FwBrowse.addEventHandler($allOrderItemGridControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'combined', dt.Totals);
        });

        FwBrowse.init($allOrderItemGridControl);
        FwBrowse.renderRuntimeHtml($allOrderItemGridControl);

        var $orderNoteGrid        = $form.find('div[data-grid="OrderNoteGrid"]');
        var $orderNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderNoteGridBrowse').html());
        $orderNoteGrid.empty().append($orderNoteGridControl);
        $orderNoteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId')
            };
        });
        $orderNoteGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        });
        FwBrowse.init($orderNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderNoteGridControl);

        var $orderContactGrid        = $form.find('div[data-grid="OrderContactGrid"]');
        var $orderContactGridControl = jQuery(jQuery('#tmpl-grids-OrderContactGridBrowse').html());
        $orderContactGrid.empty().append($orderContactGridControl);
        $orderContactGridControl.find('div[data-datafield="IsOrderedBy"]').attr('data-caption', 'Quoted For');
        $orderContactGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId')
            };
        });
        $orderContactGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($orderContactGridControl);
        FwBrowse.renderRuntimeHtml($orderContactGridControl);

        let itemGrids = [$orderItemGridRental];
        if ($form.attr('data-mode') === 'NEW') {
            for (var i = 0; i < itemGrids.length; i++) {
                itemGrids[i].find('.btn').filter(function () { return jQuery(this).data('type') === 'NewButton' })
                    .off()
                    .on('click', function () {
                        self.saveForm($form, { closetab: false });
                    })
            }
        }

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.usedsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);
        let $pending    = $form.find('div.fwformfield[data-datafield="PendingPo"] input').prop('checked');
        let status      = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes    = FwFormField.getValueByDataField($form, 'HasNotes');
        let rentalTab   = $form.find('[data-type="tab"][data-caption="Rental"]'),
            salesTab    = $form.find('[data-type="tab"][data-caption="Sales"]'),
            miscTab     = $form.find('[data-type="tab"][data-caption="Miscellaneous"]'),
            laborTab    = $form.find('[data-type="tab"][data-caption="Labor"]'),
            usedSaleTab = $form.find('[data-type="tab"][data-caption="Used Sale"]')
            //lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss and Damage"]')

        if ($form.find('[data-datafield="CombineActivity"] input').val() === 'false') {
            // show / hide tabs
            if (!FwFormField.getValueByDataField($form, 'Rental')) { rentalTab.hide(), FwFormField.disable($form.find('[data-datafield="RentalSale"]')); }
            if (!FwFormField.getValueByDataField($form, 'Sales')) { salesTab.hide() }
            if (!FwFormField.getValueByDataField($form, 'Miscellaneous')) { miscTab.hide() }
            if (!FwFormField.getValueByDataField($form, 'Labor')) { laborTab.hide() }
            if (!FwFormField.getValueByDataField($form, 'RentalSale')) { usedSaleTab.hide() }
            //if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) { lossDamageTab.hide(), FwFormField.disable($form.find('[data-datafield="Rental"]')); }
        }

        if (status === 'ORDERED' || status === 'CLOSED' || status === 'CANCELLED') {
            FwModule.setFormReadOnly($form);
        }

        if (hasNotes) {
            FwTabs.setTabColor($form.find('.notestab'), '#FFFF00');
        }

        var $orderStatusHistoryGrid = $form.find('[data-name="OrderStatusHistoryGrid"]');
        var $orderItemGridRental    = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        var $orderItemGridSales     = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        var $orderItemGridLabor     = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        var $orderItemGridMisc      = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        var $orderNoteGrid          = $form.find('[data-name="OrderNoteGrid"]');
        var $orderContactGrid       = $form.find('[data-name="OrderContactGrid"]');
        var $allOrderItemGrid       = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
        var $orderItemGridUsedSale  = $form.find('.usedsalegrid [data-name="OrderItemGrid"]');

        if (FwFormField.getValueByDataField($form, 'DisableEditingUsedSaleRate')) {
            $orderItemGridUsedSale.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingMiscellaneousRate')) {
            $orderItemGridMisc.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingLaborRate')) {
            $orderItemGridLabor.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingSalesRate')) {
            $orderItemGridSales.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingRentalRate')) {
            $orderItemGridRental.find('.rates').attr('data-formreadonly', true);
        }

        //hide subworksheet and add LD items
        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $allOrderItemGrid.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();
        $orderItemGridUsedSale.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();

        if (FwFormField.getValueByDataField($form, 'HasRentalItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        }
        if (FwFormField.getValueByDataField($form, 'HasSalesItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Sales'));
        }
        if (FwFormField.getValueByDataField($form, 'HasMiscellaneousItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Miscellaneous'));
        }
        if (FwFormField.getValueByDataField($form, 'HasLaborItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'Labor'));
        }
        if (FwFormField.getValueByDataField($form, 'HasRentalSaleItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'RentalSale'));
        }

        var rate = FwFormField.getValueByDataField($form, 'RateType');
        if (rate === '3WEEK') {
            $allOrderItemGrid.find('.3week').parent().show();
            $allOrderItemGrid.find('.weekextended').parent().hide();
            $allOrderItemGrid.find('.price').find('.caption').text('Week 1 Rate');
            $orderItemGridRental.find('.3week').parent().show();
            $orderItemGridRental.find('.weekextended').parent().hide();
            $orderItemGridRental.find('.price').find('.caption').text('Week 1 Rate');
        }
        // Display weeks or month field in billing tab
        if (rate === 'MONTHLY') {
            $form.find(".BillingWeeks").hide();
            $form.find(".BillingMonths").show();
        } else {
            $form.find(".BillingMonths").hide();
            $form.find(".BillingWeeks").show();
        }
        // Display D/W field in rental
        if (rate === 'DAILY') {
            $form.find(".RentalDaysPerWeek").show();
            $form.find(".combineddw").show();
            $allOrderItemGrid.find('.dw').parent().show();
            $orderItemGridRental.find('.dw').parent().show();
            $orderItemGridLabor.find('.dw').parent().show();
            $orderItemGridMisc.find('.dw').parent().show();
        } else {
            $form.find(".RentalDaysPerWeek").hide();
        }

        if ($pending === true) {
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.enable($form.find('[data-datafield="PoAmount"]'));
        }

        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');

        var noChargeValue = FwFormField.getValueByDataField($form, 'NoCharge');
        if (noChargeValue == false) {
            FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        }

        // Disable withTax checkboxes if Total field is 0.00
        //this.disableWithTaxCheckbox($form);
    }
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Quote" data-control="FwBrowse" data-type="Browse" id="QuoteBrowse" class="fwcontrol fwbrowse" data-orderby="QuoteId" data-sort="desc" data-controller="QuoteController">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="QuoteId" data-datatype="key" data-sort="off"></div>
          </div>
          <!--<div class="column" data-width="0" data-visible="false">
            <div class="field" data-datafield="Inactive" data-datatype="text"  data-visible="false"></div>
          </div>-->
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Quote No" data-datafield="QuoteNumber" data-cellcolor="QuoteNumberColor" data-datatype="text" data-sort="off" data-sortsequence="2" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Date" data-datafield="QuoteDate" data-browsedatatype="date" data-sortsequence="1" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="20px" data-visible="true">
            <div class="field" data-caption="Version" data-datafield="VersionNumber" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="350px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-cellcolor="DescriptionColor" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Deal No" data-datafield="DealNumber" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-cellcolor="StatusColor" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Status As Of" data-datafield="StatusDate" data-datatype="date" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="180px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="150px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-cellcolor="WarehouseColor" data-datatype="text" data-sort="off"></div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="quoteform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Equipment Request" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="QuoteController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="QuoteId"></div>
          <div id="quoteform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="generaltab" class="generaltab tab" data-tabpageid="generaltabpage" data-caption="Request"></div>
              <div data-type="tab" id="rentaltab" class="notcombinedtab tab" data-tabpageid="rentaltabpage" data-caption="Items" style="display:none;"></div>
              <div data-type="tab" id="salestab" class="notcombinedtab tab" data-tabpageid="salestabpage" data-caption="Sales" style="display:none;"></div>
              <div data-type="tab" id="misctab" class="notcombinedtab tab" data-tabpageid="misctabpage" data-caption="Miscellaneous" style="display:none;"></div>
              <div data-type="tab" id="labortab" class="notcombinedtab tab" data-tabpageid="labortabpage" data-caption="Labor" style="display:none;"></div>
              <div data-type="tab" id="alltab" class="combinedtab tab" data-tabpageid="alltabpage" data-caption="Items" style="display:none;"></div>
              <div data-type="tab" id="usedsaletab" class="notcombinedtab tab" data-tabpageid="usedsaletabpage" data-caption="Used Sale" style="display:none;"></div>
              <div data-type="tab" id="billingtab" class="billingtab tab" data-tabpageid="billingtabpage" data-caption="Billing" style="display:none;"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts" style="display:none;"></div>
              <div data-type="tab" id="delivershiptab" class="tab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship" style="display:none;"></div>
              <div data-type="tab" id="notetab" class="tab notestab" data-tabpageid="notetabpage" data-caption="Notes"></div>
              <div data-type="tab" id="historytab" class="tab" data-tabpageid="historytabpage" data-caption="History" style="display:none;"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="formpage">
                  <!-- Quote / Status section-->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 700px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Equipment Request">
                        <div class="flexrow">
                          <div class="flexcolumn" style="flex:1 1 600px">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Request No." data-datafield="QuoteNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 250px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Version" data-datafield="VersionNumber" data-enabled="false" style="flex:0 1 50px;display:none;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;display:none;"></div>
                            </div>
                           <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dealnumber" data-caption="Deal No." data-datafield="DealNumber" data-enabled="false" style="flex:0 1 100px;display:none;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidate" data-required="false" style="flex:1 1 225px;display:none;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RateType" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 125px;display:none;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" data-required="true" style="flex:1 1 125px;display:none;"></div>
                            </div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 115px;display:none;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Pick Time" data-datafield="PickTime" style="flex:1 1 84px;display:none;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="Required Date" data-datafield="EstimatedStartDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="EstimatedStartTime" data-required="false" style="flex:1 1 84px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="Return Date" data-datafield="EstimatedStopDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Return Time" data-datafield="EstimatedStopTime" data-required="false" style="flex:1 1 84px;"></div>
                        </div>
                        <div class="flexrow" style="display:none;">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Rental" data-datafield="DisableEditingRentalRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Sales" data-datafield="DisableEditingSalesRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Miscellaneous" data-datafield="DisableEditingMiscellaneousRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Labor" data-datafield="DisableEditingLaborRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Used Sale" data-datafield="DisableEditingUsedSaleRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Loss and Damage" data-datafield="DisableEditingLossAndDamageRate" style="float:left;width:150px;"></div>
                        </div>
                        <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="Location"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 125px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 0 115px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As of" data-datafield="StatusDate" data-enabled="false" style="flex:1 0 115px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 0 115px;display:none;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="wideflexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                      </div>
                    </div>
                  </div>

                  <!-- Location section 
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 450px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="Location"></div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 125px;display:none;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Print">
                        <div class="print fwformcontrol" data-type="button" style="flex:1 1 50px;margin:15px 0 0 10px;">Print</div>
                      </div>
                    </div>
                  </div>
                  -->

                  <!-- Personnel -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 600px;display:none;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managed By" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                          <!--
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Outside Sales Representative" data-datafield="OutsideSalesRepresentativeId" data-displayfield="OutsideSalesRepresentative" data-enabled="true" data-validationname="ContactValidation" style="flex:1 1 185px;"></div>
                          -->
                        </div>
                      </div>
                    </div>
                    <!--Documents -->
                    <div class="flexcolumn" style="flex:1 1 500px;display:none;">
                      <div class="fwcontrol fwcontainer fwform-section itemsection" data-control="FwContainer" data-type="section" data-caption="Documents">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Cover Letter" data-datafield="CoverLetterId" data-displayfield="CoverLetter" data-enabled="true" data-validationname="CoverLetterValidation" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Terms &#038; Conditions" data-datafield="TermsConditionsId" data-displayfield="TermsConditions" data-enabled="true" data-validationname="TermsConditionsValidation" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!--Activity section-->
                  <div class="flexrow" style="display:none;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity" style="flex:1 1 770px">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Combine Activity" data-datafield="CombineActivity" style="display:none"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="Sales" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Miscellaneous" data-datafield="Miscellaneous" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="Labor" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Used Sale" data-datafield="RentalSale" style="flex:1 1 100px;"></div>

                        <!--<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="Facilities" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="Transportation" style="flex:1 1 150px;"></div>-->
                      </div>
                      <div class="flexrow" style="visibility:hidden;">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasSalesItem" data-datafield="HasSalesItem" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasMiscellaneousItem" data-datafield="HasMiscellaneousItem" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLaborItem" data-datafield="HasLaborItem" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasNotes" data-datafield="HasNotes" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>

                  <!-- Value/Cost // Weight // Office/Warehouse -->
                  <div class="flexrow">
                    <!-- Value / Cost -->
                    <div class="flexcolumn" style="flex:2 1 325px;display:none;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Value / Replacement Cost">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Total Value" data-datafield="" data-framedatafield="ValueTotal" data-formreadonly="true" data-enabled="false" style="flex: 1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Total Replacement" data-datafield="" data-framedatafield="ReplacementCostTotal" data-formreadonly="true" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Owned Value" data-datafield="" data-framedatafield="ValueOwned" data-formreadonly="true" style="flex:1 1 125px;display:none;"></div>
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Owned Replacement" data-datafield="" data-framedatafield="ReplacementCostOwned" data-formreadonly="true" style="flex:1 1 125px;display:none;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Sub Value" data-datafield="" data-framedatafield="ValueSubs" data-formreadonly="true" style="flex:1 1 125px;display:none;"></div>
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Sub Replacement" data-datafield="" data-framedatafield="ReplacementCostSubs" data-formreadonly="true" style="flex:1 1 125px;display:none;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- US Customary Weight -->
                    <div class="flexcolumn" style="flex:1 1 325px;display:none;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Weight">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Pounds" data-datafield="" data-framedatafield="WeightPounds" data-formreadonly="true" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Ounces" data-datafield="" data-framedatafield="WeightOunces" data-formreadonly="true" data-enabled="false" style="flex:1 1 70px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Pounds (In Case)" data-datafield="" data-framedatafield="WeightInCasePounds" data-formreadonly="true" style="flex:1 1 100px;display:none;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Ounces (In Case)" data-datafield="" data-framedatafield="WeightInCaseOunces" data-formreadonly="true" style="flex:1 1 70px;display:none;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Metric Weight -->
                    <div class="flexcolumn" style="flex:1 1 325px;display:none;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Weight">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Kilograms" data-datafield="" data-framedatafield="WeightKilograms" data-formreadonly="true" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Grams" data-datafield="" data-framedatafield="WeightGrams" data-formreadonly="true" style="flex:1 1 70px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Kikograms (In Case)" data-datafield="" data-framedatafield="WeightInCaseKilograms" data-formreadonly="true" style="flex:1 1 100px;display:none;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Grams (In Case)" data-datafield="" data-framedatafield="WeightInCaseGrams" data-formreadonly="true" style="flex:1 1 70px;display:none;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Office / Warehouse -->
                    <div class="flexcolumn" style="flex:2 1 325px;">
                      
                    </div>
                  </div>
                </div>
              </div>


              <!-- ITEMS TAB -->
              <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Spacer section
                  <div class="wideflexrow">
                    <div class="flexcolumn" style="flex:1 1 125px;">
                    </div>
                    <div class="flexcolumn rentalAdjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield totals RentalDaysPerWeek" data-caption="D/W" data-datafield="RentalDaysPerWeek" data-digits="3" data-digitsoptional="false" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield bottom_line_discount" data-caption="Disc. %" data-rectype="R" data-datafield="RentalDiscountPercent" data-digits="2" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsPeriod" data-caption="Total" data-rectype="R" data-datafield="PeriodRentalTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="R" data-datafield="PeriodRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsWeekly" data-caption="Total" data-rectype="R" data-datafield="WeeklyRentalTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="R" data-datafield="WeeklyRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsMonthly" data-caption="Total" data-rectype="R" data-datafield="MonthlyRentalTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="R" data-datafield="MonthlyRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn rentaltotals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="rental" data-datafield="" style="flex:1 1 250px;">
                            <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                            <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                            <div data-value="P" class="periodType" data-caption="Period"></div>
                          </div>
                        </div>
                      </div>
                    </div>  
                  </div>
                  -->
                </div>
              </div>

              <!-- SALES TAB -->
              <div data-type="tabpage" id="salestabpage" class="salesgrid notcombined tabpage" data-tabid="salestab">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                  <div class="flexrow" style="max-width:none;">
                    <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sales Items"></div>
                  </div>
                </div>
                <div class="flexrow" style="float:right;">
                  <div class="flexcolumn salesAdjustments" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="S" data-datafield="SalesDiscountPercent" style="flex:1 1 50px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals salesOrderItemTotal bottom_line_total_tax" data-caption="Total" data-rectype="S" data-datafield="SalesTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield salesTotalWithTax bottom_line_total_tax" data-caption="w/ Tax" data-rectype="S" data-datafield="SalesTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn salestotals" style="flex:2 1 550px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- BILLING TAB PAGE -->
              <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
                <!-- Billing Period -->
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Period">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_start_date" data-caption="Start" data-datafield="BillingStartDate" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_end_date" data-caption="Stop" data-datafield="BillingEndDate" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingWeeks week_or_month_field" data-caption="Weeks" data-datafield="BillingWeeks" data-enabled="true" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingMonths week_or_month_field" data-caption="Months" data-datafield="BillingMonths" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Delay Billing Search Until" data-datafield="DelayBillingSearchUntil" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lock Billing Dates" data-datafield="LockBillingDates" style="flex:1 1 150px;padding-left:25px;margin-top:10px;"></div>
                    </div>
                  </div>
                </div>
                <!-- Billing Cycle -->
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Cycle">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="BillingCycleValidation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" style="flex:1 1 250px;" data-required="true"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTermsValidation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTypeValidation" class="fwcontrol fwformfield" data-caption="Pay Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" style="flex:1 1 250px;"></div>
                    </div>
                  </div>
                </div>
                <!-- Bill Based On / Labor Fees / Contact Confirmation -->
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Determine Quantities to Bill Based on">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DetermineQuantitiesToBillBasedOn" style="flex:1 1 250px;">
                          <div data-value="CONTRACT" data-caption="Contract Activity"></div>
                          <div data-value="ORDER" data-caption="Order Quantity"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 25px;">
                    &#32;
                  </div>
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Prep Fees">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="IncludePrepFeesInRentalRate" style="flex:1 1 400px;">
                          <div data-value="false" data-caption="Add Prep Fees as Labor Charges"></div>
                          <div data-value="true" data-caption="Add Prep Fees into the Rental Item Rate"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 25px;">
                    &#32;
                  </div>
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Confirmation">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Require Contact Confirmation" data-datafield="RequireContactConfirmation" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- Tax Rates / Order Group / Contact Confirmation -->
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="TaxOptionValidation" class="fwcontrol fwformfield" data-caption="Tax Option" data-datafield="TaxOptionId" data-displayfield="TaxOption" style="flex:1 1 250px"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalTaxRate1" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesTaxRate1" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTaxRate1" style="flex:1 1 75px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 25px;">
                    &#32;
                  </div>
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Hiatus Schedule">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="HiatusDiscountFrom" style="flex:1 1 200px;">
                          <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;"></div>
                          <div data-value="ORDER" data-caption="This Order" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 25px;">
                    &#32;
                  </div>
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Group">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="In Group?" data-datafield="InGroup" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Group No" data-datafield="GroupNumber" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- Issue To / Bill To Address -->
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 15%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Address">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="PrintIssuedToAddressFrom" style="flex:1 1 150px;">
                          <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;"></div>
                          <div data-value="CUSTOMER" data-caption="Customer" style="flex:1 1 100px;"></div>
                          <div data-value="ORDER" data-caption="Order" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 42.5%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Issue To">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="IssuedToName" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="IssuedToAttention" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="IssuedToAttention2" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="IssuedToAddress1" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="IssuedToAddress2" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="IssuedToCity" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="IssuedToState" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="IssuedToZipCode" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="IssuedToCountryId" data-displayfield="IssuedToCountry" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 42.5%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bill To">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Different Than Issue To Address" data-datafield="BillToAddressDifferentFromIssuedToAddress" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Name" data-datafield="BillToName" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Attention" data-datafield="BillToAttention" data-enabled="false" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="" data-datafield="BillToAttention2" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Address" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="differentaddress fwcontrol fwformfield" data-caption="State/Province" data-datafield="BillToState" data-enabled="false" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="differentaddress fwcontrol fwformfield" data-caption="Country" data-datafield="BillToCountryId" data-displayfield="BillToCountry" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- Options -->
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 400px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="No Charge">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="No Charge" data-datafield="NoCharge" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Reason" data-datafield="NoChargeReason" style="flex:1 1 350px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 675px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Rental Items to go Out again after being Checked-In without increasing the Order quantity" data-datafield="RoundTripRentals" style="flex:1 1 650px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Discount Reason" data-datafield="DiscountReasonId" data-displayfield="DiscountReason" data-validationname="DiscountReasonValidation" style="flex:1 1 250px; float:left;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- DELIVER / SHIP TAB -->
              <div data-type="tabpage" id="delivershiptabpage" class="tabpage" data-tabid="delivershiptab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Outgoing -->
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield outtype delivery-delivery" data-caption="Type" data-datafield="OutDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="OutDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="OutDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="OutDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="OutDeliveryToContact" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="OutDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="OutDeliveryCarrierId" data-displayfield="OutDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="OutDeliveryShipViaId" data-displayfield="OutDeliveryShipVia" data-formbeforevalidate="beforeValidateOutShipVia"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:0 1 150px;">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield OutDeliveryAddressType delivery-type-radio" data-caption="Address" data-datafield="OutDeliveryAddressType">
                            <div data-value="DEAL" data-caption="Deal"></div>
                            <div data-value="OTHER" data-caption="Other"></div>
                            <div data-value="VENUE" data-caption="Venue"></div>
                            <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:1 1 350px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="OutDeliveryToLocation"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="OutDeliveryToAttention"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="OutDeliveryToAddress1"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="OutDeliveryToAddress2"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="OutDeliveryToCity"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="OutDeliveryToState"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="OutDeliveryToZipCode"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="OutDeliveryToCountryId" data-displayfield="OutDeliveryToCountry"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="OutDeliveryToCrossStreets"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="OutDeliveryDeliveryNotes"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="OutDeliveryOnlineOrderNumber"></div>
                            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield online" data-caption="Status" data-datafield="OutDeliveryOnlineOrderStatus"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <!-- Incoming -->
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield intype delivery-delivery" data-caption="Type" data-datafield="InDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="InDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="InDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="InDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="InDeliveryToContact" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="InDeliveryCarrierId" data-displayfield="InDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="InDeliveryShipViaId" data-displayfield="InDeliveryShipVia" data-formbeforevalidate="beforeValidateInShipVia"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:0 1 150px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield InDeliveryAddressType delivery-type-radio" data-caption="Address" data-datafield="InDeliveryAddressType">
                              <div data-value="DEAL" data-caption="Deal"></div>
                              <div data-value="OTHER" data-caption="Other"></div>
                              <div data-value="VENUE" data-caption="Venue"></div>
                              <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                            </div>
                          </div>
                          <div class="flexrow">
                            <div class="copy fwformcontrol" data-type="button" style="flex:0 1 50px;margin:15px 0 0 10px;text-align:center;">Copy</div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:1 1 350px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="InDeliveryToLocation"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="InDeliveryToAttention"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="InDeliveryToAddress1"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="InDeliveryToAddress2"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InDeliveryToCity"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="InDeliveryToState"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InDeliveryToZipCode"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="InDeliveryToCountryId" data-displayfield="InDeliveryToCountry"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="InDeliveryToCrossStreets"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="InDeliveryDeliveryNotes"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- CONTACTS TAB -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="OrderContactGrid" data-securitycaption="Contacts"></div>
                  </div>
                </div>
              </div>

              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notetabpage" class="tabpage" data-tabid="notetab">
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwGrid" data-grid="OrderNoteGrid" data-securitycaption="Notes"></div>
                    </div>
                  </div>
                </div>
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Office Location / Warehouse">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-enabled="false" style="flex:1 1 175px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-enabled="false" style="flex:1 1 175px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="WarehouseCode" data-datafield="WarehouseCode" data-formreadonly="true" data-enabled="false" style="display:none"></div>
                    </div>
                  </div>
                </div>
                </div>

              <!-- HISTORY TAB -->
              <div data-type="tabpage" id="historytabpage" class="tabpage" data-tabid="historytab">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Request Status History">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="OrderStatusHistoryGrid" data-securitycaption="Quote Status History"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
}

var QuoteController = new Quote();