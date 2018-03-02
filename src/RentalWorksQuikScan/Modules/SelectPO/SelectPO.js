var RwSelectPO = {};
//----------------------------------------------------------------------------------------------
RwSelectPO.getSelectPOScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle;
    
    if (typeof properties.moduleType === 'undefined') throw 'RwSelectPO.getSelectPOScreen: properties.moduleType is required.';
    switch(properties.moduleType) {
        case RwConstants.moduleTypes.SubReturn:  pageTitle = RwLanguages.translate('PO Sub-Return');  break;
        case RwConstants.moduleTypes.SubReceive: pageTitle = RwLanguages.translate('PO Sub-Receive'); break;
        default: throw 'RwSelectPO.getSelectPOScreen moduleType not supported';
    };
    combinedViewModel = jQuery.extend({
        captionPageTitle:    pageTitle
      , captionPageSubTitle: ''
      , htmlScanBarcode:     RwPartialController.getScanBarcodeHtml({
            captionInstructions: RwLanguages.translate('Select PO...')
          , captionBarcodeICode: RwLanguages.translate('PO No.')
        })
      , captionPONo:         RwLanguages.translate('PO')
      , captionVendor:       RwLanguages.translate('Vendor')
      , captionDeal:         RwLanguages.translate('Deal')
      , captionOrderDesc:    RwLanguages.translate('Order')
      , captionMsg:          RwLanguages.translate('Messages')
      , captionConfirm:      RwLanguages.translate('Continue...')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-selectPO').html(), combinedViewModel, {});
    screen = {};
    screen.viewModel = combinedViewModel;
    screen.properties = properties;
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    screen.$view.find('#selectPOView-response').hide();
    screen.$view.find('#selectPOView-messages').hide();
    screen.$view.find('.browsepurchaseorders-container').hide();

    screen.getSuspendedSessionPopup = function(suspendedContracts) {'use strict';
        var result, html, statusdate, sessionno, orderno, orderdesc, deal, username, status, rowView, rowModel;
    
        result = {};
        rowView = jQuery('#tmpl-SelectPO-SuspendedSession').html();
        html = [];
        html.push('<div class="po-suspendedsessions">');
        if (typeof suspendedContracts === 'object') {
            for (var rowno = 0; rowno < suspendedContracts.Rows.length; rowno++) {
                rowModel = {};
                rowModel.contractid = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.contractid];
                rowModel.statusdate = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.statusdate];
                rowModel.sessionno  = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.sessionno];
                rowModel.orderno    = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.orderno];
                rowModel.orderdesc  = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.orderdesc];
                rowModel.deal       = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.deal];
                rowModel.username   = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.username];
                rowModel.status     = suspendedContracts.Rows[rowno][suspendedContracts.ColumnIndex.status];
                html.push(Mustache.render(rowView, rowModel));
            }
        }
        html.push('</div>');
        result.$popup = FwConfirmation.renderConfirmation(suspendedContracts.Rows.length.toString() + ' Suspended Session' + ((suspendedContracts.Rows.length === 1) ? '' : 's'), html.join(''));
        result.$popup.attr('data-nopadding', 'true');
        result.$btnJoinSession = FwConfirmation.addButton(result.$popup, 'Join Session', false);
        result.$btnJoinSession.on('click', function() {
            var $suspendedsession;
            try {
                $suspendedsession = result.$popup.find('.po-suspendedsession');
                if ($suspendedsession.length === 1) {
                    $suspendedsession.click();
                } else if ($suspendedsession.length > 1) {
                    FwFunc.showMessage('Please click one of the Suspended Sessions in the popup below.');
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
        result.$btnNewSession  = FwConfirmation.addButton(result.$popup, 'New Session', true);
        FwConfirmation.addButton(result.$popup, 'Cancel', true);

        return result;
    };
    
    screen.loadPurchaseOrders = function() {
        screen.$view.find('#selectPOView-messages').hide();
        var request = {
            pageno: 1,
            pagesize: 1000,
            moduletype: properties.moduleType
        };
        RwServices.callMethod('SelectPO', 'GetPurchaseOrders', request, function(response) {
            var itemtemplate, rowhtml, itemmodel, dt, html;
            dt = response.datatable;
            html = [];
            if (dt !== null) {
                itemtemplate = jQuery('#tmpl-SelectPO-SubRentalPOBrowseItem').html();
                for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
                    itemmodel = {
                        captionorderno:   RwLanguages.translate('PO No'),
                        valueorderno:     dt.Rows[rowno][dt.ColumnIndex.orderno],
                        valueorderdesc:   dt.Rows[rowno][dt.ColumnIndex.orderdesc],
                        captiondeal:      RwLanguages.translate('Deal'),
                        valuedeal:        dt.Rows[rowno][dt.ColumnIndex.deal],
                        captionorderdate: RwLanguages.translate('Date'),
                        valueorderdate:   dt.Rows[rowno][dt.ColumnIndex.orderdate],
                        captionstatus:    RwLanguages.translate('Status'),
                        valuestatus:      dt.Rows[rowno][dt.ColumnIndex.status],
                        captionasof:      RwLanguages.translate('As Of'),
                        valueasof:        dt.Rows[rowno][dt.ColumnIndex.statusdate]
                    };
                    rowhtml = Mustache.render(itemtemplate, itemmodel);
                    html.push(rowhtml);
                }
            }
            screen.$view.find('.browsepurchaseorders').html(html.join('\n'));
            screen.$view.find('.browsepurchaseorders-container').show();
        });
    };

    screen.$view.on('click', '.browsepurchaseorders > li', function() {
        var $li, pono, skipconfirmation; 
        $li              = jQuery(this);
        pono    = $li.attr('data-pono');
        skipconfirmation = true;
        screen.selectPO(pono, skipconfirmation, false);
    });
    
    screen.selectPO = function(pono, skipconfirmation, forcecreatecontract) {
        var request;
        request = {
            poNo: pono,
            moduleType: properties.moduleType,
            forcecreatecontract: forcecreatecontract
        };
        RwServices.callMethod('SelectPO', 'WebSelectPO', request, function(response) {
            var activityType;
            try {
                if (!skipconfirmation) {
                    program.playStatus(response.webSelectPO.status === 0);
                }
                if (typeof response.suspendedContracts === 'object') {
                    skipconfirmation = true;
                    var suspendedContractsPopup = screen.getSuspendedSessionPopup(response.suspendedContracts);
                    suspendedContractsPopup.$popup.on('click', '.po-suspendedsession', function() {
                        var requestSelectSession, $suspendedsession, sessionno;
                        try {
                            $suspendedsession = jQuery(this);
                            requestSelectSession = {
                                poNo: pono,
                                moduleType: properties.moduleType,
                                forcecreatecontract: false,
                                sessionNo: $suspendedsession.attr('data-sessionno'),
                                contractId: $suspendedsession.attr('data-contractid')
                            };
                            RwServices.callMethod('SelectPO', 'WebSelectPO', requestSelectSession, function(responserequestSelectSession) {
                                try {
                                    screen.selectPOStep2(responserequestSelectSession, skipconfirmation);
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    suspendedContractsPopup.$btnNewSession.on('click', function() {
                        var requestCreateSession;
                        try {
                            screen.selectPO(pono, skipconfirmation, true);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } else {
                    screen.selectPOStep2(response, skipconfirmation);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.selectPOStep2 = function(response, skipconfirmation) {
        switch (response.webSelectPO.activityType) {
            case ("R"):  { activityType = RwLanguages.translate('Sub-Rental') + " "; break; }
            case ("S"):  { activityType = RwLanguages.translate('Sub-Sale') + " "; break; }
            case ("RS"): { activityType = RwLanguages.translate('Sub-Rental/Sale') + " "; break; }
        }
        screen.$view.find('#selectPOView-msg').html(response.webSelectPO.msg);
        screen.$view.find('#selectPOView-messages').toggle(response.webSelectPO.status !== 0);
        screen.$view.find('#selectPOView-confirm').toggle(response.webSelectPO.status === 0);
        if (response.webSelectPO.status === 0) {
            screen.$view.find('#selectPOView-btnSelectPO')
                .data('webselectpo', response.webSelectPO)
                .attr('contractid', response.contract.contractId)
                .attr('sessionno', response.contract.sessionNo)
            ;
        }
        if (!skipconfirmation) {
            screen.$view.find('.browsepurchaseorders-container').hide();
            screen.$view.find('.captionPONo').html(response.webSelectPO.poNo);
            screen.$view.find('.valuePONo').html(response.webSelectPO.poNo);
            screen.$view.find('.valuePODesc').html(response.webSelectPO.poDesc);
            screen.$view.find('.valueVendor').html(response.webSelectPO.vendor);
            screen.$view.find('.valueVendorNo').html(response.webSelectPO.vendorId);
            screen.$view.find('.valueDeal').html(response.webSelectPO.deal);
            screen.$view.find('.valueDealNo').html(response.webSelectPO.dealNo);
            screen.$view.find('.valueOrderDesc').html(response.webSelectPO.orderDesc);
            screen.$view.find('.valueOrderNo').html(response.webSelectPO.orderNo);
            screen.$view.find('#selectPOView-info').toggle(response.webSelectPO.status === 0);
            screen.$view.find('#selectPOView-response').show();
        } else {
            screen.$view.find('#selectPOView-response').hide();
            if (response.webSelectPO.status === 0) {
                screen.$view.find('#selectPOView-btnSelectPO').click();
            }
        }
    };
    
    screen.$view.on('change', '.selectpo-search .fwmobilecontrol-value', function() {
        var $this, pono;
        try {
            $this = jQuery(this);
            pono = $this.val().toUpperCase();
            if (pono.length > 0) {
                screen.selectPO(pono, false, false);
            } else {
                screen.loadPurchaseOrders();
            }

        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$view.on('click', '#selectPOView-btnSelectPO', function() {
        var $subReceiveReturnView, subReceiveReturnView_viewModel, subReceiveReturnView_properties, $this;
        $this = jQuery(this);
        subReceiveReturnView_viewModel = {};
        subReceiveReturnView_properties = {
            webSelectPO: $this.data('webselectpo')
            , contractId:  $this.attr('contractid')
            , sessionNo:   $this.attr('sessionno')
        };
        subReceiveReturnView_properties = jQuery.extend({}, properties, subReceiveReturnView_properties);
        $subReceiveReturnView = POSubReceiveReturn.getPOReceiveReturnScreen(subReceiveReturnView_viewModel, subReceiveReturnView_properties);
        program.pushScreen($subReceiveReturnView);
    });

    screen.load = function() {
        program.setScanTarget('.selectpo-search .fwmobilecontrol-value');
        if (!Modernizr.touch) {
            jQuery('.selectpo-search .fwmobilecontrol-value').select();
        }
        screen.loadPurchaseOrders();
        //screen.loadlistview();
    };

    //screen.loadlistview = function() {
    //    var $listview = screen.$view.find('.listview');
    //    FwListView.search($listview, {
    //            moduletype: properties.moduleType
    //        },
    //        function(request) {
    //            RwServices.callMethod('SelectPO', 'GetPurchaseOrders', request, function(response) {
    //                FwListView.load($listview, request, response.datatable, jQuery('#tmpl-SelectPO-SubRentalPOBrowseItem').html());
    //            });
    //        }
    //    );
    //};
    
    return screen;
};