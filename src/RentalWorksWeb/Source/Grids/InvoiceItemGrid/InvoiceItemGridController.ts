class InvoiceItemGrid {
    Module: string = 'InvoiceItemGrid';
    apiurl: string = 'api/v1/invoiceitem';
    //----------------------------------------------------------------------------------------------
    onRowNewMode($control: JQuery, $tr: JQuery) {
        const $form = $control.closest('.fwform');
        const $grid = $tr.parents('[data-grid="InvoiceItemGrid"]');

        if ($grid.hasClass('R')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'R' });
        } else if ($grid.hasClass('S')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'S' });
        } else if ($grid.hasClass('P')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'P' });
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        if ($form.attr('data-controller') !== 'BillingWorksheetController') {
            var invoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
            if (invoiceId != '') {
                request.uniqueids = {
                    InvoiceId: invoiceId
                }
            }
        }
        switch (datafield) {
            case 'InventoryId':
                const recType = $tr.find('div[data-browsedatafield="RecType"] input.value').val();
                if (recType !== null) {
                    switch (recType) {
                        case 'R':
                            request.uniqueids = {
                                AvailFor: 'R'
                            };
                            break;
                        case 'S':
                            request.uniqueids = {
                                AvailFor: 'S'
                            };
                            break;
                        case 'P':
                            request.uniqueids = {
                                AvailFor: 'P'
                            };
                            break;
                    }
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                break;
            case 'ItemId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateitem`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            //// Bold Row
            //if ($tr.find('.order-item-bold').text() === 'true') {
            //    $tr.css('font-weight', "bold");
            //}

            // Group Header Row
            if ($tr.find('.itemclass').text() === 'GH') {
                $tr.css('font-weight', "bold");
                $tr.css('background-color', "#ffffcc");
                $tr.find('.field:not(.groupheaderline) ').text('');
            }

            // Text Row
            if ($tr.find('.itemclass').text() === 'T') {
                $tr.find('.field:not(.textline) ').text('');
            }

            // Sub-Total Row
            if ($tr.find('.itemclass').text() === 'ST') {
                $tr.css('font-weight', "bold");
                $tr.css('background-color', "#ffff80");
                $tr.find('.field:not(.subtotalline) ').text('');
            }
        });

        //$generatedtr.find('div[data-browsedatafield="ItemId"]').data('onchange', function ($tr) {
        //    $generatedtr.find('.field[data-browsedatafield="InventoryId"] input').val($tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue'));
        //    $generatedtr.find('.field[data-browsedatafield="InventoryId"] input.text').val($tr.find('.field[data-browsedatafield="ICode"]').attr('data-originalvalue'));
        //    $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        //    $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
        //});
        //----------------------------------------------------------------------------------------------
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            //const rowQty = $tr.find('.field[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            //if (rowQty === '' || rowQty === undefined) {
            $generatedtr.find('.field[data-browsedatafield="Quantity"] input').val("1");
            //}
        });

        if ($generatedtr.hasClass("newmode")) { }

        //if ($form.attr('data-controller') === 'TemplateController') {
        //    $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
        //        var warehouse = FwFormField.getTextByDataField($form, 'WarehouseId');
        //        var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        //        let warehouseCode = $form.find('[data-datafield="WarehouseCode"] input').val();
        //        let inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
        //        let rateType = $form.find('[data-datafield="RateType"] input').val();
        //        let inventoryType = $generatedtr.find('[data-browsedatafield="InventoryId"]').attr('data-validationname');

        //        $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        //        $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
        //        $generatedtr.find('.field[data-browsedatafield="SubQuantity"] input').val("0");
        //        $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
        //        $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
        //        $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
        //        $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);

        //        if ($generatedtr.hasClass("newmode")) {
        //            FwAppData.apiMethod(true, 'GET', "api/v1/pricing/" + inventoryId + "/" + warehouseId, null, FwServices.defaultTimeout, function onSuccess(response) {
        //                switch (rateType) {
        //                    case 'DAILY':
        //                        $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].DailyRate);
        //                        break;
        //                    case 'WEEKLY':
        //                        $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].WeeklyRate);
        //                        break;
        //                    case 'MONTHLY':
        //                        $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].MonthlyRate);
        //                        break;
        //                }
        //            }, null, $form);
        //        }
        //    });
        //}
        //----------------------------------------------------------------------------------------------
        function calculateExtended(type, field?) {
            let rateType, recType, fromDate, toDate, quantity, rate, daysPerWeek, discountPercent, weeklyExtended, unitExtended, periodExtended,
                monthlyExtended, unitDiscountAmount, weeklyDiscountAmount, monthlyDiscountAmount, periodDiscountAmount;
            rateType = $form.find('[data-datafield="RateType"] input').val();
            recType = $generatedtr.find('.field[data-browsedatafield="RecType"] input').val();
            fromDate = $generatedtr.find('.field[data-browsedatafield="FromDate"] input').val();
            toDate = $generatedtr.find('.field[data-browsedatafield="ToDate"] input').val();
            quantity = $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val();
            rate = $generatedtr.find('.field[data-browsedatafield="Price"] input').val();
            daysPerWeek = $generatedtr.find('.field[data-browsedatafield="DaysPerWeek"] input').val();
            //discountPercent = $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val();
            if (field == "DiscountPercent") {
                discountPercent = $generatedtr.find('.field[data-browsedatafield="DiscountPercentDisplay"] input').val();
            }
            else {
                discountPercent = $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val();
            }
            weeklyExtended = $generatedtr.find('.field[data-browsedatafield="WeeklyExtended"] input').val();
            unitExtended = $generatedtr.find('.field[data-browsedatafield="UnitExtended"] input').val();
            periodExtended = $generatedtr.find('.field[data-browsedatafield="PeriodExtended"] input').val();
            monthlyExtended = $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val();
            unitDiscountAmount = $generatedtr.find('.field[data-browsedatafield="UnitDiscountAmount"] input').val();
            weeklyDiscountAmount = $generatedtr.find('.field[data-browsedatafield="WeeklyDiscountAmount"] input').val();
            monthlyDiscountAmount = $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val();
            periodDiscountAmount = $generatedtr.find('.field[data-browsedatafield="PeriodDiscountAmount"] input').val();

            let apiurl = "api/v1/orderitem/"

            if (type == "Extended") {
                apiurl += "calculateextended?RateType="
            } else if (type == "Discount") {
                apiurl += "calculatediscountpercent?RateType="
            }
            apiurl +=
                rateType
                + "&RecType="
                + recType
                + "&FromDate="
                + fromDate
                + "&ToDate="
                + toDate
                + "&Quantity="
                + quantity
                + "&Rate="
                + (+(rate.substring(1).replace(',', '')))
                + "&DaysPerWeek="
                + daysPerWeek

            if (type == 'Extended') {
                apiurl += "&DiscountPercent=" + discountPercent;

                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout, function onSuccess(response) {
                    $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val(response.DiscountPercent);
                    $generatedtr.find('.field[data-browsedatafield="UnitExtended"] input').val(response.UnitExtended);
                    $generatedtr.find('.field[data-browsedatafield="UnitDiscountAmount"] input').val(response.UnitDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyExtended"] input').val(response.WeeklyExtended);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyDiscountAmount"] input').val(response.WeeklyDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val(response.MonthlyExtended);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val(response.MonthlyDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="PeriodExtended"] input').val(response.PeriodExtended);
                    $generatedtr.find('.field[data-browsedatafield="PeriodDiscountAmount"] input').val(response.PeriodDiscountAmount);
                }, null, null);
            }

            if (type == 'Discount') {
                switch (field) {
                    case 'UnitExtended':
                        apiurl += "&UnitExtended=" + (+unitExtended.substring(1).replace(',', ''));
                        break;
                    case 'WeeklyExtended':
                        apiurl += "&WeeklyExtended=" + (+weeklyExtended.substring(1).replace(',', ''));
                        break;
                    case 'MonthlyExtended':
                        apiurl += "&MonthlyExtended=" + (+monthlyExtended.substring(1).replace(',', ''));
                        break;
                    case 'PeriodExtended':
                        apiurl += "&PeriodExtended=" + (+periodExtended.substring(1).replace(',', ''));
                        break;
                    case 'UnitDiscountAmount':
                        apiurl += "&UnitDiscountAmount=" + (+unitDiscountAmount.substring(1).replace(',', ''));
                        break;
                    case 'WeeklyDiscountAmount':
                        apiurl += "&WeeklyDiscountAmount=" + (+weeklyDiscountAmount.substring(1).replace(',', ''));
                        break;
                    case 'MonthlyDiscountAmount':
                        apiurl += "&MonthlyDiscountAmount=" + (+monthlyDiscountAmount.substring(1).replace(',', ''));
                        break;
                    case 'PeriodDiscountAmount':
                        apiurl += "&PeriodDiscountAmount=" + (+periodDiscountAmount.substring(1).replace(',', ''));
                        break;
                }
                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout, function onSuccess(response) {
                    $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val(response.DiscountPercent);
                    $generatedtr.find('.field[data-browsedatafield="DiscountPercentDisplay"] input').val(response.DiscountPercent);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyExtended"] input').val(response.WeeklyExtended);
                    $generatedtr.find('.field[data-browsedatafield="UnitExtended"] input').val(response.UnitExtended);
                    $generatedtr.find('.field[data-browsedatafield="PeriodExtended"] input').val(response.PeriodExtended);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val(response.MonthlyExtended);
                    $generatedtr.find('.field[data-browsedatafield="UnitDiscountAmount"] input').val(response.UnitDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyDiscountAmount"] input').val(response.WeeklyDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val(response.MonthlyDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="PeriodDiscountAmount"] input').val(response.PeriodDiscountAmount);
                }, null, null);
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    toggleOrderItemView($form: any, event: any, module) {
        // Toggle between Detail and Summary view in all InvoiceItemGrid
        let request: any = {};

        //if (recType === 'R') {
        //    $invoiceItemGrid = $form.find('.rentalgrid [data-name="InvoiceItemGrid"]');
        //}
        //if (recType === 'S') {
        //    $invoiceItemGrid = $form.find('.salesgrid [data-name="InvoiceItemGrid"]');
        //}
        //if (recType === 'L') {
        //    $invoiceItemGrid = $form.find('.laborgrid [data-name="InvoiceItemGrid"]');
        //}
        //if (recType === 'M') {
        //    $invoiceItemGrid = $form.find('.miscgrid [data-name="InvoiceItemGrid"]');
        //}
        //if (recType === '') {
        //    $invoiceItemGrid = $form.find('.combinedgrid div[data-grid="InvoiceItemGrid"]');
        //}

        const $element = jQuery(event.currentTarget);
        const $invoiceItemGrid = $element.closest('[data-name="InvoiceItemGrid"]');
        let isSummary;
        if ($invoiceItemGrid.data('isSummary') === false) {
            isSummary = true;
            $invoiceItemGrid.data('isSummary', true);
            $element.children().text('Detail View')
        }
        else {
            isSummary = false;
            $invoiceItemGrid.data('isSummary', false);
            $element.children().text('Summary View')
        }

        const recType = $element.closest('[data-grid="InvoiceItemGrid"]').attr('class');
        const orderId = FwFormField.getValueByDataField($form, `${module}Id`);
        $invoiceItemGrid.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: orderId,
                Summary: isSummary,
                RecType: recType
            }
            request.pagesize = 9999;
            request.orderby = "RowNumber,RecTypeDisplay"

            const isSubGrid = $element.closest('[data-grid="InvoiceItemGrid"]').attr('data-issubgrid');
            if (isSubGrid === "true") {
                request.uniqueids.Subs = true;
            }
        });

        $invoiceItemGrid.data('beforesave', request => {
            request.OrderId = orderId;
            request.RecType = recType;
            request.Summary = isSummary;
        });

        FwBrowse.search($invoiceItemGrid);
    };
}
//----------------------------------------------------------------------------------------------
var InvoiceItemGridController = new InvoiceItemGrid();