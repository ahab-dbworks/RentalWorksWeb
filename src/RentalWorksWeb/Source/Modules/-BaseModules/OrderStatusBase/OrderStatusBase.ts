﻿abstract class OrderStatusBase {
    Module: string;
    caption: string;
    nav: string;
    id: string;
    Type: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.getOrder($form);
        this.toggleView($form);

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, `${this.Type}Id`, parentmoduleinfo.OrderId, parentmoduleinfo.OrderNumber);
            $form.find(`[data-datafield="${this.Type}Id"]`).change();
        }

        $form.find('.rentalview').hide();
        $form.find('.salesview').hide();

        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield=""]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
        });
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getOrder($form: JQuery): void {
        const max = 9999;
        $form.on('change', `[data-datafield="${this.Type}Id"]`, () => {
            try {
                $form.find('.toggle [data-value="Summary"] input').prop('checked', true);
                $form.find('.summaryview').show();
                const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                let apiUrl;
                switch (this.Type) {
                    case 'Order':
                        apiUrl = `api/v1/order/${orderId}`;
                        break;
                    case 'Transfer':
                        apiUrl = `api/v1/transferorder/${orderId}`;
                        break;
                    case 'Item':
                        apiUrl = `api/v1/containeritem/${orderId}`;
                        break;
                }
                FwAppData.apiMethod(true, 'GET', apiUrl, null, FwServices.defaultTimeout, response => {
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Status', response.Status);
                    FwFormField.setValueByDataField($form, 'PickDate', response.PickDate);
                    FwFormField.setValueByDataField($form, 'PickTime', response.PickTime);

                    if (this.Type === 'Order') {
                        FwFormField.setValueByDataField($form, 'Deal', response.Deal);
                        FwFormField.setValueByDataField($form, 'Warehouse', response.Warehouse);
                        FwFormField.setValueByDataField($form, 'EstimatedStartDate', response.EstimatedStartDate);
                        FwFormField.setValueByDataField($form, 'EstimatedStartTime', response.EstimatedStartTime);
                        FwFormField.setValueByDataField($form, 'EstimatedStopDate', response.EstimatedStopDate);
                        FwFormField.setValueByDataField($form, 'EstimatedStopTime', response.EstimatedStopTime);
                    }
                    const rental = response.Rental;
                    const sales = response.Sales;
                    if (rental === false && sales === false) {
                        $form.find('div[data-value="Details"]').hide();
                    } else {
                        $form.find('div[data-value="Details"]').show();
                    }

                    if (rental === true) {
                        $form.find('.rentalview').show();
                    } else {
                        $form.find('.rentalview').hide();
                    }

                    if (sales === true) {
                        $form.find('.salesview').show();
                    } else {
                        $form.find('.salesview').hide();
                    }

                    $form.find('.details').hide();
                }, null, $form);

                const $orderStatusSummaryGridControl = $form.find('[data-name="OrderStatusSummaryGrid"]');
                $orderStatusSummaryGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId
                    }
                    request.pagesize = max;
                })
                FwBrowse.search($orderStatusSummaryGridControl);

                const $orderStatusRentalDetailGridControl = $form.find('[data-name="OrderStatusRentalDetailGrid"]');
                $orderStatusRentalDetailGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId,
                        RecType: "R"
                    }
                    request.pagesize = max;
                })
                FwBrowse.search($orderStatusRentalDetailGridControl);

                const $orderStatusSalesDetailGridControl = $form.find('[data-name="OrderStatusSalesDetailGrid"]');
                $orderStatusSalesDetailGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId,
                        RecType: "S"
                    }
                    request.pagesize = max;
                })
                FwBrowse.search($orderStatusSalesDetailGridControl);

                setTimeout(function () {
                    var $trs = $form.find('.ordersummarygrid tr.viewmode');

                    var $contractpeek = $form.find('.outcontract, .incontract');
                    $contractpeek.attr('data-browsedatafield', 'ContractId');

                    for (var i = 0; i <= $trs.length; i++) {
                        var $rectype = jQuery($trs[i]).find('[data-browsedatafield="RecTypeDisplay"]');
                        var recvalue = $rectype.attr('data-originalvalue');
                        var $validationfield = jQuery($trs[i]).find('[data-browsedatafield="InventoryId"]');

                        switch (recvalue) {
                            case 'RENTAL':
                                $validationfield.attr('data-validationname', 'RentalInventoryValidation');
                                break;
                            case 'SALES':
                                $validationfield.attr('data-validationname', 'SalesInventoryValidation');
                                break;
                        }
                    }
                }, 2000);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const max = 9999;
        //----------------------------------------------------------------------------------------------
        const $orderStatusSummaryGrid = $form.find('div[data-grid="OrderStatusSummaryGrid"]');
        const $orderStatusSummaryGridControl = FwBrowse.loadGridFromTemplate('OrderStatusSummaryGrid');
        $orderStatusSummaryGrid.empty().append($orderStatusSummaryGridControl);
        $orderStatusSummaryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
            };
            request.pagesize = max;
        })
        FwBrowse.init($orderStatusSummaryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusSummaryGridControl);
        this.addLegend($form, $orderStatusSummaryGrid);
        //----------------------------------------------------------------------------------------------
        const $orderStatusRentalDetailGrid = $form.find('div[data-grid="OrderStatusRentalDetailGrid"]');
        const $orderStatusRentalDetailGridControl = FwBrowse.loadGridFromTemplate('OrderStatusRentalDetailGrid');
        $orderStatusRentalDetailGrid.empty().append($orderStatusRentalDetailGridControl);
        $orderStatusRentalDetailGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                RecType: "R"
            };
            request.pagesize = max;
        })
        FwBrowse.init($orderStatusRentalDetailGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusRentalDetailGridControl);
        this.addLegend($form, $orderStatusRentalDetailGrid);
        //----------------------------------------------------------------------------------------------
        const $orderStatusSalesDetailGrid = $form.find('div[data-grid="OrderStatusSalesDetailGrid"]');
        const $orderStatusSalesDetailGridControl = FwBrowse.loadGridFromTemplate('OrderStatusSalesDetailGrid');
        $orderStatusSalesDetailGrid.empty().append($orderStatusSalesDetailGridControl);
        $orderStatusSalesDetailGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                RecType: "S"
            };
            request.pagesize = max;
        })
        FwBrowse.init($orderStatusSalesDetailGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusSalesDetailGridControl);
        this.addLegend($form, $orderStatusSalesDetailGrid);
        //----------------------------------------------------------------------------------------------
        const $filter = $form.find('.filter[data-type="radio"]');
        $filter.on("change", () => {
            const filterValue = $form.find('.filter input[type="radio"]:checked').val().toUpperCase();

            $orderStatusSummaryGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
                };
                request.pagesize = max;
                request.filterfields = {
                    Status: filterValue
                }
            })
            FwBrowse.search($orderStatusSummaryGridControl);

            $orderStatusRentalDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                    RecType: "R"
                };
                request.pagesize = max;
                request.filterfields = {
                    Status: filterValue
                }
            })
            FwBrowse.search($orderStatusRentalDetailGridControl);

            $orderStatusSalesDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                    RecType: "S"
                };
                request.pagesize = max;
                request.filterfields = {
                    Status: filterValue
                }
            })
            FwBrowse.search($orderStatusSalesDetailGridControl);
        });
        //----------------------------------------------------------------------------------------------
        //Filter field events
        const $filterValidations = $form.find('#filters [data-type="multiselectvalidation"] input.fwformfield-value');
        $filterValidations.on("change", e => {
            const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
            const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            const inventoryId = FwFormField.getValueByDataField($form, 'ICode');
            const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');

            $orderStatusSummaryGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: orderId
                };
                request.pagesize = max;
                if (inventoryTypeId !== "") {
                    var invObj = { InventoryTypeId: inventoryTypeId }
                }
                if (warehouseId !== "") {
                    var whObj = { WarehouseId: warehouseId }
                }
                if (categoryId !== "") {
                    var catObj = { CategoryId: categoryId }
                }
                if (inventoryId !== "") {
                    var iObj = { InventoryId: inventoryId }
                }
                if (subCategoryId !== "") {
                    var subObj = { SubCategoryId: subCategoryId }
                }
                request.filterfields = jQuery.extend(invObj, whObj, catObj, iObj, subObj);
            })
            FwBrowse.search($orderStatusSummaryGridControl);

            $orderStatusRentalDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "R"
                };
                request.pagesize = max;
                if (inventoryTypeId !== "") {
                    var invObj = { InventoryTypeId: inventoryTypeId }
                }
                if (warehouseId !== "") {
                    var whObj = { WarehouseId: warehouseId }
                }
                if (categoryId !== "") {
                    var catObj = { CategoryId: categoryId }
                }
                if (inventoryId !== "") {
                    var iObj = { InventoryId: inventoryId }
                }
                if (subCategoryId !== "") {
                    var subObj = { SubCategoryId: subCategoryId }
                }
                request.filterfields = jQuery.extend(invObj, whObj, catObj, iObj, subObj);
            })
            FwBrowse.search($orderStatusRentalDetailGridControl);

            $orderStatusSalesDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "S"
                };
                request.pagesize = max;
                if (inventoryTypeId !== "") {
                    var invObj = { InventoryTypeId: inventoryTypeId }
                }
                if (warehouseId !== "") {
                    var whObj = { WarehouseId: warehouseId }
                }
                if (categoryId !== "") {
                    var catObj = { CategoryId: categoryId }
                }
                if (inventoryId !== "") {
                    var iObj = { InventoryId: inventoryId }
                }
                if (subCategoryId !== "") {
                    var subObj = { SubCategoryId: subCategoryId }
                }
                request.filterfields = jQuery.extend(invObj, whObj, catObj, iObj, subObj);
            })
            FwBrowse.search($orderStatusSalesDetailGridControl);
        });

        const $textFilter = $form.find('#filters [data-type="text"] input.fwformfield-value');
        $textFilter.on("change", () => {
            const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
            const description = FwFormField.getValueByDataField($form, 'FilterDescription');
            const barCode = FwFormField.getValueByDataField($form, 'FilterBarCode');

            $orderStatusSummaryGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: orderId
                };
                request.pagesize = max;
                if (description !== "") {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("Description");
                    request.searchfieldvalues.push(description);
                };

                //justin 02/11/2018 (commmented - Bar Code column not present in data set)
                //if (BarCode !== "") {
                //    request.searchfieldoperators.push("like");
                //    request.searchfields.push("BarCode");
                //    request.searchfieldvalues.push(BarCode);
                //};
            })
            FwBrowse.search($orderStatusSummaryGridControl);

            $orderStatusRentalDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "R"
                };
                request.pagesize = max;
                if (description !== "") {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("Description");
                    request.searchfieldvalues.push(description);
                };
                if (barCode !== "") {
                    request.searchfieldoperators.push("like");
                    //request.searchfields.push("BarCode");
                    request.searchfields.push("BarCodeSerialRfid");  //justin 02/11/2018 replaced with correct field name
                    request.searchfieldvalues.push(barCode);
                };
            })
            FwBrowse.search($orderStatusRentalDetailGridControl);

            $orderStatusSalesDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "S"
                };
                request.pagesize = max;
                if (description !== "") {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("Description");
                    request.searchfieldvalues.push(description);
                };
                if (barCode !== "") {
                    request.searchfieldoperators.push("like");
                    //request.searchfields.push("BarCode");
                    request.searchfields.push("BarCodeSerialRfid");  //justin 02/11/2018 replaced with correct field name
                    request.searchfieldvalues.push(barCode);
                };
            })
            FwBrowse.search($orderStatusSalesDetailGridControl);
        });
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        let typeFieldHtml;
        let caption;
        switch (this.Type) {
            case 'Order':
                caption = 'Order Status';
                typeFieldHtml = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>`;
                break;
            case 'Transfer':
                caption = 'Transfer Status';
                typeFieldHtml = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transfer No." data-datafield="TransferId" data-displayfield="TransferNumber" data-validationname="TransferOrderValidation" style="flex:0 1 175px;"></div>`;
                break;
            case 'Item':
                caption = 'Container Status';
                typeFieldHtml = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container No." data-datafield="ItemId" data-displayfield="BarCode" data-validationname="ContainerValidation" style="flex:0 1 175px;"></div>`;
                break;
        }
        return `
        <div id="orderstatusform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="${caption}" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="${this.Module}Controller">
          <div id="dealform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
            </div>
            <div class="tabpages">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="${caption}">
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 850px;">
                        <div class="flexrow">
                           ${typeFieldHtml}
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 300px;" data-enabled="false"></div>
                          ${this.Type === 'Order' ?
                '<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="Deal" style="flex:1 1 300px;" data-enabled="false"></div>'
                : ''}
                        </div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 150px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" style="flex:1 1 125px;" data-enabled="false"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 850px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 150px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="PickTime" style="flex:1 1 100px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Estimated Start Date" data-datafield="EstimatedStartDate" style="flex:1 1 150px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="EstimatedStartTime" style="flex:1 1 100px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Estimated Stop Date" data-datafield="EstimatedStopDate" style="flex:1 1 150px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="EstimatedStopTime" style="flex:1 1 100px;" data-enabled="false"></div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 150px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" style="flex:1 1 125px;" data-enabled="false"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:0 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="View">
                      <div class="flexrow">
                        <div class="flexcolumn">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield filter" data-caption="" data-datafield="" style="flex:1 1 150px;">
                            <div data-value="All" data-caption="All"></div>
                            <div data-value="StagedOnly" data-caption="Staged Only"></div>
                            <div data-value="NotYetStaged" data-caption="Not Yet Staged"></div>
                            <div data-value="StillOut" data-caption="Still Out"></div>
                            <div data-value="InOnly" data-caption="In Only"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield toggle" data-caption="" data-datafield="" style="flex:0 1 125px;margin-left:15px;">
                            <div data-value="Summary" data-caption="Summary"></div>
                            <div data-value="Details" data-caption="Detail"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 775px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filter">
                      <div class="flexrow">
                        <div id="filters" class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-validationname="InventoryTypeValidation" style="flex:1 1 200px;"></div>
                            <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-validationname="RentalCategoryValidation" style="flex:1 1 200px;"></div>
                            <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Sub-Category" data-datafield="SubCategoryId" data-displayfield="SubCategory" data-validationname="SubCategoryValidation" style="flex:1 1 200px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-displayfield="ICode" data-validationname="RentalInventoryValidation" style="flex:1 1 200px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield textfilter" data-caption="Description" data-datafield="FilterDescription" style="flex:1 1 400px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield textfilter" data-caption="Bar Code No." data-datafield="FilterBarCode" style="flex:1 1 250px;"></div>
                            <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="flex:1 1 225px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexrow" style="max-width:1400px;">
                  <div class="flexcolumn summaryview">
                    <div class="flexrow">
                      <div data-control="FwGrid" data-grid="OrderStatusSummaryGrid" data-securitycaption="Order Status Summary"></div>
                    </div>
                  </div>
                </div>
                <div class="flexrow rentalview details" style="max-width:1800px;">
                  <div data-control="FwGrid" data-grid="OrderStatusRentalDetailGrid" data-securitycaption="Rental Detail"></div>
                </div>
                <div class="flexrow salesview details" style="max-width:1800px;">
                  <div data-control="FwGrid" data-grid="OrderStatusSalesDetailGrid" data-securitycaption="Sales Detail"></div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    toggleView($form: any) {
        const $toggle = $form.find('.toggle[data-type="radio"]');
        $toggle.on("change", () => {
            const view = $form.find('.toggle input[type="radio"]:checked').val();
            switch (view) {
                case 'Summary':
                    $form.find('.summaryview').show();
                    $form.find('.details').hide();
                    break;
                case 'Details':
                    $form.find('.details').show();
                    $form.find('.summaryview').hide();
                    break;
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    addLegend($form: any, $grid) {
        FwBrowse.addLegend($grid, 'Complete', '#8888ff');
        FwBrowse.addLegend($grid, 'Kit', '#03d337');
        FwBrowse.addLegend($grid, 'Exchange', '#a0cdb4');
        FwBrowse.addLegend($grid, 'Sub Vendor', '#ffb18c');
        FwBrowse.addLegend($grid, 'Consignor', '#8080ff');
        FwBrowse.addLegend($grid, 'Truck', '#ffff00');
        FwBrowse.addLegend($grid, 'Suspended', '#0000a0');
        FwBrowse.addLegend($grid, 'Lost', '#ff8080');
        FwBrowse.addLegend($grid, 'Sales', '#ff0080');
        FwBrowse.addLegend($grid, 'Not Yet Staged or Still Out', '#ff0000');
        FwBrowse.addLegend($grid, 'Too Many Staged', '#00ff80');
    }
    //----------------------------------------------------------------------------------------------
}