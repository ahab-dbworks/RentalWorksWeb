var RwSelectOrder = {};
//----------------------------------------------------------------------------------------------
RwSelectOrder.getSelectOrderScreen = function(viewModel, properties) {'use strict';
    var combinedViewModel, screen, captionBarcodeICode, searchPlaceholder;
    captionBarcodeICode = RwLanguages.translate('Order No');
    searchPlaceholder  = 'Order No/Desc/Deal';
    if (properties.moduleType === RwConstants.moduleTypes.Truck) {
        captionBarcodeICode = RwLanguages.translate('Truck No.');
        searchPlaceholder = 'Truck No/Desc';
    }
    combinedViewModel = jQuery.extend({
        captionPageTitle:    RwOrderController.getPageTitle(properties)
      , captionPageSubTitle: ''
      , captionBarcodeICode: captionBarcodeICode
      , captionOrderDesc:    RwLanguages.translate('Order')
      , captionDealDesc:     RwLanguages.translate('Deal')
      , captionMsg:          RwLanguages.translate('Messages')
      , captionConfirm:      RwLanguages.translate('Continue...')
      , searchPlaceholder:   searchPlaceholder
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-selectOrder').html(), combinedViewModel, {});
    screen                   = {};
    screen.viewModel         = viewModel;
    screen.combinedViewModel = combinedViewModel;
    screen.properties        = properties;
    screen.$view             = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    
    screen.$view.find('#selectOrder-error').hide();
    screen.$view.find('.selectOrder-browseorders-container').hide();
    screen.$view.find('#selectOrder-response').hide();

    switch(properties.moduleType) {
        case 'Order': break;
        case 'Truck': screen.$view.find('.fwmobilecontrol-value').attr('placeholder', RwLanguages.translate('Truck No')); break;
    }

    screen.$view.on('click', '.listview .item', function() {
        var $this, orderno, skipconfirmation; 
        $this              = jQuery(this);
        orderno          = $this.attr('data-orderno');
        skipconfirmation = true;
        screen.selectOrder(orderno, skipconfirmation);
    });
    //screen.loadOrders = function() {
    //    screen.$view.find('#selectOrder-error').hide();
    //    screen.$view.find('.selectOrder-response').hide();
    //    var getOrdersRequest = {
    //        moduletype: properties.moduleType,
    //        activitytype: properties.activityType,
    //        pageno: 1,
    //        pagesize: 1000
    //    }
    //    RwServices.callMethod('SelectOrder', 'GetOrders', getOrdersRequest, function(response) {
    //        var itemtemplate, rowhtml, itemmodel, dt, html;
    //        dt = response.GetOrders;
    //        html = [];
    //        if (dt !== null) {
    //            switch(properties.moduleType) {
    //                case 'Order':        
    //                    itemtemplate = jQuery('#tmpl-getOrdersItem').html();
    //                    break;
    //                case 'Truck': 
    //                    itemtemplate = jQuery('#tmpl-getPackageTruckItem').html(); 
    //                    break;
    //            }
    //            for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
    //                switch(properties.moduleType) {
    //                    case 'Order':        
    //                        itemmodel = {
    //                            captiondescription: RwLanguages.translate('Desc'),
    //                            valuedescription:   dt.Rows[rowno][dt.ColumnIndex.orderdesc],
    //                            captionorderno:     RwLanguages.translate('Order No'),
    //                            valueorderno:       dt.Rows[rowno][dt.ColumnIndex.orderno],
    //                            captionorderdate:   RwLanguages.translate('Date'),
    //                            valueorderdate:     dt.Rows[rowno][dt.ColumnIndex.orderdate],
    //                            captiondeal:        RwLanguages.translate('Deal'),
    //                            valuedeal:          dt.Rows[rowno][dt.ColumnIndex.deal],
    //                            captionasof:        RwLanguages.translate('As Of'),
    //                            valueasof:          dt.Rows[rowno][dt.ColumnIndex.orderdate],
    //                            captionstatus:      RwLanguages.translate('Status'),
    //                            valuestatus:        dt.Rows[rowno][dt.ColumnIndex.status],
    //                            captionstartdate:   RwLanguages.translate('Est Dates'),
    //                            valuestartdate:     dt.Rows[rowno][dt.ColumnIndex.estrentfrom],
    //                            valueenddate:       dt.Rows[rowno][dt.ColumnIndex.estrentto]
    //                        }
    //                        break;
    //                    case 'Truck': 
    //                        itemmodel = {
    //                            captionDescription: RwLanguages.translate('Desc'),
    //                            valueDescription:   dt.Rows[rowno][dt.ColumnIndex.orderdesc],
    //                            captionOrderNo:     RwLanguages.translate('Order No'),
    //                            valueOrderNo:       dt.Rows[rowno][dt.ColumnIndex.orderno],
    //                            captionAsOf:        RwLanguages.translate('As Of'),
    //                            valueAsOf:          dt.Rows[rowno][dt.ColumnIndex.asof],
    //                            captionStatus:      RwLanguages.translate('Status'),
    //                            valueStatus:        dt.Rows[rowno][dt.ColumnIndex.status],
    //                            captionDepartment:  RwLanguages.translate('Department'),
    //                            valueDepartment:    dt.Rows[rowno][dt.ColumnIndex.department]
    //                        } 
    //                        break;
    //                }
    //                rowhtml = Mustache.render(itemtemplate, itemmodel);
    //                html.push(rowhtml);
    //            }
    //        }
    //        screen.$view.find('#selectOrder-browseorders').html(html.join('\n'));
    //        screen.$view.find('.selectOrder-browseorders-container').show();
    //    });
    //};
    
    screen.$view.on('click', '.bc-find-img', function() {
        //screen.loadOrders();
        screen.loadlistview();
    });

    screen.selectOrder = function(orderno, skipconfirmation) {
        var request;
        screen.$view.find('.listview').hide();
        request = {
            orderNo:      orderno,
            moduleType:   properties.moduleType
        };
        if (typeof properties.activityType !== 'undefined') {
            request.activityType = properties.activityType;
        }
        RwServices.callMethod('SelectOrder', 'WebSelectOrder', request, function(response) {
            if (!skipconfirmation) {
                program.playStatus(response.webSelectOrder.status <= 0);
            }
            if ((response) && (typeof response.request !== 'undefined') && (typeof response.webSelectOrder !== 'undefined')) {
                properties.webSelectOrder = response.webSelectOrder;
                if (typeof response.contract === 'object') {
                    properties.contract = response.contract;
                }
                if (typeof response.suspendedInContracts === 'object') {
                    properties.suspendedInContracts = response.suspendedInContracts;
                }
                if ((typeof properties.activityType !== 'undefined') && (properties.activityType == 'Staging')) {
                    properties.responsibleperson = response.responsibleperson;
                }
                if (!skipconfirmation) {
                    screen.$view.find('.selectOrder-browseorders-container').hide();
                    screen.$view.find('.selectOrder-response').show();
                    jQuery('#selectOrder-txtOrderNo').html(response.request.orderNo);
                    jQuery('#selectOrder-txtOrderDesc').html(response.webSelectOrder.orderDesc);
                    jQuery('#selectOrder-dealDesc').toggle(properties.moduleType !== RwConstants.moduleTypes.Truck);
                    jQuery('#selectOrder-txtDealNo').html(response.webSelectOrder.dealNo);
                    jQuery('#selectOrder-txtDealDesc').html(response.webSelectOrder.deal);
                    jQuery('#selectOrder-error').html(response.webSelectOrder.msg);
                    jQuery('#selectOrder-response').show();
                    jQuery('#selectOrder-order').toggle(response.webSelectOrder.status <= 0);
                    jQuery('#selectOrder-error').toggle(response.webSelectOrder.status > 0);
                    jQuery('#selectOrder-btnSelectOrder').focus();
                } else {
                    jQuery('#selectOrder-btnSelectOrder').click();
                }
            } else {
                throw 'RwServices.order.selectOrder: Response was not in an expected format';
            }
        });
    };
    
    screen.$view
        .on('change', '.fwmobilecontrol-value', function() {
            try {
                screen.loadlistview();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#selectOrder-btnSelectOrder', function() {'use strict';
            var suspendedContractsPopup, stageItemScreen, stageItemScreen_viewModel, stageItemScreen_properties, checkInItemScreen, 
                checkInItemScreen_viewModel, checkInItemScreen_properties;
            
            try {
                switch(properties.activityType) {
                    case RwConstants.activityTypes.Staging:
                        stageItemScreen_viewModel = {};
                        stageItemScreen_properties = jQuery.extend({}, properties, {
                            webSelectOrder: properties.webSelectOrder
                        });
                        if (properties.stagingType === RwConstants.stagingType.Normal) {
                            stageItemScreen = StagingController.getStagingScreen(stageItemScreen_viewModel, stageItemScreen_properties);
                        } else if (properties.stagingType === RwConstants.stagingType.RfidPortal) {
                            stageItemScreen = RFIDStaging.getModuleScreen(stageItemScreen_viewModel, stageItemScreen_properties);
                        }
                        program.pushScreen(stageItemScreen);
                        break;
                    case RwConstants.activityTypes.CheckIn:
                        if (typeof properties.suspendedInContracts === 'object') {
                            suspendedContractsPopup = CheckInController.getOrderSuspendedSessionPopup(properties.suspendedInContracts);
                            suspendedContractsPopup.$popup.on('click', '.checkin-suspendedsession', function() {
                                var requestSelectSession, $suspendedsession, sessionno;
                                try {
                                    $suspendedsession = jQuery(this);
                                    requestSelectSession = {};
                                    requestSelectSession.sessionNo  = $suspendedsession.find('.checkin-suspendedsession-sessionno-value').text();
                                    requestSelectSession.moduleType = properties.moduleType
                                    if ((properties.checkInMode === RwConstants.checkInModes.Session) && (isNaN(requestSelectSession.sessionNo))) {
                                        throw 'Please enter a numeric Session No.';
                                    }
                                    delete properties.webSelectOrder;
                                    properties.checkInMode = 'Session';
                                    RwServices.order.selectSession(requestSelectSession, function(responseSelectSession) {
                                        try {
                                            properties.selectedsession = responseSelectSession.webSelectSession;
                                            if (((properties.moduleType == RwConstants.moduleTypes.Order) || (properties.moduleType == RwConstants.moduleTypes.Transfer) || (properties.moduleType == RwConstants.moduleTypes.Truck)) && 
                                                (properties.activityType == RwConstants.activityTypes.CheckIn)) {
                                                checkInItemScreen_viewModel = {};
                                                checkInItemScreen_properties = jQuery.extend({}, properties, {
                                                    selectedsession: properties.selectedsession
                                                });
                                                if (properties.checkInType === RwConstants.checkInType.Normal) {
                                                    checkInItemScreen = CheckInController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                                } else if (properties.checkInType === RwConstants.checkInType.RfidPortal) {
                                                    checkInItemScreen = RFIDCheckIn.getModuleScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                                }
                                                program.pushScreen(checkInItemScreen);
                                            } else {
                                                throw 'Not implemented!';
                                            }
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
                                    requestCreateSession = {};
                                    requestCreateSession.orderid      = properties.webSelectOrder.orderId;
                                    requestCreateSession.dealid       = properties.webSelectOrder.dealId;
                                    requestCreateSession.departmentid = properties.webSelectOrder.departmentId;
                                    RwServices.order.createNewInContractAndSuspend(requestCreateSession, function(responseCreateSession) {
                                        try {
                                            if (typeof responseCreateSession.contract === 'object') {
                                                properties.contract = responseCreateSession.contract;
                                                checkInItemScreen_viewModel = {};
                                                checkInItemScreen_properties =  jQuery.extend({}, properties, {
                                                    webSelectOrder: properties.webSelectOrder,
                                                    sessionNo:      properties.contract.sessionNo,
                                                    contractId:     properties.contract.contractId, 
                                                    dealId:         properties.webSelectOrder.dealId, 
                                                    departmentId:   properties.webSelectOrder.departmentId
                                                });
                                                //checkInItemScreen = CheckInController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                                if (properties.checkInType === RwConstants.checkInType.Normal) {
                                                    checkInItemScreen = CheckInController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                                } else if (properties.checkInType === RwConstants.checkInType.RfidPortal) {
                                                    checkInItemScreen = RFIDCheckIn.getModuleScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                                }
                                                program.pushScreen(checkInItemScreen);
                                            }
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    });
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } else {
                            checkInItemScreen_viewModel = {};
                            checkInItemScreen_properties =  jQuery.extend({}, properties, {
                                webSelectOrder: properties.webSelectOrder,
                                sessionNo:      properties.contract.sessionNo,
                                contractId:     properties.contract.contractId, 
                                dealId:         properties.webSelectOrder.dealId, 
                                departmentId:   properties.webSelectOrder.departmentId
                            });
                            //checkInItemScreen = CheckInController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                            if (properties.checkInType === RwConstants.checkInType.Normal) {
                                checkInItemScreen = CheckInController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                            } else if (properties.checkInType === RwConstants.checkInType.RfidPortal) {
                                checkInItemScreen = RFIDCheckIn.getModuleScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                            }
                            program.pushScreen(checkInItemScreen);
                        }
                        break;
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;
    
    screen.load = function() {
        program.setScanTarget('#fwmobilecontrol-value');
        if (!Modernizr.touch) {
            jQuery('#fwmobilecontrol-value').select();
        }
        //screen.loadOrders();
        screen.loadlistview();
    };

    screen.loadlistview = function() {
        var $listview, itemtemplate, itemmodel={};
        screen.$view.find('#selectOrder-error').hide();
        screen.$view.find('.selectOrder-response').hide();
        $listview = screen.$view.find('.listview');
        $listview.show();
        switch(properties.moduleType) {
            case 'Order':
                itemmodel = {
                    captiondescription: RwLanguages.translate('Desc'),
                    captionorderno:     RwLanguages.translate('Order No'),
                    captionorderdate:   RwLanguages.translate('Date'),
                    captiondeal:        RwLanguages.translate('Deal'),
                    captionstatusdate:  RwLanguages.translate('As Of'),
                    captionstatus:      RwLanguages.translate('Status'),
                    captionestrentfrom: RwLanguages.translate('Est')
                }
                itemtemplate = jQuery('#tmpl-getOrdersItem').html();
                break;
            case 'Truck':
                itemmodel = {
                    captionDescription: RwLanguages.translate('Desc'),
                    captionOrderNo:     RwLanguages.translate('Truck No'),
                    captionAsOf:        RwLanguages.translate('As Of'),
                    captionStatus:      RwLanguages.translate('Status'),
                    captionDepartment:  RwLanguages.translate('Department')
                }
                itemtemplate = jQuery('#tmpl-getPackageTruckItem').html();
                break;
        }
        
        FwListView.search($listview, {
                moduletype: properties.moduleType,
                activitytype: properties.activityType
            },
            function(request) {
                request.searchstring = screen.$view.find('.fwmobilecontrol-value').val().toUpperCase();
                RwServices.callMethod('SelectOrder', 'GetOrders', request, function(response) {
                    FwListView.clear($listview);
                    FwListView.load($listview, request, response.GetOrders, itemtemplate, itemmodel);
                });
            }
        );
    };

    return screen;
};