class OrderItemGrid {
    Module: string = 'OrderItemGrid';
    apiurl: string = 'api/v1/orderitem';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        let $form = $control.closest('.fwform');
        let $grid = $tr.parents('[data-grid="OrderItemGrid"]');
        let inventoryType;
        if ($form[0].dataset.controller !== "TemplateController" && $form[0].dataset.controller !== "PurchaseOrderController") {
            var pickDate = FwFormField.getValueByDataField($form, 'PickDate');
            var pickTime = FwFormField.getValueByDataField($form, 'PickTime');
            var fromDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
            var fromTime = FwFormField.getValueByDataField($form, 'EstimatedStartTime');
            var toDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
            var toTime = FwFormField.getValueByDataField($form, 'EstimatedStopTime');
        };

        if ($grid.hasClass('R')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'R' });
            inventoryType = 'Rental';
        } else if ($grid.hasClass('S')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'S' });
            inventoryType = 'Sales';
        } else if ($grid.hasClass('M')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'M' });
            inventoryType = 'Misc';
        } else if ($grid.hasClass('L')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'L' });
            inventoryType = 'Labor';
        } else if ($grid.hasClass('RS')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'RS' });
            inventoryType = 'RentalSales';
        } else if ($grid.hasClass('A')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'A' });
            inventoryType = 'Combined';
        } else if ($grid.hasClass('P')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'P' });
            inventoryType = 'Parts';
        }   else if ($grid.hasClass('F')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'F' });
            inventoryType = 'LossAndDamage';
        }

        if ($form[0].dataset.controller !== "TemplateController" && $form[0].dataset.controller !== "PurchaseOrderController") {
            let daysPerWeek, discountPercent;
            if (inventoryType !== 'RentalSales') {
                discountPercent = FwFormField.getValueByDataField($form, `${inventoryType}DiscountPercent`);
            }
            if (inventoryType == 'Rental') {
                daysPerWeek = FwFormField.getValueByDataField($form, `RentalDaysPerWeek`);
            };

            FwBrowse.setFieldValue($grid, $tr, 'PickDate', { value: pickDate });
            FwBrowse.setFieldValue($grid, $tr, 'PickTime', { value: pickTime });
            FwBrowse.setFieldValue($grid, $tr, 'FromDate', { value: fromDate });
            FwBrowse.setFieldValue($grid, $tr, 'FromTime', { value: fromTime });
            FwBrowse.setFieldValue($grid, $tr, 'ToDate', { value: toDate });
            FwBrowse.setFieldValue($grid, $tr, 'ToTime', { value: toTime });
            if (inventoryType !== 'RentalSales') {
                FwBrowse.setFieldValue($grid, $tr, 'DiscountPercent', { value: discountPercent });
                FwBrowse.setFieldValue($grid, $tr, 'DiscountPercentDisplay', { value: discountPercent });
            }
            if (inventoryType == 'Rental') {
                FwBrowse.setFieldValue($grid, $tr, 'DaysPerWeek', { value: daysPerWeek });
            };
        }
    }

    beforeValidateItem = function ($browse, $grid, request, datafield, $tr) {
        var rate = $tr.find('div[data-browsedatafield="RecType"] input.value').val();

        if (rate !== null) {
            switch (rate) {
                case 'R':
                    request.uniqueIds = {
                        AvailFor: 'R'
                    };
                    break;
                case 'S':
                    request.uniqueIds = {
                        AvailFor: 'S'
                    };
                    break;
                case 'M':
                    request.uniqueIds = {
                        AvailFor: 'M'
                    };
                    break;
                case 'L':
                    request.uniqueIds = {
                        AvailFor: 'L'
                    };
                    break;
            }
        }
    };

    beforeValidateBarcode = function ($browse, $grid, request, datafield, $tr) {
        let inventoryId = $tr.find('.field[data-browsedatafield="InventoryId"] input').val();
        if (inventoryId != '') {
            request.uniqueIds = {
                InventoryId: inventoryId
            }
        }
    }

    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');

        if ($form.attr('data-controller') === 'OrderController' || $form.attr('data-controller') === 'QuoteController' || $form.attr('data-controller') === 'PurchaseOrderController') {
            FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
                // Bold Row
                if ($tr.find('.order-item-bold').text() === 'true') {
                    $tr.css('font-weight', "bold");
                }

                // Summarized Row
                if ($tr.find('.order-item-rowsrolledup').text() === 'true') {
                    $tr.css('font-style', "italic");
                }

                const availabilityState = FwBrowse.getValueByDataField($control, $generatedtr, 'AvailabilityState');
                const $availQty = $generatedtr.find('[data-browsedatafield="AvailableQuantity"]')
                $availQty.attr('data-state', availabilityState);
                $availQty.css('cursor', 'pointer');
            });


            FwBrowse.setAfterRenderFieldCallback($control, ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => {
                // Lock Fields
                if ($tr.find('.order-item-lock').text() === 'true') {
                    $tr.find('.field-to-lock').css('background-color', "#f5f5f5");
                    $tr.find('.field-to-lock').attr('data-formreadonly', 'true');
                    // disabled grids were rendering with different shade background color
                    if ($control.attr('data-enabled') === 'false') {
                        $tr.find('.field-to-lock').css('background-color', 'transparent');
                    }
                }

                //enable editing price on misc items
                const isMiscClass = FwBrowse.getValueByDataField($control, $generatedtr, 'ItemClass');
                if (isMiscClass === 'M') {
                    $generatedtr.find('[data-browsedatafield="Price"]').attr('data-formreadonly', 'false');
                }
            });

            //availability calendar popup
            $generatedtr.find('div[data-browsedatafield="AvailableQuantity"]').on('click', e => {
                let $popup = jQuery(`
                <div>
                    <div class="close-modal" style="background-color:white;top:.8em;right:.1em; padding-right:.5em; border-radius:.2em;justify-content:flex-end;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                    <div id="availabilityCalendarPopup" class="fwform fwcontrol fwcontainer" data-control="FwContainer" data-type="form" style="overflow:auto;max-height:90vh;max-width:90vw;background-color:white; margin-top:2em; border:2px solid gray;">
                        <div class="flexcolumn" style="overflow:auto;">
                             <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Availability Calendar">
                              <div data-control="FwScheduler" style="overflow:auto;" class="fwcontrol fwscheduler calendar"></div>
                              <div data-control="FwSchedulerDetailed" class="fwcontrol fwscheduler realscheduler"></div>
                             </div>
                        </div>
                    </div>
                </div>`);
                FwControl.renderRuntimeControls($popup.find('.fwcontrol'));
                $popup = FwPopup.renderPopup($popup, { ismodal: true });
                FwPopup.showPopup($popup);
                $form.data('onscreenunload', () => { FwPopup.destroyPopup($popup); });

                $popup.find('.close-modal').on('click', function (e) {
                    FwPopup.detachPopup($popup);
                });

                const $calendar = $popup.find('.calendar');
                const $scheduler = $popup.find('.realscheduler');
                const inventoryId = FwBrowse.getValueByDataField($control, $generatedtr, 'InventoryId');

                FwScheduler.renderRuntimeHtml($calendar);
                FwScheduler.init($calendar);
                FwScheduler.loadControl($calendar);
                RentalInventoryController.addCalendarEvents($generatedtr, $calendar, inventoryId);
                const schddate = FwScheduler.getTodaysDate();
                FwScheduler.navigate($calendar, schddate);
                FwScheduler.refresh($calendar);

                FwSchedulerDetailed.renderRuntimeHtml($scheduler);
                FwSchedulerDetailed.init($scheduler);
                FwSchedulerDetailed.loadControl($scheduler);
                RentalInventoryController.addSchedulerEvents($generatedtr, $scheduler, inventoryId);
                FwSchedulerDetailed.navigate($scheduler, schddate, 35);
                FwSchedulerDetailed.refresh($scheduler);
            });

            $generatedtr.find('div[data-browsedatafield="ItemId"]').data('onchange', function ($tr) {
                $generatedtr.find('.field[data-browsedatafield="InventoryId"] input').val($tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="InventoryId"] input.text').val($tr.find('.field[data-browsedatafield="ICode"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
            });

            $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
                var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                const warehouseCode = FwFormField.getValueByDataField($form, 'WarehouseCode');
                let inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
                let officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
                let rateType = $form.find('[data-datafield="RateType"] input').val();
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                let discountPercent;
                let daysPerWeek;
                let isRentalGrid = jQuery($control).parent('[data-grid="OrderItemGrid"]').hasClass('R');
                $generatedtr.find('.field[data-browsedatafield="ItemId"] input').val('');
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));

                if ($form[0].dataset.controller !== "TemplateController" && $form[0].dataset.controller !== "PurchaseOrderController") {
                    discountPercent = FwFormField.getValueByDataField($form, 'RentalDiscountPercent');
                    FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercent', { value: discountPercent });
                    FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercentDisplay', { value: discountPercent });
                    if (isRentalGrid === true) {
                        daysPerWeek = FwFormField.getValueByDataField($form, `RentalDaysPerWeek`);
                        FwBrowse.setFieldValue($control, $generatedtr, 'DaysPerWeek', { value: daysPerWeek });
                    }
                }

                if ($generatedtr.hasClass("newmode")) {
                    FwAppData.apiMethod(true, 'GET', "api/v1/pricing/" + inventoryId + "/" + warehouseId, null, FwServices.defaultTimeout, function onSuccess(response) {
                        switch (rateType) {
                            case 'DAILY':
                                $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].DailyRate);
                                break;
                            case 'WEEKLY':
                                $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].WeeklyRate);
                                break;
                            case 'MONTHLY':
                                $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].MonthlyRate);
                                break;
                        }
                    }, null, $form);

                    FwAppData.apiMethod(true, 'GET', "api/v1/taxable/" + inventoryId + "/" + officeLocationId, null, FwServices.defaultTimeout, function onSuccess(response) {
                        if (response[0].Taxable) {
                            $generatedtr.find('.field[data-browsedatafield="Taxable"] input').prop('checked', 'true');
                        }
                    }, null, $form);
                    $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                    $generatedtr.find('.field[data-browsedatafield="SubQuantity"] input').val("0");
                    $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                    $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                    $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                    $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);
                }
            });

            $generatedtr.find('div[data-browsedatafield="FromDate"]').on('change', 'input.value', function ($tr) {
                calculateExtended('Extended');
            });
            $generatedtr.find('div[data-browsedatafield="ToDate"]').on('change', 'input.value', function ($tr) {
                calculateExtended('Extended');
            });
            $generatedtr.find('div[data-browsedatafield="QuantityOrdered"]').on('change', 'input.value', function ($tr) {
                calculateExtended('Extended');
            });
            $generatedtr.find('div[data-browsedatafield="Price"]').on('change', 'input.value', function ($tr) {
                calculateExtended('Extended');
            });
            $generatedtr.find('div[data-browsedatafield="DaysPerWeek"]').on('change', 'input.value', function ($tr) {
                calculateExtended('Extended');
            });
            $generatedtr.find('div[data-browsedatafield="DiscountPercentDisplay"]').on('change', 'input.value', function ($tr) {
                calculateExtended('Extended', 'DiscountPercent');
            });
            $generatedtr.find('div[data-browsedatafield="UnitExtended"]').on('change', 'input.value', function ($tr) {
                updatePrice('Discount', 'UnitExtended', 'Unit');
            });
            $generatedtr.find('div[data-browsedatafield="WeeklyExtended"]').on('change', 'input.value', function ($tr) {
                updatePrice('Discount', 'WeeklyExtended', 'Weekly');
            });
            $generatedtr.find('div[data-browsedatafield="MonthlyExtended"]').on('change', 'input.value', function ($tr) {
                updatePrice('Discount', 'MonthlyExtended', 'Monthly');
            });
            $generatedtr.find('div[data-browsedatafield="PeriodExtended"]').on('change', 'input.value', function ($tr) {
                updatePrice('Discount', 'PeriodExtended', 'Period');
            });
            $generatedtr.find('div[data-browsedatafield="UnitDiscountAmount"]').on('change', 'input.value', function ($tr) {
                updatePrice('Discount', 'UnitDiscountAmount', 'Unit');
            });
            $generatedtr.find('div[data-browsedatafield="WeeklyDiscountAmount"]').on('change', 'input.value', function ($tr) {
                updatePrice('Discount', 'WeeklyDiscountAmount', 'Weekly');
            });
            $generatedtr.find('div[data-browsedatafield="MonthlyDiscountAmount"]').on('change', 'input.value', function ($tr) {
                updatePrice('Discount', 'MonthlyDiscountAmount', 'Monthly');
            });
            $generatedtr.find('div[data-browsedatafield="PeriodDiscountAmount"]').on('change', 'input.value', function ($tr) {
                updatePrice('Discount', 'PeriodDiscountAmount', 'Period');
            });
        }
        if ($form.attr('data-controller') === 'TemplateController') {
            $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
                let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                let warehouseCode = FwFormField.getValueByDataField($form, 'WarehouseCode');
                let inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
                let rateType = FwFormField.getValueByDataField($form, 'RateType');

                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                $generatedtr.find('.field[data-browsedatafield="SubQuantity"] input').val("0");
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);

                if ($generatedtr.hasClass("newmode")) {
                    FwAppData.apiMethod(true, 'GET', `api/v1/pricing/${inventoryId}/${warehouseId}`, null, FwServices.defaultTimeout,
                        response => {
                            switch (rateType) {
                                case 'DAILY':
                                    FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response[0].DailyRate });
                                    break;
                                case 'WEEKLY':
                                    FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response[0].WeeklyRate });
                                    break;
                                case 'MONTHLY':
                                    FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response[0].MonthlyRate });
                                    break;
                            }
                        }, ex => FwFunc.showError(ex), $form);
                }
            });
        }

        function updatePrice(type, field?, periodType?) {
            let rate: number = +FwBrowse.getValueByDataField($control, $generatedtr, 'Price');
            const extendedVal: number = +FwBrowse.getValueByDataField($control, $generatedtr, `${periodType}Extended`);
            if (rate == 0) {
                if (extendedVal == 0) {
                    FwNotification.renderNotification('WARNING', 'Unable to apply discount because Price and Extended values are 0.');
                    return;
                } else {
                    let discount;
                    const quantity: number = +FwBrowse.getValueByDataField($control, $generatedtr, 'QuantityOrdered');
                    const discountPercent: number = +FwBrowse.getValueByDataField($control, $generatedtr, 'DiscountPercent');
                    const billablePeriods: number = +FwBrowse.getValueByDataField($control, $generatedtr, 'BillablePeriods');
                    const discountAmount: number = +FwBrowse.getValueByDataField($control, $generatedtr, `${periodType}DiscountAmount`);

                    if (discountPercent == 0) {
                        if (field === 'UnitExtended') {
                            rate = extendedVal + discountAmount;
                        } else if (field === 'WeeklyExtended') {
                            if (quantity == 0) {
                                FwNotification.renderNotification('ERROR', 'Quantity must be greater than 0.');
                                return;
                            } else {
                                rate = ((extendedVal + discountAmount) / quantity);
                            }
                        } else if (field === 'PeriodExtended') {
                            if (quantity == 0 || billablePeriods == 0) {
                                FwNotification.renderNotification('ERROR', 'Either Quantity or Billable Periods must be greater than 0.')
                                return;
                            } else {
                                rate = ((extendedVal + discountAmount) / (quantity * billablePeriods));
                            }
                        } else {
                            FwNotification.renderNotification('ERROR', `Unknown field: ${field}`);
                            return;
                        }
                    } else {
                        discount = 1 - (discountPercent / 100);
                        if (discount === 0) {
                            FwNotification.renderNotification('ERROR', `Discount percent must be less than 100.`);
                            return;
                        } else {
                            if (field === 'UnitExtended') {
                                rate = (extendedVal / discount);
                            } else if (field === 'WeeklyExtended') {
                                if (quantity == 0) {
                                    FwNotification.renderNotification('ERROR', 'Quantity must be greater than 0.')
                                    return;
                                } else {
                                    rate = ((extendedVal / quantity) / discount);
                                }
                            } else if (field === 'PeriodExtended') {
                                if (quantity == 0 || billablePeriods == 0) {
                                    FwNotification.renderNotification('ERROR', 'Either Quantity or Billable Periods must be greater than 0.')
                                    return;
                                } else {
                                    rate = ((extendedVal / (quantity * billablePeriods)) / discount);
                                }
                            } else {
                                FwNotification.renderNotification('ERROR', `Unknown field: ${field}`);
                                return;
                            }
                        }
                    }
                    let rateDisplay = Number(rate.toFixed(2)).toLocaleString();
                    const rateLength = rateDisplay.length;
                    const decimalIndex = rateDisplay.indexOf('.');
                    if (rateLength - decimalIndex > rateLength) {
                        rateDisplay = `${rateDisplay}.00`;
                    } else if (rateLength - decimalIndex == 2) {
                        rateDisplay = `${rateDisplay}0`;
                    }
                    const $confirmation = FwConfirmation.renderConfirmation(`Update Rate`, `Update Rate to ${rateDisplay}?`);
                    const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                    const $no = FwConfirmation.addButton($confirmation, 'No', false);

                    $yes.on('click', () => {
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: rate.toString() });
                        calculateExtended(type, field);
                    });

                    $no.on('click', () => {
                        FwConfirmation.destroyConfirmation($confirmation);
                        const originalVal = $generatedtr.find(`[data-browsedatafield="${field}"]`).attr('data-originalvalue');
                        FwBrowse.setFieldValue($control, $generatedtr, field, { value: originalVal });
                    });
                }
            } else {
                calculateExtended(type, field);
            }
        }

        function calculateExtended(type, field?) {
            let rateType, recType, fromDate, toDate, quantity, rate, daysPerWeek, discountPercent, weeklyExtended, unitExtended, periodExtended,
                monthlyExtended, unitDiscountAmount, weeklyDiscountAmount, monthlyDiscountAmount, periodDiscountAmount;
            rateType = FwFormField.getValueByDataField($form, 'RateType');
            recType = FwBrowse.getValueByDataField($control, $generatedtr, 'RecType');
            fromDate = FwBrowse.getValueByDataField($control, $generatedtr, 'FromDate');
            toDate = FwBrowse.getValueByDataField($control, $generatedtr, 'ToDate');
            quantity = FwBrowse.getValueByDataField($control, $generatedtr, 'QuantityOrdered');
            rate = FwBrowse.getValueByDataField($control, $generatedtr, 'Price');
            daysPerWeek = FwBrowse.getValueByDataField($control, $generatedtr, 'DaysPerWeek');
            if (field == "DiscountPercent") {
                discountPercent = FwBrowse.getValueByDataField($control, $generatedtr, 'DiscountPercentDisplay');
            }
            else {
                discountPercent = FwBrowse.getValueByDataField($control, $generatedtr, 'DiscountPercent');
            }
            weeklyExtended = FwBrowse.getValueByDataField($control, $generatedtr, 'WeeklyExtended');
            unitExtended = FwBrowse.getValueByDataField($control, $generatedtr, 'UnitExtended');
            periodExtended = FwBrowse.getValueByDataField($control, $generatedtr, 'PeriodExtended');
            //monthlyExtended = $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val();
            unitDiscountAmount = FwBrowse.getValueByDataField($control, $generatedtr, 'UnitDiscountAmount');
            weeklyDiscountAmount = FwBrowse.getValueByDataField($control, $generatedtr, 'WeeklyDiscountAmount');
            //monthlyDiscountAmount = $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val();
            periodDiscountAmount = FwBrowse.getValueByDataField($control, $generatedtr, 'PeriodDiscountAmount');

            let apiurl = "api/v1/orderitem/"

            if (type == "Extended") {
                apiurl += "calculateextended?";
            } else if (type == "Discount") {
                apiurl += "calculatediscountpercent?";
            }
            apiurl += `RateType=${rateType}&RecType=${recType}&FromDate=${fromDate}&ToDate=${toDate}&Quantity=${quantity}&Rate=${rate}&DaysPerWeek=${daysPerWeek}`;

            if (type == 'Extended') {
                apiurl += `&DiscountPercent=${discountPercent}`;

                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercent', { value: (response.DiscountPercent || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'UnitExtended', { value: (response.UnitExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'UnitDiscountAmount', { value: (response.UnitDiscountAmount || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'WeeklyExtended', { value: (response.WeeklyExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'WeeklyDiscountAmount', { value: (response.WeeklyDiscountAmount || 0).toString() });
                        //FwBrowse.setFieldValue($control, $generatedtr, 'MonthlyExtended', { value: response.MonthlyExtended });
                        //FwBrowse.setFieldValue($control, $generatedtr, 'MonthlyDiscountAmount', { value: response.MonthlyDiscountAmount });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodExtended', { value: (response.PeriodExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodDiscountAmount', { value: (response.PeriodDiscountAmount || 0).toString() });
                    }, ex => FwFunc.showError(ex), null);
            }

            if (type == 'Discount') {
                switch (field) {
                    case 'UnitExtended':
                        apiurl += `&UnitExtended=${unitExtended}`;
                        break;
                    case 'WeeklyExtended':
                        apiurl += `&WeeklyExtended=${weeklyExtended}`;
                        break;
                    case 'MonthlyExtended':
                        apiurl += `&MonthlyExtended=${monthlyExtended}`;
                        break;
                    case 'PeriodExtended':
                        apiurl += `&PeriodExtended=${periodExtended}`;
                        break;
                    case 'UnitDiscountAmount':
                        apiurl += `&UnitDiscountAmount=${unitDiscountAmount}`;
                        break;
                    case 'WeeklyDiscountAmount':
                        apiurl += `&WeeklyDiscountAmount=${weeklyDiscountAmount}`;
                        break;
                    case 'MonthlyDiscountAmount':
                        apiurl += `&MonthlyDiscountAmount=${monthlyDiscountAmount}`;
                        break;
                    case 'PeriodDiscountAmount':
                        apiurl += `&PeriodDiscountAmount=${periodDiscountAmount}`;
                        break;
                }
                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercent', { value: (response.DiscountPercent || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercentDisplay', { value: (response.DiscountPercent || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'UnitExtended', { value: (response.UnitExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'UnitDiscountAmount', { value: (response.UnitDiscountAmount || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'WeeklyExtended', { value: (response.WeeklyExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'WeeklyDiscountAmount', { value: (response.WeeklyDiscountAmount || 0).toString() });
                        //FwBrowse.setFieldValue($control, $generatedtr, 'MonthlyExtended', { value: response.MonthlyExtended });
                        //FwBrowse.setFieldValue($control, $generatedtr, 'MonthlyDiscountAmount', { value: response.MonthlyDiscountAmount });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodExtended', { value: (response.PeriodExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodDiscountAmount', { value: (response.PeriodDiscountAmount || 0).toString() });
                    }, ex => FwFunc.showError(ex), null);
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    //toggleOrderItemView($form: any, event: any, module) {
    //    // Toggle between Detail and Summary view in all OrderItemGrid
    //    let $element, $orderItemGrid, isSummary, orderId, isSubGrid;
    //    $element = jQuery(event.currentTarget);
    //    isSubGrid = $element.closest('[data-grid="OrderItemGrid"]').attr('data-issubgrid');
    //    orderId = FwFormField.getValueByDataField($form, `${module}Id`);
    //    //const totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]

    //    $orderItemGrid = $element.closest('[data-name="OrderItemGrid"]');

    //    if ($orderItemGrid.data('isSummary') === false) {
    //        isSummary = true;
    //        $orderItemGrid.data('isSummary', true);
    //        $element.children().text('Detail View')
    //    }
    //    else {
    //        isSummary = false;
    //        $orderItemGrid.data('isSummary', false);
    //        $element.children().text('Summary View')
    //    }

    //    $orderItemGrid.data('ondatabind', request => {
    //        request.uniqueids = {
    //            OrderId: orderId,
    //            Summary: isSummary,
    //        }
    //        request.orderby = "RowNumber,RecTypeDisplay"
    //        request.totalfields = totalFields;
    //        if (isSubGrid === "true") {
    //            request.uniqueids.Subs = true;
    //        }
    //    });

    //    FwBrowse.search($orderItemGrid);
    //};
    //----------------------------------------------------------------------------------------------
    orderItemGridBoldUnbold($browse: any, event: any): void {
        let orderId, $selectedCheckBoxes, boldItems = [];
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('tbody .cbselectrow:checked');

        if (orderId != null) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                let orderItem: any = {};
                let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                let orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                let description = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Description"]').attr('data-originalvalue');
                let quantityOrdered = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue');
                let recType = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RecType"]').attr('data-originalvalue');
                let rowsRolledUp = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RowsRolledUp"]').attr('data-originalvalue');

                orderItem.OrderItemId = orderItemId
                orderItem.OrderId = orderId;
                orderItem.Description = description;
                orderItem.QuantityOrdered = quantityOrdered;
                orderItem.RecType = recType;
                orderItem.RowsRolledUp = rowsRolledUp;

                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Bold"]').attr('data-originalvalue') === 'true') {
                    orderItem.Bold = false;
                } else {
                    orderItem.Bold = true;
                }
                boldItems.push(orderItem);
            }
        }

        boldUnboldItem(boldItems);

        function boldUnboldItem(orders): void {

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/many`, orders, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };
    };
    //----------------------------------------------------------------------------------------------
    orderItemGridLockUnlock($browse: any, event: any): void {
        let orderId, $selectedCheckBoxes, lockedItems = [];
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('tbody .cbselectrow:checked');

        if (orderId != null) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                let orderItem: any = {};
                let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                let orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                let description = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Description"]').attr('data-originalvalue');
                let quantityOrdered = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue');
                let recType = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RecType"]').attr('data-originalvalue');
                let rowsRolledUp = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RowsRolledUp"]').attr('data-originalvalue');

                orderItem.OrderItemId = orderItemId
                orderItem.OrderId = orderId;
                orderItem.Description = description;
                orderItem.QuantityOrdered = quantityOrdered;
                orderItem.RecType = recType;
                orderItem.RowsRolledUp = rowsRolledUp;

                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Locked"]').attr('data-originalvalue') === 'true') {
                    orderItem.Locked = false;
                } else {
                    orderItem.Locked = true;
                }
                lockedItems.push(orderItem);
            }
        }

        lockUnlockItem(lockedItems);

        function lockUnlockItem(orders): void {
            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/many`, orders, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };
    };
}
//----------------------------------------------------------------------------------------------
//Search Interface
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.Search.id] = function (e) {
    let grid = jQuery(e.currentTarget).parents('[data-control="FwGrid"]');
    let search, $form, orderId, quoteId, purchaseOrderId, templateId;

    $form = jQuery(this).closest('.fwform');
    let controllerName = $form.attr('data-controller');
    search = new SearchInterface();

    let gridInventoryType;
    if (grid.hasClass('R')) {
        gridInventoryType = 'Rental';
    }
    if (grid.hasClass('S')) {
        gridInventoryType = 'Sales';
    }
    if (grid.hasClass('L')) {
        gridInventoryType = 'Labor';
    }
    if (grid.hasClass('M')) {
        gridInventoryType = 'Misc';
    }
    if (grid.hasClass('P')) {
        gridInventoryType = 'Parts';
    }

    if ($form.attr('data-mode') === 'NEW') {
        let isValid = FwModule.validateForm($form);
        if (isValid) {
            let activeTabId = jQuery($form.find('[data-type="tab"].active')).attr('id');
            if (controllerName === "OrderController") {
                OrderController.saveForm($form, { closetab: false });
            } else if (controllerName === "QuoteController") {
                QuoteController.saveForm($form, { closetab: false });
            }
            $form.attr('data-opensearch', 'true');
            $form.attr('data-searchtype', gridInventoryType);
            $form.attr('data-activetabid', activeTabId);
        }
        return;
    }

    switch (controllerName) {
        case 'OrderController':
            orderId = FwFormField.getValueByDataField($form, 'OrderId');
            if (orderId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            } else {
                search.renderSearchPopup($form, orderId, 'Order', gridInventoryType);
            }
            break;
        case 'QuoteController':
            quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
            if (quoteId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            } else {
                search.renderSearchPopup($form, quoteId, 'Quote', gridInventoryType);
            }
            break;
        case 'PurchaseOrderController':
            purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            if (purchaseOrderId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            } else {
                search.renderSearchPopup($form, purchaseOrderId, 'PurchaseOrder', gridInventoryType);
            }
            break;
        case 'TemplateController':
            templateId = FwFormField.getValueByDataField($form, 'TemplateId');
            if (templateId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            } else {
                search.renderSearchPopup($form, templateId, 'Template', gridInventoryType);
            }
            break;
    }
};
//----------------------------------------------------------------------------------------------
//Sub worksheet
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.SubWorksheet.id] = function (e) {
    var $form, $subWorksheetForm, subWorksheetData: any = {};
    let $grid = jQuery(e.currentTarget).parents('[data-control="FwGrid"]');

    if ($grid.hasClass('A')) {
        subWorksheetData.RecType = ''
    } else if ($grid.hasClass('R')) {
        subWorksheetData.RecType = 'R'
    } else if ($grid.hasClass('S')) {
        subWorksheetData.RecType = 'S'
    } else if ($grid.hasClass('M')) {
        subWorksheetData.RecType = 'M'
    } else if ($grid.hasClass('L')) {
        subWorksheetData.RecType = 'L'
    } else if ($grid.hasClass('RS')) {
        subWorksheetData.RecType = 'RS'
    }

    try {
        $form = jQuery(this).closest('.fwform');
        subWorksheetData.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        subWorksheetData.RateType = FwFormField.getValueByDataField($form, 'RateType');
        subWorksheetData.CurrencyId = FwFormField.getValueByDataField($form, 'CurrencyId');
        subWorksheetData.CurrencyCode = FwFormField.getTextByDataField($form, 'CurrencyId');
        subWorksheetData.EstimatedStartDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
        subWorksheetData.EstimatedStopDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
        subWorksheetData.EstimatedStartTime = FwFormField.getValueByDataField($form, 'EstimatedStartTime');
        $subWorksheetForm = SubWorksheetController.openForm('EDIT', subWorksheetData);
        FwModule.openSubModuleTab($form, $subWorksheetForm);
        jQuery('.tab.submodule.active').find('.caption').html('Sub Worksheet');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
// Toggle between Detail and Summary view
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.SummaryView.id] = function (event) {
    let $element = jQuery(event.currentTarget);
    const $orderItemGrid = jQuery(this).closest('[data-name="OrderItemGrid"]');

    let summary: boolean = $orderItemGrid.data('Summary');
    summary = !summary;
    $orderItemGrid.data('Summary', summary);
    $element.children().text(summary ? 'Detail View' : 'Summary View');

    const onDataBind = $orderItemGrid.data('ondatabind');
    if (typeof onDataBind == 'function') {
        $orderItemGrid.data('ondatabind', function (request) {
            onDataBind(request);
            request.uniqueids.Summary = summary;
        });
    }
    FwBrowse.search($orderItemGrid);
    jQuery(document).trigger('click');
};
//----------------------------------------------------------------------------------------------
//Copy Template
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.CopyTemplate.id] = function (e) {
    const $form = jQuery(this).closest('.fwform');
    const $grid = jQuery(this).closest('[data-name="OrderItemGrid"]');
    let recType;
    recType = jQuery(this).closest('[data-grid="OrderItemGrid"]');
    if (recType.hasClass('R')) {
        recType = 'R';
    } else if (recType.hasClass('S')) {
        recType = 'S';
    } else if (recType.hasClass('L')) {
        recType = 'L';
    } else if (recType.hasClass('M')) {
        recType = 'M';
    } else if (recType.hasClass('P')) {
        recType = 'P';
    } else if (recType.hasClass('A')) {
        recType = '';
    } else if (recType.hasClass('RS')) {
        recType = 'RS'
    }
    let module = $form.attr('data-controller').replace('Controller', '');
    const HTML: Array<string> = [];
    HTML.push(
        `<div class="fwcontrol fwcontainer fwform popup template-popup" data-control="FwContainer" data-type="form">
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                <div class="tabpages">
                  <div class="formpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section">
                      <div class="formrow">
                        <div class="formcolumn" style="width:100%;margin-top:5px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div class="fwform-section-title" style="margin-bottom:10px;">Copy Template to ${module}</div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="select-items fwformcontrol" data-type="button" style="float:right;">Add to ${module}</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`
    );

    const addTemplateBrowse = () => {
        let $browse = FwBrowse.loadBrowseFromTemplate('Template');
        $browse.attr('data-hasmultirowselect', 'true');
        $browse = FwModule.openBrowse($browse);
        $browse.find('.fwbrowse-menu').hide();

        $browse.data('ondatabind', function (request) {
            request.pagesize = 20;
            request.orderby = "Description asc";
        });
        return $browse;
    }
    const $popupHtml = HTML.join('');
    const $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
    FwPopup.showPopup($popup);
    const $templateBrowse = addTemplateBrowse();
    $popup.find('.container').append($templateBrowse);
    const $templatePopup = $popup.find('.template-popup');

    $popup.find('.close-modal').one('click', e => {
        FwPopup.destroyPopup($popup);
        jQuery(document).find('.fwpopup').off('click');
        jQuery(document).off('keydown');
    });

    $popup.on('click', '.select-items', e => {
        const $selectedCheckBoxes = $popup.find('[data-control="FwGrid"] tbody .cbselectrow:checked');
        let templateIds: Array<string> = [];
        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let $this = jQuery($selectedCheckBoxes[i]);
            let id;
            id = $this.closest('tr').find('div[data-browsedatafield="TemplateId"]').attr('data-originalvalue');
            templateIds.push(id);
        };

        let request: any = {};
        request = {
            TemplateIds: templateIds
            , RecType: recType
            , OrderId: FwFormField.getValueByDataField($form, `${module}Id`)
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/order/copytemplate`, request, FwServices.defaultTimeout, function onSuccess(response) {
            $popup.find('.close-modal').click();
            FwBrowse.search($grid);
        }, null, $templatePopup);

    });

    FwBrowse.search($templateBrowse);
};
//----------------------------------------------------------------------------------------------
////Refresh Availability
//FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.RefreshAvailability.id] = function (e) {
//    const $orderItemGrid = jQuery(this).closest('[data-name="OrderItemGrid"]');
//    let recType;
//    recType = jQuery(this).closest('[data-grid="OrderItemGrid"]');
//    if (recType.hasClass('R')) {
//        recType = 'R';
//    } else if (recType.hasClass('S')) {
//        recType = 'S';
//    } else if (recType.hasClass('L')) {
//        recType = 'L';
//    } else if (recType.hasClass('M')) {
//        recType = 'M';
//    } else if (recType.hasClass('P')) {
//        recType = 'P';
//    } else if (recType.hasClass('A')) {
//        recType = '';
//    } else if (recType.hasClass('RS')) {
//        recType = 'RS'
//    }

//    const pageNumber = $orderItemGrid.attr('data-pageno');
//    const onDataBind = $orderItemGrid.data('ondatabind');
//    if (typeof onDataBind == 'function') {
//        $orderItemGrid.data('ondatabind', function (request) {
//            onDataBind(request);
//            request.uniqueids.RefreshAvailability = true;
//            request.pageno = parseInt(pageNumber);
//        });
//    }

//    FwBrowse.search($orderItemGrid);
//    $orderItemGrid.attr('data-pageno', pageNumber);
//    //resets ondatabind
//    $orderItemGrid.data('ondatabind', onDataBind);

//    jQuery(document).trigger('click');
//}
////---------------------------------------------------------------------------------
// Bold Selected
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.BoldUnBoldSelected.id] = function (event) {
    const $browse = jQuery(this).closest('.fwbrowse');

    try {
        OrderItemGridController.orderItemGridBoldUnbold($browse, event);
        jQuery(document).trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
// Lock Selected
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.LockUnlockSelected.id] = function (event) {
    const $browse = jQuery(this).closest('.fwbrowse');

    try {
        OrderItemGridController.orderItemGridLockUnlock($browse, event);
        jQuery(document).trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
// Copy Line Items
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.CopyLineItems.id] = e => {
    const $grid = jQuery(e.currentTarget).closest('.fwbrowse');
    const $form = jQuery(e.currentTarget).closest('.fwform');
    const module = $form.attr('data-controller').replace('Controller', '');
    const orderId = FwFormField.getValueByDataField($form, `${module}Id`);
    const ids = [];
    const $selectedCheckBoxes = $grid.find('tbody .cbselectrow:checked');
    for (let i = 0; i < $selectedCheckBoxes.length; i++) {
        const $this = jQuery($selectedCheckBoxes[i]);
        const id = $this.closest('tr').find('div[data-browsedatafield="OrderItemId"]').attr('data-originalvalue');
        ids.push(id);
    };
    const request: any = {};
    request.OrderId = orderId;
    request.OrderItemIds = ids;
    FwAppData.apiMethod(true, 'POST', `api/v1/order/copyorderitems`, request, FwServices.defaultTimeout,
        response => {
            jQuery(document).trigger('click');
            FwBrowse.search($grid);
        },
        ex => FwFunc.showError(ex), $grid);
};
//----------------------------------------------------------------------------------------------
// Manual Sorting
//FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.ManualSorting.id] = e => {
//    const $grid = jQuery(e.currentTarget).closest('.fwbrowse');
//    const $form = jQuery(e.currentTarget).closest('.fwform');
//    const module = $form.attr('data-controller').replace('Controller', '');
//    const orderId = FwFormField.getValueByDataField($form, `${module}Id`);

//    //hides the paging controls in manual sorting mode
//    const $pagingControls = $grid.find('.pager').children().children();
//    $pagingControls.hide();

//    //show all rows
//    const onDataBind = $grid.data('ondatabind');
//    if (typeof onDataBind == 'function') {
//        $grid.data('ondatabind', request => {
//            onDataBind(request);
//            request.pagesize = 9999;
//        });
//    }

//    $grid.data('afterdatabindcallback', () => {
//        //add sortable handle
//        const $tdselectrow = $grid.find('tbody td.tdselectrow');
//        $tdselectrow.find('div.divselectrow').hide();
//        if ($tdselectrow.find('.drag-handle').length === 0) {
//            $tdselectrow
//                .append('<i style="vertical-align:-webkit-baseline-middle; cursor:grab;" class="material-icons drag-handle">drag_handle</i>')
//                .css('text-align', 'center');
//        } else {
//            $tdselectrow.find('.drag-handle').show();
//        }

//        //adds button to apply changes in sorting
//        const $applyChangesBtn = jQuery('<div data-type="button" class="fwformcontrol sorting"><i class="material-icons" style="position:relative; top:5px;">&#xE161;</i>Apply</div>');
//        const $gridMenu = $grid.find('[data-control="FwMenu"]');
//        $applyChangesBtn.on('click', e => {
//            try {
//                const $trs = $grid.find('tbody  tr');
//                const orderItemIds: any = [];
//                let startAtIndex = '';
//                const isFirstPage = $grid.attr('data-pageno') === "1";
//                for (let i = 0; i < $trs.length; i++) {
//                    const $tr = jQuery($trs[i]);
//                    const id = $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue');
//                    //get index of first row if not on first page of the grid
//                    if (i === 0 && !isFirstPage) {
//                        startAtIndex = $tr.find('[data-browsedatafield="RowNumber"]').attr('data-originalvalue');
//                    }
//                    orderItemIds.push(id);
//                }

//                const request: any = {};
//                request.OrderId = orderId;
//                request.OrderItemIds = orderItemIds;
//                //request.pageno = parseInt($grid.attr('data-pageno'));
//                if (startAtIndex != '') request.StartAtIndex = startAtIndex;
//                FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/sort`, request, FwServices.defaultTimeout,
//                    response => {
//                        FwBrowse.search($grid);
//                        $pagingControls.show();
//                        $gridMenu.find('.sorting').hide();
//                        $tdselectrow.find('.drag-handle').hide();
//                        $tdselectrow.find('div.divselectrow').show();
//                        $gridMenu.find('.buttonbar').show();
//                    },
//                    ex => FwFunc.showError(ex), $grid);
//            } catch (ex) {
//                FwFunc.showError(ex);
//            }
//        });

//        //cancel sorting button
//        const $cancelBtn = jQuery('<div data-type="button" class="fwformcontrol sorting" style="margin-left:10px;">Cancel</div>');
//        $cancelBtn.on('click', e => {
//            FwBrowse.search($grid); //refresh grid to reset to original sorting order
//            $pagingControls.show();
//            $gridMenu.find('.sorting').hide();
//            $tdselectrow.find('.drag-handle').hide();
//            $tdselectrow.find('div.divselectrow').show();
//            $gridMenu.find('.buttonbar').show();
//        });

//        //toggle displayed buttons
//        $gridMenu.find('.buttonbar').hide();
//        if ($gridMenu.find('.sorting').length < 1) {
//            $gridMenu.append($applyChangesBtn, $cancelBtn);
//        } else {
//            $gridMenu.find('.sorting').show();
//        }

//        //initialize Sortable
//        Sortable.create($grid.find('tbody').get(0), {
//            handle: 'i.drag-handle'
//        });

//        $grid.data('afterdatabindcallback', null);
//    });

//    FwBrowse.search($grid);
//    //resets ondatabind if we want the grid to go back to original pagesize (move to apply/cancel events)
//    //$grid.data('ondatabind', onDataBind);

//    //closes menu
//    jQuery(document).trigger('click');
//};
//----------------------------------------------------------------------------------------------
// Color Legend
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.ColorLegend.id] = e => {
    const html = `
                <div id="previewHtml">
                    <div style="margin-bottom:10px;">Availability</div>
                    <div class="flexrow" style="margin-bottom:5px"><div data-browsedatafield="QuantityAvailable" data-state="enough"><div class="fieldvalue">32</div></div><div>Enough Available</div></div>
                    <div class="flexrow" style="margin-bottom:5px"><div data-browsedatafield="QuantityAvailable" data-state="negative"><div class="fieldvalue">-7</div></div><div>Inventory Shortage</div></div>
                    <div class="flexrow" style="margin-bottom:5px"><div data-browsedatafield="QuantityAvailable" data-state="low"><div class="fieldvalue">4</div></div><div>Low Availability</div></div>
                </div>`;

    const $confirmation = FwConfirmation.renderConfirmation(`Color Legend`, html);
    const containerCSS = {
        'box-sizing': 'border-box'
        , 'height': '100%'
        , 'line-height': '100%'
        , 'white-space': 'nowrap'
        , 'text-overflow': 'ellipsis'
        , 'vertical-align': 'middle'
        , 'display': 'flex'
        , 'align-items': 'center'
        , 'font-size': '.8em'
        , 'max-width': '65px'
    }
    $confirmation.find('[data-browsedatafield="QuantityAvailable"]').css(containerCSS);
    const $close = FwConfirmation.addButton($confirmation, 'Close', false);

    $close.on('click', () => {
        FwConfirmation.destroyConfirmation($confirmation);
    });
};
//---------------------------------------------------------------------------------
//Shortages Only
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.ShortagesOnly.id] = function (e) {
    let $element = jQuery(event.currentTarget);
    const $orderItemGrid = jQuery(this).closest('[data-name="OrderItemGrid"]');

    let shortages: boolean = $orderItemGrid.data('Shortages');
    shortages = !shortages;
    $orderItemGrid.data('Shortages', shortages);
    $element.children().text(shortages ? 'All Items (not Shortages Only)' : 'Shortages Only');

    const onDataBind = $orderItemGrid.data('ondatabind');
    if (typeof onDataBind == 'function') {
        $orderItemGrid.data('ondatabind', function (request) {
            onDataBind(request);
            request.uniqueids.ShortagesOnly = shortages;
        });
    }

    FwBrowse.search($orderItemGrid);

    jQuery(document).trigger('click');
}
//---------------------------------------------------------------------------------
//Split Detail
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.SplitDetails.id] = function (e) {
    let $element = jQuery(event.currentTarget);
    const $orderItemGrid = jQuery(this).closest('[data-name="OrderItemGrid"]');

    let splitDetails: boolean = $orderItemGrid.data('SplitDetails');
    splitDetails = !splitDetails;
    $orderItemGrid.data('SplitDetails', splitDetails);
    $element.children().text(splitDetails ? 'Roll-up Quantities' : 'Show Split Details');

    const onDataBind = $orderItemGrid.data('ondatabind');
    if (typeof onDataBind == 'function') {
        $orderItemGrid.data('ondatabind', function (request) {
            onDataBind(request);
            request.uniqueids.SplitDetails = splitDetails;
        });
    }

    FwBrowse.search($orderItemGrid);

    jQuery(document).trigger('click');
}
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.AddLossAndDamageItems.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        if ($form.attr('data-mode') !== 'NEW') {
            OrderController.addLossDamage($form, event);
        } else {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.RetireLossAndDamageItems.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        if ($form.attr('data-mode') !== 'NEW') {
            let $form = jQuery(this).closest('.fwform');
            OrderController.retireLossDamage($form);
        } else {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
var OrderItemGridController = new OrderItemGrid();