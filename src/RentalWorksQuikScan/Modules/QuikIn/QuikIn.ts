class QuikInClass {
    getModuleScreen(viewModel, properties): void {
        var combinedViewModel = jQuery.extend({
            captionPageTitle:   RwLanguages.translate('QuikIn'),
            htmlScanBarcode:    RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Bar Code / I-Code')})
        
        }, viewModel);
        combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-quikIn').html(), combinedViewModel);
        var screen: any = {};
        screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);

        screen.$view.find('#scanBarcodeView').hide();

        screen.$view.find('#scanBarcodeView-txtBarcodeData').on('change', (e: JQuery.ClickEvent) => {
            try {
                var $this = jQuery(e.currentTarget);
                if ((<string>$this.val()).length > 0) {
                    var currentPage = screen.getCurrentPage();
                    if (currentPage.name === screen.pages.quikinsessionsearch.name) {
                        screen.pdaSelectSession();
                    } else 
                    if (currentPage.name === screen.pages.scanbarcodes.name) {
                        screen.QuikInAddItem(screen.getBarCode(), -1);
                    }
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        screen.contractId = null;
        screen.setContractId = (contractId: string) => {
            screen.contractId = contractId;
        }
        screen.getContractId = () => {
            if (screen.contractId === null) {
                throw 'ContractId is null!'
            }
            return screen.contractId;
        };

        screen.sessionNo = null;
        screen.setSessionNo = (sessionNo: string) => {
            screen.sessionNo = sessionNo;
        }
        screen.getSessionNo = () => {
            if (screen.sessionNo === null) {
                throw 'Session No is null!'
            }
            return screen.sessionNo;
        };

        screen.deal = null;
        screen.setDeal = (deal: string) => {
            screen.deal = deal;
        }
        screen.getDeal = () => {
            if (screen.deal === null) {
                throw 'Deal is null!'
            }
            return screen.deal;
        };

        screen.getBarCode = () => {
            return screen.$view.find('#scanBarcodeView-txtBarcodeData').val();
        };

        screen.setBarCode = (code) => {
            return screen.$view.find('#scanBarcodeView-txtBarcodeData').val(code).change();
        };

        screen.$modulecontrol = screen.$view.find('.modulecontrol');
        screen.$modulecontrol.fwmobilemodulecontrol({
            buttons: [
                {
                    id: 'quikinsessionsearch-close',
                    caption:     'Close',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       'quikinsessionsearch',
                    buttonclick: function () {
                        try {
                            // intead of program.popScreen(), using this workaround to support the fact that the CheckInMenu screen doesn't support going back to it.  The screen is blank.
                            program.screens = [];
                            program.navigate('order/checkinmenu');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id: 'quikinsessionsearch-newsession',
                    caption:     'New Session',
                    orientation: 'right',
                    icon:        '&#xE145;', //chevron_left
                    state:       'quikinsessionsearch',
                    isVisible: function () {
                        return false;
                    },
                    buttonclick: function () {
                        try {
                            screen.pages.scanbarcodes.forward();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }

                    }
                },
                {
                    id: 'scanbarcodes-back',
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       'scanbarcodes',
                    buttonclick: function () {
                        try {
                            screen.pages.scanbarcodes.back();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }
            ]
        });

        screen.pagehistory = [];
        screen.getCurrentPage = function () {
            return screen.pagehistory[screen.pagehistory.length - 1];
        };
        screen.pages = {
            reset: function () {
                screen.setBarCode('');
                screen.$view.find('#scanBarcodeView').hide();
                screen.pages.quikinsessionsearch.getElement().hide();
                screen.pages.scanbarcodes.getElement().hide();
                screen.pages.quikinsessionsearch.getElement().find('.quikinsessionsearch').hide();
                screen.pages.scanbarcodes.getElement().find('.pdaquikinitem').html('');
                screen.pages.scanbarcodes.getElement().find('.quikinsessionsearch').fwmobilesearch('clearsearchresults');
                screen.pages.scanbarcodes.getElement().find('.sessioninsearch').fwmobilesearch('clearsearchresults');
            },

            // Page: Select Session No
            quikinsessionsearch: {
                name: 'quikinsessionsearch',
                getElement: function() {
                    return screen.$view.find('.page-quikinsessionsearch');
                },
                show: function () {
                    screen.pages.reset();
                    screen.setContractId('');
                    screen.setSessionNo('');
                    screen.setDeal('');
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    FwMobileMasterController.setTitle('Select a Session...');
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', 'quikinsessionsearch');
                    screen.$view.find('#scanBarcodeView-txtBarcodeData').attr('placeholder', 'SESSION NO');
                    screen.$view.find('#scanBarcodeView').show();
                    if (applicationConfig.quikIn.enableQuikInSessionSearch) {
                        screen.pages.quikinsessionsearch.getElement().find('.quikinsessionsearch').fwmobilesearch('search');
                        screen.pages.quikinsessionsearch.getElement().find('.quikinsessionsearch').show();
                    }
                    screen.pages.quikinsessionsearch.getElement().show();
                },
                forward: function () {
                    screen.pagehistory.push(screen.pages[this.name]);
                    screen.pages[this.name].show();
                }
                //,back: function () {

                //}
            },

            // Page: Scan Bar Codes
            scanbarcodes: {
                name: 'scanbarcodes',
                getElement: function() {
                    return screen.$view.find('.page-scanbarcodes');
                },
                show: function () {
                    screen.pages.reset();
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    FwMobileMasterController.setTitle(screen.getSessionNo() + ' - ' + screen.getDeal());
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', 'scanbarcodes');
                    screen.$view.find('#scanBarcodeView-txtBarcodeData').attr('placeholder', 'BARCODE / SERIAL NO / I-CODE');
                    screen.$view.find('#scanBarcodeView').show();
                    if (applicationConfig.quikIn.enableSessionInItemSearch) {
                        screen.pages.scanbarcodes.getElement().find('.sessioninsearch').fwmobilesearch('search');
                        screen.pages.scanbarcodes.getElement().find('.sessioninsearch').show();
                    }
                    screen.pages.scanbarcodes.getElement().show();
                },
                forward: function () {
                    screen.pagehistory.push(screen.pages[this.name]);
                    screen.pages[this.name].show();
                },
                back: function () {
                    screen.pagehistory.pop();
                    screen.getCurrentPage().show();
                }
            }
        }


        screen.$pageQuikInSessionSearch = screen.$view.find('.page-quikinsessionsearch');
        
        screen.$pageQuikInSessionSearch.find('.quikinsessionsearch').fwmobilesearch({
            service: 'QuikIn',
            method:  'QuikInSessionSearch',
            getRequest: function() {
                var request = {
                    contractId: screen.getContractId()
                };
                return request;
            },
            cacheItemTemplate: false,
            itemTemplate: function(model) {
                var html = [];
                html.push('<div class="item">');
                //html.push('  <div class="row1"><span class="orderdesc">{{orderdesc}}</span></div>');
                html.push('  <div class="row2">');
                html.push('    <div class="col1">');
                if (typeof model.deal !== 'undefined') {
                    html.push('      <div class="datafield deal">')
                    html.push('        <div class="caption">' + RwLanguages.translate('Deal') + ':</div>');
                    html.push('        <div class="value">{{deal}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.sessionno !== 'undefined') {
                    html.push('      <div class="datafield sessionno">')
                    html.push('        <div class="caption">' + RwLanguages.translate('Session No') + ':</div>');
                    html.push('        <div class="value">{{sessionno}}</div>');
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
                if (typeof model.username !== 'undefined') {
                    html.push('      <div class="datafield username">')
                    html.push('        <div class="caption">' + RwLanguages.translate('Owner') + ':</div>');
                    html.push('        <div class="value">{{username}}</div>');
                    html.push('      </div>');
                }
                html.push('    </div>');
                html.push('  </div>');
                html.push('</div>');
                var htmlString = html.join('\n');
                return htmlString;
            },
            recordClick: function(recorddata, $record) {
                try { 
                    screen.setContractId(recorddata.contractid);
                    screen.setSessionNo(recorddata.sessionno);
                    screen.setDeal(recorddata.deal);
                    screen.pages.scanbarcodes.forward();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            afterLoad: function(plugin, response) {

            }
        });

        screen.pdaSelectSession = () => {
            var request = {
                sessionno: screen.getBarCode()
            };
            RwServices.callMethod('QuikIn', 'PdaSelectSession', request, function (response) {
                try {
                    program.playStatus(response.status === 0);
                    if (response.status === 0) {
                        screen.setContractId(response.contractId);
                        screen.setSessionNo(request.sessionno);
                        screen.setDeal(response.deal);
                        screen.pages.scanbarcodes.forward();
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        };

        screen.QuikInAddItem = (code:string, qty: number) => {
            var requestQuikInAddItem = {
                code: code,
                contractId: screen.getContractId(),
                qty: qty
            };
            RwServices.callMethod('QuikIn', 'QuikInAddItem', requestQuikInAddItem, function (responsePdaQuikIn: any) {
                try {
                    screen.setBarCode('');
                    program.playStatus(responsePdaQuikIn.status === 0);
                    if (responsePdaQuikIn.action === 'ACTION_QUIKINADDITEM' && applicationConfig.quikIn.enableSessionInItemSearch) {
                        screen.pages.scanbarcodes.getElement().find('.sessioninsearch').fwmobilesearch('search');
                    }
                    var html = [];

                    html.push('<div class="Messages">');
                    if (responsePdaQuikIn.status !== 0 && responsePdaQuikIn.msg.length > 0) {
                        html.push('  <div class="ErrorMessage">' + responsePdaQuikIn.msg + '</div>');
                    }
                    else if (responsePdaQuikIn.action !== 'ACTION_PROMPT_FOR_QTY') {
                        if (responsePdaQuikIn.msg.length > 0) {
                            html.push('  <div class="SuccessMessage">' + responsePdaQuikIn.msg + '</div>');
                        }
                        
                    }
                    html.push('</div>');
                    
                    html.push('<div class="ItemInfo">');
                    if (responsePdaQuikIn.trackedby !== 'QUANTITY') {
                        html.push('  <div class="RowBarcode">');
                        html.push('    ' + RwLanguages.translate('Code') + ': ' + code);
                        html.push('  </div>');
                    }
                    if (typeof responsePdaQuikIn.masterno !== 'undefined') {
                        html.push('  <div class="RowDescription">');
                        html.push('    <div class="masterno">I-Code: ' + responsePdaQuikIn.masterno + '</div>');
                        html.push('    <span class="description">' + responsePdaQuikIn.description + '</span>');
                        html.push('  </div>');
                    }
                    if (typeof responsePdaQuikIn.qtyOrdered !== 'undefined') {
                        html.push('  <div class="Row RowOrderStatus">');
                        html.push('    <div class="Field Ordered">');
                        html.push('      <div class="caption"></div>');
                        html.push('      <div class="value"></div>');
                        html.push('    </div>');
                        html.push('    <div class="Field SessionIn">');
                        html.push('      <div class="caption">' + RwLanguages.translate('Session In') + ':</div>');
                        html.push('      <div class="value"><span class="tag">' + responsePdaQuikIn.sessionIn + '</span></div>');
                        html.push('    </div>');
                        html.push('  </div>');
                    }
                    //if (typeof responsePdaQuikIn.qtyOrdered !== 'undefined') {
                    //    html.push('  <div class="Row RowOrderStatus">');
                    //    html.push('    <div class="Field Ordered">');
                    //    html.push('      <div class="caption">' + RwLanguages.translate('Ordered') + ':</div>');
                    //    html.push('      <div class="value"><span class="tag">' + responsePdaQuikIn.qtyOrdered + '</span></div>');
                    //    html.push('    </div>');
                    //    html.push('    <div class="Field SessionIn">');
                    //    html.push('      <div class="caption">' + RwLanguages.translate('Session In') + ':</div>');
                    //    html.push('      <div class="value"><span class="tag">' + responsePdaQuikIn.sessionIn + '</span></div>');
                    //    html.push('    </div>');
                    //    html.push('  </div>');
                    //    html.push('  <div class="Row RowOrderStatus">');
                    //    html.push('    <div class="Field In">');
                    //    html.push('      <div class="caption">' + RwLanguages.translate('Total In') + ':</div>');
                    //    html.push('      <div class="value"><span class="tag">' + responsePdaQuikIn.inQty + '</span></div>');
                    //    html.push('    </div>');
                    //    html.push('    <div class="Field StillOut">');
                    //    html.push('      <div class="caption">' + RwLanguages.translate('Still Out') + ':</div>');
                    //    html.push('      <div class="value"><span class="tag">' + responsePdaQuikIn.stillOut +  '</span></div>');
                    //    html.push('    </div>');
                    //    html.push('  </div>');
                    //}
                    //if (typeof responsePdaQuikIn.counted !== 'undefined') {
                    //    html.push('  <div class="Row">');
                    //    html.push('    <div class="Field Counted">');
                    //    html.push('      <div class="value"><span class="caption">' + RwLanguages.translate('Session In') + ':</span> ' + responsePdaQuikIn.counted + '</div>');
                    //    html.push('    </div>');
                    //    html.push('  </div>');
                    //}
                    if (responsePdaQuikIn.action === 'ACTION_PROMPT_FOR_QTY') {
                        html.push('  <div class="Row QtyRow">');
                        html.push('    <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield qtyfield" data-caption="" data-datafield="qty" data-formatnumeric="true"></div>');
                        html.push('  </div>');
                        html.push('  <div class="Row ButtonRow">');
                        html.push('    <span class="button default btnAddItem">Add</span>');
                        html.push('  </div>');
                    }
                    html.push('</div>');

                    var htmlString = html.join('\n');
                    var $pdaquikinitem = screen.$view.find('.pdaquikinitem');
                    $pdaquikinitem.html(htmlString);
                    FwControl.renderRuntimeControls($pdaquikinitem.find('.fwcontrol'));
                    FwFormField.setValueByDataField($pdaquikinitem, 'qty', 1);
                    $pdaquikinitem.find('.btnAddItem').on('click', (e: JQuery.ClickEvent) => {
                        try {
                            var qty: any = FwFormField.getValueByDataField(screen.$view, 'qty');
                            if (typeof qty !== 'string') {
                                throw 'Invalid qty!';
                            }
                            qty = parseInt(qty);
                            if (isNaN(qty)) {
                                throw 'Invalid qty!';
                            }
                            if (qty <= 0) {
                                throw 'Invalid qty!';
                            }
                            screen.QuikInAddItem(code, qty);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        };

        screen.pages.scanbarcodes.getElement().find('.sessioninsearch').fwmobilesearch({
            service: 'QuikIn',
            method:  'SessionInSearch',
            getRequest: function() {
                var request = {
                    contractId: screen.getContractId()
                };
                return request;
            },
            cacheItemTemplate: false,
            itemTemplate: function(model) {
                var html = [];
                var isheader    = ((model.itemclass === 'N') || (model.sessionin === 0));
                var isClickable = applicationConfig.quikIn.enableCancelItem;
                var masterclass = 'item itemclass-' + model.itemclass;
                masterclass += (isClickable ? ' link' : '');
                html.push('<div class="' + masterclass + '">');

                if (typeof model.description !== 'undefined' && model.description.length > 0) {
                    html.push('  <div class="row1">');
                    html.push('     <div class="title">{{description}}</div>');
                    html.push('  </div>');
                }

                html.push('  <div class="row2">');
                html.push('    <div class="col1">');
                if (typeof model.barcode !== 'undefined' && model.barcode.length > 0) {
                    html.push('      <div class="datafield barcode">');
                    html.push('        <div class="caption">' + RwLanguages.translate('B/C') + ':</div>');
                    html.push('        <div class="value">{{barcode}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.masterno !== 'undefined' && model.masterno.length > 0) {
                    html.push('      <div class="datafield masterno">');
                    html.push('        <div class="caption">' + RwLanguages.translate('I-Code') + ':</div>');
                    html.push('        <div class="value">{{masterno}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.trackedby !== 'undefined' && model.trackedby.length > 0) {
                    html.push('      <div class="datafield trackedby">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Tracked By') + ':</div>');
                    html.push('        <div class="value">{{trackedby}}</div>');
                    html.push('      </div>');
                }
                html.push('    </div>');
                html.push('    <div class="col2">');
                
                if (typeof model.counted !== 'undefined' && model.counted.length > 0) {
                    html.push('      <div class="datafield counted">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Session In') + ':</div>');
                    html.push('        <div class="value">{{counted}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.status !== 'undefined' && model.status.length > 0) {
                    html.push('      <div class="datafield status">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Status') + ':</div>');
                    html.push('        <div class="value">{{status}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.scannedby !== 'undefined' && model.scannedby.length > 0) {
                    html.push('      <div class="datafield scannedby">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Scanned By') + ':</div>');
                    html.push('        <div class="value">{{scannedby}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.scannedbydatetime !== 'undefined' && model.scannedbydatetime.length > 0) {
                    html.push('      <div class="datafield scannedbydatetime">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Date / Time') + ':</div>');
                    html.push('        <div class="value">{{scannedbydatetime}}</div>');
                    html.push('      </div>');
                }
                html.push('    </div>');
                html.push('  </div>');

                //html.push('  <div class="row3">');
                //html.push('    <div class="col1">');
                //html.push('      <div class="datafield status">');
                //html.push('        <div class="caption">Status:</div>');
                //html.push('        <div class="value">{{status}}</div>');
                //html.push('      </div>');
                //html.push('    </div>');
                //html.push('    <div class="col2">');
                //html.push('      <div class="datafield counted">');
                //html.push('        <div class="caption">Session In:</div>');
                //html.push('        <div class="value">{{counted}}</div>');
                //html.push('      </div>');
                //html.push('   </div>');
                //html.push('  </div>');

                //html.push('  <div class="row4">');
                //html.push('    <div class="col1">');
                //html.push('      <div class="datafield scannedby">');
                //html.push('        <div class="caption">Scanned By:</div>');
                //html.push('        <div class="value">{{scannedby}}</div>');
                //html.push('      </div>');
                //html.push('    </div>');
                //html.push('    <div class="col2">');
                //html.push('      <div class="datafield scannedbydatetime">');
                //html.push('        <div class="caption">Date / Time:</div>');
                //html.push('        <div class="value">{{scannedbydatetime}}</div>');
                //html.push('      </div>');
                //html.push('    </div>');
                //html.push('  </div>');
                
                if (model.exception.length > 0) {
                    html.push('  <div class="row5">');
                    html.push('    <div class="datafield exception">');
                    html.push('      <div class="caption">Exception:</div>');
                    html.push('      <div class="value">{{exception}}</div>');
                    html.push('    </div>');
                    html.push('  </div>');
                }

                html.push('</div>');

                var htmlString = html.join('\n');
                return htmlString;
            },
            recordClick: function(recorddata, $record, e) {
                try {
                    if (applicationConfig.quikIn.enableCancelItem) {
                        var $menu = FwContextMenu.render(recorddata.description, 'center');
                        FwContextMenu.addMenuItem($menu, 'Cancel Item', (e: JQuery.ClickEvent) => {
                            var request;
                            try {
                                request = {
                                    internalchar: recorddata.internalchar,
                                    quikinitemid: recorddata.quikinitemid
                                };
                                RwServices.callMethod("QuikIn", "CancelItem", request, function(response) {
                                     screen.$view.find('.pdaquikinitem').empty();
                                    screen.pages.scanbarcodes.getElement().find('.sessioninsearch').fwmobilesearch('search');
                                });
                                FwContextMenu.destroy($menu);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            afterLoad: function(plugin, response) {
                
            }
        });

        screen.load = function () {
            program.setScanTarget('#scanBarcodeView-txtBarcodeData');
            screen.pages.quikinsessionsearch.forward();
        };

        screen.unload = function () {

        };

        return screen;
    }
}

var QuikIn = new QuikInClass();
