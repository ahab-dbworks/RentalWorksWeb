//----------------------------------------------------------------------------------------------
class OrderBase {
    DefaultOrderType:   string;
    DefaultOrderTypeId: string;
    CombineActivity:    string;
    Module:             string;
    CachedOrderTypes:   any = {};

    //----------------------------------------------------------------------------------------------
    dynamicColumns($form, orderTypeId?) {
        let self = this;
        let orderType,
            $rentalGrid = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
            fields = jQuery($rentalGrid).find('thead tr.fieldnames > td.column > div.field'),
            fieldNames = [];
        let hiddenRentals, hiddenSales, hiddenLabor, hiddenMisc, hiddenUsedSale, hiddenLossDamage, hiddenCombined;

        if (typeof orderTypeId !== 'undefined') {
            orderType = orderTypeId
        }
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
                //self.afterLoad($form);
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
            //$salesGrid      = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
            //$laborGrid      = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
            //$miscGrid       = $form.find('.miscgrid [data-name="OrderItemGrid"]'),
            //$usedSaleGrid   = $form.find('.usedsalegrid [data-name="OrderItemGrid"]'),
            $lossDamageGrid = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            //$combinedGrid   = $form.find('.combinedgrid [data-name="OrderItemGrid"]'),
            //rate            = FwFormField.getValueByDataField($form, 'RateType');

        //$form.find('[data-datafield="CombineActivity"] input').val(data.CombineActivityTabs);

        //if (data.CombineActivityTabs === true) {
        //    $form.find('.notcombined').css('display', 'none');
        //    $form.find('.notcombinedtab').css('display', 'none');
        //    $form.find('.combined').show();
        //    $form.find('.combinedtab').show();
        //} else {
        //    $form.find('.combined').css('display', 'none');
        //    $form.find('.combinedtab').css('display', 'none');
        //    $form.find('.notcombined').show();
        //    $form.find('.notcombinedtab').show();
        //}

        for (var i = 0; i < data.hiddenRentals.length; i++) {
            jQuery($rentalGrid.find('[data-mappedfield="' + data.hiddenRentals[i] + '"]')).parent().hide();
        }
        //for (var j = 0; j < data.hiddenSales.length; j++) {
        //    jQuery($salesGrid.find('[data-mappedfield="' + data.hiddenSales[j] + '"]')).parent().hide();
        //}
        //for (var k = 0; k < data.hiddenLabor.length; k++) {
        //    jQuery($laborGrid.find('[data-mappedfield="' + data.hiddenLabor[k] + '"]')).parent().hide();
        //}
        //for (var l = 0; l < data.hiddenMisc.length; l++) {
        //    jQuery($miscGrid.find('[data-mappedfield="' + data.hiddenMisc[l] + '"]')).parent().hide();
        //}
        //for (var l = 0; l < data.hiddenUsedSale.length; l++) {
        //    jQuery($usedSaleGrid.find('[data-mappedfield="' + data.hiddenUsedSale[l] + '"]')).parent().hide();
        //}
        for (let i = 0; i < data.hiddenLossDamage.length; i++) {
            jQuery($lossDamageGrid.find(`[data-mappedfield="${data.hiddenLossDamage[i]}"]`)).parent().hide();
        }
        //for (let i = 0; i < data.hiddenCombined.length; i++) {
        //    jQuery($combinedGrid.find('[data-mappedfield="' + data.hiddenCombined[i] + '"]')).parent().hide();
        //}
        //if (data.hiddenRentals.indexOf('WeeklyExtended') === -1 && rate === '3WEEK') {
        //    $rentalGrid.find('.3weekextended').parent().show();
        //} else if (data.hiddenRentals.indexOf('WeeklyExtended') === -1 && rate !== '3WEEK') {
        //    $rentalGrid.find('.weekextended').parent().show();
        //}
    }
    //----------------------------------------------------------------------------------------------
}
var OrderBaseController = new OrderBase();