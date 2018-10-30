class Contract {
    Module: string = 'Contract';
    apiurl: string = 'api/v1/contract';
    caption: string = 'Contract';
    nav: string = 'module/contract';
    id: string = '6BBB8A0A-53FA-4E1D-89B3-8B184B233DEA';
    ActiveView: string = 'ALL';

    getModuleScreen = () => {
        let screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse = () => {
        //let $browse;

        //$browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        const location = JSON.parse(sessionStorage.getItem('location'));
        this.ActiveView = 'LocationId=' + location.locationid;

        $browse.data('ondatabind', request => {
            request.activeview = this.ActiveView;
        });

        FwBrowse.addLegend($browse, 'Unassigned Items', '#FF0000');
        FwBrowse.addLegend($browse, 'Pending Exchanges', '#FFFF00');
        FwBrowse.addLegend($browse, 'Migrated', '#8080FF');
        FwBrowse.addLegend($browse, 'Inactive Deal', '#C0C0C0');
        FwBrowse.addLegend($browse, 'Truck (No Charge)', '#FFFF00');
        FwBrowse.addLegend($browse, 'Adjusted Billing Date', '#FF8080');
        FwBrowse.addLegend($browse, 'Voided Items', '#00FFFF');

        return $browse;
    }

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems = ($menuObject) => {
        let self = this;
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', function () {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        const viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($allLocations);
        let $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };

    openForm(mode: string) {
        //var $form;

        //$form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContractId"] input').val(uniqueids.ContractId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    renderGrids($form) {
        var $contractSummaryGrid;
        var $contractSummaryGridControl;
        $contractSummaryGrid = $form.find('div[data-grid="ContractSummaryGrid"]');
        $contractSummaryGridControl = jQuery(jQuery('#tmpl-grids-ContractSummaryGridBrowse').html());
        $contractSummaryGrid.empty().append($contractSummaryGridControl);
        $contractSummaryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            };
        });
        FwBrowse.init($contractSummaryGridControl);
        FwBrowse.renderRuntimeHtml($contractSummaryGridControl);

        var $contractRentalDetailGrid;
        var $contractRentalDetailGridControl;
        $contractRentalDetailGrid = $form.find('.rentaldetailgrid div[data-grid="ContractDetailGrid"]');
        $contractRentalDetailGridControl = jQuery(jQuery('#tmpl-grids-ContractDetailGridBrowse').html());
        $contractRentalDetailGrid.empty().append($contractRentalDetailGridControl);
        $contractRentalDetailGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                RecType: "R"
            };
        });
        FwBrowse.init($contractRentalDetailGridControl);
        FwBrowse.renderRuntimeHtml($contractRentalDetailGridControl);

        var $contractSalesDetailGrid;
        var $contractSalesDetailGridControl;
        $contractSalesDetailGrid = $form.find('.salesdetailgrid div[data-grid="ContractDetailGrid"]');
        $contractSalesDetailGridControl = jQuery(jQuery('#tmpl-grids-ContractDetailGridBrowse').html());
        $contractSalesDetailGrid.empty().append($contractSalesDetailGridControl);
        $contractSalesDetailGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                RecType: "S"
            };
        });
        FwBrowse.init($contractSalesDetailGridControl);
        FwBrowse.renderRuntimeHtml($contractSalesDetailGridControl);

        var $contractExchangeItemGrid;
        var $contractExchangeItemGridControl;
        $contractExchangeItemGrid = $form.find('div[data-grid="ContractExchangeItemGrid"]');
        $contractExchangeItemGridControl = jQuery(jQuery('#tmpl-grids-ContractExchangeItemGridBrowse').html());
        $contractExchangeItemGrid.empty().append($contractExchangeItemGridControl);
        $contractExchangeItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                RecType: "S"
            };
        });
        FwBrowse.init($contractExchangeItemGridControl);
        FwBrowse.renderRuntimeHtml($contractExchangeItemGridControl);
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="ContractId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $contractSummaryGrid;
        $contractSummaryGrid = $form.find('[data-name="ContractSummaryGrid"]');
        FwBrowse.search($contractSummaryGrid);

        var $contractRentalGrid;
        $contractRentalGrid = $form.find('.rentaldetailgrid [data-name="ContractDetailGrid"]');
        FwBrowse.search($contractRentalGrid);

        var $contractSalesGrid;
        $contractSalesGrid = $form.find('.salesdetailgrid [data-name="ContractDetailGrid"]');
        FwBrowse.search($contractSalesGrid);

        var $contractExchangeItemGrid;
        $contractExchangeItemGrid = $form.find('[data-name="ContractExchangeItemGrid"]');
        FwBrowse.search($contractExchangeItemGrid);

        var type = FwFormField.getValueByDataField($form, 'ContractType');
        var $billing = $form.find('[data-datafield="BillingDate"] .fwformfield-caption');

        switch (type) {
            case 'RECEIVE':
                $billing.html('Billing Start');
                break;
            case 'OUT':
                $billing.html('Billing Start');
                break;
            case 'IN':
                $billing.html('Billing Stop');
                break;
            case 'RETURN':
                $billing.html('Billing Stop');
                break;
            case 'LOST':
                $billing.html('Billing Stop');
                break;
            default:
                $billing.html('Billing Date');
                break;
        }

        let showSales = $form.find('[data-datafield="Sales"] input').val(),
            showRental = $form.find('[data-datafield="Rental"] input').val(),
            showExchange = $form.find('[data-datafield="Exchange"] input').val();

        if (showSales === 'false') {
            $form.find('[data-type="tab"][data-caption="Sales Detail"]').hide();
        }

        if (showRental === 'false') {
            $form.find('[data-type="tab"][data-caption="Rental Detail"]').hide();
        }

        if (showExchange === 'true') {
            $form.find('.summary').hide();
        } else {
            $form.find('.exchange').hide();
        }

        $form.find('.print').on('click', e => {
            let $report, contractNumber, contractId, recordTitle, printTab, module, hideModule;
            module = this.Module;
            try {
                contractNumber = $form.find(`div.fwformfield[data-datafield="${module}Number"] input`).val();
                contractId = $form.find(`div.fwformfield[data-datafield="${module}Id"] input`).val();
                recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
                $report = RwOutContractReportController.openForm();

                FwModule.openSubModuleTab($form, $report);

                $report.find(`div.fwformfield[data-datafield="${module}Id"] input`).val(contractId);
                $report.find(`div.fwformfield[data-datafield="${module}Id"] .fwformfield-text`).val(contractNumber);
                jQuery('.tab.submodule.active').find('.caption').html(`Print ${module}`);

                printTab = jQuery('.tab.submodule.active');
                printTab.find('.caption').html(`Print ${module}`);
                printTab.attr('data-caption', `${module} ${recordTitle}`);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------  
    getBrowseTemplate(): string {
      return `
      <div data-name="Contract" data-control="FwBrowse" data-type="Browse" id="ContractBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="ContractController" data-hasinactive="false">
        <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="true" data-datafield="ContractId" data-browsedatatype="key"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Contract No." data-datafield="ContractNumber" data-browsedatatype="text" data-sort="off" data-searchfieldoperators="startswith"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Type" data-datafield="ContractType" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Date" data-datafield="ContractDate" data-browsedatatype="date" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Time" data-datafield="ContractTime" data-browsedatatype="text" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Department" data-datafield="Department" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Vendor" data-datafield="Vendor" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Order Description" data-datafield="OrderDescription" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="PO No." data-datafield="PurchaseOrderNumber" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Billing Start/Stop" data-datafield="BillingDate" data-browsedatatype="date" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Warehouse" data-datafield="Warehouse" data-browsedatatype="text" data-sort="off"></div>
        </div>
         <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Location" data-datafield="Location" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column spacer" data-width="auto" data-visible="true"></div>
      </div>`;
  }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
      return `
      <div id="contractform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Contract" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ContractController">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="ContractId"></div>
        <div id="contractform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
            <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
            <div data-type="tab" id="rentaltab" class="tab" data-tabpageid="rentaltabpage" data-caption="Rental Detail"></div>
            <div data-type="tab" id="salestab" class="tab" data-tabpageid="salestabpage" data-caption="Sales Detail"></div>
          </div>
          <div class="tabpages">
            <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contract">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contract Number" data-datafield="ContractNumber" style="float:left;width:250px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="ContractDate" style="float:left;width:150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-datafield="ContractTime" style="float:left;width:100px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="OfficeLocationValidation" data-displayfield="Location" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" style="float:left;width:150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="WarehouseValidation" data-displayfield="Warehouse" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" style="float:left;width:150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="ContractType" style="float:left;width:200px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="DepartmentValidation" data-displayfield="Department" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" style="float:left;width:250px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Billing Start" data-datafield="BillingDate" style="float:left;width:150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="Sales" style="float:left;width:250px; display:none"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="float:left;width:250px; display:none"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Exchange" data-datafield="Exchange" style="float:left;width:250px; display:none"></div>
                    <div class="print fwformcontrol" data-type="button" style="flex:1 1 50px;margin:15px 0 0 10px;">Print</div>
                  </div>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Summary">
                  <div class="flexrow summary" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractSummaryGrid" data-securitycaption="Contract Summary"></div>
                  </div>
                  <div class="flexrow exchange" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractExchangeItemGrid" data-securitycaption="Contract Exchange Item"></div>
                  </div>
                </div>
              </div>
            </div>
            <div data-type="tabpage" id="rentaltabpage" class="tabpage" data-tabid="rentaltab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                  <div class="flexrow rentaldetailgrid" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractDetailGrid" data-securitycaption="Rental Detail"></div>
                  </div>
                </div>
              </div>
            </div>
            <div data-type="tabpage" id="salestabpage" class="tabpage" data-tabid="salestab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                  <div class="flexrow salesdetailgrid" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractDetailGrid" data-securitycaption="Sales Detail"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>`;
  }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{8C34754E-B27F-4FE1-93F3-8D6D84339322}'] = function (e) {
    var $form;
    try {
        $form = jQuery(this).closest('.fwform');
        $form.find('.print').trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------

var ContractController = new Contract();