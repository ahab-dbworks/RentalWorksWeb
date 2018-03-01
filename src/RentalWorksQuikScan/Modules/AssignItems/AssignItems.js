var AssignItems = {};
//----------------------------------------------------------------------------------------------
AssignItems.getMenuScreen = function(viewModel, properties) {
    var screen, combinedViewModel;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Assign Items')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assignitemsmenu').html(), combinedViewModel);

    screen            = {};
    screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.properties = properties;

    screen.$view
        .on('click', '#miNewItems', function() {
            try {
                application.navigate('assignitems/newitems');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#miExistingItems', function() {
            try {
                application.navigate('assignitems/existingitems');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    return screen;
};
//----------------------------------------------------------------------------------------------
AssignItems.getNewItemsScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $fwcontrols, selectedrecord, selecteditem, $search, $itemlist, $itemlistmenu, $itemassign, $multiscan;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Assign Items')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assignitems-newitems').html(), combinedViewModel);

    screen            = {};
    screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.properties = properties;

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    $search     = screen.$view.find('.ui-search');
    $itemlist   = screen.$view.find('.ui-itemlist');
    $itemassign = screen.$view.find('.ui-itemassign');
    $multiscan  = screen.$view.find('.ui-multiscan');

    $search.find('#searchcontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //arrow_back
                state:       0,
                buttonclick: function () {
                    try {
                        application.navigate('assignitems');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            }
        ]
    });
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
            try {
                $search.hide();
                selectedrecord = recorddata;
                $itemlist.showscreen();
            } catch (ex) {
                FwFunc.showError(ex);
            }
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
                icon:        '&#xE5CB;', //arrow_back
                state:       0,
                buttonclick: function () {
                    try {
                        selectedrecord = {};
                        $itemlist.find('#items').fwmobilesearch('clearsearchbox');
                        $itemlist.hide();
                        $search.showscreen();
                    } catch (ex) {
                        FwFunc.showError(ex);
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
                        id:      'multiscanrfid',
                        caption: 'Multi-Scan RFID',
                        buttonclick: function() {
                            try {
                                $itemlist.hide();
                                $multiscan.showscreen();
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
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
        beforeSearch: function() {
            if (this.currentMode == 'BARCODE') {
                $itemlist.find('#items .searchbox').val(RwAppData.stripBarcode($itemlist.find('#items .searchbox').val()));
            }
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
            try {
                $itemassign.showscreen(recorddata);
                $itemlist.hide();
            } catch (ex) {
                FwFunc.showError(ex);
            }
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
                icon:        '&#xE5CB;', //arrow_back
                state:       0,
                buttonclick: function () {
                    try {
                        $itemassign.clearscreen();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            },
            {
                caption:     'Assign',
                orientation: 'right',
                icon:        '&#xE161;', //save
                state:       0,
                buttonclick: function () {
                    try {
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
                            RwServices.callMethod("AssignItem", "UpdateAssignableItemAsset", request, function(response) {
                                try {
                                    if (response.updateitem.status == '0') {
                                        $itemassign.clearscreen();
                                    } else if (response.updateitem.status != '0') {
                                        FwFunc.showError(response.updateitem.msg);
                                    }
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } else {
                            FwFunc.showError('No new data has been defined.');
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            },
            {
                caption:     '',
                orientation: 'right',
                icon:        '&#xE8BF;', //settings_input_antenna
                state:       0,
                buttonclick: function () {
                    try {
                        RwRFID.setTslRfidPowerLevel();
                    } catch (ex) {
                        FwFunc.showError(ex);
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

        if (recorddata.mixedcaseserialno === 'T') {
            $itemassign.find('div[data-datafield="mfgserial"]').attr('data-mixedcase', true);
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
        $itemassign.find('div[data-datafield="mfgserial"]').attr('data-mixedcase', false);
        if (selectedrecord.trackedby == 'RFID') RwRFID.unregisterEvents();
        $itemlist.showscreen();
    };

    $multiscan.find('#multiscancontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5C4;', //arrow_back
                state:       0,
                buttonclick: function () {
                    try {
                        $multiscan.clearscreen();
                        $itemlist.showscreen();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            },
            {
                caption:     '',
                orientation: 'right',
                icon:        '&#xE8BF;', //settings_input_antenna
                state:       0,
                buttonclick: function () {
                    try {
                        RwRFID.setTslRfidPowerLevel();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            }
        ]
    });
    $multiscan.rfidscan = function(epcs) {
        var html = [], $multiscanread, epcsTags;
        $multiscan.find('.multiscan-tags').empty();
        $multiscan.find('.multiscan-readready').hide();
        $multiscan.find('.multiscan-read').remove();

        html.push('<div class="multiscan-read">');
        html.push('  <div class="tagsread">Tags Read</div>');
        html.push('  <div class="tagsreadcount"></div>');
        html.push('  <div class="buttonrow">');
        html.push('    <div class="fwformcontrol cancel" data-type="button">Cancel</div>');
        html.push('    <div class="fwformcontrol submit" data-type="button">Submit</div>');
        html.push('  </div>');
        html.push('</div>');
        $multiscanread = jQuery(html.join(''));

        $multiscanread
            .on('click', '.submit', function() {
                $multiscan.postTags(epcs);
                $multiscanread.remove();
            })
            .on('click', '.cancel', function() {
                $multiscanread.remove();
                $multiscan.find('.multiscan-readready').show();
            })
        ;

        epcsTags = epcs.split(',');
        $multiscanread.find('.tagsreadcount').html(epcsTags.length);

        $multiscan.append($multiscanread);
    };
    $multiscan.postTags = function (epcs) {
        var request;
        request = {
            epcs:           epcs,
            selectedrecord: selectedrecord
        };
        RwServices.callMethod("AssignItem", "MultiScanTags", request, function(response) {
            try {
                var html, $record;
                $multiscan.find('.multiscan-tags').empty();
                $multiscan.find('.multiscan-readready').hide();
                if (response.assignedassetupdate != null) selectedrecord = response.assignedassetupdate;
                $multiscan.find('.pending .value').html((response.assignedassetupdate != null) ? response.assignedassetupdate.qty : '0');
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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    $multiscan.showscreen = function() {
        $multiscan.show();
        $multiscan.find('.multiscan-title').html(selectedrecord.master);
        $multiscan.find('.pending .value').html(selectedrecord.qty);
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
AssignItems.getExistingItemsScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $fwcontrols, selectedrecord, $scan, $itemassign;
    combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Assign Items')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assignitems-existingitems').html(), combinedViewModel);

    screen            = {};
    screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.properties = properties;

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    $scan       = screen.$view.find('.ui-scan');
    $itemassign = screen.$view.find('.ui-itemassign');

    $scan.find('#scancontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5C4;', //arrow_back
                state:       0,
                buttonclick: function () {
                    try {
                        application.navigate('assignitems');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            }
        ]
    });
    $scan.showscreen = function() {
        $scan.show();
        application.setScanTarget('.ui-scan .fwmobilecontrol-value');
        RwRFID.registerEvents($scan.rfidscan);
    };
    $scan.on('change', '.fwmobilecontrol-value', function() {
        var request, $this;
        $this = jQuery(this);
        if ($this.val() != '')
        {
            request = {
                code: $this.val()
            };
            RwServices.callMethod("AssignItem", "GetBarcodeRFIDItem", request, function(response) {
                if (response.recorddata.status == 0) {
                    RwRFID.unregisterEvents();
                    selectedrecord = response.recorddata;
                    $scan.hide();
                    $itemassign.showscreen(response.recorddata);
                    $this.val('');
                } else {
                    FwNotification.renderNotification('ERROR', response.recorddata.msg);
                }
            });
        }
    });
    $scan.rfidscan = function(epcs) {
        if (parseInt(jQuery('.tagCountPopup .tagCount').html()) == 1) {
            $scan.find('.fwmobilecontrol-value').val(epcs).change();
        } else {
            application.playStatus(false);
        }
    };

    $itemassign.find('#itemassigncontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5C4;', //arrow_back
                state:       0,
                buttonclick: function () {
                    try {
                        $itemassign.clearscreen();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            },
            {
                caption:     'Assign',
                orientation: 'right',
                icon:        '&#xE161;', //save
                state:       0,
                buttonclick: function () {
                    try {
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
                            request.selectedrecord.rowtype = 'I-CODE';
                            RwServices.callMethod("AssignItem", "UpdateAssignableItemAsset", request, function(response) {
                                try {
                                    if (response.updateitem.status == '0') {
                                        $itemassign.clearscreen();
                                        FwNotification.renderNotification('SUCCESS', 'Item updated.');
                                    } else if (response.updateitem.status != '0') {
                                        FwFunc.showError(response.updateitem.msg);
                                    }
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } else {
                            FwFunc.showError('No new data has been defined.');
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            },
            {
                caption:     '',
                orientation: 'right',
                icon:        '&#xE8BF;', //settings_input_antenna
                state:       0,
                buttonclick: function () {
                    try {
                        RwRFID.setTslRfidPowerLevel();
                    } catch (ex) {
                        FwFunc.showError(ex);
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

        if (recorddata.mixedcaseserialno === 'T') {
            $itemassign.find('div[data-datafield="mfgserial"]').attr('data-mixedcase', true);
        }

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
    };
    $itemassign.rfidscan = function(epcs) {
        if (($itemassign.find('div[data-datafield="rfid"] input').length != 0) && (parseInt(jQuery('.tagCountPopup .tagCount').html()) == 1)) {
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
        $itemassign.find('div[data-datafield="mfgserial"]').attr('data-mixedcase', false);
        if (selectedrecord.trackedby == 'RFID') RwRFID.unregisterEvents();
        $scan.showscreen();
    };

    screen.load = function() {
        $scan.showscreen();
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.registerListener('deviceConnected', 'deviceConnected_unassigneditemscontrollerjs_getUnassignedItemsScreen', function() {
                RwRFID.isConnected = true;
            });
            window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_unassigneditemscontrollerjs_getUnassignedItemsScreen', function() {
                RwRFID.isConnected = false;
            });
        }
    }

    screen.unload = function () {
        if (typeof window.TslReader !== 'undefined') {
            window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_unassigneditemscontrollerjs_getUnassignedItemsScreen');
            window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_unassigneditemscontrollerjs_getUnassignedItemsScreen');
        }
    };

    return screen;
};
//----------------------------------------------------------------------------------------------