routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2] }; return QuoteController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class Quote {
    Module: string = 'Quote';
    apiurl: string = 'api/v1/quote';
    ActiveView: string = 'ALL';
    DefaultOrderType: string;
    DefaultOrderTypeId: string;
    CombineActivity: string;

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
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);

        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });

        FwBrowse.addLegend($browse, 'Locked', '#ff704d');
        FwBrowse.addLegend($browse, 'On Hold', '#EA300F');
        FwBrowse.addLegend($browse, 'Reserved', '#1E90FF');
        FwBrowse.addLegend($browse, 'No Charge', '#ff8040');
        FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
        FwBrowse.addLegend($browse, 'Multi-Warehouse', '#D6E180');
        FwBrowse.addLegend($browse, 'Quote Request', '#00FF00');

        var department = JSON.parse(sessionStorage.getItem('department'));;
        var location = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultOrderType = response.DefaultOrderType;
            self.DefaultOrderTypeId = response.DefaultOrderTypeId;

        }, null, null);



        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        var self = this;
        var $all: JQuery = FwMenu.generateDropDownViewBtn('All', true);
        var $prospect: JQuery = FwMenu.generateDropDownViewBtn('Prospect', true);
        var $active: JQuery = FwMenu.generateDropDownViewBtn('Active', false);
        var $reserved: JQuery = FwMenu.generateDropDownViewBtn('Reserved', false);
        var $ordered: JQuery = FwMenu.generateDropDownViewBtn('Ordered', false);
        var $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false);
        var $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false);

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $prospect.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'PROSPECT';
            FwBrowse.search($browse);
        });
        $active.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACTIVE';
            FwBrowse.search($browse);
        });
        $reserved.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'RESERVED';
            FwBrowse.search($browse);
        });
        $ordered.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ORDERED';
            FwBrowse.search($browse);
        });
        $cancelled.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CANCELLED';
            FwBrowse.search($browse);
        });
        $closed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CLOSED';
            FwBrowse.search($browse);
        });

        FwMenu.addVerticleSeparator($menuObject);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($prospect);
        viewSubitems.push($active);
        viewSubitems.push($reserved);
        viewSubitems.push($ordered);
        viewSubitems.push($cancelled);
        viewSubitems.push($closed);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        //Location Filter
        var location = JSON.parse(sessionStorage.getItem('location'));
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        var $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        var viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($allLocations);
        var $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        var $form;
        var self = this;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true')

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
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));

            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);

            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();
        }

        //RateType change affecting billing tab weeks or months
        $form.find('.RateType').on('change', $tr => {
            if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
                $form.find(".BillingWeeks").hide();
                $form.find(".BillingMonths").show();
            } else {
                $form.find(".BillingMonths").hide();
                $form.find(".BillingWeeks").show();
            }
        });

        //RateType change affecting DaysPerWeek field in rental tab
        $form.find('.RateType').on('change', $tr => {
            if (FwFormField.getValueByDataField($form, 'RateType') === 'DAILY') {
                $form.find(".RentalDaysPerWeek").show();
            } else {
                $form.find(".RentalDaysPerWeek").hide();
            }
        });

        $form.find('[data-datafield="PendingPo"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
                FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
            }
            else {
                FwFormField.enable($form.find('[data-datafield="PoNumber"]'));
                FwFormField.enable($form.find('[data-datafield="PoAmount"]'));
            }
        }); 

        $form.find('[data-datafield="BillToAddressDifferentFromIssuedToAddress"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.differentaddress'));
            }
            else {
                FwFormField.disable($form.find('.differentaddress'));
            }
        });

        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

        $form.find('div[data-datafield="DealId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', $tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));
        });

        $form.find('[data-datafield="NoCharge"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
            }
        });

        $form.find('div[data-datafield="OrderTypeId"]').data('onchange', function ($tr) {
            self.CombineActivity = $tr.find('.field[data-browsedatafield="CombineActivityTabs"]').attr('data-originalvalue');
        });

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="DealId"]').data('onchange', function ($tr) {
            var type = $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'RateType', type);
            $form.find('div[data-datafield="RateType"] input.fwformfield-text').val(type);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', $tr.find('.field[data-browsedatafield="PaymentTermsId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentTerms"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTypeId"]', $tr.find('.field[data-browsedatafield="PaymentTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Currency"]').attr('data-originalvalue'));
        });

        FwFormField.loadItems($form.find('.outtype'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);


        FwFormField.loadItems($form.find('.intype'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
        }

        $form.find('.print').on('click', e => {
            var $report, quoteNumber, quoteId;
            try {
                quoteNumber = $form.find('div.fwformfield[data-datafield="QuoteNumber"] input').val();
                quoteId = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
                $report = RwPrintOrderController.openForm();
                FwModule.openSubModuleTab($form, $report);
                $report.find('.fwform-section[data-caption="Order"]').css('display', 'none');
                $report.find('div.fwformfield[data-datafield="QuoteId"] input').val(quoteId);
                $report.find('div.fwformfield[data-datafield="QuoteId"] .fwformfield-text').val(quoteNumber);

                jQuery('.tab.submodule.active').find('.caption').html('Print Quote');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateOutShipVia($browse, $grid, request) {
        var validationName = request.module;
        var outDeliveryCarrierId = jQuery($grid.find('[data-datafield="OutDeliveryCarrierId"] input')).val();

        switch (validationName) {
            case 'ShipViaValidation':
                request.uniqueids = {
                    VendorId: outDeliveryCarrierId
                };
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateInShipVia($browse, $grid, request) {
        var validationName = request.module;
        var inDeliveryCarrierId = jQuery($grid.find('[data-datafield="InDeliveryCarrierId"] input')).val();

        switch (validationName) {
            case 'ShipViaValidation':
                request.uniqueids = {
                    VendorId: inDeliveryCarrierId
                };
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateCarrier($browse, $grid, request) {
        var validationName = request.module;

        switch (validationName) {
            case 'VendorValidation':
                request.uniqueids = {
                    Freight: true
                };
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    renderFrames($form: any) {
        let quoteId;

        $form.find('.frame input').css('width', '100%');

        quoteId = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();

        FwAppData.apiMethod(true, 'GET', `api/v1/ordersummary/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            var key;
            for (key in response) {
                if (response.hasOwnProperty(key)) {
                    $form.find('[data-framedatafield="' + key + '"] input').val(response[key]);
                    $form.find('[data-framedatafield="' + key + '"]').attr('data-originalvalue', response[key]);
                }
            }

            var $profitFrames = $form.find('.profitFrames .frame');
            $profitFrames.each(function () {
                var profit = parseFloat(jQuery(this).attr('data-originalvalue'));

                if (profit > 0) {
                    jQuery(this).find('input').css('background-color', '#A6D785');
                } else if (profit < 0) {
                    jQuery(this).find('input').css('background-color', '#ff9999');
                }

            });

            var $totalFrames = $form.find('.totalColors input');
            $totalFrames.each(function () {
                var total = jQuery(this).val();
                if (total != 0) {
                    jQuery(this).css('background-color', '#ffffe5');
                }
            })


        }, null, null);

        FwFormField.disable($form.find('.frame'));

        $form.find(".frame .add-on").children().hide();
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        var $orderStatusHistoryGrid: any;
        var $orderStatusHistoryGridControl: any;
        var max = 9999;

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
            request.pagesize = max;
        });
        $orderItemGridRentalControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'R';
        });
        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'rental');

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
            request.pagesize = max;
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'sales');
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
            request.pagesize = max;
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'labor');
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
            request.pagesize = max;
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'M';
        });
        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'misc');
        });
        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);

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
            request.pagesize = max;
        });
        $allOrderItemGridControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
        }
        );
        FwBrowse.addEventHandler($allOrderItemGridControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'combined');
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

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        var $form;
        $form = $grid.closest('.fwform');
        var officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');

        request.uniqueids = {
            LocationId: officeLocationId
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any, mode: string) {
        var $orderStatusHistoryGrid: any;
        var $pending = $form.find('div.fwformfield[data-datafield="PendingPo"] input').prop('checked');

        $orderStatusHistoryGrid = $form.find('[data-name="OrderStatusHistoryGrid"]');
        FwBrowse.search($orderStatusHistoryGrid);


        var $orderItemGridRental;
        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridRental);
        var $orderItemGridSales;
        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridSales);
        var $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridLabor);
        var $orderItemGridLabor;
        $orderItemGridLabor = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridLabor);
        var $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        FwBrowse.search($orderNoteGrid);
        var $orderContactGrid;
        $orderContactGrid = $form.find('[data-name="OrderContactGrid"]');
        FwBrowse.search($orderContactGrid);

        if ($pending === true) {
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.enable($form.find('[data-datafield="PoAmount"]'));
        }

        this.renderFrames($form);
        this.dynamicColumns($form);
        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');

        FwFormField.disable($form.find('[data-caption="Weeks"]'));

        var noChargeValue = FwFormField.getValueByDataField($form, 'NoCharge');
        if (noChargeValue == false) {
            FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        }

        // Display weeks or month field in billing tab
        if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
            $form.find(".BillingWeeks").hide();
            $form.find(".BillingMonths").show();
        } else {
            $form.find(".BillingMonths").hide();
            $form.find(".BillingWeeks").show();
        }

        // Display D/W field in rental
        if (FwFormField.getValueByDataField($form, 'RateType') === 'DAILY') {
            $form.find(".RentalDaysPerWeek").show();
        } else {
            $form.find(".RentalDaysPerWeek").hide();
        }

        // Bottom Line Total with Tax
        $form.find('.bottom_line_total_tax').on('change', event => {
            this.bottomLineTotalWithTaxChange($form, event);
        });

        // Bottom Line Discount
        $form.find('.bottom_line_discount').on('change', event => {
            this.bottomLineDiscountChange($form, event);
        });

        // Order Item Grid View
        $form.find('.order_item_view_select').on('change', event => {
            this.toggleOrderItemView($form, event);
        });

        // Disable withTax checkboxes if Total field is 0.00
        this.disableWithTaxCheckbox($form);

        // RentalDaysPerWeek for Rental OrderItemGrid
        $form.find('.RentalDaysPerWeek').on('change', '.fwformfield-text, .fwformfield-value', event => {
            let request: any = {};
            let quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
            let daysperweek = FwFormField.getValueByDataField($form, 'RentalDaysPerWeek');

            request.DaysPerWeek = parseFloat(daysperweek);
            request.RecType = 'R';
            request.OrderId = quoteId;

            FwAppData.apiMethod(true, 'POST', `api/v1/quote/applybottomlinedaysperweek/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($orderItemGridRental);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });

    };

    //----------------------------------------------------------------------------------------------
    disableWithTaxCheckbox($form: any) {
        if (FwFormField.getValueByDataField($form, 'PeriodRentalTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'SalesTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodLaborTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodMiscTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
        }
        if (FwFormField.getValueByDataField($form, 'PeriodCombinedTotal') === '0.00') {
            FwFormField.disable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        }
    }

    //----------------------------------------------------------------------------------------------
    bottomLineDiscountChange($form: any, event: any) {
        // DiscountPercent for all OrderItemGrid
        let $element, $orderItemGrid, quoteId, recType, discountPercent;
        let request: any = {};

        $element = jQuery(event.currentTarget);
        recType = $element.attr('data-rectype');
        quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        discountPercent = $element.find('.fwformfield-value').val().slice(0, -1);

        if (recType === 'R') {
            $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodRentalTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
        }
        if (recType === 'S') {
            $orderItemGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'SalesTotal', '');
            FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
        }
        if (recType === 'L') {
            $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodLaborTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
        }
        if (recType === 'M') {
            $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodMiscTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodCombinedTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        }
        request.DiscountPercent = parseFloat(discountPercent);
        request.RecType = recType;
        request.OrderId = quoteId;

        FwAppData.apiMethod(true, 'POST', `api/v1/quote/applybottomlinediscountpercent/`, request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    };

    //----------------------------------------------------------------------------------------------
    bottomLineTotalWithTaxChange($form: any, event: any) {
        // Total and With Tax for all OrderItemGrid
        let $element, $orderItemGrid, recType, quoteId, total, includeTaxInTotal, isWithTaxCheckbox;
        let request: any = {};

        $element = jQuery(event.currentTarget);

        isWithTaxCheckbox = $element.attr('data-type') === 'checkbox';
        recType = $element.attr('data-rectype');
        quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

        if (recType === 'R') {
            $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValueByDataField($form, 'PeriodRentalTotal');
            includeTaxInTotal = FwFormField.getValue($form, '.rentalTotalWithTax');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'RentalDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="PeriodRentalTotalIncludesTax"]'));
            }
        }
        if (recType === 'S') {
            $orderItemGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.salesOrderItemTotal');
            includeTaxInTotal = FwFormField.getValue($form, '.salesTotalWithTax');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'SalesDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="SalesTotalIncludesTax"]'));
            }
        }
        if (recType === 'L') {
            $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.laborOrderItemTotal');
            includeTaxInTotal = FwFormField.getValue($form, '.laborTotalWithTax');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'LaborDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="PeriodLaborTotalIncludesTax"]'));
            }
        }
        if (recType === 'M') {
            $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.miscOrderItemTotal');
            includeTaxInTotal = FwFormField.getValue($form, '.miscTotalWithTax');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'MiscDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="PeriodMiscTotalIncludesTax"]'));
            }
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.combinedOrderItemTotal');
            includeTaxInTotal = FwFormField.getValue($form, '.combinedTotalWithTax');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'CombinedDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
            }
        }

        request.IncludeTaxInTotal = includeTaxInTotal;
        request.RecType = recType;
        request.OrderId = quoteId;
        request.Total = +total;

        FwAppData.apiMethod(true, 'POST', `api/v1/quote/applybottomlinetotal/`, request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    };

    //----------------------------------------------------------------------------------------------
    toggleOrderItemView($form: any, event: any) {
        // Toggle between Detail and Summary view in all OrderItemGrid
        let $element, $orderItemGrid, recType, isSummary, quoteId;
        let request: any = {};

        $element = jQuery(event.currentTarget);
        recType = $element.parentsUntil('.flexrow').eq(9).attr('class');
        quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

        if (recType === 'R') {
            $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        }
        if (recType === 'S') {
            $orderItemGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        }
        if (recType === 'L') {
            $orderItemGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        }
        if (recType === 'M') {
            $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid div[data-grid="OrderItemGrid"]');
        }

        if ($orderItemGrid.data('isSummary') === false) {
            isSummary = true;
            $orderItemGrid.data('isSummary', true);
            $element.children().text('Detail View')
        }
        else {
            isSummary = false;
            $orderItemGrid.data('isSummary', false);
            $element.children().text('Summary View')
        }

        $orderItemGrid.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: quoteId,
                Summary: isSummary,
                RecType: recType
            }
            request.pagesize = 9999;
            request.orderby = "RowNumber,RecTypeDisplay"
        });

        $orderItemGrid.data('beforesave', request => {
            request.OrderId = quoteId;
            request.RecType = recType;
            request.Summary = isSummary;
        });

        FwBrowse.search($orderItemGrid);
    }

    //----------------------------------------------------------------------------------------------
    copyQuote($form) {
        var $confirmation, $yes, $no;

        $confirmation = FwConfirmation.renderConfirmation('Copy Quote', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        var html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="" style="width:90px;float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="" style="width:340px; float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="" style="width:90px; float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="width:340px;float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="New Deal" data-datafield="CopyToDealId" data-browsedisplayfield="Deal" data-validationname="DealValidation"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Copy To" data-datafield="">');
        html.push('      <div data-value="Q" data-caption="Quote"> </div>');
        html.push('    <div data-value="O" data-caption="Order"> </div></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Rates & Prices" data-datafield="CopyRatesFromInventory"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Combine Subs" data-datafield="CombineSubs"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Documents" data-datafield="CopyDocuments"></div>');
        html.push('</div>');

        var copyConfirmation = html.join('');
        var quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

        FwConfirmation.addControls($confirmation, html.join(''));

        var orderNumber, deal, description, dealId;
        $confirmation.find('div[data-caption="Type"] input').val(this.Module);
        orderNumber = FwFormField.getValueByDataField($form, this.Module + 'Number');
        $confirmation.find('div[data-caption="No"] input').val(orderNumber);
        deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
        $confirmation.find('div[data-caption="Deal"] input').val(deal);
        description = FwFormField.getValueByDataField($form, 'Description');
        $confirmation.find('div[data-caption="Description"] input').val(description);
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-text').val(deal);
        dealId = $form.find('[data-datafield="DealId"] input.fwformfield-value').val();
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-value').val(dealId);

        FwFormField.disable($confirmation.find('div[data-caption="Type"]'));
        FwFormField.disable($confirmation.find('div[data-caption="No"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Deal"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Description"]'));

        $confirmation.find('div[data-datafield="CopyRatesFromInventory"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyDates"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyLineItemNotes"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CombineSubs"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyDocuments"] input').prop('checked', true);

        $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');


        $yes.on('click', makeACopy);

        function makeACopy() {
            var request: any = {};
            request.CopyToType = $confirmation.find('[data-type="radio"] input:checked').val();
            request.CopyToDealId = FwFormField.getValueByDataField($confirmation, 'CopyToDealId');
            request.CopyRatesFromInventory = FwFormField.getValueByDataField($confirmation, 'CopyRatesFromInventory');
            request.CopyDates = FwFormField.getValueByDataField($confirmation, 'CopyDates');
            request.CopyLineItemNotes = FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes');
            request.CombineSubs = FwFormField.getValueByDataField($confirmation, 'CombineSubs');
            request.CopyDocuments = FwFormField.getValueByDataField($confirmation, 'CopyDocuments');

            if (request.CopyRatesFromInventory == "T") {
                request.CopyRatesFromInventory = "False"
            }

            for (var key in request) {
                if (request.hasOwnProperty(key)) {
                    if (request[key] == "T") {
                        request[key] = "True";
                    } else if (request[key] == "F") {
                        request[key] = "False";
                    }
                }
            }

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Copying...');
            $yes.off('click');
            var $confirmationbox = jQuery('.fwconfirmationbox');
            FwAppData.apiMethod(true, 'POST', 'api/v1/quote/copy/' + quoteId, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Quote Successfully Copied');
                FwConfirmation.destroyConfirmation($confirmation);

                var uniqueids: any = {};
                if (request.CopyToType == "O") {
                    uniqueids.OrderId = response.OrderId;
                    var $form = OrderController.loadForm(uniqueids);
                } else if (request.CopyToType == "Q") {
                    uniqueids.QuoteId = response.QuoteId;
                    var $form = QuoteController.loadForm(uniqueids);
                }
                FwModule.openModuleTab($form, "", true, 'FORM', true)

            }, function onError(response) {
                $yes.on('click', makeACopy);
                $yes.text('Copy');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                }, $confirmationbox);
        };
    }

    //----------------------------------------------------------------------------------------------
    calculateOrderItemGridTotals($form: any, gridType: string) {
        let subTotal, discount, salesTax, grossTotal, total;
        let periodExtendedTotal = new Decimal(0);
        let periodDiscountTotal = new Decimal(0);
        let taxTotal = new Decimal(0);
        const periodExtendedColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="PeriodExtended"]');
        const periodDiscountColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="PeriodDiscountAmount"]');
        const taxColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="Tax"]');

        for (let i = 1; i < periodExtendedColumn.length; i++) {
            // PeriodExtended Column
            let inputValueFromExtended: any = +periodExtendedColumn.eq(i).attr('data-originalvalue');
            periodExtendedTotal = periodExtendedTotal.plus(inputValueFromExtended);
            // PeriodDiscountAmount Column
            let inputValueFromDiscount: any = +periodDiscountColumn.eq(i).attr('data-originalvalue');
            periodDiscountTotal = periodDiscountTotal.plus(inputValueFromDiscount);
            // Tax Column
            let inputValueFromTax: any = +taxColumn.eq(i).attr('data-originalvalue');
            taxTotal = taxTotal.plus(inputValueFromTax);
        };

        subTotal = periodExtendedTotal.toFixed(2);
        discount = periodDiscountTotal.toFixed(2);
        salesTax = taxTotal.toFixed(2);
        grossTotal = periodExtendedTotal.plus(periodDiscountTotal).toFixed(2);
        total = taxTotal.plus(periodExtendedTotal).toFixed(2);

        $form.find('.' + gridType + 'totals [data-totalfield="SubTotal"] input').val(subTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Discount"] input').val(discount);
        $form.find('.' + gridType + 'totals [data-totalfield="Tax"] input').val(salesTax);
        $form.find('.' + gridType + 'totals [data-totalfield="GrossTotal"] input').val(grossTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val(total);
    };

    //----------------------------------------------------------------------------------------------
    dynamicColumns($form) {
        var orderType = FwFormField.getValueByDataField($form, "OrderTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [];

        for (var i = 3; i < fields.length; i++) {
            var name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "QuantityOrdered") {
                fieldNames.push(name);
            }
        }

        FwAppData.apiMethod(true, 'GET', "api/v1/ordertype/" + orderType, null, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.CombineActivityTabs === true) {
                $form.find('.notcombined').css('display', 'none');
                $form.find('.notcombinedtab').css('display', 'none');
                var $allOrderItemGrid;
                $allOrderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($allOrderItemGrid);
            } else {
                $form.find('.combined').css('display', 'none');
                $form.find('.combinedtab').css('display', 'none');
                var $orderItemGridRental;
                $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($orderItemGridRental);
                var $orderItemGridSales;
                $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($orderItemGridSales);
                var $orderItemGridLabor;
                $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($orderItemGridLabor);
                var $orderItemGridMisc;
                $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($orderItemGridMisc);
            }
            var hiddenRentals = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.RentalShowFields))
            var hiddenSales = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.SalesShowFields))
            var hiddenLabor = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.LaborShowFields))
            var hiddenMisc = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.MiscShowFields))
            for (var i = 0; i < hiddenRentals.length; i++) {
                jQuery($rentalGrid.find('[data-mappedfield="' + hiddenRentals[i] + '"]')).parent().hide();
            }
            for (var j = 0; j < hiddenSales.length; j++) {
                jQuery($salesGrid.find('[data-mappedfield="' + hiddenSales[j] + '"]')).parent().hide();
            }
            for (var k = 0; k < hiddenLabor.length; k++) {
                jQuery($laborGrid.find('[data-mappedfield="' + hiddenLabor[k] + '"]')).parent().hide();
            }
            for (var l = 0; l < hiddenMisc.length; l++) {
                jQuery($miscGrid.find('[data-mappedfield="' + hiddenMisc[l] + '"]')).parent().hide();
            }
        }, null, null);
    };

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
    }
}
//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{B918C711-32D7-4470-A8E5-B88AB5712863}'] = function (event) {
    var $form
    $form = jQuery(this).closest('.fwform');
    try {
        QuoteController.copyQuote($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
}
//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{BC3B1A5E-7270-4547-8FD1-4D14F505D452}'] = function (event) {
    let search, $form, quoteId, $popup;
    $form = jQuery(this).closest('.fwform');
    quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

    if (quoteId == "") {
        FwNotification.renderNotification('WARNING', 'Please save the record before performing this function');
    } else {
        search = new SearchInterface();
        $popup = search.renderSearchPopup($form, quoteId, 'Quote');
    }
}
//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{B20DDE47-A5D7-49A9-B980-8860CADBF7F6}'] = function (e) {
    var $form, $report, quoteNumber, quoteId;
    try {
        $form = jQuery(this).closest('.fwform');
        quoteNumber = $form.find('div.fwformfield[data-datafield="QuoteNumber"] input').val();
        quoteId = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
        $report = RwPrintOrderController.openForm();
        FwModule.openSubModuleTab($form, $report);
        $report.find('.fwform-section[data-caption="Order"]').css('display', 'none');
        $report.find('div.fwformfield[data-datafield="QuoteId"] input').val(quoteId);
        $report.find('div.fwformfield[data-datafield="QuoteId"] .fwformfield-text').val(quoteNumber);
        jQuery('.tab.submodule.active').find('.caption').html('Print Quote');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//---------------------------------------------------------------------------------------------- 
FwApplicationTree.clickEvents['{D27AD4E7-E924-47D1-AF6E-992B92F5A647}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');

    try {
        if ($form.attr('data-controller') === 'QuoteController') {
            QuoteController.toggleOrderItemView($form, event);
        } else {
            OrderController.toggleOrderItemView($form, event);
        }
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

}
var QuoteController = new Quote();