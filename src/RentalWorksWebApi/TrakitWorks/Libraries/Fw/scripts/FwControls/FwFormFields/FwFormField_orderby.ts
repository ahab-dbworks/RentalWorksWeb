class FwFormField_orderbyClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {

    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery, html: string[]): void {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');;
        html.push('<div class="fwformfield-control">');
        html.push('  <ol>');
        html.push('  </ol>');
        html.push('</div>');
        $control.html(html.join(''));
        if ($control.attr('data-sortable') === 'true') {
            $control.find('ol').addClass('sortable');
            var ol = $control.find('ol');
            if (ol.length > 0) {
                let olElement = ol.get(0);
                Sortable.create(olElement);
            }
        }
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery, items: any, hideEmptyItem: boolean): void {
        var html, hasorderby, checkboxid;

        html = [];
        if ((typeof items !== 'undefined') && (items !== null)) {
            if ($control.attr('data-type') === 'orderby') {
                $control.attr('data-sortable', 'true');
                $control.attr('data-orderby', 'true');
            }
            hasorderby = ((typeof $control.attr('data-orderby') === 'string') && ($control.attr('data-orderby') === 'true'));
            for (var i = 0; i < items.length; i++) {
                checkboxid = FwControl.generateControlId('cb' + i.toString());
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
                if (hasorderby) {
                    html.push('<div class="handle">::</div>');
                }
                html.push('<input class="checkbox" type="checkbox" id="');
                html.push(checkboxid);
                html.push('"');
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
            var $this, $li;
            $this = jQuery(this);
            $li = $this.closest('li');
            $li.attr('data-selected', $this.prop('checked') ? 'T' : 'F');
        });
        $control.find('ol .orderbydirection').on('click', function () {
            var $this, $li;
            $this = jQuery(this);
            $li = $this.closest('li');
            switch ($li.attr('data-orderbydirection')) {
                case 'asc': $li.attr('data-orderbydirection', 'desc'); break;
                case 'desc': $li.attr('data-orderbydirection', 'asc'); break;
            }
        });
    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery, table: string, field: string, value: any, text: string, model: any): void {

    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery): any {
        var value = [];
        $fwformfield.find('li[data-selected="T"]').each(function (index, element) {
            var $li, item;
            $li = jQuery(element);
            item = {};
            item.value = $li.attr('data-value');
            if (typeof $li.attr('data-orderbydirection') === 'string') {
                item.orderbydirection = $li.attr('data-orderbydirection');
            }
            value.push(item);
        });
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_orderby = new FwFormField_orderbyClass();