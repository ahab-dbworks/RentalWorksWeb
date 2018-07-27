class FwBrowseColumn_textClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    }
    setFieldValue($browse, $tr, $field, data) {
        $field.find('input.value').val(data.value);
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
        $field.data('selectthetextbox', false);
        if (typeof $field.attr('data-rowclassmapping') !== 'undefined') {
            var rowclassmapping = JSON.parse($field.attr('data-rowclassmapping'));
            if (originalvalue in rowclassmapping === true) {
                $tr.addClass(rowclassmapping[originalvalue]);
            }
        }
<<<<<<< refs/remotes/origin/develop
=======
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('selectthetextbox', true);
            }
        });
>>>>>>> Updates Fw
    }
    setFieldEditMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var formmaxlength = (typeof $field.attr('data-formmaxlength') === 'string') ? $field.attr('data-formmaxlength') : '';
        let html = [];
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (formmaxlength != '') {
            html.push(' maxlength="' + formmaxlength + '"');
        }
        html.push(' />');
        let htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
<<<<<<< refs/remotes/origin/develop
=======
        if ($field.data('selectthetextbox') === true) {
            $field.data('selectthetextbox', false);
            $field.find('.value').select();
        }
>>>>>>> Updates Fw
    }
}
var FwBrowseColumn_text = new FwBrowseColumn_textClass();
//# sourceMappingURL=FwBrowseColumn_text.js.map