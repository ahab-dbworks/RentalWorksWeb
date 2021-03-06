﻿abstract class OrderStatusBase {
    Module: string;
    apiurl: string;
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

        if (this.Module === 'ContainerStatus') $form.find('.hide-on-container').hide();

        this.getOrder($form);
        this.toggleView($form);

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, `${this.Type}Id`, parentmoduleinfo.OrderId, parentmoduleinfo.OrderNumber);
            if (parentmoduleinfo.OrderId !== '') {
                $form.find(`[data-datafield="${this.Type}Id"]`).change();
            }
        }

        $form.find('.details').hide();

        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield=""]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
        });

        if (this.Type === 'PurchaseOrder') {
            $form.find('.hide-on-po').hide();
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getOrder($form: JQuery): void {
        $form.on('change', `[data-datafield="${this.Type}Id"]`, e => {
            try {
                $form.find('.toggle [data-value="Summary"] input').prop('checked', true).change();
                const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                let apiUrl;
                switch (this.Type) {
                    case 'Order':
                        apiUrl = `api/v1/order/${orderId}`;
                        break;
                    case 'Transfer':
                        apiUrl = `api/v1/transferorder/${orderId}`;
                        break;
                    case 'ContainerItem':
                        apiUrl = `api/v1/containeritem/${orderId}`;
                        break;
                    case 'PurchaseOrder':
                        apiUrl = `api/v1/purchaseorder/${orderId}`;
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

                    if (this.Type === 'PurchaseOrder') {
                        FwFormField.setValueByDataField($form, 'Warehouse', response.Warehouse);
                    }
                    if (this.Type === 'ContainerItem') {
                        FwFormField.setValueByDataField($form, 'ContainerStatus', response.ContainerStatus);
                        FwFormField.setValueByDataField($form, 'Warehouse', response.Warehouse);
                    }
                    //const rental = response.Rental;
                    //const sales = response.Sales;
                    //const parts = response.Parts;
                    //if (rental === false && sales === false && parts === false) {
                    //    $form.find('div[data-value="Details"]').hide();
                    //} else {
                    //    $form.find('div[data-value="Details"]').show();
                    //}

                    //if (this.Type === 'PurchaseOrder') {
                    //    $form.find('.rentalview').hide();
                    //    $form.find('.salesview').show();
                    //}
                    //else {
                    //    if (rental === true) {
                    //        $form.find('.rentalview').show();
                    //    } else {
                    //        $form.find('.rentalview').hide();
                    //    }
                    //
                    //    if ((sales === true) || (parts === true)) {
                    //        $form.find('.salesview').show();
                    //    } else {
                    //        $form.find('.salesview').hide();
                    //    }
                    //}

                    $form.find('.details').hide();
                }, null, $form);

                const $orderStatusSummaryGridControl = $form.find('[data-name="OrderStatusSummaryGrid"]');
                $orderStatusSummaryGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId
                    }
                })
                FwBrowse.search($orderStatusSummaryGridControl);

                const $orderStatusDetailGridControl = $form.find('[data-name="OrderStatusDetailGrid"]');
                $orderStatusDetailGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId,
                    }
                })
                FwBrowse.search($orderStatusDetailGridControl);

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
    //---------------------------------------------------------------------------------------------
    printOrderStatus($form: any, whichStatusReport: string) {
        try {
            const orderId = FwFormField.getValueByDataField($form, 'OrderId');
            const orderIdText = FwFormField.getTextByDataField($form, 'OrderId');
            let $report;
            if (whichStatusReport === 'Summary') {
                $report = OrderStatusSummaryReportController.openForm();
            } else {
                $report = OrderStatusDetailReportController.openForm();
            }
            FwModule.openSubModuleTab($form, $report);
            FwFormField.setValue($report, `div[data-datafield="OrderId"]`, orderId, orderIdText);
            const $tab = FwTabs.getTabByElement($report);
            $tab.find('.caption').html(`Print Order Status Summary`);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        //----------------------------------------------------------------------------------------------
        //Order Status Summary Grid
        const $orderStatusSummaryGridControl = FwBrowse.renderGrid({
            nameGrid: 'OrderStatusSummaryGrid',
            gridSecurityId: '3NAO1rd02hBF',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)   //jh - need this.Type here because this Form has descendants
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                if (this.Type === 'ContainerItem') {
                    $browse.find('[data-datafield="StillOutQuantity"]').attr('data-caption', 'In Container');
                    $browse.find(`[data-datafield="InQuantity"], [data-datafield="OutWarehouseCode"], [data-datafield="InWarehouseCode"]`).parent('div').remove();
                }
            }
        });
        //----------------------------------------------------------------------------------------------
        //Order Status Detail Grid
        const $orderStatusDetailGridControl = FwBrowse.renderGrid({
            nameGrid: 'OrderStatusDetailGrid',
            gridSecurityId: '75OyLDxMPa8z',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                if (this.Type === 'PurchaseOrder') {
                    $browse.find('div[data-datafield="OutDateTime"]').attr('data-caption', 'Receive Date/Time');
                    $browse.find('div[data-datafield="OutContractId"]').attr('data-caption', 'Receive Contract');
                };
                if (this.Type === 'ContainerItem') {
                    $browse.find('[data-datafield="OutDateTime"]').attr('data-caption', 'In Container Date/Time');
                    $browse.find('[data-datafield="InDateTime"]').attr('data-caption', 'Removed from Container Date/Time');
                    $browse.find(`[data-datafield="InContractId"], [data-datafield="OutContractId"]`).parent('div').remove();
                }
            }
        });
        //----------------------------------------------------------------------------------------------
        const $filter = $form.find('.filter[data-type="radio"]');
        $filter.on("change", () => {
            const filterValue = $form.find('.filter input[type="radio"]:checked').val().toUpperCase();

            $orderStatusSummaryGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`)
                };
                request.filterfields = {
                    Status: filterValue
                }
            })
            FwBrowse.search($orderStatusSummaryGridControl);

            $orderStatusDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                };
                request.filterfields = {
                    Status: filterValue
                }
            })
            FwBrowse.search($orderStatusDetailGridControl);
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

            $orderStatusDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: orderId,
                };
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
            FwBrowse.search($orderStatusDetailGridControl);
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
                if (description !== "") {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("Description");
                    request.searchfieldvalues.push(description);
                }

                //justin 02/11/2018 (commmented - Bar Code column not present in data set)
                //if (BarCode !== "") {
                //    request.searchfieldoperators.push("like");
                //    request.searchfields.push("BarCode");
                //    request.searchfieldvalues.push(BarCode);
                //};
            })
            FwBrowse.search($orderStatusSummaryGridControl);

            $orderStatusDetailGridControl.data('ondatabind', request => {
                request.uniqueids = {
                    OrderId: orderId,
                };
                if (description !== "") {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("Description");
                    request.searchfieldvalues.push(description);
                }
                if (barCode !== "") {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("BarCodeSerialRfid");
                    request.searchfieldvalues.push(barCode);
                }
            })
            FwBrowse.search($orderStatusDetailGridControl);
        });

        if (this.Type === 'PurchaseOrder') {
            $orderStatusSummaryGridControl.find(`thead [data-browsedatafield="OutQuantity"], 
                                                 thead [data-browsedatafield="InQuantity"],
                                                 thead [data-browsedatafield="InWarehouseCode"],
                                                 thead [data-browsedatafield="StillOutQuantity"],
                                                 thead [data-browsedatafield="StagedQuantity"]`).parent('td').hide();
            $orderStatusSummaryGridControl.find('thead [data-browsedatafield="QuantityReturned"], thead [data-browsedatafield="QuantityReceived"]').parent('td').show();
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        let subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        request.uniqueids = {
            WarehouseId: warehouse.warehouseid
        };
        switch (datafield) {
            case 'OrderId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorder`);
                break;
            case 'InventoryTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'CategoryId':
                if (inventoryTypeId) {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                break;
            case 'SubCategoryId':
                if (inventoryTypeId) {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    };
                }
                if (categoryId) {
                    request.uniqueids = {
                        CategoryId: categoryId,
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                break;
            case 'ICode':
                if (inventoryTypeId) {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    };
                }
                if (categoryId) {
                    request.uniqueids = {
                        CategoryId: categoryId,
                    };
                }
                if (subCategoryId) {
                    request.uniqueids = {
                        SubCategoryId: subCategoryId,
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateicode`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'TransferId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetransfer`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        let caption;
        switch (this.Type) {
            case 'Order':
                caption = 'Order Status';
                //typeFieldHtml = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>`;
                break;
            case 'Transfer':
                caption = 'Transfer Status';
                //typeFieldHtml = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transfer No." data-datafield="TransferId" data-displayfield="TransferNumber" data-validationname="TransferOrderValidation" style="flex:0 1 175px;"></div>`;
                break;
            case 'ContainerItem':
                caption = 'Container Status';
                //typeFieldHtml = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container No." data-datafield="ContainerItemId" data-displayfield="BarCode" data-validationname="ContainerItemValidation" style="flex:0 1 175px;"></div>`;
                break;
            case 'PurchaseOrder':
                caption = 'Purchase Order Status';
                //typeFieldHtml = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Purchase Order No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" style="flex:0 1 175px;"></div>`;
                break;
        }
        let html = ``;
        html += `
        <div id="orderstatusform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="${caption}" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="${this.Module}Controller">
            <div class="flexpage">
            <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="${caption}">
                <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 850px;">
                    <div class="flexrow">`;
        if (this.Type === 'ContainerItem') {
            html += `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container No." data-datafield="ContainerItemId" data-displayfield="BarCode" data-validationname="ContainerItemValidation" style="flex:0 1 175px;"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Container Description" data-datafield="Description" style="flex:1 1 300px;" data-enabled="false"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Container Status" data-datafield="ContainerStatus" style="flex:1 1 300px;" data-enabled="false"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" style="flex:1 1 125px;" data-enabled="false"></div>`;
        }
        else if (this.Type === 'Transfer') {
            html += `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transfer No." data-datafield="TransferId" data-displayfield="TransferNumber" data-validationname="TransferOrderValidation" style="flex:0 1 175px;"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 300px;" data-enabled="false"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" style="flex:1 1 125px;" data-enabled="false"></div>`;
        }
        else if (this.Type === 'PurchaseOrder') {
            html += `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Purchase Order No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" style="flex:0 1 175px;"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 300px;" data-enabled="false"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" style="flex:1 1 125px;" data-enabled="false"></div>`;
        }
        else { //Order
            html += `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 300px;" data-enabled="false"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="${Constants.Modules.Agent.children.Deal.caption}" data-datafield="Deal" style="flex:1 1 300px;" data-enabled="false"></div>`;
            html += `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" style="flex:1 1 125px;" data-enabled="false"></div>`;
        }
        html += `
                    </div>
                    </div>
                </div>`;
        if (this.Type === 'ContainerItem') {
        }
        else if (this.Type === 'Transfer') {
            html += `        <div class="flexcolumn" style="flex:1 1 850px;">`;
            html += `        <div class="flexrow hide-on-po">`;
            html += `            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 150px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="PickTime" style="flex:1 1 100px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Estimated Start Date" data-datafield="EstimatedStartDate" style="flex:1 1 150px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="EstimatedStartTime" style="flex:1 1 100px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Estimated Stop Date" data-datafield="EstimatedStopDate" style="flex:1 1 150px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="EstimatedStopTime" style="flex:1 1 100px;" data-enabled="false"></div>`;
            html += `        </div>`;
            html += `        </div>`;
            html += `        <div class="flexcolumn" style="flex:1 1 150px;">`;
            html += `        <div class="flexrow">`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" style="flex:1 1 125px;" data-enabled="false"></div>`;
            html += `        </div>`;
            html += `        </div>`;
        }
        else if (this.Type === 'PurchaseOrder') {
            html += `        <div class="flexcolumn" style="flex:1 1 850px;">`;
            html += `        <div class="flexrow hide-on-po">`;
            html += `            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Receive Date" data-datafield="EstimatedStartDate" style="flex:1 1 150px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="EstimatedStartTime" style="flex:1 1 100px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Return Date" data-datafield="EstimatedStopDate" style="flex:1 1 150px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="EstimatedStopTime" style="flex:1 1 100px;" data-enabled="false"></div>`;
            html += `        </div>`;
            html += `        </div>`;
            html += `        <div class="flexcolumn" style="flex:1 1 150px;">`;
            html += `        <div class="flexrow">`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" style="flex:1 1 125px;" data-enabled="false"></div>`;
            html += `        </div>`;
            html += `        </div>`;
        }
        else { //Order
            html += `        <div class="flexcolumn" style="flex:1 1 850px;">`;
            html += `        <div class="flexrow hide-on-po">`;
            html += `            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 150px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="PickTime" style="flex:1 1 100px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Estimated Start Date" data-datafield="EstimatedStartDate" style="flex:1 1 150px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="EstimatedStartTime" style="flex:1 1 100px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Estimated Stop Date" data-datafield="EstimatedStopDate" style="flex:1 1 150px;" data-enabled="false"></div>`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Time" data-datafield="EstimatedStopTime" style="flex:1 1 100px;" data-enabled="false"></div>`;
            html += `        </div>`;
            html += `        </div>`;
            html += `        <div class="flexcolumn" style="flex:1 1 150px;">`;
            html += `        <div class="flexrow">`;
            html += `            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" style="flex:1 1 125px;" data-enabled="false"></div>`;
            html += `        </div>`;
            html += `        </div>`;
        }
        html += `
                </div>
            </div>
            <div class="flexrow">
                <div class="flexcolumn" style="flex:0 1 350px;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="View">
                    <div class="flexrow">
                    <div class="flexcolumn">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield filter" data-caption="" data-datafield="" style="flex:1 1 150px;">
                        <div data-value="All" data-caption="All"></div>`;
        if (this.Type === 'ContainerItem') {
            html += `<div data-value="StagedOnly" data-caption="Staged Only"></div><div data-value="NotYetStaged" data-caption="Not Yet Staged"></div><div data-value="StillOut" data-caption="In Container"></div><div data-value="InOnly" data-caption="Removed from Container"></div>`;
        }
        else if (this.Type === 'PurchaseOrder') {
            html += `<div data-value="NotYetReceived" data-caption="Not Yet Received"></div><div data-value="Received" data-caption="Received"></div><div data-value="Returned" data-caption="Returned"></div><div data-value="NotYetBarcoded" data-caption="Not Yet Barcoded"></div>`;
        }
        else {
            html += `<div data-value="StagedOnly" data-caption="Staged Only"></div><div data-value="NotYetStaged" data-caption="Not Yet Staged"></div><div data-value="StillOut" data-caption="Still Out"></div><div data-value="InOnly" data-caption="In Only"></div>`;
        }
        html += `
                        </div>
                    </div>
                    <div class="flexcolumn">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield toggle" data-caption="" data-datafield="" style="flex:0 1 125px;margin-left:15px; max-height:75px;">
                        <div data-value="Summary" data-caption="Summary"></div>
                        <div data-value="Details" data-caption="Detail"></div>
                        </div>
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield show-all-history" data-caption="" data-datafield="" style="display:none; flex:0 1 125px;margin-left:15px;">
                                <div data-value="Current" data-caption="Current Status"></div>
                                <div data-value="ShowAll" data-caption="Show All History"></div>
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
                        <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Sub-Category" data-datafield="SubCategoryId" data-displayfield="SubCategory" data-validationname="SubCategoryValidation" data-validationpeek="false" style="flex:1 1 200px;"></div>
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
                <div data-control="FwGrid" data-grid="OrderStatusDetailGrid" data-securitycaption="Rental Detail"></div>
            </div>
          </div>
            < /div>`;
        return html;
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
                    $form.find('.show-all-history').hide();
                    break;
                case 'Details':
                    $form.find('.details').show();
                    $form.find('.summaryview').hide();
                    if (this.Type === 'ContainerItem') {
                        $form.find('.show-all-history').show();
                        $form.find('.salesview').hide();
                    } else {
                        $form.find('.show-all-history').hide();
                    }
                    break;
            }
        });

        const $orderStatusDetailGrid = $form.find('div[data-name="OrderStatusDetailGrid"]');
        const onDataBind = $orderStatusDetailGrid.data('ondatabind');

        $form.on('change', '.show-all-history', e => {
            const val = FwFormField.getValue($form, '.show-all-history');
            if (val === 'ShowAll') {
                if (typeof onDataBind == 'function') {
                    $orderStatusDetailGrid.data('ondatabind', request => {
                        onDataBind(request);
                        request.uniqueids.AllHistory = true;
                    });
                }
            } else {
                $orderStatusDetailGrid.data('ondatabind', onDataBind);
            }
            FwBrowse.search($orderStatusDetailGrid);
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