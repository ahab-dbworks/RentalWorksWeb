//----------------------------------------------------------------------------------------------
RwOrderController.getCheckInMenuScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $menu, $ordersearch, $sessionsearch, $dealsearch;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Check-In Menu'),
        captionCheckInMenu: RwLanguages.translate('Check-In Menu'),
        captionSingleOrder: RwLanguages.translate('Order No'),
        captionMultiOrder:  RwLanguages.translate('Bar Code No'),
        captionSession:     RwLanguages.translate('Session No'),
        captionDeal:        RwLanguages.translate('Deal No'),
        captionQuikIn:      RwLanguages.translate('QuikIn')
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

    $menu          = screen.$view.find('#cim-menu');
    $ordersearch   = screen.$view.find('#cim-ordersearch');
    $sessionsearch = screen.$view.find('#cim-sessionsearch');
    $dealsearch    = screen.$view.find('#cim-dealsearch');
    $quikin        = screen.$view.find('#cim-quikin');

    var applicationOptions = program.getApplicationOptions();
    $quikin.toggle(typeof applicationOptions.quikin !== 'undefined' && applicationOptions.quikin.enabled === true);

    $menu
        .on('click', '#cim-singleorder', function() {
            $menu.hide();
            $ordersearch.showscreen();
        })
        .on('click', '#cim-multiorder', function() {
            var props, checkInItemScreen;
            props = {
                moduleType:   RwConstants.moduleTypes.Order,
                activityType: RwConstants.activityTypes.CheckIn,
                checkInMode:  RwConstants.checkInModes.MultiOrder,
                checkInType:  RwConstants.checkInType.Normal
            };

            checkInItemScreen = RwOrderController.getCheckInScreen({}, props);
            program.pushScreen(checkInItemScreen);
        })
        .on('click', '#cim-session', function() {
            $menu.hide();
            $sessionsearch.showscreen({});
        })
        .on('click', '#cim-deal', function() {
            $menu.hide();
            $dealsearch.showscreen();
        })
        .on('click', '#cim-quikin', function() {
            try {
                $menu.hide();
                var quikInScreen = QuikIn.getModuleScreen({}, {});
                program.pushScreen(quikInScreen);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    $ordersearch.find('#ordersearchcontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       0,
                buttonclick: function () {
                    FwMobileMasterController.setTitle('');
                    $ordersearch.hide();
                    $menu.show();
                }
            },
            {
                id:          'itemlist_menu',
                type:        'menu',
                orientation: 'right',
                icon:        '&#xE5D4;', //more-vert
                state:       0,
                menuoptions: [
                    {
                        id:      'showalllocation',
                        caption: 'Show All Locations',
                        buttonclick: function() {
                            screen.toggleShowAllLocation();
                            $ordersearch.find('#ordersearch').fwmobilesearch('search');
                        }
                    }
                ]
            }
        ]
    });
    $ordersearch.find('#ordersearch').fwmobilesearch({
        service:   'CheckInMenu',
        method:    'OrderSearch',
        searchModes: [
            { value: 'ORDERNO',     caption: 'Order No.' },
            { value: 'DESCRIPTION', caption: 'Description' },
            { value: 'DEAL',        caption: 'Deal' }
        ],
        getRequest: function() {
            var request = {
                showalllocation: screen.showalllocation
            };
            return request;
        },
        cacheItemTemplate: false,
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
                html.push('      </div>');
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
                html.push('      <div class="datafield estrentto">')
                html.push('        <div class="caption">' + RwLanguages.translate('Est Return') + ':</div>');
                html.push('        <div class="value">{{estrentto}}</div>');
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
        afterLoad: function (plugin, response) {
            var searchOption = plugin.getSearchOption();
            var isOrderNoSearch = searchOption === 'ORDERNO';
            if (isOrderNoSearch) {
                var searchText = plugin.getSearchText();
                if (searchText.length > 0) {
                    if (response.searchresults.TotalRows === 0) {
                        program.playStatus(false);
                    } else if (response.searchresults.TotalRows === 1) {
                        var colOrderNo = response.searchresults.ColumnIndex['orderno'];
                        if (response.searchresults.Rows[0][colOrderNo].toUpperCase() === searchText.toUpperCase()) {
                            program.playStatus(true);
                            var $items = plugin.$element.find('.searchresults .item');
                            if ($items.length === 1) {
                                var $item = $items.eq(0);
                                $item.click();
                            }
                        }
                    }
                }
            }
        },
        recordClick: function(recorddata) {
            var request = {
                orderid:         recorddata.orderid,
                pageno:          0,
                pagesize:        0,
                searchvalue:     '',
                showalllocation: screen.showalllocation
            };
            RwServices.callMethod('CheckInMenu', 'SessionSearch', request, function (response) {
                try {
                    if (response.searchresults.Rows.length > 0) {
                        screen.setOrderInfo(recorddata);
                        $ordersearch.hide();
                        $sessionsearch.showscreen(response.searchresults);
                    } else {
                        // since there are no suspended sessions, make a new contractid
                        var request = {
                            orderid:      recorddata.orderid,
                            dealid:       recorddata.dealid,
                            departmentid: recorddata.departmentid
                        };
                        RwServices.callMethod('CheckInMenu', 'CreateSuspendedSession', request, function (response) {
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
                            program.pushScreen(checkInItemScreen);
                        });
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    });
    $ordersearch.showscreen = function() {
        FwMobileMasterController.setTitle('Select Order...');
        $ordersearch.find('#ordersearch').fwmobilesearch('clearsearchbox');
        $ordersearch.show();
        $ordersearch.find('#ordersearch').fwmobilesearch('search');
    };

    $sessionsearch.find('#sessionsearchcontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       0,
                buttonclick: function () {
                    FwMobileMasterController.setTitle('');
                    screen.setOrderInfo({});
                    $sessionsearch.hide();
                    $menu.show();
                }
            },
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       1,
                buttonclick: function () {
                    FwMobileMasterController.setTitle('Select Order...');
                    screen.setOrderInfo({});
                    $sessionsearch.hide();
                    $ordersearch.show();
                }
            },
            {
                id:          'itemlist_menu',
                type:        'menu',
                orientation: 'right',
                icon:        '&#xE5D4;', //more_vert
                state:       0,
                menuoptions: [
                    {
                        id:      'showalllocation',
                        caption: 'Show All Locations',
                        buttonclick: function() {
                            screen.toggleShowAllLocation();
                            $sessionsearch.find('#sessionsearch').fwmobilesearch('search');
                        }
                    }
                ]
            },
            {
                caption:     'New Session',
                orientation: 'right',
                icon:        '&#xE5CC;', //chevron_right
                state:       1,
                buttonclick: function () {
                    var request, orderinfo;
                    orderinfo = screen.getOrderInfo();
                    request = {
                        orderid:      orderinfo.orderid,
                        dealid:       orderinfo.dealid,
                        departmentid: orderinfo.departmentid
                    };
                    RwServices.callMethod('CheckInMenu', 'CreateSuspendedSession', request, function (response) {
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
                        program.pushScreen(checkInItemScreen);
                    });
                }
            }
        ]
    });
    $sessionsearch.find('#sessionsearch').fwmobilesearch({
        service:   'CheckInMenu',
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
                orderid:         (typeof screen.getOrderInfo().orderid === 'undefined') ? '' : screen.getOrderInfo().orderid,
                moduletype:      RwConstants.moduleTypes.Order,
                showalllocation: screen.showalllocation
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
        afterLoad: function (plugin, response) {
            var searchOption = plugin.getSearchOption();
            var isSessionNoSearch = searchOption === 'sessionno';
            if (isSessionNoSearch) {
                var searchText = plugin.getSearchText();
                if (searchText.length > 0) {
                    if (response.searchresults.TotalRows === 0) {
                        program.playStatus(false);
                    } else if (response.searchresults.TotalRows === 1) {
                        var colSessionNo = response.searchresults.ColumnIndex['sessionno'];
                        if (response.searchresults.Rows[0][colSessionNo].toUpperCase() === searchText.toUpperCase()) {
                            program.playStatus(true);
                            var $items = plugin.$element.find('.searchresults .item');
                            if ($items.length === 1) {
                                var $item = $items.eq(0);
                                $item.click();
                            }
                        }
                    }
                }
            }
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
            program.pushScreen(checkInItemScreen);
        }
    });
    $sessionsearch.showscreen = function(searchresults) {
        FwMobileMasterController.setTitle('Select Session...');
        $sessionsearch.find('#sessionsearch').fwmobilesearch('clearsearchbox');
        $sessionsearch.show();

        if (jQuery.isEmptyObject(searchresults)) {
            $sessionsearch.find('#sessionsearchcontrol').fwmobilemodulecontrol('changeState', 0);
            $sessionsearch.find('#sessionsearch').fwmobilesearch('search');
        } else {
            $sessionsearch.find('#sessionsearchcontrol').fwmobilemodulecontrol('changeState', 1);
            $sessionsearch.find('#sessionsearch').fwmobilesearch('load', searchresults);
        }
    };

    $dealsearch.find('#dealsearchcontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //chevron_left
                state:       0,
                buttonclick: function () {
                    FwMobileMasterController.setTitle('');
                    $dealsearch.hide();
                    $menu.show();
                }
            },
            {
                id:          'itemlist_menu',
                type:        'menu',
                orientation: 'right',
                icon:        '&#xE5D4;', //more_vert
                state:       0,
                menuoptions: [
                    {
                        id:      'showalllocation',
                        caption: 'Show All Locations',
                        buttonclick: function() {
                            screen.toggleShowAllLocation();
                            $dealsearch.find('#dealsearch').fwmobilesearch('search');
                        }
                    }
                ]
            }
        ]
    });
    $dealsearch.find('#dealsearch').fwmobilesearch({
        service:   'CheckInMenu',
        method:    'DealSearch',
        upperCase: true,
        searchModes: [
            { value: 'NAME',   caption: 'Name' },
            { value: 'DEALNO', caption: 'Deal No.' }
        ],
        getRequest: function() {
            var request = {
                showalllocation: screen.showalllocation
            };
            return request;
        },
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
        afterLoad: function (plugin, response) {
            var searchOption = plugin.getSearchOption();
            var isDealNoSearch = searchOption === 'DEALNO';
            if (isDealNoSearch) {
                var searchText = plugin.getSearchText();
                if (searchText.length > 0) {
                    if (response.searchresults.TotalRows === 0) {
                        program.playStatus(false);
                    } else if (response.searchresults.TotalRows === 1) {
                        var colDealNo = response.searchresults.ColumnIndex['dealno'];
                        if (response.searchresults.Rows[0][colDealNo].toUpperCase() === searchText.toUpperCase()) {
                            program.playStatus(true);
                            var $items = plugin.$element.find('.searchresults .record');
                            if ($items.length === 1) {
                                var $item = $items.eq(0);
                                $item.click();
                            }
                        }
                    }
                }
            }
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
            program.pushScreen(checkInItemScreen);
        }
    });
    $dealsearch.showscreen = function () {
        FwMobileMasterController.setTitle('Select Deal...');
        $dealsearch.find('#dealsearch').fwmobilesearch('clearsearchbox');
        $dealsearch.show();
        $dealsearch.find('#dealsearch').fwmobilesearch('search');
    };

    screen.toggleShowAllLocation = function(value) { //value = true/false
        screen.showalllocation = (typeof value !== 'undefined') ? value : !screen.showalllocation;
        if (screen.showalllocation) {
            $ordersearch.find('#ordersearchcontrol #showalllocation').empty().html('<i class="material-icons">&#xE834;</i><div style="line-height:24px;padding-left:5px;">Show All Locations</div>'); //check_box
            $sessionsearch.find('#sessionsearchcontrol #showalllocation').empty().html('<i class="material-icons">&#xE834;</i><div style="line-height:24px;padding-left:5px;">Show All Locations</div>'); //check_box
            $dealsearch.find('#dealsearchcontrol #showalllocation').empty().html('<i class="material-icons">&#xE834;</i><div style="line-height:24px;padding-left:5px;">Show All Locations</div>'); //check_box
        } else {
            $ordersearch.find('#ordersearchcontrol #showalllocation').empty().html('<i class="material-icons">&#xE835;</i><div style="line-height:24px;padding-left:5px;">Show All Locations</div>'); //check_box_outline_blank
            $sessionsearch.find('#sessionsearchcontrol #showalllocation').empty().html('<i class="material-icons">&#xE835;</i><div style="line-height:24px;padding-left:5px;">Show All Locations</div>'); //check_box_outline_blank
            $dealsearch.find('#dealsearchcontrol #showalllocation').empty().html('<i class="material-icons">&#xE835;</i><div style="line-height:24px;padding-left:5px;">Show All Locations</div>'); //check_box_outline_blank
        }
    };

    screen.enableShowAllLocations = function(enable) {
        if (typeof enable === 'undefined') {
            RwServices.callMethod("CheckInMenu", "EnableShowAllLocations", {}, function(response) {
                screen.enableShowAllLocations(response.enable);
            });
        } else {
            if (enable) {
                $ordersearch.find('#ordersearchcontrol').fwmobilemodulecontrol('showButton', '#showalllocation');
                $sessionsearch.find('#sessionsearchcontrol').fwmobilemodulecontrol('showButton', '#showalllocation');
                $dealsearch.find('#dealsearchcontrol').fwmobilemodulecontrol('showButton', '#showalllocation');
            } else {
                $ordersearch.find('#ordersearchcontrol').fwmobilemodulecontrol('hideButton', '#showalllocation');
                $sessionsearch.find('#sessionsearchcontrol').fwmobilemodulecontrol('hideButton', '#showalllocation');
                $dealsearch.find('#dealsearchcontrol').fwmobilemodulecontrol('hideButton', '#showalllocation');
            }
        }
    };

    screen.load = function() {
        screen.enableShowAllLocations();
        screen.toggleShowAllLocation(false);
        program.setScanTarget('');
        program.onScanBarcode = function (barcode, barcodeType) {
            try {
                $ordersearch.find('#ordersearch').fwmobilesearch('setsearchmode', 'ORDERNO');
                $ordersearch.find('#ordersearch .searchbox').val(barcode).change();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
    };

    screen.unload = function() {
        program.onScanBarcode = null;
    };

    return screen;
};