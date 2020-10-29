type FwAppDocumentGrid_RenderGridOptions = {
    
}

class FwAppDocumentGridClass {
    constructor() {

    }
    //----------------------------------------------------------------------------------------------
    renderGrid(options: {
        //$control: JQuery, 
        caption: string, 
        nameGrid: string, 
        parentFormDataFields: string, 
        getBaseApiUrl: () => string, 
        moduleSecurityId: string, 
        gridSecurityId: string, 
        $form: JQuery, 
        uniqueid1Name: string, 
        getUniqueid1Value: () => string,
        uniqueid2Name: string,
        getUniqueid2Value: () => string}) {
        // if a controller does not exist for the grid, then auto-generate one.
        const controller =  (window[options.nameGrid + 'Controller'] !== undefined) ? window[options.nameGrid + 'Controller'] : {
            Module: options.nameGrid,
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
            onDataBind: (request: any) => {
                request.uniqueids = {};
                if (typeof options.uniqueid1Name === 'string' && options.uniqueid1Name.length > 0) {
                    request.uniqueids[options.uniqueid1Name] = options.getUniqueid1Value();
                }
                if (typeof options.uniqueid2Name === 'string' && options.uniqueid2Name.length > 0) {
                    request.uniqueids[options.uniqueid2Name] = options.getUniqueid2Value();
                }
            },
            beforeSave: (request: any) => {
                //if (typeof options.uniqueid1Name === 'string' && options.uniqueid1Name.length > 0) {
                //    request[options.uniqueid1Name] = options.uniqueid1Value;
                //}
                //if (typeof options.uniqueid2Name === 'string' && options.uniqueid2Name.length > 0) {
                //    request[options.uniqueid2Name] = options.uniqueid2Value;
                //}
            },
            addGridMenu: (options) => {
                
            },
            getTemplate: () => {
                return `<div data-name="${options.nameGrid}" data-control="FwBrowse"
  data-parentformdatafields="${options.parentFormDataFields}"
  data-type="Grid" 
  class="fwcontrol fwbrowse" 
  data-caption="${options.caption}"  
  data-controller="${options.nameGrid}Controller"
  data-refreshaftersave="true"
  data-refreshaftercancel="true">
  <div class="column" data-width="0" data-visible="false">
    <div class="field"  
      data-cssclass="appdocumentid" 
      data-caption=""
      data-isuniqueid="true"
      data-browsedatafield="DocumentId"
      data-browsedatatype="key" 
      data-formsaveorder="1"
      data-formdatafield="DocumentId"
      data-formdatatype="key"
      data-formreadonly="true"
      data-sort="off">
    </div>
    <div class="field"  
      data-isuniqueid="false"
      data-browsedatafield="${options.uniqueid1Name}"
      data-browsedatatype="key"
      data-formdatafield=""
      data-formsaveorder="1"
      data-formdatatype="key"
      data-formreadonly="true"
      data-sort="off">
    </div>
    <!--<div class="field"  
      data-isuniqueid="false"
      data-browsedatafield="${options.uniqueid2Name}"
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
  <div class="column" data-width="100%" data-visible="true">
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
      data-cssclass="datestamp" 
      data-caption="Last Modified"
      data-isuniqueid="false"
      data-browsedatafield="DateStamp"
      data-browsedatatype="utcdatetime"
      data-formdatafield=""
      data-formdatatype="utcdatetime"
      data-formreadonly="true"
      data-sort="desc">
    </div>
  </div>
  <div class="column" data-width="40px" data-visible="true">
    <div class="field"
      data-cssclass="images"
      data-caption="Images"
      data-isuniqueid="false"
      data-browsedatafield="FileAppImageId"
      data-browsedatatype="documentimage"
      data-baseapiurl="${options.getBaseApiUrl()}"
      data-appdocumentidfield="DocumentId"
      data-hasimagesfield="HasImages"
      data-documenttypeidfield="DocumentTypeId"
      data-uniqueid1field="${options.uniqueid1Name}"
      data-uniqueid2field="${options.uniqueid2Name}"
      data-formdatafield="*"
      data-formdatatype="documentimage"
      data-formreadonly="false"
      data-miscfield="Image"
      data-sort="off"
      data-showsearch="false">
    </div>
  </div>
<div class="column" data-width="40px" data-visible="true">
    <div class="field"
      data-cssclass="file"
      data-caption="File"
      data-isuniqueid="false"
      data-browsedatafield="FileAppImageId"
      data-browsedatatype="documentfile"
      data-baseapiurl="${options.getBaseApiUrl()}"
      data-appdocumentidfield="DocumentId"
      data-hasfilefield="HasFile"
      data-filenamefield="Description"
      data-fileextensionfield="FileExtension"
      data-documenttypeidfield="DocumentTypeId"
      data-uniqueid1field="${options.uniqueid1Name}"
      data-uniqueid2field="${options.uniqueid2Name}"
      data-formdatafield="*"
      data-formdatatype="documentfile"
      data-formreadonly="false"
      data-miscfield="Image"
      data-sort="off"
      data-showsearch="false">
    </div>
  </div>
  <div class="column" data-width="auto" data-visible="true"></div>
</div>`;
            }
         });
    }
    //----------------------------------------------------------------------------------------------
}

var FwAppDocumentGrid = new FwAppDocumentGridClass();