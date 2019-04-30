class PhysicalInventory {
    Module: string = 'PhysicalInventory';
    apiurl: string = 'api/v1/physicalinventory';
    caption: string = 'Physical Inventory';
    nav: string = 'module/physicalinventory';
    id: string = 'BABFE80E-8A52-49D4-81D9-6B6EBB518E89';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Physical Inventory', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    addBrowseMenuItems($menuObject) {
        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    };

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));    
            FwFormField.setValue($form.find('.warehouse'), warehouse.warehouseid, warehouse.warehouse);
            FwFormField.setValueByDataField($form, 'CycleIncludeOwned', true);
        }

        $form.find('.prescan').on('click', function (e) {
            const $confirmation = FwConfirmation.renderConfirmation(`Pre-Scan`, 'Pre-Initializing Physical Inventory make take several minutes! </br> Continue?');
            const $yes = FwConfirmation.addButton($confirmation, 'Continue', false);
            var $cancel = FwConfirmation.addButton($confirmation, 'Close', true);
        });

        $form.find('.initate').on('click', function (e) {
            const $confirmation = FwConfirmation.renderConfirmation(`Initiate`, 'Initializing Physical Inventory make take several minutes.');
            const $yes = FwConfirmation.addButton($confirmation, 'Continue', false);
            var $cancel = FwConfirmation.addButton($confirmation, 'Close', true);
        });

        $form.find('.approve').on('click', function (e) {
            const $confirmation = FwConfirmation.renderConfirmation(`Approve`, 'Approve Physical Inventory Counts?');
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            var $no = FwConfirmation.addButton($confirmation, 'No', true);
        });

        $form.find('.closeinventorywithadj').on('click', function (e) {
            const $confirmation = FwConfirmation.renderConfirmation(`Close Physical Inventory`, 'Close Physical Inventory and apply adjustments to Inventory?');
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            var $no = FwConfirmation.addButton($confirmation, 'No', true);
        });

        $form.find('.closeinventorywithoutadj').on('click', function (e) {
            const $confirmation = FwConfirmation.renderConfirmation(`Close Physical Inventory (Without Adjustments)`, 'Close Physical Inventory without adjusting any Inventory?');
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            var $no = FwConfirmation.addButton($confirmation, 'No', true);
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PhysicalInventoryId"] input').val(uniqueids.PhysicalInventoryId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
    }

    beforeValidateInventoryType($browse, $grid, request) {
        let validationName = request.module,
            recType = FwFormField.getValueByDataField($grid, 'RecTypeDisplay');
        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids = {};
                if (recType === 'RENTAL') { request.uniqueids.Rental = true };
                if (recType === 'SALES') { request.uniqueids.Sales = true };
                if (recType === 'PARTS') { request.uniqueids.Parts = true };
                break;
        }

    }
}

var PhysicalInventoryController = new PhysicalInventory();