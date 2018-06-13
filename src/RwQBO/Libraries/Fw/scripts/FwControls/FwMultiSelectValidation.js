var FwMultiSelectValidation = {};
//---------------------------------------------------------------------------------
FwMultiSelectValidation.init = function($control, validationName, $valuefield, $searchfield, $btnvalidate) {
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
                    FwBrowse.databindcallback($browse, response.browse);
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
    $popup = FwPopup.attach($browse);
    $searchfield.on('change', function() {
        try {
            if ($searchfield.val().length === 0) {
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
                selectedrows = [];
                for (var key in selectedrowsobj) {
                    if (selectedrowsobj.hasOwnProperty(key)) {
                        selectedrows.push(selectedrowsobj[key]);
                    }
                }
                if (selectedrows.length === 0) {
                    FwNotification.renderNotification('WARNING', 'No rows selected.');
                } else {
                    FwMultiSelectValidation.select(selectedrows, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller);
                }
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
    ;

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
}
//---------------------------------------------------------------------------------
FwMultiSelectValidation.validate = function(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, useSearchFieldValue) {
    var $validationSearchbox;
    if (useSearchFieldValue && ($searchfield.val().length > 0)) {
        $validationSearchbox = $browse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
        if ($validationSearchbox.length == 1) {
            $validationSearchbox.val($searchfield.val());
        } else {
            throw 'FwMultiSelectValidation: Validation is not setup correctly. Missing validation display field.';
        }
    }
    FwBrowse.search($browse)
    FwPopup.show($popup);
};
//---------------------------------------------------------------------------------
FwMultiSelectValidation.getUniqueIds = function($tr) {
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
FwMultiSelectValidation.select = function(selectedrows, validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller) {
    var $validationUniqueIdField, uniqueid, $validationSearchField, text, $keyfields, $displayfields;

    uniqueid = [];
    text     = [];
    for (var i = 0; i < selectedrows.length; i++) {
        var $tr;
        $tr = selectedrows[i];
        uniqueid.push(FwMultiSelectValidation.getUniqueIds($tr));
        $displayfields = $tr.find('div.field[data-isuniqueid="false"]');
        $displayfields.each(function(index, element) {
            var $displayfield;
            if (index == 0) {
                $displayfield = jQuery(element);
                text.push($displayfield.text());
            }
        });
    }
    $validationUniqueIdField = $tr.find('.field[data-browsedatatype="key"]');
    uniqueid = uniqueid.join(',');
    $valuefield.val(uniqueid).change();
    

    $validationSearchField = $tr.find('.field[data-validationdisplayfield="true"]');
    text                     = text.join(',');
    $searchfield.val(text);
    if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['loadRelatedValidationFields'] === 'function')) {
        window[controller]['loadRelatedValidationFields'](validationName, $valuefield, $tr);
    }
    FwPopup.hide($popup);
};
//---------------------------------------------------------------------------------
FwMultiSelectValidation.selectAll = function(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller) {
    $browse.data('selectedrows', {});
    $valuefield.val('').change();
    $searchfield.val('');
    FwPopup.hide($popup);
};
//---------------------------------------------------------------------------------
FwMultiSelectValidation.clear = function(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller) {
    var uniqueid, $trGrid, $gridUniqueIdField;
    $browse.data('selectedrows', {});
    $valuefield.val('').change();
    $searchfield.val('');
    $browse.find('tbody tr').removeClass('selected');
    //FwPopup.hide($popup);
};
//---------------------------------------------------------------------------------
FwMultiSelectValidation.viewSelection = function(validationName, $valuefield, $searchfield, $btnvalidate, $popup, $browse, controller) {
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