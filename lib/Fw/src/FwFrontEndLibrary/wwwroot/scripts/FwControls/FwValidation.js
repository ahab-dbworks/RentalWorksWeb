var FwValidation = {};
//---------------------------------------------------------------------------------
FwValidation.init = function($control) {
    var $validationbrowse, $popup, $object, controller, formbeforevalidate, control_boundfields, boundfields, validationName, $valuefield, $searchfield, $btnvalidate;
    validationName = (typeof $control.attr('data-validationname') != 'undefined') ? $control.attr('data-validationname') : $control.attr('data-formvalidationname');
    $valuefield         = $control.find('input[type="hidden"]');
    $searchfield        = $control.find('input[type="text"]');
    $btnpeek            = $control.find('.btnpeek');
    $btnvalidate        = $control.find('.btnvalidate');
    $validationbrowse   = jQuery(jQuery('#tmpl-validations-' + validationName + 'Browse').html());
    $object             = ($control.closest('.fwbrowse[data-controller!=""]').length > 0) ? $control.closest('.fwbrowse[data-controller!=""]') : $control.closest('.fwform[data-controller!=""]');
    controller          = $object.attr('data-controller');
    formbeforevalidate  = $control.attr('data-formbeforevalidate');
    control_boundfields = $control.attr('data-boundfields');

    // auto generate controllers for validations if they don't have one, so we only have to look in 1 place for the apiurl
    if (typeof $validationbrowse.attr('data-name') !== 'undefined' && typeof $validationbrowse.attr('data-apiurl') !== 'undefined') {
        if (typeof window[$validationbrowse.attr('data-apiurl') + 'Controller'] === 'undefined') {
            window[$validationbrowse.attr('data-name') + 'Controller'] = {
                Module: $validationbrowse.attr('data-name'),
                apiurl: $validationbrowse.attr('data-apiurl')
            };
        } else {
            var controller = window[$validationbrowse.attr('data-apiurl') + 'Controller'];
            if (typeof controller.Module === 'undefined') {
                controller.Module = $validationbrowse.attr('data-name');
            }
            if (typeof controller.apiurl === 'undefined') {
                controller.apiurl = $validationbrowse.attr('data-apiurl');
            }
        }
    } else if (typeof $validationbrowse.attr('data-name') !== 'undefined') {
        if (typeof window[$validationbrowse.attr('data-apiurl') + 'Controller'] === 'undefined') {
            window[$validationbrowse.attr('data-name') + 'Controller'] = {
                Module: $validationbrowse.attr('data-name')
            };
        }
    }

    if (typeof control_boundfields != 'undefined') {
        boundfields = control_boundfields.split(',');
    }
    FwBrowse.init($validationbrowse);
    //$validationbrowse.attr('data-name', validationName);
    $validationbrowse.data('ondatabind', function (request) {
        var $btnvalidate = $validationbrowse.data('$btnvalidate');
        request.module = validationName;
        if ($object.attr('data-type') === 'Grid') {
            var $tr = $btnvalidate.closest('tr');
            var datafields = FwBrowse.getRowFormDataFields($object, $tr, false);
            request.filterfields = {};
            for (var key in datafields) {
                request.filterfields[key] = datafields[key].value;
            }
        }
        if (typeof $validationbrowse.attr('data-apiurl') === 'undefined') {
            if ((typeof boundfields !== 'undefined') && (boundfields.length > 0)) {
                request.boundids = {};
                for (var i = 0; i < boundfields.length; i++) {
                    request.boundids[boundfields[i].split('.').pop()] = $object.find('.fwformfield[data-datafield="' + boundfields[i] + '"] .fwformfield-value').val();
                }
            }
        }
        if ((typeof controller === 'string') && (typeof window[controller] === 'object') && (typeof window[controller][formbeforevalidate] === 'function')) {
            if ($object.attr('data-type') === 'Grid') {
                window[controller][formbeforevalidate]($validationbrowse, $object.closest('.fwform'), request);
            } else {
                window[controller][formbeforevalidate]($validationbrowse, $object, request);
            }
        }
    });
    $validationbrowse.data('$control', $control);
    //$validationbrowse.data('calldatabind', function(request) {
        
    //    FwServices.validation.method(request, validationName, 'Browse', $validationbrowse,
    //        // onSuccess
    //        function(response) {
    //            try {
    //                FwBrowse.databindcallback($validationbrowse, response.browse);
    //            } catch (ex) {
    //                FwFunc.showError(ex);
    //            }
    //        }
    //    );
    //});
    FwBrowse.renderRuntimeHtml($validationbrowse);
    if (typeof $control.data('oninit') === 'function') {
        $control.data('oninit')($control, $validationbrowse, $popup, $valuefield, $searchfield, $btnvalidate);
    }
    FwPopup.renderPopup($validationbrowse);
    $control
        .on('change', 'input[type="hidden"]', function() {
            try {
                // need to clear out any boundfields here
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('change', 'input[type="text"]', function() {
            try {
                if ($searchfield.val().length === 0) {
                    $valuefield.val('').change();
                } else {
                    FwValidation.validate(validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, true);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('keydown', 'input[type="text"]', function(e) {
            var code = e.keyCode || e.which;
            try {
                if(code === 13) { //Enter Key
                    FwValidation.validate(validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, true);
                    e.preventDefault;
                    return false;
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.btnvalidate', function() {
            try {
                if ((typeof $control.attr('data-enabled') !== 'string') || ($control.attr('data-enabled') !== 'false')) {
                    FwValidation.validate(validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, false);
                }
                jQuery(document).on('keydown', function (e) {
                    var code = e.keyCode || e.which;
                    if (code === 27) { //ESC Key  
                        try {
                            FwValidation.clearSearchCriteria($validationbrowse, $btnvalidate);
                            $popup = $validationbrowse.closest('.fwpopup');
                            FwPopup.detachPopup($popup);
                            jQuery(document).off('keydown');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                })  
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;
    $validationbrowse
        .data('onrowdblclick', function() {
            var $tr, originalcolor;
            try {
                $tr = jQuery(this);
                FwValidation.selectRow($tr, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                originalcolor = $searchfield.css('background-color');
                $searchfield.css('background-color', '#abcdef').animate({ backgroundColor: originalcolor }, 1500, function () { $searchfield.attr('style', '') });
                jQuery(document).off('keydown'); 
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.validationbuttons .btnSelect', function() {
            var $tr;
            try {
                $tr = $validationbrowse.find('tr.selected');
                if ($tr.length === 0) {
                    throw 'No row selected.';
                }
                else if ($tr.length === 1) {
                    FwValidation.selectRow($tr, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                    jQuery(document).off('keydown'); 
                }
                else if ($trselected.length > 1) {
                    throw 'Please select only one row.';
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.validationbuttons .btnClear', function() {
            var uniqueid, $trGrid, $gridUniqueIdField;
            try {
                $valuefield.val('').change();
                $searchfield.val('');
                FwValidation.clearSearchCriteria($validationbrowse, $btnvalidate);
                $popup = $validationbrowse.closest('.fwpopup');
                FwPopup.detachPopup($popup);
                jQuery(document).off('keydown'); 
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '.validationbuttons .btnCancel', function() {
            try {
                FwValidation.clearSearchCriteria($validationbrowse, $btnvalidate);
                $popup = $validationbrowse.closest('.fwpopup');
                FwPopup.detachPopup($popup);
                jQuery(document).off('keydown'); 
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;
    $btnpeek
        .on('click', function () {
            var $popupForm;
            var popupModule = validationName.slice(0, -10);
            var popupModuleId = $valuefield.val();

            if (popupModuleId !== '') {
                $popupForm = jQuery(jQuery('#tmpl-modules-' + popupModule + 'Form').html());
                $popupForm = FwModule.openForm($popupForm, 'EDIT');

                //$popupForm.find('.fwform-menu').remove();
                $popupForm.find('.btnpeek').remove();
                $popupForm.css({ 'background-color': 'white', 'box-shadow': '0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22)', 'width': '60vw', 'height': '60vh', 'overflow':'scroll' });
                $popupForm.find('div.fwformfield[data-datafield="' + popupModule + 'Id"] input').val(popupModuleId);

                FwModule.loadForm(popupModule, $popupForm);
                //FwModule.setFormReadOnly($popupForm);

                FwPopup.showPopup(FwPopup.renderPopup($popupForm));

                jQuery(document).find('.fwpopup').on('click', function (e) {
                    e = e || window.event;
                    if (e.target.outerHTML === '<i class="material-icons"></i>' || e.target.outerHTML === '<div class="btn-text">Save</div>') {

                    } else {
                        FwPopup.destroyPopup(this);
                        jQuery(document).off('keydown');
                        jQuery(document).find('.fwpopup').off('click');
                    }                    
                });

                jQuery(document).one('keydown', function (e) {
                    var code = e.keyCode || e.which;
                    if (code === 27) { //ESC Key  
                        try {
                            FwPopup.destroyPopup(jQuery(document).find('.fwpopup'));
                            jQuery(document).find('.fwpopup').off('click');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                });

                jQuery(document).find('.fwpopupbox').on('click', function (e) {
                    e = e || window.event;
                    if (e.target.outerHTML === '<i class="material-icons"></i>' || e.target.outerHTML === '<div class="btn-text">Save</div>') {

                    } else {
                        e.stopImmediatePropagation();
                    }
                });
            };
            
        })
    ;
}
//---------------------------------------------------------------------------------
FwValidation.validate = function(validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, useSearchFieldValue) {
    var $validationSearchbox;

    $validationbrowse.data('$btnvalidate', $btnvalidate);
    $btnvalidate.find('.icon-validation').css('background-image', 'url(' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/loading.001.gif)');
    if (useSearchFieldValue && ($searchfield.val().length > 0)) {
        $validationSearchbox = $validationbrowse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
        if ($validationSearchbox.length == 1) {
            $validationSearchbox.val($searchfield.val());
        } else {
            throw 'FwValidation: Validation is not setup correctly. Missing validation display field.';
        }
    }
    FwBrowse.search($validationbrowse);
};
//---------------------------------------------------------------------------------
FwValidation.validateSearchCallback = function($validationbrowse) {
    var $rows, $searchboxes, allSearchBoxesAreEmpty, showpopup, $headerfields, alreadyopen;
    var $btnvalidate = $validationbrowse.data('$control').find('.btnvalidate');
    FwValidation.hideValidateButtonLoadingIcon($btnvalidate);
    $headerfields = $validationbrowse.find('table > thead > tr > td > .field');
    $searchboxes  = $headerfields.find('.search > input');
    $rows         = $validationbrowse.find('table > tbody > tr');
    showpopup     = true;
    alreadyopen   = $validationbrowse.parents('.application').length > 0;
    if ($rows.length === 0) {
        allSearchBoxesAreEmpty = true;
        $searchboxes.each(function(index, element) {
            var $searchbox;
            $searchbox = jQuery(element);
            if ($searchbox.val() !== '') {
                allSearchBoxesAreEmpty = false;
                return false; // breaks from the loop
            }
        });
        // only search again if there is already a search, and this time try with the search boxes empty
        if (!allSearchBoxesAreEmpty) {
            $searchboxes.val('');
            FwBrowse.search($validationbrowse);
        }
    } else if (($rows.length === 1) && (!alreadyopen)) {
        $searchboxes.each(function(index, element) {
            var $txtSearch;
            $txtSearch = jQuery(element);
            if ($txtSearch.val() !== '') {
                $rows.eq(0).trigger('dblclick');
                showpopup = false;
            }
        });
    } else if (($rows.length > 0) && (!alreadyopen)) {
        var $validationdisplayheaderfield;
        $validationdisplayheaderfield = $validationbrowse.find('table > thead > tr > td > .field[data-validationdisplayfield="true"]');
        $rows.each(function(index, element) {
            var $validationdisplayfield;
            $validationdisplayfield = jQuery(element).find('[data-validationdisplayfield="true"]');
            if ((typeof $validationdisplayfield.attr('data-originalvalue') === 'string') && ($validationdisplayheaderfield.find('.search input').val().toUpperCase() == $validationdisplayfield.attr('data-originalvalue').toUpperCase())) {
                jQuery(element).trigger('dblclick');
                showpopup = false;
                return false;
            }
        });
    }
    if (showpopup) {
        FwPopup.showPopup($validationbrowse.parents('.fwpopup'));
    }
};
//---------------------------------------------------------------------------------
FwValidation.selectRow = function($tr, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller) {
    var $validationUniqueIdField, uniqueid, $validationSearchField, text, $popup;
    // set the uniqueid
    FwValidation.hideValidateButtonLoadingIcon($btnvalidate);
    $validationUniqueIdField = $tr.find('.field[data-validationvaluefield="true"]');
    if ($validationUniqueIdField.length === 0) {
        $validationUniqueIdField = $tr.find('.field[data-isuniqueid="true"]');
    }
    uniqueid                 = $validationUniqueIdField.attr('data-originalvalue');
    $valuefield.val(uniqueid).change();
    //set the text
    $validationSearchField = $tr.find('.field[data-validationdisplayfield="true"]');
    text                   = $validationSearchField.attr('data-originalvalue');
    $searchfield.val(text);
    if ((typeof controller === 'string') && (typeof window[controller] === 'object') && (typeof window[controller]['loadRelatedValidationFields'] === 'function')) {
        window[controller]['loadRelatedValidationFields'](validationName, $valuefield, $tr);
    }
    FwValidation.clearSearchCriteria($validationbrowse, $btnvalidate);
    $popup = $validationbrowse.parents('.fwpopup');
    FwPopup.detachPopup($popup);
};
//---------------------------------------------------------------------------------
FwValidation.clearSearchCriteria = function($validationbrowse, $btnvalidate) {
    var $validationSearchboxes, $validationSearchbox;
    FwValidation.hideValidateButtonLoadingIcon($btnvalidate);
    $validationSearchboxes = $validationbrowse.find('thead .field > .search > input');
    $validationSearchboxes.each(function(index, element) {
        $validationSearchbox = jQuery(element);
        $validationSearchbox.val('');
    });
};
//---------------------------------------------------------------------------------
FwValidation.hideValidateButtonLoadingIcon = function($btnvalidate) {
    $btnvalidate.find('.icon-validation').css('background-image', 'url(' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/browsesearch.001.png)');
};
//---------------------------------------------------------------------------------
