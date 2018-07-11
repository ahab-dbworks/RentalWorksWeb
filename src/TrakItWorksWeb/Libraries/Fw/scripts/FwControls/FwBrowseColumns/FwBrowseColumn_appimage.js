var FwBrowseColumn_appimageClass = (function () {
    function FwBrowseColumn_appimageClass() {
    }
    FwBrowseColumn_appimageClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
        var uniqueid1, uniqueid2, uniqueid3, hasimage;
        uniqueid1 = (typeof dtRow[dt.ColumnIndex[$field.attr('data-uniqueid1field')]] === 'string') ? dtRow[dt.ColumnIndex[$field.attr('data-uniqueid1field')]] : '';
        uniqueid2 = (typeof dtRow[dt.ColumnIndex[$field.attr('data-uniqueid2field')]] === 'string') ? dtRow[dt.ColumnIndex[$field.attr('data-uniqueid2field')]] : '';
        uniqueid3 = (typeof dtRow[dt.ColumnIndex[$field.attr('data-uniqueid3field')]] === 'string') ? dtRow[dt.ColumnIndex[$field.attr('data-uniqueid3field')]] : '';
        hasimage = (typeof dtRow[dt.ColumnIndex[$field.attr('data-browsedatafield')]] === 'string') ? dtRow[dt.ColumnIndex[$field.attr('data-browsedatafield')]] : '';
        $field.attr('data-uniqueid1', uniqueid1);
        $field.attr('data-uniqueid2', uniqueid2);
        $field.attr('data-uniqueid3', uniqueid3);
        $field.attr('data-hasimage', hasimage);
    };
    FwBrowseColumn_appimageClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_appimageClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        throw 'FwBrowseColumn_appimage.setFieldValue: Not Implemented!';
    };
    FwBrowseColumn_appimageClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_appimageClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        FwBrowseColumn_appimage.setFieldEditMode($browse, $tr, $field);
    };
    FwBrowseColumn_appimageClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var $icon = null, $popup, $popupInner, $appimage, $fwformfields;
        if (!$tr.hasClass('newmode')) {
            if ($tr.hasClass('viewmode')) {
                if ($field.attr('data-hasimage') === 'T') {
                    $icon = jQuery('<img src="' + applicationConfig.appbaseurl + 'theme/fwimages/icons/16/fileextension-image.png" style="cursor:pointer;" />');
                }
            }
            else if ($tr.hasClass('editmode')) {
                if ($field.attr('data-hasimage') === 'T') {
                    $icon = jQuery('<img src="' + applicationConfig.appbaseurl + 'theme/fwimages/icons/16/fileextension-image.png" style="cursor:pointer;" />');
                }
                else {
                    $icon = jQuery('<img src="' + applicationConfig.appbaseurl + 'theme/fwimages/icons/16/document-new.png" style="cursor:pointer;" />');
                }
            }
            if ($icon !== null) {
                $icon.on('click', function (e) {
                    var uniqueid1, uniqueid2, uniqueid3;
                    var html = [];
                    html.push('<div class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-mode="VIEW">');
                    html.push('  <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-caption="" data-datafield="uniqueid1"></div>');
                    html.push('  <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-caption="" data-datafield="uniqueid2"></div>');
                    html.push('  <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-caption="" data-datafield="uniqueid3"></div>');
                    html.push('  <div class="fwcontrol fwappimage" data-control="FwAppImage" data-type="" data-uniqueid1field="uniqueid1" data-uniqueid2field="uniqueid2" data-uniqueid3field="uniqueid3" data-description="" data-rectype=""></div>');
                    html.push('</div>');
                    var htmlString = html.join('');
                    $popup = FwConfirmation.renderConfirmation('Documents', '');
                    FwConfirmation.addControls($popup, htmlString);
                    FwConfirmation.addButton($popup, 'Close', true);
                    $fwformfields = $popup.find('div[data-control="FwFormField"]');
                    FwControl.renderRuntimeControls($fwformfields);
                    uniqueid1 = $field.attr('data-uniqueid1');
                    uniqueid2 = $field.attr('data-uniqueid2');
                    uniqueid3 = $field.attr('data-uniqueid3');
                    FwFormField.setValueByDataField($popup, 'uniqueid1', uniqueid1);
                    FwFormField.setValueByDataField($popup, 'uniqueid2', uniqueid2);
                    FwFormField.setValueByDataField($popup, 'uniqueid3', uniqueid3);
                    $appimage = $popup.find('.fwappimage');
                    if ($tr.hasClass('viewmode')) {
                        $appimage.attr('data-hasdelete', 'false');
                        $appimage.attr('data-hasadd', 'false');
                    }
                    FwAppImage.loadControl($appimage);
                });
                $field.empty().append([$icon]);
            }
        }
        else {
            $field.empty().append('<div>(Save then Edit)</div>');
        }
    };
    return FwBrowseColumn_appimageClass;
}());
var FwBrowseColumn_appimage = new FwBrowseColumn_appimageClass();
//# sourceMappingURL=FwBrowseColumn_appimage.js.map