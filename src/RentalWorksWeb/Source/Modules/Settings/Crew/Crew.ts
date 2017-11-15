﻿declare var FwModule: any;
declare var FwBrowse: any;

class Crew {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Crew';
        this.apiurl = 'api/v1/crew';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Crew', false, 'BROWSE', true);
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

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CrewId"] input').val(uniqueids.CrewId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CrewId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var nameCrewPositionGrid: string = 'CrewPositionGrid';
        var $crewPositionGrid: any = $crewPositionGrid = $form.find('div[data-grid="' + nameCrewPositionGrid + '"]');
        var $crewPositionGridControl: any = FwBrowse.loadGridFromTemplate(nameCrewPositionGrid);

        $crewPositionGrid.empty().append($crewPositionGridControl);
        $crewPositionGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CrewId: FwFormField.getValueByDataField($form, 'CrewId')
            };
        });
        $crewPositionGridControl.data('beforesave', function (request) {
            request.CrewId = FwFormField.getValueByDataField($form, 'CrewId')
        });
        FwBrowse.init($crewPositionGridControl);
        FwBrowse.renderRuntimeHtml($crewPositionGridControl);

        var nameCrewLocationGrid: string = 'CrewLocationGrid';
        var $crewLocationGrid: any = $crewLocationGrid = $form.find('div[data-grid="' + nameCrewLocationGrid + '"]');
        var $crewLocationGridControl: any = FwBrowse.loadGridFromTemplate(nameCrewLocationGrid);

        $crewLocationGrid.empty().append($crewLocationGridControl);
        $crewLocationGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CrewId: FwFormField.getValueByDataField($form, 'CrewId')
            };
        });
        $crewLocationGridControl.data('beforesave', function (request) {
            request.CrewId = FwFormField.getValueByDataField($form, 'CrewId')
        });
        FwBrowse.init($crewLocationGridControl);
        FwBrowse.renderRuntimeHtml($crewLocationGridControl);
    }

    afterLoad($form: any) {
        var $crewPositionGrid: any = $form.find('[data-name="CrewPositionGrid"]');
        FwBrowse.search($crewPositionGrid);

        var $crewLocationGrid: any = $form.find('[data-name="CrewLocationGrid"]');
        FwBrowse.search($crewLocationGrid);

    }
}

(<any>window).CrewController = new Crew();

