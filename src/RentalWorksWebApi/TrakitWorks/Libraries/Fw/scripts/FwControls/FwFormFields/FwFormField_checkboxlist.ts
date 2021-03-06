﻿class FwFormField_checkboxlistClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery, html: string[]): void {
        if ($control.attr('data-caption')) {
            html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');;
        }
        html.push('<div class="fwformfield-control">');
        html.push('  <ol style="min-height:1200px;min-width:200px;">');
        html.push('  </ol>');
        html.push('</div>');
        $control.html(html.join(''));
        const options: any = {};
        if ($control.attr('data-share') === 'true') {
            options.group = 'shared';
            options.onAdd = function (evt) {
                if (jQuery(evt.item).parent().children().length === 1) {
                    jQuery(evt.item).parent().parent().find('.empty-message').hide();
                }
                jQuery(evt.item).find('.checkbox').prop('checked', true).show();
            }
        }
        if ($control.attr('data-listtype') === 'cloneonly') {
            options.sort = false;
            options.group = {
                name: 'shared',
                pull: 'clone',
                revertClone: true
            }
            options.onAdd = function (evt) {
                this.el.removeChild(evt.item);
                const $form = $control.closest('.fwform');
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        }
        if ($control.attr('data-sortable') === 'true') {
            $control.find('ol').addClass('sortable');
            const ol = $control.find('ol');
            if (ol.length > 0) {
                const olElement = ol.get(0);
                Sortable.create(olElement, options);
            }
        }
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery, items: any, hideEmptyItem: boolean): void {
        const html = [];
        if ((typeof items !== 'undefined') && (items !== null)) {
            if ($control.attr('data-type') === 'orderby') {
                $control.attr('data-sortable', 'true');
                $control.attr('data-orderby', 'true');
            }
            for (let i = 0; i < items.length; i++) {
                const checkboxid = FwControl.generateControlId('cb' + i.toString());
                if (typeof items[i].selected !== 'string') items[i].selected = 'F';
                if (typeof items[i].orderbydirection !== 'string') items[i].orderbydirection = '';
                html.push('<li data-value="');
                html.push(items[i].value);
                html.push('" data-selected="');
                html.push(items[i].selected.toString());
                html.push('"');
                if ((typeof $control.attr('data-orderby') === 'string') && ($control.attr('data-orderby') === 'true')) {
                    html.push(' data-orderbydirection="');
                    html.push(items[i].orderbydirection);
                    html.push('"');
                }
                html.push('>');
                html.push('<div class="wrapper">');
                const hasorderby = ((typeof $control.attr('data-orderby') === 'string') && ($control.attr('data-orderby') === 'true'));
                if (hasorderby) {
                    html.push('<div class="handle">::</div>');
                }
                html.push('<input class="checkbox" type="checkbox" id="');
                html.push(checkboxid);
                html.push('"');
                if ($control.attr('data-showcheckboxes') === 'false') {
                    html.push(' style="display:none" ');
                }
                if (items[i].selected === 'T') {
                    html.push(' checked="checked"');
                }
                html.push('/>');
                html.push('<label for="');
                html.push(checkboxid);
                html.push('">');
                html.push(items[i].text);
                html.push('</label>');
                if (hasorderby) {
                    html.push('<div class="orderbydirection"></div>');
                }
                html.push('</div>');
                html.push('</li>');
            }
        }
        $control.find('ol').html(html.join(''));
        $control.find('ol .checkbox').on('change', function () {
            const $this = jQuery(this);
            const $li = $this.closest('li');;
            $li.attr('data-selected', $this.prop('checked') ? 'T' : 'F');
        });
        $control.find('ol .orderbydirection').on('click', function () {
            const $this = jQuery(this);
            const $li = $this.closest('li');
            switch ($li.attr('data-orderbydirection')) {
                case 'asc': $li.attr('data-orderbydirection', 'desc'); break;
                case 'desc': $li.attr('data-orderbydirection', 'asc'); break;
            }
        });
    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery, table: string, field: string, value: any, text, model: any) {
        const html = [];
        if ((typeof value !== 'undefined') && (value !== null)) {
            for (let i = 0; i < value.length; i++) {
                const checkboxid = FwControl.generateControlId('cb' + i.toString());
                if (value[i].selected) {
                    value[i].selected = 'T';
                } else {
                    value[i].selected = 'F';
                }
                if (typeof value[i].orderbydirection !== 'string') value[i].orderbydirection = '';
                html.push('<li data-value="');
                html.push(value[i].value);
                html.push('" data-selected="');
                if ($fwformfield.attr('data-listtype') === 'cloneonly') {
                    html.push('T')
                    html.push('" data-userwidgetid="');
                } else if ($fwformfield.attr('data-listtype') === 'standard') {
                    html.push(value[i].selected.toString());
                    html.push('" data-userwidgetid="');
                    html.push(value[i].userWidgetId);
                } else {
                    html.push(value[i].selected.toString());
                }
                html.push('">');
                html.push('<div class="wrapper">');
                html.push('<div class="handle">::</div>');
                html.push('<input class="checkbox" type="checkbox" id="');
                html.push(checkboxid);
                html.push('"');
                if ($fwformfield.attr('data-listtype') === 'cloneonly') {
                    html.push(' style="display:none" ');
                }
                if (value[i].selected === 'T') {
                    html.push(' checked="checked"');
                }
                html.push('/>');
                html.push('<label for="');
                html.push(checkboxid);
                html.push('">');
                html.push(value[i].text);
                html.push('</label>');
                if ($fwformfield.attr('data-listtype') === 'standard') {
                    html.push('<div class="settings"><i class="material-icons">settings</i></div>')
                }
                const hasorderby = ((typeof $fwformfield.attr('data-orderby') === 'string') && ($fwformfield.attr('data-orderby') === 'true'));
                if (hasorderby) {
                    html.push('<div class="orderbydirection"></div>');
                }
                html.push('</div>');
                html.push('</li>');
            }
        }
        $fwformfield.find('ol').html(html.join(''));

        if (value.length === 0) {
            $fwformfield.find('.fwformfield-control').append('<div class="empty-message" style="position:fixed;align-self: self-start;margin-top: 2em;">You have no widgets yet - Drag and drop them here to get started!</div>');
        }

        $fwformfield.find('ol .checkbox').on('change', function () {
            const $this = jQuery(this);
            const $li = $this.closest('li');
            $li.attr('data-selected', $this.prop('checked') ? 'T' : 'F');
        });
        $fwformfield.find('ol .orderbydirection').on('click', function () {
            const $this = jQuery(this);
            const $li = $this.closest('li');
            switch ($li.attr('data-orderbydirection')) {
                case 'asc': $li.attr('data-orderbydirection', 'desc'); break;
                case 'desc': $li.attr('data-orderbydirection', 'asc'); break;
            }
        });
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery): any {
        let value = [];
        if ($fwformfield.data('checkboxlist') === 'persist') {
            $fwformfield.find('li[data-selected="T"]').each(function (index, element) {
                const $li = jQuery(element);
                const item: any = {};
                item.value = $li.attr('data-value');
                item.text = $li.find('label').text();
                item.userWidgetId = $li.attr('data-userwidgetid');
                item.selected = 'true'
                jQuery.extend(item, $li.data('request'));
                value.push(item);
            });
            $fwformfield.find('li[data-selected="F"]').each(function (index, element) {
                const $li = jQuery(element);
                const item: any = {};
                item.value = $li.attr('data-value');
                item.text = $li.find('label').text();
                item.userWidgetId = $li.attr('data-userwidgetid');
                item.selected = 'false'
                value.push(item);
            });
        } else {
            $fwformfield.find('li[data-selected="T"]').each(function (index, element) {
                const $li = jQuery(element);
                const item: any = {};
                item.value = $li.attr('data-value');
                if (typeof $li.attr('data-orderbydirection') === 'string') {
                    item.orderbydirection = $li.attr('data-orderbydirection');
                }
                value.push(item);
            });
        }
        if ($fwformfield.data('returncsv') && $fwformfield.data('returncsv').toString() === 'true') {
            const values: string[] = [];
            value = value.map((item, index, array) => {
                values.push(item.value.toString());
            });
            return values.join(',');
        }

        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery, value: any, text: string, firechangeevent: boolean): void {
        const $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_checkboxlist = new FwFormField_checkboxlistClass();