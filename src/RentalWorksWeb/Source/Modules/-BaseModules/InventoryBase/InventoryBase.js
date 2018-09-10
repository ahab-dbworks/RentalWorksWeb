class InventoryBase {
    constructor() {
        this.Module = 'BaseInventory';
        this.ActiveView = 'ALL';
        this.caption = 'Base Inventory';
        this.AvailableFor = '';
    }
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
    ;
    openBrowse() {
        let self = this;
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        this.ActiveView = 'ALL';
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
    }
    ;
    openForm(mode) {
        var $form, controller;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            this.setupNewMode($form);
        }
        controller = $form.attr('data-controller');
        if (typeof window[controller]['openFormInventory'] === 'function') {
            window[controller]['openFormInventory']($form);
        }
        this.events($form);
        return $form;
    }
    ;
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryId"] input').val(uniqueids.InventoryId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    ;
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    ;
    loadAudit($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    ;
    events($form) {
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
            }
            else {
                $form.find('.wardrobetab').hide();
            }
        });
        $form.find('div[data-datafield="CategoryId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('.subcategory'));
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('.subcategory'));
            }
            else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '');
            }
        });
        $form.find('.class-tracked-radio input').on('change', () => {
            classificationValue = FwFormField.getValueByDataField($form, 'Classification');
            trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');
            if (classificationValue === 'I' || classificationValue === 'A') {
                if (trackedByValue !== 'QUANTITY') {
                    $form.find('.asset-submodule').show();
                }
                else {
                    $form.find('.asset-submodule').hide();
                }
            }
            else {
                $form.find('.asset-submodule').hide();
            }
        });
    }
    addBrowseMenuItems($menuObject) {
        var self = this;
        var $all = FwMenu.generateDropDownViewBtn('All', true);
        var $item = FwMenu.generateDropDownViewBtn('Item', true);
        var $accessory = FwMenu.generateDropDownViewBtn('Accessory', false);
        var $complete = FwMenu.generateDropDownViewBtn('Complete', false);
        var $kitset = FwMenu.generateDropDownViewBtn('Kit', false);
        var $misc = FwMenu.generateDropDownViewBtn('Misc', false);
        var $container = FwMenu.generateDropDownViewBtn('Container', false);
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
        var viewSubitems = [];
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
    }
    ;
    setupNewMode($form) {
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
        });
    }
    afterLoadSetClassification($form) {
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
}
var InventoryBaseController = new InventoryBase();
//# sourceMappingURL=InventoryBase.js.map