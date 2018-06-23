var FwBrowseColumn_textClass = (function () {
    function FwBrowseColumn_textClass() {
    }
    FwBrowseColumn_textClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_textClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    };
    FwBrowseColumn_textClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_textClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_textClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
        if (typeof $field.attr('data-rowclassmapping') !== 'undefined') {
            var rowclassmapping = JSON.parse($field.attr('data-rowclassmapping'));
            if (originalvalue in rowclassmapping === true) {
                $tr.addClass(rowclassmapping[originalvalue]);
            }
        }
    };
    FwBrowseColumn_textClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var formmaxlength = (typeof $field.attr('data-formmaxlength') === 'string') ? $field.attr('data-formmaxlength') : '';
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (formmaxlength != '') {
            html.push(' maxlength="' + formmaxlength + '"');
        }
        html.push(' />');
        html = html.join('');
        $field.html(html);
        $field.find('input.value').val(originalvalue);
    };
    return FwBrowseColumn_textClass;
}());
var FwBrowseColumn_text = new FwBrowseColumn_textClass();
//# sourceMappingURL=FwBrowseColumn_text.js.map