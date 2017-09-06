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

        renderGrids($form: any) {
            var $companyTaxGrid, $companyTaxControl: any;

            // load AttributeValue Grid
            $companyTaxGrid = $form.find('div[data-grid="CompanyTaxGrid"]');
            $companyTaxControl = jQuery(jQuery('#tmpl-grids-CompanyTaxGridBrowse').html());
            $companyTaxGrid.empty().append($companyTaxControl);
            $companyTaxControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    CompanyId: $form.find('div.fwformfield[data-datafield="VendorId"] input').val()
                }
            });
            FwBrowse.init($companyTaxControl);
            FwBrowse.renderRuntimeHtml($companyTaxControl);
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
            var $companyTaxGrid: any;

            $companyTaxGrid = $form.find('[data-name="CompanyTaxGrid"]');
            FwBrowse.search($companyTaxGrid);
        }

    }

(window as any).VendorController = new Vendor();