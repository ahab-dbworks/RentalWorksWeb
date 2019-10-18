//----------------------------------------------------------------------------------------------
class OrderBase {
    DefaultOrderType: string;
    DefaultOrderTypeId: string;
    DefaultTermsConditionsId: string;
    DefaultTermsConditions: string;
    DefaultCoverLetterId: string;
    DefaultCoverLetter: string;
    CombineActivity: string;
    Module: string;
    apiurl: string;
    CachedOrderTypes: any = {};
    totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;

    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string { return ``; }
    getFormTemplate(): string { return ``; }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        // ----------
        const $orderStatusHistoryGrid = $form.find('div[data-grid="OrderStatusHistoryGrid"]');
        const $orderStatusHistoryGridControl = FwBrowse.loadGridFromTemplate('OrderStatusHistoryGrid');
        $orderStatusHistoryGrid.empty().append($orderStatusHistoryGridControl);
        $orderStatusHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
            };
        });
        FwBrowse.init($orderStatusHistoryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusHistoryGridControl);
        // ----------
        const $orderNoteGrid = $form.find('div[data-grid="OrderNoteGrid"]');
        const $orderNoteGridControl = FwBrowse.loadGridFromTemplate('OrderNoteGrid');
        $orderNoteGrid.empty().append($orderNoteGridControl);
        $orderNoteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
            };
        });
        $orderNoteGridControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`)
        });
        FwBrowse.init($orderNoteGridControl);
        FwBrowse.renderRuntimeHtml($orderNoteGridControl);
        // ----------
        const $orderContactGrid = $form.find('div[data-grid="OrderContactGrid"]');
        const $orderContactGridControl = FwBrowse.loadGridFromTemplate('OrderContactGrid');
        $orderContactGrid.empty().append($orderContactGridControl);
        $orderContactGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
            };
        });
        $orderContactGridControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($orderContactGridControl);
        FwBrowse.renderRuntimeHtml($orderContactGridControl);
        // ----------
        const $orderItemGridRental = $form.find('.rentalgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridRentalControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridRental.empty().append($orderItemGridRentalControl);
        $orderItemGridRental.addClass('R');

        $orderItemGridRentalControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                RecType: 'R'
            };
            request.totalfields = this.totalFields;
        });
        $orderItemGridRentalControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            request.RecType = 'R';
        }
        );
        FwBrowse.addEventHandler($orderItemGridRentalControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'rental', dt.Totals);

            let rentalItems = $form.find('.rentalgrid tbody').children();
            rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
        });

        FwBrowse.init($orderItemGridRentalControl);
        FwBrowse.renderRuntimeHtml($orderItemGridRentalControl);
        // ----------
        const $orderItemGridSales = $form.find('.salesgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridSalesControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridSalesControl.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
        $orderItemGridSalesControl.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
        $orderItemGridSales.empty().append($orderItemGridSalesControl);
        $orderItemGridSales.addClass('S');

        $orderItemGridSalesControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                RecType: 'S'
            };
            request.totalfields = this.totalFields;
        });
        $orderItemGridSalesControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            request.RecType = 'S';
        });
        FwBrowse.addEventHandler($orderItemGridSalesControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'sales', dt.Totals);

            let salesItems = $form.find('.salesgrid tbody').children();
            salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
        });

        FwBrowse.init($orderItemGridSalesControl);
        FwBrowse.renderRuntimeHtml($orderItemGridSalesControl);
        // ----------
        const $orderItemGridLabor = $form.find('.laborgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridLaborControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridLaborControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        $orderItemGridLabor.empty().append($orderItemGridLaborControl);
        $orderItemGridLabor.addClass('L');

        $orderItemGridLaborControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                RecType: 'L'
            };
            request.totalfields = this.totalFields;
        });
        $orderItemGridLaborControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            request.RecType = 'L';
        });
        FwBrowse.addEventHandler($orderItemGridLaborControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'labor', dt.Totals);

            let laborItems = $form.find('.laborgrid tbody').children();
            laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
        });

        FwBrowse.init($orderItemGridLaborControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLaborControl);
        // ----------
        const $orderItemGridMisc = $form.find('.miscgrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridMiscControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridMiscControl.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
        $orderItemGridMisc.empty().append($orderItemGridMiscControl);
        $orderItemGridMisc.addClass('M');

        $orderItemGridMiscControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                RecType: 'M'
            };
            request.totalfields = this.totalFields;
        });
        $orderItemGridMiscControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            request.RecType = 'M';
        }
        );

        FwBrowse.addEventHandler($orderItemGridMiscControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'misc', dt.Totals);

            let miscItems = $form.find('.miscgrid tbody').children();
            miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
        });

        FwBrowse.init($orderItemGridMiscControl);
        FwBrowse.renderRuntimeHtml($orderItemGridMiscControl);
        // ----------
        const $orderItemGridUsedSale = $form.find('.usedsalegrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridUsedSaleControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridUsedSale.empty().append($orderItemGridUsedSaleControl);
        $orderItemGridUsedSale.addClass('RS');

        $orderItemGridUsedSaleControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                RecType: 'RS'
            };
            request.totalfields = this.totalFields;
        });
        $orderItemGridUsedSaleControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            request.RecType = 'RS';
        });
        FwBrowse.addEventHandler($orderItemGridUsedSaleControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'usedsale', dt.Totals);
        });
        FwBrowse.init($orderItemGridUsedSaleControl);
        FwBrowse.renderRuntimeHtml($orderItemGridUsedSaleControl);
        // ----------
        const $combinedOrderItemGrid = $form.find('.combinedgrid div[data-grid="OrderItemGrid"]');
        const $combinedOrderItemGridControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $combinedOrderItemGridControl.find('.combined').attr('data-visible', 'true');
        $combinedOrderItemGridControl.find('.individual').attr('data-visible', 'false');
        $combinedOrderItemGrid.empty().append($combinedOrderItemGridControl);
        $combinedOrderItemGrid.addClass('A');

        $combinedOrderItemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
            };
            request.totalfields = this.totalFields;
        });
        $combinedOrderItemGridControl.data('beforesave', request => {
            request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
        }
        );
        FwBrowse.addEventHandler($combinedOrderItemGridControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'combined', dt.Totals);
        });

        FwBrowse.init($combinedOrderItemGridControl);
        FwBrowse.renderRuntimeHtml($combinedOrderItemGridControl);
        // ----------

        const itemGrids = [$orderItemGridRental, $orderItemGridSales, $orderItemGridLabor, $orderItemGridMisc];
        if ($form.attr('data-mode') === 'NEW') {
            for (let i = 0; i < itemGrids.length; i++) {
                itemGrids[i].find('.btn').filter(function () { return jQuery(this).data('type') === 'NewButton' })
                    .off()
                    .on('click', () => {
                        this.saveForm($form, { closetab: false });
                    })
            }
        }

        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.usedsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');

        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');

        if (this.Module === 'Quote') {
            //hide subworksheet and LD menu items
            $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $orderItemGridSales.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $orderItemGridLabor.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $orderItemGridMisc.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $orderItemGridUsedSale.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $combinedOrderItemGrid.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
        }
        else if (this.Module === 'Order') {
            //hide LD menu items
            $orderItemGridRental.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $orderItemGridSales.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $orderItemGridLabor.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $orderItemGridMisc.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $orderItemGridUsedSale.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            $combinedOrderItemGrid.find('.submenu-btn').filter('[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]').hide();
            //Hides non-LD menu items
            const $lossDamageGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            $lossDamageGrid.find('.submenu-btn').filter('[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="AD3FB369-5A40-4984-8A65-46E683851E52"], [data-securityid="B6B68464-B95C-4A4C-BAF2-6AA59B871468"], [data-securityid="01EB96CB-6C62-4D5C-9224-8B6F45AD9F63"], [data-securityid="9476D532-5274-429C-A563-FE89F5B89B01"]').hide();
        }
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                $tr.css('color', '#aaaaaa');
            }
        });

        const chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
        if (!chartFilters) {
            $browse.data('ondatabind', request => {
                request.activeviewfields = this.ActiveViewFields;
            });
        } else {
            $browse.data('ondatabind', request => {
                request.activeviewfields = '';
            });
        }

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        const department = JSON.parse(sessionStorage.getItem('department'));;
        const location = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', `api/v1/departmentlocation/${department.departmentid}~${location.locationid}`, null, FwServices.defaultTimeout, response => {
            this.DefaultOrderType = response.DefaultOrderType;
            this.DefaultOrderTypeId = response.DefaultOrderTypeId;

            const request: any = {};
            request.uniqueids = {
                OrderTypeId: this.DefaultOrderTypeId,
                LocationId: location.locationid
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/ordertypelocation/browse`, request, FwServices.defaultTimeout,
                response => {
                    if (response.Rows.length > 0) {
                        this.DefaultTermsConditionsId = response.Rows[0][response.ColumnIndex.TermsConditionsId];
                        this.DefaultTermsConditions = response.Rows[0][response.ColumnIndex.TermsConditions];
                        this.DefaultCoverLetterId = response.Rows[0][response.ColumnIndex.CoverLetterId];
                        this.DefaultCoverLetter = response.Rows[0][response.ColumnIndex.CoverLetter];
                    }
                }, null, null);
        }, null, null);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.combinedtab').hide();

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            const today = FwFunc.getDate();
            const department = JSON.parse(sessionStorage.getItem('department'));
            const office = JSON.parse(sessionStorage.getItem('location'));
            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));

            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
            FwFormField.setValueByDataField($form, 'BillingMonths', '0');
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', controlDefaults.defaultdealbillingcycleid, controlDefaults.defaultdealbillingcycle);
            FwFormField.setValue($form, 'div[data-datafield="TermsConditionsId"]', this.DefaultTermsConditionsId, this.DefaultTermsConditions);
            FwFormField.setValue($form, 'div[data-datafield="CoverLetterId"]', this.DefaultCoverLetterId, this.DefaultCoverLetter);

            FwFormField.setValue($form, 'div[data-datafield="PendingPo"]', true);
            // Dynamic set value for user's dpt default activities
            const defaultActivities = department.activities;
            for (let i = 0; i < defaultActivities.length; i++) {
                if (defaultActivities[i] === 'Rental' || defaultActivities[i] === 'Sales' || defaultActivities[i] === 'Labor' || defaultActivities[i] === 'Miscellaneous')
                    FwFormField.setValueByDataField($form, `${defaultActivities[i]}`, true);
            }

            // show/hide tabs based on Activity boxes checked
            const $rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]');
            $form.find('[data-datafield="Rental"] input').prop('checked') ? $rentalTab.show() : $rentalTab.hide();
            const $salesTab = $form.find('[data-type="tab"][data-caption="Sales"]');
            $form.find('[data-datafield="Sales"] input').prop('checked') ? $salesTab.show() : $salesTab.hide();
            const $miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]');
            $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? $miscTab.show() : $miscTab.hide();
            const $laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
            $form.find('[data-datafield="Labor"] input').prop('checked') ? $laborTab.show() : $laborTab.hide();

            FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
            $form.find('[data-type="tab"][data-caption="Used Sale"]').hide();
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));

            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();

            FwFormField.setValueByDataField($form, 'RateType', office.ratetype, office.ratetype);
        }

        let $emailHistorySubModuleBrowse = this.openEmailHistoryBrowse($form);
        $form.find('.emailhistory-page').append($emailHistorySubModuleBrowse);

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

        //Toggle Buttons - Profit Loss tab
        FwFormField.loadItems($form.find('div[data-datafield="totalTypeProfitLoss"]'), [
            { value: 'W', caption: 'Weekly' },
            { value: 'M', caption: 'Monthly' },
            { value: 'P', caption: 'Period', checked: 'checked' }
        ]);

        //Toggle Buttons - Rental tab - Rental totals
        FwFormField.loadItems($form.find('div[data-datafield="totalTypeRental"]'), [
            { value: 'W', caption: 'Weekly' },
            { value: 'M', caption: 'Monthly' },
            { value: 'P', caption: 'Period', checked: 'checked' }
        ]);

        //Toggle Buttons - Misc. tab - Misc. totals
        FwFormField.loadItems($form.find('div[data-datafield="totalTypeMisc"]'), [
            { value: 'W', caption: 'Weekly' },
            { value: 'M', caption: 'Monthly' },
            { value: 'P', caption: 'Period', checked: 'checked' }
        ]);

        //Toggle Buttons - Labor tab - Labor totals
        FwFormField.loadItems($form.find('div[data-datafield="totalTypeLabor"]'), [
            { value: 'W', caption: 'Weekly' },
            { value: 'M', caption: 'Monthly' },
            { value: 'P', caption: 'Period', checked: 'checked' }
        ]);

        // Show/Hide available view buttons based on rate type
        if (mode === 'NEW') {
            const rateType = FwFormField.getValueByDataField($form, 'RateType');
            if (rateType === 'MONTHLY') {
                $form.find('.togglebutton-item input[value="W"]').parent().hide();
                $form.find('.togglebutton-item input[value="M"]').parent().show();
            } else {
                $form.find('.togglebutton-item input[value="W"]').parent().show();
                $form.find('.togglebutton-item input[value="M"]').parent().hide();
            }
        }

        //Toggle Buttons - Billing tab - Issue To Address
        FwFormField.loadItems($form.find('div[data-datafield="PrintIssuedToAddressFrom"]'), [
            { value: 'DEAL', caption: 'Deal' },
            { value: 'CUSTOMER', caption: 'Customer' },
            { value: 'ORDER', caption: 'Order' }
        ]);

        //Toggle Buttons - Billing tab - Bill Quantities From 
        FwFormField.loadItems($form.find('div[data-datafield="DetermineQuantitiesToBillBasedOn"]'), [
            { value: 'CONTRACT', caption: 'Contract Activity' },
            { value: 'ORDER', caption: 'Order Quantity' }
        ]);

        //Toggle Buttons - Billing tab - Labor Prep Fees
        FwFormField.loadItems($form.find('div[data-datafield="IncludePrepFeesInRentalRate"]'), [
            { value: 'false', caption: 'As Labor Charge' },
            { value: 'true', caption: 'Into Rental Rate' }
        ]);

        //Toggle Buttons - Billing tab - Hiatus Schedule
        FwFormField.loadItems($form.find('div[data-datafield="HiatusDiscountFrom"]'), [
            { value: 'DEAL', caption: 'Deal' },
            { value: 'ORDER', caption: 'Order' }
        ]);

        //Toggle Buttons - Deliver/Ship tab - Outgoing Address
        FwFormField.loadItems($form.find('div[data-datafield="OutDeliveryAddressType"]'), [
            { value: 'DEAL', caption: 'Deal' },
            { value: 'VENUE', caption: 'Venue' },
            { value: 'WAREHOUSE', caption: 'Warehouse' },
            { value: 'OTHER', caption: 'Other' }
        ]);

        //Toggle Buttons - Deliver/Ship tab - Incoming Address
        FwFormField.loadItems($form.find('div[data-datafield="InDeliveryAddressType"]'), [
            { value: 'DEAL', caption: 'Deal' },
            { value: 'VENUE', caption: 'Venue' },
            { value: 'WAREHOUSE', caption: 'Warehouse' },
            { value: 'OTHER', caption: 'Other' }
        ]);
        //Toggle Buttons - Manifest tab - Rental Valuation
        FwFormField.loadItems($form.find('div[data-datafield="rentalValueSelector"]'), [
            { value: '', caption: 'Default Value' },
            { value: '', caption: 'Replacement Cost' }
        ]);
        //Toggle Buttons - Manifest tab - Sales Valuation
        FwFormField.loadItems($form.find('div[data-datafield="salesValueSelector"]'), [
            { value: '', caption: 'Sell Price' },
            { value: '', caption: 'Default Cost' },
            { value: '', caption: 'Average Cost' }
        ]);
        //Toggle Buttons - Manifest tab - Weight Type
        FwFormField.loadItems($form.find('div[data-datafield="weightSelector"]'), [
            { value: 'IMPERIAL', caption: 'Imperial' },
            { value: 'METRIC', caption: 'Metric' }
        ]);

        this.events($form);
        this.activityCheckboxEvents($form, mode);
        this.renderPrintButton($form);
        this.renderSearchButton($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    openEmailHistoryBrowse($form) {
        const $browse = EmailHistoryController.openBrowse();
        $browse.data('ondatabind', request => {
            request.uniqueids = {
                RelatedToId: $form.find(`[data-datafield="${this.Module}Id"] input.fwformfield-value`).val()
            }
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    renderPrintButton($form: any) {
        var self = this;
        var $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', function () {
            self.printQuoteOrder($form);
        });
    }
    //----------------------------------------------------------------------------------------------
    renderSearchButton($form: any) {
        var self = this;
        var $search = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'QuikSearch', 'searchbtn');
        $search.prepend('<i class="material-icons">search</i>');
        $search.on('click', function () {
            try {
                let $form = jQuery(this).closest('.fwform');
                let orderId = FwFormField.getValueByDataField($form, `${self.Module}Id`);

                if (orderId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else if (!jQuery(this).hasClass('disabled')) {
                    let search = new SearchInterface();
                    search.renderSearchPopup($form, orderId, self.Module);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderFrames($form: any, cachedId?, period?) {
        FwFormField.disable($form.find('.frame'));
        let id = FwFormField.getValueByDataField($form, `${this.Module}Id`);
        $form.find('.frame input').css('width', '100%');
        if (typeof cachedId !== 'undefined' && cachedId !== null) {
            id = cachedId;
        }
        if (id !== '') {
            if (typeof period !== 'undefined') {
                id = `${id}~${period}`
            }
            FwAppData.apiMethod(true, 'GET', `api/v1/ordersummary/${id}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    if (response.hasOwnProperty(key)) {
                        $form.find(`[data-framedatafield="${key}"] input`).val(response[key]);
                        $form.find(`[data-framedatafield="${key}"]`).attr('data-originalvalue', response[key]);
                    }
                }

                const $profitFrames = $form.find('.profitframes .frame');
                $profitFrames.each(function () {
                    var profit = parseFloat(jQuery(this).attr('data-originalvalue'));
                    if (profit > 0) {
                        jQuery(this).find('input').css('background-color', '#A6D785');
                    } else if (profit < 0) {
                        jQuery(this).find('input').css('background-color', '#ff9999');
                    }
                });

                const $totalFrames = $form.find('.totalColors input');
                $totalFrames.each(function () {
                    var total = jQuery(this).val();
                    if (total != 0) {
                        jQuery(this).css('background-color', '#ffffe5');
                    }
                })

                //const totalTaxVal = parseFloat(FwFormField.getValue2($form.find('[data-framedatafield="TotalTax"]')));
                //totalTaxVal == 0 ? $form.find('.salestax-pl').hide() : $form.find('.salestax-pl').show();

            }, null, $form);
            $form.find(".frame .add-on").children().hide();
        }
    };
    //----------------------------------------------------------------------------------------------
    activityCheckboxEvents($form: any, mode: string) {
        let rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
            , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
            , miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]')
            , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]')
            , lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss and Damage"]')
            , usedSaleTab = $form.find('[data-type="tab"][data-caption="Used Sale"]');

        $form.find('[data-datafield="Rental"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    rentalTab.show();
                    FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                } else {
                    rentalTab.hide();
                    FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        rentalTab.show();
                        FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                    } else {
                        rentalTab.hide();
                        FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                    }
                }
            }
        });
        $form.find('[data-datafield="Sales"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    salesTab.show();
                } else {
                    salesTab.hide();
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        salesTab.show();
                    } else {
                        salesTab.hide();
                    }
                }
            }
        });
        $form.find('[data-datafield="Miscellaneous"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    miscTab.show();
                } else {
                    miscTab.hide();
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        miscTab.show();
                    } else {
                        miscTab.hide();
                    }
                }
            }
        });

        const quikSearchMenuId = Constants.Modules.Home.Order.form.menuItems.Search.id.replace('{', '').replace('}', '');
        $form.find('[data-datafield="LossAndDamage"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                lossDamageTab.show();
                $form.find('[data-securityid="searchbtn"]').addClass('disabled');
                $form.find(`.submenu-btn[data-securityid="${quikSearchMenuId}"]`).attr('data-enabled', 'false');
                FwFormField.disable($form.find('[data-datafield="Rental"]'));
                FwFormField.disable($form.find('[data-datafield="Sales"]'));
                FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
            } else {
                lossDamageTab.hide();
                console.log('in change b4: ', $form.data('antiLD'))
                $form.find('[data-securityid="searchbtn"]').removeClass('disabled');
                $form.find(`.submenu-btn[data-securityid="${quikSearchMenuId}"]`).attr('data-enabled', 'true');
                //if ()
                FwFormField.enable($form.find('[data-datafield="Rental"]'));
                FwFormField.enable($form.find('[data-datafield="Sales"]'));
                FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                $form.data('antiLD', null);
                console.log('inchange after null: ', $form.data('antiLD'))
            }
        });
        // Determine previous values for enabled / disabled checkboxes
        $form.find('[data-datafield="LossAndDamage"]').click(e => {
            // e.stopImmediatePropagation()
            let LossAndDamageVal = FwFormField.getValueByDataField($form, 'LossAndDamage')
            console.log('losdamageval', LossAndDamageVal)
            if (LossAndDamageVal === false) {
                let salesEnabled = $form.find('[data-datafield="Sales"]').attr('data-enabled');
                let rentalEnabled = $form.find('[data-datafield="Rental"]').attr('data-enabled');
                let rentalSalesEnabled = $form.find('[data-datafield="RentalSale"]').attr('data-enabled');
                $form.data('antiLD', {
                    "salesEnabled": salesEnabled,
                    "rentalEnabled": rentalEnabled,
                    "rentalSalesEnabled": rentalSalesEnabled
                });
            }
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    laborTab.show();
                } else {
                    laborTab.hide();
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        laborTab.show();
                    } else {
                        laborTab.hide();
                    }
                }
            }
        });

        $form.find('[data-datafield="RentalSale"] input').on('change', e => {
            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    usedSaleTab.show();
                    FwFormField.disable($form.find('[data-datafield="Rental"]'));
                } else {
                    usedSaleTab.hide();
                    FwFormField.enable($form.find('[data-datafield="Rental"]'));
                }
            } else {
                let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
                if (combineActivity == 'false') {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        usedSaleTab.show();
                        FwFormField.disable($form.find('[data-datafield="Rental"]'));
                    } else {
                        usedSaleTab.hide();
                        FwFormField.enable($form.find('[data-datafield="Rental"]'));
                    }
                }
            }
        });
        // Loss and Damage disable against Rental, Sales, Used Sale
        // Also in AfterLoad
        $form.find('.anti-LD').on('change', e => {
            let rentalVal = FwFormField.getValueByDataField($form, 'Rental');
            let salesVal = FwFormField.getValueByDataField($form, 'Sales');
            let usedSaleVal = FwFormField.getValueByDataField($form, 'RentalSale');
            if (rentalVal === true || salesVal === true || usedSaleVal === true) {
                FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
            } else if (rentalVal === false && salesVal === false && usedSaleVal === false) {
                FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    copyOrderOrQuote($form: any) {
        const module = this.Module;
        const $confirmation = FwConfirmation.renderConfirmation(`Copy ${module}`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        const html = [];
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
        html.push('    <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Copy To" data-datafield="CopyTo">');
        html.push('      <div data-value="Q" data-caption="Quote"> </div>');
        html.push('    <div data-value="O" data-caption="Order"> </div></div><br>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Rates & Prices" data-datafield="CopyRatesFromInventory"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Dates" data-datafield="CopyDates"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Line Item Notes" data-datafield="CopyLineItemNotes"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Combine Subs" data-datafield="CombineSubs"></div>');
        html.push('    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Copy Documents" data-datafield="CopyDocuments"></div>');
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
    beforeValidateOutShipVia($browse: any, $grid: any, request: any) {
        let validationName = request.module,
            outDeliveryCarrierId = jQuery($grid.find('[data-datafield="OutDeliveryCarrierId"] input')).val();
        switch (validationName) {
            case 'ShipViaValidation':
                request.uniqueids = {
                    VendorId: outDeliveryCarrierId
                };
                break;
        }
    };
    beforeValidateInShipVia($browse: any, $grid: any, request: any) {
        let validationName = request.module;
        let inDeliveryCarrierId = jQuery($grid.find('[data-datafield="InDeliveryCarrierId"] input')).val();
        switch (validationName) {
            case 'ShipViaValidation':
                request.uniqueids = {
                    VendorId: inDeliveryCarrierId
                };
                break;
        }
    };
    beforeValidateCarrier($browse: any, $grid: any, request: any) {
        let validationName = request.module;
        switch (validationName) {
            case 'VendorValidation':
                request.uniqueids = {
                    Freight: true
                };
                break;
        }
    };
    beforeValidateDeal($browse: any, $grid: any, request: any) {
        const $form = $grid.closest('.fwform');
        const shareDealsAcrossOfficeLocations = JSON.parse(sessionStorage.getItem('controldefaults')).sharedealsacrossofficelocations;
        if (!shareDealsAcrossOfficeLocations) {
            const officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
            request.uniqueids = {
                LocationId: officeLocationId
            }
        }
    };
    beforeValidateMarketSegment($browse: any, $grid: any, request: any) {
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
    beforeValidateWarehouse($browse: any, $form: any, request: any) {
        request.uniqueids = {};
        const locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');

        if (locationId) {
            request.uniqueids.LocationId = locationId;
        }
    };
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        //let weeklyType = $form.find(".weeklyType");
        //let monthlyType = $form.find(".monthlyType");
        //let rentalDaysPerWeek = $form.find(".RentalDaysPerWeek");
        //let billingMonths = $form.find(".BillingMonths");
        //let billingWeeks = $form.find(".BillingWeeks");

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
        // RentalDaysPerWeek for Rental OrderItemGrid
        $form.find('.RentalDaysPerWeek').on('change', '.fwformfield-text, .fwformfield-value', event => {
            let request: any = {},
                $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
                module = this.Module,
                orderId = FwFormField.getValueByDataField($form, `${module}Id`),
                daysperweek = FwFormField.getValueByDataField($form, 'RentalDaysPerWeek');

            request.DaysPerWeek = parseFloat(daysperweek);
            request.RecType = 'R';
            request.OrderId = orderId;

            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/applybottomlinedaysperweek/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($orderItemGridRental);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
        // ----------
        $form.find('div[data-datafield="RateType"]').on('change', event => {
            this.applyOrderTypeAndRateTypeToForm($form);
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
        // ----------
        $form.find('[data-datafield="BillToAddressDifferentFromIssuedToAddress"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.differentaddress'));
            }
            else {
                FwFormField.disable($form.find('.differentaddress'));
            }
        });
        // ----------
        $form.find('div[data-datafield="OrderTypeId"]').on('change', event => {
            this.renderGrids($form);
            this.applyOrderTypeAndRateTypeToForm($form);
        });
        // ----------
        $form.find('[data-datafield="NoCharge"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
            }
        });
        // ----------
        $form.find('div[data-datafield="OrderTypeId"]').data('onchange', e => {
            this.applyOrderTypeDefaults($form);
        });
        // ----------
        $form.find('div[data-datafield="DepartmentId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingRentalRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingRentalRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingSalesRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingSalesRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingLaborRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingLaborRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingMiscellaneousRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingMiscellaneousRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingUsedSaleRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingUsedSaleRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingLossAndDamageRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingLossAndDamageRate"]').attr('data-originalvalue')));
            if ($form.attr('data-mode') === 'NEW') {
                const defaultActivities: any = {};
                defaultActivities['Rental'] = $tr.find('.field[data-browsedatafield="DefaultActivityRental"]').attr('data-originalvalue');
                defaultActivities['Sales'] = $tr.find('.field[data-browsedatafield="DefaultActivitySales"]').attr('data-originalvalue');
                defaultActivities['Labor'] = $tr.find('.field[data-browsedatafield="DefaultActivityLabor"]').attr('data-originalvalue');
                defaultActivities['Miscellaneous'] = $tr.find('.field[data-browsedatafield="DefaultActivityMiscellaneous"]').attr('data-originalvalue');

                for (let key in defaultActivities) {
                    FwFormField.setValueByDataField($form, `${key}`, defaultActivities[key] === 'true');
                }
            }
        });
        // ----------
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
        // ----------
        $form.find('.allFrames').css('display', 'none');
        $form.find('.hideFrames').css('display', 'none');
        $form.find('.expandArrow').on('click', e => {
            //$form.find('.hideFrames').toggle();
            //$form.find('.expandFrames').toggle();
            $form.find('.allFrames').toggle();
            $form.find('.totalRowFrames').toggle();
            //if ($form.find('.summarySection').css('flex') != '0 1 65%') {
            //    $form.find('.summarySection').css('flex', '0 1 65%');
            //} else {
            //    $form.find('.summarySection').css('flex', '');
            //}
            const $this = jQuery(e.currentTarget);
            const isExpanded = $this.hasClass('expandFrames');
            const $summarySection = $this.parents('.toggle-totals-buttons').siblings('.summarySection');
            if (isExpanded) {
                $summarySection.show();
            } else {
                $summarySection.hide();
            }
            $this.toggle();
            $this.siblings('.expandArrow').toggle();
        });
        $form.find(".weeklyType").show();
        $form.find(".monthlyType").hide();
        $form.find(".periodType input").prop('checked', true);

        //Defaults Address information when user selects a deal
        $form.find('[data-datafield="DealId"]').data('onchange', $tr => {
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            const type = $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'RateType', type);
            $form.find('div[data-datafield="RateType"] input.fwformfield-text').val(type);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', $tr.find('.field[data-browsedatafield="PaymentTermsId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentTerms"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTypeId"]', $tr.find('.field[data-browsedatafield="PaymentTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Currency"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', $tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));

            FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealId}`, null, FwServices.defaultTimeout, response => {
                FwFormField.setValueByDataField($form, 'CustomerId', response.CustomerId, response.Customer); // hidden field needed for other operations
                FwFormField.setValueByDataField($form, 'IssuedToAttention', response.BillToAttention1);
                FwFormField.setValueByDataField($form, 'IssuedToAttention2', response.BillToAttention2);
                FwFormField.setValueByDataField($form, 'IssuedToAddress1', response.BillToAddress1);
                FwFormField.setValueByDataField($form, 'IssuedToAddress2', response.BillToAddress2);
                FwFormField.setValueByDataField($form, 'IssuedToCity', response.BillToCity);
                FwFormField.setValueByDataField($form, 'IssuedToState', response.BillToStateId, response.BillToState);
                FwFormField.setValueByDataField($form, 'IssuedToZipCode', response.BillToZipCode);
                FwFormField.setValueByDataField($form, 'IssuedToCountryId', response.BillToCountryId, response.BillToCountry);
                FwFormField.setValueByDataField($form, 'PrintIssuedToAddressFrom', response.BillToAddressType);

                if ($form.attr('data-mode') === 'NEW') {
                    FwFormField.setValueByDataField($form, 'OutDeliveryDeliveryType', response.DefaultOutgoingDeliveryType);
                    FwFormField.setValueByDataField($form, 'InDeliveryDeliveryType', response.DefaultIncomingDeliveryType);
                    if (response.DefaultOutgoingDeliveryType === 'DELIVER' || response.DefaultOutgoingDeliveryType === 'SHIP') {
                        FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'DEAL');
                        this.fillDeliveryAddressFieldsforDeal($form, 'Out', response);
                    }
                    else if (response.DefaultOutgoingDeliveryType === 'PICK UP') {
                        FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'WAREHOUSE');
                        this.getWarehouseAddress($form, 'Out');
                    }

                    if (response.DefaultIncomingDeliveryType === 'DELIVER' || response.DefaultIncomingDeliveryType === 'SHIP') {
                        FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'WAREHOUSE');
                        this.getWarehouseAddress($form, 'In');
                    }
                    else if (response.DefaultIncomingDeliveryType === 'PICK UP') {
                        FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'DEAL');
                        this.fillDeliveryAddressFieldsforDeal($form, 'In', response);
                    }
                }
            }, null, null);
        });
        // Out / In DeliveryType radio in Deliver tab
        $form.find('.delivery-type-radio').on('change', event => {
            this.deliveryTypeAddresses($form, event);
        });
        // Quote Address - Issue To radio -  Billing
        $form.find('[data-datafield="PrintIssuedToAddressFrom"]').on('change', event => {
            this.issueToAddresses($form, event);
        });
        // Stores previous value for Out / InDeliveryDeliveryType
        $form.find('.delivery-delivery').on('click', event => {
            let $element, newValue, prevValue;
            $element = jQuery(event.currentTarget);
            if ($element.attr('data-datafield') === 'OutDeliveryDeliveryType') {
                $element.data('prevValue', FwFormField.getValueByDataField($form, 'OutDeliveryDeliveryType'))
            } else {
                $element.data('prevValue', FwFormField.getValueByDataField($form, 'InDeliveryDeliveryType'))
            }
        });
        // Delivery type select field on Deliver tab
        $form.find('.delivery-delivery').on('change', event => {
            let $element, newValue, prevValue;
            $element = jQuery(event.currentTarget);
            newValue = $element.find('.fwformfield-value').val();
            prevValue = $element.data('prevValue');

            if ($element.attr('data-datafield') === 'OutDeliveryDeliveryType') {
                if (newValue === 'DELIVER' && prevValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'DEAL');
                }
                if (newValue === 'SHIP' && prevValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'DEAL');
                }
                if (newValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'WAREHOUSE');
                }
                $form.find('.OutDeliveryAddressType').change();
            }
            else if ($element.attr('data-datafield') === 'InDeliveryDeliveryType') {
                if (newValue === 'DELIVER' && prevValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'WAREHOUSE');
                }
                if (newValue === 'SHIP' && prevValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'WAREHOUSE');
                }
                if (newValue === 'PICK UP') {
                    FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'DEAL');
                }
                $form.find('.InDeliveryAddressType').change();
            }
        });
        //Hide/Show summary buttons based on rate type
        $form.find('[data-datafield="RateType"] input').on('change', e => {
            const rateType = FwFormField.getValueByDataField($form, 'RateType');
            if (rateType === 'MONTHLY') {
                $form.find('.togglebutton-item input[value="W"]').parent().hide();
                $form.find('.togglebutton-item input[value="M"]').parent().show();
            } else {
                $form.find('.togglebutton-item input[value="W"]').parent().show();
                $form.find('.togglebutton-item input[value="M"]').parent().hide();
            }
            //resets back to period summary frames
            $form.find('.togglebutton-item input[value="P"]').click();
        });

        $form.find(".combineddw").on('change', '.fwformfield-text, .fwformfield-value', event => {
            let val = event.target.value;
            let dwRequest = {
                'OrderId': FwFormField.getValueByDataField($form, `${this.Module}Id`),
                'RecType': '',
                'DaysPerWeek': val
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/order/applybottomlinedaysperweek`, dwRequest, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($form.find('.combinedgrid [data-name="OrderItemGrid"]'));
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });

        //Track shipment-out
        $form.find('.track-shipment-out').on('click', e => {
            const trackingURL = FwFormField.getValueByDataField($form, 'OutDeliveryFreightTrackingUrl');
            if (trackingURL !== '') {
                try {
                    window.open(trackingURL);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });

        //Track shipment-in
        $form.find('.track-shipment-in').on('click', e => {
            const trackingURL = FwFormField.getValueByDataField($form, 'InDeliveryFreightTrackingUrl');
            if (trackingURL !== '') {
                try {
                    window.open(trackingURL);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });

        //profit & loss toggle buttons
        $form.find('.profit-loss-total input').off('change').on('change', e => {
            const period = FwFormField.getValueByDataField($form, 'totalTypeProfitLoss');
            this.renderFrames($form, FwFormField.getValueByDataField($form, `${this.Module}Id`), period);
        });

        //SpecifyBillingDatesByType and LockBillingDates checkbox events
        //need to change datafields for these - jason h 08/30/2019
        $form.find('[data-datafield="SpecifyBillingDatesByType"]').on('change', e => {
            this.billingPeriodEvents($form);
        });
        $form.find('[data-datafield="LockBillingDates"]').on('change', e => {
            this.billingPeriodEvents($form);
        });
    };
    //----------------------------------------------------------------------------------------------
    bottomLineDiscountChange($form: any, event: any) {
        // DiscountPercent for all OrderItemGrid
        let $element, $orderItemGrid, orderId, recType, discountPercent, module;
        let request: any = {};
        module = this.Module;
        $element = jQuery(event.currentTarget);
        recType = $element.attr('data-rectype');
        orderId = FwFormField.getValueByDataField($form, `${module}Id`);
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
        if (recType === 'F') {
            $orderItemGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'LossAndDamageTotal', '');
            FwFormField.disable($form.find('div[data-datafield="LossAndDamageTotalIncludesTax"]'));
        }
        if (recType === 'RS') {
            $orderItemGrid = $form.find('.usedsalegrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'UsedSaleTotal', '');
            FwFormField.disable($form.find('div[data-datafield="UsedSaleTotalIncludesTax"]'));
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'PeriodCombinedTotal', '');
            FwFormField.disable($form.find('div[data-datafield="PeriodCombinedTotalIncludesTax"]'));
        }
        request.DiscountPercent = parseFloat(discountPercent);
        request.RecType = recType;
        request.OrderId = orderId;

        FwAppData.apiMethod(true, 'POST', `api/v1/${module}/applybottomlinediscountpercent/`, request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    };
    //----------------------------------------------------------------------------------------------
    bottomLineTotalWithTaxChange($form: any, event: any) {
        // Total and With Tax for all OrderItemGrid
        let $element, $orderItemGrid, recType, orderId, total, includeTaxInTotal, isWithTaxCheckbox, totalType, module;
        let request: any = {};

        $element = jQuery(event.currentTarget);
        module = this.Module;
        isWithTaxCheckbox = $element.attr('data-type') === 'checkbox';
        recType = $element.attr('data-rectype');
        orderId = FwFormField.getValueByDataField($form, `${module}Id`);

        if (recType === 'R') {
            $orderItemGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.rentalOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.rentalTotalWithTax:visible');
            totalType = $form.find('.rentalgrid .totalType input:checked').val();
            FwFormField.setValue($form, '.rentalAdjustments .rentalOrderItemTotal:hidden', '0.00');
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
            FwFormField.setValue($form, '.laborAdjustments .laborOrderItemTotal:hidden', '0.00');
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
            FwFormField.setValue($form, '.miscAdjustments .miscOrderItemTotal:hidden', '0.00');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'MiscDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('.miscTotalWithTax:visible'));
            } else {
                FwFormField.enable($form.find('.miscTotalWithTax:visible'));
            }
        }
        if (recType === 'F') {
            $orderItemGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValueByDataField($form, 'LossAndDamageTotal');
            includeTaxInTotal = FwFormField.getValueByDataField($form, 'LossAndDamageTotalIncludesTax');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'LossAndDamageDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="LossAndDamageTotalIncludesTax"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="LossAndDamageTotalIncludesTax"]'));
            }
        }
        if (recType === '') {
            $orderItemGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
            total = FwFormField.getValue($form, '.combinedOrderItemTotal:visible');
            includeTaxInTotal = FwFormField.getValue($form, '.combinedTotalWithTax:visible');
            totalType = $form.find('.combinedgrid .totalType input:checked').val();
            FwFormField.setValue($form, '.combinedAdjustments .combinedOrderItemTotal:hidden', '0.00');
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
        request.OrderId = orderId;
        request.Total = +total;

        FwAppData.apiMethod(true, 'POST', `api/v1/${module}/applybottomlinetotal/`, request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.search($orderItemGrid);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
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
    //----------------------------------------------------------------------------------------------
    calculateOrderItemGridTotals($form: any, gridType: string, totals?): void {
        let subTotal, discount, salesTax, grossTotal, total;

        let rateValue = $form.find(`.${gridType}grid .totalType input:checked`).val();
        switch (rateValue) {
            case 'W':
                subTotal = totals.WeeklyExtended;
                discount = totals.WeeklyDiscountAmount;
                salesTax = totals.WeeklyTax;
                grossTotal = totals.WeeklyExtendedNoDiscount;
                total = totals.WeeklyTotal;
                break;
            case 'P':
                subTotal = totals.PeriodExtended;
                discount = totals.PeriodDiscountAmount;
                salesTax = totals.PeriodTax;
                grossTotal = totals.PeriodExtendedNoDiscount;
                total = totals.PeriodTotal;
                break;
            case 'M':
                subTotal = totals.MonthlyExtended;
                discount = totals.MonthlyDiscountAmount;
                salesTax = totals.MonthlyTax;
                grossTotal = totals.MonthlyExtendedNoDiscount;
                total = totals.MonthlyTotal;
                break;
            default:
                subTotal = totals.PeriodExtended;
                discount = totals.PeriodDiscountAmount;
                salesTax = totals.PeriodTax;
                grossTotal = totals.PeriodExtendedNoDiscount;
                total = totals.PeriodTotal;
        }

        $form.find(".totalType input").off('change').on('change', e => {
            let $target = jQuery(e.currentTarget),
                gridType = $target.parents('.totalType').attr('data-gridtype'),
                rateType = $target.val(),
                adjustmentsPeriod = $form.find('.' + gridType + 'AdjustmentsPeriod'),
                adjustmentsWeekly = $form.find('.' + gridType + 'AdjustmentsWeekly'),
                adjustmentsMonthly = $form.find('.' + gridType + 'AdjustmentsMonthly');
            switch (rateType) {
                case 'W':
                    adjustmentsPeriod.hide();
                    adjustmentsWeekly.show();
                    break;
                case 'M':
                    adjustmentsPeriod.hide();
                    adjustmentsMonthly.show();
                    break;
                case 'P':
                    adjustmentsWeekly.hide();
                    adjustmentsMonthly.hide();
                    adjustmentsPeriod.show();
                    break;
            }
            let total = FwFormField.getValue($form, '.' + gridType + 'OrderItemTotal:visible');
            if (total === '0.00') {
                FwFormField.disable($form.find('.' + gridType + 'TotalWithTax:visible'));
            } else {
                FwFormField.enable($form.find('.' + gridType + 'TotalWithTax:visible'));
            }
            this.calculateOrderItemGridTotals($form, gridType, totals);
        });

        //const extendedColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Extended"]`);
        //const discountColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}DiscountAmount"]`);
        //const taxColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Tax"]`);

        //for (let i = 1; i < extendedColumn.length; i++) {
        //    // Extended Column
        //    let inputValueFromExtended: any = +extendedColumn.eq(i).attr('data-originalvalue');
        //    extendedTotal = extendedTotal.plus(inputValueFromExtended);
        //    // DiscountAmount Column
        //    let inputValueFromDiscount: any = +discountColumn.eq(i).attr('data-originalvalue');
        //    discountTotal = discountTotal.plus(inputValueFromDiscount);
        //    // Tax Column
        //    let inputValueFromTax: any = +taxColumn.eq(i).attr('data-originalvalue');
        //    taxTotal = taxTotal.plus(inputValueFromTax);
        //};

        //subTotal = extendedTotal.toFixed(2);
        //discount = discountTotal.toFixed(2);
        //salesTax = taxTotal.toFixed(2);
        //grossTotal = extendedTotal.plus(discountTotal).toFixed(2);
        //total = taxTotal.plus(extendedTotal).toFixed(2);

        $form.find(`.${gridType}totals [data-totalfield="SubTotal"] input`).val(subTotal);
        $form.find(`.${gridType}totals [data-totalfield="Discount"] input`).val(discount);
        $form.find(`.${gridType}totals [data-totalfield="Tax"] input`).val(salesTax);
        $form.find(`.${gridType}totals [data-totalfield="GrossTotal"] input`).val(grossTotal);
        $form.find(`.${gridType}totals [data-totalfield="Total"] input`).val(total);
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
                FwNotification.renderNotification('WARNING', "Your chosen 'Billing Stop Date' is before 'Start Date'.");
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
                    }, null);
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
        }
        else {
            FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
        }
        if (!isNaN(daysBetweenDates)) {
            if (Math.sign(daysBetweenDates) >= 0) {
                $form.find('div[data-datafield="BillingEndDate"]').removeClass('error');
            } else {
                FwNotification.renderNotification('WARNING', "Your chosen 'Billing Stop Date' is before 'Start Date'.");
                $form.find('div[data-datafield="BillingEndDate"]').addClass('error');
                FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
                FwFormField.setValueByDataField($form, 'BillingMonths', '0');
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    deliveryTypeAddresses($form: any, event: any): void {
        const $element = jQuery(event.currentTarget);
        if ($element.attr('data-datafield') === 'OutDeliveryAddressType') {
            let value = FwFormField.getValueByDataField($form, 'OutDeliveryAddressType');
            if (value === 'WAREHOUSE') {
                this.getWarehouseAddress($form, 'Out');
            } else if (value === 'DEAL') {
                this.fillDeliveryAddressFieldsforDeal($form, 'Out');
            }
        }
        else if ($element.attr('data-datafield') === 'InDeliveryAddressType') {
            let value = FwFormField.getValueByDataField($form, 'InDeliveryAddressType');
            if (value === 'WAREHOUSE') {
                this.getWarehouseAddress($form, 'In');
            } else if (value === 'DEAL') {
                this.fillDeliveryAddressFieldsforDeal($form, 'In');
            }
        }
    }
    issueToAddresses($form: JQuery, event: any): void {
        const value = FwFormField.getValueByDataField($form, 'PrintIssuedToAddressFrom');

        if (value === 'DEAL') {
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            if (dealId !== '') {
                FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealId}`, null, FwServices.defaultTimeout, res => {
                    FwFormField.setValueByDataField($form, `IssuedToName`, res.Deal);
                    setValues(res);
                }, null, null);
            }
        } else if (value === 'CUSTOMER') {
            const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
            if (customerId !== '') {
                FwAppData.apiMethod(true, 'GET', `api/v1/customer/${customerId}`, null, FwServices.defaultTimeout, res => {
                    FwFormField.setValueByDataField($form, `IssuedToName`, res.Customer);
                    setValues(res);
                }, null, null);
            }
        }
        const setValues = (response: any): void => {
            FwFormField.setValueByDataField($form, `IssuedToAttention`, response.BillToAttention1);
            FwFormField.setValueByDataField($form, `IssuedToAttention2`, response.BillToAttention2);
            FwFormField.setValueByDataField($form, `IssuedToAddress1`, response.BillToAddress1);
            FwFormField.setValueByDataField($form, `IssuedToAddress2`, response.BillToAddress2);
            FwFormField.setValueByDataField($form, `IssuedToCity`, response.BillToCity);
            FwFormField.setValueByDataField($form, `IssuedToState`, response.BillToStateId, response.BillToState);
            FwFormField.setValueByDataField($form, `IssuedToZipCode`, response.BillToZipCode);
            FwFormField.setValueByDataField($form, `IssuedToCountryId`, response.BillToCountryId, response.BillToCountry);
        }
    }
    //----------------------------------------------------------------------------------------------
    getWarehouseAddress($form: any, prefix: string): void {
        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        let WHresponse: any = {};

        if ($form.data('whAddress')) {
            WHresponse = $form.data('whAddress');
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToLocation`, WHresponse.Warehouse);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, WHresponse.Attention);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, WHresponse.Address1);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, WHresponse.Address2);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, WHresponse.City);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, WHresponse.State);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, WHresponse.Zip);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, WHresponse.CountryId, WHresponse.Country);
        } else {
            FwAppData.apiMethod(true, 'GET', `api/v1/warehouse/${warehouseId}`, null, FwServices.defaultTimeout, response => {
                WHresponse = response;
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToLocation`, WHresponse.Warehouse);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, WHresponse.Attention);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, WHresponse.Address1);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, WHresponse.Address2);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, WHresponse.City);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, WHresponse.State);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, WHresponse.Zip);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, WHresponse.CountryId, WHresponse.Country);
                // Preventing unnecessary API calls once warehouse addresses have been requested once
                $form.data('whAddress', {
                    'Warehouse': response.Warehouse,
                    'Attention': response.Attention,
                    'Address1': response.Address1,
                    'Address2': response.Address2,
                    'City': response.City,
                    'State': response.State,
                    'Zip': response.Zip,
                    'CountryId': response.CountryId,
                    'Country': response.Country
                })
            }, null, null);
        }
    }
    //----------------------------------------------------------------------------------------------
    fillDeliveryAddressFieldsforDeal($form: any, prefix: string, response?: any): void {
        if (!response) {
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealId}`, null, FwServices.defaultTimeout, res => {
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToLocation`, res.Deal);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, res.ShipAttention);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, res.ShipAddress1);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, res.ShipAddress2);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, res.ShipCity);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, res.ShipState);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, res.ShipZipCode);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, res.ShipCountryId, res.ShipCountry);
            }, null, null);
        } else {
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToLocation`, response.Deal);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, response.ShipAttention);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, response.ShipAddress1);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, response.ShipAddress2);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, response.ShipCity);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, response.ShipState);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, response.ShipZipCode);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, response.ShipCountryId, response.ShipCountry);
        }
    }

    //----------------------------------------------------------------------------------------------
    disableWithTaxCheckbox($form: any): void {
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
    applyOrderTypeDefaults($form) {
        const locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        const orderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        const request: any = {};
        request.uniqueids = {
            OrderTypeId: orderTypeId,
            LocationId: locationId
        }
        FwAppData.apiMethod(true, 'POST', `api/v1/ordertypelocation/browse`, request, FwServices.defaultTimeout,
            response => {
                if (response.Rows.length > 0) {
                    const termsConditionsId = response.Rows[0][response.ColumnIndex.TermsConditionsId];
                    const termsConditions = response.Rows[0][response.ColumnIndex.TermsConditions];
                    const coverLetterId = response.Rows[0][response.ColumnIndex.CoverLetterId];
                    const coverLetter = response.Rows[0][response.ColumnIndex.CoverLetter];
                    FwFormField.setValue($form, 'div[data-datafield="TermsConditionsId"]', termsConditionsId, termsConditions);
                    FwFormField.setValue($form, 'div[data-datafield="CoverLetterId"]', coverLetterId, coverLetter);
                }
            }, null, null);
    }
    //----------------------------------------------------------------------------------------------
    cancelUncancelOrder($form: any) {
        let $confirmation, $yes, $no, id, orderStatus, self, module;
        self = this;
        module = this.Module;
        id = FwFormField.getValueByDataField($form, `${module}Id`);
        orderStatus = FwFormField.getValueByDataField($form, 'Status');

        if (id != null) {
            if (orderStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push(`    <div>Would you like to un-cancel this ${module}?</div>`);
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, `Un-Cancel ${module}`, false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', uncancelOrder);
            }
            else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push(`    <div>Would you like to cancel this ${module}?</div>`);
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, `Cancel ${module}`, false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', cancelOrder);
            }
        }
        else {
            if (module === 'Order') {
                FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
            } else if (module === 'Quote') {
                FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
            }
        }

        function cancelOrder() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Canceling...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/cancel/${id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Cancelled`);
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, self);
            }, function onError(response) {
                $yes.on('click', cancelOrder);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, self);
            }, $form);
        };

        function uncancelOrder() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Retrieving...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/uncancel/${id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Retrieved`);
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, self);
            }, function onError(response) {
                $yes.on('click', uncancelOrder);
                $yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, self);
            }, $form);
        };
    };
    //----------------------------------------------------------------------------------------------
    changeOfficeLocation($form: any) {
        let $confirmation, $yes, $no, id, self, module, controller;
        self = this;
        module = this.Module;
        controller = $form.attr('data-controller');
        id = FwFormField.getValueByDataField($form, `${module}Id`);

        if (id != null) {
            $confirmation = FwConfirmation.renderConfirmation('Change Office Location / Warehouse', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            let html = [];
            html.push(`<div class="fwform" data-controller="${controller}" style="background-color: transparent;">`);
            html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push(`    <div>Change Office Location / Warehouse for this ${module}:</div>`);
            html.push(`      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push(`        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-validationname="OfficeLocationValidation" data-required="true"></div>`);
            html.push(`      </div>`);
            html.push(`      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push(`        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-formbeforevalidate="beforeValidateWarehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-boundfields="OfficeLocationId" data-required="true"></div>`);
            html.push(`      </div>`);
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            FwFormField.setValueByDataField($confirmation, 'OfficeLocationId', FwFormField.getValueByDataField($form, 'OfficeLocationId'), FwFormField.getTextByDataField($form, 'OfficeLocationId'));
            FwFormField.setValueByDataField($confirmation, 'WarehouseId', FwFormField.getValueByDataField($form, 'WarehouseId'), FwFormField.getTextByDataField($form, 'WarehouseId'));
            $confirmation.find('[data-datafield="OfficeLocationId"]').data('onchange', e => {
                FwFormField.setValue($confirmation, 'div[data-datafield="WarehouseId"]', '', '');
            });

            $yes = FwConfirmation.addButton($confirmation, `Change`, false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', changeOffice);
        }
        else {
            FwNotification.renderNotification(`WARNING`, `No ${module} selected.`);
        }

        function changeOffice() {
            let valid = FwModule.validateForm($confirmation);
            if (valid) {
                let request: any = {};
                const locationid = FwFormField.getValueByDataField($confirmation, 'OfficeLocationId');
                const warehouseid = FwFormField.getValueByDataField($confirmation, 'WarehouseId');
                request = {
                    OfficeLocationId: locationid,
                    Warehouseid: warehouseid
                };
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Changing...');
                $yes.off('click');
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

                FwAppData.apiMethod(true, 'POST', `api/v1/${module}/changeofficelocation/${id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success) {

                        FwNotification.renderNotification('SUCCESS', `${module} Successfully Changed`);
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwModule.refreshForm($form, self);
                    }
                    else {
                        $yes.on('click', changeOffice);
                        $yes.text('Change');
                        FwFunc.showError(response.msg);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                    }
                }, function onError(response) {
                    $yes.on('click', changeOffice);
                    $yes.text('Change');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form, self);
                }, realConfirm);
            }
        };
    };
    //----------------------------------------------------------------------------------------------    
    applyOrderTypeAndRateTypeToForm($form) {
        const $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        const rateType = FwFormField.getValueByDataField($form, 'RateType');

        // get the OrderTypeId from the form
        const orderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');

        if (this.CachedOrderTypes[orderTypeId] !== undefined) {
            applyOrderTypeToColumns($form, this.CachedOrderTypes[orderTypeId]);
        } else {
            const fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field');
            const fieldNames = [];

            for (let i = 3; i < fields.length; i++) {
                const name = jQuery(fields[i]).attr('data-mappedfield');
                if (name != "QuantityOrdered") {
                    fieldNames.push(name);
                }
            }

            FwAppData.apiMethod(true, 'GET', `api/v1/ordertype/${orderTypeId}`, null, FwServices.defaultTimeout, response => {
                const hiddenRentals = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.RentalShowFields))
                const hiddenSales = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.SalesShowFields))
                const hiddenLabor = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.LaborShowFields))
                const hiddenMisc = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.MiscShowFields))
                const hiddenUsedSale = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.RentalSaleShowFields))
                const hiddenLossDamage = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.LossAndDamageShowFields))
                const hiddenCombined = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.CombinedShowFields))

                this.CachedOrderTypes[orderTypeId] = {
                    CombineActivityTabs: response.CombineActivityTabs,
                    hiddenRentals: hiddenRentals,
                    hiddenSales: hiddenSales,
                    hiddenLabor: hiddenLabor,
                    hiddenMisc: hiddenMisc,
                    hiddenUsedSale: hiddenUsedSale,
                    hiddenLossDamage: hiddenLossDamage,
                    hiddenCombined: hiddenCombined
                }
                applyOrderTypeToColumns($form, this.CachedOrderTypes[orderTypeId]);
            }, null, null);
        }

        //sets active tab and opens search interface from a newly saved record 
        //12-12-18 moved here from afterSave Jason H 
        if ($form.attr('data-opensearch') === "true") {
            //FwTabs.setActiveTab($form, $tab); //this method doesn't seem to be working correctly
            const activeTabId = $form.attr('data-activetabid');
            const $newTab = $form.find(`#${activeTabId}`);
            $newTab.click();
            const search = new SearchInterface();
            const searchType = $form.attr('data-searchtype');
            if ($form.attr('data-controller') === "OrderController") {
                search.renderSearchPopup($form, FwFormField.getValueByDataField($form, 'OrderId'), 'Order', searchType);
            } else if ($form.attr('data-controller') === "QuoteController") {
                search.renderSearchPopup($form, FwFormField.getValueByDataField($form, 'QuoteId'), 'Quote', searchType);
            }
            $form.removeAttr('data-opensearch data-searchtype data-activetabid');
        }

        function applyOrderTypeToColumns($form, orderTypeData) {
            $form.find('[data-datafield="CombineActivity"] input').val(orderTypeData.CombineActivityTabs);
            const $lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss & Damage"]');

            if (orderTypeData.CombineActivityTabs === true) {
                $form.find('.notcombined').css('display', 'none');
                $form.find('.notcombinedtab').css('display', 'none');
                $form.find('.combinedtab').show();
            } else {
                $form.find('.combined').css('display', 'none');
                $form.find('.combinedtab').css('display', 'none');
                $form.find('.notcombinedtab').show();

                // show/hide tabs based on Activity boxes checked
                const $rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]');
                $form.find('[data-datafield="Rental"] input').prop('checked') ? $rentalTab.show() : $rentalTab.hide();
                const $salesTab = $form.find('[data-type="tab"][data-caption="Sales"]');
                $form.find('[data-datafield="Sales"] input').prop('checked') ? $salesTab.show() : $salesTab.hide();
                const $miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]');
                $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? $miscTab.show() : $miscTab.hide();
                const $laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
                $form.find('[data-datafield="Labor"] input').prop('checked') ? $laborTab.show() : $laborTab.hide();
                const $usedSaleTab = $form.find('[data-type="tab"][data-caption="Used Sale"]');
                $form.find('[data-datafield="RentalSale"] input').prop('checked') ? $usedSaleTab.show() : $usedSaleTab.hide();
                if ($lossDamageTab !== undefined) {
                    $form.find('[data-datafield="LossAndDamage"] input').prop('checked') ? $lossDamageTab.show() : $lossDamageTab.hide();
                }
            }

            for (let i = 0; i < orderTypeData.hiddenRentals.length; i++) {
                jQuery($rentalGrid.find(`[data-mappedfield="${orderTypeData.hiddenRentals[i]}"]`)).parent().hide();
            }
            const $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
            for (let i = 0; i < orderTypeData.hiddenSales.length; i++) {
                jQuery($salesGrid.find(`[data-mappedfield="${orderTypeData.hiddenSales[i]}"]`)).parent().hide();
            }
            const $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
            for (let i = 0; i < orderTypeData.hiddenLabor.length; i++) {
                jQuery($laborGrid.find(`[data-mappedfield="${orderTypeData.hiddenLabor[i]}"]`)).parent().hide();
            }
            const $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
            for (let i = 0; i < orderTypeData.hiddenMisc.length; i++) {
                jQuery($miscGrid.find(`[data-mappedfield="${orderTypeData.hiddenMisc[i]}"]`)).parent().hide();
            }
            const $usedSaleGrid = $form.find('.usedsalegrid [data-name="OrderItemGrid"]');
            for (let i = 0; i < orderTypeData.hiddenUsedSale.length; i++) {
                jQuery($usedSaleGrid.find(`[data-mappedfield="${orderTypeData.hiddenUsedSale[i]}"]`)).parent().hide();
            }
            const $lossDamageGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            if ($lossDamageTab !== undefined) {
                for (let i = 0; i < orderTypeData.hiddenLossDamage.length; i++) {
                    jQuery($lossDamageGrid.find(`[data-mappedfield="${orderTypeData.hiddenLossDamage[i]}"]`)).parent().hide();
                }
            }
            const $combinedGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
            for (let i = 0; i < orderTypeData.hiddenCombined.length; i++) {
                jQuery($combinedGrid.find(`[data-mappedfield="${orderTypeData.hiddenCombined[i]}"]`)).parent().hide();
            }
            if (orderTypeData.hiddenRentals.indexOf('WeeklyExtended') === -1 && rateType === '3WEEK') {
                $rentalGrid.find('.3weekextended').parent().show();
            } else if (orderTypeData.hiddenRentals.indexOf('WeeklyExtended') === -1 && rateType !== '3WEEK') {
                $rentalGrid.find('.weekextended').parent().show();
            }

            const weeklyType = $form.find(".weeklyType");
            const monthlyType = $form.find(".monthlyType");
            const rentalDaysPerWeek = $form.find(".RentalDaysPerWeek");
            const billingMonths = $form.find(".BillingMonths");
            const billingWeeks = $form.find(".BillingWeeks");


            switch (rateType) {
                case 'DAILY':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.show();
                    billingMonths.hide();
                    billingWeeks.show();
                    $rentalGrid.find('.dw').parent().show();
                    //$form.find('.combinedgrid [data-name="OrderItemGrid"]').parent().show();
                    //$form.find('.rentalgrid [data-name="OrderItemGrid"]').parent().show();
                    //$form.find('.salesgrid [data-name="OrderItemGrid"]').parent().show();
                    //$form.find('.laborgrid [data-name="OrderItemGrid"]').parent().show();
                    //$form.find('.miscgrid [data-name="OrderItemGrid"]').parent().show();
                    break;
                case 'WEEKLY':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.hide();
                    $rentalGrid.find('.dw').parent().hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case '3WEEK':
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.hide();
                    $rentalGrid.find('.dw').parent().hide();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
                case 'MONTHLY':
                    weeklyType.hide();
                    monthlyType.show();
                    rentalDaysPerWeek.hide();
                    $rentalGrid.find('.dw').parent().hide();
                    billingWeeks.hide();
                    billingMonths.show();
                    break;
                default:
                    weeklyType.show();
                    monthlyType.hide();
                    rentalDaysPerWeek.show();
                    $rentalGrid.find('.dw').parent().show();
                    billingMonths.hide();
                    billingWeeks.show();
                    break;
            }


            //if (rateType === '3WEEK') {
            //    $allOrderItemGrid.find('.3week').parent().show();
            //    $allOrderItemGrid.find('.weekextended').parent().hide();
            //    $allOrderItemGrid.find('.price').find('.caption').text('Week 1 Rate');
            //    $orderItemGridRental.find('.3week').parent().show();
            //    $orderItemGridRental.find('.weekextended').parent().hide();
            //    $orderItemGridRental.find('.price').find('.caption').text('Week 1 Rate');
            //}
        }
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form, response) {
        const period = FwFormField.getValueByDataField($form, 'totalTypeProfitLoss');
        this.renderFrames($form, FwFormField.getValueByDataField($form, `${this.Module}Id`), period);
        this.applyOrderTypeAndRateTypeToForm($form);

        // disable/enable PO Number and Amount based on PO Pending
        let $pending = $form.find('div.fwformfield[data-datafield="PendingPo"] input').prop('checked');
        if ($pending === true) {
            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.enable($form.find('[data-datafield="PoAmount"]'));
        }

        // update fields on the form based on Rate Type
        const rateType = FwFormField.getValueByDataField($form, 'RateType');
        if (rateType === 'MONTHLY') {
            $form.find(".BillingWeeks").hide();
            $form.find(".BillingMonths").show();
        } else {
            $form.find(".BillingMonths").hide();
            $form.find(".BillingWeeks").show();
        }

        // show/hide D/W field based on Rate Type
        if (rateType === 'DAILY') {
            $form.find(".RentalDaysPerWeek").show();
            $form.find(".combineddw").show();
        } else {
            $form.find(".RentalDaysPerWeek").hide();
            $form.find(".combineddw").hide();
        }

        //Show/hide summary buttons based on rate type
        $form.find('.summaryperiod').addClass('pressed');
        if (rateType === 'MONTHLY') {
            $form.find('.togglebutton-item input[value="W"]').parent().hide();
            $form.find('.togglebutton-item input[value="M"]').parent().show();
        } else {
            $form.find('.togglebutton-item input[value="W"]').parent().show();
            $form.find('.togglebutton-item input[value="M"]').parent().hide();
        }

        $form.find(".totals .add-on").hide();
        $form.find('.totals input').css('text-align', 'right');

        // find all the items grids on the form
        const $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        const $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        const $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        const $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        const $usedSaleGrid = $form.find('.usedsalegrid [data-name="OrderItemGrid"]');
        const $lossDamageGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');

        // disable the Rate column
        if (FwFormField.getValueByDataField($form, 'DisableEditingRentalRate')) {
            $rentalGrid.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingSalesRate')) {
            $salesGrid.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingLaborRate')) {
            $laborGrid.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingMiscellaneousRate')) {
            $miscGrid.find('.rates').attr('data-formreadonly', true);
        }
        if (FwFormField.getValueByDataField($form, 'DisableEditingUsedSaleRate')) {
            $usedSaleGrid.find('.rates').attr('data-formreadonly', true);
        }
        if ($lossDamageGrid !== undefined) {
            if (FwFormField.getValueByDataField($form, 'DisableEditingLossAndDamageRate')) {
                $lossDamageGrid.find('.rates').attr('data-formreadonly', true);
            }
        }

        // disable/enable the No Charge Reason field
        const noChargeValue = FwFormField.getValueByDataField($form, 'NoCharge');
        if (noChargeValue == false) {
            FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        } else {
            FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        }

        // Disable withTax checkboxes if Total field is 0.00
        this.disableWithTaxCheckbox($form);


        // color the Notes tab if notes exist
        const hasNotes = FwFormField.getValueByDataField($form, 'HasNotes');
        if (hasNotes) {
            FwTabs.setTabColor($form.find('.notestab'), '#FFFF8d');
        }

        // color the Rental tab if RentalItems exist
        const hasRentalItem = FwFormField.getValueByDataField($form, 'HasRentalItem');
        if (hasRentalItem) {
            FwTabs.setTabColor($form.find('.rentaltab'), '#FFFF8d');
        }
        // color the Sales tab if SalesItems exist
        const hasSalesItem = FwFormField.getValueByDataField($form, 'HasSalesItem');
        if (hasSalesItem) {
            FwTabs.setTabColor($form.find('.salestab'), '#FFFF8d');
        }
        // color the Misc. tab if MiscItems exist
        const hasMiscItem = FwFormField.getValueByDataField($form, 'HasMiscellaneousItem');
        if (hasMiscItem) {
            FwTabs.setTabColor($form.find('.misctab'), '#FFFF8d');
        }
        // color the Labor tab if LaborItems exist
        const hasLaborItem = FwFormField.getValueByDataField($form, 'HasLaborItem');
        if (hasLaborItem) {
            FwTabs.setTabColor($form.find('.labortab'), '#FFFF8d');
        }
        // color the Rental Sale tab if RentalSaleItems exist
        const hasRentalSaleItem = FwFormField.getValueByDataField($form, 'HasRentalSaleItem');
        if (hasRentalSaleItem) {
            FwTabs.setTabColor($form.find('.usedsaletab'), '#FFFF8d');
        }

        // color the Loss and Damage tab if LossDamageItems exist
        let hasLossAndDamageItem = FwFormField.getValueByDataField($form, 'HasLossAndDamageItem');
        if (hasLossAndDamageItem) {
            FwTabs.setTabColor($form.find('.lossdamagetab'), '#FFFF8d');
        }

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabPageId = $tab.attr('data-tabpageid');
            if ($tab.hasClass('audittab') == false) {
                const $gridControls = $form.find(`#${tabPageId} [data-type="Grid"]`);
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

                const $browseControls = $form.find(`#${tabPageId} [data-type="Browse"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
            $tab.addClass('tabGridsLoaded');
        });

        // disable the Activity checkboxes if Items exist
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

        // Enable/Disable checkboxes and show/hide Profit & Loss sections
        const rentalVal = FwFormField.getValueByDataField($form, 'Rental');
        const salesVal = FwFormField.getValueByDataField($form, 'Sales');
        const usedSaleVal = FwFormField.getValueByDataField($form, 'RentalSale');
        const laborVal = FwFormField.getValueByDataField($form, 'Labor');
        const miscVal = FwFormField.getValueByDataField($form, 'Miscellaneous');
        let lossDamageVal;
        if (rentalVal === true || salesVal === true || usedSaleVal === true) {
            FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
        } else if (rentalVal === false && salesVal === false && usedSaleVal === false) {
            FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
        }
        if (this.Module === 'Order') {
            lossDamageVal = FwFormField.getValueByDataField($form, 'LossAndDamage');
            if (rentalVal || lossDamageVal) {
                FwFormField.disable(FwFormField.getDataField($form, 'RentalSale'));
            } else if (!rentalVal && !lossDamageVal) {
                FwFormField.enable(FwFormField.getDataField($form, 'RentalSale'));
            }
        } else {
            if (rentalVal) {
                FwFormField.disable(FwFormField.getDataField($form, 'RentalSale'));
            } else if (!rentalVal && !lossDamageVal) {
                FwFormField.enable(FwFormField.getDataField($form, 'RentalSale'));
            }
        }

        const quikSearchMenuId = Constants.Modules.Home.Order.form.menuItems.Search.id.replace('{', '').replace('}', '');
        if (lossDamageVal) {
            $form.find('[data-securityid="searchbtn"]').addClass('disabled')
            $form.find(`.submenu-btn[data-securityid="${quikSearchMenuId}"]`).attr('data-enabled', 'false');
        } else {
            $form.find('[data-securityid="searchbtn"]').removeClass('disabled');
            $form.find(`.submenu-btn[data-securityid="${quikSearchMenuId}"]`).attr('data-enabled', 'true');
        }

        //toggle profit & loss activity section visibility
        rentalVal ? $form.find('.rental-pl').show() : $form.find('.rental-pl').hide();
        salesVal ? $form.find('.sales-pl').show() : $form.find('.sales-pl').hide();
        laborVal ? $form.find('.labor-pl').show() : $form.find('.labor-pl').hide();
        miscVal ? $form.find('.misc-pl').show() : $form.find('.misc-pl').hide();
        usedSaleVal ? $form.find('.usedsale-pl').show() : $form.find('.usedsale-pl').hide();

        // disable all controls on the form based on Quote/Order status
        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status === 'ORDERED' || status === 'CLOSED' || status === 'CANCELLED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
            $form.find('.btn[data-securityid="searchbtn"]').addClass('disabled');
        }

        //Disable 'Track Shipment' button
        const outtrackingNumber = FwFormField.getValueByDataField($form, 'OutDeliveryFreightTrackingNumber');
        const $outtrackShipmentBtn = $form.find('.track-shipment-out');
        if (outtrackingNumber === '') {
            FwFormField.disable($outtrackShipmentBtn);
        } else {
            FwFormField.enable($outtrackShipmentBtn);
        }

        const intrackingNumber = FwFormField.getValueByDataField($form, 'InDeliveryFreightTrackingNumber');
        const $intrackShipmentBtn = $form.find('.track-shipment-in');
        if (intrackingNumber === '') {
            FwFormField.disable($intrackShipmentBtn);
        } else {
            FwFormField.enable($intrackShipmentBtn);
        }

        this.billingPeriodEvents($form);
        this.renderScheduleDateAndTimeSection($form, response);
    }
    //----------------------------------------------------------------------------------------------
    billingPeriodEvents($form) {
        const lockDatesChecked = FwFormField.getValueByDataField($form, 'LockBillingDates');
        const specifyDatesChecked = FwFormField.getValueByDataField($form, 'SpecifyBillingDatesByType');
        const dateTypeSection = $form.find('.date-types');
        const $billingPeriodFields = dateTypeSection.find('.fwformfield[data-type="date"]');
        const $billingDates = $form.find('.date-types-disable');
        if (specifyDatesChecked) {
            dateTypeSection.show();
            FwFormField.disable($billingDates);
            if (lockDatesChecked) {
                FwFormField.disable($billingPeriodFields);
            } else {
                FwFormField.enable($billingPeriodFields);
            }
        } else {
            dateTypeSection.hide();
            FwFormField.disable($billingPeriodFields);
            FwFormField.enable($billingDates);
        }

        if (lockDatesChecked) {
            FwFormField.disable($billingDates);
        } else {
            if (!specifyDatesChecked) {
                FwFormField.enable($billingDates);
            } else {
                FwFormField.enable($billingPeriodFields);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    renderScheduleDateAndTimeSection($form, response) {
        //this is being called from FwModule > afterLoadForm in order to get the ActivityDatesAndTimes fields
        const dates = `<span class="modify" style="cursor:pointer; color:blue; margin-left:20px; text-decoration:underline;">Show All Dates and Times</span>`;
        $form.find('.activity-dates-toggle').empty().append(dates);
        $form.find('.activity-dates').empty();
        const activityDatesAndTimes = response.ActivityDatesAndTimes;
        for (let i = 0; i < activityDatesAndTimes.length; i++) {
            const row = activityDatesAndTimes[i];
            const $row = jQuery(`<div class="flexrow date-row">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="OrderTypeDateTypeId" style="display:none;"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="${row.Descriptiondisplay}" data-datafield="Date" data-enabled="true" style="flex:0 1 150px;"></div>
                              <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="" data-datafield="Time" data-enabled="true" style="flex:0 1 120px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Day" data-datafield="DayOfWeek" data-enabled="false" style="flex:0 1 120px;"></div>                          
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Production Activity" data-datafield="IsProductionActivity" style="display:none; flex:0 1 180px;"></div>                          
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Milestone" data-datafield="IsMilestone" style="display:none; flex:0 1 110px;"></div>                          
                              </div>`);
            FwControl.renderRuntimeControls($row.find('.fwcontrol'));
            FwFormField.setValueByDataField($row, 'OrderTypeDateTypeId', row.OrderTypeDateTypeId);
            FwFormField.setValueByDataField($row, 'Date', row.Date);
            FwFormField.setValueByDataField($row, 'Time', row.Time);
            FwFormField.setValueByDataField($row, 'DayOfWeek', row.DayOfWeek);
            FwFormField.setValueByDataField($row, 'IsProductionActivity', row.IsProductionActivity);
            FwFormField.setValueByDataField($row, 'IsMilestone', row.IsMilestone);
            $form.find('.activity-dates').append($row);
        };

        const $showActivitiesAndMilestones = jQuery(`<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Production Activities and Milestones" data-datafield="ShowActivitiesAndMilestones" style="flex:1 1 250px;"></div>`);
        FwControl.renderRuntimeControls($showActivitiesAndMilestones);
        $form.find('.activity-dates').append($showActivitiesAndMilestones);

        $showActivitiesAndMilestones.on('change', e => {
            const isChecked = jQuery(e.currentTarget).find('input').prop('checked');
            const $checkboxes = $form.find('[data-datafield="IsMilestone"], [data-datafield="IsProductionActivity"]');
            if (isChecked) {
                $checkboxes.show();
            } else {
                $checkboxes.hide();
            }
        });

        //activity dates
        $form.find('.modify').off().on('click', e => {
            $form.find('.schedule-date-fields').hide();
            $form.find('.activity-dates').show();
        });

        $form.data('beforesave', request => {
            if ($form.find('.activity-dates:visible').length > 0) {
                const activityDatesAndTimes = [];
                const $rows = $form.find('.date-row');
                for (let i = 0; i < $rows.length; i++) {
                    const $row = jQuery($rows[i]);
                    activityDatesAndTimes.push({
                        OrderTypeDateTypeId: FwFormField.getValue2($row.find('[data-datafield="OrderTypeDateTypeId"]'))
                        , Date: FwFormField.getValue2($row.find('[data-datafield="Date"]'))
                        , Time: FwFormField.getValue2($row.find('[data-datafield="Time"]'))
                        , IsProductionActivity: FwFormField.getValue2($row.find('[data-datafield="IsProductionActivity"]'))
                        , IsMilestone: FwFormField.getValue2($row.find('[data-datafield="IsMilestone"]'))
                    });
                }
                request['ActivityDatesAndTimes'] = activityDatesAndTimes;
            }
        });

        //stops field event bubbling
        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form) {
        const $activeTab = $form.find('.active[data-type="tab"]');
        if (this.CombineActivity === 'true') {
            $form.find('.combinedtab').css('display', 'flex');
            //$form.find('.combined').css('display', 'block');
            //$form.find('.generaltab').click();
        } else {
            $form.find('.notcombinedtab').css('display', 'flex');
            //$form.find('.notcombined').css('display', 'block');
            //$form.find('.generaltab').click();
        }
        this.renderGrids($form);
        const period = FwFormField.getValueByDataField($form, 'totalTypeProfitLoss');
        this.renderFrames($form, FwFormField.getValueByDataField($form, `${this.Module}Id`), period);
        //this.dynamicColumns($form);
        this.applyOrderTypeAndRateTypeToForm($form);
        $activeTab.click();
    };
    //----------------------------------------------------------------------------------------------
}
var OrderBaseController = new OrderBase();