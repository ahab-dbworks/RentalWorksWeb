var Exchange = {};
//----------------------------------------------------------------------------------------------
Exchange.getModuleScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle, $fwcontrols;
    combinedViewModel = jQuery.extend({
        captionPageTitle: RwLanguages.translate('Exchange'),
        captionOrderNo: RwLanguages.translate('Order No'),
        captionOrder: RwLanguages.translate('Order'),
        captionDeal: RwLanguages.translate('Deal'),
        captionDealNo: RwLanguages.translate('Deal No'),
        captionDepartment: RwLanguages.translate('Department'),
        captionSessionNo: RwLanguages.translate('Session No'),
        captionICode: RwLanguages.translate('I-Code'),
        captionItemDesc: RwLanguages.translate('Item Desc'),
        captionSubRentalVendor: RwLanguages.translate('Sub-Vendor'),
        captioPONo: RwLanguages.translate('PO No'),
        captionWarehouse: RwLanguages.translate('Warehouse')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-Exchange').html(), combinedViewModel);
    screen = {};
    screen.properties = properties;
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);
    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);
    
    screen.setexchangecontractid = function(exchangecontractid) { screen.exchangecontractid = exchangecontractid; };
    screen.getexchangecontractid = function() { return screen.exchangecontractid; };

    screen.getinbarcode = function() { 
        var barcode = FwFormField.getValue(screen.$view, '.checkin');
        barcode = RwAppData.stripBarcode(barcode);
        return barcode;
    };
    screen.setinbarcode = function(barcode) { FwFormField.setValue(scren.$view, '.checkin', barcode); };
    screen.getoutbarcode = function() { 
        var barcode = FwFormField.getValue(screen.$view, '.checkout');
        barcode = RwAppData.stripBarcode(barcode);
        return barcode;
    };
    screen.setoutbarcode = function(barcode) { FwFormField.setValue(scren.$view, '.checkout', barcode); };
    screen.getqty = function() { 
        var qty = FwFormField.getValue(screen.$view, '.exchangeqty');
        qty = parseInt(qty);
        if (isNaN(qty)) {
            qty = 1;
        }
        return qty; 
    };
    screen.setqty = function(qty) { FwFormField.setValue(screen.$view, '.exchangeqty', qty); };
    
    screen.getorderid = function() { return screen.orderid; };
    screen.setorderid = function(orderid) { screen.orderid = orderid; };
    screen.getorderno = function() { return screen.orderno; };
    screen.setorderno = function(orderno) { screen.orderno = orderno; };
    screen.getorderdesc = function() { return screen.orderdesc; };
    screen.setorderdesc = function(orderdesc) { screen.orderdesc = orderdesc; };

    screen.getdealid = function() { return screen.dealid; };
    screen.setdealid = function(dealid) { screen.dealid = dealid; };
    screen.getdeal = function() { return screen.deal; };
    screen.setdeal = function(deal) { screen.deal = deal; };
    screen.getdealno = function() { return screen.dealno; };
    screen.setdealno = function(dealno) { screen.dealno = dealno; };

    screen.getdepartmentid = function() { return screen.departmentid; };
    screen.setdepartmentid = function(departmentid) { screen.departmentid = departmentid; };
    screen.getdepartment = function() { return screen.department; };
    screen.setdepartment = function(department) { screen.department = department; };

    screen.getExchangeMode = function() { return screen.exchangeMode; };
    screen.setExchangeMode = function(mode) { screen.exchangeMode = mode; };

    screen.getCurrentPage = function() { return screen.currentpage; };
    screen.setCurrentPage = function(currentpage) { screen.currentpage = currentpage; };

    screen.getcompletingpending = function() { return screen.completingpending; };
    screen.setcompletingpending = function(completingpending) { screen.completingpending = completingpending; };

    screen.allowcrossicode = false;
    screen.getallowcrossicode = function() { return screen.allowcrossicode; };
    screen.setallowcrossicode = function(allowcrossicode) { screen.allowcrossicode = allowcrossicode; };
    
    screen.getsessionno = function() { return screen.sessionno; };
    screen.setsessionno = function(sessionno) { screen.sessionno = sessionno; };

    screen.resetall = function() {
        var exchange = screen.getexchange();
        exchange.allowcrossicode = false;
        exchange.allowcrosswarehouse = false;
        screen.clearInItem();
        screen.clearOutItem();

        screen.$view.find('.pageexchange .valueOrderNo').text('');
        screen.$view.find('.pageexchange .valueOrder').text('');
        screen.$view.find('.pageexchange .valueICode').text('');
        screen.$view.find('.pageexchange .valueItem').text('');
        FwFormField.enable(screen.$view.find('.exchangeqty'));
        FwFormField.setValue(screen.$view, '.exchangeqty', 0);
    };

    screen.$btnback = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', false, function() { //back
        try {
            screen.pages[screen.getCurrentPage()].back();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btnnewsession = FwMobileMasterController.addFormControl(screen, 'New Session', 'right', '&#xE5CC;', false, function() { //continue
        try {
            var request = {

            };
            RwServices.callMethod('Exchange', 'CreateExchangeContract', request, function(response) {
                try {
                    screen.setexchangecontractid(response.createexchangecontract.exchangecontractid);
                    screen.pages.exchange.show();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btncreatecontract = FwMobileMasterController.addFormControl(screen, 'Create Contract', 'right', '&#xE5CC;', false, function() { //continue
        try {
            properties.contract = {
                contractType:        'EXCHANGE',
                contractId:          screen.getexchangecontractid(),
                orderId:             screen.getorderid(),
                responsiblePersonId: ''
            };
            program.pushScreen(RwOrderController.getContactSignatureScreen(viewModel, properties));
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$tabscan = FwMobileMasterController.tabcontrols.addtab('Scan', true);
    screen.$tabscan.on('click', function() {
        try {
            screen.$view.find('.pageexchange .tab').hide();
            screen.$view.find('.pageexchange .tab.scan').show();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$tabsession  = FwMobileMasterController.tabcontrols.addtab('Session', false);
    screen.$tabsession.on('click', function() {
        try {
            screen.$view.find('.pageexchange .tab').hide();
            screen.$view.find('.pageexchange .tab.session').show();
            screen.$view.find('.exchangesessionsearch').fwmobilesearch('search');
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$tabrepair  = FwMobileMasterController.tabcontrols.addtab('Repair', false);
    screen.$tabrepair.on('click', function() {
        try {
            screen.$view.find('.pageexchange .tab').hide();
            screen.$view.find('.pageexchange .tab.repair').show();
            screen.$view.find('.exchangerepairsearch').fwmobilesearch('search');
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$tabtransfer  = FwMobileMasterController.tabcontrols.addtab('Transfer', false);
    screen.$tabtransfer.on('click', function() {
        try {
            screen.$view.find('.pageexchange .tab').hide();
            screen.$view.find('.pageexchange .tab.transfer').show();
            screen.$view.find('.exchangetransfersearch').fwmobilesearch('search');
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.modes = {
        barcode: 1,
        order: 2,
        deal: 3,
        companydepartment: 4,
        pendingexchange: 5,
        suspendedsession: 6
    };

    screen.hideAllPages = function() {
        screen.$view.find('.page').hide();
        screen.$btnback.hide();
        screen.$btnnewsession.hide();
        screen.$btncreatecontract.hide();
        screen.$view.find('.pageexchangeby .spacerpendingexchange').hide();
        screen.$view.find('.pageexchangeby .btnPendingExchange').hide();
        screen.$view.find('.pageexchangeby .spacersuspendedsessions').hide();
        screen.$view.find('.pageexchangeby .btnSuspendedSession').hide();
        screen.$tabscan.hide();
        screen.$tabsession.hide();
        screen.$tabrepair.hide();
        screen.$tabtransfer.hide();
        screen.$view.find('.pageexchange .tab').hide();
    };

    screen.pages = {
        exchangeby: {
            back: function() {
                throw 'This page should not have a back button!';
            },
            show: function() {
                screen.setCurrentPage('exchangeby');
                screen.hideAllPages();
                screen.setcompletingpending(false);
                screen.setexchangecontractid('');
                screen.setorderid('');
                screen.setorderno('');
                screen.setorderdesc('');
                screen.setdealid('');
                screen.setdealno('');
                screen.setdeal('');
                screen.setdepartmentid('');
                screen.setdepartment('');
                screen.$view.find('.pageexchangeby').show();
                var requestPendingExchangeSearch = {
                    pageno: 0,
                    pagesize: 0,
                    searchmode: '',
                    searchvalue: ''
                };
                RwServices.callMethod('Exchange', 'PendingExchangeSearch', requestPendingExchangeSearch, function(response) {
                    try {
                        if (response.searchresults.Rows.length > 0) {
                            // show the suspended sessions button
                             screen.$view.find('.pageexchangeby .spacerpendingexchange').show();
                             screen.$view.find('.pageexchangeby .btnPendingExchange').show();
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
                var requestSuspendedSessionSearch = {
                    pageno: 0,
                    pagesize: 0
                };
                RwServices.callMethod('Exchange', 'SuspendedSessionSearch', requestSuspendedSessionSearch, function(response) {
                    try {
                        if (response.searchresults.Rows.length > 0) {
                            // show the suspended sessions button
                             screen.$view.find('.pageexchangeby .spacersuspendedsessions').show();
                             screen.$view.find('.pageexchangeby .btnSuspendedSession').show();
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        },
        selectorder: {
            back: function() {
                screen.hideAllPages();
                screen.setexchangecontractid('');
                screen.setcompletingpending(false);
                screen.pages.exchangeby.show();
            }, 
            show: function() {
                screen.setCurrentPage('selectorder');
                screen.hideAllPages();
                screen.setexchangecontractid('');
                screen.setorderid('');
                screen.setorderdesc('');
                screen.setorderno('');
                screen.setdealid('');
                screen.setdealno('');
                screen.setdeal('');
                screen.setdepartmentid('');
                screen.setdepartment('');
                screen.$view.find('.pageselectorder').show();
                screen.$btnback.show();
                screen.$view.find('.ordersearch').fwmobilesearch('search');
            }
        },
        selectdeal: {
            back: function() {
                screen.hideAllPages();
                screen.setexchangecontractid('');
                screen.setcompletingpending(false);
                screen.pages.exchangeby.show();
            }, 
            show: function() {
                screen.setCurrentPage('selectdeal');
                screen.hideAllPages();
                screen.$view.find('.pageselectdeal').show();
                screen.$btnback.show();
                screen.$view.find('.dealsearch').fwmobilesearch('search');
            }
        },
        selectcompanydepartment: {
            back: function() {
                screen.hideAllPages();
                screen.setexchangecontractid('');
                screen.setcompletingpending(false);
                screen.pages.exchangeby.show();
            }, 
            show: function() {
                screen.setCurrentPage('selectcompanydepartment');
                screen.hideAllPages();
                screen.$view.find('.pageselectcompanydepartment').show();
                screen.$btnback.show();
                screen.$view.find('.companydepartmentsearch').fwmobilesearch('search');
            }
        },
        selectpendingexchange: {
            back: function() {
                screen.hideAllPages();
                screen.setexchangecontractid('');
                screen.setcompletingpending(false);
                screen.pages.exchangeby.show();
            }, 
            show: function() {
                screen.setCurrentPage('selectpendingexchange');
                screen.hideAllPages();
                screen.$view.find('.pageselectpendingexchange').show();
                screen.$btnback.show();
                screen.$view.find('.pendingexchangesearch').fwmobilesearch('search');
            }
        },
        selectsuspendedsession: {
            back: function() {
                screen.hideAllPages();
                screen.setexchangecontractid('');
                screen.setcompletingpending(false);
                screen.pages.exchangeby.show();
            }, 
            show: function() {
                screen.setCurrentPage('selectsuspendedsession');
                screen.hideAllPages();
                screen.$view.find('.pageselectsuspendedsession').show();
                screen.$btnback.show();
                var mode = screen.getExchangeMode();
                switch(mode) {
                    case screen.modes.order:
                    case screen.modes.deal:
                    case screen.modes.companydepartment:
                        screen.$btnnewsession.show();
                        break;
                }
                screen.$view.find('.suspendedsessionsearch').fwmobilesearch('search');
            }
        },
        exchange: {
            back: function() {
                if (!screen.getcompletingpending()) {
                    var requestCancelContract = {
                        exchangecontractid: screen.getexchangecontractid()
                    };
                    FwConfirmation.yesNo('Cancel Contract?', 'Cancel Exchange Contract?', 
                        function onyes() {
                            try {
                                RwServices.callMethod('Exchange', 'CancelContract', requestCancelContract, function(responseCancelContract) {
                                    screen.pages.exchange.back2();
                                });
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        }, 
                        function onno() {
                            try {
                                //do nothing
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        }
                    );
                }
                else {
                    screen.pages.exchange.back2();
                }
            },
            back2: function() {
                screen.hideAllPages();
                screen.setexchangecontractid('');
                screen.setcompletingpending(false);
                var mode = screen.getExchangeMode();
                switch(mode) {
                    case screen.modes.order:
                        screen.pages.selectorder.show();
                        break;
                    case screen.modes.deal:
                        screen.pages.selectdeal.show();
                        break;
                    case screen.modes.companydepartment:
                        screen.pages.selectcompanydepartment.show();
                        break;
                    case screen.modes.pendingexchange:
                        screen.pages.selectpendingexchange.show();
                        break;
                    case screen.modes.suspendedsession:
                        screen.pages.selectsuspendedsession.show();
                        break;
                }
            },
            show: function() {
                var requestGetNewExchangeModel = {
                };
                RwServices.callMethod('Exchange', 'GetNewExchangeModel', requestGetNewExchangeModel, function(responseGetNewExchangeModel) {
                    try {
                        if (typeof responseGetNewExchangeModel.exchange === 'object') {
                            screen.setexchange(responseGetNewExchangeModel.exchange);
                            var $pageexchange = screen.$view.find('.pageexchange');
                            FwFormField.setValue($pageexchange, '.checkin', '', '', false);
                            FwFormField.setValue($pageexchange, '.checkout', '', '', false);
                            screen.setCurrentPage('exchange');
                            $pageexchange.find('.valueHeaderOrderNo').text(screen.getorderno());
                            $pageexchange.find('.valueHeaderOrderDesc').text(screen.getorderdesc());
                            $pageexchange.find('.valueHeaderDeal').text(screen.getdeal());
                            $pageexchange.find('.valueHeaderDealNo').text(screen.getdealno());
                            $pageexchange.find('.valueHeaderCompanyDepartment').text(screen.getdepartment());
                            $pageexchange.find('.valueHeaderSessionNo').text(screen.getsessionno());
                            $pageexchange.find('.fieldHeaderOrderNo').hide();
                            $pageexchange.find('.fieldHeaderOrderDesc').hide();
                            $pageexchange.find('.fieldHeaderDeal').hide();
                            $pageexchange.find('.fieldHeaderDealNo').hide();
                            $pageexchange.find('.fieldHeaderCompanyDepartment').hide();
                            $pageexchange.find('.fieldHeaderSessionNo').hide();
                            screen.hideAllPages();
                            screen.$btncreatecontract.show();
                            screen.$tabscan.show();
                            screen.$tabsession.show();
                            //screen.setupExchangeSessionSearch();
                            screen.$tabrepair.show();
                            screen.$tabtransfer.show();
                            screen.$tabscan.click();
                            var mode = screen.getExchangeMode();
                            switch(mode) {
                                case screen.modes.order:
                                    $pageexchange.find('.fieldHeaderOrderNo').show();
                                    $pageexchange.find('.fieldHeaderOrderDesc').show();
                                    $pageexchange.find('.fieldHeaderDeal').show();
                                    $pageexchange.find('.fieldHeaderDealNo').show();
                                    $pageexchange.find('.fieldHeaderCompanyDepartment').show();
                                    $pageexchange.find('.fieldHeaderSessionNo').show();
                                    $pageexchange.find('.rowcheckin').show();
                                    $pageexchange.find('.rowcheckout').show();
                                    break;
                                case screen.modes.deal:
                                    $pageexchange.find('.fieldHeaderDeal').show();
                                    $pageexchange.find('.fieldHeaderDealNo').show();
                                    $pageexchange.find('.fieldHeaderSessionNo').show();
                                    $pageexchange.find('.rowcheckin').show();
                                    $pageexchange.find('.rowcheckout').show();
                                    break;
                                case screen.modes.companydepartment:
                                    $pageexchange.find('.fieldHeaderCompanyDepartment').show();
                                    $pageexchange.find('.fieldHeaderSessionNo').show();
                                    $pageexchange.find('.rowcheckin').show();
                                    $pageexchange.find('.rowcheckout').show();
                                    break;
                                case screen.modes.pendingexchange:
                                    $pageexchange.find('.fieldHeaderOrderNo').show();
                                    $pageexchange.find('.fieldHeaderOrderDesc').show();
                                    $pageexchange.find('.fieldHeaderDeal').show();
                                    $pageexchange.find('.fieldHeaderDealNo').show();
                                    $pageexchange.find('.fieldHeaderCompanyDepartment').show();
                                    $pageexchange.find('.fieldHeaderSessionNo').show();
                                    $pageexchange.find('.rowcheckin').show();
                                    $pageexchange.find('.rowcheckout').hide();
                                    break;
                                case screen.modes.suspendedsession:
                                    $pageexchange.find('.fieldHeaderOrderNo').show();
                                    $pageexchange.find('.fieldHeaderOrderDesc').show();
                                    $pageexchange.find('.fieldHeaderDeal').show();
                                    $pageexchange.find('.fieldHeaderDealNo').show();
                                    $pageexchange.find('.fieldHeaderCompanyDepartment').show();
                                    $pageexchange.find('.fieldHeaderSessionNo').show();
                                    $pageexchange.find('.rowcheckin').show();
                                    $pageexchange.find('.rowcheckout').show();
                                    break;
                            }
                            $pageexchange.show();
                            screen.$btnback.show();
                        } else {
                            throw 'An error occur while trying to intialize an exchange session!';
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        }
    };

    screen.$view.find('.pageexchangeby')
        .on('click', '.btnOrder', function() {
            try {
                screen.setExchangeMode(screen.modes.order);
                screen.pages.selectorder.show();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnDeal', function() {
            try {
                screen.setExchangeMode(screen.modes.deal);
                screen.pages.selectdeal.show();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnCompanyDepartment', function() {
            try {
                screen.setExchangeMode(screen.modes.companydepartment);
                screen.pages.selectcompanydepartment.show();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnPendingExchange', function() {
            try {
               screen.setExchangeMode(screen.modes.pendingexchange);
               screen.pages.selectpendingexchange.show();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnSuspendedSession', function() {
            try {
               screen.setExchangeMode(screen.modes.suspendedsession);
               screen.pages.selectsuspendedsession.show();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.$view.find('.pageexchange')
        .on('focus', '.scangroup .fwformfield-value', function() {
            screen.$view.find('.pageexchange').find('.scangroup .fwformfield-value').removeClass('scantarget');
            jQuery(this).addClass('scantarget');
        })
        .on('keypress', '.checkin .fwformfield-value', function(e) {
            if (e.which === 13) {
                e.preventDefault();
                jQuery(this).blur();
            }
        })
        .on('blur', '.checkin .fwformfield-value', function() {
            try {
                if (this.value.length > 0) {
                    var request = {
                        exchange: screen.getexchange()
                    };
                    RwServices.callMethod('Exchange', 'GetItemInfo', request, function(response) {
                        try {
                            program.playStatus(response.success);
                            if (response.getiteminfo.success === false) {
                                screen.setinbarcode('');
                                screen.$view.find('.pageexchange .valueOrderNo').text('');
                                screen.$view.find('.pageexchange .valueOrder').text('');
                                screen.$view.find('.pageexchange .valueICode').text('');
                                screen.$view.find('.pageexchange .valueItem').text('');
                                screen.$view.find('.pageexchange .valueCheckInSubRentalVendor').text('');
                                screen.$view.find('.pageexchange .valueCheckinPoNo').text('');
                                screen.$view.find('.pageexchange .valueCheckInWarehouse').text('');
                                FwFunc.showMessage(response.getiteminfo.msg);
                            }
                            else {
                                screen.$view.find('.pageexchange .valueOrderNo').text(response.getiteminfo.iteminfo.returnitemorderno);
                                screen.$view.find('.pageexchange .valueOrder').text(response.getiteminfo.iteminfo.returnitemorderdesc);
                                screen.$view.find('.pageexchange .valueICode').text(response.getiteminfo.iteminfo.returnitemmasterno);
                                screen.$view.find('.pageexchange .valueItem').text(response.getiteminfo.iteminfo.returnitemdescription);
                                screen.$view.find('.pageexchange .valueCheckInSubRentalVendor').text(response.getiteminfo.iteminfo.returnitemvendor);
                                screen.$view.find('.pageexchange .valueCheckinPoNo').text(response.getiteminfo.iteminfo.returnitempono);
                                screen.$view.find('.pageexchange .valueCheckInWarehouse').text(response.getiteminfo.iteminfo.returnitemwarehouse);
                                if (response.getiteminfo.iteminfo.trackedby !== 'QUANTITY') {
                                    screen.setqty(1);
                                    FwFormField.disable(screen.$view.find('.exchangeqty'));
                                }
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } else {
                    screen.$view.find('.pageexchange .valueOrderNo').text('');
                    screen.$view.find('.pageexchange .valueOrder').text('');
                    screen.$view.find('.pageexchange .valueICode').text('');
                    screen.$view.find('.pageexchange .valueItem').text('');
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('keypress', '.checkout .fwformfield-value', function(e) {
            if (e.which === 13) {
                e.preventDefault();
                jQuery(this).blur();
            }
        })
        .on('blur', '.checkout .fwformfield-value', function() {
            try {
                if (this.value.length > 0) {
                    //screen.exchangeItem();
                    screen.validDlgInNonBc();
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.getexchange = function() {
        var exchange = screen.exchange;
        exchange.exchangecontractid = screen.getexchangecontractid();
        exchange.frmInExchangeItem.barcode = screen.getinbarcode();
        exchange.frmOutExchangeItem.barcode = screen.getoutbarcode();
        exchange.qty = screen.getqty();
        exchange.completingpending = screen.getcompletingpending();
        exchange.orderid = screen.getorderid();
        exchange.dealid = screen.getdealid();
        exchange.departmentid = screen.getdepartmentid();
        //exchange.allowcrossicode = screen.getallowcrossicode(); // TODO: need to program
        return exchange;
    };

    screen.setexchange = function(exchange) {
        screen.exchange = exchange;
    };

    screen.clearInItem = function() {
       screen.setinbarcode('');
        screen.$view.find('.pageexchange .valueCheckInSubRentalVendor').text('');
        screen.$view.find('.pageexchange .valueCheckinPoNo').text('');
        screen.$view.find('.pageexchange .valueCheckInWarehouse').text('');
    };

    screen.clearOutItem = function() {
        screen.setoutbarcode('');
        screen.$view.find('.pageexchange .valueCheckOutSubRentalVendor').text('');
        screen.$view.find('.pageexchange .valueCheckOutPoNo').text('');
        screen.$view.find('.pageexchange .valueCheckOutWarehouse').text('');
    };

    screen.exchangeItem = function() {
        var request = {
            exchange: screen.getexchange()
        };
        RwServices.callMethod('Exchange', 'ExchangeItem', request, function(response) {
            screen.exchangeItemResponse
            try {
                screen.exchangeItemResponse(response);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.exchangeItemResponse = function(response) {
        var exchange = response.exchange;
        screen.setexchange(exchange);
        program.playStatus(exchange.response.success);
        if (exchange.response.resetall) {
            screen.resetall();
        }
        else if (excehange.fromInExchangeItem.clearitem) {
            //FwFunc.showError('Clear In Item: Not Implemented!');
            screen.clearInItem();
        }
        else if (excehange.fromOutExchangeItem.clearitem) {
            //FwFunc.showError('Clear Out Item: Not Implemented!');
            screen.clearOutItem();
        }
        else {
            // show buttons
            if (excehange.fromInExchangeItem.showbtnremovefromcontainer) {
                FwFunc.showError('Show btnremovefrom container: Not Implemented!');
            }
            if (excehange.fromOutExchangeItem.showbtnremovefromcontainer) {
                FwFunc.showError('Show btnremovefrom container: Not Implemented!');
            }
            
            // show dialogs
            if (exchange.response.confirmallowcrossicode) {
                screen.confirmAllowCrossICode();
            }
            else if (exchange.response.confirmallowcrosswarehouse) {
                screen.confirmAllowCrossWarehouse();
            }
            else if (exchange.validDlgType !== 'None') {
                switch(exchange.validDlgType) {
                    case 'InNonBC':
                        screen.validDlgInNonBc();
                        break;
                    case 'InSerial':
                        screen.validDlgInSerial();
                        break;
                    case 'OutNonBc':
                        screen.validDlgOutNonBc();
                        break;
                    case 'PendingInBC':
                        screen.validDlgPendingInBC();
                        break;
                    case 'PendingInNonBc':
                        screen.validDlgPendingInNonBc();
                        break;
                    case 'PendingOutNonBc':
                        screen.validDlgPendingOutNonBc();
                        break;
                    case 'PendingOutSerial':
                        screen.validDlgPendingOutSerial();
                        break;
                }
            }
        }
    };

    screen.confirmAllowCrossICode = function() {
        FwConfirmation.yesNo('Confirm', 'Allow Cross I-Code Exchange?', 
            function onyes() {
                try {
                    var exchange = screen.getexchange();
                    screen.allowcrossicode = true;
                    screen.exchangeItem();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }, 
            function onno() {
                try {
                        
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        );
    };

    screen.confirmAllowCrossWarehouse = function() {
        FwConfirmation.yesNo('Confirm', 'Allow Cross Warehouse Exchange?', 
            function onyes() {
                try {
                    var exchange = screen.getexchange();
                    exchange.allowcrosswarehouse = true;
                    screen.exchangeItem();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }, 
            function onno() {
                try {
                        
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        );
    };

    screen.setupOrderSearch = function() {
        var $search = screen.$view.find('.ordersearch').fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'OrderSearch',
            queryTimeout: 30,
            searchModes:  [
                {caption:'Order', value:'orderdesc'},
                {caption:'Order No', value:'orderno'},
                {caption:'Deal', value:'deal'},
                {caption:'Scan Barcode', value:'barcode'}
            ],
            cacheItemTemplate: true,
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item">');
                html.push('  <div class="col1">');
                html.push('    <div class="flexrow">');
                html.push('      <div class="valueDescription">{{orderdesc}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Deal') + ':</div>');
                html.push('      <div class="value">{{deal}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('      <div class="value">{{orderno}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Order Date') + ':</div>');
                html.push('      <div class="value">{{orderdate}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Status') + ':</div>');
                html.push('      <div class="value">{{status}} - {{statusdate}}</div>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <i class="material-icons">&#xE5CC;</i>'); //chevron_right
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            beforeSearch: function() {
                //screen.showScanBarcodeScreen(true);
            },
            recordClick: function(model) {
                try {
                    screen.setorderid(model.orderid);
                    screen.setorderdesc(model.orderdesc);
                    screen.setorderno(model.orderno);
                    screen.setdealid(model.dealid);
                    screen.setdealno(model.dealno);
                    screen.setdeal(model.deal);
                    screen.setdepartmentid(model.departmentid);
                    screen.setdepartment(model.department);
                    var requestSuspendedSessionSearch = {
                        pageno: 0,
                        pagesize: 0,
                        orderid: model.orderid
                    };
                    RwServices.callMethod('Exchange', 'SuspendedSessionSearch', requestSuspendedSessionSearch, function(responseSuspendedSessionSearch) {
                        try {
                            if (responseSuspendedSessionSearch.searchresults.Rows.length === 0) {
                                var requestCreateExchangeContract = {
                                    orderid: screen.getorderid(),
                                    dealid: screen.getdealid(),
                                    departmentid: screen.getdepartmentid()
                                };
                                RwServices.callMethod('Exchange', 'CreateExchangeContract', requestCreateExchangeContract, function(responseCreateExchangeContract) {
                                    try {
                                        screen.setexchangecontractid(responseCreateExchangeContract.createexchangecontract.exchangecontractid);
                                        screen.setsessionno(responseCreateExchangeContract.createexchangecontract.sessionno);
                                        screen.pages.exchange.show();
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } else {
                                screen.pages.selectsuspendedsession.show();
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
    };

    screen.setupDealSearch = function() {
        var $search = screen.$view.find('.dealsearch').fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'DealSearch',
            queryTimeout: 30,
            searchModes:  [
                {caption:'Deal', value:'deal'},
                {caption:'Deal No', value:'dealno'},
                {caption:'Customer', value:'customer'}
            ],
            cacheItemTemplate: true,
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item">');
                html.push('  <div class="col1">');
                html.push('    <div class="flexrow">');
                html.push('      <div class="valueDeal">{{deal}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Deal No') + ':</div>');
                html.push('      <div class="value">{{dealno}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Customer') + ':</div>');
                html.push('      <div class="value">{{customer}}</div>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <i class="material-icons">&#xE5CC;</i>'); //chevron_right
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            beforeSearch: function() {
                //screen.showScanBarcodeScreen(true);
            },
            recordClick: function(model) {
                try {
                    screen.setexchangecontractid('');
                    screen.setorderid('');
                    screen.setorderno('');
                    screen.setorderdesc('');
                    screen.setdealid(model.dealid);
                    screen.setdealno(model.dealno);
                    screen.setdeal(model.deal);
                    screen.setdepartmentid('');
                    screen.setdepartment('');
                    screen.setsessionno('');
                    var requestSuspendedSessionSearch = {
                        pageno: 0,
                        pagesize: 0,
                        dealid: model.dealid
                    };
                    RwServices.callMethod('Exchange', 'SuspendedSessionSearch', requestSuspendedSessionSearch, function(responseSuspendedSessionSearch) {
                        try {
                            if (responseSuspendedSessionSearch.searchresults.Rows.length === 0) {
                                var requestCreateExchangeContract = {
                                    orderid: screen.getorderid(),
                                    dealid: screen.getdealid(),
                                    departmentid: screen.getdepartmentid()
                                };
                                RwServices.callMethod('Exchange', 'CreateExchangeContract', requestCreateExchangeContract, function(responseCreateExchangeContract) {
                                    try {
                                        screen.setexchangecontractid(responseCreateExchangeContract.createexchangecontract.exchangecontractid);
                                        screen.setsessionno(responseCreateExchangeContract.createexchangecontract.sessionno);
                                        screen.pages.exchange.show();
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } else {
                                screen.pages.selectsuspendedsession.show();
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
    };

    screen.setupCompanyDepartmentSearch = function() {
        var $search = screen.$view.find('.companydepartmentsearch').fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'CompanyDepartmentSearch',
            queryTimeout: 30,
            searchModes:  [
                {caption:'Dept', value:'department'},
                {caption:'Dept Code', value:'deptcode'}
            ],
            cacheItemTemplate: true,
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item">');
                html.push('  <div class="col1">');
                html.push('    <div class="flexrow">');
                html.push('      <div class="valueDepartment">{{department}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Dept Code') + ':</div>');
                html.push('      <div class="value">{{deptcode}}</div>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <i class="material-icons">&#xE5CC;</i>'); //chevron_right
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            beforeSearch: function() {
                //screen.showScanBarcodeScreen(true);
            },
            recordClick: function(model) {
                try {
                    screen.setexchangecontractid('');
                    screen.setorderid('');
                    screen.setorderno('');
                    screen.setorderdesc('');
                    screen.setdealid('');
                    screen.setdealno('');
                    screen.setdeal('');
                    screen.setdepartmentid(model.departmentid);
                    screen.setdepartment(model.department);
                    screen.setsessionno('');
                    var requestSuspendedSessionSearch = {
                        pageno: 0,
                        pagesize: 0,
                        departmentid: model.departmentid
                    };
                    RwServices.callMethod('Exchange', 'SuspendedSessionSearch', requestSuspendedSessionSearch, function(responseSuspendedSessionSearch) {
                        try {
                            if (responseSuspendedSessionSearch.searchresults.Rows.length === 0) {
                                var requestCreateExchangeContract = {
                                    orderid: screen.getorderid(),
                                    dealid: screen.getdealid(),
                                    departmentid: screen.getdepartmentid()
                                };
                                RwServices.callMethod('Exchange', 'CreateExchangeContract', requestCreateExchangeContract, function(responseCreateExchangeContract) {
                                    try {
                                        screen.setexchangecontractid(responseCreateExchangeContract.createexchangecontract.exchangecontractid);
                                        screen.setsessionno(responseCreateExchangeContract.createexchangecontract.sessionno);
                                        screen.pages.exchange.show();
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } else {
                                screen.pages.selectsuspendedsession.show();
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
    };

    screen.setupPendingExchangeSearch = function() {
        var $search = screen.$view.find('.pendingexchangesearch').fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'PendingExchangeSearch',
            queryTimeout: 30,
            searchModes:  [
                {caption:'Order', value:'orderdesc'},
                {caption:'Order No', value:'orderno'},
                {caption:'Deal', value:'deal'}
            ],
            cacheItemTemplate: true,
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item">');
                html.push('  <div class="col1">');
                html.push('    <div class="flexrow">');
                html.push('      <div class="valueDescription">{{orderdesc}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Session No') + ':</div>');
                html.push('      <div class="value">{{sessionno}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Date') + ':</div>');
                html.push('      <div class="value">{{contractdate}} {{contracttime}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('      <div class="value">{{orderdate}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Deal') + ':</div>');
                html.push('      <div class="value">{{deal}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Department') + ':</div>');
                html.push('      <div class="value">{{department}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Warehouse') + ':</div>');
                html.push('      <div class="value">{{warehouse}}</div>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <i class="material-icons">&#xE5CC;</i>'); //chevron_right
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            beforeSearch: function() {
                //screen.showScanBarcodeScreen(true);
            },
            recordClick: function(model) {
                try {
                    screen.setexchangecontractid(model.contractid);
                    screen.setorderid(model.orderid);
                    screen.setorderno(model.orderno);
                    screen.setorderdesc(model.orderdesc);
                    screen.setdealid(model.dealid);
                    screen.setdealno(model.dealno);
                    screen.setdeal(model.deal);
                    screen.setdepartmentid(model.departmentid);
                    screen.setdepartment(model.department);
                    screen.setsessionno(model.sessionno);
                    screen.setcompletingpending(true);
                    screen.pages.exchange.pageheader = RwLanguages.translate('Pending Exchange') + ': ' + model.sessionno;
                    screen.pages.exchange.show();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
    };

    screen.setupSuspendedSessionSearch = function() {
        var $search = screen.$view.find('.suspendedsessionsearch').fwmobilesearch({
            getRequest: function() {
                var request = {};
                if (screen.getExchangeMode() === screen.modes.order) {
                    request.orderid = screen.getorderid();
                }
                else if (screen.getExchangeMode() === screen.modes.deal) {
                    request.dealid = screen.getdealid();
                }
                else if (screen.getExchangeMode() === screen.modes.companydepartment) {
                    request.departmentid = screen.getdepartmentid();
                }
                return request;
            },
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'SuspendedSessionSearch',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item" data-status="{{status}}">');
                html.push('  <div class="col1">');
                //html.push('    <div class="flexrow">');
                //html.push('      <div class="valueOrderDesc">{{orderdesc}}</div>');
                //html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Session No') + ':</div>');
                html.push('      <div class="value">{{sessionno}}</div>');
                html.push('    </div>');
                if (model.orderid.length > 0) {
                    html.push('    <div class="flexrow">');
                    html.push('      <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                    html.push('      <div class="value">{{orderno}}</div>');
                    html.push('    </div>');
                    html.push('    <div class="flexrow">');
                    html.push('      <div class="caption">' + RwLanguages.translate('Order') + ':</div>');
                    html.push('      <div class="value">{{orderdesc}}</div>');
                    html.push('    </div>');
                }
                if (model.dealid.length > 0) {
                    html.push('    <div class="flexrow">');
                    html.push('      <div class="caption">' + RwLanguages.translate('Deal No') + ':</div>');
                    html.push('      <div class="value">{{dealno}}</div>');
                    html.push('    </div>');
                    html.push('    <div class="flexrow">');
                    html.push('      <div class="caption">' + RwLanguages.translate('Deal / Vendor') + ':</div>');
                    html.push('      <div class="value">{{deal}}</div>');
                    html.push('    </div>');
                }
                if (model.departmentid.length > 0) {
                    html.push('    <div class="flexrow">');
                    html.push('      <div class="caption">' + RwLanguages.translate('Department') + ':</div>');
                    html.push('      <div class="value">{{department}}</div>');
                    html.push('    </div>');
                }
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('User') + ':</div>');
                html.push('      <div class="value">{{username}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Status') + ':</div>');
                html.push('      <div class="value valueStatus">{{status}} - {{statusdate}}</div>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <i class="material-icons">&#xE5CC;</i>'); //chevron_right
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    screen.setexchangecontractid(model.contractid);
                    screen.setorderid(model.orderid);
                    screen.setorderno(model.orderno);
                    screen.setorderdesc(model.orderdesc);
                    screen.setdealid(model.dealid);
                    screen.setdealno(model.dealno);
                    screen.setdeal(model.deal);
                    screen.setdepartmentid(model.departmentid);
                    screen.setdepartment(model.department);
                    screen.setsessionno(model.sessionno);
                    screen.pages.exchange.show();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
    };

    screen.setupExchangeSessionSearch = function() {
        var $search = screen.$view.find('.exchangesessionsearch');
        //$search.fwmobilesearch('destroy');
        $search.fwmobilesearch({
            getRequest: function() {
                var request = {
                    exchangecontractid: screen.getexchangecontractid(),
                    pendingonly: screen.getcompletingpending()
                };
                return request;
            },
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ExchangeSessionSearch',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            itemTemplate: function(model) {
                var html = [];
                var completedpendingexchange = screen.getcompletingpending() && (model.itemstatus === 'O' || model.itemstatus === 'I') ? 'T' : 'F';
                html.push('<div class="item" data-itemstatus="{{itemstatus}}" data-completedpendingexchange="' + completedpendingexchange + '" data-torepair="{{torepair}}" data-multiwarehouse="{{multiwarehouse}}">');
                html.push('  <div class="col1">');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionDescription">' + RwLanguages.translate('Item Desc') + ':</div>');
                html.push('      <div class="valueDescription">{{description}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionQty">' + RwLanguages.translate('Qty') + ':</div>');
                html.push('      <div class="valueQty">{{qty}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionDirectionDisplay">' + RwLanguages.translate('Direction') + ':</div>');
                html.push('      <div class="valueDepartment">{{directiondisplay}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionMasterNo">' + RwLanguages.translate('I-Code') + ':</div>');
                html.push('      <div class="valueMasterNo">{{masterno}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionBarcode">' + RwLanguages.translate('BC / Serial') + ':</div>');
                html.push('      <div class="valueBarcode">{{barcode}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionOrderNo">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('      <div class="valueOrderNo">{{orderno}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionOrderDesc">' + RwLanguages.translate('Order Desc') + ':</div>');
                html.push('      <div class="valueOrderDesc">{{orderdesc}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionVendor">' + RwLanguages.translate('Vendor') + ':</div>');
                html.push('      <div class="valueVendor">{{vendor}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionWH">' + RwLanguages.translate('W/H') + ':</div>');
                html.push('      <div class="valueWH">{{whcode}}</div>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <i class="material-icons">&#xE5D4;</i>'); //more_vert
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    var $contextmenu = FwContextMenu.render('');
                    FwContextMenu.addMenuItem($contextmenu, 'Send to Repair', function() {
                        FwFunc.showMessage('Not implemented!');
                        //alert(JSON.stringify(model));
                    });
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
    };

    screen.setupExchangeRepairSearch = function() {
        var $search = screen.$view.find('.exchangerepairsearch');
        //$search.fwmobilesearch('destroy');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ExchangeRepairSearch',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchangecontractid: screen.getexchangecontractid()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item">');
                html.push('  <div class="col1">');
                html.push('    <div class="flexrow">');
                html.push('      <div class="valueDescription">{{master}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionQty">' + RwLanguages.translate('Repair Qty') + ':</div>');
                html.push('      <div class="valueQty">{{qty}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionDirectionDisplay">' + RwLanguages.translate('I-Code') + ':</div>');
                html.push('      <div class="valueDepartment">{{masterno}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionMasterNo">' + RwLanguages.translate('Barcode') + ':</div>');
                html.push('      <div class="valueMasterNo">{{barcode}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="captionBarcode">' + RwLanguages.translate('Serial No') + ':</div>');
                html.push('      <div class="valueBarcode">{{mfgserial}}</div>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <i class="material-icons">&#xE5D4;</i>'); //more_vert
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
    };

    screen.setupExchangeTransferSearch = function() {
        var $search = screen.$view.find('.exchangetransfersearch');
        //$search.fwmobilesearch('destroy');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ExchangeTransferSearch',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchangecontractid: screen.getexchangecontractid()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item">');
                html.push('  <div class="col1">');
                html.push('    <div class="flexrow">');
                html.push('      <div class="valueDescription">{{master}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('I-Code') + ':</div>');
                html.push('      <div class="value">{{masterno}}</div>');
                html.push('    </div>');
                html.push('      <div class="caption">' + RwLanguages.translate('Barcode') + ':</div>');
                html.push('      <div class="value">{{orderno}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Qty') + ':</div>');
                html.push('      <div class="value">{{orderdesc}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('      <div class="value">{{orderno}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('Order Desc') + ':</div>');
                html.push('      <div class="value">{{orderdesc}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('From Warehouse') + ':</div>');
                html.push('      <div class="value">{{fromwhcode}}</div>');
                html.push('    </div>');
                html.push('    <div class="flexrow">');
                html.push('      <div class="caption">' + RwLanguages.translate('To Warehouse') + ':</div>');
                html.push('      <div class="value">{{towhcode}}</div>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <i class="material-icons">&#xE5D4;</i>'); //more_vert
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
    };

    screen.validDlgInNonBc = function() {
        var $confirmation = FwConfirmation.renderConfirmation('Select...', '');
        FwConfirmation.addButton($confirmation, 'Cancel', true);
        var $search = $confirmation.find('.body');
        $search.addClass('exchangevaliddlg');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ValidDlgInNonBc',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchange: screen.getexchange()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item">');
                html.push('  <div class="flexrow">');
                html.push('    <div class="caption">' + RwLanguages.translate('Item Desc') + ':</div>');
                html.push('    <div class="value">' + model.description + '</div>');
                html.push('  </div>');
                html.push('  <div class="flexrow">');
                html.push('    <div class="caption">' + RwLanguages.translate('I-Code') + ':</div>');
                html.push('    <div class="value">' +model.masterno + '</div>');
                html.push('  </div>');
                html.push('  <div class="flexrow">');
                html.push('    <div class="caption">' + RwLanguages.translate('Qty Ordered') + ':</div>');
                html.push('    <div class="value">' + model.qtyordered + '</div>');
                html.push('  </div>');
                html.push('  <div class="flexrow">');
                html.push('    <div class="caption">' + RwLanguages.translate('Qty') + ':</div>');
                html.push('    <div class="value">' + model.qtyout + '</div>');
                html.push('  </div>');
                html.push('  <div class="flexrow">');
                html.push('    <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                html.push('    <div class="value">' + model.orderno + '</div>');
                html.push('  </div>');
                html.push('  <div class="flexrow">');
                html.push('    <div class="caption">' + RwLanguages.translate('Order Desc') + ':</div>');
                html.push('    <div class="value">' + model.orderdesc + '</div>');
                html.push('  </div>');
                html.push('  <div class="flexrow">');
                html.push('    <div class="caption">' + RwLanguages.translate('Vendor') + ':</div>');
                html.push('    <div class="value">' + model.vendor + '</div>');
                html.push('  </div>');
                html.push('  <div class="flexrow">');
                html.push('    <div class="caption">' + RwLanguages.translate('W/H') + ':</div>');
                html.push('    <div class="value">' + model.whcode + '</div>');
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                    //$search.fwmobilesearch('destroy');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $search.fwmobilesearch('search');
    };

    screen.validDlgInSerial = function() {
        var $confirmation = FwConfirmation.renderConfirmation('Select...', '');
        FwConfirmation.addButton($confirmation, 'Cancel', true);
        var $search = $confirmation.find('.body');
        $search.addClass('exchangevaliddlg');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'validDlgInSerial',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchange: screen.getexchange()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push(JSON.stringify(model));
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                    //$search.fwmobilesearch('destroy');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $search.fwmobilesearch('search');
    };

    screen.validDlgOutNonBc = function() {
        var $confirmation = FwConfirmation.renderConfirmation('Select...', '');
        FwConfirmation.addButton($confirmation, 'Cancel', true);
        var $search = $confirmation.find('.body');
        $search.addClass('exchangevaliddlg');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ValidDlgOutNonBc',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchange: screen.getexchange()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push(JSON.stringify(model));
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                    //$search.fwmobilesearch('destroy');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $search.fwmobilesearch('search');
    };

    screen.validDlgPendingInBC = function() {
        var $confirmation = FwConfirmation.renderConfirmation('Select...', '');
        FwConfirmation.addButton($confirmation, 'Cancel', true);
        var $search = $confirmation.find('.body');
        $search.addClass('exchangevaliddlg');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ValidDlgPendingInBC',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchange: screen.getexchange()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push(JSON.stringify(model));
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                    //$search.fwmobilesearch('destroy');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $search.fwmobilesearch('search');
    };

    screen.validDlgPendingInNonBc = function() {
        var $confirmation = FwConfirmation.renderConfirmation('Select...', '');
        FwConfirmation.addButton($confirmation, 'Cancel', true);
        var $search = $confirmation.find('.body');
        $search.addClass('exchangevaliddlg');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ValidDlgPendingInNonBc',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchange: screen.getexchange()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push(JSON.stringify(model));
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                    //$search.fwmobilesearch('destroy');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $search.fwmobilesearch('search');
    };

    screen.validDlgPendingOutNonBc = function() {
        var $confirmation = FwConfirmation.renderConfirmation('Select...', '');
        FwConfirmation.addButton($confirmation, 'Cancel', true);
        var $search = $confirmation.find('.body');
        $search.addClass('exchangevaliddlg');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ValidDlgPendingOutNonBc',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchange: screen.getexchange()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push(JSON.stringify(model));
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                    //$search.fwmobilesearch('destroy');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $search.fwmobilesearch('search');
    };

    screen.validDlgPendingOutSerial = function() {
        var $confirmation = FwConfirmation.renderConfirmation('Select...', '');
        FwConfirmation.addButton($confirmation, 'Cancel', true);
        var $search = $confirmation.find('.body');
        $search.addClass('exchangevaliddlg');
        $search.fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase:    true,
            service: 'Exchange', 
            method: 'ValidDlgPendingOutSerial',
            queryTimeout: 30,
            searchModes:  [],
            cacheItemTemplate: false,
            getRequest: function() {
                var request = {
                    exchange: screen.getexchange()
                };
                return request;
            },
            itemTemplate: function(model) {
                var html = [];
                html.push(JSON.stringify(model));
                html = html.join('\n');
                return html;
            },
            recordClick: function(model) {
                try {
                    FwFunc.showMessage('Not implemented!');
                    //$search.fwmobilesearch('destroy');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $search.fwmobilesearch('search');
    };

    screen.load = function() {
        program.setScanTarget('');
        if (typeof window.DTDevices !== 'undefined') {
            window.DTDevices.registerListener('barcodeData', 'barcodeData_exchange', function(barcode, barcodeType) {
                var currentpage = screen.getCurrentPage();
                switch(currentpage) {
                    case 'selectorder':
                        screen.$view.find('.ordersearch').fwmobilesearch('setsearchmode', 'scanbarcode');
                        screen.$view.find('.ordersearch .searchbox').val(barcode).blur().change();
                        break;
                    case 'exchange':
                        screen.$view.find('.pageexchange .scantarget').val(barcode).blur().change();
                        break;
                }
            });
        }
        screen.pages.exchangeby.show();
        screen.setupOrderSearch();
        screen.setupDealSearch();
        screen.setupCompanyDepartmentSearch();
        screen.setupPendingExchangeSearch();
        screen.setupSuspendedSessionSearch();
        screen.setupExchangeSessionSearch();
        screen.setupExchangeRepairSearch();
        screen.setupExchangeTransferSearch();
    };

    screen.unload = function() {
        if (typeof window.DTDevices !== 'undefined') {
            window.DTDevices.unregisterListener('barcodeData', 'barcodeData_exchange');
        }
        screen.$view.find('.ordersearch').fwmobilesearch('destroy');
        screen.$view.find('.dealsearch').fwmobilesearch('destroy');
        screen.$view.find('.companydepartmentsearch').fwmobilesearch('destroy');
        screen.$view.find('.pendingexchangeordersearch').fwmobilesearch('destroy');
        screen.$view.find('.suspendedsessionsearch').fwmobilesearch('destroy');
        screen.$view.find('.exchangesessionsearch').fwmobilesearch('destroy');
        screen.$view.find('.exchangerepairsearch').fwmobilesearch('destroy');
        screen.$view.find('.exchangetransfersearch').fwmobilesearch('destroy');
    };

    screen.beforeNavigateAway = function(navigateAway) {
        if (!screen.getcompletingpending()) {
            var request = {
                exchangecontractid: screen.getexchangecontractid()
            };
            if (typeof request.exchangecontractid === 'string' && request.exchangecontractid.length > 0) {
                FwConfirmation.yesNo('Cancel Contract?', 'Cancel Exchange Contract?', 
                    function onyes() {
                        try {
                            RwServices.callMethod('Exchange', 'CancelContract', request, function(response) {
                                navigateAway();
                            });
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    }, 
                    function onno() {
                        try {
                            //do nothing
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    }
                );
            } else {
                navigateAway();
            }
        }
    };

    return screen;
};