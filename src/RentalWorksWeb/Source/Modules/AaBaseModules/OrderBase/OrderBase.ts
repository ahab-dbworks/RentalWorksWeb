//----------------------------------------------------------------------------------------------
class OrderBase {
    DefaultOrderType: string;
    DefaultOrderTypeId: string;
    DefaultTermsConditionsId: string;
    DefaultTermsConditions: string;
    DefaultCoverLetterId: string;
    DefaultCoverLetter: string;
    DefaultPresentationLayerId: string;
    DefaultPresentationLayer: string;
    DefaultFromTime: string;
    DefaultPickTime: string;
    DefaultToTime: string;
    CombineActivity: string;
    AllowRoundTripRentals: boolean;
    Module: string;
    id: string;
    apiurl: string;
    CachedOrderTypes: any = {};
    totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax1', 'WeeklyTax2', 'WeeklyTax', 'WeeklyTotal', 'AverageWeeklyExtendedNoDiscount', 'AverageWeeklyDiscountAmount', 'AverageWeeklyExtended', 'AverageWeeklyTax1', 'AverageWeeklyTax2', 'AverageWeeklyTax', 'AverageWeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTax1', 'MonthlyTax2', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTax1', 'PeriodTax2', 'PeriodTotal',]
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string { return ``; }
    getFormTemplate(): string { return ``; }
    //findFilterHide($fwgrid: JQuery | JQuery[], element: string, filter: string): void {
    //    if (Array.isArray($fwgrid)) {
    //        $fwgrid.forEach((grid) => {
    //            grid.find(element).filter(filter).hide();
    //        });
    //    } else {
    //        $fwgrid.find(element).filter(filter).hide();
    //    }
    //}
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'OrderHiatusDiscountGrid',
            gridSecurityId: 'q4N43Gk5H1471',
            moduleSecurityId: this.id,
            $form: $form,
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
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'DealHiatusDiscountGrid',
            gridSecurityId: 'qyEHq2bK1WIJ4',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DealId: FwFormField.getValueByDataField($form, `DealId`)
                };
            },
            beforeSave: (request: any) => {
                request.DealId = FwFormField.getValueByDataField($form, `DealId`);
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderStatusHistoryGrid',
            gridSecurityId: 'B9CzDEmYe1Zf',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
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
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderNoteGrid',
            gridSecurityId: 'B9CzDEmYe1Zf',
            moduleSecurityId: this.id,
            $form: $form,
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
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderContactGrid',
            gridSecurityId: 'B9CzDEmYe1Zf',
            moduleSecurityId: this.id,

            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            },
            beforeSave: (request: any) => {
                let companyId = FwFormField.getValueByDataField($form, 'DealId');
                if (companyId === '') {
                    companyId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                }
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.CompanyId = companyId;
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.data('deletewithnoids', OrderContactGridController.deleteWithNoIds);
            },
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderActivitySummaryGrid',
            gridSecurityId: 'anBvrz1T2ipsv',
            moduleSecurityId: this.id,

            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'OrderBillingScheduleGrid',
            gridSecurityId: 'uOnqzcfEDJnJ',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                }
            }
        });
        // ----------
        let $orderItemGridRental: JQuery;
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.rentalgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                const $sutotalinggroup = FwMenu.addSubMenuGroup($optionscolumn, 'Headers / Text / Sub-Totals', '')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Lock / Unlock Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.lockUnlock(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                if ($form.attr('data-controller') !== 'QuoteController') {
                    FwMenu.addSubMenuItem($optionsgroup, 'Sub PO Worksheet', '', (e: JQuery.ClickEvent) => {
                        try {
                            OrderItemGridController.SubPOWorksheet(e);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                FwMenu.addSubMenuItem($optionsgroup, 'Restore System Sorting', 'HEg0EHlwFNVr', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.restoreSystemSorting(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Header Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertHeaderLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Text Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertTextLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Sub-Total Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertSubTotalLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Summary View', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.detailSummaryView(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Shortages Only', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.shortagesOnly(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Rollup Quantities', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.rollup(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
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
                $orderItemGridRental = $fwgrid;
                $orderItemGridRental.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'rental', dt.Totals);
                let rentalItems = $form.find('.rentalgrid tbody').children();
                rentalItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Rental"]')) : FwFormField.enable($form.find('[data-datafield="Rental"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        let $orderItemGridSales: JQuery;
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.salesgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                const $sutotalinggroup = FwMenu.addSubMenuGroup($optionscolumn, 'Headers / Text / Sub-Totals', '')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Lock / Unlock Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.lockUnlock(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                if ($form.attr('data-controller') !== 'QuoteController') {
                    FwMenu.addSubMenuItem($optionsgroup, 'Sub PO Worksheet', '', (e: JQuery.ClickEvent) => {
                        try {
                            OrderItemGridController.SubPOWorksheet(e);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                FwMenu.addSubMenuItem($optionsgroup, 'Restore System Sorting', 'HEg0EHlwFNVr', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.restoreSystemSorting(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Header Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertHeaderLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Text Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertTextLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Sub-Total Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertSubTotalLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Summary View', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.detailSummaryView(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Shortages Only', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.shortagesOnly(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Rollup Quantities', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.rollup(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    RecType: 'S'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.RecType = 'S';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('S');
                $fwgrid.find('div[data-datafield="Price"]').attr('data-caption', 'Unit Price');
                $fwgrid.find('div[data-datafield="PeriodDiscountAmount"]').attr('data-caption', 'Discount Amount');
                $fwgrid.find('div[data-datafield="PeriodExtended"]').attr('data-caption', 'Extended');
                $orderItemGridSales = $fwgrid;
                $orderItemGridSales.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'sales', dt.Totals);
                let salesItems = $form.find('.salesgrid tbody').children();
                salesItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Sales"]')) : FwFormField.enable($form.find('[data-datafield="Sales"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        let $orderItemGridLabor: JQuery;
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.laborgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                const $sutotalinggroup = FwMenu.addSubMenuGroup($optionscolumn, 'Headers / Text / Sub-Totals', '')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Lock / Unlock Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.lockUnlock(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                if ($form.attr('data-controller') !== 'QuoteController') {
                    FwMenu.addSubMenuItem($optionsgroup, 'Sub PO Worksheet', '', (e: JQuery.ClickEvent) => {
                        try {
                            OrderItemGridController.SubPOWorksheet(e);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                };
                FwMenu.addSubMenuItem($optionsgroup, 'Restore System Sorting', 'HEg0EHlwFNVr', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.restoreSystemSorting(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Header Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertHeaderLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Text Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertTextLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Sub-Total Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertSubTotalLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Summary View', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.detailSummaryView(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Shortages Only', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.shortagesOnly(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Rollup Quantities', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.rollup(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    RecType: 'L'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.RecType = 'L';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('L');
                $browse.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
                $orderItemGridLabor = $fwgrid;
                $orderItemGridLabor.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'labor', dt.Totals);
                let laborItems = $form.find('.laborgrid tbody').children();
                laborItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Labor"]')) : FwFormField.enable($form.find('[data-datafield="Labor"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        let $orderItemGridMisc: JQuery;
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.miscgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                const $sutotalinggroup = FwMenu.addSubMenuGroup($optionscolumn, 'Headers / Text / Sub-Totals', '')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Lock / Unlock Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.lockUnlock(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                if ($form.attr('data-controller') !== 'QuoteController') {
                    FwMenu.addSubMenuItem($optionsgroup, 'Sub PO Worksheet', '', (e: JQuery.ClickEvent) => {
                        try {
                            OrderItemGridController.SubPOWorksheet(e);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                };
                FwMenu.addSubMenuItem($optionsgroup, 'Restore System Sorting', 'HEg0EHlwFNVr', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.restoreSystemSorting(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Header Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertHeaderLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Text Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertTextLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Sub-Total Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertSubTotalLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Summary View', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.detailSummaryView(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Shortages Only', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.shortagesOnly(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Rollup Quantities', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.rollup(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    RecType: 'M'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.RecType = 'M';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('M');
                $browse.find('div[data-datafield="InventoryId"]').attr('data-caption', 'Item No.');
                $orderItemGridMisc = $fwgrid;
                $orderItemGridMisc.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'misc', dt.Totals);
                let miscItems = $form.find('.miscgrid tbody').children();
                miscItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="Miscellaneous"]')) : FwFormField.enable($form.find('[data-datafield="Miscellaneous"]'));
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        let $orderItemGridRentalSale: JQuery;
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.rentalsalegrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                const $sutotalinggroup = FwMenu.addSubMenuGroup($optionscolumn, 'Headers / Text / Sub-Totals', '')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Lock / Unlock Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.lockUnlock(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Restore System Sorting', 'HEg0EHlwFNVr', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.restoreSystemSorting(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Header Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertHeaderLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Text Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertTextLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Sub-Total Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertSubTotalLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Summary View', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.detailSummaryView(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Shortages Only', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.shortagesOnly(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Rollup Quantities', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.rollup(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                    RecType: 'RS'
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                request.RecType = 'RS';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('RS');
                $orderItemGridRentalSale = $fwgrid;
                $orderItemGridRentalSale.find('[data-datafield="Description"]').attr({ 'data-datatype': 'validation', 'data-validationpeek': 'false' });
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'rentalsale', dt.Totals);
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        let $combinedOrderItemGrid: JQuery;
        FwBrowse.renderGrid({
            nameGrid: 'OrderItemGrid',
            gridSelector: '.combinedgrid div[data-grid="OrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                const $sutotalinggroup = FwMenu.addSubMenuGroup($optionscolumn, 'Headers / Text / Sub-Totals', '')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Line-Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyLineItems(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Lock / Unlock Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.lockUnlock(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Bold / Unbold Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.boldUnbold(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Mute / Unmute Selected', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.muteUnmute(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                if ($form.attr('data-controller') !== 'QuoteController') {
                    FwMenu.addSubMenuItem($optionsgroup, 'Sub PO Worksheet', '', (e: JQuery.ClickEvent) => {
                        try {
                            OrderItemGridController.SubPOWorksheet(e);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                };
                FwMenu.addSubMenuItem($optionsgroup, 'Restore System Sorting', 'HEg0EHlwFNVr', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.restoreSystemSorting(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Header Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertHeaderLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Text Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertTextLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($sutotalinggroup, 'Insert Sub-Total Lines', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.insertSubTotalLines(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                const $viewcolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $viewgroup = FwMenu.addSubMenuGroup($viewcolumn, 'View', 'securityid2')
                FwMenu.addSubMenuItem($viewgroup, 'Summary View', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.detailSummaryView(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Shortages Only', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.shortagesOnly(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Rollup Quantities', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.rollup(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($viewgroup, 'Color Legend', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.colorLegend(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`)
                };
                request.totalfields = this.totalFields;
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('A');
                $fwgrid.find('.combined').attr('data-visible', 'true');
                $fwgrid.find('.individual').attr('data-visible', 'false');
                $combinedOrderItemGrid = $fwgrid;

            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.calculateOrderItemGridTotals($form, 'combined', dt.Totals);
            },
            onOverrideNotesTemplate: ($field, controlhtml, $confirmation, $browse, $tr, $ok) => {
                OrderItemGridController.addPrintNotes($field, controlhtml, $confirmation, $browse, $tr, $ok);
            },
        });
        // ----------
        let nodeActivity = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'hb52dbhX1mNLZ');
        if (nodeActivity !== undefined && nodeActivity.properties.visible === 'T') {
            FwBrowse.renderGrid({
                nameGrid: 'ActivityGrid',
                gridSecurityId: 'hb52dbhX1mNLZ',
                moduleSecurityId: this.id,
                $form: $form,
                onDataBind: (request: any) => {
                    request.uniqueids = {
                        OrderId: FwFormField.getValueByDataField($form, `${this.Module}Id`),
                        ShowShipping: FwFormField.getValueByDataField($form, 'ShowShipping')
                    };
                },
                beforeSave: (request: any) => {
                    request.OrderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);
                },
            });
        }
        // ----------
        //const itemGrids = [$orderItemGridRental, $orderItemGridSales, $orderItemGridLabor, $orderItemGridMisc];
        //if ($form.attr('data-mode') === 'NEW') {
        //    for (let i = 0; i < itemGrids.length; i++) {
        //        itemGrids[i].find('.btn').filter(function () { return jQuery(this).data('type') === 'NewButton' })
        //            .off()
        //            .on('click', () => {
        //                this.saveForm($form, { closetab: false });
        //            })
        //    }
        //}


        jQuery($form.find('.rentalgrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');
        jQuery($form.find('.salesgrid .valtype')).attr('data-validationname', 'SalesInventoryValidation');
        jQuery($form.find('.laborgrid .valtype')).attr('data-validationname', 'LaborRateValidation');
        jQuery($form.find('.miscgrid .valtype')).attr('data-validationname', 'MiscRateValidation');
        jQuery($form.find('.rentalsalegrid .valtype')).attr('data-validationname', 'RentalInventoryValidation');

        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');

        //if (this.Module === 'Quote') {
        //    //hide subworksheet and LD menu items
        //    const hideGridsBtn: JQuery[] = [$orderItemGridRental, $orderItemGridSales, $orderItemGridLabor, $orderItemGridMisc, $orderItemGridUsedSale, $combinedOrderItemGrid];
        //    this.findFilterHide(hideGridsBtn, '.submenu-btn', '[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]');
        //}
        //else if (this.Module === 'Order') {
        //    //hide LD menu items
        //    const hideGridsBtn: JQuery[] = [$orderItemGridRental, $orderItemGridSales, $orderItemGridLabor, $orderItemGridMisc, $combinedOrderItemGrid];
        //    this.findFilterHide(hideGridsBtn, '.submenu-btn', '[data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]');
        //    this.findFilterHide($orderItemGridUsedSale, '.submenu-btn', '[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="427FCDFE-7E42-4081-A388-150D3D7FAE36"], [data-securityid="78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412"]');

        //    //Hides non-LD menu items
        //    const $lossDamageGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
        //    this.findFilterHide($lossDamageGrid, '.submenu-btn', '[data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"], [data-securityid="AD3FB369-5A40-4984-8A65-46E683851E52"], [data-securityid="B6B68464-B95C-4A4C-BAF2-6AA59B871468"], [data-securityid="01EB96CB-6C62-4D5C-9224-8B6F45AD9F63"], [data-securityid="9476D532-5274-429C-A563-FE89F5B89B01"]');
        //}
    }
    //----------------------------------------------------------------------------------------------
    loadBrowseMenu($browse: JQuery) { };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let me = this;
        let $browse = jQuery(this.getBrowseTemplate());
        this.loadBrowseMenu($browse);
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

        FwAppData.apiMethod(true, 'GET', `${this.apiurl}/department/${department.departmentid}/location/${location.locationid}`, null, FwServices.defaultTimeout, response => {
            this.DefaultOrderType = response.DefaultOrderType;
            this.DefaultOrderTypeId = response.DefaultOrderTypeId;

            const request: any = {};
            request.uniqueids = {
                OrderTypeId: this.DefaultOrderTypeId,
                LocationId: location.locationid
            }

            if (this.DefaultOrderTypeId) {
                FwAppData.apiMethod(true, 'GET', `${this.apiurl}/ordertype/${this.DefaultOrderTypeId}`, null, FwServices.defaultTimeout, response => {
                    this.DefaultFromTime = response.DefaultFromTime;
                    this.DefaultToTime = response.DefaultToTime;
                    this.DefaultPickTime = response.DefaultPickTime;
                    this.AllowRoundTripRentals = response.AllowRoundTripRentals;
                }, ex => FwFunc.showError(ex), $browse);
            }

            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/ordertypelocation/browse`, request, FwServices.defaultTimeout,
                response => {
                    if (response.Rows.length > 0) {
                        this.DefaultTermsConditionsId = response.Rows[0][response.ColumnIndex.TermsConditionsId];
                        this.DefaultTermsConditions = response.Rows[0][response.ColumnIndex.TermsConditions];
                        this.DefaultCoverLetterId = response.Rows[0][response.ColumnIndex.CoverLetterId];
                        this.DefaultCoverLetter = response.Rows[0][response.ColumnIndex.CoverLetter];
                        this.DefaultPresentationLayerId = response.Rows[0][response.ColumnIndex.PresentationLayerId];
                        this.DefaultPresentationLayer = response.Rows[0][response.ColumnIndex.PresentationLayer];
                    }
                }, ex => FwFunc.showError(ex), $browse);
        }, ex => FwFunc.showError(ex), $browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    loadFormMenu($form: JQuery) { };
    //----------------------------------------------------------------------------------------------
    //justin hoffman 09/25/2020
    // this method is a single place where all of the activity Tabs are shown or hidden based on the Activity Checkboxes 
    showHideActivityTabs($form: JQuery) {
        if ($form.find('[data-datafield="Rental"] input').prop('checked')) {
            this.showTab($form, 'rentaltab');
            this.showTab($form, 'subrentaltab');
        } else {
            this.hideTab($form, 'rentaltab');
            this.hideTab($form, 'subrentaltab');
        }


        if ($form.find('[data-datafield="Sales"] input').prop('checked')) {
            this.showTab($form, 'salestab');
            this.showTab($form, 'subsalestab');
        } else {
            this.hideTab($form, 'salestab');
            this.hideTab($form, 'subsalestab');
        }


        if ($form.find('[data-datafield="Miscellaneous"] input').prop('checked')) {
            this.showTab($form, 'misctab');
            this.showTab($form, 'submisctab');
        } else {
            this.hideTab($form, 'misctab');
            this.hideTab($form, 'submisctab');
        }


        if ($form.find('[data-datafield="Labor"] input').prop('checked')) {
            this.showTab($form, 'labortab');
            this.showTab($form, 'sublabortab');
        } else {
            this.hideTab($form, 'labortab');
            this.hideTab($form, 'sublabortab');
        }


        if ($form.find('[data-datafield="RentalSale"] input').prop('checked')) {
            this.showTab($form, 'rentalsaletab');
        } else {
            this.hideTab($form, 'rentalsaletab');
        }

        // loss and damage (order only)
        if ($form.find('[data-datafield="LossAndDamage"]') != undefined) {
            if ($form.find('[data-datafield="LossAndDamage"] input').prop('checked')) {
                this.showTab($form, 'lossdamagetab');
            } else {
                this.hideTab($form, 'lossdamagetab');
            }
        }

    }
    //----------------------------------------------------------------------------------------------
    controlMutuallyExclusiveActivities($form: JQuery) {
        const rentalVal = FwFormField.getValueByDataField($form, 'Rental');
        const salesVal = FwFormField.getValueByDataField($form, 'Sales');
        const rentalSaleVal = FwFormField.getValueByDataField($form, 'RentalSale');
        const hasRentalSaleItem = FwFormField.getValueByDataField($form, 'HasRentalSaleItem');
        const hasLossAndDamageItem = FwFormField.getValueByDataField($form, 'HasLossAndDamageItem');
        let lossDamageVal: boolean = false;
        if (this.Module === 'Order') {
            lossDamageVal = FwFormField.getValueByDataField($form, 'LossAndDamage');
            if (rentalVal || salesVal || rentalSaleVal) {
                FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
            } else if (!rentalVal && !salesVal && !rentalSaleVal && !hasLossAndDamageItem) {
                FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
            }
        }
        if (rentalVal || lossDamageVal) {
            FwFormField.disable(FwFormField.getDataField($form, 'RentalSale'));
        } else if (!rentalVal && !lossDamageVal && !hasRentalSaleItem) {
            FwFormField.enable(FwFormField.getDataField($form, 'RentalSale'));
        }
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = jQuery(this.getFormTemplate());
        //FwTabs.hideTab($form.find('.emailhistorytab'));
        this.loadFormMenu($form);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.combinedtab').hide();

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            const department = JSON.parse(sessionStorage.getItem('department'));
            const office = JSON.parse(sessionStorage.getItem('location'));
            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));

            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            // Dates and Times
            const today = FwLocale.getDate();
            FwFormField.setValue($form, 'div[data-dateactivitytype="PICK"]', today);
            FwFormField.setValue($form, 'div[data-timeactivitytype="PICK"]', this.DefaultPickTime);
            FwFormField.setValue($form, 'div[data-dateactivitytype="START"]', today);
            FwFormField.setValue($form, 'div[data-timeactivitytype="START"]', this.DefaultFromTime);
            FwFormField.setValue($form, 'div[data-dateactivitytype="STOP"]', today);
            FwFormField.setValue($form, 'div[data-timeactivitytype="STOP"]', this.DefaultToTime);

            FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
            FwFormField.setValueByDataField($form, 'BillingMonths', '0');
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', office.defaultcurrencyid, office.defaultcurrencycode);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);
            this.defaultBillQuantities($form);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', controlDefaults.defaultdealbillingcycleid, controlDefaults.defaultdealbillingcycle);
            FwFormField.setValue($form, 'div[data-datafield="TermsConditionsId"]', this.DefaultTermsConditionsId, this.DefaultTermsConditions);
            FwFormField.setValue($form, 'div[data-datafield="CoverLetterId"]', this.DefaultCoverLetterId, this.DefaultCoverLetter);
            FwFormField.setValue($form, 'div[data-datafield="PresentationLayerId"]', this.DefaultPresentationLayerId, this.DefaultPresentationLayer);
            FwFormField.setValue($form, 'div[data-datafield="PendingPo"]', true);
            FwFormField.setValueByDataField($form, 'RoundTripRentals', this.AllowRoundTripRentals);
            // Dynamic set value for user's department default activities
            const defaultActivities = department.activities;
            if (defaultActivities) {
                for (let i = 0; i < defaultActivities.length; i++) {
                    if (defaultActivities[i] === 'Rental' || defaultActivities[i] === 'Sales' || defaultActivities[i] === 'Labor' || defaultActivities[i] === 'Miscellaneous')
                        FwFormField.setValueByDataField($form, `${defaultActivities[i]}`, true);
                }
            }

            //// show/hide tabs based on Activity boxes checked
            //const $rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]');
            //const $subRentalTab = $form.find('[data-type="tab"][data-caption="Sub-Rental"]');
            //if ($form.find('[data-datafield="Rental"] input').prop('checked')) {
            //    $rentalTab.show();
            //    $subRentalTab.show();
            //} else {
            //    $rentalTab.hide();
            //    $subRentalTab.hide();
            //}

            //const $salesTab = $form.find('[data-type="tab"][data-caption="Sales"]');
            //const $subSalesTab = $form.find('[data-type="tab"][data-caption="Sub-Sales"]');
            //if ($form.find('[data-datafield="Sales"] input').prop('checked')) {
            //    $salesTab.show();
            //    $subSalesTab.show();
            //} else {
            //    $salesTab.hide();
            //    $subSalesTab.hide();
            //}

            //const $miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]');
            //const $subMiscTab = $form.find('[data-type="tab"][data-caption="Sub-Miscellaneous"]');
            //if ($form.find('[data-datafield="Miscellaneous"] input').prop('checked')) {
            //    $miscTab.show();
            //    $subMiscTab.show();
            //} else {
            //    $miscTab.hide();
            //    $subMiscTab.hide();
            //}

            //const $laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
            //const $subLaborTab = $form.find('[data-type="tab"][data-caption="Sub-Labor"]');
            //if ($form.find('[data-datafield="Labor"] input').prop('checked')) {
            //    $laborTab.show();
            //    $subLaborTab.show();
            //} else {
            //    $laborTab.hide();
            //    $subLaborTab.hide();
            //}

            //const $rentalSaleTab = $form.find('[data-type="tab"].rentalsaletab');
            //if ($form.find('[data-datafield="RentalSale"] input').prop('checked')) {
            //    $rentalSaleTab.show();
            //} else {
            //    $rentalSaleTab.hide();
            //}

            // show/hide tabs based on Activity boxes checked
            this.showHideActivityTabs($form);
            this.controlMutuallyExclusiveActivities($form);

            /*
            FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
            //$form.find('[data-type="tab"].rentalsaletab').hide();
            this.hideTab($form, 'rentalsaletab');
*/

            FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            FwFormField.disable($form.find('[data-datafield="PoAmount"]'));

            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();

            FwFormField.setValueByDataField($form, 'RateType', office.ratetype, office.ratetypedisplay);
            this.getScheduleDatesByOrderType($form);
        }

        let nodeEmailHistory = FwApplicationTree.getNodeById(FwApplicationTree.tree, '3XHEm3Q8WSD8z');
        if (nodeEmailHistory !== undefined && nodeEmailHistory.properties.visible === 'T') {
            FwTabs.showTab($form.find('.emailhistorytab'));
            let $emailHistorySubModuleBrowse = this.openEmailHistoryBrowse($form);
            $form.find('.emailhistory-page').append($emailHistorySubModuleBrowse);
        }

        FwFormField.loadItems($form.find('div[data-datafield="totalTypeSubRental"]'), [
            { value: 'W', caption: 'Weekly' },
            { value: 'M', caption: 'Monthly' },
            { value: 'P', caption: 'Period' }
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="OutDeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Deliver to Customer' },
            { value: 'SHIP', text: 'Ship to Customer' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="InDeliveryDeliveryType"]'), [
            { value: 'DELIVER', text: 'Customer Deliver' },
            { value: 'SHIP', text: 'Customer Ship' },
            { value: 'PICK UP', text: 'Pick Up from Customer' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="OutDeliveryOnlineOrderStatus"]'), [
            { value: '', text: '' },
            { value: 'PARTIAL', text: 'Partial' },
            { value: 'COMPLETE', text: 'Complete' }
        ], true);

        FwFormField.setValue($form, 'div[data-datafield="ShowShipping"]', true);  //justin hoffman 03/12/2020 - this is temporary until the Shipping tab is updated

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
        if ($form.attr('data-mode') === 'NEW') {
            FwFormField.setValueByDataField($form, 'DetermineQuantitiesToBillBasedOn', 'CONTRACT');
        }

        //Toggle Buttons - Billing tab - Labor Prep Fees
        FwFormField.loadItems($form.find('div[data-datafield="IncludePrepFeesInRentalRate"]'), [
            { value: 'false', caption: 'As Labor Charge' },
            { value: 'true', caption: 'Into Rental Rate' }
        ]);
        if ($form.attr('data-mode') === 'NEW') {
            FwFormField.setValueByDataField($form, 'IncludePrepFeesInRentalRate', 'false');
        }

        //Toggle Buttons - Billing tab - Hiatus Schedule
        FwFormField.loadItems($form.find('div[data-datafield="HiatusDiscountFrom"]'), [
            { value: 'DEAL', caption: 'Deal' },
            { value: 'ORDER', caption: 'Order' }
        ]);
        if ($form.attr('data-mode') === 'NEW') {
            FwFormField.setValueByDataField($form, 'HiatusDiscountFrom', 'DEAL');
        }

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

        if (typeof parentModuleInfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="DealId"]', parentModuleInfo.DealId, parentModuleInfo.Deal);
            FwFormField.setValue($form, 'div[data-datafield="RateType"]', parentModuleInfo.RateTypeId, parentModuleInfo.RateType);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', parentModuleInfo.BillingCycleId, parentModuleInfo.BillingCycle);
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', parentModuleInfo.CurrencyId, parentModuleInfo.CurrencyCode);
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', parentModuleInfo.PaymentTermsId, parentModuleInfo.PaymentTerms);
            FwFormField.setValue($form, 'div[data-datafield="PaymentTypeId"]', parentModuleInfo.PaymentTypeId, parentModuleInfo.PaymentType);
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', parentModuleInfo.DealNumber, parentModuleInfo.DealNumber);
            FwFormField.setValueByDataField($form, 'CustomerId', parentModuleInfo.CustomerId, parentModuleInfo.Customer);
            FwFormField.setValueByDataField($form, 'CustomerNumber', parentModuleInfo.CustomerNumber);

            FwFormField.setValueByDataField($form, 'PrintIssuedToAddressFrom', parentModuleInfo.BillToAddressType);
            if (parentModuleInfo.BillToAddressType === 'DEAL') {
                FwFormField.setValueByDataField($form, `IssuedToName`, parentModuleInfo.Deal);
            } else if (parentModuleInfo.BillToAddressType === 'CUSTOMER') {
                FwFormField.setValueByDataField($form, `IssuedToName`, parentModuleInfo.Customer);
            }
            FwFormField.setValueByDataField($form, 'IssuedToAttention', parentModuleInfo.BillToAttention1);
            FwFormField.setValueByDataField($form, 'IssuedToAttention2', parentModuleInfo.BillToAttention2);
            FwFormField.setValueByDataField($form, 'IssuedToAddress1', parentModuleInfo.BillToAddress1);
            FwFormField.setValueByDataField($form, 'IssuedToAddress2', parentModuleInfo.BillToAddress2);
            FwFormField.setValueByDataField($form, 'IssuedToCity', parentModuleInfo.BillToCity);
            FwFormField.setValueByDataField($form, 'IssuedToState', parentModuleInfo.BillToState);
            FwFormField.setValueByDataField($form, 'IssuedToZipCode', parentModuleInfo.BillToZipCode);
            FwFormField.setValueByDataField($form, 'IssuedToCountryId', parentModuleInfo.BillToCountryId, parentModuleInfo.BillToCountry);


            FwFormField.setValueByDataField($form, 'OutDeliveryDeliveryType', parentModuleInfo.DefaultOutgoingDeliveryType);
            FwFormField.setValueByDataField($form, 'InDeliveryDeliveryType', parentModuleInfo.DefaultIncomingDeliveryType);
            if (parentModuleInfo.DefaultOutgoingDeliveryType === 'DELIVER' || parentModuleInfo.DefaultOutgoingDeliveryType === 'SHIP') {
                FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'DEAL');
                this.fillDeliveryAddressFieldsforDeal($form, 'Out');
            }
            else if (parentModuleInfo.DefaultOutgoingDeliveryType === 'PICK UP') {
                FwFormField.setValueByDataField($form, 'OutDeliveryAddressType', 'WAREHOUSE');
                this.getWarehouseAddress($form, 'Out');
            }

            if (parentModuleInfo.DefaultIncomingDeliveryType === 'DELIVER' || parentModuleInfo.DefaultIncomingDeliveryType === 'SHIP') {
                FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'WAREHOUSE');
                this.getWarehouseAddress($form, 'In');
            }
            else if (parentModuleInfo.DefaultIncomingDeliveryType === 'PICK UP') {
                FwFormField.setValueByDataField($form, 'InDeliveryAddressType', 'DEAL');
                this.fillDeliveryAddressFieldsforDeal($form, 'In');
            }
        }

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
        const $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', e => {
            this.printQuoteOrder($form);
        });
    }
    //----------------------------------------------------------------------------------------------
    renderSearchButton($form: any) {
        const $search = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'QuikSearch', 'searchbtn');
        $search.prepend('<i class="material-icons">search</i>');
        $search.on('click', e => {
            try {
                const $form = jQuery(e.currentTarget).closest('.fwform');
                const orderId = FwFormField.getValueByDataField($form, `${this.Module}Id`);

                if (orderId == "") {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else if (!jQuery(e.currentTarget).hasClass('disabled')) {
                    const search = new SearchInterface();
                    search.renderSearchPopup($form, orderId, this.Module);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    //renderFrames($form: any, cachedId?, period?) {
    loadProfitAndLoss($form: any) {

        const period = FwFormField.getValueByDataField($form, 'totalTypeProfitLoss');
        //this.renderFrames($form, FwFormField.getValueByDataField($form, `${this.Module}Id`), period);


        FwFormField.disable($form.find('.frame'));
        let id = FwFormField.getValueByDataField($form, `${this.Module}Id`);
        $form.find('.frame input').css('width', '100%');
        //if (typeof cachedId !== 'undefined' && cachedId !== null) {
        //    id = cachedId;
        //}
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
        $form.find('[data-datafield="Rental"] input').on('change', e => {
            //const $rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]');
            //const $subRentalTab = $form.find('[data-type="tab"][data-caption="Sub-Rental"]');

            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    //$rentalTab.show();
                    //$subRentalTab.show();
                    this.showTab($form, 'rentaltab');
                    this.showTab($form, 'subrentaltab');
                    $form.find('.rental-pl').show()
                    FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                } else {
                    //$rentalTab.hide();
                    //$subRentalTab.hide();
                    this.hideTab($form, 'rentaltab');
                    this.hideTab($form, 'subrentaltab');
                    $form.find('.rental-pl').hide();
                    FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                }
            } else {
                const combineActivity = FwFormField.getValueByDataField($form, 'CombineActivity');
                if (!combineActivity) {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        //$rentalTab.show();
                        //$subRentalTab.show();
                        this.showTab($form, 'rentaltab');
                        this.showTab($form, 'subrentaltab');
                        $form.find('.rental-pl').show();
                        FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
                    } else {
                        //$rentalTab.hide();
                        //$subRentalTab.hide();
                        this.hideTab($form, 'rentaltab');
                        this.hideTab($form, 'subrentaltab');
                        $form.find('.rental-pl').hide();
                        FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                    }
                }
            }
        });
        $form.find('[data-datafield="Sales"] input').on('change', e => {
            //const $salesTab = $form.find('[data-type="tab"][data-caption="Sales"]');
            //const $subSalesTab = $form.find('[data-type="tab"][data-caption="Sub-Sales"]');

            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    //$salesTab.show();
                    //$subSalesTab.show();
                    this.showTab($form, 'salestab');
                    this.showTab($form, 'subsalestab');
                    $form.find('.sales-pl').show();
                } else {
                    //$salesTab.hide();
                    //$subSalesTab.hide();
                    this.hideTab($form, 'salestab');
                    this.hideTab($form, 'subsalestab');
                    $form.find('.sales-pl').hide();
                }
            } else {
                const combineActivity = FwFormField.getValueByDataField($form, 'CombineActivity');
                if (!combineActivity) {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        //$salesTab.show();
                        //$subSalesTab.show();
                        this.showTab($form, 'salestab');
                        this.showTab($form, 'subsalestab');
                        $form.find('.sales-pl').show();
                    } else {
                        //$salesTab.hide();
                        //$subSalesTab.hide();
                        this.hideTab($form, 'salestab');
                        this.hideTab($form, 'subsalestab');
                        $form.find('.sales-pl').hide();
                    }
                }
            }
        });
        $form.find('[data-datafield="Miscellaneous"] input').on('change', e => {
            //const $miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]');
            //const $subMiscTab = $form.find('[data-type="tab"][data-caption="Sub-Miscellaneous"]');

            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    //$miscTab.show();
                    //$subMiscTab.show();
                    this.showTab($form, 'misctab');
                    this.showTab($form, 'submisctab');
                    $form.find('.misc-pl').show();
                } else {
                    //$miscTab.hide();
                    //$subMiscTab.hide();
                    this.hideTab($form, 'misctab');
                    this.hideTab($form, 'submisctab');
                    $form.find('.misc-pl').hide();
                }
            } else {
                const combineActivity = FwFormField.getValueByDataField($form, 'CombineActivity');
                if (!combineActivity) {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        //$miscTab.show();
                        //$subMiscTab.show();
                        this.showTab($form, 'misctab');
                        this.showTab($form, 'submisctab');
                        $form.find('.misc-pl').show();
                    } else {
                        //$miscTab.hide();
                        //$subMiscTab.hide();
                        this.hideTab($form, 'misctab');
                        this.hideTab($form, 'submisctab');
                        $form.find('.misc-pl').hide();
                    }
                }
            }
        });

        $form.find('[data-datafield="LossAndDamage"] input').on('change', e => {
            //const $lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss & Damage"]');

            if (jQuery(e.currentTarget).prop('checked')) {
                //$lossDamageTab.show();
                this.showTab($form, 'lossdamagetab');
                $form.find('[data-securityid="searchbtn"]').addClass('disabled');
                //$form.find(`.submenu-btn[data-securityid="${quikSearchMenuId}"]`).attr('data-enabled', 'false');
                FwFormField.disable($form.find('[data-datafield="Rental"]'));
                FwFormField.disable($form.find('[data-datafield="Sales"]'));
                FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
            } else {
                //$lossDamageTab.hide();
                this.hideTab($form, 'lossdamagetab');
                console.log('in change b4: ', $form.data('antiLD'))
                $form.find('[data-securityid="searchbtn"]').removeClass('disabled');

                FwFormField.enable($form.find('[data-datafield="Rental"]'));
                FwFormField.enable($form.find('[data-datafield="Sales"]'));
                FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                $form.data('antiLD', null);
                console.log('inchange after null: ', $form.data('antiLD'))
            }
        });
        // Determine previous values for enabled / disabled checkboxes
        $form.find('[data-datafield="LossAndDamage"]').click(e => {
            // e.stopImmediatePropagation();
            const LossAndDamageVal = FwFormField.getValueByDataField($form, 'LossAndDamage')
            if (LossAndDamageVal === false) {
                const salesEnabled = $form.find('[data-datafield="Sales"]').attr('data-enabled');
                const rentalEnabled = $form.find('[data-datafield="Rental"]').attr('data-enabled');
                const rentalSalesEnabled = $form.find('[data-datafield="RentalSale"]').attr('data-enabled');
                $form.data('antiLD', {
                    "salesEnabled": salesEnabled,
                    "rentalEnabled": rentalEnabled,
                    "rentalSalesEnabled": rentalSalesEnabled
                });
            }
        });
        $form.find('[data-datafield="Labor"] input').on('change', e => {
            //const $laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
            //const $subLaborTab = $form.find('[data-type="tab"][data-caption="Sub-Labor"]');

            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    //$laborTab.show();
                    //$subLaborTab.show();
                    this.showTab($form, 'labortab');
                    this.showTab($form, 'sublabortab');
                    $form.find('.labor-pl').show();
                } else {
                    //$laborTab.hide();
                    //$subLaborTab.hide();
                    this.hideTab($form, 'labortab');
                    this.hideTab($form, 'sublabortab');
                    $form.find('.labor-pl').hide();
                }
            } else {
                const combineActivity = FwFormField.getValueByDataField($form, 'CombineActivity');
                if (!combineActivity) {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        //$laborTab.show();
                        //$subLaborTab.show();
                        this.showTab($form, 'labortab');
                        this.showTab($form, 'sublabortab');
                        $form.find('.labor-pl').show();
                    } else {
                        //$laborTab.hide();
                        //$subLaborTab.hide();
                        this.hideTab($form, 'labortab');
                        this.hideTab($form, 'sublabortab');
                        $form.find('.labor-pl').hide();
                    }
                }
            }
        });

        $form.find('[data-datafield="RentalSale"] input').on('change', e => {
            //const $rentalSaleTab = $form.find('[data-type="tab"].rentalsaletab');

            if (mode == "NEW") {
                if (jQuery(e.currentTarget).prop('checked')) {
                    //$rentalSaleTab.show();
                    this.showTab($form, 'rentalsaletab');
                    $form.find('.rentalsale-pl').show();
                    FwFormField.disable($form.find('[data-datafield="Rental"]'));
                } else {
                    //$rentalSaleTab.hide();
                    this.hideTab($form, 'rentalsaletab');
                    $form.find('.rentalsale-pl').hide();
                    FwFormField.enable($form.find('[data-datafield="Rental"]'));
                }
            } else {
                const combineActivity = FwFormField.getValueByDataField($form, 'CombineActivity');
                if (!combineActivity) {
                    if (jQuery(e.currentTarget).prop('checked')) {
                        //$rentalSaleTab.show();
                        this.showTab($form, 'rentalsaletab');
                        $form.find('.rentalsale-pl').show();
                        FwFormField.disable($form.find('[data-datafield="Rental"]'));
                    } else {
                        //$rentalSaleTab.hide();
                        this.hideTab($form, 'rentalsaletab');
                        $form.find('.rentalsale-pl').hide();
                        FwFormField.enable($form.find('[data-datafield="Rental"]'));
                    }
                }
            }
        });
        // Loss and Damage disable against Rental, Sales, Rental Sale
        // Also in AfterLoad
        $form.find('.anti-LD').on('change', e => {
            const rentalVal = FwFormField.getValueByDataField($form, 'Rental');
            const salesVal = FwFormField.getValueByDataField($form, 'Sales');
            const rentalSaleVal = FwFormField.getValueByDataField($form, 'RentalSale');
            if (rentalVal === true || salesVal === true || rentalSaleVal === true) {
                FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
            } else if (rentalVal === false && salesVal === false && rentalSaleVal === false) {
                FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    copyOrderOrQuote($form: any) {
        const module = this.Module;
        const $confirmation = FwConfirmation.renderConfirmation(`Copy ${module}`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '550px');
        const html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="" style="width:120px;float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="" style="width:400px; float:left;"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No" data-datafield="" style="width:120px; float:left;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="width:400px;float:left;"></div>');
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
            request.WarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
            request.LocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
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
    //beforeValidateOutShipVia($browse: any, $grid: any, request: any) {
    //    let validationName = request.module,
    //        outDeliveryCarrierId = jQuery($form.find('[data-datafield="OutDeliveryCarrierId"] input')).val();
    //    switch (validationName) {
    //        case 'ShipViaValidation':
    //            request.uniqueids = {
    //                VendorId: outDeliveryCarrierId
    //            };
    //            break;
    //    }
    //};
    //beforeValidateInShipVia($browse: any, $grid: any, request: any) {
    //    let validationName = request.module;
    //    let inDeliveryCarrierId = jQuery($form.find('[data-datafield="InDeliveryCarrierId"] input')).val();
    //    switch (validationName) {
    //        case 'ShipViaValidation':
    //            request.uniqueids = {
    //                VendorId: inDeliveryCarrierId
    //            };
    //            break;
    //    }
    //};
    //beforeValidateCarrier($browse: any, $grid: any, request: any) {
    //    let validationName = request.module;
    //    switch (validationName) {
    //        case 'VendorValidation':
    //            request.uniqueids = {
    //                Freight: true
    //            };
    //            break;
    //    }
    //};
    //beforeValidateDeal($browse: any, $grid: any, request: any) {
    //    const $form = $grid.closest('.fwform');
    //    const shareDealsAcrossOfficeLocations = JSON.parse(sessionStorage.getItem('controldefaults')).sharedealsacrossofficelocations;
    //    if (!shareDealsAcrossOfficeLocations) {
    //        const officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
    //        request.uniqueids = {
    //            LocationId: officeLocationId
    //        }
    //    }
    //};
    //beforeValidateMarketSegment($browse: any, $grid: any, request: any) {
    //    const validationName = request.module;
    //    const marketTypeValue = jQuery($grid.find('[data-validationname="MarketTypeValidation"] input')).val();
    //    const marketSegmentValue = jQuery($grid.find('[data-validationname="MarketSegmentValidation"] input')).val();
    //    switch (validationName) {
    //        case 'MarketSegmentValidation':
    //            if (marketTypeValue !== "") {
    //                request.uniqueids = {
    //                    MarketTypeId: marketTypeValue,
    //                };
    //                break;
    //            }
    //        case 'MarketSegmentJobValidation':
    //            if (marketSegmentValue !== "") {
    //                request.uniqueids = {
    //                    MarketTypeId: marketTypeValue,
    //                    MarketSegmentId: marketSegmentValue,
    //                };
    //                break;
    //            }
    //    };
    //};
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'DealId':
                const shareDealsAcrossOfficeLocations = JSON.parse(sessionStorage.getItem('controldefaults')).sharedealsacrossofficelocations;
                if (!shareDealsAcrossOfficeLocations) {
                    const officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
                    request.uniqueids = {
                        LocationId: officeLocationId
                    }
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'RateType':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateratetype`);
                break;
            case 'OrderTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateordertype`);
                break;
            case 'AgentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateagent`);
                break;
            case 'ProjectManagerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprojectmanager`);
                break;
            case 'OutsideSalesRepresentativeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateoutsidesalesrepresentative`);
                break;
            case 'MarketTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemarkettype`);
                break;
            case 'MarketSegmentId':
                const marketTypeValue = FwFormField.getValueByDataField($form, 'MarketTypeId');
                if (marketTypeValue !== "") {
                    request.uniqueids = {
                        MarketTypeId: marketTypeValue
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemarketsegment`);
                break;
            case 'MarketSegmentJobId':
                const marketTypeId = FwFormField.getValueByDataField($form, 'MarketTypeId');
                const marketSegmentId = FwFormField.getValueByDataField($form, 'MarketSegmentId');
                if (marketTypeId !== "" && marketSegmentId !== "") {
                    request.uniqueids = {
                        MarketTypeId: marketTypeId,
                        MarketSegmentId: marketSegmentId
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemarketsegmentjob`);
                break;
            case 'CoverLetterId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecoverletter`);
                break;
            case 'TermsConditionsId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetermsconditions`);
                break;
            case 'BillingCycleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebillingcycle`);
                break;
            case 'PaymentTermsId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymentterms`);
                break;
            case 'PaymentTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymenttype`);
                break;
            case 'CurrencyId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecurrency`);
                break;
            case 'TaxOptionId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetaxoption`);
                break;
            case 'DiscountReasonId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatediscountreason`);
                break;
            case 'IssuedToCountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateissuedtocountry`);
                break;
            case 'OutDeliveryCarrierId':
                request.uniqueids = {
                    Freight: true
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateoutdeliverycarrier`);
                break;
            case 'OutDeliveryShipViaId':
                let outDeliveryCarrierId = jQuery($form.find('[data-datafield="OutDeliveryCarrierId"] input')).val();
                request.uniqueids = {
                    VendorId: outDeliveryCarrierId
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateoutdeliveryshipvia`);
                break;
            case 'InDeliveryCarrierId':
                request.uniqueids = {
                    Freight: true
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateindeliverycarrier`);
                break;
            case 'InDeliveryShipViaId':
                let inDeliveryCarrierId = jQuery($form.find('[data-datafield="InDeliveryCarrierId"] input')).val();
                request.uniqueids = {
                    VendorId: inDeliveryCarrierId
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateindeliveryshipvia`);
                break;
            case 'OutDeliveryToCountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateoutdeliverytocountry`);
                break;
            case 'InDeliveryToCountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateindeliverytocountry`);
                break;
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'WarehouseId':
                const locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
                if (locationId) {
                    request.uniqueids.LocationId = locationId;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'BillToCountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebilltocountry`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        let dealObj: any = {}, departmentObj: any = {};
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
        //06/19/2020 - the buttom code causes a bug and allows the event to fire twice
        //$form.find('.bottom_line_total_tax').on('keyup', event => {
        //    if (event.which === 13) {
        //        this.bottomLineTotalWithTaxChange($form, event);
        //    }
        //});
        // Bottom Line Discount
        $form.find('.bottom_line_discount').on('change', event => {
            this.bottomLineDiscountChange($form, event);
        });
        // Hiatus toggle on Billing tab
        $form.find('div[data-datafield="HiatusDiscountFrom"]').on('change', e => {
            const hiatusDiscountFrom = FwFormField.getValueByDataField($form, 'HiatusDiscountFrom');
            if (hiatusDiscountFrom === 'DEAL') {
                $form.find('div[data-grid="DealHiatusDiscountGrid"]').show();
                $form.find('div[data-grid="OrderHiatusDiscountGrid"]').hide();
            } else {
                $form.find('div[data-grid="OrderHiatusDiscountGrid"]').show();
                $form.find('div[data-grid="DealHiatusDiscountGrid"]').hide();
            }
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

            const displayConfirmation = () => {
                const $confirmation = FwConfirmation.renderConfirmation('Rate Change', `Changing the Rate will automatically update all pricing when this ${this.Module} is saved.`);
                FwConfirmation.addButton($confirmation, 'Ok', true);
            }

            if ($form.data('hasitems')) { //from initial load
                displayConfirmation();
            } else { //check if items have been added
                const $grids = $form.find('[data-name="OrderItemGrid"]');
                for (let i = 0; i < $grids.length; i++) {
                    const totalRows = jQuery($grids[i]).data('totalRowCount') || 0;
                    if (totalRows) {
                        $form.data('hasitems', true);
                        displayConfirmation();
                        break;
                    }
                }
            }
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
        $form.on('changeDate', '.pick-date-validation', event => {
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
            const $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.differentaddress'));
            }
            else {
                FwFormField.disable($form.find('.differentaddress'));
            }
        });
        // ----------
        $form.find('div[data-datafield="OrderTypeId"]').on('change', event => {
            this.renderGrids($form); // Reinstantiating grids because of the substantial effects that the Order Type can have
            this.applyOrderTypeAndRateTypeToForm($form);
            this.getScheduleDatesByOrderType($form);
            this.applyOrderTypeDefaults($form);
        });
        // ----------
        $form.find('[data-datafield="NoCharge"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
            }
        });
        // ----------
        $form.find('.addresscopy').on('click', e => {
            const $confirmation = FwConfirmation.renderConfirmation('Confirm Copy', '');
            const html: Array<string> = [];
            html.push('<div class="flexrow">Copy Outgoing Address into Incoming Address?</div>');
            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', copyAddress);
            function copyAddress() {

                if (FwFormField.getValueByDataField($form, 'OutDeliveryAddressType') === 'DEAL') {
                    $form.find('div[data-datafield="InDeliveryAddressType"] .togglebutton-item input[value="DEAL"]').click();
                }
                if (FwFormField.getValueByDataField($form, 'OutDeliveryAddressType') === 'VENUE') {
                    $form.find('div[data-datafield="InDeliveryAddressType"] .togglebutton-item input[value="VENUE"]').click();
                    FwFormField.setValueByDataField($form, 'InDeliveryToVenueId', FwFormField.getValueByDataField($form, 'OutDeliveryToVenueId'), FwFormField.getTextByDataField($form, 'OutDeliveryToVenueId'));
                }
                if (FwFormField.getValueByDataField($form, 'OutDeliveryAddressType') === 'WAREHOUSE') {
                    $form.find('div[data-datafield="InDeliveryAddressType"] .togglebutton-item input[value="WAREHOUSE"]').click();
                }
                if (FwFormField.getValueByDataField($form, 'OutDeliveryAddressType') === 'OTHER') {
                    $form.find('div[data-datafield="InDeliveryAddressType"] .togglebutton-item input[value="OTHER"]').click();
                }

                FwFormField.setValueByDataField($form, 'InDeliveryToLocation', FwFormField.getValueByDataField($form, 'OutDeliveryToLocation'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAttention', FwFormField.getValueByDataField($form, 'OutDeliveryToAttention'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAddress1', FwFormField.getValueByDataField($form, 'OutDeliveryToAddress1'));
                FwFormField.setValueByDataField($form, 'InDeliveryToAddress2', FwFormField.getValueByDataField($form, 'OutDeliveryToAddress2'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCity', FwFormField.getValueByDataField($form, 'OutDeliveryToCity'));
                FwFormField.setValueByDataField($form, 'InDeliveryToState', FwFormField.getValueByDataField($form, 'OutDeliveryToState'));
                FwFormField.setValueByDataField($form, 'InDeliveryToZipCode', FwFormField.getValueByDataField($form, 'OutDeliveryToZipCode'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCountryId', FwFormField.getValueByDataField($form, 'OutDeliveryToCountryId'), FwFormField.getTextByDataField($form, 'OutDeliveryToCountryId'));
                FwFormField.setValueByDataField($form, 'InDeliveryToCrossStreets', FwFormField.getValueByDataField($form, 'OutDeliveryToCrossStreets'));
                FwNotification.renderNotification('SUCCESS', 'Address Successfully Copied.');
                FwConfirmation.destroyConfirmation($confirmation);
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

        // ----------
        $form.find('[data-datafield="DepartmentId"]').on('change', e => {
            departmentObj = {
                Name: FwFormField.getValueByDataField($form, 'Department'),
                Id: $form.find('[data-datafield="DepartmentId"]').attr('data-originalvalue')
            }
        });

        $form.find('div[data-datafield="DepartmentId"]').data('onchange', $tr => {
            const hasContracts = FwFormField.getValueByDataField($form, 'HasContracts');
            const hasInvoices = FwFormField.getValueByDataField($form, 'HasInvoices');
            const hasSubPurchaseOrders = FwFormField.getValueByDataField($form, 'HasSubPurchaseOrders');
            const hasMultiOrderContracts = FwFormField.getValueByDataField($form, 'HasMultiOrderContracts');
            const hasMultiOrderInvoices = FwFormField.getValueByDataField($form, 'HasMultiOrderInvoices');
            const hasSuspendedContracts = FwFormField.getValueByDataField($form, 'HasSuspendedContracts');

            if (hasMultiOrderContracts) {
                FwNotification.renderNotification('WARNING', 'The Department cannot be changed because Multi-Order Contracts exist.');
                FwFormField.setValueByDataField($form, 'DepartmentId', departmentObj.Id, departmentObj.Name);
            }
            if (hasMultiOrderInvoices) {
                FwNotification.renderNotification('WARNING', 'The Department cannot be changed because Multi-Order Invoices exist.');
                FwFormField.setValueByDataField($form, 'DepartmentId', departmentObj.Id, departmentObj.Name);
                return false;
            }
            if (hasSuspendedContracts) {
                FwNotification.renderNotification('WARNING', 'The Department cannot be changed because Suspended Contracts exist.');
                FwFormField.setValueByDataField($form, 'DepartmentId', departmentObj.Id, departmentObj.Name);
                return false;
            }

            if (hasContracts || hasInvoices || hasSubPurchaseOrders) {
                this.changeDepartmentForOrder($form, $tr, departmentObj);
            } else {
                this.defaultFieldsOnDepartmentChange($form, $tr);
            }
        });

        $form.find('[data-datafield="DealId"]').on('change', e => {
            dealObj = {
                Name: FwFormField.getValueByDataField($form, 'Deal'),
                Id: $form.find('[data-datafield="DealId"]').attr('data-originalvalue')
            }
        });

        //Defaults Address information when user selects a deal
        $form.find('[data-datafield="DealId"]').data('onchange', $tr => {
            const hasContracts = FwFormField.getValueByDataField($form, 'HasContracts');
            const hasInvoices = FwFormField.getValueByDataField($form, 'HasInvoices');
            const hasSubPurchaseOrders = FwFormField.getValueByDataField($form, 'HasSubPurchaseOrders');
            const hasMultiOrderContracts = FwFormField.getValueByDataField($form, 'HasMultiOrderContracts');
            const hasMultiOrderInvoices = FwFormField.getValueByDataField($form, 'HasMultiOrderInvoices');
            const hasSuspendedContracts = FwFormField.getValueByDataField($form, 'HasSuspendedContracts');

            if (hasMultiOrderContracts) {
                FwNotification.renderNotification('WARNING', 'The Deal cannot be changed because Multi-Order Contracts exist.');
                FwFormField.setValueByDataField($form, 'DealId', dealObj.Id, dealObj.Name);
            }
            if (hasMultiOrderInvoices) {
                FwNotification.renderNotification('WARNING', 'The Deal cannot be changed because Multi-Order Invoices exist.');
                FwFormField.setValueByDataField($form, 'DealId', dealObj.Id, dealObj.Name);
                return false;
            }
            if (hasSuspendedContracts) {
                FwNotification.renderNotification('WARNING', 'The Deal cannot be changed because Suspended Contracts exist.');
                FwFormField.setValueByDataField($form, 'DealId', dealObj.Id, dealObj.Name);
                return false;
            }

            if (hasContracts || hasInvoices || hasSubPurchaseOrders) {
                this.changeDealForOrder($form, $tr, dealObj);
            } else {
                this.defaultFieldsOnDealChange($form, $tr);
            }
        });

        // Out / In DeliveryType radio in Deliver tab
        $form.find('.delivery-type-radio').on('change', event => {
            this.deliveryTypeAddresses($form, event);
        });
        $form.find('div[data-datafield="InDeliveryToVenueId"]').data('onchange', event => {
            this.fillDeliveryAddressFieldsforVenue($form, 'In');
        });
        $form.find('div[data-datafield="OutDeliveryToVenueId"]').data('onchange', event => {
            this.fillDeliveryAddressFieldsforVenue($form, 'Out');
        });
        $form.find('.delivery-type-radio').on('change', event => {
            this.deliveryTypeAddresses($form, event);
        });
        // Quote Address - Issue To radio -  Billing
        $form.find('[data-datafield="PrintIssuedToAddressFrom"]').on('change', event => {
            this.issueToAddresses($form, event);
        });
        // Stores previous value for Out / InDeliveryDeliveryType
        $form.find('.delivery-delivery').on('click', event => {
            const $element = jQuery(event.currentTarget);
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
                $form.find('div[data-datafield="OutDeliveryAddressType"]').change();
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
                $form.find('div[data-datafield="InDeliveryAddressType"]').change();
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

        //Print Incoming Shipping Label
        $form.find('.prnt-label-in').on('click', e => {
            try {
                this.printShippingLabel($form, 'Incoming');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //Print Outgoing Shipping Label
        $form.find('.prnt-label-out').on('click', e => {
            try {
                this.printShippingLabel($form, 'Outgoing');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //Print Delivery Instructions
        $form.find('.prnt-deliv-instruct').on('click', e => {
            try {
                let tag = 'Out';
                const $this = jQuery(e.currentTarget);
                if ($this.hasClass('in')) {
                    tag = 'In';
                }
                this.printDeliveryInstructions($form, tag);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //profit & loss toggle buttons
        $form.find('.profit-loss-total input').off('change').on('change', e => {
            //const period = FwFormField.getValueByDataField($form, 'totalTypeProfitLoss');
            //this.renderFrames($form, FwFormField.getValueByDataField($form, `${this.Module}Id`), period);
            this.loadProfitAndLoss($form);
        });

        //SpecifyBillingDatesByType and LockBillingDates checkbox events
        //need to change datafields for these - jason h 08/30/2019
        $form.find('[data-datafield="SpecifyBillingDatesByType"]').on('change', e => {
            this.billingPeriodEvents($form);
        });
        $form.find('[data-datafield="LockBillingDates"]').on('change', e => {
            this.billingPeriodEvents($form);
        });

        //Activity Filters
        $form.on('change', '.activity-filters', e => {
            ActivityGridController.filterActivities($form);
        });
        // Prevent items tab view on 'NEW' records
        $form.find('[data-type="tab"][data-notOnNew="true"]').on('click', e => {
            if ($form.attr('data-mode') === 'NEW') {
                e.stopImmediatePropagation();
                FwNotification.renderNotification('WARNING', 'Save Record first.');
            }
        });

        //Project validations
        $form.find('[data-datafield="ProjectId"]').data('onchange', $tr => {
            const validationName = $tr.closest('.fwbrowse').attr('data-name');
            const id = FwBrowse.getValueByDataField(null, $tr, 'ProjectId');
            let data: any = {};
            if (validationName === 'ProjectValidation') {
                data = {
                    field: 'ProjectNumber',
                    value: FwBrowse.getValueByDataField(null, $tr, 'ProjectNumber')
                };
            } else if (validationName === 'ProjectNumberValidation') {
                data = {
                    field: 'Project',
                    value: FwBrowse.getValueByDataField(null, $tr, 'Project')
                };
            }
            FwFormField.setValue2($form.find(`[data-validationname="${data.field}Validation"]`), id, data.value);
        });

        //Currency Change
        $form.find('[data-datafield="CurrencyId"]').data('onchange', $tr => {
            const mode = $form.attr('data-mode');
            if (mode !== 'NEW') {
                const originalCurrencyId = $form.find('[data-datafield="CurrencyId"]').attr('data-originalvalue');
                const newCurrencyId = FwFormField.getValue2($form.find('[data-datafield="CurrencyId"]'));
                if (originalCurrencyId !== '' && originalCurrencyId !== newCurrencyId) {
                    this.currencyChange($form, $tr);
                }
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    currencyChange($form: JQuery, $tr) {
        const currency = FwBrowse.getValueByDataField($form, $tr, 'Currency');
        const currencyCode = FwBrowse.getValueByDataField($form, $tr, 'CurrencyCode');
        const currencySymbol = FwBrowse.getValueByDataField($form, $tr, 'CurrencySymbol');

        FwFormField.setValueByDataField($form, 'CurrencySymbol', currencySymbol);

        const $confirmation = FwConfirmation.renderConfirmation(`Update Rates to new Currency?`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '500px');
        const originalCurrency = $form.find('[data-datafield="Currency"]').attr('data-originaltext');
        const originalCurrencyCode = $form.find('[data-datafield="CurrencyId"]').attr('data-originaltext');
        const originalCurrencyId = $form.find('[data-datafield="CurrencyId"]').attr('data-originalvalue')


        const html = [];
        html.push(`<div class="fwform" data-controller="none" style="background-color: transparent;">`);
        html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
        html.push(`    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Old Currency" data-datafield="OldCurrency" data-enabled="false" style="width:480px; float:left;">${originalCurrency}</div>`);
        html.push(`  </div>`);
        html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
        html.push(`    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="New Currency" data-datafield="NewCurrency" data-enabled="false" style="width:480px;float:left;">${currency}</div>`);
        html.push(`  </div>`);
        html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
        html.push(`   <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="UpdateAllRatesToNewCurrency">`);
        html.push(`      <div data-value="LEAVE" data-caption="Leave all Rates and Costs as they are"> </div>`);
        html.push(`     <div data-value="UPDATE" data-caption="Update all Rates and Costs on this ${this.Module} to ${currency} (${currencyCode})"> </div>`);
        html.push(`   </div>`);
        html.push(`  </div>`);
        html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
        html.push(`    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="ConfirmUpdateAllRatesToNewCurrency" style="display:none;flex:1 1 250px;"></div>`);
        html.push(`  </div>`);
        html.push(`</div>`);

        FwConfirmation.addControls($confirmation, html.join(''));
        const $apply = FwConfirmation.addButton($confirmation, `Apply`, false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');
        FwFormField.setValueByDataField($confirmation, 'OldCurrency', originalCurrency);

        FwFormField.setValueByDataField($confirmation, 'NewCurrency', currency);
        // confirmation radio
        $confirmation.find(`div[data-datafield="UpdateAllRatesToNewCurrency"]`).on('change', e => {
            const updateAllRates = FwFormField.getValueByDataField($confirmation, 'UpdateAllRatesToNewCurrency');
            const $updateRatesTextConfirmation = $confirmation.find('[data-datafield="ConfirmUpdateAllRatesToNewCurrency"]');
            if (updateAllRates === 'UPDATE') {
                $updateRatesTextConfirmation.show().find('.fwformfield-caption')
                    .text(`Type 'UPDATE RATES' here to confirm this change.  All Item Rates will be altered when this ${this.Module} is saved.`)
                    .css({ 'white-space': 'break-spaces', 'height': 'auto', 'font-size': '1em', 'color': 'red' });
            } else {
                FwFormField.setValueByDataField($confirmation, 'ConfirmUpdateAllRatesToNewCurrency', '');
                $updateRatesTextConfirmation.hide();
            }
        });
        // apply
        $apply.on('click', e => {
            const updateAllRates = FwFormField.getValueByDataField($confirmation, 'UpdateAllRatesToNewCurrency');
            if (updateAllRates === 'UPDATE') {
                const confirmUpdateRates = FwFormField.getValueByDataField($confirmation, 'ConfirmUpdateAllRatesToNewCurrency');
                if (confirmUpdateRates === 'UPDATE RATES') {
                    FwFormField.setValueByDataField($form, 'UpdateAllRatesToNewCurrency', FwFormField.getValueByDataField($confirmation, 'UpdateAllRatesToNewCurrency'));
                    FwFormField.setValueByDataField($form, 'ConfirmUpdateAllRatesToNewCurrency', FwFormField.getValueByDataField($confirmation, 'ConfirmUpdateAllRatesToNewCurrency'));
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwNotification.renderNotification('SUCCESS', 'Rates will be updated after save.');
                } else if (confirmUpdateRates !== 'UPDATE RATES') {
                    FwNotification.renderNotification('ERROR', 'You must type "UPDATE RATES" to save a new currency or click "Cancel".');
                }
            } else if (updateAllRates === 'LEAVE') {
                // leave rates as is
                FwConfirmation.destroyConfirmation($confirmation);
                //FwNotification.renderNotification('SUCCESS', 'Rates will be updated after save.');
                FwFormField.setValueByDataField($form, 'UpdateAllRatesToNewCurrency', false);
                FwFormField.setValueByDataField($form, 'ConfirmUpdateAllRatesToNewCurrency', '');
            }
        });
        // cancel
        $no.on('click', e => {
            FwConfirmation.destroyConfirmation($confirmation);
            // restore old currency
            FwFormField.setValueByDataField($form, 'CurrencyId', originalCurrencyId, originalCurrencyCode);
            FwFormField.setValueByDataField($form, 'UpdateAllRatesToNewCurrency', false);
            FwFormField.setValueByDataField($form, 'ConfirmUpdateAllRatesToNewCurrency', '');
            FwNotification.renderNotification('INFO', `Currency has been reset to ${originalCurrencyCode}.`);
        });
    }
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
            $orderItemGrid = $form.find('.rentalsalegrid [data-name="OrderItemGrid"]');
            FwFormField.setValueByDataField($form, 'RentalSaleTotal', '');
            FwFormField.disable($form.find('div[data-datafield="RentalSaleTotalIncludesTax"]'));
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
    bottomLineTotalWithTaxChange($form: any, event: any) {
        //----------------------------------------------------------------------------------------------
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
        if (recType === 'RS') {
            $orderItemGrid = $form.find('.rentalsalegrid  [data-name="OrderItemGrid"]');
            total = FwFormField.getValueByDataField($form, 'RentalSaleTotal');
            includeTaxInTotal = FwFormField.getValueByDataField($form, 'RentalSaleTotalIncludesTax');
            if (!isWithTaxCheckbox) {
                FwFormField.setValueByDataField($form, 'RentalSaleDiscountPercent', '');
            }
            if (total === '0.00') {
                FwFormField.disable($form.find('div[data-datafield="RentalSaleTotalIncludesTax"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="RentalSaleTotalIncludesTax"]'));
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
            const module = this.Module;
            const orderNumber = FwFormField.getValue($form, `div[data-datafield="${module}Number"]`);
            const orderId = FwFormField.getValue($form, `div[data-datafield="${module}Id"]`);
            const dealId = FwFormField.getValue($form, `div[data-datafield="DealId"]`);

            const $report = (module === 'Order') ? OrderReportController.openForm() : QuoteReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValue($report, `div[data-datafield="${module}Id"]`, orderId, orderNumber);
            FwFormField.setValue($report, `div[data-datafield="CompanyIdField"]`, dealId);
            const $tab = FwTabs.getTabByElement($report);
            $tab.find('.caption').html(`Print ${module}`);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    printShippingLabel($form: any, tag: string) {
        try {
            const print = () => {
                const module = this.Module;
                const orderNumber = FwFormField.getValue($form, `div[data-datafield="${module}Number"]`);
                const orderId = FwFormField.getValue($form, `div[data-datafield="${module}Id"]`);
                const dealId = FwFormField.getValue($form, `div[data-datafield="DealId"]`);

                const $report = (tag === 'Incoming') ? IncomingShippingLabelController.openForm() : OutgoingShippingLabelController.openForm();
                FwModule.openSubModuleTab($form, $report);

                FwFormField.setValue($report, `div[data-datafield="${module}Id"]`, orderId, orderNumber);
                FwFormField.setValue($report, `div[data-datafield="CompanyIdField"]`, dealId);
                const $tab = FwTabs.getTabByElement($report);
                $tab.find('.caption').html(`Print ${module} ${tag} Label `);
            }

            const isModified = $form.attr('data-modified');
            if (isModified === 'true') {
                const $confirmation = FwConfirmation.renderConfirmation('Unsaved Changes on Form', "Any unsaved changes related to your shipping label will not be reflected in print. Continue to print or cancel and save changes?");
                const $yes = FwConfirmation.addButton($confirmation, `Print Label`, false);
                const $no = FwConfirmation.addButton($confirmation, 'Cancel');
                $yes.on('click', e => {
                    FwConfirmation.destroyConfirmation($confirmation);
                    print();
                });
            } else {
                print();
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    printDeliveryInstructions($form, tag: string) {
        try {
            const print = () => {
                const module = this.Module;

                let $report;
                if (tag === 'Out') {
                    $report = OutgoingDeliveryInstructionsController.openForm();
                } else if (tag === 'In') {
                    $report = IncomingDeliveryInstructionsController.openForm();
                }
                FwModule.openSubModuleTab($form, $report);

                const deliveryId = FwFormField.getValueByDataField($form, `${tag}DeliveryId`);
                FwFormField.setValueByDataField($report, `${tag}DeliveryId`, deliveryId);
                const orderNumber = FwFormField.getValue($form, `div[data-datafield="${module}Number"]`);
                FwFormField.setValue($report, `div[data-datafield="${module}Id"]`, deliveryId, orderNumber);
                const dealId = FwFormField.getValue($form, `div[data-datafield="DealId"]`);
                FwFormField.setValue($report, `div[data-datafield="CompanyIdField"]`, dealId);

                const $tab = FwTabs.getTabByElement($report);
                $tab.find('.caption').html(`Print ${tag === 'In' ? 'Incoming' : 'Outgoing'} Delivery Instructions `);
            }

            const isModified = $form.attr('data-modified');
            if (isModified === 'true') {
                const $confirmation = FwConfirmation.renderConfirmation('Unsaved Changes on Form', "Any unsaved changes related to your Delivery Instructions will not be reflected in print. Continue to print or cancel and save changes?");
                const $yes = FwConfirmation.addButton($confirmation, `Print Instructions`, false);
                const $no = FwConfirmation.addButton($confirmation, 'Cancel');
                $yes.on('click', e => {
                    FwConfirmation.destroyConfirmation($confirmation);
                    print();
                });
            } else {
                print();
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    printValueSheet($form: any) {
        try {
            const module = this.Module;
            const orderIdText = FwFormField.getValueByDataField($form, `${module}Number`);
            const orderId = FwFormField.getValueByDataField($form, `${module}Id`);

            const $report = OrderValueSheetReportController.openForm();
            FwModule.openSubModuleTab($form, $report);

            FwFormField.setValue($report, `div[data-datafield="OrderId"]`, orderId, orderIdText);
            const $tab = FwTabs.getTabByElement($report);
            $tab.find('.caption').html(`Print Value Sheet`);

        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    calculateOrderItemGridTotals($form: any, gridType: string, totals?): void {
        let subTotal, discount, salesTax, salesTax2, grossTotal, total;

        let rateValue = $form.find(`.${gridType}grid .totalType input:checked`).val();
        switch (rateValue) {
            case 'W':
                if (FwFormField.getValueByDataField($form, 'RateType') === '3WEEK') {
                    subTotal = totals.AverageWeeklyExtended;
                    discount = totals.AverageWeeklyDiscountAmount;
                    salesTax = totals.AverageWeeklyTax1;
                    salesTax2 = totals.AverageWeeklyTax2;
                    grossTotal = totals.AverageWeeklyExtendedNoDiscount;
                    total = totals.AverageWeeklyTotal;
                } else {
                    subTotal = totals.WeeklyExtended;
                    discount = totals.WeeklyDiscountAmount;
                    salesTax = totals.WeeklyTax1;
                    salesTax2 = totals.WeeklyTax2;
                    grossTotal = totals.WeeklyExtendedNoDiscount;
                    total = totals.WeeklyTotal;
                }
                break;
            case 'P':
                subTotal = totals.PeriodExtended;
                discount = totals.PeriodDiscountAmount;
                salesTax = totals.PeriodTax1;
                salesTax2 = totals.PeriodTax2;
                grossTotal = totals.PeriodExtendedNoDiscount;
                total = totals.PeriodTotal;
                break;
            case 'M':
                subTotal = totals.MonthlyExtended;
                discount = totals.MonthlyDiscountAmount;
                salesTax = totals.MonthlyTax1;
                salesTax2 = totals.MonthlyTax2;
                grossTotal = totals.MonthlyExtendedNoDiscount;
                total = totals.MonthlyTotal;
                break;
            default:
                subTotal = totals.PeriodExtended;
                discount = totals.PeriodDiscountAmount;
                salesTax = totals.PeriodTax1;
                salesTax2 = totals.PeriodTax2;
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

        //$form.find(`.${gridType}totals [data-totalfield="SubTotal"] input`).val(subTotal);
        //$form.find(`.${gridType}totals [data-totalfield="Discount"] input`).val(discount);
        //$form.find(`.${gridType}totals [data-totalfield="Tax"] input`).val(salesTax);
        //$form.find(`.${gridType}totals [data-totalfield="Tax2"] input`).val(salesTax2);
        //$form.find(`.${gridType}totals [data-totalfield="GrossTotal"] input`).val(grossTotal);
        //$form.find(`.${gridType}totals [data-totalfield="Total"] input`).val(total);
        FwFormField.setValue2($form.find(`.${gridType}totals [data-totalfield="SubTotal"]`), subTotal);
        FwFormField.setValue2($form.find(`.${gridType}totals [data-totalfield="Discount"]`), discount);
        FwFormField.setValue2($form.find(`.${gridType}totals [data-totalfield="Tax"]`), salesTax);
        FwFormField.setValue2($form.find(`.${gridType}totals [data-totalfield="Tax2"]`), salesTax2);
        FwFormField.setValue2($form.find(`.${gridType}totals [data-totalfield="GrossTotal"]`), grossTotal);
        FwFormField.setValue2($form.find(`.${gridType}totals [data-totalfield="Total"]`), total);

    };
    //----------------------------------------------------------------------------------------------
    checkDateRangeForPick($form, event) {
        const $element = jQuery(event.currentTarget);
        //let parsedPickDate = this.getPickStartStop($form, 'div[data-dateactivitytype="PICK"]');
        //let parsedFromDate = this.getPickStartStop($form, 'div[data-dateactivitytype="START"]');;
        //let parsedToDate = this.getPickStartStop($form, 'div[data-dateactivitytype="STOP"]');;
        //
        //if (parsedPickDate != '') {
        //    parsedPickDate = Date.parse(parsedPickDate);
        //}
        //
        //if (parsedFromDate != '') {
        //    parsedFromDate = Date.parse(parsedFromDate);
        //}
        //
        //if (parsedToDate != '') {
        //    parsedToDate = Date.parse(parsedToDate);
        //}

        let parsedPickDate, parsedFromDate, parsedToDate;
        let pickStartStop: PickStartStop = this.getPickStartStop($form);

        if (pickStartStop.PickDate != '') {
            parsedPickDate = Date.parse(pickStartStop.PickDate);
        }

        if (pickStartStop.StartDate != '') {
            parsedFromDate = Date.parse(pickStartStop.StartDate);
        }

        if (pickStartStop.StopDate != '') {
            parsedToDate = Date.parse(pickStartStop.StopDate);
        }

        if ($element.attr('data-dateactivitytype') === 'START' && parsedFromDate < parsedPickDate) {
            $form.find('div[data-dateactivitytype="START"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Start Date' is before 'Pick Date'.");
        }
        else if ($element.attr('data-dateactivitytype') === 'PICK' && parsedFromDate < parsedPickDate) {
            $form.find('div[data-dateactivitytype="PICK"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'Start Date'.");
        }
        else if ($element.attr('data-dateactivitytype') === 'PICK' && parsedToDate < parsedPickDate) {
            $form.find('div[data-dateactivitytype="PICK"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Pick Date' is after 'Stop Date'.");
        }
        else if (parsedToDate < parsedFromDate) {
            $form.find('div[data-dateactivitytype="STOP"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Stop Date' is before 'Start Date'.");
        }
        else if (parsedToDate < parsedPickDate) {
            $form.find('div[data-dateactivitytype="STOP"]').addClass('error');
            FwNotification.renderNotification('WARNING', "Your chosen 'Stop Date' is before 'Pick Date'.");
        }
        else {
            $form.find('div[data-dateactivitytype="PICK"]').removeClass('error');
            $form.find('div[data-dateactivitytype="START"]').removeClass('error');
            $form.find('div[data-dateactivitytype="STOP"]').removeClass('error');
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
                        newEndDate = FwLocale.getDate(response, null, { Quantity: -1, ObjectModified: 'days' });
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
                    newEndDate = FwLocale.getDate(billingStartDate, null, { Quantity: daysToAdd, ObjectModified: 'days' });
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
            const value = FwFormField.getValueByDataField($form, 'OutDeliveryAddressType');
            if (value === 'WAREHOUSE') {
                this.getWarehouseAddress($form, 'Out');
            } else if (value === 'DEAL') {
                this.fillDeliveryAddressFieldsforDeal($form, 'Out');
            } else if (value === 'VENUE') {
                this.fillDeliveryAddressFieldsforVenue($form, 'Out');
            } else if (value === 'OTHER') {
                this.showHideDeliveryLocationField($form);
            }
        }
        else if ($element.attr('data-datafield') === 'InDeliveryAddressType') {
            const value = FwFormField.getValueByDataField($form, 'InDeliveryAddressType');
            if (value === 'WAREHOUSE') {
                this.getWarehouseAddress($form, 'In');
            } else if (value === 'DEAL') {
                this.fillDeliveryAddressFieldsforDeal($form, 'In');
            } else if (value === 'VENUE') {
                this.fillDeliveryAddressFieldsforVenue($form, 'In');
            } else if (value === 'OTHER') {
                this.showHideDeliveryLocationField($form);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
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
            FwFormField.setValueByDataField($form, `IssuedToState`, response.BillToState);
            FwFormField.setValueByDataField($form, `IssuedToZipCode`, response.BillToZipCode);
            FwFormField.setValueByDataField($form, `IssuedToCountryId`, response.BillToCountryId, response.BillToCountry);
        }
    }
    //----------------------------------------------------------------------------------------------
    getWarehouseAddress($form: any, prefix: string): void {
        const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        this.showHideDeliveryLocationField($form);

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
            if (warehouseId) {
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
            } else {
                FwNotification.renderNotification('INFO', `No Warehouse chosen on ${this.Module}.`);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    fillDeliveryAddressFieldsforDeal($form: any, prefix: string, response?: any): void {
        this.showHideDeliveryLocationField($form);
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
    fillDeliveryAddressFieldsforVenue($form: any, prefix: string): void {
        this.showHideDeliveryLocationField($form);

        FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, '');
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, '');
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, '');
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, '');
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, '');
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, '');
        FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, '', '');
        const venueId = FwFormField.getValueByDataField($form, `${prefix}DeliveryToVenueId`);

        if (venueId) {
            let venueRes: any = {};
            if ($form.data('venueAddress')) {
                venueRes = $form.data('venueAddress');
                if (venueRes.VenueId === venueId) {
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, venueRes.PrimaryContact);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, venueRes.Address1);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, venueRes.Address2);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, venueRes.City);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, venueRes.State);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, venueRes.Zip);
                    FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, venueRes.CountryId, venueRes.Country);
                } else {
                    getVenueAddress($form, venueId, prefix);
                }
            } else {
                getVenueAddress($form, venueId, prefix);
            }
        }

        function getVenueAddress($form, venueId, prefix) {
            FwAppData.apiMethod(true, 'GET', `api/v1/venue/${venueId}`, null, FwServices.defaultTimeout, response => {
                const venueRes = response;
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, response.PrimaryContact);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, response.Address1);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, response.Address2);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, response.City);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, response.State);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, response.Zip);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, response.CountryId, venueRes.Country);
                // Preventing unnecessary API calls once venue addresses have been requested once
                $form.data('venueAddress', {
                    'VenueId': response.VenueId,
                    'Attention': response.PrimaryContact,
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
                    const presentationLayerId = response.Rows[0][response.ColumnIndex.PresentationLayerId];
                    const presentationLayer = response.Rows[0][response.ColumnIndex.PresentationLayer];
                    FwFormField.setValue($form, 'div[data-datafield="TermsConditionsId"]', termsConditionsId, termsConditions);
                    FwFormField.setValue($form, 'div[data-datafield="CoverLetterId"]', coverLetterId, coverLetter);
                    FwFormField.setValue($form, 'div[data-datafield="PresentationLayerId"]', presentationLayerId, presentationLayer);
                }
            }, null, null);

        if ($form.attr('data-mode') === 'NEW') {
            if (this.DefaultOrderTypeId) {
                FwAppData.apiMethod(true, 'GET', `api/v1/ordertype/${orderTypeId}`, null, FwServices.defaultTimeout, response => {
                    this.DefaultFromTime = response.DefaultFromTime;
                    this.DefaultToTime = response.DefaultToTime;
                    this.DefaultPickTime = response.DefaultPickTime;
                    this.AllowRoundTripRentals = response.AllowRoundTripRentals;
                    FwFormField.setValueByDataField($form, 'PickTime', this.DefaultPickTime);
                    FwFormField.setValueByDataField($form, 'EstimatedStartTime', this.DefaultFromTime);
                    FwFormField.setValueByDataField($form, 'EstimatedStopTime', this.DefaultToTime);
                    FwFormField.setValueByDataField($form, 'RoundTripRentals', this.AllowRoundTripRentals);
                }, ex => FwFunc.showError(ex), $form);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    cancelUncancel($form: any) {
        let $confirmation, $yes, $no;
        const module = this.Module;
        const id = FwFormField.getValueByDataField($form, `${module}Id`);
        const orderStatus = FwFormField.getValueByDataField($form, 'Status');

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
            //$yes.text('Canceling...');
            $yes.off('click');
            const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
            const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/cancel/${id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Cancelled`);
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form);
            }, function onError(response) {
                $yes.on('click', cancelOrder);
                //$yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                //FwModule.refreshForm($form);
            }, $realConfirm);
        };

        function uncancelOrder() {
            let request: any = {};

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            //$yes.text('Retrieving...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/uncancel/${id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Retrieved`);
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form);
            }, function onError(response) {
                $yes.on('click', uncancelOrder);
                //$yes.text('Cancel');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                //FwModule.refreshForm($form);
            }, $form);
        };
    };
    //----------------------------------------------------------------------------------------------
    createEstimate($form: JQuery) {
        const module = this.Module;
        const $confirmation = FwConfirmation.renderConfirmation('Create Estimate', '');
        //$confirmation.find('.fwconfirmationbox').css('width', '550px');
        const html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="flexrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealNumber" data-enabled="false" style="flex: 0 1 150px;"></div>');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DealDescription" data-enabled="false"></div>');
        html.push('  </div>');
        html.push('  <div class="flexrow">');
        html.push(`    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="${module}" data-datafield="OrderNumber" data-enabled="false" style="flex: 0 1 150px;"></div>`);
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="OrderDescription" data-enabled="false"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Dates">');
        html.push('     <div class="flexrow">');
        html.push('         <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycle" data-enabled="false"></div>');
        html.push('     </div>');
        html.push('     <div class="flexrow">');
        html.push('         <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Start" data-datafield="BillingStartDate"></div>');
        html.push('         <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="End" data-datafield="BillingEndDate" data-enabled="false"></div>');
        html.push('     </div>');
        html.push('     <div class="flexrow">');
        html.push('         <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Items not yet Checked Out" data-datafield="IncludeNotYetOut"></div>');
        html.push('     </div>');
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        const id = FwFormField.getValueByDataField($form, `${module}Id`);
        let startDate, endDate;

        FwAppData.apiMethod(true, 'GET', `api/v1/${module}/${id}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            FwFormField.setValueByDataField($confirmation, 'DealNumber', response.DealNumber);
            FwFormField.setValueByDataField($confirmation, 'DealDescription', response.Deal);
            FwFormField.setValueByDataField($confirmation, 'OrderNumber', response.OrderNumber);
            FwFormField.setValueByDataField($confirmation, 'OrderDescription', response.Description);
            FwFormField.setValueByDataField($confirmation, 'BillingCycle', response.BillingCycle);

            const billingCycleType = response.BillingCycleType;

            FwAppData.apiMethod(true, 'POST', `api/v1/billing/getorderbillingdates/${id}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($confirmation, 'BillingStartDate', moment(response.PeriodStart).format('MM-DD-YYYY'));
                FwFormField.setValueByDataField($confirmation, 'BillingEndDate', moment(response.PeriodEnd).format('MM-DD-YYYY'));

                const disableStartDateTypes = ['IMMEDIATE', 'ATCLOSE', 'EVENTS'];
                if (disableStartDateTypes.indexOf(billingCycleType) !== -1) {
                    FwFormField.disable($confirmation.find('[data-datafield="BillingStartDate"]'));
                } else {
                    startDate = response.PeriodStart;
                    switch (billingCycleType) {
                        case 'WEEKLY':
                            endDate = moment(startDate).add(6, 'days').format('MM-DD-YYYY');
                            break;
                        case 'BIWEEKLY':
                            endDate = moment(startDate).add(13, 'days').format('MM-DD-YYYY');
                            break;
                        case 'MONTHLY':
                            endDate = moment(startDate).add(1, 'month').subtract(1, 'days').format('MM-DD-YYYY');
                            break;
                        case 'CALMONTH':
                            endDate = moment(startDate).endOf('month').format('MM-DD-YYYY');
                            break;
                        case 'EPISODIC':
                            //Will need a new validation that is not yet defined.
                            break;
                    }
                    FwFormField.setValueByDataField($confirmation, 'BillingEndDate', endDate);
                }
            }, function onError(response) { FwFunc.showError(response) }, $confirmation.find('.fwconfirmationbox'));
        }, function onError(response) { FwFunc.showError(response) }, $confirmation.find('.fwconfirmationbox'));

        const $ok = FwConfirmation.addButton($confirmation, 'Create Estimate', false);
        FwConfirmation.addButton($confirmation, 'Cancel');

        $ok.on('click', e => {
            const request: any = {
                OrderId: id,
                PeriodStart: FwFormField.getValueByDataField($confirmation, 'BillingStartDate'),
                PeriodEnd: FwFormField.getValueByDataField($confirmation, 'BillingEndDate'),
                IncludeNotYetOut: (FwFormField.getValueByDataField($confirmation, 'IncludeNotYetOut') === 'T')
            };
            FwAppData.apiMethod(true, 'POST', `api/v1/billing/createinvoiceestimate`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Estimate Successfully Created.');
                FwConfirmation.destroyConfirmation($confirmation);
                const uniqueids: any = {};
                uniqueids.InvoiceId = response.InvoiceId;
                let $form = InvoiceController.loadForm(uniqueids);
                FwModule.openModuleTab($form, "", true, 'FORM', true);
            }, function onError(response) { FwFunc.showError(response) }, $confirmation.find('.fwconfirmationbox'));
        });
    }
    //----------------------------------------------------------------------------------------------
    browseCancelOption($browse: JQuery) {
        try {
            const module = this.Module;
            let $confirmation, $yes, $no;
            let orderId = $browse.find(`.selected [data-browsedatafield="${module}Id"]`).attr('data-originalvalue');
            let orderStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');


            if (orderId != null) {
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

                function cancelOrder() {
                    let request: any = {};

                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    //$yes.text('Canceling...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/${module}/cancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', `${module} Successfully Cancelled`);
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', cancelOrder);
                        //$yes.text('Cancel Order');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        //FwBrowse.databind($browse);
                    }, $browse);
                };

                function uncancelOrder() {
                    let request: any = {};

                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    //$yes.text('Retrieving...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/${module}/uncancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', `${module} Successfully Retrieved`);
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', uncancelOrder);
                        //$yes.text('Cancel');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        //FwBrowse.databind($browse);
                    }, $browse);
                };
            } else {
                if (module === 'Order') {
                    FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
                } else if (module === 'Quote') {
                    FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
                }
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    checkMessages($form, module, id) {
        if (id) {
            FwAppData.apiMethod(true, 'GET', `api/v1/${module}/${id}/messages`, null, FwServices.defaultTimeout, response => {
                if (response.success) {
                    const messages = response.Messages;
                    if (messages.length) {
                        const $formBody = $form.find('.fwform-body');
                        $form.find('.form-alert-container').remove();
                        const html: Array<string> = [];
                        html.push(`<div class="form-alert-container">`);
                        for (let i = 0; i < messages.length; i++) {
                            let alertClass = 'elevated'; //yellow
                            if (messages[i].PreventCheckOut === true) {
                                alertClass = 'severe'; // red
                            }
                            html.push(`<div class="form-alert ${alertClass}"><div style="float:left;"></div><span>${messages[i].Message}</span><div class="close"><i class="material-icons">clear</i></div></div>`);
                        }
                        html.push(`</div>`);
                        $formBody.before(html.join(''));

                        // close button
                        $form.find('div.form-alert i').on('click', e => {
                            jQuery(e.currentTarget).parents('.form-alert').remove();
                            if ($form.find('div.form-alert').length === 0) {
                                $form.find('.form-alert-container').remove();
                            }
                        });
                    }
                }
            }, ex => FwFunc.showError(ex), $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    changeOfficeLocation($form: any) {
        const module = this.Module;
        const controller = $form.attr('data-controller');
        const id = FwFormField.getValueByDataField($form, `${module}Id`);
        let $confirmation, $yes, $no;
        if (id != null) {
            $confirmation = FwConfirmation.renderConfirmation('Change Office Location / Warehouse', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            const html: Array<string> = [];
            html.push(`<div class="fwform" data-controller="${controller}" style="background-color: transparent;">`);
            html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push(`    <div>Change Office Location / Warehouse for this ${module}:</div>`);
            html.push(`      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push(`        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-validationname="OfficeLocationValidation" data-required="true"></div>`);
            html.push(`      </div>`);
            html.push(`      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push(`        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-boundfields="OfficeLocationId" data-required="true"></div>`);
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
            const valid = FwModule.validateForm($confirmation);
            if (valid) {
                const locationid = FwFormField.getValueByDataField($confirmation, 'OfficeLocationId');
                const warehouseid = FwFormField.getValueByDataField($confirmation, 'WarehouseId');
                const request: any = {
                    OfficeLocationId: locationid,
                    Warehouseid: warehouseid
                };
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Changing...');
                $yes.off('click');
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

                FwAppData.apiMethod(true, 'POST', `api/v1/${module}/changeofficelocation/${id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success) {

                        FwNotification.renderNotification('SUCCESS', `${module} Successfully Changed`);
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwModule.refreshForm($form);
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
                    FwModule.refreshForm($form);
                }, $realConfirm);
            }
        };
    };
    //----------------------------------------------------------------------------------------------    
    applyOrderTypeAndRateTypeToForm($form) {
        const $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        //const rateType = FwFormField.getValueByDataField($form, 'RateType');

        // get the OrderTypeId from the form
        const orderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');

        if (this.CachedOrderTypes[orderTypeId] !== undefined) {
            this.applyOrderTypeToColumns($form, this.CachedOrderTypes[orderTypeId]);
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
                if ($form.attr('data-mode') === 'NEW') {
                    if (response.DetermineQuantitiesToBillBasedOn) {
                        FwFormField.setValueByDataField($form, 'DetermineQuantitiesToBillBasedOn', response.DetermineQuantitiesToBillBasedOn);
                    }
                }

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
                const hiddenRentalSale = fieldNames.filter(function (field) {
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
                    hiddenRentalSale: hiddenRentalSale,
                    hiddenLossDamage: hiddenLossDamage,
                    hiddenCombined: hiddenCombined
                }
                this.applyOrderTypeToColumns($form, this.CachedOrderTypes[orderTypeId]);
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
    };
    //----------------------------------------------------------------------------------------------    
    applyOrderTypeToColumns($form, orderTypeData) {
        FwFormField.setValueByDataField($form, 'CombineActivity', orderTypeData.CombineActivityTabs);
        //const $lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss & Damage"]');

        if (orderTypeData.CombineActivityTabs === true) {
            $form.find('.notcombined').css('display', 'none');
            $form.find('.notcombinedtab').css('display', 'none');
            $form.find('.combinedtab').show();
        } else {
            $form.find('.combined').css('display', 'none');
            $form.find('.combinedtab').css('display', 'none');
            $form.find('.notcombinedtab').show();

            // show/hide tabs based on Activity boxes checked
            //const $rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]');
            //const $subRentalTab = $form.find('[data-type="tab"][data-caption="Sub-Rental"]');
            //if ($form.find('[data-datafield="Rental"] input').prop('checked')) {
            //    $rentalTab.show();
            //    $subRentalTab.show();
            //} else {
            //    $rentalTab.hide();
            //    $subRentalTab.hide();
            //}
            //const $salesTab = $form.find('[data-type="tab"][data-caption="Sales"]');
            //const $subSalesTab = $form.find('[data-type="tab"][data-caption="Sub-Sales"]');
            //if ($form.find('[data-datafield="Sales"] input').prop('checked')) {
            //    $salesTab.show();
            //    $subSalesTab.show();
            //} else {
            //    $salesTab.hide();
            //    $subSalesTab.hide();
            //}
            //const $miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]');
            //const $subMiscTab = $form.find('[data-type="tab"][data-caption="Sub-Miscellaneous"]');
            //if ($form.find('[data-datafield="Miscellaneous"] input').prop('checked')) {
            //    $miscTab.show();
            //    $subMiscTab.show();
            //} else {
            //    $miscTab.hide();
            //    $subMiscTab.hide();
            //}
            //const $laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
            //const $subLaborTab = $form.find('[data-type="tab"][data-caption="Sub-Labor"]');
            //if ($form.find('[data-datafield="Labor"] input').prop('checked')) {
            //    $laborTab.show();
            //    $subLaborTab.show();
            //} else {
            //    $laborTab.hide();
            //    $subLaborTab.hide();
            //}
            //const $rentalSaleTab = $form.find('[data-type="tab"].rentalsaletab');
            //$form.find('[data-datafield="RentalSale"] input').prop('checked') ? $rentalSaleTab.show() : $rentalSaleTab.hide();
            //if ($lossDamageTab !== undefined) {
            //    $form.find('[data-datafield="LossAndDamage"] input').prop('checked') ? $lossDamageTab.show() : $lossDamageTab.hide();
            //}

            // show/hide tabs based on Activity boxes checked
            this.showHideActivityTabs($form);

        }
        const rateType = FwFormField.getValueByDataField($form, 'RateType');

        const $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        for (let i = 0; i < orderTypeData.hiddenRentals.length; i++) {
            //jQuery($rentalGrid.find(`[data-mappedfield="${orderTypeData.hiddenRentals[i]}"]`)).parent().remove();
            jQuery($rentalGrid.find(`[data-mappedfield="${orderTypeData.hiddenRentals[i]}"]`)).parent().hide();
            //jQuery($rentalGrid.find(`[data-mappedfield="${orderTypeData.hiddenRentals[i]}"]`)).parent().remove();
        }
        const $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        for (let i = 0; i < orderTypeData.hiddenSales.length; i++) {
            jQuery($salesGrid.find(`[data-mappedfield="${orderTypeData.hiddenSales[i]}"]`)).parent().hide();
            //jQuery($salesGrid.find(`[data-mappedfield="${orderTypeData.hiddenSales[i]}"]`)).parent().remove();
        }
        const $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        for (let i = 0; i < orderTypeData.hiddenLabor.length; i++) {
            jQuery($laborGrid.find(`[data-mappedfield="${orderTypeData.hiddenLabor[i]}"]`)).parent().hide();
            //jQuery($laborGrid.find(`[data-mappedfield="${orderTypeData.hiddenLabor[i]}"]`)).parent().remove();
        }
        const $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        for (let i = 0; i < orderTypeData.hiddenMisc.length; i++) {
            jQuery($miscGrid.find(`[data-mappedfield="${orderTypeData.hiddenMisc[i]}"]`)).parent().hide();
            //jQuery($miscGrid.find(`[data-mappedfield="${orderTypeData.hiddenMisc[i]}"]`)).parent().remove();
        }
        const $rentalSaleGrid = $form.find('.rentalsalegrid [data-name="OrderItemGrid"]');
        for (let i = 0; i < orderTypeData.hiddenRentalSale.length; i++) {
            jQuery($rentalSaleGrid.find(`[data-mappedfield="${orderTypeData.hiddenRentalSale[i]}"]`)).parent().hide();
            //jQuery($usedSaleGrid.find(`[data-mappedfield="${orderTypeData.hiddenRentalSale[i]}"]`)).parent().remove();
        }

        // loss and damage (order only)
        if ($form.find('[data-datafield="LossAndDamage"]') != undefined) {
            const $lossDamageGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            for (let i = 0; i < orderTypeData.hiddenLossDamage.length; i++) {
                jQuery($lossDamageGrid.find(`[data-mappedfield="${orderTypeData.hiddenLossDamage[i]}"]`)).parent().hide();
                //jQuery($lossDamageGrid.find(`[data-mappedfield="${orderTypeData.hiddenLossDamage[i]}"]`)).parent().remove();
            }
        }

        const $combinedGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]');
        for (let i = 0; i < orderTypeData.hiddenCombined.length; i++) {
            jQuery($combinedGrid.find(`[data-mappedfield="${orderTypeData.hiddenCombined[i]}"]`)).parent().hide();
            //jQuery($combinedGrid.find(`[data-mappedfield="${orderTypeData.hiddenCombined[i]}"]`)).parent().remove();
        }
        if (orderTypeData.hiddenRentals.indexOf('WeeklyExtended') === -1 && rateType === '3WEEK') {
            $rentalGrid.find('.3weekextended').parent().show();
        } else if (orderTypeData.hiddenRentals.indexOf('WeeklyExtended') === -1 && rateType !== '3WEEK') {
            $rentalGrid.find('.weekextended').parent().show();
        }

        if (orderTypeData.hiddenRentals.indexOf('Rate') === -1 && rateType === '3WEEK') {
            $rentalGrid.find('.3week').parent().show();
            $rentalGrid.find('.price').find('.caption').text('Week 1 Rate');
            $rentalGrid.find('.price4').find('.caption').text('Week 4+ Rate');
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
    //----------------------------------------------------------------------------------------------
    defaultFieldsOnDealChange($form:any, $tr: any) {
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const type = $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue');
        const office = JSON.parse(sessionStorage.getItem('location'));
        const currencyId = FwBrowse.getValueByDataField(null, $tr, 'CurrencyId') || office.defaultcurrencyid;
        const currencyCode = FwBrowse.getValueByDataField(null, $tr, 'CurrencyCode') || office.defaultcurrencycode;
        FwFormField.setValueByDataField($form, 'RateType', type);
        $form.find('div[data-datafield="RateType"] input.fwformfield-text').val(type);
        FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
        FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', $tr.find('.field[data-browsedatafield="PaymentTermsId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentTerms"]').attr('data-originalvalue'));
        FwFormField.setValue($form, 'div[data-datafield="PaymentTypeId"]', $tr.find('.field[data-browsedatafield="PaymentTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentType"]').attr('data-originalvalue'));
        FwFormField.setValueByDataField($form, 'CurrencyId', currencyId, currencyCode);
        FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', $tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));
        FwFormField.setValue($form, 'div[data-datafield="Deal"]', $tr.find('.field[data-browsedatafield="Deal"]').attr('data-originalvalue'));

        FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealId}`, null, FwServices.defaultTimeout, response => {
            FwFormField.setValueByDataField($form, 'CustomerId', response.CustomerId, response.Customer);
            FwFormField.setValueByDataField($form, 'CustomerNumber', response.CustomerNumber);
            FwFormField.setValueByDataField($form, 'IssuedToAttention', response.BillToAttention1);
            FwFormField.setValueByDataField($form, 'IssuedToAttention2', response.BillToAttention2);
            FwFormField.setValueByDataField($form, 'IssuedToAddress1', response.BillToAddress1);
            FwFormField.setValueByDataField($form, 'IssuedToAddress2', response.BillToAddress2);
            FwFormField.setValueByDataField($form, 'IssuedToCity', response.BillToCity);
            FwFormField.setValueByDataField($form, 'IssuedToState', response.BillToState);
            FwFormField.setValueByDataField($form, 'IssuedToZipCode', response.BillToZipCode);
            FwFormField.setValueByDataField($form, 'IssuedToCountryId', response.BillToCountryId, response.BillToCountry);
            FwFormField.setValueByDataField($form, 'PrintIssuedToAddressFrom', response.BillToAddressType);
            if (response.BillToAddressType === 'DEAL') {
                FwFormField.setValueByDataField($form, `IssuedToName`, response.Deal);
            } else if (response.BillToAddressType === 'CUSTOMER') {
                FwFormField.setValueByDataField($form, `IssuedToName`, response.Customer);
            }

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
    }
    //----------------------------------------------------------------------------------------------
    defaultFieldsOnDepartmentChange($form: any, $tr: any) {
        FwFormField.setValue($form, 'div[data-datafield="DisableEditingRentalRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingRentalRate"]').attr('data-originalvalue')));
        FwFormField.setValue($form, 'div[data-datafield="DisableEditingSalesRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingSalesRate"]').attr('data-originalvalue')));
        FwFormField.setValue($form, 'div[data-datafield="DisableEditingLaborRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingLaborRate"]').attr('data-originalvalue')));
        FwFormField.setValue($form, 'div[data-datafield="DisableEditingMiscellaneousRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingMiscellaneousRate"]').attr('data-originalvalue')));
        FwFormField.setValue($form, 'div[data-datafield="DisableEditingRentalSaleRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingRentalSaleRate"]').attr('data-originalvalue')));
        FwFormField.setValue($form, 'div[data-datafield="DisableEditingLossAndDamageRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingLossAndDamageRate"]').attr('data-originalvalue')));
        FwFormField.setValue($form, 'div[data-datafield="Department"]', $tr.find('.field[data-browsedatafield="Department"]').attr('data-originalvalue'));

        if ($form.attr('data-mode') === 'NEW') {
            const defaultActivities: any = {};
            defaultActivities['Rental'] = $tr.find('.field[data-browsedatafield="DefaultActivityRental"]').attr('data-originalvalue');
            defaultActivities['Sales'] = $tr.find('.field[data-browsedatafield="DefaultActivitySales"]').attr('data-originalvalue');
            defaultActivities['Labor'] = $tr.find('.field[data-browsedatafield="DefaultActivityLabor"]').attr('data-originalvalue');
            defaultActivities['Miscellaneous'] = $tr.find('.field[data-browsedatafield="DefaultActivityMiscellaneous"]').attr('data-originalvalue');
            defaultActivities['RentalSale'] = $tr.find('.field[data-browsedatafield="DefaultActivityRentalSale"]').attr('data-originalvalue');

            for (let key in defaultActivities) {
                FwFormField.setValueByDataField($form, `${key}`, defaultActivities[key] === 'true');
            }
            $form.find(`.fwformfield.activity input`).change();
        }

        const enableProjects = FwBrowse.getValueByDataField($form, $tr, 'EnableProjects');
        enableProjects === 'true' ? $form.find('.projecttab').show() : $form.find('.projecttab').hide();
    }
    //----------------------------------------------------------------------------------------------
    defaultBillQuantities($form) {
        const orderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        FwAppData.apiMethod(true, 'GET', `api/v1/ordertype/${orderTypeId}`, null, FwServices.defaultTimeout, response => {
            if (response.DetermineQuantitiesToBillBasedOn) {
                FwFormField.setValueByDataField($form, 'DetermineQuantitiesToBillBasedOn', response.DetermineQuantitiesToBillBasedOn);
            }
        }, null, null);
    }
    //----------------------------------------------------------------------------------------------
    disableRateColumns($form) {

        // find all the items grids on the form
        const $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        const $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        const $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        const $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        const $rentalSaleGrid = $form.find('.rentalsalegrid [data-name="OrderItemGrid"]');
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
        if (FwFormField.getValueByDataField($form, 'DisableEditingRentalSaleRate')) {
            $rentalSaleGrid.find('.rates').attr('data-formreadonly', true);
        }
        if ($lossDamageGrid !== undefined) {
            if (FwFormField.getValueByDataField($form, 'DisableEditingLossAndDamageRate')) {
                $lossDamageGrid.find('.rates').attr('data-formreadonly', true);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form, response) {
        //const period = FwFormField.getValueByDataField($form, 'totalTypeProfitLoss');
        //this.renderFrames($form, FwFormField.getValueByDataField($form, `${this.Module}Id`), period);
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

        const hiatusDiscountFrom = FwFormField.getValueByDataField($form, 'HiatusDiscountFrom');
        if (hiatusDiscountFrom === 'DEAL') {
            $form.find('div[data-grid="DealHiatusDiscountGrid"]').show();
            $form.find('div[data-grid="OrderHiatusDiscountGrid"]').hide();
        } else {
            $form.find('div[data-grid="OrderHiatusDiscountGrid"]').show();
            $form.find('div[data-grid="DealHiatusDiscountGrid"]').hide();
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

        /*
        // find all the items grids on the form
        const $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        const $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]');
        const $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]');
        const $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]');
        const $usedSaleGrid = $form.find('.rentalsalegrid [data-name="OrderItemGrid"]');
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
        */
        this.disableRateColumns($form);


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
        //const hasNotes = FwFormField.getValueByDataField($form, 'HasNotes');
        //if (hasNotes) {
        //    FwTabs.setTabColor($form.find('.notestab'), '#FFFF8d');
        //}

        this.highlightTab($form, 'notestab', 'HasNotes');
        this.highlightTab($form, 'documentstab', 'HasDocuments');
        this.highlightTab($form, 'emailhistorytab', 'HasEmailHistory');
        this.highlightTab($form, 'contactstab', 'HasContacts');
        this.highlightTab($form, 'subpurchaseordertab', 'HasSubPurchaseOrders');
        this.highlightTab($form, 'picklisttab', 'HasPickLists');
        this.highlightTab($form, 'contracttab', 'HasContracts');
        this.highlightTab($form, 'invoicetab', 'HasInvoices');

        // color the Rental tab if RentalItems exist
        const hasRentalItem = FwFormField.getValueByDataField($form, 'HasRentalItem');
        if (hasRentalItem) {
            $form.data('hasitems', true);
            const $tab = $form.find('.rentaltab');
            FwTabs.setTabColor($tab, '#FFFF8d');
            FwFormField.disable(FwFormField.getDataField($form, 'Rental'));
        }
        // color the Sales tab if SalesItems exist
        const hasSalesItem = FwFormField.getValueByDataField($form, 'HasSalesItem');
        if (hasSalesItem) {
            $form.data('hasitems', true);
            const $tab = $form.find('.salestab');
            FwTabs.setTabColor($tab, '#FFFF8d');
            FwFormField.disable(FwFormField.getDataField($form, 'Sales'));
        }
        // color the Misc. tab if MiscItems exist
        const hasMiscItem = FwFormField.getValueByDataField($form, 'HasMiscellaneousItem');
        if (hasMiscItem) {
            $form.data('hasitems', true);
            const $tab = $form.find('.misctab');
            FwTabs.setTabColor($tab, '#FFFF8d');
            FwFormField.disable(FwFormField.getDataField($form, 'Miscellaneous'));
        }
        // color the Labor tab if LaborItems exist
        const hasLaborItem = FwFormField.getValueByDataField($form, 'HasLaborItem');
        if (hasLaborItem) {
            $form.data('hasitems', true);
            const $tab = $form.find('.labortab');
            FwTabs.setTabColor($tab, '#FFFF8d');
            FwFormField.disable(FwFormField.getDataField($form, 'Labor'));
        }
        // color the Rental Sale tab if RentalSaleItems exist
        const hasRentalSaleItem = FwFormField.getValueByDataField($form, 'HasRentalSaleItem');
        if (hasRentalSaleItem) {
            $form.data('hasitems', true);
            const $tab = $form.find('.rentalsaletab');
            FwTabs.setTabColor($tab, '#FFFF8d');
            FwFormField.disable(FwFormField.getDataField($form, 'RentalSale'));
        }

        // color the Loss and Damage tab if LossDamageItems exist
        const hasLossAndDamageItem = FwFormField.getValueByDataField($form, 'HasLossAndDamageItem');
        if (hasLossAndDamageItem) {
            $form.data('hasitems', true);
            const $tab = $form.find('.lossdamagetab');
            FwTabs.setTabColor($tab, '#FFFF8d');
            FwFormField.disable(FwFormField.getDataField($form, 'LossAndDamage'));
        }

        this.controlMutuallyExclusiveActivities($form); // Controls Activity checkboxes along with code above ^

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabPageId = $tab.attr('data-tabpageid');

            if ($tab.hasClass('profitlosstab') == true) {
                this.loadProfitAndLoss($form);
            }
            else if ($tab.hasClass('audittab') == false) {
                const $gridControls = $form.find(`#${tabPageId} [data-type="Grid"]`);
                if ((($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) || ($tab.attr('data-hasSubGrid') === 'true' && $gridControls.length > 0)) {
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
                if (!$tab.hasClass('activitytab')) {
                    $tab.addClass('tabGridsLoaded');
                }
            }
        });

        // show/hide Profit & Loss sections
        const rentalVal = FwFormField.getValueByDataField($form, 'Rental');
        const salesVal = FwFormField.getValueByDataField($form, 'Sales');
        const rentalSaleVal = FwFormField.getValueByDataField($form, 'RentalSale');
        const laborVal = FwFormField.getValueByDataField($form, 'Labor');
        const miscVal = FwFormField.getValueByDataField($form, 'Miscellaneous');
        let lossDamageVal: boolean = false;
        if (this.Module === 'Order') {
            lossDamageVal = FwFormField.getValueByDataField($form, 'LossAndDamage');
        }

        //const quikSearchMenuId = this.menuSearchId;
        if (lossDamageVal) {
            $form.find('[data-securityid="searchbtn"]').addClass('disabled')
            //$form.find(`.submenu-btn[data-securityid="${quikSearchMenuId}"]`).attr('data-enabled', 'false');
        } else {
            $form.find('[data-securityid="searchbtn"]').removeClass('disabled');
            //$form.find(`.submenu-btn[data-securityid="${quikSearchMenuId}"]`).attr('data-enabled', 'true');
        }

        //toggle profit & loss activity section visibility
        rentalVal ? $form.find('.rental-pl').show() : $form.find('.rental-pl').hide();
        salesVal ? $form.find('.sales-pl').show() : $form.find('.sales-pl').hide();
        laborVal ? $form.find('.labor-pl').show() : $form.find('.labor-pl').hide();
        miscVal ? $form.find('.misc-pl').show() : $form.find('.misc-pl').hide();
        rentalSaleVal ? $form.find('.rentalsale-pl').show() : $form.find('.rentalsale-pl').hide();

        // disable all controls on the form based on Quote/Order status
        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status === 'ORDERED' || status === 'CLOSED' || status === 'CANCELLED' || status === 'SNAPSHOT') {
            FwModule.setFormReadOnly($form);
            $form.find('.btn[data-securityid="searchbtn"]').addClass('disabled');

            this.disableOrderItemGridMenus($form);
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

        //Project Tab
        const enableProjects = FwFormField.getValueByDataField($form, 'EnableProjects');
        enableProjects ? $form.find('.projecttab').show() : $form.find('.projecttab').hide();

        //reset Currency change fields
        FwFormField.setValueByDataField($form, 'UpdateAllRatesToNewCurrency', false);
        FwFormField.setValueByDataField($form, 'ConfirmUpdateAllRatesToNewCurrency', '');

        //justin hoffman 02/11/2020 - after all, I want this option available even if manual sort is not set. There are other conditions where the user may need to do this.
        ////hide "Restore System Sorting" menu option from grids
        //if (!FwFormField.getValueByDataField($form, 'IsManualSort')) {
        //    $form.find('.gridmenu .submenu-btn .caption:contains(Restore System Sorting)').parent('.submenu-btn').hide();
        //}

        const isManualSort = FwFormField.getValueByDataField($form, 'IsManualSort');
        $form.data('ismanualsort', isManualSort);

        this.billingPeriodEvents($form);
        this.renderScheduleDateAndTimeSection($form, response);
        this.showHideDeliveryLocationField($form);
        this.applyTaxOptions($form, response);
        const $totalFields = $form.find('.totals[data-type="money"], .frame[data-type="money"], .manifest-totals [data-type="money"]');
        const $grids = $form.find('[data-name="OrderItemGrid"], [data-name="OrderManifestGrid"]');
        this.applyCurrencySymbolToTotalFields($form, response, $totalFields, $grids);


        $form.find('[data-datafield="Currency"]').attr('data-originaltext', FwFormField.getValueByDataField($form, 'Currency'));
        $form.find('[data-datafield="CurrencyId"]').attr('data-originaltext', FwFormField.getTextByDataField($form, 'CurrencyId'));


    }
    //----------------------------------------------------------------------------------------------
    applyCurrencySymbolToTotalFields($form: JQuery, response: any, $totalFields, $grids) {

        $totalFields.each((index, element) => {
            let $fwformfield, currencySymbol;
            $fwformfield = jQuery(element);
            currencySymbol = response[$fwformfield.attr('data-currencysymbol')];
            if (typeof currencySymbol == 'undefined' || currencySymbol === '') {
                currencySymbol = '$';
            }

            $fwformfield.attr('data-currencysymboldisplay', currencySymbol);

            $fwformfield
                .find('.fwformfield-value')
                .inputmask('currency', {
                    prefix: currencySymbol + ' ',
                    placeholder: "0.00",
                    min: ((typeof $fwformfield.attr('data-minvalue') !== 'undefined') ? $fwformfield.attr('data-minvalue') : undefined),
                    max: ((typeof $fwformfield.attr('data-maxvalue') !== 'undefined') ? $fwformfield.attr('data-maxvalue') : undefined),
                    digits: ((typeof $fwformfield.attr('data-digits') !== 'undefined') ? $fwformfield.attr('data-digits') : 2),
                    radixPoint: '.',
                    groupSeparator: ','
                });
        });

        //add to grids
        $grids.each((index, element) => {
            let $grid, currencySymbol;
            $grid = jQuery(element);
            currencySymbol = response["CurrencySymbol"];
            if (typeof currencySymbol != 'undefined' && currencySymbol != '') {
                $grid.attr('data-currencysymboldisplay', currencySymbol);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    disableOrderItemGridMenus($form: JQuery) {
        // Disable the 'Options' and 'Headers / Text / Sub-Totals' column in OrderItemGrid menu
        const $menus = $form.find('[data-name="OrderItemGrid"] .submenu');
        if ($menus) {
            $menus.each((i, e) => {
                const second = jQuery(e.children[1]);
                if (second) {
                    second.css({
                        'pointer-events': 'none',
                        'color': '#dcdcdc',
                    });
                } else {
                    console.error(`disableOrderItemGridMenus method menu option undefined`);
                }
            });

            // Delete option
            const $deleteOptions = $menus.find('.deleteoption');
            if ($deleteOptions) {
                $deleteOptions.parent().css({
                    'pointer-events': 'none',
                    'color': '#dcdcdc',
                });
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    applyTaxOptions($form: JQuery, response: any) {
        const $taxFields = $form.find('[data-totalfield="Tax"]');
        const tax1Name = response.Tax1Name;
        const tax2Name = response.Tax2Name;

        const updateCaption = ($fields, taxName, count) => {
            for (let i = 0; i < $fields.length; i++) {
                const $field = jQuery($fields[i]);
                const taxType = $field.attr('data-taxtype');
                if (typeof taxType != 'undefined') {
                    const taxRateName = taxType + 'TaxRate' + count;
                    const taxRatePercentage = response[taxRateName];
                    if (typeof taxRatePercentage == 'number') {
                        const caption = taxName + ` (${taxRatePercentage.toFixed(3) + '%'})`;
                        $field.find('.fwformfield-caption').text(caption);
                    }
                }
            }

            const $billingTabTaxFields = $form.find(`[data-datafield="RentalTaxRate${count}"], [data-datafield="SalesTaxRate${count}"], [data-datafield="LaborTaxRate${count}"]`);
            for (let i = 0; i < $billingTabTaxFields.length; i++) {
                const $field = jQuery($billingTabTaxFields[i]);
                const taxType = $field.attr('data-taxtype');
                if (typeof taxType != 'undefined') {
                    const newCaption = taxType + ' ' + taxName;
                    $field.find('.fwformfield-caption').text(newCaption);
                }
                $field.show();
            }
        }

        if (tax1Name != "") {
            updateCaption($taxFields, tax1Name, 1);
        }

        const $tax2Fields = $form.find('[data-totalfield="Tax2"]');
        if (tax2Name != "") {
            $tax2Fields.show();
            updateCaption($tax2Fields, tax2Name, 2);
        } else {
            $tax2Fields.hide();
            $form.find(`[data-datafield="RentalTaxRate2"], [data-datafield="SalesTaxRate2"], [data-datafield="LaborTaxRate2"]`).hide();
        }
    }
    //----------------------------------------------------------------------------------------------
    showHideDeliveryLocationField($form) {
        const inDeliveryAddressType = FwFormField.getValueByDataField($form, 'InDeliveryAddressType');
        if (inDeliveryAddressType === 'VENUE') {
            $form.find(`div[data-datafield="InDeliveryToLocation"]`).hide();
            $form.find(`div[data-datafield="InDeliveryToVenueId"]`).show();
        } else {
            $form.find(`div[data-datafield="InDeliveryToLocation"]`).show();
            $form.find(`div[data-datafield="InDeliveryToVenueId"]`).hide();
        }
        const outDeliveryAddressType = FwFormField.getValueByDataField($form, 'OutDeliveryAddressType');
        if (outDeliveryAddressType === 'VENUE') {
            $form.find(`div[data-datafield="OutDeliveryToLocation"]`).hide();
            $form.find(`div[data-datafield="OutDeliveryToVenueId"]`).show();
        } else {
            $form.find(`div[data-datafield="OutDeliveryToLocation"]`).show();
            $form.find(`div[data-datafield="OutDeliveryToVenueId"]`).hide();
        }
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
    getScheduleDatesByOrderType($form: JQuery) {
        const request: any = {};
        request.OrderBy = "OrderBy";
        request.uniqueids = {
            OrderTypeId: FwFormField.getValueByDataField($form, 'OrderTypeId'),
            Enabled: true
        }
        FwAppData.apiMethod(true, 'POST', `api/v1/orderdates/browse`, request, FwServices.defaultTimeout, response => {
            let orderDates: any = [];
            const columnNames = response.ColumnNameByIndex;
            for (let i = 0; i < response.Rows.length; i++) {
                const container: any = {};
                const item = response.Rows[i];
                for (let j = 0; j < item.length; j++) {
                    container[columnNames[j]] = item[j];
                }
                orderDates.push(container);
            }
            response.ActivityDatesAndTimes = orderDates;
            this.renderScheduleDateAndTimeSection($form, response);
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    }
    //----------------------------------------------------------------------------------------------
    renderScheduleDateAndTimeSection($form, response) {
        //const dates = `<span class="modify" style="cursor:pointer; color:blue; margin-left:20px; text-decoration:underline;">Show All Dates and Times</span>`;
        //$form.find('.activity-dates-toggle').empty().append(dates);
        $form.find('.activity-dates').empty();
        const activityDatesAndTimes = response.ActivityDatesAndTimes;
        for (let i = 0; i < activityDatesAndTimes.length; i++) {
            const row = activityDatesAndTimes[i];
            let validationClass = '';
            if (row.ActivityType === 'PICK' || row.ActivityType === 'START' || row.ActivityType === 'STOP') {
                validationClass = 'pick-date-validation';
            }
            const $row = jQuery(`<div class="flexrow date-row">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="OrderTypeDateTypeId" style="display:none;"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield ${validationClass}" data-caption="${row.DescriptionDisplayTitleCase || row.DescriptionDisplay} Date" data-dateactivitytype="${row.ActivityType}" data-datafield="Date" data-enabled="true" style="flex:0 1 150px;"></div>
                              <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-timeactivitytype="${row.ActivityType}" data-datafield="Time" data-enabled="true" style="flex:0 1 120px;"></div>
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

        ////activity dates
        //$form.find('.modify').off().on('click', e => {
        //    const scheduleFields = $form.find('.schedule-date-fields');
        //    const activityDateFields = $form.find('.activity-dates');
        //    if (scheduleFields.css('display') === 'none') {
        //        scheduleFields.show();
        //        activityDateFields.hide();
        //    } else {
        //        scheduleFields.hide();
        //        activityDateFields.show();
        //    }
        //});
        // $newFields = $fieldsDataObj.remove(oldDateFields);
        //const oldDateFields = $form.find('.og-datetime');


        const scheduleFields = $form.find('.schedule-date-fields');
        scheduleFields.remove();
        const activityDateFields = $form.find('.activity-dates');
        activityDateFields.show();

        function addNewFieldsToDataObj($form, $newFieldsObj) {
            const $fieldsDataObj = $form.data('fields');
            const $newFields = $fieldsDataObj.add($newFieldsObj);
            $form.data('fields', $newFields);
        }

        const newDateFields = $form.find('.pick-date-validation');
        addNewFieldsToDataObj($form, newDateFields);

        $form.data('beforesave', request => {
            //if ($form.find('.activity-dates:visible').length > 0) {
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
            //}
            delete request['StatusDate']; // Removing StatusDate from request since it's value is maintained at the API level
        });

        //stops field event bubbling
        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');
    }
    //----------------------------------------------------------------------------------------------
    changeDealForOrder($form: any, $tr: any, oldDeal: any) {
            const newDeal = FwBrowse.getValueByDataField($form, $tr, 'Deal');
            const $confirmation = FwConfirmation.renderConfirmation('Update Deal', '');
            $confirmation.find('.fwconfirmationbox').css('width', '500px');

            const html = [];
            html.push(`<div class="fwform" data-controller="none" style="background-color: transparent;">`);
            html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push(`    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Old Deal" data-datafield="OldDeal" data-enabled="false" style="width:480px; float:left;"></div>`);
            html.push(`  </div>`);
            html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
            html.push(`    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="New Deal" data-datafield="NewDeal" data-enabled="false" style="width:480px;float:left;"></div>`);
            html.push(`  </div>`);
            html.push(`  <div style="padding:.5em;">Are you sure you want to change the Deal on this ${this.Module}? This will update all Contracts, Sub Purchase Orders, and Invoices related to this ${this.Module}.</div>`);
            html.push(`</div>`);

            FwConfirmation.addControls($confirmation, html.join(''));
            const $apply = FwConfirmation.addButton($confirmation, `Apply`, false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');
            FwFormField.setValueByDataField($confirmation, 'OldDeal', oldDeal.Name);
            FwFormField.setValueByDataField($confirmation, 'NewDeal', newDeal);

            // apply
            $apply.on('click', e => {
                FwConfirmation.destroyConfirmation($confirmation);
                this.defaultFieldsOnDealChange($form, $tr);
            });
            // cancel
            $no.on('click', e => {
                FwConfirmation.destroyConfirmation($confirmation);
                FwFormField.setValueByDataField($form, 'DealId', oldDeal.Id, oldDeal.Name);
            });
    }
    //----------------------------------------------------------------------------------------------
    changeDepartmentForOrder($form: any, $tr: any, oldDepartment: any) {
        const department = FwBrowse.getValueByDataField($form, $tr, 'Department');
        const $confirmation = FwConfirmation.renderConfirmation('Update Department', '');
        $confirmation.find('.fwconfirmationbox').css('width', '500px');

        const html = [];
        html.push(`<div class="fwform" data-controller="none" style="background-color: transparent;">`);
        html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
        html.push(`    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Old Department" data-datafield="OldDepartment" data-enabled="false" style="width:480px; float:left;"></div>`);
        html.push(`  </div>`);
        html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
        html.push(`    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="New Department" data-datafield="NewDepartment" data-enabled="false" style="width:480px;float:left;"></div>`);
        html.push(`  </div>`);
        html.push(`  <div style="padding:.5em;">Are you sure you want to change the Department on this ${this.Module}? This will update all Contracts, Sub Purchase Orders, and Invoices related to this ${this.Module}.</div>`);
        html.push(`</div>`);

        FwConfirmation.addControls($confirmation, html.join(''));
        const $apply = FwConfirmation.addButton($confirmation, `Apply`, false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');
        FwFormField.setValueByDataField($confirmation, 'OldDepartment', oldDepartment.Name);
        FwFormField.setValueByDataField($confirmation, 'NewDepartment', department);

        // apply
        $apply.on('click', e => {
            FwConfirmation.destroyConfirmation($confirmation);
            this.defaultFieldsOnDepartmentChange($form, $tr);
        });
        // cancel
        $no.on('click', e => {
            FwConfirmation.destroyConfirmation($confirmation);
            FwFormField.setValueByDataField($form, 'DepartmentId', oldDepartment.Id, oldDepartment.Name);
        });
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
        // this.renderGrids($form);                 -- J. Pace 9/16/20 - Commenting out this invocation because it is erasing all settings attached to the grids such a Summary View, Rollup Quantities, etc.
        //const period = FwFormField.getValueByDataField($form, 'totalTypeProfitLoss');
        //this.renderFrames($form, FwFormField.getValueByDataField($form, `${this.Module}Id`), period);
        //this.dynamicColumns($form);
        this.applyOrderTypeAndRateTypeToForm($form);
        this.disableRateColumns($form);
        $activeTab.click();
    };
    //----------------------------------------------------------------------------------------------
    //04/28/2020 jason hoang and justin hoffman
    //           method to safely retrieve the Pick, Start, and Stop Dates and Times
    //           will return blank values when the Order Type has these values disabled/excluded
    getPickStartStop($form: JQuery): PickStartStop {
        function safeGetActivityDate(activityType: string) {
            let value: string = "";
            let $field = $form.find(`div[data-dateactivitytype="${activityType}"]`);
            if ($field.length) {
                value = FwFormField.getValue2($field);
            }
            return value;
        }
        function safeGetActivityTime(activityType: string) {
            let value: string = "";
            let $field = $form.find(`div[data-timeactivitytype="${activityType}"]`);
            if ($field.length) {
                value = FwFormField.getValue2($field);
            }
            return value;
        }
        let pickStartStop: PickStartStop = new PickStartStop();
        pickStartStop.PickDate = safeGetActivityDate('PICK');
        pickStartStop.PickTime = safeGetActivityTime('PICK');
        pickStartStop.StartDate = safeGetActivityDate('START');
        pickStartStop.StartTime = safeGetActivityTime('START');
        pickStartStop.StopDate = safeGetActivityDate('STOP');
        pickStartStop.StopTime = safeGetActivityTime('STOP');
        return pickStartStop;
    }
    //----------------------------------------------------------------------------------------------
    getTab($form: JQuery, tabClass: string): JQuery<HTMLElement> {
        return $form.find(`[data-type="tab"].${tabClass}`);
    }
    //----------------------------------------------------------------------------------------------
    showTab($form: JQuery, tabClass: string) {
        this.getTab($form, tabClass).show();
    }
    //----------------------------------------------------------------------------------------------
    hideTab($form: JQuery, tabClass: string) {
        this.getTab($form, tabClass).hide();
    }
    //----------------------------------------------------------------------------------------------
    highlightTab($form: JQuery, tabClass: string, fieldName: string) {
        // color the tab if records exist
        const hasRecords = FwFormField.getValueByDataField($form, fieldName);
        if (hasRecords) {
            FwTabs.setTabColor(this.getTab($form, tabClass), '#FFFF8d');
        }
    }
    //----------------------------------------------------------------------------------------------
}
class PickStartStop {
    PickDate: string;
    PickTime: string;
    StartDate: string;
    StartTime: string;
    StopDate: string;
    StopTime: string;
}
//----------------------------------------------------------------------------------------------
var OrderBaseController = new OrderBase();
