FwBrowseColumn_appdocumentversion = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_appdocumentversion.databindfield = function($browse, $field, dt, dtRow, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_appdocumentversion.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_appdocumentversion.isModified = function ($browse, $tr, $field) {
    var isModified = false;
    return isModified;
};
//---------------------------------------------------------------------------------
FwBrowseColumn_appdocumentversion.setFieldViewMode = function($browse, $field, $tr, html) {
    var $control, $tr, $uniqueid1field, $documenttypeidfield, $documenttypefield, version, documenttype, hidecolreviewed, hidecolexpired;
    
    version = $field.attr('data-originalvalue');
    $uniqueid1field      = $tr.find('.field[data-browsedatafield="uniqueid1"]');
    $documenttypeidfield = $tr.find('.field[data-browsedatafield="documenttypeid"]');
    $documenttypefield   = $tr.find('.field[data-browsedatafield="documenttype"]');
    hidecolreviewed      = ($browse.attr('data-hidecolreviewed') === 'true');
    hidecolexpired       = ($browse.attr('data-hidecolexpired')  === 'true')
    uniqueid1      = ((typeof $uniqueid1field.html()      === 'string') && ($uniqueid1field.html().length      > 0)) ? $uniqueid1field.html()      : '';
    documenttypeid = $documenttypeidfield.attr('data-originalvalue');
    documenttype =   ((typeof $documenttypefield.html()   === 'string') && ($documenttypefield.html().length   > 0)) ? $documenttypefield.html()   : '';
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
        $control.on('click', '.btnShowVersions', function(event) {
            var $popupbrowse, $confirmation, $btnClose;
            try {
                $confirmation = FwConfirmation.renderConfirmation('Additional Documents for ' + $control.attr('data-documenttype'));
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
                $popupbrowse.data('ondatabind', function(request) {
                    request.miscfields.uniqueid1      = $control.attr('data-uniqueid1');
                    request.miscfields.documenttypeid = $control.attr('data-documenttypeid');
                });
                $popupbrowse.data('beforesave', function(request) {
                    request.miscfields.uniqueid1      = $control.attr('data-uniqueid1');
                    request.miscfields.documenttypeid = $control.attr('data-documenttypeid');
                });
                FwBrowse.search($popupbrowse);
                $btnClose = FwConfirmation.addButton($confirmation, 'Close', false);
                $btnClose.on('click', function(event) {
                    FwBrowse.search($browse);
                    FwConfirmation.destroyConfirmation($confirmation);
                });
                event.preventDefault();
                return false;
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
        $field.append($control);
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_appdocumentversion.setFieldEditMode = function($browse, $field, $tr, html) {
    var $control;
    html.push('<div class="editappdocumentversion"></div>');
    html = html.join('\n');
    $control = jQuery(html)
    $field.empty().append($control);
};
//---------------------------------------------------------------------------------
