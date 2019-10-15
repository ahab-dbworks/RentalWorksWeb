class Repair {
    Module: string = 'Repair';
    apiurl: string = 'api/v1/repair';
    caption: string = Constants.Modules.Home.Repair.caption;
    nav: string = Constants.Modules.Home.Repair.nav;
    id: string = Constants.Modules.Home.Repair.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = (filter?: { datafield: string, search: string }) => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            // Dashboard search
            if (typeof filter !== 'undefined') {
                const datafields = filter.datafield.split('%20');
                for (let i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                const parsedSearch = filter.search.replace(/%20/g, " ").replace(/%2f/g, '/');
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(parsedSearch);
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        //let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
            const out = response.filter(item => item.StatusType === 'OUT');
            const intransit = response.filter(item => item.StatusType === 'INTRANSIT');

            FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
            FwBrowse.addLegend($browse, 'Priority', '#EA300F');
            FwBrowse.addLegend($browse, 'Not Billed', '#0fb70c');
            FwBrowse.addLegend($browse, 'Billable', '#0c6fcc');
            FwBrowse.addLegend($browse, 'Outside', '#fffb38');
            FwBrowse.addLegend($browse, 'Released', '#d6d319');
            FwBrowse.addLegend($browse, 'Pending Repair', out[0].Color);
            FwBrowse.addLegend($browse, 'Transferred', intransit[0].Color);
        }, null, $browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);
        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }
        const viewSubItems: Array<JQuery> = [];
        viewSubItems.push($userWarehouse, $all);
        FwMenu.addViewBtn($menuObject, 'Warehouse', viewSubItems, true, "WarehouseId");

        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm = (mode: string) => {
        //let $form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        $form.find('.warehouseid').hide();
        $form.find('.locationid').hide();
        $form.find('.inputbyuserid').hide();
        $form.find('.icodesales').hide();

        // Tax Option Validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-formdatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-formdatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-formdatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
        // Complete / Estimate
        $form.find('.complete').on('click', $tr => {
            this.completeOrder($form);
        });
        $form.find('.estimate').on('click', $tr => {
            this.estimateOrder($form);
        });
        // Release Items
        $form.find('.releaseitems').on('click', $tr => {
            this.releaseItems($form);
        });
        // New Orders
        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            $form.find('.completeestimate').hide();
            $form.find('.releasesection').hide();

            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValueByDataField($form, 'Priority', 'MED');
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'RepairDate', today);
            const office = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'Location', office.location);
            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid);
            FwFormField.setValueByDataField($form, 'Warehouse', warehouse.warehouse);
            FwFormField.setValueByDataField($form, 'Quantity', 1);
            const userId = JSON.parse(sessionStorage.getItem('userid'));
            FwFormField.setValueByDataField($form, 'InputByUserId', userId.webusersid);
            const locationId = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'LocationId', locationId.locationid);

            $form.find('div[data-datafield="PendingRepair"] input').prop('checked', false);
            $form.find('div[data-datafield="PoPending"] input').prop('checked', true);

            FwFormField.enable($form.find('div[data-displayfield="BarCode"]'));
            FwFormField.enable($form.find('div[data-displayfield="SerialNumber"]'));
            FwFormField.enable($form.find('div[data-displayfield="ICode"]'));
            FwFormField.enable($form.find('div[data-displayfield="RfId"]'));
            FwFormField.enable($form.find('div[data-displayfield="DamageOrderNumber"]'));
            FwFormField.enable($form.find('div[data-datafield="AvailFor"]'));
            FwFormField.enable($form.find('div[data-datafield="RepairType"]'));
            FwFormField.enable($form.find('div[data-datafield="PendingRepair"]'));

            // BarCode, SN, RFID Validation
            $form.find('div[data-datafield="ItemId"]').data('onchange', $tr => {
                FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]:visible', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
                FwFormField.setValue($form, 'div[data-displayfield="BarCode"]', $tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="BarCode"]').attr('data-originalvalue'));
                FwFormField.setValue($form, 'div[data-displayfield="ICode"]', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="ICode"]').attr('data-originalvalue'));
                FwFormField.setValue($form, 'div[data-displayfield="SerialNumber"] ', $tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="SerialNumber"]').attr('data-originalvalue'));
                FwFormField.setValue($form, 'div[data-displayfield="RfId"]', $tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="RfId"]').attr('data-originalvalue'));
                FwFormField.disable($form.find('div[data-displayfield="ICode"]'));
            });

            // ICode Validation
            $form.find('div[data-datafield="InventoryId"]').data('onchange', $tr => {
                FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]:visible', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
                FwFormField.enable($form.find('div[data-datafield="Quantity"]'));
                FwFormField.disable($form.find('div[data-displayfield="BarCode"]'));
                FwFormField.disable($form.find('div[data-displayfield="SerialNumber"]'));
                FwFormField.disable($form.find('div[data-displayfield="RfId"]'));

                if (FwFormField.getValue($form, '.repairavailforradio') === 'S') {
                    FwFormField.setValue($form, '.icoderental', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'));
                }
                else {
                    FwFormField.setValue($form, '.icodesales', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'));
                }
            });

            // Order Validation
            $form.find('div[data-datafield="DamageOrderId"]').data('onchange', $tr => {
                FwFormField.setValue($form, 'div[data-datafield="DamageOrderDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
                FwFormField.setValue($form, 'div[data-datafield="DamageDeal"]', $tr.find('.field[data-formdatafield="Deal"]').attr('data-originalvalue'));
            });

            // Sales or Rent Order
            $form.find('.repairavailforradio').on('change', $tr => {
                if (FwFormField.getValueByDataField($form, 'RepairType') === 'OWNED') {
                    if (FwFormField.getValue($form, '.repairavailforradio') === 'S') {
                        $form.find('.icodesales').show();
                        $form.find('.icoderental').hide();
                    }
                    else {
                        $form.find('.icodesales').hide();
                        $form.find('.icoderental').show();
                    }
                }
            });

            // Repair Type Change
            $form.find('.repairtyperadio').on('change', $tr => {
                if (FwFormField.getValueByDataField($form, 'RepairType') === 'OUTSIDE') {
                    $form.find('.itemid').hide();
                    $form.find('.icode').hide();
                    $form.find('.not-owned').show();
                    FwFormField.enable($form.find('.not-owned'));
                    FwFormField.disable($form.find('div[data-datafield="AvailFor"]'));
                } else {
                    $form.find('.itemid').show();
                    $form.find('.not-owned').hide();
                    FwFormField.enable($form.find('div[data-datafield="AvailFor"]'));
                    FwFormField.disable($form.find('.not-owned'));
                    if (FwFormField.getValue($form, '.repairavailforradio') === 'S') {
                        $form.find('.icodesales').show();
                        $form.find('.icoderental').hide();
                    }else {
                        $form.find('.icodesales').hide();
                        $form.find('.icoderental').show();
                    }
                }
            });

            // Pending PO Number
            $form.find('[data-datafield="PoPending"] .fwformfield-value').on('change', function () {
                const $this = jQuery(this);
                if ($this.prop('checked') === true) {
                    FwFormField.disable($form.find('div[data-datafield="PoNumber"]'));
                }
                else {
                    FwFormField.enable($form.find('div[data-datafield="PoNumber"]'));
                }
            });

            FwFormField.disable($form.find('.frame'));
            $form.find(".frame .add-on").children().hide();
        }

        this.events($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm = (uniqueids: any) => {
        let $form: JQuery = this.openForm('EDIT');
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="RepairId"] input').val(uniqueids.RepairId);
        FwModule.loadForm(this.Module, $form);

        $form.find('[data-datafield="PoPending"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('div[data-datafield="PoNumber"]'));
            }
            else {
                FwFormField.enable($form.find('div[data-datafield="PoNumber"]'));
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        // ----------
        const $repairCostGrid = $form.find('div[data-grid="RepairCostGrid"]');
        const $repairCostGridControl = FwBrowse.loadGridFromTemplate('RepairCostGrid');
        $repairCostGrid.empty().append($repairCostGridControl);
        $repairCostGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val()
            }
            request.totalfields = ["GrossTotal", "Tax", "Extended", "Total", "DiscountAmount"]
        });
        $repairCostGridControl.data('beforesave', request => {
            request.RepairId = FwFormField.getValueByDataField($form, 'RepairId');
        });
        FwBrowse.addEventHandler($repairCostGridControl, 'afterdatabindcallback', ($repairCostGridControl, response) => {
            FwFormField.setValue($form, 'div[data-totalfield="RepairCostTotal"]', response.Totals.Total);
            FwFormField.setValue($form, 'div[data-totalfield="RepairCostTax"]', response.Totals.Tax);
            FwFormField.setValue($form, 'div[data-totalfield="RepairCostExtended"]', response.Totals.Extended);
            FwFormField.setValue($form, 'div[data-totalfield="RepairCostGrossTotal"]', response.Totals.GrossTotal);
            FwFormField.setValue($form, 'div[data-totalfield="RepairCostDiscount"]', response.Totals.DiscountAmount);
        });
        FwBrowse.init($repairCostGridControl);
        FwBrowse.renderRuntimeHtml($repairCostGridControl);
        // ----------
        const $repairPartGrid = $form.find('div[data-grid="RepairPartGrid"]');
        const $repairPartGridControl = FwBrowse.loadGridFromTemplate('RepairPartGrid');
        $repairPartGrid.empty().append($repairPartGridControl);
        $repairPartGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val()
            }
            request.totalfields = ["GrossTotal", "Tax", "Extended", "Total", "DiscountAmount"]
        });
        $repairPartGridControl.data('beforesave', request => {
            request.RepairId = FwFormField.getValueByDataField($form, 'RepairId');
        });
        FwBrowse.addEventHandler($repairPartGridControl, 'afterdatabindcallback', ($repairPartGridControl, response) => {
            FwFormField.setValue($form, 'div[data-totalfield="RepairPartTotal"]', response.Totals.Total);
            FwFormField.setValue($form, 'div[data-totalfield="RepairPartTax"]', response.Totals.Tax);
            FwFormField.setValue($form, 'div[data-totalfield="RepairPartExtended"]', response.Totals.Extended);
            FwFormField.setValue($form, 'div[data-totalfield="RepairPartGrossTotal"]', response.Totals.GrossTotal);
            FwFormField.setValue($form, 'div[data-totalfield="RepairPartDiscount"]', response.Totals.DiscountAmount);
        });
        FwBrowse.init($repairPartGridControl);
        FwBrowse.renderRuntimeHtml($repairPartGridControl);
        // ----------
        const $repairReleaseGrid = $form.find('div[data-grid="RepairReleaseGrid"]');
        const $repairReleaseGridControl = FwBrowse.loadGridFromTemplate('RepairReleaseGrid');
        $repairReleaseGrid.empty().append($repairReleaseGridControl);
        $repairReleaseGridControl.data('ondatabind', request => {
            request.uniqueids = {
                RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val()
            }
        });
        $repairReleaseGridControl.data('beforesave', request => {
            request.RepairId = FwFormField.getValueByDataField($form, 'RepairId');
        });
        FwBrowse.init($repairReleaseGridControl);
        FwBrowse.renderRuntimeHtml($repairReleaseGridControl);
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
        $form.find('.completeestimate').show();
        $form.find('.releasesection').show();
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
        let $repairCostGrid: any = $form.find('[data-name="RepairCostGrid"]');
        FwBrowse.search($repairCostGrid);
        let $repairPartGrid: any = $form.find('[data-name="RepairPartGrid"]');
        FwBrowse.search($repairPartGrid);
        let $repairReleaseGrid: any = $form.find('[data-name="RepairReleaseGrid"]');
        FwBrowse.search($repairReleaseGrid);

        if (FwFormField.getValueByDataField($form, 'Status') === 'ESTIMATED') {
            $form.data('hasEstimated', true);
        } else {
            $form.data('hasEstimated', false);
        }

        if (FwFormField.getValueByDataField($form, 'Status') === 'COMPLETE') {
            $form.data('hasCompleted', true);
        } else {
            $form.data('hasCompleted', false);
        }

        const $pending = $form.find('div.fwformfield[data-datafield="PoPending"] input').prop('checked');
        if ($pending === true) {
            FwFormField.disable($form.find('div[data-datafield="PoNumber"]'));
        }
        else {
            FwFormField.enable($form.find('div[data-datafield="PoNumber"]'));
        }

        FwFormField.disable($form.find('div[data-displayfield="BarCode"]'));
        FwFormField.disable($form.find('div[data-displayfield="SerialNumber"]'));
        FwFormField.disable($form.find('div[data-displayfield="ICode"]'));
        FwFormField.disable($form.find('div[data-displayfield="RfId"]'));
        FwFormField.disable($form.find('div[data-displayfield="DamageOrderNumber"]'));
        FwFormField.disable($form.find('div[data-datafield="AvailFor"]'));
        FwFormField.disable($form.find('div[data-datafield="RepairType"]'));
        FwFormField.disable($form.find('div[data-datafield="PendingRepair"]'));

 
        if (FwFormField.getValueByDataField($form, 'RepairType') === 'OUTSIDE') {
            $form.find('.itemid').hide();
            $form.find('.icode').hide();
            $form.find('.not-owned').show();
            FwFormField.enable($form.find('.not-owned'));
            FwFormField.disable($form.find('div[data-datafield="AvailFor"]'));
        } else {
            $form.find('.itemid').show();
            $form.find('.not-owned').hide();
            FwFormField.enable($form.find('div[data-datafield="AvailFor"]'));
            FwFormField.disable($form.find('.not-owned'));
            if (FwFormField.getValue($form, '.repairavailforradio') === 'S') {
                $form.find('.icodesales').show();
                $form.find('.icoderental').hide();
            } else {
                $form.find('.icodesales').hide();
                $form.find('.icoderental').show();
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Repair" data-control="FwBrowse" data-type="Browse" id="RepairBrowse" class="fwcontrol fwbrowse" data-orderby="RepairId" data-controller="RepairController" data-hasinactive="true">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="RepairId" data-browsedatatype="key" ></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="Repair No." data-datafield="RepairNumber" data-browsedatatype="text" data-cellcolor="RepairNumberColor" data-sort="desc" data-sortsequence="2" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Date" data-datafield="RepairDate" data-browsedatatype="date" data-sortsequence="1" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Bar Code" data-datafield="BarCode" data-browsedatatype="text" data-cellcolor="BarCodeColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Serial Number" data-datafield="SerialNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="RFID" data-datafield="RfId" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="150px" data-visible="true">
            <div class="field" data-caption="I-Code" data-datafield="ICode" data-browsedatatype="text" data-cellcolor="ICodeColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="450px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="ItemDescription" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="125px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-browsedatatype="text" data-cellcolor="StatusColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Quantity" data-datafield="Quantity" data-browsedatatype="number" data-cellcolor="QuantityColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="As of Date" data-datafield="StatusDate" data-browsedatatype="date" data-sort="off"></div>
          </div>
           <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="Warehouse" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="Type" data-datafield="AvailForDisplay" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="200px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="DamageDeal" data-browsedatatype="text" data-cellcolor="DamageDealColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="PO No." data-datafield="PoNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="Priority" data-datafield="PriorityDescription" data-browsedatatype="text" data-cellcolor="PriorityColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Quantity Released" data-datafield="ReleasedQuantity" data-browsedatatype="number" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true"></div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="repairform" class="fwcontrol fwcontainer fwform flexpage" data-control="FwContainer" data-type="form" data-version="1" data-caption="Repair Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RepairController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield flexbox" data-isuniqueid="true" data-saveorder="1" data-caption="Order No." data-datafield="RepairId"></div>
          <div id="orderform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs flexbox">
              <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="Repair Ticket"></div>
              <div data-type="tab" id="damagetab" class="tab" data-tabpageid="damagetabpage" data-caption="Damage/Correction"></div>
              <div data-type="tab" id="costtab" class="tab" data-tabpageid="costtabpage" data-caption="Costs"></div>
              <div data-type="tab" id="partstab" class="tab" data-tabpageid="partstabpage" data-caption="Parts"></div>
              <div data-type="tab" id="chargetab" class="tab" data-tabpageid="chargetabpage" data-caption="Charge"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair Ticket">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Repair Number" data-datafield="RepairNumber" data-enabled="false"  style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="RepairDate"  data-enabled="false"             style="flex:1 1 115px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield repairavailforradio" data-caption="Available For" data-datafield="AvailFor" data-enabled="false" style="flex:1 1 115px;">
                            <div data-value="R" data-caption="Rent"></div>
                            <div data-value="S" data-caption="Sell"></div>
                          </div>
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield repairtyperadio" data-caption="Type" data-datafield="RepairType" data-enabled="false" style="flex:1 1 115px;">
                            <div data-value="OWNED" data-caption="Owned"></div>
                            <div data-value="OUTSIDE" data-caption="Not Owned"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 525px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield icoderental icode" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-enabled="false" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-validationname="RentalInventoryValidation" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield icodesales icode" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-enabled="false" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-validationname="SalesInventoryValidation" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield not-owned" data-caption="Item Description" data-datafield="ItemDescription" data-enabled="false" style="flex:1 1 300px; display:none;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield itemid" data-caption="Item Description" data-datafield="ItemDescription" data-enabled="false" style="flex:1 1 300px;"></div>                       
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield itemid" data-caption="Quantity" data-datafield="Quantity" data-enabled="false" style="flex:1 1 50px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Bar Code" data-datafield="ItemId" data-displayfield="BarCode" data-enabled="false" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-validationname="AssetValidation" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Serial Number" data-datafield="ItemId" data-displayfield="SerialNumber" data-enabled="false" data-validationpeek="true" data-validationname="AssetValidation" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="RFID" data-datafield="ItemId" data-displayfield="RfId" data-enabled="false" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-validationname="AssetValidation" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 285px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ticket Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As Of Date" data-datafield="StatusDate" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending Repair" data-datafield="PendingRepair" data-enabled="false" style="flex:1 1 125px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Due Date" data-datafield="DueDate" data-enabled="true" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 775px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location / Warehouse / Department">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-enabled="false"   style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Office" data-datafield="Location" data-enabled="false"       style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 200px;"></div>
                          <!--Hidden Fields-->
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield locationid" data-caption="" data-datafield="LocationId" data-enabled="false" data-visible="false" style="float:left;width:0px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield warehouseid" data-caption="" data-datafield="BillingWarehouseId" data-enabled="false" data-visible="false" style="float:left;width:0px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield inputbyuserid" data-caption="" data-datafield="InputByUserId" data-enabled="false" data-visible="false" style="float:left;width:0px;"></div>
                        </div>
                     </div>
                   </div>
                    <div class="flexcolumn" style="flex:1 1 125px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Option">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Billable" data-datafield="Billable" style="flex:1 1 75px;margin-top:10px;"></div>
                        </div>
                      </div>
                   </div>
                   <div class="flexcolumn" style="flex:1 1 125px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Priority">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield repairtyperadio" data-caption="" data-datafield="Priority">
                        <div data-value="HIG" data-caption="High"></div>
                        <div data-value="MED" data-caption="Medium"></div>
                        <div data-value="LOW" data-caption="Low"></div>
                      </div>
                    </div>
                   </div>
                 </div>
                 <!-- Last Order / Billable Repair / Item Status -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 600px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Last Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order Number" data-datafield="DamageOrderId" data-displayfield="DamageOrderNumber" data-required="false" data-enabled="false" data-validationname="OrderValidation" style="flex:1 1 136px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Description" data-datafield="DamageOrderDescription" data-enabled="false" style="flex:1 1 232px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DamageDeal" data-enabled="false" style="flex:1 1 232px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Contract Number" data-datafield="DamageContractId" data-displayfield="DamageContractNumber" data-required="false" data-enabled="false" data-validationname="ContractValidation" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Contract Date" data-datafield="DamageContractDate" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Scanned By" data-datafield="DamageScannedBy" data-enabled="false" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Loss and Damage Order Number" data-datafield="LossAndDamageOrderId" data-displayfield="LossAndDamageOrderNumber" data-required="false" data-enabled="false" data-validationname="OrderValidation" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Loss and Damage Order Description" data-datafield="LossAndDamageOrderDescription" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase Order">
                        <div class="flexrow">
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending PO Number" data-datafield="PoPending" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield PoNumber" data-caption="Authorized PO Number" data-datafield="PoNumber" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Outside Repair" data-datafield="OutsideRepair" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RepairItemStatus" data-caption="Item Status" data-datafield="RepairItemStatusId" data-displayfield="RepairItemStatus" data-enabled="true" data-validationname="RepairItemStatusValidation" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!--Estimate / Complete-->
                    <div class="flexcolumn completeestimate"  style="flex:1 1 600px;">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Create Estimate">
                          <div class="flexrow">
                            <div class="fwformcontrol estimate" data-type="button" style="margin:16px 0 0 0;flex:0 1 75px">Estimate</div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="EstimateDate" data-enabled="false"        style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Estimated By" data-datafield="EstimateBy" data-enabled="false"  style="flex:1 1 250px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Complete Repair">
                          <div class="flexrow">
                            <div class="fwformcontrol complete" data-type="button" style="margin:16px 0 0 0;flex:0 1 75px;">Complete</div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="CompleteDate" data-enabled="false"        style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Completed By" data-datafield="CompleteBy" data-enabled="false"  style="flex:1 1 250px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>

                    <!--Release Grid-->
                    <div class="flexcolumn releasesection" style="flex:1 1 500px;">
                    <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Releases">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield releasetotals" data-caption="Total Released" data-datafield="ReleasedQuantity" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div class="fwformcontrol releaseitems" data-type="button" style="margin:16px 0 0 0;flex:0 1 75px;">Release</div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwGrid" id="RepairReleaseGrid" data-grid="RepairReleaseGrid" data-caption="" data-securitycaption="RepairReleaseGrid" style="flex:1 1 300px;margin-top:5px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!--Damage / Correction Tab-->
            <div data-type="tabpage" id="damagetabpage" class="tabpage" data-tabid="damagetab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Damage Information">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="Damage" data-height="500px"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Correction Information">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="Correction" data-height="500px"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!--Cost Tab-->
            <div data-type="tabpage" id="costtabpage" class="tabpage" data-tabid="costtab">
              <div class="formpage">
                <div class="formrow" style="width:100%;">
                  <div class="formrow costgrid" style="width:100%;">
                    <div data-control="FwGrid" id="RepairCostGrid" data-grid="RepairCostGrid" data-caption="Costs" data-securitycaption="RepairCostGrid" style="min-width:240px;"></div>
                  </div>
                </div>
                  <div class="formrow" style="display: flex; justify-content: flex-end;">
                    <div class="formcolumn costtotals" style="width:auto;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield=""  data-enabled="false" data-totalfield="RepairCostGrossTotal" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="RepairCostDiscount" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Extended" data-datafield="" data-enabled="false" data-totalfield="RepairCostExtended" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="RepairCostTax" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="RepairCostTotal" style="max-width:125px; float:left;"></div>
                    </div>
                  </div>
              </div>
            </div>
            <!--Parts Tab-->
            <div data-type="tabpage" id="partstabpage" class="tabpage" data-tabid="partstab">
              <div class="formpage">
                <div class="formrow" style="width:100%;">
                  <div class="formrow partgrid" style="width:100%;">
                    <div data-control="FwGrid" id="RepairPartGrid" data-grid="RepairPartGrid" data-caption="Parts" data-securitycaption="RepairPartGrid" style="min-width:240px;"></div>
                  </div>
                </div>
                  <div class="formrow" style="display: flex; justify-content: flex-end;">
                    <div class="formcolumn parttotals" style="width:auto;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="RepairPartGrossTotal" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="RepairPartDiscount" style="max-width:125px; float:left"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Extended" data-datafield="" data-enabled="false" data-totalfield="RepairPartExtended" style="max-width:125px; float:left"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="RepairPartTax" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="RepairPartTotal" style="max-width:125px; float:left;"></div>
                    </div>
                  </div>
              </div>
            </div>
            <!--Charge Tab-->
            <div data-type="tabpage" id="chargetabpage" class="tabpage" data-tabid="chargetab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn" style="float:left;max-width:450px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Location">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" style="float:left;max-width:200px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;max-width:200px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="float:left;max-width:535px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billed Order and Invoice">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Number" data-datafield="ChargeOrderNumber" data-enabled="false" style="float:left;width:200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ChargeOrderDescription" data-enabled="false" style="float:left;width:300px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Invoice Number" data-datafield="ChargeInvoiceNumber" data-enabled="false" style="float:left;width:200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ChargeInvoiceDescription" data-enabled="false" style="float:left;width:300px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="float:left;max-width:400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax and Currency">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" data-validationname="CurrencyValidation" style="float:left;max-width:150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Tax Option" data-datafield="TaxOptionId" data-displayfield="TaxOption" data-validationname="TaxOptionValidation" style="float:left;max-width:450px;"></div>
                        </div>
                      </div>
                    </div>
                      <div class="flexcolumn" style="max-width:200px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalTaxRate1" data-enabled="false" style="width:125px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield SalesTaxRate1" data-caption="Sales" data-datafield="SalesTaxRate1" data-enabled="false" style="width:125px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTaxRate1" data-enabled="false" style="width:125px;"></div>
                        </div>
                        </div>
                      </div>
                  </div>
              </div>
            </div>
            <!--Notes Tab-->
            <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-maxlength="255" data-caption="" data-datafield="Notes"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            </div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        // Sales or Rent Order
        $form.find('.repairavailforradio').on('change', $tr => {
            if (FwFormField.getValueByDataField($form, 'RepairType') === 'OWNED') {
                if (FwFormField.getValue($form, '.repairavailforradio') === 'S') {
                    $form.find('.icodesales').show();
                    $form.find('.icoderental').hide();
                }
                else {
                    $form.find('.icodesales').hide();
                    $form.find('.icoderental').show();
                }
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    estimateOrder($form: JQuery): void {
        let $yes, $no;
        const $confirmation = FwConfirmation.renderConfirmation('Estimate', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        const html: Array<string> = [];

        if ($form.data('hasCompleted') === true) {
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>This Repair Order has already been completed and cannot be unestimated.</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            $no = FwConfirmation.addButton($confirmation, 'OK');
        }

        else if ($form.data('hasEstimated') === true) {
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Cancel this estimate for this Repair Order?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            $yes = FwConfirmation.addButton($confirmation, 'Cancel Estimate', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', cancelEstimate);
        } else {
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Make an estimate for this Repair Order?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            $yes = FwConfirmation.addButton($confirmation, 'Estimate', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeEstimate);
        }
        // ----------
        function makeEstimate() {
            $form.data('hasEstimated', true);

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);

            $yes.text('Estimating...');
            $yes.off('click');
            const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
            const blockConfirmation = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

            const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
            FwAppData.apiMethod(true, 'POST', `api/v1/repair/estimate/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success) {
                    FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Estimated');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, RepairController);
                }
                else {
                    $yes.on('click', makeEstimate);
                    $yes.text('Estimate');
                    FwFunc.showError(response.msg);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                }
            }, function onError(response) {
                $yes.on('click', makeEstimate);
                $yes.text('Estimate');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, RepairController);
            }, blockConfirmation);
        };
        // ----------
        function cancelEstimate() {
            $form.data('hasEstimated', false)

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);

            $yes.text('Canceling Estimate...');
            $yes.off('click');
            const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
            const blockConfirmation = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);
            const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
            FwAppData.apiMethod(true, 'POST', `api/v1/repair/estimate/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success) {
                    FwNotification.renderNotification('SUCCESS', 'Estimate Successfully Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, RepairController);
                }
                else {
                    $yes.on('click', cancelEstimate);
                    $yes.text('Cancel Estimate');
                    FwFunc.showError(response.msg);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                }
            }, function onError(response) {
                $yes.on('click', cancelEstimate);
                $yes.text('Cancel Estimate');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, RepairController);
            }, blockConfirmation);
        };
    };
    //----------------------------------------------------------------------------------------------
    completeOrder($form: JQuery): void {
        let $yes, $no;
        const $confirmation = FwConfirmation.renderConfirmation('Complete', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        const html: Array<string> = [];

        if ($form.data('hasCompleted') === true) {
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>This Repair Order has already been completed.</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            $no = FwConfirmation.addButton($confirmation, 'OK');
        } else if ($form.data('hasEstimated') === true) {
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Complete this Repair Order?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            $yes = FwConfirmation.addButton($confirmation, 'Complete', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeComplete);
        } else {
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Not yet estimated. Make estimate and complete this Repair Order?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            $yes = FwConfirmation.addButton($confirmation, 'Complete', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeComplete);
        }
        // ----------
        function makeComplete() {
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Completing...');
            $yes.off('click');
            const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
            const blockConfirmation = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

            const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
            FwAppData.apiMethod(true, 'POST', `api/v1/repair/complete/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success) {
                    FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Completed');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, RepairController);
                    $form.data('hasCompleted', true);
                }
                else {
                    $yes.on('click', makeComplete);
                    $yes.text('Complete');
                    FwFunc.showError(response.msg);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                }
            }, function onError(response) {
                $yes.on('click', makeComplete);
                $yes.text('Complete');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, RepairController);
                $form.data('hasCompleted', true);
            }, blockConfirmation);
        };
    };
    //----------------------------------------------------------------------------------------------
    voidOrder($form: JQuery): void {
        const status = FwFormField.getValueByDataField($form, 'Status');
        let $confirmation, $yes, $no;
        if (status !== 'COMPLETE') {
            $confirmation = FwConfirmation.renderConfirmation('Void', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            const html: Array<string> = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Void this Repair Order?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            $yes = FwConfirmation.addButton($confirmation, 'Void', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeVoid);
        } else if (status === 'COMPLETE') {
            FwNotification.renderNotification('WARNING', 'COMPLETE Repair Orders cannot be VOIDED.');
        }
        // ----------
        function makeVoid() {

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Voiding...');
            $yes.off('click');
            const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
            const blockConfirmation = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

            const repairId = FwFormField.getValueByDataField($form, 'RepairId');
            FwAppData.apiMethod(true, 'POST', `api/v1/repair/void/${repairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success) {
                    FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Voided');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, RepairController);
                }
                else {
                    $yes.on('click', makeVoid);
                    $yes.text('Void');
                    FwFunc.showError(response.msg);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                }
            }, function onError(response) {
                $yes.on('click', makeVoid);
                $yes.text('Void');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, RepairController);
            }, blockConfirmation);
        };
    };
    //----------------------------------------------------------------------------------------------
    releaseItems($form: JQuery): void {
        const releasedQuantityForm = +FwFormField.getValueByDataField($form, 'ReleasedQuantity');
        const quantityForm = +FwFormField.getValueByDataField($form, 'Quantity');
        const totalQuantity = quantityForm - releasedQuantityForm;
        const $confirmation = FwConfirmation.renderConfirmation('Release Items', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        const $yes = FwConfirmation.addButton($confirmation, 'Release', false);
        let $no = FwConfirmation.addButton($confirmation, 'Cancel');
        const html: Array<string> = [];
        if (quantityForm > releasedQuantityForm && quantityForm > 0) {
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" style="width:90px;float:left;"></div>');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ItemDescription" style="width:340px; float:left;"></div>');
            html.push('  </div>');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" style="width:75px; float:left;"></div>');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Released" data-datafield="Released" style="width:75px;float:left;"></div>');
            html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity to Release" data-datafield="ReleasedQuantity" data-enabled="true" style="width:75px;float:left;"></div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            const ICode = $form.find('[data-datafield="InventoryId"] input.fwformfield-text').val();
            $confirmation.find('div[data-caption="I-Code"] input').val(ICode);

            const ItemDescription = FwFormField.getValueByDataField($form, 'ItemDescription');
            $confirmation.find('div[data-caption="Description"] input').val(ItemDescription);

            const Quantity = +FwFormField.getValueByDataField($form, 'Quantity');
            $confirmation.find('div[data-caption="Quantity"] input').val(Quantity);

            const ReleasedQuantity = +FwFormField.getValueByDataField($form, 'ReleasedQuantity');
            $confirmation.find('div[data-caption="Released"] input').val(ReleasedQuantity);

            const QuantityToRelease = Quantity - ReleasedQuantity;
            $confirmation.find('div[data-caption="Quantity to Release"] input').val(QuantityToRelease);

            FwFormField.disable($confirmation.find('div[data-caption="I-Code"]'));
            FwFormField.disable($confirmation.find('div[data-caption="Description"]'));
            FwFormField.disable($confirmation.find('div[data-caption="Quantity"]'));
            FwFormField.disable($confirmation.find('div[data-caption="Released"]'));

            $yes.on('click', release);
        } else {
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>There are either no items to release or number chosen is greater than items available.</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            $no = FwConfirmation.addButton($confirmation, 'OK');
        }
        // ----------
        function release() {
            const releasedQuantityConfirmation = +FwFormField.getValueByDataField($confirmation, 'ReleasedQuantity');

            if (releasedQuantityConfirmation <= totalQuantity) {
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Releasing...');
                $yes.off('click');
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const blockConfirmation = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);
                const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
                FwAppData.apiMethod(true, 'POST', `api/v1/repair/releaseitems/${RepairId}/${releasedQuantityConfirmation}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Items Successfully Released');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form, RepairController);
                }, function onError(response) {
                    $yes.on('click', release);
                    $yes.text('Release');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                }, blockConfirmation);
            } else {
                FwFunc.showError("You are attempting to release more items than are available.");
            }
        };
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        switch (validationName) {
            case 'AssetValidation':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid
                };
                break;
            case 'RentalInventoryValidation':
                request.uniqueids = {
                    Classification: 'I',
                    TrackedBy: 'QUANTITY',
                };
                break;
            case 'SalesInventoryValidation':
                request.uniqueids = {
                    Classification: 'I',
                    TrackedBy: 'QUANTITY',
                };
                break;
        };
    }
};
// using COMPLETE security guid
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.form.menuItems.Complete.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        RepairController.completeOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
// using ESTIMATE security guid
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.form.menuItems.Estimate.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        RepairController.estimateOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
// using VOID security guid
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.form.menuItems.Void.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        RepairController.voidOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
// using RELEASE security guid
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.form.menuItems.ReleaseItems.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(this).closest('.fwform');
        RepairController.releaseItems($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
//Browse Void Option
FwApplicationTree.clickEvents[Constants.Modules.Home.Repair.browse.menuItems.Void.id] = function (event: JQuery.ClickEvent) {
    try {
        const $browse = jQuery(this).closest('.fwbrowse');
        const RepairId = $browse.find('.selected [data-browsedatafield="RepairId"]').attr('data-originalvalue');
        if (RepairId != null) {
            const $confirmation = FwConfirmation.renderConfirmation('Void', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            const html: Array<string> = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Void this Repair Order?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            const $yes = FwConfirmation.addButton($confirmation, 'Void', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', makeVoid);
            // ----------
            function makeVoid() {
                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Voiding...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/repair/void/${RepairId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Voided');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', makeVoid);
                    $yes.text('Void');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };
        } else {
            FwNotification.renderNotification('WARNING', 'Select a Repair Order to void.');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//------------------------------------------------------------------------------------------------
var RepairController = new Repair();