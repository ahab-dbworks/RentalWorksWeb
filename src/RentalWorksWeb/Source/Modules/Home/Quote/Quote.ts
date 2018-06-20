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
    };

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
    };

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
    };

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
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));

            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);

            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();
        }

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
            $form.find('[data-datafield="CombineActivity"] input').val(self.CombineActivity); 

            const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
                , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
                , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
                , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
            let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == "true") {
                $form.find('.notcombinedtab').hide();
                $form.find('.combinedtab').show();
            } else if (combineActivity == "false") {
                $form.find('.combinedtab').hide();
                $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
                $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
                $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
                $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
            }
        });

        FwFormField.disable($form.find('[data-datafield="RentalTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="SalesTaxRate1"]'));
        FwFormField.disable($form.find('[data-datafield="LaborTaxRate1"]'));

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

        FwFormField.loadItems($form.find('.online'), [
            { value: 'PARTIAL', text: 'Partial' },
            { value: 'COMPLETE', text: 'Complete' }
        ], true);

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
        }

        $form.find('.print').on('click', e => {
            var $report, quoteNumber, quoteId, recordTitle, printQuoteTab;
            try {
                quoteNumber = $form.find('div.fwformfield[data-datafield="QuoteNumber"] input').val();
                quoteId = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
                recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
                $report = RwPrintOrderController.openForm();
                FwModule.openSubModuleTab($form, $report);
                $report.find('.fwform-section[data-caption="Order"]').css('display', 'none');
                $report.find('div.fwformfield[data-datafield="QuoteId"] input').val(quoteId);
                $report.find('div.fwformfield[data-datafield="QuoteId"] .fwformfield-text').val(quoteNumber);
                jQuery('.tab.submodule.active').find('.caption').html('Print Quote');

                printQuoteTab = jQuery('.tab.submodule.active');
                printQuoteTab.find('.caption').html('Print Quote');
                printQuoteTab.attr('data-caption', 'Quote ' + recordTitle);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $form.find('.allFrames').css('display', 'none');
        $form.find('.hideFrames').css('display', 'none');
        $form.find('.expandArrow').on('click', e => {
            $form.find('.hideFrames').toggle();
            $form.find('.expandFrames').toggle();
            $form.find('.allFrames').toggle();
            $form.find('.totalRowFrames').toggle();
            if ($form.find('.summarySection').css('flex') != '0 1 65%') {
                $form.find('.summarySection').css('flex', '0 1 65%');
            } else {
                $form.find('.summarySection').css('flex', '');
            }
        });

        $form.find('.copy').on('click', e => {
            var $confirmation, $yes, $no;

            $confirmation = FwConfirmation.renderConfirmation('Confirm Copy', '');
            var html = [];
            html.push('<div class="flexrow">Copy Outgoing Address into Incoming Address?</div>');
            FwConfirmation.addControls($confirmation, html.join(''));
            $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', copyAddress);
            var $confirmationbox = jQuery('.fwconfirmationbox');
            function copyAddress() {
                FwNotification.renderNotification('SUCCESS', 'Address Successfully Copied.');
                FwConfirmation.destroyConfirmation($confirmation);
                FwFormField.setValueByDataField($form, 'InDeliveryToLocation', FwFormField.getValueByDataField($form, 'OutDeliveryToLocation'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAttention', FwFormField.getValueByDataField($form, 'OutDeliveryToAttention'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAddress1', FwFormField.getValueByDataField($form, 'OutDeliveryToAddress1'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAddress2', FwFormField.getValueByDataField($form, 'OutDeliveryToAddress2'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCity', FwFormField.getValueByDataField($form, 'OutDeliveryToCity'));
                FwFormField.setValueByDataField($form, 'InDeliveryToState', FwFormField.getValueByDataField($form, 'OutDeliveryToState'));
                FwFormField.setValueByDataField($form, 'InDeliveryToZipCode', FwFormField.getValueByDataField($form, 'OutDeliveryToZipCode'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCountryId', FwFormField.getValueByDataField($form, 'OutDeliveryToCountryId'), FwFormField.getTextByDataField($form, 'OutDeliveryToCountryId'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCrossStreets', FwFormField.getValueByDataField($form, 'OutDeliveryToCrossStreets'));                
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });

        $form.find(".weeklyType").show();
        $form.find(".monthlyType").hide();
        $form.find(".periodType input").prop('checked', true);

        $form.find(".totalType input").on('change', e => {
            const $target = jQuery(e.currentTarget),
                gridType = $target.parents('.totalType').attr('data-gridtype'),
                rateType = $target.val();

            switch (rateType) {
                case 'W':
                    $form.find('.' + gridType + 'AdjustmentsPeriod').hide();
                    $form.find('.' + gridType + 'AdjustmentsWeekly').show();
                    break;
                case 'M':
                    $form.find('.' + gridType + 'AdjustmentsPeriod').hide();
                    $form.find('.' + gridType + 'AdjustmentsMonthly').show();
                    break;
                case 'P':
                    $form.find('.' + gridType + 'AdjustmentsWeekly').hide();
                    $form.find('.' + gridType + 'AdjustmentsMonthly').hide();
                    $form.find('.' + gridType + 'AdjustmentsPeriod').show();
                    break;
            }

            this.calculateOrderItemGridTotals($form, gridType);
        });

        //$form.find('div[data-datafield="CombineActivity"]').data('onchange', $tr => {
           
        //});

        this.events($form);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };

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
            request.pagesize = max;
        });
        $orderItemGridSalesControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'sales');
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
            request.pagesize = max;
        });
        $orderItemGridLaborControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'labor');
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
            request.pagesize = max;
        });
        $orderItemGridMiscControl.data('beforesave', function (request) {
            request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
            request.RecType = 'M';
        });
        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', () => {
            this.calculateOrderItemGridTotals($form, 'misc');

            let miscItems = $form.find('.miscgrid tbody').children();
            miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
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
    };

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
        var $orderItemGridMisc;
        $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($orderItemGridLabor);
        var $orderNoteGrid;
        $orderNoteGrid = $form.find('[data-name="OrderNoteGrid"]');
        FwBrowse.search($orderNoteGrid);
        var $orderContactGrid;
        $orderContactGrid = $form.find('[data-name="OrderContactGrid"]');
        FwBrowse.search($orderContactGrid);
        var $allOrderItemGrid;
        $allOrderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
        FwBrowse.search($allOrderItemGrid);

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

        this.renderFrames($form);
        this.dynamicColumns($form);
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

        const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
            , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
            , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
            , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
        $form.find('[data-datafield="Rental"] input').on('change', e => {
            let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == 'false') {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                } else {
                    rentalTab.hide();
                }
            }
        });
        $form.find('[data-datafield="Sales"] input').on('change', e => {
            let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == 'false') {
                if (jQuery(e.currentTarget).prop('checked')) {
                    salesTab.show();
                } else {
                    salesTab.hide();
                }
            }
        });
        $form.find('[data-datafield="Miscellaneous"] input').on('change', e => {
            let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == 'false') {
                if (jQuery(e.currentTarget).prop('checked')) {
                    miscTab.show();
                } else {
                    miscTab.hide();
                }
            }
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
            if (combineActivity == 'false') {
                if (jQuery(e.currentTarget).prop('checked')) {
                    laborTab.show();
                } else {
                    laborTab.hide();
                }
            }
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
    };

    //----------------------------------------------------------------------------------------------
    events($form: any) {
        // All event listeners can be placed here and invoked in openForm
        let weeklyType = $form.find(".weeklyType");
        let monthlyType = $form.find(".monthlyType");
        let rentalDaysPerWeek = $form.find(".RentalDaysPerWeek");
        let billingMonths = $form.find(".BillingMonths");
        let billingWeeks = $form.find(".BillingWeeks");

        //Populate tax info fields with validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
        //MarketSegmentValidations
        $form.find('div[data-datafield="MarketSegmentJobId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="MarketTypeId"]', $tr.find('.field[data-browsedatafield="MarketTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="MarketType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="MarketSegmentId"]', $tr.find('.field[data-browsedatafield="MarketSegmentId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="MarketSegment"]').attr('data-originalvalue'));
        });
        //MarketSegmentValidations
        $form.find('div[data-datafield="MarketSegmentId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="MarketTypeId"]', $tr.find('.field[data-browsedatafield="MarketTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="MarketType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="MarketSegmentJobId"]', $tr.find('.field[data-browsedatafield="MarketSegmentJobId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="MarketSegmentJob"]').attr('data-originalvalue'));
        });
        // This must be below the MarketSegment Validation behavaviors
        $form.find('[data-datafield="MarketTypeId"] input').on('change', event => {
            FwFormField.setValueByDataField($form, 'MarketSegmentId', '');
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
        // This must be below the MarketSegment Validation behavaviors
        $form.find('[data-datafield="MarketSegmentId"] input').on('change', event => {
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
        // Bottom Line Total with Tax
        $form.find('.bottom_line_total_tax').on('change', event => {
            this.bottomLineTotalWithTaxChange($form, event);
        });
        // Bottom Line Discount
        $form.find('.bottom_line_discount').on('change', event => {
            this.bottomLineDiscountChange($form, event);
        });
        // Market Type Change
        $form.find('[data-datafield="MarketTypeId"] input').on('change', event => {
            FwFormField.setValueByDataField($form, 'MarketSegmentId', '');
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
        $form.find('[data-datafield="MarketSegmentId"] input').on('change', event => {
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
        ////RateType change affecting billing tab weeks or months
        //$form.find('.RateType').on('change', $tr => {
        //    if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
        //        $form.find(".BillingWeeks").hide();
        //        $form.find(".BillingMonths").show();
        //    } else {
        //        $form.find(".BillingMonths").hide();
        //        $form.find(".BillingWeeks").show();
        //    }
        //});
        ////RateType change affecting DaysPerWeek field in rental tab
        //$form.find('.RateType').on('change', $tr => {
        //    if (FwFormField.getValueByDataField($form, 'RateType') === 'DAILY') {
        //        $form.find(".RentalDaysPerWeek").show();
        //    } else {
        //        $form.find(".RentalDaysPerWeek").hide();
        //    }
        //});

        $form.find('.RateType').on('change', $tr => {
            let rateType = FwFormField.getValueByDataField($form, 'RateType');
            switch (rateType) {
                case 'DAILY':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    $form.find('.combinedgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.rentalgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.salesgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.laborgrid [data-name="OrderItemGrid"]').parent().show();
                    $form.find('.miscgrid [data-name="OrderItemGrid"]').parent().show();
                    break;
                case 'WEEKLY':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case '3WEEK':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case 'MONTHLY':
                    weeklyType.hide();
                    monthlyType.show();
                    rentalDaysPerWeek.hide();
                    billingWeeks.hide();
                    billingMonths.show();
                    break;
                default:
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
            }
        });

        // PickDate Validations
        $form.find('.pick_date_validation').on('changeDate', event => {
            this.checkDateRangeForPick($form, event);
        });
        // BillingDate Change
        $form.find('.billing_start_date').on('changeDate', event => {
            this.adjustWeekorMonthBillingField($form, event);
        });
        // BillingDate Change
        $form.find('.billing_end_date').on('changeDate', event => {
            this.adjustWeekorMonthBillingField($form, event);
        });
        // Billing Weeks or Month field change
        $form.find('.week_or_month_field').on('change', event => {
            this.adjustBillingEndDate($form, event);
        });
        // Pending PO
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
        // RentalDaysPerWeek for Rental OrderItemGrid
        $form.find('.RentalDaysPerWeek').on('change', '.fwformfield-text, .fwformfield-value', event => {
            let request: any = {};
            let $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
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
    adjustBillingEndDate($form, event) {
        let newEndDate, daysToAdd, parsedBillingStartDate, daysBetweenDates, parsedBillingEndDate, monthValue, weeksValue, billingStartDate;
        parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
        parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
        billingStartDate = FwFormField.getValueByDataField($form, 'BillingStartDate');
        daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000; // 1 day has 86400000ms
        monthValue = FwFormField.getValueByDataField($form, 'BillingMonths');
        weeksValue = FwFormField.getValueByDataField($form, 'BillingWeeks');

        if (!isNaN(parsedBillingStartDate)) { // only if StartDate is defined
            if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
                if (!isNaN(monthValue) && monthValue !== '0' && Math.sign(monthValue) !== -1 && Math.sign(monthValue) !== -0) {
                    FwAppData.apiMethod(true, 'GET', `api/v1/datefunctions/addmonths?Date=${billingStartDate}&Months=${monthValue}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                        newEndDate = FwFunc.getDate(response, -1)
                        FwFormField.setValueByDataField($form, 'BillingEndDate', newEndDate);
                        parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
                        parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
                        daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000; // 1 day has 86400000ms
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form);
                }
            }
            else {
                if (!isNaN(weeksValue) && weeksValue !== '0' && Math.sign(weeksValue) !== -1 && Math.sign(weeksValue) !== -0) {
                    daysToAdd = +(weeksValue * 7) - 1;
                    newEndDate = FwFunc.getDate(billingStartDate, daysToAdd);
                    FwFormField.setValueByDataField($form, 'BillingEndDate', newEndDate);
                    parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
                    parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
                    daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000; // 1 day has 86400000ms
                }
            }
        }

        if (!isNaN(daysBetweenDates)) {
            if (Math.sign(daysBetweenDates) >= 0) {
                $form.find('div[data-datafield="BillingEndDate"]').removeClass('error');
            } else {
                FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
                $form.find('div[data-datafield="BillingEndDate"]').addClass('error');
                FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
                FwFormField.setValueByDataField($form, 'BillingMonths', '0');
            }
        }
    };

    //----------------------------------------------------------------------------------------------
    adjustWeekorMonthBillingField($form, event) {
        let monthValue, daysBetweenDates, billingStartDate, billingEndDate, weeksValue, parsedBillingStartDate, parsedBillingEndDate;
        billingStartDate = FwFormField.getValueByDataField($form, 'BillingStartDate');
        billingEndDate = FwFormField.getValueByDataField($form, 'BillingEndDate');
        parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
        parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
        monthValue = FwFormField.getValueByDataField($form, 'BillingMonths');
        weeksValue = FwFormField.getValueByDataField($form, 'BillingWeeks');
        daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000; // 1 day has 86400000ms
        
        if (!isNaN(parsedBillingStartDate)) { // only if StartDate is defined
            if (FwFormField.getValueByDataField($form, 'RateType') === 'MONTHLY') {
                monthValue = Math.ceil(daysBetweenDates / 31);
                if (!isNaN(monthValue) && monthValue !== '0' && Math.sign(monthValue) !== -1 && Math.sign(monthValue) !== -0) {
                    FwAppData.apiMethod(true, 'GET', `api/v1/datefunctions/numberofmonths?FromDate=${billingStartDate}&ToDate=${billingEndDate}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                        monthValue = response;
                        FwFormField.setValueByDataField($form, 'BillingMonths', monthValue);
                        parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
                        parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
                        daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000; // 1 day has 86400000ms
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, $form);
                } else if (daysBetweenDates === 0) {
                    FwFormField.setValueByDataField($form, 'BillingMonths', '0');
                }
            } else {
                weeksValue = Math.ceil(daysBetweenDates / 7);
                if (!isNaN(weeksValue) && weeksValue !== '0' && Math.sign(weeksValue) !== -1 && Math.sign(weeksValue) !== -0) {
                    FwFormField.setValueByDataField($form, 'BillingWeeks', weeksValue);
                    parsedBillingStartDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingStartDate'));
                    parsedBillingEndDate = Date.parse(FwFormField.getValueByDataField($form, 'BillingEndDate'));
                    daysBetweenDates = (parsedBillingEndDate - parsedBillingStartDate) / 86400000; // 1 day has 86400000ms
                } else if (daysBetweenDates === 0) {
                    FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
                }
            }
        } else {
            FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
        }

        if (!isNaN(daysBetweenDates)) {
            if (Math.sign(daysBetweenDates) >=0) {
                $form.find('div[data-datafield="BillingEndDate"]').removeClass('error');
            } else {
                FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
                $form.find('div[data-datafield="BillingEndDate"]').addClass('error');
                FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
                FwFormField.setValueByDataField($form, 'BillingMonths', '0');
            }
        }
    };

    //----------------------------------------------------------------------------------------------
    cancelUncancelQuote($form: any) {
        let $confirmation, $yes, $no, quoteId, quoteStatus, self;
        self = this;
        quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        quoteStatus = FwFormField.getValueByDataField($form, 'Status');

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
        }
        else {
            throw new Error("Please select a Quote to perform this action.");
        }

        function cancelQuote() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Canceling...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Quote Successfully Canceled');
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, self);
            }, function onError(response) {
                $yes.on('click', cancelQuote);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, self);
            }, $form);
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
                FwModule.refreshForm($form, self);
            }, function onError(response) {
                $yes.on('click', uncancelQuote);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, self);
            }, $form);
        };
    };

    //----------------------------------------------------------------------------------------------
    checkDateRangeForPick($form, event) {
        let $element, parsedPickDate, parsedFromDate, parsedToDate;
        $element = jQuery(event.currentTarget);

        parsedPickDate = Date.parse(FwFormField.getValueByDataField($form, 'PickDate'));
        parsedFromDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStartDate'));
        parsedToDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStopDate'));

        if ($element.attr('data-datafield') === 'EstimatedStartDate' && parsedFromDate < parsedPickDate) {
            $form.find('div[data-datafield="EstimatedStartDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'From Date' is before 'Pick Date'.");
        }
        else if ($element.attr('data-datafield') === 'PickDate' && parsedFromDate < parsedPickDate) {
            $form.find('div[data-datafield="PickDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'From Date'.");
        }
        else if ($element.attr('data-datafield') === 'PickDate' && parsedToDate < parsedPickDate) {
            $form.find('div[data-datafield="PickDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'To Date'.");
        }
        else if (parsedToDate < parsedFromDate) {
            $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
        }
        else if (parsedToDate < parsedPickDate) {
            $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'Pick Date'.");
        }
        else {
            $form.find('div[data-datafield="PickDate"]').removeClass('error');
            $form.find('div[data-datafield="EstimatedStartDate"]').removeClass('error');
            $form.find('div[data-datafield="EstimatedStopDate"]').removeClass('error');
        }
    };

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
        let $element, $orderItemGrid, recType, quoteId, total, includeTaxInTotal, isWithTaxCheckbox, totalType;
        let request: any = {};

        $element = jQuery(event.currentTarget);

        isWithTaxCheckbox = $element.attr('data-type') === 'checkbox';
        recType = $element.attr('data-rectype');
        quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

        if (recType === 'R') {
            $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.rentalOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.rentalTotalWithTax:visible');
            totalType = $form.find('.rentalgrid .totalType input:checked').val();
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'RentalDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.rentalTotalWithTax:visible'));
            } else {
                FwFormField.enable($form.find('.rentalTotalWithTax:visible'));
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
            total = FwFormField.getValue($form, '.laborOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.laborTotalWithTax:visible');
            totalType = $form.find('.laborgrid .totalType input:checked').val();
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'LaborDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.laborTotalWithTax:visible'));
            } else {
                FwFormField.enable($form.find('.laborTotalWithTax:visible'));
            }
        }
        if (recType === 'M') {
            $orderItemGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.miscOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.miscTotalWithTax:visible');
            totalType = $form.find('.miscgrid .totalType input:checked').val();
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'MiscDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.miscTotalWithTax:visible'));
            } else {
                FwFormField.enable($form.find('.miscTotalWithTax:visible'));
            }
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.combinedOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.combinedTotalWithTax:visible');
            totalType = $form.find('.combinedgrid .totalType input:checked').val();
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'CombinedDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.combinedTotalWithTax:visible'));
            } else {
                FwFormField.enable($form.find('.combinedTotalWithTax:visible'));
            }
        }
        request.TotalType = totalType;
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
    };

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
    orderItemGridBoldUnbold($browse: any, event: any) {
        let $confirmation, $yes, $no, quoteId, orderItemId, boldStatus;

        quoteId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        orderItemId = $browse.find('.selected [data-formdatafield="OrderItemId"]').attr('data-originalvalue');
        boldStatus = $browse.find('.selected [data-formdatafield="Bold"]').attr('data-originalvalue');

        if (quoteId != null) {
            if (boldStatus === "true") {
                $confirmation = FwConfirmation.renderConfirmation('Unbold', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to remove bold for this item?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Unbold Item', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', unboldItem);
            } else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to bold this Item?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Bold Item', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', boldItem);

            }
        } else {
            throw new Error("Please select an Item to perform this action.");
        }

        function boldItem() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Bolding Item...');
            $yes.off('click');

            request = {
                OrderId: quoteId,
                OrderItemId: orderItemId,
                Bold: true,
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Success');
                FwConfirmation.destroyConfirmation($confirmation);
                FwBrowse.databind($browse);
            }, function onError(response) {
                $yes.on('click', boldItem);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwBrowse.databind($browse);
            }, $browse);
        };

        function unboldItem() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Removing...');
            $yes.off('click');

            request = {
                OrderId: quoteId,
                OrderItemId: orderItemId,
                Bold: false,
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Bold Removed');
                FwConfirmation.destroyConfirmation($confirmation);
                FwBrowse.databind($browse);
            }, function onError(response) {
                $yes.on('click', unboldItem);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwBrowse.databind($browse);
            }, $browse);
        };
    };

    //----------------------------------------------------------------------------------------------
    orderItemGridLockUnlock($browse: any, event: any) {
        let $confirmation, $yes, $no, quoteId, orderItemId, lockedStatus;

        quoteId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        orderItemId = $browse.find('.selected [data-formdatafield="OrderItemId"]').attr('data-originalvalue');
        lockedStatus = $browse.find('.selected [data-formdatafield="Locked"]').attr('data-originalvalue');

        if (quoteId != null) {
            if (lockedStatus === "true") {
                $confirmation = FwConfirmation.renderConfirmation('Unlock', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to unlock this item?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Unlock Item', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', unlockItem);
            } else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to lock this Item?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Lock Item', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', lockItem);

            }
        } else {
            throw new Error("Please select an Item to perform this action.");
        }
        
        function lockItem() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Locking Item...');
            $yes.off('click');

            request = {
                OrderId: quoteId,
                OrderItemId: orderItemId,
                Locked: true,
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Item Successfully Locked');
                FwConfirmation.destroyConfirmation($confirmation);
                FwBrowse.databind($browse);
            }, function onError(response) {
                $yes.on('click', lockItem);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwBrowse.databind($browse);
            }, $browse);
        };

        function unlockItem() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Unlocking...');
            $yes.off('click');

            request = {
                OrderId: quoteId,
                OrderItemId: orderItemId,
                Locked: false,
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Item Successfully Unlocked');
                FwConfirmation.destroyConfirmation($confirmation);
                FwBrowse.databind($browse);
            }, function onError(response) {
                $yes.on('click', unlockItem);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwBrowse.databind($browse);
            }, $browse);
        };
    };

    //----------------------------------------------------------------------------------------------
    calculateOrderItemGridTotals($form: any, gridType: string) {
        let subTotal, discount, salesTax, grossTotal, total, rateType;
        let extendedTotal = new Decimal(0);
        let discountTotal = new Decimal(0);
        let taxTotal = new Decimal(0);

        let rateValue = $form.find('.' + gridType + 'grid .totalType input:checked').val();
        switch (rateValue) {
            case 'W':
                rateType = 'Weekly';
                break;
            case 'P':
                rateType = 'Period';
                break;
            case 'M':
                rateType = 'Monthly';
                break;
        }

        const extendedColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'Extended"]');
        const discountColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'DiscountAmount"]');
        const taxColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="' + rateType + 'Tax"]');

        for (let i = 1; i < extendedColumn.length; i++) {
            // Extended Column
            let inputValueFromExtended: any = +extendedColumn.eq(i).attr('data-originalvalue');
            extendedTotal = extendedTotal.plus(inputValueFromExtended);
            // DiscountAmount Column
            let inputValueFromDiscount: any = +discountColumn.eq(i).attr('data-originalvalue');
            discountTotal = discountTotal.plus(inputValueFromDiscount);
            // Tax Column
            let inputValueFromTax: any = +taxColumn.eq(i).attr('data-originalvalue');
            taxTotal = taxTotal.plus(inputValueFromTax);
        };

        subTotal = extendedTotal.toFixed(2);
        discount = discountTotal.toFixed(2);
        salesTax = taxTotal.toFixed(2);
        grossTotal = extendedTotal.plus(discountTotal).toFixed(2);
        total = taxTotal.plus(extendedTotal).toFixed(2);

        $form.find('.' + gridType + 'totals [data-totalfield="SubTotal"] input').val(subTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Discount"] input').val(discount);
        $form.find('.' + gridType + 'totals [data-totalfield="Tax"] input').val(salesTax);
        $form.find('.' + gridType + 'totals [data-totalfield="GrossTotal"] input').val(grossTotal);
        $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val(total);

        $form.find('.' + gridType + 'Adjustments .' + gridType + 'OrderItemTotal:visible input').val(total);
    };

    //----------------------------------------------------------------------------------------------
    dynamicColumns($form) {
        var orderType = FwFormField.getValueByDataField($form, "OrderTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [],
            self = this;

        for (var i = 3; i < fields.length; i++) {
            var name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "QuantityOrdered") {
                fieldNames.push(name);
            }
        }

        FwAppData.apiMethod(true, 'GET', "api/v1/ordertype/" + orderType, null, FwServices.defaultTimeout, function onSuccess(response) {
            $form.find('[data-datafield="CombineActivity"] input').val(response.CombineActivityTabs);

            const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
                , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
                , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
                , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
            if (response.CombineActivityTabs === false) {
                $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
                $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
                $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
                $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
            }

            if (response.CombineActivityTabs === true) {
                $form.find('.notcombined').css('display', 'none');
                $form.find('.notcombinedtab').css('display', 'none');
            } else {
                $form.find('.combined').css('display', 'none');
                $form.find('.combinedtab').css('display', 'none');
            }
            var hiddenRentals : Array<string> = fieldNames.filter(function (field) {
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
            if (!hiddenRentals.includes('WeeklyExtended')) {
                $rentalGrid.find('.3weekextended').parent().show();
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
    };

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
    };

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
    };

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
    };

    //----------------------------------------------------------------------------------------------
    beforeValidateMarketSegment($browse, $grid, request) {
        const validationName = request.module;
        const marketTypeValue = jQuery($grid.find('[data-validationname="MarketTypeValidation"] input')).val();
        const marketSegmentValue = jQuery($grid.find('[data-validationname="MarketSegmentValidation"] input')).val();

        switch (validationName) {
            case 'MarketSegmentValidation':
                if (marketTypeValue !== "") {
                    request.uniqueids = {
                        MarketTypeId: marketTypeValue,
                    };
                    break;
                }
            case 'MarketSegmentJobValidation':
                if (marketSegmentValue !== "") {
                    request.uniqueids = {
                        MarketTypeId: marketTypeValue,
                        MarketSegmentId: marketSegmentValue,
                    };
                    break;
                }
        };
    };

    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        var $form;
        $form = $grid.closest('.fwform');
        var officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');

        request.uniqueids = {
            LocationId: officeLocationId
        }
    };
};


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
};

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
        QuoteController.cancelUncancelQuote($form);
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
                    FwNotification.renderNotification('SUCCESS', 'Quote Successfully Canceled');
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
            throw new Error("Please select a Quote to perform this action.");
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
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//-----------------------------------------------------------------------------------------------------
var QuoteController = new Quote();