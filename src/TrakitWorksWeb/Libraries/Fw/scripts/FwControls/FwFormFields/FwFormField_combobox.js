class FwFormField_comboboxClass {
    renderDesignerHtml($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<i class="material-icons md-dark btnvalidate">&#xE5CF;</i>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    renderRuntimeHtml($control, html) {
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<i class="material-icons md-dark btnvalidate">&#xE5CF;</i>');
        html.push('</div>');
        $control.html(html.join(''));
        $control.data('dropdown', null);
        $control.data('searchtimeout', null);
        FwFormField_combobox.initControl($control);
    }
    loadItems($control, items, hideEmptyItem) {
    }
    loadForm($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('input.fwformfield-value')
            .val(value);
        $fwformfield.find('.fwformfield-text')
            .val(text);
    }
    disable($control) {
        $control.find('.btnvalidate').attr('data-enabled', 'false');
        $control.find('.fwformfield-text').prop('disabled', true);
    }
    enable($control) {
        $control.find('.btnvalidate').attr('data-enabled', 'true');
        $control.find('.fwformfield-text').prop('disabled', false);
    }
    getValue2($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    getText2($fwformfield) {
        var text = $fwformfield.find('.fwformfield-text').val();
        return text;
    }
    setValue($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        var $inputtext = $fwformfield.find('.fwformfield-text');
        $inputtext.val(text);
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    }
    initControl($control) {
        var $validationbrowse, $popup, $object, controller, formbeforevalidate, control_boundfields, boundfields, validationName, $valuefield, $searchfield, $btnvalidate;
        validationName = (typeof $control.attr('data-validationname') != 'undefined') ? $control.attr('data-validationname') : $control.attr('data-formvalidationname');
        $valuefield = $control.find('.fwformfield-value');
        $searchfield = $control.find('.fwformfield-text');
        $btnvalidate = $control.find('.btnvalidate');
        $validationbrowse = jQuery(jQuery('#tmpl-validations-' + validationName + 'Browse').html());
        $object = ($control.closest('.fwbrowse[data-controller!=""]').length > 0) ? $control.closest('.fwbrowse[data-controller!=""]') : $control.closest('.fwform[data-controller!=""]');
        if (typeof $validationbrowse.attr('data-name') !== 'undefined' && typeof $validationbrowse.attr('data-apiurl') !== 'undefined') {
            if (typeof window[$validationbrowse.attr('data-name') + 'Controller'] === 'undefined') {
                window[$validationbrowse.attr('data-name') + 'Controller'] = {
                    Module: $validationbrowse.attr('data-name'),
                    apiurl: $validationbrowse.attr('data-apiurl')
                };
            }
            else {
                var controllerObj = window[$validationbrowse.attr('data-name') + 'Controller'];
                if (typeof controllerObj.Module === 'undefined') {
                    controllerObj.Module = $validationbrowse.attr('data-name');
                }
                if (typeof controllerObj.apiurl === 'undefined') {
                    controllerObj.apiurl = $validationbrowse.attr('data-apiurl');
                }
            }
        }
        else if ((typeof $validationbrowse.attr('data-name') !== 'undefined') && (typeof window[$validationbrowse.attr('data-name') + 'Controller'] === 'undefined')) {
            window[$validationbrowse.attr('data-name') + 'Controller'] = {
                Module: $validationbrowse.attr('data-name')
            };
        }
        controller = FwFormField.getController($control);
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
                    if ($object.length === 0)
                        throw 'combobox needs to be in a fwform to use boundfields';
                    request.boundids[boundfields[i].split('.').pop()] = FwFormField.getValue2($object.find('.fwformfield[data-datafield="' + boundfields[i] + '"]'));
                }
            }
            if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller][formbeforevalidate] === 'function')) {
                if ($object.attr('data-type') === 'Grid') {
                    controller[formbeforevalidate]($validationbrowse, $object.closest('.fwform'), request, $control.attr('data-datafield'));
                }
                else {
                    controller[formbeforevalidate]($validationbrowse, $object, request, $control.attr('data-datafield'));
                }
            }
        });
        $validationbrowse.data('calldatabind', function (request) {
            FwServices.validation.method(request, request.module, 'Browse', $control, function (response) {
                try {
                    if (typeof window[$validationbrowse.attr('data-name') + 'Controller'].apiurl !== 'undefined') {
                        FwFormField_combobox.databindcallback($control, $validationbrowse, response, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                    }
                    else {
                        FwFormField_combobox.databindcallback($control, $validationbrowse, response.browse, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        });
        FwBrowse.renderRuntimeHtml($validationbrowse);
        if (typeof $control.data('oninit') === 'function') {
            $control.data('oninit')($control, $validationbrowse, $popup, $valuefield, $searchfield, $btnvalidate);
        }
        if ($control.attr('data-validate') === 'true') {
            $control.on('change', '.fwformfield-value', function () {
                try {
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        $control
            .on('keydown', '.fwformfield-text', function (e) {
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
                        FwFormField_combobox.highlightPrevRow($control);
                        e.preventDefault();
                        return;
                    }
                    if (e.which === 40) {
                        FwFormField_combobox.highlightNextRow($control);
                        e.preventDefault();
                        return;
                    }
                    if (e.which === 13) {
                        $row = FwFormField_combobox.getHighlightedRow($control);
                        FwFormField_combobox.selectRow($control, $row, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
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
                        if ($control.find('.fwformfield-text').val().length <= 0) {
                            search = false;
                            FwFormField_combobox.closeDropDown($control, true);
                        }
                        break;
                    case 9:
                        search = false;
                        FwFormField_combobox.closeDropDown($control, true);
                        break;
                    case 12:
                    case 13:
                        search = false;
                        $control.find('.fwformfield-text').change();
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
                if (search) {
                    if ($control.data('searchtimeout') !== null) {
                        window.clearTimeout($control.data('searchtimeout'));
                        $control.data('searchtimeout', null);
                    }
                    $control.data('searchtimeout', window.setTimeout(function () {
                        FwFormField_combobox.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, usesearchfield);
                    }, 250));
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('change', '.fwformfield-text', function () {
            try {
                if ($searchfield.val().length === 0) {
                    $valuefield.val('').change();
                    FwFormField_combobox.closeDropDown($control, true);
                }
                else {
                    FwFormField_combobox.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, true);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.btnvalidate', function () {
            try {
                if ($control.data('dropdown') !== null) {
                    FwFormField_combobox.closeDropDown($control, true);
                }
                else if ((typeof $control.attr('data-enabled') !== 'string') || ($control.attr('data-enabled') !== 'false')) {
                    FwFormField_combobox.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, false);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    databindcallback($control, $browse, dt, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller) {
        var html = [], $dropdown, controlOffset, originalcolor;
        var pageSize = parseInt($validationbrowse.attr('data-pagesize'));
        var htmlPager = [];
        var rownostart = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? ((dt.PageNo * pageSize) - pageSize + 1) : 0;
        var rownoend = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? (dt.PageNo * pageSize) - (pageSize - dt.Rows.length) : 0;
        $searchfield.focus();
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
            if (dt.TotalPages > 1) {
                if (FwFunc.isDesktop()) {
                    $dropdown.find('.rows').css('min-height', pageSize * 22);
                }
                else if (FwFunc.isMobile()) {
                    $dropdown.find('.rows').css('min-height', (pageSize * 32) + 10);
                }
            }
            $dropdown.on('mouseover', '.row', function () {
                var $row = jQuery(this);
                FwFormField_combobox.highlightRow($control, $row);
            });
            $dropdown.on('click', '.row', function () {
                var $row = jQuery(this);
                FwFormField_combobox.selectRow($control, $row, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
            });
            controlOffset = $control.offset();
            if ($control.data('dropdown') !== null) {
                FwFormField_combobox.closeDropDown($control, false);
                $control.append($dropdown);
            }
            else {
                $dropdown.hide();
                $control.append($dropdown);
                $dropdown.slideDown(200);
            }
            if (FwFunc.isDesktop()) {
                $dropdown.offset({
                    top: controlOffset.top + 55,
                    left: controlOffset.left + 5
                });
            }
            else if (FwFunc.isMobile()) {
                $dropdown.offset({
                    top: controlOffset.top + 64,
                    left: controlOffset.left + 5
                });
            }
            $dropdown.width($control.width() - 2);
            $control.data('dropdown', $dropdown);
            if (dt.TotalPages > 1) {
                htmlPager.push('<div class="pager dropdownpager">');
                if ((pageSize > 0) && (dt.PageNo > 1)) {
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnFirstPage" data-enabled="true">&#xE5DC;</i>');
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnPreviousPage" data-enabled="true">&#xE5CB;</i>');
                }
                else {
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnFirstPage" disabled="disabled" data-enabled="false">&#xE5DC;</i>');
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnPreviousPage" disabled="disabled" data-enabled="false">&#xE5CB;</i>');
                }
                htmlPager.push('<input class="txtPageNo" type="text" value="' + dt.PageNo + '"/>');
                htmlPager.push('<span class="of"> of </span>');
                htmlPager.push('<span class="txtTotalPages">' + dt.TotalPages + '</span>');
                if ((pageSize > 0) && (dt.TotalPages > 1) && (dt.PageNo < dt.TotalPages)) {
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnNextPage" data-enabled="true">&#xE5CC;</i>');
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnLastPage" data-enabled="true">&#xE5DD;</i>');
                }
                else {
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnNextPage" disabled="disabled" data-enabled="false">&#xE5CC;</i>');
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnLastPage" disabled="disabled" data-enabled="false">&#xE5DD;</i>');
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
                        pageno = parseInt($pager.find('.txtPageNo').val()) - 1;
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
                try {
                    let $btnNextPage = jQuery(this);
                    if ($btnNextPage.attr('data-enabled') === 'true') {
                        let pageno = parseInt($pager.find('.txtPageNo').val()) + 1;
                        let totalPages = parseInt($pager.find('.txtTotalPages').html());
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
                FwFormField_combobox.highlightRow($control, FwFormField_combobox.getHighlightedRow($control));
            }
            $control.data('onclickdocument', function (e) {
                var $target = jQuery(e.target);
                if ($target.closest('.dropdown').length === 0) {
                    FwFormField_combobox.closeDropDown($control, true);
                }
                else {
                    jQuery('body').one('click', $control.data('onclickdocument'));
                }
            });
            jQuery('body').one('click', $control.data('onclickdocument'));
        }
        else {
            originalcolor = $searchfield.css('background-color');
            $searchfield.css('background-color', '#ffcccc').animate({ backgroundColor: originalcolor }, 1500, function () { $searchfield.attr('style', ''); });
        }
    }
    closeDropDown($control, showAnimation) {
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
        $control.find('.dropdown').remove();
    }
    validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, useSearchFieldValue) {
        var $validationSearchbox;
        FwFormField_combobox.clearSearchCriteria($validationbrowse);
        if (useSearchFieldValue) {
            $validationSearchbox = $validationbrowse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
            if ($validationSearchbox.length == 1) {
                $validationSearchbox.val($searchfield.val());
            }
            else {
                throw 'FwFormField_combobox: Validation is not setup correctly. Missing validation display field.';
            }
            if ($searchfield.val().length === 0) {
                FwFormField_combobox.closeDropDown($control, false);
            }
        }
        $validationbrowse.attr('data-pagesize', 10);
        FwBrowse.search($validationbrowse);
    }
    selectRow($control, $row, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller) {
        FwFormField_combobox.highlightRow($control, $row);
        let uniqueid = $row.attr('data-uniqueid');
        $control.find('.fwformfield-value').val(uniqueid);
        let text = $row.text();
        $control.find('.fwformfield-text').val(text);
        if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['loadRelatedValidationFields'] === 'function')) {
            window[controller]['loadRelatedValidationFields'](validationName, $valuefield, $row);
        }
        FwFormField_combobox.clearSearchCriteria($validationbrowse);
        FwFormField_combobox.closeDropDown($control, true);
        let originalcolor = $searchfield.css('background-color');
        $searchfield.css('background-color', '#abcdef').animate({ backgroundColor: originalcolor }, 1500, function () { $searchfield.attr('style', ''); });
        $control.find('.fwformfield-value').change();
    }
    clearSearchCriteria($validationbrowse) {
        var $validationSearchboxes, $validationSearchbox;
        $validationSearchboxes = $validationbrowse.find('thead .field > .search > input');
        $validationSearchboxes.each(function (index, element) {
            $validationSearchbox = jQuery(element);
            $validationSearchbox.val('');
        });
    }
    getHighlightedRow($control) {
        var $row = null;
        if ($control.data('dropdown') !== null) {
            $row = $control.data('dropdown').find('.row.selected');
            if (($row.length === 0) && ($control.data('dropdown').find('.row').length > 0)) {
                $row = $control.data('dropdown').find('.row').eq(0);
                FwFormField_combobox.highlightRow($control, $row);
            }
        }
        return $row;
    }
    highlightRow($control, $row) {
        if (($control.data('dropdown') !== null) && (typeof $row !== 'undefined') && ($row.length > 0)) {
            $control.data('dropdown').find('.row').removeClass('selected');
            $row.addClass('selected');
        }
    }
    highlightNextRow($control) {
        var $row = FwFormField_combobox.getHighlightedRow($control);
        if ($row !== null) {
            var $nextrow = $row.next('.row');
            if ($nextrow.length > 0) {
                FwFormField_combobox.highlightRow($control, $nextrow);
            }
        }
    }
    highlightPrevRow($control) {
        var $row = FwFormField_combobox.getHighlightedRow($control);
        if ($row !== null) {
            var $prevrow = $row.prev('.row');
            if ($prevrow.length > 0) {
                FwFormField_combobox.highlightRow($control, $prevrow);
            }
        }
    }
}
var FwFormField_combobox = new FwFormField_comboboxClass();
//# sourceMappingURL=FwFormField_combobox.js.map