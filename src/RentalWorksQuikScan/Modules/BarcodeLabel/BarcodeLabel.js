var RwBarcodeLabel = {};
RwRoutes.push({
    url: 'barcodelabel',
    action: function () {
        return RwBarcodeLabel.getModuleScreen({}, {});
    }
});
//----------------------------------------------------------------------------------------------
RwBarcodeLabel.getModuleScreen = function (viewModel, properties) {
    var combinedViewModel = jQuery.extend({
        captionPageTitle:   RwLanguages.translate('Barcode Label')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-barcodelabel').html(), combinedViewModel);

    var screen            = {};
    screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.properties = properties;

    var $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    screen.$searchbarcodes     = screen.$view.find('.search-barcodes');
    screen.$searchicodes       = screen.$view.find('.search-icodes');
    screen.$searchbarcodelabel = screen.$view.find('.search-barcodelabel');

    screen.$btnback = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', false, function () {
        try {
            screen.getCurrentPage().back();
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btnprint = FwMobileMasterController.addFormControl(screen, 'Print', 'right', '&#xE8AD;', false, function () {
        try {
            var $records;
            if (screen.getCurrentPage() === screen.pages.barcodesearch) {
                $records = screen.pages.barcodesearch.getElement().find('.record[data-selected="true"]');
            } else if (screen.getCurrentPage() === screen.pages.icodesearch) {
                $records = screen.pages.icodesearch.getElement().find('.record[data-selected="true"]');
            }
            var models = [];
            $records.each(function (index, element) {
                var $record = jQuery(element);
                var model = $record.data('recorddata');
                models.push(model);
            });
            //alert(JSON.stringify(models));
            if ($records.length > 0) {
                if (screen.getCurrentPage() === screen.pages.barcodesearch) {
                    screen.showBarcodePrintingDialog(models, 'Barcode Label');
                } else if (screen.getCurrentPage() === screen.pages.icodesearch) {
                    screen.showBarcodePrintingDialog(models, 'I-Code Label');
                }
            } else {
                FwFunc.showMessage('Select a row to print.');
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btnselectnone = FwMobileMasterController.addFormControl(screen, 'None', 'right', '&#xE14C;', false, function () {
        try {
            var $records;
            if (screen.getCurrentPage().name === 'barcodesearch') {
                $records = screen.pages.barcodesearch.getElement().find('.record');
                $records.attr('data-selected', 'false');
            } else if (screen.getCurrentPage().name === 'icodesearch') {
                $records = screen.pages.icodesearch.getElement().find('.record');
                $records.attr('data-selected', 'false');
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.$btnselectall = FwMobileMasterController.addFormControl(screen, 'All', 'right', '&#xE065;', false, function () {
        try {
            var $records;
            if (screen.getCurrentPage().name === 'barcodesearch') {
                $records = screen.pages.barcodesearch.getElement().find('.record');
                $records.attr('data-selected', 'true');
            } else if (screen.getCurrentPage().name === 'icodesearch') {
                $records = screen.pages.icodesearch.getElement().find('.record');
                $records.attr('data-selected', 'true');
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    screen.getCurrentPage = function () {
        return screen.pagehistory[screen.pagehistory.length - 1];
    };

    screen.pagehistory = [];
    screen.pages = {
        reset: function () {
            for (key in screen.pages) {
                var page = screen.pages[key];
                if (typeof page.getElement === 'function') {
                    page.getElement().hide();
                }
            }
            screen.$btnback.hide();
            screen.$btnselectnone.hide();
            screen.$btnselectall.hide();
            screen.$btnprint.hide();
            if (typeof screen.$btnnewbarcodelabel !== 'undefined') {
                screen.$btnnewbarcodelabel.remove();
            }
            if (typeof screen.$btnsavebarcodelabel !== 'undefined') {
                screen.$btnsavebarcodelabel.remove();
            }
            if (typeof screen.$btncancelbarcodelabel !== 'undefined') {
                screen.$btncancelbarcodelabel.remove();
            }
            FwMobileMasterController.setTitle('');
        },
        menu: {
            name: 'menu',
            getElement: function () {
                return screen.$view.find('.page-menu');
            },
            init: function () {
                if (typeof program.runningInCordova !== 'undefined' && program.runningInCordova === true) {
                    this.getElement().find('.miManageBarcodeLabels').hide();
                }
                screen.$view.find('.miBarcodeLabel').on('click', function () {
                    try {
                        screen.pages.barcodesearch.forward();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                screen.$view.find('.miManageBarcodeLabels').on('click', function () {
                    try {
                        screen.pages.managebarcodelabels.forward();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                screen.$view.find('.miICodeLabel').on('click', function () {
                    try {
                        screen.pages.icodesearch.forward();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            show: function () {
                screen.pages.reset();
                screen.pages.menu.getElement().show();
            },
            forward: function () {
                screen.pagehistory.push(screen.pages.menu);
                screen.pages.menu.show();
            },
            back: function () {

            }
        },
        barcodesearch: {
            name: 'barcodesearch',
            getElement: function () {
                return screen.$view.find('.page-barcodesearch');
            },
            init: function () {
                screen.$searchbarcodes.fwmobilesearch({
                    service: 'BarcodeLabel',
                    method: 'BarcodedItemSearch',
                    searchModes: [
                        { value: 'DESCRIPTION', caption: 'Description' },
                        { value: 'ICODE', caption: 'I-Code' }
                    ],
                    cacheItemTemplate: false,
                    itemTemplate: function (model) {
                        var html = [];
                        html.push('<div class="record" style="border-left:10px solid ' + model.color + '">');
                        html.push('  <div class="row">');
                        html.push('    <div class="value desc">{{master}}</div>');
                        html.push('  </div>');
                        html.push('  <div class="row">');
                        html.push('    <div class="caption fixed">I-Code:</div>');
                        html.push('    <div class="value icode">{{masterno}}</div>');
                        html.push('    <div class="caption fixed">Status:</div>');
                        html.push('    <div class="value statustype">{{statustype}}</div>');
                        html.push('  </div>');
                        html.push('  <div class="row">');
                        html.push('    <div class="caption fixed">Barcode:</div>');
                        html.push('    <div class="value barcode">{{barcode}}</div>');
                        html.push('    <div class="caption fixed">Status Date:</div>');
                        html.push('    <div class="value statusdate">{{statusdate}}</div>');
                        html.push('  </div>');
                        html.push('</div>');
                        html = html.join('\n');
                        return html;
                    },
                    recordClick: function (model, $record) {
                        try {
                            var selected = $record.attr('data-selected') === 'true';
                            $record.attr('data-selected', !selected);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                });
            },
            show: function () {
                screen.pages.reset();
                screen.pages.barcodesearch.getElement().show();
                screen.$btnback.show();
                screen.$btnselectnone.show();
                screen.$btnselectall.show();
                screen.$btnprint.show();
                FwMobileMasterController.setTitle('Print Barcode Label');
                //screen.$view.find('.page-search').addClass('page-slidein').show();
                screen.$searchbarcodes.fwmobilesearch('search');
            },
            forward: function () {
                screen.pagehistory.push(screen.pages.barcodesearch);
                screen.pages.barcodesearch.show();
            },
            back: function () {
                screen.pagehistory.pop();
                screen.getCurrentPage().show();
            }
        },
        icodesearch: {
            name: 'icodesearch',
            getElement: function () {
                return screen.$view.find('.page-icodesearch');
            },
            init: function () {
                screen.$searchicodes.fwmobilesearch({
                    service: 'BarcodeLabel',
                    method: 'ICodeSearch',
                    searchModes: [
                        { value: 'DESCRIPTION', caption: 'Description' },
                        { value: 'ICODE', caption: 'I-Code' },
                        { value: 'DEPARTMENT', caption: 'Department' },
                        { value: 'CATEGORY', caption: 'Category' },
                        { value: 'SUBCATEGORY', caption: 'Sub-Category' },
                    ],
                    cacheItemTemplate: false,
                    itemTemplate: function (model) {
                        var html = [];
                        html.push('<div class="record" style="border-left:10px solid ' + model.color + '">');
                        html.push('  <div class="row">');
                        html.push('    <div class="value desc">{{master}}</div>');
                        html.push('  </div>');
                        html.push('  <div class="row">');
                        html.push('    <div class="caption fixed">I-Code:</div>');
                        html.push('    <div class="value masterno">{{masterno}}</div>');
                        html.push('    <div class="caption fixed">Department:</div>');
                        html.push('    <div class="value inventorydepartment">{{inventorydepartment}}</div>');
                        html.push('  </div>');
                        html.push('  <div class="row">');
                        html.push('    <div class="caption fixed">Tracked By:</div>');
                        html.push('    <div class="value trackedby">{{trackedby}}</div>');
                        html.push('    <div class="caption fixed">Category:</div>');
                        html.push('    <div class="value category">{{category}}</div>');
                        html.push('  </div>');
                        html.push('  <div class="row">');
                        html.push('    <div class="caption fixed">Total Qty:</div>');
                        html.push('    <div class="value qty">{{qty}}</div>');
                        html.push('    <div class="caption fixed">Sub-Category:</div>');
                        html.push('    <div class="value subcategory">{{subcategory}}</div>');
                        html.push('  </div>');
                        html.push('</div>');
                        html = html.join('\n');
                        return html;
                    },
                    recordClick: function (model, $record) {
                        try {
                            var selected = $record.attr('data-selected') === 'true';
                            $record.attr('data-selected', !selected);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                });
            },
            show: function () {
                screen.pages.reset();
                screen.pages.icodesearch.getElement().show();
                screen.$btnback.show();
                screen.$btnselectnone.show();
                screen.$btnselectall.show();
                screen.$btnprint.show();
                FwMobileMasterController.setTitle('Print I-Code Label');
                //screen.$view.find('.page-search').addClass('page-slidein').show();
                screen.$searchicodes.fwmobilesearch('search');
            },
            forward: function () {
                screen.pagehistory.push(screen.pages.icodesearch);
                screen.pages.icodesearch.show();
            },
            back: function () {
                screen.pagehistory.pop();
                screen.getCurrentPage().show();
            }
        },
        managebarcodelabels: {
            name: 'barcodelabelsearch',
            getElement: function () {
                return screen.$view.find('.page-managebarcodelabels');
            },
            init: function () {
                screen.$searchbarcodelabel.fwmobilesearch({
                    service: 'BarcodeLabel',
                    method: 'BarcodeLabelSearch',
                    searchModes: [
                        { value: 'DESCRIPTION', caption: 'Description' },
                        { value: 'CATEGORY', caption: 'Category' }
                    ],
                    cacheItemTemplate: false,
                    itemTemplate: function (model) {
                        var html = [];
                        html.push('<div class="record">');
                        html.push('  <div class="row">');
                        html.push('    <div class="value desc">{{description}}</div>');
                        html.push('  </div>');
                        html.push('  <div class="row">');
                        html.push('    <div class="caption fixed">Category:</div>');
                        html.push('    <div class="value category">{{category}}</div>');
                        html.push('    <div class="caption dynamic">Modified:</div>');
                        html.push('    <div class="value statustype">' + new Date(model.datestamp).toLocaleString() + '</div>');
                        html.push('  </div>');
                        html.push('</div>');
                        html = html.join('\n');
                        return html;
                    },
                    recordClick: function (model) {
                        try {
                            var $contextmenu = FwContextMenu.render("options");
                            FwContextMenu.addMenuItem($contextmenu, 'Edit', function () {
                                try {
                                    screen.pages.uploadbarcodelabel.forward('EDIT', model);
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                            FwContextMenu.addMenuItem($contextmenu, 'Delete', function () {
                                try {
                                    var $confirmation = FwConfirmation.yesNo('Confirm', 'Delete this barcode label?',
                                        function onyes() {
                                            try {
                                                var request = {
                                                    barcodelabelid: model.barcodelabelid
                                                }
                                                RwServices.callMethod('BarcodeLabel', 'DeleteBarcodeLabel', request, function () {
                                                    try {
                                                        screen.getBarcodeLabels(function () {
                                                            screen.$searchbarcodelabel.fwmobilesearch('search');
                                                        });
                                                    } catch (ex) {
                                                        FwFunc.showError(ex);
                                                    }
                                                });
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        },
                                        function onno() {
                                            try {
                                                
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        }
                                    );
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                });
            },
            show: function () {
                screen.pages.reset();
                screen.pages.managebarcodelabels.getElement().show();
                screen.$btnback.show();
                FwMobileMasterController.setTitle('Manage Barcode Labels...');
                screen.$searchbarcodelabel.fwmobilesearch('search');
                screen.$btnnewbarcodelabel = FwMobileMasterController.addFormControl(screen, 'New', 'right', '&#xE145;', true, function () {
                    try {
                        screen.pages.uploadbarcodelabel.forward('NEW');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            forward: function () {
                screen.pagehistory.push(screen.pages.managebarcodelabels);
                screen.pages.managebarcodelabels.show();
            },
            back: function () {
                screen.pagehistory.pop();
                screen.getCurrentPage().show();
            }
        },
        uploadbarcodelabel: {
            name: 'uploadbarcodelabel',
            getElement: function () {
                return screen.$view.find('.page-uploadbarcodelabel');
            },
            init: function () {
                
            },
            show: function (mode, model) {
                screen.pages.reset();
                screen.pages.uploadbarcodelabel.getElement().show();
                FwMobileMasterController.setTitle('Upload Barcode Label...');
                screen.$btncancelbarcodelabel = FwMobileMasterController.addFormControl(screen, 'Cancel', 'left', '&#xE5C9;', true, function () {
                    try {
                        screen.getCurrentPage().back();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                screen.$btnsavebarcodelabel = FwMobileMasterController.addFormControl(screen, 'Save', 'right', '&#xE161;', true, function () {
                    try {
                        var fileBarcodeLabel = screen.$view.find('.fileBarcodeLabel');
                        if (fileBarcodeLabel.get(0).files.length > 0) {
                            var file = fileBarcodeLabel.get(0).files[0];
                            var reader = new FileReader();
                            reader.readAsText(file);
                            reader.onload = function () {
                                //console.log(reader.result);
                                var request = {
                                    mode: mode,
                                    category: screen.$view.find('.page-uploadbarcodelabel .category').val(),
                                    description: screen.$view.find('.page-uploadbarcodelabel .description').val(),
                                    barcodelabel: reader.result
                                };
                                if (typeof model !== 'undefined') {
                                    request.barcodelabelid = model.barcodelabelid;
                                }
                                RwServices.callMethod('BarcodeLabel', 'SaveBarcodeLabel', request, function (response) {
                                    try {
                                        screen.getBarcodeLabels(function () {
                                            screen.getCurrentPage().back();
                                        });
                                    } catch (ex) {
                                        FwFwunc.showError(ex);
                                    }
                                });
                            };
                            reader.onerror = function (error) {
                                //console.log('Error: ', error);
                                FwFunc.showError(error);
                            };
                        } else {
                            if (mode === 'NEW') {
                                FwFunc.showMessage('Select a file to upload!');
                            } else if (mode === 'EDIT') {
                                var request = {
                                    mode: mode,
                                    category: screen.$view.find('.page-uploadbarcodelabel .category').val(),
                                    description: screen.$view.find('.page-uploadbarcodelabel .description').val(),
                                    barcodelabel: ''
                                };
                                if (typeof model !== 'undefined') {
                                    request.barcodelabelid = model.barcodelabelid;
                                }
                                RwServices.callMethod('BarcodeLabel', 'SaveBarcodeLabel', request, function (response) {
                                    try {
                                        screen.getBarcodeLabels(function () {
                                            screen.getCurrentPage().back();
                                        });
                                    } catch (ex) {
                                        FwFwunc.showError(ex);
                                    }
                                });
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                var html = [];
                html.push('<div class="field">');
                html.push('    <div class="caption">Category</div>');
                html.push('    <div class="value">');
                html.push('    <select class="category">');
                html.push('        <option value="Barcode Label">Barcode Label</option>');
                html.push('        <option value="I-Code Label">I-Code Label</option>');
                //html.push('        <option value="Warehouse Barcode">Warehouse Barcode</option>');
                //html.push('        <option value="Warehouse Asset Barcode">Warehouse Asset Barcode</option>');
                html.push('    </select>');
                html.push('    </div>');
                html.push('</div>');
                html.push('<div class="field">');
                html.push('    <div class="caption">Description</div>');
                html.push('    <div class="value"><input class="description" type="text" /></div>');
                html.push('</div>');
                html.push('<div class="field">');
                html.push('    <div class="caption">');
                html.push('    Barcode Label (Print to File)<br/>');
                html.push('    </div>');
                html.push('    <div class="value"><input class="fileBarcodeLabel" type="file" /></div>');
                html.push('    <div class="explanation">');
                html.push('    (Must be the actual printed output saved to a file, not the designer file. For example, from Zebra designer choose the print to file option, and save the .prn file.');
                html.push('    You should use a desktop browser to upload the file.  Using the file picker above from a mobile device may cause the mobile app to close.)');
                html.push('    <br/><br/>');
                html.push('    <span style="text-decoration:underline;">Barcode Label Fields:</span><br />');
                html.push('    [barcode]<br />');
                html.push('    [description]<br />');
                html.push('    [icode]');
                html.push('    <br/><br/>');
                html.push('    <span style="text-decoration:underline;">I-Code Label Fields:</span><br/>');
                html.push('    [icode]<br/>');
                html.push('    [description]<br/>');
                html.push('    [department]<br/>');
                html.push('    [category]<br/>');
                html.push('    [subcategory]');
                html.push('    </div>');
                html.push('</div>');
                screen.pages.uploadbarcodelabel.getElement().html(html.join('\n'));
                //screen.pages.uploadbarcodelabel.getElement().find('select').selectize({
                //    create: true,
                //    sortField: 'text'
                //});
                if (mode === 'EDIT') {
                    screen.pages.uploadbarcodelabel.getElement().find('.category').val(model.category);
                    screen.pages.uploadbarcodelabel.getElement().find('.description').val(model.description);
                }
            },
            forward: function (mode, model) {
                screen.pagehistory.push(screen.pages.uploadbarcodelabel);
                screen.pages.uploadbarcodelabel.show(mode, model);
            },
            back: function () {
                screen.pagehistory.pop();
                screen.getCurrentPage().show();
            }
        }
    };

    screen.showBarcodePrintingDialog = function (models, category) {
        var html = [];
        html.push('<div class="barcodelabel-printdialog">');

        html.push('  <div class="printerpluginfield">');
        html.push('    <div>Printer:</div>');
        html.push('    <div>');
        html.push('      <select class="printerplugin">');
        html.push('        <option value="">Not Selected</option>');
        //if (typeof window.ZebraLinkOS === 'object') {
        if (typeof window.DwCordovaFunc === 'object') {
            html.push('        <option value="cordovaBT">Bluetooth Printer</option>');
            html.push('        <option value="cordovaNet">Network Printer</option>');
        }
        if (typeof window.electronPlugins !== 'undefined') {
            //var bt = require('bluetooth-serial-port');
            //if (typeof bt !== 'undefined') {
            //    html.push('        <option value="ztDesktopLabelBT">Bluetooth Printer</option>');
            //}

            //var printer = require('printer');
            if (typeof window.electronPlugins.labelPrinter !== 'undefined') {
                html.push('        <option value="electronNet">Network Printer</option>');
                html.push('        <option value="electronLocal">Local Printer</option>');
            }
        }
        html.push('      </select>');
        html.push('    </div>');
        html.push('  </div>');

        html.push('  <div class="hostfield" style="display:none">');
        html.push('    <div>Host Name/IP Address:</div>');
        html.push('    <div><input class="host" type="text" value="" /></div>');
        html.push('  </div>');

        html.push('  <div class="portfield" style="display:none">');
        html.push('    <div>Port Number:</div>');
        html.push('    <div><input class="port" type="text" /></div>');
        html.push('  </div>');

        html.push('  <div class="printerfield" style="display:none">');
        html.push('    <div>Printer:</div>');
        html.push('    <div>');
        html.push('      <select class="printer"></select>');
        html.push('    </div>');
        html.push('  </div>');

        html.push('  <div class="barcodelabelidfield">');
        html.push('    <div>Barcode Label:</div>');
        html.push('    <div>');
        html.push('      <select class="barcodelabelid">');
        html.push('        <option value="">Not Selected</option>');
        for (var labelno = 0; labelno < screen.labels.length; labelno++) {
            var label = screen.labels[labelno];
            if (label.category === category) {
                html.push('        <option value="' + label.barcodelabelid + '">' + label.description + '</option>');
            }
        }
        html.push('      </select>');
        html.push('    </div>');
        html.push('  </div>');

        html.push('  <div class="qtyfield">');
        html.push('    <div>Qty:</div>');
        html.push('    <div><input class="qty" type="number" value="1" /></div>');
        html.push('  </div>');

        html.push('</div>');
        html = html.join('\n');
        var $win = FwConfirmation.renderConfirmation('Print', html)
        var $btnprint = FwConfirmation.addButton($win, 'Print', false);
        $btnprint.on('click', function () {
            try {
                var printerplugin  = $win.find('.printerplugin').val();
                var host           = $win.find('.host').val();
                var port           = parseInt($win.find('.port').val());
                var barcodelabelid = $win.find('.barcodelabelid').val();
                var qty            = parseInt($win.find('.qty').val());
                for (modelno = 0; modelno < models.length; modelno++) {
                    var model = models[modelno];
                    var labeltemplate = '';
                    for (var labelno = 0; labelno < screen.labels.length; labelno++) {
                        var label = screen.labels[labelno];
                        if (label.barcodelabelid == barcodelabelid) {
                            labeltemplate = label.barcodelabel;
                            break;
                        }
                    }
                    for (var key in model) {
                        while (labeltemplate.indexOf('[' + key + ']') !== -1) {
                            labeltemplate = labeltemplate.replace('[' + key + ']', model[key]);
                        }
                        //labeltemplate = labeltemplate.replace(new RegExp('\[' + key + '\]', 'g'), model[key]);
                    }
                    labeltemplate = labeltemplate + '\r\n';
                    //alert(labeltemplate);
                    $win.savePrintingPreferences();
                    if (isNaN(qty) || qty <= 0) {
                        throw 'Invalid Qty!';
                    }
                    var labeldata = '';
                    for (var i = 0; i < qty; i++) {
                        labeldata = labeldata + labeltemplate;
                    }
                    switch (printerplugin) {
                        case 'cordovaBT':
                            {
                                if (typeof window.ZebraLinkOS !== 'undefined') {
                                    if (typeof window.ZebraLinkOS.printBluetooth === 'undefined') {
                                        throw 'Please update your iOS app (RentalWorks on the App Store) or Android app (Google Play Store or https://www.dbwcloud.com/androidapps/) to the latest version.';
                                    }
                                    window.ZebraLinkOS.printBluetooth(labeldata,
                                        function success() { },
                                        function error(responseArgs) {
                                            try {
                                                throw responseArgs[0];
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        }
                                    );
                                }
                                else if (typeof window.DwCordovaFunc !== 'undefined') {
                                    if (typeof window.DwCordovaFunc.printBluetoothSocket === 'undefined') {
                                        throw 'Please update your iOS app (RentalWorks on the App Store) or Android app (Google Play Store or https://www.dbwcloud.com/androidapps/) to the latest version.';
                                    }
                                    window.DwCordovaFunc.printBluetoothSocket(labeldata,
                                        function success() { },
                                        function error(responseArgs) {
                                            try {
                                                throw responseArgs[0];
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        }
                                    );
                                }
                            }
                            break;
                        case 'cordovaNet':
                            {
                                // enable ZPL mode for Zebra printers
                                //if (port === 9100 || port === 6101) {
                                //    labeldata = "! U1 setvar \"device.languages\" \"hybrid_xml_zpl\"\r\n" + labeldata;
                                //}
                                if (typeof window.DwCordovaFunc.printNetworkSocket === 'undefined') {
                                    throw 'Please update your iOS app (RentalWorks on the App Store) or Android app (Google Play Store or https://www.dbwcloud.com/androidapps/) to the latest version.';
                                }
                                window.DwCordovaFunc.printNetworkSocket(labeldata, host, port,
                                    function success() { },
                                    function error(responseArgs) {
                                        try {
                                            throw responseArgs[0];
                                        } catch (ex) {
                                            FwFunc.showError(ex);
                                        }
                                    }
                                );
                            }
                            break;
                        case 'electronNet':
                            {
                                if (typeof window.require !== 'undefined') {
                                    var net = require('net');
                                    var client = new net.Socket();
                                    client.connect(port, host, function () {
                                        console.log('Connected');
                                        client.write(labeldata);
                                        //client.destroy();
                                    });

                                    ////client.on('data', function (data) {
                                    ////    console.log('Received: ' + data);
                                    ////    client.destroy(); // kill client after server's response
                                    ////});

                                    //client.on('close', function () {
                                    //    console.log('Connection closed');
                                    //});
                                }
                            }
                            break;
                        case 'electronLocal':
                            {
                                var printername = $win.find('.printer').val();
                                //if (printername.indexOf('ZPL') >= -1) {
                                //    labeldata = "! U1 setvar \"device.languages\" \"hybrid_xml_zpl\"\r\n" + labeldata;
                                //}
                                window.electronPlugins.labelPrinter.printLocalAsync(printername, 'Barcode Label', 'RAW', labeldata);
                            }
                            break;
                    }
                }
                FwConfirmation.destroyConfirmation($win);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        var $btncancel = FwConfirmation.addButton($win, 'Cancel', false);
        $btncancel.on('click', function () {
            $win.savePrintingPreferences();
            FwConfirmation.destroyConfirmation($win);
        });
        $win.on('change', '.printerplugin', function () {
            var printerplugin = jQuery(this).val();
            $win.find('.hostfield').hide();
            $win.find('.portfield').hide();
            $win.find('.printerfield').hide();
            switch (printerplugin) {
                case 'cordovaBT':
                    {

                    }
                    break;
                case 'cordovaNet':
                    {
                        $win.find('.hostfield').show();
                        $win.find('.portfield').show();
                        $win.find('.port').val('9100');   //Zebra Desktop Label Printer
                        //$win.find('.port').val('6101'); //Zebra Mobile Label Printer
                    }
                    break;
                case 'electronNet':
                    {
                        $win.find('.hostfield').show();
                        $win.find('.portfield').show();
                        $win.find('.port').val('9100');
                    }
                    break;
                case 'electronLocal':
                    {
                        var printers = window.electronPlugins.labelPrinter.getLocalPrinters();
                        var options = [];
                        for (var printerno = 0; printerno < printers.length; printerno++) {
                            options.push('<option value="' + printers[printerno].name + '">' + printers[printerno].name + '</option>');
                        }
                        $win.find('.printer').html(options.join('\n'));
                        $win.find('.printerfield').show();
                    }
                    break;
            }
        });

        $win.savePrintingPreferences = function () {
            var printerplugin = $win.find('.printerplugin').val();
            var printer = $win.find('.printer').val();
            var host = $win.find('.host').val();
            var port = parseInt($win.find('.port').val());
            var barcodelabelid = $win.find('.barcodelabelid').val();
            localStorage.setItem(program.localstorageprefix + category.replace(' ', '') + '_printerplugin', printerplugin);
            localStorage.setItem(program.localstorageprefix + category.replace(' ', '') + '_barcodelabelid', barcodelabelid);
            switch (printerplugin) {
                case 'cordovaBT':
                    break;
                case 'electronLocal':
                   localStorage.setItem(program.localstorageprefix + category.replace(' ', '') + '_printer', printer);
                    break;
                case 'cordovaNet':
                case 'electronNet':
                    localStorage.setItem(program.localstorageprefix + category.replace(' ', '') + '_host', host);
                    localStorage.setItem(program.localstorageprefix + category.replace(' ', '') + '_port', port);
                    break;
            }
        };

        $win.loadPrintingPreferences = function () {
            var printerplugin = localStorage.getItem(program.localstorageprefix + category.replace(' ', '') + '_printerplugin');
            var printer = localStorage.getItem(program.localstorageprefix + category.replace(' ', '') + '_printer');
            var host = localStorage.getItem(program.localstorageprefix + category.replace(' ', '') + '_host');
            var port = localStorage.getItem(program.localstorageprefix + category.replace(' ', '') + '_port');
            var barcodelabelid = localStorage.getItem(program.localstorageprefix + category.replace(' ', '') + '_barcodelabelid');
            if (typeof printerplugin !== 'undefined') {
                $win.find('.printerplugin').val(printerplugin).change();
            }
            if (typeof printer !== 'undefined') {
                $win.find('.printer').val(printer).change();
            }
            if (typeof printerplugin !== 'undefined') {
                $win.find('.host').val(host).change();
            }
            if (typeof printerplugin !== 'undefined') {
                $win.find('.port').val(port).change();
            }
            if (typeof printerplugin !== 'undefined') {
                $win.find('.barcodelabelid').val(barcodelabelid).change();
            }
        };

        $win.find('.body').css({
            'overflow': 'visible'
        });
        //$win.find('select').selectize({
        //    create: true,
        //    sortField: 'text'
        //});
        //$win.find('.qty').inputmask('999');
        //$win.find('.port').inputmask('99999');

        if (typeof localStorage.getItem(program.localstorageprefix + 'printerplugin') !== 'undefined') {
            var savedprinterplugin = localStorage.getItem(program.localstorageprefix + 'printerplugin');
            $win.find('.printerplugin').val(savedprinterplugin).change();
        }
        $win.loadPrintingPreferences();
    };


    screen.getBarcodeLabels = function (oncompleted) {
        RwServices.callMethod("BarcodeLabel", "GetBarcodeLabels", {}, function (response) {
            try {
                screen.labels = response.labels;
                if (typeof oncompleted === 'function') {
                    oncompleted();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    screen.load = function () {
        for (key in screen.pages) {
            var page = screen.pages[key];
            if (typeof page.init === 'function') {
                page.init();
            }
        }
        screen.getBarcodeLabels(function () {
            screen.pages.menu.forward();
        });
    };

    screen.unload = function () {
        //program.setScanTarget('.ui-search .fwmobilesearch .searchbox');
    };
    
    return screen;
};
//----------------------------------------------------------------------------------------------