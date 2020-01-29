class FwAppDocumentGridClass {
    constructor() {
    }
    init($control) {
    }
    renderRuntimeHtml($control) {
        $control.attr('data-rendermode', 'runtime');
    }
    renderGrid(options) {
        const controller = (window[options.nameGrid + 'Controller'] !== undefined) ? window[options.nameGrid + 'Controller'] : {
            Module: 'CustomerDocumentGrid',
            apiurl: ''
        };
        window[options.nameGrid + 'Controller'] = controller;
        FwBrowse.renderGrid({
            moduleSecurityId: options.moduleSecurityId,
            $form: options.$form,
            gridSecurityId: options.gridSecurityId,
            nameGrid: options.nameGrid,
            pageSize: 15,
            getBaseApiUrl: () => options.getBaseApiUrl(),
            onDataBind: (request) => {
                request.uniqueids = {
                    uniqueid1: FwFormField.getValueByDataField(options.$form, 'CustomerId')
                };
            },
            beforeSave: (request) => {
                request.uniqueid1 = FwFormField.getValueByDataField(options.$form, 'CustomerId');
            },
            addGridMenu: (options) => {
            },
            getTemplate: () => {
                return this.getTemplate(options.caption, options.nameGrid, options.parentFormDataFields);
            }
        });
    }
    getTemplate(caption, nameGrid, parentFormDataFields) {
        return `<div data-name="${nameGrid}" data-control="FwBrowse"
  data-parentformdatafields="${parentFormDataFields}"
  data-type="Grid" 
  class="fwcontrol fwbrowse" 
  data-caption="${caption}"  
  data-controller="${nameGrid}Controller">
  <div class="column" data-width="0" data-visible="false">
    <div class="field"  
      data-cssclass="appdocumentid" 
      data-caption=""
      data-isuniqueid="true"
      data-browsedatafield="AppDocumentId"
      data-browsedatatype="key" 
      data-formsaveorder="1"
      data-formdatafield="AppDocumentId"
      data-formdatatype="key"
      data-formreadonly="true"
      data-sort="off">
    </div>
    <div class="field"  
      data-isuniqueid="false"
      data-browsedatafield="UniqueId1"
      data-browsedatatype="key"
      data-formdatafield=""
      data-formsaveorder="1"
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
      data-browsedatafield="Extension"
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
      data-browsedatafield="DocumentTypeId"
      data-browsedisplayfield="DocumentType"
      data-browsedatatype="validation"
      data-formvalidationname="DocumentTypeValidation"
      data-formsaveorder="1"
      data-formdatafield="DocumentTypeId"
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
      data-browsedatafield="Description"
      data-browsedatatype="text"
      data-formsaveorder="1"
      data-formdatafield="Description"
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
         data-browsedatafield="InputByUsersId"
         data-browsedisplayfield="InputBy"
         data-browsedatatype="validation"
         data-formsaveorder="1"
         data-formdatafield="InputByUsersId"
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
      data-browsedatafield="AttachDate"
      data-browsedatatype="date"
      data-formsaveorder="1"
      data-formdatafield=""
      data-formdatatype="date"
      data-formreadonly="true"
      data-sort="desc">
    </div>
  </div>
  <div class="column" data-width="80px" data-visible="true">
    <div class="field" 
      data-cssclass="attachtime" 
      data-caption="Time"
      data-isuniqueid="false"
      data-browsedatafield="AttachTime"
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
      data-browsedatafield="FileAppImageId"
      data-browsedatatype="appdocumentimage"
      data-browseappdocumentidfield="AppDocumentId"
      data-documenttypeidfield="DocumentTypeId"
      data-uniqueid1field="UniqueId1"
      data-uniqueid2field="UniqueId2"
      data-browsefilenamefield="DocumentType"
      data-browsefileextensionfield="FileExtension"
      data-formsaveorder="1"
      data-formdatafield=""
      data-formdatatype="appdocumentimage"
      data-formreadonly="false"
      data-miscfield="Image"
      data-sort="off"
      data-showsearch="false">
    </div>
  </div>
  <div class="column" data-width="auto" data-visible="true"></div>
</div>`;
    }
}
var FwAppDocumentGrid = new FwAppDocumentGridClass();
//# sourceMappingURL=FwAppDocumentGrid.js.map