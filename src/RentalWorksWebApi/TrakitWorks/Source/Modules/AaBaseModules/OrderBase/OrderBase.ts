//----------------------------------------------------------------------------------------------
class OrderBase {
    CachedOrderTypes: any = {};
    Module: string;

    //----------------------------------------------------------------------------------------------
    dynamicColumns($form) {
        let self = this;
        let $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
        const orderTypeId = FwFormField.getValueByDataField($form, 'OrderTypeId');
        let hiddenRentals, hiddenLossDamage;

        
        if (self.CachedOrderTypes[orderTypeId] !== undefined) {
            this.columnLogic($form, this.CachedOrderTypes[orderTypeId]);
        } else {
            const fields     = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field');
            const fieldNames = [];

            for (var i = 3; i < fields.length; i++) {
                var name = jQuery(fields[i]).attr('data-mappedfield');
                if (name != "QuantityOrdered") {
                    fieldNames.push(name);
                }
            }

            FwAppData.apiMethod(true, 'GET', "api/v1/ordertype/" + orderTypeId, null, FwServices.defaultTimeout, function onSuccess(response) {
                hiddenRentals = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.RentalShowFields))
                hiddenLossDamage = fieldNames.filter(function (field) {
                    return !this.has(field)
                }, new Set(response.LossAndDamageShowFields))

                self.CachedOrderTypes[orderTypeId] = {
                    CombineActivityTabs: response.CombineActivityTabs,
                    hiddenRentals:       hiddenRentals,
                    hiddenLossDamage:    hiddenLossDamage
                }
                self.columnLogic($form, self.CachedOrderTypes[orderTypeId]);
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
        let $rentalGrid     = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            $lossDamageGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');

        for (var i = 0; i < data.hiddenRentals.length; i++) {
            jQuery($rentalGrid.find('[data-mappedfield="' + data.hiddenRentals[i] + '"]')).parent().hide();
        }
        for (let i = 0; i < data.hiddenLossDamage.length; i++) {
            jQuery($lossDamageGrid.find(`[data-mappedfield="${data.hiddenLossDamage[i]}"]`)).parent().hide();
        }
    }
    //----------------------------------------------------------------------------------------------
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
        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="New Job" data-datafield="CopyToDealId" data-browsedisplayfield="Deal" data-validationname="DealValidation"></div>');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
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
    loadAudit($form) {
        const uniqueid = FwFormField.getValueByDataField($form, 'OrderId');
        FwModule.loadAudit($form, uniqueid);
    }
}
var OrderBaseController = new OrderBase();