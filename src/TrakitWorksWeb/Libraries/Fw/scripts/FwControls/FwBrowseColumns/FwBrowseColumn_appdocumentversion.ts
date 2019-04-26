class FwBrowseColumn_appdocumentversionClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    };
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {

    };
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        throw 'FwBrowseColumn_appdocumentversion.setFieldValue: Not Implemented!';
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = false;
        return isModified;
    };
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        var $control, $tr, $uniqueid1field, $documenttypeidfield, $documenttypefield, version, documenttype, hidecolreviewed, hidecolexpired;
        let html = [];
        version = $field.attr('data-originalvalue');
        $uniqueid1field = $tr.find('.field[data-browsedatafield="uniqueid1"]');
        $documenttypeidfield = $tr.find('.field[data-browsedatafield="documenttypeid"]');
        $documenttypefield = $tr.find('.field[data-browsedatafield="documenttype"]');
        hidecolreviewed = ($browse.attr('data-hidecolreviewed') === 'true');
        hidecolexpired = ($browse.attr('data-hidecolexpired') === 'true')
        let uniqueid1 = ((typeof $uniqueid1field.html() === 'string') && ($uniqueid1field.html().length > 0)) ? $uniqueid1field.html() : '';
        let documenttypeid = $documenttypeidfield.attr('data-originalvalue');
        documenttype = ((typeof $documenttypefield.html() === 'string') && ($documenttypefield.html().length > 0)) ? $documenttypefield.html() : '';
        if (version !== '0') {
            html.push('<div class="viewappdocumentversion">');
            html.push('  <div><img class="btnShowVersions" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/browsesearch.001.png" style="width:16px;height:16px;vertical-align:middle;cursor:pointer;" />' + version + '</div>');
            html.push('</div>');
        } else {
            html.push('<div class="viewappdocumentversion"></div>');
        }
        $field.empty();
        $control = jQuery(html.join(''));
        $control.attr('data-uniqueid1', uniqueid1);
        $control.attr('data-documenttypeid', documenttypeid);
        $control.attr('data-documenttype', documenttype);
        if (html.length > 0) {
            $control.on('click', '.btnShowVersions', function (event) {
                var $popupbrowse, $confirmation, $btnClose;
                try {
                    $confirmation = FwConfirmation.renderConfirmation('Additional Documents for ' + $control.attr('data-documenttype'), '');
                    $confirmation.attr('data-nopadding', 'true');
                    html = [];
                    html.push('<div class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-mode="EDIT">');
                    $popupbrowse = jQuery('<div>').append(jQuery('#tmpl-grids-AppDocumentVersionGridBrowse').html());
                    if (FwSecurity.isContact()) {
                        $popupbrowse.find('[data-browsedatafield="reviewed"]').attr('data-formreadonly', true);
                    }
                    if (hidecolreviewed) {
                        $popupbrowse.find('.column[data-columnname="reviewed"]').attr('data-visible', 'false');
                    }
                    if (hidecolexpired) {
                        $popupbrowse.find('.column[data-columnname="expiration"]').attr('data-visible', 'false');
                    }
                    FwConfirmation.addControls($confirmation, $popupbrowse.html());
                    $popupbrowse = $confirmation.find('.fwbrowse');
                    $popupbrowse.data('ondatabind', function (request) {
                        request.miscfields.uniqueid1 = $control.attr('data-uniqueid1');
                        request.miscfields.documenttypeid = $control.attr('data-documenttypeid');
                    });
                    $popupbrowse.data('beforesave', function (request) {
                        request.miscfields.uniqueid1 = $control.attr('data-uniqueid1');
                        request.miscfields.documenttypeid = $control.attr('data-documenttypeid');
                    });
                    FwBrowse.search($popupbrowse);
                    $btnClose = FwConfirmation.addButton($confirmation, 'Close', false);
                    $btnClose.on('click', function (event) {
                        FwBrowse.search($browse);
                        FwConfirmation.destroyConfirmation($confirmation);
                    });
                    event.preventDefault();
                    return false;
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            $field.append($control);
        }
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        let html = [];
        html.push('<div class="editappdocumentversion"></div>');
        let htmlStr = html.join('\n');
        let $control = jQuery(htmlStr)
        $field.empty().append($control);
    };
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_appdocumentversion = new FwBrowseColumn_appdocumentversionClass();