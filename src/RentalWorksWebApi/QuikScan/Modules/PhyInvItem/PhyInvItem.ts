//----------------------------------------------------------------------------------------------
RwInventoryController.getPhyInvItemScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
        captionPageTitle:    RwLanguages.translate('Physical Inventory')
      , captionPageSubTitle: '<div class="title">' + RwLanguages.translate('Physical Inventory No') + ": " + properties.phyNo + '</div>'
      , htmlScanBarcode:     RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Bar Code / I-Code')})
      , captionICode:        RwLanguages.translate('I-Code')
      , captionQty:          RwLanguages.translate('Qty')
      , captionAdd:          RwLanguages.translate('Add')
      , captionReplace:      RwLanguages.translate('Replace')
      , captionSubmit:       RwLanguages.translate('Submit')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-phyInvItem').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.$view.find('#phyInvItemView-response').hide();
    screen.$view.find('#phyInvItemView-qty').hide();
    
    screen.$view
        .on('change', '.fwmobilecontrol-value', function() {
            var $this, requestPhyCountItem;
            try {
                $this = jQuery(this);
                requestPhyCountItem = {
                    code:        RwAppData.stripBarcode(jQuery(this).val().toString().toUpperCase())
                  , physicalId:  properties.physicalId
                };
                RwServices.order.phyCountItem(requestPhyCountItem, function(response) {
                    try {
                        properties.response = response;
                        jQuery('#phyInvItemView-ICodeBarcode').html(response.webGetPhyItemInfo.masterNo + ((response.webGetPhyItemInfo.itemType === "NonBarCoded") ? "" : " (BC: " + response.request.code + ")"));
                        jQuery('#phyInvItemView-master').html(response.webGetPhyItemInfo.master);
                        jQuery('#phyInvItemView-genericMsgValue').html((((response.webGetPhyItemInfo.itemType === "BarCoded") && (response.webGetPhyItemInfo.status === 0)) ? response.webPhyCountItem.genericMsg : response.webGetPhyItemInfo.genericMsg));
                        jQuery('#phyInvItemView-msgValue').html((((response.webGetPhyItemInfo.itemType === "BarCoded") && (response.webGetPhyItemInfo.status === 0)) ? response.webPhyCountItem.msg : response.webGetPhyItemInfo.msg));
                        program.playStatus(response.webGetPhyItemInfo.status === 0);

                        jQuery('#phyInvItemView-response').show();
                        jQuery('#phyInvItemView-genericMsg')
                            .toggle((applicationConfig.designMode) || (jQuery('#phyInvItemView-genericMsgValue').html().length > 0))
                            .attr('class', ((response.webGetPhyItemInfo.status === 0) ? 'qssuccess' : 'qserror'))
                        ;
                        jQuery('#phyInvItemView-msg')
                            .toggle((applicationConfig.designMode) || (jQuery('#phyInvItemView-msgValue').html().length > 0));
                        jQuery('#phyInvItemView-messages')
                            .toggle((applicationConfig.designMode) || ((jQuery('#phyInvItemView-genericMsgValue').html().length > 0) || (jQuery('#phyInvItemView-msgValue').html().length > 0)));
                        jQuery('#phyInvItemView-info')
                            .toggle((applicationConfig.designMode) || (response.webGetPhyItemInfo.status !== 301));
                        jQuery('#phyInvItemView-qty')
                            .toggle((applicationConfig.designMode) || ((response.webGetPhyItemInfo.itemType === "NonBarCoded") && (response.webGetPhyItemInfo.status === 0)));
                        jQuery('#phyInvItemView-addReplace')
                            .toggle((applicationConfig.designMode) || (response.webGetPhyItemInfo.showAddRep));
                        jQuery('#phyInvItemView-submit')
                            .toggle((applicationConfig.designMode) || (!(response.webGetPhyItemInfo.showAddRep)));
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#phyInvItemView-btnAdd, #phyInvItemView-btnReplace', function() {
            var btnCaption, requestPhyCountItem, qty;
            try {
                btnCaption = jQuery(this).html();
                qty = jQuery('#phyInvItemView-qty input').val();
                if ( !isNaN(parseInt(qty)) && (parseInt(qty) >= 0) )
                {
                    requestPhyCountItem = {
                          physicalId:     properties.physicalId
                        , physicalItemId: properties.response.webGetPhyItemInfo.physicalItemId
                        , rentalItemId:   properties.response.webGetPhyItemInfo.rentalItemId
                        , masterId:       properties.response.webGetPhyItemInfo.masterId
                        , addReplace:     ((btnCaption === "Replace") ? "R" : "A")
                        , qty:            parseInt(qty)
                    };

                    RwServices.order.phyCountItem(requestPhyCountItem, function(response) {
                        try {
                            jQuery('#phyInvItemView-genericMsgValue').html(response.webPhyCountItem.genericMsg);
                            jQuery('#phyInvItemView-msgValue').html(response.webPhyCountItem.msg);
                            program.playStatus(response.webPhyCountItem.status === 0);

                            jQuery('#phyInvItemView-genericMsg')
                                .toggle((applicationConfig.designMode) || (jQuery('#phyInvItemView-genericMsgValue').html().length > 0))
                                .attr('class', ((response.webPhyCountItem.status === 0) ? 'qssuccess' : 'qserror'))
                            ;
                            jQuery('#phyInvItemView-msg')
                                .toggle((applicationConfig.designMode) || (jQuery('#phyInvItemView-msgValue').html().length > 0));
                            jQuery('#phyInvItemView-messages')
                                .toggle((applicationConfig.designMode) || ((jQuery('#phyInvItemView-genericMsgValue').html().length > 0) || (jQuery('#phyInvItemView-msgValue').html().length > 0)));
                            jQuery('#phyInvItemView-qty').hide().find('input').val('');
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } else {
                    FwFunc.showError('Qty is invalid');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#phyInvItemView-btnSubmit', function() {
            var btnCaption, requestPhyCountItem, qty;
            try {
                btnCaption = jQuery(this).html();
                qty = jQuery('#phyInvItemView-qty input').val();
                if ( !isNaN(parseInt(qty)) && (parseInt(qty) >= 0) )
                {
                    requestPhyCountItem = {
                          physicalId:     properties.physicalId
                        , physicalItemId: properties.response.webGetPhyItemInfo.physicalItemId
                        , rentalItemId:   properties.response.webGetPhyItemInfo.rentalItemId
                        , masterId:       properties.response.webGetPhyItemInfo.masterId
                        , addReplace:     "A"
                        , qty:            parseInt(qty)
                    };

                    RwServices.order.phyCountItem(requestPhyCountItem, function(response) {
                        try {
                            jQuery('#phyInvItemView-genericMsgValue').html(response.webPhyCountItem.genericMsg);
                            jQuery('#phyInvItemView-msgValue').html(response.webPhyCountItem.msg);
                            program.playStatus(response.webPhyCountItem.status === 0);

                            jQuery('#phyInvItemView-genericMsg')
                                .toggle((applicationConfig.designMode) || (jQuery('#phyInvItemView-genericMsgValue').html().length > 0))
                                .attr('class', ((response.webPhyCountItem.status === 0) ? 'qssuccess' : 'qserror'))
                            ;
                            jQuery('#phyInvItemView-msg')
                                .toggle((applicationConfig.designMode) || (jQuery('#phyInvItemView-msgValue').html().length > 0));
                            jQuery('#phyInvItemView-messages')
                                .toggle((applicationConfig.designMode) || ((jQuery('#phyInvItemView-genericMsgValue').html().length > 0) || (jQuery('#phyInvItemView-msgValue').html().length > 0)));
                            jQuery('#phyInvItemView-qty').hide().find('input').val('');
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } else {
                    FwFunc.showError('Qty is invalid');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.load = function() {
        program.setScanTarget('.fwmobilecontrol-value');
        if (!Modernizr.touch) {
            jQuery('.fwmobilecontrol-value').select();
        }
    };

    return screen;
};