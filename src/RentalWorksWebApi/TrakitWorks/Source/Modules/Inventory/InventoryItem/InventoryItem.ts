routes.push({ pattern: /^module\/inventoryitem$/, action: function(match: RegExpExecArray) { return InventoryItemController.getModuleScreen(); } });

class InventoryItem {
    Module:             string = 'InventoryItem';
    apiurl:             string = 'api/v1/rentalinventory';
    caption:            string = Constants.Modules.Inventory.children.InventoryItem.caption;
    nav:                string = Constants.Modules.Inventory.children.InventoryItem.nav;
    id:                 string = Constants.Modules.Inventory.children.InventoryItem.id;
    ActiveView:         string = 'ALL';
    AvailableFor:       string = 'X';
    yearData:           any    = [];
    ActiveViewFields:   any    = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse, self = this;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let self = this;
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, uniqueids?) {
        var $form, $calendar, inventoryId, $realScheduler;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        let self         = this;
        let warehouseId  = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        let startOfMonth = moment().startOf('month').format('MM/DD/YYYY');
        let endOfMonth   = moment().endOf('month').format('MM/DD/YYYY');

        if (typeof uniqueids !== 'undefined') {
            inventoryId = uniqueids.InventoryId;
        }

        $calendar = $form.find('.calendar');
        $calendar
            .data('ongetevents', function (calendarRequest) {
                startOfMonth = moment(calendarRequest.start.value).format('MM/DD/YYYY');
                endOfMonth = moment(calendarRequest.start.value).add(calendarRequest.days, 'd').format('MM/DD/YYYY');

                FwAppData.apiMethod(true, 'POST', `api/v1/inventoryavailability/calendarandscheduledata`,
                    {
                        InventoryId: inventoryId,
                        WarehouseId: [warehouseId],
                        FromDate: startOfMonth,
                        ToDate: endOfMonth,
                        SortReservationsBy: 'OrderNumber'
                    }, FwServices.defaultTimeout, function onSuccess(response) {
                    var calendarevents = response.InventoryAvailabilityCalendarEvents;
                    var schedulerEvents = response.InventoryAvailabilityScheduleEvents;
                    for (var i = 0; i < calendarevents.length; i++) {
                        if (calendarevents[i].textColor !== 'rgb(0,0,0') {
                            calendarevents[i].html = `<div style="color:${calendarevents[i].textColor}">${calendarevents[i].text}</div>`
                        }
                    }
                    FwScheduler.loadEventsCallback($calendar, [{ id: '1', name: '' }], calendarevents);
                }, function onError(response) {
                    FwFunc.showError(response);
                }, $calendar)
            })
            .data('ontimerangedoubleclicked', function (event) {
                try {
                    var date;
                    date = event.start.toString('MM/dd/yyyy');
                    FwScheduler.setSelectedDay($calendar, date);
                    //DriverController.openTicket($form);
                    $form.find('div[data-type="Browse"][data-name="Schedule"] .browseDate .fwformfield-value').val(date).change();
                    $form.find('div.tab.schedule').click();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        $realScheduler = $form.find('.realscheduler');
        $realScheduler.data('ongetevents', function (request) {
            var start = moment(request.start.value).format('MM/DD/YYYY');
            var end = moment(request.start.value).add(31, 'days').format('MM/DD/YYYY')

            FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailability/calendarandscheduledata?&InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${start}&ToDate=${end}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                var schedulerEvents = response.InventoryAvailabilityScheduleEvents;
                for (var i = 0; i < schedulerEvents.length; i++) {
                    if (schedulerEvents[i].textColor !== 'rgb(0,0,0') {
                        schedulerEvents[i].html = `<div style="color:${schedulerEvents[i].textColor}">${schedulerEvents[i].text}</div>`
                    }
                }
                FwSchedulerDetailed.loadEventsCallback($realScheduler, response.InventoryAvailabilityScheduleResources, response.InventoryAvailabilityScheduleEvents);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $calendar)
        });

        FwFormField.loadItems($form.find('div[data-datafield="LampCount"]'), [
            { value: '0', text: '0' },
            { value: '1', text: '1' },
            { value: '2', text: '2' },
            { value: '3', text: '3' },
            { value: '4', text: '4' }
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="Classification"]'), [
            {value:'I',     text:'Item'},
            {value:'A',     text:'Accessory'},
            {value:'C',     text:'Complete'},
            {value:'K',     text:'Kit'},
            {value:'N',     text:'Container'},
            {value:'M',     text:'Misc.'}
        ], false);

        FwFormField.loadItems($form.find('div[data-datafield="TrackedBy"]'), [
            {value:'BARCODE',    text:'Barcode'},
            {value:'SERIALNO',   text:'Serial No'},
            {value:'RFID',       text:'RFID'},
            {value:'QUANTITY',   text:'Quantity'}
        ], false);

        FwFormField.loadItems($form.find('div[data-datafield="weightselector"]'), [
            { value: 'IMPERIAL', caption: 'Imperial', checked: true },
            { value: 'METRIC', caption: 'Metric' }
        ]);

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            if ($form.data('mode') !== 'NEW') {
                const tabpageid = jQuery(e.currentTarget).data('tabpageid');

                const $gridControls = $form.find(`#${tabpageid} [data-type="Grid"]`);
                if ($gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        const $gridcontrol = jQuery($gridControls[i]);
                        FwBrowse.search($gridcontrol);
                    }
                }

                const $browseControls = $form.find(`#${tabpageid} [data-type="Browse"]`);
                if ($browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
        });

        $form.on('change', 'div[data-datafield="Classification"] .fwformfield-value', function () {
            var $this      = jQuery(this);
            var $trackedby = $form.find('div[data-datafield="TrackedBy"]');

            $form.find('.completeskitstab').hide();
            $form.find('.containertab').hide();
            $form.find('.completetab').hide();
            $form.find('.kittab').hide();

            switch ($this.val()) {
                case 'I':
                    $form.find('.completeskitstab').show();
                    FwFormField.enable($trackedby);
                    break;
                case 'A':
                    $form.find('.completeskitstab').show();
                    FwFormField.enable($trackedby);
                    break;
                case 'C':
                    $form.find('.completetab').show();
                    FwFormField.disable($trackedby);
                    FwFormField.setValueByDataField($form, 'TrackedBy', '');
                    break;
                case 'K':
                    $form.find('.kittab').show();
                    $form.find('.completeskitstab').show();
                    FwFormField.disable($trackedby);
                    FwFormField.setValueByDataField($form, 'TrackedBy', '');
                    break;
                case 'N':
                    $form.find('.containertab').show();
                    FwFormField.disable($trackedby);
                    FwFormField.setValueByDataField($form, 'TrackedBy', '');
                    break;
                case 'M':
                    $form.find('.completeskitstab').show();
                    FwFormField.disable($trackedby);
                    FwFormField.setValueByDataField($form, 'TrackedBy', 'QUANTITY');
                    break;
            }
        });

        let $submoduleRepairOrderBrowse = this.openRepairOrderBrowse($form);
        $form.find('.repairOrderSubModule').append($submoduleRepairOrderBrowse);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);

        var $calendar = $form.find('.calendar');
        if ($calendar.length > 0) {
            setTimeout(function () {
                var schddate = FwScheduler.getTodaysDate();
                FwScheduler.navigate($calendar, schddate);
                FwScheduler.refresh($calendar);
            }, 1);
        }

        var $realScheduler = $form.find('.realscheduler');
        if ($realScheduler.length > 0) {
            setTimeout(function () {
                var schddate = FwSchedulerDetailed.getTodaysDate();
                FwSchedulerDetailed.navigate($realScheduler, schddate);
                FwSchedulerDetailed.refresh($realScheduler);
            }, 1);
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    openRepairOrderBrowse($form) {
        let $browse     = RepairController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = RepairController.ActiveViewFields;
            request.uniqueids = {
                InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
            };
        });
        jQuery($browse).find('.ddviewbtn-caption:contains("Show:")').siblings('.ddviewbtn-select').find('.ddviewbtn-dropdown-btn:contains("All")').click();
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
        });

        $form.find('div[data-datafield="InventoryTypeId"]').data('onchange', $tr => {
            if ($tr.find('.field[data-browsedatafield="Wardrobe"]').attr('data-originalvalue') === 'true') {
                $form.find('.wardrobetab').show();
            } else {
                $form.find('.wardrobetab').hide();
            }
        });

        $form.find('div[data-datafield="CategoryId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('.subcategory'));
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('.subcategory'));
            } else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '')
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions) {
        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $item: JQuery = FwMenu.generateDropDownViewBtn('Item', true, "I");
        const $accessory: JQuery = FwMenu.generateDropDownViewBtn('Accessory', false, "A");
        const $complete: JQuery = FwMenu.generateDropDownViewBtn('Complete', false, "C");
        const $kit: JQuery = FwMenu.generateDropDownViewBtn('Kit', false, "K");
        const $set: JQuery = FwMenu.generateDropDownViewBtn('Set', false, "S");
        const $misc: JQuery = FwMenu.generateDropDownViewBtn('Misc', false, "M");
        const $container: JQuery = FwMenu.generateDropDownViewBtn('Container', false, "N");

        FwMenu.addVerticleSeparator(options.$menu);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $item, $accessory, $complete, $kit, $set, $misc);
        if (this.AvailableFor === "R") {
            viewSubitems.push($container);
        }
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Classification");

        return options;
    }
    //----------------------------------------------------------------------------------------------
    afterLoadSetClassification($form: any) {
        var $classification = $form.find('div[data-datafield="Classification"]');
        var $trackedby      = $form.find('div[data-datafield="TrackedBy"]');

        var $completeskitstab = $form.find('.completeskitstab');
        var $containertab     = $form.find('.containertab');
        var $completetab      = $form.find('.completetab');
        var $kittab           = $form.find('.kittab');
        var $settingstab      = $form.find('.settingstab');

        var $rentalinventorywarehousegrid = $form.find('[data-name="RentalInventoryWarehouseGrid"]');
        var $containerwarehousegrid       = $form.find('[data-grid="ContainerWarehouseGrid"]');

        switch (FwFormField.getValue2($classification)) {
            case 'I':
            case 'A':
                $completeskitstab.show();
                $classification.find('option[value="N"]').hide();
                $classification.find('option[value="C"]').hide();
                $classification.find('option[value="K"]').hide();
                $classification.find('option[value="M"]').hide();
                FwBrowse.search($rentalinventorywarehousegrid);
                break;
            case 'N':
                $completeskitstab.hide();
                $containertab.show();
                $settingstab.show();
                $rentalinventorywarehousegrid.hide();
                $containerwarehousegrid.show();
                FwBrowse.search($containerwarehousegrid);
                FwFormField.disable($classification);
                FwFormField.disable($trackedby);
                FwFormField.setValue2($trackedby, '');
                break;
            case 'C':
                $completeskitstab.hide();
                $completetab.show();
                FwBrowse.search($rentalinventorywarehousegrid);
                FwFormField.disable($classification);
                FwFormField.disable($trackedby);
                FwFormField.setValue2($trackedby, '');
                break;
            case 'K':
                $kittab.show();
                $completeskitstab.show();
                FwBrowse.search($rentalinventorywarehousegrid);
                FwFormField.disable($classification);
                FwFormField.disable($trackedby);
                FwFormField.setValue2($trackedby, '');
                break;
            case 'M':
                $completeskitstab.show();
                FwBrowse.search($rentalinventorywarehousegrid);
                FwFormField.disable($classification);
                FwFormField.disable($trackedby);
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        // RentalInventoryWarehouseGrid
        let $rentalInventoryWarehouseGrid = $form.find('div[data-grid="RentalInventoryWarehouseGrid"]');
        let $rentalInventoryWarehouseGridControl = jQuery(jQuery('#tmpl-grids-RentalInventoryWarehouseGridBrowse').html());
        $rentalInventoryWarehouseGrid.empty().append($rentalInventoryWarehouseGridControl);
        $rentalInventoryWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $rentalInventoryWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($rentalInventoryWarehouseGridControl);
        FwBrowse.renderRuntimeHtml($rentalInventoryWarehouseGridControl);

        // ContainerWarehouseGrid
        let containerWarehouseGrid: any;
        let containerWarehouseGridControl: any;
        containerWarehouseGrid = $form.find('div[data-grid="ContainerWarehouseGrid"]');
        containerWarehouseGridControl = jQuery(jQuery('#tmpl-grids-ContainerWarehouseGridBrowse').html());
        containerWarehouseGrid.empty().append(containerWarehouseGridControl);
        containerWarehouseGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        containerWarehouseGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init(containerWarehouseGridControl);
        FwBrowse.renderRuntimeHtml(containerWarehouseGridControl);

        // InventoryAvailabilityGrid
        let $inventoryAvailabilityGrid = $form.find('div[data-grid="InventoryAvailabilityGrid"]');
        let $inventoryAvailabilityGridControl = jQuery(jQuery('#tmpl-grids-InventoryAvailabilityGridBrowse').html());
        $inventoryAvailabilityGrid.empty().append($inventoryAvailabilityGridControl);
        $inventoryAvailabilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAvailabilityGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryAvailabilityGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAvailabilityGridControl);

        // InventoryCompleteGrid
        const $inventoryCompleteGrid = $form.find('div[data-grid="InventoryCompleteGrid"]');
        const $inventoryCompleteGridControl = FwBrowse.loadGridFromTemplate('InventoryCompleteGrid');
        $inventoryCompleteGrid.empty().append($inventoryCompleteGridControl);
        $inventoryCompleteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
                WarehouseId: warehouse.warehouseid
            };
        });
        $inventoryCompleteGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        $inventoryCompleteGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
            let primaryRowIndex;
            if (primaryRowIndex === undefined) {
                const orderByIndex = dt.ColumnIndex.OrderBy;
                const inventoryIdIndex = dt.ColumnIndex.InventoryId
                for (let i = 0; i < dt.Rows.length; i++) {
                    if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
                        primaryRowIndex = i
                    }
                }
            }
            if (rowIndex === primaryRowIndex) {
                return true;
            } else {
                return false;
            }
        });
        FwBrowse.init($inventoryCompleteGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteGridControl);

        // InventoryKitGrid
        const $inventoryKitGrid = $form.find('div[data-grid="InventoryKitGrid"]');
        const $inventoryKitGridControl = FwBrowse.loadGridFromTemplate('InventoryKitGrid');
        $inventoryKitGrid.empty().append($inventoryKitGridControl);
        $inventoryKitGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PackageId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryKitGridControl.data('beforesave', function (request) {
            request.PackageId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        $inventoryKitGridControl.data('isfieldeditable', function ($field, dt, rowIndex) {
            let primaryRowIndex;
            if (primaryRowIndex === undefined) {
                const orderByIndex = dt.ColumnIndex.OrderBy;
                const inventoryIdIndex = dt.ColumnIndex.InventoryId
                for (let i = 0; i < dt.Rows.length; i++) {
                    if (dt.Rows[i][orderByIndex] === 1 && dt.Rows[i][inventoryIdIndex] !== '') {
                        primaryRowIndex = i
                    }
                }
            }
            if (rowIndex === primaryRowIndex) {
                return true;
            } else {
                return false;
            }
        });
        FwBrowse.init($inventoryKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryKitGridControl);

        // InventoryCompleteKitGrid
        let $inventoryCompleteKitGrid = $form.find('div[data-grid="InventoryCompleteKitGrid"]');
        let $inventoryCompleteKitGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompleteKitGridBrowse').html());
        $inventoryCompleteKitGrid.empty().append($inventoryCompleteKitGridControl);
        $inventoryCompleteKitGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryCompleteKitGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryCompleteKitGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompleteKitGridControl);

        // InventorySubstituteGrid
        let $inventorySubstituteGrid = $form.find('div[data-grid="InventorySubstituteGrid"]');
        let $inventorySubstituteGridControl = jQuery(jQuery('#tmpl-grids-InventorySubstituteGridBrowse').html());
        $inventorySubstituteGrid.empty().append($inventorySubstituteGridControl);
        $inventorySubstituteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(),
                WarehouseId: warehouse.warehouseid
            };
        });
        $inventorySubstituteGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventorySubstituteGridControl);
        FwBrowse.renderRuntimeHtml($inventorySubstituteGridControl);

        // InventoryCompatibilityGrid
        let $inventoryCompatibilityGrid = $form.find('div[data-grid="InventoryCompatibilityGrid"]');
        let $inventoryCompatibilityGridControl = jQuery(jQuery('#tmpl-grids-InventoryCompatibilityGridBrowse').html());
        $inventoryCompatibilityGrid.empty().append($inventoryCompatibilityGridControl);
        $inventoryCompatibilityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryCompatibilityGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryCompatibilityGridControl);
        FwBrowse.renderRuntimeHtml($inventoryCompatibilityGridControl);

        // InventoryQcGrid
        let $inventoryQcGrid = $form.find('div[data-grid="InventoryQcGrid"]');
        let $inventoryQcGridControl = jQuery(jQuery('#tmpl-grids-InventoryQcGridBrowse').html());
        $inventoryQcGrid.empty().append($inventoryQcGridControl);
        $inventoryQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryQcGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryQcGridControl);
        FwBrowse.renderRuntimeHtml($inventoryQcGridControl);

        // InventoryAttributeValueGrid
        let $inventoryAttributeValueGrid = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        let $inventoryAttributeValueGridControl = jQuery(jQuery('#tmpl-grids-InventoryAttributeValueGridBrowse').html());
        $inventoryAttributeValueGrid.empty().append($inventoryAttributeValueGridControl);
        $inventoryAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAttributeValueGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAttributeValueGridControl);

        // InventoryVendorGrid
        let $inventoryVendorGrid = $form.find('div[data-grid="InventoryVendorGrid"]');
        let $inventoryVendorGridControl = jQuery(jQuery('#tmpl-grids-InventoryVendorGridBrowse').html());
        $inventoryVendorGrid.empty().append($inventoryVendorGridControl);
        $inventoryVendorGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryVendorGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryVendorGridControl);
        FwBrowse.renderRuntimeHtml($inventoryVendorGridControl);

        // InventoryWarehouseStagingGrid
        let $inventoryWarehouseStagingGrid = $form.find('div[data-grid="InventoryWarehouseStagingGrid"]');
        let $inventoryWarehouseStagingGridControl = jQuery(jQuery('#tmpl-grids-InventoryWarehouseStagingGridBrowse').html());
        $inventoryWarehouseStagingGrid.empty().append($inventoryWarehouseStagingGridControl);
        $inventoryWarehouseStagingGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryWarehouseStagingGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryWarehouseStagingGridControl);
        FwBrowse.renderRuntimeHtml($inventoryWarehouseStagingGridControl);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        this.afterLoadSetClassification($form);
        this.addAssetTab($form);

        if ($form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'))
        }

        if ($form.find('[data-datafield="InventoryTypeIsWardrobe"] .fwformfield-value').prop('checked') === true) {
            $form.find('.wardrobetab').show();
        };

        if ($form.find('[data-datafield="SubCategoryCount"] .fwformfield-value').val() > 0) {
            FwFormField.enable($form.find('.subcategory'));
        } else {
            FwFormField.disable($form.find('.subcategory'));
        }

    }
    //----------------------------------------------------------------------------------------------
    addAssetTab($form: any): void {
        var classificationValue = FwFormField.getValueByDataField($form, 'Classification');
        var trackedByValue      = FwFormField.getValueByDataField($form, 'TrackedBy');

        if ((classificationValue === 'I' || classificationValue === 'A') && (trackedByValue !== 'QUANTITY')) {
            $form.find('.tab.asset').show();
            var $submoduleAssetBrowse = this.openAssetBrowse($form);
            $form.find('.tabpage.asset').html($submoduleAssetBrowse);
            $submoduleAssetBrowse.find('div.btn[data-type="NewMenuBarButton"]')
                .off('click')
                .on('click', function () {
                    var $browse       = jQuery(this).closest('.fwbrowse');
                    var controller    = $browse.attr('data-controller');
                    var assetFormData = {
                        InventoryId: FwFormField.getValueByDataField($form, 'InventoryId')
                    };
                    if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
                    if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
                    var $assetForm = window[controller]['openForm']('NEW', assetFormData);
                    FwModule.openSubModuleTab($browse, $assetForm);
                })
            ;
        }
    }
    //----------------------------------------------------------------------------------------------
    openAssetBrowse($form: any) {
        let $browse, inventoryId;
        $browse = AssetController.openBrowse();
        inventoryId = FwFormField.getValueByDataField($form, 'InventoryId');

        $browse.data('ondatabind', request => {
            request.activeviewfields = AssetController.ActiveViewFields;
            request.uniqueids = {
                InventoryId: inventoryId
            }
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const InventoryTypeValue = jQuery($grid.find('[data-validationname="InventoryTypeValidation"] input')).val();
        const CategoryTypeId = jQuery($grid.find('[data-validationname="RentalCategoryValidation"] input')).val();

        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids = {
                    Rental: true
                };
                break;
            case 'RentalCategoryValidation':
                request.uniqueids = {
                    InventoryTypeId: InventoryTypeValue
                };
                break;
            case 'SubCategoryValidation':
                request.uniqueids = {
                    TypeId: InventoryTypeValue,
                    CategoryId: CategoryTypeId
                };
                break;
        };
    }
    //--------------------------------------------------------------------------------------------
}

var InventoryItemController = new InventoryItem();