class FwFormField_comboboxClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<i class="material-icons md-dark btnvalidate">&#xE5CF;</i>'); //expand_more
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery, html: string[]): void {
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
        html.push('<i class="material-icons md-dark btnvalidate">&#xE5CF;</i>'); //expand_more
        html.push('</div>');
        $control.html(html.join(''));
        $control.data('dropdown', null);
        $control.data('searchtimeout', null);
        FwFormField_combobox.initControl($control);
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('input.fwformfield-value')
            .val(value);
        $fwformfield.find('.fwformfield-text')
            .val(text);
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery): void {
        $control.find('.btnvalidate').attr('data-enabled', 'false');
        $control.find('.fwformfield-text').prop('disabled', true);
    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery): void {
        $control.find('.btnvalidate').attr('data-enabled', 'true');
        $control.find('.fwformfield-text').prop('disabled', false);
    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery): any {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    getText2($fwformfield: JQuery): string {
        var text = <string>$fwformfield.find('.fwformfield-text').val();
        return text;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        var $inputtext = $fwformfield.find('.fwformfield-text');
        $inputtext.val(text);
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
    initControl($control: JQuery): void {
        var $validationbrowse, $popup, $object, controller, formbeforevalidate, control_boundfields, boundfields, validationName, $valuefield, $searchfield, $btnvalidate;
        validationName = (typeof $control.attr('data-validationname') != 'undefined') ? $control.attr('data-validationname') : $control.attr('data-formvalidationname');
        $valuefield = $control.find('.fwformfield-value');
        $searchfield = $control.find('.fwformfield-text');
        $btnvalidate = $control.find('.btnvalidate');
        $validationbrowse = jQuery(jQuery('#tmpl-validations-' + validationName + 'Browse').html());
        $object = ($control.closest('.fwbrowse[data-controller!=""]').length > 0) ? $control.closest('.fwbrowse[data-controller!=""]') : $control.closest('.fwform[data-controller!=""]');
        if ($control.attr('data-pageno') === undefined) {
            $control.attr('data-pageno', '1');
        }
        if ($control.attr('data-pagesize') != null) {
            $control.attr('data-pagesize', '15');
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

        controller = FwFormField.getController($control);
        formbeforevalidate = $control.attr('data-formbeforevalidate');
        control_boundfields = $control.attr('data-boundfields');
        if (typeof control_boundfields != 'undefined') {
            boundfields = control_boundfields.split(',');
        }
        FwBrowse.init($validationbrowse);
        $validationbrowse.attr('data-name', validationName);

        // setup the request for the databind method
        $validationbrowse.data('ondatabind', function (request) {
            request.module = validationName;
            if ((typeof boundfields != 'undefined') && (boundfields.length > 0)) {
                request.boundids = {};
                for (var i = 0; i < boundfields.length; i++) {
                    if ($object.length === 0) throw 'combobox needs to be in a fwform to use boundfields';
                    request.boundids[boundfields[i].split('.').pop()] = FwFormField.getValue2($object.find('.fwformfield[data-datafield="' + boundfields[i] + '"]'));
                }
            }
            if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller][formbeforevalidate] === 'function')) {
                if ($object.attr('data-type') === 'Grid') {
                    (<any>controller[formbeforevalidate])($validationbrowse, $object.closest('.fwform'), request, $control.attr('data-datafield'));
                } else {
                    (<any>controller[formbeforevalidate])($validationbrowse, $object, request, $control.attr('data-datafield'));
                }
            }
        });

        // overrides the databind method in FwBrowse
        $validationbrowse.data('calldatabind', async (request: any, callback: (response: any) => void): Promise<any> => {
            return new Promise<any>(async (resolve, reject) => {
                let validationmode = $control.data('validationmode');
                if (validationmode !== undefined && validationmode === 2) {
                    try {
                        let getManyRequest = FwBrowse.getManyRequest($validationbrowse);
                        //getManyRequest.pageno = (!isNaN(parseInt($control.attr('data-pageno')))) ? parseInt($control.attr('data-pageno')) : getManyRequest.pageno;
                        //getManyRequest.pagesize = (!isNaN(parseInt($control.attr('data-pagesize')))) ? parseInt($control.attr('data-pagesize')) : getManyRequest.pagesize;
                        //$control.attr('data-pageno', getManyRequest.pageno);
                        //$control.attr('data-pagesize', getManyRequest.pagesize);


                        let url = Array<string>();
                        url.push(`${applicationConfig.apiurl}${$control.attr('data-apiurl')}?`);
                        url.push(`pageno=${getManyRequest.pageno}`);
                        url.push(`&pagesize=${getManyRequest.pagesize}`);
                        if (getManyRequest.sort.length > 0) {
                            url.push(`&sort=${getManyRequest.sort}`);
                        }
                        for (let filterno = 0; filterno < getManyRequest.filters.length; filterno++) {
                            let filter = getManyRequest.filters[filterno];
                            url.push(`&${filter.fieldName}=${filter.comparisonOperator}:${filter.fieldValue}`);
                        }
                        if (typeof control_boundfields != 'undefined') {
                            for (let i = 0; i < boundfields.length; i++) {
                                url.push(`&${boundfields[i]}=${FwFormField.getValueByDataField($object, boundfields[i])}`);
                            }
                        }
                        let request = new FwAjaxRequest();
                        request.httpMethod = 'GET';
                        request.url = encodeURI(url.join(''));
                        request.timeout = 15000;
                        request.$elementToBlock = $control.data('$control');
                        request.addAuthorizationHeader = true;
                        let getManyResponse = await FwAjax.callWebApi<any, GetManyModel<any>>(request);
                        let dt = DataTable.objectListToDataTable(getManyResponse);
                        FwFormField_combobox.databindcallback($control, $validationbrowse, dt, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                        callback(dt);
                        resolve();
                    } catch (ex) {
                        reject(ex);
                    }
                }
                else {
                    FwServices.validation.method(request, request.module, 'Browse', $control, function (response) {
                        try {
                            if (typeof window[$validationbrowse.attr('data-name') + 'Controller'].apiurl !== 'undefined') {
                                FwFormField_combobox.databindcallback($control, $validationbrowse, response, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                            } else {
                                FwFormField_combobox.databindcallback($control, $validationbrowse, response.browse, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
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
                    // need to clear out any boundfields here
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            //$control.on('change', '.fwformfield-text', function() {
            //    try {
            //        if ($searchfield.val().length === 0) {
            //            $valuefield.val('').change();
            //            FwFormField_combobox.closeDropDown($control);
            //        } else {
            //            FwFormField_combobox.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, true);
            //        }
            //    } catch (ex) {
            //        FwFunc.showError(ex);
            //    }
            //});
        }
        $control
            .on('keydown', '.fwformfield-text', (e) => {
                var $row, search, usesearchfield;
                try {
                    //console.log(e.which);
                    search = true;
                    usesearchfield = true;
                    if (e.which === 27) { // Esc
                        if ($control.data('dropdown') !== null) {
                            $control.data('dropdown').remove();
                            $control.data('dropdown', null);
                        }
                        search = false;
                    }
                    if ($control.data('dropdown') !== null) {
                        if (e.which === 38) { // Up Arrow
                            FwFormField_combobox.highlightPrevRow($control);
                            e.preventDefault();
                            return;
                        }
                        if (e.which === 40) { // Down Arrow
                            FwFormField_combobox.highlightNextRow($control);
                            e.preventDefault();
                            return;
                        }
                        //if (e.which === 13) { //Enter Key
                        //    $row = FwFormField_combobox.getHighlightedRow($control);
                        //    FwFormField_combobox.selectRow($control, $row, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                        //    return;
                        //}
                    } else {
                        if (e.which === 40) { // Down Arrow
                            usesearchfield = false;
                        }
                    }
                    switch (e.which) {
                        //case 8:
                        //case 46:
                        //    if ((<string>$control.find('.fwformfield-text').val()).length <= 0) {
                        //        search = false;
                        //        FwFormField_combobox.closeDropDown($control, true);
                        //        return;
                        //    }
                        //    break;
                        case 9: // Tab
                            search = false;
                            FwFormField_combobox.closeDropDown($control, true);
                            break;
                        //case 12: // numpad(5)
                        //case 13: // Enter
                        //    search = false;
                        //    //$control.find('.fwformfield-text').change();
                        //    e.preventDefault();
                        //    return;
                        //    break;
                        case 12: // numpad(5)
                        case 13: // Enter
                        case 8:  // backspace
                        case 46: // delete
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
                    if (search) {
                        this.clearSearchTimeout($control);
                        $control.data('searchtimeout', window.setTimeout(async () => {
                            await this.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, usesearchfield);
                        }, 500));
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('keyup', '.fwformfield-text', (e) => {
                try {
                    console.log(e.which, $control.find('.fwformfield-text').val());
                    switch (e.which) {
                        case 8:
                        case 46:
                            if ((<string>$control.find('.fwformfield-text').val()).length <= 0) {
                                FwFormField_combobox.closeDropDown($control, true);
                            }
                            break;
                        case 12: // numpad(5)
                        case 13: // Enter
                            if ($control.data('dropdown') !== null) {
                                const $row = FwFormField_combobox.getHighlightedRow($control);
                                FwFormField_combobox.selectRow($control, $row, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
                            }
                            else {
                                $control.find('.fwformfield-text').change();
                            }
                            e.preventDefault();
                            break;
                            
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('change', '.fwformfield-text', async () => {
                try {
                    if ($searchfield.val().length === 0) {
                        $valuefield.val('').change();
                        FwFormField_combobox.closeDropDown($control, true);
                    } else {
                        await FwFormField_combobox.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, true);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnvalidate', async () => {
                try {
                    if ($control.data('dropdown') != null) {
                        FwFormField_combobox.closeDropDown($control, true);
                    }
                    else if ((typeof $control.attr('data-enabled') !== 'string') || ($control.attr('data-enabled') !== 'false')) {
                        await FwFormField_combobox.validate($control, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, false);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            ;
    }
    //---------------------------------------------------------------------------------
    databindcallback($control: JQuery, $browse: JQuery, dt: DataTable, validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $validationbrowse: JQuery, controller: string) {
        var html = [], $dropdown, controlOffset, originalcolor;
        var pageSize = parseInt($validationbrowse.attr('data-pagesize'));
        var htmlPager = [];
        var rownostart = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? ((dt.PageNo * pageSize) - pageSize + 1) : 0;
        var rownoend = (((dt.PageNo * pageSize) - pageSize + 1) > 0) ? (dt.PageNo * pageSize) - (pageSize - dt.Rows.length) : 0;
        let validationmode: number = 1;
        validationmode = ($control.attr('data-validationmode') !== undefined && !isNaN(parseInt($control.attr('data-validationmode')))) ? parseInt($control.attr('data-validationmode')) : validationmode;

        // only focus the searchfield on desktop browsers so the user can use keydown handler on the field to arrow up and down through the dropdown
        if (typeof (<any>window).cordova === 'undefined') {
            $searchfield.focus();
        }

        if (dt.Rows.length > 0) {
            var uniqueid, displayfield;
            for (var i = 0; i < dt.Columns.length; i++) {
                if (validationmode === 2) {
                    var $col = $validationbrowse.find(`[data-browsedatafield="${dt.Columns[i].DataField}"]`);
                    if ($col.length > 0) {
                        const isUniqueId = $col.attr('data-isuniqueid');
                        if (isUniqueId !== undefined && isUniqueId.toLowerCase() === 'true') {
                            uniqueid = dt.Columns[i].DataField;
                            break;
                        }
                    }
                } else { // validationmode === 1
                    if (dt.Columns[i].IsUniqueId) {
                        uniqueid = dt.Columns[i].DataField;
                        break;
                    }
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
            if (dt.TotalPages > 1) {
                if (FwFunc.isDesktop()) {
                    $dropdown.find('.rows').css('min-height', pageSize * 22);
                } else if (FwFunc.isMobile()) {
                    $dropdown.find('.rows').css('min-height', (pageSize * 32) + 10);
                }
            }
            //$dropdown.on('mouseover', '.row', function () {
            //    var $row = jQuery(this);
            //    FwFormField_combobox.highlightRow($control, $row);
            //});
            $dropdown.on('click', '.row', function () {
                var $row = jQuery(this);
                FwFormField_combobox.selectRow($control, $row, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller);
            });
            controlOffset = $control.offset();
            if ($control.data('dropdown') != null) {
                FwFormField_combobox.closeDropDown($control, false);
                $control.append($dropdown);
            } else {
                $dropdown.hide();
                $control.append($dropdown);
                $dropdown.slideDown(200);
            }
            if (FwFunc.isDesktop()) {
                $dropdown.offset({
                    top: controlOffset.top + 55,
                    left: controlOffset.left + 5
                });
            } else if (FwFunc.isMobile()) {
                $dropdown.offset({
                    top: controlOffset.top + 64,
                    left: controlOffset.left + 5
                });
            }
            //$dropdown.width($control.width() - 34);
            $dropdown.width($control.width() - 2);
            $control.data('dropdown', $dropdown);

            if (dt.TotalPages > 1) {
                htmlPager.push('<div class="pager dropdownpager">');
                if ((pageSize > 0) && (dt.PageNo > 1)) {
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnFirstPage" data-enabled="true">&#xE5DC;</i>'); //first_page
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnPreviousPage" data-enabled="true">&#xE5CB;</i>'); //chevron_left
                } else {
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnFirstPage" disabled="disabled" data-enabled="false">&#xE5DC;</i>'); //first_page
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnPreviousPage" disabled="disabled" data-enabled="false">&#xE5CB;</i>'); //chevron_left
                }
                htmlPager.push('<input class="txtPageNo" type="text" value="' + dt.PageNo + '"/>');
                htmlPager.push('<span class="of"> of </span>');
                htmlPager.push('<span class="txtTotalPages">' + dt.TotalPages + '</span>');
                if ((pageSize > 0) && (dt.TotalPages > 1) && (dt.PageNo < dt.TotalPages)) {
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnNextPage" data-enabled="true">&#xE5CC;</i>'); //chevron_right
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnLastPage" data-enabled="true">&#xE5DD;</i>'); //last_page
                } else {
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnNextPage" disabled="disabled" data-enabled="false">&#xE5CC;</i>'); //chevron_right
                    htmlPager.push('<i class="material-icons md-dark pagerbutton btnLastPage" disabled="disabled" data-enabled="false">&#xE5DD;</i>'); //last_page
                }
                htmlPager.push('</div>');
            }
            var $pager = jQuery(htmlPager.join('\n'));
            $pager.find('select.pagesize').val(pageSize);
            $dropdown.append($pager);

            $pager.find('.btnFirstPage')
                .on('click', async (e: JQuery.ClickEvent) => {
                    var pageno, $thisbtn;
                    try {
                        $thisbtn = jQuery(e.target);
                        if ($thisbtn.attr('data-enabled') === 'true') {
                            $validationbrowse.attr('data-pageno', '1');
                            try {
                                this.toggleWaitingIndicator($control, true);
                                await FwBrowse.databind($validationbrowse);
                            } finally {
                                this.toggleWaitingIndicator($control, false);
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                ;
            $pager.find('.btnPreviousPage')
                .on('click', async (e: JQuery.ClickEvent) => {
                    var pageno, $thisbtn;
                    try {
                        $thisbtn = jQuery(e.target);
                        if ($thisbtn.attr('data-enabled') === 'true') {
                            pageno = parseInt(<string>$pager.find('.txtPageNo').val()) - 1;
                            pageno = (pageno >= 1) ? pageno : 1;
                            $validationbrowse.attr('data-pageno', pageno.toString());
                            try {
                                this.toggleWaitingIndicator($control, true);
                                await FwBrowse.databind($validationbrowse);
                            } finally {
                                this.toggleWaitingIndicator($control, false);
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                ;
            $pager.find('.txtPageNo')
                .on('change', async (e: JQuery.ChangeEvent) => {
                    var pageno, originalpageno, originalpagenoStr, $txtPageNo, totalPages;
                    try {
                        $txtPageNo = jQuery(e.target);
                        originalpagenoStr = $txtPageNo.val();
                        if (!isNaN(originalpagenoStr)) {
                            pageno = parseInt(originalpagenoStr);
                            originalpageno = pageno;
                            totalPages = parseInt($pager.find('.txtTotalPages').html());
                            pageno = (pageno >= 1) ? pageno : 1;
                            pageno = (pageno <= totalPages) ? pageno : totalPages;
                            if (pageno === originalpageno) {
                                $validationbrowse.attr('data-pageno', pageno.toString());
                                try {
                                    this.toggleWaitingIndicator($control, true);
                                    await FwBrowse.databind($validationbrowse);
                                } finally {
                                    this.toggleWaitingIndicator($control, false);
                                }
                            } else {
                                $pager.find('.txtTotalPages').val(pageno);
                            }
                        } else {

                        }
                    } catch (ex) {
                        $pager.find('.txtTotalPages').val(originalpagenoStr);
                        FwFunc.showError(ex);
                    }
                })
                ;
            $pager.find('.btnNextPage')
                .on('click', async (e: JQuery.ClickEvent) => {
                    //var pageno, totalPages, $thisbtn;
                    try {
                        let $btnNextPage = jQuery(e.target);
                        if ($btnNextPage.attr('data-enabled') === 'true') {
                            let pageno = parseInt(<string>$pager.find('.txtPageNo').val()) + 1;
                            let totalPages = parseInt($pager.find('.txtTotalPages').html());
                            pageno = (pageno <= totalPages) ? pageno : totalPages;
                            $validationbrowse.attr('data-pageno', pageno.toString());
                            try {
                                this.toggleWaitingIndicator($control, true);
                                await FwBrowse.databind($validationbrowse);
                            } finally {
                                this.toggleWaitingIndicator($control, false);
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                ;
            $pager.find('.btnLastPage')
                .on('click', async (e: JQuery.ClickEvent) => {
                    var pageno, $thisbtn;
                    try {
                        $thisbtn = jQuery(e.target);
                        if ($thisbtn.attr('data-enabled') === 'true') {
                            pageno = parseInt($pager.find('.txtTotalPages').html());
                            $validationbrowse.attr('data-pageno', pageno.toString());
                            try {
                                this.toggleWaitingIndicator($control, true);
                                await FwBrowse.databind($validationbrowse);
                            } finally {
                                this.toggleWaitingIndicator($control, false);
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                ;

            // highlight the first row
            if (FwFunc.isDesktop()) {
                FwFormField_combobox.highlightRow($control, FwFormField_combobox.getHighlightedRow($control));
            }

            // register a one time event to close the dropdown when the users clicks on the document
            $control.data('onclickdocument', function (e: JQuery.ClickEvent) {
                var $target = jQuery(e.target);
                if ($target.closest('.dropdown').length === 0) {
                    FwFormField_combobox.closeDropDown($control, true);
                } else {
                    jQuery('body').one('click', $control.data('onclickdocument'));
                }
            });
            jQuery('body').one('click', $control.data('onclickdocument'));
        } else {
            originalcolor = $searchfield.css('background-color');
            //$searchfield.css('background-color', '#ffcccc').animate({backgroundColor: originalcolor}, 1500 );
            $searchfield.css('background-color', '#ffcccc').animate({ backgroundColor: originalcolor }, 1500, function () { $searchfield.attr('style', '') });
        }
    }
    //---------------------------------------------------------------------------------
    clearSearchTimeout($control: JQuery) {
        if ($control.data('searchtimeout') != null) {
            window.clearTimeout($control.data('searchtimeout'));
        }
        $control.data('searchtimeout', null);

    }
    //---------------------------------------------------------------------------------
    closeDropDown($control: JQuery, showAnimation: boolean) {
        this.clearSearchTimeout($control);
        if (($control.data('dropdown') != null)) {
            if (showAnimation) {
                $control.data('dropdown').slideUp({
                    duration: 250
                }, function () {
                    $control.data('dropdown').remove();
                });
            } else {
                $control.data('dropdown').remove();
            }
        }
        $control.data('dropdown', null);
        $control.find('.dropdown').remove();
    }
    //---------------------------------------------------------------------------------
    toggleWaitingIndicator($control: JQuery, isVisible: boolean) {
        if (isVisible) {
            $control.find('.fwformfield-control i.material-icons').html('hourglass_empty');
        } else {
            $control.find('.fwformfield-control i.material-icons').html('&#xE5CF;');
        }
    }
    //---------------------------------------------------------------------------------
    async validate($control: JQuery, validationName: string, $valuefield: JQuery, $searchfield: JQuery, $btnvalidate: JQuery, $validationbrowse: JQuery, useSearchFieldValue: boolean) {
        var $validationSearchbox;

        FwFormField_combobox.clearSearchCriteria($validationbrowse);
        if (useSearchFieldValue) {
            $validationSearchbox = $validationbrowse.find('thead .field[data-validationdisplayfield="true"] > .search > input');
            if ($validationSearchbox.length == 1) {
                $validationSearchbox.val($searchfield.val());
            } else {
                throw 'FwFormField_combobox: Validation is not setup correctly. Missing validation display field.';
            }
            if ((<string>$searchfield.val()).length === 0) {
                FwFormField_combobox.closeDropDown($control, false);
            }
        }
        $validationbrowse.attr('data-pagesize', 10);
        try {
            this.toggleWaitingIndicator($control, true);
            await FwBrowse.search($validationbrowse);
        } finally {
            this.toggleWaitingIndicator($control, false);
        }
    }
    //---------------------------------------------------------------------------------
    selectRow($control, $row, validationName, $valuefield, $searchfield, $btnvalidate, $validationbrowse, controller) {
        FwFormField_combobox.highlightRow($control, $row);

        // set the uniqueid
        let uniqueid = $row.attr('data-uniqueid');
        // mv 2016-01-15 having problems with $valuefield and $seachfield not working when modifying val, so requerying from $control
        $control.find('.fwformfield-value').val(uniqueid);
        //set the text
        let text = $row.text();
        $control.find('.fwformfield-text').val(text);
        if ((typeof controller === 'string') && (typeof window[controller] !== 'undefined') && (typeof window[controller]['loadRelatedValidationFields'] === 'function')) {
            window[controller]['loadRelatedValidationFields'](validationName, $valuefield, $row);
        }
        FwFormField_combobox.clearSearchCriteria($validationbrowse);
        FwFormField_combobox.closeDropDown($control, true);
        let originalcolor = $searchfield.css('background-color');
        //$searchfield.css('background-color', '#abcdef').animate({backgroundColor: originalcolor}, 1500 );
        $searchfield.css('background-color', '#abcdef').animate({ backgroundColor: originalcolor }, 1500, function () { $searchfield.attr('style', '') });
        $control.find('.fwformfield-value').change();
    }
    //---------------------------------------------------------------------------------
    clearSearchCriteria($validationbrowse) {
        var $validationSearchboxes, $validationSearchbox;
        $validationSearchboxes = $validationbrowse.find('thead .field > .search > input');
        $validationSearchboxes.each(function (index, element) {
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
                FwFormField_combobox.highlightRow($control, $row);
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
        var $row = FwFormField_combobox.getHighlightedRow($control);
        if ($row !== null) {
            var $nextrow = $row.next('.row');
            if ($nextrow.length > 0) {
                FwFormField_combobox.highlightRow($control, $nextrow);
            }
        }
    }
    //---------------------------------------------------------------------------------
    highlightPrevRow($control) {
        var $row = FwFormField_combobox.getHighlightedRow($control);
        if ($row !== null) {
            var $prevrow = $row.prev('.row');
            if ($prevrow.length > 0) {
                FwFormField_combobox.highlightRow($control, $prevrow);
            } else {
                FwFormField_combobox.closeDropDown($control, true);
            }
        }
    }
}

var FwFormField_combobox = new FwFormField_comboboxClass();