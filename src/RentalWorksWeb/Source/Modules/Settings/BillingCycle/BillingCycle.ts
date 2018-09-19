﻿class BillingCycle {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'BillingCycle';
        this.apiurl = 'api/v1/billingcycle';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Billing Cycle', false, 'BROWSE', true);
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

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.find('div[data-datafield="BillingCycleType"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
   
            if ($this.prop('checked') === true && $this.val() === "EVENTS") {
                $form.find(".eventstab").show();
            } else {
                $form.find(".eventstab").hide();
            }
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BillingCycleId"] input').val(uniqueids.BillingCycleId);
        FwModule.loadForm(this.Module, $form);


        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="BillingCycleId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var $billingCycleEventsGrid: any;
        var $billingCycleEventsGridControl: any;

        // load AttributeValue Grid
        $billingCycleEventsGrid = $form.find('div[data-grid="BillingCycleEventsGrid"]');
        $billingCycleEventsGridControl = jQuery(jQuery('#tmpl-grids-BillingCycleEventsGridBrowse').html());
        $billingCycleEventsGrid.empty().append($billingCycleEventsGridControl);
        $billingCycleEventsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                BillingCycleId: $form.find('div.fwformfield[data-datafield="BillingCycleId"] input').val()
            };
        });
        FwBrowse.init($billingCycleEventsGridControl);
        FwBrowse.renderRuntimeHtml($billingCycleEventsGridControl);
    }

    afterLoad($form: any) {
        var $billingCycleEventsGrid: any;

        $billingCycleEventsGrid = $form.find('[data-name="BillingCycleEventsGrid"]');
        FwBrowse.search($billingCycleEventsGrid);


        var radioType = FwFormField.getValueByDataField($form, 'BillingCycleType');
        if (radioType === "EVENTS") {
            $form.find(".eventstab").show();
        } else {
            $form.find(".eventstab").hide();
        }

    }
}

var BillingCycleController = new BillingCycle();