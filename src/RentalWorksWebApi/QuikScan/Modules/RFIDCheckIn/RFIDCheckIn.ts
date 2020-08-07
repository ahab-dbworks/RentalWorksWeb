var RFIDCheckIn: any = {};
RFIDCheckIn.getModuleScreen = function(viewModel, properties) {
    var combinedViewModel, screen, pageTitle, $fwcontrols;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('RFID Check-In')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-rfidcheckin').html(), combinedViewModel);
    screen = {
        isscanning: false,
        processqtyspeechtimeout: null,
        exceptionqtyspeechtimeout: null,
        batchtimeout: 10,
        scanagain: 20,
        scanagaintimeremaining: 0,
        shouldstopscanagaintimer: false
    };
    if ( (typeof localStorage.getItem(program.localstorageitems.rfidcheckin_batchtimeout) === 'string') && 
         (!isNaN(parseInt(localStorage.getItem(program.localstorageitems.rfidcheckin_batchtimeout)))) ) {
        screen.batchtimeout = parseInt(localStorage.getItem(program.localstorageitems.rfidcheckin_batchtimeout));
    } else {
        localStorage.setItem(program.localstorageitems.rfidcheckin_batchtimeout, screen.batchtimeout);
    }
    if ( (typeof localStorage.getItem(program.localstorageitems.rfidcheckin_scanagain) === 'string') && 
         (!isNaN(parseInt(localStorage.getItem(program.localstorageitems.rfidcheckin_scanagain)))) ) {
        screen.scanagain = parseInt(localStorage.getItem(program.localstorageitems.rfidcheckin_scanagain));
    } else {
        localStorage.setItem(program.localstorageitems.rfidcheckin_scanagain, screen.scanagain);
    }
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.$view.find('.btnstopscanning').hide();
    screen.$view.find('.toggleitemscol .icon').css('background-image', 'url(' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/arrow-south.png)');
    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);

    screen.$view.find('.tabpanel').hide();
    screen.$view.find('.tabpanel.rfiditems').show();
    
    var $tabrfiditems = FwMobileMasterController.tabcontrols.addtab('RFID Items', true);
    $tabrfiditems.on('click', function() {
        try {
            screen.$view.find('.tabpanel').hide();
            screen.$view.find('.tabpanel.rfiditems').show();
            screen.getPendingItems();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    var $stageditems = FwMobileMasterController.tabcontrols.addtab('Session Items', false);
    $stageditems.on('click', function() {
        try {
            screen.$view.find('.tabpanel').hide();
            screen.$view.find('.tabpanel.stageditems').show();
            screen.getSessionItems();
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$view
        .on('click', '.btnscanitems', function() {
            try {
                screen.startScanning();
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnstopscanning', function() {
            try {
                screen.stopScanning(true);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.exceptionheader .contextmenu', function() {
            var $item, $contextmenu;
            $item        = jQuery(this).closest('.item');
            $contextmenu = FwContextMenu.render('Options', 'center');
            FwContextMenu.addMenuItem($contextmenu, 'Clear All Exceptions', function() {
                try {
                    screen.clearAllExceptions();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            });
        })
        .on('click', '.exceptioncontent .item .contextmenu', function() {
            var $item, $contextmenu;
            try {
                $item        = jQuery(this).closest('.item');
                $contextmenu = FwContextMenu.render('Options', 'center');
                FwContextMenu.addMenuItem($contextmenu, 'Clear Exception', function() {
                    try {
                        screen.clearException($item);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
                if ($item.attr('data-hasadditemtoorder') === 'true') {
                    FwContextMenu.addMenuItem($contextmenu, 'Add Item To Order', function() {
                        try {
                            screen.addItemToOrder($item);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                if ($item.attr('data-hasaddcompletetoorder') === 'true') {
                    FwContextMenu.addMenuItem($contextmenu, 'Add Complete To Order', function() {
                        try {
                            screen.addCompleteToOrder($item);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                if ($item.attr('data-hasoverrideavailability') === 'true') {
                    FwContextMenu.addMenuItem($contextmenu, 'Override Availability Conflict', function() {
                        try {
                            screen.overrideAvailabilityConflict($item);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                if ($item.attr('data-hastransferiteminrepair') === 'true') {
                    FwContextMenu.addMenuItem($contextmenu, 'Transfer Item In Repair', function() {
                        try {
                            screen.transferItemInRepair($item);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                if ($item.attr('data-hasreleasefromrepair') === 'true') {
                    FwContextMenu.addMenuItem($contextmenu, 'Release From Repair', function() {
                        try {
                            screen.releaseFromRepair($item);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.stageditems .contextmenu', function() {
            var $item, $contextmenu;
            try {
                $item        = jQuery(this).closest('.item');
                $contextmenu = FwContextMenu.render('Options', 'center');
                FwContextMenu.addMenuItem($contextmenu, 'Unstage Item', function() {
                    try {
                        screen.unstageItem($item);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('focus', '.txtbatchtimeout', function() {
            var $txt, oldvalue;
            $txt = jQuery(this);
            oldvalue = $txt.val();
            if (isNaN(oldvalue)) {
                oldvalue = screen.batchtimeout;
            }
            $txt.attr('data-oldvalue', oldvalue);
            $txt.val('');
        })
        .on('blur', '.txtbatchtimeout', function() {
            var $txt = jQuery(this);
            if ($txt.val().toString().length === 0 || isNaN(parseInt($txt.val().toString()))) {
                var oldvalue = $txt.attr('data-oldvalue');
                if (typeof oldvalue === 'string') {
                    if (!isNaN(parseInt(oldvalue))) {
                        $txt.val(oldvalue);
                    } else {
                        $txt.val(screen.batchtimeout);
                    }
                } else {
                    $txt.val(screen.batchtimeout);
                }
            }
            $txt.removeAttr('data-oldvalue');
            localStorage.setItem('rwqs_rfidcheckin_batchtimeout', $txt.val().toString());
        })
        .on('focus', '.txtscanagain', function() {
            var $txt, oldvalue;
            $txt = jQuery(this);
            oldvalue = $txt.val();
            if (isNaN(oldvalue)) {
                oldvalue = screen.scanagain;
            }
            $txt.attr('data-oldvalue', oldvalue);
            $txt.val('');
        })
        .on('blur', '.txtscanagain', function() {
            var $txt = jQuery(this);
            if ($txt.val().toString().length === 0 || isNaN(parseInt($txt.val().toString()))) {
                var oldvalue = $txt.attr('data-oldvalue');
                if (typeof oldvalue === 'string') {
                    if (!isNaN(parseInt(oldvalue))) {
                        $txt.val(oldvalue);
                    } else {
                        $txt.val(screen.scanagain);
                    }
                } else {
                    $txt.val(screen.scanagain);
                }
            }
            $txt.removeAttr('data-oldvalue');
            localStorage.setItem('rwqs_rfidcheckin_scanagain', $txt.val().toString());
        })
    ;

    screen.stopScanning = function(sendCloseCommand) {
        //speechSynthesis.speak(new SpeechSynthesisUtterance('Scanning stopped.'));
        if (!sendCloseCommand) {
            setTimeout(function() {
                FwConfirmation.destroyConfirmation(jQuery('.tagCountPopup'));
                screen.isscanning = false;
                screen.$view.find('.btnstopscanning').hide();
                screen.$view.find('.btnscanitems').show();
            }, 100);
        } else {
            screen.stopScanAgainTimer();
            screen.shouldstopscanagaintimer = true;
            screen.isscanning = false;
            screen.$view.find('.btnstopscanning').hide();
            screen.$view.find('.btnscanitems').show();
            switch(screen.websocket.readyState) {
                case 0: //connection has not yet been established

                    break;
                case 1: //connection is established and communication is possible
                    if (sendCloseCommand) {
                        var request: any = {
                            command: 'stoplistening'
                        };
                        request = JSON.stringify(request);
                        screen.websocket.send(request);
                    } else {
                        screen.websocket.close();
                    }
                    break;
                case 2: //connection is going through the closing handshake
                
                    break;
                case 3: //connection has been closed or could not be opened

                    break;
            }
        }
    };

    screen.showTagCount = function(count) {
        screen.$view.find('.batchqtyvalue').html(count);
        if (jQuery('.tagCountPopup').length) {
            jQuery('.tagCount').html(count);
        } else {
            var $confirmation, html;
            //html = [];
            //html.push('<div class="tagcount">');
            //html.push('  <div class="tagcountbox">');
            //html.push('    <div class="title">Total Qty</div>');
            //html.push('    <div class="count"></div>');
            //html.push('    <div class="buttons"></div>');
            //html.push('  <div>');
            //html.push('<div>');
            $confirmation = FwConfirmation.renderConfirmation('Total Qty', '<div class="tagCount" style="color:red;font-weight:bold;text-align:center;font-size:50vw;"></div>');
            $confirmation.find('.fwconfirmationbox').css({
                'width': '90vw'
            });
            $confirmation.find('.title').css({
                'text-align': 'center',
                'font-size':'1em'
            });
            var $btncancel = FwConfirmation.addButton($confirmation, 'Stop Scanning', false);
            $btncancel.css({
                'color':'#ffffff',
                'background-color':'#f44336',
                'font-size':'1em'
            });
            $btncancel.html('<img style="width:1.3em;height:1.3em;margin:-.15em .5em 0 0;vertical-align:middle;" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/images/icons/128/stop.001.png" />Stop Scanning');
            $btncancel.on('click', function() {
                screen.stopScanning(true);
            });
            $confirmation.addClass('tagCountPopup');
            $confirmation.find('.tagCount').html(count);
        }
    };

    screen.stopScanAgainTimer = function() {
        if (typeof screen.scanagaininterval === 'number') {
            window.clearInterval(screen.scanagaininterval);
        }
        screen.$view.find('.captionscanagaincountdown').html('');
    };
    
    screen.startScanning = function() {
        //speechSynthesis.speak(new SpeechSynthesisUtterance('Scanning started.'));
        screen.stopScanAgainTimer();
        screen.shouldstopscanagaintimer = false;
        var exceptionqty = parseInt(screen.$view.find('.exceptionqtyvalue').html());
        if (exceptionqty > 0) {
            FwFunc.showMessage('Exceptions must be cleared/handled before scanning can continue.');
            return;
        }

        screen.isscanning = true;
        screen.showTagCount(0);
        screen.$view.find('.btnscanitems').hide();
        screen.$view.find('.btnstopscanning').show();

        screen.$view.find('.processedcontent').empty();
        screen.$view.find('.processedcount').html('0');
        //screen.$view.find('.exceptioncontent').empty();
        screen.$view.find('.pendingitemscontent').empty();
        screen.$view.find('.batchqtyvalue').html('0');
        screen.$view.find('.processqtyvalue').html('0');
        //screen.$view.find('.exceptionqtyvalue').html('0');

        // close the web socket in case it's open, but don't allow it to stop scanning in the close event
        var allowstopscanning = false;
        if (typeof screen.websocket !== 'undefined') {
            screen.websocket.close();
        }
        allowstopscanning = true;

        screen.websocket = new WebSocket(applicationConfig.rentalworksapi);
        
        screen.websocket.onopen = function (event) {
           setTimeout(function() {
               try {
                  var request: any = {
                    command: 'startlistening',
                    portal: 'Portal 1',
                    batchtimeout: parseInt(screen.$view.find('.txtbatchtimeout').val())
                  };
                  request = JSON.stringify(request);
                  screen.websocket.send(request);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, 0);
        };

        screen.websocket.onerror = function(error) {
            setTimeout(function() {
                try {
                    screen.stopScanning(false);
                    FwFunc.showError('A websocket error occured.');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }, 0);
        };

        screen.websocket.onclose = function() {
            setTimeout(function() {
                try {
                    if (allowstopscanning) {
                        screen.stopScanning(false);
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            },0);
        };
        
        screen.websocket.onmessage = function (event) {
            setTimeout(function() {
                try {
                    //console.log(event.data);
                    var message = JSON.parse(event.data);
                    switch(message.type) {
                        case 'count':
                            screen.showTagCount(message.count);
                            break;
                        case 'batch':
                            //this doesn't work anymore
                            //setTimeout(function() {
                            //    var utterance = new SpeechSynthesisUtterance();
                            //    var voices = window.speechSynthesis.getVoices();
                            //    utterance.voice = voices[0]; // Note: some voices don't support altering params
                            //    utterance.voiceURI = 'native';
                            //    utterance.volume = 1; // 0 to 1
                            //    utterance.rate = 1; // 0.1 to 10
                            //    utterance.pitch = 2; //0 to 2
                            //    utterance.text = message.count.toString()
                            //    utterance.lang = 'en-US';
                            //    speechSynthesis.speak(utterance);
                            //}, 2000);
                            screen.showTagCount(message.count);
                            setTimeout(function() {
                                FwConfirmation.destroyConfirmation(jQuery('.tagCountPopup'));
                                screen.stopScanning(false);
                                var tags: string | string[] = [];
                                for (var epcno = 0; epcno < message.epcs.length; epcno++) {
                                    var edecsmessage = message.epcs[epcno];
                                    tags.push(edecsmessage.epc);
                                }
                                tags = tags.join(',');
                                screen.processBatch(tags);
                            }, 250);
                            break;
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, 0);
        }
    }

    screen.getOrderId = function() {
        var orderid = '';
        if (typeof properties.webSelectOrder === 'object') {
            orderid = properties.webSelectOrder.orderId;
        }
        else if (typeof properties.webSelectSession === 'object') {
            orderid = properties.webSelectSession.orderId;
        }
        return orderid;
    };

    screen.getPortal = function() {
        return 'Portal 1';
    };

    screen.batchid = '';
    screen.getBatchId = function() {
        return screen.batchid;
    };

    screen.processBatch = function(tags) {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal(),
            tags: tags
        };
        RwServices.callMethod("RfidCheckIn", "ProcessBatch", request, function(response) {
            try {
                screen.batchid = response.batchid;
                screen.loadProcessed(response.funcscannedtag);
                screen.loadExceptions(response.funcscannedtagexception);
                screen.scanagaintimeremaining = parseInt(screen.$view.find('.txtscanagain').val());
                var exceptionqty = parseInt(screen.$view.find('.exceptionqtyvalue').html());
                if (isNaN(exceptionqty)) throw 'exceptionqty is not a number {DCD6E918-875B-4046-B6A5-50FD94166A36}';
                var scanagain = parseInt(screen.$view.find('.txtscanagain').val());
                if (isNaN(exceptionqty)) throw 'scanagain is not a number {F411AECD-23C7-4DE8-AAD5-685679BD2B8D}';
                screen.scanagaintimeremaining = scanagain;
                if (!screen.shouldstopscanagaintimer && screen.scanagaintimeremaining > 0 && exceptionqty === 0) {
                    screen.stopScanAgainTimer();
                    screen.scanagaininterval = setInterval(function() {
                        try {
                            screen.scanagaintimeremaining -= 1;
                            screen.$view.find('.captionscanagaincountdown').html(' (' + screen.scanagaintimeremaining + ')');
                            if (screen.scanagaintimeremaining <= 0) {
                                screen.scanagaintimeremaining = 0;
                                screen.startScanning();
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    }, 1000);
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.addItemToOrder = function($item) {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal(),
            tag: $item.attr('data-tag')
        };
        RwServices.callMethod("RfidCheckIn", "AddItemToOrder", request, function(response) {
            try {
                screen.handleExceptionResponse(response, $item);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.addCompleteToOrder = function($item) {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal(),
            tag: $item.attr('data-tag')
        };
        RwServices.callMethod("RfidCheckIn", "AddCompleteToOrder", request, function(response) {
            try {
                screen.handleExceptionResponse(response, $item);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.overrideAvailabilityConflict = function($item) {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal(),
            tag: $item.attr('data-tag')
        };
        RwServices.callMethod("RfidCheckIn", "OverrideAvailabilityConflict", request, function(response) {
            try {
                screen.handleExceptionResponse(response, $item);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.transferItemInRepair = function($item) {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal(),
            tag: $item.attr('data-tag')
        };
        RwServices.callMethod("RfidCheckIn", "TransferItemInRepair", request, function(response) {
            try {
                screen.handleExceptionResponse(response, $item);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.releaseFromRepair = function($item) {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal(),
            tag: $item.attr('data-tag')
        };
        RwServices.callMethod("RfidCheckIn", "ReleaseFromRepair", request, function(response) {
            try {
                screen.handleExceptionResponse(response, $item);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.clearException = function($item) {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal(),
            tag: $item.attr('data-tag')
        };
        RwServices.callMethod("RfidCheckIn", "ClearException", request, function(response) {
            try {
                screen.handleExceptionResponse(response, $item);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.handleExceptionResponse = function(response, $item) {
        switch(response.status) {
            case 0:
                $item.remove();
                screen.removeException($item);
                if (screen.$view.find('.exception .item').length == 0) {
                    screen.$view.find('.exception').hide();
                    screen.$view.find('.pendingitems').show();
                    screen.getPendingItems();
                    screen.startScanning();
                }
                //screen.loadExceptions(response.funcscannedtagexception);
                break;
            default:
                FwFunc.showError(response.msg);
                break;
        }
    };

    screen.clearAllExceptions = function() {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal()
        };
        RwServices.callMethod("RfidCheckIn", "ClearAllExceptions", request, function(response) {
            try {
                screen.loadExceptions(response.funcscannedtagexception);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.removeException = function($item) {
        $item.addClass('delete');
        setTimeout(function() {
            $item.remove();
        }, 550);
        var count = parseInt(screen.$view.find('.exceptioncount').html()) - 1;
        screen.$view.find('.exceptioncount').html(count);
        screen.$view.find('.exceptionqtyvalue').html(count);
        if (count === 0) {
            // make certain there are no exception on the server
            screen.getExceptions();
        }
    };

    screen.loadProcessed = function(dt) {
        var html: string | string[] = [];
        var stagedcount = 0, alreadystagedcount = 0;
        for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
            var row = dt.Rows[rowno];
            if (row[dt.ColumnIndex.status] === 'STAGED') {
                alreadystagedcount++;
            } else {
                stagedcount++;
            }
            html.push('<div class="item new" data-tag="' + row[dt.ColumnIndex.tag] + '" data-staged="' + (row[dt.ColumnIndex.status] === 'STAGED').toString() + '">');
            html.push('  <div class="master">' + row[dt.ColumnIndex.master] + '</div>');
            html.push('  <div class="tag">' + row[dt.ColumnIndex.tag] + '</div>');
            //if (row[dt.ColumnIndex.status] === 'STAGED') {
            //    html.push('  <div class="message">' + row[dt.ColumnIndex.message] + '</div>');
            //}
            html.push('</div>');
        }
        html = html.join('\n');
        screen.$view.find('.processedcontent').html(html);
        screen.$view.find('.processedcount').html(dt.Rows.length);
        screen.$view.find('.processqtyvalue').html(stagedcount);
        setTimeout(function() {
            screen.$view.find('.processedcontent .item.new').removeClass('new');
        }, 250);
    };

    screen.getExceptions = function() {
        var request = {
            orderid: screen.getOrderId(),
            portal: screen.getPortal(),
            batchid: screen.getBatchId()
        };
        RwServices.callMethod("RfidCheckIn", "GetExceptions", request, function(response) {
            try {
                screen.loadExceptions(response.funcscannedtagexception);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.loadExceptions = function(dt) {
        var $exceptioncontent = screen.$view.find('.exceptioncontent');
        if (dt.Rows.length > 0) {
            var colindex_tag           = dt.ColumnIndex.tag;
            var colindex_master        = dt.ColumnIndex.master;
            var colindex_message       = dt.ColumnIndex.message;
            var colindex_exceptiontype = dt.ColumnIndex.exceptiontype;
            var html: string | string[] = [];
            for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
                var row = dt.Rows[rowno];
                var hasadditemtoorder       = ' data-hasadditemtoorder="false"';
                var hasaddcompletetoorder   = ' data-hasaddcompletetoorder="false"';
                var hasoverrideavailability = ' data-hasoverrideavailability="false"';
                var hastransferiteminrepair = ' data-hastransferiteminrepair="false"';
                var hasreleasefromrepair    = ' data-hasreleasefromrepair="false"';
                var exceptiontype = parseInt(row[colindex_exceptiontype]);
                switch(exceptiontype) {
                    case RwConstants.STAGING_STATUS_QTY_EXCEEDS_ORDER_CAN_ADD_ITEM:
                    case RwConstants.STAGING_STATUS_ITEM_NOT_ON_ORDER_CAN_ADD_ITEM:
                        hasadditemtoorder       = ' data-hasadditemtoorder="true"';
                        break;
                    case RwConstants.STAGING_STATUS_QTY_EXCEEDS_ORDER_CAN_ADD_ITEM_OR_COMPLETE:
                    case RwConstants.STAGING_STATUS_ITEM_NOT_ON_ORDER_CAN_ADD_ITEM_OR_COMPLETE:
                        hasadditemtoorder       = ' data-hasadditemtoorder="true"';
                        hasaddcompletetoorder   = ' data-hasaddcompletetoorder="true"';
                        break;
                    case RwConstants.STAGING_STATUS_CONFLICT_WITH_RESERVATION_CAN_OVERRIDE:
                        hasoverrideavailability = ' data-hasoverrideavailability="true"';
                        break;
                    case RwConstants.STAGING_STATUS_ITEM_IN_REPAIR_CAN_TRANSFER:
                        hastransferiteminrepair = ' data-hastransferiteminrepair="true"';
                        break;
                    case RwConstants.STAGING_STATUS_ITEM_IN_REPAIR:
                        hasreleasefromrepair    = ' data-hasreleasefromrepair="true"';
                        break;
                }
                html.push('<div class="flexrow item new" data-tag="' + row[colindex_tag] + '"' + hasadditemtoorder + hasaddcompletetoorder + hasoverrideavailability + hastransferiteminrepair + hasreleasefromrepair + '>');
                html.push('  <div class="col1">');
                html.push('    <div class="master">' + row[colindex_master] + '</div>');
                html.push('    <div class="tag">' + row[colindex_tag] + '</div>');
                html.push('    <div class="message">' + row[colindex_message] + '</div>');
                html.push('  </div>');
                html.push('  <div class="col2">');
                html.push('    <img class="contextmenu" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/verticalellipsis-white.001.png" />'); 
                html.push('  </div>');
                html.push('</div>');
            }
            html = html.join('\n');
            screen.$view.find('.pendingitems').hide();
            screen.$view.find('.exception').show();
            screen.$view.find('.exceptioncontent').html(html);
            screen.$view.find('.exceptioncount').html(dt.Rows.length);
            screen.$view.find('.exceptionqtyvalue').html(dt.Rows.length);
            setTimeout(function() {
                try {
                    screen.$view.find('.exceptioncontent .item.new').removeClass('new');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }, 250);

        } else {
            screen.$view.find('.exception').hide();
            screen.$view.find('.exceptioncount').html(dt.Rows.length);
            screen.$view.find('.exceptionqtyvalue').html(dt.Rows.length);
            screen.$view.find('.pendingitemscontent').empty();
            screen.$view.find('.pendingitems').show();
            screen.getPendingItems();
        }
    };

    screen.getPendingItems = function() {
        var request = {
            contractid: screen.getContractId(),
            rectype: 'R',
            containeritemid: ''
        };
        RwServices.callMethod("RfidCheckIn", "GetPendingItems", request, function(response) {
            try {
                var dt = response.funccheckoutexception;
                var html: string | string[] = [];
                var pendingcount = 0;
                for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
                    var row = dt.Rows[rowno];
                    html.push('<div class="item">');
                    html.push('  <div class="description">' + row[dt.ColumnIndex.description] + '</div>');
                    html.push('  <div class="flexrow">');
                    html.push('    <div class="masternocaption">I-Code:</div>');
                    html.push('    <div class="masternovalue">' + row[dt.ColumnIndex.masterno] + '</div>');
                    html.push('    <div class="missingqtycaption">Remaining:</div>');
                    html.push('    <div class="missingqtyvalue">' + row[dt.ColumnIndex.missingqty] + '</div>');
                    html.push('  </div>');
                    html.push('  <div class="flexrow">');
                    html.push('    <div class="spacercaption"></div>');
                    html.push('    <div class="spacervalue"></div>');
                    html.push('    <div class="qtystagedandoutcaption">Staged/Out:</div>');
                    html.push('    <div class="qtystagedandoutvalue">' + row[dt.ColumnIndex.qtystagedandout] + '</div>');
                    html.push('  </div>');
                    html.push('  <div class="flexrow">');
                    html.push('    <div class="spacercaption"></div>');
                    html.push('    <div class="spacervalue"></div>');
                    html.push('    <div class="qtyorderedcaption">Ordered:</div>');
                    html.push('    <div class="qtyorderedvalue">' + row[dt.ColumnIndex.qtyordered] + '</div>');
                    html.push('  </div>');
                    html.push('</div>');
                    pendingcount += parseInt(row[dt.ColumnIndex.missingqty]);
                }
                html = html.join('\n');
                screen.$view.find('.pendingitemscontent').html(html);
                screen.$view.find('.pendingitemscount').html(pendingcount);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.getSessionItems = function() {
        var request = {
            orderid: screen.getOrderId()
        };
        RwServices.callMethod("RfidCheckIn", "GetSessionItems", request, function (response) {
            try {
                var dt = response.funcstageditemsweb;
                var html: string | string[] = [];
                for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
                    var row = dt.Rows[rowno];
                    html.push('<div class="flexrow item" data-barcode="' + row[dt.ColumnIndex.barcode] + '">');
                    html.push('  <div class="col1">');
                    html.push('    <div class="description">' + row[dt.ColumnIndex.description] + '</div>');
                    html.push('    <div class="flexrow">');
                    html.push('      <div class="masternocaption">I-Code:</div>');
                    html.push('      <div class="masternovalue">' + row[dt.ColumnIndex.masterno] + '</div>');
                    html.push('      <div class="quantitycaption">Staged:</div>');
                    html.push('      <div class="quantityvalue">' + row[dt.ColumnIndex.quantity] + '</div>');
                    html.push('    </div>');
                    html.push('    <div class="flexrow">');
                    html.push('      <div class="barcodecaption">Tag:</div>');
                    html.push('      <div class="barcodevalue">' + row[dt.ColumnIndex.barcode] + '</div>');
                    html.push('    </div>');
                    if (row[dt.ColumnIndex.vendorid].length > 0) {
                        html.push('    <div class="flexrow">');
                        html.push('      <div class="vendorcaption">Vendor:</div>');
                        html.push('      <div class="vendorvalue">' + row[dt.ColumnIndex.vendor] + '</div>');
                        html.push('    </div>');
                    }
                    html.push('  </div>');
                    html.push('  <div class="col2">');
                    html.push('    <img class="contextmenu" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/verticalellipsis-white.001.png" />'); 
                    html.push('  </div>');
                    html.push('</div>');
                }
                html = html.join('\n');
                screen.$view.find('.stageditems > .card-body').html(html);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.unstageItem = function($item) {
        var request = {
            orderid: screen.getOrderId(),
            barcode: $item.attr('data-barcode')
        };
        RwServices.callMethod("RfidCheckIn", "UnstageItem", request, function(response) {
            try {
                switch(response.status) {
                    case 0:
                        $item.remove();
                        //screen.getSessionItems();
                        break;
                    default:
                        FwFunc.showError(response.msg);
                        break;
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.load = function() {
        screen.$view.find('.txtbatchtimeout').val(screen.batchtimeout);
        screen.$view.find('.txtscanagain').val(screen.scanagain);

        if (typeof properties.webSelectOrder === 'object') {
            screen.$view.find('.dealvalue').text(properties.webSelectOrder.deal);
            screen.$view.find('.agentvalue').text(properties.webSelectOrder.agent);
            screen.$view.find('.ordernovalue').text(properties.webSelectOrder.orderNo);
            screen.$view.find('.orderdescvalue').text(properties.webSelectOrder.orderDesc);
        }
        else if (typeof properties.webSelectSession === 'object') {
            screen.$view.find('.dealvalue').text(properties.webSelectSession.deal);
            screen.$view.find('.agentvalue').text(properties.webSelectSession.agent);
            screen.$view.find('.ordernovalue').text(properties.webSelectSession.orderNo);
            screen.$view.find('.orderdescvalue').text(properties.webSelectSession.orderDesc);
        }

        

        //screen.getExceptions();
        screen.clearAllExceptions();
    };

    screen.beforeNavigateAway = function(navigateAway) {
        screen.clearAllExceptions();
        navigateAway();
    };

    return screen;
};