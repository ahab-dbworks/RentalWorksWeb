class AssignItemsClass {
    //----------------------------------------------------------------------------------------------
    getMenuScreen() {
        var viewModel: any = {
            captionPageTitle: RwLanguages.translate('Assign Items')
        };
        viewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assignitemsmenu').html(), viewModel);
        var screen: any = {};
        screen.$view = FwMobileMasterController.getMasterView(viewModel);

        screen.$view
            .on('click', '#miNewItems', function() {
                try {
                    program.navigate('assignitems/newitems');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '#miExistingItems', function() {
                try {
                    program.navigate('assignitems/existingitems');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    getNewItemsScreen() {
        var selectedrecord, selectedstatus;
        var viewModel: any = {
            captionPageTitle: RwLanguages.translate('Assign Items')
        };
        viewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assignitems-newitems').html(), viewModel);
        var screen: any = {};
        screen.$view = FwMobileMasterController.getMasterView(viewModel);

        var $fwcontrols = screen.$view.find('.fwcontrol');
        FwControl.renderRuntimeControls($fwcontrols);

        var $search       = screen.$view.find('.ui-search');
        var $assetdetails = screen.$view.find('.ui-assetdetails');
        var $itemlist     = screen.$view.find('.ui-itemlist');
        var $itemassign   = screen.$view.find('.ui-itemassign');
        var $multiscan    = screen.$view.find('.ui-multiscan');

        $search.find('#searchcontrol').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        try {
                            program.navigate('assignitems');
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
                var html: string[] | string = [];
                html.push('<div class="record ' + ((model.rowtype === 'PO') ? 'poitem' : 'item') + '">');
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
                    if (selectedrecord.rowtype === 'I-CODE') {
                        $assetdetails.showscreen();
                    } else if (selectedrecord.rowtype === 'PO') {
                        $itemlist.showscreen();
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        $search.showscreen = function() {
            $search.show();
            $search.find('#finditem').fwmobilesearch('search');
            program.setScanTarget('.ui-search .fwmobilesearch .searchbox');
        };

        $assetdetails.find('#assetdetailscontrol').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        try {
                            selectedrecord = {};
                            $assetdetails.hide();
                            $search.showscreen();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }
            ]
        });
        $assetdetails.showscreen = function () {
            $assetdetails.show();
            $assetdetails.find('.assetinfo').html(selectedrecord.master);
            $assetdetails.loaddetails();
        };
        $assetdetails.loaddetails = function () {
            var request = {
                selectedrecord: selectedrecord
            };
            RwServices.callMethod("AssignItem", "GetAssignAssetDetails", request, function(response) {
                $assetdetails.find('.details').empty();
                if (typeof response !== 'undefined' && typeof response.results !== 'undefined' && response.results.length === 1 && 
                    typeof response.results[0].orderno !== 'undefined' && response.results[0].orderno === '' &&
                    typeof response.results[0].statustype !== 'undefined' && response.results[0].statustype === 'IN') {
                    $assetdetails.hide();
                    selectedstatus = response.results[0];
                    selectedstatus.goBackToSearch = true;
                    $itemlist.showscreen();
                } else {
                    for (var i = 0; i < response.results.length; i++) {
                        var html = [];
                        html.push('<div class="detailrecord ' + ((response.results[i].statustype === 'IN') ? 'in' : 'out') + '">');
                        html.push('  <div class="row">');
                        html.push('    <div class="caption">Status:</div>');
                        html.push('    <div class="value">' + response.results[i].statustype + '</div>');
                        html.push('    <div class="caption">Qty:</div>');
                        html.push('    <div class="value">' + response.results[i].qty + '</div>');
                        html.push('  </div>');
                        if (response.results[i].orderid !== '') {
                            html.push('  <div class="row">');
                            html.push('    <div class="caption">Order No:</div>');
                            html.push('    <div class="value">' + response.results[i].orderno + '</div>');
                            html.push('  </div>');
                            html.push('  <div class="row">');
                            html.push('    <div class="caption">Order:</div>');
                            html.push('    <div class="value">' + response.results[i].orderdesc + '</div>');
                            html.push('  </div>');
                            html.push('</div>');
                        }
                        var $record = jQuery(html.join(''));
                        $record.data('recorddata', response.results[i]);
                        $assetdetails.find('.details').append($record);
                    }
                }
            });
        };
        $assetdetails
            .on('click', '.detailrecord', function () {
                $assetdetails.hide();
                selectedstatus = jQuery(this).data('recorddata');
                $itemlist.showscreen();
            })
        ;

        $itemlist.find('#itemscontrol').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        try {
                            if (selectedrecord.rowtype === 'I-CODE') {
                                if (typeof selectedstatus.goBackToSearch === 'boolean' && selectedstatus.goBackToSearch) {
                                    selectedstatus = {};
                                    $search.showscreen();
                                } else {
                                    selectedstatus = {};
                                    $assetdetails.showscreen();
                                }
                            } else if (selectedrecord.rowtype === 'PO') {
                                selectedrecord = {};
                                $search.showscreen();
                            }
                            $itemlist.find('#items').fwmobilesearch('clearsearchbox');
                            $itemlist.hide();
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
                    selectedrecord: selectedrecord,
                    selectedstatus: selectedstatus
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
                var html: string[] | string = [];
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
            program.setScanTarget('.ui-itemlist .fwmobilesearch .searchbox');

            if ((selectedrecord.trackedby == 'RFID') && (selectedrecord.allowmassrfidassignment === 'T')) {
                $itemlist.find('#itemscontrol').fwmobilemodulecontrol('showButton', '#itemlist_menu'); //2017-1-09 MY: Remove when more items are added to this menu
                $itemlist.find('#itemscontrol').fwmobilemodulecontrol('showButton', '#multiscanrfid');
            } else {
                $itemlist.find('#itemscontrol').fwmobilemodulecontrol('hideButton', '#itemlist_menu'); //2017-1-09 MY: Remove when more items are added to this menu
                $itemlist.find('#itemscontrol').fwmobilemodulecontrol('hideButton', '#multiscanrfid');
            }
        };

        let moduleControlButtons: any[] = [];
        moduleControlButtons.push({
            caption: 'Back',
            orientation: 'left',
            icon: '&#xE5CB;', //chevron_left
            state: 0,
            buttonclick: function () {
                try {
                    $itemassign.clearscreen();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        moduleControlButtons.push({
            caption: 'Assign',
            orientation: 'right',
            icon: '&#xE161;', //save
            state: 0,
            buttonclick: function () {
                try {
                    var request, updaterecord, recorddata, barcode, mfgserial, rfid, mfgdate;
                    recorddata = $itemassign.data('recorddata');
                    barcode = $itemassign.find('div[data-datafield="barcode"] input').val();
                    mfgserial = $itemassign.find('div[data-datafield="mfgserial"] input').val();
                    rfid = $itemassign.find('div[data-datafield="rfid"] input').val();
                    mfgdate = $itemassign.find('div[data-datafield="mfgdate"] input').val();
                    updaterecord = ((barcode != '') && (recorddata.barcode != barcode)) ||
                        ((mfgserial != '') && (recorddata.mfgserial != mfgserial)) ||
                        ((rfid != '') && (recorddata.rfid != rfid)) ||
                        ((mfgdate != '') && (recorddata.mfgdate != mfgdate));

                    if (updaterecord == true) {
                        request = {
                            selectedrecord: selectedrecord,
                            recorddata: recorddata,
                            barcode: barcode,
                            mfgserial: mfgserial,
                            rfid: rfid,
                            mfgdate: mfgdate
                        };
                        RwServices.callMethod("AssignItem", "UpdateAssignableItemAsset", request, function (response) {
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
        });
        if (RwRFID.isConnected && RwRFID.isTsl) {
            moduleControlButtons.push({
                caption: '',
                orientation: 'right',
                icon: '&#xE8BF;', //settings_input_antenna
                state: 0,
                buttonclick: function () {
                    try {
                        RwRFID.setTslRfidPowerLevel();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            });
        }
        $itemassign.find('#itemassigncontrol').fwmobilemodulecontrol({
            buttons: moduleControlButtons
        });
        $itemassign.showscreen = function(recorddata) {
            $itemassign.data('recorddata', recorddata);
            $itemassign.show();
            program.setScanTarget('.txtbarcode .fwformfield-value');
            program.setScanTargetLpNearfield('.txtrfid .fwformfield-value', false);
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
                program.playStatus(false);
            }
        };
        $itemassign.clearscreen = function() {
            program.setScanTarget('');
            program.setScanTargetLpNearfield('');
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
                    icon:        '&#xE5CB;', //chevron_left
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
            $multiscan.find('.multiscan-tags').empty();
            $multiscan.find('.multiscan-readready').hide();
            $multiscan.find('.multiscan-read').remove();

            var html = [];
            html.push('<div class="multiscan-read">');
            html.push('  <div class="tagsread">Tags Read</div>');
            html.push('  <div class="tagsreadcount"></div>');
            html.push('  <div class="buttonrow">');
            html.push('    <div class="fwformcontrol cancel" data-type="button">Cancel</div>');
            html.push('    <div class="fwformcontrol submit" data-type="button">Submit</div>');
            html.push('  </div>');
            html.push('</div>');
            var $multiscanread = jQuery(html.join(''));

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

            var epcsTags = epcs.split(',');
            $multiscanread.find('.tagsreadcount').html(epcsTags.length);

            $multiscan.append($multiscanread);
        };
        $multiscan.postTags = function (epcs) {
            var request = {
                epcs:           epcs,
                selectedrecord: selectedrecord,
                selectedstatus: selectedstatus
            };
            RwServices.callMethod("AssignItem", "MultiScanTags", request, function(response) {
                try {
                    var html, $record, qty;
                    if (selectedrecord.rowtype === 'I-CODE') {
                        if (response.selectedstatusupdate != null) selectedstatus = response.selectedstatusupdate;
                        qty = (response.selectedstatusupdate != null) ? response.selectedstatusupdate.qty : '0';
                    } else if (selectedrecord.rowtype === 'PO') {
                        if (response.selectedrecordupdate != null) selectedrecord = response.selectedrecordupdate;
                        qty = (response.selectedrecordupdate != null) ? response.selectedrecordupdate.qty : '0';
                    }
                    $multiscan.find('.multiscan-tags').empty();
                    $multiscan.find('.multiscan-readready').hide();
                    $multiscan.find('.pending .value').html(qty);
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
            var qty;

            if (selectedrecord.rowtype === 'I-CODE') {
                qty = selectedstatus.qty;
            } else if (selectedrecord.rowtype === 'PO') {
                qty = selectedrecord.qty;
            }

            $multiscan.show();
            $multiscan.find('.multiscan-title').html(selectedrecord.master);
            $multiscan.find('.pending .value').html(qty);
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
        };

        screen.unload = function () {

        };
    
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    getExistingItemsScreen() {
        var selectedrecord;
        var viewModel: any = {
            captionPageTitle: RwLanguages.translate('Assign Items')
        };
        viewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assignitems-existingitems').html(), viewModel);
        var screen: any = {};
        screen.$view = FwMobileMasterController.getMasterView(viewModel);

        var $fwcontrols = screen.$view.find('.fwcontrol');
        FwControl.renderRuntimeControls($fwcontrols);

        var $scan       = screen.$view.find('.ui-scan');
        var $itemassign = screen.$view.find('.ui-itemassign');

        $scan.find('#scancontrol').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        try {
                            program.navigate('assignitems');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }
            ]
        });
        $scan.showscreen = function() {
            $scan.show();
            program.setScanTarget('.ui-scan .fwmobilecontrol-value');
            program.setScanTargetLpNearfield('.ui-scan .fwmobilecontrol-value', false);
            RwRFID.registerEvents($scan.rfidscan);
        };
        $scan.on('change', '.fwmobilecontrol-value', function() {
            var $this = jQuery(this);
            if ($this.val() != '')
            {
                var request = {
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
                program.playStatus(false);
            }
        };

        $itemassign.find('#itemassigncontrol').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
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
                            var recorddata = $itemassign.data('recorddata');
                            var barcode    = $itemassign.find('div[data-datafield="barcode"] input').val();
                            var mfgserial  = $itemassign.find('div[data-datafield="mfgserial"] input').val();
                            var rfid       = $itemassign.find('div[data-datafield="rfid"] input').val();
                            var mfgdate    = $itemassign.find('div[data-datafield="mfgdate"] input').val();
                            var updaterecord = ((barcode   != '') && (recorddata.barcode   != barcode))   ||
                                               ((mfgserial != '') && (recorddata.mfgserial != mfgserial)) ||
                                               ((rfid      != '') && (recorddata.rfid      != rfid))      ||
                                               ((mfgdate   != '') && (recorddata.mfgdate   != mfgdate));

                            if (updaterecord == true) {
                                var request = {
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
            program.setScanTarget('div[data-datafield="barcode"] input');
            program.setScanTargetLpNearfield('.txtrfid .fwformfield-value', false);

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
                program.playStatus(false);
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
            program.setScanTarget('');
            program.setScanTargetLpNearfield('');

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
            if (typeof window.DTDevices === 'object') {
                program.setScanTarget('#scanBarcodeView-txtBarcodeData');
                program.setScanTargetLpNearfield('');
            }
            if (typeof window.TslReader !== 'undefined') {
                window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_unassigneditemscontrollerjs_getUnassignedItemsScreen');
                window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_unassigneditemscontrollerjs_getUnassignedItemsScreen');
            }
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
}

var AssignItems = new AssignItemsClass();
