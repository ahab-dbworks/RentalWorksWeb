class FwMultiSelectValidationClass {
    //---------------------------------------------------------------------------------
    init($control: JQuery, validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery): void {
        var $browse, $popup, $form, controller, formbeforevalidate, control_boundfields, boundfields, hasselectall;
        hasselectall = ((typeof $control.attr('data-hasselectall') !== 'string') || ($control.attr('data-hasselectall') === 'true'));
        if (hasselectall) $searchfield.attr('placeholder', 'ALL');
        $searchfield 
        $browse             = jQuery(jQuery('#tmpl-validations-' + validationName + 'Browse').html());
        $control.data('browse', $browse);
        $browse.attr('data-multiselectvalidation', 'true');
        $form               = $control.closest('.fwform');
        controller          = $form.attr('data-controller');
        formbeforevalidate  = $control.attr('data-formbeforevalidate');
        control_boundfields = $control.attr('data-boundfields');
        if (typeof control_boundfields != 'undefined') {
            boundfields = control_boundfields.split(',');
        }

        // auto generate controllers for validations if they don't have one, so we only have to look in 1 place for the apiurl
        if (typeof $browse.attr('data-name') !== 'undefined' && typeof $browse.attr('data-apiurl') !== 'undefined') {
            if (typeof window[$browse.attr('data-apiurl') + 'Controller'] === 'undefined') {
                window[$browse.attr('data-name') + 'Controller'] = {
                    Module: $browse.attr('data-name'),
                    apiurl: $browse.attr('data-apiurl')
                };
            } else {
                var controller = window[$browse.attr('data-apiurl') + 'Controller'];
                if (typeof controller.Module === 'undefined') {
                    controller.Module = $browse.attr('data-name');
                }
                if (typeof controller.apiurl === 'undefined') {
                    controller.apiurl = $browse.attr('data-apiurl');
                }
            }
        } else if (typeof $browse.attr('data-name') !== 'undefined') {
            if (typeof window[$browse.attr('data-apiurl') + 'Controller'] === 'undefined') {
                window[$browse.attr('data-name') + 'Controller'] = {
                    Module: $browse.attr('data-name')
                };
            }
        }

        FwBrowse.init($browse);
        $browse.data('$control', $control);
        $browse.data('ondatabind', function(request) {
            request.module = validationName;
            if ((typeof boundfields != 'undefined') && (boundfields.length > 0)) {
                request.boundids = {};
                for (var i = 0; i < boundfields.length; i++) {
                    var boundid, $boundfield;
                    $boundfield = $form.find('[data-datafield="' + boundfields[i] + '"]');
                    if ($boundfield.length == 0) throw 'Unable to find boundfield "' + boundid + '" for ' + $control.attr('data-caption');
                    boundid = FwFormField.getValue2($boundfield);
                    if ((typeof boundid === 'undefined') || (boundid === '')) throw 'Please select "' + $boundfield.attr('data-caption') + '" first.';
                    request.boundids[boundfields[i].split('.').pop()] = boundid;
                }
            }
            if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller][formbeforevalidate] === 'function')) {
                window[controller][formbeforevalidate]($browse, $form, request);
            }
            FwServices.validation.method(request, validationName, 'Browse', $browse,
                // onSuccess
                function(response) {
                    try {
                        FwBrowse.databindcallback($browse, response);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            );
        });
        FwBrowse.renderRuntimeHtml($browse);
        if (hasselectall) {
            $browse.find('.btnSelectAll').css('display', 'inline-block');
        }
        $browse.find('.btnViewSelection').css('display', 'inline-block');

        //adds display field select options
        window['FwFormField_multiselectvalidation'].loadDisplayFields($browse);

        $popup = FwPopup.attach($browse);
        $searchfield.on('change', function() {
            try {
                if ((<string>$searchfield.val()).length === 0) {
                    $valuefield.val('');
                } else {
                    FwMultiSelectValidation.validate(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, true);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $searchfield.on('keydown', function(e) {
            var code = e.keyCode || e.which;
            try {
                if(code === 13) { //Enter Key
                    FwMultiSelectValidation.validate(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, true);
                    e.preventDefault();
                    return false;
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $btnvalidate.on('click', function() {
            try {
                if ((typeof $control.attr('data-enabled') !== 'undefined') && ($control.attr('data-enabled') !== 'false')) {
                    FwMultiSelectValidation.validate(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, false);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $browse
            .on('click', '.validationbuttons .btnSelect', function() {
                var selectedrowsobj, selectedrows;
                try {
                    selectedrowsobj = $browse.data('selectedrows');
                    //selectedrows = [];
                    //for (var key in selectedrowsobj) {
                    //    if (selectedrowsobj.hasOwnProperty(key)) {
                    //        selectedrows.push(selectedrowsobj[key]);
                    //    }
                    //}

                    //if (selectedrows.length === 0) {
                    //    FwNotification.renderNotification('WARNING', 'No rows selected.');
                    //} else {
                    FwMultiSelectValidation.select($control, selectedrowsobj, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
                    //}
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnSelectAll', function() {
                try {
                    FwMultiSelectValidation.selectAll(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnClear', function() {
                try {
                    FwMultiSelectValidation.clear(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnViewSelection', function() {
                try {
                    FwMultiSelectValidation.viewSelection(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnCancel', function() {
                try {
                    FwPopup.hide($popup);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            //.on('keydown', 'input[type="text"]', e => {
            //    let code = e.keyCode || e.which;
            //    try {
            //        if (code === 13) { //Enter Key
            //            e.preventDefault();
            //            this.validate(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, true);
            //        }
            //    } catch (ex) {
            //        FwFunc.showError(ex);
            //    }
            //})
        ;

        $browse.data('onrowdblclick', e => {
            let $tr, selectedRows, $keyfields, compositekey;
            $tr = jQuery(e.currentTarget);
            try {
                if (typeof $browse.data('selectedrows') === 'undefined') {
                    $browse.data('selectedrows', {});
                }
                $keyfields = $tr.find('div.field[data-isuniqueid="true"]');
                compositekey = [];
                $keyfields.each(function (index, element) {
                    var $keyfield;
                    $keyfield = jQuery(element);
                    compositekey.push($keyfield.html());
                });
                compositekey = compositekey.join(',');
                selectedRows = $browse.data('selectedrows');
                $tr.addClass('selected');
                selectedRows[compositekey] = $tr;
                FwMultiSelectValidation.select($control, selectedRows, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $browse.data('afterdatabindcallback', function() {
            var $trs, $tr, selectedrows, uniqueids;
            if (typeof $browse.data('selectedrows') !== 'undefined') {
                selectedrows = $browse.data('selectedrows');
                $trs = $browse.find('tbody > tr');
                $trs.each(function(index, element) {
                    $tr = jQuery(element);
                    uniqueids = FwMultiSelectValidation.getUniqueIds($tr);
                    if (typeof selectedrows[uniqueids] !== 'undefined') {
                        $tr.addClass('selected');
                    }
                });
            }
        
        });

        $control
            .on('click', '.multiselectitems i', e => {
                let $this = jQuery(e.currentTarget);
                try {
                    let $item = $this.parent('div.multiitem');
                    let itemvalue = $item.attr('data-multivalue');
                    let value: any = $valuefield.val();
                    value = value
                        .split(',')
                        .filter((value) => {
                            return value !== itemvalue;
                        })
                        .join(',');
                    $valuefield.val(value);
                    $item.remove();
                    if ($item.attr('data-customvalue') != "true") {
                        let $selectedRows = $browse.data('selectedrows');
                        let selectedRowUniqueIds = $browse.data('selectedrowsuniqueids');
                        if (typeof $selectedRows[itemvalue] !== 'undefined') {
                            delete $selectedRows[itemvalue];
                        }
                        let index = selectedRowUniqueIds.indexOf(itemvalue);
                        if (index != -1) {
                            selectedRowUniqueIds.splice(index, 1);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('keydown', 'div[contenteditable="true"]', e => {
                let code = e.keyCode || e.which;
                try {
                    if (code === 13) { //Enter Key
                        e.preventDefault();
                        let $container = $control.find('div.multiselectitems');
                        let $input = $container.find('span.addItem');
                        let value = $input.text();
                        //jQuery(`
                        //    <div contenteditable="false" class="multiitem" data-multivalue="${value}" data-customvalue="true">
                        //        <span>${value}</span>
                        //        <i class="material-icons">clear</i>
                        //    </div>`)
                        //    .insertBefore($input);
                        //$input.text('');

                        //let formFieldValue = $valuefield.val();
                        //formFieldValue == "" ? formFieldValue += value : formFieldValue += `,${value}`;
                        //$valuefield.val(formFieldValue).change();
                        $searchfield.val(value);
                        this.validate(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, true);
                    }
                    else if (code === 8) { //Backspace
                        let length = $control.find('span.addItem').text().length;
                        if (length === 0) {
                            e.preventDefault();
                            let $this = jQuery(e.currentTarget);
                            let $item = $this.find('div.multiitem:last');
                            let itemvalue = $item.attr('data-multivalue');
                            let $selectedRows = $browse.data('selectedrows');
                            if (typeof $selectedRows[itemvalue] !== 'undefined') {
                                delete $selectedRows[itemvalue];
                            }
                            let selectedRowUniqueIds = $browse.data('selectedrowsuniqueids');
                            let index = selectedRowUniqueIds.indexOf(itemvalue);
                            if (index != -1) {
                                selectedRowUniqueIds.splice(index, 1);
                            }
                            let value: any = $valuefield.val();
                            value = value
                                .split(',')
                                .filter((value) => {
                                    return value !== itemvalue;
                                })
                                .join(',');
                            $valuefield.val(value);
                            $item.remove();
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })

        //$control.find('input[type="text"]').on('change', e => {
        //    e.preventDefault();
        //    e.stopImmediatePropagation();
        //    try {
        //        if ($searchfield.val().length === 0) {
        //            $valuefield.val('').change();
        //        } else {
        //            FwValidation.validate(validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, true);
        //            var $rows = FwBrowse.getRows($validationbrowse);
        //            FwBrowse.selectRow($validationbrowse, $rows.first(), true)
        //        }
        //        $validationbrowse.data('previousActiveElement', $searchfield);
        //        focusValidationSearchBox($validationbrowse);

        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
    }
    //---------------------------------------------------------------------------------
    validate(validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $popup: JQuery, $browse: JQuery, useSearchFieldValue: boolean): void {
        var $validationSearchbox;
        $validationSearchbox = $browse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
        if (useSearchFieldValue && ((<string>$searchfield.val()).length > 0)) {
            if ($validationSearchbox.length == 1) {
                $validationSearchbox.val($searchfield.val());
            } else {
                throw 'FwMultiSelectValidation: Validation is not setup correctly. Missing validation display field.';
            }
        } else {
            $validationSearchbox.val('');
        }

        $browse.data('$btnvalidate', $btnvalidate);
        $btnvalidate.hide();
        $browse.data('$control').find('.validation-loader').show();
        FwBrowse.search($browse)
        FwPopup.show($popup);
    };
    //---------------------------------------------------------------------------------
    getUniqueIds($tr: JQuery): Array<string> {
        var $keyfields, uniqueids;
        uniqueids = [];
        $keyfields     = $tr.find('div.field[data-isuniqueid="true"]');
        $keyfields.each(function(index, element) {
            var $keyfield;
            $keyfield = jQuery(element);
            uniqueids.push($keyfield.html());
        });
        uniqueids = uniqueids.join(',');
        return uniqueids;
    };
    //---------------------------------------------------------------------------------
    select($control, $selectedRows: Array<JQuery>, validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $popup: JQuery, $browse: JQuery, controller: string): void {
        var uniqueid, $trs;
        let multiselectfield = $control.find('.multiselectitems');
        let fieldToDisplay = $browse.find('.multiSelectDisplay select option:selected').attr('data-datafield');
        let $inputField = multiselectfield.find('span.addItem');

        if (typeof $browse.data('selectedrowsuniqueids') === 'undefined') {
            $browse.data('selectedrowsuniqueids', []);
        }
        let selectedRowUniqueIds = $browse.data('selectedrowsuniqueids');
        $trs = $browse.find('tbody > tr');
        for (let i = 0; i < $trs.length; i++) {
            var $tr, uniqueIdValue;
            $tr = jQuery($trs[i]);
            uniqueIdValue = FwMultiSelectValidation.getUniqueIds($tr);
            if ((typeof $selectedRows[uniqueIdValue] !== 'undefined') && (selectedRowUniqueIds.indexOf(uniqueIdValue) == -1)) {
                $tr.addClass('selected');
                let textValue = $tr.find(`[data-browsedatafield="${fieldToDisplay}"]`).attr('data-originalvalue');
                multiselectfield.append(`
                <div contenteditable="false" class="multiitem" data-multivalue="${uniqueIdValue}">
                    <span>${textValue}</span>
                    <i class="material-icons">clear</i>
                </div>`);
                selectedRowUniqueIds.push(uniqueIdValue);
            }
        }
        multiselectfield.append($inputField);
        uniqueid = selectedRowUniqueIds.join(',');
        $valuefield.val(uniqueid).change();
        $inputField.text('');
        $searchfield.val('');

        if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['loadRelatedValidationFields'] === 'function')) {
            window[controller]['loadRelatedValidationFields'](validationName, $valuefield, $tr);
        }

        if (typeof $control.data('onchange') === 'function') {
            $control.data('onchange')($selectedRows);
        }

        FwPopup.hide($popup);
    };
    //---------------------------------------------------------------------------------
    selectAll(validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $popup: JQuery, $browse: JQuery, controller: string): void {
        $browse.data('selectedrows', {});
        $valuefield.val('').change();
        $searchfield.val('');
        FwPopup.hide($popup);
    };
    //---------------------------------------------------------------------------------
    clear(validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $popup: JQuery, $browse: JQuery, controller: string): void {
        var uniqueid, $trGrid, $gridUniqueIdField;
        $browse.data('selectedrows', {});
        $valuefield.val('').change();
        $searchfield.val('');
        $browse.find('tbody tr').removeClass('selected');
        //FwPopup.hide($popup);
    };
    //---------------------------------------------------------------------------------
    viewSelection (validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $popup, $browse: JQuery, controller: string): void {
        var selectedrows, trs, $tr;
        selectedrows = $browse.data('selectedrows');
        trs = [];
        for (var key in selectedrows) {
            if (selectedrows.hasOwnProperty(key)) {
                $tr = selectedrows[key];
                trs.push($tr);
            }
        }
        $browse.find('tbody').empty().append(trs);
        $browse.find('tbody tr')
            .on('click', function(event) {
                var $tr, $cb, selectedrows, compositekey, $keyfields;
                try {
                    $tr = jQuery(this);
                    if (($tr.find('> td.select').length === 0) && (!$tr.hasClass('selected'))) {
                        $tr.addClass('selected');
                        if (typeof $browse.data('selectedrows') === 'undefined') {
                            $browse.data('selectedrows', {});
                        }
                        compositekey = FwMultiSelectValidation.getUniqueIds($tr);
                        selectedrows = $browse.data('selectedrows');
                        selectedrows[compositekey] = $tr;
                    } else {
                        $cb = $tr.find('> td.select > div > input[type="checkbox"]');
                        if ($tr.hasClass('selected')) {
                            $tr.removeClass('selected');
                            $cb.prop('checked', false);
                            compositekey = FwMultiSelectValidation.getUniqueIds($tr);
                            selectedrows = $browse.data('selectedrows');
                            if (typeof selectedrows[compositekey] !== 'undefined') {
                                delete selectedrows[compositekey]
                            }
                        } else {
                            $tr.addClass('selected');
                            $cb.prop('checked', true);
                        }
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
    };
}

var FwMultiSelectValidation = new FwMultiSelectValidationClass();
