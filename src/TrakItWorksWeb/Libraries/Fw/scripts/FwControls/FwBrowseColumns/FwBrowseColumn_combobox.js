var FwBrowseColumn_comboboxClass = (function () {
    function FwBrowseColumn_comboboxClass() {
    }
    FwBrowseColumn_comboboxClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
        var displayFieldValue = dtRow[dt.ColumnIndex[$field.attr('data-browsedisplayfield')]];
        $field.attr('data-originaltext', displayFieldValue);
    };
    FwBrowseColumn_comboboxClass.prototype.getFieldUniqueId = function ($browse, $tr, $field, uniqueid, originalvalue) {
        if ($tr.hasClass('editmode')) {
            uniqueid.value = $field.find('input.value').val();
        }
    };
    FwBrowseColumn_comboboxClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    };
    FwBrowseColumn_comboboxClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        $field.find('.value').val(data.value);
        $field.find('.text').val(data.text);
    };
    FwBrowseColumn_comboboxClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_comboboxClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
        $field.html(originaltext);
    };
    FwBrowseColumn_comboboxClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var validationName, validationFor, $valuefield, $textfield, $btnvalidate;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
        var html = [];
        html.push('<input class="value" type="hidden" />');
        html.push('<input class="text" type="text" autocapitalize="none"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled');
        }
        html.push(' />');
        html.push('<i class="material-icons md-dark btnvalidate">&#xE5CF;</i>');
        var htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue, text: originaltext });
        this.initControl($field);
    };
    FwBrowseColumn_comboboxClass.prototype.initControl = function ($control) {
        var me = this;
        var $validationbrowse, $popup, $object, controller, formbeforevalidate, control_boundfields, boundfields, validationName, $valuefield, $btnvalidate;
        validationName = (typeof $control.attr('data-validationname') != 'undefined') ? $control.attr('data-validationname') : $control.attr('data-formvalidationname');
        $valuefield = $control.find('.value');
        $btnvalidate = $control.find('.btnvalidate');
        $validationbrowse = jQuery(jQuery('#tmpl-validations-' + validationName + 'Browse').html());
        $object = ($control.closest('.fwbrowse[data-controller!=""]').length > 0) ? $control.closest('.fwbrowse[data-controller!=""]') : $control.closest('.fwform[data-controller!=""]');
        if (typeof $validationbrowse.attr('data-name') !== 'undefined' && typeof $validationbrowse.attr('data-apiurl') !== 'undefined') {
            if (typeof window[$validationbrowse.attr('data-apiurl') + 'Controller'] === 'undefined') {
                window[$validationbrowse.attr('data-name') + 'Controller'] = {
                    Module: $validationbrowse.attr('data-name'),
                    apiurl: $validationbrowse.attr('data-apiurl')
                };
            }
            else {
                var controllerObj = window[$validationbrowse.attr('data-apiurl') + 'Controller'];
                if (typeof controllerObj.Module === 'undefined') {
                    controllerObj.Module = $validationbrowse.attr('data-name');
                }
                if (typeof controllerObj.apiurl === 'undefined') {
                    controllerObj.apiurl = $validationbrowse.attr('data-apiurl');
                }
            }
        }
        else if (typeof $validationbrowse.attr('data-name') !== 'undefined') {
            if (typeof window[$validationbrowse.attr('data-apiurl') + 'Controller'] === 'undefined') {
                window[$validationbrowse.attr('data-name') + 'Controller'] = {
                    Module: $validationbrowse.attr('data-name')
                };
            }
        }
        controller = $object.attr('data-controller');
        formbeforevalidate = $control.attr('data-formbeforevalidate');
        control_boundfields = $control.attr('data-boundfields');
        if (typeof control_boundfields != 'undefined') {
            boundfields = control_boundfields.split(',');
        }
        FwBrowse.init($validationbrowse);
        $validationbrowse.attr('data-name', validationName);
        $validationbrowse.data('ondatabind', function (request) {
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
                }
                else {
                    window[controller][formbeforevalidate]($validationbrowse, $object, request);
                }
            }
        });
        $validationbrowse.data('calldatabind', function (request) {
            FwServices.validation.method(request, request.module, 'Browse', $control, function (response) {
                try {
                    var validationController = window[$validationbrowse.attr('data-name') + 'Controller'];
                    if (typeof validationController.apiurl !== 'undefined') {
                        me.databindcallback($control, $validationbrowse, response, validationName, $valuefield, $btnvalidate, $validationbrowse, controller);
                    }
                    else {
                        me.databindcallback($control, $validationbrowse, response.browse, validationName, $valuefield, $btnvalidate, $validationbrowse, controller);
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        });
        FwBrowse.renderRuntimeHtml($validationbrowse);
        if (typeof $control.data('oninit') === 'function') {
            $control.data('oninit')($control, $validationbrowse, $popup, $valuefield, $btnvalidate);
        }
        $control
            .on('keydown', '.value', function (e) {
            var $row, search, usesearchfield;
            try {
                search = true;
                usesearchfield = true;
                if (e.which === 27) {
                    if ($control.data('dropdown') !== null) {
                        $control.data('dropdown').remove();
                        $control.data('dropdown', null);
                    }
                    search = false;
                }
                if ($control.data('dropdown') !== null) {
                    if (e.which === 38) {
                        me.highlightPrevRow($control);
                        e.preventDefault();
                        return;
                    }
                    if (e.which === 40) {
                        me.highlightNextRow($control);
                        e.preventDefault();
                        return;
                    }
                    if (e.which === 13) {
                        $row = me.getHighlightedRow($control);
                        me.selectRow($control, $row, validationName, $valuefield, $btnvalidate, $validationbrowse, controller);
                        return;
                    }
                }
                else {
                    if (e.which === 40) {
                        usesearchfield = false;
                    }
                }
                switch (e.which) {
                    case 8:
                    case 46:
                        if ($control.find('.value').val().length <= 0) {
                            search = false;
                            me.closeDropDown($control, false);
                        }
                        break;
                    case 9:
                        search = false;
                        me.closeDropDown($control, false);
                        break;
                    case 12:
                    case 13:
                        search = false;
                        $control.find('.value').change();
                        e.preventDefault();
                        break;
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 36:
                    case 33:
                    case 34:
                    case 35:
                    case 37:
                    case 38:
                    case 39:
                    case 45:
                    case 92:
                    case 93:
                    case 144:
                    case 145:
                        search = false;
                        break;
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.btnvalidate', function () {
            try {
                if ((typeof $control.data('dropdown') != 'undefined') && ($control.data('dropdown') !== null)) {
                    me.closeDropDown($control, true);
                }
                else if ((typeof $control.attr('data-enabled') !== 'string') || ($control.attr('data-enabled') !== 'false')) {
                    me.validate($control, validationName, $valuefield, $btnvalidate, $validationbrowse, false);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    FwBrowseColumn_comboboxClass.prototype.databindcallback = function ($control, $browse, dt, validationName, $valuefield, $btnvalidate, $validationbrowse, controller) {
        var me = this;
        if (typeof dt === 'undefined')
            throw 'Unable to load data: dt is undefined.';
        var html = [], $dropdown, controlOffset, originalcolor;
        var pageSize = parseInt($validationbrowse.attr('data-pagesize'));
        var htmlPager = [];
        var rownostart = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? ((dt.PageNo * pageSize) - pageSize + 1) : 0;
        var rownoend = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? (dt.PageNo * pageSize) - (pageSize - dt.Rows.length) : 0;
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
            if (typeof uniqueid !== 'string')
                throw 'data-browsedatafield is not setup on the validation template.';
            if (typeof displayfield !== 'string')
                throw 'data-browsedatafield is not setup on the validation template.';
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
            }
            else if (FwFunc.isMobile()) {
                $dropdown.find('.rows').css('min-height', (pageSize * 32) + 10);
            }
            $dropdown.on('mouseover', '.row', function () {
                var $row = jQuery(this);
                me.highlightRow($control, $row);
            });
            $dropdown.on('click', '.row', function () {
                var $row = jQuery(this);
                me.selectRow($control, $row, validationName, $valuefield, $btnvalidate, $validationbrowse, controller);
            });
            controlOffset = $control.offset();
            if ($control.data('dropdown') !== null) {
                me.closeDropDown($control, false);
                $control.append($dropdown);
            }
            else {
                $dropdown.hide();
                $control.append($dropdown);
                $dropdown.slideDown(200);
            }
            $dropdown.width($control.width() - 2);
            $control.data('dropdown', $dropdown);
            if (dt.TotalPages > 1) {
                htmlPager.push('<div class="pager dropdownpager">');
                if ((pageSize > 0) && (dt.PageNo > 1)) {
                    htmlPager.push('<div tabindex="0" class="button btnFirstPage" data-enabled="true" title="First" alt="First"><div class="firsticon"></div></div>');
                    htmlPager.push('<div tabindex="0" class="button btnPreviousPage" data-enabled="true" title="Previous" alt="Previous"><div class="previousicon"></div></div>');
                }
                else {
                    htmlPager.push('<div class="button btnFirstPage" disabled="disabled" data-enabled="false" title="First" alt="First"><div class="firsticon"></div></div>');
                    htmlPager.push('<div class="button btnPreviousPage" disabled="disabled" data-enabled="false" title="Previous" alt="Previous"><div class="previousicon"></div></div>');
                }
                htmlPager.push('<input class="txtPageNo" type="text" value="' + dt.PageNo + '"/>');
                htmlPager.push('<span class="of"> of </span>');
                htmlPager.push('<span class="txtTotalPages">' + dt.TotalPages + '</span>');
                if ((pageSize > 0) && (dt.TotalPages > 1) && (dt.PageNo < dt.TotalPages)) {
                    htmlPager.push('<div class="button btnNextPage" data-enabled="true" title="Next" alt="Next"><div class="nexticon"></div></div>');
                    htmlPager.push('<div class="button btnLastPage" data-enabled="true" title="Last" alt="Last"><div class="lasticon"></div></div>');
                }
                else {
                    htmlPager.push('<div class="button btnNextPage" disabled="disabled" data-enabled="false" title="Next" alt="Next"><div class="nexticon"></div></div>');
                    htmlPager.push('<div class="button btnLastPage" disabled="disabled" data-enabled="false" title="Last" alt="Last"><div class="lasticon"></div></div>');
                }
                htmlPager.push('</div>');
            }
            var $pager = jQuery(htmlPager.join('\n'));
            $pager.find('select.pagesize').val(pageSize);
            $dropdown.append($pager);
            $pager.find('.btnFirstPage')
                .on('click', function () {
                var pageno, $thisbtn;
                try {
                    $thisbtn = jQuery(this);
                    if ($thisbtn.attr('data-enabled') === 'true') {
                        $validationbrowse.attr('data-pageno', '1');
                        FwBrowse.databind($validationbrowse);
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            $pager.find('.btnPreviousPage')
                .on('click', function () {
                var pageno, $thisbtn;
                try {
                    $thisbtn = jQuery(this);
                    if ($thisbtn.attr('data-enabled') === 'true') {
                        pageno = parseInt($pager.find('.txtPageNo').val().toString()) - 1;
                        pageno = (pageno >= 1) ? pageno : 1;
                        $validationbrowse.attr('data-pageno', pageno.toString());
                        FwBrowse.databind($validationbrowse);
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            $pager.find('.txtPageNo')
                .on('change', function () {
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
                        }
                        else {
                            $pager.find('.txtTotalPages').val(pageno);
                        }
                    }
                    else {
                    }
                }
                catch (ex) {
                    $pager.find('.txtTotalPages').val(originalpagenoStr);
                    FwFunc.showError(ex);
                }
            });
            $pager.find('.btnNextPage')
                .on('click', function () {
                var pageno, totalPages, $thisbtn;
                try {
                    var $btnNextPage = jQuery(this);
                    if ($btnNextPage.attr('data-enabled') === 'true') {
                        pageno = parseInt($pager.find('.txtPageNo').val().toString()) + 1;
                        totalPages = parseInt($pager.find('.txtTotalPages').html());
                        pageno = (pageno <= totalPages) ? pageno : totalPages;
                        $validationbrowse.attr('data-pageno', pageno.toString());
                        FwBrowse.databind($validationbrowse);
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            $pager.find('.btnLastPage')
                .on('click', function () {
                var pageno, $thisbtn;
                try {
                    $thisbtn = jQuery(this);
                    if ($thisbtn.attr('data-enabled') === 'true') {
                        pageno = parseInt($pager.find('.txtTotalPages').html());
                        $validationbrowse.attr('data-pageno', pageno.toString());
                        FwBrowse.databind($validationbrowse);
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            if (FwFunc.isDesktop()) {
                me.highlightRow($control, me.getHighlightedRow($control));
            }
            $control.data('onclickdocument', function (e) {
                var $target = jQuery(e.target);
                if ($target.closest('.dropdownpager').length === 0) {
                    me.closeDropDown($control, false);
                }
                else {
                    jQuery(document).one('click', $control.data('onclickdocument'));
                }
            });
            jQuery(document).one('click', $control.data('onclickdocument'));
        }
        else {
            originalcolor = $valuefield.css('background-color');
            $valuefield.css('background-color', '#ffcccc').animate({ backgroundColor: originalcolor }, 1500, function () { $valuefield.attr('style', ''); });
        }
    };
    FwBrowseColumn_comboboxClass.prototype.closeDropDown = function ($control, showAnimation) {
        if ((typeof $control.data('dropdown') !== 'undefined') && ($control.data('dropdown') !== null)) {
            if (showAnimation) {
                $control.data('dropdown').slideUp({
                    duration: 250
                }, function () {
                    $control.data('dropdown').remove();
                });
            }
            else {
                $control.data('dropdown').remove();
            }
        }
        $control.data('dropdown', null);
    };
    FwBrowseColumn_comboboxClass.prototype.validate = function ($control, validationName, $valuefield, $btnvalidate, $validationbrowse, useSearchFieldValue) {
        var $validationSearchbox;
        this.clearSearchCriteria($validationbrowse);
        if (useSearchFieldValue) {
            $validationSearchbox = $validationbrowse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
            if ($validationSearchbox.length == 1) {
                $validationSearchbox.val($valuefield.val());
            }
            else {
                throw 'FwBrowseColumn_combobox: Validation is not setup correctly. Missing validation display field.';
            }
            if ($valuefield.val().length === 0) {
                this.closeDropDown($control, false);
            }
        }
        FwBrowse.search($validationbrowse);
    };
    ;
    FwBrowseColumn_comboboxClass.prototype.selectRow = function ($control, $tr, validationName, $valuefield, $btnvalidate, $validationbrowse, controller) {
        var uniqueid, text;
        this.highlightRow($control, $tr);
        uniqueid = $tr.attr('data-uniqueid');
        $control.find('.value').val(uniqueid);
        if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['loadRelatedValidationFields'] === 'function')) {
            window[controller]['loadRelatedValidationFields'](validationName, $valuefield, $tr);
        }
        this.clearSearchCriteria($validationbrowse);
        this.closeDropDown($control, true);
        var originalcolor = $valuefield.css('background-color');
        $valuefield.css('background-color', '#abcdef').animate({ backgroundColor: originalcolor }, 1500, function () { $valuefield.attr('style', ''); });
        $control.find('.value').change();
    };
    FwBrowseColumn_comboboxClass.prototype.clearSearchCriteria = function ($validationbrowse) {
        var $validationSearchboxes, $validationSearchbox;
        $validationSearchboxes = $validationbrowse.find('thead .field > .search > input');
        $validationSearchboxes.each(function (index, element) {
            $validationSearchbox = jQuery(element);
            $validationSearchbox.val('');
        });
    };
    FwBrowseColumn_comboboxClass.prototype.getHighlightedRow = function ($control) {
        var $row = null;
        if ($control.data('dropdown') !== null) {
            $row = $control.data('dropdown').find('.row.selected');
            if (($row.length === 0) && ($control.data('dropdown').find('.row').length > 0)) {
                $row = $control.data('dropdown').find('.row').eq(0);
                this.highlightRow($control, $row);
            }
        }
        return $row;
    };
    FwBrowseColumn_comboboxClass.prototype.highlightRow = function ($control, $row) {
        if (($control.data('dropdown') !== null) && (typeof $row !== 'undefined') && ($row.length > 0)) {
            $control.data('dropdown').find('.row').removeClass('selected');
            $row.addClass('selected');
        }
    };
    FwBrowseColumn_comboboxClass.prototype.highlightNextRow = function ($control) {
        var $row = this.getHighlightedRow($control);
        if ($row !== null) {
            var $nextrow = $row.next('.row');
            if ($nextrow.length > 0) {
                this.highlightRow($control, $nextrow);
            }
        }
    };
    ;
    FwBrowseColumn_comboboxClass.prototype.highlightPrevRow = function ($control) {
        var $row = this.getHighlightedRow($control);
        if ($row !== null) {
            var $prevrow = $row.prev('.row');
            if ($prevrow.length > 0) {
                this.highlightRow($control, $prevrow);
            }
        }
    };
    ;
    return FwBrowseColumn_comboboxClass;
}());
var FwBrowseColumn_combobox = new FwBrowseColumn_comboboxClass();
//# sourceMappingURL=FwBrowseColumn_combobox.js.map