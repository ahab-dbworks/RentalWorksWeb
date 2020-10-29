routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return QuoteController.getModuleScreen(filter); } });

class Quote {
    Module:             string = 'Quote';
    apiurl:             string = 'api/v1/quote';
    caption:            string = Constants.Modules.Agent.children.Quote.caption;
	nav:                string = Constants.Modules.Agent.children.Quote.nav;
	id:                 string = Constants.Modules.Agent.children.Quote.id;
    ActiveViewFields:   any    = {};
    ActiveViewFieldsId: string;
    DefaultOrderType:   string;
    DefaultOrderTypeId: string;
    totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var self        = this;
        var screen: any = {};
        screen.$view    = FwModule.getModuleControl(this.Module + 'Controller');

        var $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
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

        var department = JSON.parse(sessionStorage.getItem('department'));
        var location   = JSON.parse(sessionStorage.getItem('location'));
        
        FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
            self.DefaultOrderType = response.DefaultOrderType;
            self.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    //addBrowseMenuItems(options: IAddBrowseMenuOptions) {
    //    const $all:       JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
    //    const $new:       JQuery = FwMenu.generateDropDownViewBtn('New', false, "NEW");
    //    const $request:   JQuery = FwMenu.generateDropDownViewBtn('Request', false, "REQUEST");
    //    const $active:    JQuery = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
    //    const $reserved:  JQuery = FwMenu.generateDropDownViewBtn('Reserved', false, "RESERVED");
    //    const $ordered:   JQuery = FwMenu.generateDropDownViewBtn('Ordered', false, "ORDERED");
    //    const $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
    //    const $closed:    JQuery = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

    //    FwMenu.addVerticleSeparator(options.$menu);

    //    let viewSubitems: Array<JQuery> = [];
    //    viewSubitems.push($all, $new, $request, $active, $reserved, $ordered, $cancelled, $closed);
    //    FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");

    //    if (sessionStorage.getItem('userType') === 'USER') {
    //        //Location Filter
    //        const location      = JSON.parse(sessionStorage.getItem('location'));
    //        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
    //        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

    //        if (typeof this.ActiveViewFields["LocationId"] === 'undefined') {
    //            this.ActiveViewFields.LocationId = [location.locationid];
    //        }

    //        let viewLocation: Array<JQuery> = [];
    //        viewLocation.push($userLocation, $allLocations);
    //        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    //    }
    //    else if (sessionStorage.getItem('userType') === 'CONTACT') {
    //        //Deal Filter
    //        const deal      = JSON.parse(sessionStorage.getItem('deal'));
    //        if (typeof this.ActiveViewFields["DealId"] === 'undefined') {
    //            this.ActiveViewFields.DealId = [deal.dealid];
    //        }
    //    }

    //    return options;
    //}
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'dpH0uCuEp3E89', (e: JQuery.ClickEvent) => {
            try {
                this.browseCancelOption(options.$browse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $new: JQuery = FwMenu.generateDropDownViewBtn('New', false, "NEW");
        const $request: JQuery = FwMenu.generateDropDownViewBtn('Request', false, "REQUEST");
        const $prospect: JQuery = FwMenu.generateDropDownViewBtn('Prospect', true, "PROSPECT");
        const $active: JQuery = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $reserved: JQuery = FwMenu.generateDropDownViewBtn('Reserved', false, "RESERVED");
        const $ordered: JQuery = FwMenu.generateDropDownViewBtn('Ordered', false, "ORDERED");
        const $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        FwMenu.addVerticleSeparator(options.$menu);

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $request, $prospect, $active, $reserved, $ordered, $cancelled, $closed);
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");

        if (sessionStorage.getItem('userType') === 'USER') {
            //Location Filter
            const location = JSON.parse(sessionStorage.getItem('location'));
            const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
            const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

            if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
                this.ActiveViewFields.LocationId = [location.locationid];
            }

            let viewLocation: Array<JQuery> = [];
            viewLocation.push($userLocation, $allLocations);
            FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
        } else if (sessionStorage.getItem('userType') === 'CONTACT') {
            //Location Filter
            const deal = JSON.parse(sessionStorage.getItem('deal'));
            //const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
            //const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

            if (typeof this.ActiveViewFields["DealId"] == 'undefined') {
                this.ActiveViewFields.DealId = [deal.dealid];
            }

            //let viewLocation: Array<JQuery> = [];
            //viewLocation.push($userLocation, $allLocations);
            //FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
        }
    }
    //----------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Copy Quote', 'KaKyGTNrQjLq', (e: JQuery.ClickEvent) => {
            try {
                this.copyOrderOrQuote(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //need to discuss
        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Quote', '', (e: JQuery.ClickEvent) => {
            try {
                this.printQuoteOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //FwMenu.addSubMenuItem(options.$groupOptions, 'Search', '', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.search(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Order', 'PPxCaMMveWaz', (e: JQuery.ClickEvent) => {
            try {
                this.createOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //FwMenu.addSubMenuItem(options.$groupOptions, 'New Version', '6KMadUFDT4cX4', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.createNewVersionQuote(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
        //FwMenu.addSubMenuItem(options.$groupOptions, 'Make Quote Active', '7mrZ4Q8ShsJ', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.makeQuoteActive(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
        //need to discuss
        //FwMenu.addSubMenuItem(options.$groupOptions, 'Reserve', '', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.reserveQuote(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'gchoKFMGs1WY', (e: JQuery.ClickEvent) => {
            try {
                OrderController.cancelUncancelOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //FwMenu.addSubMenuItem(options.$groupOptions, 'Change Office Location', 'eu2FcQiK9adgk', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.changeOfficeLocation(options.$form);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
    }


    openForm(mode: string, parentModuleInfo?: any) {
        let userType = sessionStorage.getItem('userType');
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            var today      = FwFunc.getDate();
            var warehouse  = JSON.parse(sessionStorage.getItem('warehouse'));
            var office     = JSON.parse(sessionStorage.getItem('location'));
            var department = JSON.parse(sessionStorage.getItem('department'));

            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            FwFormField.setValueByDataField($form, 'VersionNumber', 1);

            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="Rental"]', true);
            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);
            FwFormField.setValueByDataField($form, 'PendingPo', true);
            FwFormField.setValueByDataField($form, 'PoNumber', 'PENDING');

            if (userType === 'CONTACT') {
                let deal = JSON.parse(sessionStorage.getItem('deal'));
                FwFormField.setValueByDataField($form, 'DealId', deal.dealid, deal.deal);
            }
        }

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
        }

        this.events($form);
        this.renderPrintButton($form);
        this.renderSearchButton($form);

        if (userType === 'CONTACT') {
            FwFormField.disableDataField($form, 'DealId');
            this.renderSubmitButton($form);
        } else if (userType === 'USER') {
            this.renderActiveRequest($form);
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderPrintButton($form: any) {
        var $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', function () {
            try {
                var $form       = jQuery(this).closest('.fwform');
                var quoteNumber = FwFormField.getValue($form, 'div[data-datafield="QuoteNumber"]');
                var quoteId     = FwFormField.getValue($form, 'div[data-datafield="QuoteId"]');
                var $report     = QuoteReportController.openForm();

                FwModule.openSubModuleTab($form, $report);

                FwFormField.setValueByDataField($report, 'QuoteId', quoteId, quoteNumber);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderSearchButton($form: any) {
        var $search = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'QuikSearch', 'searchbtn');
        $search.prepend('<i class="material-icons">search</i>');
        $search.addClass('disabled');
        $search.on('click', function () {
            try {
                let $form   = jQuery(this).closest('.fwform');
                let orderId = FwFormField.getValueByDataField($form, 'QuoteId');
                
                if (orderId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else if (!jQuery(this).hasClass('disabled')) {
                    let search = new SearchInterface();
                    search.renderSearchPopup($form, orderId, 'Request');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderSubmitButton($form: any) {
        var $submit = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Submit Request', 'submitbtn');
        $submit.prepend('<i class="material-icons">publish</i>');
        $submit.addClass('disabled');
        $submit.on('click', function () {
            try {
                let $form   = jQuery(this).closest('.fwform');
                let quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
                
                if (quoteId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else if (!jQuery(this).hasClass('disabled')) {
                    var $confirmation = FwConfirmation.renderConfirmation('Submit Request', 'Would you like to submit this request?');
                    var $submit       = FwConfirmation.addButton($confirmation, 'Submit');
                    var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel');

                    $submit.on('click', function() {
                        FwAppData.apiMethod(true, 'POST', `api/v1/quote/submit/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                            if (response !== null) {
                                FwNotification.renderNotification('SUCCESS', 'Request Submitted.');
                                FwFormField.setValueByDataField($form, 'Status', response.Status);
                                $form.find('.btn[data-securityid="submitbtn"]').addClass('disabled');
                            }
                        }, null, null);
                    });
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderActiveRequest($form: any) {
        var $activate = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Activate Request', 'activatebtn');
        $activate.prepend('<i class="material-icons">publish</i>');
        $activate.addClass('disabled');
        $activate.on('click', function () {
            try {
                let $form = jQuery(this).closest('.fwform');
                
                if (!jQuery(this).hasClass('disabled')) {
                    var $confirmation = FwConfirmation.renderConfirmation('Activate Request?', 'Would you like to activate this request?');
                    var $activate     = FwConfirmation.addButton($confirmation, 'Activate');
                    var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel');

                    $activate.on('click', function () {
                        let quoteId       = FwFormField.getValueByDataField($form, 'QuoteId');
                        let $quoteTab     = jQuery('#' + $form.closest('.tabpage').attr('data-tabid'));

                        FwAppData.apiMethod(true, 'POST', `api/v1/quote/activatequoterequest/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                            FwConfirmation.destroyConfirmation($confirmation);
                            FwTabs.removeTab($quoteTab);
                            let uniqueids: any = {
                                QuoteId: response.QuoteId
                            };
                            var $quoteform = QuoteController.loadForm(uniqueids);
                            FwModule.openModuleTab($quoteform, "", true, 'FORM', true);
                            FwNotification.renderNotification('SUCCESS', 'Request Activated.');
                        }, null, $confirmation);
                    });
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
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
        //let self        = this;
        //var $orderStatusHistoryGrid        = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        //var $orderStatusHistoryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusHistoryGridBrowse').html());
        //$orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        //$orderStatusHistoryGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: $form.find('div.fwformfield[data-datafield="QuoteId"] input').val()
        //    };
        //})
        //FwBrowse.init($orderStatusHistoryGridControl);
        //FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderStatusHistoryGrid',
            gridSecurityId: 'lATsdnAx7B4s',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            }
        });

        //var $quoteItemGridRental        = $form.find('div[data-grid="QuoteItemGrid"]');
        //var $quoteItemGridRentalControl = jQuery(jQuery('#tmpl-grids-QuoteItemGridBrowse').html());
        //$quoteItemGridRental.empty().append($quoteItemGridRentalControl);
        //$quoteItemGridRental.addClass('R');
        //$quoteItemGridRentalControl.data('isSummary', false);

        //$quoteItemGridRentalControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'QuoteId'),
        //        RecType: 'R'
        //    };
        //    request.totalfields = this.totalFields;
        //});
        //$quoteItemGridRentalControl.data('beforesave', function (request) {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
        //    request.RecType = 'R';
        //});
        //FwBrowse.addEventHandler($quoteItemGridRentalControl, 'afterdatabindcallback', ($control, dt) => {
        //    let rentalItems = $form.find('.rentalgrid tbody').children();
        //    rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        //});
        //FwBrowse.init($quoteItemGridRentalControl);
        //FwBrowse.renderRuntimeHtml($quoteItemGridRentalControl);
        FwBrowse.renderGrid({
            nameGrid: 'QuoteItemGrid',
            gridSelector: '.rentalgrid div[data-grid="QuoteItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    RecType: 'R'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.RecType = 'R';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
                $browse.data('isSummary', false);
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                let rentalItems = $form.find('.rentalgrid tbody').children();
                rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
            }
        });

        //var $orderNoteGrid        = $form.find('div[data-grid="OrderNoteGrid"]');
        //var $orderNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderNoteGridBrowse').html());
        //$orderNoteGrid.empty().append($orderNoteGridControl);
        //$orderNoteGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'QuoteId')
        //    };
        //});
        //$orderNoteGridControl.data('beforesave', function (request) {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId')
        //});
        //FwBrowse.init($orderNoteGridControl);
        //FwBrowse.renderRuntimeHtml($orderNoteGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderNoteGrid',
            gridSecurityId: '8aq0E3nK2upt',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            }
        });

        //var $orderContactGrid        = $form.find('div[data-grid="OrderContactGrid"]');
        //var $orderContactGridControl = jQuery(jQuery('#tmpl-grids-OrderContactGridBrowse').html());
        //$orderContactGrid.empty().append($orderContactGridControl);
        //$orderContactGridControl.find('div[data-datafield="IsOrderedBy"]').attr('data-caption', 'Quoted For');
        //$orderContactGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'QuoteId')
        //    };
        //});
        //$orderContactGridControl.data('beforesave', function (request) {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'QuoteId');
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        //});
        //FwBrowse.init($orderContactGridControl);
        //FwBrowse.renderRuntimeHtml($orderContactGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'OrderContactGrid',
            gridSecurityId: '7CUe9WvpWNat',
            moduleSecurityId: this.id,

            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        let self        = this;
        let status      = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes    = FwFormField.getValueByDataField($form, 'HasNotes');
        let usertype    = sessionStorage.getItem('userType');

        if ((status === 'ORDERED' || status === 'CLOSED' || status === 'CANCELLED') ||
           ((usertype === 'CONTACT') && (status === 'ACTIVE' || status === 'RESERVED')) ||
           ((usertype === 'USER') && (status === 'REQUEST' || status === 'NEW'))) {
            FwModule.setFormReadOnly($form);
        } else {
            $form.find('.btn[data-securityid="searchbtn"]').removeClass('disabled');
        }

        if ((usertype === 'CONTACT') && (status === 'NEW')) {
            $form.find('.btn[data-securityid="submitbtn"]').removeClass('disabled');
        }

        if ((usertype === 'USER') && (status === 'REQUEST')) {
            $form.find('.btn[data-securityid="activatebtn"]').removeClass('disabled');
        }

        if (hasNotes) {
            FwTabs.setTabColor($form.find('.notestab'), '#FFFF00');
        }

        var $quoteItemGridRental = $form.find('[data-name="QuoteItemGrid"]');
        FwBrowse.search($quoteItemGridRental);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        $form
            .on('changeDate', 'div[data-datafield="PickDate"], div[data-datafield="EstimatedStartDate"], div[data-datafield="EstimatedStopDate"]', event => {
                var $element           = jQuery(event.currentTarget);
                var PickDate           = Date.parse(FwFormField.getValueByDataField($form, 'PickDate'));
                var EstimatedStartDate = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStartDate'));
                var EstimatedStopDate  = Date.parse(FwFormField.getValueByDataField($form, 'EstimatedStopDate'));

                if ($element.attr('data-datafield') === 'EstimatedStartDate' && EstimatedStartDate < PickDate) {
                    $form.find('div[data-datafield="EstimatedStartDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'From Date' is before 'Pick Date'.");
                } else if ($element.attr('data-datafield') === 'PickDate' && EstimatedStartDate < PickDate) {
                    $form.find('div[data-datafield="PickDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'From Date'.");
                } else if ($element.attr('data-datafield') === 'PickDate' && EstimatedStopDate < PickDate) {
                    $form.find('div[data-datafield="PickDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'To Date'.");
                } else if (EstimatedStopDate < EstimatedStartDate) {
                    $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
                } else if (EstimatedStopDate < PickDate) {
                    $form.find('div[data-datafield="EstimatedStopDate"]').addClass('error');
                    FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'Pick Date'.");
                } else {
                    $form.find('div[data-datafield="PickDate"]').removeClass('error');
                    $form.find('div[data-datafield="EstimatedStartDate"]').removeClass('error');
                    $form.find('div[data-datafield="EstimatedStopDate"]').removeClass('error');
                }
            })
            .on('click', '[data-type="tab"]', event => {
                if ($form.attr('data-mode') !== 'NEW') {
                    const $tab      = jQuery(event.currentTarget);
                    const tabpageid = jQuery(event.currentTarget).data('tabpageid');

                    if ($tab.hasClass('audittab') == false) {
                        const $gridControls = $form.find(`#${tabpageid} [data-type="Grid"]`);
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

                        const $browseControls = $form.find(`#${tabpageid} [data-type="Browse"]`);
                        if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                            for (let i = 0; i < $browseControls.length; i++) {
                                const $browseControl = jQuery($browseControls[i]);
                                FwBrowse.search($browseControl);
                            }
                        }
                    }
                    $tab.addClass('tabGridsLoaded');
                }
            })
        ;
    }
    //----------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------
    browseCancelOption($browse: JQuery) {
        try {
            let $confirmation, $yes, $no;
            const quoteId = $browse.find('.selected [data-browsedatafield="QuoteId"]').attr('data-originalvalue');
            const quoteStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');
            if (quoteId != null) {
                if (quoteStatus === "CANCELLED") {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    const html: Array<string> = [];
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
    }
    //----------------------------------------------------------------------------------------------
    copyOrderOrQuote($form: any) {
        const module = this.Module;
        const $confirmation = FwConfirmation.renderConfirmation(`Copy ${module}`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        const html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="" style="width:90px; float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="width:340px;float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="New Deal" data-datafield="CopyToDealId" data-browsedisplayfield="Deal" data-validationname="DealValidation"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Copy To" data-datafield="CopyTo">');
        html.push('      <div data-value="Q" data-caption="Quote"> </div>');
        html.push('    <div data-value="O" data-caption="Order"> </div></div><br>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
        html.push('</div>');


        FwConfirmation.addControls($confirmation, html.join(''));

        $confirmation.find('div[data-caption="Type"] input').val(module);
        const orderNumber = FwFormField.getValueByDataField($form, `${module}Number`);
        $confirmation.find('div[data-caption="No"] input').val(orderNumber);
        const deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
        $confirmation.find('div[data-caption="Deal"] input').val(deal);
        const description = FwFormField.getValueByDataField($form, 'Description');
        $confirmation.find('div[data-caption="Description"] input').val(description);
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-text').val(deal);
        const dealId = $form.find('[data-datafield="DealId"] input.fwformfield-value').val();
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-value').val(dealId);

        if (module === 'Order') {
            $confirmation.find('div[data-datafield="CopyTo"] [data-value="O"] input').prop('checked', true);
        };

        FwFormField.disable($confirmation.find('div[data-caption="Type"]'));
        FwFormField.disable($confirmation.find('div[data-caption="No"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Deal"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Description"]'));

        $confirmation.find('div[data-datafield="CopyRatesFromInventory"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyDates"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyLineItemNotes"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CombineSubs"] input').prop('checked', true);
        $confirmation.find('div[data-datafield="CopyDocuments"] input').prop('checked', true);

        const $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', makeACopy);

        function makeACopy() {
            const request: any = {};
            request.CopyToType = $confirmation.find('[data-type="radio"] input:checked').val();
            request.CopyToDealId = FwFormField.getValueByDataField($confirmation, 'CopyToDealId');
            request.CopyRatesFromInventory = FwFormField.getValueByDataField($confirmation, 'CopyRatesFromInventory');
            request.CopyDates = FwFormField.getValueByDataField($confirmation, 'CopyDates');
            request.CopyLineItemNotes = FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes');
            request.CombineSubs = FwFormField.getValueByDataField($confirmation, 'CombineSubs');
            request.CopyDocuments = FwFormField.getValueByDataField($confirmation, 'CopyDocuments');

            if (request.CopyRatesFromInventory == "T") {
                request.CopyRatesFromInventory = "False"
            };

            for (let key in request) {
                if (request.hasOwnProperty(key)) {
                    if (request[key] == "T") {
                        request[key] = "True";
                    } else if (request[key] == "F") {
                        request[key] = "False";
                    }
                }
            };

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Copying...');
            $yes.off('click');
            const $confirmationbox = jQuery('.fwconfirmationbox');
            const orderId = FwFormField.getValueByDataField($form, `${module}Id`);
            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/copyto${(request.CopyToType === "Q" ? "quote" : "order")}/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Copied`);
                FwConfirmation.destroyConfirmation($confirmation);
                let $control;
                const uniqueids: any = {};
                if (request.CopyToType == "O") {
                    uniqueids.OrderId = response.OrderId;
                    uniqueids.OrderTypeId = response.OrderTypeId;
                    $control = OrderController.loadForm(uniqueids);
                } else if (request.CopyToType == "Q") {
                    uniqueids.QuoteId = response.QuoteId;
                    uniqueids.OrderTypeId = response.OrderTypeId;
                    $control = QuoteController.loadForm(uniqueids);
                }
                FwModule.openModuleTab($control, "", true, 'FORM', true);
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
    printQuoteOrder($form: any) {
        try {
            var module = this.Module;
            var orderNumber = FwFormField.getValue($form, `div[data-datafield="${module}Number"]`);
            var orderId = FwFormField.getValue($form, `div[data-datafield="${module}Id"]`);
            var recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();

            var $report = (module === 'Order') ? OrderReportController.openForm() : QuoteReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValue($report, `div[data-datafield="${module}Id"]`, orderId, orderNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print ${module}`);

            var printTab = jQuery('.tab.submodule.active');
            printTab.find('.caption').html(`Print ${module}`);
            printTab.attr('data-caption', `${module} ${recordTitle}`);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //-----------------------------------------------------------------------------------------------------
    createOrder($form) {
        const status = FwFormField.getValueByDataField($form, 'Status');

        if ((status === 'ACTIVE') || (status === 'RESERVED')) {
            const quoteNumber = FwFormField.getValueByDataField($form, 'QuoteNumber');
            const $confirmation = FwConfirmation.renderConfirmation('Create Order', `<div>Create Order for Quote ${quoteNumber}?</div>`);
            const $yes = FwConfirmation.addButton($confirmation, 'Create Order', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', function () {
                const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);
                FwAppData.apiMethod(true, 'POST', `api/v1/quote/createorder/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwConfirmation.destroyConfirmation($confirmation);
                    const $quoteTab = jQuery(`#${$form.closest('.tabpage').attr('data-tabid')}`);
                    FwTabs.removeTab($quoteTab);
                    const uniqueids: any = {
                        OrderId: response.OrderId
                    };
                    const $orderform = OrderController.loadForm(uniqueids);
                    FwModule.openModuleTab($orderform, "", true, 'FORM', true);
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Created.');
                }, null, realConfirm);
            });
        } else {
            FwNotification.renderNotification('WARNING', 'Can only convert an "ACTIVE" or "RESERVED" Quote to an Order.');
        }
    }
}

//----------------------------------------------------------------------------------------------
//Copy Quote
//FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.CopyQuote.id] = function (event) {
//    try {
//        var $form           = jQuery(this).closest('.fwform');
//        const $confirmation = FwConfirmation.renderConfirmation('Copy Request', '');
//        const $yes          = FwConfirmation.addButton($confirmation, 'Copy', false);
//        const $no           = FwConfirmation.addButton($confirmation, 'Cancel');
//        const html          = [];
//        html.push('<div class="fwform" data-controller="none">');
//        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
//        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="RequestNo" data-enabled="false" style="width:90px;float:left;"></div>');
//        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="RequestDescription" data-enabled="false" style="width:340px;float:left;"></div>');
//        html.push('  </div>');
//        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
//        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="New Deal" data-datafield="CopyToDealId" data-browsedisplayfield="Deal" data-validationname="DealValidation"></div>');
//        html.push('  </div>');
//        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
//        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
//        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
//        html.push('  </div>');
//        html.push('</div>');
//        FwConfirmation.addControls($confirmation, html.join(''));

//        FwFormField.setValue($confirmation, 'div[data-datafield="RequestNo"]', FwFormField.getValueByDataField($form, 'QuoteNumber'));
//        FwFormField.setValue($confirmation, 'div[data-datafield="RequestDescription"]', FwFormField.getValueByDataField($form, 'Description'));
//        FwFormField.setValue($confirmation, 'div[data-datafield="CopyDates"]', true);
//        FwFormField.setValue($confirmation, 'div[data-datafield="CopyLineItemNotes"]', true);

//        $yes.on('click', function makeACopy () {
//            const request: any = {
//                CopyToType:             'Q',
//                CopyToDealId:           '',
//                CopyDates:              (FwFormField.getValueByDataField($confirmation, 'CopyDates') === 'T' ? 'True' : 'False'),
//                CopyLineItemNotes:      (FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes') === 'T' ? 'True' : 'False'),
//                CopyRatesFromInventory: 'False',
//                CombineSubs:            'False',
//                CopyDocuments:          'False'
//            };

//            FwFormField.disable($confirmation.find('.fwformfield'));
//            FwFormField.disable($yes);
//            $yes.text('Copying...');
//            $yes.off('click');
//            const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
//            FwAppData.apiMethod(true, 'POST', `api/v1/quote/copytoquote/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
//                FwNotification.renderNotification('SUCCESS', 'Request Successfully Copied');
//                FwConfirmation.destroyConfirmation($confirmation);
//                const uniqueids: any = {
//                    QuoteId:     response.QuoteId,
//                    OrderTypeId: response.OrderTypeId
//                };
//                let $control = QuoteController.loadForm(uniqueids);
//                FwModule.openModuleTab($control, "", true, 'FORM', true);
//            }, function onError(response) {
//                $yes.on('click', makeACopy);
//                $yes.text('Copy');
//                FwFunc.showError(response);
//                FwFormField.enable($confirmation.find('.fwformfield'));
//                FwFormField.enable($yes);
//            }, $confirmation);
//        });
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Search
//FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.Search.id] = function (event) {
//    let search, $form, quoteId;
//    $form = jQuery(this).closest('.fwform');
//    quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

//    if ($form.attr('data-mode') === 'NEW') {
//        QuoteController.saveForm($form, { closetab: false });
//        return;
//    }

//    if (quoteId == "") {
//        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//    } else {
//        search = new SearchInterface();
//        search.renderSearchPopup($form, quoteId, 'Request');
//    }
//};
//----------------------------------------------------------------------------------------------
////Print Quote
//FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.PrintQuote.id] = function (e) {
//    try {
//        var $form       = jQuery(this).closest('.fwform');
//        var quoteNumber = FwFormField.getValue($form, 'div[data-datafield="QuoteNumber"]');
//        var quoteId     = FwFormField.getValue($form, 'div[data-datafield="QuoteId"]');
//        var $report     = QuoteReportController.openForm();

//        FwModule.openSubModuleTab($form, $report);

//        FwFormField.setValue($report, 'div[data-datafield="QuoteId"]', quoteId, quoteNumber);
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//New Version
//FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.NewVersion.id] = function (event) {
//    let $form         = jQuery(this).closest('.fwform');
//    let quoteNumber   = FwFormField.getValueByDataField($form, 'QuoteNumber');
//    let quoteId       = FwFormField.getValueByDataField($form, 'QuoteId');
//    var $confirmation = FwConfirmation.renderConfirmation('Create New Version', `<div>Create New Version for Quote ${quoteNumber}?</div>`);
//    var $yes          = FwConfirmation.addButton($confirmation, 'Create New Version', false);
//    var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//    $yes.on('click', function () {
//        FwAppData.apiMethod(true, 'POST', `api/v1/quote/createnewversion/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
//            FwNotification.renderNotification('SUCCESS', 'New Version Successfully Created.');
//            FwConfirmation.destroyConfirmation($confirmation);
//            let uniqueids: any = {
//                QuoteId: response.QuoteId
//            };
//            var $quoteform = QuoteController.loadForm(uniqueids);
//            FwModule.openModuleTab($quoteform, "", true, 'FORM', true);

//            FwModule.refreshForm($form, QuoteController);
//        }, null, $confirmation);
//    });
//};
//----------------------------------------------------------------------------------------------
//Cancel / Uncancel - Form
//FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.CancelUncancel.id] = function (event) {
//    try {
//        let $confirmation, $yes, $no;
//        let $form       = jQuery(this).closest('.fwform');
//        var self        = this;
//        var id          = FwFormField.getValueByDataField($form, 'QuoteId');
//        var orderStatus = FwFormField.getValueByDataField($form, 'Status');

//        if (id != null) {
//            if (orderStatus === "CANCELLED") {
//                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to un-cancel this request?</div>');
//                $yes          = FwConfirmation.addButton($confirmation, 'Un-Cancel Request', false);
//                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//                $yes.on('click', function uncancelOrder() {
//                    FwFormField.disable($confirmation.find('.fwformfield'));
//                    FwFormField.disable($yes);
//                    $yes.text('Retrieving...');
//                    $yes.off('click');

//                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/uncancel/${id}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
//                        FwNotification.renderNotification('SUCCESS', 'Request Successfully Retrieved');
//                        FwConfirmation.destroyConfirmation($confirmation);
//                        FwModule.refreshForm($form);
//                    }, function onError(response) {
//                        $yes.on('click', uncancelOrder);
//                        $yes.text('Cancel');
//                        FwFunc.showError(response);
//                        FwFormField.enable($confirmation.find('.fwformfield'));
//                        FwFormField.enable($yes);
//                        FwModule.refreshForm($form);
//                    }, $form);
//                });
//            } else {
//                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to cancel this request?</div>');
//                $yes          = FwConfirmation.addButton($confirmation, 'Cancel Request', false);
//                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//                $yes.on('click', function cancelOrder() {
//                    FwFormField.disable($confirmation.find('.fwformfield'));
//                    FwFormField.disable($yes);
//                    $yes.text('Canceling...');
//                    $yes.off('click');

//                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${id}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
//                        FwNotification.renderNotification('SUCCESS', 'Request Successfully Cancelled');
//                        FwConfirmation.destroyConfirmation($confirmation);
//                        FwModule.refreshForm($form);
//                    }, function onError(response) {
//                        $yes.on('click', cancelOrder);
//                        $yes.text('Cancel');
//                        FwFunc.showError(response);
//                        FwFormField.enable($confirmation.find('.fwformfield'));
//                        FwFormField.enable($yes);
//                        FwModule.refreshForm($form);
//                    }, $form);
//                });
//            }
//        } else {
//            FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
//        }
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
//Create Order
//FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.form.menuItems.CreateOrder.id] = function (event) {
//    let $form  = jQuery(this).closest('.fwform');
//    let status = FwFormField.getValueByDataField($form, 'Status');
    
//    if (status === 'ACTIVE') {
//        let $quoteTab     = jQuery('#' + $form.closest('.tabpage').attr('data-tabid'));
//        let quoteNumber   = FwFormField.getValueByDataField($form, 'QuoteNumber');
//        var $confirmation = FwConfirmation.renderConfirmation('Create Order', `<div>Create Order for Request ${quoteNumber}?</div>`);
//        var $yes          = FwConfirmation.addButton($confirmation, 'Create Order');
//        var $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//        $yes.on('click', function () {
//            var quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
//            FwAppData.apiMethod(true, 'POST', `api/v1/quote/createorder/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
//                FwConfirmation.destroyConfirmation($confirmation);
//                FwTabs.removeTab($quoteTab);
//                let uniqueids: any = {
//                    OrderId: response.OrderId
//                };
//                var $orderform = OrderController.loadForm(uniqueids);
//                FwModule.openModuleTab($orderform, "", true, 'FORM', true);
//                FwNotification.renderNotification('SUCCESS', 'Order Successfully Created.');
//            }, null, $confirmation);
//        });
//    } else {
//        FwNotification.renderNotification('WARNING', 'Can only convert an "Active" request to an order!');
//    }
//};
//----------------------------------------------------------------------------------------------
////Cancel / Uncancel - Browse
//FwApplicationTree.clickEvents[Constants.Modules.Home.Quote.browse.menuItems.CancelUncancel.id] = function (event) {
//    try {
//        let $confirmation, $yes, $no;
//        var $browse     = jQuery(this).closest('.fwbrowse');
//        var quoteId     = $browse.find('.selected [data-browsedatafield="QuoteId"]').attr('data-originalvalue');
//        var quoteStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');

//        if (quoteId != null) {
//            if (quoteStatus === "CANCELLED") {
//                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to un-cancel this Quote?</div>');
//                $yes          = FwConfirmation.addButton($confirmation, 'Un-Cancel Quote', false);
//                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//                $yes.on('click', function uncancelQuote() {
//                    FwFormField.disable($confirmation.find('.fwformfield'));
//                    FwFormField.disable($yes);
//                    $yes.text('Retrieving...');
//                    $yes.off('click');

//                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/uncancel/${quoteId}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
//                        FwNotification.renderNotification('SUCCESS', 'Quote Successfully Retrieved');
//                        FwConfirmation.destroyConfirmation($confirmation);
//                        FwBrowse.databind($browse);
//                    }, function onError(response) {
//                        $yes.on('click', uncancelQuote);
//                        $yes.text('Cancel');
//                        FwFunc.showError(response);
//                        FwFormField.enable($confirmation.find('.fwformfield'));
//                        FwFormField.enable($yes);
//                        FwBrowse.databind($browse);
//                    }, $browse);
//                });
//            } else {
//                $confirmation = FwConfirmation.renderConfirmation('Cancel', '<div>Would you like to cancel this Quote?</div>');
//                $yes          = FwConfirmation.addButton($confirmation, 'Cancel Quote', false);
//                $no           = FwConfirmation.addButton($confirmation, 'Cancel');

//                $yes.on('click', function cancelQuote() {
//                    FwFormField.disable($confirmation.find('.fwformfield'));
//                    FwFormField.disable($yes);
//                    $yes.text('Canceling...');
//                    $yes.off('click');

//                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${quoteId}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
//                        FwNotification.renderNotification('SUCCESS', 'Quote Successfully Cancelled');
//                        FwConfirmation.destroyConfirmation($confirmation);
//                        FwBrowse.databind($browse);
//                    }, function onError(response) {
//                        $yes.on('click', cancelQuote);
//                        $yes.text('Cancel');
//                        FwFunc.showError(response);
//                        FwFormField.enable($confirmation.find('.fwformfield'));
//                        FwFormField.enable($yes);
//                        FwBrowse.databind($browse);
//                    }, $browse);
//                });
//            }
//        } else {
//            FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
//        }
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------

var QuoteController = new Quote();