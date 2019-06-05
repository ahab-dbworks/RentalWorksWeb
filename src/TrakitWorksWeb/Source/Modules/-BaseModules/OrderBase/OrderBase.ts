//----------------------------------------------------------------------------------------------
class OrderBase {
    CachedOrderTypes:   any = {};

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
}
var OrderBaseController = new OrderBase();