declare var FwModule: any;
declare var FwBrowse: any;

    class WardrobeMaterial {
        Module: string;
        apiurl: string;

        constructor() {
            this.Module = 'WardrobeMaterial';
            this.apiurl = 'api/v1/wardrobematerial';
        }

        getModuleScreen() {
            var screen, $browse;

            screen = {};
            screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
            screen.viewModel = {};
            screen.properties = {};

            $browse = this.openBrowse();

            screen.load = function () {
                FwModule.openModuleTab($browse, 'Wardrobe Material', false, 'BROWSE', true);
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
            $form.find('div.fwformfield[data-datafield="WardrobeMaterialId"] input').val(uniqueids.WardrobeMaterialId);
            FwModule.loadForm(this.Module, $form);

                return $form;
        }

        saveForm($form: any, closetab: boolean, navigationpath: string)
            {
                FwModule.saveForm(this.Module, $form, closetab, navigationpath);
            }

        loadAudit($form: any) {
                var uniqueid;
                uniqueid = $form.find('div.fwformfield[data-datafield="WardrobeMaterialId"] input').val();
                FwModule.loadAudit($form, uniqueid);
        }

        afterLoad($form: any) {

        }

    }

(<any>window).WardrobeMaterialController = new WardrobeMaterial();