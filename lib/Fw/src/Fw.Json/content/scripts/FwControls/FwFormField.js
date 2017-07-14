//---------------------------------------------------------------------------------
var FwFormField = {};
//---------------------------------------------------------------------------------
FwFormField.init = function($control) {
    if (typeof $control.attr('data-isuniqueid') === 'undefined') {
        $control.attr('data-isuniqueid', 'false');
    }
    if (typeof $control.attr('data-originalvalue') === 'undefined') {
        $control.attr('data-originalvalue', '');
    }
    if (typeof $control.attr('data-enabled') === 'undefined') {
        $control.attr('data-enabled', 'true');
    }
    if (typeof $control.attr('data-required') === 'undefined') {
        $control.attr('data-required', 'false');
    }
    if (typeof $control.attr('data-version') === 'undefined') {
        $control.attr('data-version', '1');
    }
    if (typeof $control.attr('data-rendermode') === 'undefined') {
        $control.attr('data-rendermode', 'template');
    }
    if (typeof $control.attr('data-noduplicate') === 'undefined') {
        $control.attr('data-noduplicate', 'false');
    }
    if ($control.attr('data-noduplicate') == 'true') {
        $control.attr('data-required', 'true');
    }
};
//---------------------------------------------------------------------------------
FwFormField.getDroppableRegions = function(data_type) {
    var selector;

    if ((typeof window['FwFormField_' + data_type] === 'object') &&
        (typeof window['FwFormField_' + data_type].getDroppableRegions === 'function')) {
        selector = window['FwFormField_' + data_type].getDroppableRegions();
    }
    else {
        selector = '.fwform-fieldrow > .children';
    }

    return selector;
};
//---------------------------------------------------------------------------------
FwFormField.getHtmlTag = function(data_type) {
    var template, html, properties, i;

    if ((typeof window['FwFormField_' + data_type] === 'object') && 
        (typeof window['FwFormField_' + data_type].getHtmlTag === 'function')) {
         html = window['FwFormField_' + data_type].getHtmlTag();
    }
    else {
        template = [];
        template.push('<div');
        properties = this.getDesignerProperties(data_type);
        for (i = 0; i < properties.length; i++) {
            template.push(' ' + properties[i].attribute + '="' + properties[i].defaultvalue + '"');
        }
        template.push('></div>');
        html = template.join('');
    }

    return html;
};
//---------------------------------------------------------------------------------
FwFormField.getDesignerProperties = function(data_type) {
    var properties = [], propId, propClass, propDataControl, propDataType, propDataVersion, propDataCaption, propDataDisabled, propDataOriginalValue, propDataImageUrl, propDataField, propDataRequired, propDataMinValue, propDataMaxValue, propDataAutocomplete, propDataName, propWidth, propFloat;
    
    if ((typeof window['FwFormField_' + data_type] === 'object') && 
        (typeof window['FwFormField_' + data_type].getDesignerProperties === 'function')) {
         window['FwFormField_' + data_type].getDesignerProperties(properties);
    }
    else {
        //id
        propId = {
            caption: 'ID'
          , datatype: 'string'
          , attribute: 'id'
          , defaultvalue: ''
          , visible: true
          , enabled: true
        };
        propId.defaultvalue = FwControl.generateControlId(data_type);
        //class
        propClass = {
            caption: 'CSS Class'
          , datatype: 'string'
          , attribute: 'class'
          , defaultvalue: ''
          , visible: false
          , enabled: false
        };
        //data-control
        propDataControl = {
            caption: 'Control'
          , datatype: 'string'
          , attribute: 'data-control'
          , defaultvalue: 'FwFormField'
          , visible: true
          , enabled: false
        };
        //data-type
        propDataType = {
            caption: 'Type'
          , datatype: 'string'
          , attribute: 'data-type'
          , defaultvalue: data_type
          , visible: true
          , enabled: false
        };
        //data-version
        propDataVersion = {
            caption: 'Version'
          , datatype: 'string'
          , attribute: 'data-version'
          , defaultvalue: '1'
          , visible: false
          , enabled: false
        };
        //data-caption
        propDataCaption = {
            caption: 'Caption'
          , datatype: 'string'
          , attribute: 'data-caption'
          , defaultvalue: 'Caption'
          , visible: true
          , enabled: true
        };
        //data-enabled
        propDataEnabled = {
            caption: 'Enabled'
          , datatype: 'boolean'
          , attribute: 'data-enabled'
          , defaultvalue: 'true'
          , visible: true
          , enabled: true
        };
        //data-originalvalue
        propDataOriginalValue = {
            caption: 'Original Value'
          , datatype: 'string'
          , attribute: 'data-originalvalue'
          , defaultvalue: ''
          , visible: false
          , enabled: false
        };
        //data-imgurl
        propDataImageUrl = {
            caption: 'Image URL'
          , datatype: 'string' 
          , attribute: 'data-imgurl'
          , defaultvalue: ''
          , visible: true
          , enabled: true
        };
        //data-datafield
        propDataField = {
            caption: 'Data Field'
          , datatype: 'string'
          , attribute: 'data-datafield'
          , defaultvalue: ''
          , visible: true
          , enabled: true
        };
        //data-required
        propDataRequired = {
            caption: 'Required'
          , datatype: 'boolean'
          , attribute: 'data-required'
          , defaultvalue: 'false'
          , visible: true
          , enabled: true
        };
        //data-minvalue
        propDataMinValue = {
            caption: 'Minimum Value'
          , datatype: 'number'
          , attribute: 'data-minvalue'
          , defaultvalue: '0'
          , visible: true
          , enabled: true
        };
        //data-maxvalue
        propDataMaxValue = {
            caption: 'Maximum Value'
          , datatype: 'number'
          , attribute: 'data-maxvalue'
          , defaultvalue: '9999999'
          , visible: true
          , enabled: true
        };
        //data-name
        propDataName = {
            caption: 'Name'
          , datatype: 'string'
          , attribute: 'data-name'
          , defaultvalue: ''
          , visible: true
          , enabled: true
        };
        //width
        propWidth = {
          caption: 'Width'
          , datatype: 'string'
          , attribute: 'width'
          , defaultvalue: ''
          , visible: true
          , enabled: true
        };
        //float
        propFloat = {
          caption: 'Float'
          , datatype: 'string'
          , attribute: 'float'
          , defaultvalue: ''
          , visible: true
          , enabled: true
        };
        switch(data_type) {
            case 'text':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);            
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'email':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);            
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'searchbox':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataImageUrl);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'number':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataMinValue);
                properties.push(propDataMaxValue);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'textarea':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'select':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'password':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'checkbox':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataName);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'date':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'time':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'color':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'url':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'radio':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataName);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'phone':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'key':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                break;
            case 'validation':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'zipcode':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);            
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'multiselectvalidation':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
            case 'checkboxlist':
            case 'orderby':
                propClass.defaultvalue = 'fwcontrol fwformfield';
                properties.push(propDataControl);
                properties.push(propDataType);
                properties.push(propId);
                properties.push(propClass);
                properties.push(propDataCaption);
                properties.push(propDataField);
                properties.push(propDataRequired);
                properties.push(propDataEnabled);
                properties.push(propDataOriginalValue);
                properties.push(propDataVersion);
                properties.push(propWidth);
                properties.push(propFloat);
                break;
        }
    }
    
    return properties;
};
//---------------------------------------------------------------------------------
FwFormField.renderDesignerHtml = function($control) {
    var html, data_type;

    data_type = $control.attr('data-type');
    $control.attr('data-rendermode', 'designer');
    html = [];
    if ((typeof window['FwFormField_' + data_type] === 'object') && 
        (typeof window['FwFormField_' + data_type].renderDesignerHtml === 'function')) {
         window['FwFormField_' + data_type].renderDesignerHtml($control, html);
    }
};
//---------------------------------------------------------------------------------
FwFormField.renderRuntimeHtml = function($control) {
    var html, data_type, data_rendermode, uniqueId, validationName, $valuefield, $searchfield, $btnvalidate, timepickerTimeFormat, inputmaskTimeFormat, $children, child, name;

    data_type = $control.attr('data-type');
    $control.attr('data-rendermode', 'runtime');
    html = [];
    if ((typeof window['FwFormField_' + data_type] === 'object') && 
        (typeof window['FwFormField_' + data_type].renderRuntimeHtml === 'function')) {
         window['FwFormField_' + data_type].renderRuntimeHtml($control, html);
    }
    $control.removeAttr('data-rendermode');
    $control.removeAttr('data-version');
};
//---------------------------------------------------------------------------------
FwFormField.renderTemplateHtml = function($control) {
    var html, data_type;
    data_type = $control.attr('data-type');
    $control.attr('data-rendermode', 'template');
    html = [];
    if ((typeof window['FwFormField_' + data_type] === 'object') &&
        (typeof window['FwFormField_' + data_type].renderTemplateHtml === 'function')) {
        window['FwFormField_' + data_type].renderTemplateHtml($control, html);
    }
    else {
        switch(data_type) {
            default:
                $control.empty();
                break;
        }
    }
};
//---------------------------------------------------------------------------------
FwFormField.loadItems = function($control, items, hideEmptyItem) {
    var html, selected, previousoptgroup, $orderbydirection, checkboxid, hasorderby, optgroup, data_type;
    
    data_type = $control.attr('data-type');
    if ((typeof window['FwFormField_' + data_type] === 'object') &&
        (typeof window['FwFormField_' + data_type].loadItems === 'function')) {
        window['FwFormField_' + data_type].loadItems($control, items, hideEmptyItem);
    }
    //else {
    //    switch(data_type) {
    //        case 'select':
    //            html = [];
    //            if (!hideEmptyItem) {
    //                html.push('<option value=""></option>');
    //            }
    //            previousoptgroup = null;
    //            optgroup = '';
    //            if ((typeof items !== 'undefined') && (items !== null)) {
    //                for(var i = 0; i < items.length; i++) {
    //                    if ( (typeof items[i].optgroup !== 'undefined') && (items[i].optgroup !== previousoptgroup) ) {
    //                        previousoptgroup = items[i].optgroup;
    //                        html.push('<optgroup label="' + items[i].optgroup + '">');
    //                    }
    //                    selected = '';
    //                    if ((typeof items[i].selected !== 'undefined') && (items[i].selected === true)) {
    //                        selected = ' selected="true"';
    //                        $control.attr('data-originalvalue', items[i].value);
    //                    }
    //                    html.push('<option value="' + items[i].value + '"' + selected);
    //                    for (var key in items[i]) {
    //                        if (key != 'value' && key != 'text') {
    //                            html.push(' data-' + key + '="' + items[i][key] + '"');
    //                        }
    //                    }
    //                    html.push('>' + items[i].text + '</option>');
    //                    if ( (typeof items.optgroup !== 'undefined') && ((i+1) < items.length) && (items[i].optgroup !== items[i+1].optgroup) ) {
    //                        html.push('</optgroup>');
    //                    }
    //                }
    //            }
    //            $control.find('.fwformfield-control > select.fwformfield-value').html(html.join('\n'));
    //            break;
    //        case 'checkboxlist':
    //        case 'orderby':
    //            html = [];
    //            if ((typeof items !== 'undefined') && (items !== null)) {
    //                if ($control.attr('data-type') === 'orderby') {
    //                    $control.attr('data-sortable', 'true');
    //                    $control.attr('data-orderby', 'true');
    //                }
    //                hasorderby = ((typeof $control.attr('data-orderby') === 'string') && ($control.attr('data-orderby') === 'true'));
    //                for(var i = 0; i < items.length; i++) {
    //                    checkboxid = FwControl.generateControlId('cb' + i.toString());
    //                    if (typeof items[i].selected         !== 'string') items[i].selected         = 'F';
    //                    if (typeof items[i].orderbydirection !== 'string') items[i].orderbydirection = '';
    //                    html.push('<li data-value="');
    //                    html.push(items[i].value);
    //                    html.push('" data-selected="');
    //                    html.push(items[i].selected.toString());
    //                    html.push('"');
    //                    if ((typeof $control.attr('data-orderby') === 'string') && ($control.attr('data-orderby') === 'true')) {
    //                        html.push(' data-orderbydirection="');
    //                        html.push(items[i].orderbydirection);
    //                        html.push('"');
    //                    }
    //                    html.push('>');
    //                    html.push('<div class="wrapper">');
    //                    if (hasorderby) {
    //                        html.push('<div class="handle">::</div>');
    //                    }
    //                    html.push('<input class="checkbox" type="checkbox" id="');
    //                    html.push(checkboxid);
    //                    html.push('"');
    //                    if (items[i].selected === 'T') {
    //                        html.push(' checked="checked"');
    //                    }
    //                    html.push('/>');
    //                    html.push('<label for="');
    //                    html.push(checkboxid);
    //                    html.push('">');
    //                    html.push(items[i].text);
    //                    html.push('</label>');
    //                    if (hasorderby) {
    //                        html.push('<div class="orderbydirection"></div>');
    //                    }
    //                    html.push('</div>');
    //                    html.push('</li>');
    //                }
    //            }
    //            $control.find('ol').html(html.join(''));
    //            $control.find('ol .checkbox').on('change', function() {
    //                var $this, $li;
    //                $this = jQuery(this);
    //                $li = $this.closest('li');
    //                $li.attr('data-selected', $this.prop('checked') ? 'T' : 'F');
    //            });
    //            $control.find('ol .orderbydirection').on('click', function() {
    //                var $this, $li;
    //                $this = jQuery(this);
    //                $li = $this.closest('li');
    //                switch($li.attr('data-orderbydirection')) {
    //                    case 'asc':  $li.attr('data-orderbydirection', 'desc'); break;
    //                    case 'desc': $li.attr('data-orderbydirection', 'asc');  break;
    //                }
    //            });
    //            break;
    //    }
    //}
};
//---------------------------------------------------------------------------------
FwFormField.loadForm = function($fwformfields, model) {
    $fwformfields.each(function(index, element) {
        var $fwformfield, datafield, data_type, datafieldArray, table, field, caption, value, text;
        $fwformfield = jQuery(element);
        datafield = $fwformfield.attr('data-datafield');
        data_type  = $fwformfield.attr('data-type');
        if ((typeof datafield === 'string') && (datafield.length > 0)) {
            if (FwAppData.useWebApi && typeof window[$fwformfields.closest('.fwform').attr('data-controller')].apiurl !== 'undefined') {
                if (typeof model[datafield] !== 'undefined') {
                    table = 'model';
                    value = model[datafield];
                    text = value;
                    //text = tables[table].fields[field].text;

                    if ((typeof window['FwFormField_' + data_type] === 'object') &&
                        (typeof window['FwFormField_' + data_type].loadForm === 'function')) {
                        window['FwFormField_' + data_type].loadForm($fwformfield, table, field, value, text);
                    }
                    else {
                        $fwformfield
                            .attr('data-originalvalue', value)
                            .find('.fwformfield-value')
                            .val(value);
                    }
                } else {
                    $fwformfield.css({
                        backgroundColor: 'red'
                    });
                    //$fwformfield.find('.fwformfield-control').html(datafield + ' not returned by web service'); //MY 10/01/2015: Causing unmaskvalue function to throw errors because there is no longer an input field available
                } 
            } else {
                var tables = model;
                datafieldArray = datafield.toString().split('.');
                if (datafieldArray.length != 2) throw 'Invalid data-datafield in html template: ' + datafield;
                table = datafieldArray[0];
                field = datafieldArray[1];
                if ((typeof tables[table] !== 'undefined') && (typeof tables[table].fields[field] !== 'undefined')) {
                    value = tables[table].fields[field].value;
                    text = tables[table].fields[field].text;

                    if ((typeof window['FwFormField_' + data_type] === 'object') &&
                        (typeof window['FwFormField_' + data_type].loadForm === 'function')) {
                        window['FwFormField_' + data_type].loadForm($fwformfield, table, field, value, text);
                    }
                    else {
                        $fwformfield
                            .attr('data-originalvalue', value)
                            .find('.fwformfield-value')
                            .val(value);
                    }
                } else {
                    $fwformfield.css({
                        backgroundColor: 'red'
                    });
                    //$fwformfield.find('.fwformfield-control').html(datafield + ' not returned by web service'); //MY 10/01/2015: Causing unmaskvalue function to throw errors because there is no longer an input field available
                }   
            }
        }
    });
};
//---------------------------------------------------------------------------------
FwFormField.bulkGetValues = function($controls) {
    var $control, tables, value, originalvalue, datafield, datafieldArray, table, field;
    tables = {};
    $controls.each(function(index, element) {
        $control = jQuery(element);
        datafield = $control.attr('data-datafield');
        if (typeof datafield === 'string') {
            datafieldArray = datafield.toString().split('.');
            if (datafieldArray.length != 2) throw 'Invalid data-datafield in html template: ' + datafield;
            table         = datafieldArray[0];
            field         = datafieldArray[1];
            $control      = jQuery(element);
            originalvalue = $control.find('.fwformfield-value').attr('data-originalvalue');
            value         = $control.find('.fwformfield-value').val();
            if (typeof tables[table] === 'undefined') {
                tables[table] = {
                    fields: {}
                };
            }
            tables[table].fields[field] = {};
            tables[table].fields[field].value = value;
            if ($control.find('.fwformfield-value[data-originalvalue]').length > 0) {
                tables[table].fields[field].value = originalvalue;
            };
        }
    });
    return tables;
};
//---------------------------------------------------------------------------------
FwFormField.toggle = function($controls, isEnabled) {
    if (isEnabled) {
        FwFormField.enable($controls);
    } else {
        FwFormField.disable($controls);
    }
};
//---------------------------------------------------------------------------------
FwFormField.disable = function($controls) {
    $controls.each(function(index, element) {
        var $control, data_type;
        $control = jQuery(element);
        $control.attr('data-enabled', 'false');
        $control.find('.fwformfield-value').prop('disabled', true);
        data_type = $control.attr('data-type');
        if (typeof data_type === 'string') {
            if ((typeof window['FwFormField_' + data_type] === 'object') &&
                (typeof window['FwFormField_' + data_type].disable === 'function')) {
                window['FwFormField_' + data_type].disable($control);
            }
        }
    });
};
//---------------------------------------------------------------------------------
FwFormField.enable = function($controls) {
    $controls.each(function(index, element) {
        var $control, data_type;
        $control = jQuery(element);
        $control.attr('data-enabled', 'true');
        $control.find('.fwformfield-value').prop('disabled', false);
        data_type = $control.attr('data-type');
        if (typeof data_type === 'string') {
            if ((typeof window['FwFormField_' + data_type] === 'object') &&
                (typeof window['FwFormField_' + data_type].enable === 'function')) {
                window['FwFormField_' + data_type].enable($control);
            }
        }
    });
};
//---------------------------------------------------------------------------------
FwFormField.getValue = function($parent, selector) {
    var $fwformfield, value;
    $fwformfield = $parent.find(selector);
    value = FwFormField.getValue2($fwformfield);
    return value;
};
//---------------------------------------------------------------------------------
FwFormField.getValueByDataField = function($parent, datafield) {
    var selector, value;
    selector = 'div[data-datafield="' + datafield + '"]';
    try {
        value    = FwFormField.getValue($parent, selector);
    } catch(ex) {
        throw 'FwFormField.getValueByDataField: Unable to get value for datafield: ' + datafield;
    }
    return value;
};
//---------------------------------------------------------------------------------
FwFormField.getValue2 = function($fwformfield) {
    var data_type, value, keys;

    data_type = $fwformfield.attr('data-type');
    if ($fwformfield.length === 0)                            throw 'FwFormField.getValue2: Unable to find $fwformfield.';
    if ($fwformfield.length > 1)                              throw 'FwFormField.getValue2: $fwformfield must contain exactly one element.';
    if ($fwformfield.attr('data-control') !== 'FwFormField')  throw 'FwFormField.getValue2: $fwformfield is not a FwFormField.';
    if ((typeof window['FwFormField_' + data_type] === 'object') &&
        (typeof window['FwFormField_' + data_type].getValue2 === 'function')) {
        value = window['FwFormField_' + data_type].getValue2($fwformfield);
    }

    return value;
}
//---------------------------------------------------------------------------------
FwFormField.getValue3 = function(parentselector, selector) {
    var $parent, $fwformfield, value;
    $parent = jQuery(parentselector);
    $fwformfield = $parent.find(selector);
    value = FwFormField.getValue2($fwformfield);
    return value;
};
//---------------------------------------------------------------------------------
FwFormField.getText = function($parent, selector) {
    var $fwformfield, value;
    $fwformfield = $parent.find(selector);
    value = FwFormField.getText2($fwformfield);
    return value;
};
//---------------------------------------------------------------------------------
FwFormField.getText2 = function($fwformfield) {
    var data_type, value, keys;

    data_type = $fwformfield.attr('data-type');
    if ($fwformfield.length === 0)                            throw 'FwFormField.getText2: Unable to find $fwformfield.';
    if ($fwformfield.length > 1)                              throw 'FwFormField.getText2: $fwformfield must contain exactly one element.';
    if ($fwformfield.attr('data-control') !== 'FwFormField')  throw 'FwFormField.getText2: $fwformfield is not a FwFormField.';
    if ((typeof window['FwFormField_' + data_type] === 'object') &&
        (typeof window['FwFormField_' + data_type].getText2 === 'function')) {
        value = window['FwFormField_' + data_type].getText2($fwformfield);
    }

    return value;
};
//---------------------------------------------------------------------------------
// text is optional
// firechangeevent is optional
FwFormField.setValue = function($parent, selector, value, text, firechangeevent) {
    var $fwformfield, data_type, keys, $inputtext, $inputvalue;
    $fwformfield = $parent.find(selector);
    data_type = $fwformfield.attr('data-type');
    if ($fwformfield.length === 0)                            throw 'FwFormField.setValue: Unable to find: ' + selector;
    if ($fwformfield.attr('data-control') !== 'FwFormField')  throw 'FwFormField.setValue: ' + selector + ' is not a FwFormField';
    if ((typeof window['FwFormField_' + data_type] === 'object') &&
        (typeof window['FwFormField_' + data_type].setValue === 'function')) {
        window['FwFormField_' + data_type].setValue($fwformfield, value, text, firechangeevent);
    }
};
//---------------------------------------------------------------------------------
FwFormField.setValueByDataField = function($parent, datafield, value, text, firechangeevent) {
    var selector;
    selector = 'div[data-datafield="' + datafield + '"]';
    FwFormField.setValue($parent, selector, value, text, firechangeevent);
};
//---------------------------------------------------------------------------------
FwFormField.onRemove = function($fwformfield) {
    var data_type;
    data_type = $fwformfield.attr('data-type');
    if (typeof data_type === 'string') {
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].onRemove === 'function')) {
            window['FwFormField_' + data_type].onRemove($fwformfield);
        }
    }
};
//---------------------------------------------------------------------------------
