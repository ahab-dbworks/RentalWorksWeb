//---------------------------------------------------------------------------------
var FwAudit = {};
//---------------------------------------------------------------------------------
FwAudit.init = function($control) {
    
};
//---------------------------------------------------------------------------------
FwAudit.getHtmlTag = function(data_type) {
    var template, html, properties, i;
    template = [];
    template.push('<div');
    properties = this.getDesignerProperties();
    for (i = 0; i < properties.length; i++) {
        template.push(' ' + properties[i].attribute + '="' + properties[i].defaultvalue + '"');
    }
    template.push('></div>');
    html = template.join('');
    return html;
};
//---------------------------------------------------------------------------------
FwAudit.getDesignerProperties = function() {
    var properties = [], propId, propClass, propDataControl, propDataType, propDataVersion;
    propId          = { caption: 'ID',        datatype: 'string', attribute: 'id',           defaultvalue: FwControl.generateControlId(data_type), visible: true,  enabled: true };
    propClass       = { caption: 'CSS Class', datatype: 'string', attribute: 'class',        defaultvalue: 'fwcontrol fwaudit',                    visible: false, enabled: false };
    propDataControl = { caption: 'Control',   datatype: 'string', attribute: 'data-control', defaultvalue: 'FwFormField',                          visible: true,  enabled: false };
    propDataType    = { caption: 'Type',      datatype: 'string', attribute: 'data-type',    defaultvalue: data_type,                              visible: true,  enabled: false };
    propDataVersion = { caption: 'Version',   datatype: 'string', attribute: 'data-version', defaultvalue: '1',                                    visible: false, enabled: false };

    properties = [propDataControl, propDataType, propId, propClass];

    return properties;
};
//---------------------------------------------------------------------------------
FwAudit.renderDesignerHtml = function($control) {
    var html, controltype;
    data_rendermode = $control.attr('data-rendermode');
    $control.attr('data-rendermode', 'designer');
    html = [];

    html.push(FwControl.generateDesignerHandle(controltype, $control.attr('id')));
    html.push('<div class="formpage">');
        html.push('<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-version="1" data-caption="Audit" data-rendermode="template">');
            html.push('<div class="fwcontrol fwcontainer fwgroupbox" data-control="FwContainer" data-type="groupbox" data-version="1" data-caption="Input By" data-rendermode="template">');
                html.push('<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="webauditnewview.name" data-required="false" data-enabled="false" data-originalvalue="" data-version="1" data-rendermode="template" data-isuniqueid="false"></div>');
                html.push('<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Date" data-datafield="webauditnewview.updatedate" data-required="false" data-enabled="false" data-originalvalue="" data-version="1" data-rendermode="template" data-isuniqueid="false"></div>');
            html.push('</div>');
            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" data-version="1" data-rendermode="template">');
                html.push('<div class="auditHistoryGrid" style="margin:5px;"></div>');
            html.push('</div>');
        html.push('</div>');
    html.push('</div>');

    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwAudit.renderRuntimeHtml = function($control) {
    var html, data_rendermode, $fwcontrols;
    data_rendermode = $control.attr('data-rendermode');
    $control.attr('data-rendermode', 'runtime');
    html = [];

    html.push('<div class="formpage">');
        html.push('<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-version="1" data-caption="Audit" data-rendermode="template">');
            html.push('<div class="fwcontrol fwcontainer fwgroupbox" data-control="FwContainer" data-type="groupbox" data-version="1" data-caption="Input By" data-rendermode="template">');
                html.push('<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield inputname" data-caption="Name" data-datafield="" data-enabled="false" style="float:left;width:50%;"></div>');
                html.push('<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield inputdate" data-caption="Date" data-datafield="" data-enabled="false" style="float:left;width:50%;"></div>');
            html.push('</div>');
            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" data-version="1" data-rendermode="template">');
                html.push('<div class="auditHistoryGrid" style="margin:5px;"></div>');
            html.push('</div>');
        html.push('</div>');
    html.push('</div>');

    $control.html(html.join(''));

    $fwcontrols = $control.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);
};
//---------------------------------------------------------------------------------
FwAudit.renderTemplateHtml = function($control) {
    var html, data_rendermode;

    data_rendermode = $control.attr('data-rendermode');
    $control.attr('data-rendermode', 'template');
    html = [];

    $control.empty();

};
//---------------------------------------------------------------------------------
FwAudit.loadAudit = function($form, uniqueid) {
    var $auditHistoryGrid, $auditHistoryGridControl, request;
    
    // load Audit History Grid
    $auditHistoryGrid        = $form.find('.auditHistoryGrid');
    $auditHistoryGridControl = jQuery(FwBrowse.loadGridFromTemplate('AuditHistoryGrid'));
    $auditHistoryGrid.empty().append($auditHistoryGridControl);
    $auditHistoryGridControl.data('ondatabind', function(request) {
        request.module = 'AuditHistoryGrid';
        request.uniqueid = uniqueid;
        FwServices.grid.method(request, request.module, 'Browse', $form, function(response) {
            var createdFlagIndex, nameIndex, updateDateIndex;
            if (typeof response.browse.ColumnIndex['createdflag'] === 'undefined') {
                throw 'FwAudit.loadAudit: Error loading createdflag.';
            }
            if (typeof response.browse.ColumnIndex['name'] === 'undefined') {
                throw 'FwAudit.loadAudit: Error loading name.';
            }
            if (typeof response.browse.ColumnIndex['updatedate'] === 'undefined') {
                throw 'FwAudit.loadAudit: Error loading updatedate.';
            }
            createdFlagIndex = response.browse.ColumnIndex['createdflag'];
            nameIndex        = response.browse.ColumnIndex['name'];
            updateDateIndex  = response.browse.ColumnIndex['updatedate'];
            for (var i = (response.browse.Rows.length - 1); i >= 0; i--) {
                if (response.browse.Rows[i][createdFlagIndex] === "T") {
                    var date = new Date(response.browse.Rows[i][updateDateIndex]);
                    $form.find('.inputname input').val(response.browse.Rows[i][nameIndex]);
                    $form.find('.inputdate input').val(date.toLocaleDateString() + ' ' + date.toLocaleTimeString());

                    response.browse.Rows.splice(i, 1)
                }
            }

            FwBrowse.databindcallback($auditHistoryGridControl, response.browse);
        });
    });
    FwBrowse.init($auditHistoryGridControl);
    FwBrowse.renderRuntimeHtml($auditHistoryGridControl);
    FwBrowse.search($auditHistoryGridControl);
};
//---------------------------------------------------------------------------------
