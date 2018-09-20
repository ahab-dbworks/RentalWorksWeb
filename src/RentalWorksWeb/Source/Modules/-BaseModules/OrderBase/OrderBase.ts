//----------------------------------------------------------------------------------------------
class OrderBase {
    ActiveView: string = 'ALL';
    DefaultOrderType: string;
    DefaultOrderTypeId: string;
    CombineActivity: string;
    Module: string;

    renderFrames($form: any, period?) {
        let id = FwFormField.getValueByDataField($form, `${this.Module}Id`);
        $form.find('.frame input').css('width', '100%');

        if (typeof period !== 'undefined') {
            id = `${id}~${period}`
        }

        FwAppData.apiMethod(true, 'GET', `api/v1/ordersummary/${id}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            var key;
            for (key in response) {
                if (response.hasOwnProperty(key)) {
                    $form.find(`[data-framedatafield="${key}"] input`).val(response[key]);
                    $form.find(`[data-framedatafield="${key}"]`).attr('data-originalvalue', response[key]);
                }
            }

            var $profitFrames = $form.find('.profitframes .frame');
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
        }, null, $form);

        FwFormField.disable($form.find('.frame'));
        $form.find(".frame .add-on").children().hide();
    };
    //----------------------------------------------------------------------------------------------
    dynamicColumns($form) {
        var orderType = FwFormField.getValueByDataField($form, "OrderTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            $usedSaleGrid = $form.find('.usedsalegrid [data-name="OrderItemGrid"]'),
            $combinedGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [];

        for (var i = 3; i < fields.length; i++) {
            var name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "QuantityOrdered") {
                fieldNames.push(name);
            }
        }
        FwAppData.apiMethod(true, 'GET', "api/v1/ordertype/" + orderType, null, FwServices.defaultTimeout, function onSuccess(response) {
            $form.find('[data-datafield="CombineActivity"] input').val(response.CombineActivityTabs);
            let rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
                , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
                , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
                , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]')
                , usedSaleTab = $form.find('[data-type="tab"][data-caption="Used Sale"]')
                , rate = FwFormField.getValueByDataField($form, 'RateType');

            if (response.CombineActivityTabs === false) {
                $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
                $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
                $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
                $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
                $form.find('[data-datafield="RentalSale"] input').prop('checked') ? usedSaleTab.show() : usedSaleTab.hide();
            }
            if (response.CombineActivityTabs === true) {
                $form.find('.notcombined').css('display', 'none');
                $form.find('.notcombinedtab').css('display', 'none');
            } else {
                $form.find('.combined').css('display', 'none');
                $form.find('.combinedtab').css('display', 'none');
            }
            var hiddenRentals: Array<string> = fieldNames.filter(function (field) {
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
            var hiddenUsedSale = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.RentalSaleShowFields))
            var hiddenCombined = fieldNames.filter(function (field) {
                return !this.has(field)
            }, new Set(response.CombinedShowFields))
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
            for (var l = 0; l < hiddenUsedSale.length; l++) {
                jQuery($usedSaleGrid.find('[data-mappedfield="' + hiddenUsedSale[l] + '"]')).parent().hide();
            }
            for (var l = 0; l < hiddenCombined.length; l++) {
                jQuery($combinedGrid.find('[data-mappedfield="' + hiddenCombined[l] + '"]')).parent().hide();
            }
            if (hiddenRentals.indexOf('WeeklyExtended') === -1 && rate === '3WEEK') {
                $rentalGrid.find('.3weekextended').parent().show();
            } else if (hiddenRentals.indexOf('WeeklyExtended') === -1 && rate !== '3WEEK') {
                $rentalGrid.find('.weekextended').parent().show();
            }
        }, null, null);
    };
    //----------------------------------------------------------------------------------------------
    activityCheckboxEvents($form: any, mode: string) {
        let rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
            , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
            , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
            , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]')
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
    }
    //----------------------------------------------------------------------------------------------
    copyOrderOrQuote($form: any) {
        let $confirmation, $yes, $no, module;
        module = this.Module;

        $confirmation = FwConfirmation.renderConfirmation(`Copy ${module}`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        let html = [];
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

        let copyConfirmation = html.join('');
        let orderId = FwFormField.getValueByDataField($form, `${module}Id`);

        FwConfirmation.addControls($confirmation, html.join(''));

        let orderNumber, deal, description, dealId;
        $confirmation.find('div[data-caption="Type"] input').val(module);
        orderNumber = FwFormField.getValueByDataField($form, `${module}Number`);
        $confirmation.find('div[data-caption="No"] input').val(orderNumber);
        deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
        $confirmation.find('div[data-caption="Deal"] input').val(deal);
        description = FwFormField.getValueByDataField($form, 'Description');
        $confirmation.find('div[data-caption="Description"] input').val(description);
        $confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-text').val(deal);
        dealId = $form.find('[data-datafield="DealId"] input.fwformfield-value').val();
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

        $yes = FwConfirmation.addButton($confirmation, 'Copy', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', makeACopy);

        function makeACopy() {
            let request: any = {};
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

            for (var key in request) {
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
            var $confirmationbox = jQuery('.fwconfirmationbox');
            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/copy/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', `${module} Successfully Copied`);
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
    beforeValidate($browse: any, $grid: any, request: any) {
        let $form = $grid.closest('.fwform');
        var officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        request.uniqueids = {
            LocationId: officeLocationId
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

        $form.find('[data-datafield="BillToAddressDifferentFromIssuedToAddress"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.differentaddress'));
            }
            else {
                FwFormField.disable($form.find('.differentaddress'));
            }
        });

        $form.find('div[data-datafield="OrderTypeId"]').data('onchange', function ($tr) {
            let combineActivity = $tr.find('.field[data-browsedatafield="CombineActivityTabs"]').attr('data-originalvalue');
            $form.find('[data-datafield="CombineActivity"] input').val(combineActivity);

            let rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
                , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
                , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
                , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
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

        $form.find('[data-datafield="NoCharge"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
            }
        });

        $form.find('div[data-datafield="DepartmentId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingRentalRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingRentalRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingSalesRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingSalesRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingLaborRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingLaborRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingMiscellaneousRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingMiscellaneousRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingUsedSaleRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingUsedSaleRate"]').attr('data-originalvalue')));
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingFinalLdRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingFinalLdRate"]').attr('data-originalvalue')));
        });

        $form.find('div[data-datafield="DealId"]').data('onchange', function ($tr) {
            var type = $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'RateType', type);
            $form.find('div[data-datafield="RateType"] input.fwformfield-text').val(type);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', $tr.find('.field[data-browsedatafield="PaymentTermsId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentTerms"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTypeId"]', $tr.find('.field[data-browsedatafield="PaymentTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Currency"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', $tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));
        });
        //Open Print Order Report
        $form.find('.print').on('click', e => {
            let $report, orderNumber, orderId, recordTitle, printTab, module, hideModule;
            module = this.Module;
            try {
                orderNumber = $form.find(`div.fwformfield[data-datafield="${module}Number"] input`).val();
                orderId = $form.find(`div.fwformfield[data-datafield="${module}Id"] input`).val();
                recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
                $report = RwPrintOrderController.openForm();
                FwModule.openSubModuleTab($form, $report);

                if (module === 'Order') {
                    hideModule = 'Quote';
                } else {
                    hideModule = 'Order';
                };

                $report.find(`.fwform-section[data-caption="${hideModule}"]`).css('display', 'none');
                $report.find(`div.fwformfield[data-datafield="${module}Id"] input`).val(orderId);
                $report.find(`div.fwformfield[data-datafield="${module}Id"] .fwformfield-text`).val(orderNumber);
                jQuery('.tab.submodule.active').find('.caption').html(`Print ${module}`);

                printTab = jQuery('.tab.submodule.active');
                printTab.find('.caption').html(`Print ${module}`);
                printTab.attr('data-caption', `${module} ${recordTitle}`);
            }
            catch (ex) {
                FwFunc.showError(ex);
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

        $form.find(".totalType input").on('change', e => {
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
            this.calculateOrderItemGridTotals($form, gridType);
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
        $form.find(".weeklyType").show();
        $form.find(".monthlyType").hide();
        $form.find(".periodType input").prop('checked', true);

        //Defaults Address information when user selects a deal
        $form.find('[data-datafield="DealId"]').on('change', e => {
            let dealId = FwFormField.getValueByDataField($form, 'DealId')
            FwAppData.apiMethod(true, 'GET', `api/v1/deal/${dealId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($form, 'IssuedToAttention', response.BillToAttention1);
                FwFormField.setValueByDataField($form, 'IssuedToAttention2', response.BillToAttention2);
                FwFormField.setValueByDataField($form, 'IssuedToAddress1', response.BillToAddress1);
                FwFormField.setValueByDataField($form, 'IssuedToAddress2', response.BillToAddress2);
                FwFormField.setValueByDataField($form, 'BillToCity', response.BillToCity);
                FwFormField.setValueByDataField($form, 'IssuedToState', response.BillToState);
                FwFormField.setValueByDataField($form, 'IssuedToZipCode', response.BillToZipCode);
                FwFormField.setValueByDataField($form, 'IssuedToCountryId', response.BillToCountryId, response.BillToCountry);
                FwFormField.setValueByDataField($form, 'PrintIssuedToAddressFrom', response.BillToAddressType);
            }, null, $form);
        });

        //Hide/Show summary buttons based on rate type
        $form.find('[data-datafield="RateType"]').data('onchange', e => {
            let rateType = FwFormField.getValueByDataField($form, 'RateType');
            if (rateType === 'MONTHLY') {
                $form.find('.summaryweekly').hide();
                $form.find('.summarymonthly').show();
            } else {
                $form.find('.summarymonthly').hide();
                $form.find('.summaryweekly').show();
            }
            //resets back to period summary frames
            $form.find('.summaryperiod').click();
        });

        //Summary button events
        $form.find('.summaryperiod, .summaryweekly, .summarymonthly').on('click', e => {
            let $this = jQuery(e.currentTarget);
            let period;
            if ($this.hasClass('summaryperiod')) {
                period = 'P';
                $form.find('.summaryperiod').addClass('pressed');
                $form.find('.summaryweekly, .summarymonthly').removeClass('pressed');
            } else if ($this.hasClass('summaryweekly')) {
                period = 'W';
                $form.find('.summaryweekly').addClass('pressed');
                $form.find('.summaryperiod, .summarymonthly').removeClass('pressed');
            } else if ($this.hasClass('summarymonthly')) {
                period = 'M';
                $form.find('.summarymonthly').addClass('pressed');
                $form.find('.summaryperiod, .summaryweekly').removeClass('pressed');
            }
            this.renderFrames($form, period);
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
            default:
                rateType = 'Period';
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
    orderItemGridLockUnlock($browse: any, event: any) {
        let orderId, $selectedCheckBoxes;

        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('.cbselectrow:checked');

        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
            let orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');

            if (orderId != null) {
                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Locked"]').attr('data-originalvalue') === 'true') {
                    unlockItem(orderId, orderItemId);
                } else {
                    lockItem(orderId, orderItemId);
                }
            }
        }

        function lockItem(orderId, orderItemId) {
            let request: any = {};

            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Locked: true,
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };

        function unlockItem(orderId, orderItemId) {
            let request: any = {};

            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Locked: false,
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };
    };
    //----------------------------------------------------------------------------------------------
    orderItemGridBoldUnbold($browse: any, event: any) {
        let orderId, $selectedCheckBoxes;
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('.cbselectrow:checked');

        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
            let orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');

            if (orderId != null) {
                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Bold"]').attr('data-originalvalue') === 'true') {
                    unboldItem(orderId, orderItemId);
                } else {
                    boldItem(orderId, orderItemId);
                }
            }
        }

        function boldItem(orderId, orderItemId) {
            let request: any = {};

            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Bold: true,
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };

        function unboldItem(orderId, orderItemId) {
            let request: any = {};

            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Bold: false,
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };
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
    afterLoad($form) {
        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            let tabname = jQuery(e.currentTarget).attr('id');
            let lastIndexOfTab = tabname.lastIndexOf('tab');
            let tabpage = tabname.substring(0, lastIndexOfTab) + 'tabpage' + tabname.substring(lastIndexOfTab + 3);

            let $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    let $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            let $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    let $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });

        //Show/hide summary buttons based on rate type
        $form.find('.summaryperiod').addClass('pressed');
        let rateType = FwFormField.getValueByDataField($form, 'RateType');
        if (rateType === 'MONTHLY') {
            $form.find('.summaryweekly').hide();
            $form.find('.summarymonthly').show();
        } else if (rateType === 'WEEKLY') {
            $form.find('.summarymonthly').hide();
            $form.find('.summaryweekly').show();
        }
    }
}
var OrderBaseController = new OrderBase();