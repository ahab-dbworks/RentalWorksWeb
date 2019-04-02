//routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2] }; return QuoteController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class Quote extends OrderBase {
    Module: string = 'Quote';
    apiurl: string = 'api/v1/quote';
    caption: string = 'Quote';
    nav: string = 'module/quote';
    id: string = '4D785844-BE8A-4C00-B1FA-2AA5B05183E5';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

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
    };

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
        var location = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultOrderType = response.DefaultOrderType;
            self.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $prospect: JQuery = FwMenu.generateDropDownViewBtn('Prospect', true, "PROSPECT");
        const $active: JQuery = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $reserved: JQuery = FwMenu.generateDropDownViewBtn('Reserved', false, "RESERVED");
        const $ordered: JQuery = FwMenu.generateDropDownViewBtn('Ordered', false, "ORDERED");
        const $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

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
    };

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

        let $emailHistorySubModuleBrowse = this.openEmailHistoryBrowse($form);
        $form.find('.emailhistory-page').append($emailHistorySubModuleBrowse);

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
            this.dynamicColumns($form, parentModuleInfo.OrderTypeId);
        }

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };

    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };

    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        var $orderStatusHistoryGrid: any;
        var $orderStatusHistoryGridControl: any;
        let self = this;
        var totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]

        $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        $orderStatusHistoryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusHistoryGridBrowse').html());
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: $form.find('div.fwformfield[data-datafield="QuoteId"] input').val()
            };
        })
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);

        var $orderItemGridRental;
        var $orderItemGridRentalControl;
        $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridRentalControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
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
            this.calculateOrderItemGridTotals($form, 'rental', dt.Totals);

            let rentalItems = $form.find('.rentalgrid tbody').children();
            rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });
        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);

        var $orderItemGridSales;
        var $orderItemGridSalesControl;
        $orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridSalesControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridSalesControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSales.empty().append($orderItemGridSalesControl);
        $orderItemGridSales.addClass('S');
        $orderItemGridSalesControl.data('isSummary', false);

        $orderItemGridSalesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'S'
            };
            request.totalfields = totalFields;
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'sales', dt.Totals);
            let salesItems = $form.find('.salesgrid tbody').children();
            salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
        });
        FwBrowse.init($orderItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);

        var $orderItemGridLabor;
        var $orderItemGridLaborControl;
        $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridLaborControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridLabor.empty().append($orderItemGridLaborControl);
        $orderItemGridLabor.addClass('L');
        $orderItemGridLaborControl.data('isSummary', false);

        $orderItemGridLaborControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'L'
            };
            request.totalfields = totalFields;
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'labor', dt.Totals);
            let laborItems = $form.find('.laborgrid tbody').children();
            laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
        });
        FwBrowse.init($orderItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);

        var $orderItemGridMisc;
        var $orderItemGridMiscControl;
        $orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        $orderItemGridMiscControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
        $orderItemGridMisc.empty().append($orderItemGridMiscControl);
        $orderItemGridMisc.addClass('M');
        $orderItemGridMiscControl.data('isSummary', false);

        $orderItemGridMiscControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
                RecType: 'M'
            };
            request.totalfields = totalFields;
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'M';
        });
        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'misc', dt.Totals);
            let miscItems = $form.find('.miscgrid tbody').children();
            miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
        });
        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);

        var $orderItemGridUsedSale;
        var $orderItemGridUsedSaleControl;
        $orderItemGridUsedSale = $form.find('.usedsalegrid div[data-grid="OrderItemGrid"]');
        $orderItemGridUsedSaleControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
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

        var $allOrderItemGrid;
        var $allOrderItemGridControl;
        $allOrderItemGrid = $form.find('.combinedgrid div[data-grid="OrderItemGrid"]');
        $allOrderItemGridControl = jQuery(jQuery('#tmpl-grids-OrderItemGridBrowse').html());
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

        var $orderNoteGrid;
        var $orderNoteGridControl;
        $orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        $orderNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderNoteGridBrowse').html());
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

        var $orderContactGrid;
        var $orderContactGridControl;
        $orderContactGrid = $form.find('div[data-grid="OrderContactGrid"]');
        $orderContactGridControl = jQuery(jQuery('#tmpl-grids-OrderContactGridBrowse').html());
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

        let itemGrids = [$orderItemGridRental, $orderItemGridSales, $orderItemGridLabor, $orderItemGridMisc];
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
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form, false);
        let $pending = $form.find('div.fwformfield[data-datafield="PendingPo"] input').prop('checked');
        let status = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes = FwFormField.getValueByDataField($form, 'HasNotes');
        let rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]'),
            salesTab = $form.find('[data-type="tab"][data-caption="Sales"]'),
            miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]'),
            laborTab = $form.find('[data-type="tab"][data-caption="Labor"]'),
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
        //FwBrowse.search($orderStatusHistoryGrid);

        var $orderItemGridRental;
        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridRental);
        var $orderItemGridSales;
        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridSales);
        var $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridLabor);
        var $orderItemGridMisc;
        $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridLabor);
        var $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        //FwBrowse.search($orderNoteGrid);
        var $orderContactGrid;
        $orderContactGrid = $form.find('[data-name="OrderContactGrid"]');
        //FwBrowse.search($orderContactGrid);
        var $allOrderItemGrid;
        $allOrderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($allOrderItemGrid);
        var $orderItemGridUsedSale;
        $orderItemGridUsedSale = $form.find('.usedsalegrid [data-name="OrderItemGrid"]');
        //FwBrowse.search($orderItemGridUsedSale);

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
        this.disableWithTaxCheckbox($form);
    };
    //----------------------------------------------------------------------------------------------
    openEmailHistoryBrowse($form) {
        var $browse;

        $browse = EmailHistoryController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.uniqueids = {
                RelatedToId: $form.find('[data-datafield="QuoteId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Quote" data-control="FwBrowse" data-type="Browse" id="QuoteBrowse" class="fwcontrol fwbrowse" data-orderby="QuoteId" data-sort="desc" data-controller="QuoteController">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="QuoteId" data-datatype="key" data-sort="off"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="OrderTypeId" data-browsedatatype="key"></div>
          </div>
          <!--<div class="column" data-width="0" data-visible="false">
            <div class="field" data-datafield="Inactive" data-datatype="text"  data-visible="false"></div>
          </div>-->
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Quote No" data-datafield="QuoteNumber" data-cellcolor="QuoteNumberColor" data-datatype="text" data-sort="off" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Date" data-datafield="QuoteDate" data-browsedatatype="date" data-sort="desc"></div>
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
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="Total" data-datafield="Total" data-datatype="number" data-cellcolor="CurrencyColor" data-formatnumeric="true" data-digits="2" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="PO No." data-datafield="PoNumber" data-cellcolor="PoNumberColor" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="180px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="150px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-cellcolor="WarehouseColor" data-datatype="text" data-sort="off"></div>
          </div>
        </div>
        `;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="quoteform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Quote" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="QuoteController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="QuoteId"></div>
          <div id="quoteform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="generaltab" class="generaltab tab" data-tabpageid="generaltabpage" data-caption="Quote"></div>
              <div data-type="tab" id="rentaltab" class="notcombinedtab tab" data-tabpageid="rentaltabpage" data-caption="Rental"></div>
              <div data-type="tab" id="salestab" class="notcombinedtab tab" data-tabpageid="salestabpage" data-caption="Sales"></div>
              <div data-type="tab" id="labortab" class="notcombinedtab tab" data-tabpageid="labortabpage" data-caption="Labor"></div>
              <div data-type="tab" id="misctab" class="notcombinedtab tab" data-tabpageid="misctabpage" data-caption="Miscellaneous"></div>
              <div data-type="tab" id="usedsaletab" class="notcombinedtab tab" data-tabpageid="usedsaletabpage" data-caption="Used Sale"></div>
              <div data-type="tab" id="alltab" class="combinedtab tab" data-tabpageid="alltabpage" data-caption="Items"></div>
              <div data-type="tab" id="billingtab" class="billingtab tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="delivershiptab" class="tab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
              <div data-type="tab" id="notetab" class="tab notestab" data-tabpageid="notetabpage" data-caption="Notes"></div>
              <div data-type="tab" id="historytab" class="tab" data-tabpageid="historytabpage" data-caption="History"></div>
              <div data-type="tab" id="emailhistorytab" class="tab" data-tabpageid="emailhistorytabpage" data-caption="Email History"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="formpage">
                  <!-- Quote / Status section-->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 700px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote">
                        <div class="flexrow">
                          <div class="flexcolumn" style="flex:1 1 600px">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quote No." data-datafield="QuoteNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 250px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Version" data-datafield="VersionNumber" data-enabled="false" style="flex:0 1 50px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;"></div>
                            </div>
                           <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dealnumber" data-caption="Deal No." data-datafield="DealNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidate" data-required="false" style="flex:1 1 225px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RateType" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 125px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" data-required="true" style="flex:1 1 125px;"></div>
                            </div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Pick Time" data-datafield="PickTime" style="flex:1 1 84px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="From Date" data-datafield="EstimatedStartDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="From Time" data-datafield="EstimatedStartTime" data-required="false" style="flex:1 1 84px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="To Date" data-datafield="EstimatedStopDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="To Time" data-datafield="EstimatedStopTime" data-required="false" style="flex:1 1 84px;"></div>
                        </div>
                        <div class="flexrow" style="display:none;">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Rental" data-datafield="DisableEditingRentalRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Sales" data-datafield="DisableEditingSalesRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Miscellaneous" data-datafield="DisableEditingMiscellaneousRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Labor" data-datafield="DisableEditingLaborRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Used Sale" data-datafield="DisableEditingUsedSaleRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Loss and Damage" data-datafield="DisableEditingLossAndDamageRate" style="float:left;width:150px;"></div>
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
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 0 115px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!-- Location / PO section -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 325px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="Location"></div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending" data-datafield="PendingPo" style="flex:1 1 90px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Flat PO" data-datafield="FlatPo" style="flex:1 1 90px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PoNumber" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="PO Amount" data-datafield="PoAmount" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 125px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Print">
                        <div class="print fwformcontrol" data-type="button" style="flex:1 1 50px;margin:15px 0 0 10px;">Print</div>
                      </div>
                    </div>
                  </div>

                  <!-- Summary Details -->
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section summarySection" data-control="FwContainer" data-type="section" data-caption="Summary" style="flex:0 1 65%;">
                      <div class="flexrow totalRowFrames">
                        <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Charge</div>
                        <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Discount</div>
                        <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Total</div>
                        <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Cost</div>
                        <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Gross Profit</div>
                        <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Markup %</div>
                        <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Margin %</div>
                      </div>
                      <div class="flexrow totalRowFrames" style="margin-top:-10px;">
                        <div class="flexcolumn" style="width:7%;">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalPrice"></div>
                        </div>
                        <div class="flexcolumn" style="width:7%;">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalDiscount"></div>
                        </div>
                        <div class="flexcolumn totalColors" style="width:7%;">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalTotal"></div>
                        </div>
                        <div class="flexcolumn" style="width:7%;">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalCost"></div>
                        </div>
                        <div class="flexcolumn profitframes" style="width:7%;">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalProfit"></div>
                        </div>
                        <div class="flexcolumn" style="width:7%;">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalMarkup"></div>
                        </div>
                        <div class="flexcolumn" style="width:7%;">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalMargin"></div>
                        </div>
                      </div>
                      <div class="allFrames">
                        <div class="flexrow" style="float:right;">
                          <div class="summaryweekly fwformcontrol" data-type="button" style="display:none; max-width: 56px;margin:5px 0 15px 10px;">Weekly</div>
                          <div class="summarymonthly fwformcontrol" data-type="button" style="display:none; max-width:72px; margin:5px 0 15px 10px;">Monthly</div>
                          <div class="summaryperiod fwformcontrol" data-type="button" style="max-width: 57px;margin:5px 0 15px 10px;">Period</div>
                        </div>
                        <div class="flexrow" style="clear:right;">
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">&#160;</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Rental</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Sales</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Facilities</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Transportation</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Crew</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Miscellaneous</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Used Sale</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Parts</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Sales Tax</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Total</div>
                        </div>
                        <div class="flexrow" style="flex:1 1 auto;">
                          <div class="flexcolumn" style="flex:1 1 7%;margin-top:25px;margin-left:15px;font-size:.85em;">
                            <div>Charge</div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalPrice"></div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesPrice"></div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesPrice"></div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationPrice"></div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborPrice"></div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscPrice"></div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSalePrice"></div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsPrice"></div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div>&#160;</div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalPrice"></div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div class="flexcolumn" style="width:7%;margin-top:25px;margin-left:15px;font-size:.85em;">
                            <div>Discount</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalDiscount"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesDiscount"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesDiscount"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationDiscount"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborDiscount"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscDiscount"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleDiscount"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsDiscount"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div>&#160;</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalDiscount"></div>
                          </div>
                        </div>
                        <div class="flexrow totalColors">
                          <div class="flexcolumn" style="width:7%;margin-top:25px;margin-left:15px;font-size:.85em;">
                            <div>Total</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalTotal"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesTotal"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesTotal"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationTotal"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborTotal"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscTotal"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleTotal"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsTotal"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalTax"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalTotal"></div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div class="flexcolumn" style="width:7%;margin-top:25px;margin-left:15px;font-size:.85em;">
                            <div>Cost</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TaxCost"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalCost"></div>
                          </div>
                        </div>
                        <div class="flexrow profitframes">
                          <div class="flexcolumn" style="width:7%;margin-top:25px;margin-left:15px;font-size:.85em;">
                            <div>Gross Profit</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalProfit"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesProfit"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesProfit"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationProfit"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborProfit"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscProfit"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleProfit"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsProfit"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div>&#160;</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalProfit"></div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div class="flexcolumn" style="width:7%;margin-top:25px;margin-left:15px;font-size:.85em;">
                            <div>Markup %</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalMarkup"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesMarkup"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesMarkup"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationMarkup"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborMarkup"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscMarkup"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleMarkup"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsMarkup"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div>&#160;</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalMarkup"></div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div class="flexcolumn" style="width:7%;margin-top:25px;margin-left:15px;font-size:.85em;">
                            <div>Margin %</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalMargin"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesMargin"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesMargin"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationMargin"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborMargin"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscMargin"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleMargin"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsMargin"></div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div>&#160;</div>
                          </div>
                          <div class="flexcolumn" style="width:7%;">
                            <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalMargin"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn totalRowFrames">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Value / Replacement Cost" style="flex:0 1 30%;">
                        <div class="flexrow">
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Total Value</div>
                          <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Total Replacement</div>
                        </div>
                        <div class="flexrow" style="margin-top:-10px;">
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="ValueTotal" data-formreadonly="true" style="flex: 1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="ReplacementCostTotal" data-formreadonly="true" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div style="flex:0 1 2%; margin-top:10px; margin-left: -5px;">
                      <i class="material-icons expandArrow expandFrames" style="cursor:pointer; background-color:#37474F; color:white">keyboard_arrow_right</i>
                      <i class="material-icons expandArrow hideFrames" style="cursor:pointer; background-color:#37474F; color:white">keyboard_arrow_down</i>
                    </div>
                  </div>
                  <!-- Value/Cost // Weight // Office/Warehouse -->
                  <div class="flexrow allFrames">
                    <!-- Value / Cost -->
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Value / Replacement Cost">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Total Value" data-datafield="" data-framedatafield="ValueTotal" data-formreadonly="true" style="flex: 1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Total Replacement" data-datafield="" data-framedatafield="ReplacementCostTotal" data-formreadonly="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Owned Value" data-datafield="" data-framedatafield="ValueOwned" data-formreadonly="true" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Owned Replacement" data-datafield="" data-framedatafield="ReplacementCostOwned" data-formreadonly="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Sub Value" data-datafield="" data-framedatafield="ValueSubs" data-formreadonly="true" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Sub Replacement" data-datafield="" data-framedatafield="ReplacementCostSubs" data-formreadonly="true" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- US Customary Weight -->
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="U.S. Customary Weight">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Pounds" data-datafield="" data-framedatafield="WeightPounds" data-formreadonly="true" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Ounces" data-datafield="" data-framedatafield="WeightOunces" data-formreadonly="true" style="flex:1 1 70px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Pounds (In Case)" data-datafield="" data-framedatafield="WeightInCasePounds" data-formreadonly="true" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Ounces (In Case)" data-datafield="" data-framedatafield="WeightInCaseOunces" data-formreadonly="true" style="flex:1 1 70px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Metric Weight -->
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Metric Weight">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Kilograms" data-datafield="" data-framedatafield="WeightKilograms" data-formreadonly="true" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Grams" data-datafield="" data-framedatafield="WeightGrams" data-formreadonly="true" style="flex:1 1 70px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Kikograms (In Case)" data-datafield="" data-framedatafield="WeightInCaseKilograms" data-formreadonly="true" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Grams (In Case)" data-datafield="" data-framedatafield="WeightInCaseGrams" data-formreadonly="true" style="flex:1 1 70px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!--Activity section-->
                  <div class="flexrow">
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
                      <div class="flexrow" style="display:none;">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasSalesItem" data-datafield="HasSalesItem" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasMiscellaneousItem" data-datafield="HasMiscellaneousItem" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLaborItem" data-datafield="HasLaborItem" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasNotes" data-datafield="HasNotes" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>

                  <!-- Personnel -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 600px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Outside Sales Representative" data-datafield="OutsideSalesRepresentativeId" data-displayfield="OutsideSalesRepresentative" data-enabled="true" data-validationname="ContactValidation" style="flex:1 1 185px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!-- Market Segment Section -->
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Market Segment" style="flex:1 1 770px">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Market Type" data-datafield="MarketTypeId" data-displayfield="MarketType" data-validationpeek="true" data-validationname="MarketTypeValidation" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Market Segment" data-datafield="MarketSegmentId" data-displayfield="MarketSegment" data-validationpeek="true" data-formbeforevalidate="beforeValidateMarketSegment" data-validationname="MarketSegmentValidation" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Job" data-datafield="MarketSegmentJobId" data-displayfield="MarketSegmentJob" data-validationpeek="true" data-formbeforevalidate="beforeValidateMarketSegment" data-validationname="MarketSegmentJobValidation" style="flex:1 1 150px;"></div>
                      </div>
                    </div>
                  </div>

                  <!-- Documents | Office / Warehouse -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 500px;">
                      <div class="fwcontrol fwcontainer fwform-section itemsection" data-control="FwContainer" data-type="section" data-caption="Documents">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Cover Letter" data-datafield="CoverLetterId" data-displayfield="CoverLetter" data-enabled="true" data-validationname="CoverLetterValidation" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Terms &#038; Conditions" data-datafield="TermsConditionsId" data-displayfield="TermsConditions" data-enabled="true" data-validationname="TermsConditionsValidation" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Office Location / Warehouse">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="WarehouseCode" data-datafield="WarehouseCode" data-formreadonly="true" data-enabled="false" style="display:none"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- RENTAL TAB -->
              <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Spacer section -->
                  <div class="flexrow" style="float:right;">
                    <!-- Adjustments section -->
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
                    <!-- Totals section -->
                    <div class="flexcolumn rentaltotals" style="flex:2 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 85px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="rental" data-datafield="" style="flex:0 1 100px;">
                            <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                            <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                            <div data-value="P" class="periodType" data-caption="Period"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
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

              <!-- LABOR TAB -->
              <div data-type="tabpage" id="labortabpage" class="laborgrid notcombined tabpage" data-tabid="labortab">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Items">
                  <div class="flexrow" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Labor Items"></div>
                  </div>
                </div>
                <div class="flexrow" style="float:right;">
                  <div class="flexcolumn laborAdjustments" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="L" data-datafield="LaborDiscountPercent" style="flex:1 1 50px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsPeriod" data-caption="Total" data-rectype="L" data-datafield="PeriodLaborTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="L" data-datafield="PeriodLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsWeekly" data-caption="Total" data-rectype="L" data-datafield="WeeklyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="L" data-datafield="WeeklyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsMonthly" data-caption="Total" data-rectype="L" data-datafield="MonthlyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="L" data-datafield="MonthlyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn labortotals" style="flex:2 1 550px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="labor" data-datafield="" style="flex:0 1 100px;">
                          <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                          <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                          <div data-value="P" class="periodType" data-caption="Period"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- MISC TAB -->
              <div data-type="tabpage" id="misctabpage" class="miscgrid notcombined tabpage" data-tabid="misctab">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Items">
                  <div class="flexrow" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Misc Items"></div>
                  </div>
                </div>
                <div class="flexrow" style="float:right;">
                  <div class="flexcolumn miscAdjustments" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="M" data-datafield="MiscDiscountPercent" style="flex:1 1 50px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsPeriod" data-caption="Total" data-rectype="M" data-datafield="PeriodMiscTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="M" data-datafield="PeriodMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsWeekly" data-caption="Total" data-rectype="M" data-datafield="WeeklyMiscTotal" style="flex:1 1 100px; display:none;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="M" data-datafield="WeeklyMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsMonthly" data-caption="Total" data-rectype="M" data-datafield="MonthlyMiscTotal" style="flex:1 1 100px; display:none;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="M" data-datafield="MonthlyMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn misctotals" style="flex:2 1 550px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="misc" data-datafield="" style="flex:0 1 100px;">
                          <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                          <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                          <div data-value="P" class="periodType" data-caption="Period"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- USED SALE TAB -->
              <div data-type="tabpage" id="usedsaletabpage" class="usedsalegrid notcombined tabpage" data-tabid="usedsaletab" data-render="false">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sale Items">
                  <div class="wide-flexrow">
                    <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Used Sale Items"></div>
                  </div>
                </div>
              </div>

              <!-- ALL TAB -->
              <div data-type="tabpage" id="alltabpage" class="combined combined tabpage" data-tabid="alltab">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Items">
                  <div class="fwcontrol fwcontainer fwform-fieldrow combinedgrid" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Quote Items"></div>
                  </div>
                </div>
                <div class="flexrow" style="float:right;">
                  <div class="flexcolumn combinedAdjustments" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield combineddw" data-caption="D/W" data-datafield="CombinedDaysPerWeek" data-digits="3" data-digitsoptional="false" style="flex:1 1 50px;display:none;" data-enabled="true"></div>
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="" data-datafield="CombinedDiscountPercent" style="flex:1 1 50px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsPeriod" data-caption="Total" data-rectype="" data-datafield="PeriodCombinedTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="" data-datafield="PeriodCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsWeekly" data-caption="Total" data-rectype="" data-datafield="WeeklyCombinedTotal" style="flex:1 1 100px; display:none;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="" data-datafield="WeeklyCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsMonthly" data-caption="Total" data-rectype="" data-datafield="MonthlyCombinedTotal" style="flex:1 1 100px; display:none;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="" data-datafield="MonthlyCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn combinedtotals" style="flex:2 1 550px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="combined" data-datafield="" style="flex:0 1 100px;">
                          <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                          <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                          <div data-value="P" class="periodType" data-caption="Period"></div>
                        </div>
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
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="OrderNoteGrid" data-securitycaption="Notes"></div>
                  </div>
                </div>
              </div>
              <!-- HISTORY TAB -->
              <div data-type="tabpage" id="historytabpage" class="tabpage" data-tabid="historytab">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Status History">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="OrderStatusHistoryGrid" data-securitycaption="Quote Status History"></div>
                  </div>
                </div>
              </div>
             <div data-type="tabpage" id="emailhistorytabpage" class="tabpage submodule emailhistory-page" data-tabid="emailhistorytab"></div>
            </div>
          </div>
        </div>
        `;                      
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        super.events($form);

        // Market Type Change
        $form.find('[data-datafield="MarketTypeId"] input').on('change', event => {
            FwFormField.setValueByDataField($form, 'MarketSegmentId', '');
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
        $form.find('[data-datafield="MarketSegmentId"] input').on('change', event => {
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
    };
    //----------------------------------------------------------------------------------------------
    createNewVersionQuote($form, event) {
        let quoteNumber, quoteId;
        quoteNumber = FwFormField.getValueByDataField($form, 'QuoteNumber');
        quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        var $confirmation, $yes, $no;

        $confirmation = FwConfirmation.renderConfirmation('Create New Version', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        var html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Create New Version for Quote ${quoteNumber}?</div>`);
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        $yes = FwConfirmation.addButton($confirmation, 'Create New Version', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', createNewVersion);
        var $confirmationbox = jQuery('.fwconfirmationbox');
        function createNewVersion() {
            FwAppData.apiMethod(true, 'POST', `api/v1/quote/createnewversion/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'New Version Successfully Created.');
                FwConfirmation.destroyConfirmation($confirmation);
                let uniqueids: any = {};
                uniqueids.QuoteId = response.QuoteId;
                var $quoteform = QuoteController.loadForm(uniqueids);
                FwModule.openModuleTab($quoteform, "", true, 'FORM', true);

                FwModule.refreshForm($form, QuoteController);
            }, null, $confirmationbox);
        }
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form) {
        if (this.CombineActivity === 'true') {
            $form.find('.combined').css('display', 'block');
            $form.find('.combinedtab').css('display', 'flex');
            $form.find('.generaltab').click();
        } else {
            $form.find('.notcombined').css('display', 'block');
            $form.find('.notcombinedtab').css('display', 'flex');
            $form.find('.generaltab').click();
        }
        this.renderGrids($form);
        this.renderFrames($form, FwFormField.getValueByDataField($form, 'OrderId'));
        this.dynamicColumns($form, FwFormField.getValueByDataField($form, 'OrderTypeId'));
    };
   
};

//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{B918C711-32D7-4470-A8E5-B88AB5712863}'] = function (event) {
    var $form
    $form = jQuery(this).closest('.fwform');
    try {
        QuoteController.copyOrderOrQuote($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//-----------------------------------------------------------------------------------------------------
//Open Search Interface
FwApplicationTree.clickEvents['{BC3B1A5E-7270-4547-8FD1-4D14F505D452}'] = function (event) {
    let search, $form, quoteId;
    $form = jQuery(this).closest('.fwform');
    quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

    if ($form.attr('data-mode') === 'NEW') {
        QuoteController.saveForm($form, { closetab: false });
        return;
    }

    if (quoteId == "") {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    } else {
        search = new SearchInterface();
        search.renderSearchPopup($form, quoteId, 'Quote');
    }
};
//-----------------------------------------------------------------------------------------------------
//Print Quote
FwApplicationTree.clickEvents['{B20DDE47-A5D7-49A9-B980-8860CADBF7F6}'] = function (e) {
    try {
        var $form = jQuery(this).closest('.fwform');
        $form.find('.print').trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{F79F8C21-66DF-4458-BBEB-E19B2BFCAEAA}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');

    try {
        QuoteController.createNewVersionQuote($form, event);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Form Cancel Option
FwApplicationTree.clickEvents['{BF633873-8A40-4BD6-8ED8-3EAC27059C84}'] = function (event) {
    let $form
    $form = jQuery(this).closest('.fwform');

    try {
        QuoteController.cancelUncancelOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{E265DFD0-380F-4E8C-BCFD-FA5DCBA4A654}'] = function (event) {
    let $form, quoteNumber;
    $form = jQuery(this).closest('.fwform');
    quoteNumber = FwFormField.getValueByDataField($form, 'QuoteNumber');
    var $confirmation, $yes, $no;

    $confirmation = FwConfirmation.renderConfirmation('Create Order', '');
    $confirmation.find('.fwconfirmationbox').css('width', '450px');
    var html = [];
    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
    html.push('    <div>Create Order for Quote ' + quoteNumber + '?</div>');
    html.push('  </div>');
    html.push('</div>');

    FwConfirmation.addControls($confirmation, html.join(''));

    $yes = FwConfirmation.addButton($confirmation, 'Create Order', false);
    $no = FwConfirmation.addButton($confirmation, 'Cancel');

    $yes.on('click', createOrder);
    var $confirmationbox = jQuery('.fwconfirmationbox');
    function createOrder() {
        var quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        FwAppData.apiMethod(true, 'POST', "api/v1/quote/createorder/" + quoteId, null, FwServices.defaultTimeout, function onSuccess(response) {
            FwNotification.renderNotification('SUCCESS', 'Order Successfully Created.');
            FwConfirmation.destroyConfirmation($confirmation);
            let uniqueids: any = {};
            uniqueids.OrderId = response.OrderId;
            var $orderform = OrderController.loadForm(uniqueids);
            FwModule.openModuleTab($orderform, "", true, 'FORM', true);

            FwModule.refreshForm($form, QuoteController);
        }, null, $confirmationbox);
    }
};
//---------------------------------------------------------------------------------
//Browse Cancel Option
FwApplicationTree.clickEvents['{78ACB73C-23DD-46F0-B179-0571BAD3A17D}'] = function (event) {
    let $confirmation, $yes, $no, $browse, quoteId, quoteStatus;

    $browse = jQuery(this).closest('.fwbrowse');
    quoteId = $browse.find('.selected [data-browsedatafield="QuoteId"]').attr('data-originalvalue');
    quoteStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');

    try {
        if (quoteId != null) {
            if (quoteStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to un-cancel this Quote?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Quote', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', uncancelQuote);
            }
            else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to cancel this Quote?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Cancel Quote', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', cancelQuote);
            }

            function cancelQuote() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Canceling...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Quote Successfully Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', cancelQuote);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };

            function uncancelQuote() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Retrieving...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/quote/uncancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Quote Successfully Retrieved');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', uncancelQuote);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };
        } else {
            FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//-----------------------------------------------------------------------------------------------------
var QuoteController = new Quote();