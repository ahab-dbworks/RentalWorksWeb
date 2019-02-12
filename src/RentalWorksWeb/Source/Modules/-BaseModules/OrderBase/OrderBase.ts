//----------------------------------------------------------------------------------------------
class OrderBase {
    ActiveView: string = 'ALL';
    DefaultOrderType: string;
    DefaultOrderTypeId: string;
    CombineActivity: string;
    Module: string;
    CachedOrderTypes: any = {};

    renderFrames($form: any, period?) {
        let id = FwFormField.getValueByDataField($form, `${this.Module}Id`);
        $form.find('.frame input').css('width', '100%');

        if (id !== '') {
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
        }        
    };
    //----------------------------------------------------------------------------------------------
    dynamicColumns($form) {
        let self = this;
        let orderType = FwFormField.getValueByDataField($form, "OrderTypeId"),
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [];
        let combineActivity, hiddenRentals, hiddenSales, hiddenLabor, hiddenMisc, hiddenUsedSale, hiddenLossDamage, hiddenCombined;


        for (var i = 3; i < fields.length; i++) {
            var name = jQuery(fields[i]).attr('data-mappedfield');
            if (name != "QuantityOrdered") {
                fieldNames.push(name);
            }
        }
        if (self.CachedOrderTypes[orderType] !== undefined) {
            this.columnLogic($form, this.CachedOrderTypes[orderType]);
        } else {
            FwAppData.apiMethod(true, 'GET', "api/v1/ordertype/" + orderType, null, FwServices.defaultTimeout, function onSuccess(response) {
                combineActivity = response.combineActivityTabs
                hiddenRentals = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.RentalShowFields))
                hiddenSales = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.SalesShowFields))
                hiddenLabor = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.LaborShowFields))
                hiddenMisc = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.MiscShowFields))
                hiddenUsedSale = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.RentalSaleShowFields))
                hiddenLossDamage = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.LossAndDamageShowFields))
                hiddenCombined = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.CombinedShowFields))

                self.CachedOrderTypes[orderType] = {
                    CombineActivityTabs: response.CombineActivityTabs,
                    hiddenRentals: hiddenRentals,
                    hiddenSales: hiddenSales,
                    hiddenLabor: hiddenLabor,
                    hiddenMisc: hiddenMisc,
                    hiddenUsedSale: hiddenUsedSale,
                    hiddenLossDamage: hiddenLossDamage,
                    hiddenCombined: hiddenCombined
                }
                self.columnLogic($form, self.CachedOrderTypes[orderType]);
            }, null, null);
        }        

        //sets active tab and opens search interface from a newly saved record 
        //12-12-18 moved here from afterSave Jason H 
        let openSearch = $form.attr('data-opensearch');
        let searchType = $form.attr('data-searchtype');
        let activeTabId = $form.attr('data-activetabid');
        let search = new SearchInterface();
        if (openSearch === "true") {
            //FwTabs.setActiveTab($form, $tab); //this method doesn't seem to be working correctly
            let $newTab = $form.find(`#${activeTabId}`);
            $newTab.click();
            if ($form.attr('data-controller') === "OrderController") {
                search.renderSearchPopup($form, FwFormField.getValueByDataField($form, 'OrderId'), 'Order', searchType);
            } else if ($form.attr('data-controller') === "QuoteController") {
                search.renderSearchPopup($form, FwFormField.getValueByDataField($form, 'QuoteId'), 'Quote', searchType);
            }
            $form.removeAttr('data-opensearch data-searchtype data-activetabid');
        }

    };
    //----------------------------------------------------------------------------------------------
    columnLogic($form, data) {
        let $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $salesGrid = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            $laborGrid = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            $miscGrid = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            $usedSaleGrid = $form.find('.usedsalegrid [data-name="OrderItemGrid"]'),
            $lossDamageGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]'),
            $combinedGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]'),
            rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]'),
            salesTab = $form.find('[data-type="tab"][data-caption="Sales"]'),
            miscTab = $form.find('[data-type="tab"][data-caption="Miscellaneous"]'),
            laborTab = $form.find('[data-type="tab"][data-caption="Labor"]'),
            usedSaleTab = $form.find('[data-type="tab"][data-caption="Used Sale"]'),
            lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss and Damage"]'),
            rate = FwFormField.getValueByDataField($form, 'RateType');

        $form.find('[data-datafield="CombineActivity"] input').val(data.combineActivity);

        if (data.combineActivity === true) {
            $form.find('.notcombined').css('display', 'none');
            $form.find('.notcombinedtab').css('display', 'none');
            $form.find('.combined').show();
            $form.find('.combinedtab').show();
        } else {
            $form.find('.combined').css('display', 'none');
            $form.find('.combinedtab').css('display', 'none');
            $form.find('.notcombined').show();
            $form.find('.notcombinedtab').show();
            $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
            $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
            $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
            $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
            $form.find('[data-datafield="LossAndDamage"] input').prop('checked') ? lossDamageTab.show() : lossDamageTab.hide();
            $form.find('[data-datafield="RentalSale"] input').prop('checked') ? usedSaleTab.show() : usedSaleTab.hide();
        }


        for (var i = 0; i < data.hiddenRentals.length; i++) {
            jQuery($rentalGrid.find('[data-mappedfield="' + data.hiddenRentals[i] + '"]')).parent().hide();
        }
        for (var j = 0; j < data.hiddenSales.length; j++) {
            jQuery($salesGrid.find('[data-mappedfield="' + data.hiddenSales[j] + '"]')).parent().hide();
        }
        for (var k = 0; k < data.hiddenLabor.length; k++) {
            jQuery($laborGrid.find('[data-mappedfield="' + data.hiddenLabor[k] + '"]')).parent().hide();
        }
        for (var l = 0; l < data.hiddenMisc.length; l++) {
            jQuery($miscGrid.find('[data-mappedfield="' + data.hiddenMisc[l] + '"]')).parent().hide();
        }
        for (var l = 0; l < data.hiddenUsedSale.length; l++) {
            jQuery($usedSaleGrid.find('[data-mappedfield="' + data.hiddenUsedSale[l] + '"]')).parent().hide();
        }
        for (let i = 0; i < data.hiddenLossDamage.length; i++) {
            jQuery($lossDamageGrid.find(`[data-mappedfield="${data.hiddenLossDamage[i]}"]`)).parent().hide();
        }
        for (let i = 0; i < data.hiddenCombined.length; i++) {
            jQuery($combinedGrid.find('[data-mappedfield="' + data.hiddenCombined[i] + '"]')).parent().hide();
        }
        if (data.hiddenRentals.indexOf('WeeklyExtended') === -1 && rate === '3WEEK') {
            $rentalGrid.find('.3weekextended').parent().show();
        } else if (data.hiddenRentals.indexOf('WeeklyExtended') === -1 && rate !== '3WEEK') {
            $rentalGrid.find('.weekextended').parent().show();
        }
    }
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
        $form.find('[data-datafield="LossAndDamage"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                lossDamageTab.show();
                FwFormField.disable($form.find('[data-datafield="Rental"]'));
                FwFormField.disable($form.find('[data-datafield="Sales"]'));
                FwFormField.disable($form.find('[data-datafield="RentalSale"]'));
            } else {
                lossDamageTab.hide();
                console.log('in change b4: ', $form.data('antiLD'))
                //if ()
                FwFormField.enable($form.find('[data-datafield="Rental"]'));
                FwFormField.enable($form.find('[data-datafield="Sales"]'));
                FwFormField.enable($form.find('[data-datafield="RentalSale"]'));
                $form.data('antiLD', null)
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
                console.log('checkbox val in click: ', $form.data('antiLD'))
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
            FwAppData.apiMethod(true, 'POST', `api/v1/${module}/copyto` + (request.CopyToType === "Q" ? "quote" : "order") + `/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
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
                FwModule.openModuleTab($form, "", true, 'FORM', true);
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
            FwFormField.setValue($form, 'div[data-datafield="DisableEditingLossAndDamageRate"]', JSON.parse($tr.find('.field[data-browsedatafield="DisableEditingLossAndDamageRate"]').attr('data-originalvalue')));
        });
        //Open Print Order Report
        $form.find('.print').on('click', e => {
            let $report, orderNumber, orderId, recordTitle, printTab, module, hideModule;
            module = this.Module;
            try {
                orderNumber = $form.find(`div.fwformfield[data-datafield="${module}Number"] input`).val();
                orderId = $form.find(`div.fwformfield[data-datafield="${module}Id"] input`).val();
                recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();

                if (module === 'Order') {
                    $report = RwOrderReportController.openForm();
                } else {
                    $report = RwQuoteReportController.openForm();
                };
                FwModule.openSubModuleTab($form, $report);

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
        $form.find('[data-datafield="DealId"]').data('onchange', $tr => {
            const DEALID = FwFormField.getValueByDataField($form, 'DealId');
            var type = $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'RateType', type);
            $form.find('div[data-datafield="RateType"] input.fwformfield-text').val(type);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', $tr.find('.field[data-browsedatafield="PaymentTermsId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentTerms"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="PaymentTypeId"]', $tr.find('.field[data-browsedatafield="PaymentTypeId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PaymentType"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', $tr.find('.field[data-browsedatafield="CurrencyId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Currency"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="DealNumber"]', $tr.find('.field[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));   

            FwAppData.apiMethod(true, 'GET', `api/v1/deal/${DEALID}`, null, FwServices.defaultTimeout, response => {
                FwFormField.setValueByDataField($form, 'IssuedToAttention', response.BillToAttention1);
                FwFormField.setValueByDataField($form, 'IssuedToAttention2', response.BillToAttention2);
                FwFormField.setValueByDataField($form, 'IssuedToAddress1', response.BillToAddress1);
                FwFormField.setValueByDataField($form, 'IssuedToAddress2', response.BillToAddress2);
                FwFormField.setValueByDataField($form, 'BillToCity', response.BillToCity);
                FwFormField.setValueByDataField($form, 'IssuedToState', response.BillToState);
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
    calculateOrderItemGridTotals($form: any, gridType: string): void {
        let subTotal, discount, salesTax, grossTotal, total, rateType;
        let extendedTotal = new Decimal(0);
        let discountTotal = new Decimal(0);
        let taxTotal = new Decimal(0);

        let rateValue = $form.find(`.${gridType}grid .totalType input:checked`).val();
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
        const extendedColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Extended"]`);
        const discountColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}DiscountAmount"]`);
        const taxColumn: any = $form.find(`.${gridType}grid [data-browsedatafield="${rateType}Tax"]`);

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
    orderItemGridLockUnlock($browse: any, event: any): void {
        let orderId, $selectedCheckBoxes;

        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('.cbselectrow:checked');

        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
            let orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');

            if (orderId != null) {
                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Locked"]').attr('data-originalvalue') === 'true') {
                    lockUnlockItem(orderId, orderItemId, false);
                } else {
                    lockUnlockItem(orderId, orderItemId, true);
                }
            }
        }

        function lockUnlockItem(orderId: string, orderItemId: string, lockrow: boolean): void {
            let request: any = {};

            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Locked: lockrow,
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
    deliveryTypeAddresses($form: any, event: any): void {
        let $element;
        $element = jQuery(event.currentTarget);
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
    //----------------------------------------------------------------------------------------------
    getWarehouseAddress($form: any, prefix: string): void {
        const WAREHOUSEID = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        let WHresponse: any = {};

        if ($form.data('whAddress')) {
            WHresponse = $form.data('whAddress');

            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, WHresponse.Attention);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, WHresponse.Address1);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, WHresponse.Address2);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, WHresponse.City);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, WHresponse.State);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, WHresponse.Zip);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, WHresponse.CountryId, WHresponse.Country);
        } else {
            FwAppData.apiMethod(true, 'GET', `api/v1/warehouse/${WAREHOUSEID}`, null, FwServices.defaultTimeout, response => {
                WHresponse = response;

                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, WHresponse.Attention);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, WHresponse.Address1);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, WHresponse.Address2);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, WHresponse.City);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, WHresponse.State);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, WHresponse.Zip);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, WHresponse.CountryId, WHresponse.Country);
                // Preventing unnecessary API calls once warehouse addresses have been requested once
                $form.data('whAddress', {
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
            const DEALID = FwFormField.getValueByDataField($form, 'DealId');
            FwAppData.apiMethod(true, 'GET', `api/v1/deal/${DEALID}`, null, FwServices.defaultTimeout, res => {
                response = res
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, res.Attention);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, res.Address1);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, res.Address2);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, res.City);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, res.State);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, res.Zip);
                FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, res.CountryId, res.Country);
            }, null, null);
        } else {
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAttention`, response.Attention);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress1`, response.Address1);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToAddress2`, response.Address2);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCity`, response.City);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToState`, response.State);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToZipCode`, response.Zip);
            FwFormField.setValueByDataField($form, `${prefix}DeliveryToCountryId`, response.CountryId, response.Country);
        }
    }
    //----------------------------------------------------------------------------------------------
    orderItemGridBoldUnbold($browse: any, event: any): void {
        let orderId, $selectedCheckBoxes;
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('.cbselectrow:checked');

        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
            let orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');

            if (orderId != null) {
                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Bold"]').attr('data-originalvalue') === 'true') {
                    boldUnboldItem(orderId, orderItemId, false);
                } else {
                    boldUnboldItem(orderId, orderItemId, true);
                }
            }
        }

        function boldUnboldItem(orderId: string, orderItemId: string, boldrow: boolean): void {
            let request: any = {};

            request = {
                OrderId: orderId,
                OrderItemId: orderItemId,
                Bold: boldrow,
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
            let $tab = jQuery(e.currentTarget);
            let tabname = $tab.attr('id');
            let lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            let tabpage = tabname.substring(0, lastIndexOfTab) + 'tabpage' + tabname.substring(lastIndexOfTab + 3);

            if ($tab.hasClass('audittab') == false) {
                let $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
                if (($tab.hasClass('tabGridsLoaded') == false) && $gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        try {
                            let $gridcontrol = jQuery($gridControls[i]);
                            FwBrowse.search($gridcontrol);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }

                let $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
                if (($tab.hasClass('tabGridsLoaded') == false) && $browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        let $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
            $tab.addClass('tabGridsLoaded');
        });
        // show / hide tabs
        if (!FwFormField.getValueByDataField($form, 'Rental')) { $form.find('[data-type="tab"][data-caption="Rental"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Sales')) { $form.find('[data-type="tab"][data-caption="Sales"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Miscellaneous')) { $form.find('[data-type="tab"][data-caption="Miscellaneous"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'Labor')) { $form.find('[data-type="tab"][data-caption="Labor"]').hide() }
        if (!FwFormField.getValueByDataField($form, 'RentalSale')) { $form.find('[data-type="tab"][data-caption="Used Sale"]').hide() }
        // LD Disable checkbox in Order form
        let rentalVal = FwFormField.getValueByDataField($form, 'Rental');
        let salesVal = FwFormField.getValueByDataField($form, 'Sales');
        let usedSaleVal = FwFormField.getValueByDataField($form, 'RentalSale');
        if (rentalVal === true || salesVal === true || usedSaleVal === true) {
            FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
        } else if (rentalVal === false && salesVal === false && usedSaleVal === false) {
            FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
        }
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