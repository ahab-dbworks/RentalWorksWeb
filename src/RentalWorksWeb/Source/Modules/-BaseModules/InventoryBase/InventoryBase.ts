class InventoryBase {
    Module: string = 'BaseInventory';
    ActiveView: string = 'ALL';
    caption: string = 'Base Inventory';
    AvailableFor: string = '';

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
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let self = this;
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        this.ActiveView = 'ALL'; // Resets view to all when revisting module page

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'Item', '#ffffff');
        FwBrowse.addLegend($browse, 'Accessory', '#fffa00');
        FwBrowse.addLegend($browse, 'Complete', '#0080ff');
        FwBrowse.addLegend($browse, 'Kit', '#00c400');
        FwBrowse.addLegend($browse, 'Misc', '#ff0080');
        if (this.AvailableFor === "R") {
            FwBrowse.addLegend($browse, 'Container', '#ff8040');
        }

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, uniqueids) {
        var $form, controller, $calendar;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //let warehouseId = JSON.parse(sessionStorage.warehouse).warehouseid;
        let warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;   //justin 11/11/2018 fixing build error
        let inventoryId = uniqueids.InventoryId;
        let startOfMonth = moment().startOf('month').format('MM/DD/YYYY');
        let endOfMonth = moment().endOf('month').format('MM/DD/YYYY');

        $calendar = $form.find('.calendar');
        $calendar
            .data('ongetevents', function (request) {
                startOfMonth = moment(request.start.value).format('MM/DD/YYYY');
                endOfMonth = moment(request.start.value).add(request.days, 'd').format('MM/DD/YYYY');

                FwAppData.apiMethod(true, 'GET', `api/v1/inventoryavailabilitydate?InventoryId=${inventoryId}&WarehouseId=${warehouseId}&FromDate=${startOfMonth}&ToDate=${endOfMonth}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwScheduler.loadEventsCallback($calendar, [{ id: '1', name: '' }], response);
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


        if (mode === 'NEW') {
            this.setupNewMode($form);
        }

        this.loadScheduler($form);

        controller = $form.attr('data-controller');
        if (typeof window[controller]['openFormInventory'] === 'function') {
            window[controller]['openFormInventory']($form);
        }

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form, $calendar, schddate;

        $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);

        $calendar = $form.find('.calendar');
        if ($calendar.length > 0) {
            setTimeout(function () {
                schddate = FwScheduler.getTodaysDate();
                FwScheduler.navigate($calendar, schddate);
                FwScheduler.refresh($calendar);
            }, 1);
        }

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let classificationValue, trackedByValue;

        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
            else {
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
        // Hides or shows Asset tab for particular settings on the form
        $form.find('.class-tracked-radio input').on('change', () => {
            classificationValue = FwFormField.getValueByDataField($form, 'Classification');
            trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');
            if (classificationValue === 'I' || classificationValue === 'A') {
                if (trackedByValue !== 'QUANTITY') {
                    $form.find('.asset-submodule').show();
                } else {
                    $form.find('.asset-submodule').hide();
                }
            } else {
                $form.find('.asset-submodule').hide();
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    loadScheduler($form) {

        var dp = new DayPilot.Scheduler($form.find('#gantt')[0]);

        // behavior and appearance
        dp.cellWidth = 40;
        dp.eventHeight = 30;
        dp.headerHeight = 25;

        // view
        dp.startDate = moment().format('YYYY-MM-DD');  // or just dp.startDate = "2013-03-25";
        dp.days = 31;
        dp.scale = "Day";
        dp.timeHeaders = [
            { groupBy: "Month" },
            { groupBy: "Day", format: "d" }
        ];
        dp.treeEnabled = true;
        dp.resources = [
            {
                name: "200002-024", id: "A", expanded: true, children: [
                    { name: "A", id: "A.1" },
                    { name: "B", id: "A.2" }
                ]
            },
            { name: "L300044", id: "B" },
            { name: "L300230", id: "C" },
            { name: "L300962", id: "D" }
        ];
        dp.events.list = [
            {
                start: "2018-11-30T00:00:00",
                end: "2018-12-10T00:00:00",
                id: "1",
                resource: "A",
                text: "200002-024 PENDING EXCHANGE (DED RENTALS)",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            },
            {
                start: "2018-12-13T00:00:00",
                end: "2018-12-27T00:00:00",
                id: "6",
                resource: "A",
                text: "200002-024 PENDING EXCHANGE (DED RENTALS)",
                orderNumber: "200002-024",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            },
            {
                start: "2018-11-30T00:00:00",
                end: "2018-12-27T00:00:00",
                id: "2",
                resource: "B",
                text: "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                orderNumber: "L300044",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            },
            {
                start: "2018-11-30T00:00:00",
                end: "2018-12-27T00:00:00",
                id: "2",
                resource: "C",
                text: "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
                orderNumber: "L300230",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            },
            {
                start: "2018-11-30T00:00:00",
                end: "2018-12-27T00:00:00",
                id: "2",
                resource: "D",
                text: "L300962 ORDER FOR STAGING A CONTAINER (ENDLESS3)",
                orderNumber: "L300962",
                orderStatus: "CONFIRMED",
                deal: "Testing"
            }
        ];

        dp.bubble = new DayPilot.Bubble({
            cssClassPrefix: "bubble_default",
            onLoad: function (args) {
                var ev = args.source;
                args.async = true;  // notify manually using .loaded()

                // simulating slow server-side load
                    args.html = "<div style='font-weight:bold'>" + ev.text() + "</div><div>Order Number: " + ev.data.orderNumber + "</div><div>Order Status: " + ev.data.orderStatus + "</div><div>Deal: " + ev.data.deal + "</div><div>Start: " + ev.start().toString("MM/dd/yyyy HH:mm") + "</div><div>End: " + ev.end().toString("MM/dd/yyyy HH:mm") + "</div><div>Id: " + ev.id() + "</div>";
                    args.loaded();

            }
        });

        dp.init();

        //dp.onBeforeEventRender = function (args) {
        //    args.data.html = '123';
        //    args.data.backColor = "#ffc0c0";
        //};
        //dp.update();
    }
    //----------------------------------------------------------------------------------------------
    loadGantt($form) {
        var dp = new DayPilot.Gantt($form.find('#gantt')[0]);
        dp.startDate = moment().format('YYYY-MM-DD');
        dp.days = 31;
        dp.init();
        dp.onBeforeRowHeaderRender = function (args) {
            args.row.backColor = 'coral';
        };
        dp.onBeforeCellRender = function (args) {
            args.cell.backColor = 'coral';
        };

        dp.tasks.list = [
            {
                "id": "1",
                "text": "200002-024",
                "start": "2018-11-30T00:00:00",
                "end": "2018-12-15T00:00:00",
                "complete": "200002-024 PENDING EXCHANGE (DED RENTALS)",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "200002-024 PENDING EXCHANGE (DED RENTALS)",
                    "htmlRight": "200002-024"
                },
            },
            {
                "id": "2",
                "text": "L300044",
                "start": "2018-11-30T00:00:00",
                "end": "2018-12-30T00:00:00",
                "complete": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                    "htmlRight": "L300044"
                },
            },
            {
                "id": "5",
                "text": "L300044",
                "start": "2018-12-16T00:00:00",
                "end": "2018-12-30T00:00:00",
                "complete": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "L300044 SUB-RENTAL REPORT - SUBS (THE MOVIE HOUSE - 2014 RENTALS)",
                    "htmlRight": "L300044"
                },
            },
            {
                "id": "3",
                "text": "L300230",
                "start": "2018-11-30T00:00:00",
                "end": "2018-12-30T00:00:00",
                "complete": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
                "title": "hello",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
                    "title": "L300230 CROSS I-CODE EXCHANGE TEST (FELD RENTALS)",
                    "htmlRight": "L300230"
                },
            },
            {
                "id": "4",
                "text": "L300962",
                "start": "2018-11-30T00:00:00",
                "end": "2018-12-02T00:00:00",
                "complete": "",
                "title": "hello",
                "box": {
                    "bubbleHtml": "Task details (box): <br/>Task 0<br>Starting on November 21, 2018",
                    "html": "L300962 ORDER FOR STAGING A CONTAINER (ENDLESS3)",
                    "title": "L300962 ORDER FOR STAGING A CONTAINER (ENDLESS3)",
                    "htmlRight": "L300962"
                },
                "row": {
                    "bubbleHtml": "Task details (row): <br/>Task 0<br>Starting on November 21, 2018"
                }
            },
        ]
        dp.update();
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        var self = this;
        var $all: JQuery = FwMenu.generateDropDownViewBtn('All', true);
        var $item: JQuery = FwMenu.generateDropDownViewBtn('Item', true);
        var $accessory: JQuery = FwMenu.generateDropDownViewBtn('Accessory', false);
        var $complete: JQuery = FwMenu.generateDropDownViewBtn('Complete', false);
        var $kitset: JQuery = FwMenu.generateDropDownViewBtn('Kit', false);
        var $misc: JQuery = FwMenu.generateDropDownViewBtn('Misc', false);
        var $container: JQuery = FwMenu.generateDropDownViewBtn('Container', false);

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $item.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ITEM';
            FwBrowse.search($browse);
        });
        $accessory.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACCESSORY';
            FwBrowse.search($browse);
        });
        $complete.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.search($browse);
        });
        $kitset.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'KIT';
            FwBrowse.search($browse);
        });
        $misc.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'MISC';
            FwBrowse.search($browse);
        });
        $container.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CONTAINER';
            FwBrowse.search($browse);
        });

        FwMenu.addVerticleSeparator($menuObject);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($item);
        viewSubitems.push($accessory);
        viewSubitems.push($complete);
        viewSubitems.push($kitset);
        viewSubitems.push($misc);
        if (this.AvailableFor === "R") {
            viewSubitems.push($container);
        }
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    setupNewMode($form: any) {
        FwFormField.enable($form.find('[data-datafield="Classification"]'));

        $form.find('div[data-datafield="Classification"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);

            $form.find('.completeskitstab').show();
            $form.find('.containertab').hide();
            $form.find('.completetab').hide();
            $form.find('.kittab').hide();

            if ($this.prop('checked') === true && $this.val() === 'I') {
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').show();
            }
            if ($this.prop('checked') === true && $this.val() === 'A') {
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').show();
            }
            if ($this.prop('checked') === true && $this.val() === 'C') {
                $form.find('.completetab').show();
                $form.find('.completeskitstab').hide();
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'K') {
                $form.find('.kittab').show();
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'N') {
                $form.find('.containertab').show();
                $form.find('.completeskitstab').hide();
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'S') {
                FwFormField.enable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').hide();
                $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
            }
            if ($this.prop('checked') === true && $this.val() === 'M') {
                FwFormField.setValueByDataField($form, 'TrackedBy', 'QUANTITY');
                FwFormField.disable($form.find('div[data-datafield="TrackedBy"]'));
                $form.find('.tracked-by-column').show();
            }
        })
    }
    //----------------------------------------------------------------------------------------------
    afterLoadSetClassification($form: any) {
        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'I' || FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'A') {
            FwFormField.enable($form.find('[data-datafield="Classification"]'));
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'N') {
            $form.find('.containertab').show();
            $form.find('.completeskitstab').hide();
            $form.find('.kitradio').hide();
            $form.find('.completeradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'C') {
            $form.find('.completetab').show();
            $form.find('.completeskitstab').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'K') {
            $form.find('.kittab').show();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.miscradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'M') {
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            FwFormField.setValueByDataField($form, 'TrackedBy', 'QUANTITY');
            FwFormField.disable($form.find('div[data-datafield="TrackedBy"]'));
        }

        if (FwFormField.getValue($form, 'div[data-datafield="Classification"]') === 'S') {
            $form.find('.completeradio').hide();
            $form.find('.kitradio').hide();
            $form.find('.itemradio').hide();
            $form.find('.accessoryradio').hide();
            $form.find('.containerradio').hide();
            $form.find('.tracked-by-column').hide();
            $form.find('div[data-datafield="TrackedBy"] input').prop('checked', false);
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            let tabname = jQuery(e.currentTarget).attr('id');
            let lastIndexOfTab = tabname.lastIndexOf('tab');
            let tabpage = tabname.substring(0, lastIndexOfTab) + 'tabpage' + tabname.substring(lastIndexOfTab + 3);

            let $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    let $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            let $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    let $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
    }
}

var InventoryBaseController = new InventoryBase();