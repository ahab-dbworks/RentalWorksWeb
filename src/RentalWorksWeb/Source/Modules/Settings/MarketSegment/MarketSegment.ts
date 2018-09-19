routes.push({ pattern: /^module\/marketsegment$/, action: function (match: RegExpExecArray) { return MarketSegmentController.getModuleScreen(); } }); 

class MarketSegment {
    Module: string = 'MarketSegment';
    apiurl: string = 'api/v1/marketsegment';

    getModuleScreen() {
        let screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Market Segment', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    renderGrids($form: any) {
        var marketSegmentJobGrid, $marketSegmentJobGridControl;

        marketSegmentJobGrid = $form.find('div[data-grid="MarketSegmentJobGrid"]');
        $marketSegmentJobGridControl = jQuery(jQuery('#tmpl-grids-MarketSegmentJobGridBrowse').html());
        marketSegmentJobGrid.empty().append($marketSegmentJobGridControl);
        $marketSegmentJobGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                MarketSegmentId: $form.find('div.fwformfield[data-datafield="MarketSegmentId"] input').val()
            }
        });
        $marketSegmentJobGridControl.data('beforesave', function (request) {
            request.MarketSegmentId = FwFormField.getValueByDataField($form, 'MarketSegmentId');
        })
        FwBrowse.init($marketSegmentJobGridControl);
        FwBrowse.renderRuntimeHtml($marketSegmentJobGridControl);
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

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="MarketSegmentId"] input').val(uniqueids.MarketSegmentId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="MarketSegmentId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $marketSegmentJobGrid: any = $form.find('[data-name="MarketSegmentJobGrid"]');
        FwBrowse.search($marketSegmentJobGrid);
    }
}

var MarketSegmentController = new MarketSegment();