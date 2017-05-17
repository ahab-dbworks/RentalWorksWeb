//----------------------------------------------------------------------------------------------
RwOrderController.getCheckInMenuScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $menu, $findorder, $findsession, $finddeal;
    combinedViewModel = jQuery.extend({
          captionPageTitle:   RwLanguages.translate('Check-In Menu')
        , captionCheckInMenu: RwLanguages.translate('Check-In Menu')
        , captionSingleOrder: RwLanguages.translate('Order No')
        , captionMultiOrder:  RwLanguages.translate('Bar Code No')
        , captionSession:     RwLanguages.translate('Session No')
        , captionDeal:        RwLanguages.translate('Deal No')
        , captionQuikCheckIn: RwLanguages.translate('Quik Check-In')
    }, viewModel);
    combinedViewModel.htmlPageBody  = Mustache.render(jQuery('#tmpl-checkInMenu').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);

    screen.orderinfo = {};
    screen.setOrderInfo = function (orderinfo) {
        screen.orderinfo = orderinfo;
    };
    screen.getOrderInfo = function () {
        return screen.orderinfo;
    };
    screen.ordertosession = false;
    screen.setOrderToSession = function (ordertosession) {
        screen.ordertosession = ordertosession;
    };
    screen.getOrderToSession = function () {
        return screen.ordertosession;
    };

    $menu        = screen.$view.find('.fwmenu');
    $findorder   = screen.$view.find('.cim-findorder');
    $findsession = screen.$view.find('.cim-findsession');
    $finddeal    = screen.$view.find('.cim-finddeal');

    screen.$btnback = FwMobileMasterController.addFormControl(screen, 'Back', 'left', 'back', false, function () {
        if (screen.getOrderToSession()) {
            screen.setOrderToSession(false);
            $findorder.show();
            $findsession.hide();
            screen.$btnnewsession.hide();
        } else {
            screen.setOrderInfo({});
            $menu.show();
            $findorder.hide();
            $findsession.hide();
            $finddeal.hide();
            screen.$btnback.hide();
        }
    });
    screen.$btnnewsession = FwMobileMasterController.addFormControl(screen, 'New Session', 'right', 'continue', false, function () {
        var request, orderinfo;
        orderinfo = screen.getOrderInfo();
        request = {
            orderid:      orderinfo.orderid,
            dealid:       orderinfo.dealid,
            departmentid: orderinfo.departmentid
        };
        RwServices.callMethod('CheckIn', 'CreateSuspendedSession', request, function (response) {
            var props, checkInItemScreen;
            props = {
                moduleType:   RwConstants.moduleTypes.Order,
                activityType: RwConstants.activityTypes.CheckIn,
                checkInMode:  RwConstants.checkInModes.SingleOrder,
                checkInType:  RwConstants.checkInType.Normal,
                selectedorder: {
                    orderid:      orderinfo.orderid,
                    orderno:      orderinfo.orderno,
                    orderdesc:    orderinfo.orderdesc,
                    dealid:       orderinfo.dealid,
                    departmentid: orderinfo.departmentid,
                    contractid:   response.contractid,
                    sessionno:    response.sessionno
                }
            };

            checkInItemScreen = RwOrderController.getCheckInScreen({}, props);
            application.pushScreen(checkInItemScreen);
        });
    });

    $menu
        .on('click', '#checkInMenuView-iconSingleOrder', function() {
            $menu.hide();
            $findorder.show();
            $findorder.find('#ordersearch').fwmobilesearch('clearsearchbox');
            $findorder.find('#ordersearch').fwmobilesearch('search');
            screen.$btnback.show();
        })
        .on('click', '#checkInMenuView-iconMultiOrder', function() {
            var props, checkInItemScreen;
            props = {
                moduleType:   RwConstants.moduleTypes.Order,
                activityType: RwConstants.activityTypes.CheckIn,
                checkInMode:  RwConstants.checkInModes.MultiOrder,
                checkInType:  RwConstants.checkInType.Normal
            };

            checkInItemScreen = RwOrderController.getCheckInScreen({}, props);
            application.pushScreen(checkInItemScreen);
        })
        .on('click', '#checkInMenuView-iconSession', function() {
            $menu.hide();
            $findsession.show();
            $findsession.find('#sessionsearch').fwmobilesearch('clearsearchbox');
            $findsession.find('#sessionsearch').fwmobilesearch('search');
            screen.$btnback.show();
        })
        .on('click', '#checkInMenuView-iconDeal', function() {
            $menu.hide();
            $finddeal.show();
            $finddeal.find('#dealsearch').fwmobilesearch('clearsearchbox');
            $finddeal.find('#dealsearch').fwmobilesearch('search');
            screen.$btnback.show();
        })
    ;

    $findorder.find('#ordersearch').fwmobilesearch({
        service:   'CheckIn',
        method:    'OrderSearch',
        upperCase: true,
        searchModes: [
            { value: 'ORDERNO',     caption: 'Order No.' },
            { value: 'DESCRIPTION', caption: 'Description' },
            { value: 'DEAL',        caption: 'Deal' }
        ],
        itemTemplate: function(model) {
            var html = [];
            html.push('<div class="item">');
            html.push('  <div class="row1"><span class="orderdesc">{{orderdesc}}</span></div>');
            html.push('  <div class="row2">');
            html.push('    <div class="col1">');
            if (typeof model.orderno !== 'undefined') {
                html.push('      <div class="datafield orderno">');
                html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('        <div class="value">{{orderno}}</div>');
                html.push('      </div>')
            }
            if (typeof model.orderdate !== 'undefined') {
                html.push('      <div class="datafield orderdate">')
                html.push('        <div class="caption">' + RwLanguages.translate('Date') + ':</div>');
                html.push('        <div class="value">{{orderdate}}</div>');
                html.push('      </div>');
            }
            if (typeof model.deal !== 'undefined') {
                html.push('      <div class="datafield deal">')
                html.push('        <div class="caption">' + RwLanguages.translate('Deal') + ':</div>');
                html.push('        <div class="value">{{deal}}</div>');
                html.push('      </div>');
            }
            if (typeof model.estrentfrom !== 'undefined') {
                html.push('      <div class="datafield estrentfrom">')
                html.push('        <div class="caption">' + RwLanguages.translate('Est') + ':</div>');
                html.push('        <div class="value">{{estrentfrom}}</div>');
                html.push('      </div>');
            }
            if (typeof model.fromwarehouse !== 'undefined') {
                html.push('      <div class="datafield fromwarehouse">')
                html.push('        <div class="caption">' + RwLanguages.translate('From Warehouse') + ':</div>');
                html.push('        <div class="value">{{fromwarehouse}}</div>');
                html.push('      </div>');
            }
            if (typeof model.towarehouse !== 'undefined') {
                html.push('      <div class="datafield towarehouse">')
                html.push('        <div class="caption">' + RwLanguages.translate('To Warehouse') + ':</div>');
                html.push('        <div class="value">{{towarehouse}}</div>');
                html.push('      </div>');
            }
            if (typeof model.department !== 'undefined') {
                html.push('      <div class="datafield department">')
                html.push('        <div class="caption">' + RwLanguages.translate('Department') + ':</div>');
                html.push('        <div class="value">{{department}}</div>');
                html.push('      </div>');
            }
            if (typeof model.origorderno !== 'undefined') {
                html.push('      <div class="datafield origorderno">')
                html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('        <div class="value">{{origorderno}}</div>');
                html.push('      </div>');
            }
            html.push('    </div>');
            html.push('    <div class="col2">');
            if (typeof model.status !== 'undefined') {
                html.push('      <div class="datafield status">')
                html.push('        <div class="caption">' + RwLanguages.translate('Status') + ':</div>');
                html.push('        <div class="value">{{status}}</div>');
                html.push('      </div>');
            }
            if (typeof model.statusdate !== 'undefined') {
                html.push('      <div class="datafield statusdate">')
                html.push('        <div class="caption">' + RwLanguages.translate('As Of') + ':</div>');
                html.push('        <div class="value">{{statusdate}}</div>');
                html.push('      </div>');
            }
            if (typeof model.sessionno !== 'undefined') {
                html.push('      <div class="datafield sessionno">')
                html.push('        <div class="caption">' + RwLanguages.translate('Session No') + ':</div>');
                html.push('        <div class="value">{{sessionno}}</div>');
                html.push('      </div>');
            }
            if (typeof model.username !== 'undefined') {
                html.push('      <div class="datafield username">')
                html.push('        <div class="caption">' + RwLanguages.translate('Owner') + ':</div>');
                html.push('        <div class="value">{{username}}</div>');
                html.push('      </div>');
            }
            if (typeof model.pickdate !== 'undefined') {
                html.push('      <div class="datafield pickdate">')
                html.push('        <div class="caption">' + RwLanguages.translate('Pick Date') + ':</div>');
                html.push('        <div class="value">{{pickdate}}</div>');
                html.push('      </div>');
            }
            if (typeof model.shipdate !== 'undefined') {
                html.push('      <div class="datafield shipdate">')
                html.push('        <div class="caption">' + RwLanguages.translate('Ship Date') + ':</div>');
                html.push('        <div class="value">{{shipdate}}</div>');
                html.push('      </div>');
            }
            if (typeof model.receivedate !== 'undefined') {
                html.push('      <div class="datafield receivedate">')
                html.push('        <div class="caption">' + RwLanguages.translate('Receive Date') + ':</div>');
                html.push('        <div class="value">{{receivedate}}</div>');
                html.push('      </div>');
            }
            html.push('    </div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('\n');
            return html;
        },
        recordClick: function(recorddata) {
            var request = {
                orderid:     recorddata.orderid,
                pageno:      0,
                pagesize:    0,
                searchvalue: ''
            };
            RwServices.callMethod('CheckIn', 'SessionSearch', request, function (response) {
                try {
                    if (response.searchresults.Rows.length > 0) {
                        screen.setOrderInfo(recorddata);
                        screen.setOrderToSession(true);
                        $findorder.hide();
                        $findsession.show();
                        screen.$btnnewsession.show();
                        $findsession.find('#sessionsearch').fwmobilesearch('clearsearchbox');
                        $findsession.find('#sessionsearch').fwmobilesearch('load', response.searchresults);
                    } else {
                        // since there are no suspended sessions, make a new contractid
                        var request = {
                            orderid:      recorddata.orderid,
                            dealid:       recorddata.dealid,
                            departmentid: recorddata.departmentid
                        };
                        RwServices.callMethod('CheckIn', 'CreateSuspendedSession', request, function (response) {
                            var props, checkInItemScreen;
                            props = {
                                moduleType:   RwConstants.moduleTypes.Order,
                                activityType: RwConstants.activityTypes.CheckIn,
                                checkInMode:  RwConstants.checkInModes.SingleOrder,
                                checkInType:  RwConstants.checkInType.Normal,
                                selectedorder: {
                                    orderid:      recorddata.orderid,
                                    orderno:      recorddata.orderno,
                                    orderdesc:    recorddata.orderdesc,
                                    dealid:       recorddata.dealid,
                                    departmentid: recorddata.departmentid,
                                    contractid:   response.contractid,
                                    sessionno:    response.sessionno
                                }
                            };

                            checkInItemScreen = RwOrderController.getCheckInScreen({}, props);
                            application.pushScreen(checkInItemScreen);
                        });
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    });

    $findsession.find('#sessionsearch').fwmobilesearch({
        service:   'CheckIn',
        method:    'SessionSearch',
        upperCase: true,
        searchModes: [
            {
                caption: 'Suspended Sessions', placeholder: 'Session No', value: 'sessionno', visible: false,
                click: function () {
                    screen.$ordersuspendedsessions.fwmobilesearch('clearsearchbox');
                    screen.$ordersuspendedsessions.fwmobilesearch('search');
                }
            }
        ],
        getRequest: function() {
            var request = {
                orderid:    (typeof screen.getOrderInfo().orderid == 'undefined') ? '' : screen.getOrderInfo().orderid,
                moduletype: RwConstants.moduleTypes.Order
            };
            return request;
        },
        itemTemplate: function(model) {
            var html = [];
            html.push('<div class="item">');
            html.push('  <div class="row1"><span class="orderdesc">{{orderdesc}}</span></div>');
            html.push('  <div class="row2">');
            html.push('    <div class="col1">');
            if (typeof model.orderno !== 'undefined') {
                html.push('      <div class="datafield orderno">');
                html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('        <div class="value">{{orderno}}</div>');
                html.push('      </div>')
            }
            if (typeof model.orderdate !== 'undefined') {
                html.push('      <div class="datafield orderdate">')
                html.push('        <div class="caption">' + RwLanguages.translate('Date') + ':</div>');
                html.push('        <div class="value">{{orderdate}}</div>');
                html.push('      </div>');
            }
            if (typeof model.deal !== 'undefined') {
                html.push('      <div class="datafield deal">')
                html.push('        <div class="caption">' + RwLanguages.translate('Deal') + ':</div>');
                html.push('        <div class="value">{{deal}}</div>');
                html.push('      </div>');
            }
            if (typeof model.estrentfrom !== 'undefined') {
                html.push('      <div class="datafield estrentfrom">')
                html.push('        <div class="caption">' + RwLanguages.translate('Est') + ':</div>');
                html.push('        <div class="value">{{estrentfrom}}</div>');
                html.push('      </div>');
            }
            if (typeof model.warehouse !== 'undefined') {
                html.push('      <div class="datafield warehouse">')
                html.push('        <div class="caption">' + RwLanguages.translate('Warehouse') + ':</div>');
                html.push('        <div class="value">{{warehouse}}</div>');
                html.push('      </div>');
            }
            html.push('    </div>');
            html.push('    <div class="col2">');
            if (typeof model.status !== 'undefined') {
                html.push('      <div class="datafield">')
                html.push('        <div class="caption status">' + RwLanguages.translate('Status') + ':</div>');
                html.push('        <div class="value status">{{status}}</div>');
                html.push('      </div>');
            }
            if (typeof model.statusdate !== 'undefined') {
                html.push('      <div class="datafield">')
                html.push('        <div class="caption statusdate">' + RwLanguages.translate('As Of') + ':</div>');
                html.push('        <div class="value statusdate">{{statusdate}}</div>');
                html.push('      </div>');
            }
            if (typeof model.sessionno !== 'undefined') {
                html.push('      <div class="datafield sessionno">')
                html.push('        <div class="caption">' + RwLanguages.translate('Session No') + ':</div>');
                html.push('        <div class="value">{{sessionno}}</div>');
                html.push('      </div>');
            }
            if (typeof model.username !== 'undefined') {
                html.push('      <div class="datafield username">')
                html.push('        <div class="caption">' + RwLanguages.translate('Owner') + ':</div>');
                html.push('        <div class="value">{{username}}</div>');
                html.push('      </div>');
            }
            html.push('    </div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('\n');
            return html;
        },
        recordClick: function(recorddata) {
            var props, checkInItemScreen;
            props = {
                moduleType:   RwConstants.moduleTypes.Order,
                activityType: RwConstants.activityTypes.CheckIn,
                checkInMode:  RwConstants.checkInModes.Session,
                checkInType:  RwConstants.checkInType.Normal,
                selectedsession: {
                    orderid:      recorddata.orderid,
                    orderno:      recorddata.orderno,
                    orderdesc:    recorddata.orderdesc,
                    dealid:       recorddata.dealid,
                    departmentid: recorddata.departmentid,
                    contractid:   recorddata.contractid,
                    sessionno:    recorddata.sessionno
                }
            };

            checkInItemScreen = RwOrderController.getCheckInScreen({}, props);
            application.pushScreen(checkInItemScreen);
        }
    });

    $finddeal.find('#dealsearch').fwmobilesearch({
        service:   'CheckIn',
        method:    'DealSearch',
        upperCase: true,
        searchModes: [
            { value: 'DEALNO', caption: 'Deal No.' },
            { value: 'NAME',   caption: 'Name' }
        ],
        itemTemplate: function() {
            var html = [];

            html.push('<div class="record">');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed bold">Name:</div><div class="value">{{deal}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed bold">Deal No:</div><div class="value">{{dealno}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Department:</div><div class="value">{{department}}</div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('');

            return html;
        },
        recordClick: function(recorddata) {
            var props, checkInItemScreen;
            props = {
                moduleType:   RwConstants.moduleTypes.Order,
                activityType: RwConstants.activityTypes.CheckIn,
                checkInMode:  RwConstants.checkInModes.Deal,
                checkInType:  RwConstants.checkInType.Normal,
                selecteddeal: {
                    dealid:       recorddata.dealid,
                    dealno:       recorddata.dealno,
                    deal:         recorddata.deal,
                    departmentid: recorddata.departmentid
                }
            };

            checkInItemScreen = RwOrderController.getCheckInScreen({}, props);
            application.pushScreen(checkInItemScreen);
        }
    });

    screen.load = function() {

    };

    return screen;
};