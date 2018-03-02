var AssetSetLocation = {};
//----------------------------------------------------------------------------------------------
AssetSetLocation.getModuleScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $search, $scan, $searchbtn;
    combinedViewModel = jQuery.extend({
        captionPageTitle:         RwLanguages.translate('Item Set Location')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-assetsetlocation').html(), combinedViewModel);

    screen            = {};
    screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.properties = properties;

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);

    $searchbtn = jQuery('<i class="material-icons">&#xE8B6;</i>'); //search
    screen.$view.find('#module-controls').append($searchbtn);
    $searchbtn.parent().on('click', function() {
        if ($scan.find('div[data-caption="Cancel"]').is(':visible')) $scan.find('div[data-caption="Cancel"]').click();
        $scan.hide();
        $search.show();
    });

    $scan   = screen.$view.find('.asl-scan');
    $search = screen.$view.find('.asl-search');

    $scan.find('#scancontrol').fwmobilemodulecontrol({
        buttons: [
            { 
                caption:     'Edit',
                orientation: 'right',
                icon:        '&#xE3C9;', //edit
                state:       1,
                buttonclick: function () {
                    $scan.toggleeditable(true);
                    this.nextState();
                }
            },
            { 
                caption:     'Save',
                orientation: 'right',
                icon:        '&#xE3C9;', //check
                state:       2,
                buttonclick: function () {
                    var plugin = this;
                    var qtyflag, request;
                    qtyflag = ($scan.find('.asl-quantity').length > 0);
                    request = {
                        recorddata:    $scan.find('.itemdetails').data('recorddata'),
                        setno:         FwFormField.getValue($scan, 'div[data-datafield="setno"]'),
                        vendor:        FwFormField.getValue($scan, 'div[data-datafield="vendor"]'),
                        primaryvendor: FwFormField.getValue($scan, 'div[data-datafield="primaryvendor"]'),
                        manufacturer:  FwFormField.getValue($scan, 'div[data-datafield="manufacturer"]'),
                        sourceno:      FwFormField.getValue($scan, 'div[data-datafield="sourceno"]'),
                        transactionno: FwFormField.getValue($scan, 'div[data-datafield="transactionno"]'),
                        accountingno:  FwFormField.getValue($scan, 'div[data-datafield="accountingno"]'),
                        acquiredate:   FwFormField.getValue($scan, 'div[data-datafield="acquire"]')
                    };
                    if (qtyflag) {
                        var $asl_records;
                        request.location          = '';
                        request.assetsetlocations = [];
                        $asl_records = $scan.find('.assetsetlocation-record');
                        for (var i = 0; i < $asl_records.length; i++) {
                            var location, qty;
                            location = jQuery($asl_records[i]).find('.location').val();
                            qty      = jQuery($asl_records[i]).find('.qty').html();
                            if ((location !== '') && (qty !== '0')) {
                                request.assetsetlocations.push({'location': location, 'qty': qty, 'rowdata': jQuery($asl_records[i]).data('rowdata')});
                            }
                        }
                    } else {
                        request.location = FwFormField.getValue($scan, 'div[data-datafield="location"]')
                    }
                    RwServices.callMethod("AssetSetLocation", "SaveItemInformation", request, function(response) {
                        plugin.previousState();
                        $scan.loadcallback(response.itemdetails);
                    });
                }
            },
            { 
                caption:     'Cancel',
                orientation: 'left',
                icon:        '&#xE14C;', //clear
                state:       2,
                buttonclick: function () {
                    $scan.toggleeditable(false);
                    this.previousState();
                }
            }
        ]
    });
    $scan
        .on('change', '.fwmobilecontrol-value', function() {
            if (this.value !== '') {
                $scan.search(this.value);
            }
        })
        .on('keydown', '.fwmobilecontrol-value', function(event) {
            try {
                if ((event.keyCode === 13) && (this.value !== '')) {
                    $scan.search(this.value);
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('change', 'div[data-datafield="setcharacter"] .fwformfield-value', function() {
            $scan.setquantitydata(this.value);
            FwFormField.toggle($scan.find('div[data-datafield="setcharacter"]'), false);
        })
        .on('click', '.asl-add', function() {
            var $this, totalqty, $qty, currentqty, $location;
            currentqty        = 0;
            $this             = jQuery(this);
            $qty              = $this.siblings('.qty');
            $location         = $qty.parents('.assetsetlocation-record').find('.location');
            totalqty          = parseInt($scan.find('.assetsetlocation-totals-value').html());
            if (!$location.is(':disabled')) {
                $scan.find('.assetsetlocation-record .qty').each(function(index, element) {
                    currentqty = currentqty + parseInt(jQuery(element).html());
                });
                if ((totalqty > currentqty) && ($location.val() !== '')) {
                    $qty.html(parseInt($qty.html()) + 1);
                }
            }
        })
        .on('click', '.asl-subtract', function() {
            var $this, totalqty, $qty, currentqty, $location;
            currentqty        = 0;
            $this             = jQuery(this);
            $qty              = $this.siblings('.qty');
            $location         = $qty.parents('.assetsetlocation-record').find('.location');
            totalqty          = parseInt($scan.find('.assetsetlocation-totals-value').html());
            if (!$location.is(':disabled')) {
                $scan.find('.assetsetlocation-record .qty').each(function(index, element) {
                    currentqty = currentqty + parseInt(jQuery(element).html());
                });
                if ((totalqty >= currentqty) && (parseInt($qty.html()) !== 0)) {
                    $qty.html(parseInt($qty.html()) - 1);
                }
            }
        })
        .on('change', '.location', function() {
            var $this = jQuery(this);
            $scan.find('.assetsetlocation-record .location').each(function(index, element) {
                if ((element !== $this[0]) && (jQuery(element).val() === $this.val())) {
                    $this.val('');
                    $this.parents('.assetsetlocation-record').find('.qty').html('0');
                }
            });
            if (($this.val() !== '') && (!$this.hasClass('triggered'))) {
                $this.addClass('triggered');
                $scan.addassetsetlocation();
                $scan.find('.assetsetlocation-record .editable').removeAttr('disabled');
            }
        })
    ;
    $scan.search = function(searchvalue) {
        var request;
        $scan.resetscreen();
        request = {
            searchvalue: searchvalue
        };
        RwServices.callMethod("AssetSetLocation", "ScanSearch", request, function(response) {
            $scan.loadcallback(response.itemdetails);
        });
    };
    $scan.loadcallback = function(itemdetails) {
        $scan.data('records', itemdetails);
        if (itemdetails.length === 0) {
            $scan.find('.error').html('Barcode/I-Code is invalid and does not exist in inventory.').show();
        } else if ((itemdetails.length === 1) && (itemdetails[0].trackedby === 'BARCODE')) { //Scanning a barcode
            if (itemdetails[0].statustype === 'OUT') {
                $scan.find('.fwmobilecontrol-value').val('');
                $scan.loadbarcode(itemdetails[0]);
            } else {
                $scan.find('.error').html('Barcode/I-Code is not on a set.').show();
            }
        } else if (itemdetails[0].trackedby === 'QUANTITY') { //Scanning an I-Code
            $scan.find('.fwmobilecontrol-value').val('');
            $scan.loadquantity(itemdetails);
        }
    };
    $scan.loadbarcode = function(recorddata) {
        $scan.find('.itemdetails').html(Mustache.render(jQuery('#tmpl-assetsetlocationbarcodetemplate').html())).show();
        FwControl.renderRuntimeControls($scan.find('.itemdetails .fwcontrol'));
        $scan.find('#scancontrol').fwmobilemodulecontrol('changeState', 1);
        $scan.find('.itemdetails').data('recorddata', recorddata);
        $scan.find('.itemdetails-title').html(recorddata.master);

        FwFormField.loadItems($scan.find('div[data-datafield="setcharacter"]'), [{'value': recorddata.orderid, 'text': recorddata.setcharacter}], true);
        FwFormField.setValue($scan, 'div[data-datafield="location"]',      recorddata.location);
        $scan.loadshareddata(recorddata);
    };
    $scan.loadquantity = function(recorddata) {
        $scan.find('.itemdetails').html(Mustache.render(jQuery('#tmpl-assetsetlocationquantitytemplate').html())).show();
        FwControl.renderRuntimeControls($scan.find('.itemdetails .fwcontrol'));
        $scan.find('.itemdetails-title').html(recorddata[0].master);
        $scan.addassetsetlocation();

        if (recorddata.length > 1) {
            var setchararray = [];
            for (var i = 0; i < recorddata.length; i++) {
                setchararray.push({'value': recorddata[i].orderid, 'text': recorddata[i].setcharacter});
            }
            FwFormField.loadItems($scan.find('div[data-datafield="setcharacter"]'), setchararray, false);
            FwFormField.toggle($scan.find('div[data-datafield="setcharacter"]'), true);
        } else {
            $scan.find('.itemdetails').data('recorddata', recorddata[0]);
            FwFormField.loadItems($scan.find('div[data-datafield="setcharacter"]'), [{'value': recorddata[0].orderid, 'text': recorddata[0].setcharacter}], true);
            $scan.find('#scancontrol').fwmobilemodulecontrol('changeState', 1);
            $scan.loadshareddata(recorddata[0]);
            $scan.loadquantityassetlocations(recorddata[0].orderid, recorddata[0].masterid);
        }
    };
    $scan.setquantitydata = function(orderid) {
        var data;
        data = $scan.data('records');
        for (var i = 0; i < data.length; i++) {
            if (data[i].orderid === orderid) {
                $scan.find('.itemdetails').data('recorddata', data[i]);
                $scan.find('#scancontrol').fwmobilemodulecontrol('changeState', 1);
                $scan.loadshareddata(data[i]);
                $scan.loadquantityassetlocations(data[i].orderid, data[i].masterid);
                break;
            }
        }
    };
    $scan.loadquantityassetlocations = function(orderid, masterid) {
        var request;
        $scan.find('.assetsetlocation-body').empty();
        $scan.find('.assetsetlocation-totals-value').html('');
        request = {
            orderid:  orderid,
            masterid: masterid
        }
        RwServices.callMethod("AssetSetLocation", "LoadAssetLocations", request, function(response) {
            var totalqty = 0;
            for (var i = 0; i < response.assetlocations.length; i++) {
                $scan.addassetsetlocation();
                $scan.find('.assetsetlocation-record[data-recordcount="' + i + '"] .location').val(response.assetlocations[i].location);
                $scan.find('.assetsetlocation-record[data-recordcount="' + i + '"] .qty').html(response.assetlocations[i].qty);
                totalqty = totalqty + parseInt(response.assetlocations[i].qty);
                $scan.find('.assetsetlocation-record[data-recordcount="' + i + '"]').data('rowdata', response.assetlocations[i]);
            }
            $scan.find('.assetsetlocation-totals-value').html(totalqty);
        });
    };
    $scan.addassetsetlocation = function() {
        var html = [], $assetsetlocation;

        html.push('<div class="assetsetlocation-record flexrow" data-recordcount="' + $scan.find('.assetsetlocation-record').length + '">');
        html.push('  <div class="assetsetlocation-col1">');
        html.push('    <input type="text" class="location editable" disabled />');
        html.push('  </div>');
        html.push('  <div class="assetsetlocation-col2 flexrow">');
        html.push('    <i class="material-icons md-dark asl-subtract">&#xE15D;</i>'); //remove_circle_outline
        html.push('    <div class="qty">0</div>');
        html.push('    <i class="material-icons md-dark asl-add">&#xE148;</i>'); //add_circle_outline
        html.push('  </div>');
        html.push('</div>');
        $assetsetlocation = jQuery(html.join(''));

        $scan.find('.assetsetlocation-body').append($assetsetlocation);
    };
    $scan.loadshareddata = function(recorddata) {
        FwFormField.setValue($scan, 'div[data-datafield="setno"]',         recorddata.setnoid, recorddata.setno);
        FwFormField.setValue($scan, 'div[data-datafield="department"]',    recorddata.department);
        FwFormField.setValue($scan, 'div[data-datafield="departmentno"]',  recorddata.departmentno);
        FwFormField.setValue($scan, 'div[data-datafield="production"]',    recorddata.production);
        FwFormField.setValue($scan, 'div[data-datafield="productionno"]',  recorddata.productionno);
        FwFormField.setValue($scan, 'div[data-datafield="vendor"]',        recorddata.vendorid, recorddata.vendor);
        FwFormField.setValue($scan, 'div[data-datafield="primaryvendor"]', recorddata.buyerid,  recorddata.primaryvendor);
        FwFormField.setValue($scan, 'div[data-datafield="manufacturer"]',  recorddata.mfgid,    recorddata.manufacturer);
        FwFormField.setValue($scan, 'div[data-datafield="sourceno"]',      recorddata.sourcecode);
        FwFormField.setValue($scan, 'div[data-datafield="transactionno"]', recorddata.transactionno);
        FwFormField.setValue($scan, 'div[data-datafield="accountingno"]',  recorddata.accountingcode);
        FwFormField.setValue($scan, 'div[data-datafield="refnobox"]',      recorddata.refnobox);
        FwFormField.setValue($scan, 'div[data-datafield="refnopallet"]',   recorddata.refnopallet);
        FwFormField.setValue($scan, 'div[data-datafield="cost"]',          recorddata.cost);
        FwFormField.setValue($scan, 'div[data-datafield="acquire"]',       recorddata.dateacquired);
        if ((recorddata.thumbnail !== null) && (recorddata.thumbnail.length > 0)) {
            $scan.find('.imagecontainer').html('<img src="data:image/jpg;base64,' + recorddata.thumbnail + '" />');
        }
    };
    $scan.toggleeditable = function(editable) {
        var qtyflag = ($scan.find('.asl-quantity').length > 0);
        FwFormField.toggle($scan.find('.fwformfield.editable'), editable);
        if (editable) {
            if (qtyflag) {
                $scan.addassetsetlocation();
                $scan.find('.assetsetlocation-record .editable').removeAttr('disabled');
            }
        } else {
            $scan.loadshareddata($scan.find('.itemdetails').data('recorddata'));
            if (qtyflag) {
                $scan.find('.assetsetlocation-record .editable').attr('disabled', 'disabled');
                $scan.find('.assetsetlocation-record').each(function(index, element) {
                    var $this = jQuery(element);
                    if (typeof $this.data('rowdata') === 'undefined') {
                        $this.remove();
                    } else {
                        $this.find('.location').val($this.data('rowdata').location);
                        $this.find('.qty').html($this.data('rowdata').qty);
                    }
                });
            }
        }
    };
    $scan.resetscreen = function() {
        $scan.find('.error').empty().hide();
        $scan.find('#scancontrol').fwmobilemodulecontrol('changeState', 0);
        $scan.find('.itemdetails').hide().empty();
    };

    $search.find('#finditem').fwmobilesearch({
        service:   'AssetSetLocation',
        method:    'ItemSearch',
        searchModes: [
            { value: 'DESCRIPTION',  caption: 'Description' },
            { value: 'DEPARTMENT',   caption: 'Department' },
            { value: 'PRODUCTION',   caption: 'Production' },
            { value: 'SETCHARACTER', caption: 'Set Character' }
        ],
        cacheItemTemplate: false,
        itemTemplate: function(model) {
            var html = [];

            html.push('<div class="record">');
            html.push('  <div class="column" style="flex: 0 0 108px;">');
            if (model.thumbnail.length > 0) {
                html.push('    <div class="image"><img src="{{thumbnail}}" /></div>');
            }
            html.push('  </div>');
            html.push('  <div class="column">');
            html.push('    <div class="row">');
            html.push('      <div class="caption fixed">I-Code:</div><div class="value" style="color:#00aaff;text-decoration: underline;">{{masterno}}</div>');
            html.push('    </div>');
            html.push('    <div class="row">');
            html.push('      <div class="caption fixed">Description:</div><div class="value">{{master}}</div>');
            html.push('    </div>');
            html.push('    <div class="row">');
            html.push('      <div class="caption fixed">Barcode:</div><div class="value">{{barcode}}</div>');
            html.push('    </div>');
            html.push('    <div class="row">');
            html.push('      <div class="caption fixed">Set Character:</div><div class="value">{{setcharacter}}</div>');
            html.push('    </div>');
            html.push('    <div class="row">');
            html.push('      <div class="caption fixed">Department:</div><div class="value">{{department}}</div>');
            html.push('    </div>');
            html.push('    <div class="row">');
            html.push('      <div class="caption fixed">Production:</div><div class="value">{{production}}</div>');
            html.push('    </div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('');

            return html;
        },
        recordClick: function(recorddata) {
            var request;
            $search.hide();
            $scan.resetscreen();
            $scan.show();
            request = {
                recorddata: recorddata
            };
            RwServices.callMethod("AssetSetLocation", "LoadItemInformation", request, function(response) {
                $scan.loadcallback(response.itemdetails);
            });
        }
    });
    $search.find('#searchcontrol').fwmobilemodulecontrol({
        buttons: [
            { 
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //arrow_back
                state:       0,
                buttonclick: function () {
                    $scan.show();
                    $search.hide();
                }
            }
        ]
    });

    screen.load = function() {
        program.setScanTarget('.asl-scan .fwmobilecontrol-value');
    };

    screen.unload = function () {
        $search.find('#finditem').fwmobilesearch('destroy');
    };
    
    return screen;
};
//----------------------------------------------------------------------------------------------