class FwFormFieldClass {
    //---------------------------------------------------------------------------------
    init($control: JQuery) {
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
        $control.on('focus', '.fwformfield-text, .fwformfield-value', function () {
            var $field = jQuery(this).closest('.fwformfield');
            $field.addClass('focused');
        });
        $control.on('blur', '.fwformfield-text, .fwformfield-value', function () {
            var $field = jQuery(this).closest('.fwformfield');
            $field.removeClass('focused');
        });
    }
    //---------------------------------------------------------------------------------
    getDroppableRegions(data_type: string) {
        var selector;

        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].getDroppableRegions === 'function')) {
            selector = window['FwFormField_' + data_type].getDroppableRegions();
        }
        else {
            selector = '.fwform-fieldrow > .children';
        }

        return selector;
    }
    //---------------------------------------------------------------------------------
    getHtmlTag(data_type: string) {
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
    }
    //---------------------------------------------------------------------------------
    getDesignerProperties(data_type: string) {
        var properties = [], propId, propClass, propDataControl, propDataType, propDataVersion, propDataCaption, propDataEnabled, propDataOriginalValue, propDataImageUrl, propDataField, propDataRequired, propDataMinValue, propDataMaxValue, propDataAutocomplete, propDataName, propWidth, propFloat;

        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].getDesignerProperties === 'function')) {
            window['FwFormField_' + data_type].getDesignerProperties(properties);
        }
        else {
            propId = { caption: 'ID', datatype: 'string', attribute: 'id', defaultvalue: FwControl.generateControlId(data_type), visible: true, enabled: true };
            propClass = { caption: 'CSS Class', datatype: 'string', attribute: 'class', defaultvalue: 'fwcontrol fwformfield', visible: false, enabled: false };
            propDataControl = { caption: 'Control', datatype: 'string', attribute: 'data-control', defaultvalue: 'FwFormField', visible: true, enabled: false };
            propDataType = { caption: 'Type', datatype: 'string', attribute: 'data-type', defaultvalue: data_type, visible: true, enabled: false };
            propDataVersion = { caption: 'Version', datatype: 'string', attribute: 'data-version', defaultvalue: '1', visible: false, enabled: false };
            propDataCaption = { caption: 'Caption', datatype: 'string', attribute: 'data-caption', defaultvalue: 'Caption', visible: true, enabled: true };
            propDataEnabled = { caption: 'Enabled', datatype: 'boolean', attribute: 'data-enabled', defaultvalue: 'true', visible: true, enabled: true };
            propDataOriginalValue = { caption: 'Original Value', datatype: 'string', attribute: 'data-originalvalue', defaultvalue: '', visible: false, enabled: false };
            propDataImageUrl = { caption: 'Image URL', datatype: 'string', attribute: 'data-imgurl', defaultvalue: '', visible: true, enabled: true };
            propDataField = { caption: 'Data Field', datatype: 'string', attribute: 'data-datafield', defaultvalue: '', visible: true, enabled: true };
            propDataRequired = { caption: 'Required', datatype: 'boolean', attribute: 'data-required', defaultvalue: 'false', visible: true, enabled: true };
            propDataMinValue = { caption: 'Minimum Value', datatype: 'number', attribute: 'data-minvalue', defaultvalue: '0', visible: true, enabled: true };
            propDataMaxValue = { caption: 'Maximum Value', datatype: 'number', attribute: 'data-maxvalue', defaultvalue: '9999999', visible: true, enabled: true };
            propDataName = { caption: 'Name', datatype: 'string', attribute: 'data-name', defaultvalue: '', visible: true, enabled: true };
            propWidth = { caption: 'Width', datatype: 'string', attribute: 'width', defaultvalue: '', visible: true, enabled: true };
            propFloat = { caption: 'Float', datatype: 'string', attribute: 'float', defaultvalue: '', visible: true, enabled: true };
            switch (data_type) {
                case 'text':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'email':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'searchbox':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataImageUrl, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'number':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataMinValue, propDataMaxValue, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'textarea':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'select':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'password':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'checkbox':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataName, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'date':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'time':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'color':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'url':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'radio':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataName, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'phone':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'key':
                    properties = [propDataControl, propDataType, propId, propClass, propDataOriginalValue, propDataVersion];
                    break;
                case 'validation':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'zipcode':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'multiselectvalidation':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
                case 'checkboxlist':
                case 'orderby':
                    properties = [propDataControl, propDataType, propId, propClass, propDataCaption, propDataField, propDataRequired, propDataEnabled, propDataOriginalValue, propDataVersion, propWidth, propFloat];
                    break;
            }
        }

        return properties;
    }
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery) {
        var data_type = $control.attr('data-type');
        $control.attr('data-rendermode', 'designer');
        var html = [];
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].renderDesignerHtml === 'function')) {
            window['FwFormField_' + data_type].renderDesignerHtml($control, html);
        }
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery) {
        var data_type = $control.attr('data-type');
        $control.attr('data-rendermode', 'runtime');
        var html = [];
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].renderRuntimeHtml === 'function')) {
            window['FwFormField_' + data_type].renderRuntimeHtml($control, html);
        }
        $control.removeAttr('data-rendermode');
        $control.removeAttr('data-version');
    }
    //---------------------------------------------------------------------------------
    renderTemplateHtml($control: JQuery) {
        var data_type = $control.attr('data-type');
        $control.attr('data-rendermode', 'template');
        var html = [];
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].renderTemplateHtml === 'function')) {
            window['FwFormField_' + data_type].renderTemplateHtml($control, html);
        }
        else {
            switch (data_type) {
                default:
                    $control.empty();
                    break;
            }
        }
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery, items: any, hideEmptyItem?: boolean) {
        var data_type = $control.attr('data-type');
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].loadItems === 'function')) {
            window['FwFormField_' + data_type].loadItems($control, items, hideEmptyItem);
        }
    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfields: JQuery, model: any) {
        if (typeof model._Custom !== 'undefined' && model._Custom.length > 0 && model._Custom !== undefined) {
            for (var i = 0; i < model._Custom.length; i++) {
                model[model._Custom[i].FieldName] = model._Custom[i].FieldValue
            }
        }

        $fwformfields.each(function (index, element) {
            var $fwformfield, datafield, displayfield, data_type, datafieldArray, table, field, caption, value, text;
            $fwformfield = jQuery(element);
            datafield = $fwformfield.attr('data-datafield');
            displayfield = $fwformfield.attr('data-displayfield');
            data_type = $fwformfield.attr('data-type');
            if ((typeof datafield === 'string') && (datafield.length > 0)) {
                if (typeof window[$fwformfields.closest('.fwform').attr('data-controller')].apiurl !== 'undefined') {
                    if (typeof model[datafield] !== 'undefined') {
                        table = 'model';
                        value = model[datafield];
                        text = '';
                        if (typeof model[datafield] !== 'undefined') {
                            text = model[displayfield];
                        }

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
    }
    //---------------------------------------------------------------------------------
    bulkGetValues($controls: JQuery) {
        var $control, tables, value, originalvalue, datafield, datafieldArray, table, field;
        tables = {};
        $controls.each(function (index, element) {
            $control = jQuery(element);
            datafield = $control.attr('data-datafield');
            if (typeof datafield === 'string') {
                datafieldArray = datafield.toString().split('.');
                if (datafieldArray.length != 2) throw 'Invalid data-datafield in html template: ' + datafield;
                table = datafieldArray[0];
                field = datafieldArray[1];
                $control = jQuery(element);
                originalvalue = $control.find('.fwformfield-value').attr('data-originalvalue');
                value = $control.find('.fwformfield-value').val();
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
    }
    //---------------------------------------------------------------------------------
    toggle($controls: JQuery, isEnabled: boolean) {
        if (isEnabled) {
            this.enable($controls);
        } else {
            this.disable($controls);
        }
    }
    //---------------------------------------------------------------------------------
    disable($controls: JQuery) {
        $controls.each(function (index, element) {
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
    }
    //---------------------------------------------------------------------------------
    enable($controls: JQuery) {
        $controls.each(function (index, element) {
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
    }
    //---------------------------------------------------------------------------------
    getValue($parent: JQuery, selector: string) {
        var $fwformfield, value;
        $fwformfield = $parent.find(selector);
        if ($fwformfield.length > 1) {
            $fwformfield = jQuery($fwformfield[0]);
        }
        value = this.getValue2($fwformfield);
        return value;
    }
    //---------------------------------------------------------------------------------
    getValueByDataField($parent: JQuery, datafield: string) {
        var selector, value;
        selector = 'div[data-datafield="' + datafield + '"]';
        try {
            value = this.getValue($parent, selector);
        } catch (ex) {
            throw 'this.getValueByDataField: Unable to get value for datafield: ' + datafield;
        }
        return value;
    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery) {
        var data_type, value, keys;

        data_type = $fwformfield.attr('data-type');
        if ($fwformfield.length === 0) throw 'FwFormField.getValue2: Unable to find $fwformfield.';
        if ($fwformfield.length > 1) throw 'FwFormField.getValue2: $fwformfield must contain exactly one element.';
        if ($fwformfield.attr('data-control') !== 'FwFormField') throw 'FwFormField.getValue2: $fwformfield is not a FwFormField.';
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].getValue2 === 'function')) {
            value = window['FwFormField_' + data_type].getValue2($fwformfield);
        }

        return value;
    }
    //---------------------------------------------------------------------------------
    getValue3(parentselector: string, selector: string) {
        var $parent, $fwformfield, value;
        $parent = jQuery(parentselector);
        $fwformfield = $parent.find(selector);
        value = this.getValue2($fwformfield);
        return value;
    }
    //---------------------------------------------------------------------------------
    getTextByDataField($parent: JQuery, datafield: string) {
        var selector, value;
        selector = 'div[data-datafield="' + datafield + '"]';
        try {
            value = this.getText($parent, selector);
        } catch (ex) {
            throw 'FwFormField.getValueByDataField: Unable to get value for datafield: ' + datafield;
        }
        return value;
    }
    //---------------------------------------------------------------------------------
    getText($parent: JQuery, selector: string) {
        var $fwformfield, value;
        $fwformfield = $parent.find(selector);
        value = this.getText2($fwformfield);
        return value;
    }
    //---------------------------------------------------------------------------------
    getText2($fwformfield: JQuery) {
        var data_type, value, keys;

        data_type = $fwformfield.attr('data-type');
        if ($fwformfield.length === 0) throw 'FwFormField.getText2: Unable to find $fwformfield.';
        if ($fwformfield.length > 1) throw 'FwFormField.getText2: $fwformfield must contain exactly one element.';
        if ($fwformfield.attr('data-control') !== 'FwFormField') throw 'FwFormField.getText2: $fwformfield is not a FwFormField.';
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].getText2 === 'function')) {
            value = window['FwFormField_' + data_type].getText2($fwformfield);
        }

        return value;
    }
    //---------------------------------------------------------------------------------
    // text is optional
    // firechangeevent is optional
    setValue = function ($parent: JQuery, selector: string, value: string | number | boolean, text?: string | number, firechangeevent?: boolean) {
        var $fwformfield, data_type, keys, $inputtext, $inputvalue;
        $fwformfield = $parent.find(selector);
        data_type = $fwformfield.attr('data-type');
        if ($fwformfield.length === 0) return;
        if ($fwformfield.attr('data-control') !== 'FwFormField') throw 'FwFormField.setValue: ' + selector + ' is not a FwFormField';
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].setValue === 'function')) {
            window['FwFormField_' + data_type].setValue($fwformfield, value, text, firechangeevent);
        }
    }
    //---------------------------------------------------------------------------------
    setValue2($fwformfield: JQuery, value: string | number, text?: string | number | boolean, firechangeevent?: boolean) {
        var data_type, keys, $inputtext, $inputvalue;
        data_type = $fwformfield.attr('data-type');
        if ($fwformfield.length === 0) return;
        if ($fwformfield.attr('data-control') !== 'FwFormField') throw 'FwFormField.setValue2: $fwformfield is not a FwFormField';
        if ((typeof window['FwFormField_' + data_type] === 'object') &&
            (typeof window['FwFormField_' + data_type].setValue === 'function')) {
            window['FwFormField_' + data_type].setValue($fwformfield, value, text, firechangeevent);
        }
    }
    //---------------------------------------------------------------------------------
    setValueByDataField($parent: JQuery, datafield: string, value: string | number | boolean, text?: string | number, firechangeevent?: boolean) {
        var selector;
        selector = 'div[data-datafield="' + datafield + '"]';
        this.setValue($parent, selector, value, text, firechangeevent);
    }
    //---------------------------------------------------------------------------------
    onRemove($fwformfield: JQuery) {
        var data_type;
        data_type = $fwformfield.attr('data-type');
        if (typeof data_type === 'string') {
            if ((typeof window['FwFormField_' + data_type] === 'object') &&
                (typeof window['FwFormField_' + data_type].onRemove === 'function')) {
                window['FwFormField_' + data_type].onRemove($fwformfield);
            }
        }
    }
    //---------------------------------------------------------------------------------
    getControllerName($fwformfield: JQuery) {
        var controllername = '';
        var $form = $fwformfield.closest('.fwform')
        if ($form.length > 0) {
            controllername = $form.attr('data-controller');
        } else {
            var $browse = $fwformfield.closest('.fwbrowse');
            if ($browse.length > 0) {
                controllername = $browse.attr('data-controller');
            }
        }
        //if (controllername.length === 0) { 
        //    throw 'There is no controller defined on parent form/browse.' 
        //} 
        return controllername;
    }
    //---------------------------------------------------------------------------------
    getController($fwformfield: JQuery) {
        var controllername = this.getControllerName($fwformfield);
        var controller = window[controllername];
        //if (typeof controller === 'undefined') { 
        //    throw 'There is no controller with the name: ' + controllername; 
        //} 
        return controller;
    }
    //---------------------------------------------------------------------------------
    getDataField($parent: JQuery<HTMLElement>, datafield: string) {
        var $field = $parent.find(`div[data-datafield="${datafield}"]`);
        return $field;
    }
    //---------------------------------------------------------------------------------
}

var FwFormField = new FwFormFieldClass();
