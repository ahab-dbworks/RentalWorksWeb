class CheckInControllerClass {
    //----------------------------------------------------------------------------------------------
    getOrderSuspendedSessionPopup(suspendedInContracts) {'use strict';
        var result, html, statusdate, sessionno, orderno, orderdesc, deal, username, status, rowView, rowModel;
    
        result = {};
        rowView = jQuery('#tmpl-checkIn-suspendedSession').html();
        html = [];
        html.push('<div class="checkin-suspendedsessions">');
        if (typeof suspendedInContracts === 'object') {
            for (var rowno = 0; rowno < suspendedInContracts.Rows.length; rowno++) {
                rowModel = {};
                rowModel.statusdate = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.statusdate];
                rowModel.sessionno  = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.sessionno];
                rowModel.orderno    = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.orderno];
                rowModel.orderdesc  = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.orderdesc];
                rowModel.deal       = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.deal];
                rowModel.username   = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.username];
                rowModel.status     = suspendedInContracts.Rows[rowno][suspendedInContracts.ColumnIndex.status];
                html.push(Mustache.render(rowView, rowModel));
            }
        }
        html.push('</div>');
        result.$popup = FwConfirmation.renderConfirmation(suspendedInContracts.Rows.length.toString() + ' Suspended Session' + ((suspendedInContracts.Rows.length === 1) ? '' : 's'), html.join(''));
        result.$popup.attr('data-nopadding', 'true');
        result.$btnJoinSession = FwConfirmation.addButton(result.$popup, 'Join Session', false);
        result.$btnJoinSession.on('click', function() {
            var $suspendedsession;
            try {
                $suspendedsession = result.$popup.find('.checkin-suspendedsession');
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
    //----------------------------------------------------------------------------------------------
    getCheckInScreen(viewModel, properties) {
        var combinedViewModel, screen, pageSubTitle, applicationOptions, locationdata = null, aisleshelfdata = null, moduleproperties = null;
        applicationOptions = program.getApplicationOptions();
        if (typeof properties.orderId            === 'undefined') { properties.orderId            = ''; }
        if (typeof properties.dealId             === 'undefined') { properties.dealId             = ''; }
        if (typeof properties.departmentId       === 'undefined') { properties.departmentId       = ''; }
        if (typeof properties.orderDesc          === 'undefined') { properties.orderDesc          = ''; }
        if (typeof properties.contractId         === 'undefined') { properties.contractId         = ''; }
        //if (typeof properties.exchangeContractId === 'undefined') {properties.exchangeContractId = '';}
        switch(properties.checkInMode) {
            case RwConstants.checkInModes.SingleOrder:
                pageSubTitle            = '<div class="title">' + properties.selectedorder.orderno + ' - ' + properties.selectedorder.orderdesc + ' (' + RwLanguages.translate('Session') + ': ' + properties.selectedorder.sessionno + ')</div>';
                properties.orderId      = properties.selectedorder.orderid;
                properties.dealId       = properties.selectedorder.dealid;
                properties.departmentId = properties.selectedorder.departmentid;
                properties.orderDesc    = properties.selectedorder.orderdesc;
                properties.contractId   = properties.selectedorder.contractid;
                break;
            case RwConstants.checkInModes.MultiOrder:
                pageSubTitle            = '<div class="title">' + RwLanguages.translate('Multi-Order') + '<br/>'  + RwLanguages.translate('First scan must be a Bar Code...') + '</div>';
                break;
            case RwConstants.checkInModes.Session:
                pageSubTitle            = '<div class="title">' + properties.selectedsession.orderno + ' - ' + properties.selectedsession.orderdesc + ' (' + RwLanguages.translate('Session') + ': ' + properties.selectedsession.sessionno + ')</div>';
                properties.orderId      = properties.selectedsession.orderid;
                properties.dealId       = properties.selectedsession.dealid;
                properties.departmentId = properties.selectedsession.departmentid;
                properties.orderDesc    = properties.selectedsession.orderdesc;
                properties.contractId   = properties.selectedsession.contractid;
                break;
            case RwConstants.checkInModes.Deal:
                pageSubTitle            = '<div class="title">' + properties.selecteddeal.dealno + ' - ' + properties.selecteddeal.deal + '</div>';
                properties.dealId       = properties.selecteddeal.dealid;
                properties.departmentId = properties.selecteddeal.departmentid;
                break;
        }
        combinedViewModel = jQuery.extend({
            captionPageTitle:          RwOrderController.getPageTitle(properties),
            captionPageSubTitle:       pageSubTitle,
            htmlScanBarcode:           RwPartialController.getScanBarcodeHtml({
                captionBarcodeICode: RwLanguages.translate('Barcode / I-Code')
            }),
            captionDesc:               RwLanguages.translate('Description'),
            captionQty:                RwLanguages.translate('Qty'),
            captionSummary:            RwLanguages.translate('Summary'),
            captionApplyAllQtyItems:   RwLanguages.translate('Apply All Qty Items')
        }, viewModel);
        combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-checkIn').html(), combinedViewModel);
        screen            = {};
        screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel, properties);
        screen.properties = properties;

        screen.properties.currentview = '';

        screen.getOrderId            = function() { return properties.orderId; };
        screen.getDealId             = function() { return properties.dealId; };
        screen.getDepartmentId       = function() { return properties.departmentId; };
        screen.getOrderDesc          = function() { return properties.orderDesc; };
        screen.getContractId         = function() { return properties.contractId; };
        screen.getExchangeContractId = function() { return properties.exchangeContractId; };
        screen._locationdata = function(locationData?: object) {
            if (locationData) {
                locationdata = locationData;
            } else {
                return locationdata;
            }
        }
        screen._aisleshelfdata = function(aisleshelfData?: object) {
            if (aisleshelfData) {
                aisleshelfdata = aisleshelfData;
            } else {
                return aisleshelfdata;
            }
        }

        const $primarywindow       = screen.$view.find('#checkin-primary');
        const $barcodescanwindow   = screen.$view.find('#scanBarcodeView');
        const $pending             = screen.$view.find('#checkIn-pendingList');
        const $rfid                = screen.$view.find('#checkIn-rfid');
        const $sessionin           = screen.$view.find('#checkIn-sessionInList');
        const $orderpriority       = screen.$view.find('#checkIn-orderpriority');
        const $checkinmodeselector = screen.$view.find('#checkinmodeselector');
        const $checkincontrol      = screen.$view.find('#checkincontroller');
        const $checkinserial       = screen.$view.find('#checkIn-serial');
        const $extraitems          = screen.$view.find('#checkIn-extraitems');
        const $orderlocation       = screen.$view.find('#checkIn-orderlocation');

        $checkinmodeselector.fwmobilemoduletabs({
            tabs: [
                {
                    id:          'pendingtab',
                    caption:     'Pending',
                    buttonclick: function () {
                        program.setScanTargetLpNearfield('#pendingsearch .searchbox');
                        $pending.showscreen();
                        $pending.find('#pendingsearch').fwmobilesearch('refresh');
                    }
                },
                {
                    id:          'rfidtab',
                    caption:     'RFID',
                    buttonclick: function () {
                        program.setScanTargetLpNearfield('');
                        $rfid.showscreen();
                    }
                },
                {
                    id:          'sessiontab',
                    caption:     'Session In',
                    buttonclick: function () {
                        program.setScanTargetLpNearfield('#sessioninsearch .searchbox');
                        $sessionin.showscreen();
                        $sessionin.find('#sessioninsearch').fwmobilesearch('refresh');
                    }
                }
            ]
        });
        $checkincontrol.fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Close',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        if (properties.moduleType === 'Order') {
                            program.navigate('order/checkinmenu');
                        } else if (properties.moduleType === 'Transfer') {
                            program.navigate('order/transferin');
                        } else {
                            program.navigate('home/home');
                        }
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
                            id:      'reconcile',
                            caption: 'Reconcile',
                            buttonclick: function() {
                                $orderpriority.showscreen();
                            }
                        },
                        {
                            id:          'fillcontainer',
                            caption:     'Fill Container',
                            buttonclick: function() {
                                try {
                                    var fillcontainerproperties = jQuery.extend({}, properties, {
                                        mode:              'checkin',
                                        modulecaption:     jQuery('#master-header-caption').html(),
                                        pagetitle:         pageSubTitle,
                                        checkincontractid: screen.getContractId(),
                                        orderid:           screen.getOrderId(),
                                        dealid:            screen.getDealId(),
                                        departmentid:      screen.getDepartmentId(),
                                        aisle:             screen._aisleshelfdata()?.Aisle || '',
                                        shelf:             screen._aisleshelfdata()?.Shelf || ''
                                    });
                                    program.pushScreen(RwFillContainer.getFillContainerScreen({}, fillcontainerproperties));
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            }
                        },
                        {
                            id:      'rfidexceptions',
                            caption: 'Load RFID Exceptions',
                            buttonclick: function() {
                                $rfid.loadexceptions();
                            }
                        },
                        {
                            id:      'selectorderlocation',
                            caption: 'Select Order Location',
                            buttonclick: function() {
                                $primarywindow.hide();
                                $orderlocation.showscreen();
                            }
                        },
                        {
                            id:      'applyallqtyitems',
                            caption: 'Apply All Qty Items',
                            buttonclick: function() {
                                $pending.applyallqtyitems();
                            }
                        },
                        {
                            id:      'extraitems',
                            caption: 'Extra Items',
                            buttonclick: function() {
                                $sessionin.hide();
                                $extraitems.showscreen();
                            }
                        },
                        {
                            id: 'startrfid',
                            caption: 'Start RFID',
                            buttonclick: function () {
                                try {
                                    RwRFID.tslSwitchSinglePress();
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            }
                        },
                        {
                            id: 'stoprfid',
                            caption: 'Stop RFID',
                            buttonclick: function () {
                                try {
                                    RwRFID.tslAbort();
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            }
                        }
                    ]
                },
                {
                    caption:     'Create Contract',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //chevron_right
                    state:       0,
                    isVisible: function () {
                        var isVisible = false;
                        if (sessionStorage.getItem('users_enablecreatecontract') !== null) {
                            isVisible = (sessionStorage.getItem('users_enablecreatecontract') === 'T');
                        }
                        return isVisible;
                    },
                    buttonclick: function () {
                        if (screen.getContractId() !== '') {
                            var request = {
                                contractid: screen.getContractId()
                            };
                            RwServices.callMethod('CheckIn', 'GetShowCreateContract', request, function (response) {
                                try {
                                    if (response.showcreatecontract) {
                                        if (properties.moduleType === 'Transfer') {
                                            var request = {
                                                contractId:          screen.getContractId(),
                                                contractType:        'IN',
                                                orderId:             screen.getOrderId(),
                                                responsiblePersonId: '',
                                                printname:           '',
                                                signatureImage:      '',
                                                images:              []
                                            };
                                            RwServices.callMethod('CheckIn', 'CreateContract', request, function (response) {
                                                if (response.createcontract.status === 0) {
                                                    var $confirmation = FwConfirmation.renderConfirmation('Message', response.createcontract.msg);
                                                    var $ok           = FwConfirmation.addButton($confirmation, 'OK', true);

                                                    $ok.on('click', function () {
                                                        program.navigate('home/home');
                                                    });
                                                } else {
                                                    FwFunc.showError(response.createcontract.msg);
                                                }
                                            });
                                        } else {
                                            properties.contract = {
                                                contractType:        'IN',
                                                contractId:          screen.getContractId(),
                                                orderId:             screen.getOrderId(),
                                                responsiblePersonId: ''
                                            };
                                            program.pushScreen(RwOrderController.getContactSignatureScreen(viewModel, properties));
                                        }
                                    } else {
                                        FwFunc.showMessage("There is no activity on this Check-In Session!");
                                    }
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } else {
                            FwFunc.showMessage("There is no activity on this Check-In Session!");
                        }
                    }
                }
            ]
        });

        $pending.find('#pendingsearch').fwmobilesearch({
            service: 'CheckIn',
            method: 'PendingSearch',
            searchModes: [
                {
                    caption: 'Scan (no refresh)',
                    placeholder: 'Scan Bar Code / I-Code',
                    value: 'code',
                    hasVirtualNumpad: true,
                    //hasPager: false,
                    //hasSearchResults: false,
                    search: function (value, plugin) {
                        if (value.length > 0) {
                            screen.scanCode(value);
                            plugin.clearsearchbox();
                        } else {
                            return true; // do a normal search
                        }
                    },
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                },
                {
                    caption: 'Search by Description', placeholder: 'Description', value: 'description',
                    //search: function (description) {
                    //    if (description.length > 0) {
                    //        screen.$search.fwmobilesearch('search');
                    //    }
                    //},
                    click: function (plugin) {
                        plugin.clearsearchbox();
                        //plugin.search();
                    }
                }
            ],
            getRequest: function() {
                var request = {
                    contractid: screen.getContractId()
                };
                return request;
            },
            cacheItemTemplate: false,
            itemTemplate: function (model) {
                var html: string | string[] = [];
                html.push(`<div data-itemclass="${model.itemclass}">`);
                html.push('  <div class="row1"><div class="title">{{description}}</div></div>');
                html.push('  <div class="row2">');
                html.push('    <div class="col1">');
                html.push('      <div class="datafield masterno">');
                html.push('        <div class="caption">' + RwLanguages.translate('I-Code') + ':</div>');
                html.push('        <div class="value">{{masterno}}</div>');
                html.push('      </div>');
                if (model.vendorid === '') {
                    html.push('      <div class="datafield trackedby">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Tracked By') + ':</div>');
                    html.push('        <div class="value">{{trackedby}}</div>');
                    html.push('      </div>');
                } else {
                    html.push('      <div class="datafield trackedby">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Sub By') + ':</div>');
                    html.push('        <div class="value">' + ((model.subbyquantity) ? 'QUANTITY' : 'BARCODE') + '</div>');
                    html.push('      </div>');
                }
                html.push('      <div class="datafield orderno">');
                html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('        <div class="value">{{orderno}}</div>');
                html.push('      </div>');
                html.push('    </div>');
                html.push('    <div class="col2">');
                html.push('      <div class="datafield qtystillout">');
                html.push('        <div class="caption">' + RwLanguages.translate('Still Out') + ':</div>');
                html.push('        <div class="value">{{qtystillout}}</div>');
                html.push('      </div>');
                html.push('      <div class="datafield sessionin">');
                html.push('        <div class="caption">' + RwLanguages.translate('Session In') + ':</div>');
                html.push('        <div class="value">{{qtyin}}</div>');
                html.push('      </div>');
                html.push('    </div>');
                html.push('  </div>');
                if (model.vendorid !== '') {
                    html.push('  <div class="row3">');
                    html.push('    <div class="datafield vendor">');
                    html.push('      <div class="caption">' + RwLanguages.translate('Vendor/Consignor') + ':</div>');
                    html.push('      <div class="value">{{vendor}}</div>');
                    html.push('    </div>');
                    html.push('  </div>');
                }
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            hasRecordClick: (model: any): boolean => {
                return ((model.trackedby === 'SERIALNO' || model.trackedby === 'QUANTITY' || model.subbyquantity) && (model.qtystillout > 0));
            },
            recordClick: function(recorddata, $record) {
                try {
                    if (recorddata.trackedby === 'QUANTITY' || recorddata.subbyquantity === true) {
                        var orderId        = recorddata.orderid;
                        var masterItemId   = recorddata.masteritemid;
                        var masterId       = recorddata.masterid;
                        var code           = recorddata.masterno;
                        var qty            = 0;
                        var newOrderAction = '';
                        var aisle          = screen._aisleshelfdata()?.Aisle || '';
                        var shelf          = screen._aisleshelfdata()?.Shelf || '';
                        var playStatus     = false;
                        var vendorId       = recorddata.vendorid;
                        var trackedby      = recorddata.trackedby;
                        screen.checkInItem(orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId, trackedby);
                    } else if ($record.find('.trackedby .value').html() === 'SERIALNO') {
                        $pending.hide();
                        $checkinserial.showscreen(recorddata);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            afterLoad: function(plugin, response) {
                if (sessionStorage.getItem('users_qsallowapplyallqtyitems') === 'T') {
                    $checkincontrol.fwmobilemodulecontrol('showButton', '#applyallqtyitems');
                } else {
                    $checkincontrol.fwmobilemodulecontrol('hideButton', '#applyallqtyitems');
                }
                var showhideselectorder = (moduleproperties.syscontrol.itemsinrooms == "T") ? 'showButton' : 'hideButton';
                $checkincontrol.fwmobilemodulecontrol(showhideselectorder, '#selectorderlocation');
            }
        });
        $pending.showscreen = function() {
            $sessionin.hide();
            $rfid.hide();
            $pending.show();
            $barcodescanwindow.show();
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#rfidexceptions');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#selectorderlocation');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#applyallqtyitems');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#extraitems');
            screen.properties.currentview = 'PENDING';
            if (screen.getContractId() !== '') {
                const searchOption = $pending.find('#pendingsearch').fwmobilesearch('getSearchOption');
                switch (searchOption) {
                    case 'code':
                        $pending.find('#pendingsearch').fwmobilesearch('clearsearchbox');
                        $pending.find('#pendingsearch').fwmobilesearch('clearsearchresults');
                        break;
                    case 'decription':
                        $pending.find('#pendingsearch').fwmobilesearch('search');
                        break;
                }
            }
        };
        $pending.applyallqtyitems = function() {
            var request;
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Confirm',  RwLanguages.translate('Apply All Qty Items') + '?');
                var $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
                FwConfirmation.addButton($confirmation, 'Cancel', true);
                $btnok.on('click', function() {
                    try {
                        request = {
                            contractId: screen.getContractId()
                        };
                        RwServices.callMethod("CheckIn", "CheckInAllQtyItems", request, function(response) {
                            $pending.find('#pendingsearch').fwmobilesearch('search');
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        };

        $rfid.showscreen = function() {
            $sessionin.hide();
            $pending.hide();
            $barcodescanwindow.hide();
            $checkincontrol.fwmobilemodulecontrol('showButton', '#rfidexceptions');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#selectorderlocation');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#applyallqtyitems');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#extraitems');
            $rfid.show();
            screen.properties.currentview = 'RFID';
        };
        $rfid.loadexceptions = function() {
            var $item, request;
            $rfid.data('batchid', '');
            $rfid.find('#rfidfilter-exceptions').click();

            request = {
                sessionid: screen.getContractId(),
                portal:    device.uuid
            };
            RwServices.callMethod("CheckIn", "LoadRFIDExceptions", request, function(response) {
                $rfid.loaditems(response.tags);
            });
        };
        $rfid.scanrfid = function (epcs) {
            var request;
            if (screen.properties.currentview !== 'RFID') {
                $checkinmodeselector.find('#rfidtab').click();
            }
            $rfid.data('batchid', '');
            $rfid.find('#rfidfilter-all').click();

            request = {
                sessionid: screen.getContractId(),
                portal:    device.uuid,
                tags:      epcs,
                aisle:     screen._aisleshelfdata()?.Aisle || '',
                shelf:     screen._aisleshelfdata()?.Shelf || ''
            };
            RwServices.callMethod("CheckIn", "RFIDScan", request, function(response) {
                $rfid.data('batchid', response.batchid);
                $rfid.loaditems(response.tags);
                screen.toggleReconcileButton(response.enablereconcile);
            });
        };
        $rfid.rfiditem = function(itemtype) {
            var html;
            html = [];
            html.push('<div class="rfid-item ' + itemtype + '">');
                html.push('<div class="rfid-item-title"></div>');
                html.push('<div class="rfid-item-info">');
                    if (itemtype === 'processed' || itemtype === 'exception') {
                        html.push('<div class="rfid-data rfid">');
                            html.push('<div class="item-caption">RFID:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                        html.push('<div class="rfid-data barcode">');
                            html.push('<div class="item-caption">Barcode:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                        html.push('<div class="rfid-data serial">');
                            html.push('<div class="item-caption">Serial No:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                    }
                    if (itemtype === 'exception') {
                        html.push('<div class="rfid-data message">');
                            html.push('<div class="item-caption">Message:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                    }
                html.push('</div>');
            html.push('</div>');
            return jQuery(html.join(''));
        };
        $rfid.loaditems = function(tags) {
            var $item;
            $rfid.find('.rfid-items').empty();

            for (var i = 0; i < tags.length; i++) {
                $item = null;
                if (tags[i].status === 'PROCESSED') {
                    $item = $rfid.rfiditem('processed');
                    $item.find('.rfid-item-title').html(tags[i].title);
                    $item.find('.rfid-data.rfid .item-value').html(tags[i].rfid);
                    $item.find('.rfid-data.barcode .item-value').html((tags[i].barcode !== '' ) ? tags[i].barcode : '-');
                    $item.find('.rfid-data.serial .item-value').html((tags[i].serialno !== '') ? tags[i].serialno : '-');
                    if (tags[i].duplicatescan === 'T') {
                        $item.addClass('duplicate');
                    }
                } else if (tags[i].status === 'EXCEPTION') {
                    $item = $rfid.rfiditem('exception');
                    $item.find('.rfid-item-title').html(tags[i].title);
                    $item.find('.rfid-data.rfid .item-value').html(tags[i].rfid);
                    $item.find('.rfid-data.barcode .item-value').html((tags[i].barcode !== '' ) ? tags[i].barcode : '-');
                    $item.find('.rfid-data.serial .item-value').html((tags[i].serialno !== '') ? tags[i].serialno : '-');
                    $item.find('.rfid-data.message .item-value').html(tags[i].message);
                    $item.attr('data-exceptiontype', tags[i].exceptiontype);
                }
                if ($item !== null) {
                    $item.data('recorddata', tags[i]);
                    $rfid.find('.rfid-items').append($item);
                }
            }

            switch ($rfid.data('filterview')) {
                case 'exceptions':
                    $rfid.find('#rfidfilter-exceptions').click();
                    break;
                case 'processed':
                    $rfid.find('#rfidfilter-processed').click();
                    break;
                case 'all':
                    $rfid.find('#rfidfilter-all').click();
                    break;
            }
        };
        $rfid
            .on('click', '.rfidfilter .option', function() {
                var $this;
                $this = jQuery(this);
                $this.siblings().removeClass('active');
                $this.addClass('active');
            })
            .on('click', '#rfidfilter-all', function() {
                $rfid.data('filterview', 'all');
                $rfid.find('.rfid-item').show();
            })
            .on('click', '#rfidfilter-processed', function() {
                $rfid.data('filterview', 'processed');
                $rfid.find('.rfid-item.processed').show();
                $rfid.find('.rfid-item.exception').hide();
            })
            .on('click', '#rfidfilter-exceptions', function() {
                $rfid.data('filterview', 'exceptions');
                $rfid.find('.rfid-item.exception').show();
                $rfid.find('.rfid-item.processed').hide();
            })
            .on('click', '.rfid-item.processed', function() {
                var $this, $confirmation, $cancel;
                $this         = jQuery(this);
                $confirmation = FwConfirmation.renderConfirmation('Options', '<div class="exceptionbuttons"></div>');
                $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

                $confirmation.find('.exceptionbuttons').append('<div class="tagfinder">Tag Finder</div>');
                $confirmation.find('.exceptionbuttons').append('<div class="clear">Clear item?</div>');

                $confirmation.on('click', '.tagfinder', () => {
                    try {
                        FwConfirmation.destroyConfirmation($confirmation);
                        const recorddata = $this.data('recorddata');
                        RwRFID.startTagFinder(recorddata.rfid);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                $confirmation.on('click', '.clear', function() {
                    var request;
                    request = {
                        recorddata:  $this.data('recorddata'),
                        contractid:  screen.getContractId(),
                        portal:      device.uuid,
                        moduletype:  properties.moduleType,
                        checkinmode: properties.checkInMode,
                        batchid:     $rfid.data('batchid')
                    };
                    RwServices.callMethod("CheckIn", "RFIDCancelItem", request, function(response) {
                        $rfid.loaditems(response.tags);
                    });
                    FwConfirmation.destroyConfirmation($confirmation);
                });
            })
            .on('click', '.rfid-item.exception', function() {
                var $this, $confirmation, $cancel;
                $this         = jQuery(this);
                $confirmation = FwConfirmation.renderConfirmation('Exception', '<div class="exceptionbuttons"></div>');
                $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);
                switch ($this.attr('data-exceptiontype')) {
                    //case '1005': //Item count exceeds quantity ordered - Item   - 2017/02/24 MY: removed due to Emil
                    case '1015':
                    case '1019':   //Package Truck
                        $confirmation.find('.exceptionbuttons').append('<div class="addordertosession">Add Order To Check-In Session</div>');
                        break;
                    case '104': //Item is Staged on Order
                    case '301': //I-Code / Bar Code not found in Inventory.
                        break;
                    case '1007': //item is on new order - no swap available
                        $confirmation.find('.exceptionbuttons').append('<div class="addordertosession">Add Order To Check-In Session</div>');
                        break;
                    case '1005': //item is on new order - swap available
                        $confirmation.find('.exceptionbuttons').append('<div class="addordertosession">Add Order To Check-In Session</div>');
                        $confirmation.find('.exceptionbuttons').append('<div class="swap">Swap</div>');
                        break;
                }
                $confirmation.find('.exceptionbuttons').append('<div class="tagfinder">Tag Finder</div>');
                $confirmation.find('.exceptionbuttons').append('<div class="clear">Clear Item</div>');

                $confirmation.find('.exceptionbuttons')
                    .on('click', '.tagfinder', () => {
                        try {
                            FwConfirmation.destroyConfirmation($confirmation);
                            const recorddata = $this.data('recorddata');
                            RwRFID.startTagFinder(recorddata.rfid);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    })
                    .on('click', '.addordertosession, .clear, .swap', function() {
                        var request: any = {};
                        if (jQuery(this).hasClass('addordertosession')) {
                            request.method = 'AddOrderToSession';
                        } else if (jQuery(this).hasClass('clear')) {
                            request.method = 'Clear';
                        } else if (jQuery(this).hasClass('swap')) {
                            request.method = 'Swap';
                        }
                        request.sessionid    = screen.getContractId();
                        request.rfid         = $this.find('.rfid-data.rfid .item-value').html();
                        request.portal       = device.uuid,
                        request.moduletype   = properties.moduleType;
                        request.checkinmode  = properties.checkInMode;
                        request.batchid      = $rfid.data('batchid');
                        RwServices.callMethod("CheckIn", "ProcessRFIDException", request, function(response) {
                            $rfid.loaditems(response.tags);
                        });
                        FwConfirmation.destroyConfirmation($confirmation);
                    })
                ;
            })
        ;

        $sessionin.find('#sessioninsearch').fwmobilesearch({
            service: 'CheckIn',
            method: 'SessionInSearch',
            searchModes: [
                {
                    caption: 'Scan (no refresh)',
                    placeholder: 'Scan Bar Code / I-Code',
                    value: 'code',
                    hasVirtualNumpad: true,
                    //hasPager: false,
                    //hasSearchResults: false,
                    search: function (value, plugin) {
                        if (value.length > 0) {
                            screen.scanCode(value);
                            plugin.clearsearchbox();
                        } else {
                            return true; // do a normal search
                        }
                    },
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                },
                {
                    caption: 'Search by Description', placeholder: 'Description', value: 'description',
                    //search: function (description) {
                    //    if (description.length > 0) {
                    //        screen.$search.fwmobilesearch('search');
                    //    }
                    //},
                    click: function (plugin) {
                        plugin.clearsearchbox();
                        //plugin.search();
                    }
                }
            ],
            getRequest: function() {
                var request = {
                    contractid: screen.getContractId()
                };
                return request;
            },
            cacheItemTemplate: false,
            itemTemplate: function(model) {
                var html: string | string[] = [];
                const isheader    = ((model.itemclass === 'N') || (model.sessionin === 0))
                const attrInRepair = model.inrepair === 'T' ? ' data-inrepair="T">' : ''
                html.push(`<div data-itemclass="${model.itemclass}"${attrInRepair}>`);
                html.push('  <div class="row1"><div class="title">{{description}}</div></div>');
                if (!isheader) {
                    html.push('  <div class="row2">');
                    html.push('    <div class="col1">');
                    html.push('      <div class="datafield masterno">');
                    html.push('        <div class="caption">' + RwLanguages.translate('I-Code') + ':</div>');
                    html.push('        <div class="value">{{masterno}}</div>');
                    html.push('      </div>');
                    if (model.barcode !== '') {
                        var barcodecaption = (model.trackedby === 'SERIALNO') ? RwLanguages.translate('Serial No') : RwLanguages.translate('Barcode');
                        html.push('      <div class="datafield barcode">');
                        html.push('        <div class="caption">' + barcodecaption + ':</div>');
                        html.push('        <div class="value">{{barcode}}</div>');
                        html.push('      </div>');
                    }
                    if (model.orderid !== '' && model.orderid !== 'xxxxxxxx') {
                        html.push('      <div class="datafield orderno">');
                        html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                        html.push('        <div class="value">{{orderno}}</div>');
                        html.push('      </div>');
                    }
                    html.push('    </div>');
                    html.push('    <div class="col2">');
                    html.push('      <div class="datafield sessionin">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Session In') + ':</div>');
                    html.push('        <div class="value">{{sessionin}}</div>');
                    html.push('      </div>');
                    if (model.orderid !== '' && model.orderid !== 'xxxxxxxx') {
                        html.push('      <div class="datafield qtyordered">');
                        html.push('        <div class="caption">' + RwLanguages.translate('Ordered') + ':</div>');
                        html.push('        <div class="value">{{qtyordered}}</div>');
                        html.push('      </div>');
                    }
                    html.push('    </div>');
                    html.push('  </div>');
                    if (model.vendorid !== '') {
                        html.push('  <div class="row3">');
                        html.push('    <div class="datafield vendor">');
                        html.push('      <div class="caption">' + RwLanguages.translate('Vendor/Consignor') + ':</div>');
                        html.push('      <div class="value">{{vendor}}</div>');
                        html.push('    </div>');
                        html.push('  </div>');
                    }
                    if (model.inrepair === 'T') {
                        html.push('  <div class="row3">');
                        html.push('    <div class="datafield status">');
                        html.push('      <div class="caption">' + RwLanguages.translate('Status') + ':</div>');
                        html.push('      <div class="value">Item In Repair</div>');
                        html.push('    </div>');
                        html.push('  </div>');
                    }
                }
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            hasRecordClick: (model: any): boolean => {
                const isheader = ((model.itemclass === 'N') || (model.sessionin === 0));
                return !isheader;
            },
            recordClick: function(recorddata, $record) {
                try {
                    if (recorddata.inrepair !== 'T') {
                        var $confirmation = FwConfirmation.renderConfirmation('', '<div class="exceptionbuttons"></div>');
                        var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);
                        $confirmation.find('.exceptionbuttons').append('<div class="cancelitem">Cancel Item</div>');
                        $confirmation.find('.exceptionbuttons').append('<div class="sendtorepair">Send To Repair</div>');

                        $confirmation
                            .on('click', '.cancelitem', function() {
                                var request;
                                try {
                                    request = {
                                        contractid:   screen.getContractId(),
                                        masteritemid: recorddata.masteritemid,
                                        masterid:     recorddata.masterid,
                                        vendorid:     recorddata.vendorid,
                                        consignorid:  recorddata.consignorid,
                                        description:  recorddata.description,
                                        ordertranid:  recorddata.ordertranid,
                                        internalchar: recorddata.internalchar,
                                        qty:          recorddata.sessionin,
                                        trackedby:    recorddata.trackedby,
                                        aisle:        screen._aisleshelfdata()?.Aisle || '',
                                        shelf:        screen._aisleshelfdata()?.Shelf || '',
                                        orderid:      recorddata.orderid
                                    };
                                    RwServices.callMethod("CheckIn", "CheckInItemCancel", request, function(response) {
                                        $sessionin.find('#sessioninsearch').fwmobilesearch('search');
                                    });
                                    FwConfirmation.destroyConfirmation($confirmation);
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            })
                            .on('click', '.sendtorepair', function() {
                                FwConfirmation.destroyConfirmation($confirmation);
                                var request = {
                                    contractid:   screen.getContractId(),
                                    orderid:      recorddata.orderid,
                                    masteritemid: recorddata.masteritemid,
                                    rentalitemid: recorddata.rentalitemid,
                                    qty:          recorddata.sessionin
                                };
                                try {
                                    if (recorddata.trackedby === 'QUANTITY') {
                                        var $confirmationstr = FwConfirmation.showMessage('How many?', '<input class="qty" type="number" style="font-size:16px;padding:5px;border:1pxc solid #bdbdbd;box-sizing:border-box;width:100%;" value="' + request.qty + '" />', true, false, 'OK', function() {
                                            try {
                                                var userqty = $confirmationstr.find('input.qty').val();
                                                if (userqty === '') {
                                                    throw 'Qty is required.';
                                                }
                                                if (isNaN(userqty)) {
                                                    throw 'Please enter a valid qty.';
                                                }
                                                userqty = parseFloat(userqty);
                                                if (userqty > request.qty) {
                                                    throw 'Qty cannot exceed ' + request.qty.toString() + '.';
                                                }
                                                if (userqty <= 0) {
                                                    throw 'Qty must be > 0.';
                                                }
                                                request.qty = userqty;
                                                RwServices.callMethod("CheckIn", "CheckInItemSendToRepair", request, function(response) {
                                                    try {
                                                        FwConfirmation.destroyConfirmation($confirmationstr);
                                                        var repairOrderScreen = RwInventoryController.getRepairOrderScreen({}, {mode:'sendtorepair', repairno:response.repairno});
                                                        program.pushScreen(repairOrderScreen);
                                                    } catch(ex) {
                                                        FwFunc.showError(ex);
                                                    }
                                                });
                                            } catch(ex) {
                                                FwFunc.showError(ex);
                                            }
                                        });
                                    } else {
                                        RwServices.callMethod("CheckIn", "CheckInItemSendToRepair", request, function(response) {
                                            try {
                                                var repairOrderScreen = RwInventoryController.getRepairOrderScreen({}, {mode:'sendtorepair', repairno:response.repairno, qty:1});
                                                program.pushScreen(repairOrderScreen);
                                            } catch(ex) {
                                                FwFunc.showError(ex);
                                            }
                                        });
                                    }
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            })
                        ;
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            afterLoad: function(plugin, response) {
                if (response.extraitems > 0) {
                    $checkincontrol.fwmobilemodulecontrol('showButton', '#extraitems');
                } else {
                    $checkincontrol.fwmobilemodulecontrol('hideButton', '#extraitems');
                }

                //const $recordcount = plugin.$element.find('.searchfooter .recordcount');
                //$recordcount.html($recordcount.text().replace('items', 'lines') + ' / ' + response.totalin + ' items');
            }
        });
        $sessionin.showscreen = function() {
            var request;
            $pending.hide();
            $rfid.hide();
            $sessionin.show();
            $barcodescanwindow.show();
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#rfidexceptions');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#selectorderlocation');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#applyallqtyitems');
            $checkincontrol.fwmobilemodulecontrol('hideButton', '#extraitems');
            screen.properties.currentview = 'SESSIONIN';
            if (screen.getContractId() !== '') {
                const searchOption = $sessionin.find('#sessioninsearch').fwmobilesearch('getSearchOption');
                switch (searchOption) {
                    case 'code':
                        $sessionin.find('#sessioninsearch').fwmobilesearch('clearsearchbox');
                        $sessionin.find('#sessioninsearch').fwmobilesearch('clearsearchresults');
                        break;
                    case 'decription':
                        $sessionin.find('#sessioninsearch').fwmobilesearch('search');
                        break;
                }
            }
        };

        $orderpriority.find('#orderprioritycontroller').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Cancel',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        $orderpriority.close();
                    }
                },
                {
                    caption:     'Reconcile',
                    orientation: 'right',
                    icon:        '',
                    state:       0,
                    buttonclick: function() {
                        var request = {
                            contractid: screen.getContractId()
                        };
                        RwServices.callMethod('CheckIn', 'Reconcile', request, function () {
                            $orderpriority.close();
                        });
                    }
                }
            ]
        });
        $orderpriority.showscreen = function() {
            var request;
            $pending.hide();
            $rfid.hide();
            $sessionin.hide();
            $orderpriority.show();
            RwRFID.unregisterEvents();
            $barcodescanwindow.hide();
            $checkinmodeselector.hide();
            $checkincontrol.hide();

            request = {
                contractid: screen.getContractId()
            };
            RwServices.callMethod("CheckIn", "LoadOrderPriority", request, function(response) {
                $orderpriority.loadorders(response.orders);
            });
        };
        $orderpriority.close = function() {
            switch (screen.properties.currentview) {
                case 'SESSIONIN':
                    $sessionin.showscreen();
                    break;
                case 'RFID':
                    $rfid.showscreen();
                    break;
                case 'PENDING':
                    $pending.showscreen();
                    break;
            }

            $orderpriority.hide();
            RwRFID.registerEvents(screen.rfidscan);
            $checkinmodeselector.show();
            $checkincontrol.show();
        };
        $orderpriority.loadorders = function(orders) {
            var html = [];
            $orderpriority.find('#orderpriority').empty();

            html.push('<div class="order">');
            html.push('  <div class="ordertitle"></div>');
            html.push('  <div class="orderbody">');
            html.push('    <div class="orderdetails">');
            html.push('      <div class="orderno"><div class="caption">Order No:</div><div class="value"></div></div>');
            html.push('      <div class="orderdate"><div class="caption">Order Date:</div><div class="value"></div></div>');
            html.push('    </div>');
            html.push('    <div class="ordercontrols">');
            html.push('      <div class="control priority" data-priority="add"><i class="material-icons">&#xE313;</i></div>'); //keyboard_arrow_down
            html.push('      <div class="control priority" data-priority="subtract"><i class="material-icons">&#xE316;</i></div>'); //keyboard_arrow_up
            html.push('      <div class="control clear"></div>');
            html.push('    </div>');
            html.push('  </div>');
            html.push('</div>');

            for (var i = 0; i < orders.length; i++) {
                var $order = jQuery(html.join(''));

                $order.find('.ordertitle').html(orders[i].orderdesc);
                $order.find('.orderno .value').html(orders[i].orderno);
                $order.find('.orderdate .value').html(orders[i].orderdate);
                $order.data('recorddata', orders[i]);
                $order.attr('data-checkininclude', orders[i].chkininclude);
                $order.attr('data-index', i+1);

                $orderpriority.find('#orderpriority').append($order);
            }
        };
        $orderpriority
            .on('click', '.order .clear', function() {
                var $order, request;
                $order = jQuery(this).closest('.order');
                request = {
                    recorddata: $order.data('recorddata'),
                    contractid: screen.getContractId()
                };
                RwServices.callMethod("CheckIn", "ToggleOrderPriority", request, function(response) {
                    $orderpriority.loadorders(response.orders);
                    if (response.status !== 0) {
                        FwFunc.showError(response.message);
                    }
                });
            })
            .on('click', '.order .priority', function() {
                var $order, request;
                $order = jQuery(this).closest('.order');
                request = {
                    recorddata: $order.data('recorddata'),
                    contractid: screen.getContractId(),
                    priority:   jQuery(this).attr('data-priority'),
                    index:      $order.attr('data-index')
                };
                RwServices.callMethod("CheckIn", "UpdateOrderPriority", request, function(response) {
                    $orderpriority.loadorders(response.orders);
                });
            })
        ;

        $checkinserial.find('#serialcontroller').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        $checkinserial.close();
                    }
                },
                {
                    id:          'setmeters',
                    caption:     'Set Meters',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //chevron_right
                    state:       0,
                    buttonclick: function() {
                        $checkinserial.find('.serial-details').hide();
                        $checkinserial.find('.serialitems').hide();
                        $checkinserial.find('.serialitem-meters').show();
                        $checkinserial.loadSerialMeters();
                        $checkinserial.find('#serialcontroller').fwmobilemodulecontrol('nextState');
                    }
                },
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       1,
                    buttonclick: function () {
                        $checkinserial.find('.serialitems').show();
                        $checkinserial.find('.serial-details').show();
                        $checkinserial.find('.serialitem-meters').hide();
                        $checkinserial.loadSerialItems();
                        $checkinserial.find('#serialcontroller').fwmobilemodulecontrol('previousState');
                    }
                },
                {
                    caption:     'Finish',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //chevron_right
                    state:       1,
                    buttonclick: function () {
                        $checkinserial.close();
                    }
                }
            ]
        });
        $checkinserial.showscreen = function(recorddata) {
            var request = {
                orderid:      recorddata.orderid,
                masteritemid: recorddata.masteritemid,
                masterid:     recorddata.masterid,
                contractid:   screen.getContractId()
            };
            $checkinserial.data('recorddata', request);
            $checkinserial.find('#serial').empty();
            RwServices.callMethod('CheckIn', 'GetSerialInfo', request, function (response) {
                var html = [];
                $checkinserial.show();
                RwRFID.unregisterEvents();
                $barcodescanwindow.hide();
                $checkinmodeselector.hide();
                $checkincontrol.hide();

                if (response.serial.metered === 'T') {
                    $checkinserial.find('#serialcontroller').fwmobilemodulecontrol('showButton', '#setmeters');
                } else {
                    $checkinserial.find('#serialcontroller').fwmobilemodulecontrol('hideButton', '#setmeters');
                }

                html.push('<div class="serial-title">');
                html.push('  <div class="serial-title-object">I-Code: ' + response.serial.masterno + '</div>');
                html.push('  <div class="serial-title-object">' + response.serial.description + '</div>');
                html.push('</div>');
                html.push('<div class="serial-details">');
                html.push('  <div class="serial-details-row">');
                html.push('    <div class="serial-details-row-caption">Remaining:</div>');
                html.push('    <div class="serial-details-row-value qtyremaining">' + response.serial.qtyout + '</div>');
                html.push('    <div class="serial-details-row-caption">Out:</div>');
                html.push('    <div class="serial-details-row-value">' + response.serialitems.length + '</div>');
                html.push('  </div>');
                html.push('  <div class="serial-details-row">');
                html.push('    <div class="serial-details-row-caption">Ordered:</div>');
                html.push('    <div class="serial-details-row-value">' + response.serial.qtyordered + '</div>');
                html.push('    <div class="serial-details-row-caption">In:</div>');
                html.push('    <div class="serial-details-row-value qtyin">' + response.serial.qtyin + '</div>');
                html.push('  </div>');
                html.push('</div>');
                html.push('<div class="serialitems"></div>');
                html.push('<div class="serialitem-meters"></div>');
                $checkinserial.find('#serial').append(jQuery(html.join('')));

                $checkinserial.loadSerialItems();
            });
        };
        $checkinserial.close = function() {
            switch (screen.properties.currentview) {
                case 'SESSIONIN':
                    $sessionin.showscreen();
                    break;
                case 'RFID':
                    $rfid.showscreen();
                    break;
                case 'PENDING':
                    $pending.showscreen();
                    break;
            }

            $checkinserial.data('recorddata', '');
            $checkinserial.find('#serialcontroller').fwmobilemodulecontrol('changeState', 0);
            $checkinserial.hide();
            RwRFID.registerEvents(screen.rfidscan);
            $checkinmodeselector.show();
            $checkincontrol.show();
        };
        $checkinserial.loadSerialItems = function() {
            $checkinserial.find('.serialitems').empty();
            RwServices.callMethod('CheckIn', 'GetSerialItems', $checkinserial.data('recorddata'), function (response) {
                for (var i = 0; i < response.serialitems.length; i++) {
                    var html = [], $serialitem;
                    html.push('<div class="serialitem standard' + ((response.serialitems[i].itemstatus === 'O') ? '' : ' in') + '">');
                    html.push('  <div class="serialitem-caption">Serial No:</div>');
                    html.push('  <div class="serialitem-value">' + response.serialitems[i].mfgserial + '</div>');
                    html.push('</div>');
                    $serialitem = jQuery(html.join(''));
                    $serialitem.data('recorddata', response.serialitems[i]);
                    $checkinserial.find('.serialitems').append($serialitem);
                }
            });
        };
        $checkinserial.loadSerialMeters = function() {
            $checkinserial.find('.serialitem-meters').empty();
            RwServices.callMethod('CheckIn', 'GetSerialItems', $checkinserial.data('recorddata'), function (response) {
                var zerosetflg = true;
                for (var i = 0; i < response.serialitems.length; i++) {
                    var html = [], $serialitem, meterinvalue, notsetflg;
                    if (response.serialitems[i].itemstatus === 'I') {
                        zerosetflg = false;
                        if (response.serialitems[i].meterin === '0.00') {
                            meterinvalue = response.serialitems[i].meterout;
                            notsetflg    = false;
                        } else {
                            meterinvalue = response.serialitems[i].meterin;
                            notsetflg    = true;
                        }
                        html.push('<div class="serialitem metered' + (notsetflg ? ' valueset' : '')  + '">');
                        html.push('  <div class="serialitem-row">');
                        html.push('    <div class="serialitem-caption">Serial No:</div>');
                        html.push('    <div class="serialitem-value">' + response.serialitems[i].mfgserial + '</div>');
                        html.push('    <div class="serialitem-metericon"><i class="material-icons">&#xE5CF;</i></div>'); //expand_more
                        html.push('  </div>');
                        html.push('  <div class="serialitem-meter-dropdown">');
                        html.push('    <div class="serialitem-meter-dropdown-row meteredout">');
                        html.push('      <div class="serialitem-meter-dropdown-caption">Out Value: </div>');
                        html.push('      <div class="serialitem-meter-dropdown-value meteroutvalue">' + numberWithCommas(parseFloat(response.serialitems[i].meterout).toFixed(2)) + '</div>');
                        html.push('    </div>');
                        html.push('    <div class="serialitem-meter-dropdown-row">');
                        html.push('      <div class="serialitem-meter-dropdown-caption">In Value: </div>');
                        html.push('      <div class="serialitem-meter-dropdown-value meterinvalue' + (notsetflg ? ' valueset' : '')  + '">');
                        html.push(         numberWithCommas(parseFloat(meterinvalue).toFixed(2)));
                        html.push('      </div>');
                        html.push('    </div>');
                        html.push('  </div>');
                        html.push('</div>');
                        $serialitem = jQuery(html.join(''));
                        $serialitem.data('recorddata', response.serialitems[i]);
                        $checkinserial.find('.serialitem-meters').append($serialitem);
                    }
                }
                if (zerosetflg) {
                    html.push('<div class="zeroitems">0 items sessioned in</div>');
                    $checkinserial.find('.serialitem-meters').append(jQuery(html.join('')));
                }
            });
        };
        $checkinserial
            .on('click', '.serialitem.standard', function() {
                var request, $this;
                $this = jQuery(this);
                request = {
                    orderid:      jQuery(this).data('recorddata').orderid,
                    masteritemid: jQuery(this).data('recorddata').masteritemid,
                    masterid:     jQuery(this).data('recorddata').masterid,
                    rentalitemid: jQuery(this).data('recorddata').rentalitemid,
                    contractid:   screen.getContractId(),
                    meter:        '0',
                    toggledelete: 'T'
                };
                RwServices.callMethod('CheckIn', 'SerialSessionIn', request, function (response) {
                    $checkinserial.find('.qtyremaining').html(response.serial.qtyout);
                    $checkinserial.find('.qtyin').html(response.serial.qtyin);
                    $this.toggleClass('in');
                });
            })
            .on('click', '.serialitem-row', function() {
                var $serialitem;
                $serialitem = jQuery(this).parent();
                var visible = $serialitem.find('.serialitem-meter-dropdown').is(':visible');
                $checkinserial.find('.serialitem-meter-dropdown').hide();
                $checkinserial.find('.serialitem-metericon .material-icons').html('&#xE5CF;'); //expand_more
                if (!visible) {
                    $serialitem.find('.serialitem-meter-dropdown').show();
                    $serialitem.find('.serialitem-metericon .material-icons').html('&#xE5CE;'); //expand_less
                }
            })
            .on('click', '.meterinvalue', function() {
                var $confirmation, $ok, $cancel, html = [], $this;
                $this         = jQuery(this);
                $confirmation = FwConfirmation.renderConfirmation('Enter Meter In Value', '');
                $ok           = FwConfirmation.addButton($confirmation, 'Ok', false);
                $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

                html.push('<div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="In Value" data-datafield="meterinvalue" data-minvalue="' + $this.closest('.metered').data('recorddata').meterout + '" data-formatnumeric="true"></div>');

                FwConfirmation.addControls($confirmation, html.join(''));

                FwFormField.setValue($confirmation, 'div[data-datafield="meterinvalue"]', parseFloat($this.html()));

                $ok.on('click', function() {
                    const request = {
                        orderid:      $this.closest('.metered').data('recorddata').orderid,
                        masteritemid: $this.closest('.metered').data('recorddata').masteritemid,
                        masterid:     $this.closest('.metered').data('recorddata').masterid,
                        rentalitemid: $this.closest('.metered').data('recorddata').rentalitemid,
                        contractid:   screen.getContractId(),
                        meter:        FwFormField.getValue($confirmation, 'div[data-datafield="meterinvalue"]'),
                        toggledelete: 'F'
                    };
                    RwServices.callMethod('CheckIn', 'SerialSessionIn', request, function (response) {
                        FwConfirmation.destroyConfirmation($confirmation);
                        $this.addClass('valueset').html(numberWithCommas(parseFloat(request.meter).toFixed(2)));
                        $this.closest('.metered').addClass('valueset');
                    });
                });
            })
        ;

        $extraitems.find('#extraitemscontroller').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        switch (screen.properties.currentview) {
                            case 'SESSIONIN':
                                $sessionin.showscreen();
                                break;
                            case 'RFID':
                                $rfid.showscreen();
                                break;
                            case 'PENDING':
                                $pending.showscreen();
                                break;
                        }

                        $extraitems.hide();
                        RwRFID.registerEvents(screen.rfidscan);
                        $checkinmodeselector.show();
                        $checkincontrol.show();
                    }
                }
            ]
        });
        $extraitems.find('#extraitemssearch').fwmobilesearch({
            service: 'CheckIn',
            method:  'ExtraItemsSearch',
            getRequest: function() {
                var request = {
                    contractid: screen.getContractId()
                };
                return request;
            },
            cacheItemTemplate: false,
            itemTemplate: function(model) {
                var html: string | string[] = [];
                html.push('<div class="item link">');
                html.push('  <div class="row1"><div class="title">{{description}}</div></div>');
                html.push('  <div class="row2">');
                html.push('    <div class="col1">');
                html.push('      <div class="datafield masterno">');
                html.push('        <div class="caption">' + RwLanguages.translate('I-Code') + ':</div>');
                html.push('        <div class="value">{{masterno}}</div>');
                html.push('      </div>');
                html.push('      <div class="datafield inorderno">');
                html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('        <div class="value">{{inorderno}}</div>');
                html.push('      </div>');
                html.push('    </div>');
                if (model.inbarcode !== '') {
                    html.push('    <div class="col2">');
                    html.push('      <div class="datafield inbarcode">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Barcode') + ':</div>');
                    html.push('        <div class="value">{{inbarcode}}</div>');
                    html.push('      </div>');
                    html.push('    </div>');
                }
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            recordClick: function(recorddata, $record) {
                try {
                    var request, $confirmation, $ok, $cancel;
                    $confirmation = FwConfirmation.renderConfirmation('Confirm', 'Remove Item?');
                    $ok           = FwConfirmation.addButton($confirmation, 'Yes', true);
                    $cancel       = FwConfirmation.addButton($confirmation, 'No',  true);

                    $ok.on('click', function() {
                        request = {
                            contractid: screen.getContractId(),
                            recorddata: recorddata
                        };
                        RwServices.callMethod("CheckIn", "RemoveExtraItem", request, function(response) {
                            $extraitems.find('#extraitemssearch').fwmobilesearch('search');
                        });
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $extraitems.showscreen = function() {
            $extraitems.show();
            RwRFID.unregisterEvents();
            $barcodescanwindow.hide();
            $checkinmodeselector.hide();
            $checkincontrol.hide();
            $extraitems.find('#extraitemssearch').fwmobilesearch('search');
        };

        screen.toggleRfid = function () {
            if (RwRFID.isConnected) {
                $checkinmodeselector.fwmobilemoduletabs('showTab', '#rfidtab');
                $checkincontrol.fwmobilemodulecontrol('showButton', '#startrfid')
                               .fwmobilemodulecontrol('showButton', '#stoprfid');
                RwRFID.registerEvents(screen.rfidscan);

                var request = {
                    sessionid: screen.getContractId()
                };
                RwServices.callMethod("CheckIn", "RFIDClearSession", request, function(response) {});
            } else {
                $checkinmodeselector.fwmobilemoduletabs('hideTab', '#rfidtab');
                $checkincontrol.fwmobilemodulecontrol('hideButton', '#startrfid')
                               .fwmobilemodulecontrol('hideButton', '#stoprfid');
                RwRFID.unregisterEvents();
            }
        };

        $orderlocation.find('#orderlocationcontroller').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        $orderlocation.hide();
                        $primarywindow.show();
                    }
                },
                {
                    caption:     'Select Location',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //chevron_right
                    state:       0,
                    buttonclick: function () {
                        var locationdata = $orderlocation.find('.location.selected').data('recorddata');
                        if (locationdata != null) {
                            screen._locationdata(locationdata);
                            screen.displayOrderLocation(locationdata.location);
                            $orderlocation.hide();
                            $primarywindow.show();
                        } else {
                            FwNotification.renderNotification('ERROR', 'Select a location.')
                        }
                    }
                }
            ]
        });
        $orderlocation.showscreen = function () {
            $orderlocation.show();
            $orderlocation.searchlocation('');
        };
        $orderlocation.searchlocation = function (searchvalue) {
            $orderlocation.find('.orderlocations').empty();
            var request = {
                orderid:     screen.getOrderId(),
                searchvalue: searchvalue
            };
            RwServices.callMethod("CheckIn", "SearchLocations", request, function(response) {
                if (response.locations.length > 0) {
                    for (var item of response.locations) {
                        var html = [];
                        html.push('<div class="location">');
                        html.push(`  <div class="row1"><div class="title">${item.location}</div></div>`);
                        html.push('  <div class="row2">');
                        html.push('    <div class="col1">');
                        html.push('      <div class="datafield masterno">');
                        html.push('        <div class="caption">Building:</div>');
                        html.push(`        <div class="value">${item.building}</div>`);
                        html.push('      </div>');
                        html.push('    </div>');
                        html.push('    <div class="col2">');
                        html.push('      <div class="datafield rate">');
                        html.push('        <div class="caption">Floor:</div>');
                        html.push(`        <div class="value">${item.floor}</div>`);
                        html.push('      </div>');
                        html.push('    </div>');
                        html.push('  </div>');
                        html.push('  <div class="row3">');
                        html.push('    <div class="col1">');
                        html.push('      <div class="datafield masterno">');
                        html.push('        <div class="caption">Space:</div>');
                        html.push(`        <div class="value">${item.space}</div>`);
                        html.push('      </div>');
                        html.push('    </div>');
                        html.push('  </div>');
                        html.push('</div>');
                        var $item = jQuery(html.join(''));
                        $item.data('recorddata', item);

                        $orderlocation.find('.orderlocations').append($item);
                    }
                } else {
                    var $zeroitems = jQuery('<div class="zeroitems">0 Locations Found</div>');
                    $orderlocation.find('.orderlocations').append($zeroitems);
                }
            });
        };
        $orderlocation
            .on('change', '.fwmobilecontrol-value', function () {
                var value = jQuery(this).val();
                if (value != '') {
                    $orderlocation.searchlocation(value);
                }
            })
            .on('click', '.location', function () {
                var $this = jQuery(this);
                $this.siblings().removeClass('selected');
                $this.addClass('selected');
            })
        ;

        if ((screen.getContractId() === '') && (properties.checkInMode === RwConstants.checkInModes.SingleOrder)) { 
            RwServices.order.createInContract({
                orderId:      screen.getOrderId(),
                dealId:       screen.getDealId(),
                departmentId: screen.getDepartmentId()
            }, function(response) {
                properties.contractId = response.createInContract.contractId;
            });
        }

        screen.showPopupQty = function() {
            FwPopup.showPopup(screen.$popupQty);
        };
        screen.hidePopupQty = function() {
            FwPopup.destroyPopup(screen.$popupQty);
            jQuery('#checkIn-txtBarcodeData').val('');
        };

        screen.checkInItem = function(orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId, trackedby) {
            var requestCheckInItem = {
                moduleType:         properties.moduleType,
                checkInMode:        properties.checkInMode,
                contractId:         screen.getContractId(),
                orderId:            orderId,
                dealId:             screen.getDealId(),
                departmentId:       screen.getDepartmentId(),
                vendorId:           vendorId,
                consignorId:        '',
                description:        '',
                internalChar:       '',
                masterItemId:       masterItemId,
                masterId:           masterId,
                parentId:           '',
                code:               RwAppData.stripBarcode(code.toUpperCase()),
                qty:                qty,
                newOrderAction:     newOrderAction,
                aisle:              aisle,
                shelf:              shelf ,
                playStatus:         playStatus,
                trackedby:          trackedby,
                locationdata:       screen._locationdata()
                //exchangecontractid: screen.getExchangeContractId(),
                //getSuspenededSessions: true
            };
            //RwServices.callMethod("CheckIn", "CheckInItem", requestCheckInItem, function(responseCheckInItem) {
            //    screen.enableBarcodeField();
            //    properties.responseCheckInItem = responseCheckInItem;
            //    screen.toggleReconcileButton(responseCheckInItem.enablereconcile);
            //    screen.checkInItemCallback(responseCheckInItem);
            //});
            // trying to prevent duplicate scans
            FwAppData.jsonPost(true, 'services.ashx?path=/services/CheckIn/CheckInItem', requestCheckInItem, null,
                function success(responseCheckInItem) {
                    screen.enableBarcodeField();
                    properties.responseCheckInItem = responseCheckInItem;
                    screen.toggleReconcileButton(responseCheckInItem.enablereconcile);
                    screen.checkInItemCallback(responseCheckInItem);
                },
                function fail(response) {
                    screen.enableBarcodeField();
                    FwFunc.showError(response);
                },
                null);
        };

        screen.checkInItemCallback = function(responseCheckInItem) {
            try {
                if ((typeof properties.contract !== 'object') && (typeof responseCheckInItem.suspendedInContracts === 'object') && (responseCheckInItem.suspendedInContracts.Rows.length > 0)) {
                    const suspendedContractsPopup = CheckInController.getOrderSuspendedSessionPopup(responseCheckInItem.suspendedInContracts);
                    suspendedContractsPopup.$popup.on('click', '.checkin-suspendedsession', function() {
                        var requestSelectSession, $suspendedsession, sessionno;
                        try {
                            $suspendedsession = jQuery(this);
                            requestSelectSession = {};
                            requestSelectSession.sessionNo  = $suspendedsession.find('.checkin-suspendedsession-sessionno-value').text();
                            requestSelectSession.moduleType = properties.moduleType;
                            properties.checkInMode = 'Session';
                            RwServices.order.selectSession(requestSelectSession, function(responseSelectSession) {
                                var requestCheckInItem2;
                                try {
                                    properties.selectedsession = responseSelectSession.webSelectSession;
                                    if (((properties.moduleType === RwConstants.moduleTypes.Order) || (properties.moduleType === RwConstants.moduleTypes.Transfer)) && 
                                        (properties.activityType === RwConstants.activityTypes.CheckIn)) {
                                        const checkInItemScreen_viewModel: any = {};
                                        const checkInItemScreen_properties = jQuery.extend({}, properties, {
                                            selectedsession: properties.selectedsession
                                        });
                                        const checkInItemScreen = CheckInController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                        program.updateScreen(checkInItemScreen);
                                        requestCheckInItem2 = responseCheckInItem.request;
                                        requestCheckInItem2.contractId = responseSelectSession.webSelectSession.contractid;
                                        RwServices.callMethod("CheckIn", "CheckInItem", responseCheckInItem.request, function(responseCheckInItem2) {
                                            properties.responseCheckInItem = responseCheckInItem2;
                                            checkInItemScreen.toggleReconcileButton(responseCheckInItem2.enablereconcile);
                                            checkInItemScreen.checkInItemCallback(responseCheckInItem2);
                                        });
                                    } else {
                                        throw 'Not implemented for moduleType: ' + properties.moduleType;
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
                            requestCreateSession = {
                                orderid: '',
                                dealid: '',
                                departmentid: ''
                            };
                            if (typeof properties.webSelectOrder === 'object') {
                                requestCreateSession.orderid      = properties.webSelectOrder.orderId;
                                requestCreateSession.dealid       = properties.webSelectOrder.dealId;
                                requestCreateSession.departmentid = properties.webSelectOrder.departmentId;
                            }
                            else if (typeof responseCheckInItem.webGetItemStatus === 'object') {
                                requestCreateSession.orderid      = responseCheckInItem.webGetItemStatus.orderid;
                                requestCreateSession.dealid       = responseCheckInItem.webGetItemStatus.dealid;
                                requestCreateSession.departmentid = responseCheckInItem.webGetItemStatus.departmentid;
                            }
                            RwServices.order.createNewInContractAndSuspend(requestCreateSession, function(responseCreateSession) {
                                var requestSelectSession, $suspendedsession, sessionno;
                                try {
                                    $suspendedsession = jQuery(this);
                                    requestSelectSession = {};
                                    requestSelectSession.sessionNo  = responseCreateSession.contract.sessionNo;
                                    requestSelectSession.moduleType = properties.moduleType;
                                    properties.checkInMode = 'Session';
                                    RwServices.order.selectSession(requestSelectSession, function(responseSelectSession) {
                                        var requestCheckInItem2;
                                        try {
                                            properties.selectedsession = responseSelectSession.webSelectSession;
                                            if (((properties.moduleType === RwConstants.moduleTypes.Order) || (properties.moduleType === RwConstants.moduleTypes.Transfer)) && 
                                                (properties.activityType === RwConstants.activityTypes.CheckIn)) {
                                                const checkInItemScreen_viewModel = {};
                                                const checkInItemScreen_properties = jQuery.extend({}, properties, {
                                                    selectedsession: properties.selectedsession
                                                });
                                                const checkInItemScreen = CheckInController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                                program.updateScreen(checkInItemScreen);
                                                requestCheckInItem2 = responseCheckInItem.request;
                                                requestCheckInItem2.contractId = responseSelectSession.webSelectSession.contractid;
                                                RwServices.callMethod("CheckIn", "CheckInItem", responseCheckInItem.request, function(responseCheckInItem2) {
                                                    properties.responseCheckInItem = responseCheckInItem2;
                                                    checkInItemScreen.toggleReconcileButton(responseCheckInItem2.enablereconcile);
                                                    checkInItemScreen.checkInItemCallback(responseCheckInItem2);
                                                });
                                            } else {
                                                throw 'Not implemented for moduleType: ' + properties.moduleType;
                                            }
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    });
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } else {
                    screen.checkInItemCallback2(responseCheckInItem);
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        };

        screen.checkInItemCallback2 = function(responseCheckInItem) {
            var $liCheckInPending, valTxtQty, isScannedICode, suspendedContractsPopup;
            screen.renderPopupQty();
            if (responseCheckInItem.request.playStatus) {
                program.playStatus(responseCheckInItem.webCheckInItem.status === 0);
            }
            isScannedICode = (responseCheckInItem.webCheckInItem.isICode) && (responseCheckInItem.request.masterItemId.length === 0);
            //if (screen.$view.find('#checkIn-btnPendingList').hasClass('selected')) {
            //    screen.$view.find('#checkIn-btnPendingList').click();
            //}
            screen.$popupQty.find('#checkIn-popupQty-genericMsg').html(responseCheckInItem.webCheckInItem.genericMsg);
            screen.$popupQty.find('#checkIn-popupQty-msg').html(responseCheckInItem.webCheckInItem.msg);
            screen.$popupQty.find('#checkIn-popupQty-masterNo').html(responseCheckInItem.webCheckInItem.masterNo);
            screen.$popupQty.find('#checkIn-popupQty-code').html(responseCheckInItem.request.code).toggle(responseCheckInItem.request.code.length > 0);
            screen.$popupQty.find('#checkIn-popupQty-description').html(responseCheckInItem.webCheckInItem.description).show();
            valTxtQty = (isScannedICode) ? '' : String(responseCheckInItem.webCheckInItem.stillOut);
            screen.$popupQty.find('.fwformfield[data-datafield="qty"] .fwformfield-value').val(valTxtQty);
            screen.$popupQty.find('.fwformfield[data-datafield="qty"]').attr('data-minvalue', 1);
            if (isScannedICode) {
                screen.$popupQty.find('.fwformfield[data-datafield="qty"]').removeAttr('data-maxvalue');
            } else {
                screen.$popupQty.find('.fwformfield[data-datafield="qty"]').attr('data-maxvalue', valTxtQty);
                screen.$popupQty.find('.row2,.row3,.row4').show();
                screen.$popupQty.find('#checkIn-popupQty-qtyOrdered').html(String(responseCheckInItem.webCheckInItem.qtyOrdered));
                screen.$popupQty.find('#checkIn-popupQty-sessionIn').html(String(responseCheckInItem.webCheckInItem.sessionIn));
                screen.$popupQty.find('#checkIn-popupQty-subQty').html(String(responseCheckInItem.webCheckInItem.subQty));
                screen.$popupQty.find('#checkIn-popupQty-stillOut').html(String(responseCheckInItem.webCheckInItem.stillOut));
                screen.$popupQty.find('#checkIn-popupQty-totalIn').html(String(responseCheckInItem.webCheckInItem.totalIn));
            }
        
            screen.$popupQty.find('#checkIn-popupQty-summary-table-trVendor').toggle(responseCheckInItem.webCheckInItem.vendorId.length > 0);
            if (responseCheckInItem.webCheckInItem.vendorId.length > 0) {
                screen.$popupQty.find('#checkIn-popupQty-vendor').html(responseCheckInItem.webCheckInItem.vendor);
            }
            if (properties.contractId === '') {
                properties.contractId = responseCheckInItem.webCheckInItem.contractId;
                //jQuery('#checkIn-pageSelectorInner').show();
            }
            //if (properties.exchangeContractId === '') {
            //    properties.exchangeContractId = responseCheckInItem.webCheckInItem.exchangecontractid;
            //}
            if (properties.dealId === '') {
                properties.dealId = responseCheckInItem.webCheckInItem.dealId;
            }
            if (properties.departmentId === '') {
                properties.departmentId = responseCheckInItem.webCheckInItem.departmentId;
            }
            if (((typeof responseCheckInItem.webCheckInItem.sessionNo === 'string') && (responseCheckInItem.webCheckInItem.sessionNo.length > 0)) && ((typeof properties.sessionNo === 'undefined') || (properties.sessionNo.length === 0))) {
                properties.sessionNo = responseCheckInItem.webCheckInItem.sessionNo;
                //jQuery('#masterLoggedInView-captionPageSubTitle').html(RwLanguages.translate('Session') + ': ' + properties.sessionNo);
                FwMobileMasterController.setTitle(RwLanguages.translate('Session') + ': ' + properties.sessionNo);
            }
            //screen.$popupQty.find('#checkIn-newOrder')
            //    .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.showNewOrder));
            //screen.$popupQty.find('#checkIn-newOrder-btnSwap')
            //    .toggle(responseCheckInItem.webCheckInItem.allowSwap);
            screen.$popupQty.find('#checkIn-newOrder').hide();
            screen.$popupQty.find('#checkIn-newOrder-btnSwap').hide();
            switch (responseCheckInItem.webCheckInItem.status) {
                //case '1005': //Item count exceeds quantity ordered - Item   - 2017/02/24 MY: removed due to Emil
                case 1015:
                case 1019:   //Package Truck
                    screen.$popupQty.find('#checkIn-newOrder').show();
                    break;
                case 104: //Item is Staged on Order
                case 301: //I-Code / Bar Code not found in Inventory.
                    break;
                case 1007: //item is on new order - no swap available
                    screen.$popupQty.find('#checkIn-newOrder').show();
                    break;
                case 1005: //item is on new order - swap available
                    screen.$popupQty.find('#checkIn-newOrder').show();
                    screen.$popupQty.find('#checkIn-newOrder-btnSwap').show();
                    break;
            }
            if (responseCheckInItem.webCheckInItem.status === 0) {
                screen.$popupQty.find('#checkIn-popupQty-genericMsg').removeClass('qserror').addClass('qssuccess');
            }
            screen.$popupQty.find('#checkIn-popupQty-msg')
                .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.msg.length > 0));
            screen.$popupQty.find('#checkIn-popupQty-fields')
                .toggle((applicationConfig.designMode) || ((responseCheckInItem.webCheckInItem.status === 0)/* && (!isScannedICode)*/));  // mv 2016-12-20 it was causing the info to not show when scanning an icode
            screen.$popupQty.find('#checkIn-popupQty-genericMsg')
                .toggle((applicationConfig.designMode) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0));
            screen.$popupQty.find('#checkIn-popupQty-messages')
                .toggle((responseCheckInItem.webCheckInItem.msg.length > 0) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0));
            screen.$popupQty.find('#checkIn-popupQty-qtyPanel')
                .toggle((applicationConfig.designMode) || ((responseCheckInItem.request.qty === 0) && (responseCheckInItem.webCheckInItem.isICode) && responseCheckInItem.webCheckInItem.status === 0));
            //jQuery('#checkIn-pageSelectorInner').toggle(properties.contractId !== '');
            if ((responseCheckInItem.request.qty === 0) || (responseCheckInItem.webCheckInItem.genericMsg.length > 0) || (responseCheckInItem.webCheckInItem.msg.length > 0)) {
                screen.showPopupQty();
                if (isScannedICode) {
                    setTimeout(function() {
                        var $txt = screen.$popupQty.find('.fwformfield[data-datafield="qty"] .fwformfield-value');
                        $txt.focus();
                    }, 50);
                }
                if (responseCheckInItem.webCheckInItem.status === 0 && responseCheckInItem.request.qty > 0) {
                    setTimeout(function() {
                        screen.hidePopupQty();
                    }, 3000);
                }
            }
            else {
                screen.hidePopupQty();
            }
            switch (screen.properties.currentview) {
                case 'PENDING':
                    $checkinmodeselector.find('#pendingtab').click();
                    break;
                case 'SESSIONIN':
                    $checkinmodeselector.find('#sessiontab').click();
                    break;
            }
        };

        screen.getRowCountItem = function(lineitemcount, totalitems) {
            var li = [];
            li.push('<li class="normal lineitemcount">');
            li.push('  <div>' + lineitemcount.toString() + '  lines / ' + totalitems + ' items</div>');
            li.push('</li>');
            return li.join('');
        };

        screen.rfidscan = function(epcs) {
            if ((epcs !== '') && (screen.getContractId() !== '')) {
                $rfid.scanrfid(epcs);
            }
        };

        screen.disableBarcodeField = function () {
            //screen.$view.find('#scanBarcodeView-txtBarcodeData').prop('disabled', true);
        };

        screen.enableBarcodeField = function () {
            screen.$view.find('#scanBarcodeView-txtBarcodeData').prop('disabled', false);
        };

        screen.isBarcodeFieldDisabled = function () {
            return screen.$view.find('#scanBarcodeView-txtBarcodeData').prop('disabled');
        };

        screen.scanCode = function(code) {
            var orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId, isAisleShelfBarcode, trackedby;
            if (!screen.isBarcodeFieldDisabled()) {
                switch (screen.properties.currentview) {
                    case 'PENDING':
                        //screen.$view.find('#pendingsearch').fwmobilesearch('setsearchmode', 'code');
                        screen.$view.find('#pendingsearch').fwmobilesearch('setSearchText', code, false);
                        break;
                    case 'SESSIONIN':
                        //screen.$view.find('#sessioninsearch').fwmobilesearch('setsearchmode', 'code');
                        screen.$view.find('#sessioninsearch').fwmobilesearch('setSearchText', code, false);
                        break;
                }
                screen.disableBarcodeField();
                if (code.length > 0) {
                    isAisleShelfBarcode = /^[A-z0-9]{4}-[A-z0-9]{4}$/.test(code);  // Format: AAAA-SSSS
                    if (isAisleShelfBarcode) {
                        let aisleshelfstring = code.split('-');
                        let aisleshelfdata = { 'Aisle': aisleshelfstring[0].toUpperCase(), 'Shelf': aisleshelfstring[1].toUpperCase() };

                        screen._aisleshelfdata(aisleshelfdata);

                        screen.displayAisleShelf(aisleshelfdata.Aisle, aisleshelfdata.Shelf);

                        //$txtBarcodeData.val('');
                        screen.enableBarcodeField();
                    } else {
                        orderId = screen.getOrderId();
                        masterItemId = '';
                        masterId = '';
                        const barcode = RwAppData.stripBarcode(code.toUpperCase());
                        qty = 0;
                        newOrderAction = '';
                        aisle = screen._aisleshelfdata()?.Aisle || '';
                        shelf = screen._aisleshelfdata()?.Shelf || '';
                        playStatus = true;
                        vendorId = '';
                        trackedby = '';
                        screen.checkInItem(orderId, masterItemId, masterId, barcode, qty, newOrderAction, aisle, shelf, playStatus, vendorId, trackedby);
                    }
                }
            }  
        }

        screen.$view
            //.on('change', '#scanBarcodeView-txtBarcodeData', function() {
            //    var barcode, $txtBarcodeData, requestCheckInItem, orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId, isAisleShelfBarcode, trackedby;
            //    try {
            //        if (!screen.isBarcodeFieldDisabled()) {
            //            screen.disableBarcodeField();
            //            $txtBarcodeData = jQuery(this);
            //            barcode = $txtBarcodeData.val();
            //            if (barcode.length > 0) {
            //                isAisleShelfBarcode = /^[A-z0-9]{4}-[A-z0-9]{4}$/.test(barcode);  // Format: AAAA-SSSS
            //                if (isAisleShelfBarcode) {
            //                    let aisleshelfstring = $txtBarcodeData.val().split('-');
            //                    let aisleshelfdata   = { 'Aisle': aisleshelfstring[0].toUpperCase(), 'Shelf': aisleshelfstring[1].toUpperCase() };

            //                    screen._aisleshelfdata(aisleshelfdata);

            //                    screen.displayAisleShelf(aisleshelfdata.Aisle, aisleshelfdata.Shelf);

            //                    $txtBarcodeData.val('');
            //                    screen.enableBarcodeField();
            //                } else {
            //                    orderId        = screen.getOrderId();
            //                    masterItemId   = '';
            //                    masterId       = '';
            //                    code           = RwAppData.stripBarcode($txtBarcodeData.val().toUpperCase());
            //                    qty            = 0;
            //                    newOrderAction = '';
            //                    aisle          = screen._aisleshelfdata()?.Aisle || '';
            //                    shelf          = screen._aisleshelfdata()?.Shelf || '';
            //                    playStatus     = true;
            //                    vendorId       = '';
            //                    trackedby      = '';
            //                    screen.checkInItem(orderId, masterItemId, masterId, code, qty, newOrderAction, aisle, shelf, playStatus, vendorId, trackedby);
            //                }
            //            }
            //        }
            //    } catch(ex) {
            //        FwFunc.showError(ex);
            //    }
            //})
            .on('click', '.aisleshelfclear', function() {
                screen.$view.find('#checkIn-bottomtray .aisleshelf').remove();
                screen.refreshbottomspacer();

                aisleshelfdata = null;
            })
            .on('click', '.orderlocationclear', function() {
                screen.$view.find('#checkIn-bottomtray .orderlocation').remove();
                screen.refreshbottomspacer();

                locationdata = null;
            })
        ;

        screen.displayAisleShelf = function (aisle, shelf) {
            screen.$view.find('#checkIn-bottomtray .aisleshelf').remove();

            let html = `<div class="aisleshelf">
                          <div class="item aisle"><div class="caption">Aisle:</div><div class="value">${aisle}</div></div>
                          <div class="item shelf"><div class="caption">Shelf:</div><div class="value">${shelf}</div></div>
                          <div class="aisleshelfclear btnclear">Clear</div>
                        </div>`;
            screen.$view.find('#checkIn-bottomtray').append(html);

            screen.refreshbottomspacer();
        };
        screen.displayOrderLocation = function (orderlocation) {
            screen.$view.find('#checkIn-bottomtray .orderlocation').remove();

            let html = `<div class="orderlocation">
                          <div class="item"><div class="caption">Location:</div><div class="value">${orderlocation}</div></div>
                          <div class="orderlocationclear btnclear">Clear</div>
                        </div>`;
            screen.$view.find('#checkIn-bottomtray').append(html);

            screen.refreshbottomspacer();
        };
        screen.refreshbottomspacer = function () {
            screen.$view.find('#checkIn-bottomspacer').css('height', screen.$view.find('#checkIn-bottomtray').height());
        };

        screen.renderPopupQty = function() {
            var template = Mustache.render(jQuery('#tmpl-CheckIn-PopupQty').html(), {
                captionICodeDesc:          RwLanguages.translate('I-Code'),
                captionRemaining:          RwLanguages.translate('Remaining'),
                captionSessionIn:          RwLanguages.translate('Session In'),
                captionQtyOrdered:         RwLanguages.translate('Ordered'),
                captionTotalIn:            RwLanguages.translate('In'),
                captionSubQty:             RwLanguages.translate('Sub'),
                captionSubVendor:          RwLanguages.translate('Vendor/Consignor'),
                valueTxtQty:               '',
                captionCheckIn:            RwLanguages.translate('Check-In'),
                captionNewOrder:           RwLanguages.translate('New Order'),
                captionSwap:               RwLanguages.translate('Swap'),
                captionCancel:             RwLanguages.translate('Cancel')
            });
            var $popupcontent = jQuery(template);
            if (typeof screen.$popupQty === 'object' && screen.$popupQty.length > 0) {
                FwPopup.destroyPopup(screen.$popupQty);
            }
            screen.$popupQty = FwPopup.renderPopup($popupcontent, {ismodal:false});
            screen.$popupQty.find('#checkIn-popupQty-description').hide();
            screen.$popupQty.find('#checkIn-popupQty-messages').hide();
            screen.$popupQty.find('#checkIn-popupQty-fields').hide();
            screen.$popupQty.find('#checkIn-popupQty-qtyPanel').hide();
            screen.$popupQty.find('#checkIn-newOrder').hide();
            FwControl.renderRuntimeControls(screen.$popupQty.find('.fwcontrol'));
            FwPopup.showPopup(screen.$popupQty);
            screen.$popupQty
                .on('click', '#checkIn-qty-btnCheckIn', function() {
                    var requestChangeQty, remaining, $this;
                    try {
                        $this = jQuery(this);
                        $this.prop('disabled', true);
                        requestChangeQty = jQuery.extend({}, properties.responseCheckInItem.request);
                        requestChangeQty.qty = parseInt(FwFormField.getValue2(screen.$popupQty.find('.fwformfield[data-datafield="qty"]')));
                        remaining = Number(jQuery('#checkIn-popupQty-stillOut').html());
                        if (isNaN(requestChangeQty.qty) || requestChangeQty.qty <= 0 || (screen.$popupQty.find('.row2').is(':visible') && requestChangeQty.qty > remaining)) {
                            throw 'Invalid qty.';
                        }
                        RwServices.callMethod("CheckIn", "CheckInItem", requestChangeQty, function(responseChangeQty) {
                            screen.toggleReconcileButton(responseChangeQty.enablereconcile);
                            screen.checkInItemCallback(responseChangeQty);
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#checkIn-qty-btnCancel', function () {
                    var $this;
                    try {
                        $this = jQuery(this);
                        $this.prop('disabled', true);
                        FwPopup.destroyPopup(screen.$popupQty);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#checkIn-newOrder-btnSwap', function() {
                    var requestSwap, $this;
                    try {
                        $this = jQuery(this);
                        $this.prop('disabled', true);
                        requestSwap = jQuery.extend({}, properties.responseCheckInItem.request);
                        requestSwap.newOrderAction = 'S';
                        RwServices.callMethod("CheckIn", "CheckInItem", requestSwap, function(responseSwap) {
                            screen.toggleReconcileButton(responseSwap.enablereconcile);
                            screen.checkInItemCallback(responseSwap);
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#checkIn-newOrder-btnCancel', function() {
                    var $this;
                    try {
                        $this = jQuery(this);
                        $this.prop('disabled', true);
                        FwPopup.destroyPopup(screen.$popupQty);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#checkIn-newOrder-btnNewOrder', function() {
                    var requestNewOrder, $this;
                    try {
                        $this = jQuery(this);
                        $this.prop('disabled', true);
                        requestNewOrder = jQuery.extend({}, properties.responseCheckInItem.request);
                        requestNewOrder.newOrderAction = 'Y';
                        RwServices.callMethod("CheckIn", "CheckInItem", requestNewOrder, function(responseNewOrder) {
                            screen.toggleReconcileButton(responseNewOrder.enablereconcile);
                            screen.checkInItemCallback(responseNewOrder);
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
            ;
        };

        screen.load = function() {
            program.setScanTarget('');
            program.setScanTargetLpNearfield('');

            program.onScanBarcode = function (barcode, barcodeType) {
                screen.scanCode(barcode);
            }

            if (typeof window.TslReader !== 'undefined') {
                window.TslReader.registerListener('deviceConnected', 'deviceConnected_checkincontrollerjs_getCheckInScreen', function() {
                    RwRFID.isConnected = true;
                    screen.toggleRfid();
                });
                window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_checkincontrollerjs_getCheckInScreen', function() {
                    RwRFID.isConnected = false;
                    screen.toggleRfid();
                });
                RwRFID.registerEvents(screen.rfidscan);
            }
            if (typeof window.ZebraRFIDAPI3 !== 'undefined') {
                window.ZebraRFIDAPI3.registerListener('deviceConnected', 'deviceConnected_checkincontrollerjs_getCheckInScreen', function () {
                    screen.toggleRfid();
                });
                window.ZebraRFIDAPI3.registerListener('deviceDisconnected', 'deviceDisconnected_checkincontrollerjs_getCheckInScreen', function () {
                    screen.toggleRfid();
                });
                RwRFID.registerEvents(screen.rfidscan);
            }

            jQuery(window)
                .on('scroll', function() {
                    screen.evalWindowPosition();
                })
                .on('touchmove', function() {
                    screen.evalWindowPosition();
                })
            ;

            RwServices.callMethod("CheckIn", "LoadModuleProperties", {}, function(response) {
                moduleproperties = response;
                $checkinmodeselector.find('#pendingtab').click();
            });

            screen.toggleRfid();
            screen.toggleFillContainerButton();
            screen.toggleReconcileButton();
        };

        screen.unload = function() {
            // Reset the LineaPro
            program.setScanTarget('#scanBarcodeView-txtBarcodeData');
            program.setScanTargetLpNearfield('');

            if (typeof window.TslReader !== 'undefined') {
                window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_checkincontrollerjs_getCheckInScreen');
                window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_checkincontrollerjs_getCheckInScreen');
                RwRFID.unregisterEvents();
            }
            if (typeof window.ZebraRFIDAPI3 !== 'undefined') {
                window.ZebraRFIDAPI3.unregisterListener('deviceConnected', 'deviceConnected_checkincontrollerjs_getCheckInScreen');
                window.ZebraRFIDAPI3.unregisterListener('deviceDisconnected', 'deviceDisconnected_checkincontrollerjs_getCheckInScreen');
                RwRFID.unregisterEvents();
            }
            jQuery(window).off('scroll').off('touchmove');
        };

        screen.evalWindowPosition = function() {
            if (jQuery(window).scrollTop() > 152) {
                var $jumptotop;

                $jumptotop = jQuery('<div>')
                    .html('<i class="material-icons">&#xE5D8;</i>') //arrow_upward
                    .addClass('fwchip jumptotop')
                    .on('click', function() {
                        jQuery(window).scrollTop(0);
                        jQuery(this).remove();
                    });

                if (screen.$view.find('.jumptotop').length === 0) {
                    screen.$view.append($jumptotop);
                }
            } else {
                screen.$view.find('.jumptotop').remove();
            }
        };

        screen.beforeNavigateAway = function(navigateAway) {
            var requestCancelContract;
            requestCancelContract = {
                contractId:                    screen.getContractId(),
                activityType:                  'CheckIn',
                dontCancelIfOrderTranExists:   true,
                failSilentlyOnOwnershipErrors: true
            };
            RwServices.order.cancelContract(requestCancelContract, 
                function doneCallback(responseCancelContract) {
                    // in case there is an error we still want to navigate away
                    navigateAway();
                }),
                null,
                function failCallback(jqXHR, textStatus, errorThrown) {
                    RwAppData.error(jqXHR, textStatus, errorThrown);
                    navigateAway();
                }
            ;
        };

        screen.toggleReconcileButton = function(enable) {
            if (typeof enable === 'undefined') {
                if (screen.getContractId().length > 0) {
                    var request = {
                        contractid: screen.getContractId()
                    };
                    RwServices.callMethod("CheckIn", "ToggleReconcile", request, function(response) {
                        if (response.enable) {
                            $checkincontrol.fwmobilemodulecontrol('showButton', '#reconcile');
                        } else {
                            $checkincontrol.fwmobilemodulecontrol('hideButton', '#reconcile');
                        }
                    });
                } else {
                    $checkincontrol.fwmobilemodulecontrol('hideButton', '#reconcile');
                }
            } else {
                if (enable) {
                    $checkincontrol.fwmobilemodulecontrol('showButton', '#reconcile');
                } else {
                    $checkincontrol.fwmobilemodulecontrol('hideButton', '#reconcile');
                }
            }
        };

        screen.toggleFillContainerButton = function() {
            if ((screen.getContractId().length > 0) && (sessionStorage.userType === 'USER') && (typeof applicationOptions.container !== 'undefined') && (applicationOptions.container.enabled)) {
                //var requestHasCheckinFillContainerButton = {
                //    contractid: screen.getContractId()
                //};
                //RwServices.FillContainer.HasCheckinFillContainerButton(requestHasCheckinFillContainerButton, function(response) {
                //    if (response.hasCheckinFillContainerButton) {
                //        $checkincontrol.fwmobilemodulecontrol('showButton', '#fillcontainer');
                //    } else {
                //        $checkincontrol.fwmobilemodulecontrol('hideButton', '#fillcontainer');
                //    }
                //});
                $checkincontrol.fwmobilemodulecontrol('showButton', '#fillcontainer');
            } else {
                $checkincontrol.fwmobilemodulecontrol('hideButton', '#fillcontainer');
            }
        };

        return screen;
    };
}

var CheckInController = new CheckInControllerClass();
