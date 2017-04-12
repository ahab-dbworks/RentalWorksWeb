var UnassignedItems = {};
//----------------------------------------------------------------------------------------------
UnassignedItems.getModuleScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $fwcontrols, $tabpo, $tabitems, $search, $po, $itemassign, $selectitem, searchmode;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Unassigned Items')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-unassigneditems').html(), combinedViewModel);

    screen            = {};
    screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.properties = properties;

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    $search     = screen.$view.find('.ui-search');
    $po         = screen.$view.find('.ui-po');
    $itemassign = screen.$view.find('.ui-itemassign');
    $selectitem = screen.$view.find('.ui-selectitem');

    $tabpo    = FwMobileMasterController.tabcontrols.addtab('PO (0)',    true);
    $tabitems = FwMobileMasterController.tabcontrols.addtab('Items (0)', false);

    $tabpo.on('click', function() {
        $search.find('.tabpage[data-tab="po"]').show();
        $search.find('.tabpage[data-tab="item"]').hide();
        $search.find('.fwmobilecontrol-value').attr('placeholder', 'Search all, or enter PO No');
        searchmode = 'PO';
    });
    $tabitems.on('click', function() {
        $search.find('.tabpage[data-tab="item"]').show();
        $search.find('.tabpage[data-tab="po"]').hide();
        $search.find('.fwmobilecontrol-value').attr('placeholder', 'Search all, or enter ICode');
        searchmode = 'ITEM';
    });

    $search
        .on('change', '.fwmobilecontrol-value', function() {
            if (searchmode == 'PO') {
                $search.searchpo(jQuery(this).val());
            } else if (searchmode == 'ITEM') {
                $search.searchitem(jQuery(this).val());
            }
        })
        .on('click', '.fwmobilecontrol-search', function() {
            if (searchmode == 'PO') {
                $search.searchpo($search.find('.fwmobilecontrol-value').val());
            } else if (searchmode == 'ITEM') {
                $search.searchitem($search.find('.fwmobilecontrol-value').val());
            }
        })
        .on('click', '.record.po', function() {
            var $this;
            $this = jQuery(this);
            $tabitems.hide();
            $tabpo.hide();
            $search.hide();
            $po.showscreen($this.data('recorddata'));
        })
        .on('click', '.record.item', function() {
            $tabpo.hide();
            $tabitems.hide();
            $search.hide();
            $itemassign.showscreen($search, jQuery(this).data('recorddata'), 'ITEM');
        })
    ;
    $search.searchpo = function(searchvalue) {
        var request;
        $tabpo.html('PO (0)');
        $search.find('.tabpage[data-tab="po"]').empty();

        request = {
            searchvalue: searchvalue,
            searchmode:  'PO'
        };
        RwServices.call("UnassignedItem", "GetAssignableItems", request, function(response) {
            for (var i = 0; i < response.assignableitems.length; i++) {
                $search.renderporecord(response.assignableitems[i]);
            }

            (response.assignableitems.length > 0) ? $tabpo.html('PO (' + response.assignableitems.length +')') : $search.find('.tabpage[data-tab="po"]').html('<div class="norecord">0 records found.</div>');
        });
    };
    $search.searchitem = function(searchvalue) {
        var request;
        $tabitems.html('Items (0)');
        $search.find('.tabpage[data-tab="item"]').empty();

        request = {
            searchvalue: searchvalue,
            searchmode:  'ITEM'
        };
        RwServices.call("UnassignedItem", "GetAssignableItems", request, function(response) {
            for (var i = 0; i < response.assignableitems.length; i++) {
                $search.renderitemrecord(response.assignableitems[i]);
            }

            (response.assignableitems.length > 0) ? $tabitems.html('Items (' + response.assignableitems.length +')') : $search.find('.tabpage[data-tab="item"]').html('<div class="norecord">0 records found.</div>');
        });
    };
    $search.searchall = function() {
        var request;
        $tabitems.html('Items (0)');
        $tabpo.html('PO (0)');
        $search.find('.tabpage[data-tab="item"]').empty();
        $search.find('.tabpage[data-tab="po"]').empty();

        request = {
            searchvalue: '',
            searchmode:  'ALL'
        };
        RwServices.call("UnassignedItem", "GetAssignableItems", request, function(response) {
            var pocount = 0, itemcount = 0;
            for (var i = 0; i < response.assignableitems.length; i++) {
                if (response.assignableitems[i].rowtype == 'PO') {
                    $search.renderporecord(response.assignableitems[i]);
                    pocount++;
                } else if (response.assignableitems[i].rowtype == 'I-CODE') {
                    $search.renderitemrecord(response.assignableitems[i]);
                    itemcount++;
                }
            }

            (pocount > 0) ? $tabpo.html('PO (' + pocount +')') : $search.find('.tabpage[data-tab="po"]').html('<div class="norecord">0 records found.</div>');
            (itemcount > 0) ? $tabitems.html('Items (' + itemcount +')') : $search.find('.tabpage[data-tab="item"]').html('<div class="norecord">0 records found.</div>');
        });
    };
    $search.renderporecord = function(record) {
        var html = [], $item;
        html.push('<div class="record po">');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Desc:</div>');
        html.push('    <div class="value desc">' + record.orderdesc + '</div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">PO No:</div>');
        html.push('    <div class="value pono">' + record.orderno + '</div>');
        html.push('    <div class="caption dynamic">As Of:</div>');
        html.push('    <div class="value asof">' + record.statusdate + '</div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Vendor:</div>');
        html.push('    <div class="value vendor">' + record.vendor + '</div>');
        html.push('  </div>');
        html.push('</div>');
        $item = jQuery(html.join(''));
        $item.data('recorddata', record);
        $search.find('.tabpage[data-tab="po"]').append($item);
    };
    $search.renderitemrecord = function(record) {
        var html = [], $item;
        html.push('<div class="record item">');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Desc:</div>');
        html.push('    <div class="value desc">' + record.master + '</div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">I-Code:</div>');
        html.push('    <div class="value icode">' + record.masterno + '</div>');
        html.push('    <div class="caption dynamic">Qty:</div>');
        html.push('    <div class="value qty">' + record.qty + '</div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Tracked By:</div>');
        html.push('    <div class="value trackedby">' + record.trackedby + '</div>');
        html.push('  </div>');
        html.push('</div>');
        $item = jQuery(html.join(''));
        $item.data('recorddata', record);
        $search.find('.tabpage[data-tab="item"]').append($item);
    };
    $search.screenreturn = function(refresh) {
        $tabitems.show();
        $tabpo.show();
        $search.show();
        RwRFID.unregisterEvents();
        if (refresh) {
            $search.searchall();
        }
    };

    $po
        .on('click', '.record', function() {
            $po.$back.hide();
            $po.hide();
            $itemassign.showscreen($po, jQuery(this).data('recorddata'), 'PO');
        })
    ;
    $po.showscreen = function(podata) {
        $po.data('recorddata', podata);

        $po.loadinfo(podata);
        $po.loaditems(podata);

        $po.show();

        $po.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', 'back', true, function() {
            $po.find('.ui-po-info').empty();
            $po.find('.ui-po-items').empty();
            $po.data('recorddata', '');
            $po.hide();
            $search.screenreturn(true);
            jQuery(this).remove();
        });
    };
    $po.loadinfo = function(podata) {
        var html = [];
        html.push('<div class="orderdesc">' + podata.orderdesc + '</div>');
        html.push('<div class="orderno">PO No: ' + podata.orderno + '</div>');
        $po.find('.ui-po-info').append(html.join(''));
    };
    $po.loaditems = function(podata) {
        var request;
        $po.find('.ui-po-items').empty();

        request = {
            orderid: podata.orderid
        };
        RwServices.call("UnassignedItem", "GetPOAssignableItems", request, function(response) {
            if (response.assignableitems.length > 0) {
                for (var i = 0; i < response.assignableitems.length; i++) {
                    $po.renderitemrecord(response.assignableitems[i]);
                }
            } else {
                $po.find('.ui-po-items').html('<div class="norecord">0 records found.</div>')
            }
        });
    };
    $po.renderitemrecord = function(recorddata) {
        var html = [], $item;
        html.push('<div class="record item">');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Desc:</div>');
        html.push('    <div class="value desc">' + recorddata.master + '</div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">I-Code:</div>');
        html.push('    <div class="value icode">' + recorddata.masterno + '</div>');
        html.push('    <div class="caption dynamic">Qty:</div>');
        html.push('    <div class="value qty">' + recorddata.qty + '</div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Tracked By:</div>');
        html.push('    <div class="value trackedby">' + recorddata.trackedby + '</div>');
        html.push('  </div>');
        html.push('</div>');
        $item = jQuery(html.join(''));
        $item.data('recorddata', recorddata);
        $po.find('.ui-po-items').append($item);
    };
    $po.screenreturn = function(refresh) {
        $po.$back.show();
        $po.show();
        RwRFID.unregisterEvents();
        if (refresh) {
            $po.loaditems($po.data('recorddata'));
        }
    };

    $itemassign
        .on('click', '.assign', function() {
            $itemassign.updaterecord();
        })
        .on('change', 'div[data-datafield="barcode"] input', function() {
            for (var i = 0; i < $itemassign.data('itemstobeassigned').length; i++) {
                if ($itemassign.data('itemstobeassigned')[i].barcode == jQuery(this).val()) {
                    $itemassign.setitemtarget($itemassign.data('itemstobeassigned')[i]);
                    break;
                }
            }
        })
        .on('change', 'div[data-datafield="mfgserial"] input', function() {
            for (var i = 0; i < $itemassign.data('itemstobeassigned').length; i++) {
                if ($itemassign.data('itemstobeassigned')[i].mfgserial == jQuery(this).val()) {
                    $itemassign.setitemtarget($itemassign.data('itemstobeassigned')[i]);
                    break;
                }
            }
        })
    ;
    $itemassign.showscreen = function($previousscreen, recorddata, mode) {
        $itemassign.show();
        $itemassign.data('prevscreen', $previousscreen);

        $itemassign.$selectitem = FwMobileMasterController.addFormControl(screen, 'Select Item', 'right', '', true, function() {
            $itemassign.hide();
            $itemassign.$selectitem.hide();
            $itemassign.$back.hide();
            $selectitem.showscreen();
        });

        $itemassign.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', 'back', true, function() {
            $itemassign.hide();
            $itemassign.resettarget();
            $itemassign.find('.ui-itemassign-info').empty();
            $itemassign.find('.fwformfield-value').val('');
            $itemassign.data('mode', '');
            $itemassign.data('recorddata', '');
            $itemassign.data('itemstobeassigned', '');
            $itemassign.data('prevscreen').screenreturn($itemassign.data('updated'));
            $itemassign.find('div[data-datafield="rfid"]').hide();
            $itemassign.$selectitem.remove();
            jQuery(this).remove();
        });

        if (recorddata.trackedby == 'RFID') {
            $itemassign.find('div[data-datafield="rfid"]').show();
            RwRFID.registerEvents($itemassign.rfidscan);
        }

        $itemassign.data('itemtarget', '');
        $itemassign.data('updated', false);
        $itemassign.data('mode', mode);
        $itemassign.data('recorddata', recorddata);
        if (mode == 'PO') $itemassign.data('recorddata').orderid = $itemassign.data('prevscreen').data('recorddata').orderid;
        $itemassign.loadinfo(recorddata, mode);
    };
    $itemassign.loadinfo = function(recorddata, mode) {
        var html = [], request;
        html.push('<div class="desc">' + recorddata.master + '</div>');
        $itemassign.find('.ui-itemassign-info').append(html.join(''));

        request = {
            mode:     mode,
            orderid:  (mode == 'PO') ? $itemassign.data('prevscreen').data('recorddata').orderid : '',
            masterid: recorddata.masterid
        };
        RwServices.call("UnassignedItem", "GetAssignableItemAssets", request, function(response) {
            $itemassign.data('itemstobeassigned', response.assignableitemassets);
            $itemassign.find('.pendingvalue').html(response.assignableitemassets.length);
        });
    };
    $itemassign.updaterecord = function() {
        var request, data, updaterecord;

        if (($itemassign.data('itemtarget') == '') && ($itemassign.data('itemstobeassigned')[0].barcode == '') && ($itemassign.data('itemstobeassigned')[0].mfgserial == '')) {
            data = $itemassign.data('itemstobeassigned')[0];
        } else if (($itemassign.data('itemtarget') == '') && (($itemassign.data('itemstobeassigned')[0].barcode != '') || ($itemassign.data('itemstobeassigned')[0].mfgserial != ''))) {
            FwFunc.showError('Please select a record before assigning data.');
        } else if ($itemassign.data('itemtarget') != '') {
            data = $itemassign.data('itemtarget');
        }

        if (typeof data != 'undefined') {
            updaterecord = (($itemassign.find('div[data-datafield="barcode"] input').val()   != '') && (data.barcode   != $itemassign.find('div[data-datafield="barcode"] input').val()))   ||
                           (($itemassign.find('div[data-datafield="mfgserial"] input').val() != '') && (data.mfgserial != $itemassign.find('div[data-datafield="mfgserial"] input').val())) ||
                           (($itemassign.find('div[data-datafield="rfid"] input').val()      != '') && (data.rfid      != $itemassign.find('div[data-datafield="rfid"] input').val()))      ||
                           (($itemassign.find('div[data-datafield="mfgdate"] input').val()   != '') && (data.mfgdate   != $itemassign.find('div[data-datafield="mfgdate"] input').val()));

            if (updaterecord == true) {
                request = {
                    itemdata:  $itemassign.data('recorddata'),
                    assetdata: data,
                    mode:      $itemassign.data('mode'),
                    barcode:   $itemassign.find('div[data-datafield="barcode"] input').val(),
                    mfgserial: $itemassign.find('div[data-datafield="mfgserial"] input').val(),
                    rfid:      $itemassign.find('div[data-datafield="rfid"] input').val(),
                    mfgdate:   $itemassign.find('div[data-datafield="mfgdate"] input').val()
                };
                RwServices.call("UnassignedItem", "UpdateAssignableItemAsset", request, function(response) {
                    if (response.updateitem.status == '0') {
                        $itemassign.data('itemstobeassigned', response.assignableitemassets);
                        $itemassign.find('.pendingvalue').html($itemassign.data('itemstobeassigned').length);
                        $itemassign.data('updated', true);
                        $itemassign.resettarget();
                    } else if (response.updateitem.status != '0') {
                        FwFunc.showError(response.updateitem.msg);
                    }
                });
            } else {
                FwFunc.showError('No new data has been defined.');
            }
        }
    };
    $itemassign.rfidscan = function(epcs) {
        if (($itemassign.find('div[data-datafield="rfid"] input').length !=  0) && (parseInt(jQuery('.tagCountPopup .tagCount').html()) == 1)) {
            $itemassign.find('div[data-datafield="rfid"] input').val(epcs);
        } else {
            application.playStatus(false);
        }
    };
    $itemassign.setitemtarget = function(data) {
        $itemassign.data('itemtarget', data);
        $itemassign.find('div[data-datafield="barcode"] input').val(data.barcode);
        $itemassign.find('div[data-datafield="mfgserial"] input').val(data.mfgserial);
        $itemassign.find('div[data-datafield="rfid"] input').val(data.rfid);
        $itemassign.find('div[data-datafield="mfgdate"] input').val(data.mfgdate);
    };
    $itemassign.resettarget = function() {
        $itemassign.data('itemtarget', '');
        $itemassign.find('div[data-datafield="barcode"] input').val('');
        $itemassign.find('div[data-datafield="mfgserial"] input').val('');
        $itemassign.find('div[data-datafield="rfid"] input').val('');
        $itemassign.find('div[data-datafield="mfgdate"] input').val('');
    };

    $selectitem
        .on('click', '.record', function() {
            $itemassign.setitemtarget(jQuery(this).data('recorddata'));

            $selectitem.hide();
            $selectitem.empty();
            $selectitem.$back.remove();
            $itemassign.show();
            $itemassign.$back.show();
            $itemassign.$selectitem.show();
        })
    ;
    $selectitem.showscreen = function() {
        for (var i = 0; i < $itemassign.data('itemstobeassigned').length; i++) {
            var html = [], barcodevalue, mfgserialvalue, rfidvalue;

            barcodevalue   = (($itemassign.data('recorddata').trackedby == 'BARCODE') && ($itemassign.data('itemstobeassigned')[i].barcode   == '')) ? 'PENDING' : $itemassign.data('itemstobeassigned')[i].barcode;
            mfgserialvalue = (($itemassign.data('recorddata').trackedby == 'SERIAL')  && ($itemassign.data('itemstobeassigned')[i].mfgserial == '')) ? 'PENDING' : $itemassign.data('itemstobeassigned')[i].mfgserial;
            rfidvalue      = (($itemassign.data('recorddata').trackedby == 'RFID')    && ($itemassign.data('itemstobeassigned')[i].rfid      == '')) ? 'PENDING' : $itemassign.data('itemstobeassigned')[i].rfid;

            html.push('<div class="record item">');
            if ($itemassign.data('recorddata').trackedby == 'RFID') {
                html.push('  <div class="row">');
                html.push('    <div class="caption fixed">RFID No:</div>');
                html.push('    <div class="value rfid">' + rfidvalue + '</div>');
                html.push('  </div>');
            }
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Barcode:</div>');
            html.push('    <div class="value barcode">' + barcodevalue + '</div>');
            html.push('    <div class="caption dynamic">Serial No:</div>');
            html.push('    <div class="value mfgserial">' + mfgserialvalue + '</div>');
            html.push('  </div>');
            html.push('</div>');
            $item = jQuery(html.join(''));
            $item.data('recorddata', $itemassign.data('itemstobeassigned')[i]);
            $selectitem.append($item);
        }

        $selectitem.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', 'back', true, function() {
            $selectitem.hide();
            $selectitem.empty();
            jQuery(this).remove();
            $itemassign.show();
            $itemassign.$back.show();
            $itemassign.$selectitem.show();
        });

        $selectitem.show();
    }

    screen.load = function() {
        application.setScanTarget('div[data-datafield="barcode"] input');
        $search.searchall();
        $tabpo.click();
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.registerListener('deviceConnected', 'deviceConnected_unassigneditemscontrollerjs_getUnassignedItemsScreen', function() {
                RwRFID.isConnected = true;
            });
            window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_unassigneditemscontrollerjs_getUnassignedItemsScreen', function() {
                RwRFID.isConnected = false;
            });
        }
    };

    screen.unload = function () {
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_unassigneditemscontrollerjs_getStagingScreen');
            window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_unassigneditemscontrollerjs_getStagingScreen');
        }
    };
    
    return screen;
};
//----------------------------------------------------------------------------------------------