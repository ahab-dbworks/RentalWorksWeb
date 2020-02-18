routes.push({ pattern: /^module\/availabilityconflicts/, action: function (match: RegExpExecArray) { return AvailabilityConflictsController.getModuleScreen(); } });

class AvailabilityConflicts {
    Module: string = 'AvailabilityConflicts';
    apiurl: string = 'api/v1/availabilityconflicts'
    caption: string = Constants.Modules.Inventory.children.AvailabilityConflicts.caption;
    nav: string = Constants.Modules.Inventory.children.AvailabilityConflicts.nav;
    id: string = Constants.Modules.Inventory.children.AvailabilityConflicts.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
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
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.loadItems($form.find('div[data-datafield="Rank"]'), [
            { value: "A", text: "A", selected: "T" },
            { value: "B", text: "B", selected: "T" },
            { value: "C", text: "C", selected: "T" },
            { value: "D", text: "D", selected: "T" },
            { value: "E", text: "E", selected: "T" },
            { value: "F", text: "F", selected: "T" },
            { value: "G", text: "G", selected: "T" }
        ]);

        FwFormField.setValueByDataField($form, 'ConflictType', 'N');


        const toDate = FwFunc.getDate(new Date().toString(), 30);
        FwFormField.setValueByDataField($form, 'ToDate', toDate);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('.refresh').on('click', e => {
            const request: any = {};
            request.AvailableFor = FwFormField.getValueByDataField($form, 'AvailableFor');
            request.ConflictType = FwFormField.getValueByDataField($form, 'ConflictType');
            request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            request.InventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            request.SubCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
            request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            request.Description = FwFormField.getValueByDataField($form, 'Description');
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            request.Ranks = FwFormField.getValueByDataField($form, 'Rank');
            request.ToDate = FwFormField.getValueByDataField($form, 'ToDate');

            $form.find('#availabilityTable').empty();
            FwAppData.apiMethod(true, 'POST', 'api/v1/inventoryavailability/conflicts', request, FwServices.defaultTimeout,
                response => {
                    this.loadAvailabilityTable($form, response);
                },
                ex => FwFunc.showError(ex), $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    loadAvailabilityTable($form, data) {
        const html = `
                <table>
                    <thead>
                        <tr>
                            <th>Warehouse</th>
                            <th>Inventory Type</th>
                            <th>Category</th>
                            <th>Sub Category</th>
                            <th>I-Code</th>
                            <th>Item Description</th>
                            <th>Order Type</th>
                            <th>Order No.</th>
                            <th>Order Description</th>
                            <th>Deal</th>
                            <th class="number">Reserved</th>
                            <th class="number">Sub</th>
                            <th class="number">Available</th>
                            <th class="number">Late</th>
                            <th class="number">In</th>
                            <th class="number">QC</th>
                            <th>From</th>
                            <th>To</th>
                        <tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>`;

        $form.find('#availabilityTable').append(html);

        const $rows: any = [];
        for (let i = 0; i < data.length; i++) {
            const row = `
                    <tr class="data-row">
                        <td>${data[i].Warehouse}</td>
                        <td>${data[i].InventoryType}</td>
                        <td>${data[i].Category}</td>
                        <td>${data[i].SubCategory}</td>
                        <td class="nowrap inventory-number" data-id="${data[i].InventoryId}"><span>${data[i].ICode}</span><i class="material-icons btnpeek">more_horiz</i></td>
                        <td>${data[i].ItemDescription}</td>
                        <td>${data[i].OrderTypeDescription}</td>
                        <td class="order-number" data-id="${data[i].OrderId}" data-ordertype="${data[i].OrderType}"><span>${data[i].OrderNumber}</span><i class="material-icons btnpeek">more_horiz</i></td>
                        <td>${data[i].OrderDescription}</td>
                        <td data-id="${data[i].DealId}"><span>${data[i].Deal}</span><i class="material-icons btnpeek">more_horiz</i></td>
                        <td class="number">${data[i].QuantityReserved}</td>
                        <td class="number">${data[i].QuantitySub}</td>
                        <td class="number quantity-available"><div class="available-color" data-state=${data[i].AvailabilityState}>${data[i].QuantityAvailable}</div></td>
                        <td class="number">${data[i].QuantityLate}</td>
                        <td class="number">${data[i].QuantityIn}</td>
                        <td class="number">${data[i].QuantityQc}</td>
                        <td>${data[i].FromDateTimeString}</td>
                        <td>${data[i].ToDateTimeString}</td>
                    </tr>
                    <tr class="avail-calendar" style="display:none;"><tr>
                    `;
            $rows.push(row);
        }

        $form.find('tbody').empty().append($rows);

        this.availabilityTableEvents($form);
    }
    //----------------------------------------------------------------------------------------------
    availabilityTableEvents($form) {
        //add validation peeks
        $form.find('#availabilityTable table tr td i.btnpeek')
            .off('click')
            .on('click', e => {
            try {
                //$control.find('.btnpeek').hide();
                //$validationbrowse.data('$control').find('.validation-loader').show();
                //setTimeout(function () {
                const $control = jQuery(e.currentTarget).closest('td');
                const validationId = $control.attr('data-id');
                let datafield;
                let validationPeekFormName;
                if ($control.hasClass('order-number')) {
                    const orderType = $control.attr('data-ordertype');
                    switch (orderType) {
                        case 'O':
                            datafield = 'OrderId';
                            validationPeekFormName = 'Order';
                            break;
                        case 'Q':
                            datafield = 'QuoteId';
                            validationPeekFormName = 'Quote';
                            break;
                        case 'R':
                            datafield = 'RepairId';
                            validationPeekFormName = 'Repair';
                            break;
                        //
                    }
                } else if ($control.hasClass('inventory-number')) {
                    datafield = 'InventoryId';
                    validationPeekFormName = 'RentalInventory';
                } else {
                    datafield = 'DealId';
                    validationPeekFormName = 'Deal';
                }
                const title = $control.find('span').text();

                FwValidation.validationPeek($control, validationPeekFormName, validationId, datafield, null, title);
                //$validationbrowse.data('$control').find('.validation-loader').hide();
                //$control.find('.btnpeek').show()
                //})
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //add availability calendar and schedule
        $form.find('#availabilityTable table tr td.quantity-available')
            .off('click')
            .on('click', e => {
                let showingAvail: boolean = false;
                const $row = jQuery(e.currentTarget).parent('tr.data-row');
                const $availRow = $row.next('.avail-calendar');
                if ($availRow.hasClass('show-quantity')) {
                    $availRow.removeClass('show-quantity').hide();
                } else {
                    $availRow.addClass('show-quantity').show();
                    showingAvail = true;
                }
                const html = `<div data-control="FwScheduler" style="overflow:auto;" class="fwcontrol fwscheduler calendar"></div>
            <div data-control="FwSchedulerDetailed" class="fwcontrol fwscheduler realscheduler"></div>`;
                $availRow.empty().append(`<td colspan="10">${html}</td>`);
                const $calendar = $availRow.find('.calendar');
                const $scheduler = $availRow.find('.realscheduler');
                const inventoryId = jQuery($row.find('.inventory-number')).attr('data-id');

                if (showingAvail) {
                    FwScheduler.renderRuntimeHtml($calendar);
                    FwScheduler.init($calendar);
                    FwScheduler.loadControl($calendar);
                    RentalInventoryController.addCalSchedEvents($form, $calendar, inventoryId);
                    const schddate = FwScheduler.getTodaysDate();
                    FwScheduler.navigate($calendar, schddate);
                    FwScheduler.refresh($calendar);

                    FwSchedulerDetailed.renderRuntimeHtml($scheduler);
                    FwSchedulerDetailed.init($scheduler);
                    FwSchedulerDetailed.loadControl($scheduler);
                    RentalInventoryController.addCalSchedEvents($form, $scheduler, inventoryId);
                    FwSchedulerDetailed.navigate($scheduler, schddate, 35);
                    FwSchedulerDetailed.refresh($scheduler);
                }
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        let availFor = FwFormField.getValueByDataField($form, 'AvailableFor');
        let inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        let categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        let subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
        switch (datafield) {
            case 'InventoryTypeId':
                if (availFor === "R") {
                    request.uniqueids = {
                        Rental: true,
                    };
                }
                else if (availFor === "S") {
                    request.uniqueids = {
                        Sales: true,
                    };
                }
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
            case 'InventoryId':
                if (availFor) {
                    request.uniqueids = {
                        AvailFor: availFor,
                    };
                }
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
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'DealId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'OrderId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorder`);
                break;

        }
    }
    //----------------------------------------------------------------------------------------------
}
var AvailabilityConflictsController = new AvailabilityConflicts();