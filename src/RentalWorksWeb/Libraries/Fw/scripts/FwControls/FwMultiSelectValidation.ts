﻿class FwMultiSelectValidationClass {
    //---------------------------------------------------------------------------------
    init($control: JQuery, validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery): void {
        var $browse, $popup, $form, controller, formbeforevalidate, control_boundfields, boundfields, hasselectall;
        hasselectall = ((typeof $control.attr('data-hasselectall') !== 'string') || ($control.attr('data-hasselectall') === 'true'));
        if (hasselectall) $searchfield.attr('placeholder', 'ALL');
        $browse = jQuery(jQuery('#tmpl-validations-' + validationName + 'Browse').html());
        $control.data('browse', $browse);
        $browse.attr('data-multiselectvalidation', 'true');
        $form = $control.closest('.fwform');
        let $object = ($control.closest('.fwbrowse:not([data-controller=""])').length > 0) ? $control.closest('.fwbrowse:not([data-controller=""])') : $control.closest('.fwform:not([data-controller=""])');
        controller = $form.attr('data-controller');
        formbeforevalidate = $control.attr('data-formbeforevalidate');
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
        $browse.data('ondatabind', function (request) {
            var $btnvalidate = $browse.data('$btnvalidate');
            var $tr = $btnvalidate.closest('tr');
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
            else if ((typeof $object.attr('data-name') === 'string') && (typeof window[$object.attr('data-name') + 'Controller'] !== 'undefined') && (typeof window[$object.attr('data-name') + 'Controller']['beforeValidate'] === 'function')) {
                if ($object.attr('data-type') === 'Grid') {
                    window[$object.attr('data-name') + 'Controller']['beforeValidate']($control.attr('data-formdatafield'), request, $browse, $object.closest('.fwform'), $tr);
                } else {
                    window[$object.attr('data-name') + 'Controller']['beforeValidate']($control.attr('data-datafield'), request, $browse, $object, $tr);
                }
            }
            else if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['beforeValidate'] === 'function')) {
                window[controller]['beforeValidate']($control.attr('data-datafield'), request, $browse, $object, $tr);
            }
        });
        FwBrowse.renderRuntimeHtml($browse);
        if (hasselectall) {
            $browse.find('.btnSelectAll').css('display', 'inline-block');
        }
        $browse.find('.btnViewSelection').css('display', 'inline-block');

        $popup = FwPopup.renderPopup($browse, { 'ismodal': true });

        const $clearAllBtn = $control.find('.clearall');
        $clearAllBtn
            .on('click', e => {
                FwMultiSelectValidation.clear(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
            })
            .on('mousedown', e => {
                e.preventDefault();
            });

        $searchfield.on('change', function () {
            try {
                if ((<string>$searchfield.val()).length === 0) {
                    $valuefield.val('');
                } else {
                    FwMultiSelectValidation.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, true);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $searchfield.on('keydown', function (e) {
            var code = e.keyCode || e.which;
            try {
                if (code === 13) { //Enter Key
                    e.preventDefault();
                    FwMultiSelectValidation.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, true);
                    return false;
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $btnvalidate
            .on('click', function (e) {
                e.stopPropagation();
                try {
                    if ((typeof $control.attr('data-enabled') !== 'undefined') && ($control.attr('data-enabled') !== 'false')) {
                        if ((<string>$searchfield.val()).length > 0) {
                            FwMultiSelectValidation.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, true);
                            FwBrowse.selectRowByIndex($browse, 0);
                        } else {
                            FwMultiSelectValidation.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, false);
                            focusValidationSearchBox($browse);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('mousedown', e => { //prevents blur event from triggering
                e.preventDefault();
            });

        $browse
            .on('click', '.validationbuttons .btnSelect', function () {
                let $selectedrows, $tr, $selectedTrs, uniqueid;
                try {
                    if (typeof $browse.data('selectedrows') === 'undefined') {
                        $browse.data('selectedrows', {});
                    }
                    $selectedrows = $browse.data('selectedrows');
                    $selectedTrs = $browse.find('tbody > tr.selected');

                    for (let i = 0; i < $selectedTrs.length; i++) {
                        $tr = jQuery($selectedTrs[i]);
                        uniqueid = FwMultiSelectValidation.getUniqueIds($tr);
                        if (typeof $selectedrows[uniqueid] == 'undefined') {
                            $selectedrows[uniqueid] = $tr;
                        }
                    }
                    if ($selectedrows.length === 0) {
                        FwNotification.renderNotification('WARNING', 'No rows selected.');
                    } else {
                        FwMultiSelectValidation.select($control, $selectedrows, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnSelectAll', function () {
                try {
                    FwMultiSelectValidation.selectAll($control, $valuefield, $searchfield, $popup, $browse);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnClear', function () {
                try {
                    FwMultiSelectValidation.clear(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnViewSelection', function () {
                try {
                    FwMultiSelectValidation.viewSelection(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnNew', function () {
                var $this = jQuery(this);
                try {
                    FwMultiSelectValidation.newValidation($control, validationName.slice(0, -10), $this, $valuefield, $btnvalidate, $popup, $browse.attr('data-caption'));
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.validationbuttons .btnCancel', function () {
                try {
                    FwPopup.detachPopup($popup);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('keydown', e => {
                let code = e.keyCode || e.which;
                try {
                    switch (code) {
                        case 27: //ESC key
                            FwPopup.detachPopup($popup);
                            this.setCaret($control);
                            break;
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('keydown', 'input[type="text"]', function (e) {
                var code = e.keyCode || e.which;
                try {
                    if (code === 13) { //Enter Key
                        e.preventDefault();
                        $searchfield = jQuery(e.currentTarget);
                        FwMultiSelectValidation.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, false);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });;

        $browse.data('onrowdblclick', e => {
            let selectedRows, $keyfields, compositekey;
            const $tr = jQuery(e.currentTarget);
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

        $browse.data('afterdatabindcallback', function () {
            let $tr, uniqueids;
            if (typeof $browse.data('selectedrows') !== 'undefined') {
                const selectedrows = $browse.data('selectedrows');
                $browse.find('tr.selected').removeClass('selected');
                const $trs = $browse.find('tbody > tr');
                $trs.each(function (index, element) {
                    $tr = jQuery(element);
                    uniqueids = FwMultiSelectValidation.getUniqueIds($tr);
                    if (typeof selectedrows[uniqueids] !== 'undefined') {
                        $tr.addClass('selected');
                    }
                });
            }
        });

        $control
            //remove item
            .on('click', '.multiselectitems i', e => {
                try {
                    const $selectedRows = $browse.data('selectedrows');
                    const selectedRowUniqueIds = $browse.data('selectedrowsuniqueids');
                    const $this = jQuery(e.currentTarget);
                    const $item = $this.parent('div.multiitem');
                    //removes item from values
                    const itemValue = $item.attr('data-multivalue');
                    let value: any = $valuefield.val();
                    value = value
                        .split(',')
                        .map(s => s.trim())
                        .filter((value) => {
                            return value !== itemValue;
                        })
                        .join(',');
                    $valuefield.val(value).change();
                    //removes item from text
                    const itemText = $item.find('span').text();
                    const $textField = $valuefield.siblings('.fwformfield-text');
                    const multiSeparator = jQuery($browse.find(`thead [data-validationdisplayfield="true"]`).get(0)).attr('data-multiwordseparator') || ',';
                    let text: any = $textField.val();
                    text = text
                        .split(multiSeparator)
                        .filter((text) => {
                            return text !== itemText;
                        })
                        .join(multiSeparator);
                    $textField.val(text);

                    $item.remove();
                    if ($item.attr('data-customvalue') != "true" && $selectedRows !== undefined && selectedRowUniqueIds !== undefined) {
                        if (typeof $selectedRows[itemValue] !== 'undefined') {
                            delete $selectedRows[itemValue];
                        }
                        const index = selectedRowUniqueIds.indexOf(itemValue);
                        if (index != -1) {
                            selectedRowUniqueIds.splice(index, 1);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('keydown', 'div[contenteditable="true"]', e => {
                e.stopPropagation();
                const code = e.keyCode || e.which;
                try {
                    switch (code) {
                        case 9: //TAB key
                            if (jQuery(e.currentTarget).find('.addItem').text().length === 0) {
                                break;
                            }
                        case 13://Enter Key
                            e.preventDefault();
                            const $container = $control.find('div.multiselectitems');
                            const $input = $container.find('span.addItem');
                            const value = $input.text();
                            $searchfield.val(value);
                            this.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, true);
                            if (value.length === 0) {
                                focusValidationSearchBox($browse);
                            } else {
                                FwBrowse.selectRowByIndex($browse, 0);
                            };
                            break;
                        case 8:  //Backspace
                            const inputLength = $control.find('span.addItem').text().length;
                            const $this = jQuery(e.currentTarget);
                            const $item = $this.find('div.multiitem:last');
                            if (inputLength === 0) {
                                e.preventDefault();
                                if ($item.length > 0) {
                                    $item.find('i').click();
                                }
                            }
                            break;
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });

        const focusValidationSearchBox = $browse => {
            setTimeout(() => {
                const $searchBox = $browse.find('.search input:visible');
                if ($searchBox.length > 0) {
                    $searchBox.eq(0).focus();
                }
            }, 1000);
        };
    }
    //---------------------------------------------------------------------------------
    validate($control: JQuery, validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $popup: JQuery, $validationbrowse: JQuery, useSearchFieldValue: boolean): void {
        if ($validationbrowse.length === 0) {
            throw `Missing validation template for: ${validationName}`;
        }
        const $validationSearchbox = $validationbrowse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
        if (useSearchFieldValue && ((<string>$searchfield.val()).length > 0)) {
            if ($validationSearchbox.length == 1) {
                $validationSearchbox.val($searchfield.val());
            } else {
                throw 'FwMultiSelectValidation: Validation is not setup correctly. Missing validation display field.';
            }
        }

        if ($control.attr('data-showinactivemenu') === 'true') {
            FwValidation.addInactiveMenu($validationbrowse);
        }

        $validationbrowse.data('$btnvalidate', $btnvalidate);
        $btnvalidate.hide();
        $validationbrowse.data('$control').find('.validation-loader').show();
        FwBrowse.search($validationbrowse);
    };
    //---------------------------------------------------------------------------------
    getUniqueIds($tr: JQuery): Array<string> {
        var $keyfields, uniqueids;
        uniqueids = [];
        $keyfields = $tr.find('div.field[data-isuniqueid="true"]');
        $keyfields.each(function (index, element) {
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
        const multiselectfield = $control.find('.multiselectitems');
        const multiSeparator = jQuery($browse.find(`thead [data-validationdisplayfield="true"]`).get(0)).attr('data-multiwordseparator') || ',';
        const $inputField = multiselectfield.find('span.addItem');
        const $textField = $valuefield.siblings('.fwformfield-text');
        if (typeof $browse.data('selectedrowsuniqueids') === 'undefined' && $valuefield.val() !== '') {
            let values: any = $valuefield.val();
            values = values.split(multiSeparator);
            values = values.map(s => s.trim());
            $browse.data('selectedrowsuniqueids', values);
        } else if (typeof $browse.data('selectedrowsuniqueids') === 'undefined') {
            $browse.data('selectedrowsuniqueids', []);
        }
        let selectedRowUniqueIds = $browse.data('selectedrowsuniqueids');
        let selectedRowText: any = $textField.val();

        if (selectedRowText.length > 0) {
            selectedRowText = selectedRowText.split(multiSeparator);
        } else {
            selectedRowText = [];
        }

        $trs = $browse.find('tbody > tr');
        for (let i = 0; i < $trs.length; i++) {
            var $tr, uniqueIdValue;
            $tr = jQuery($trs[i]);
            uniqueIdValue = FwMultiSelectValidation.getUniqueIds($tr);
            if ((typeof $selectedRows[uniqueIdValue] !== 'undefined') && (selectedRowUniqueIds.indexOf(uniqueIdValue) == -1)) {
                $tr.addClass('selected');
                let textValue = $tr.find(`[data-validationdisplayfield="true"]`).attr('data-originalvalue');
                multiselectfield.append(`
                <div contenteditable="false" class="multiitem" data-multivalue="${uniqueIdValue}">
                    <span>${textValue}</span>
                    <i class="material-icons">clear</i>
                </div>`);
                selectedRowUniqueIds.push(uniqueIdValue);
                selectedRowText.push(textValue);
            }
        }
        multiselectfield.append($inputField);

        uniqueid = selectedRowUniqueIds.join(',');
        $textField.val(selectedRowText.join(multiSeparator));

        $valuefield.val(uniqueid).change();
        $inputField.text('');
        $searchfield.val('');

        if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['loadRelatedValidationFields'] === 'function')) {
            window[controller]['loadRelatedValidationFields'](validationName, $valuefield, $tr);
        }
        if (typeof $control.data('onchange') === 'function') {
            $control.data('onchange')($selectedRows);
        }
        FwPopup.detachPopup($popup);
        FwMultiSelectValidation.clearSearchCriteria($browse);
        FwMultiSelectValidation.setCaret($control);
    };
    //---------------------------------------------------------------------------------
    selectAll($control, $valuefield: JQuery, $searchfield: JQuery, $popup: JQuery, $browse: JQuery): void {
        let $selectedRows, $trs, $tr, uniqueIdValue, multiselectfield, selectedRowUniqueIds, $inputField;
        multiselectfield = $control.find('.multiselectitems');
        const multiSeparator = jQuery($browse.find(`thead [data-validationdisplayfield="true"]`).get(0)).attr('data-multiwordseparator') || ',';
        $inputField = multiselectfield.find('span.addItem');
        const $textField = $valuefield.siblings('.fwformfield-text');
        //if (typeof $browse.data('selectedrows') === 'undefined') {
        //    $browse.data('selectedrows', {});
        //}

        //if (typeof $browse.data('selectedrowsuniqueids') === 'undefined') {
        //    $browse.data('selectedrowsuniqueids', []);
        //}
        $selectedRows = {};
        selectedRowUniqueIds = [];
        const selectedRowText = [];
        multiselectfield.find('.multiitem').remove();
        //let selectedRowText: any = $textField.val();
        //if (selectedRowText.length > 0) {
        //    selectedRowText = selectedRowText.split($control.hasClass('email') ? ';' : multiSeparator);
        //} else {
        //    selectedRowText = [];
        //}
        //selectedRowUniqueIds = $browse.data('selectedrowsuniqueids');
        //$selectedRows = $browse.data('selectedrows');
        $trs = $browse.find('tbody > tr');

        for (let i = 0; i < $trs.length; i++) {
            $tr = jQuery($trs[i]);
            uniqueIdValue = FwMultiSelectValidation.getUniqueIds($tr);
            if ((typeof $selectedRows[uniqueIdValue] == 'undefined') && (selectedRowUniqueIds.indexOf(uniqueIdValue) == -1)) {
                $tr.addClass('selected');
                let textValue = $tr.find(`[data-validationdisplayfield="true"]`).attr('data-originalvalue');
                multiselectfield.append(`
                <div contenteditable="false" class="multiitem" data-multivalue="${uniqueIdValue}">
                    <span>${textValue}</span>
                    <i class="material-icons">clear</i>
                </div>`);
                if (selectedRowText.indexOf(textValue) == -1) {
                    selectedRowText.push(textValue);
                }
                selectedRowUniqueIds.push(uniqueIdValue);
                $selectedRows[uniqueIdValue] = $tr;
            }
        }
        $textField.val(selectedRowText.join(multiSeparator));

        $browse.data('selectedrows', $selectedRows);
        $browse.data('selectedrowsuniqueids', selectedRowUniqueIds);

        $valuefield.val(selectedRowUniqueIds.join(',')).change();
        $searchfield.val('');
        multiselectfield.append($inputField);
        $inputField.text('');


        FwPopup.detachPopup($popup);
        FwMultiSelectValidation.clearSearchCriteria($browse);
        FwMultiSelectValidation.setCaret($control);
    };
    //---------------------------------------------------------------------------------
    clear(validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $popup: JQuery, $browse: JQuery, controller: string): void {
        $browse.data('selectedrows', {});
        $browse.data('selectedrowsuniqueids', []);
        jQuery($valuefield.siblings('.multiselectitems')).find('.multiitem').remove();
        $valuefield.val('').change();
        $searchfield.val('');
        $browse.find('tbody tr').removeClass('selected');
    };
    //---------------------------------------------------------------------------------
    clearSearchCriteria($validationbrowse: JQuery) {
        var $validationSearchboxes, $validationSearchbox;
        $validationSearchboxes = $validationbrowse.find('thead .field > .search > input');
        $validationSearchboxes.each(function (index, element) {
            $validationSearchbox = jQuery(element);
            $validationSearchbox.val('');
        });
    };
    //---------------------------------------------------------------------------------
    viewSelection(validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $popup, $browse: JQuery, controller: string): void {
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
            .on('click', function (event) {
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
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
    };
    //---------------------------------------------------------------------------------
    newValidation($control, validationName, $this, $valuefield, $btnvalidate, $popup, title) {
        let $popupForm;

        try {
            if (jQuery('#tmpl-modules-' + validationName + 'Form').html() === undefined) {
                $popupForm = jQuery(window[validationName + 'Controller'].getFormTemplate());
            } else {
                $popupForm = jQuery(jQuery('#tmpl-modules-' + validationName + 'Form').html());
            }
            $popupForm = window[validationName + 'Controller'].openForm('NEW');
            $popupForm.find('.btnpeek').remove();
            $popupForm.css({ 'background-color': 'white', 'box-shadow': '0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22)', 'width': '60vw', 'height': '60vh', 'overflow': 'scroll', 'position': 'relative' });

            const $validationbrowse = $this.closest('div[data-control="FwBrowse"][data-type="Validation"]');
            $popupForm.data('afterSaveNewValidation', function () {
                FwMultiSelectValidation.validate($control, validationName, $valuefield, null, $btnvalidate, $popup, $validationbrowse, false);
            })

            const $popupControl = FwPopup.renderPopup($popupForm, undefined, 'New ' + title);
            FwPopup.showPopup($popup);

            jQuery('.fwpopup.new-validation').on('click', function (e: JQuery.ClickEvent) {
                if ((<HTMLElement>e.target).outerHTML === '<i class="material-icons"></i>' || (<HTMLElement>e.target).outerHTML === '<div class="btn-text">Save</div>') {

                } else {
                    FwPopup.destroyPopup($popupControl);
                    jQuery(document).off('keydown');
                    jQuery(document).find('.fwpopup').off('click');
                    FwValidation.validate($control, validationName, $valuefield, null, $btnvalidate, $validationbrowse, false);
                }
            });

            jQuery(document).on('keydown', function (e) {
                const code = e.keyCode || e.which;
                if (code === 27) { //ESC Key  
                    try {
                        FwPopup.destroyPopup($popupControl);
                        jQuery(document).find('.fwpopup').off('click');
                        jQuery(document).off('keydown');
                        FwValidation.validate($control, validationName, $valuefield, null, $btnvalidate, $validationbrowse, false);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            });

            jQuery('.fwpopupbox').on('click', function (e: JQuery.ClickEvent) {
                if ((<HTMLElement>e.target).outerHTML === '<i class="material-icons"></i>' || (<HTMLElement>e.target).outerHTML === '<div class="btn-text">Save</div>') {

                } else {
                    e.stopImmediatePropagation();
                }
            });
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    };
    //---------------------------------------------------------------------------------
    setCaret($control: JQuery) {
        const $element = $control.find('div.multiselectitems');
        if ($element.length > 0) {
            const $addItemSpan = $element.find('span.addItem');
            if ($addItemSpan.length > 0) {
                const addItemNode = $addItemSpan.get(0);
                const range = document.createRange();
                range.setStartAfter(addItemNode);
                range.collapse(true);
                const sel = window.getSelection();
                sel.removeAllRanges();
                sel.addRange(range);
                $element.focus();
            }
        }
    }
    //---------------------------------------------------------------------------------
}

var FwMultiSelectValidation = new FwMultiSelectValidationClass();
