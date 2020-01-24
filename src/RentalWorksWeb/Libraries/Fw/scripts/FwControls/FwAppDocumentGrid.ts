class FwAppDocumentGridClass {
    constructor() {

    }
    //---------------------------------------------------------------------------------
    init($control) {
    
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery) {
        $control.attr('data-rendermode', 'runtime');
        
        //let html = [];
        //html.push('<div[data-grid="FwAppDocumentGrid"]>');
        //const htmlStr = html.join('\n');
        //$control.html(htmlStr);

        //const $fwcontrols = $control.find('.fwcontrol');
        //FwControl.init($fwcontrols);
        //FwControl.renderRuntimeHtml($fwcontrols);
    }
    //---------------------------------------------------------------------------------
    renderGrid($control: JQuery, nameGrid: string, baseUrl: string, moduleSecurityId: string, gridSecurityId: string, $form: JQuery, uniqueid1: string, uniqueid2: string) {
        FwBrowse.renderGrid({
            moduleSecurityId: moduleSecurityId, 
            $form: $form, 
            gridSecurityId: gridSecurityId,
            nameGrid: 'FwAppDocumentGrid',
            pageSize: 15,
            getBaseApiUrl: () => baseUrl,
            onDataBind: (request) => {

            },
            addGridMenu: (options) => {
                
            },
            getTemplate: () => {
                return this.getTemplate();
            }
         });
        //// load Audit History Grid
        //const $appDocumentGrid        = $form.find('.appDocumentGrid');
        //const $auditHistoryGridControl = jQuery(FwBrowse.loadGridFromTemplate('AppDocumentGrid'));
        //$appDocumentGrid.empty().append($auditHistoryGridControl);
        //$auditHistoryGridControl.data('ondatabind', function(request) {
        //    request.module = 'AppDocumentGrid';
        //    request.uniqueid1 = uniqueid1;
        //    request.uniqueid2 = uniqueid2;
        //    FwServices.grid.method(request, request.module, 'Browse', $form, function(response) {
        //        var createdFlagIndetx, nameIndex, updateDateIndex;
        //        if (typeof response.browse.ColumnIndex['createdflag'] === 'undefined') {
        //            throw 'FwAudit.loadAudit: Error loading createdflag.';
        //        }
        //        if (typeof response.browse.ColumnIndex['name'] === 'undefined') {
        //            throw 'FwAudit.loadAudit: Error loading name.';
        //        }
        //        if (typeof response.browse.ColumnIndex['updatedate'] === 'undefined') {
        //            throw 'FwAudit.loadAudit: Error loading updatedate.';
        //        }
        //        createdFlagIndex = response.browse.ColumnIndex['createdflag'];
        //        nameIndex        = response.browse.ColumnIndex['name'];
        //        updateDateIndex  = response.browse.ColumnIndex['updatedate'];
        //        for (var i = (response.browse.Rows.length - 1); i >= 0; i--) {
        //            if (response.browse.Rows[i][createdFlagIndex] === "T") {
        //                var date = new Date(response.browse.Rows[i][updateDateIndex]);
        //                $form.find('.inputname input').val(response.browse.Rows[i][nameIndex]);
        //                $form.find('.inputdate input').val(date.toLocaleDateString() + ' ' + date.toLocaleTimeString());

        //                response.browse.Rows.splice(i, 1)
        //            }
        //        }

        //        FwBrowse.databindcallback($auditHistoryGridControl, response.browse);
        //    });
        //});
        //FwBrowse.init($auditHistoryGridControl);
        //FwBrowse.renderRuntimeHtml($auditHistoryGridControl);
        //FwBrowse.search($auditHistoryGridControl);
    }
    getTemplate() {
        return `<div data-name="AppDocumentGrid" data-control="FwBrowse"
  data-type="Grid" 
  class="fwcontrol fwbrowse" 
  data-caption="Customer Document" 
  data-datatable="appdocumentview" 
  data-version="1" 
  data-pageno="1" 
  data-pagesize="10" 
  data-rendermode="template" 
  data-orderby="" 
  data-showsearch="false"
  data-hasadd="true"
  data-hasedit="true"
  data-hasdelete="true"
  data-controller="CustomerDocumentGridController">
  <div class="column" data-width="0" data-visible="false">
    <div class="field"  
      data-cssclass="appdocumentid" 
      data-caption=""
      data-isuniqueid="true"
      data-browsedatafield="appdocumentid"
      data-browsedatatype="key" 
      data-formsaveorder="1"
      data-formdatafield="appdocument.appdocumentid"
      data-formdatatype="key"
      data-formreadonly="true"
      data-sort="off">
    </div>
    <div class="field"  
      data-isuniqueid="false"
      data-browsedatafield="uniqueid1"
      data-browsedatatype="key"
      data-formsaveorder="1"
      data-formdatafield="appdocument.uniqueid1"
      data-formdatatype="key"
      data-formreadonly="true"
      data-sort="off">
    </div>
    <!--<div class="field"  
      data-isuniqueid="false"
      data-browsedatafield="uniqueid2"
      data-browsedatatype="key" 
      data-formdatafield=""
      data-formdatatype="key"
      data-sort="off">
    </div>-->
    <div class="field"
      data-isuniqueid="false"
      data-browsedatafield="extension"
      data-browsedatatype="text"
      data-formdatafield=""
      data-formdatatype="text">
    </div>
  </div>
  <div class="column" data-width="100px" data-visible="true">
    <div class="field" 
      data-cssclass="documenttype" 
      data-caption="Document Type"
      data-isuniqueid="false"
      data-browsedatafield="documenttypeid"
      data-browsedisplayfield="documenttype"
      data-browsedatatype="validation"
      data-formvalidationname="DocumentTypeValidation"
      data-formsaveorder="1"
      data-formdatafield="appdocument.documenttypeid"
      data-formdatatype="validation"
      data-formreadonly="false"
      data-sort="off"
      data-formrequired="true">
    </div>
  </div>
  <div class="column" data-width="300px" data-visible="true">
    <div class="field" 
      data-cssclass="description" 
      data-caption="Description"
      data-isuniqueid="false"
      data-browsedatafield="description"
      data-browsedatatype="text"
      data-formsaveorder="1"
      data-formdatafield="appdocument.description"
      data-formdatatype="text"
      data-formreadonly="false"
      data-sort="off"
      data-formrequired="true">
    </div>
  </div>
  <div class="column" data-width="150px" data-visible="true">
    <div class="field" 
         data-cssclass="inputby" 
         data-caption="Input By"
         data-isuniqueid="false"
         data-browsedatafield="inputbyusersid"
         data-browsedisplayfield="inputby"
         data-browsedatatype="validation"
         data-formsaveorder="1"
         data-formdatafield="appdocument.inputbyusersid"
         data-formdatatype="validation"
         data-formreadonly="true"
         data-formvalidationname="WebUsersValidation"
         data-sort="off">
    </div>
  </div>
  <div class="column" data-width="80px" data-visible="true">
    <div class="field" 
      data-cssclass="attachdate" 
      data-caption="Date"
      data-isuniqueid="false"
      data-browsedatafield="attachdate"
      data-browsedatatype="date"
      data-formsaveorder="1"
      data-formdatafield=""
      data-formdatatype="date"
      data-formreadonly="true"
      data-sort="off">
    </div>
  </div>
  <div class="column" data-width="80px" data-visible="true">
    <div class="field" 
      data-cssclass="attachtime" 
      data-caption="Time"
      data-isuniqueid="false"
      data-browsedatafield="attachtime"
      data-browsedatatype="time12"
      data-formsaveorder="1"
      data-formdatafield=""
      data-formdatatype="text"
      data-formreadonly="true"
      data-sort="off">
    </div>
  </div>
  <div class="column" data-width="200px" data-visible="true">
    <div class="field"
      data-cssclass="file"
      data-caption="File"
      data-isuniqueid="false"
      data-browsedatafield="fileappimageid"
      data-browsedatatype="appdocumentimage"
      data-browseappdocumentidfield="appdocumentid"
      data-documenttypeidfield="documenttypeid"
      data-uniqueid1field="uniqueid1"
      data-uniqueid2field="uniqueid2"
      data-browsefilenamefield="documenttype"
      data-browsefileextensionfield="extension"
      data-formsaveorder="1"
      data-formdatafield=""
      data-formdatatype="appdocumentimage"
      data-formreadonly="false"
      data-miscfield="image"
      data-sort="off"
      data-showsearch="false">
    </div>
  </div>
  <div class="column" data-width="auto" data-visible="true"></div>
</div>`;
    }
    //---------------------------------------------------------------------------------
}

var FwAppDocumentGrid = new FwAppDocumentGridClass();