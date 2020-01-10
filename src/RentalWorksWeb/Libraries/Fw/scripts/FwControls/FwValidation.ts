class FwValidationClass {
    //---------------------------------------------------------------------------------
    init($control: JQuery): void {
        //var $validationbrowse, $popup, $object, controller, formbeforevalidate, control_boundfields, boundfields, validationName, $valuefield, $searchfield, $btnvalidate, $btnpeek;
        let validationName = (typeof $control.attr('data-validationname') != 'undefined') ? $control.attr('data-validationname') : $control.attr('data-formvalidationname');
        let $valuefield = $control.find('input[type="hidden"]');
        let $searchfield = $control.find('input[type="text"]');
        let $btnpeek = $control.find('.btnpeek');
        let $btnvalidate = $control.find('.btnvalidate');
        let $validationbrowse = jQuery(jQuery('#tmpl-validations-' + validationName + 'Browse').html());
        let $object = ($control.closest('.fwbrowse:not([data-controller=""])').length > 0) ? $control.closest('.fwbrowse:not([data-controller=""])') : $control.closest('.fwform:not([data-controller=""])');
        let controller = $object.attr('data-controller');
        let formbeforevalidate = $control.attr('data-formbeforevalidate');
        let control_boundfields = $control.attr('data-boundfields');
        let boundfields;
        let $popup;

        if (typeof $control.data('calldatabind') !== 'undefined') {
            $validationbrowse.data('calldatabind', $control.data('calldatabind'));
        }

        // auto generate controllers for validations if they don't have one, so we only have to look in 1 place for the apiurl
        if (typeof $validationbrowse.attr('data-name') !== 'undefined' && typeof $validationbrowse.attr('data-apiurl') !== 'undefined') {
            if (typeof window[$validationbrowse.attr('data-name') + 'Controller'] === 'undefined') {
                window[$validationbrowse.attr('data-name') + 'Controller'] = {
                    Module: $validationbrowse.attr('data-name'),
                    apiurl: $validationbrowse.attr('data-apiurl')
                };
            } else {
                var controllerObj = window[$validationbrowse.attr('data-name') + 'Controller'];
                if (typeof controllerObj.Module === 'undefined') {
                    controllerObj.Module = $validationbrowse.attr('data-name');
                }
                if (typeof controllerObj.apiurl === 'undefined') {
                    controllerObj.apiurl = $validationbrowse.attr('data-apiurl');
                }
            }
        } else if ((typeof $validationbrowse.attr('data-name') !== 'undefined') && (typeof window[$validationbrowse.attr('data-name') + 'Controller'] === 'undefined')) {
            window[$validationbrowse.attr('data-name') + 'Controller'] = {
                Module: $validationbrowse.attr('data-name')
            };
        }

        // Ability to add events for Validations within its controller
        const validationController = `${$validationbrowse.attr('data-name')}Controller`;
        if (window[validationController]) {
            if (typeof window[validationController]['addValidationEvents'] === 'function') {
                window[validationController]['addValidationEvents']($control);
            }
        }

        if (typeof control_boundfields != 'undefined') {
            boundfields = control_boundfields.split(',');
        }
        FwBrowse.init($validationbrowse);
        //$validationbrowse.attr('data-name', validationName);
        $validationbrowse.data('ondatabind', function (request) {
            var $btnvalidate = $validationbrowse.data('$btnvalidate');
            var $tr = $btnvalidate.closest('tr');
            request.module = validationName;
            if ($object.attr('data-type') === 'Grid') {
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

            if (typeof $control.data('beforevalidate') === 'function') {
                if ($object.attr('data-type') === 'Grid') {
                    $control.data('beforevalidate')($validationbrowse, $object.closest('.fwform'), request, $control.attr('data-formdatafield'), $tr);
                } else {
                    $control.data('beforevalidate')($validationbrowse, $object, request, $control.attr('data-datafield'), $tr);
                }
            }
            // This was the old way: if you define a data-formbeforevalidate data-attribute on the validation it will call that method instead.
            else if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller][formbeforevalidate] === 'function')) {
                if ($object.attr('data-type') === 'Grid') {
                    window[controller][formbeforevalidate]($validationbrowse, $object.closest('.fwform'), request, $control.attr('data-formdatafield'), $tr);
                } else {
                    window[controller][formbeforevalidate]($validationbrowse, $object, request, $control.attr('data-datafield'), $tr);
                }
            }
            // The new way: you declare the following function in your Module or Grid controller
            // beforeValidate(datafield: String, request: any, $validationbrowse: JQuery, $form: JQuery) { }
            else if ((typeof $object.attr('data-name') === 'string') && (typeof window[$object.attr('data-name') + 'Controller'] !== 'undefined') && (typeof window[$object.attr('data-name') + 'Controller']['beforeValidate'] === 'function')) {
                if ($object.attr('data-type') === 'Grid') {
                    window[$object.attr('data-name') + 'Controller']['beforeValidate']($control.attr('data-formdatafield'), request, $validationbrowse, $object.closest('.fwform'), $tr);
                } else {
                    window[$object.attr('data-name') + 'Controller']['beforeValidate']($control.attr('data-datafield'), request, $validationbrowse, $object, $tr);
                }
            }
            else if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['beforeValidate'] === 'function')) {
                window[controller]['beforeValidate']($control.attr('data-datafield'), request, $validationbrowse, $object, $tr);
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

        FwPopup.renderPopup($validationbrowse, { 'ismodal': true });

        // mv 2018-08-27 doesn't look like this does anything
        //$control.find('input[type="hidden"]').on('change', function () {
        //    try {
        //        // need to clear out any boundfields here
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});

        // modifies validation functionality with typing into field
        $control.find('input[type="text"]').on('change', (e) => {
            e.preventDefault();
            e.stopImmediatePropagation();
            try {
                if ($searchfield.val().toString().length === 0) {
                    $valuefield.val('').change();
                } else {
                    this.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, true);
                    var $rows = FwBrowse.getRows($validationbrowse);
                    FwBrowse.selectRow($validationbrowse, $rows.first(), true)
                }
                $validationbrowse.data('previousActiveElement', $searchfield);
                focusValidationSearchBox($validationbrowse);

            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        // enter key now 'skips' into next field if input is blank. Should we check for whether or not the field is required? - OW 8/1/18
        $control.find('input[type="text"]').on('keydown', e => {
            if (e.which === 13) {
                // preventing default from enter key into next field on validation because the next field had special logic being triggered unintentionall - OW 8/1/18
                e.preventDefault();
                const inputs = jQuery('.fwformfield[data-enabled="true"] input[type="text"]:visible');
                let index = jQuery(inputs).index($searchfield) + 1;
                inputs.eq(index).focus();
            }
        });


        $control.find('.btnvalidate').on('click', function () {
            try {
                if ((typeof $control.attr('data-enabled') !== 'string') || ($control.attr('data-enabled') !== 'false')) {
                    FwValidation.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, false);
                }
                $validationbrowse.data('previousActiveElement', $searchfield);
                focusValidationSearchBox($validationbrowse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        var focusValidationSearchBox = function ($validationbrowse) {
            setTimeout(function () {
                var $searchBox = $validationbrowse.find('.search input:visible');
                $searchBox.eq(0).focus();
            }, 1000);
        };

        var preserveFocus = function ($validationbrowse) {
            const $previousActiveElement = $validationbrowse.data('previousActiveElement');
            $previousActiveElement.focus();
        }

        $validationbrowse.on('keydown', e => {
            const code = e.keyCode || e.which;
            switch (code) {
                case 27: //ESC key
                    try {
                        FwValidation.clearSearchCriteria($validationbrowse, $btnvalidate);
                        $popup = $validationbrowse.closest('.fwpopup');
                        FwPopup.detachPopup($popup);

                        preserveFocus($validationbrowse);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                    break;
                case 38: //Up Arrow Key
                    FwBrowse.selectPrevRow($validationbrowse);
                    break;
                case 40: //Down Arrow Key
                    FwBrowse.selectNextRow($validationbrowse);
                    break;
            }
        });

        $validationbrowse.data('onrowdblclick', e => {
            try {
                const $tr = jQuery(e.currentTarget);
                FwValidation.selectRow($control, $tr, $valuefield, $searchfield, $btnvalidate, $validationbrowse);
                const originalcolor = $searchfield.css('background-color');
                $searchfield.css('background-color', '#abcdef').animate({ backgroundColor: originalcolor }, 1500, function () { $searchfield.attr('background-color', '') });
                jQuery(document).off('keydown');

                const $rows = $validationbrowse.find('table > tbody > tr');
                if ($rows.length !== 1) {
                    preserveFocus($validationbrowse);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $validationbrowse.on('click', '.validationbuttons .btnSelect', e => {
            try {
                const $tr = $validationbrowse.find('tr.selected');
                if ($tr.length === 0) {
                    throw 'No row selected.';
                }
                else if ($tr.length === 1) {
                    FwValidation.selectRow($control, $tr, $valuefield, $searchfield, $btnvalidate, $validationbrowse);
                    jQuery(document).off('keydown');
                }
                // $trselected not in context. ow 9/13/18
                //else if ($trselected.length > 1) {
                //    throw 'Please select only one row.';
                //}
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $validationbrowse.find('.validationbuttons .btnClear').on('click', e => {
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
        });

        $validationbrowse.find('.validationbuttons .btnCancel').on('click', e => {
            try {
                FwValidation.clearSearchCriteria($validationbrowse, $btnvalidate);
                $popup = $validationbrowse.closest('.fwpopup');
                FwPopup.detachPopup($popup);
                jQuery(document).off('keydown');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $validationbrowse.find('.validationbuttons .btnNew').on('click', e => {
            const $this = jQuery(e.currentTarget);
            try {
                FwValidation.newValidation($control, validationName.slice(0, -10), $object, $this, $valuefield, $btnvalidate, $validationbrowse.attr('data-caption'));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $validationbrowse.find('input[type="text"]').on('keydown', e => {
            const code = e.keyCode || e.which;
            try {
                if (code === 13) { //Enter Key
                    this.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, false);
                    e.preventDefault();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $control.find('.btnpeek').on('click', e => {
            try {
                $control.find('.btnpeek').hide();
                $validationbrowse.data('$control').find('.validation-loader').show();
                setTimeout(() => {
                    this.validationPeek($control, validationName.slice(0, -10), $valuefield.val().toString(), $valuefield.parent().parent().attr('data-datafield'), $object, $searchfield.val());
                    $validationbrowse.data('$control').find('.validation-loader').hide();
                    $control.find('.btnpeek').show()
                })
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //---------------------------------------------------------------------------------
    validate($control: JQuery, validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $validationbrowse: JQuery, useSearchFieldValue: boolean) {
        if ($validationbrowse.length === 0) {
            throw `Missing validation template for: ${validationName}`;
        }
        if (typeof $control.data('getapiurl') === 'function') {
            $validationbrowse.data('validationmode', 2);
            $validationbrowse.attr('data-apiurl', $control.data('getapiurl')());
        }
        else if (typeof $control.attr('data-apiurl') === 'string' && $control.attr('data-apiurl').length > 0) {
            $validationbrowse.data('validationmode', 2);
            $validationbrowse.attr('data-apiurl', $control.attr('data-apiurl'));
        }

        $validationbrowse.data('$btnvalidate', $btnvalidate);
        $btnvalidate.hide();
        $validationbrowse.data('$control').find('.validation-loader').show();
        if (useSearchFieldValue && ($searchfield.val().toString().length > 0)) {
            let $validationSearchbox = $validationbrowse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
            if ($validationSearchbox.length == 1) {
                $validationSearchbox.val($searchfield.val());
            } else {
                throw 'FwValidation: Validation is not setup correctly. Missing validation display field.';
            }
        }
        if ($control.attr('data-showinactivemenu') === 'true') {
            this.addInactiveMenu($validationbrowse);
        }
        FwBrowse.search($validationbrowse);
    };
    //---------------------------------------------------------------------------------
    validateSearchCallback($validationbrowse: JQuery) {
        var $rows, $searchboxes, allSearchBoxesAreEmpty, showpopup, $headerfields, alreadyopen;
        var $btnvalidate = $validationbrowse.data('$control').find('.btnvalidate');
        FwValidation.hideValidateButtonLoadingIcon($btnvalidate);
        $headerfields = $validationbrowse.find('table > thead > tr > td > .field');
        $searchboxes = $headerfields.find('.search > input');
        $rows = $validationbrowse.find('table > tbody > tr');
        showpopup = true;
        alreadyopen = $validationbrowse.parents('.application').length > 0;
        if ($rows.length === 0) {
            allSearchBoxesAreEmpty = true;
            $searchboxes.each(function (index, element) {
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
            $searchboxes.each(function (index, element) {
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
            $rows.each(function (index, element) {
                var $validationdisplayfield;
                $validationdisplayfield = jQuery(element).find('[data-validationdisplayfield="true"]');
                if (typeof $validationdisplayfield.attr('data-originalvalue') === 'string' &&
                    $validationdisplayfield.attr('data-originalvalue').length > 0 &&
                    $validationdisplayheaderfield.find('.search input').val().toUpperCase() == $validationdisplayfield.attr('data-originalvalue').toUpperCase()) {
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
    selectRow($control: JQuery, $tr: JQuery, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $validationbrowse: JQuery) {
        var $validationUniqueIdField, uniqueid, $validationDisplayField, text, $popup;
        FwValidation.hideValidateButtonLoadingIcon($btnvalidate);

        $validationUniqueIdField = ($tr.find('.field[data-validationvaluefield="true"]').length !== 0) ? $tr.find('.field[data-validationvaluefield="true"]') : $tr.find('.field[data-isuniqueid="true"]');
        uniqueid = $validationUniqueIdField.attr('data-originalvalue');
        $valuefield.val(uniqueid).change();

        $validationDisplayField = $tr.find('.field[data-validationdisplayfield="true"]');
        text = $validationDisplayField.attr('data-originalvalue');
        $searchfield.val(text);

        if (typeof $control.data('onchange') === 'function') {
            $control.data('onchange')($tr);
        }

        FwValidation.clearSearchCriteria($validationbrowse, $btnvalidate);
        $popup = $validationbrowse.parents('.fwpopup');
        FwPopup.detachPopup($popup);
    };
    //---------------------------------------------------------------------------------
    clearSearchCriteria($validationbrowse: JQuery, $btnvalidate: JQuery) {
        var $validationSearchboxes, $validationSearchbox;
        FwValidation.hideValidateButtonLoadingIcon($btnvalidate);
        $validationSearchboxes = $validationbrowse.find('thead .field > .search > input');
        $validationSearchboxes.each(function (index, element) {
            $validationSearchbox = jQuery(element);
            $validationSearchbox.val('');
        });
    };
    //---------------------------------------------------------------------------------
    hideValidateButtonLoadingIcon($btnvalidate: JQuery) {
        $btnvalidate.find('.icon-validation').css('background-image', 'url(' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/browsesearch.001.png)');
    };
    //---------------------------------------------------------------------------------
    validationPeek($control: JQuery, validationName: string, validationId: string, validationDatafield: string, $parentFormOrBrowse, title) {
        let $popupForm;

        var $validationbrowse = jQuery(jQuery(`#tmpl-validations-${validationName}ValidationBrowse`).html());
        const peekForm = $validationbrowse.attr('data-peekForm');      // for validations without a form, this attr can be added to point to an alternate form to open in peek - J. Pace
        validationDatafield = $validationbrowse.find('div[data-browsedatatype="key"]').data('datafield');

        try {
            if (validationId !== '') {
                //if (jQuery(`#tmpl-modules-${validationName}Form`).html() === undefined) {
                //    if (peekForm) {
                //        $popupForm = window[`${peekForm}Controller`].openForm('EDIT');
                //    } else {
                //        $popupForm = jQuery(window[`${validationName}Controller`].getFormTemplate());      // commented out unnecessary code - J. Pace 10/14/19
                //    }
                //} else {
                //    $popupForm = jQuery(jQuery(`#tmpl-modules-${validationName}Form`).html());
                //}
                //$popupForm = window[validationName + 'Controller'].openForm('EDIT');

                const ids: any = {};
                ids[validationDatafield] = validationId;
                if (peekForm) {
                    $popupForm = window[`${peekForm}Controller`].loadForm(ids);
                } else {
                    $popupForm = window[`${validationName}Controller`].loadForm(ids);
                }
                //$popupForm.find(`div.fwformfield[data-datafield="${validationDatafield}"] input`).val(validationId);
                const $popupControl = FwPopup.renderPopup($popupForm, undefined, title, validationId);
                FwPopup.showPopup($popupControl);
                const $fwcontrols = $popupForm.find('.fwcontrol');
                FwControl.loadControls($fwcontrols);
                $popupForm.find('.btnpeek').remove();
                $popupForm.css({ 'background-color': 'white', 'box-shadow': '0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22)', 'width': '60vw', 'height': '60vh', 'overflow': 'scroll', 'position': 'relative' });
                //FwModule.setFormReadOnly($popupForm);


                //jQuery(document).find('.fwpopup').on('click', function (e) {   // - Removed this event that closes validationpeeks on click outside of the popup.  -Jason Hoang 9/10/18  
                //    e = e || window.event;
                //    if (e.target.outerHTML === '<i class="material-icons"></i>' || e.target.outerHTML === '<div class="btn-text">Save</div>') {

                //    } else {
                //        FwPopup.destroyPopup(this);
                //        jQuery(document).off('keydown');
                //        jQuery(document).find('.fwpopup').off('click');
                //    }
                //});

                jQuery(document).on('keydown', function (e) {
                    var code = e.keyCode || e.which;
                    if (code === 27) { //ESC Key  
                        try {
                            FwPopup.destroyPopup($popupControl);
                            jQuery(document).find('.fwpopup').off('click');
                            jQuery(document).off('keydown');
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
            };
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    };
    //---------------------------------------------------------------------------------
    newValidation($control, validationName, $object, $this, $valuefield, $btnvalidate, title) {
        var $popupForm;
        var $object = ($control.closest('.fwbrowse[data-controller!=""]').length > 0) ? $control.closest('.fwbrowse[data-controller!=""]') : $control.closest('.fwform[data-controller!=""]');
        var $validationbrowse = $this.closest('div[data-control="FwBrowse"][data-type="Validation"]');

        try {
            if (jQuery('#tmpl-modules-' + validationName + 'Form').html() === undefined) {
                $popupForm = jQuery(window[validationName + 'Controller'].getFormTemplate());
            } else {
                $popupForm = jQuery(jQuery('#tmpl-modules-' + validationName + 'Form').html());
            }
            $popupForm = window[validationName + 'Controller'].openForm('NEW');
            $popupForm.find('.btnpeek').remove();
            $popupForm.css({ 'background-color': 'white', 'box-shadow': '0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22)', 'width': '60vw', 'height': '60vh', 'overflow': 'scroll', 'position': 'relative' });

            $popupForm.data('afterSaveNewValidation', function () {
                FwValidation.validate($control, validationName, $valuefield, null, $btnvalidate, $validationbrowse, false);
            })

            const $popupControl = FwPopup.renderPopup($popupForm, undefined, 'New ' + title);
            FwPopup.showPopup($popupControl);

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
                var code = e.keyCode || e.which;
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
    }
    //---------------------------------------------------------------------------------
    addInactiveMenu($browse: JQuery) {
        $browse.find('.runtime .fwbrowse-menu').remove();
        $browse.closest('[data-control="FwBrowse"]').attr('data-activeinactiveview', 'active');
        $browse.find('.runtime .browsecaption').after(`<div class="fwbrowse-menu"><div class="fwcontrol fwmenu default" data-control="FwMenu" data-visible="true" data-rendermode="runtime"><div class="buttonbar"></div></div></div></div>`);
        const $activeView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('Active'), true);
        $activeView.on('click', e => {
            try {
                const $fwbrowse = jQuery(e.currentTarget).closest('.fwbrowse');
                $fwbrowse.attr('data-activeinactiveview', 'active');
                FwBrowse.search($fwbrowse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        const $inactiveView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('Inactive'), false);
        $inactiveView.on('click', e => {
            try {
                const $fwbrowse = jQuery(e.currentTarget).closest('.fwbrowse');
                $fwbrowse.attr('data-activeinactiveview', 'inactive');
                FwBrowse.search($fwbrowse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        const $allView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('All'), false);
        $allView.on('click', e => {
            try {
                const $fwbrowse = jQuery(e.currentTarget).closest('.fwbrowse');
                $fwbrowse.attr('data-activeinactiveview', 'all');
                FwBrowse.search($fwbrowse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        const viewitems: JQuery[] = [];
        viewitems.push($activeView, $inactiveView, $allView);
        const $menu = FwMenu.getMenuControl('default');
        const $show = FwMenu.addViewBtn($menu, FwLanguages.translate('Show'), viewitems);
        $browse.find('.fwbrowse-menu .buttonbar').empty().append($show);
        $browse.find('.fwbrowse-menu .buttonbar .ddviewbtn').css('margin-left', 'auto');
    }
    //----------------------------------------------------------------------------------------------
    getValidationsWithPeeks(): string[] {
        let validationsWithPeeks: string[] = [];
        if ((<any>window)['Constants'] !== undefined && (<any>window)['Constants'].validationsWithPeeks !== undefined) {
            validationsWithPeeks = (<any>window)['Constants'].validationsWithPeeks;
        }
        return validationsWithPeeks;
    }
    //----------------------------------------------------------------------------------------------
    isValidationWithPeek($control: JQuery<HTMLElement>): boolean { //temp (inefficient) solution, til security tree implementations are in place, used to determine whether to render peek buttons - jpace 7/16/19
        const validationsWithPeeks: string[] = this.getValidationsWithPeeks();
        const result: boolean = (validationsWithPeeks.indexOf($control.attr('data-validationname')) > -1);
        return result;
    }
    //----------------------------------------------------------------------------------------------
}
var FwValidation = new FwValidationClass();
