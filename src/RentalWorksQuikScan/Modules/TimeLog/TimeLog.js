var TimeLog = {};
//----------------------------------------------------------------------------------------------
TimeLog.getModuleScreen = function() {
    var viewModel = {
        captionPageTitle:   RwLanguages.translate('Time Log'),
        captionOrderNo:     RwLanguages.translate('Order No'),
        captionDealNo:      RwLanguages.translate('Deal No'),
        captionEventNo:     RwLanguages.translate('Event No'),
        captionProject:     RwLanguages.translate('Project'),
        captionNewEntry:    RwLanguages.translate('New Time Log Entry'),
        captionViewEntries: RwLanguages.translate('View Time Log Entries')
    };
    viewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-timeLog').html(), viewModel);

    var screen                    = {};
    screen.$view                  = FwMobileMasterController.getMasterView(viewModel);
    screen.properties             = {};
    screen.properties.timelogdata = {};

    var $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    var $menu        = screen.$view.find('.tl-menu');
    var $search      = screen.$view.find('.tl-search');
    var $editentry   = screen.$view.find('.tl-editentry');
    var $viewentries = screen.$view.find('.tl-viewentries');

    var isCrew = (sessionStorage.userType === 'CREW');
    screen.$view.find('#miOrderNo').toggle(isCrew || (sessionStorage.getItem('iscrew') == 'T'));
    screen.$view.find('#miDealNo').toggle(isCrew || (sessionStorage.getItem('iscrew') == 'T'));
    screen.$view.find('#miEventNo').toggle(isCrew || (sessionStorage.getItem('iscrew') == 'T'));
    screen.$view.find('#miProject').toggle(!isCrew);

    $menu
        .on('click', '.fwmenu-item', function() {
            $menu.hide();
            $search.show();
            program.setScanTarget('.tl-search .fwmobilecontrol-value');
            if (!Modernizr.touch) {
                screen.$view.find('.tl-search .fwmobilecontrol-value').select();
            }
        })
        .on('click', '#miOrderNo', function() {
            $search.find('input').attr('placeholder', 'Search all, or enter Order No');
            screen.properties.timelogdata.mode = 'ORDER';
        })
        .on('click', '#miDealNo', function() {
            $search.find('input').attr('placeholder', 'Search all, or enter Deal No');
            screen.properties.timelogdata.mode = 'DEAL';
        })
        .on('click', '#miEventNo', function() {
            $search.find('input').attr('placeholder', 'Search all, or enter Event No');
            screen.properties.timelogdata.mode = 'EVENT';
        })
        .on('click', '#miProject', function() {
            $search.find('input').attr('placeholder', 'Search all, or enter Order No');
            if ($search.find('.btnpanel').length == 0) {
                $search.append('<div class="center btnpanel" style="margin-top:10px"><div class="button-strip"><div class="button default btnnoorder">No Order</div></div></div>');
                $search.on('click', '.btnnoorder', function() {
                    $viewentries.find('.title .desc').html('Order: No Order Selected.');
                    $viewentries.find('.title .number').html('');
                    screen.properties.timelogdata.selectedrecord = {};
                    $search.hide();
                    $viewentries.data('show')();
                });
            }
            screen.properties.timelogdata.mode = 'PROJECT';
        })
    ;

    $search
        .on('change', '.fwmobilecontrol-value', function() {
            var $this = jQuery(this);
            $search.data('search')($this.val());
        })
        .on('click', '.fwmobilecontrol-search', function() {
            $search.data('search')($search.find('.fwmobilecontrol-value').val());

            //jh 10/11/2016
            if (typeof screen.$btnback != 'undefined') { screen.$btnback.remove(); }
            if (typeof screen.$btncontinue != 'undefined') { screen.$btncontinue.remove(); }
        })
        .on('click', '.tl-search-foundmulti .record', function() {
            var $this;
            $this = jQuery(this);
            $search.find('.tl-search-foundmulti').hide();
            $search.data('loadrecord')($this.data('recorddata'), true);
        })
        .data({
            search: function(value) {
                var request = {};

                $search.find('.tl-search-foundrecord').empty().hide();
                $search.find('.tl-search-foundmulti').empty().hide();

                //jh 10/11/2016
                if (typeof screen.$btnback != 'undefined') { screen.$btnback.remove(); }
                if (typeof screen.$btncontinue != 'undefined') { screen.$btncontinue.remove(); }

                request = {
                    mode:  screen.properties.timelogdata.mode,
                    value: value
                };
                RwServices.utility.timelogsearch(request, function(response) {
                    if (response.records.length == 1) {
                        $search.data('loadrecord')(response.records[0], false);
                    } else if (response.records.length > 1) {
                        $search.data('loadrecords')(response.records);
                    } else {
                        $search.find('.tl-search-foundrecord').show().append('<div class="norecords" style="text-align:center;">0 records found.</div>');
                    }
                });
            },
            loadrecord: function(record, addback) {
                var html = [], $back, $continue;
                $search.find('.btnnoorder').hide();
                switch(screen.properties.timelogdata.mode) {
                    case 'ORDER':
                        html.push('<div class="field">');
                            html.push('<div class="caption"><div class="label">Order:</div><div class="number">' + record.orderno + '</div></div>');
                            html.push('<div class="value"><div class="readonlytextbox">' + record.orderdesc + '</div></div>');
                        html.push('</div>');
                        html.push('<div class="field">');
                            html.push('<div class="caption"><div class="label">Deal:</div><div class="number">' + record.dealno + '</div></div>');
                            html.push('<div class="value"><div class="readonlytextbox">' + record.deal + '</div></div>');
                        html.push('</div>');
                        break;
                    case 'DEAL':
                        html.push('<div class="field">');
                            html.push('<div class="caption"><div class="label">Deal:</div><div class="number">' + record.dealno + '</div></div>');
                            html.push('<div class="value"><div class="readonlytextbox">' + record.deal + '</div></div>');
                        html.push('</div>');
                        html.push('<div class="field">');
                            html.push('<div class="caption"><div class="label">Deal Type:</div></div>');
                            html.push('<div class="value"><div class="readonlytextbox">' + record.dealtype + '</div></div>');
                        html.push('</div>');
                        html.push('<div class="field">');
                            html.push('<div class="caption"><div class="label">Customer:</div></div>');
                            html.push('<div class="value"><div class="readonlytextbox">' + record.customer + '</div></div>');
                        html.push('</div>');
                        break;
                    case 'EVENT':
                        html.push('<div class="field">');
                            html.push('<div class="caption"><div class="label">Event:</div><div class="number">' + record.eventno + '</div></div>');
                            html.push('<div class="value"><div class="readonlytextbox">' + record.event + '</div></div>');
                        html.push('</div>');
                        break;
                    case 'PROJECT':
                        html.push('<div class="field">');
                            html.push('<div class="caption"><div class="label">Order:</div><div class="number">' + record.orderno + '</div></div>');
                            html.push('<div class="value"><div class="readonlytextbox">' + record.orderdesc + '</div></div>');
                        html.push('</div>');
                        html.push('<div class="field">');
                            html.push('<div class="caption"><div class="label">Deal:</div></div>');
                            html.push('<div class="value"><div class="readonlytextbox">' + record.deal + '</div></div>');
                        html.push('</div>');
                        break;
                }

                $back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', true, function() { //back
                    $search.find('.tl-search-foundmulti').show();
                    $search.find('.tl-search-foundrecord').empty().data('recorddata', '').hide();
                    $search.find('.btnnoorder').show();
                    $continue.remove();
                    $back.remove();
                });

                $continue = FwMobileMasterController.addFormControl(screen, 'Continue', 'right', '&#xE5CC;', true, function() { //continue
                    var recorddata;
                    recorddata = $search.find('.tl-search-foundrecord').data('recorddata');
                    switch(screen.properties.timelogdata.mode) {
                        case 'ORDER':
                            $viewentries.find('.title .desc').html(recorddata.orderdesc);
                            $viewentries.find('.title .number').html('Order: ' + recorddata.orderno);
                            break;
                        case 'DEAL':
                            $viewentries.find('.title .desc').html(recorddata.deal);
                            $viewentries.find('.title .number').html('Deal: ' + recorddata.dealno);
                            break;
                        case 'EVENT':
                            $viewentries.find('.title .desc').html(recorddata.event);
                            $viewentries.find('.title .number').html('Event: ' + recorddata.eventno);
                            break;
                        case 'PROJECT':
                            $viewentries.find('.title .desc').html(recorddata.orderdesc);
                            $viewentries.find('.title .number').html('Order: ' + recorddata.orderno);
                            break;
                    }
                    screen.properties.timelogdata.selectedrecord = recorddata;
                    $search.hide();
                    $viewentries.data('show')();
                    $continue.remove();
                    $back.remove();
                });

                //jh 10/11/2016
                screen.$btnback = $back;
                screen.$btncontinue = $continue;


                $search.find('.tl-search-foundrecord').append(html.join('')).show();
                $search.find('.tl-search-foundrecord').data('recorddata', record);
            },
            loadrecords: function(records) {
                var html = [], $record;
                switch(screen.properties.timelogdata.mode) {
                    case 'ORDER':
                        html.push('<div class="record">');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Description:</div><div class="value orderdesc"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">' + RwLanguages.translate('Order No') + ':</div><div class="value orderno"></div><div class="caption dynamic">Date:</div><div class="value orderdate"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">' + RwLanguages.translate('Deal') + ':</div><div class="value deal"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Start Date:</div><div class="value estrentfrom"></div><div class="caption dynamic">End Date:</div><div class="value estrentto"></div>');
                            html.push('</div>');
                        html.push('</div>');

                        for (var i = 0; i < records.length; i++) {
                            $record = jQuery(html.join(''));
                            $record.find('.orderdesc').html(records[i].orderdesc);
                            $record.find('.orderno').html(records[i].orderno);
                            $record.find('.deal').html(records[i].deal);
                            $record.find('.orderdate').html(FwFunc.getDate(records[i].orderdate));
                            $record.find('.estrentfrom').html(FwFunc.getDate(records[i].estrentfrom));
                            $record.find('.estrentto').html(FwFunc.getDate(records[i].estrentto));
                            $record.data('recorddata', records[i]);

                            $search.find('.tl-search-foundmulti').append($record).show();
                        }
                        break;
                    case 'DEAL':
                        html.push('<div class="record">');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Deal:</div><div class="value deal"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">' + RwLanguages.translate('Deal No') + ':</div><div class="value dealno"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Deal Type:</div><div class="value dealtype"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Customer:</div><div class="value customer"></div>');
                            html.push('</div>');
                        html.push('</div>');

                        for (var i = 0; i < records.length; i++) {
                            $record = jQuery(html.join(''));
                            $record.find('.deal').html(records[i].deal);
                            $record.find('.dealno').html(records[i].dealno);
                            $record.find('.dealtype').html(records[i].dealtype);
                            $record.find('.customer').html(records[i].customer);
                            $record.data('recorddata', records[i]);

                            $search.find('.tl-search-foundmulti').append($record).show();
                        }
                        break;
                    case 'EVENT':
                        html.push('<div class="record">');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Event:</div><div class="value event"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Event No:</div><div class="value eventno"></div>');
                            html.push('</div>');
                        html.push('</div>');

                        for (var i = 0; i < records.length; i++) {
                            $record = jQuery(html.join(''));
                            $record.find('.event').html(records[i].event);
                            $record.find('.eventno').html(records[i].eventno);
                            $record.data('recorddata', records[i]);

                            $search.find('.tl-search-foundmulti').append($record).show();
                        }
                        break;
                    case 'PROJECT':
                        html.push('<div class="record">');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Order:</div><div class="value order"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">' + RwLanguages.translate('Order No') + ':</div><div class="value orderno"></div><div class="caption dynamic">Date:</div><div class="value orderdate"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Deal:</div><div class="value deal"></div>');
                            html.push('</div>');
                            html.push('<div class="row">');
                                html.push('<div class="caption fixed">Start Date:</div><div class="value estrentfrom"></div><div class="caption dynamic">End Date:</div><div class="value estrentto"></div>');
                            html.push('</div>');
                        html.push('</div>');

                        for (var i = 0; i < records.length; i++) {
                            $record = jQuery(html.join(''));
                            $record.find('.order').html(records[i].orderdesc);
                            $record.find('.orderno').html(records[i].orderno);
                            $record.find('.deal').html(records[i].deal);
                            $record.find('.orderdate').html(FwFunc.getDate(records[i].orderdate));
                            $record.find('.estrentfrom').html(FwFunc.getDate(records[i].estrentfrom));
                            $record.find('.estrentto').html(FwFunc.getDate(records[i].estrentto));
                            $record.data('recorddata', records[i]);

                            $search.find('.tl-search-foundmulti').append($record).show();
                        }
                        break;
                }
            }
        })
    ;

    $editentry
        .on('change', '.fwformfield[data-required="true"].error', function() {
            var $this, value;
            $this = jQuery(this);
            value = FwFormField.getValue2($this);
            if (value != '') {
                $this.removeClass('error');
            }
        })
        .on('click', '.btnaddbreak', function() {
            var $this, $parent, html = [], $controls, breaks;
            $this   = jQuery(this);
            $parent = $this.parent().parent();
            breaks  = $editentry.data('breaks')+1;
            html.push('<div>');
            html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield break' + breaks + 'start" data-caption="Break ' + breaks + ' Start" data-type="time" data-datafield="" style="float:left;width:50%;" />');
            html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield break' + breaks + 'stop" data-caption="Break ' + breaks + ' Stop" data-type="time" data-datafield="" style="float:left;width:50%;" />');
            html.push('</div>');
            $controls = jQuery(html.join(''));
            $parent.empty().append($controls);
            FwControl.renderRuntimeObject($controls);
            $editentry.data('breaks', breaks);

            if (breaks < 3) {
                html = [];
                html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('  <div class="center"><div class="button default btnaddbreak">Add Break</div></div>');
                html.push('</div>');
                $parent.after(jQuery(html.join('')));
            }
        })
        .on('click', '.btnnote', function() {
            var $confirmation, $ok, $cancel;
            $confirmation = FwConfirmation.renderConfirmation('Note', '');
            $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);
            $ok           = FwConfirmation.addButton($confirmation, 'OK', true);
            FwConfirmation.addControls($confirmation, '<div data-control="FwFormField" class="fwcontrol fwformfield note" data-caption="" data-type="textarea" data-maxlength="255" data-datafield="" />');
            $confirmation.find('.note .fwformfield-value').val($editentry.data('note'))

            $ok.on('click', function() {
                $editentry.data('note', $confirmation.find('.note .fwformfield-value').val());
            });
        })
        .on('change', '.fwformfield[data-type="time"] input.fwformfield-value', function() {
            var hours = 0, starttime, stoptime, breaks;
            starttime = FwFormField.getValue($editentry, '.starttime');
            stoptime  = FwFormField.getValue($editentry, '.stoptime');
            breaks    = $editentry.data('breaks');

            if ((starttime !== '') && (stoptime !== '')) {
                hours = ( new Date("1970-01-01T" + FwFunc.convert12to24(stoptime)) - new Date("1970-01-01T" + FwFunc.convert12to24(starttime)) ) / 1000 / 60 / 60;

                for (var i = 1; i < breaks+1; i++) {
                    var breakstarttime, breakstoptime;
                    breakstarttime = FwFormField.getValue($editentry, '.break' + i + 'start');
                    breakstoptime  = FwFormField.getValue($editentry, '.break' + i + 'stop');
                    if ((breakstarttime !== '') && (breakstoptime !== '')) {
                        hours = hours - (( new Date("1970-01-01T" + FwFunc.convert12to24(breakstoptime)) - new Date("1970-01-01T" + FwFunc.convert12to24(breakstarttime)) ) / 1000 / 60 / 60);
                    }
                }
            }

            FwFormField.setValue($editentry, '.totalhours', hours);
        })
        .data({
            show: function() {
                var html = [], request, $back, $submit;
                $editentry.data('breaks', 0);
                $editentry.data('note', '');
                switch(screen.properties.timelogdata.mode) {
                    case 'ORDER':
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield date" data-caption="Date" data-type="date" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield position" data-caption="Position" data-type="text" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<hr>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield starttime" data-caption="Start Time" data-type="time" data-required="true" data-datafield="" style="float:left;width:50%;" />');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield stoptime" data-caption="Stop Time" data-type="time" data-required="true" data-datafield="" style="float:left;width:50%;" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div class="center"><div class="button default btnaddbreak">Add Break</div></div>');
                        html.push('</div>');
                        html.push('<hr>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield totalhours" data-caption="Total Hours" data-type="number" data-enabled="false" data-datafield="" style="float:left;width:50%;" />');
                        html.push('  <div class="center" style="float:left;width:50%;padding-top:7px;"><div class="button default btnnote">Note</div></div>');
                        html.push('</div>');
                        break;
                    case 'DEAL':
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield date" data-caption="Date" data-type="date" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield order" data-caption="Order" data-type="text" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield position" data-caption="Position" data-type="text" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<hr>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield starttime" data-caption="Start Time" data-type="time" data-required="true" data-datafield="" style="float:left;width:50%;" />');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield stoptime" data-caption="Stop Time" data-type="time" data-required="true" data-datafield="" style="float:left;width:50%;" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div class="center"><div class="button default btnaddbreak">Add Break</div></div>');
                        html.push('</div>');
                        html.push('<hr>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield totalhours" data-caption="Total Hours" data-type="number" data-enabled="false" data-datafield="" style="float:left;width:50%;" />');
                        html.push('  <div class="center" style="float:left;width:50%;padding-top:7px;"><div class="button default btnnote">Note</div></div>');
                        html.push('</div>');
                        break;
                    case 'EVENT':
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield date" data-caption="Date" data-type="date" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield deal" data-caption="Deal" data-type="text" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield order" data-caption="Order" data-type="text"  data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield position" data-caption="Position" data-type="text" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<hr>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield starttime" data-caption="Start Time" data-type="time" data-required="true" data-datafield="" style="float:left;width:50%;" />');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield stoptime" data-caption="Stop Time" data-type="time" data-required="true" data-datafield="" style="float:left;width:50%;" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div class="center"><div class="button default btnaddbreak">Add Break</div></div>');
                        html.push('</div>');
                        html.push('<hr>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield totalhours" data-caption="Total Hours" data-type="number" data-enabled="false" data-datafield="" style="float:left;width:50%;" />');
                        html.push('  <div class="center" style="float:left;width:50%;padding-top:7px;"><div class="button default btnnote">Note</div></div>');
                        html.push('</div>');
                        break;
                    case 'PROJECT':
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield date" data-caption="Date" data-type="date" data-required="true" data-datafield="" />');
                        html.push('</div>');
                        html.push('<hr>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield order" data-caption="Order" data-type="text" data-enabled="false" data-datafield="" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield description" data-caption="Description of work" data-type="textarea" data-required="true" data-datafield="" />');
                        html.push('</div>');
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield hours" data-caption="Hours" data-type="number" data-required="true" data-datafield="" />');
                        html.push('</div>');
                        break;
                }
                $editentry.find('.card-body').append(html.join(''));
                FwControl.renderRuntimeObject($editentry.find('.card-body'));

                if ((screen.properties.timelogdata.mode == 'ORDER') || (screen.properties.timelogdata.mode == 'DEAL') || (screen.properties.timelogdata.mode == 'EVENT')) {
                    FwFormField.setValue($editentry, '.date',     FwFunc.getDate($editentry.data('recorddata').thedate));
                    (screen.properties.timelogdata.mode == 'EVENT') ? FwFormField.setValue($editentry, '.deal', $editentry.data('recorddata').deal) : '';
                    ((screen.properties.timelogdata.mode == 'DEAL') || (screen.properties.timelogdata.mode == 'EVENT')) ? FwFormField.setValue($editentry, '.order', $editentry.data('recorddata').orderdesc) : '';
                    FwFormField.setValue($editentry, '.position', $editentry.data('recorddata').description);
                    if ($editentry.data('recorddata').notes != '') {
                        $editentry.data('note', $editentry.data('recorddata').notes);
                        $editentry.find('.btnnote').html('Edit Note');
                    }

                    if ($editentry.data('recorddata').rowtype == 'TIME') {
                        FwFormField.setValue($editentry, '.starttime',  FwFunc.convert24to12($editentry.data('recorddata').starttime));
                        FwFormField.setValue($editentry, '.stoptime',   FwFunc.convert24to12($editentry.data('recorddata').stoptime));
                        FwFormField.setValue($editentry, '.totalhours', parseInt($editentry.data('recorddata').hours) + parseInt($editentry.data('recorddata').hoursot) + parseInt($editentry.data('recorddata').hoursdt));
                        if (($editentry.data('recorddata').break1starttime != '') && ($editentry.data('recorddata').break1stoptime != '')) {
                            $editentry.find('.btnaddbreak').click();
                            FwFormField.setValue($editentry, '.break1start', FwFunc.convert24to12($editentry.data('recorddata').break1starttime));
                            FwFormField.setValue($editentry, '.break1stop',  FwFunc.convert24to12($editentry.data('recorddata').break1stoptime));
                        }
                        if (($editentry.data('recorddata').break2starttime != '') && ($editentry.data('recorddata').break2stoptime != '')) {
                            $editentry.find('.btnaddbreak').click();
                            FwFormField.setValue($editentry, '.break2start', FwFunc.convert24to12($editentry.data('recorddata').break2starttime));
                            FwFormField.setValue($editentry, '.break2stop',  FwFunc.convert24to12($editentry.data('recorddata').break2stoptime));
                        }
                        if (($editentry.data('recorddata').break3starttime != '') && ($editentry.data('recorddata').break3stoptime != '')) {
                            $editentry.find('.btnaddbreak').click();
                            FwFormField.setValue($editentry, '.break3start', FwFunc.convert24to12($editentry.data('recorddata').break3starttime));
                            FwFormField.setValue($editentry, '.break3stop',  FwFunc.convert24to12($editentry.data('recorddata').break3stoptime));
                        }
                    }
                } else if (screen.properties.timelogdata.mode == 'PROJECT') {
                    if ($editentry.data('recorddata') != '') {
                        FwFormField.setValue($editentry, '.date',        $editentry.data('recorddata').workdate);
                        FwFormField.setValue($editentry, '.description', $editentry.data('recorddata').description);
                        FwFormField.setValue($editentry, '.hours',       $editentry.data('recorddata').workhours);
                    }
                    if (typeof screen.properties.timelogdata.selectedrecord.orderno != 'undefined') {
                        FwFormField.setValue($editentry, '.order', screen.properties.timelogdata.selectedrecord.orderno + " - " + screen.properties.timelogdata.selectedrecord.orderdesc);
                    } else {
                        FwFormField.setValue($editentry, '.order', 'ADMINISTRATIVE')
                    }
                }

                $back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', true, function() { //back
                    $viewentries.show();
                    $editentry.hide();
                    $editentry.find('.card-body').empty();
                    $editentry.data('recorddata', '');
                    if (typeof $viewentries.$addnew != 'undefined') $viewentries.$addnew.show();
                    $submit.remove();
                    $back.remove();
                });

                $submit = FwMobileMasterController.addFormControl(screen, 'Submit', 'right', '&#xE161;', true, function() { //save
                    var request;
                    if ($editentry.data('validatefields')()) {
                        if ((screen.properties.timelogdata.mode == 'ORDER') || (screen.properties.timelogdata.mode == 'DEAL') || (screen.properties.timelogdata.mode == 'EVENT')) {
                            request = {
                                mode:            screen.properties.timelogdata.mode,
                                date:            $editentry.data('recorddata').thedate,
                                orderid:         $editentry.data('recorddata').orderid,
                                masteritemid:    $editentry.data('recorddata').masteritemid,
                                starttime:       FwFormField.getValue($editentry, '.starttime'),
                                stoptime:        FwFormField.getValue($editentry, '.stoptime'),
                                break1starttime: ($editentry.find('.break1start').length > 0) ? FwFormField.getValue($editentry, '.break1start') : '',
                                break1stoptime:  ($editentry.find('.break1stop').length  > 0) ? FwFormField.getValue($editentry, '.break1stop')  : '',
                                break2starttime: ($editentry.find('.break2start').length > 0) ? FwFormField.getValue($editentry, '.break2start') : '',
                                break2stoptime:  ($editentry.find('.break2stop').length  > 0) ? FwFormField.getValue($editentry, '.break2stop')  : '',
                                break3starttime: ($editentry.find('.break3start').length > 0) ? FwFormField.getValue($editentry, '.break3start') : '',
                                break3stoptime:  ($editentry.find('.break3stop').length  > 0) ? FwFormField.getValue($editentry, '.break3stop')  : '',
                                notes:           $editentry.data('note')
                            };
                        } else if (screen.properties.timelogdata.mode == 'PROJECT') {
                            request = {
                                mode:            screen.properties.timelogdata.mode,
                                update:          $editentry.data('recorddata') != '',
                                recorddata:      $editentry.data('recorddata'),
                                timelogdata:     screen.properties.timelogdata,
                                date:            FwFormField.getValue($editentry, '.date'),
                                description:     FwFormField.getValue($editentry, '.description'),
                                hours:           FwFormField.getValue($editentry, '.hours')
                            };
                        }
                        RwServices.utility.timelogsubmit(request, function(response) {
                            if (response.status.status == 0) {
                                $viewentries.data('show')();
                                $editentry.hide();
                                $editentry.find('.card-body').empty();
                                $editentry.data('recorddata', '');
                                if (typeof $viewentries.$addnew != 'undefined') $viewentries.$addnew.show();
                                $submit.remove();
                                $back.remove();
                            } else {
                                FwFunc.showError(response.msg);
                            }
                       });
                    }
                })

                $editentry.show();
            },
            validatefields: function() {
                var $fields, isvalid = true;
                $fields = $editentry.find('.fwformfield');
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
            }
        })
    ;

    $viewentries
        .on('click', '.entry', function() {
            $viewentries.hide();
            $editentry.data('recorddata', jQuery(this).data('recorddata'));

            $editentry.data('show')();
            if (typeof $viewentries.$addnew != 'undefined') $viewentries.$addnew.hide();
        })
        .data({
            show: function() {
                $viewentries.show();
                $viewentries.data('refreshdata')();

                if ((screen.properties.timelogdata.mode == 'PROJECT') && (typeof $viewentries.$addnew == 'undefined')) {
                    $viewentries.$addnew = FwMobileMasterController.modulecontrols.triggerHandler('addnew');
                    $viewentries.$addnew.on('click', function() {
                        $viewentries.hide();
                        $editentry.data('recorddata', '');
                        $editentry.data('show')();
                        jQuery(this).hide();
                    });
                }
            },
            refreshdata: function() {
                var request, entryhtml, $entry, headerhtml, $header;
                $viewentries.find('.entries').empty();
                request = {
                    mode:           screen.properties.timelogdata.mode,
                    selectedrecord: screen.properties.timelogdata.selectedrecord
                };
                RwServices.utility.timelogviewentries(request, function(response) {
                    switch (screen.properties.timelogdata.mode) {
                        case 'EVENT':
                            headerhtml = [];
                            headerhtml.push('<div class="header"></div>');
                            entryhtml = [];
                            entryhtml.push('<div class="entry">');
                                entryhtml.push('<div class="legend"></div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Deal:</div><div class="value deal"></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Order:</div><div class="value order"></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Position:</div><div class="value position"></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Total Hours:</div><div class="value hours"></div>');
                                entryhtml.push('</div>');
                            entryhtml.push('</div>');

                            for (var i = 0; i < response.entries.length; i++) {
                                if (response.entries[i].rowtype == 'HEADER') {
                                    $header = jQuery(headerhtml.join(''));
                                    $header.html(FwFunc.fixCaps(response.entries[i].description));
                                    $viewentries.find('.entries').append($header);
                                } else if (response.entries[i].rowtype == 'BLANK') {
                                    $entry = jQuery(entryhtml.join(''));
                                    $entry.find('.position').html(response.entries[i].description);
                                    $entry.find('.hours').html(parseInt(response.entries[i].hours) + parseInt(response.entries[i].hoursot) + parseInt(response.entries[i].hoursdt));
                                    $entry.find('.deal').html(response.entries[i].deal);
                                    $entry.find('.order').html(response.entries[i].orderdesc);
                                    $entry.find('.legend').addClass('blank');
                                    $entry.data('recorddata', response.entries[i]);

                                    $viewentries.find('.entries').append($entry);
                                } else if (response.entries[i].rowtype == 'TIME') {
                                    $entry = jQuery(entryhtml.join(''));
                                    $entry.find('.position').html(response.entries[i].description);
                                    $entry.find('.hours').html(parseInt(response.entries[i].hours) + parseInt(response.entries[i].hoursot) + parseInt(response.entries[i].hoursdt));
                                    $entry.find('.deal').html(response.entries[i].deal);
                                    $entry.find('.order').html(response.entries[i].orderdesc);
                                    $entry.find('.legend').addClass('time');
                                    $entry.data('recorddata', response.entries[i]);
                                    $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Start Time:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">End Time:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].stoptime) + '</div></div></div>');
                                    if ((response.entries[i].break1starttime != '') && (response.entries[i].break1stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 1 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break1starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 1 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break1stoptime) + '</div></div></div>');
                                    }
                                    if ((response.entries[i].break2starttime != '') && (response.entries[i].break2stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 2 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break2starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 2 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break2stoptime) + '</div></div></div>');
                                    }
                                    if ((response.entries[i].break3starttime != '') && (response.entries[i].break3stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 3 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break3starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 3 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break3stoptime) + '</div></div></div>');
                                    }
                                    if (response.entries[i].notes != '') {
                                        $entry.append('<div class="row"><div class="caption">Total Hours:</div><div class="value hours">' + response.entries[i].notes + '</div></div>');
                                    }

                                    $viewentries.find('.entries').append($entry);
                                }
                            }
                            break;
                        case 'DEAL':
                            headerhtml = [];
                            headerhtml.push('<div class="header"></div>');
                            entryhtml = [];
                            entryhtml.push('<div class="entry">');
                                entryhtml.push('<div class="legend"></div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Order:</div><div class="value order"></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Position:</div><div class="value position"></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Total Hours:</div><div class="value hours"></div>');
                                entryhtml.push('</div>');
                            entryhtml.push('</div>');

                            for (var i = 0; i < response.entries.length; i++) {
                                if (response.entries[i].rowtype == 'HEADER') {
                                    $header = jQuery(headerhtml.join(''));
                                    $header.html(FwFunc.fixCaps(response.entries[i].description));
                                    $viewentries.find('.entries').append($header);
                                } else if (response.entries[i].rowtype == 'BLANK') {
                                    $entry = jQuery(entryhtml.join(''));
                                    $entry.find('.position').html(response.entries[i].description);
                                    $entry.find('.hours').html(parseInt(response.entries[i].hours) + parseInt(response.entries[i].hoursot) + parseInt(response.entries[i].hoursdt));
                                    $entry.find('.order').html(response.entries[i].orderdesc);
                                    $entry.find('.legend').addClass('blank');
                                    $entry.data('recorddata', response.entries[i]);

                                    $viewentries.find('.entries').append($entry);
                                } else if (response.entries[i].rowtype == 'TIME') {
                                    $entry = jQuery(entryhtml.join(''));
                                    $entry.find('.position').html(response.entries[i].description);
                                    $entry.find('.hours').html(parseInt(response.entries[i].hours) + parseInt(response.entries[i].hoursot) + parseInt(response.entries[i].hoursdt));
                                    $entry.find('.order').html(response.entries[i].orderdesc);
                                    $entry.find('.legend').addClass('time');
                                    $entry.data('recorddata', response.entries[i]);
                                    $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Start Time:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">End Time:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].stoptime) + '</div></div></div>');
                                    if ((response.entries[i].break1starttime != '') && (response.entries[i].break1stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 1 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break1starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 1 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break1stoptime) + '</div></div></div>');
                                    }
                                    if ((response.entries[i].break2starttime != '') && (response.entries[i].break2stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 2 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break2starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 2 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break2stoptime) + '</div></div></div>');
                                    }
                                    if ((response.entries[i].break3starttime != '') && (response.entries[i].break3stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 3 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break3starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 3 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break3stoptime) + '</div></div></div>');
                                    }
                                    if (response.entries[i].notes != '') {
                                        $entry.append('<div class="row"><div class="caption">Total Hours:</div><div class="value hours">' + response.entries[i].notes + '</div></div>');
                                    }

                                    $viewentries.find('.entries').append($entry);
                                }
                            }
                            break;
                        case 'ORDER':
                            headerhtml = [];
                            headerhtml.push('<div class="header"></div>');
                            entryhtml = [];
                            entryhtml.push('<div class="entry">');
                                entryhtml.push('<div class="legend"></div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Position:</div><div class="value position"></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Total Hours:</div><div class="value hours"></div>');
                                entryhtml.push('</div>');
                            entryhtml.push('</div>');

                            for (var i = 0; i < response.entries.length; i++) {
                                if (response.entries[i].rowtype == 'HEADER') {
                                    $header = jQuery(headerhtml.join(''));
                                    $header.html(FwFunc.fixCaps(response.entries[i].description));
                                    $viewentries.find('.entries').append($header);
                                } else if (response.entries[i].rowtype == 'BLANK') {
                                    $entry = jQuery(entryhtml.join(''));
                                    $entry.find('.position').html(response.entries[i].description);
                                    $entry.find('.hours').html(parseInt(response.entries[i].hours) + parseInt(response.entries[i].hoursot) + parseInt(response.entries[i].hoursdt));
                                    $entry.find('.legend').addClass('blank');
                                    $entry.data('recorddata', response.entries[i]);

                                    $viewentries.find('.entries').append($entry);
                                } else if (response.entries[i].rowtype == 'TIME') {
                                    $entry = jQuery(entryhtml.join(''));
                                    $entry.find('.position').html(response.entries[i].description);
                                    $entry.find('.hours').html(parseInt(response.entries[i].hours) + parseInt(response.entries[i].hoursot) + parseInt(response.entries[i].hoursdt));
                                    $entry.find('.legend').addClass('time');
                                    $entry.data('recorddata', response.entries[i]);
                                    $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Start Time:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">End Time:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].stoptime) + '</div></div></div>');
                                    if ((response.entries[i].break1starttime != '') && (response.entries[i].break1stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 1 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break1starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 1 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break1stoptime) + '</div></div></div>');
                                    }
                                    if ((response.entries[i].break2starttime != '') && (response.entries[i].break2stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 2 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break2starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 2 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break2stoptime) + '</div></div></div>');
                                    }
                                    if ((response.entries[i].break3starttime != '') && (response.entries[i].break3stoptime != '')) {
                                        $entry.append('<div class="row"><div style="float:left;width:50%;"><div class="caption">Break 3 Start:</div><div class="value starttime">' + FwFunc.convert24to12(response.entries[i].break3starttime) + '</div></div><div style="float:left;width:50%;"><div class="caption">Break 3 Stop:</div><div class="value endtime">' + FwFunc.convert24to12(response.entries[i].break3stoptime) + '</div></div></div>');
                                    }
                                    if (response.entries[i].notes != '') {
                                        $entry.append('<div class="row"><div class="caption">Notes:</div><div class="value notes">' + response.entries[i].notes + '</div></div>');
                                    }

                                    $viewentries.find('.entries').append($entry);
                                }
                            }
                            break;
                        case 'PROJECT':
                            //if ($viewentries.find('.btnpanel').length == 0) {
                            //    $viewentries.prepend('<div class="center btnpanel"><div class="button-strip"><div class="button default btnnewentry">New Entry</div></div></div>');
                            //    $viewentries.on('click', '.btnnewentry', function() {
                            //        $viewentries.hide();
                            //        $editentry.data('recorddata', '');
                            //        $editentry.data('show')();
                            //    });
                            //}
                            headerhtml = [];
                            headerhtml.push('<div class="header">Entries</div>');
                            entryhtml = [];
                            entryhtml.push('<div class="entry">');
                                entryhtml.push('<div class="legend"></div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Order No:</div><div class="value orderno"></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption">Order:</div><div class="value orderdesc"></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div style="float:left;width:60%;"><div class="caption">Date:</div><div class="value date"></div></div><div style="float:left;width:40%;"><div class="caption">Hours:</div><div class="value hours"></div></div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="caption" style="width:120px;">Description of Work:</div>');
                                entryhtml.push('</div>');
                                entryhtml.push('<div class="row">');
                                    entryhtml.push('<div class="value description"></div>');
                                entryhtml.push('</div>');
                            entryhtml.push('</div>');

                            $viewentries.find('.entries').append(headerhtml.join(''));
                            for (var i = 0; i < response.entries.length; i++) {
                                $entry = jQuery(entryhtml.join(''));
                                $entry.find('.orderno').html(response.entries[i].orderno);
                                $entry.find('.orderdesc').html(response.entries[i].orderdesc);
                                $entry.find('.date').html(FwFunc.getDate(response.entries[i].workdate));
                                $entry.find('.hours').html(response.entries[i].workhours);
                                $entry.find('.description').html(response.entries[i].description);
                                $entry.data('recorddata', response.entries[i]);

                                $viewentries.find('.entries').append($entry);
                            }
                            if (response.entries.length == 0) {
                                $viewentries.find('.entries').append('<div class="center" style="margin-top:5px;">0 entries found.</div>');
                            }
                            break;
                    }
                });
            }
        })
    ;

    screen.load = function() {

    };

    screen.unload = function () {

    };
    
    return screen;
};
//----------------------------------------------------------------------------------------------