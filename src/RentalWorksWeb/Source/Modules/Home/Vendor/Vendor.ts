declare var FwModule: any;
declare var FwBrowse: any;

    class Vendor {
        Module: string;
        apiurl: string;

        constructor() {
            this.Module = 'Vendor';
            this.apiurl = 'api/v1/vendor';
        }

        getModuleScreen() {
            var screen, $browse;

            screen = {};
            screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
            screen.viewModel = {};
            screen.properties = {};

            $browse = this.openBrowse();

            this.events();

            screen.load = function () {
                FwModule.openModuleTab($browse, 'Vendor', false, 'BROWSE', true);
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            };
            screen.unload = function () {
                FwBrowse.screenunload($browse);
            };

            return screen;
        }

        events(): void {

            var $parent = jQuery(document);
            
            $parent.on('click', '.vendertyperadio input[type=radio]', (e) => {
                this.togglePanels(jQuery(e.currentTarget).val())                
            });

        }

        togglePanels(type: string): void {
            jQuery('.type_panels').hide();
            switch (type) {
                case 'Company':
                    jQuery('#company_panel').show();
                    break;
                case 'Person':
                    jQuery('#person_panel').show();
                    break;
                default:
                    throw Error(type + ' is not a known type.');
            }        
        }

        renderGrids = function ($form: any) {

            var $comapnyTaxGrid, $companyTaxGridControl;
            
            $comapnyTaxGrid = $form.find('div[data-grid="PersonalEvent"]');
            $companyTaxGridControl = jQuery(jQuery('#tmpl-grids-ContactPersonalEventBrowse').html());
            $comapnyTaxGrid.empty().append($companyTaxGridControl);
            $companyTaxGridControl.data('ondatabind', function (request) {
                request.module = 'ContactPersonalEvent';
                request.uniqueids = {
                    contactid: $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val()
                };
                FwServices.grid.method(request, 'ContactPersonalEvent', 'Browse', $companyTaxGridControl, function (response) {
                    FwBrowse.databindcallback($companyTaxGridControl, response.browse);
                });
            });
            FwBrowse.init($companyTaxGridControl);
            FwBrowse.renderRuntimeHtml($companyTaxGridControl);

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
            $form.find('div.fwformfield[data-datafield="VendorId"] input').val(uniqueids.VendorId);
            FwModule.loadForm(this.Module, $form);

                return $form;
        }

        saveForm($form: any, closetab: boolean, navigationpath: string)
            {
                FwModule.saveForm(this.Module, $form, closetab, navigationpath);
            }

        loadAudit($form: any) {
                var uniqueid;
                uniqueid = $form.find('div.fwformfield[data-datafield="VendorId"] input').val();
                FwModule.loadAudit($form, uniqueid);
        }

        afterLoad($form: any) {

        }

    }

(window as any).VendorController = new Vendor();