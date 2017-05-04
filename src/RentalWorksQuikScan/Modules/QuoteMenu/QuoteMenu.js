RwQuoteMenu = {};
//----------------------------------------------------------------------------------------------
RwQuoteMenu.getQuoteMenuScreen = function(viewModel, properties) {
    var screen, combinedViewModel, $search, $addnew, $records, $neworder;
    combinedViewModel = jQuery.extend({
        captionPageTitle:        RwLanguages.translate('QuikPick'),
        captionPageSubTitle:     RwLanguages.translate('')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-quoteMenu').html(), combinedViewModel);
    screen = {};
    screen.viewModel = viewModel;
    screen.properties = properties;
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    $search   = screen.$view.find('.qm-search');
    $records  = screen.$view.find('.qm-records');
    $neworder = screen.$view.find('.qm-newquote');

    $addnew = FwMobileMasterController.modulecontrols.triggerHandler('addnew');
    $addnew.on('click', function() {
        var $back, $continue;
        $addnew.hide();
        $search.hide();
        $records.hide();
        $neworder.rendernew();

        $addnew.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', 'back', true, function() {
            try {
                $search.show();
                $records.show();
                $neworder.empty();
                $addnew.show();
                $addnew.$back.remove();
                $continue.remove();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $continue = FwMobileMasterController.addFormControl(screen, 'Continue', 'right', 'continue', true, function() {
            $neworder.createquoteorder();
        });
    });

    $search
        .on('change', '.fwmobilecontrol-value', function() {
            var $this;
            $this = jQuery(this);
            $search.findquote($this.val().toUpperCase());
        })
        .on('click', '.fwmobilecontrol-search', function() {
            $search.findquote($search.find('.fwmobilecontrol-value').val().toUpperCase());
        })
    ;
    $search.findquote = function(searchvalue) {
        var request;
        $records.empty();
        request = {
            barcode: searchvalue
        }
        RwServices.callMethod("QuoteMenu", "FindQuote", request, function(response) {
            try {
                if (response.dealorder.length > 0) {
                    $records.loadrecords(response.dealorder);
                } else if (response.dealorder.length == 0) {
                    $records.append('<div class="norecords" style="text-align:center;">0 records found.</div>');
                    application.playStatus(false);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    $records
        .on('click', '.record', function() {
            var $this = jQuery(this);
            screen.loadquote($this.data('recorddata').orderid, $this.data('recorddata').orderno, $this.data('recorddata').orderdesc, $this.data('recorddata').ordertype);
        })
    ;
    $records.loadrecords = function(records) {
        var html = [], $item;
        html.push('<div class="record order">');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Desc:</div>');
        html.push('    <div class="value desc"></div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Order:</div>');
        html.push('    <div class="value orderno"></div>');
        html.push('    <div class="caption dynamic">Date:</div>');
        html.push('    <div class="value date"></div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Deal:</div>');
        html.push('    <div class="value deal"></div>');
        html.push('  </div>');
        html.push('  <div class="row">');
        html.push('    <div class="caption fixed">Start Date:</div>');
        html.push('    <div class="value startdate"></div>');
        html.push('    <div class="caption dynamic">End Date:</div>');
        html.push('    <div class="value enddate"></div>');
        html.push('  </div>');
        html.push('</div>');
        

        for (var i = 0; i < records.length; i++) {
            $item = jQuery(html.join(''));

            $item.find('.desc').html(records[i].orderdesc);
            $item.find('.orderno').html(records[i].orderno);
            $item.find('.date').html(records[i].orderdate);
            $item.find('.deal').html(records[i].deal);
            $item.find('.startdate').html(records[i].estrentfrom);
            $item.find('.enddate').html(records[i].estrentto);

            $item.data('recorddata', records[i]);
            $records.append($item);
        }
    };


    $neworder
        .on('change', 'div[data-datafield="startdate"] input', function() {
            var $this, $enddate;

            $this = jQuery(this);
            $enddate = $neworder.find('div[data-datafield="enddate"] input');
            if ($this.val() > $enddate.val()) {
                $enddate.val($this.val()).change();
            }
        })
        .on('change', 'div[data-datafield="enddate"] input', function() {
            var $this, $startdate;

            $this = jQuery(this);
            $startdate = $neworder.find('div[data-datafield="startdate"] input');
            if ($startdate.val() > $this.val()) {
                $this.val($startdate.val()).change();
            }
        })
    ;
    $neworder.rendernew = function() {
        var html = [], $newbody, applicationOptions, ratevalues = [];
        applicationOptions = application.getApplicationOptions();

        html.push('<div class="qm-newquote-title">New Quote/Order</div>');
        html.push('<div class="qm-newquote-fields">');
        html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Select QuikPick Type" data-type="select" data-datafield="selecttype" data-required="true" />');
        html.push('    </div>');
        html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Deal" data-type="select" data-datafield="deal" />');
        html.push('    </div>');
        html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Description" data-type="text" data-datafield="description" data-required="true" />');
        html.push('    </div>');
        html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Estimated Start Date" data-type="date" data-datafield="startdate" data-required="true" />');
        html.push('    </div>');
        html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Estimated End Date" data-type="date" data-datafield="enddate" />');
        html.push('    </div>');
        html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Rate" data-type="select" data-datafield="rate" />');
        html.push('    </div>');
        html.push('</div>');
        $newbody = jQuery(html.join(''));

        FwControl.renderRuntimeControls($newbody.find('.fwcontrol'));

        FwFormField.loadItems($newbody.find('div[data-datafield="selecttype"]'), [
            {value:'Q',     text:'Quote'},
            {value:'O',     text:'Order'}
        ], true);

        RwServices.callMethod("QuoteMenu", "GetDeals", {}, function(response) {
            var dealvalues = []
            for (var i = 0; i < response.deals.length; i++) {
                dealvalues.push({value: response.deals[i].dealid, text: response.deals[i].deal});
            }
            FwFormField.loadItems($newbody.find('div[data-datafield="deal"]'), dealvalues, false);
        });

        ratevalues.push({value:'DAILY',    text:'Daily Rate'});
        ratevalues.push({value:'WEEKLY',   text:'Weekly Rate'});
        ratevalues.push({value:'MONTHLY',  text:'Monthly Rate'});
        if ((typeof applicationOptions.tieredpricing !== 'undefined') && (applicationOptions.tieredpricing.enabled)) ratevalues.push({value:'TIEREDWK', text:'Tiered Week Rate'});
        FwFormField.loadItems($newbody.find('div[data-datafield="rate"]'), ratevalues, true);

        FwFormField.setValue($newbody, 'div[data-datafield="startdate"]', FwFunc.getDate());

        $neworder.append($newbody);
    };
    $neworder.createquoteorder = function() {
        var request;
        if ($neworder.validate()) {
            request = {
                orderdesc:   FwFormField.getValue($neworder, 'div[data-datafield="description"]'),
                estrentfrom: FwFormField.getValue($neworder, 'div[data-datafield="startdate"]'),
                estfromtime: '',
                estrentto:   FwFormField.getValue($neworder, 'div[data-datafield="enddate"]'),
                esttotime:   '',
                dealid:      FwFormField.getValue($neworder, 'div[data-datafield="deal"]'),
                ratetype:    FwFormField.getValue($neworder, 'div[data-datafield="rate"]'),
                ordertype:   FwFormField.getValue($neworder, 'div[data-datafield="selecttype"]')
            };
            RwServices.callMethod("QuoteMenu", "NewQuote", request, function(response) {
                try {
                    $addnew.$back.click();
                    screen.loadquote(response.order.orderid, response.order.orderno, response.order.orderdesc, '');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    };
    $neworder.validate = function() {
        var $fields, isvalid = true;
        $fields = $neworder.find('.fwformfield');
        $fields.each(function(index) {
            var $field = jQuery(this);

            if (($field.attr('data-required') == 'true') && ($field.attr('data-enabled') == 'true')) {
                if ($field.find('.fwformfield-value').val() == '') {
                    isvalid = false;
                    $field.addClass('error');
                }
            }
            if ($field.hasClass('error')) {
                isvalid = false;
            }
            if (isvalid) {
                $field.removeClass('error');
            }
        })

        return isvalid;
    };

    screen.loadquote = function(orderid, orderno, description, ordertype) {
        var quoteScreen_properties;
        quoteScreen_properties = {
            orderid:     orderid,
            orderno:     orderno,
            description: description,
            ordertype:   ordertype
        };
        application.pushScreen(RwQuote.getQuoteScreen({}, quoteScreen_properties));
    };

    screen.load = function() {
        application.setScanTarget('.fwmobilecontrol-value');
    };

    return screen;
};