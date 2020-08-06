class FwBrowseColumn_comboboxClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        var displayFieldValue = dtRow[dt.ColumnIndex[$field.attr('data-browsedisplayfield')]];
        $field.attr('data-originaltext', displayFieldValue);
    }
    //---------------------------------------------------------------------------------
    getFieldUniqueId($browse, $tr, $field, uniqueid, originalvalue): void {
        if ($tr.hasClass('editmode')) {
            uniqueid.value = $field.find('input.value').val();
        }
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        $field.find('.value').val(data.value);
        $field.find('.text').val(data.text);
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        var originaltext  = (typeof $field.attr('data-originaltext')   === 'string') ? $field.attr('data-originaltext') : '';
        $field.html(originaltext);
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var validationName, validationFor, $valuefield, $textfield, $btnvalidate;
        var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
        var originaltext  = (typeof $field.attr('data-originaltext')   === 'string') ? $field.attr('data-originaltext') : '';
        let html = [];
        html.push('<input class="value" type="hidden" />');
        html.push('<input class="text" type="text" autocapitalize="none"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled');
        }
        html.push(' />');
        html.push('<i class="material-icons md-dark btnvalidate">&#xE5CF;</i>'); //expand_more
        let htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue, text: originaltext });
        this.initControl($field);
    }
    //---------------------------------------------------------------------------------
    // begin shared code with browse column
    //---------------------------------------------------------------------------------
    initControl($control) {
        var me = this;
        var $validationbrowse, $popup, $object, controller, formbeforevalidate, control_boundfields, boundfields, validationName, $valuefield, $btnvalidate;
        validationName      = (typeof $control.attr('data-validationname') != 'undefined') ? $control.attr('data-validationname') : $control.attr('data-formvalidationname');
        $valuefield         = $control.find('.value');
        $btnvalidate        = $control.find('.btnvalidate');
        $validationbrowse   = jQuery(jQuery('#tmpl-validations-' + validationName + 'Browse').html());
        $object             = ($control.closest('.fwbrowse[data-controller!=""]').length > 0) ? $control.closest('.fwbrowse[data-controller!=""]') : $control.closest('.fwform[data-controller!=""]');

        // auto generate controllers for validations if they don't have one, so we only have to look in 1 place for the apiurl
        if (typeof $validationbrowse.attr('data-name') !== 'undefined' && typeof $validationbrowse.attr('data-apiurl') !== 'undefined') {
            if (typeof window[$validationbrowse.attr('data-apiurl') + 'Controller'] === 'undefined') {
                window[$validationbrowse.attr('data-name') + 'Controller'] = {
                    Module: $validationbrowse.attr('data-name'),
                    apiurl: $validationbrowse.attr('data-apiurl')
                };
            } else {
                var controllerObj = window[$validationbrowse.attr('data-apiurl') + 'Controller'];
                if (typeof controllerObj.Module === 'undefined') {
                    controllerObj.Module = $validationbrowse.attr('data-name');
                }
                if (typeof controllerObj.apiurl === 'undefined') {
                    controllerObj.apiurl = $validationbrowse.attr('data-apiurl');
                }
            }
        } else if (typeof $validationbrowse.attr('data-name') !== 'undefined') {
            if (typeof window[$validationbrowse.attr('data-apiurl') + 'Controller'] === 'undefined') {
                window[$validationbrowse.attr('data-name') + 'Controller'] = {
                    Module: $validationbrowse.attr('data-name')
                };
            }
        }

        controller = $object.attr('data-controller');
        formbeforevalidate  = $control.attr('data-formbeforevalidate');
        control_boundfields = $control.attr('data-boundfields');
        if (typeof control_boundfields != 'undefined') {
            boundfields = control_boundfields.split(',');
        }
        FwBrowse.init($validationbrowse);
        $validationbrowse.attr('data-name', validationName);
    
        // setup the request for the databind method
        $validationbrowse.data('ondatabind', function(request) {
            request.module = validationName;
            if ((typeof boundfields != 'undefined') && (boundfields.length > 0)) {
                request.boundids = {};
                for (var i = 0; i < boundfields.length; i++) {
                    request.boundids[boundfields[i].split('.').pop()] = FwFormField.getValue2($object.find('.fwformfield[data-datafield="' + boundfields[i] + '"]'));
                }
            }
            if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller][formbeforevalidate] === 'function')) {
                if ($object.attr('data-type') === 'Grid') {
                    window[controller][formbeforevalidate]($validationbrowse, $object.closest('.fwform'), request);
                } else {
                    window[controller][formbeforevalidate]($validationbrowse, $object, request);
                }
            }
        });
    
        // overrides the databind method in FwBrowse
        $validationbrowse.data('calldatabind', function(request) {
            //if (typeof $control.attr('data-pagesize') === 'string') {
            //    request.pagesize = parseInt($control.attr('data-pagesize'));
            //}
            FwServices.validation.method(request, request.module, 'Browse', $control, function(response) {
                try {
                    var validationController = window[$validationbrowse.attr('data-name') + 'Controller'];
                    if (typeof validationController.apiurl !== 'undefined') {
                        me.databindcallback($control, $validationbrowse, response, validationName, $valuefield, $btnvalidate, $validationbrowse, controller);
                    } else {
                        me.databindcallback($control, $validationbrowse, response.browse, validationName, $valuefield, $btnvalidate, $validationbrowse, controller);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        });
        FwBrowse.renderRuntimeHtml($validationbrowse);
        if (typeof $control.data('oninit') === 'function') {
            $control.data('oninit')($control, $validationbrowse, $popup, $valuefield, $btnvalidate);
        }
    
        //if ($control.attr('data-validate') === 'true') {
        //    $control.on('change', '.fwformfield-value', function() {
        //        try {
        //            // need to clear out any boundfields here
        //        } catch (ex) {
        //            FwFunc.showError(ex);
        //        }
        //    });
        //    $control.on('change', '.fwformfield-text', function() {
        //        try {
        //            if ($searchfield.val().length === 0) {
        //                $valuefield.val('').change();
        //                FwBrowseColumn_combobox.closeDropDown($control);
        //            } else {
        //                FwBrowseColumn_combobox.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, true);
        //            }
        //        } catch (ex) {
        //            FwFunc.showError(ex);
        //        }
        //    });
        //}
        $control
            .on('keydown', '.value', function(e) {
                var $row, search, usesearchfield;
                try {
                    //alert(e.which);
                    search = true;
                    usesearchfield= true;
                    if (e.which === 27) { // Esc
                        if ($control.data('dropdown') !== null) {
                            $control.data('dropdown').remove();
                            $control.data('dropdown', null);
                        }
                        search = false;
                    }
                    if ($control.data('dropdown') !== null) {
                        if (e.which === 38) { // Up Arrow
                            me.highlightPrevRow($control);
                            e.preventDefault();
                            return;
                        }
                        if (e.which === 40) { // Down Arrow
                            me.highlightNextRow($control);
                            e.preventDefault();
                            return;
                        }
                        if(e.which === 13) { //Enter Key
                            $row = me.getHighlightedRow($control);
                            me.selectRow($control, $row, validationName, $valuefield, $btnvalidate, $validationbrowse, controller);
                            return;
                        }
                    } else {
                        if (e.which === 40) { // Down Arrow
                            usesearchfield = false;
                        }
                    }
                    switch(e.which) {
                        case 8:
                        case 46:
                            if ($control.find('.value').val().length <= 0) {
                                search = false;
                                me.closeDropDown($control, false);
                            }
                            break;
                        case 9: // Tab
                            search = false;
                            me.closeDropDown($control, false);
                            break;
                        case 12: // numpad(5)
                        case 13: // Enter
                            search = false;
                            $control.find('.value').change();
                            e.preventDefault();
                            break;
                        case 16: // shift
                        case 17: // ctrl
                        case 18: // alt
                        case 19: // pause/break
                        case 36: // home/numpad(7)
                        case 33: // pgup/numpad(9)
                        case 34: // pgdn/numpad(3)
                        case 35: // end/numpad(1)
                        case 37: // left/numpad(4)
                        case 38: // up/numpad(8)
                        case 39: // right/numpad(6)
                        case 45: // insert
                        case 92: // windows
                        case 93: // context menu
                        case 144: // num lock
                        case 145: // scroll lock
                            search = false;
                            break;
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnvalidate', function() {
                try {
                    if ((typeof $control.data('dropdown') != 'undefined') && ($control.data('dropdown') !== null)) {
                        me.closeDropDown($control, true);
                    }
                    else if ((typeof $control.attr('data-enabled') !== 'string') || ($control.attr('data-enabled') !== 'false')) {
                        me.validate($control, validationName, $valuefield, $btnvalidate, $validationbrowse, false);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
    }
    //---------------------------------------------------------------------------------
    databindcallback($control, $browse, dt, validationName, $valuefield, $btnvalidate, $validationbrowse, controller) {
        let me = this;
        if (typeof dt === 'undefined') throw 'Unable to load data: dt is undefined.';
        var html = [], $dropdown, controlOffset, originalcolor;
        var pageSize = parseInt($validationbrowse.attr('data-pagesize'));
        var htmlPager = [];
        var rownostart = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? ((dt.PageNo * pageSize) - pageSize + 1) : 0;
        var rownoend   = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? (dt.PageNo * pageSize) - (pageSize - dt.Rows.length) : 0;
    
        // focus the searchfield so the user can use keydown handler on the field to arrow up and down through the dropdown
        $valuefield.focus();
    
        if (dt.Rows.length > 0) {
            var uniqueid, displayfield;
            for (var i = 0; i < dt.Columns.length; i++) {
                if (dt.Columns[i].IsUniqueId) {
                    uniqueid = dt.Columns[i].DataField;
                    break;
                }
            }
            displayfield = $validationbrowse.find('table > thead > tr > td > .field[data-validationdisplayfield="true"]').eq(0).attr('data-browsedatafield');
            if (typeof uniqueid !== 'string') throw 'data-browsedatafield is not setup on the validation template.';
            if (typeof displayfield !== 'string') throw 'data-browsedatafield is not setup on the validation template.';

            html.push('<div class="dropdown">');
            html.push('  <div class="rows">');
            for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
                html.push('<div class="row" data-uniqueid="' + dt.Rows[rowno][dt.ColumnIndex[uniqueid]] + '">' + dt.Rows[rowno][dt.ColumnIndex[displayfield]] + '</div>');
            }
            html.push('  </div>');
            html.push('</div>');
            $dropdown = jQuery(html.join('\n'));
            $dropdown.css('z-index', FwFunc.getMaxZ('*'));
            if (FwFunc.isDesktop()) {
                $dropdown.find('.rows').css('min-height', pageSize * 22);
            } else if (FwFunc.isMobile()) {
                $dropdown.find('.rows').css('min-height', (pageSize * 32) + 10);
            }
            $dropdown.on('mouseover', '.row', function() {
                var $row = jQuery(this);
                me.highlightRow($control, $row);
            });
            $dropdown.on('click', '.row', function() {
                var $row = jQuery(this);
                me.selectRow($control, $row, validationName, $valuefield, $btnvalidate, $validationbrowse, controller);
            });
            controlOffset = $control.offset();
            if ($control.data('dropdown') !== null) {
                me.closeDropDown($control, false);
                $control.append($dropdown);
            } else {
                $dropdown.hide();
                $control.append($dropdown);
                $dropdown.slideDown(200);
            }
            //if (FwFunc.isDesktop()) {
            //    $dropdown.offset({
            //        top: controlOffset.top + 42,
            //        left: controlOffset.left + 5});
            //} else if (FwFunc.isMobile()) {
            //    $dropdown.offset({
            //        top: controlOffset.top + 52,
            //        left: controlOffset.left + 5});
            //}
            //$dropdown.width($control.width() - 34);
            $dropdown.width($control.width() - 2);
            $control.data('dropdown', $dropdown);

            if (dt.TotalPages > 1) {
                htmlPager.push('<div class="pager dropdownpager">');
                    if ((pageSize > 0) && (dt.PageNo > 1)) {
                        htmlPager.push('<div tabindex="0" class="button btnFirstPage" data-enabled="true" title="First" alt="First"><div class="firsticon"></div></div>');
                        htmlPager.push('<div tabindex="0" class="button btnPreviousPage" data-enabled="true" title="Previous" alt="Previous"><div class="previousicon"></div></div>');
                    } else {
                        htmlPager.push('<div class="button btnFirstPage" disabled="disabled" data-enabled="false" title="First" alt="First"><div class="firsticon"></div></div>');
                        htmlPager.push('<div class="button btnPreviousPage" disabled="disabled" data-enabled="false" title="Previous" alt="Previous"><div class="previousicon"></div></div>');
                    }
                    htmlPager.push('<input class="txtPageNo" type="text" value="' + dt.PageNo + '"/>');
                    htmlPager.push('<span class="of"> of </span>');
                    htmlPager.push('<span class="txtTotalPages">' + dt.TotalPages + '</span>');
                    if ((pageSize > 0) &&(dt.TotalPages > 1) && (dt.PageNo < dt.TotalPages)) {
                        htmlPager.push('<div class="button btnNextPage" data-enabled="true" title="Next" alt="Next"><div class="nexticon"></div></div>');
                        htmlPager.push('<div class="button btnLastPage" data-enabled="true" title="Last" alt="Last"><div class="lasticon"></div></div>');
                    } else {
                        htmlPager.push('<div class="button btnNextPage" disabled="disabled" data-enabled="false" title="Next" alt="Next"><div class="nexticon"></div></div>');
                        htmlPager.push('<div class="button btnLastPage" disabled="disabled" data-enabled="false" title="Last" alt="Last"><div class="lasticon"></div></div>');
                    }
                htmlPager.push('</div>');
            }
            var $pager = jQuery(htmlPager.join('\n'));
            $pager.find('select.pagesize').val(pageSize);
            $dropdown.append($pager);

            $pager.find('.btnFirstPage')
                .on('click', function() {
                    var pageno, $thisbtn;
                    try {
                        $thisbtn = jQuery(this);
                        if ($thisbtn.attr('data-enabled') === 'true') {
                            $validationbrowse.attr('data-pageno', '1');
                            FwBrowse.databind($validationbrowse);
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
            ;
            $pager.find('.btnPreviousPage')
                .on('click', function() {
                    var pageno, $thisbtn;
                    try {
                        $thisbtn = jQuery(this);
                        if ($thisbtn.attr('data-enabled') === 'true') {
                            pageno = parseInt($pager.find('.txtPageNo').val().toString()) - 1;
                            pageno = (pageno >= 1) ? pageno : 1;
                            $validationbrowse.attr('data-pageno', pageno.toString());
                            FwBrowse.databind($validationbrowse);
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
            ;
            $pager.find('.txtPageNo')
                .on('change', function() {
                    var pageno, originalpageno, originalpagenoStr, $txtPageNo, totalPages;
                    try {
                        $txtPageNo = jQuery(this);
                        originalpagenoStr = $txtPageNo.val();
                        if (!isNaN(originalpagenoStr)) {
                            pageno = parseInt(originalpagenoStr);
                            originalpageno = pageno;
                            totalPages = parseInt($pager.find('.txtTotalPages').html());
                            pageno = (pageno >= 1) ? pageno : 1;
                            pageno = (pageno <= totalPages) ? pageno : totalPages;
                            if (pageno === originalpageno) {
                                $validationbrowse.attr('data-pageno', pageno.toString());
                                FwBrowse.databind($validationbrowse);
                            } else {
                                $pager.find('.txtTotalPages').val(pageno);
                            }
                        } else {

                        }
                    } catch(ex) {
                        $pager.find('.txtTotalPages').val(originalpagenoStr);
                        FwFunc.showError(ex);
                    }
                })
            ;
            $pager.find('.btnNextPage')
                .on('click', function() {
                    var pageno, totalPages, $thisbtn;
                    try {
                        let $btnNextPage = jQuery(this);
                        if ($btnNextPage.attr('data-enabled') === 'true') {
                            pageno = parseInt($pager.find('.txtPageNo').val().toString()) + 1;
                            totalPages = parseInt($pager.find('.txtTotalPages').html());
                            pageno = (pageno <= totalPages) ? pageno : totalPages;
                            $validationbrowse.attr('data-pageno', pageno.toString());
                            FwBrowse.databind($validationbrowse);
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
            ;
            $pager.find('.btnLastPage')
                .on('click', function() {
                    var pageno, $thisbtn;
                    try {
                        $thisbtn = jQuery(this);
                        if ($thisbtn.attr('data-enabled') === 'true') {
                            pageno = parseInt($pager.find('.txtTotalPages').html());
                            $validationbrowse.attr('data-pageno', pageno.toString());
                            FwBrowse.databind($validationbrowse);
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
            ;
        
            // highlight the first row
            if (FwFunc.isDesktop()) {
                me.highlightRow($control, me.getHighlightedRow($control));
            }
    
            // register a one time event to close the dropdown when the users clicks on the document
            $control.data('onclickdocument', function(e) {
                var $target = jQuery(e.target);
                if ($target.closest('.dropdownpager').length === 0) {
                    me.closeDropDown($control, false);
                } else {
                    jQuery(document).one('click', $control.data('onclickdocument'));
                }
            });
            jQuery(document).one('click', $control.data('onclickdocument'));
        } else {
            originalcolor = $valuefield.css('background-color');
            $valuefield.css('background-color', '#ffcccc').animate({backgroundColor: originalcolor}, 1500 , function() { $valuefield.attr('style', '') });
        }
    }
    //---------------------------------------------------------------------------------
    closeDropDown($control, showAnimation) {
        if ((typeof $control.data('dropdown') !== 'undefined') && ($control.data('dropdown') !== null)) {
            if (showAnimation) {
                $control.data('dropdown').slideUp({
                    duration: 250
                }, function() {
                    $control.data('dropdown').remove();
                });
            } else {
                $control.data('dropdown').remove();
            }
        }
        $control.data('dropdown', null);
    }
    //---------------------------------------------------------------------------------
    validate($control, validationName, $valuefield, $btnvalidate, $validationbrowse, useSearchFieldValue) {
        var $validationSearchbox;

        this.clearSearchCriteria($validationbrowse);
        if (useSearchFieldValue) {
            $validationSearchbox = $validationbrowse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
            if ($validationSearchbox.length == 1) {
                $validationSearchbox.val($valuefield.val());
            } else {
                throw 'FwBrowseColumn_combobox: Validation is not setup correctly. Missing validation display field.';
            }
            if ($valuefield.val().length === 0) {
                this.closeDropDown($control, false);
            }
        }
        FwBrowse.search($validationbrowse);
    };
    //---------------------------------------------------------------------------------
    selectRow($control, $tr, validationName, $valuefield, $btnvalidate, $validationbrowse, controller) {
        var uniqueid, text;
    
        this.highlightRow($control, $tr);

        // set the uniqueid
        uniqueid = $tr.attr('data-uniqueid');
        // mv 2016-01-15 having problems with $valuefield and $seachfield not working when modifying val, so requerying from $control
        $control.find('.value').val(uniqueid);
        //set the text
        if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['loadRelatedValidationFields'] === 'function')) {
            window[controller]['loadRelatedValidationFields'](validationName, $valuefield, $tr);
        }
        this.clearSearchCriteria($validationbrowse);
        this.closeDropDown($control, true);
        let originalcolor = $valuefield.css('background-color');
        $valuefield.css('background-color', '#abcdef').animate({backgroundColor: originalcolor}, 1500 , function() { $valuefield.attr('style', '') });
        $control.find('.value').change();
    }
    //---------------------------------------------------------------------------------
    clearSearchCriteria($validationbrowse) {
        var $validationSearchboxes, $validationSearchbox;
        $validationSearchboxes = $validationbrowse.find('thead .field > .search > input');
        $validationSearchboxes.each(function(index, element) {
            $validationSearchbox = jQuery(element);
            $validationSearchbox.val('');
        });
    }
    //---------------------------------------------------------------------------------
    getHighlightedRow($control) {
        var $row = null;
        if ($control.data('dropdown') !== null) {
            $row = $control.data('dropdown').find('.row.selected');
            if (($row.length === 0) && ($control.data('dropdown').find('.row').length > 0)) {
                $row = $control.data('dropdown').find('.row').eq(0);
                this.highlightRow($control, $row);
            }
        }
        return $row;
    }
    //---------------------------------------------------------------------------------
    highlightRow($control, $row) {
        if (($control.data('dropdown') !== null) && (typeof $row !== 'undefined') && ($row.length > 0)) {
            $control.data('dropdown').find('.row').removeClass('selected');
            $row.addClass('selected');
        }
    }
    //---------------------------------------------------------------------------------
    highlightNextRow($control) {
        var $row = this.getHighlightedRow($control);
        if ($row !== null) {
            var $nextrow = $row.next('.row');
            if ($nextrow.length > 0) {
                this.highlightRow($control, $nextrow);
            }
        }
    };
    //---------------------------------------------------------------------------------
    highlightPrevRow($control) {
        var $row = this.getHighlightedRow($control);
        if ($row !== null) {
            var $prevrow = $row.prev('.row');
            if ($prevrow.length > 0) {
                this.highlightRow($control, $prevrow);
            }
        }
    };
    //---------------------------------------------------------------------------------
    // end shared code with browse column
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_combobox = new FwBrowseColumn_comboboxClass();