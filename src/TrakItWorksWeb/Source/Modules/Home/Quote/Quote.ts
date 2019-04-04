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
        const location      = JSON.parse(sessionStorage.getItem('location'));
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
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            var today = FwFunc.getDate();
            var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            var office = JSON.parse(sessionStorage.getItem('location'));
            var department = JSON.parse(sessionStorage.getItem('department'));

            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            FwFormField.setValueByDataField($form, 'VersionNumber', 1);

            $form.find('div[data-datafield="DealId"]').attr('data-required', 'false');
            $form.find('div[data-datafield="PickTime"]').attr('data-required', 'false');

            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            $form.find('div[data-datafield="Rental"] input').prop('checked', true);

            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);
        }

        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', 'false');
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', 'false');

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
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

        var $orderItemGridRental        = $form.find('div[data-grid="OrderItemGrid"]');
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
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        super.afterLoad($form);
        let pending    = $form.find('div.fwformfield[data-datafield="PendingPo"] input').prop('checked');
        let status      = FwFormField.getValueByDataField($form, 'Status');
        let hasNotes    = FwFormField.getValueByDataField($form, 'HasNotes');
        let rentalTab   = $form.find('[data-type="tab"][data-caption="Rental"]');

        if ($form.find('[data-datafield="CombineActivity"] input').val() === 'false') {
            if (!FwFormField.getValueByDataField($form, 'Rental')) { rentalTab.hide(), FwFormField.disable($form.find('[data-datafield="RentalSale"]')); }
        }

        if (status === 'ORDERED' || status === 'CLOSED' || status === 'CANCELLED') {
            FwModule.setFormReadOnly($form);
        }

        if (hasNotes) {
            FwTabs.setTabColor($form.find('.notestab'), '#FFFF00');
        }

        var $orderStatusHistoryGrid = $form.find('[data-name="OrderStatusHistoryGrid"]');
        var $orderItemGridRental    = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        var $orderNoteGrid          = $form.find('[data-name="OrderNoteGrid"]');

        //hide subworksheet and add LD items
        $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"]').hide();

        //if (FwFormField.getValueByDataField($form, 'HasRentalItem')) {
        //    FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        //}
    }
    //----------------------------------------------------------------------------------------------
}

var QuoteController = new Quote();