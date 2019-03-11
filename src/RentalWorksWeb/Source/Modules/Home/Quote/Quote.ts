routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2] }; return QuoteController.getModuleScreen(filter); } });

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
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
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
        var $form;
        var self = this;

        $form = FwModule.loadFormFromTemplate(this.Module);
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

            $form.find('div[data-datafield="DealId"]').attr('data-required', false);
            $form.find('div[data-datafield="PickTime"]').attr('data-required', false);

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

        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);
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
            this.renderFrames($form, parentModuleInfo.OrderId);
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
        super.afterLoad($form);
        let $pending = $form.find('div.fwformfield[data-datafield="PendingPo"] input').prop('checked');
        let status = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes = FwFormField.getValueByDataField($form, 'HasNotes');
        let rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]'),
            salesTab = $form.find('[data-type="tab"][data-caption="Sales"]'),
            miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]'),
            laborTab = $form.find('[data-type="tab"][data-caption="Labor"]'),
            usedSaleTab = $form.find('[data-type="tab"][data-caption="Used Sale"]'),
            lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss and Damage"]')

        if ($form.find('[data-datafield="CombineActivity"] input').val() === 'false') {
            if ($form.find('[data-datafield="Rental"] input').prop('checked')) {
                FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                usedSaleTab.hide();
            } else {
                rentalTab.hide();
            }
            $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
            $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
            $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
            $form.find('[data-datafield="LossAndDamage"] input').prop('checked') ? lossDamageTab.show() : lossDamageTab.hide();
            if ($form.find('[data-datafield="RentalSale"] input').prop('checked')) {
                FwFormField.disable($form.find('[data-datafield="Rental"]'));
                rentalTab.show();
            } else {
                usedSaleTab.hide();
            }
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

//---------------------------------------------------------------------------------
//OrderItemGrid Bold Selected
FwApplicationTree.clickEvents['{E2DF5CB4-CD18-42A0-AE7C-18C18E6C4646}'] = function (event) {
    let $browse, $form;

    $browse = jQuery(this).closest('.fwbrowse');
    $form = jQuery(this).closest('.fwform');

    try {
        if ($form.attr('data-controller') === 'QuoteController') {
            QuoteController.orderItemGridBoldUnbold($browse, event);
        } else {
            OrderController.orderItemGridBoldUnbold($browse, event);
        }
        jQuery(document).trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//---------------------------------------------------------------------------------
//OrderItemGrid Lock Selected
FwApplicationTree.clickEvents['{BC467EF9-F255-4F51-A6F2-57276D8824A3}'] = function (event) {
    let $browse, $form;

    $browse = jQuery(this).closest('.fwbrowse');
    $form = jQuery(this).closest('.fwform');

    try {
        if ($form.attr('data-controller') === 'QuoteController') {
            QuoteController.orderItemGridLockUnlock($browse, event);
        } else {
            OrderController.orderItemGridLockUnlock($browse, event);
        }
        jQuery(document).trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//-----------------------------------------------------------------------------------------------------
var QuoteController = new Quote();