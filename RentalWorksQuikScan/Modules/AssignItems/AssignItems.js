var AssignItems = {};
//----------------------------------------------------------------------------------------------
AssignItems.getModuleScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $fwcontrols, selectedrecord, selecteditem, $search, $itemlist, $itemlistmenu, $itemassign, $multiscan;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Assign Items')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assignitems').html(), combinedViewModel);

    screen            = {};
    screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.properties = properties;

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    $search     = screen.$view.find('.ui-search');
    $itemlist   = screen.$view.find('.ui-itemlist');
    $itemassign = screen.$view.find('.ui-itemassign');
    $multiscan  = screen.$view.find('.ui-multiscan');

    $search.find('#finditem').fwmobilesearch({
        service:   'AssignItem',
        method:    'ItemSearch',
        searchModes: [
            { value: 'ICODE',       caption: 'I-Code' },
            { value: 'DESCRIPTION', caption: 'Description' },
            { value: 'TRACKEDBY',   caption: 'Tracked By' },
            { value: 'PONO',        caption: 'PO No.' }
        ],
        cacheItemTemplate: false,
        itemTemplate: function(model) {
            var html = [];
            html.push('<div class="record ' + ((model.rowtype == 'PO') ? 'poitem' : 'item') + '">');
            html.push('  <div class="row">');
            html.push('    <div class="value desc">{{master}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">I-Code:</div>');
            html.push('    <div class="value icode">{{masterno}}</div>');
            html.push('    <div class="caption dynamic">Qty:</div>');
            html.push('    <div class="value qty">{{qty}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Tracked By:</div>');
            html.push('    <div class="value trackedby">{{trackedby}}</div>');
            html.push('  </div>');
            if (model.orderno !== '') {
                html.push('  <div class="row">');
                html.push('    <div class="caption fixed">PO Desc:</div>');
                html.push('    <div class="value podesc">{{orderdesc}}</div>');
                html.push('  </div>');
                html.push('  <div class="row">');
                html.push('    <div class="caption fixed">PO No:</div>');
                html.push('    <div class="value pono">{{orderno}}</div>');
                html.push('    <div class="caption dynamic">As Of:</div>');
                html.push('    <div class="value asof">{{statusdate}}</div>');
                html.push('  </div>');
                html.push('  <div class="row">');
                html.push('    <div class="caption fixed">Vendor:</div>');
                html.push('    <div class="value vendor">{{vendor}}</div>');
                html.push('  </div>');
                html.push('</div>');
            }
            html.push('</div>');
            html = html.join('\n');
            return html;
        },
        recordClick: function(recorddata) {
            $search.hide();
            selectedrecord = recorddata;
            $itemlist.showscreen();
        }
    });
    $search.showscreen = function() {
        $search.show();
        $search.find('#finditem').fwmobilesearch('search');
        application.setScanTarget('.ui-search .fwmobilesearch .searchbox');
    };

    $itemlist.find('#itemscontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        'arrow_back',
                state:       0,
                buttonclick: function () {
                    selectedrecord = {};
                    $itemlist.find('#items').fwmobilesearch('clearsearchbox');
                    $itemlist.hide();
                    $search.showscreen();
                }
            },
            {
                id:          'itemlist_menu',
                type:        'menu',
                orientation: 'right',
                icon:        'more_vert',
                state:       0,
                menuoptions: [
                    {
                        id:      'multiscanrfid',
                        caption: 'Multi-Scan RFID',
                        buttonclick: function() {
                           $itemlist.hide();
                           $multiscan.showscreen();
                        }
                    }
                ]
            }
        ]
    });
    $itemlist.find('#items').fwmobilesearch({
        service:   'AssignItem',
        method:    'GetAssignableItemAssets',
        searchModes: [
            { value: 'BARCODE',  caption: 'Barcode' },
            { value: 'RFID',     caption: 'RFID' },
            { value: 'SERIALNO', caption: 'Serial No' }
        ],
        getRequest: function() {
            var request = {
                selectedrecord: selectedrecord
            };
            return request;
        },
        cacheItemTemplate: false,
        itemTemplate: function(model) {
            var html = [];
            html.push('<div class="itemrecord">');
            if (selectedrecord.rowtype == 'I-CODE') {
                if (model.barcode !== '') {
                    html.push('  <div class="row">');
                    html.push('    <div class="caption">Barcode:</div>');
                    html.push('    <div class="value barcode">' + ((model.rentalitemid == model.barcode) ? 'Unassigned' : model.barcode) + '</div>');
                    html.push('  </div>');
                }
                if (model.mfgserial !== '') {
                    html.push('  <div class="row">');
                    html.push('    <div class="caption">Serial No:</div>');
                    html.push('    <div class="value mfgserial">' + ((model.rentalitemid == model.mfgserial) ? 'Unassigned' : model.mfgserial) + '</div>');
                    html.push('  </div>');
                }
                if (selectedrecord.trackedby == 'RFID') {
                    var rfid;
                    if ((model.rfid == model.rentalitemid) ||
                        (model.rfid == model.barcode)      ||
                        (model.rfid == model.mfgserial)    ||
                        (model.rfid == '')) {
                        rfid = 'Unassigned';
                    } else {
                        rfid = model.rfid;
                    }
                    html.push('  <div class="row">');
                    html.push('    <div class="caption">RFID No:</div>');
                    html.push('    <div class="value rfid">' + rfid + '</div>');
                    html.push('  </div>');
                }
            } else if (selectedrecord.rowtype == 'PO') {
                if ((selectedrecord.trackedby == 'BARCODE') || (model.barcode != '')) {
                    html.push('  <div class="row">');
                    html.push('    <div class="caption">Barcode:</div>');
                    html.push('    <div class="value barcode">' + ((model.barcode != '') ? model.barcode : 'Unassigned') + '</div>');
                    html.push('  </div>');
                }
                if ((selectedrecord.trackedby == 'SERIALNO') || (model.mfgserial != '')) {
                    html.push('  <div class="row">');
                    html.push('    <div class="caption">Serial No:</div>');
                    html.push('    <div class="value mfgserial">' + ((model.mfgserial != '') ? model.mfgserial : 'Unassigned') + '</div>');
                    html.push('  </div>');
                }
                if (selectedrecord.trackedby == 'RFID') {
                    html.push('  <div class="row">');
                    html.push('    <div class="caption">RFID No:</div>');
                    html.push('    <div class="value rfid">Unassigned</div>');
                    html.push('  </div>');
                }
            }
            html.push('</div>');
            html = html.join('\n');
            return html;
        },
        recordClick: function(recorddata) {
            $itemassign.showscreen(recorddata);
            $itemlist.hide();
        }
    });
    $itemlist.showscreen = function() {
        $itemlist.find('#items').fwmobilesearch('search');
        $itemlist.find('.iteminfo').html(selectedrecord.master);
        $itemlist.show();
        application.setScanTarget('.ui-itemlist .fwmobilesearch .searchbox');

        if ((selectedrecord.trackedby == 'RFID') && (selectedrecord.qtynonserial > 0)) {
            $itemlist.find('#itemscontrol').fwmobilemodulecontrol('showButton', '#itemlist_menu'); //2017-1-09 MY: Remove when more items are added to this menu
            $itemlist.find('#itemscontrol').fwmobilemodulecontrol('showButton', '#multiscanrfid');
        } else {
            $itemlist.find('#itemscontrol').fwmobilemodulecontrol('hideButton', '#itemlist_menu'); //2017-1-09 MY: Remove when more items are added to this menu
            $itemlist.find('#itemscontrol').fwmobilemodulecontrol('hideButton', '#multiscanrfid');
        }
    };

    $itemassign.find('#itemassigncontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        'arrow_back',
                state:       0,
                buttonclick: function () {
                    $itemassign.clearscreen();
                }
            },
            {
                caption:     'Assign',
                orientation: 'right',
                icon:        'save',
                state:       0,
                buttonclick: function () {
                    var request, updaterecord, recorddata, barcode, mfgserial, rfid, mfgdate;
                    recorddata = $itemassign.data('recorddata');
                    barcode    = $itemassign.find('div[data-datafield="barcode"] input').val();
                    mfgserial  = $itemassign.find('div[data-datafield="mfgserial"] input').val();
                    rfid       = $itemassign.find('div[data-datafield="rfid"] input').val();
                    mfgdate    = $itemassign.find('div[data-datafield="mfgdate"] input').val();
                    updaterecord = ((barcode   != '') && (recorddata.barcode   != barcode))   ||
                                   ((mfgserial != '') && (recorddata.mfgserial != mfgserial)) ||
                                   ((rfid      != '') && (recorddata.rfid      != rfid))      ||
                                   ((mfgdate   != '') && (recorddata.mfgdate   != mfgdate));

                    if (updaterecord == true) {
                        request = {
                            selectedrecord: selectedrecord,
                            recorddata:     recorddata,
                            barcode:        barcode,
                            mfgserial:      mfgserial,
                            rfid:           rfid,
                            mfgdate:        mfgdate
                        };
                        RwServices.call("AssignItem", "UpdateAssignableItemAsset", request, function(response) {
                            if (response.updateitem.status == '0') {
                                $itemassign.clearscreen();
                            } else if (response.updateitem.status != '0') {
                                FwFunc.showError(response.updateitem.msg);
                            }
                        });
                    } else {
                        FwFunc.showError('No new data has been defined.');
                    }
                }
            }
        ]
    });
    $itemassign.showscreen = function(recorddata) {
        $itemassign.data('recorddata', recorddata);
        $itemassign.show();
        application.setScanTarget('div[data-datafield="barcode"] input');
        $itemassign.find('.itemassign-title').html(selectedrecord.master);

        $itemassign.find('div[data-datafield="rfid"]').hide();
        if (selectedrecord.trackedby == 'RFID') {
            $itemassign.find('div[data-datafield="rfid"]').show();
            RwRFID.registerEvents($itemassign.rfidscan);
        }

        if (selectedrecord.rowtype == 'I-CODE') {
            if (recorddata.barcode !== '') {
                FwFormField.setValue($itemassign, 'div[data-datafield="barcode"]', (recorddata.rentalitemid == recorddata.barcode) ? '' : recorddata.barcode);
            }
            if (recorddata.mfgserial !== '') {
                FwFormField.setValue($itemassign, 'div[data-datafield="mfgserial"]', (recorddata.rentalitemid == recorddata.mfgserial) ? '' : recorddata.mfgserial);
            }
            if (recorddata.rfid !== '') {
                var rfid;
                if ((recorddata.rfid == recorddata.rentalitemid) ||
                    (recorddata.rfid == recorddata.barcode)      ||
                    (recorddata.rfid == recorddata.mfgserial)    ||
                    (recorddata.rfid == '')) {
                    rfid = '';
                } else {
                    rfid = recorddata.rfid;
                }
                FwFormField.setValue($itemassign, 'div[data-datafield="rfid"]', rfid);
            }
            if (recorddata.mfgdate !== '') {
                FwFormField.setValue($itemassign, 'div[data-datafield="mfgdate"]', recorddata.mfgdate);
            }
        } else if (selectedrecord.rowtype == 'PO') {
            if (recorddata.barcode !== '') {
                FwFormField.setValue($itemassign, 'div[data-datafield="barcode"]', (recorddata.rentalitemid == recorddata.barcode) ? '' : recorddata.barcode);
            }
            if (recorddata.mfgserial !== '') {
                FwFormField.setValue($itemassign, 'div[data-datafield="mfgserial"]', (recorddata.rentalitemid == recorddata.mfgserial) ? '' : recorddata.mfgserial);
            }
            if (selectedrecord.trackedby == 'RFID') {

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
    $itemassign.clearscreen = function() {
        $itemassign.hide();
        $itemassign.data('recorddata', '');
        FwFormField.setValue($itemassign, 'div[data-datafield="barcode"]', '');
        FwFormField.setValue($itemassign, 'div[data-datafield="mfgserial"]', '');
        FwFormField.setValue($itemassign, 'div[data-datafield="rfid"]', '');
        FwFormField.setValue($itemassign, 'div[data-datafield="mfgdate"]', '');
        if (selectedrecord.trackedby == 'RFID') RwRFID.unregisterEvents();
        $itemlist.showscreen();
    };

    $multiscan.find('#multiscancontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        'arrow_back',
                state:       0,
                buttonclick: function () {
                    $multiscan.clearscreen();
                    $itemlist.showscreen();
                }
            }
        ]
    });
    $multiscan.rfidscan = function(epcs) {
        var request;
        request = {
            epcs:           epcs,
            selectedrecord: selectedrecord
        };
        RwServices.call("AssignItem", "MultiScanTags", request, function(response) {
            var html, $record;
            $multiscan.find('.multiscan-tags').empty();
            $multiscan.find('.multiscan-readready').hide();
            if (response.assignedassetupdate != null) selectedrecord = response.assignedassetupdate;
            $multiscan.find('.pending .value').html((response.assignedassetupdate != null) ? response.assignedassetupdate.qtynonserial : '0');
            $multiscan.find('.tagsread .value').html(response.records.length);
            $multiscan.find('.assigned .value').html(response.assignedcount);
            $multiscan.find('.exception .value').html(response.exceptioncount);
            for (var i = 0; i < response.records.length; i++) {
                html = [];
                html.push('<div class="multiscanrecord ' + response.records[i].status + '">');
                html.push('  <div class="row">');
                html.push('    <div class="caption">RFID:</div>');
                html.push('    <div class="value rfid">' + response.records[i].rfid + '</div>');
                html.push('  </div>');
                html.push('  <div class="row">');
                html.push('    <div class="caption">Status:</div>');
                html.push('    <div class="value status">' + response.records[i].statusmsg + '</div>');
                html.push('  </div>');
                if ((typeof response.records[i].msg !== 'undefined') && (response.records[i].msg !== '')) {
                    html.push('  <div class="row">');
                    html.push('    <div class="caption">Message:</div>');
                    html.push('    <div class="value msg">' + response.records[i].msg + '</div>');
                    html.push('  </div>');
                }
                html.push('</div>');
                $record = jQuery(html.join(''));

                $multiscan.find('.multiscan-tags').append($record);
            }
        });
    };
    $multiscan.showscreen = function() {
        $multiscan.show();
        $multiscan.find('.multiscan-title').html(selectedrecord.master);
        $multiscan.find('.pending .value').html(selectedrecord.qtynonserial);
        RwRFID.registerEvents($multiscan.rfidscan);
    };
    $multiscan.clearscreen = function() {
        $multiscan.hide();
        RwRFID.unregisterEvents();
        $multiscan.find('.multiscan-readready').show();
        $multiscan.find('.assigned .value').html('0');
        $multiscan.find('.exception .value').html('0');
        $multiscan.find('.tagsread .value').html('0');
        $multiscan.find('.multiscan-tags').empty();
    };

    screen.load = function() {
        $search.showscreen();
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
            window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_unassigneditemscontrollerjs_getUnassignedItemsScreen');
            window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_unassigneditemscontrollerjs_getUnassignedItemsScreen');
        }
    };
    
    return screen;
};
//----------------------------------------------------------------------------------------------