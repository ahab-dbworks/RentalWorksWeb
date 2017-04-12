/* Copyright 2005 - 2014 Annpoint, s.r.o.
   Use of this software is subject to license terms.
   http://www.daypilot.org/
*/
   
if (typeof DayPilot === 'undefined') {
    var DayPilot = {};
}

if (typeof DayPilot.Global === 'undefined') {
    DayPilot.Global = {};
}

// compatibility with 5.9.2029 and previous
if (typeof DayPilotScheduler === 'undefined') {
    var DayPilotScheduler = DayPilot.SchedulerVisible = {};
}

(function() {
    

    if (typeof DayPilot.Scheduler !== 'undefined') {
        return;
    }
    
    // temp hack
    var android = (function() {
        var nua = navigator.userAgent;
        return ((nua.indexOf('Mozilla/5.0') > -1 && nua.indexOf('Android ') > -1 && nua.indexOf('AppleWebKit') > -1) && !(nua.indexOf('Chrome') > -1));
    })();

    var DayPilotScheduler = {};

    var doNothing = function() { };

    DayPilot.Scheduler = function(id, options) {
        this.v = '1338';

        var calendar = this;
        this.id = id; // referenced
        this.isScheduler = true;

        this.hideUntilInit = true;
        this.api = 2;

        // default values
        this.allowDefaultContextMenu = false;
        this.allowEventOverlap = true;
        this.allowMultiSelect = true;
        //this.allowDuplicateResources = false;
        this.autoRefreshCommand = 'refresh';
        this.autoRefreshEnabled = false;
        this.autoRefreshInterval = 60;
        this.autoRefreshMaxCount = 20;
        this.autoScroll = "Drag";
        this.blockOnCallBack = false;
        this.borderColor = "#000000";
        this.businessBeginsHour = 9;
        this.businessEndsHour = 18;
        this.cellBackColor = "#FFFFD5";
        this.cellBackColorNonBusiness = "#FFF4BC";
        this.cellBorderColor = "#EAD098";
        this.cellDuration = 60;
        this.cellGroupBy = 'Day';
        this.cellSelectColor = "#316AC5";
        this.cellWidth = 40;
        this.cellWidthSpec = "Fixed";
        //this.clientSide = true;
        this.groupConcurrentEvents = false;
        this.groupConcurrentEventsLimit = 1;
        this._cellPropertiesLazyLoading = true; // server-based only
        this.cellSweeping = true;
        this.cellSweepingCacheSize = 1000;
        this.crosshairColor = 'Gray';
        this.crosshairOpacity = 20;
        this.crosshairType = 'Header';
        //this.debuggingEnabled = false;
        this.doubleClickTimeout = 300;
        this.dragOutAllowed = false;
        this.durationBarColor = 'blue';
        this.durationBarHeight = 3;
        this.durationBarVisible = true;
        this.durationBarMode = "Duration";
        this._durationBarDetached = false;
        this.days = 1;
        this.drawBlankCells = true;
        this.dynamicEventRendering = 'Progressive';
        this.dynamicEventRenderingMargin = 500;
        this.dynamicEventRenderingCacheSweeping = false;
        this.dynamicEventRenderingCacheSize = 200;
        this.dynamicLoading = false;
        this.eventBorderColor = "#000000";
        this.eventBorderVisible = true;
        this.eventBackColor = "#FFFFFF";
        this.eventEndSpec = "DateTime";
        this.eventFontFamily = "Tahoma, Arial";
        this.eventFontSize = '8pt';
        this.eventFontColor = '#000000';
        this.eventHeight = 25;
        this.eventMoveMargin = 5;
        this.eventMoveToPosition = false;
        this.eventResizeMargin = 5;
        this.eventStackingLineHeight = 100;
        this._ganttAppendToResources = false;
        this.headerFontColor = "#000000";
        this.headerFontFamily = "Tahoma, Arial";
        this.headerFontSize = '8pt';
        this.headerHeight = 20;
        this.heightSpec = 'Auto';
        this.hourFontFamily = "Tahoma, Arial";
        this.hourFontSize = '10pt';
        this.hourNameBackColor = "#ECE9D8";
        this.hourNameBorderColor = "#ACA899";
        this.layout = 'Auto';
        this.linkCreateHandling = "Disabled";
        this.linkBottomMargin = 10;
        this.locale = "en-us";
        this.loadingLabelText = "Loading...";
        this.loadingLabelVisible = true;
        this.loadingLabelBackColor = "orange";
        this.loadingLabelFontColor = "#ffffff";
        this.loadingLabelFontFamily = "Tahoma";
        this.loadingLabelFontSize = "10pt";
        this.messageHideAfter = 5000;
        this.moveBy = 'Full';
        this.notifyCommit = 'Immediate'; // or 'Queue'
        this.numberFormat = null;
        this.progressiveRowRendering = true;
        this.progressiveRowRenderingPreload = 25;
        this.timeHeaders = [ {"groupBy": "Default"}, {"groupBy": "Cell"} ];
        this.treePreventParentUsage = false;
        this.treeAutoExpand = true;
        this.rowHeaderHideIconEnabled = false;
        this.rowHeaderWidth = 80;
        this.rowHeaderScrolling = false;
        this.rowHeaderSplitterWidth = 3;
        this.rowHeaderWidthAutoFit = true;
        this.rowHeaderCols = null;
        this.rowMarginBottom = 0;
        this.rowMarginTop = 0;
        this.rowMinHeight = 0;
        this.scale = "CellDuration";
        this.scrollDelayDynamic = 500;
        this.scrollDelayEvents = 200;
        this.scrollDelayCells = 20;
        this.scrollDelayFloats = 0;
        this.scrollX = 0;
        this.scrollY = 0;
        this.shadow = "Fill";
        this.showBaseTimeHeader = true;  // obsolete
        this.showNonBusiness = true;
        this.showToolTip = true;
        this.snapToGrid = true;
        this.startDate = new DayPilot.Date().getDatePart();
        this.syncResourceTree = false;
        this.timeBreakColor = '#000000';
        this.treeEnabled = false;
        this.treeIndent = 20;
        this.treeImageMarginLeft = 5;
        this.treeImageMarginTop = 5;
        this.timeFormat = "Auto";
        this.useEventBoxes = 'Always';
        this.viewType = 'Resources';
        this.weekStarts = 'Auto'; // 0 = Sunday, 1 = Monday, ... 'Auto' = use .locale
        this.width = null;
        this.floatingEvents = true;
        this.floatingTimeHeaders = true;

        this.eventCorners = 'Regular'; ;

        this.separators = [];
        //this.afterRender = function() { };
        this.cornerHtml = '';

        this._crosshairLastY = -1;
        this._crosshairLastX = -1;

        this.eventClickHandling = 'Enabled';
        this.eventDeleteHandling = "Disabled";
        this.eventHoverHandling = 'Bubble';
        this.eventDoubleClickHandling = 'Disabled';
        this.eventEditHandling = 'Update';
        this.eventMoveHandling = 'Update';
        this.eventResizeHandling = 'Update';
        this.eventRightClickHandling = 'ContextMenu';
        this.eventSelectHandling = 'Update';
        this.rowClickHandling = 'Enabled';
        this.rowDoubleClickHandling = "Disabled";
        this.rowCreateHandling = "Disabled";
        this.rowEditHandling = 'Update';
        this.rowSelectHandling = 'Update';
        this.rowMoveHandling = "Disabled";
        this.timeRangeDoubleClickHandling = 'Disabled';
        this.timeRangeSelectedHandling = 'Enabled';
        
        this.eventMovingStartEndEnabled = false;
        this.eventMovingStartEndFormat = "MMMM d, yyyy";
        this.timeRangeSelectingStartEndEnabled = false;
        this.timeRangeSelectingStartEndFormat = "MMMM d, yyyy";
        this.eventResizingStartEndEnabled = false;
        this.eventResizingStartEndFormat = "MMMM d, yyyy";

        this.cssOnly = true;
        this.cssClassPrefix = "scheduler_default";

        // if null, ASP.NET callback will be used
        this.backendUrl = null;

        if (calendar.api === 1) {
            this.onEventMove = function() { };
            this.onEventResize = function() { };
            this.onResourceExpand = function() { };
            this.onResourceCollapse = function() { };
        }

        this.onRowHeaderResized = null;

        //this._debugMessages = [];

        this.autoRefreshCount = 0;
        this._innerHeightTree = 0;

        // internal, name kept non-minified for debugging purposes
        this.rowlist = [];
        this.itline = [];

        this.timeline = null;

        //this.status = {};
        this.events = {};
        this.cells = {};

        // store the element references
        this.elements = {};
        this.elements.events = [];
        this.elements.bars = [];
        this.elements.text = [];
        this.elements.cells = [];
        this.elements.linesVertical = [];
        //this.elements.linesHorizontal = [];
        this.elements.separators = [];
        this.elements.range = [];
        this.elements.breaks = [];
        this.elements.links = [];
        this.elements.linkpoints = [];

        this._cache = {};
        this._cache.cells = [];
        this._cache.linesVertical = [];
        this._cache.linesHorizontal = [];
        this._cache.timeHeaderGroups = [];
        this._cache.pixels = [];
        this._cache.breaks = [];
        this._cache.events = []; // processed using client-side beforeEventRender
        
        this.clientState = {};

        this.q = new DayPilot.Queue();

        this.members = {};
        this.members.obsolete = [
            "Init",
            "cleanSelection",
            "cssClassPrefix",
            "getHScrollPosition",
            "setHScrollPosition",
            "getVScrollPosition",
            "setVScrollPosition",
            "showBaseTimeHeader"
        ];
        this.members.ignoreFilter = function(name) {
            if (name.indexOf("div") === 0) {
                return true;
            }
            return false;
        };
        this.members.ignore = [
            "internal",
            "nav",
            "debug",
            "temp",
            "elements",
            "members",
            "cellProperties",
            "itline",
            "rowlist",
            "timeHeader"
        ];
        this.members.noCssOnly = [
            "borderColor",
            "cellBackColor",
            "cellBackColorNonBusiness",
            "cellBorderColor",
            "cellSelectColor",
            "durationBarColor",
            "eventBackColor",
            "eventBorderColor",
            "eventBorderVisible",
            "eventCorners",
            "eventFontColor",
            "eventFontFamily",
            "eventFontSize",
            "headerFontColor",
            "headerFontFamily",
            "headerFontSize",
            "hourFontFamily",
            "hourFontSize",
            "hourNameBackColor",
            "hourNameBorderColor",
            "loadingLabelBackColor",
            "loadingLabelFontColor",
            "loadingLabelFontFamily",
            "loadingLabelFontSize",
            "shadow",
            "timeBreakColor"
        ];

        this.nav = {};

        this._updateView = function(result, context) {
            var result = eval("(" + result + ")");

            if (typeof calendar.onCallBackResult === "function") { // internal API
                var args = {};
                args.result = result;
                args.preventDefault = function() {
                    args.preventDefault.value = true;
                }
                calendar.onCallBackResult(args);

                if (args.preventDefault.value) {

                    //calendar._updateFloats();
                    calendar._deleteDragSource();
                    calendar._loadingStop();
                    calendar._startAutoRefresh();

                    if (result.Message) {
                        if (calendar.message) { // condition can be removed as soon as message() is implemented properly
                            calendar.message(result.Message);
                        }
                    }

                    calendar._fireAfterRenderDetached(result.CallBackData, true);
                    calendar._doCallBackEnd();
                    calendar._clearCachedValues();

                    return;
                }
            }

            if (result.BubbleGuid) {
                var guid = result.BubbleGuid;
                var bubble = this.bubbles[guid];
                delete this.bubbles[guid];

                calendar._loadingStop();
                if (typeof result.Result.BubbleHTML !== 'undefined') {
                    bubble.updateView(result.Result.BubbleHTML, bubble);
                }
                calendar._doCallBackEnd();
                return;
            }
            
            if (result.CallBackRedirect) {
                document.location.href = result.CallBackRedirect;
                return;
            }


            if (typeof DayPilot.Bubble !== "undefined") {
                DayPilot.Bubble.hideActive();
            }

            if (typeof result.ClientState !== 'undefined') {
                if (result.ClientState === null) {
                    calendar.clientState = {};
                }
                else {
                    calendar.clientState = result.ClientState;
                }
            }

            if (result.UpdateType === "None") {
                calendar._loadingStop();
                calendar._doCallBackEnd();

                //calendar.afterRender(result.CallBackData, true);

                if (result.Message) {
                    calendar.message(result.Message);
                }

                calendar._fireAfterRenderDetached(result.CallBackData, true);

                return;
            }

            // update config
            if (result.VsUpdate) {
                var vsph = document.createElement("input");
                vsph.type = 'hidden';
                vsph.name = calendar.id + "_vsupdate";
                vsph.id = vsph.name;
                vsph.value = result.VsUpdate;
                calendar._vsph.innerHTML = '';
                calendar._vsph.appendChild(vsph);
            }

            if (typeof result.TagFields !== 'undefined') {
                calendar.tagFields = result.TagFields;
            }

            if (typeof result.SortDirections !== 'undefined') {
                calendar.sortDirections = result.SortDirections;
            }
            
            calendar._cache.drawArea = null;

            if (result.UpdateType === "Full") {
                // generated
                calendar.resources = result.Resources;
                calendar.colors = result.Colors;
                calendar.palette = result.Palette;
                calendar.dirtyColors = result.DirtyColors;
                calendar.cellProperties = result.CellProperties;
                calendar.cellConfig = result.CellConfig;
                calendar.separators = result.Separators;
                calendar.timeline = result.Timeline;
                calendar.timeHeader = result.TimeHeader;
                calendar.timeHeaders = result.TimeHeaders;
                if (typeof result.RowHeaderColumns !== 'undefined') calendar.rowHeaderColumns = result.RowHeaderColumns;

                // properties
                calendar.startDate = result.StartDate ? new DayPilot.Date(result.StartDate) : calendar.startDate;
                calendar.days = result.Days ? result.Days : calendar.days;
                calendar.cellDuration = result.CellDuration ? result.CellDuration : calendar.cellDuration;
                calendar.cellGroupBy = result.CellGroupBy ? result.CellGroupBy : calendar.cellGroupBy;
                calendar.cellWidth = result.CellWidth ? result.CellWidth : calendar.cellWidth;
                // scrollX
                // scrollY
                calendar.viewType = result.ViewType ? result.ViewType : calendar.viewType;
                calendar.hourNameBackColor = result.HourNameBackColor ? result.HourNameBackColor : calendar.hourNameBackColor;
                calendar.showNonBusiness = result.ShowNonBusiness ? result.ShowNonBusiness : calendar.showNonBusiness;
                calendar.businessBeginsHour = result.BusinessBeginsHour ? result.BusinessBeginsHour : calendar.businessBeginsHour;
                calendar.businessEndsHour = result.BusinessEndsHour ? result.BusinessEndsHour : calendar.businessEndsHour;
                if (typeof result.DynamicLoading !== 'undefined') calendar.dynamicLoading = result.DynamicLoading;
                if (typeof result.TreeEnabled !== 'undefined') calendar.treeEnabled = result.TreeEnabled;
                calendar.backColor = result.BackColor ? result.BackColor : calendar.backColor;
                calendar.nonBusinessBackColor = result.NonBusinessBackColor ? result.NonBusinessBackColor : calendar.nonBusinessBackColor;
                calendar.locale = result.Locale ? result.Locale : calendar.locale;
                if (typeof result.TimeZone !== 'undefined') calendar.timeZone = result.TimeZone;
                calendar.timeFormat = result.TimeFormat ? result.TimeFormat : calendar.timeFormat;
                calendar.rowHeaderCols = result.RowHeaderCols ? result.RowHeaderCols : calendar.rowHeaderCols;
                if (typeof result.DurationBarMode !== "undefined") calendar.durationBarMode = result.DurationBarMode;
                //if (typeof result.ShowBaseTimeHeader !== "undefined") calendar.showBaseTimeHeader = result.ShowBaseTimeHeader;

                calendar.cornerBackColor = result.CornerBackColor ? result.CornerBackColor : calendar.cornerBackColor;
                if (typeof result.CornerHTML !== 'undefined') { calendar.cornerHtml = result.CornerHTML; }

                calendar.hashes = result.Hashes;

                calendar._calculateCellWidth();
                calendar._prepareItline();

                calendar._loadResources();
                calendar._expandCellProperties();
            }
            if (result.Action !== "Scroll") {
                calendar._loadEvents(result.Events);
            }

            if (result.UpdateType === 'Full') {
                calendar._drawResHeader();
                calendar._drawTimeHeader();
            }

            calendar._prepareRowTops();
            calendar._show();

            calendar._cache.drawArea = null;

            if (result.Action !== "Scroll") {
                calendar._updateRowHeaderHeights();
                calendar._updateHeaderHeight();

                if (calendar.heightSpec === 'Auto' || calendar.heightSpec === 'Max') {
                    calendar._updateHeight();
                }

                calendar._deleteCells();
                calendar._deleteEvents();
                calendar._deleteSeparators();

                calendar.multiselect.clear(true);
                calendar.multiselect.initList = result.SelectedEvents;

                calendar._drawCells();
                calendar._drawSeparators();
                calendar._drawEvents();
            }
            else {
                calendar.multiselect.clear(true);
                calendar.multiselect.initList = result.SelectedEvents;

                //calendar._updateRowsNoLoad(updatedRows, true);

                // draw events not called here because it's now called in loadEventsDynamic
                //calendar._drawCells();

                calendar._loadEventsDynamic(result.Events);
            }

            if (calendar.timeRangeSelectedHandling !== "HoldForever") {
                calendar._deleteRange();
            }

            if (result.UpdateType === "Full") {
                calendar.setScroll(result.ScrollX, result.ScrollY);
                calendar._saveState();
            }
            
            calendar._updateFloats();
            
            calendar._deleteDragSource();

            calendar._loadingStop();
            
            if (result.UpdateType === 'Full' && navigator.appVersion.indexOf("MSIE 7.") !== -1) { // ugly bug, ugly fix - the time header disappears after expanding a dynamically loaded tree node
                window.setTimeout(function() {
                    calendar._drawResHeader();
                    calendar._updateHeight();
                }, 0);
            }

            calendar._startAutoRefresh();

            if (result.Message) {
                if (calendar.message) { // condition can be removed as soon as message() is implemented properly
                    calendar.message(result.Message);
                }
            }

            calendar._fireAfterRenderDetached(result.CallBackData, true);

            calendar._doCallBackEnd();

            calendar._clearCachedValues();
        };
        
        this._deleteDragSource = function() {
            if (calendar.todo) {
                if (calendar.todo.del) {
                    var del = calendar.todo.del;
                    del.parentNode.removeChild(del);
                    calendar.todo.del = null;
                }
            }
        };

        this._fireAfterRenderDetached = function(data, isCallBack) {
            var afterRenderDelayed = function(data, isc) {
                return function() {
                    if (calendar._api2()) {
                        if (typeof calendar.onAfterRender === 'function') {
                            var args = {};
                            args.isCallBack = isc;
                            args.data = data;
                            
                            calendar.onAfterRender(args);
                        }
                    }
                    else {
                        if (calendar.afterRender) {
                            calendar.afterRender(data, isc);
                        }
                    }
                };
            };

            window.setTimeout(afterRenderDelayed(data, isCallBack), 0);
        };

        this.scrollTo = function(date) {
            var pixels = this.getPixels(new DayPilot.Date(date)).left;
            this.setScrollX(pixels);
        };

        this.scrollToResource = function(id) {
            DayPilot.complete(function() {
                var row = calendar._findRowByResourceId(id);
                if (!row) {
                    return;
                }
                setTimeout(function() {
                    var scrollY = row.top;
                    calendar.nav.scroll.scrollTop = scrollY;
                }, 100);

            });
        };
        
        this._findHeadersInViewPort = function() {
            
            if (!this.cssOnly) {
                return;
            }
            
            if (!this.floatingTimeHeaders) {
                return;
            }
            
            if (!this.timeHeader) {
                return;
            }
            

            var area = this._getDrawArea();
            if (!area) {
                return;
            }

            this._deleteHeaderSections();

            var start = area.pixels.left;
            var end = area.pixels.right;
            
            var cells = [];
            
            for (var y = 0; y < this.timeHeader.length; y++) {
                for (var x = 0; x < this.timeHeader[y].length; x++) {
                    var h = this.timeHeader[y][x];
                    var left = h.left;
                    var right = h.left + h.width;
                    
                    var cell = null;
                    
                    if (left < start && start < right) {
                        cell = {};
                        cell.x = x;
                        cell.y = y;
                        cell.marginLeft = start - left;
                        cell.marginRight = 0;
                        cell.div = this._cache.timeHeader[x + "_" + y];
                        cells.push(cell);
                    }
                    if (left < end && end < right) {
                        
                        if (!cell) {
                            cell = {};
                            cell.x = x;
                            cell.y = y;
                            cell.marginLeft = 0;
                            cell.div = this._cache.timeHeader[x + "_" + y];
                            cells.push(cell);
                        }
                        cell.marginRight = right - end;
    
                        break; // end of this line
                    }
                }
            }
            
            for (var i = 0; i < cells.length; i++) {
                var cell = cells[i];
                this._createHeaderSection(cell.div, cell.marginLeft, cell.marginRight);
            }
            
        };
        
        this._updateFloats = function() {
            this._findHeadersInViewPort();
            this._findEventsInViewPort();
        };

        this._viewport = {};

        var viewport = this._viewport;

        viewport.events = function(type) {
            var list = [];
            var type = type || "All";

            var area = calendar._getDrawArea();
            if (!area) {
                return DayPilot.list(list);
            }

            var start = area.pixels.left;
            var end = area.pixels.right;


            for(var i = 0; i < calendar.elements.events.length; i++) {
                var e = calendar.elements.events[i];
                var data = e.event;
                var left = data.part.left;
                var right = data.part.left + data.part.width;

                switch (type) {
                    case "Left":
                        if (left < start && start < right) {
                            list.push(e);
                        }
                        break;
                    case "All":
                        if (DayPilot.Util.overlaps(left, right, start, end)) {
                            list.push(e);
                        }
                        break;
                }
            }
            return DayPilot.list(list).addProps({ "area": area});
        };
        
        this._findEventsInViewPort = function() {

            if (!this.cssOnly) {
                return;
            }

            if (!this.floatingEvents) {
                return;
            }

            var events = viewport.events("Left");

            this._deleteEventSections();

            events.each(function(item) {
                var left = events.area.pixels.left;
                var data = item.event;
                var start = data.part.left;
                var marginLeft = left - start;
                calendar._createEventSection(item, marginLeft, 0);
            });

        };
        
        this.elements.sections = [];
        this.elements.hsections = [];
        
        this._createHeaderSection = function(div, marginLeft, marginRight) {
            var section = document.createElement("div");
            section.setAttribute("unselectable", "on");
            section.className = this._prefixCssClass("_timeheader_float");
            section.style.position = "absolute";
            section.style.left = marginLeft + "px";
            section.style.right = marginRight + "px";
            section.style.top = "0px";
            section.style.bottom = "0px";
            //section.style.backgroundColor = "red";
            //section.style.color = "white";
            section.style.overflow = "hidden";
            
            var inner = document.createElement("div");
            inner.className = this._prefixCssClass("_timeheader_float_inner");
            inner.setAttribute("unselectable", "on");
            inner.innerHTML = div.cell.th.innerHTML;
            section.appendChild(inner);
            
            div.section = section;

            //div.appendChild(section);
            div.insertBefore(section, div.firstChild.nextSibling); // after inner
            div.firstChild.innerHTML = ''; // hide the content of inner temporarily
            
            this.elements.hsections.push(div);
        };
        
        this._deleteHeaderSections = function() {
            for (var i = 0; i < this.elements.hsections.length; i++) {
                var e = this.elements.hsections[i];
                
                // restore HTML in inner
                var data = e.cell;
                if (data && e.firstChild) { // might be deleted already
                    e.firstChild.innerHTML = data.th.innerHTML;  
                }
                
                DayPilot.de(e.section);
                e.section = null;
            }
            this.elements.hsections = [];
        };

        this._createEventSection = function(div, marginLeft, marginRight) {

            var section = document.createElement("div");
            section.setAttribute("unselectable", "on");
            section.className = this._prefixCssClass("_event_float");
            section.style.position = "absolute";
            section.style.left = marginLeft + "px";
            section.style.right = marginRight + "px";
            section.style.top = "0px";
            section.style.bottom = "0px";
            section.style.overflow = "hidden";

            var inner = document.createElement("div");
            inner.className = this._prefixCssClass("_event_float_inner");
            inner.setAttribute("unselectable", "on");
            inner.innerHTML = div.event.client.html();
            section.appendChild(inner);
            
            //section.innerHTML = div.event.text();
            
            div.section = section;
            //div.firstChild.appendChild(section);
            
            div.insertBefore(section, div.firstChild.nextSibling); // after inner
            div.firstChild.innerHTML = ''; // hide the content of inner temporarily
            
            this.elements.sections.push(div);
        };
        
        this._deleteEventSections = function() {
            for (var i = 0; i < this.elements.sections.length; i++) {
                var e = this.elements.sections[i];
                
                // restore HTML in inner
                var data = e.event;
                if (data) { // might be deleted already
                    e.firstChild.innerHTML = data.client.html();  
                }
                
                DayPilot.de(e.section);
                
                e.section = null;
            }
            this.elements.sections = [];
        };
        
        this.setScrollX = function(scrollX) {
            this.setScroll(scrollX, calendar.scrollY);
        };
        
        this.setScrollY = function(scrollY) {
            this.setScroll(calendar.scrollX, scrollY);
        };

        this.setScroll = function(scrollX, scrollY) {
            var scroll = calendar.nav.scroll;
            var maxHeight = calendar._innerHeightTree;
            var maxWidth = calendar._cellCount() * calendar.cellWidth;

            if (scroll.clientWidth + scrollX > maxWidth) {
                scrollX = maxWidth - scroll.clientWidth;
            }

            //var scrollY = result.ScrollY;
            if (scroll.clientHeight + scrollY > maxHeight) {
                scrollY = maxHeight - scroll.clientHeight;
            }

            calendar.divTimeScroll.scrollLeft = scrollX;
            calendar.divResScroll.scrollTop = scrollY;

            scroll.scrollLeft = scrollX;
            scroll.scrollTop = scrollY;
        };

        this.message = function(html, delay, foreColor, backColor) {
            if (html === null) {
                return;
            }

            var delay = delay || this.messageHideAfter || 2000;
            var foreColor = foreColor || "#ffffff";
            var backColor = backColor || "#000000";
            var opacity = 0.8;

            var top = this._getTotalHeaderHeight() + 1;
            var left = this._getOuterRowHeaderWidth() + resolved.splitterWidth();
            var right = DayPilot.sw(calendar.nav.scroll) + 1;
            var bottom = DayPilot.sh(calendar.nav.scroll) + 1;

            var div;
            
            if (!this.nav.message) {
                div = document.createElement("div");
                div.style.position = "absolute";
                //div.style.width = "100%";
                div.style.left = left + "px";
                div.style.right = right + "px";
                //div.style.height = "0px";
                //div.style.paddingLeft = left + "px";
                //div.style.paddingRight = right + "px";
                
                //div.style.opacity = 1;  
                //div.style.filter = "alpha(opacity=100)"; // enable fading in IE8
                div.style.display = 'none';
                
                div.onmousemove = function() {
                    if (div.messageTimeout && !div.status) {
                        clearTimeout(div.messageTimeout);
                    }
                };
                
                div.onmouseout = function() {
                    if (calendar.nav.message.style.display !== 'none') {
                        div.messageTimeout = setTimeout(calendar._hideMessage, 500);
                    }
                };

                var inner = document.createElement("div");
                inner.onclick = function() { calendar.nav.message.style.display = 'none'; };
                if (!this.cssOnly) {
                    inner.style.padding = "5px";
                    inner.style.opacity = opacity;
                    inner.style.filter = "alpha(opacity=" + (opacity * 100) + ")";
                }
                else {
                    inner.className = this._prefixCssClass("_message");
                }
                div.appendChild(inner);

                var close = document.createElement("div");
                close.style.position = "absolute";
                if (!this.cssOnly) {
                    close.style.top = "5px";
                    close.style.right = (DayPilot.sw(calendar.nav.scroll) + 5) + "px";
                    close.style.color = foreColor;
                    close.style.lineHeight = "100%";
                    close.style.cursor = "pointer";
                    close.style.fontWeight = "bold";
                    close.innerHTML = "X";
                }
                else {
                    close.className = this._prefixCssClass("_message_close");
                }
                close.onclick = function() { calendar.nav.message.style.display = 'none'; };
                div.appendChild(close);

                this.nav.top.appendChild(div);
                this.nav.message = div;

            }
            else {
                div = calendar.nav.message;
            }

            var showNow = function() {

                var inner = calendar.nav.message.firstChild;

                if (!calendar.cssOnly) {
                    inner.style.backgroundColor = backColor;
                    inner.style.color = foreColor;
                }
                inner.innerHTML = html;

                // update the right margin (scrollbar width)
                var right = DayPilot.sw(calendar.nav.scroll) + 1;
                calendar.nav.message.style.right = right + "px";
                
                // always update the position
                var position = calendar.messageBarPosition || "Top";
                if (position === "Bottom") {
                    calendar.nav.message.style.bottom = bottom + "px";
                    calendar.nav.message.style.top = "";
                }
                else if (position === "Top") {
                    calendar.nav.message.style.bottom = "";
                    calendar.nav.message.style.top = top + "px";
                }

                var end = function() { div.messageTimeout = setTimeout(calendar._hideMessage, delay); };
                DayPilot.fade(calendar.nav.message, 0.2, end);
            };

            clearTimeout(div.messageTimeout);

            // another message was visible
            if (this.nav.message.style.display !== 'none') {
                DayPilot.fade(calendar.nav.message, -0.2, showNow);
            }
            else {
                showNow();
            }
        };

        this._hideMessage = function() {
            if (!calendar.nav.message) {
                return;
            }

            var end = function() { calendar.nav.message.style.display = 'none'; };
            DayPilot.fade(calendar.nav.message, -0.2, end);
        };

        this.message.show = function(html) {
            calendar.message(html);
        };
        
        this.message.hide = function() {
            calendar._hideMessage();
        };

        // updates the height after a resize
        this._updateHeight = function() {

            if (this.nav.scroll) { // only if the control is not disposed already

                if (this.heightSpec === 'Parent100Pct') {
                    // similar to setHeight()
                    this.height = parseInt(this.nav.top.clientHeight, 10) - (this._getTotalHeaderHeight() + 2);
                }

                // getting ready for the scrollbar
                // keep it here, firefox requires it to get the scrollbar height
                this.nav.scroll.style.height = '30px';

                var height = this._getScrollableHeight();
                //height = Math.max(1, height); // make sure it's not negative

                var dividerHeight = 1;
                var total = height + this._getTotalHeaderHeight() + dividerHeight;
                if (height >= 0) {
                    this.nav.scroll.style.height = (height) + 'px';
                    this._scrollRes.style.height = (height) + 'px';
                }
                if (this.nav.divider) {
                    if (!total || isNaN(total) || total < 0) {
                        total = 0;
                    }
                    this.nav.divider.style.height = (total) + "px";
                }



                // required for table-based mode        
                if (this.heightSpec !== "Parent100Pct") {
                    this.nav.top.style.height = (total) + "px";
                }

                for (var i = 0; i < this.elements.separators.length; i++) {
                    this.elements.separators[i].style.height = this._innerHeightTree + 'px';
                }
                for (var i = 0; i < this.elements.linesVertical.length; i++) {
                    this.elements.linesVertical[i].style.height = this._innerHeightTree + 'px';
                }
            }

        };

        this._prepareItline = function() {
            this.startDate = new DayPilot.Date(this.startDate).getDatePart();
            this.endDate = (this.viewType !== 'Days') ? this.startDate.addDays(this.days) : this.startDate.addDays(1);

            this._cache.pixels = [];
            this.itline = [];
            
            var autoCellWidth = this.cellWidthSpec === "Auto";
            //var customWidth = false;
            
            var updateCellWidthForAuto = function() {
                if (!autoCellWidth) {
                    return;
                }
                var count = 0;
                if (calendar.timeHeader) {
                    if (calendar.timeline) {
                        count = calendar.timeline.length;
                    }
                    else {
                        var row = calendar.timeHeader[calendar.timeHeader.length - 1];
                        count = row.length;
                    }
                }
                else {
                    if (calendar.scale === "Manual") {
                        count = calendar.timeline.length;
                    }
                    else {
                        calendar._generateTimeline();  // hack
                        count = calendar.itline.length;
                        calendar.itline = [];
                    }
                }
                var width = calendar._getScrollableWidth();
                if (count > 0 && width > 0) {
                    //calendar.cellWidth = Math.floor(width / count);
                    calendar.cellWidth = width / count;
                    //if (calendar.cellWidth * count > width)
                }
                calendar.debug.message("updated cellWidth: " + calendar.cellWidth);

            };
            
            
            updateCellWidthForAuto();
            
            //calendar.debug.message("timeheader: " + this.timeHeader);
            
            // set on the server, copy from there
            if (this._serverBased()) {  // timeline supplied from the server
                if (this.timeline) {  // TODO dissolve 
                    calendar.debug.message("timeline received from server");
                    this.itline = [];
                    var lastEnd = null;
                    var left = 0;
                    for (var i = 0; i < this.timeline.length; i++) {
                        
                        /*
                        if (src.width) {
                            customWidth = true;
                        }*/
                        
                        var src = this.timeline[i];
                        var cell = {};
                        cell.start = new DayPilot.Date(src.start);
                        cell.end =  src.end ? new DayPilot.Date(src.end) : cell.start.addMinutes(this.cellDuration);

                        if (!src.width) {
                            var right = Math.floor(left + this.cellWidth);
                            var width = right - Math.floor(left);

                            cell.left = Math.floor(left);
                            cell.width = width;
                            left += this.cellWidth;
                        }
                        else {
                            cell.left = src.left || left; // left is optional TODO remove original syntax
                            cell.width = src.width || this.cellWidth; // width is optional TODO remove original syntax
                            left += cell.width;
                        }

                        /*
                        if (autoCellWidth) {
                            cell.left = Math.floor(left);
                            cell.width = Math.floor(this.cellWidth); // width is optional
                        }
                        else {
                            cell.left = src.left || left; // left is optional
                            cell.width = src.width || this.cellWidth; // width is optional
                        }
                        */
                        

                        cell.breakBefore = lastEnd && lastEnd.ticks !== cell.start.ticks;
                        lastEnd = cell.end;
                        
                        this.itline.push(cell);
                    }
                }

                if (autoCellWidth) {
                    this._updateHeaderGroupDimensions();
                }
            }
            else {
                this.timeHeader = [];

                if (this.scale === "Manual") {
                    this.itline = [];
                    var left = 0;
                    var lastEnd = null;
                    for (var i = 0; i < this.timeline.length; i++) {
                        var src = this.timeline[i];

                        /*
                        if (src.width) {
                            customWidth = true;
                        }
                        */

                        var cell = {};
                        cell.start = new DayPilot.Date(src.start);
                        cell.end =  src.end ? new DayPilot.Date(src.end) : cell.start.addMinutes(this.cellDuration);
                        
                        var w = src.width || this.cellWidth;

                        var right = Math.floor(left + w);
                        var width = right - Math.floor(left);

                        cell.left = Math.floor(left);
                        cell.width = width;
                        left += w;
                        
                        // TODO custom width
                        
                        //cell.left = Math.floor(left);
                        //cell.width = Math.floor(src.width || this.cellWidth);
                        //cell.breakBefore = src.breakBefore;
                        
                        cell.breakBefore = lastEnd && lastEnd.ticks !== cell.start.ticks;
                        lastEnd = cell.end;

                        this.itline.push(cell);
                        
                        //left += cell.width;
                    }
                }
                else {
                    this._generateTimeline();
                }

                //updateItlineForAutoCellWidth();
                this._prepareHeaderGroups();
            }
        };
        
        this._generateTimeline = function() {

            var start = this.startDate;
            var end = this._addScaleSize(start); //
            var breakBefore = false;

            // groups
            var timeHeaders = this.timeHeaders;

            var left = 0;
            //var hrow = [];
            while (end.ticks <= this.endDate.ticks && end.ticks > start.ticks) {
                if (this._includeCell(start, end)) {
                    
                    var right = Math.floor(left + this.cellWidth);
                    var width = right - Math.floor(left);
                    
                    var timeCell = {};
                    timeCell.start = start;
                    timeCell.end = end;
                    timeCell.left = Math.floor(left);
                    timeCell.width = width;
                    timeCell.breakBefore = breakBefore;

                    this.itline.push(timeCell);
                    
                    left += this.cellWidth;

                    breakBefore = false;
                }
                else {
                    breakBefore = true;
                }

                start = end;
                end = this._addScaleSize(start);
            }
        };

        this._updateHeaderGroupDimensions = function() {
            calendar.debug.message("updateHeaderGroupDimensions");
            if (!this.timeHeader) {
                return;
            }
            for (var y = 0; y < this.timeHeader.length; y++) {
                var row = this.timeHeader[y];
                for (var x = 0; x < row.length; x++) {
                    var h = row[x];

                    h.left = this.getPixels(new DayPilot.Date(h.start)).left;
                    var right = this.getPixels(new DayPilot.Date(h.end)).left;
                    var width = right - h.left;
                    h.width = width;
                    //calendar.debug.message("cell: " + h.start + "-" + h.end + " : left: " + h.left + " width:" + h.width);
                }
            }
        };
        
        this._prepareHeaderGroups = function() {
            var timeHeaders = this.timeHeaders;
            if (!timeHeaders) {
                timeHeaders = [
                    {"groupBy": this.cellGroupBy},
                    {"groupBy": "Cell"}
                ];
            }
            //var timeHeaders = this.timeHeaders;
            for (var i = 0; i < timeHeaders.length; i++) {
                var groupBy = timeHeaders[i].groupBy;
                var format = timeHeaders[i].format;
                
                if (groupBy === "Default") {
                    groupBy = this.cellGroupBy;
                }

                var start = this.startDate;
                var line = [];

                //var cell = {};
                var start = this.startDate;
                
                while (start.ticks < this.endDate.ticks) {
                    var h = {};
                    h.start = start;
                    h.end = this._addGroupSize(h.start, groupBy);
                    
                    if (h.start.ticks === h.end.ticks) {
                        break;
                    }
                    h.left = this.getPixels(h.start).left;
                    var right = this.getPixels(h.end).left;
                    var width = right - h.left;
                    h.width = width;
                    
                    h.colspan = Math.ceil(width / (1.0 * this.cellWidth));
                    if (format) {
                        h.innerHTML = h.start.toString(format, resolved.locale());
                    }
                    else {
                        h.innerHTML = this._getGroupName(h, groupBy);
                    }

                    if (width > 0) {

                        if (typeof this.onBeforeTimeHeaderRender === 'function') {
                            var cell = {};
                            cell.start = h.start;
                            cell.end = h.end;
                            cell.html = h.innerHTML;
                            cell.toolTip = h.innerHTML;
                            //cell.color = null;
                            cell.backColor = null;
                            if (!this.cssOnly) {
                                cell.backColor = this.hourNameBackColor;
                            }
                            cell.level = this.timeHeader.length; 

                            var args = {};
                            args.header = cell;

                            this.onBeforeTimeHeaderRender(args);

                            h.innerHTML = cell.html;
                            h.backColor = cell.backColor;
                            h.toolTip = cell.toolTip;
                        }

                        line.push(h);
                    }
                    start = h.end;
                }
                this.timeHeader.push(line);
            }
        };

        this._includeCell = function(start, end) {

            if (typeof this.onIncludeTimeCell === 'function') {
                var cell = {};
                cell.start = start;
                cell.end = end;
                cell.visible = true;
                
                var args = {};
                args.cell = cell;
                
                this.onIncludeTimeCell(args);
                
                return cell.visible;
            }

            if (this.showNonBusiness) {
                return true;
            }

            if (start.d.getUTCDay() === 0) { // Sunday
                return false;
            }
            if (start.d.getUTCDay() === 6) { // Saturday
                return false;
            }

            var cellDuration = (end.getTime() - start.getTime()) / (1000 * 60);  // minutes
            if (cellDuration < 60 * 24) {  // cell is smaller than one day
                var startHour = start.d.getUTCHours();
                startHour += start.d.getUTCMinutes() / 60.0;
                startHour += start.d.getUTCSeconds() / 3600.0;
                startHour += start.d.getUTCMilliseconds() / 3600000.0;

                /*
                var endHour = end.d.getUTCHours();
                endHour += end.d.getUTCMinutes()/60;
                endHour += end.d.getUTCSeconds()/3600;
                endHour += end.d.getUTCMilliseconds()/3600000;
                */

                if (startHour < this.businessBeginsHour) {
                    return false;
                }

                if (this.businessEndsHour >= 24) {
                    return true;
                }
                if (startHour >= this.businessEndsHour) {
                    return false;
                }
            }
            return true;
        };


        this.getPixels = function(date) {
            var ticks = date.ticks;

            var cache = this._cache.pixels[ticks];
            if (cache) {
                return cache;
            }
            
            var previous = null;
            var previousEndTicks = 221876841600000;  // December 31, 9000

            if (this.itline.length === 0 || ticks < this.itline[0].start.ticks) {
                var result = {};
                result.cut = false;
                result.left = 0;
                result.boxLeft = result.left;
                result.boxRight = result.left;
                result.i = null; // not in range
                return result;
            }
            
            //var thisone = date.toString() === "2014-05-01T00:00:00";

            for (var i = 0; i < this.itline.length; i++) {
                var found = false;
                var cell = this.itline[i];

                var cellStartTicks = cell.start.ticks;
                var cellEndTicks = cell.end.ticks;

                if (cellStartTicks < ticks && ticks < cellEndTicks) {  // inside
                    var offset = ticks - cellStartTicks;
                    
                    var result = {};
                    result.cut = false;
                    result.left = cell.left + this._ticksToPixels(cell, offset);
                    result.boxLeft = cell.left;
                    result.boxRight = cell.left + cell.width;
                    result.i = i;
                    break;
                }
                else if (cellStartTicks === ticks) {  // start

                    var result = {};
                    result.cut = false;
                    result.left = cell.left;
                    result.boxLeft = result.left;
                    result.boxRight = result.left + cell.width;
                    result.i = i;
                    break;
                }
                else if (cellEndTicks === ticks) {  // end
                    
                    var result = {};
                    result.cut = false;
                    result.left = cell.left + cell.width;
                    result.boxLeft = result.left;
                    result.boxRight = result.left;
                    result.i = i + 1;
                    break;
                }
                else if (ticks < cellStartTicks && ticks > previousEndTicks) {  // hidden
                    
                    var result = {};
                    result.cut = true;
                    result.left = cell.left;
                    result.boxLeft = result.left;
                    result.boxRight = result.left;
                    result.i = i;
                    break;
                }

                previousEndTicks = cellEndTicks;
            }

            if (!result) {
                var last = this.itline[this.itline.length - 1];
                
                var result = {};
                result.cut = true;
                result.left = last.left + last.width;
                result.boxLeft = result.left;
                result.boxRight = result.left;
                result.i = null; // not in range
                //this.log.c5 = this.log.c5 ? this.log.c5+1 : 1;
            }

            this._cache.pixels[ticks] = result;
            return result;
        };

        // left - pixel offset from start
        // precise - true: calculates exact date from pixels, false: the it's the cell start
        // isEnd - returns the end of the previous cell
        // 
        // isEnd and precise can't be combined
        this.getDate = function(left, precise, isEnd) {
            //var x = Math.floor(left / this.cellWidth);
            var position = this._getItlineCellFromPixels(left, isEnd);
            
            if (!position) {
                return null;
            }
            
            var x = position.x;
            
            var itc = this.itline[x];

            if (!itc) {
                return null;
            }

            //var start = (isEnd && x > 0) ? this.itline[x - 1].end : this.itline[x].start;
            var start = (isEnd && !precise) ? itc.end : itc.start;

            if (!precise) {
                return start;
            }
            else {
                return start.addTime(this._pixelsToTicks(position.cell, position.offset));
            }

        };
        
        this._getItlineCellFromPixels = function(pixels, isEnd) {
            var pos = 0;
            var previous = 0;
            for (var i = 0; i < this.itline.length; i++) {
                var cell = this.itline[i];
                var width = cell.width || this.cellWidth;
                pos += width;
                
                if ((pixels < pos) || (isEnd && pixels === pos)) {
                    var result = {};
                    result.x = i;
                    result.offset = pixels - previous;
                    result.cell = cell;
                    return result;
                }
                
                previous = pos;
            }
            var cell = this.itline[this.itline.length - 1];
            
            var result = {};
            result.x = this.itline.length - 1;
            result.offset = cell.width || this.cellWidth;
            result.cell = cell;
            return result;
        };

        this._getItlineCellFromTime = function(time) {
            var pos = new DayPilot.Date(time);
            //var previous = 0;
            for (var i = 0; i < this.itline.length; i++) {
                var cell = this.itline[i];
                
                if (cell.start.ticks <= pos.ticks && pos.ticks < cell.end.ticks) {
                    var result = {};
                    result.hidden = false;
                    result.current = cell;
                    return result;
                }
                if (pos.ticks < cell.start.ticks)   // it's a hidden cell
                {
                    var result = {};
                    result.hidden = true;
                    result.previous = i > 0 ? this.itline[i - 1] : null;
                    result.current = null;
                    result.next = this.itline[i];
                    return result;
                }
                
                //pos = pos.addMinutes(1);
            }
            var result = {};
            result.past = true;
            result.previous = this.itline[this.itline.length - 1];
            
            return result;
        };

        this._ticksToPixels = function(cell, ticks) { // DEBUG check that it's not used improperly (timeline)
            var width = cell.width || this.cellWidth;
            var duration = cell.end.ticks - cell.start.ticks;
            return Math.floor((width * ticks) / (duration));
        };

        this._pixelsToTicks = function(cell, pixels) {
            var duration = cell.end.ticks - cell.start.ticks;
            var width = cell.width || this.cellWidth;
            return Math.floor(pixels / width * duration );
        };
        
        this._onEventClick = function(ev) {
            if (touch.start) {
                return;
            }
            moving = {}; // clear
            calendar._eventClickDispatch(this, ev);  
        };

        this.eventClickPostBack = function(e, data) {
            this._postBack2('EventClick', e, data);
        };
        this.eventClickCallBack = function(e, data) {
            this._callBack2('EventClick', e, data);
        };

        this._eventClickDispatch = function(div, ev) {
            var e = div.event;

            var ev = ev || window.event;

            if (typeof (DayPilotBubble) !== 'undefined') {
                DayPilotBubble.hideActive();
            }

            //if (calendar.eventDoubleClickHandling === 'Disabled') {
            if (!e.client.doubleClickEnabled()) {
                calendar._eventClickSingle(div, ev);
                return;
            }

            if (!calendar.timeouts.click) {
                calendar.timeouts.click = [];
            }

            var eventClickDelayed = function(div, ev) {
                return function() {
                    calendar._eventClickSingle(div, ev);
                };
            };

            calendar.timeouts.click.push(window.setTimeout(eventClickDelayed(div, ev), calendar.doubleClickTimeout));

        };

        this._eventClickSingle = function(div, ev) {
            if (typeof ev === "boolean") {
                throw "Invalid _eventClickSingle parameters";
            }
            var e = div.event;
            var ctrlKey = ev.ctrlKey;
            var metaKey = ev.metaKey;
            //var data = div.data;
            
            if (!e.client.clickEnabled()) {
                return;
            }
            
            if (calendar._api2()) {
                
                var args = {};
                args.e = e;
                args.div = div;
                args.originalEvent = ev;
                args.ctrl = ctrlKey;
                args.meta = metaKey;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onEventClick === 'function') {
                    calendar.onEventClick(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }
                
                switch (calendar.eventClickHandling) {
                    case 'PostBack':
                        calendar.eventClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.eventClickCallBack(e);
                        break;
                    case 'Edit':
                        calendar._divEdit(div);
                        break;
                    case 'Select':
                        calendar._eventSelect(div, e, ctrlKey, metaKey);
                        break;
                    case 'ContextMenu':
                        var menu = e.client.contextMenu();
                        if (menu) {
                            menu.show(e);
                        }
                        else {
                            if (calendar.contextMenu) {
                                calendar.contextMenu.show(e);
                            }
                        }
                        break;
                    case 'Bubble':
                        if (calendar.bubble) {
                            calendar.bubble.showEvent(e);
                        }
                        break;

                }
                
                if (typeof calendar.onEventClicked === 'function') {
                    calendar.onEventClicked(args);
                }                
                
            }
            else {
                switch (calendar.eventClickHandling) {
                    case 'PostBack':
                        calendar.eventClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.eventClickCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onEventClick(e);
                        break;
                    case 'Edit':
                        calendar._divEdit(div);
                        break;
                    case 'Select':
                        calendar._eventSelect(div, e, ctrlKey, metaKey);
                        break;
                    case 'ContextMenu':
                        var menu = e.client.contextMenu();
                        if (menu) {
                            menu.show(e);
                        }
                        else {
                            if (calendar.contextMenu) {
                                calendar.contextMenu.show(e);
                            }
                        }
                        break;
                    case 'Bubble':
                        if (calendar.bubble) {
                            calendar.bubble.showEvent(e);
                        }
                        break;

                }
                
            }

        };
        
        this._eventDeleteDispatch = function(e) {

            if (calendar._api2()) {

                var args = {};
                args.e = e;
                
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onEventDelete === 'function') {
                    calendar.onEventDelete(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (calendar.eventDeleteHandling) {
                    case 'PostBack':
                        calendar.eventDeletePostBack(e);
                        break;
                    case 'CallBack':
                        calendar.eventDeleteCallBack(e);
                        break;
                    case 'Update':
                        calendar.events.remove(e);
                        break;
                }
                
                if (typeof calendar.onEventDeleted === 'function') {
                    calendar.onEventDeleted(args);
                }
            }
            else {
                switch (calendar.eventDeleteHandling) {
                    case 'PostBack':
                        calendar.eventDeletePostBack(e);
                        break;
                    case 'CallBack':
                        calendar.eventDeleteCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onEventDelete(e);
                        break;
                }
            }

        };

        this.eventDeletePostBack = function(e, data) {
            this._postBack2('EventDelete', e, data);
        };
        this.eventDeleteCallBack = function(e, data) {
            this._callBack2('EventDelete', e, data);
        };

        // obsolete
        this.setHScrollPosition = function(pixels) {
            this.nav.scroll.scrollLeft = pixels;
        };

        this.getScrollX = function() {
            return this.nav.scroll.scrollLeft;
        };

        // obsolete
        this.getHScrollPosition = this.getScrollX;

        this.getScrollY = function() {
            return this.nav.scroll.scrollTop;
        };

        this._eventSelect = function(div, e, ctrlKey, metaKey) {
            calendar._eventSelectDispatch(div, e, ctrlKey, metaKey);
        };

        this.eventSelectPostBack = function(e, change, data) {
            var params = {};
            params.e = e;
            params.change = change;
            this._postBack2('EventSelect', params, data);
        };
        this.eventSelectCallBack = function(e, change, data) {
            var params = {};
            params.e = e;
            params.change = change;
            this._callBack2('EventSelect', params, data);
        };

        this._eventSelectDispatch = function(div, e, ctrlKey, metaKey) {
            //var e = this.selectedEvent();

            if (calendar._api2()) {
                
                var m = calendar.multiselect;
                m.previous = m.events();

                var args = {};
                args.e = e;
                args.selected = m.isSelected(e);
                args.ctrl = ctrlKey;
                args.meta = metaKey;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onEventSelect === 'function') {
                    calendar.onEventSelect(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (calendar.eventSelectHandling) {
                    case 'PostBack':
                        calendar.eventSelectPostBack(e, change);
                        break;
                    case 'CallBack':
                        if (typeof WebForm_InitCallback !== 'undefined') {
                            window.__theFormPostData = "";
                            window.__theFormPostCollection = [];
                            WebForm_InitCallback();
                        }
                        calendar.eventSelectCallBack(e, change);
                        break;
                    case 'Update':
                        m._toggleDiv(div, ctrlKey, metaKey);
                        break;
                }
                
                if (typeof calendar.onEventSelected === 'function') {
                    args.change = m.isSelected(e) ? "selected" : "deselected";
                    args.selected = m.isSelected(e);
                    calendar.onEventSelected(args);
                }                
                
            }
            else {
                var m = calendar.multiselect;
                m.previous = m.events();
                m._toggleDiv(div, ctrlKey, metaKey);
                var change = m.isSelected(e) ? "selected" : "deselected";

                switch (calendar.eventSelectHandling) {
                    case 'PostBack':
                        calendar.eventSelectPostBack(e, change);
                        break;
                    case 'CallBack':
                        if (typeof WebForm_InitCallback !== 'undefined') {
                            window.__theFormPostData = "";
                            window.__theFormPostCollection = [];
                            WebForm_InitCallback();
                        }
                        calendar.eventSelectCallBack(e, change);
                        break;
                    case 'JavaScript':
                        calendar.onEventSelect(e, change);
                        break;
                }
            }


        };

        this.eventRightClickPostBack = function(e, data) {
            this._postBack2('EventRightClick', e, data);
        };
        this.eventRightClickCallBack = function(e, data) {
            this._callBack2('EventRightClick', e, data);
        };

        this._eventRightClickDispatch = function(ev) {
            var e = this.event;

            ev = ev || window.event;
            ev.cancelBubble = true;

            if (!this.event.client.rightClickEnabled()) {
                return false;
            }
            
            if (calendar._api2()) {

                var args = {};
                args.e = e;
                args.div = this;
                args.originalEvent = ev;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onEventRightClick === 'function') {
                    calendar.onEventRightClick(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }
                
                switch (calendar.eventRightClickHandling) {
                    case 'PostBack':
                        calendar.eventRightClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.eventRightClickCallBack(e);
                        break;
                    case 'ContextMenu':
                        var menu = e.client.contextMenu();
                        if (menu) {
                            menu.show(e);
                        }
                        else {
                            if (calendar.contextMenu) {
                                calendar.contextMenu.show(this.event);
                            }
                        }
                        break;
                    case 'Bubble':
                        if (calendar.bubble) {
                            calendar.bubble.showEvent(e);
                        }
                        break;                        
                }
                
                if (typeof calendar.onEventRightClicked === 'function') {
                    calendar.onEventRightClicked(args);
                }                
                
            }
            else {
                switch (calendar.eventRightClickHandling) {
                    case 'PostBack':
                        calendar.eventRightClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.eventRightClickCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onEventRightClick(e);
                        break;
                    case 'ContextMenu':
                        var menu = e.client.contextMenu();
                        if (menu) {
                            menu.show(e);
                        }
                        else {
                            if (calendar.contextMenu) {
                                calendar.contextMenu.show(this.event);
                            }
                        }
                        break;
                    case 'Bubble':
                        if (calendar.bubble) {
                            calendar.bubble.showEvent(e);
                        }
                        break;                        
                }
            }


            return false;
        };

        this.eventDoubleClickPostBack = function(e, data) {
            this._postBack2('EventDoubleClick', e, data);
        };
        this.eventDoubleClickCallBack = function(e, data) {
            this._callBack2('EventDoubleClick', e, data);
        };

        this._eventDoubleClickDispatch = function(ev) {

            if (typeof (DayPilotBubble) !== 'undefined') {
                DayPilotBubble.hideActive();
            }


            if (calendar.timeouts.click) {
                for (var toid in calendar.timeouts.click) {
                    window.clearTimeout(calendar.timeouts.click[toid]);
                }
                calendar.timeouts.click = null;
            }

            var ev = ev || window.event;
            var e = this.event;

            if (calendar._api2()) {
                
                var args = {};
                args.e = e;
                args.originalEvent = ev;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onEventDoubleClick === 'function') {
                    calendar.onEventDoubleClick(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (calendar.eventDoubleClickHandling) {
                    case 'PostBack':
                        calendar.eventDoubleClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.eventDoubleClickCallBack(e);
                        break;
                    case 'Edit':
                        calendar._divEdit(this);
                        break;
                    case 'Select':
                        calendar._eventSelect(div, e, ev.ctrlKey, ev.metaKey);
                        break;
                    case 'Bubble':
                        if (calendar.bubble) {
                            calendar.bubble.showEvent(e);
                        }
                        break;
                    case 'ContextMenu':
                        var menu = e.client.contextMenu();
                        if (menu) {
                            menu.show(e);
                        }
                        else {
                            if (calendar.contextMenu) {
                                calendar.contextMenu.show(e);
                            }
                        }
                        break;

                }
                
                if (typeof calendar.onEventDoubleClicked === 'function') {
                    calendar.onEventDoubleClicked(args);
                }

            }
            else {
                switch (calendar.eventDoubleClickHandling) {
                    case 'PostBack':
                        calendar.eventDoubleClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.eventDoubleClickCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onEventDoubleClick(e);
                        break;
                    case 'Edit':
                        calendar._divEdit(this);
                        break;
                    case 'Select':
                        calendar._eventSelect(div, e, ev.ctrlKey, ev.metaKey);
                        break;
                    case 'Bubble':
                        if (calendar.bubble) {
                            calendar.bubble.showEvent(e);
                        }
                        break;
                    case 'ContextMenu':
                        var menu = e.client.contextMenu();
                        if (menu) {
                            menu.show(e);
                        }
                        else {
                            if (calendar.contextMenu) {
                                calendar.contextMenu.show(e);
                            }
                        }
                        break;
                }
                
            }

        };

        this.eventResizePostBack = function(e, newStart, newEnd, data) {
            this._invokeEventResize("PostBack", e, newStart, newEnd, data);

        };
        this.eventResizeCallBack = function(e, newStart, newEnd, data) {
            this._invokeEventResize("CallBack", e, newStart, newEnd, data);
        };

        this._invokeEventResize = function(type, e, newStart, newEnd, data) {
            var params = {};
            params.e = e;
            params.newStart = newStart;
            params.newEnd = newEnd;

            this._invokeEvent(type, "EventResize", params, data);
        };


        this._eventResizeDispatch = function(e, newStart, newEnd) {

            if (this.eventResizeHandling === 'Disabled') {
                return;
            }
            
            if (calendar._api2()) {
                // API v2
                var args = {};

                args.e = e;
                args.newStart = newStart;
                args.newEnd = newEnd;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onEventResize === 'function') {
                    calendar.onEventResize(args);
                    if (args.preventDefault.value) {
                        return;
                    }

                    newStart = args.newStart;
                    newEnd = args.newEnd;
                }

                switch (calendar.eventResizeHandling) {
                    case 'PostBack':
                        calendar.eventResizePostBack(e, newStart, newEnd);
                        break;
                    case 'CallBack':
                        calendar.eventResizeCallBack(e, newStart, newEnd);
                        break;
                    case 'Notify':
                        calendar.eventResizeNotify(e, newStart, newEnd);
                        break;
                    case 'Update':
                        e.start(newStart);
                        e.end(newEnd);
                        calendar.events.update(e);
                        break;
                }

                if (typeof calendar.onEventResized === 'function') {
                    calendar.onEventResized(args);
                }
            }
            else {
                switch (calendar.eventResizeHandling) {
                    case 'PostBack':
                        calendar.eventResizePostBack(e, newStart, newEnd);
                        break;
                    case 'CallBack':
                        calendar.eventResizeCallBack(e, newStart, newEnd);
                        break;
                    case 'JavaScript':
                        calendar.onEventResize(e, newStart, newEnd);
                        break;
                    case 'Notify':
                        calendar.eventResizeNotify(e, newStart, newEnd);
                        break;
                    case 'Update':
                        e.start(newStart);
                        e.end(newEnd);
                        calendar.events.update(e);
                        break;
                }                
            }
          
        };

        this.eventMovePostBack = function(e, newStart, newEnd, newResource, data, line) {
            this._invokeEventMove("PostBack", e, newStart, newEnd, newResource, data, line);
        };

        this.eventMoveCallBack = function(e, newStart, newEnd, newResource, data, line) {
            this._invokeEventMove("CallBack", e, newStart, newEnd, newResource, data, line);
        };

        this._invokeEventMove = function(type, e, newStart, newEnd, newResource, data, line) {
            var params = {};
            params.e = e;
            params.newStart = newStart;
            params.newEnd = newEnd;
            params.newResource = newResource;
            params.position = line;

            this._invokeEvent(type, "EventMove", params, data);
        };

        this._invokeEvent = function(type, action, params, data) {

            if (type === 'PostBack') {
                calendar._postBack2(action, params, data);
            }
            else if (type === 'CallBack') {
                calendar._callBack2(action, params, data, "CallBack");
            }
            else if (type === 'Immediate') {
                calendar._callBack2(action, params, data, "Notify");
            }
            else if (type === 'Queue') {
                calendar.queue.add(new DayPilot.Action(this, action, params, data));
            }
            else if (type === 'Notify') {
                if (resolved.notifyType() === 'Notify') {
                    calendar._callBack2(action, params, data, "Notify");
                }
                else {
                    calendar.queue.add(new DayPilot.Action(calendar, action, params, data));
                }
            }
            else {
                throw "Invalid event invocation type";
            }
        };

        this.eventMoveNotify = function(e, newStart, newEnd, newResource, data, line) {

            var old = new DayPilot.Event(e.copy(), this);

            var rows = calendar.events._removeFromRows(e.data);

            e.start(newStart);
            e.end(newEnd);
            e.resource(newResource);
            e.commit();

            rows = rows.concat(calendar.events._addToRows(e.data));
            calendar._loadRows(rows);

            calendar._updateRowHeights();

            calendar._updateRowsNoLoad(rows);

            this._invokeEventMove("Notify", old, newStart, newEnd, newResource, data, line);

        };

        this.eventResizeNotify = function(e, newStart, newEnd, data) {

            var old = new DayPilot.Event(e.copy(), this);

            var rows = calendar.events._removeFromRows(e.data);

            e.start(newStart);
            e.end(newEnd);
            e.commit();

            rows = rows.concat(calendar.events._addToRows(e.data));
            
            calendar._loadRows(rows);

            calendar._updateRowHeights();

            calendar._updateRowsNoLoad(rows);

            this._invokeEventResize("Notify", old, newStart, newEnd, data);

        };

        // internal methods for handling event selection
        this.multiselect = {};

        this.multiselect.initList = [];
        this.multiselect.list = [];
        this.multiselect.divs = [];
        this.multiselect.previous = [];

        this.multiselect._serialize = function() {
            var m = calendar.multiselect;
            return DayPilot.JSON.stringify(m.events());
        };

        this.multiselect.events = function() {
            var m = calendar.multiselect;
            var events = [];
            events.ignoreToJSON = true;
            for (var i = 0; i < m.list.length; i++) {
                events.push(m.list[i]);
            }
            return events;
        };

        this.multiselect._updateHidden = function() {
            // not implemented
        };

        this.multiselect._toggleDiv = function(div, ctrl, meta) {
            var m = calendar.multiselect;

            if (m.isSelected(div.event)) {
                if (calendar.allowMultiSelect) {
                    if (ctrl || meta) {
                        m.remove(div.event, true);
                    }
                    else {
                        var count = m.list.length;
                        m.clear();
                        if (count > 1) {
                            m.add(div.event, true);
                        }
                    }
                }
                else { // clear all
                    m.clear();
                }
            }
            else {
                if (calendar.allowMultiSelect) {
                    if (ctrl || meta) {
                        m.add(div.event, true);
                    }
                    else {
                        m.clear();
                        m.add(div.event, true);
                    }
                }
                else {
                    m.clear();
                    m.add(div.event, true);
                }
            }
            //m.redraw();
            m._update(div);
            m._updateHidden();
        };

        // compare event with the init select list
        this.multiselect._shouldBeSelected = function(ev) {
            var m = calendar.multiselect;
            return m._isInList(ev, m.initList);
        };

        this.multiselect._alert = function() {
            var m = calendar.multiselect;
            var list = [];
            for (var i = 0; i < m.list.length; i++) {
                var event = m.list[i];
                list.push(event.value());
            }
            alert(list.join("\n"));
        };

        this.multiselect.add = function(ev, dontRedraw) {
            var m = calendar.multiselect;
            if (m.indexOf(ev) === -1) {
                m.list.push(ev);
            }
            
            if (dontRedraw) {
                return;
            }
            m.redraw();

        };

        this.multiselect.remove = function(ev, dontRedraw) {
            var m = calendar.multiselect;
            var i = m.indexOf(ev);
            if (i !== -1) {
                m.list.splice(i, 1);
            }

            if (dontRedraw) {
                return;
            }
            m.redraw();
        };

        this.multiselect.clear = function(dontRedraw) {
            var m = calendar.multiselect;
            m.list = [];

            if (dontRedraw) {
                return;
            }
            m.redraw();
        };

        this.multiselect.redraw = function() {
            var m = calendar.multiselect;

            for (var i = 0; i < calendar.elements.events.length; i++) {
                var div = calendar.elements.events[i];
                if (!div.event) {
                    continue;
                }
                if (!div.event.isEvent) {
                    continue;
                }
                if (m.isSelected(div.event)) {
                    m._divSelect(div);
                }
                else {
                    m._divDeselect(div);
                }
            }
        };

        /*
        this.multiselect._redrawForRow = function(i) {

        };
        */

        // not used
        this.multiselect._updateEvent = function(ev) {
            var m = calendar.multiselect;
            var div = null;
            for (var i = 0; i < calendar.elements.events.length; i++) {
                if (m.isSelected(calendar.elements.events[i].event)) {
                    div = calendar.elements.events[i];
                    break;
                }
            }
            m._update(div);
        };

        // used for faster redraw
        this.multiselect._update = function(div) {
            if (!div) {
                return;
            }

            var m = calendar.multiselect;

            if (m.isSelected(div.event)) {
                m._divSelect(div);
            }
            else {
                m._divDeselect(div);
            }
        };


        this.multiselect._divSelect = function(div) {
            var m = calendar.multiselect;
            var cn = calendar.cssOnly ? calendar._prefixCssClass("_selected") : calendar._prefixCssClass("selected");
            var div = m._findContentDiv(div);
            DayPilot.Util.addClass(div, cn);
            m.divs.push(div);
        };

        
        this.multiselect._findContentDiv = function(div) {
            return div;
        };

        this.multiselect._divDeselectAll = function() {
            var m = calendar.multiselect;
            for (var i = 0; i < m.divs.length; i++) {
                var div = m.divs[i];
                m._divDeselect(div, true);
            }
            m.divs = [];
        };

        this.multiselect._divDeselect = function(div, dontRemoveFromCache) {
            var m = calendar.multiselect;
            var cn = calendar.cssOnly ? calendar._prefixCssClass("_selected") : calendar._prefixCssClass("selected");
            DayPilot.Util.removeClass(div, cn);

            if (dontRemoveFromCache) {
                return;
            }
            var i = DayPilot.indexOf(m.divs, div);
            if (i !== -1) {
                m.divs.splice(i, 1);
            }

        };

        this.multiselect.isSelected = function(ev) {
            if (!ev) {
                return false;
            }
            if (!ev.isEvent) {
                return false;
            }
            return calendar.multiselect._isInList(ev, calendar.multiselect.list);
        };

        this.multiselect.indexOf = function(ev) {
            //return DayPilot.indexOf(calendar.multiselect.list, ev);
            var data = ev.data;
            for (var i = 0; i < calendar.multiselect.list.length; i++) {
                var item = calendar.multiselect.list[i];
                if (item.data === data) {
                    return i;
                }
            }
            return -1;
        };

        this.multiselect._isInList = function(e, list) {
            if (!list) {
                return false;
            }
            for (var i = 0; i < list.length; i++) {
                var ei = list[i];
                if (e === ei) {
                    return true;
                }
                if (typeof ei.id === 'function') {
                    if (ei.id() !== null && e.id() !== null && ei.id() === e.id()) {
                        return true;
                    }
                    if (ei.id() === null && e.id() === null && ei.recurrentMasterId() === e.recurrentMasterId() && e.start().toStringSortable() === ei.start().toStringSortable()) {
                        return true;
                    }
                }
                else {
                    if (ei.id !== null && e.id() !== null && ei.id === e.id()) {
                        return true;
                    }
                    if (ei.id === null && e.id() === null && ei.recurrentMasterId === e.recurrentMasterId() && e.start().toStringSortable() === ei.start) {
                        return true;
                    }
                }

            }

            return false;
        };

        this._update = function(args) {
            var args = args || {};
            var full = !args.eventsOnly;

            if (full) {
                if (!this._serverBased()) {
                    calendar.timeHeader = null;
                    calendar.cellProperties = {};
                }
                calendar._calculateCellWidth();
                calendar._prepareItline();
                if (!args || !args.dontLoadResources) {
                    calendar._loadResources();
                }
            }

            this._loadEvents();

            if (full) {
                calendar._updateCorner();
                calendar._drawResHeader();
                calendar._drawTimeHeader();
            }

            calendar._prepareRowTops();
            calendar._updateRowHeaderHeights();
            calendar._updateRowHeaderWidth();
            calendar._updateHeaderHeight();

            linktools.hideLinkpoints();

            this._deleteEvents();
            this._deleteSeparators();
            this._deleteCells();

            this._clearCachedValues();
            this._expandCellProperties();

            this._drawCells();
            this._drawSeparators();

            calendar._updateHeight();

            if (args.immediateEvents || args.eventsOnly) {
                calendar._drawEvents();
            }
            else {
                setTimeout(function() { calendar._drawEvents(); }, 100);
            }

            if (DayPilot.isArray(calendar.multiselect.list) && calendar.multiselect.list.length > 0) {
                this.multiselect.redraw();
            }

            this._updateFloats();

            this._onScroll();

        };


        // full update
        this.update = function() {
            this._update({"immediateEvents":true});
        };

        this._updateRowsNoLoad = function(rows, appendOnlyIfPossible, finishedCallBack) {
            //var start, end;

            rows = DayPilot.ua(rows);

            for (var i = 0; i < rows.length; i++) {
                var ri = rows[i];
                calendar._drawRowForced(ri);
            }

            if (this._rowsDirty) {
                this._prepareRowTops();
                this._updateRowHeaderHeights();

                this._deleteCells();

                this._deleteSeparators();

                for (var i = 0; i < rows.length; i++) {
                    var ri = rows[i];
                    this._deleteEventsInRow(ri);
                }

                for (var i = 0; i < rows.length; i++) {
                    var ri = rows[i];
                    this._drawEventsInRow(ri);
                }

                this._drawCells();

                this._drawSeparators();
                this._updateEventTops();

            }
            else {
                var batch = true;
                
                if (batch) {
                    var doRow = function(i) {
                        if (i >= rows.length) {
                            return;
                        }
                        var ri = rows[i];
                        if (!appendOnlyIfPossible) {
                            calendar._deleteEventsInRow(ri);
                        }
                        calendar._drawEventsInRow(ri);
                        if (i + 1 < rows.length) {
                            setTimeout(function() { doRow(i+1); }, 10);
                        }
                        else {
                            calendar._findEventsInViewPort();
                            linktools.load();
                            calendar.multiselect.redraw();
                            if (finishedCallBack) {
                                finishedCallBack();
                            }
                        }
                    };
                    doRow(0);
                }
                else {
                    for (var i = 0; i < rows.length; i++) {
                        var ri = rows[i];
                        if (!appendOnlyIfPossible) {
                            this._deleteEventsInRow(ri);
                        }
                        this._drawEventsInRow(ri);
                    }

                }
            }
            
            calendar._findEventsInViewPort();
            linktools.load();
            calendar.multiselect.redraw();

            if (finishedCallBack) {
                finishedCallBack();
            }

            this._clearCachedValues();
            
        };

        this._eventMoveDispatch = function(e, newStart, newEnd, newResource, external, ev, line) {

            if (calendar.eventMoveHandling === 'Disabled') {
                return;
            }

            if (calendar._api2()) {
                // API v2
                var args = {};

                args.e = e;
                args.newStart = newStart;
                args.newEnd = newEnd;
                args.newResource = newResource;
                args.external = external;
                args.ctrl = false;
                args.meta = false;
                args.shift = false;
                if (ev) {
                    args.shift = ev.shiftKey;
                    args.ctrl = ev.ctrlKey;
                    args.meta = ev.metaKey;
                }
                args.position = line;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onEventMove === 'function') {
                    calendar.onEventMove(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                    newStart = args.newStart;
                    newEnd = args.newEnd;
                }

                switch (calendar.eventMoveHandling) {
                    case 'PostBack':
                        calendar.eventMovePostBack(e, newStart, newEnd, newResource, null, line);
                        break;
                    case 'CallBack':
                        calendar.eventMoveCallBack(e, newStart, newEnd, newResource, null, line);
                        break;
                    case 'Notify':
                        calendar.eventMoveNotify(e, newStart, newEnd, newResource, null, line);
                        break;
                    case 'Update':
                        e.start(newStart);
                        e.end(newEnd);
                        e.resource(newResource);
                        if (external) {
                            e.commit();
                            calendar.events.add(e);
                        }
                        else {
                            calendar.events.update(e);
                        }
                        calendar._deleteDragSource();
                        break;
                }
                
                if (typeof calendar.onEventMoved === 'function') {
                    calendar.onEventMoved(args);
                }
            }
            else {
                switch (calendar.eventMoveHandling) {
                    case 'PostBack':
                        calendar.eventMovePostBack(e, newStart, newEnd, newResource, null, line);
                        break;
                    case 'CallBack':
                        calendar.eventMoveCallBack(e, newStart, newEnd, newResource, null, line);
                        break;
                    case 'JavaScript':
                        calendar.onEventMove(e, newStart, newEnd, newResource, external, ev ? ev.ctrlKey : false, ev ? ev.shiftKey : false, line);
                        break;
                    case 'Notify':
                        calendar.eventMoveNotify(e, newStart, newEnd, newResource, null, line);
                        break;
                    case 'Update':
                        e.start(newStart);
                        e.end(newEnd);
                        e.resource(newResource);
                        calendar.events.update(e);
                        break;
                }
            }
        };


        this._bubbleCallBack = function(args, bubble) {
            var guid = calendar._recordBubbleCall(bubble);

            var params = {};
            params.args = args;
            params.guid = guid;

            calendar._callBack2("Bubble", params);
        };

        this._recordBubbleCall = function(bubble) {
            var guid = DayPilot.guid();
            if (!this.bubbles) {
                this.bubbles = [];
            }

            this.bubbles[guid] = bubble;
            return guid;
        };

        this.eventMenuClickPostBack = function(e, command, data) {
            var params = {};
            params.e = e;
            params.command = command;

            this._postBack2('EventMenuClick', params, data);
        };
        this.eventMenuClickCallBack = function(e, command, data) {

            var params = {};
            params.e = e;
            params.command = command;

            this._callBack2('EventMenuClick', params, data);
        };

        this._eventMenuClick = function(command, e, handling) {
            switch (handling) {
                case 'PostBack':
                    calendar.eventMenuClickPostBack(e, command);
                    break;
                case 'CallBack':
                    calendar.eventMenuClickCallBack(e, command);
                    break;
            }
        };

        this.timeRangeSelectedPostBack = function(start, end, resource, data) {
            var range = {};
            range.start = start;
            range.end = end;
            range.resource = resource;

            this._postBack2('TimeRangeSelected', range, data);
        };
        this.timeRangeSelectedCallBack = function(start, end, resource, data) {

            var range = {};
            range.start = start;
            range.end = end;
            range.resource = resource;

            this._callBack2('TimeRangeSelected', range, data);
        };

        this._timeRangeSelectedDispatch = function(start, end, resource) {
            
            if (calendar.timeRangeSelectedHandling === 'Disabled') {
                return;
            }

            if (calendar._api2()) {
                
                var args = {};
                args.start = start;
                args.end = end;
                args.resource = resource;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onTimeRangeSelect === 'function') {
                    calendar.onTimeRangeSelect(args);
                    if (args.preventDefault.value) {
                        return;
                    }

                    start = args.start;
                    end = args.end;
                }

                (function updateRange() {
                    var range = DayPilotScheduler.rangeHold;

                    var itc, cell;

                    // fix start
                    if (start.getTime() < calendar.itline[0].start.getTime()) {
                        range.start.x = 0;
                    }
                    else {
                        itc = calendar._getItlineCellFromTime(start);
                        cell = itc.current || itc.previous;
                        range.start.x = DayPilot.indexOf(calendar.itline, cell);
                    }

                    // fix end
                    itc = calendar._getItlineCellFromTime(end.addMilliseconds(-1));
                    if (itc.past) {
                        range.end.x = calendar.itline.length - 1;
                    }
                    else {
                        cell = itc.current || itc.next;
                        range.end.x = DayPilot.indexOf(calendar.itline, cell);
                    }

                    calendar._drawRange(range);

                })();


                // now perform the default built-in action
                switch (calendar.timeRangeSelectedHandling) {
                    case 'PostBack':
                        calendar.timeRangeSelectedPostBack(start, end, resource);
                        break;
                    case 'CallBack':
                        calendar.timeRangeSelectedCallBack(start, end, resource);
                        break;
                }
                
                if (typeof calendar.onTimeRangeSelected === 'function') {
                    calendar.onTimeRangeSelected(args);
                }
                
            }
            else {
                switch (calendar.timeRangeSelectedHandling) {
                    case 'PostBack':
                        calendar.timeRangeSelectedPostBack(start, end, resource);
                        break;
                    case 'CallBack':
                        calendar.timeRangeSelectedCallBack(start, end, resource);
                        break;
                    case 'JavaScript':
                        calendar.onTimeRangeSelected(start, end, resource);
                        break;
                    case 'Hold':
                        break;
                }
            }
        };

        this.timeRangeDoubleClickPostBack = function(start, end, resource, data) {
            var range = {};
            range.start = start;
            range.end = end;
            range.resource = resource;

            this._postBack2('TimeRangeDoubleClick', range, data);
        };
        this.timeRangeDoubleClickCallBack = function(start, end, resource, data) {

            var range = {};
            range.start = start;
            range.end = end;
            range.resource = resource;

            this._callBack2('TimeRangeDoubleClick', range, data);
        };


        this._timeRangeDoubleClickDispatch = function(start, end, resource) {
            
            if (calendar._api2()) {

                
                var args = {};
                args.start = start;
                args.end = end;
                args.resource = resource;
                
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onTimeRangeDoubleClick === 'function') {
                    calendar.onTimeRangeDoubleClick(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (calendar.timeRangeDoubleClickHandling) {
                    case 'PostBack':
                        calendar.timeRangeDoubleClickPostBack(start, end, resource);
                        break;
                    case 'CallBack':
                        calendar.timeRangeDoubleClickCallBack(start, end, resource);
                        break;
                }
                
                if (typeof calendar.onTimeRangeDoubleClicked === 'function') {
                    calendar.onTimeRangeDoubleClicked(args);
                }
            }
            else {
                switch (calendar.timeRangeDoubleClickHandling) {
                    case 'PostBack':
                        calendar.timeRangeDoubleClickPostBack(start, end, resource);
                        break;
                    case 'CallBack':
                        calendar.timeRangeDoubleClickCallBack(start, end, resource);
                        break;
                    case 'JavaScript':
                        calendar.onTimeRangeDoubleClick(start, end, resource);
                        break;
                }
                
            }

        };

        this.timeRangeMenuClickPostBack = function(e, command, data) {
            var params = {};
            params.selection = e;
            params.command = command;

            this._postBack2("TimeRangeMenuClick", params, data);
        };
        this.timeRangeMenuClickCallBack = function(e, command, data) {
            var params = {};
            params.selection = e;
            params.command = command;

            this._callBack2("TimeRangeMenuClick", params, data);
        };


        this._timeRangeMenuClick = function(command, e, handling) {
            switch (handling) {
                case 'PostBack':
                    calendar.timeRangeMenuClickPostBack(e, command);
                    break;
                case 'CallBack':
                    calendar.timeRangeMenuClickCallBack(e, command);
                    break;
            }
        };

        this.linkMenuClickPostBack = function(e, command, data) {
            var params = {};
            params.link = e;
            params.command = command;

            this._postBack2("LinkMenuClick", params, data);
        };

        this.linkMenuClickCallBack = function(e, command, data) {
            var params = {};
            params.link = e;
            params.command = command;

            this._callBack2("LinkMenuClick", params, data);
        };

        this._linkMenuClick = function(command, e, handling) {
            switch (handling) {
                case 'PostBack':
                    calendar.linkMenuClickPostBack(e, command);
                    break;
                case 'CallBack':
                    calendar.linkMenuClickCallBack(e, command);
                    break;
            }
        };

        this.rowMenuClickPostBack = function(e, command, data) {
            var params = {};
            params.resource = e;
            params.command = command;

            this._postBack2("RowMenuClick", params, data);
        };

        // backwards compatibility
        this.resourceHeaderMenuClickPostBack = this.rowMenuClickPostBack;
        
        this.rowMenuClickCallBack = function(e, command, data) {
            var params = {};
            params.resource = e;
            params.command = command;

            this._callBack2("RowMenuClick", params, data);
        };

        this.resourceHeaderMenuClickCallBack = this.rowMenuClickCallBack;

        this._rowMenuClick = function(command, e, handling) {
            switch (handling) {
                case 'PostBack':
                    calendar.rowMenuClickPostBack(e, command);
                    break;
                case 'CallBack':
                    calendar.rowMenuClickCallBack(e, command);
                    break;
            }
        };
        
        this._rowUpdateText = function (row, newText) {
            var r = rowtools._findTableRow(row);
            var c = r.cells[0];
            var text = c.textDiv;
            
            text.innerHTML = newText;
            row.Text = newText;
            row.html = newText;
        };

        this._rowCreateDispatch = function(row, newText) {
            if (!newText) {
                return;
            }

            var args = {};
            args.text = newText;
            args.preventDefault = function() {
                this.preventDefault.value = true;
            };

            if (typeof calendar.onRowCreate === "function") {
                calendar.onRowCreate(args);
                if (args.preventDefault.value) {
                    return;
                }
            }

            switch (calendar.rowCreateHandling) {
                case "CallBack":
                    calendar.rowCreateCallBack(args.text);
                    break;
                case "PostBack":
                    calendar.rowCreatePostBack(args.text);
                    break;

            }

            if (typeof calendar.onRowCreated === "function") {
                calendar.onRowCreated(args);
            }

        };
        
        this._rowEditDispatch = function(row, newText) {
            if (row.isNewRow) {
                calendar._rowCreateDispatch(row, newText);
                return;
            }


            var index = DayPilot.indexOf(calendar.rowlist, row);
            var e = calendar._createRowObject(row, index);
            
            if (calendar._api2()) {
                
                var args = {};
                args.resource = e;
                args.newText = newText;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onRowEdit === 'function') {
                    calendar.onRowEdit(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (calendar.rowEditHandling) {
                    case 'PostBack':
                        calendar.rowEditPostBack(e, newText);
                        break;
                    case 'CallBack':
                        calendar.rowEditCallBack(e, newText);
                        break;
                    case 'Update':
                        calendar._rowUpdateText(row, newText);
                        break;
                }   
                
                if (typeof calendar.onRowEdited === 'function') {
                    calendar.onRowEdited(args);
                }
            }
            else {
                switch (calendar.rowEditHandling) {
                    case 'PostBack':
                        calendar.rowEditPostBack(e, newText);
                        break;
                    case 'CallBack':
                        calendar.rowEditCallBack(e, newText);
                        break;
                    case 'JavaScript':
                        calendar.onrowEdit(e, newText);
                        break;
                }
            }
        };

        this.rowCreatePostBack = function(newText, data) {
            var params = {};
            params.text = newText;

            this._postBack2("RowCreate", params, data);
        };

        this.rowCreateCallBack = function(newText, data) {
            var params = {};
            params.text = newText;

            this._callBack2("RowCreate", params, data);
        };
        
        this.rowEditPostBack = function(e, newText, data) {
            var params = {};
            params.resource = e;
            params.newText = newText;

            this._postBack2("RowEdit", params, data);
        };

        this.rowEditCallBack = function(e, newText, data) {
            var params = {};
            params.resource = e;
            params.newText = newText;

            this._callBack2("RowEdit", params, data);
        };

        this.rowMovePostBack = function(source, target, position, data) {
            var params = {};
            params.source = source;
            params.target = target;
            params.position = position;

            this._postBack2("RowMove", params, data);
        };

        this.rowMoveCallBack = function(source, target, position, data) {
            var params = {};
            params.source = source;
            params.target = target;
            params.position = position;

            this._callBack2("RowMove", params, data);
        };

        this.rowMoveNotify = function(source, target, position, data) {
            var params = {};
            params.source = source;
            params.target = target;
            params.position = position;

            this._callBack2("RowMove", params, data, "Notify");
        };

        this.rowClickPostBack = function(e, data) {
            var params = {};
            params.resource = e;

            this._postBack2("RowClick", params, data);
        };
        
        // backwards compatibility
        this.resourceHeaderClickPostBack = this.rowClickPostBack;
        
        this.rowClickCallBack = function(e, data) {
            var params = {};
            params.resource = e;

            this._callBack2("RowClick", params, data);
        };
        
        // backwards compatibility
        this.resourceHeaderClickCallBack = this.rowClickCallBack;
        
        this._rowClickDispatch = function(e, ctrl, shift, meta) {
            
            if (calendar.rowDoubleClickHandling === "Disabled") {
                calendar._rowClickSingle(e, ctrl, shift, meta);
                return;
            }

            if (!calendar.timeouts.resClick) {
                calendar.timeouts.resClick = [];
            }

            var resClickDelayed = function(e, ctrl, shift, meta) {
                return function() {
                    calendar._rowClickSingle(e, ctrl, shift, meta);
                };
            };

            calendar.timeouts.resClick.push(window.setTimeout(resClickDelayed(e, ctrl, shift, meta), calendar.doubleClickTimeout));
        };

        this._rowClickSingle = function(e, ctrl, shift, meta) {
            
            // backwards compatibility
            var rowClickHandling = calendar.resourceHeaderClickHandling || calendar.rowClickHandling;
            
            if (calendar._api2()) {
                
                var args = {};
                args.resource = e;
                args.ctrl = ctrl;
                args.shift = shift;
                args.meta = meta;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onRowClick === 'function') {
                    calendar.onRowClick(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }
                
                // backwards compatiblity
                if (typeof calendar.onResourceHeaderClick === 'function') {
                    calendar.onResourceHeaderClick(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }
                
                switch (rowClickHandling) {
                    case 'PostBack':
                        calendar.rowClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.rowClickCallBack(e);
                        break;
                    case 'Select':
                        calendar._rowSelectDispatch(e.$.row, ctrl, shift, meta);
                        break;
                    case 'Edit':
                        calendar._rowtools.edit(e.$.row);
                        break;
                }
                
                if (typeof calendar.onRowClicked === 'function') {
                    calendar.onRowClicked(args);
                }  
                
                if (typeof calendar.onResourceHeaderClicked === 'function') {
                    calendar.onResourceHeaderClicked(args);
                } 

            }
            else {
      
                switch (rowClickHandling) {
                    case 'PostBack':
                        calendar.rowClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.rowClickCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onRowClick(e);
                        break;
                    case 'Select':
                        calendar._rowSelectDispatch(e.$.row, ctrl, shift);
                        break;
                    case 'Edit':
                        calendar._rowtools.edit(e.$.row);
                        break;
                }
            }
        };
        //

        this.timeHeaderClickPostBack = function(e, data) {
            var params = {};
            params.header = e;

            this._postBack2("TimeHeaderClick", params, data);
        };

        this.timeHeaderClickCallBack = function(e, data) {
            var params = {};
            params.header = e;

            this._callBack2("TimeHeaderClick", params, data);
        };

        this._timeHeaderClickDispatch = function(e) {
            if (calendar._api2()) {
                
                var args = {};
                args.header = e;
                /*
                 * start
                 * end
                 * level
                 * 
                 */
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onTimeHeaderClick === 'function') {
                    calendar.onTimeHeaderClick(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }
                
                switch (this.timeHeaderClickHandling) {
                    case 'PostBack':
                        calendar.timeHeaderClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.timeHeaderClickCallBack(e);
                        break;
                }     
                
                if (typeof calendar.onTimeHeaderClicked === 'function') {
                    calendar.onTimeHeaderClicked(args);
                }                
            }
            else {
                switch (this.timeHeaderClickHandling) {
                    case 'PostBack':
                        calendar.timeHeaderClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.timeHeaderClickCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onTimeHeaderClick(e);
                        break;
                }
            }
        };

        //        
        this.resourceCollapsePostBack = function(e, data) {
            var params = {};
            params.resource = e;

            this._postBack2("ResourceCollapse", params, data);
        };
        this.resourceCollapseCallBack = function(e, data) {
            var params = {};
            params.resource = e;

            this._callBack2("ResourceCollapse", params, data);
        };

        this._resourceCollapseDispatch = function(e) {
            
            if (calendar._api2()) {
                
                var args = {};
                args.resource = e;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onResourceCollapse === 'function') {
                    calendar.onResourceCollapse(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }
                
                switch (this.resourceCollapseHandling) {
                    case 'PostBack':
                        calendar.resourceCollapsePostBack(e);
                        break;
                    case 'CallBack':
                        calendar.resourceCollapseCallBack(e);
                        break;
                }                
            }
            else {
                switch (this.resourceCollapseHandling) {
                    case 'PostBack':
                        calendar.resourceCollapsePostBack(e);
                        break;
                    case 'CallBack':
                        calendar.resourceCollapseCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onResourceCollapse(e);
                        break;
                }
            }
            
        };

        // expand
        this.resourceExpandPostBack = function(e, data) {
            var params = {};
            params.resource = e;

            this._postBack2("ResourceExpand", params, data);
        };
        this.resourceExpandCallBack = function(e, data) {
            var params = {};
            params.resource = e;

            this._callBack2("ResourceExpand", params, data);
        };

        this._resourceExpandDispatch = function(e) {
            
            if (calendar._api2()) {

                var args = {};
                args.resource = e;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onResourceExpand === 'function') {
                    calendar.onResourceExpand(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (this.resourceExpandHandling) {
                    case 'PostBack':
                        calendar.resourceExpandPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.resourceExpandCallBack(e);
                        break;
                }

            }
            else {
                switch (this.resourceExpandHandling) {
                    case 'PostBack':
                        calendar.resourceExpandPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.resourceExpandCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onResourceExpand(e);
                        break;
                }
                
            }
            
        };

        this.eventEditPostBack = function(e, newText, data) {
            var params = {};
            params.e = e;
            params.newText = newText;

            this._postBack2("EventEdit", params, data);
        };
        this.eventEditCallBack = function(e, newText, data) {

            var params = {};
            params.e = e;
            params.newText = newText;

            this._callBack2("EventEdit", params, data);
        };

        this._eventEditDispatch = function(e, newText) {
            if (calendar._api2()) {
                
                var args = {};
                args.e = e;
                args.newText = newText;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onEventEdit === 'function') {
                    calendar.onEventEdit(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (calendar.eventEditHandling) {
                    case 'PostBack':
                        calendar.eventEditPostBack(e, newText);
                        break;
                    case 'CallBack':
                        calendar.eventEditCallBack(e, newText);
                        break;
                    case 'Update':
                        e.text(newText);
                        calendar.events.update(e);
                        break;
                }   
                
                if (typeof calendar.onEventEdited === 'function') {
                    calendar.onEventEdited(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }
            }
            else {
                switch (calendar.eventEditHandling) {
                    case 'PostBack':
                        calendar.eventEditPostBack(e, newText);
                        break;
                    case 'CallBack':
                        calendar.eventEditCallBack(e, newText);
                        break;
                    case 'JavaScript':
                        calendar.onEventEdit(e, newText);
                        break;
                }
            }
        };

        this.commandCallBack = function(command, data) {
            this._invokeCommand("CallBack", command, data);
        };

        this.commandPostBack = function(command, data) {
            this._invokeCommand("PostBack", command, data);
        };

        this._invokeCommand = function(type, command, data) {
            var params = {};
            params.command = command;

            this._invokeEvent(type, "Command", params, data);
        };


        this._postBack2 = function(action, parameters, data) {
            var envelope = {};
            envelope.action = action;
            envelope.type = "PostBack";
            envelope.parameters = parameters;
            envelope.data = data;
            envelope.header = this._getCallBackHeader();

            var commandstring = "JSON" + DayPilot.JSON.stringify(envelope);
            __doPostBack(calendar.uniqueID, commandstring);
        };

        this._callBack2 = function(action, parameters, data, type) {
            
            if (!calendar._serverBased()) {
                calendar.debug.message("Callback invoked without the server-side backend specified. Callback canceled.", "warning");
                return;
            }

            if (typeof type === 'undefined') {
                type = "CallBack";
            }

            this._pauseAutoRefresh();

            calendar._loadingStart();

            var envelope = {};

            envelope.action = action;
            envelope.type = type;
            envelope.parameters = parameters;
            envelope.data = data;
            envelope.header = this._getCallBackHeader();

            var json = DayPilot.JSON.stringify(envelope);

            var commandstring;
            if (typeof Iuppiter !== 'undefined' && Iuppiter.compress) {
                commandstring = "LZJB" + Iuppiter.Base64.encode(Iuppiter.compress(json));
            }
            else {
                commandstring = "JSON" + json;
            }

            this._doCallBackStart(envelope);

            var context = null;
            if (this.backendUrl) {
                DayPilot.request(this.backendUrl, this._callBackResponse, commandstring, this._ajaxError);
            }
            else if (typeof WebForm_DoCallback === 'function') {
                WebForm_DoCallback(this.uniqueID, commandstring, this._updateView, context, this.callbackError, true);
            }
        };


        this._doCallBackStart = function(envelope) {
            var args = {};
            if (typeof calendar.onCallBackStart === 'function') {
                calendar.onCallBackStart(args);
            }
        };

        this._doCallBackEnd = function() {
            var args = {};
            if (typeof calendar.onCallBackEnd === 'function') {
                setTimeout(function() {
                    calendar.onCallBackEnd(args);
                }, 0);
            }
        };
        
        this._serverBased = function() {
            if (this.backendUrl) {  // ASP.NET MVC, Java
                return true;
            }
            if (this._isAspnetWebForms()) {
                return true;
            }
            return false;
        };

        this._isAspnetWebForms = function() {
            if (typeof WebForm_DoCallback === 'function' && this.uniqueID) {
                return true;
            }
            return false;
        };
        
        this._ajaxError = function(req) {
            if (typeof calendar.onAjaxError === 'function') {
                var args = {};
                args.request = req;
                calendar.onAjaxError(args);
            }
            else if (typeof calendar.ajaxError === 'function') { // backwards compatibility
                calendar.ajaxError(req);
            }
        };      

        this._callBackResponse = function(response) {
            calendar._updateView(response.responseText);
        };

        this._getCallBackHeader = function() {
            var h = {};

            h.v = this.v;
            h.control = "dps";
            h.id = this.id;

            // callback-changeable state
            h.startDate = calendar.startDate;
            h.days = calendar.days;
            h.cellDuration = calendar.cellDuration;
            h.cellGroupBy = calendar.cellGroupBy;
            h.cellWidth = calendar.cellWidth;
            h.cellWidthSpec = calendar.cellWidthSpec;

            // extra properties
            h.viewType = calendar.viewType; // serialize
            h.hourNameBackColor = calendar.hourNameBackColor;
            h.showNonBusiness = calendar.showNonBusiness;
            h.businessBeginsHour = calendar.businessBeginsHour;
            h.businessEndsHour = calendar.businessEndsHour;
            h.weekStarts = calendar.weekStarts;
            h.treeEnabled = calendar.treeEnabled;
            h.backColor = calendar.cellBackColor;
            h.nonBusinessBackColor = calendar.cellBackColorNonBusiness;
            h.locale = calendar.locale;
            h.timeZone = calendar.timeZone;
            h.tagFields = calendar.tagFields;
            h.timeHeaders = calendar.timeHeaders;
            h.cssOnly = calendar.cssOnly;
            h.cssClassPrefix = calendar.cssClassPrefix;
            h.durationBarMode = calendar.durationBarMode;
            h.showBaseTimeHeader = true; // to be removed
            h.rowHeaderColumns = calendar.rowHeaderColumns;
            h.rowMarginBottom = calendar.rowMarginBottom;
            h.rowMarginTop = calendar.rowMarginTop;
            h.rowMinHeight = calendar.rowMinHeight;
            h.scale = calendar.scale;

            // custom state
            h.clientState = calendar.clientState;

            // user-changeable state
            if (this.nav.scroll) {
                h.scrollX = this.nav.scroll.scrollLeft;
                h.scrollY = this.nav.scroll.scrollTop;
            }

            h.selected = calendar.multiselect.events();
            h.selectedRows = rowtools._getSelectedList();

            // special
            h.hashes = calendar.hashes;

            var area = calendar._getArea(h.scrollX, h.scrollY);
            var range = calendar._getAreaRange(area);
            var res = calendar._getAreaResources(area);

            //h.scrollX = calendar.scrollX;
            //h.scrollY = calendar.scrollY;

            h.rangeStart = range.start;
            h.rangeEnd = range.end;
            h.resources = res;
            h.dynamicLoading = calendar.dynamicLoading;

            if (this.syncResourceTree) {
                h.tree = this._getTreeState();
            }
            if (this.syncLinks) {
                h.links = this._getLinksState();
            }

            if (typeof calendar.onCallBackHeader === "function") {
                var args = {};
                args.header = h;
                calendar.onCallBackHeader(args);
            }

            return h;
        };

        this._getLinksState = function() {
            var list = [];

            var getTags = function(link) {
                var result = {};
                if (link.tags) {
                    for (var name in link.tags) {
                        result[name] = "" + link.tags[name];
                    }
                }
                return result;
            };

            for (var i = 0; i < calendar.links.length; i++) {
                var link = calendar.links[i];
                var json = {};
                json.id = link.id;
                json.from = link.from;
                json.to = link.to;
                json.type = link.type;
                json.tags = getTags(link);
                list.push(json);
            }
            return list;
        };
        
        this.getViewPort = function() {
            var scrollX = this.nav.scroll.scrollLeft;
            var scrollY = this.nav.scroll.scrollTop;
            
            var area = this._getArea(scrollX, scrollY);
            var range = this._getAreaRange(area);
            var resources = this._getAreaResources(area);
            
            var result = {};
            result.start = range.start;
            result.end = range.end;
            result.resources = resources;
            
            return result;
        };

        this._getArea = function(scrollX, scrollY) {
            var area = {};
            area.start = {};
            area.end = {};

            area.start.x = Math.floor(scrollX / calendar.cellWidth);
            area.end.x = Math.floor((scrollX + calendar.nav.scroll.clientWidth) / calendar.cellWidth);

            area.start.y = calendar._getRow(scrollY).i;
            area.end.y = calendar._getRow(scrollY + calendar.nav.scroll.clientHeight).i;

            var maxX = this.itline.length;
            if (area.end.x >= maxX) {
                area.end.x = maxX - 1;
            }

            return area;
        };

        this._getAreaRange = function(area) {
            var result = {};

            if (this.itline.length <= 0) {
                result.start = this.startDate;
                result.end = this.startDate;
                return result;
            }

            if (!this.itline[area.start.x]) {
                throw 'Internal error: area.start.x is null.';
            }
            result.start = this.itline[area.start.x].start;
            result.end = this.itline[area.end.x].end;

            return result;
        };

        this._getAreaResources = function(area) {
            // this might not be necessary, ported from DPSD
            if (!area) {
                var area = this._getArea(this.nav.scroll.scrollLeft, this.nav.scroll.scrollTop);
            }

            var res = [];
            res.ignoreToJSON = true;  // preventing Gaia and prototype to mess up with Array serialization

            for (var i = area.start.y; i <= area.end.y; i++) {
                var r = calendar.rowlist[i];
                if (r && !r.hidden) {
                    res.push(r.id);
                }
            }
            return res;
        };


        this._getTreeState = function() {
            var tree = [];
            tree.ignoreToJSON = true; // preventing Gaia and prototype to mess up with Array serialization

            for (var i = 0; i < this.rowlist.length; i++) {
                var row = this.rowlist[i];
                if (row.level > 0) {
                    continue;
                }

                if (row.isNewRow) {
                    continue;
                }

                var node = this._getNodeState(i);
                tree.push(node);
            }
            return tree;
        };

        this._getNodeChildren = function(indices) {
            var children = [];
            children.ignoreToJSON = true; // preventing Gaia to mess up with Array serialization
            for (var i = 0; i < indices.length; i++) {
                var index = indices[i];
                var row = calendar.rowlist[index];
                if (row.isNewRow) {
                    continue;
                }
                children.push(calendar._getNodeState(index));
            }
            return children;
        };

        this._getNodeState = function(i) {
            var row = this.rowlist[i];

            if (typeof calendar.onGetNodeState === "function") {
                var args = {};
                args.row = row;
                args.preventDefault = function() {
                    args.preventDefault.value = true;
                };
                args.result = {};

                calendar.onGetNodeState(args);

                if (args.preventDefault.value) {
                    return args.result;
                }
            }

            var node = {};
            node.Value = row.id;
            node.BackColor = row.backColor;
            node.Name = row.name;
            node.InnerHTML = row.html;
            node.ToolTip = row.toolTip;
            node.Expanded = row.expanded;
            node.Children = this._getNodeChildren(row.children);
            node.Loaded = row.loaded;
            node.IsParent = row.isParent;
            node.Columns = this._getNodeColumns(row);
            if (row.minHeight !== calendar.rowMinHeight) {
                node.MinHeight = row.minHeight;
            }
            if (row.marginBottom !== calendar.rowMarginBottom) {
                node.MarginBottom = row.marginBottom;
            }
            if (row.marginTop !== calendar.rowMarginTop) {
                node.MarginTop = row.marginTop;
            }
            if (row.eventHeight !== calendar.eventHeight) {
                node.EventHeight = row.eventHeight;
            }

            return node;
        };

        this._getNodeColumns = function(row) {

            if (!row.columns || row.columns.length === 0) {
                return null;
            }

            var columns = [];
            columns.ignoreToJSON = true; // preventing Gaia to mess up with Array serialization

            for (var i = 0; i < row.columns.length; i++) {
                var c = {};
                c.InnerHTML = row.columns[i].html;

                columns.push(c);
            }

            return columns;
        };

/*
        this.$ = function(subid) {
            return document.getElementById(id + "_" + subid);
        };
*/
        this._prefixCssClass = function(part) {
            var prefix = this.theme || this.cssClassPrefix;
            if (prefix) {
                return prefix + part;
            }
            else {
                return "";
            }
        };

        this._registerDispose = function() {
            //var root = document.getElementById(id);
            this.nav.top.dispose = this.dispose;
        };

        this.dispose = function() {

            var c = calendar;
            
            if (!c.nav.top) {
                return;
            }

            c._pauseAutoRefresh();

            c._deleteEvents();
            c.divBreaks = null;
            c.divCells = null;
            c.divCorner = null;
            c.divCrosshair = null;
            c.divEvents = null;
            if (c.divHeader) {
                c.divHeader.rows = null;
            }
            c.divHeader = null;
            c.divLines = null;
            c.divNorth = null;
            c.divRange = null;
            c.divResScroll = null;
            c.divSeparators = null;
            c.divSeparatorsAbove = null;
            c.divStretch = null;
            c.divTimeScroll = null;
            c._scrollRes = null;
            c._vsph = null;
            c._maind.calendar = null;
            c._maind = null;

            c.nav.loading = null;

            c.nav.top.onmousemove = null;
            c.nav.top.dispose = null;
            c.nav.top.ontouchstart = null;
            c.nav.top.ontouchmove = null;
            c.nav.top.ontouchend = null;
            
            c.nav.top.removeAttribute('style');
            c.nav.top.removeAttribute('class');
            c.nav.top.innerHTML = "";
            c.nav.top.dp = null;
            c.nav.top = null;

            c.nav.scroll.onscroll = null;
            c.nav.scroll.root = null;
            c.nav.scroll = null;

            DayPilot.ue(window, 'resize', c._resize);

            DayPilotScheduler.unregister(c);
        };

        this._createShadowRange = function(object, type) {
            var maind = calendar._maind;
            var coords = calendar._getShadowCoords(object);
            var event = object.event;

            var height = event.part.height || calendar._resolved.eventHeight();
            var top = (event.part && event.part.top) ? (event.part.top + calendar.rowlist[event.part.dayIndex].top) : coords.top;

            var shadow = document.createElement('div');
            shadow.setAttribute('unselectable', 'on');
            shadow.style.position = 'absolute';
            shadow.style.width = (coords.width) + 'px';
            shadow.style.height = height + 'px';
            shadow.style.left = coords.left + 'px';
            shadow.style.top = top + 'px';
            shadow.style.zIndex = 101;
            shadow.style.overflow = 'hidden';

            var inner = document.createElement("div");
            shadow.appendChild(inner);

            if (this.cssOnly) {
                shadow.className = this._prefixCssClass("_shadow");
                inner.className = this._prefixCssClass("_shadow_inner");
            }

            if (!this.cssOnly) {
                if (type === 'Fill') { // transparent shadow        
                    inner.style.backgroundColor = "#aaaaaa";
                    inner.style.opacity = 0.5;
                    inner.style.filter = "alpha(opacity=50)";
                    inner.style.height = "100%";
                    if (object && object.event && object.style) {
                        //shadow.style.overflow = 'hidden';
                        inner.style.fontSize = object.style.fontSize;
                        inner.style.fontFamily = object.style.fontFamily;
                        inner.style.color = object.style.color;
                        inner.innerHTML = object.event.client.innerHTML();
                    }
                }
                else {
                    shadow.style.paddingTop = "2px";
                    inner.style.border = '2px dotted #666666';
                }
            }

            maind.appendChild(shadow);
            shadow.calendar = calendar;

            return shadow;
        };

        this._createShadow = function(object, type) {
            var maind = calendar._maind;
            var coords = calendar._getShadowCoords(object);
            var event = object.event;
            var ev = event;

            var height = event.part.height || calendar._resolved.eventHeight();
            var top = (event.part && event.part.top) ? (event.part.top + calendar.rowlist[event.part.dayIndex].top) : coords.top;
            var left = coords.left;

            var verticalAllowed = (ev.cache && typeof ev.cache.moveVDisabled !== 'undefined') ? !ev.cache.moveVDisabled : !ev.data.moveVDisabled;
            var horizontalAllowed = (ev.cache && typeof ev.cache.moveHDisabled !== 'undefined') ? !ev.cache.moveHDisabled :!ev.data.moveHDisabled;

            if (!verticalAllowed && DayPilotScheduler.moving) {
                top = calendar.rowlist[ev.part.dayIndex].top;
            }
            if (!horizontalAllowed && DayPilotScheduler.moving) {
                left = event.part.left;
            }

            var shadow = document.createElement('div');
            shadow.setAttribute('unselectable', 'on');
            shadow.style.position = 'absolute';
            shadow.style.width = (coords.width) + 'px';
            shadow.style.height = height + 'px';
            shadow.style.left = left + 'px';
            shadow.style.top = top + 'px';
            shadow.style.zIndex = 101;
            shadow.style.overflow = 'hidden';

            var inner = document.createElement("div");
            shadow.appendChild(inner);

            if (this.cssOnly) {
                shadow.className = this._prefixCssClass("_shadow");
                inner.className = this._prefixCssClass("_shadow_inner");
            }

            if (!this.cssOnly) {
                if (type === 'Fill') { // transparent shadow        
                    inner.style.backgroundColor = "#aaaaaa";
                    inner.style.opacity = 0.5;
                    inner.style.filter = "alpha(opacity=50)";
                    inner.style.height = "100%";
                    if (object && object.event && object.style) {
                        //shadow.style.overflow = 'hidden';
                        inner.style.fontSize = object.style.fontSize;
                        inner.style.fontFamily = object.style.fontFamily;
                        inner.style.color = object.style.color;
                        inner.innerHTML = object.event.client.innerHTML();
                    }
                }
                else {
                    shadow.style.paddingTop = "2px";
                    inner.style.border = '2px dotted #666666';
                }
            }

            maind.appendChild(shadow);
            shadow.calendar = calendar;

            return shadow;
        };

        // y is in pixels, not row index
        this._getRow = function(y) {
            var result = {};
            var element;

            var top = 0;
            var rowEnd = 0;
            var iMax = this.rowlist.length; // maximum row index

            for (var i = 0; i < iMax; i++) {
                var row = this.rowlist[i];
                if (row.hidden) {
                    continue;
                }
                rowEnd += row.height;
                if (y < rowEnd || i === iMax - 1) {
                    top = rowEnd - row.height;
                    element = row;
                    break;
                }
            }

            result.top = top;
            result.bottom = rowEnd;
            result.i = i;
            result.element = element;

            return result;
        };

        var linktools = {};

        this._linktools = linktools;

        linktools.clear = function() {
            calendar.divLinksAbove.innerHTML = '';
            calendar.divLinksBelow.innerHTML = '';
            calendar.elements.links = [];
        };

        linktools.showLinkpoints = function() {
            var events = viewport.events();
            events.each(function(div) {
                linktools.showLinkpoint(div);
            });
        };

        linktools.showLinkpoint = function(div) {
            var width = 10;
            var mid = width/2;

            var left = div.event.part.left;
            var top = calendar.rowlist[div.event.part.dayIndex].top + div.event.part.top;
            var height = div.event.part.height;
            var right = div.event.part.right;

            var start = DayPilot.Util.div(calendar.divLinkpoints, left - mid, top - mid + height/2, width, width);
            start.style.backgroundColor = "white";
            start.style.border = "1px solid gray";
            start.style.borderRadius = "5px";
            start.style.boxSizing = "border-box";
            start.coords = {x: left, y: top + height/2};
            start.type = "Start";
            start.event = div.event;
            linktools.activateLinkpoint(start);
            calendar.elements.linkpoints.push(start);

            var end = DayPilot.Util.div(calendar.divLinkpoints, right - mid, top - mid + height/2, width, width);
            end.style.backgroundColor = "white";
            end.style.border = "1px solid gray";
            end.style.borderRadius = "5px";
            end.style.boxSizing = "border-box";
            end.coords = {x: right, y: top + height/2};
            end.type = "Finish";
            end.event = div.event;
            linktools.activateLinkpoint(end);
            calendar.elements.linkpoints.push(end);
        };

        linktools.activateLinkpoint = function(div) {

            //linktools.clearHideTimeout();

            div.onmousedown = function(ev) {
                var ev = ev || window.event;
                linking.source = div;
                linking.calendar = calendar;
                linktools.showLinkpoints();
                ev.preventDefault && ev.preventDefault(); // prevent text selection cursor in chrome
                ev.stopPropagation && ev.stopPropagation();
                return false;
            };
            div.onmousemove = function(ev) {
                //if (linking.source) {
                    div.style.backgroundColor = "black";
                //}
                linktools.clearHideTimeout();
            };
            div.onmouseout = function(ev) {
                if (!linking.source || linking.source.event !== div.event) {
                    div.style.backgroundColor = "white";
                }
            };
            div.onmouseup = function(ev) {
                if (linking.source) {
                    var type = linking.source.type + "To" + div.type;
                    var from = linking.source.event.id();
                    var to = div.event.id();

                    var args = {};
                    args.from = from;
                    args.to = to;
                    args.type = type;
                    args.id = null;
                    args.preventDefault = function() {
                        this.preventDefault.value = true;
                    };

                    if (typeof calendar.onLinkCreate === "function") {
                        calendar.onLinkCreate(args);
                        if (args.preventDefault.value) {
                            return;
                        }
                    }

                    var update = function() {
                        calendar.links.push({"from": from, "to": to, "type": type});
                        linktools.load();
                    };

                    switch (calendar.linkCreateHandling) {
                        case "Update":
                            update();
                            break;
                        case "CallBack":
                            calendar._linkCreateCallBack(args);
                            break;
                        case "PostBack":
                            calendar._linkCreatePostBack(args);
                            break;
                        case "Notify":
                            update();
                            calendar._linkCreateNotify(args);
                            break;
                    }

                    if (typeof calendar.onLinkCreated === "function") {
                        calendar.onLinkCreated(args);
                    }

                }
            };
        }

        linktools.hideLinkpoints = function() {
            calendar.divLinkpoints.innerHTML = '';
            calendar.elements.linkpoints = [];
        };

        linktools.hideTimeout = null;

        linktools.hideLinkpointsWithDelay = function() {
            linktools.hideTimeout = setTimeout(function() {
                linktools.hideLinkpoints();
            }, 100);
        };

        linktools.clearHideTimeout = function() {
            if (linktools.hideTimeout) {
                clearTimeout(linktools.hideTimeout);
                linktools.hideTimeout = null;
            }
        };

        linktools.load = function() {
            linktools.clear();
            if (!DayPilot.isArray(calendar.links)) {
                return;
            }
            for (var i = 0; i < calendar.links.length; i++) {
                var link = calendar.links[i];
                linktools.drawLinkId(link.from, link.to, link);
            }
        };

        linktools.drawLinkId = function(from, to, props) {
            var start = calendar.events.find(from);
            var end = calendar.events.find(to);
            linktools.drawLink(start, end, props);
        };

        linktools.drawLink = function(from, to, props) {
            var divFrom = calendar._findEventDiv(from);
            var divTo = calendar._findEventDiv(to);

            if (!divFrom) {
                return;
            }

            if (!divTo) {
                return;
            }

            var type = props.type || "FinishToStart";

            var start, end;
            var fromRowTop = calendar.rowlist[divFrom.event.part.dayIndex].top;
            var toRowTop = calendar.rowlist[divTo.event.part.dayIndex].top;
            switch (type) {
                case "FinishToStart":
                    start = {x: divFrom.event.part.right, y: fromRowTop + divFrom.event.part.top};
                    end = {x: divTo.event.part.left, y: toRowTop + divTo.event.part.top};
                    break;
                case "StartToFinish":
                    start = {x: divFrom.event.part.left, y: fromRowTop + divFrom.event.part.top};
                    end = {x: divTo.event.part.right, y: toRowTop + divTo.event.part.top};
                    break;
                case "StartToStart":
                    start = {x: divFrom.event.part.left, y: fromRowTop + divFrom.event.part.top};
                    end = {x: divTo.event.part.left, y: toRowTop + divTo.event.part.top};
                    break;
                case "FinishToFinish":
                    start = {x: divFrom.event.part.right, y: fromRowTop + divFrom.event.part.top};
                    end = {x: divTo.event.part.right, y: toRowTop + divTo.event.part.top};
                    break;
            }
            linktools.drawLinkXy(start, end, props);
        };

        linktools.clearShadow = function() {
            calendar.divLinkShadow.innerHTML = '';
            calendar.elements.linkshadow = [];
        };

        linktools.drawShadow = function(from, to) {
            linktools.clearShadow();

            if (DayPilot.browser.ielt9) {
                linktools.drawShadowOldStyle(from, to);
            }
            else {
                var parent = calendar.divLinkShadow;
                var line = DayPilot.line(from.x, from.y, to.x, to.y, true);
                parent.appendChild(line);
                calendar.elements.linkshadow.push(line);
            }
        };

        linktools.drawShadowOldStyle = function(from, to) {
            var nx = Math.min(from.x, to.x);
            var width = 2;
            var parent = calendar.divLinkShadow;

            var color = "black";

            var h1 = DayPilot.Util.div(parent, nx, from.y, from.x - nx, width);
            h1.style.backgroundColor = color;
            calendar.elements.linkshadow.push(h1);

            var v1 = DayPilot.Util.div(parent, nx, from.y, width, to.y - from.y);
            v1.style.backgroundColor = color;
            calendar.elements.linkshadow.push(v1);

            var h3 = DayPilot.Util.div(parent, nx, to.y, to.x - nx, width);
            h3.style.backgroundColor = color;
            calendar.elements.linkshadow.push(h3);

            var a = DayPilot.Util.div(parent, to.x - 6, to.y - 5, 0, 0);
            a.style.borderColor = "transparent transparent transparent " + color;
            a.style.borderStyle = "solid";
            a.style.borderWidth = "6px";
            calendar.elements.linkshadow.push(a);
        };

        linktools.drawLinkXy = function(from, to, props) {
            var indent = calendar.eventHeight/2;

            var width = props.width || 1;
            var type = props.type || "FinishToStart";
            var color = props.color;
            var style = props.style;
            var layer = props.layer || "Above";
            var above = layer === "Above";
            var divLinks = above ? calendar.divLinksAbove : calendar.divLinksBelow;
            var height = calendar.eventHeight;
            var bottom = calendar.linkBottomMargin;
            //var border = width + "px " + style + " " + color;

            var applyBorder = function(div, which) {
                if (color) {
                    div.style["border" + which + "Color"] = color;
                }
                if (style) {
                    div.style["border" + which + "Style"] = style;
                }
            };

            var divs = [];

            var saveDiv = function(div, dontHover) {
                calendar.elements.links.push(div);
                activateHover(div);
                activateContextMenu(div);

                div.divs = divs;
                // required for hover
                if (!dontHover) {
                    divs.push(div);
                }
            };

            var activateContextMenu = function(div) {
                div.oncontextmenu = function(ev) {
                    ev = ev || window.event;

                    if (calendar.contextMenuLink) {
                        var link = new DayPilot.Link(props, calendar);
                        calendar.contextMenuLink.show(link);
                    }

                    ev.cancelBubble = true;
                    ev.preventDefault ? ev.preventDefault() : null;
                }
            };

            var activateHover = function(div) {
                div.onmouseenter = function() {
                    DayPilot.Util.addClass(div.divs, calendar._prefixCssClass("_link_hover"));
                };
                div.onmouseleave = function() {
                    DayPilot.Util.removeClass(div.divs, calendar._prefixCssClass("_link_hover"));
                };
            };

            if (type === "FinishToStart") {
                if (from.x > to.x) {
                    var above = 5;
                    var midy = to.y - above;

                    var h1 = DayPilot.Util.div(divLinks, from.x, from.y + height - bottom, indent + width, width);
                    h1.style.boxSizing = "border-box";
                    h1.style.borderBottomWidth = width + "px";
                    h1.className = calendar._prefixCssClass("_link_horizontal");
                    DayPilot.Util.addClass(h1, props.cssClass);
                    applyBorder(h1, "Bottom");
                    saveDiv(h1);

                    var v1 = DayPilot.Util.div(divLinks, from.x + indent, from.y + height - bottom, width, midy - (from.y + height - bottom));
                    v1.style.boxSizing = "border-box";
                    v1.style.borderRightWidth = width + "px";
                    v1.className = calendar._prefixCssClass("_link_vertical");
                    DayPilot.Util.addClass(v1, props.cssClass);
                    applyBorder(v1, "Right");
                    saveDiv(v1);

                    var h2 = DayPilot.Util.div(divLinks, to.x - indent, midy, from.x + 2*indent + width - to.x, width);
                    h2.style.boxSizing = "border-box";
                    h2.style.borderBottomWidth = width + "px";
                    h2.className = calendar._prefixCssClass("_link_horizontal");
                    DayPilot.Util.addClass(h2, props.cssClass);
                    applyBorder(h2, "Bottom");
                    saveDiv(h2);

                    var v2 = DayPilot.Util.div(divLinks, to.x - indent, midy, width, to.y - midy + height - bottom);
                    v2.style.boxSizing = "border-box";
                    v2.style.borderRightWidth = width + "px";
                    v2.className = calendar._prefixCssClass("_link_vertical");
                    DayPilot.Util.addClass(v2, props.cssClass);
                    applyBorder(v2, "Right");
                    saveDiv(v2);

                    var h3 = DayPilot.Util.div(divLinks, to.x - indent, to.y + height - bottom, indent, width);
                    h3.style.boxSizing = "border-box";
                    h3.style.borderBottomWidth = width + "px";
                    h3.className = calendar._prefixCssClass("_link_horizontal");
                    DayPilot.Util.addClass(h3, props.cssClass);
                    applyBorder(h3, "Bottom");
                    saveDiv(h3);

                    var a;
                    if (color) {
                        a = DayPilot.Util.div(divLinks, to.x - 6, to.y + height - bottom - 5, 0, 0);
                        a.style.borderWidth = "6px";
                        a.style.borderColor = "transparent transparent transparent " + color;
                        a.style.borderStyle = "solid";
                    }
                    else {
                        a = DayPilot.Util.div(divLinks, to.x - 6, to.y + height - bottom - 5, 6, 6);
                        a.className = calendar._prefixCssClass("_link_arrow_right");
                        DayPilot.Util.addClass(a, props.cssClass);
                    }
                    saveDiv(a, true);
                }
                else {
                    var startx = from.x;
                    var starty = from.y + height - bottom;

                    var tox = to.x + indent;
                    var toy = to.y;

                    var h1 = DayPilot.Util.div(divLinks, startx, starty, tox - from.x, width);
                    h1.style.boxSizing = "border-box";
                    h1.style.borderBottomWidth = width + "px";
                    h1.className = calendar._prefixCssClass("_link_horizontal");
                    DayPilot.Util.addClass(h1, props.cssClass);
                    applyBorder(h1, "Bottom");
                    saveDiv(h1);

                    var v1 = DayPilot.Util.div(divLinks, tox, starty, width, toy - starty);
                    v1.style.boxSizing = "border-box";
                    v1.style.borderRightWidth = width + "px";
                    v1.className = calendar._prefixCssClass("_link_vertical");
                    DayPilot.Util.addClass(v1, props.cssClass);
                    applyBorder(v1, "Right");
                    saveDiv(v1);

                    var a;
                    if (color) {
                        a = DayPilot.Util.div(divLinks, tox - 5 + Math.floor(width/2), toy - 5, 0, 0);
                        a.style.borderColor = color + " transparent transparent transparent";
                        a.style.borderStyle = "solid";
                        a.style.borderWidth = "5px";
                    }
                    else {
                        a = DayPilot.Util.div(divLinks, tox - 6 + Math.floor(width/2), toy - 6, 6, 6);
                        a.className = calendar._prefixCssClass("_link_arrow_down");
                        DayPilot.Util.addClass(a, props.cssClass);
                    }
                    saveDiv(a, true);
                }
            }
            else if (type === "StartToFinish") {
                var above = 5;
                var midy = to.y - above;
                //var midy = DayPilot.Util.avg(from.y, to.y);

                var h1 = DayPilot.Util.div(divLinks, to.x, to.y + height - bottom, indent + width, width);
                h1.style.boxSizing = "border-box";
                h1.style.borderBottomWidth = width + "px";
                h1.className = calendar._prefixCssClass("_link_horizontal");
                DayPilot.Util.addClass(h1, props.cssClass);
                applyBorder(h1, "Bottom");
                saveDiv(h1);

                var v1 = DayPilot.Util.div(divLinks, to.x + indent, to.y + height - bottom, width, midy - (to.y + height - bottom) + 0);
                v1.style.boxSizing = "border-box";
                v1.style.borderRightWidth = width + "px";
                v1.className = calendar._prefixCssClass("_link_vertical");
                DayPilot.Util.addClass(v1, props.cssClass);
                applyBorder(v1, "Right");
                saveDiv(v1);

                var h2 = DayPilot.Util.div(divLinks, from.x - indent, midy, to.x + 2*indent + width - from.x, width);
                h2.style.boxSizing = "border-box";
                h2.style.borderBottomWidth = width + "px";
                h2.className = calendar._prefixCssClass("_link_horizontal");
                DayPilot.Util.addClass(h2, props.cssClass);
                applyBorder(h2, "Bottom");
                saveDiv(h2);

                var v2 = DayPilot.Util.div(divLinks, from.x - indent, midy, width, from.y + height - bottom - midy + 0);
                v2.style.boxSizing = "border-box";
                v2.style.borderRightWidth = width + "px";
                v2.className = calendar._prefixCssClass("_link_vertical");
                DayPilot.Util.addClass(v2, props.cssClass);
                applyBorder(v2, "Right");
                saveDiv(v2);

                var h3 = DayPilot.Util.div(divLinks, from.x - indent, from.y + height - bottom, indent, width);
                h3.style.boxSizing = "border-box";
                h3.style.borderBottomWidth = width + "px";
                h3.className = calendar._prefixCssClass("_link_horizontal");
                DayPilot.Util.addClass(h3, props.cssClass);
                applyBorder(h3, "Bottom");
                saveDiv(h3);

                var a;
                if (color) {
                    a = DayPilot.Util.div(divLinks, to.x - 6, to.y + height - bottom - 5, 0, 0);
                    a.style.borderColor = "transparent " + color + " transparent transparent";
                    a.style.borderStyle = "solid";
                    a.style.borderWidth = "6px";
                }
                else {
                    a = DayPilot.Util.div(divLinks, to.x - 6, to.y + height - bottom - 5, 6, 6);
                    a.className = calendar._prefixCssClass("_link_arrow_left");
                    DayPilot.Util.addClass(a, props.cssClass);
                }
                saveDiv(a, true);
            }
            else if (type === "StartToStart") {
                var nx = Math.min(from.x, to.x) - indent;

                var h1 = DayPilot.Util.div(divLinks, nx, from.y + height - bottom, from.x - nx, width);
                h1.style.boxSizing = "border-box";
                h1.style.borderBottomWidth = width + "px";
                h1.className = calendar._prefixCssClass("_link_horizontal");
                DayPilot.Util.addClass(h1, props.cssClass);
                applyBorder(h1, "Bottom");
                saveDiv(h1);

                var v1 = DayPilot.Util.div(divLinks, nx, from.y + height - bottom, width, to.y - from.y);
                v1.style.boxSizing = "border-box";
                v1.style.borderRightWidth = width + "px";
                v1.className = calendar._prefixCssClass("_link_vertical");
                DayPilot.Util.addClass(v1, props.cssClass);
                applyBorder(v1, "Right");
                saveDiv(v1);

                var h3 = DayPilot.Util.div(divLinks, nx, to.y + height - bottom, to.x - nx, width);
                h3.style.boxSizing = "border-box";
                h3.style.borderBottomWidth = width + "px";
                h3.className = calendar._prefixCssClass("_link_horizontal");
                DayPilot.Util.addClass(h3, props.cssClass);
                applyBorder(h3, "Bottom");
                saveDiv(h3);

                var a;
                if (color) {
                    a = DayPilot.Util.div(divLinks, to.x - 6, to.y  + height - bottom - 5, 0, 0);
                    a.style.borderColor = "transparent transparent transparent " + color;
                    a.style.borderStyle = "solid";
                    a.style.borderWidth = "6px";
                }
                else {
                    a = DayPilot.Util.div(divLinks, to.x - 6, to.y  + height - bottom - 5, 6, 6);
                    a.className = calendar._prefixCssClass("_link_arrow_right");
                    DayPilot.Util.addClass(a, props.cssClass);
                }
                saveDiv(a, true);

            }
            else if (type === "FinishToFinish") {
                //var midy = DayPilot.Util.avg(from.y, to.y);
                var fx = Math.max(to.x, from.x) + indent;

                var h1 = DayPilot.Util.div(divLinks, from.x, from.y + height - bottom, fx - from.x, width);
                h1.style.boxSizing = "border-box";
                h1.style.borderBottomWidth = width + "px";
                h1.className = calendar._prefixCssClass("_link_horizontal");
                DayPilot.Util.addClass(h1, props.cssClass);
                applyBorder(h1, "Bottom");
                saveDiv(h1);

                var v1 = DayPilot.Util.div(divLinks, fx, from.y + height - bottom, width, to.y - from.y);
                v1.style.boxSizing = "border-box";
                v1.style.borderRightWidth = width + "px";
                v1.className = calendar._prefixCssClass("_link_vertical");
                DayPilot.Util.addClass(v1, props.cssClass);
                applyBorder(v1, "Right");
                saveDiv(v1);

                var h3 = DayPilot.Util.div(divLinks, to.x, to.y + height - bottom, fx - to.x, width);
                h3.style.boxSizing = "border-box";
                h3.style.borderBottomWidth = width + "px";
                h3.className = calendar._prefixCssClass("_link_horizontal");
                DayPilot.Util.addClass(h3, props.cssClass);
                applyBorder(h3, "Bottom");
                saveDiv(h3);

                var a;
                if (color) {
                    a = DayPilot.Util.div(divLinks, to.x - 6, to.y + height - bottom - 5, 0, 0);
                    a.style.borderColor = "transparent " + color + " transparent transparent";
                    a.style.borderStyle = "solid";
                    a.style.borderWidth = "6px";
                }
                else {
                    a = DayPilot.Util.div(divLinks, to.x - 6, to.y + height - bottom - 5, 6, 6);
                    a.className = calendar._prefixCssClass("_link_arrow_left");
                    DayPilot.Util.addClass(a, props.cssClass);
                }
                saveDiv(a, true);
            }
        };

        this._linkCreateCallBack = function(args, data) {
            var params = {};
            params.from = args.from;
            params.to = args.to;
            params.type = args.type;

            calendar._callBack2("LinkCreate", params, data);
        };

        this._linkCreateNotify = function(args, data) {
            var params = {};
            params.from = args.from;
            params.to = args.to;
            params.type = args.type;

            calendar._callBack2("LinkCreate", params, data, "Notify");
        };

        this._linkCreatePostBack = function(args, data) {
            var params = {};
            params.from = args.from;
            params.to = args.to;
            params.type = args.type;

            calendar._postBack2("LinkCreate", params, data);
        };

        this._getRowByIndex = function(i) {
            var top = 0;
            var bottom = 0;
            var index = 0; // visible index

            if (i > this.rowlist.length - 1) {
                throw "Row index too high (DayPilotScheduler._getRowByIndex)";
            }

            for (var j = 0; j <= i; j++) {
                var row = this.rowlist[j];

                if (row.hidden) {
                    continue;
                }

                bottom += row.height;
                index++;
            }

            top = bottom - row.height;

            var result = {};
            result.top = top;
            result.height = row.height;
            result.bottom = bottom;
            result.i = index - 1;
            result.data = row;

            return result;

        };

        this._isShortInit = function() {
            // make sure it has a place to ask
            if (this.backendUrl) {
                return (typeof calendar.events.list === 'undefined') || (!calendar.events.list);
            }
            else {
                return false;
            }
        };

        this.events.find = function(id) {
            if (!calendar.events.list || typeof calendar.events.list.length === 'undefined') {
                return null;
            }
            var len = calendar.events.list.length;
            for (var i = 0; i < len; i++) {
                if (calendar.events.list[i].id === id) {
                    return new DayPilot.Event(calendar.events.list[i], calendar);
                }
            }
            return null;
        };

        this.events.all = function() {
            var list = [];
            for (var i = 0; i < calendar.events.list.length; i++) {
                var e = new DayPilot.Event(calendar.events.list[i], calendar);
                list.push(e);
            }
            return DayPilot.list(list);
        };

        this.events.filter = function(args) {
            calendar.events._filterParams = args;
            calendar._update({"eventsOnly":true});
        };
        
        this.events.load = function(url, success, error) {
            var onError = function(args) {
                var largs = {};
                largs.exception = args.exception;
                largs.request = args.request;

                if (typeof error === 'function') {
                    error(largs);
                }
            };

            var onSuccess = function(args) {
                var r = args.request;
                var data;

                // it's supposed to be JSON
                try {
                    data = eval("(" + r.responseText + ")");
                }
                catch (e) {
                    var fargs = {};
                    fargs.exception = e;
                    onError(fargs);
                    return;
                }

                if (DayPilot.isArray(data)) {
                    var sargs = {};
                    sargs.preventDefault = function() {
                        this.preventDefault.value = true;
                    };
                    sargs.data = data;
                    if (typeof success === "function") {
                        success(sargs);
                    }

                    if (sargs.preventDefault.value) {
                        return;
                    }

                    calendar.events.list = data;
                    if (calendar._initialized) {
                        calendar.update();
                    }
                }
            };

            DayPilot.ajax({
                "url": url,
                "success": onSuccess,
                "error": onError
            });
        };

        this.events.findRecurrent = function(masterId, time) {
            if (!calendar.events.list || typeof calendar.events.list.length === 'undefined') {
                return null;
            }

            var len = calendar.events.list.length;
            for (var i = 0; i < len; i++) {
                if (calendar.events.list[i].recurrentMasterId === masterId && calendar.events.list[i].start.getTime() === time.getTime()) {
                    return new DayPilot.Event(calendar.events.list[i], calendar);
                }
            }
            return null;
        };

        // internal
        this.events._removeFromRows = function(data) {
            var rows = [];
            for (var i = 0; i < calendar.rowlist.length; i++) {
                var row = calendar.rowlist[i];
                calendar._ensureRowData(i);
                for (var r = 0; r < row.events.length; r++) {
                    if (row.events[r].data === data) {
                        //data.rendered = false;
                        rows.push(i);
                        row.events.splice(r, 1);
                        break; // only once per row
                    }
                }
            }
            return rows;
        };

        // internal
        // fast, use instead of full loadEvents()
        this.events._addToRows = function(data) {
            var rows = [];
            var testAll = calendar._containsDuplicateResources() || calendar.viewType === "Days";

            var index = DayPilot.indexOf(calendar.events.list, data);
            calendar._doBeforeEventRender(index);

            for (var i = 0; i < calendar.rowlist.length; i++) {
                var row = calendar.rowlist[i];
                calendar._ensureRowData(i);
                var ep = calendar._loadEvent(data, row);
                if (ep) {
                    if (typeof calendar.onBeforeEventRender === 'function') {
                        ep.cache = calendar._cache.events[index];
                    }

                    rows.push(i);
                    if (!testAll) {
                        break;
                    }
                }
            }
            return rows;
        };


        this.events.update = function(e, data) {
            var params = {};
            params.oldEvent = new DayPilot.Event(e.copy(), calendar);
            params.newEvent = new DayPilot.Event(e.temp(), calendar);

            var action = new DayPilot.Action(calendar, "EventUpdate", params, data);

            if (calendar._angular.scope) {
                e.commit();
                calendar._angular.notify();
            }
            else {
                var rows = calendar.events._removeFromRows(e.data);

                e.commit();

                rows = rows.concat(calendar.events._addToRows(e.data));

                calendar._loadRows(rows);
                calendar._updateRowHeights();

                if (calendar._initialized) {
                    if (calendar.viewType === "Gantt") {
                        calendar.update();
                    }
                    else {
                        calendar._updateRowsNoLoad(rows);
                        calendar._updateHeight();
                    }
                }
            }

            return action;
        };


        this.events.remove = function(e, data) {

            var params = {};
            params.e = new DayPilot.Event(e.data, calendar);

            var action = new DayPilot.Action(calendar, "EventRemove", params, data);

            var index = DayPilot.indexOf(calendar.events.list, e.data);
            calendar.events.list.splice(index, 1);

            if (calendar._angular.scope) {
                calendar._angular.notify();
            }
            else {
                var rows = calendar.events._removeFromRows(e.data);

                calendar._loadRows(rows);

                calendar._updateRowHeights();

                if (calendar._initialized) {
                    if (calendar.viewType === "Gantt") {
                        calendar.update();
                    }
                    else {
                        calendar._updateRowsNoLoad(rows);
                        calendar._updateHeight();
                    }
                }
            }

            return action;
        };

        this.events.add = function(e, data) {

            e.calendar = calendar;

            if (!calendar.events.list) {
                calendar.events.list = [];
            }

            calendar.events.list.push(e.data);

            if (calendar._angular.scope) {
                calendar._angular.notify();
            }
            else {
                var params = {};
                params.e = e;

                var action = new DayPilot.Action(calendar, "EventAdd", params, data);

                //var ri = DayPilot.indexOf(calendar.rows, calendar._findRowByResourceId(e.resource()));
                //var row = calendar.rows[ri];

                // prepare
                //var start = new Date();

                var rows = calendar.events._addToRows(e.data);

                calendar._loadRows(rows);

                calendar._updateRowHeights();

                //var end = new Date();

                if (calendar._initialized) {
                    if (calendar.viewType === "Gantt") {
                        calendar.update();
                    }
                    else {
                        calendar._updateRowsNoLoad(rows);
                        calendar._updateHeight();
                    }
                }
            }

            return action;

        };

        this.queue = {};
        this.queue.list = [];
        this.queue.list.ignoreToJSON = true;

        this.queue.add = function(action) {
            if (!action) {
                return;
            }
            if (action.isAction) {
                calendar.queue.list.push(action);
            }
            else {
                throw "DayPilot.Action object required for queue.add()";
            }
        };

        this.queue.notify = function(data) {
            var params = {};
            params.actions = calendar.queue.list;
            calendar._callBack2('Notify', params, data, "Notify");

            calendar.queue.list = [];
        };


        this.queue.clear = function() {
            calendar.queue.list = [];
        };

        this.queue.pop = function() {
            return calendar.queue.list.pop();
        };

        this.cells.find = function(start, resource) {
            var pixels = calendar.getPixels(new DayPilot.Date(start));
            if (!pixels) {
                return cellArray();
            }
            var x = pixels.i;
            
            var row = calendar._findRowByResourceId(resource);
            if (!row) {
                return cellArray();
            }
            var top = row.top;
            var y = calendar._getRow(top).i;
            
            return this.findXy(x, y);
        };
        
        this.cells.findByPixels = function(x, y) {
            var itc = calendar._getItlineCellFromPixels(x);
            if (!itc) {
                return cellArray();
            }
            var x = itc.x;
            
            var row = calendar._getRow(y);
            if (!row) {
                return cellArray();
            }
            var y = row.i;
            return this.findXy(x, y);
        };
        
        this.cells.all = function() {
            var list = [];
            // may require optimization
            var maxX = calendar.itline.length;
            var maxY = calendar.rowlist.length;
            for(var x = 0; x < maxX; x++) {
                for (var y = 0; y < maxY; y++) {
                    var cell = calendar.cells.findXy(x, y);
                    list.push(cell[0]);
                }
            }
            return cellArray(list);
        };
        
        this.cells._cell = function(x, y) {
            var itc = calendar.itline[x];
            
            var cell = {};
            cell.x = x;
            cell.y = y;
            cell.i = x + "_" + y;
            cell.resource = calendar.rowlist[y].id;
            cell.start = itc.start;
            cell.end = itc.end;
            cell.update = function() { // if visible

                if (!calendar.rowlist[cell.y].hidden) {
                    var area = calendar._getDrawArea();
                    if (area.xStart <= cell.x && cell.x <= area.xEnd) {
                        if (area.yStart <= cell.y && cell.y <= area.yEnd) {
                            var old = calendar._cache.cells[cell.i];
                            calendar._deleteCell(old);
                            calendar._drawCell(cell.x, cell.y);
                        }
                    }
                }

            };
            cell.div = calendar._cache.cells[cell.i];

            /*
            if (!calendar.cellProperties) {
                calendar.cellProperties = {};
            }
            if (calendar.cellProperties) {
                var p = calendar._getCellProperties(x, y);
                if (!p) {
                    p = {};
                    calendar.cellProperties[cell.i] = p;
                }
                cell.properties = p;
            }
            */

            var p = calendar._getCellProperties(x, y);
            cell.properties = p;

            return cell;
        };
        
        /* accepts findXy(0,0) or findXy([{x:0, y:0}, {x:0, y:1}]) */
        this.cells.findXy = function(x, y) {
            
            if (DayPilot.isArray(x)) {
                var cells = [];
                for (var i = 0; i < x.length; i++) {
                    var o = x[i];
                    cells.push(calendar.cells._cell(o.x, o.y));
                }
                return cellArray(cells);
            } 
            else if (x === null || y === null) {
                return cellArray(); // empty
            }
            var cell = calendar.cells._cell(x, y);
            return cellArray(cell);
        };

        var rowArray = function(a) {
            var list = [];

            if (DayPilot.isArray(a)) {
                for (var i = 0; i < a.length; i++) {
                    list.push(a[i]);
                }
            }
            else if (typeof a === 'object') {
                list.push(a);
            }

            list.each = function(f) {
                if (!f) {
                    return;
                }
                for (var i = 0; i < list.length; i++) {
                    f(list[i]);
                }
            };

            return list;

        };


        var cellArray = function(a) {
            var list = [];
            
            if (DayPilot.isArray(a)) {
                for (var i = 0; i < a.length; i++) {
                    list.push(a[i]);
                }
            }
            else if (typeof a === 'object') {
                list.push(a);
            }
            
            list.cssClass = function(css) {
                this.each(function(item) {
                    item.properties.cssClass = DayPilot.Util.addClassToString(item.properties.cssClass, css);
                    item.update();
/*
                    if (item.div) {
                        DayPilot.Util.addClass(item.div, css);
                    }*/
                });
                return this;
            };
            
            list.removeClass = function(css) {
                this.each(function(item) {
                    item.properties.cssClass = DayPilot.Util.removeClassFromString(item.properties.cssClass, css);
                    item.update();
                    /*
                    if (item.div) {
                        DayPilot.Util.removeClass(item.div, css);
                    }*/
                });
                return this;
            };
            
            list.addClass = list.cssClass;
            
            list.html = function(html) {
                this.each(function(item) {
                    item.properties.html = html;
                    item.update();
                    /*
                    if (item.div) {
                        item.div.innerHTML = html;
                    }*/
                });
                return this;
            };
            
            list.each = function(f) {
                if (!f) {
                    return;
                }
                for (var i = 0; i < this.length; i++) {
                    f(list[i]);
                }
            };

            return list;
        };

        /*
        this._debug = function(msg, append) {
            if (!this.debuggingEnabled) {
                return;
            }

            if (!calendar._debugMessages) {
                calendar._debugMessages = [];
            }
            calendar._debugMessages.push(msg);

        };
        */

        /*
        this.showDebugMessages = function() {
            alert("Log:\n" + calendar._debugMessages.join("\n"));
        };
        */

        this._angular = {};
        this._angular.scope = null;
        this._angular.notify = function() {
            if (calendar._angular.scope) {
                calendar._angular.scope["$apply"]();
            }
        };

        this.debug = new DayPilot.Debug(this);
        
        this._getRowStartInDaysView = function(date) {
            if (calendar.viewType !== 'Days') {
                throw "Checking row start when viewType !== 'Days'";
            }
            for (var i = 0; i < calendar.rowlist.length; i++) {
                var row = calendar.rowlist[i];
                var data = row.element ? row.element.data : row.data;
                var start = data.start;
                if (date.getTime() >= start.getTime() && date.getTime() < start.addDays(1).getTime()) {
                    return start;
                }
            }
            return null;
        };
        
        this._getBoxStart = function(date) {
            
            if (date.ticks === this.startDate.ticks) {
                return date;
            }

            var cursor = this.startDate;

            if (date.ticks < this.startDate.ticks) {
                var firstCellDuration = this.itline[0].end.ticks - this.itline[0].start.ticks;
                while (cursor.ticks > date.ticks) {
                    cursor = cursor.addTime(-firstCellDuration);
                }
                return cursor;
            }
            
            if (calendar.viewType === 'Days') {
                var rowStart = this._getRowStartInDaysView(date);
                var offset = rowStart.getTime() - calendar.startDate.getTime();

                var cell = this._getItlineCellFromTime(date.addTime(-offset));
                if (cell.current) {
                    return cell.current.start.addTime(offset);
                }
                if (cell.past) {
                    return cell.previous.end.addTime(offset);
                }
                throw "getBoxStart(): time not found";
                
            }
            else {
                var cell = this._getItlineCellFromTime(date);
                if (cell.current) {
                    return cell.current.start;
                }
                if (cell.past) {
                    return cell.previous.end;
                }
                if (cell.hidden) {
                    var diff = cell.next.start.getTime() - date.getTime();
                    var cellduration = cell.next.end.getTime() - cell.next.start.getTime();
                    var rounded = Math.ceil(diff / cellduration) * cellduration;
                    var result = cell.next.start.addTime(-rounded);
                    return result;
                }
                throw "getBoxStart(): time not found";
            }
            
            
            /*
            cursor = date;
            var cell = null;

            while (cell === null) {
                cell = this._getItlineCellFromTime(cursor);
                cursor = cursor.addMinutes(1);
            }

            return cell.start;
            */

        };

        this._getShadowCoords = function(object) {

            // get row
            var row = this._getRow(calendar.coords.y);

            //var object = DayPilotScheduler.moving;
            var e = object.event;
            if (typeof e.end !== 'function') {
                throw "e.end function is not defined";
            }
            if (!e.end()) {
                throw "e.end() returns null";
            }
            var duration = DayPilot.Date.diff(e.end().d, e.start().d);
            duration = Math.max(duration, 1);

            var useBox = resolved.useBox(duration);

            var isMilestone = e.data && e.data.type === "Milestone";
            var milestoneWidth = calendar.eventHeight;

            //var day = e.start().getDatePart();
            var startOffsetTime = 0;

            var x = calendar.coords.x;
            if (isMilestone) {
                x += milestoneWidth/2;
            }

            if (calendar.scale === "Manual") {
                var minusDurationPx = (function() {
                    var end = calendar.getDate(calendar.coords.x, true, true);
                    var start = end.addTime(-duration);

                    var startPix = calendar.getPixels(start).boxLeft;
                    var endPix = calendar.getPixels(end).boxRight;

                    var end = Math.min(endPix, calendar.coords.x);

                    return end - startPix;
                })();

                var offset = Math.min(DayPilotScheduler.moveOffsetX, minusDurationPx);

                x = calendar.coords.x - offset;
                
            }

            if (useBox && !isMilestone) {
                //startOffsetTime = e.start().getTime() - (day.getTime() + Math.floor((e.start().getHours() * 60 + e.start().getMinutes()) / calendar.cellDuration) * calendar.cellDuration * 60 * 1000);
                
                var cell = calendar._getItlineCellFromTime(e.start());
                var startInTimeline = !cell.hidden && !cell.past;
                
                startOffsetTime = e.start().getTime() - this._getBoxStart(e.start()).getTime();
                
                if (startInTimeline) {
                    startOffsetTime = (function(originalTime, offset) {
                        var oticks = calendar._getCellTicks(calendar._getItlineCellFromTime(originalTime).current);
                        var nticks = calendar._getCellTicks(calendar._getItlineCellFromPixels(x).cell);

                        if (oticks > nticks * 1.2) { // normally one would be fine but avoid month issues when moving to shorter month (28 vs 31 days)
                            var sign = offset > 0 ? 1 : -1;
                            var offset = Math.abs(offset);
                            while (offset >= nticks) {
                                offset -= nticks;
                            }
                            offset *= sign;
                        }
                        return offset;
                    })(e.start(), startOffsetTime);
                }
            } 

            var dragOffsetTime = 0;
            
            // this keeps the cell offset the same after moving
            if (DayPilotScheduler.moveDragStart && calendar.scale !== "Manual") {
                
                if (useBox) {
                    dragOffsetTime = DayPilotScheduler.moveDragStart.getTime() - this._getBoxStart(e.start()).getTime();
                    var cellDurationTicks = calendar._getCellDuration() * 60 * 1000;
                    dragOffsetTime = Math.floor(dragOffsetTime/cellDurationTicks) * cellDurationTicks;
                }
                else {
                    dragOffsetTime = DayPilotScheduler.moveDragStart.getTime() - e.start().getTime();
                }
            } 
            else { // external drag
                //dragOffsetTime = this.cellDuration * 60000 / 2; // half cell duration
                dragOffsetTime = 0; // half cell duration
            }
            if (this.eventMoveToPosition) {
                dragOffsetTime = 0;
            }
            
            var start = this.getDate(x, true).addTime(-dragOffsetTime);
            
            if (DayPilotScheduler.resizing) {
                start = e.start();
            }

            if (this.snapToGrid) { // limitation: this blocks moving events before startDate
                if (calendar.viewType === "Days") {
                    start = this._getBoxStart(start);
                }
                else {
                    start = this._getBoxStart(start);
                }
            }
            
            start = start.addTime(startOffsetTime);
            var end = start.addTime(duration);

            var adjustedStart = start;
            var adjustedEnd = end;

            if (this.viewType === 'Days') {
                var rowOffset = this.rowlist[e.part.dayIndex].start.getTime() - this.startDate.getTime();
                var adjustedStart = start.addTime(-rowOffset);
                var adjustedEnd = adjustedStart.addTime(duration);

                var currentRowOffset = row.element.data.start.getTime() - this.startDate.getTime();
                start = adjustedStart.addTime(currentRowOffset);
                end = start.addTime(duration);
            }

            var startPixels = this.getPixels(adjustedStart);
            var endPixels = this.getPixels(adjustedEnd);

            var left = (useBox) ? startPixels.boxLeft : startPixels.left;
            var width = (useBox) ? (endPixels.boxRight - left) : (endPixels.left - left);


            if (isMilestone) {
                width = milestoneWidth;
                left -= width/2;
            }

            var coords = {};
            coords.top = row.top;
            coords.left = left;
            coords.row = row.element;
            coords.rowIndex = row.i;
            coords.width = width;
            coords.start = start;
            coords.end = end;
            coords.relativeY = calendar.coords.y - row.top;

            return coords;
        };
        
        this._getCellDuration = function() {  // approximate, needs to be updated for a specific time (used only for rounding in getShadowCoords
            switch (this.scale) {
                case "CellDuration":
                    return this.cellDuration;
                case "Minute":
                    return 1;
                case "Hour":
                    return 60;
                case "Day":
                    return 60*24;
                case "Week":
                    return 60*24*7;
                case "Month":
                    return 60*24*30;
                case "Year":
                    return 60*24*365;
            }
            throw "can't guess cellDuration value";
        };
        
        this._getCellTicks = function(itc) {
            return itc.end.ticks - itc.start.ticks;
        };

        this._isRowDisabled = function(y) {
            return this.treePreventParentUsage && this._isRowParent(y);
        };

        this._isRowParent = function(y) {
            var row = this.rowlist[y];
            if (row.isParent) {
                return true;
            }
            if (this.treeEnabled) {
                if (row.children && row.children.length > 0) {
                    return true;
                }
            }
            return false;
        };
        
        this._autoexpand = {};
        this._expandParent = function() {
            
            if (!calendar.treeAutoExpand) {
                return;
            }
            
            var coords = this._getShadowCoords(DayPilotScheduler.moving);
            var y = coords.rowIndex;
            var isParent = this._isRowParent(y);
            
            var expand = this._autoexpand;
            if (expand.timeout && expand.y !== y) {
                clearTimeout(expand.timeout);
                expand.timeout = null;
            }
            
            if (isParent) {
                expand.y = y;
                if (!expand.timeout) {
                    expand.timeout = setTimeout(function() {
                        var collapsed = !calendar.rowlist[expand.y].expanded;
                        if (collapsed) {
                            calendar._toggle(expand.y);
                            calendar._moveShadow();
                        }
                        expand.timeout = null;
                    }, 500);
                }
            }
        };
        
        this._updateResizingShadow = function() {
            var shadowWidth = DayPilotScheduler.resizingShadow.clientWidth;
            var shadowLeft = DayPilotScheduler.resizingShadow.offsetLeft;
            var e = DayPilotScheduler.resizing.event;
            var border = DayPilotScheduler.resizing.dpBorder;

            // TODO involve rowStart for Days mode
            var row = calendar.rowlist[DayPilotScheduler.resizing.event.part.dayIndex];
            var rowOffset = row.start.getTime() - calendar._visibleStart().getTime();

            var newStart = null;
            var newEnd = null;
            
            var exact = !calendar.snapToGrid;

            if (border === 'left') {
                newStart = calendar.getDate(shadowLeft, exact).addTime(rowOffset);
                newEnd = e.end();
            }
            else if (border === 'right') {
                newStart = e.start();
                newEnd = calendar.getDate(shadowLeft + shadowWidth, exact, true).addTime(rowOffset);
            }
            
            DayPilotScheduler.resizingShadow.start = newStart;
            DayPilotScheduler.resizingShadow.end = newEnd;
            
        };

        this._moveShadow = function() {
            var scroll = this.nav.scroll;
            if (!calendar.coords) {
                return;
            }

            calendar._hideMessage();

            var shadow = DayPilotScheduler.movingShadow;
            var coords = this._getShadowCoords(DayPilotScheduler.moving);
            var ev = DayPilotScheduler.moving.event;

            var linepos = 0;
            (function calculatePosition() {
                //return;
                var y = coords.relativeY;
                var row = coords.row;
                var linesCount = row.lines.length;
                var top = 0;
                var lh = calendar._resolved.eventHeight();
                var max = row.lines.length;
                for (var i = 0; i < row.lines.length; i++) {
                    var line = row.lines[i];
                    //if (line.isFree(coords.left, coords.width)) {
                    if (line.isFree(coords.left, calendar.cellWidth)) {
                        max = i;
                        break;
                    }
                }

                var pos = Math.floor((y - top + lh / 2) / lh);  // rounded position
                var pos = Math.min(max, pos);  // no more than max
                var pos = Math.max(0, pos);  // no less then 0

                linepos = pos;

            })();

            var verticalAllowed = (ev.cache && typeof ev.cache.moveVDisabled !== 'undefined') ? !ev.cache.moveVDisabled : !ev.data.moveVDisabled;
            var horizontalAllowed = (ev.cache && typeof ev.cache.moveHDisabled !== 'undefined') ? !ev.cache.moveHDisabled :!ev.data.moveHDisabled;

            var relY = linepos * calendar._resolved.eventHeight();
            if (relY > 0) {
                relY -= 3;
            }

            if (verticalAllowed) {
                if (!this._isRowDisabled(coords.rowIndex)) {
                    shadow.row = coords.row;
                    shadow.style.height = Math.max(coords.row.height, 0) + 'px';
                    shadow.style.top = (coords.top) + 'px';
                    if (calendar.eventMoveToPosition) {
                        shadow.style.top = (coords.top + relY) + "px";
                        shadow.style.height = "3px";
                        shadow.line = linepos;
                    }
                }
                else {
                    var oldRow = shadow.row;
                    var dir = coords.rowIndex < oldRow.index ? 1 : -1;
                    for (var i = coords.rowIndex; i !== oldRow.index; i += dir) {
                        var row = this.rowlist[i];
                        if (!this._isRowDisabled(i) && !row.hidden) {
                            shadow.style.top = (row.top) + 'px';
                            shadow.style.height = Math.max(row.height, 0) + 'px';
                            shadow.row = row;

                            if (calendar.eventMoveToPosition) {
                                linepos = dir > 0 ? 0 : row.lines.length - 1;
                                shadow.style.top = (coords.top + relY) + "px";
                                shadow.style.height = "3px";
                                shadow.line = linepos;
                            }

                            break;
                        }
                    }
                }
            }
            else {
                var oldRow = calendar.rowlist[ev.part.dayIndex];
                //var oldRow = this.rowlist[this._getRow(parseInt(shadow.style.top)).i];

                var max = oldRow.lines.length;
                for (var i = 0; i < oldRow.lines.length; i++) {
                    var line = oldRow.lines[i];
                    if (line.isFree(coords.left, calendar.cellWidth)) {
                        max = i;
                        break;
                    }
                }

                shadow.style.height = Math.max(oldRow.height, 0) + 'px';
                shadow.style.top = (oldRow.top) + 'px';
                shadow.row = oldRow;
                if (calendar.eventMoveToPosition) {
                    if (coords.row === oldRow) {
                        shadow.style.top = (oldRow.top + relY) + "px";
                        shadow.style.height = "3px";
                        shadow.line = linepos;
                    }
                    else {
                        var pos = (coords.rowIndex > oldRow.index && max > 0) ? max * calendar._resolved.eventHeight() - 3 : 0;
                        shadow.style.top = (oldRow.top + pos) + "px";
                        shadow.style.height = "3px";
                        shadow.line = 0;
                    }
                }
            }

            if (horizontalAllowed) {
                shadow.style.left = coords.left + 'px';
                if (calendar.eventMoveToPosition) {
                    shadow.style.width = (calendar.cellWidth) + 'px';
                }
                else {
                    shadow.style.width = (coords.width) + 'px';
                }
                shadow.start = coords.start;
                shadow.end = coords.end;
            }
            else {
                shadow.style.left = ev.part.left + "px";
                shadow.start = ev.start();
                shadow.end = ev.end();
            }

            (function checkOverlap() {
                var row = coords.row;
                var data = ev.data;
                var width = coords.width;
                var left = coords.left;

                calendar._overlappingShadow(shadow, row, left, width, data);
            })();

                
            (function() {

                var last = calendar._lastEventMoving;

                // don't fire the event if there is no change
                if (last && last.start.getTime() === shadow.start.getTime() && last.end.getTime() === shadow.end.getTime() && last.resource === shadow.row.id) {
                    return;
                }

                var args = {};
                args.start = shadow.start;
                args.end = shadow.end;
                args.e = ev;
                //args.row = shadow.row;
                args.resource = shadow.row.id;
                args.position = shadow.line;
                args.overlapping = shadow.overlapping;
                args.left = {};
                args.left.html = args.start.toString(calendar.eventMovingStartEndFormat);
                args.left.enabled = calendar.eventMovingStartEndEnabled;
                args.right = {};
                args.right.html = args.end.toString(calendar.eventMovingStartEndFormat);
                args.right.enabled = calendar.eventMovingStartEndEnabled;

                calendar._lastEventMoving = args;

                if (typeof calendar.onEventMoving === 'function') {
                    calendar.onEventMoving(args);
                }

                calendar._showShadowHover(DayPilotScheduler.movingShadow, args);
            })();
        };

        this._overlappingShadow = function(shadow, row, left, width, data) {

            if (calendar.allowEventOverlap) {
                return;
            }

            (function calculate() {
                for (var i = 0; i < row.lines.length; i++) {
                    var line = row.lines[i];
                    if (!line.isFree(left, width, data)) {
                        shadow.overlapping = true;
                        return;
                    }
                }
                shadow.overlapping = false;
            })();

            var overlapping = shadow.overlapping;

            var cssClass = calendar._prefixCssClass("_shadow_overlap");
            if (overlapping) {
                DayPilot.Util.addClass(shadow, cssClass);
            }
            else {
                DayPilot.Util.removeClass(shadow, cssClass);
            }
        };
        
        this._showShadowHover = function(shadow, args) {
            
            /*
             * uses:
             * 
             * args.left.width (optional)
             * args.left.html
             * args.left.enabled
             * 
             * args.right.width (optional)
             * args.right.html
             * args.right.enabled
             * 
             */
            
            //var shadow = DayPilotScheduler.movingShadow;
            var space = 5;
            
            this._clearShadowHover();
            
            var pos = {};
            pos.left = parseInt(shadow.style.left);
            pos.top = parseInt(shadow.style.top);
            pos.right = pos.left + parseInt(shadow.style.width);
            
            var width = args.left.width || 10;
            
            var left = document.createElement("div");
            left.style.position = "absolute";
            left.style.left = (pos.left - width - space) + "px";
            left.style.top = pos.top + "px";
            left.style.height = calendar.eventHeight + "px";
            left.style.overflow = "hidden";
            left.innerHTML = args.left.html;
            left.className = this._prefixCssClass("_event_move_left");

            if (args.left.enabled) {
                calendar.divHover.appendChild(left);
            }

            if (args.left.width) {
                left.style.width = width + "px";
            }
            else {
                left.style.whiteSpace = "nowrap";
                var nwidth = left.offsetWidth;
                var nleft = pos.left - nwidth - space;
                left.style.width = nwidth + "px";
                left.style.left = nleft + "px";
            }

            var right = document.createElement("div");
            right.style.position = "absolute";
            right.style.left = (pos.right + space) + "px";
            right.style.top = pos.top + "px";
            right.style.height = calendar.eventHeight + "px";
            right.style.overflow = "hidden";
            if (args.right.width) {
                right.style.width = args.right.width + "px";
            }
            else {
                right.style.whiteSpace = "nowrap";
            }
            right.innerHTML = args.right.html;
            right.className = this._prefixCssClass("_event_move_right");
            
            if (args.right.enabled) {
                calendar.divHover.appendChild(right);
            }

        };
        
        this._clearShadowHover = function() {
            calendar.divHover.innerHTML = ''; // clear
        };
        
        this._loadRowHeaderColumns = function() {
            if (this.rowHeaderColumns) {
                this.rowHeaderCols = DayPilot.Util.propArray(this.rowHeaderColumns, "width");
            }
        };



        this._getTotalRowHeaderWidth = function() {
            var totalWidth = 0;
            this._loadRowHeaderColumns();
            if (this.rowHeaderCols) {
                for (var i = 0; i < this.rowHeaderCols.length; i++) {
                    totalWidth += this.rowHeaderCols[i];
                }
            }
            else {
                totalWidth = this.rowHeaderWidth;
            }
            return totalWidth;
        };

        this._getAreaRowsWithMargin = function() {
            return this._getAreaRows(calendar.progressiveRowRenderingPreload);
        };

        this._getAreaRows = function(margin) {
            //var margin = calendar.progressiveRowRenderingPreload;
            var margin = margin || 0;

            var start = 0;
            var end = calendar.rowlist.length;
            var progressive = calendar.progressiveRowRendering;
            if (progressive) {
                var area = calendar._getDrawArea();
                start = area.yStart;
                end = area.yEnd + 1;

                start = Math.max(0, start - margin);
                end = Math.min(calendar.rowlist.length, end + margin);
            }

            return {
                "start": start,
                "end": end
            }
        };

        this._autoRowHeaderWidth = function() {
            if (!this._visible()) {   // not visible, doesn't make sense now
                return;
            }

            if (!this.rowHeaderWidthAutoFit) {
                return;
            }

            /*
            if (this.cellWidthSpec === 'Auto') {
                calendar.debug.message("AutoRowHeaderWidth turned off because CellWidthSpec is set to 'Auto'.", "warning");
                return;
            }
            */
           
            var table = this.divHeader;
            
            if (!table) {
                return;
            }
            
            if (!table.rows) {
                return;
            }

            var max = [];

            var range = calendar._getAreaRowsWithMargin();

            for (var i = range.start; i < range.end; i++) {
                var row = table.rows[i];

                if (!row) {
                    continue;
                }

                if (row.hidden) {
                    continue;
                }

                if (row.autofitDone) {
                    continue;
                }

                row.autofitDone = true;

                //var left = 0;
                for (var j = 0; j < row.cells.length; j++) {
                    var inner = row.cells[j].firstChild.firstChild;
                    if (!inner || !inner.style) {
                        continue;
                    }
                    var oldWidth = inner.style.width;
                    var oldRight = inner.style.right;
                    inner.style.position = "absolute";
                    inner.style.width = "auto";
                    inner.style.right = "auto";
                    inner.style.whiteSpace = "nowrap";
                    var w = inner.offsetWidth + 2;
                    inner.style.position = "";
                    inner.style.width = oldWidth;
                    inner.style.right = oldRight;
                    inner.style.whiteSpace = "";
                    if (typeof max[j] === 'undefined') { max[j] = 0; }
                    max[j] = Math.max(max[j], w);
                }
            }
            var maxAll = 0;
            var needsUpdate = false;

            this._loadRowHeaderColumns();
            
            if (this.rowHeaderCols) {
                for (var i = 0; i < max.length; i++) {
                    if (this.rowHeaderCols[i]) {
                        if (max[i] > this.rowHeaderCols[i]) {
                            this.rowHeaderCols[i] = max[i];
                            needsUpdate = true;
                        }
                        maxAll += this.rowHeaderCols[i];
                    }
                }
            }
            else {
                maxAll = this.rowHeaderWidth;
                if (this.rowHeaderWidth < max[0]) {
                    maxAll = max[0];
                    needsUpdate = true;
                }
            }

            if (calendar.progressiveRowRendering) {
                //needsUpdate = true;
            }

            if (needsUpdate) {
                if (this._splitter) {
                    // update header
                    this._splitter.widths = this.rowHeaderCols;
                    this._splitter.updateWidths();
                    // update cells
                    DayPilot.Util.updatePropsFromArray(this.rowHeaderColumns, "width", this.rowHeaderCols);
                }

                if (!this.rowHeaderScrolling) {
                    this.rowHeaderWidth = maxAll;
                }
                this._updateRowHeaderWidth();
                
                this._updateAutoCellWidth();
            }
        };

        this._drawResHeader = function() {

            this._resHeaderDivBased = true;

            //DayPilot.puc(parent);
            //parent.innerHTML = '';

            this._loadRowHeaderColumns();

            //var rowHeaderCols = this.rowHeaderCols;
            //var columns = rowHeaderCols ? this.rowHeaderCols.length : 0;
            var totalWidth = this._getTotalRowHeaderWidth();

            var wrap = this.divHeader;
            if (wrap) {
                wrap.innerHTML = '';
                DayPilot.puc(wrap);
            }
            else {
                wrap = document.createElement("div");
                wrap.onmousemove = function() { calendar._out(); };
                if (!this.cssOnly) {
                    wrap.className = this._prefixCssClass("resourceheader");
                }
                this.divHeader = wrap;
            }

            wrap.style.width = totalWidth + "px";
            wrap.style.height = calendar._innerHeightTree + "px";
            wrap.rows = [];

            var progressive = calendar.progressiveRowRendering;
            if (progressive) {
                doNothing();
            }
            else {
                var m = this.rowlist.length;
                for (var i = 0; i < m; i++) {
                    calendar._drawRow(i);
                }
            }
            calendar._drawResScrollSpace();

            this.divResScroll.appendChild(wrap);

            if (this.rowHeaderWidthAutoFit) {
                this._autoRowHeaderWidth();
            }

        };

        this._drawResHeadersProgressive = function() {

            if (!calendar.progressiveRowRendering) {
                return;
            }

            var area = this._getAreaRowsWithMargin();

            //var wrap = document.createElement("div");
            //wrap.style.position = "absolute";

            for (var i = 0; i < calendar.rowlist.length; i++) {
                if (area.start <= i && i < area.end) {
                    calendar._drawRow(i);
                }
                else {
                    calendar._deleteRow(i);
                }
            }

            /*
            for (var i = area.start; i < area.end; i++) {
                calendar._drawRow(i, null);
            }*/

            //this.divHeader.appendChild(wrap);

            if (this.rowHeaderWidthAutoFit) {
                var originalWidth = calendar._getOuterRowHeaderWidth();
                this._autoRowHeaderWidth();
                var newWidth = calendar._getOuterRowHeaderWidth();
                if (newWidth !== originalWidth) {
                    var originalCellWidth = this.cellWidth;
                    this._calculateCellWidth();
                    var newCellWidth = this.cellWidth;
                    if (newCellWidth !== originalCellWidth) {
                        this._prepareItline();
                        this._drawTimeHeader();
                        this._updateHeight();
                        this._loadEvents();
                        /*
                        this._deleteEvents();
                        this._drawEvents();
                        */
                    }
                }
            }

        };

        this._drawResScrollSpace = function() {

            /*
            if (calendar.nav.resScrollSpace) {
                return;
            }
            */


            var wrap = calendar.divHeader;
            var rowHeaderCols = this.rowHeaderCols;
            var columns = rowHeaderCols ? this.rowHeaderCols.length : 0;
            var totalWidth = this._getTotalRowHeaderWidth();

            //var r = table.insertRow(-1);
            //var c = r.insertCell(-1);
            var c = document.createElement("div");
            c.style.position = "absolute";
            c.style.top = this._innerHeightTree + "px";
            c.colSpan = columns + 1;
            c.style.width = totalWidth + "px";
            c.style.height = (calendar.divResScroll.clientHeight + 20) + "px";
            wrap.appendChild(c);

            calendar.nav.resScrollSpace = c;

            //c.style.borderRight = "1px solid " + this.borderColor;
            if (!this.cssOnly) {
                c.style.backgroundColor = this.hourNameBackColor;
                c.style.cursor = 'default';
            }
            c.setAttribute("unselectable", "on");
            if (!this.cssOnly) {
                c.className = this._prefixCssClass('rowheader');
                c.style.fontSize = "1px";
                c.innerHTML = "&nbsp;";
            }

            if (this.cssOnly) {
                var div = document.createElement("div");
                div.style.position = "relative";
                div.style.height = "100%";
                div.className = this._prefixCssClass('_rowheader');
                c.appendChild(div);
            }

        };

        this._deleteRow = function(i) {
            var row = calendar.divHeader.rows[i];

            if (!row) {
                return;
            }

            DayPilot.de(row.cells);
            calendar.divHeader.rows[i] = null;
        };

        this._drawRowForced = function(i) {
            this._deleteRow(i);
            this._drawRow(i);
        }

        this._drawRow = function(i) {
            var wrap = calendar.divHeader;

            var divHeader = this.divHeader;

            if (divHeader.rows[i]) { // already rendered
                return;
            }

            var rowHeaderCols = this.rowHeaderCols;
            var columns = rowHeaderCols ? this.rowHeaderCols.length : 0;
            var totalWidth = this._getTotalRowHeaderWidth();

            var row = this.rowlist[i];

            if (!row) {  // not found
                return;
            }

            //var node = this.tree[i];
            if (row.hidden) {
                return;
            }

            var args = this._doBeforeRowHeaderRender(row);

            divHeader.rows[i] = {};
            divHeader.rows[i].cells = [];

            var c = document.createElement("div");
            c.style.position = "absolute";
            c.style.top = row.top + "px";

            c.row = row;
            c.index = i;

            var props = args.row;

            var width = rowHeaderCols ? rowHeaderCols[0] : this.rowHeaderWidth;
            c.style.width = (width) + "px";
            c.style.border = "0px none";

            if (!this.cssOnly) {
                c.style.borderRight = "1px solid " + this.borderColor;
                c.style.backgroundColor = typeof props.backColor === 'undefined' ? calendar.hourNameBackColor : props.backColor;
                c.style.fontFamily = this.headerFontFamily;
                c.style.fontSize = this.headerFontSize;
                c.style.color = this.headerFontColor;
                c.style.cursor = 'default';
                c.style.padding = '0px';
            }
            if (props.toolTip) {
                c.title = props.toolTip;
            }
            c.setAttribute("unselectable", "on");
            //c.setAttribute('resource', row.id);

            c.onmousemove = calendar._onResMouseMove;
            c.onmouseout = calendar._onResMouseOut;
            c.onmouseup = calendar._onResMouseUp;
            c.oncontextmenu = calendar._onResRightClick;
            c.onclick = calendar._onResClick;
            c.ondblclick = calendar._onResDoubleClick;

            var div = document.createElement("div");
            div.style.width = (width) + "px";
            div.setAttribute("unselectable", "on");
            div.className = this.cssOnly ? this._prefixCssClass('_rowheader') : this._prefixCssClass('rowheader');
            if (props.cssClass) {
                DayPilot.Util.addClass(div, props.cssClass);
            }
            if (props.backColor) {
                div.style.background = props.backColor;
            }
            div.style.height = (row.height) + "px";
            div.style.overflow = 'hidden';
            div.style.position = 'relative';

            var inner = document.createElement("div");
            //inner.style.width = "100%";
            inner.setAttribute("unselectable", "on");
            inner.className = this.cssOnly ? this._prefixCssClass('_rowheader_inner') : "";
            //inner.style.position = 'absolute';
            div.appendChild(inner);

            var moving = this.rowMoveHandling !== "Disabled";
            var dragHandleWidth = 10;

            var areas = props.areas || [];

            if (moving && !props.moveDisabled) { // add moving handle
                areas.push({
                    "v": "Hover",
                    "w": dragHandleWidth,
                    "bottom": 0,
                    "top": 0,
                    "left": 0,
                    "css": calendar._prefixCssClass("_rowmove_handle"),
                    "action": "Move"
                });
            }

            var ro = calendar._createRowObject(row);
            DayPilot.Areas.attach(div, ro, {
                "areas": areas,
                "allowed": function() { return !rowmoving.row; }
            });

            var border = document.createElement("div");
            border.style.position = "absolute";
            border.style.bottom = "0px";
            border.style.width = "100%";
            border.style.height = "1px";
            if (this.cssOnly) {
                border.className = this._prefixCssClass("_resourcedivider");
            }
            else {
                border.style.backgroundColor = this.borderColor;
            }
            //div.dpDivider = border;
            div.appendChild(border);

            (function drawText() {

                if (calendar.treeEnabled  && !row.isNewRow) {

                    var left = row.level * calendar.treeIndent + calendar.treeImageMarginLeft;
                    if (moving) {
                        left += dragHandleWidth;
                    }
                    var width = 10;

                    var expand = document.createElement("div");

                    expand.style.width = "10px";
                    expand.style.height = width + "px";
                    expand.style.backgroundRepeat = "no-repeat";
                    expand.style.position = 'absolute';
                    expand.style.left = left + 'px';
                    expand.style.top = calendar.treeImageMarginTop + "px";


                    if (!row.loaded && row.children.length === 0) {
                        if (calendar.treeImageExpand && !calendar.cssOnly) {
                            expand.style.backgroundImage = "url('" + calendar.treeImageExpand + "')";
                        }
                        expand.className = calendar.cssOnly ? calendar._prefixCssClass('_tree_image_expand') : calendar._prefixCssClass('tree_image_expand');
                        expand.style.cursor = 'pointer';
                        expand.index = i;
                        expand.onclick = function(ev) { calendar._loadNode(this.index); ev = ev || window.event; ev.cancelBubble = true; };
                    }
                    else if (row.children.length > 0) {
                        if (row.expanded) {
                            if (calendar.treeImageCollapse && !calendar.cssOnly) {
                                expand.style.backgroundImage = "url('" + calendar.treeImageCollapse + "')";
                            }
                            expand.className = calendar.cssOnly ? calendar._prefixCssClass('_tree_image_collapse') : calendar._prefixCssClass('tree_image_collapse');
                        }
                        else {
                            if (calendar.treeImageExpand && !calendar.cssOnly) {
                                expand.style.backgroundImage = "url('" + calendar.treeImageExpand + "')";
                            }
                            expand.className = calendar.cssOnly ? calendar._prefixCssClass('_tree_image_expand') : calendar._prefixCssClass('tree_image_expand');
                        }

                        expand.style.cursor = 'pointer';
                        expand.index = i;
                        expand.onclick = function(ev) { calendar._toggle(this.index); ev = ev || window.event; ev.cancelBubble = true; };
                    }
                    else {
                        if (calendar.treeImageNoChildren && !calendar.cssOnly) {
                            //expand.src = calendar.treeImageNoChildren;
                            expand.style.backgroundImage = "url('" + calendar.treeImageNoChildren + "')";
                        }
                        expand.className = calendar.cssOnly ? calendar._prefixCssClass('_tree_image_no_children') : calendar._prefixCssClass('tree_image_no_children');
                    }

                    inner.appendChild(expand);

                }

                var text = document.createElement("div");
                if (calendar.treeEnabled) {
                    text.style.marginLeft = (left + width) + "px";
                }
                else if (!calendar.cssOnly) {
                    text.style.marginLeft = "4px";
                }
                text.innerHTML = props.html;
                c.textDiv = text;

                inner.appendChild(text);

            })();

            c.appendChild(div);

            if (props.areas) {
                for (var j = 0; j < props.areas.length; j++) {
                    var area = props.areas[j];
                    var v = area.v || "Visible";
                    if (v !== "Visible") {
                        continue;
                    }
                    var r = calendar._createRowObject(row);
                    var a = DayPilot.Areas.createArea(div, r, area);
                    div.appendChild(a);
                }
            }

            wrap.appendChild(c);
            divHeader.rows[i].cells.push(c);

            if (!row.columns || row.columns.length === 0) {
                c.colSpan = columns > 0 ? columns : 1;
                div.style.width = totalWidth + "px";
            }
            else {
                var left = width;
                for (var j = 1; j < columns; j++) {

                    //var c = r.insertCell(-1);
                    var c = document.createElement("div");
                    c.style.position = "absolute";
                    c.style.top = row.top + "px";
                    c.style.left = left + "px";
                    wrap.appendChild(c);
                    divHeader.rows[i].cells.push(c);

                    c.row = row;
                    c.index = i;

                    //c.style.width = (rowHeaderCols[j]) + "px";
                    if (!this.cssOnly) {
                        c.style.borderRight = "1px solid " + this.borderColor;
                        //c.style.borderBottom = "1px solid " + this.borderColor;
                        c.style.backgroundColor = props.backColor;
                        c.style.fontFamily = this.headerFontFamily;
                        c.style.fontSize = this.headerFontSize;
                        c.style.color = this.headerFontColor;
                        c.style.cursor = 'default';
                        c.style.padding = '0px';
                    }
                    if (props.toolTip) {
                        c.title = props.toolTip;
                    }
                    c.setAttribute("unselectable", "on");
                    if (!this.cssOnly) {
                        c.className = this._prefixCssClass('rowheader');
                    }
                    //c.setAttribute('resource', row.id);

                    c.onmousemove = calendar._onResMouseMove;
                    c.onmouseout = calendar._onResMouseOut;
                    c.onmouseup = calendar._onResMouseUp;
                    c.oncontextmenu = calendar._onResRightClick;
                    c.onclick = calendar._onResClick;
                    c.ondblclick = calendar._onResDoubleClick;

                    var div = document.createElement("div");
                    var w = this.cssOnly ? rowHeaderCols[j] : rowHeaderCols[j] - 1;
                    left += w;
                    if (props.backColor) {
                        div.style.backgroundColor = props.backColor;
                    }
                    div.style.width = w + "px";
                    div.style.height = (row.height) + "px";
                    div.style.overflow = 'hidden';
                    div.style.position = 'relative';
                    div.setAttribute("unselectable", "on");
                    if (this.cssOnly) {
                        DayPilot.Util.addClass(div, this._prefixCssClass("_rowheader"));
                        DayPilot.Util.addClass(div, this._prefixCssClass("_rowheadercol"));
                        DayPilot.Util.addClass(div, this._prefixCssClass("_rowheadercol" + j));
                    }
                    if (props.cssClass) {
                        DayPilot.Util.addClass(div, props.cssClass);
                    }

                    var inner = document.createElement("div");
                    //inner.style.position = 'absolute';
                    inner.setAttribute("unselectable", "on");
                    if (this.cssOnly) {
                        inner.className = this._prefixCssClass("_rowheader_inner");
                    }
                    div.appendChild(inner);

                    var border = document.createElement("div");
                    border.style.position = "absolute";
                    border.style.bottom = "0px";
                    border.style.width = "100%";
                    border.style.height = "1px";
                    border.className = this._prefixCssClass("_resourcedivider");
                    if (!this.cssOnly) {
                        border.style.backgroundColor = this.borderColor;
                    }
                    div.appendChild(border);


                    var text = document.createElement("div");
                    if (!this.cssOnly) {
                        text.style.marginLeft = '4px';
                    }

                    var col = props.columns[j - 1];
                    var innerHTML = col && col.html ? col.html : "";

                    text.innerHTML = innerHTML;
                    c.textDiv = text;

                    inner.appendChild(text);

                    c.appendChild(div);
                }
            }
        };

        this._onResRightClick = function() {
            var row = this.row;

            if (row.contextMenu) {
                row.contextMenu.show(calendar._createRowObject(row));
            }
            return false;
        };

        this._onResClick = function(ev) {
            if (rowtools.cancelClick) {
                return;
            }

            var row = this.row;
            var r = calendar._createRowObject(row, this.index);

            if (row.isNewRow) {
                calendar._rowtools.edit(row);
                return;
            }

            calendar._rowClickDispatch(r, ev.ctrlKey, ev.shiftKey, ev.metaKey);
        };
        
        this._onResDoubleClick = function(ev) {
            
            if (calendar.timeouts.resClick) {
                for (var toid in calendar.timeouts.resClick) {
                    window.clearTimeout(calendar.timeouts.resClick[toid]);
                }
                calendar.timeouts.resClick = null;
            }

            var row = this.row;
            var e = calendar._createRowObject(row, this.index);

            if (calendar._api2()) {
                
                var args = {};
                args.resource = e;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onRowDoubleClick === 'function') {
                    calendar.onRowDoubleClick(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }
                
                switch (calendar.rowDoubleClickHandling) {
                    case 'PostBack':
                        calendar.rowDoubleClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.rowDoubleClickCallBack(e);
                        break;
                    case 'Select':
                        calendar._rowSelectDispatch(row, ev.ctrlKey, ev.shiftKey, ev.metaKey);
                        break;
                    case 'Edit':
                        calendar._rowtools.edit(row);
                        break;
                }
                
                if (typeof calendar.onRowDoubleClicked === 'function') {
                    calendar.onRowDoubleClicked(args);
                }                

            }
            else {
                switch (calendar.rowDoubleClickHandling) {
                    case 'PostBack':
                        calendar.rowDoubleClickPostBack(e);
                        break;
                    case 'CallBack':
                        calendar.rowDoubleClickCallBack(e);
                        break;
                    case 'JavaScript':
                        calendar.onRowDoubleClick(e);
                        break;
                    case 'Select':
                        calendar._rowSelectDispatch(row, ev.ctrlKey, ev.shiftKey, ev.metaKey);
                        break;
                    case 'Edit':
                        calendar._rowtools.edit(row);
                        break;
                }
            }
        };
        
        this.rowDoubleClickPostBack = function(e, data) {
            var params = {};
            params.resource = e;

            this._postBack2("RowDoubleClick", params, data);
        };
        this.rowDoubleClickCallBack = function(e, data) {
            var params = {};
            params.resource = e;

            this._callBack2("RowDoubleClick", params, data);
        };

        this._onTimeClick = function(ev) {
            var cell = {};
            
            cell.start = this.cell.start;
            cell.level = this.cell.level;
            cell.end = this.cell.end;
            if (!cell.end) {
                cell.end = new DayPilot.Date(cell.start).addMinutes(calendar.cellDuration);
            }

            calendar._timeHeaderClickDispatch(cell);
        };

        this._createRowObject = function(row) {
            return new DayPilot.Row(row, calendar);
        };

        this._ensureRowData = function(i) {
            var row = this.rowlist[i];
            
            if (!row.events) {
                row.resetEvents();
            }
            
            if (row.data) {
                return;
            }
            
            row.data = {};

            // to be used later during client-side operations
            // rowStart
            row.data.start = new DayPilot.Date(row.start);
            // rowStartTicks
            row.data.startTicks = row.data.start.getTime();
            // rowEnd
            row.data.end = resolved.isResourcesView() ? this._visibleEnd() : row.data.start.addDays(1);
            // rowEndTicks 
            row.data.endTicks = row.data.end.getTime();
            // rowOffset
            row.data.offset = row.start.getTime() - this._visibleStart().getTime();
            row.data.i = i;
        };

        this._loadEvents = function(events) {
            if (events) {
                this.events.list = events;
            }
            else if (!this.events.list) {
                this.events.list = [];
            }

            eventloading.prepareRows(true);

            var list = this.events.list;
            var listlength = list.length;

            var ober = typeof this.onBeforeEventRender === 'function';
            var rows;
            var isRes = calendar.viewType === "Resources";

            for (var j = 0; j < listlength; j++) {
                var edata = list[j];

                if (!edata) {
                    continue;
                }

                if (ober) {
                    this._doBeforeEventRender(j);
                }

                if (edata.resource === "*") {
                    rows = calendar.rowlist;
                }
                else if (isRes) {
                    rows = eventloading.rowcache[edata.resource];
                }
                else if (calendar.viewType === "Days") {
                    rows = calendar.rowlist;
                }
                else if (calendar.viewType === "Gantt") {
                    rows = eventloading.rowcache[edata.id];
                }

                for (var x = 0; rows && x < rows.length; x++) {
                    var row = rows[x];
                    var ep = this._loadEvent(edata, row);

                    if (!ep) {
                        continue;
                    }

                    if (ober) {
                        ep.cache = this._cache.events[j];
                    }
                }
            }

            // sort events inside rows
            for (var i = 0; i < this.rowlist.length; i++) {
                var row = this.rowlist[i];
                this._loadRow(row);
            }

            this._updateRowHeights();
        };

        // assumes rows collection is created
        this._loadEventsOld = function(events) {
            var loadCache = [];

            var updatedRows = [];

            var append = null;
            if (events && add) {
                // append new events
                var supplied = events;

                var append = [];
                for (var i = 0; i < supplied.length; i++) {
                    var e = supplied[i];
                    var found = false;
                    for (var j = 0; j < this.events.list.length; j++) {
                        var ex = this.events.list[j];
                        // this causes changed events to be rendered again
                        if (ex.id === e.id && ex.start.toString() === e.start.toString() && ex.resource === e.resource) {
                        //if (ex.id === e.id) {
                            var rows = calendar.events._removeFromRows(ex);
                            updatedRows = updatedRows.concat(rows);
                            this.events.list[j] = e;
                            found = true;
                            break;
                        }
                    }
                    if (!found) {
                        append.push(e);
                    }
                }
                this.events.list = this.events.list.concat(append);
                //events = this.events; 
            }
            else if (events) {
                this.events.list = events;
            }
            else if (!this.events.list) {
                this.events.list = [];
            }

            var list = append || this.events.list;


            if (events) {
                this.events.list = events;
            }
            else if (!this.events.list) {
                this.events.list = [];
            }

            var list = this.events.list;
            var eventsLength = list.length;

            if (typeof this.onBeforeEventRender === 'function') {
                var start = append ? this.events.list.length - append.length : 0;
                //var start = 0;
                var end = this.events.list.length;
                for (var i = start; i < end; i++) {
                    this._doBeforeEventRender(i);
                }
            }

            var useLoadCache = !this._containsDuplicateResources();

            var optimizedLoading = true;

            if (optimizedLoading) {
                var resetEvents = !append || typeof row.events === "undefined";
                eventloading.prepareRows(resetEvents);

                for (var j = 0; list && j < eventsLength; j++) {
                    if (loadCache[j]) {
                        continue;
                    }
                    var edata = list[j];

                    if (!edata) {
                        continue;
                    }

                    var rows = [];

                    if (edata.resource === "*") {
                        rows = calendar.rowlist;
                    }
                    else if (calendar.viewType === "Days") {
                        rows = calendar.rowlist;
                    }
                    else if (calendar.viewType === "Gantt") {
                        rows = eventloading.rowcache[edata.id];
                    }
                    else {
                        rows = eventloading.rowcache[edata.resource];
                    }

                    for (var x = 0; rows && x < rows.length; x++) {
                        var row = rows[x];
                        var ep = this._loadEvent(edata, row);

                        if (!ep) {
                            continue;
                        }

                        if (typeof this.onBeforeEventRender === 'function') {
                            ep.cache = this._cache.events[j + start];
                        }
                        updatedRows.push(row.index);
                    }
                }
            }
            else {
                // first, load event parts into rows
                for (var i = 0; i < this.rowlist.length; i++) {
                    var row = this.rowlist[i];
                    if (!append || typeof row.events === "undefined") {
                        row.resetEvents();
                    }
                    //row.lines = [];

                    calendar._ensureRowData(i);

                    if (this._isRowDisabled(i)) {
                        continue;
                    }

                    for (var j = 0; list && j < eventsLength; j++) {
                        if (loadCache[j]) {
                            continue;
                        }
                        var e = list[j];

                        var ep = this._loadEvent(e, row);

                        if (!ep) {
                            continue;
                        }

                        if (typeof this.onBeforeEventRender === 'function') {
                            ep.cache = this._cache.events[j + start];
                        }

                        updatedRows.push(i);

                        // load cache is disabled to allow rows with duplicate ids
                        if (useLoadCache) {
                            if (ep.data.resource !== "*" && ep.part.start.getTime() === ep.start().getTime() && ep.part.end.getTime() === ep.end().getTime()) {
                                loadCache[j] = true;
                            }
                        }
                    }
                }
            }

            /*
            DayPilot.dynlist(this.rowlist).each(function(item) {
                calendar._loadRow(item);
            });
            */

            // sort events inside rows
            for (var i = 0; i < this.rowlist.length; i++) {
                var row = this.rowlist[i];
                this._loadRow(row);
            }


            this._updateRowHeights();

            return DayPilot.ua(updatedRows);
        };

        this._eventloading = {};
        var eventloading = this._eventloading;

        eventloading.rowCache = {};

        eventloading.prepareRows = function(resetEvents) {
            eventloading.rowcache = {};

            // initialize
            for (var i = 0; i < calendar.rowlist.length; i++) {
                var row = calendar.rowlist[i];
                if (resetEvents) {
                    row.resetEvents();
                }
                calendar._ensureRowData(i);

                if (!row.id) {
                    continue;
                }
                var key = row.id.toString();
                if (!eventloading.rowcache[key]) {
                    eventloading.rowcache[key] = [];
                }
                eventloading.rowcache[key].push(row);
            }
        };

        eventloading.loadEvent = function(edata) {

        };
        
        this._containsDuplicateResources = function() {
            var idlist = {};
            
            if (calendar.viewType !== "Resources") {
                return false;
            }
            for (var i = 0; i < calendar.rowlist.length; i++) {
                var row = calendar.rowlist[i];
                var id = row.id;
                if (idlist[id]) {
                    return true;
                }
                idlist[id] = true;
            }
            return false;
        };
        
        this._doBeforeEventRender = function(i) {
            var cache = this._cache.events;
            var data = this.events.list[i];
            var evc = {};
            
            // make a copy
            for (var name in data) {
                evc[name] = data[name];
            }
            
            if (typeof this.onBeforeEventRender === 'function') {
                var args = {};
                args.e = evc;
                this.onBeforeEventRender(args);
            }
            
            cache[i] = evc;
            
        };

        // internal
        this._loadRow = function(row) {
            row.lines = [];
            row.sections = null;
            //row.blocks = [];

            if (row.isNewRow) {
                return;
            }
            
            if (this.sortDirections) {
                row.events.sort(this._eventComparerCustom);
            }
            else {
                row.events.sort(this._eventComparer);
            }

            var collapsible = calendar.groupConcurrentEvents;

            if (collapsible) {
                for (var i = 0; i < row.blocks.length; i++) {
                    row.blocks[i].events = [];
                }
            }

            // put into lines
            for (var j = 0; j < row.events.length; j++) {
                var e = row.events[j];
                row.putIntoLine(e);
                if (collapsible) {
                    row.putIntoBlock(e);
                }
            }

            //row.calculateUtilization();

            // calculate line tops
            var lineTop = 0;
            for (var i = 0; i < row.lines.length; i++) {
                var line = row.lines[i];
                line.top = lineTop;
                lineTop += (line.height || row.eventHeight) * this.eventStackingLineHeight/100;
            }

            if (collapsible) {
                for (var j = 0; j < row.blocks.length; j++) {
                    var block = row.blocks[j];
                    block.lines = [];
                    block.events.sort(this._eventComparerCustom);
                    for (var k = 0; k < block.events.length; k++) {
                        var e = block.events[k];
                        block.putIntoLine(e);
                    }
                    if (block.lines.length <= calendar.groupConcurrentEventsLimit) {
                        block.expanded = true;
                    }
                    // calculate line tops
                    var lineTop = 0;
                    for (var i = 0; i < block.lines.length; i++) {
                        var line = block.lines[i];
                        line.top = lineTop;
                        lineTop += (line.height || row.eventHeight) * this.eventStackingLineHeight/100;
                    }

                }
            }

        };

        // internal
        this._loadRows = function(rows) {  // row indices
            rows = DayPilot.ua(rows); // unique

            for (var i = 0; i < rows.length; i++) {
                var ri = rows[i];
                calendar._loadRow(calendar.rowlist[ri]);
            }

            for (var i = 0; i < rows.length; i++) {
                var ri = rows[i];
                var row = calendar.rowlist[ri];
                calendar._updateEventPositionsInRow(row);
            }

        };

        // internal
        // returns ep if the event was added to this row, otherwise null
        this._loadEvent = function(e, row) {
            var start = new DayPilot.Date(e.start);
            var end = new DayPilot.Date(e.end);

            if (calendar.eventEndSpec === "Date") {
                end = end.getDatePart().addDays(1);
            }

            var startTicks = start.ticks;
            var endTicks = end.ticks;
            
            if (endTicks < startTicks) {  // skip invalid events
                return null;
            }

            var cache = null;
            if (typeof calendar.onBeforeEventRender === 'function') {
                var index = DayPilot.indexOf(calendar.events.list, e);
                cache = calendar._cache.events[index];
            }

            if (cache) {
                if (cache.hidden) {
                    return null;
                }
            }
            else if (e.hidden) {
                return null;
            }

            // belongs here
            var belongsHere = false;
            switch (this.viewType) {
                case 'Days':
                    belongsHere = !(endTicks <= row.data.startTicks || startTicks >= row.data.endTicks) || (startTicks === endTicks && startTicks === row.data.startTicks);
                    break;
                case 'Resources':
                    belongsHere = (row.id === e.resource || row.id === "*" || e.resource === "*") && (!(endTicks <= row.data.startTicks || startTicks >= row.data.endTicks) || (startTicks === endTicks && startTicks === row.data.startTicks));
                    break;
                case 'Gantt':
                    belongsHere = (row.id === e.id) && !(endTicks <= row.data.startTicks || startTicks >= row.data.endTicks);
                    break;

            }
            
            if (!belongsHere) {
                return null;
            }
            
            var ep = new DayPilot.Event(e, calendar); // event part
            ep.part.dayIndex = row.data.i;
            //ep.part.start = row.data.startTicks < startTicks ? ep.start() : row.data.start;
            ep.part.start = row.data.startTicks < startTicks ? start : row.data.start;
            //ep.part.end = row.data.endTicks > endTicks ? ep.end() : row.data.end;
            ep.part.end = row.data.endTicks > endTicks ? end : row.data.end;

            var partStartPixels = this.getPixels(ep.part.start.addTime(-row.data.offset));
            var partEndPixels = this.getPixels(ep.part.end.addTime(-row.data.offset));
            //if (ep.part.start.getTime() === ep.part.end.getTime()) {
            if (ep.part.start.ticks === ep.part.end.ticks) {
                partEndPixels = this.getPixels(ep.part.end.addTime(-row.data.offset).addTime(1));
            }

            if (cache && cache.height) {
                ep.part.height = cache.height;
            }

            var left = partStartPixels.left;
            var right = partEndPixels.left;

            // events in the hidden areas
            if (left === right && (partStartPixels.cut || partEndPixels.cut)) {
                return null;
            }

            ep.part.box = resolved.useBox(endTicks - startTicks);

            var milestoneWidth = calendar.eventHeight;

            if (e.type === "Milestone") {
                var width = e.width || milestoneWidth;
                ep.part.end = ep.part.start;
                ep.part.left = left - width /2;
                ep.part.width = width;
                ep.part.barLeft = 0;
                ep.part.barWidth = width;
            }
            else if (ep.part.box) {
                var boxLeft = partStartPixels.boxLeft;
                var boxRight = partEndPixels.boxRight;
                //var itc = this._getItlineCellFromPixels()

                //ep.part.left = Math.floor(left / this.cellWidth) * this.cellWidth;
                ep.part.left = boxLeft;
                ep.part.width = boxRight - boxLeft;
                ep.part.barLeft = Math.max(left - ep.part.left, 0);  // minimum 0
                ep.part.barWidth = Math.max(right - left, 1);  // minimum 1
            }
            else {
                ep.part.left = left;
                ep.part.width = Math.max(right - left, 0);
                ep.part.barLeft = 0;
                ep.part.barWidth = Math.max(right - left - 1, 1);
            }

            if (typeof calendar.onEventFilter === "function" && calendar.events._filterParams) {
                var args = {};
                args.filter = calendar.events._filterParams;
                args.visible = true;
                args.e = ep;

                calendar.onEventFilter(args);

                if (!args.visible) {
                    return null;
                }
            }

            row.events.push(ep);

            return ep;

        };

        this._eventComparer = function(a, b) {
            if (!a || !b || !a.start || !b.start) {
                return 0; // no sorting, invalid arguments
            }

            var byStart = a.start().ticks - b.start().ticks;
            if (byStart !== 0) {
                return byStart;
            }

            var byEnd = b.end().ticks - a.end().ticks; // desc
            return byEnd;
        };

        this._eventComparerCustom = function(a, b) {
            if (!a || !b) {
                return 0; // no sorting, invalid arguments
            }

            if (!a.data || !b.data || !a.data.sort || !b.data.sort || a.data.sort.length === 0 || b.data.sort.length === 0) { // no custom sorting, using default sorting (start asc, end asc);
                return calendar._eventComparer(a, b);
            }

            var result = 0;
            var i = 0;
            while (result === 0 && a.data.sort[i] && b.data.sort[i]) {
                if (a.data.sort[i] === b.data.sort[i]) {
                    result = 0;
                }
                else {
                    result = calendar._stringComparer(a.data.sort[i], b.data.sort[i], calendar.sortDirections[i]);
                }
                i++;
            }

            return result;
        };

        this._stringComparer = function(a, b, direction) {
            var asc = (direction !== "desc");
            var aFirst = asc ? -1 : 1;
            var bFirst = -aFirst;

            if (a === null && b === null) {
                return 0;
            }
            // nulls first
            if (b === null) { // b is smaller
                return bFirst;
            }
            if (a === null) {
                return aFirst;
            }

            //return asc ? a.localeCompare(a, b) : -a.localeCompare(a, b);

            var ar = [];
            ar[0] = a;
            ar[1] = b;

            ar.sort();

            return a === ar[0] ? aFirst : bFirst;
        };
        
        
        this._rowSelectDispatch = function(row, ctrl, shift, meta) {

            if (calendar._api2()) {

                var index = DayPilot.indexOf(calendar.rowlist, row);
                var e = calendar._createRowObject(row, index);
                var selected = DayPilot.indexOf(rowtools.selected, row) !== -1;
                var change = selected ? "deselected" : "selected";

                var args = {};
                args.row = e;
                args.selected = selected;
                args.ctrl = ctrl;
                args.shift = shift;
                args.meta = meta;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onRowSelect === 'function') {
                    calendar.onRowSelect(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (calendar.rowSelectHandling) {
                    case 'PostBack':
                        calendar.rowSelectPostBack(e, change);
                        break;
                    case 'CallBack':
                        calendar.rowSelectCallBack(e, change);
                        break;
                    case 'Update':
                        rowtools.select(row, ctrl, shift, meta);
                        break;
                }
                
                if (typeof calendar.onRowSelected === 'function') {
                    args.selected = DayPilot.indexOf(rowtools.selected, row) !== -1;
                    calendar.onRowSelected(args);
                }                
                
            }
            else {
                rowtools.select(row, ctrl, shift);

                var index = DayPilot.indexOf(calendar.rowlist, row);
                var e = calendar._createRowObject(row, index);
                var selected = DayPilot.indexOf(rowtools.selected, row) !== -1;
                var change = selected ? "deselected" : "selected";

                switch (calendar.rowSelectHandling) {
                    case 'PostBack':
                        calendar.rowSelectPostBack(e, change);
                        break;
                    case 'CallBack':
                        calendar.rowSelectCallBack(e, change);
                        break;
                    case 'JavaScript':
                        calendar.onRowSelect(e, change);
                        break;
                }
            }

        };
        
        this.rowSelectPostBack = function(r, change, data) {
            var params = {};
            params.resource = r;
            params.change = change;
            this._postBack2('RowSelect', params, data);
        };
        
        this.rowSelectCallBack = function(r, change, data) {
            var params = {};
            params.resource = r;
            params.change = change;
            this._callBack2('RowSelect', params, data);
        };
        
        this.rows = {};
        this.rows.selection = {};

        var rowsel = this.rows.selection;

        rowsel.get = function() {
            var list = [];
            DayPilot.list(rowtools.selected).each(function(item) {
                list.push(calendar._createRowObject(item));
            });
            return list;
        };

        rowsel.clear = function() {
            rowtools.clearSelection();
        };

        this.rows.all = function() {
            var list = [];
            for(var i = 0; i < calendar.rowlist.length; i++) {
                var r = calendar._createRowObject(calendar.rowlist[i]);
                list.push(r);
            }
            return rowArray(list);

        };

        this.rows.each = function(f) {
            calendar.rows.all().each(f);
        };

        this.rows.filter = function(param) {
            calendar.rows._filterParams = param;
            calendar._update({"immediateEvents": true});
        };

        this.rows.find = function(id, start) {
            var rows = calendar.rows.all();
            for (var i = 0; i < rows.length; i++) {
                var item = rows[i];
                if (item.id === id) {
                    if (typeof start === "string") {
                        if (start === item.start.toString()) {
                            return item;
                        }
                    }
                    else {
                        return item;
                    }
                }
            }
        };

        this.rows.load = function(url) {
            DayPilot.ajax({
                "url": url,
                "success": function(args) {
                    var r = args.request;
                    var data = eval("(" + r.responseText + ")");
                    if (DayPilot.isArray(data)) {
                        calendar.resources = data;
                        if (calendar._initialized) {
                            calendar.update();
                        }
                    }
                }
            });
        };

        this.rows.expand = function(levels) {
            var rows = [];
            var level = levels || 1;
            for (var i = 0; i < calendar.rowlist.length; i++) {
                var row = calendar.rowlist[i];
                var withinLevel = level === -1;
                if (row.level < level) {
                    withinLevel = true;
                }
                if (withinLevel && !row.expanded && row.children && row.children.length > 0) {
                    rows.push(row.index);
                }
            }
            if (rows.length === 0) {
                return;
            }
            if (rows.length === 1) {
                calendar._toggle(rows[0]);
            }
            else {
                for (var i = 0; i < rows.length; i++) {
                    var index = rows[i];
                    var row = calendar.rowlist[index];
                    row.expanded = true;
                }
                calendar._update();
            }
        };

        this.rows.expandAll = function() {
            calendar.rows.expand(-1);
        };


        this.rows.headerHide = function() {
            calendar._rowHeaderHidden = true;
            calendar._updateRowHeaderWidthOuter();
            calendar._updateAutoCellWidth();
        };

        this.rows.headerShow = function() {
            calendar._rowHeaderHidden = false;
            calendar._updateRowHeaderWidthOuter();
            calendar._updateAutoCellWidth();
        };

        this.rows.headerToggle = function() {
            if (calendar._rowHeaderHidden) {
                calendar.rows.headerShow();
            }
            else {
                calendar.rows.headerHide();
            }
        };

        this._rowMoveDispatch = function() {
            var source = rowmoving.source;
            var target = rowmoving.target;
            var position = rowmoving.position;

            rowtools.resetMoving();

            if (calendar._api2()) {
                var args  = {};
                args.source = calendar._createRowObject(source);
                args.target = calendar._createRowObject(target);
                args.position = position;
                args.preventDefault = function() {
                    this.preventDefault.value = true;
                };

                if (typeof calendar.onRowMove === "function") {
                    calendar.onRowMove(args);
                    if (args.preventDefault.value) {
                        return;
                    }
                }

                switch (calendar.rowMoveHandling) {
                    case "Update":
                        rowtools.move(args);
                        break;
                    case "CallBack":
                        calendar.rowMoveCallBack(args.source, args.target, args.position);
                        break;
                    case "PostBack":
                        calendar.rowMovePostBack(args.source, args.target, args.position);
                        break;
                    case "Notify":
                        rowtools.move(args);
                        calendar.rowMoveNotify(args.source, args.target, args.position);
                        break;
                }

                if (typeof calendar.onRowMoved === "function") {
                    calendar.onRowMoved(args);
                }
            }
            else {
                //rowtools.move(args);

                var source = calendar._createRowObject(source);
                var target = calendar._createRowObject(target);
                var position = position;

                switch (calendar.rowMoveHandling) {
                    case "CallBack":
                        calendar.rowMoveCallBack(source, target, position);
                        break;
                    case "PostBack":
                        calendar.rowMovePostBack(source, target, position);
                        break;
                    case "JavaScript":
                        calendar.onRowMove(source, target, position);
                        break;
                }

            }

        };

        /**
         * @param rows Array of strings (row IDs)
         * @private
         */
        this._loadSelectedRows = function(rows) {
            var list = DayPilot.list(rows);
            rowtools.selected = [];
            list.each(function(item) {
                var row = calendar._findRowByResourceId(item);
                if (row) {
                    rowtools.selected.push(row);
                }
            });
            rowtools._updateHighlighting();
        };

        this._rowtools = {};
        var rowtools = this._rowtools;
        
        rowtools.edit = function(row) {
            var input = rowtools._textarea(row);
        };

        /*
        rowtools.findFirst = function(ro) {
            if (ro._row) {
                return ro._row;
            }
            return calendar._findRowByResourceId(ro.id);
        };
        */

        rowtools.createOverlay = function(row) {

            var width = calendar._getTotalRowHeaderWidth();
            var css = calendar._prefixCssClass("_rowmove_source");

            var div = DayPilot.Util.div(calendar.divHeader, 0, row.top, width, row.height);
            div.className = css;

            row.moveOverlay = div;

                /*
            var div = document.createElement("div");
            div.style.position = "absolute";
            div.style.left = "0px";
            div.style.right = "0px";
            div.style.top = "0px";
            div.style.bottom = "0px";

            div.className = calendar._prefixCssClass("_rowmove_source");


            var r = rowtools._findTableRow(row);
            var c = r.cells[0];
            c.firstChild.appendChild(div);

            c.movingoverlay = div;
            */
        };

        rowtools.deleteOverlay = function(row) {
            DayPilot.de(row.moveOverlay);
            row.moveOverlay = null;

            /*
            var r = rowtools._findTableRow(row);
            var c = r.cells[0];
            if (c.movingoverlay) {
                DayPilot.de(c.movingoverlay);
            }*/
        };

        rowtools._textarea = function(row) {
            var r = rowtools._findTableRow(row);
            var c = r.cells[0];
            
            if (c.input) {
                return c.input;
            }
            
            var width = c.clientWidth;
            if (row.isNewRow) {
                width = calendar._getOuterRowHeaderWidth();
            }

            var input = document.createElement("textarea");
            input.style.position = "absolute";
            input.style.top = "0px";
            input.style.left = "0px";
            input.style.width = width + "px";
            input.style.height = row.height + "px";
            input.style.border = "0px none";
            input.style.overflow = "hidden";
            input.style.boxSizing = "border-box";
            input.style.resize = "none";
            //input.style.lineHeight = row.height + "px";
            
            var object = c.textDiv;
            input.style.fontFamily = DayPilot.gs(object, 'fontFamily') || DayPilot.gs(object, 'font-family');
            input.style.fontSize = DayPilot.gs(object, 'fontSize') || DayPilot.gs(object, 'font-size');
            
            input.value = row.html;
            c.firstChild.appendChild(input);
            c.input = input;
            
            var remove = function() {
                try {
                    c.input.parentNode.removeChild(c.input);
                }
                catch(e) {
                    doNothing();
                }
                c.input = null;
            };

            input.focus();
            input.onblur = function() {
                input.onblur = null;
                var newText = input.value;
                var index = DayPilot.indexOf(calendar.rowlist, row);
                remove();
                if (!input.canceled) {
                    calendar._rowEditDispatch(row, newText);
                }
            };
            input.onkeydown = function(e) {
                var keynum = (window.event) ? event.keyCode : e.keyCode;
                
                if (keynum === 27) {
                    input.canceled = true;
                    remove();
                }
                if (keynum === 13) {
                    input.onblur();
                    return false;
                }  
                return true;
            };
            
            if (input.setSelectionRange) {
                input.setSelectionRange(0, 9999); // using this instead of .select() that is not fully supported in mobile Safari
            }
            else {
                input.select();
            }

            return input;
        };

        rowtools.selected = [];
        rowtools.select = function(row, ctrl, shift, meta) {

            var selected = DayPilot.indexOf(rowtools.selected, row) !== -1;

            if (ctrl || meta) {
                if (selected) {
                    rowtools.unselect(row);
                    DayPilot.rfa(rowtools.selected, row);
                    return;
                }
            }
            else {
                selected = false;
                rowtools.clearSelection();
            }

            rowtools._highlight(row);
            
            // selected list
            if (!selected) {
                rowtools.selected.push(row);
            }
            
        };
        
        rowtools._updateHighlighting = function() {
            for (var i = 0; i < rowtools.selected.length; i++) {
                var row = rowtools.selected[i];
                rowtools._highlight(row);
            }
        };
        
        rowtools._highlight = function(row) {
            // cells
            var y = DayPilot.indexOf(calendar.rowlist, row);
            if (y === -1) {  // not found, search using id
                var idrow = calendar._findRowByResourceId(row.id).index;
                if (idrow) {
                    y = idrow.index;
                }
                else {
                    return;
                }
            }

            var cl = calendar._prefixCssClass("_cell_selected");

            var cells = [];
            for(var i = 0; i < calendar.itline.length; i++) {
                var c = {};
                c.x = i;
                c.y = y;
                cells.push(c);
            }
            calendar.cells.findXy(cells).addClass(cl);
            
            // header
            var cl = calendar._prefixCssClass("_rowheader_selected");
            var table = calendar.divHeader;
            for (var y = 0; y < table.rows.length; y++) {
                var r = table.rows[y];
                if (r && r.cells[0] && r.cells[0].row === row) {
                    for (var x = 0; x < r.cells.length; x++) {
                        var c = r.cells[x];
                        var first = c.firstChild;
                        DayPilot.Util.addClass(first, cl);
                    }
                }
            }
        };
        
        rowtools._isSelected = function(id) {
            for (var i = 0; i < rowtools.selected.length; i++) {
                var row = rowtools.selected[i];
                if (row.id === id) {
                    return true;
                }
            }
            return false;
        };
        
        rowtools._getSelectedList = function() {
            var list = [];
            if (!rowtools.selected) {
                return list;
            }
            for(var i = 0; i < rowtools.selected.length; i++) {
                var row = rowtools.selected[i];
                var index = DayPilot.indexOf(calendar.rowlist, row);
                var r = calendar._createRowObject(row, index);
                list.push(r.toJSON());
            }
            return list;
        };
        
        rowtools.unselect = function(row) {
            // cells
            var cl = calendar._prefixCssClass("_cell_selected");
            var y = DayPilot.indexOf(calendar.rowlist, row);
            var cells = [];
            for(var i = 0; i < calendar.itline.length; i++) {
                var c = {};
                c.x = i;
                c.y = y;
                cells.push(c);
            }
            calendar.cells.findXy(cells).removeClass(cl);

            // header
            var cl = calendar._prefixCssClass("_rowheader_selected");
            var table = calendar.divHeader;
            var r = rowtools._findTableRow(row);
            if (r) {
                for (var x = 0; x < r.cells.length; x++) {
                    var c = r.cells[x];
                    var first = c.firstChild;
                    DayPilot.Util.removeClass(first, cl);
                }
            }
        };
        
        rowtools.clearSelection = function() {
            
            for(var j = 0; j < rowtools.selected.length; j++) {
                var row = rowtools.selected[j];
                rowtools.unselect(row);
            }
            
            // clear the list
            rowtools.selected = [];
        };
        
        rowtools._findTableRow = function(row) {
            var table = calendar.divHeader;
            for (var y = 0; y < table.rows.length; y++) {
                var r = table.rows[y];
                if (r && r.cells[0] && r.cells[0].row === row) {
                    return r;
                }
            }
            return null;
        };
        
        // helper
        rowtools.selectById = function(id) {
            var row = calendar._findRowByResourceId(id);
            if (row) {
                rowtools.select(row);
            }
        };

        rowtools.startMoving = function(row) {
            var rowmoving = DayPilot.Global.rowmoving;
            rowmoving.row = row;
            rowmoving.cursor = calendar.divResScroll.style.cursor;
            calendar.divResScroll.style.cursor = "move";

            rowtools.createOverlay(row);
        };

        rowtools.resetMoving = function() {
            calendar.divResScroll.style.cursor = rowmoving.cursor;
            DayPilot.de(rowmoving.div);
            rowtools.deleteOverlay(rowmoving.row);
            DayPilot.Global.rowmoving = rowmoving = {};
        };

        rowtools.move = function(args) {
            // modify .resources
            var source = args.source.$.row.resource;
            var target = args.target.$.row.resource;
            var position = args.position;

            if (position === "forbidden") {
                return;
            }

            // remove from source
            var sourceParent = restools.findParentArray(source);
            if (!sourceParent) {
                throw "Cannot find source node parent";
            }
            var sourceIndex = DayPilot.indexOf(sourceParent, source);
            sourceParent.splice(sourceIndex, 1);

            // move to target
            var targetParent = restools.findParentArray(target);
            if (!targetParent) {
                throw "Cannot find target node parent";
            }
            var targetIndex = DayPilot.indexOf(targetParent, target);

            switch (position) {
                case "before":
                    targetParent.splice(targetIndex, 0, source);
                    break;
                case "after":
                    targetParent.splice(targetIndex + 1, 0, source);
                    break;
                case "child":
                    if (!target.children) {
                        target.children = [];
                        target.expanded = true;
                    }
                    target.children.push(source);
                    break;
            }

            calendar.update();
        };

        var restools = {};

        restools.findParentArray = function(res) {
            return restools.findInArray(calendar.resources, res);
        };

        restools.findInArray = function(array, res) {
            if (DayPilot.indexOf(array, res) !== -1) {
                return array;
            }
            for(var i = 0; i < array.length; i++) {
                var r = array[i];
                if (r.children && r.children.length > 0) {
                    var parent = restools.findInArray(r.children, res);
                    if (parent) {
                        return parent;
                    }
                }
            }
            return null;
        };

        this._loadResources = function() {
            this.rowlist = [];
            //this.hasChildren = false;
            
            var resources = this.resources;

            var force = this._serverBased();
            if (!force) {
                if (this.viewType === "Gantt") {
                    resources = this._loadResourcesGantt();
                }
                else if (this.viewType === "Days") {
                    resources = this._loadResourcesDays();
                }
            }

            // pass by reference
            var index = {};
            index.i = 0;

            this._loadResourceChildren(resources, index, 0, null, this.treeEnabled, false);

            var newResourceRow = calendar.rowCreateHandling !== "Disabled";
            if (newResourceRow) {
                this._createNewResourceRow();
            }
            
            this._updateSelectedRows();
        };

        this._createNewResourceRow = function() {
            var r = {};
            r.id = "NEW";
            r.isNewRow = true;
            r.html = "";
            //r.moveEnabled = false;
            r.loaded = true;
            r.start = this.startDate;
            r.children = [];
            r.height = calendar.eventHeight;
            r.marginBottom = 0;
            r.marginTop = 0;
            r.getHeight = function() {
                return calendar.eventHeight + calendar.rowMarginBottom + calendar.rowMarginTop;
                //return Math.max(calendar.rowMinHeight, calendar.eventHeight + calendar.rowMarginBottom);
            };
            r.putIntoLine = function() {};
            r.resetEvents = function() {};

            this.rowlist.push(r);
        };
        
        this._updateSelectedRows = function() {
            var list = [];
            for (var i = 0; i < calendar.rowlist.length; i++) {
                var row = calendar.rowlist[i];
                var id = row.id;
                if (rowtools._isSelected(id)) {
                    list.push(row);
                }
            }
            rowtools.selected = list;
            
        };
        
        this._loadResourcesGantt = function() {
            var list = [];
            
            if (this._ganttAppendToResources && this.resources) {
                for (var i = 0; i < this.resources.length; i++) {
                    list.push(this.resources[i]);
                }
            }
            
            if (!this.events.list) {
                return;
            }
            
            //this.resources = [];
            for (var i = 0; i < this.events.list.length; i++) {
                var e = this.events.list[i];
                var r = {};
                r.id = e.id;
                r.name = e.text;
                list.push(r);
            }
            
            return list;
        };

        this._loadResourcesDays = function() {
            var list = [];
            var locale = this._resolved.locale();
            for (var i = 0; i < this.days; i++) {
                var d = this.startDate.addDays(i);
                var r = {};
                r.name = d.toString(locale.datePattern, locale);
                r.start = d;
                list.push(r);
            }
            return list;
        };
        
        this._visibleStart = function() {
            if (this.itline && this.itline.length > 0) {
                return this.itline[0].start;
            }
            return this.startDate;
        };
        
        this._visibleEnd = function() {
            if (this.itline && this.itline.length > 0) {
                return this.itline[this.itline.length - 1].end;
            }
            return this.startDate.addDays(this.days);
        };

        this._loadResourceChildren = function(resources, index, level, parent, recursively, hidden) {
            if (!resources) {
                return;
            }
            
            for (var i = 0; i < resources.length; i++) {
                if (!resources[i]) {
                    continue;
                }
                var additional = {};
                additional.level = level;
                additional.hidden = hidden;
                additional.index = index.i;

                //var res = this._createBeforeResHeaderRenderArgs(resources[i], additional);
                var res = this._doBeforeResHeaderRender(resources[i], additional);

                var row = {};

                // defined values

                row.backColor = res.backColor;
                row.cssClass = res.cssClass;
                row.expanded = res.expanded;
                row.name = res.name;
                row.html = res.html ? res.html : row.name;
                row.eventHeight = typeof res.eventHeight !== 'undefined' ? res.eventHeight : calendar._resolved.eventHeight();
                row.minHeight = typeof res.minHeight !== 'undefined' ? res.minHeight : calendar.rowMinHeight;
                row.marginBottom = typeof res.marginBottom !== 'undefined' ? res.marginBottom : calendar.rowMarginBottom;
                row.marginTop = typeof res.marginTop !== 'undefined' ? res.marginTop : calendar.rowMarginTop;
                row.loaded = !res.dynamicChildren;  // default is true
                row.id = res.id || res.value;  // accept both id and value
                row.toolTip = res.toolTip;
                row.children = [];
                row.columns = [];
                row.start = res.start ? new DayPilot.Date(res.start) : this._visibleStart();
                row.isParent = res.isParent;
                row.contextMenu = res.contextMenu ? eval(res.contextMenu) : this.contextMenuResource;
                row.areas = res.areas;
                row.moveDisabled = res.moveDisabled;

                // gantt
                row.task = res.task;

                // calculated
                row.height = row.eventHeight;  // TODO might not be necessary
                row.hidden = hidden;
                row.level = level;
                row.index = index.i;
                
                // reference to resource
                row.resource = resources[i];

                // event ordering
                row.lines = [];
                row.blocks = [];

                row.isRow = true;

                // functions
                row.getHeight = function() {
                    var height = 0;
                    if (calendar.groupConcurrentEvents) {
                        for (var i = 0; i < this.blocks.length; i++) {
                            var block = this.blocks[i];
                            height = Math.max(height, block.getHeight());
                        }
                    }
                    else {
                        if (this.lines.length > 0) {
                            var last = this.lines.length - 1;
                            var line = this.lines[last];
                            var lheight = line.height || this.eventHeight;
                            var top = line.top || 0;
                            height = top + lheight;
                        }
                    }
                    if (height === 0) {
                        height = this.eventHeight;
                    }
                    return (height > this.minHeight) ? height : this.minHeight;
                };

                row.resetEvents = function() {
                    var r = this;
                    r.events = [];
                    r.events.forRange = function(start, end) {
                        var result = [];
                        for (var i = 0; i < r.events.length; i++) {
                            var ev = r.events[i];
                            if (DayPilot.Util.overlaps(ev.start, ev.end, start, end)) {
                                result.push(ev);
                            }
                        }
                        return result;
                    };
                };

                row.calculateUtilization = function() {
                    var r = this;

                    var sections = r.sections = getSections();
                    for (var i = 0; i < sections.length; i++) {
                        var section = sections[i];
                        section.events = [];
                        //var test = section.start.addTime(1);
                        for (var x = 0; x < r.events.length; x++) {
                            var e = r.events[x];
                            if (DayPilot.Util.overlaps(section.start, section.end, e.start(), e.end())) {
                                section.events.push(e);
                            }
                        }
                        section.sum = function(name) {
                            var sum = 0;
                            for (var i = 0; i < this.events.length; i++) {
                                var e = this.events[i];
                                var value = e.data[name];
                                if (typeof value === 'number') {
                                    sum += value;
                                }
                            }
                            return sum;
                        }
                    }

                    function getPoints() {
                        var points = [];
                        for (var i = 0; i < r.events.length; i++) {
                            var e = r.events[i];
                            if (!DayPilot.contains(points, e.start().toString())) {
                                points.push(e.start().toString());
                            }
                            if (!DayPilot.contains(points, e.end().toString())) {
                                points.push(e.end().toString());
                            }
                        }

                        points.sort();
                        return points;
                    }

                    function getSections() {
                        var points = getPoints();
                        var sections = [];

                        var section = { "start": r.data.start};
                        for (var i = 0; i < points.length; i++) {
                            section.end = new DayPilot.Date(points[i]);
                            sections.push(section);
                            section = { "start": new DayPilot.Date(points[i])};
                        }
                        section.end = r.data.end;
                        sections.push(section);

                        sections.forRange = function(start, end) {
                            var list = [];
                            for (var i = 0; i < this.length; i++) {
                                var section = this[i];
                                if (DayPilot.Util.overlaps(start, end, section.start, section.end)) {
                                    list.push(section);
                                }
                            }
                            list.maxSum = function(name) {
                                var max = 0;
                                for (var i = 0; i < this.length; i++) {
                                    var section = this[i];
                                    var sum = section.sum(name);
                                    if (sum > max) {
                                        max = sum;
                                    }
                                }
                                return max;
                            };
                            return list;
                        };

                        return sections;
                    }
                };

                row.putIntoLine = function(ep) {
                    var thisRow = this;

                    for (var i = 0; i < this.lines.length; i++) {
                        var line = this.lines[i];
                        if (line.isFree(ep.part.left, ep.part.width)) {
                            line.add(ep);
                            return i;
                        }
                    }

                    var line = [];
                    line.height = 0;
                    line.add = function(ep) {
                        this.push(ep);
                        if (ep.part.height > line.height) {
                            line.height = ep.part.height;
                        }
                    };
                    line.isFree = function(colStart, colWidth, except) {
                        //var free = true;
                        var end = colStart + colWidth - 1;
                        var max = this.length;

                        for (var i = 0; i < max; i++) {
                            var e = this[i];
                            if (!(end < e.part.left || colStart > e.part.left + e.part.width - 1)) {
                                if (except && except === e.data) {
                                    continue;
                                }
                                return false;
                            }
                        }

                        return true;
                    };

                    line.add(ep);

                    this.lines.push(line);

                    return this.lines.length - 1;
                };

                row.putIntoBlock = function(ep) {

                    for (var i = 0; i < this.blocks.length; i++) {
                        var block = this.blocks[i];
                        if (DayPilot.indexOf(block.events, ep) !== -1) {
                            return;
                        }
                        if (block.overlapsWith(ep.part.left, ep.part.width)) {
                            //block.putIntoLine(ep);
                            block.events.push(ep);
                            ep.part.block = block;
                            block.min = Math.min(block.min, ep.part.left);
                            block.max = Math.max(block.max, ep.part.left + ep.part.width);
                            return i;
                        }
                    }

                    // no suitable block found, create a new one
                    var block = {};
                    block.expanded = false;
                    block.row = this;
                    block.events = [];
                    block.lines = [];

                    block.putIntoLine = function(ep) {
                        var thisCol = this;

                        for (var i = 0; i < this.lines.length; i++) {
                            var line = this.lines[i];
                            if (line.isFree(ep.part.left, ep.part.width)) {
                                line.add(ep);
                                return i;
                            }
                        }

                        var line = [];
                        line.height = 0;
                        line.add = function(ep) {
                            this.push(ep);
                            if (ep.part.height > line.height) {
                                line.height = ep.part.height;
                            }
                        };
                        line.isFree = function(start, width) {
                            //var free = true;
                            var end = start + width - 1;
                            var max = this.length;

                            for (var i = 0; i < max; i++) {
                                var e = this[i];
                                if (!(end < e.part.left || start > e.part.left + e.part.width - 1)) {
                                    return false;
                                }
                            }

                            return true;
                        };

                        line.add(ep);

                        this.lines.push(line);

                        //return this.lines.length - 1;

                    };

                    block.overlapsWith = function(start, width) {
                        var end = start + width - 1;

                        if (!(end < this.min || start > this.max - 1)) {
                            return true;
                        }

                        return false;
                    };

                    block.getHeight = function() {
                        if (!this.expanded) {
                            return calendar.eventHeight;
                        }
                        if (this.lines.length > 0) {
                            var last = this.lines.length - 1;
                            var line = this.lines[last];
                            var lheight = line.height || calendar.eventHeight;
                            var top = line.top || 0;
                            return top + lheight;
                        }
                    };

                    //block.putIntoLine(ep);
                    block.events.push(ep);
                    ep.part.block = block;
                    block.min = ep.part.left;
                    block.max = ep.part.left + ep.part.width;

                    this.blocks.push(block);

                    //return this.blocks.length - 1;
                };

                this.rowlist.push(row);

                if (typeof calendar.onRowFilter === "function" && calendar.rows._filterParams) {
                    var args = {};
                    args.visible = !hidden;
                    args.row = calendar._createRowObject(row);
                    args.filter = calendar.rows._filterParams;

                    calendar.onRowFilter(args);

                    if (!args.visible) {
                        row.hidden = true;
                        //hidden = true; // children
                    }
                }

                if (parent !== null) {
                    parent.children.push(index.i);
                }

                if (res.columns) {
                    for (var j = 0; j < res.columns.length; j++) {
                        row.columns.push(res.columns[j]); // plain copy, it's the same structure
                    }
                }

                index.i++;

                if (recursively && res.children && res.children.length) {
                    //this.hasChildren = true;
                    var hiddenChildren = row.hidden || !row.expanded;
                    this._loadResourceChildren(res.children, index, level + 1, row, true, hiddenChildren);
                }
            }
        };

        this._doBeforeRowHeaderRender = function(row) {
            if (row.isNewRow) {
                return {
                    "row": {
//                        "children": [],
                        "cssClass": calendar._prefixCssClass("_row_new"),
                        "moveDisabled": true,
                        "html": ""
                    }
                };
            }


            var args = {};
            args.row = this._createRowObject(row);
            //args.client = {};

            DayPilot.Util.copyProps(row, args.row, ['html', 'backColor', 'cssClass', 'toolTip', 'contextMenu', 'moveDisabled']);
            args.row.columns = DayPilot.Util.createArrayCopy(row.columns, ['html']);
            args.row.areas = DayPilot.Util.createArrayCopy(row.areas);

            if (typeof args.row.columns === 'undefined' && calendar.rowHeaderColumns && calendar.rowHeaderColumns.length > 0) {
                r.columns = [];
                for (var i = 0; i < calendar.rowHeaderColumns.length; i++) {
                    r.columns.push({});
                }
            }

            if (typeof this.onBeforeRowHeaderRender === "function") {
                this.onBeforeRowHeaderRender(args);
            }
            return args;
        };

        this._doBeforeResHeaderRender = function(res, additional) {
            var r = this._createBeforeResHeaderRenderArgs(res, additional);

            if (typeof this.onBeforeResHeaderRender === 'function') {
                var args = {};
                args.resource = r;
                this.onBeforeResHeaderRender(args);
            }
            return r;
        };
        
        this._createBeforeResHeaderRenderArgs = function(res, additional) {
            var r = {};

            // extra properties like level, index, hidden
            for (var name in additional) {
                r[name] = additional[name];
            }

            // shallow copy
            // TODO resolve children, columns
            for (var name in res) {
                r[name] = res[name];
            }
            
            if (typeof res.html === 'undefined') {
                r.html = res.name;
            }

            if (typeof r.columns === 'undefined' && calendar.rowHeaderColumns && calendar.rowHeaderColumns.length > 0) {
                r.columns = [];
                for (var i = 0; i < calendar.rowHeaderColumns.length; i++) {
                    r.columns.push({});
                }
            }

            return r;
        };


        this._initPrepareDiv = function() {
            this._loadTop();
            this.nav.top.dp = this;
            this.nav.top.innerHTML = "";  // TODO remove
            if (!this.cssOnly) {
                this.nav.top.style.border = "1px solid " + this.borderColor;
            }
            else {
                DayPilot.Util.addClass(this.nav.top, this._prefixCssClass("_main"));
            }

            if (DayPilot.browser.ie9) {
                DayPilot.Util.addClass(this.nav.top, this._prefixCssClass("_browser_ie9"));
            }
            if (DayPilot.browser.ie8) {
                DayPilot.Util.addClass(this.nav.top, this._prefixCssClass("_browser_ie8"));
            }

            this.nav.top.style.MozUserSelect = 'none';
            this.nav.top.style.KhtmlUserSelect = 'none';
            this.nav.top.style.webkitUserSelect = 'none';
            if (this.width) {
                this.nav.top.style.width = this.width;
            }
            if (this.heightSpec === "Parent100Pct") {
                this.nav.top.style.height = "100%";
            }
            this.nav.top.style.lineHeight = "1.2";
            this.nav.top.style.position = "relative";
            
            /*
            // moved to maind
            this.nav.top.onmousemove = function(ev) {
                ev = ev || window.event;
                ev.insideMainD = true;
                if (window.event) {
                    window.event.srcElement.inside = true;
                }
            };
            */

            this.nav.top.onmousemove = this._onTopMouseMove;

            this.nav.top.ontouchstart = touch.onMainTouchStart;
            this.nav.top.ontouchmove = touch.onMainTouchMove;
            this.nav.top.ontouchend = touch.onMainTouchEnd;

            if (this.hideUntilInit && this.backendUrl) {
                this.nav.top.style.visibility = 'hidden';
            }
            var rowHeaderWidth = this._getOuterRowHeaderWidth();

            var layout = this._resolved.layout();
            if (layout === 'DivBased') {
                // left
                var left = document.createElement("div");
                //left.style.cssFloat = "left";
                //left.style.styleFloat = "left";  // IE7
                left.style.position = "absolute";
                left.style.left = "0px";
                left.style.width = (rowHeaderWidth) + "px";

                left.appendChild(this._drawCorner());

                // divider horizontal
                var dh1 = document.createElement("div");
                dh1.style.height = "1px";
                dh1.className = this._prefixCssClass("_divider_horizontal");
                if (!this.cssOnly) {
                    dh1.style.backgroundColor = this.borderColor;
                }
                left.appendChild(dh1);
                this.nav.dh1 = dh1;

                left.appendChild(this._drawResScroll());
                this.nav.left = left;

                // divider                
                var divider = document.createElement("div");
                divider.style.position = "absolute";
                divider.style.left = (rowHeaderWidth) + "px";
                //divider.style.cssFloat = "left";
                //divider.style.styleFloat = "left";  // IE7
                divider.style.width = resolved.splitterWidth() + "px";
                divider.style.height = (this._getTotalHeaderHeight() + this._getScrollableHeight()) + "px";
                divider.className = this._prefixCssClass("_divider") + " " + this._prefixCssClass("_splitter");  // TODO _divider is obsolete
                divider.setAttribute("unselectable", "on");
                if (!this.cssOnly) {
                    divider.style.backgroundColor = this.borderColor;
                }
                this.nav.divider = divider;

                // maybe not the best place
                if (this.rowHeaderScrolling) {
                    this._activateSplitter();
                }

                // right
                var right = document.createElement("div");
                right.style.marginLeft = (rowHeaderWidth + resolved.splitterWidth()) + "px";
                right.style.marginRight = '1px';
                right.style.position = 'relative';

                right.appendChild(this._drawTimeHeaderDiv());
                this.nav.right = right;

                // divider horizontal #2
                var dh2 = document.createElement("div");
                dh2.style.height = "1px";
                dh2.style.position = "absolute";
                dh2.style.top = this._getTotalHeaderHeight() + "px";
                dh2.style.width = "100%";
                dh2.className = this._prefixCssClass("_divider_horizontal");
                if (!this.cssOnly) {
                    dh2.style.backgroundColor = this.borderColor;
                }
                right.appendChild(dh2);
                this.nav.dh2 = dh2;

                right.appendChild(this._drawMainContent());

                // clear
                var clear = document.createElement("div");
                clear.style.clear = 'left';

                // add all at once
                this.nav.top.appendChild(left);
                this.nav.top.appendChild(divider);
                this.nav.top.appendChild(right);
                this.nav.top.appendChild(clear);
            }
            else {
                var table = document.createElement("table");
                table.cellPadding = 0;
                table.cellSpacing = 0;
                table.border = 0;

                // required for proper width measuring (onresize)
                table.style.position = 'absolute';
                if (!this.cssOnly) {
                    table.style.backgroundColor = this.hourNameBackColor;
                }

                var row1 = table.insertRow(-1);

                var td1 = row1.insertCell(-1);
                td1.appendChild(this._drawCorner());

                var td2 = row1.insertCell(-1);
                td2.appendChild(this._drawTimeHeaderDiv());

                var row2 = table.insertRow(-1);

                var td3 = row2.insertCell(-1);
                td3.appendChild(this._drawResScroll());

                var td4 = row2.insertCell(-1);
                td4.appendChild(this._drawMainContent());

                this.nav.top.appendChild(table);
            }

            // hidden fields
            this._vsph = document.createElement("div");
            //this.vsph.id = this.id + "_vsph";
            this._vsph.style.display = "none";
            this.nav.top.appendChild(this._vsph);

            if (this._isAspnetWebForms()) {
                var stateInput = document.createElement("input");
                stateInput.type = "hidden";
                stateInput.id = this.id + "_state";
                stateInput.name = this.id + "_state";
                this.nav.state = stateInput;
                this.nav.top.appendChild(stateInput);
            }

            var margin = 5;

            var loading = document.createElement("div");
            loading.style.position = 'absolute';
            loading.style.left = (this._getOuterRowHeaderWidth() + resolved.splitterWidth() + 5) + "px";
            loading.style.top = (this._getTotalHeaderHeight() + 5) + "px";
            loading.style.display = 'none';
            if (!this.cssOnly) {
                loading.style.backgroundColor = this.loadingLabelBackColor;
                loading.style.fontSize = this.loadingLabelFontSize;
                loading.style.fontFamily = this.loadingLabelFontFamily;
                loading.style.color = this.loadingLabelFontColor;
                loading.style.padding = '2px';
            }
            loading.innerHTML = this.loadingLabelText;

            DayPilot.Util.addClass(loading, this._prefixCssClass("_loading"));

            this.nav.loading = loading;
            this.nav.top.appendChild(loading);

            this._drawRowHeaderHideIcon();
        };

        this._onTopMouseMove = function(ev) {
            if (rowmoving.row) {
                var coords = DayPilot.mo3(calendar.divHeader, ev);
                var ri = calendar._getRow(coords.y);
                var row = calendar.rowlist[ri.i];
                if (row.isNewRow) {
                    return;
                }
                var relative = coords.y - ri.top;
                var rowheight = ri.bottom - ri.top;
                var third1 = rowheight/3;
                var third2 = third1*2;
                var mid = rowheight/2;
                var position = "before";
                var hasChildren = row.children && row.children.length > 0;
                var sourceInParents = (function() {
                    var i = ri.i;
                    var lastlevel = row.level;
                    while (i >= 0) {
                        var r = calendar.rowlist[i];
                        i--;

                        if (lastlevel <= r.level) {
                            continue;
                        }
                        if (r === rowmoving.row) {
                            return true;
                        }
                        if (r.level === 0) {
                            return false;
                        }
                        lastlevel = r.level;
                    }
                    return false;
                })();
                
                var childEnabled = true;
                if (sourceInParents || ri.i === rowmoving.row.index) {
                    position = "forbidden";
                }
                else if (childEnabled) {
                    if (hasChildren) {
                        if (relative < mid) {
                            position = "before";
                        }
                        else {
                            position = "child";
                        }
                    }
                    else {
                        if (relative < third1) {
                            position = "before";
                        }
                        else if (relative < third2) {
                            position = "child";
                        }
                        else {
                            position = "after";
                        }
                    }
                }
                else {
                    if (hasChildren) {
                        position = "before";
                    }
                    else {
                        if (relative < mid) {
                            position = "before";
                        }
                        else {
                            position = "after";
                        }
                    }
                }

                if (rowmoving.row.moveDisabled) {
                    position = "forbidden";
                }

                rowmoving.calendar = calendar;
                rowmoving.source = rowmoving.row;
                rowmoving.target = calendar.rowlist[ri.i];
                rowmoving.position = position;

                var changed = (function() {
                    if (!rowmoving.last) {
                        return true;
                    }
                    if (rowmoving.last.target !== rowmoving.target) {
                       return true;
                    }
                    if (rowmoving.last.position !== rowmoving.position) {
                        return true;
                    }
                    return false;
                })();

                if (changed) {
                    if (typeof calendar.onRowMoving === 'function') {
                        var args = {};
                        args.source = calendar._createRowObject(rowmoving.source);
                        args.target = calendar._createRowObject(rowmoving.target);
                        args.position = position;

                        calendar.onRowMoving(args);

                        rowmoving.position = args.position;
                    }
                }
                else if (rowmoving.last) {
                    rowmoving.position = rowmoving.last.position;
                }

                rowmoving.last = {};
                rowmoving.last.target = rowmoving.target;
                rowmoving.last.position = rowmoving.position;

                (function drawRowPosition() {
                    if (rowmoving.div) {
                        DayPilot.de(rowmoving.div);
                    }
                    var top = ri.top;
                    var pos = rowmoving.position;
                    var ro = calendar.rowlist[ri.i];
                    var level = ro.level;
                    var left = level * calendar.treeIndent;

                    switch (pos) {
                        case "before":
                            top = ri.top;
                            break;
                        case "child":
                            top = ri.top + mid;
                            //left += calendar.treeIndent;
                            break;
                        case "after":
                            top = ri.bottom;
                            break;
                        case "forbidden":
                            top = ri.top + mid;
                            break;
                    }

                    var width = calendar._getTotalRowHeaderWidth() - left;

                    var position = document.createElement("div");
                    position.style.position = "absolute";
                    position.style.left = left + "px";
                    position.style.width = width + "px";
                    //position.style.height = "2px";
                    position.style.top = top + "px";
                    //position.style.backgroundColor = "#999";

                    position.className = calendar._prefixCssClass("_rowmove_position_" + pos);

                    rowmoving.div = position;
                    calendar.divResScroll.appendChild(position);

                    /*
                    if (pos === 'child') {
                        var plus = document.createElement("div");
                        plus.style.position = "absolute";
                        plus.style.left = left + "px";
                        plus.style.top = top + "px";
                        plus.style.color = "#999";
                        plus.innerHTML = "+";
                        calendar.divResScroll.appendChild(plus);

                        rowmoving.div = [];
                        rowmoving.div.push(position);
                        rowmoving.div.push(plus);
                    }
                    else if (pos === 'forbidden') {
                        var plus = document.createElement("div");
                        plus.style.position = "absolute";
                        plus.style.left = left + "px";
                        plus.style.top = top + "px";
                        plus.style.color = "red";
                        plus.innerHTML = "x";
                        calendar.divResScroll.appendChild(plus);

                        rowmoving.div = [];
                        rowmoving.div.push(position);
                        rowmoving.div.push(plus);
                    }
                    */
                })();
            }
            else if (DayPilotScheduler.splitting) {
                var width = DayPilot.mo3(calendar.nav.top, ev).x;
                var max = calendar._getTotalRowHeaderWidth();
                var newWidth = Math.min(max, width - 1);
                calendar.rowHeaderWidth = newWidth;
                calendar._rowHeaderHidden = false;
                calendar._updateRowHeaderWidthOuter();
            }
        };
        

        // update all positions that depend on header height
        this._updateHeaderHeight = function() {
            var height = this._getTotalHeaderHeight();

            this.nav.corner.style.height = (height) + "px";
            //this.divCorner.style.height = (height) + "px";

            this.divTimeScroll.style.height = height + "px";
            this.divNorth.style.height = height + "px";
            if (this.nav.dh1 && this.nav.dh2) {
                this.nav.dh1.style.top = height + "px";
                this.nav.dh2.style.top = height + "px";
            }

            this.nav.loading.style.top = (height + 5) + "px";
            this.nav.scroll.style.top = (height + 1) + "px";
        };


        this._getOuterRowHeaderWidth = function() {
            if (this._rowHeaderHidden) {
                return 0;
            }

            var fixed = this.rowHeaderScrolling;
            if (fixed) {
                return this.rowHeaderWidth;
            }
            return this._getTotalRowHeaderWidth();
        };

        this._activateSplitter = function() {
            var div = this.nav.divider;
            div.style.cursor = "col-resize";
            div.setAttribute("unselectable", "on");

            div.onmousedown = function(ev) {
                var splitting = DayPilotScheduler.splitting = {};
                splitting.cursor = calendar.nav.top.style.cursor;
                splitting.cleanup = function() {
                    calendar.nav.top.style.cursor = splitting.cursor;
                    if (typeof calendar.onRowHeaderResized === "function") {
                        var args = {};
                        calendar.onRowHeaderResized(args);
                    }
                };
                calendar.nav.top.style.cursor = "col-resize";
                return false;
            };
        };

        this._updateRowHeaderWidthOuter = function() {
            var dividerWidth = resolved.splitterWidth();

            var width = this._getOuterRowHeaderWidth();
            this.nav.corner.style.width = width + "px";
            this.divCorner.style.width = width + "px";
            this.divResScroll.style.width = width + "px";
            this.nav.left.style.width = (width) + "px";
            this.nav.divider.style.left = (width) + "px";
            this.nav.right.style.marginLeft = (width + dividerWidth) + "px";
            if (this.nav.message) {
                this.nav.message.style.left = (width + dividerWidth) + "px";
            }
            if (this.nav.loading) {
                this.nav.loading.style.left = (width + dividerWidth + 5) + "px";
            }
            if (this.nav.hideIcon) {
                var hi = this.nav.hideIcon;
                var showCss = calendar._prefixCssClass("_header_icon_show");
                var hideCss = calendar._prefixCssClass("_header_icon_hide");
                hi.style.left = (width + dividerWidth - 1) + "px";
                if (calendar._rowHeaderHidden) {
                    DayPilot.Util.removeClass(hi, hideCss);
                    DayPilot.Util.addClass(hi, showCss);
                }
                else {
                    DayPilot.Util.removeClass(hi, showCss);
                    DayPilot.Util.addClass(hi, hideCss);
                }
            }
        };

        this._updateRowHeaderWidthInner = function() {
            this._loadRowHeaderColumns();
            var total = this._getTotalRowHeaderWidth();

            var updateCell = function(cell, width, left) {
                if (!cell || !cell.style) {
                    return;
                }
                var div = cell.firstChild;
                if (calendar._resHeaderDivBased) {
                    cell.style.width = width + "px";
                    div.style.width = width + "px";
                    if (typeof left === "number") {
                        cell.style.left = left + "px";
                    }
                }
                else {
                    div.style.width = width + "px";
                }
            };

            var table = this.divHeader;
            table.style.width = total + "px";

            var range = calendar._getAreaRowsWithMargin();

            for (var i = range.start; i < range.end; i++) {
                var row = table.rows[i];

                if (!row) {
                    continue;
                }

                var cell = row.cells[0];
                if (cell.colSpan > 1) {
                    var cell = row.cells[0];
                    updateCell(cell, total);
                }
                else {
                    if (this.rowHeaderCols) {
                        var left = 0;
                        for (var j = 0; j < row.cells.length; j++) {
                            var width = this.rowHeaderCols[j];
                            var cell = row.cells[j];
                            updateCell(cell, width, left);
                            left += width;
                        }
                    }
                    else {
                        var width = this.rowHeaderWidth;
                        var cell = row.cells[0];
                        updateCell(cell, width);
                    }
                }

            }

            if (calendar.nav.resScrollSpace) {
                calendar.nav.resScrollSpace.style.width = total + "px";
            }

            this._crosshairHide(); // update

        };

        this._updateRowHeaderWidth = function() {
            this._updateRowHeaderWidthOuter();
            this._updateRowHeaderWidthInner();
        };
        
        
        this._drawHeaderColumns = function() {
            var div = calendar.nav.corner;
            /*
            var sampleProps = [
                { title: 'Event', width: 150 },
                { title: 'Duration', width: 100 },
            ];
            */
            
            var props = this.rowHeaderColumns;

            var scroll = document.createElement("div");
            scroll.style.position = "absolute";
            scroll.style.bottom = "0px";
            scroll.style.left = "0px";
            scroll.style.width = "100%";
            scroll.style.height = resolved.headerHeight() + "px";
            scroll.style.overflow = "hidden";
            calendar.nav.columnScroll = scroll;

            var row = document.createElement("div");
            row.style.position = "absolute";
            row.style.bottom = "0px";
            row.style.left = "0px";
            row.style.width = "5000px";
            //row.style.width = "100%";
            row.style.height = resolved.headerHeight() + "px";
            row.style.overflow = "hidden";
            row.className = this._prefixCssClass("_columnheader");
            scroll.appendChild(row);

            var inner = document.createElement("div");
            inner.style.position = "absolute";
            inner.style.top = "0px";
            inner.style.bottom = "0px";
            inner.style.left = "0px";
            inner.style.right = "0px";
            inner.className = this._prefixCssClass("_columnheader_inner");
            row.appendChild(inner);
            
            var splitter = new DayPilot.Splitter(inner);
            splitter.widths = DayPilot.Util.propArray(props, "width");
            splitter.height = resolved.headerHeight();
            splitter.css.title = this._prefixCssClass("_columnheader_cell");
            splitter.css.titleInner = this._prefixCssClass("_columnheader_cell_inner");
            splitter.css.splitter = this._prefixCssClass("_columnheader_splitter");
            splitter.titles = DayPilot.Util.propArray(props, "title");
            splitter.updating = function(args) { 
                //calendar.rowHeaderCols = this.widths;
                DayPilot.Util.updatePropsFromArray(calendar.rowHeaderColumns, "width", this.widths);
                calendar._updateRowHeaderWidth();
                if (calendar.cellWidthSpec === "Auto") {

                }
            };
            splitter.updated = function(rargs) {
                if (typeof calendar.onRowHeaderColumnResized === "function") {
                    var args = {};
                    args.column = calendar.rowHeaderColumns[rargs.index];
                    calendar.onRowHeaderColumnResized(args);
                };
                calendar._updateAutoCellWidth();
            };
            splitter.color = '#000000';
            splitter.opacity = 30;
            //splitter.height = 19;
            splitter.init();

            div.appendChild(scroll);
            this._splitter = splitter;
        };
        
        this._updateCorner = function() {
            var div = this.nav.corner;
            div.innerHTML = '';
            div.className = this.cssOnly ? this._prefixCssClass('_corner') : this._prefixCssClass('corner');
            if (!this.cssOnly) {
                div.style.backgroundColor = this.hourNameBackColor;
                div.style.fontFamily = this.hourFontFamily;
                div.style.fontSize = this.hourFontSize;
                div.style.cursor = 'default';
            }

            var inner = document.createElement("div");
            inner.style.position = "absolute";
            inner.style.top = "0px";
            inner.style.left = "0px";
            inner.style.right = "0px";
            inner.style.bottom = "0px";
            
            if (this.cssOnly) {
                inner.className = this._prefixCssClass('_corner_inner');
            }
            this.divCorner = inner;
            inner.innerHTML = '&nbsp;';
    
            if (this.rowHeaderColumns && this.rowHeaderColumns.length > 0) {
                var mini = document.createElement("div");
                mini.style.position = "absolute";
                mini.style.top = "0px";
                mini.style.left = "0px";
                mini.style.right = "0px";
                mini.style.bottom = (resolved.headerHeight() + 1) + "px";
                div.appendChild(mini);
                
                var divider = document.createElement("div");
                divider.style.position = "absolute";
                divider.style.left = "0px";
                divider.style.right = "0px";
                divider.style.height = "1px";
                divider.style.bottom = (resolved.headerHeight()) + "px";
                divider.className = this._prefixCssClass("_divider");
                div.appendChild(divider);
                
                mini.appendChild(inner);

                this._drawHeaderColumns();
            }
            else {
                div.appendChild(inner);
            }

            var inner2 = document.createElement("div");
            inner2.style.position = 'absolute';
            inner2.style.padding = '2px';
            inner2.style.top = '0px';
            inner2.style.left = '1px';
            inner2.style.backgroundColor = "#FF6600";
            inner2.style.color = "white";
            inner2.innerHTML = "\u0044\u0045\u004D\u004F";

            if (this.numberFormat) div.appendChild(inner2);

        };

        this._drawRowHeaderHideIcon = function() {

            if (!this.rowHeaderHideIconEnabled) {
                return;
            }

            var marginTop = 3;

            var left = this._getOuterRowHeaderWidth() + resolved.splitterWidth() - 1;
            var width = 10;
            var height = 20;
            var top = this._getTotalHeaderHeight() + marginTop;

            var div = DayPilot.Util.div(this.nav.top, left, top, width, height);
            //div.style.backgroundColor = "gray";
            div.style.cursor = "pointer";
            div.className = calendar._prefixCssClass("_header_icon");
            DayPilot.Util.addClass(div, calendar._prefixCssClass("_header_icon_hide"));
            div.onclick = function() {
                calendar.rows.headerToggle();
            };
            this.nav.hideIcon = div;
        };

        this._drawCorner = function() {
            var rowHeaderWidth = this._getOuterRowHeaderWidth();

            var div = document.createElement("div");
            calendar.nav.corner = div;
            div.style.width = rowHeaderWidth + "px";
            div.style.height = (this._getTotalHeaderHeight()) + "px";
            div.style.overflow = 'hidden';
            div.style.position = 'relative';
            div.setAttribute("unselectable", "on");
            div.onmousemove = function() { calendar._out(); };
            div.oncontextmenu = function() { return false; };
            
            this._updateCorner();

            return div;
        };

        this._getTotalHeaderHeight = function() {
            if (this.timeHeader) {
                var lines = this.timeHeader.length;
                /*
                if (!this.showBaseTimeHeader) {
                    lines -= 1;
                }
                */
                return lines * resolved.headerHeight();
            }
            return 2 * resolved.headerHeight();
        };


        this._drawResScroll = function() {
            var div = document.createElement("div");
            if (!this.cssOnly) {
                //div.style.border = "1px solid " + this.borderColor;
                div.style.backgroundColor = this.hourNameBackColor;
            }
            div.style.width = (this._getOuterRowHeaderWidth()) + "px";
            div.style.height = this._getScrollableHeight() + "px";
            div.style.overflow = 'hidden';
            div.style.position = 'relative';
            div.className = calendar._prefixCssClass("_rowheader_scroll");

            /*
            if (this.rowHeaderScrolling) {
                div.style.overflowX = "auto";
            }
            */

            //div.id = this.id + "_resscroll";
            div.onmousemove = function() {
                calendar._out();
            };
            div.onscroll = function() {
                if (calendar.nav.columnScroll) {
                    calendar.nav.columnScroll.scrollLeft = div.scrollLeft;
                }
                calendar.nav.scroll.scrollTop = div.scrollTop;
            }
            div.oncontextmenu = function() { return false; };

            div.onmouseenter = function() {
                if (calendar.rowHeaderScrolling) {
                    div.style.overflowX = "auto";
                }
            };

            div.onmouseleave = function() {
                if (calendar.rowHeaderScrolling) {
                    div.style.overflowX = "hidden";
                }
            };

            this.divResScroll = div;

            this._scrollRes = div;

            return div;
        };

        this._setRightColWidth = function(div) {
            if (resolved.layout() === 'TableBased') {
                var width = parseInt(this.width, 10);
                var isPercentage = (this.width.indexOf("%") !== -1);
                var isIE = /MSIE/i.test(navigator.userAgent);
                var rowHeaderWidth = this._getTotalRowHeaderWidth();

                if (isPercentage) {
                    if (this.nav.top && this.nav.top.offsetWidth > 0) {
                        div.style.width = (this.nav.top.offsetWidth - 6 - rowHeaderWidth) + "px";
                    }
                }
                else {  // fixed
                    div.style.width = (width - rowHeaderWidth) + "px";
                }
            }
        };

        this._resize = function() {
            if (calendar._resolved.layout() === 'TableBased') {
                calendar._setRightColWidth(calendar.nav.scroll);
                calendar._setRightColWidth(calendar.divTimeScroll);
            }

            calendar._updateHeight();
            calendar._updateAutoCellWidth();
            
            calendar._cache.drawArea = null;
            calendar._findHeadersInViewPort();
        };
        
        this._updateAutoCellWidth = function() {
            if (!calendar.initialized) {
                return;
            }
            if (calendar.cellWidthSpec !== 'Auto') {
                return;
            }

            // TODO detect a real dimension change
            calendar.debug.message("cell width recalc in _resize");
            calendar._calculateCellWidth();
            calendar._prepareItline();
            calendar._drawTimeHeader();
            calendar._deleteCells();
            calendar._drawCells();
            calendar._deleteSeparators();
            calendar._drawSeparators();
            calendar._deleteEvents();
            calendar._loadEvents();
            calendar._drawEvents();

        };

        this._calculateCellWidth = function() {

            // only valid for automatic cell width
            if (this.cellWidthSpec !== 'Auto') {
                return;
            }
            var total = this.nav.top.clientWidth;
            var header = this._getOuterRowHeaderWidth();
            var full = total - header;
            var cell = full / this._cellCount();
            this.cellWidth = Math.floor(cell);
            
            calendar.debug.message("AutoCellWidth: " + this.cellWidth);
        };
        
        this._getScrollableWidth = function() {  // only the visible part
            /*
            if (this.nav.scroll) {
                this.debug.message("scrollableWidth/clientWidth: " + this.nav.scroll.clientWidth);
                return this.nav.scroll.clientWidth;
            }
            */
            
            //
            // TODO get directly from nav.scroll (but it may not be ready yet)
            var total = this.nav.top.clientWidth;
            var header = this._getOuterRowHeaderWidth();
            var manualAdjustment = 2;
            
            
            var height = this._getScrollableHeight();
            var innerHeight = this._getScrollableInnerHeight();
            var scrollBarWidth = 0;
            if (innerHeight > height) {
                scrollBarWidth = DayPilot.swa();
            }
            
            var full = total - header - manualAdjustment - scrollBarWidth;
            this.debug.message("scrollableWidth: " + full);
            return full;
        };

        this._drawTimeHeaderDiv = function() {

            var div = document.createElement("div");
            div.style.overflow = 'hidden';
            if (!this.cssOnly) {
                div.style.backgroundColor = this.hourNameBackColor;
                //div.style.borderRight = "1px solid " + this.borderColor;
            }
            div.style.position = 'absolute';
            div.style.display = 'block';
            div.style.top = "0px";
            div.style.width = "100%";
            div.style.height = this._getTotalHeaderHeight() + "px";
            div.style.overflow = "hidden";
            div.onmousemove = function() { calendar._out(); if (calendar.cellBubble) { calendar.cellBubble.delayedHide(); } };

            this._setRightColWidth(div);

            this.divTimeScroll = div;

            var inner = document.createElement("div");
            /*
            if (!this.cssOnly) {
            inner.style.borderTop = "1px solid " + this.borderColor;
            }
            */
            inner.style.width = (this._cellCount() * this.cellWidth + 5000) + "px";

            this.divNorth = inner;

            div.appendChild(inner);

            return div;
        };

        this._getScrollableHeight = function() {
            var height = 0;
            if (this.heightSpec === 'Fixed' || this.heightSpec === "Parent100Pct") {
                return this.height ? this.height : 0;
            }
            else {
                height = this._getScrollableInnerHeight();
            }

            if (this.heightSpec === 'Max' && height > this.height) {
                return this.height;
            }

            return height;
        };
        
        this._getScrollableInnerHeight = function() {
            var height;
            if (this._innerHeightTree) {
                var scrollHeight = DayPilot.sh(calendar.nav.scroll);
                if (scrollHeight === 0) { // no horizontal scrollbar
                    height = this._innerHeightTree;
                }
                else {
                    height = this._innerHeightTree + scrollHeight; // guessing that the vertical scrollbar width is the same as horizontal scrollbar height, used to be 18 hardcoded
                }
            }
            else {
                height = this.rowlist.length * this._resolved.eventHeight();
            }
            return height;
        };

/*
        this._findGrouplineX = function(left) {
            for (var i = 0; i < this.groupline.length; i++) {
                var cell = this.groupline[i];
                if (left >= cell.left && left < cell.left + cell.width) {
                    return i;
                }
            }
            return this.groupline.length - 1;
        };
*/
        this._out = function() {
            this._crosshairHide();
            this._stopScroll();
            this._cellhoverout();
        };

        this._drawMainContent = function() {

            var div = document.createElement("div");
            /*
            if (this.cellWidthSpec === "Auto") {
                div.style.overflowX = "hidden";
                div.style.overflowY = "auto";
            }
            else {*/
                div.style.overflow = "auto";
                div.style.overflowX = "auto";
                div.style.overflowY = "auto";
            //}
            //div.style.overflow = "scroll";
            div.style.position = "absolute";
            div.style.height = (this._getScrollableHeight()) + "px";
            div.style.top = (this._getTotalHeaderHeight() + 1) + "px";
            div.style.width = "100%";
            if (!this.cssOnly) {
                div.style.backgroundColor = this.emptyBackColor;
            }
            div.className = this._prefixCssClass("_scrollable");
            div.oncontextmenu = function() { return false; };

            this._setRightColWidth(div);

            //this.divScroll = div;
            this.nav.scroll = div;

            this._maind = document.createElement("div");
            this._maind.style.MozUserSelect = "none";
            this._maind.style.KhtmlUserSelect = "none";
            this._maind.style.webkitUserSelect = "none";
            this._maind.daypilotMainD = true;
            this._maind.calendar = this;  // used in DayPilotScheduler.gTouchMove

            // Android browser bug
            if (android) {
                this._maind.style.webkitTransform = "translateZ(0px)";
            }
            this._maind.style.position = 'absolute';
            //this._maind.style.overflow = "hidden";

            var gridwidth = this._getGridWidth();
            if (gridwidth > 0 && !isNaN(gridwidth)) {
                this._maind.style.width = (gridwidth) + "px";
                console.log("setting grid width: " + gridwidth);
            }
            this._maind.setAttribute("unselectable", "on");

            this._maind.onmousedown = this._onMaindMouseDown;
            this._maind.onmousemove = this._onMaindMouseMove;
            this._maind.oncontextmenu = this._onMaindRightClick;
            this._maind.ondblclick = this._onMaindDblClick;

            this._maind.className = this._prefixCssClass("_matrix");

            this.divStretch = document.createElement("div");
            this.divStretch.style.position = 'absolute';
            this.divStretch.style.height = '1px';
            this._maind.appendChild(this.divStretch);

            this.divCells = document.createElement("div");
            this.divCells.style.position = 'absolute';
            this.divCells.oncontextmenu = this._onMaindRightClick;
            this._maind.appendChild(this.divCells);

            this.divLines = document.createElement("div");
            this.divLines.style.position = 'absolute';
            this.divLines.oncontextmenu = this._onMaindRightClick;
            this._maind.appendChild(this.divLines);

            this.divBreaks = document.createElement("div");
            this.divBreaks.style.position = 'absolute';
            this.divBreaks.oncontextmenu = this._onMaindRightClick;
            this._maind.appendChild(this.divBreaks);

            this.divSeparators = document.createElement("div");
            this.divSeparators.style.position = 'absolute';
            this.divSeparators.oncontextmenu = this._onMaindRightClick;
            this._maind.appendChild(this.divSeparators);

            this.divLinksBelow = document.createElement("div");
            this.divLinksBelow.style.position = "absolute";
            this._maind.appendChild(this.divLinksBelow);

            this.divCrosshair = document.createElement("div");
            this.divCrosshair.style.position = 'absolute';
            this.divCrosshair.ondblclick = this._onMaindDblClick;
            this._maind.appendChild(this.divCrosshair);

            this.divRange = document.createElement("div");
            this.divRange.style.position = 'absolute';
            this.divRange.oncontextmenu = this._onMaindRightClick;
            this._maind.appendChild(this.divRange);

            this.divEvents = document.createElement("div");
            this.divEvents.style.position = 'absolute';
            this._maind.appendChild(this.divEvents);

            this.divSeparatorsAbove = document.createElement("div");
            this.divSeparatorsAbove.style.position = 'absolute';
            this.divSeparatorsAbove.oncontextmenu = this._onMaindRightClick;
            this._maind.appendChild(this.divSeparatorsAbove);

            this.divLinksAbove = document.createElement("div");
            this.divLinksAbove.style.position = "absolute";
            this._maind.appendChild(this.divLinksAbove);

            this.divLinkShadow = document.createElement("div");
            this.divLinkShadow.style.position = "absolute";
            this._maind.appendChild(this.divLinkShadow);

            this.divLinkpoints = document.createElement("div");
            this.divLinkpoints.style.position = "absolute";
            this._maind.appendChild(this.divLinkpoints);

            this.divHover = document.createElement("div");
            this.divHover.style.position = 'absolute';
            this._maind.appendChild(this.divHover);

            // TODO add scroll labels

            div.appendChild(this._maind);

            return div;
        };

        this._overlay = {};
        var overlay = this._overlay;

        overlay.create = function() {
            if (calendar.nav.overlay) {
                return;
            }
            var div = document.createElement('div');
            div.style.position = "absolute";
            div.style.left = "0px";
            div.style.right = "0px";
            div.style.top = "0px";
            div.style.bottom = "0px";
            div.className = calendar._prefixCssClass("_block");
            calendar.nav.top.appendChild(div);
            calendar.nav.overlay = div;
        };

        overlay.show = function() {
            overlay.create();
            calendar.nav.overlay.style.display = '';
        };

        overlay.hide = function() {
            if (calendar.nav.overlay) {
                calendar.nav.overlay.style.display = 'none';
            }
        };

        this._loadingStart = function() {
            if (calendar.loadingTimeout) {
                window.clearTimeout(calendar.loadingTimeout);
            }
            calendar.loadingTimeout = window.setTimeout(function() {
                if (calendar.loadingLabelVisible) {
                    calendar.nav.loading.innerHTML = calendar.loadingLabelText;
                    calendar.nav.loading.style.display = '';
                }
                if (calendar.blockOnCallBack) {
                    overlay.show();
                }
            }, 100);
        };

        this._loadingStop = function(msg) {
            //calendar.status.loadingEvents = false;
            if (this.loadingTimeout) {
                window.clearTimeout(this.loadingTimeout);
            }

            if (msg) {
                this.nav.loading.innerHTML = msg;
                window.setTimeout(function() { calendar._loadingStop(); }, 1000);
            }
            else {
                this.nav.loading.style.display = 'none';
                if (this.blockOnCallBack) {
                    overlay.hide();
                }
            }

        };

        this._prepareVariables = function() {
            this.startDate = new DayPilot.Date(this.startDate).getDatePart();
            //this._getEventHeightFromCss();
        };

        this._getDimensionsFromCss = function(className) {
            var div = document.createElement("div");
            div.style.position = "absolute";
            div.style.top = "-2000px";
            div.style.left = "-2000px";
            div.className = this._prefixCssClass(className);

            document.body.appendChild(div);
            var height = div.offsetHeight;
            var width = div.offsetWidth;
            document.body.removeChild(div);

            var result = {};
            result.height = height;
            result.width = width;
            return result;
        };

        // interval defined in seconds, minimum 30 seconds
        this._startAutoRefresh = function(forceEnabled) {
            if (forceEnabled) {
                this.autoRefreshEnabled = true;
            }

            if (!this.autoRefreshEnabled) {
                return;
            }

            if (this.autoRefreshCount >= this.autoRefreshMaxCount) {
                return;
            }

            calendar.debug.message("autorefresh started");

            this._pauseAutoRefresh();

            var interval = this.autoRefreshInterval;
            if (!interval || interval < 10) {
                throw "The minimum autoRefreshInterval is 10 seconds";
            }
            //this.autoRefresh = interval * 1000;
            this.autoRefreshTimeout = window.setTimeout(function() { calendar._doRefresh(); }, this.autoRefreshInterval * 1000);
        };

        this._pauseAutoRefresh = function() {
            if (this.autoRefreshTimeout) {
                window.clearTimeout(this.autoRefreshTimeout);
            }
        };

        this._doRefresh = function() {
            // skip if an operation is active
            if (!DayPilotScheduler.resizing && !DayPilotScheduler.moving && !DayPilotScheduler.drag && !DayPilotScheduler.range) {
                var skip = false;
                if (typeof this.onAutoRefresh === 'function') {
                    var args = {};
                    args.i = this.autoRefreshCount;
                    args.preventDefault = function() {
                        this.preventDefault.value = true;
                    };

                    calendar.onAutoRefresh(args);
                    if (args.preventDefault.value) {
                        skip = true;
                    }
                }
                if (!skip && this._serverBased()) {
                    this.commandCallBack(this.autoRefreshCommand);
                }
                this.autoRefreshCount++;
            }
            if (this.autoRefreshCount < this.autoRefreshMaxCount) {
                this.autoRefreshTimeout = window.setTimeout(function() { calendar._doRefresh(); }, this.autoRefreshInterval * 1000);
            }
        };

        this._registerGlobalHandlers = function() {
            if (!DayPilotScheduler.globalHandlers) {
                DayPilotScheduler.globalHandlers = true;
                DayPilot.re(document, 'mousemove', DayPilotScheduler.gMouseMove);
                DayPilot.re(document, 'mouseup', DayPilotScheduler.gMouseUp);
                DayPilot.re(document, 'touchmove', DayPilotScheduler.gTouchMove);
                DayPilot.re(document, 'touchend', DayPilotScheduler.gTouchEnd);
                //DayPilot.re(window, 'unload', DayPilotScheduler.gUnload);
            }
            DayPilot.re(window, 'resize', this._resize);
        };


        this._registerOnScroll = function() {
            this.nav.scroll.root = this;  // might not be necessary
            this.nav.scroll.onscroll = this._onScroll;

            calendar._scrollPos = this.nav.scroll.scrollLeft;
            calendar._scrollTop = this.nav.scroll.scrollTop;
            calendar._scrollWidth = this.divNorth.clientWidth; // this is always available, divScroll might be not (if there are no resources)
        };

        this._saveState = function() {
            if (!this.nav.state) {
                return;
            }

            //var start = new Date();
            var state = {};
            state.scrollX = this.nav.scroll.scrollLeft;
            state.scrollY = this.nav.scroll.scrollTop;

            if (this.syncResourceTree) {
                state.tree = this._getTreeState();
            }

            this.nav.state.value = DayPilot.he(DayPilot.JSON.stringify(state));
        };

        this._drawSeparators = function() {
            if (!this.separators) {
                return;
            }
            for (var i = 0; i < this.separators.length; i++) {
                this._drawSeparator(i);
            }
        };

        this._batch = {};
        this._batch.step = 300;
        this._batch.delay = 10;
        this._batch.mode = "display";
        this._batch.layers = true;


        this._updateEventPositionsInRow = function(row) {
            var alwaysRecalculate = true;
            var eventMarginTop = this._durationBarDetached ? 10 : 0;

            var lineTop = 0;
            for (var j = 0; j < row.lines.length; j++) {
                var line = row.lines[j];
                for (var k = 0; k < line.length; k++) {
                    var e = line[k];

                    // do something faster instead, probably move it to another function

                    if (!e.part.top || alwaysRecalculate) {
                        e.part.line = j;
                        if (!e.part.height) {
                            e.part.height = row.eventHeight;
                        }
                        //e.part.top = j * (e.part.height + eventMarginTop) + eventMarginTop;
                        e.part.top = lineTop + row.marginTop;
                        e.part.detachedBarTop = e.part.top - eventMarginTop;
                        //e.part.height = this._resolved.eventHeight();
                        e.part.right = e.part.left + e.part.width;
                        e.part.fullTop = this.rowlist[e.part.dayIndex].top + e.part.top;
                        e.part.fullBottom = e.part.fullTop + e.part.height;
                    }
                }
                lineTop += (line.height || row.eventHeight) * this.eventStackingLineHeight/100;
            }
        };

        // batch rendering flushes events in 10-item batches
        this._drawEvents = function(batch) {
            var step = this._batch.step;  // batch size
            var layers = this._batch.layers;

            // experimental
            if (layers) {
                // create a new layer
                calendar.divEvents = document.createElement("div");
                calendar.divEvents.style.position = 'absolute';  // relative
//                calendar.maind.appendChild(this.divEvents);

                calendar._maind.insertBefore(this.divEvents, this.divSeparatorsAbove);

            }

            if (this._batch.mode === 'display') {
                this.divEvents.style.display = 'none';
            }
            else if (this._batch.mode === 'visibility') {
                this.divEvents.style.visibility = 'hidden';
            }

            var dynamic = this.dynamicEventRendering === 'Progressive';
            var area = this._getDrawArea();
            var top = area.pixels.top;
            var bottom = area.pixels.bottom;


            for (var i = 0; i < this.rowlist.length; i++) {

                var row = this.rowlist[i];

                var rowTop = row.top - this.dynamicEventRenderingMargin;
                var rowBottom = rowTop + row.height + 2 * this.dynamicEventRenderingMargin;
                if (dynamic && (bottom <= rowTop || top >= rowBottom)) {
                    continue;
                }

                this._updateEventPositionsInRow(row);

                for (var j = 0; j < row.lines.length; j++) {
                    var line = row.lines[j];
                    for (var k = 0; k < line.length; k++) {
                        var e = line[k];
                        var rendered = this._drawEvent(e);

                        if (batch && rendered) {
                            step--;
                            // flush
                            if (step <= 0) {
                                this.divEvents.style.visibility = '';
                                this.divEvents.style.display = '';
                                window.setTimeout(function() { calendar._drawEvents(batch); }, calendar._batch.delay);
                                return;
                            }
                        }
                    }
                }
            }

            this.divEvents.style.display = '';
            this._findEventsInViewPort();
            linktools.load();
            this._loadingStop();

        };

        this._drawEventsInRow = function(rowIndex) {
            // not hiding and showing this.divEvents, caused flickering

            var row = this.rowlist[rowIndex];

            // create new layer
            this.divEvents = document.createElement("div");
            this.divEvents.style.position = 'absolute';  // relative
            this.divEvents.style.display = 'none';
            //this.maind.appendChild(this.divEvents);

            this._maind.insertBefore(this.divEvents, this.divSeparatorsAbove);

            var eventMarginTop = this._durationBarDetached ? 10 : 0;

            this._updateEventPositionsInRow(row);

            //var lineTop = 0;
            for (var j = 0; j < row.lines.length; j++) {
                var line = row.lines[j];
                for (var k = 0; k < line.length; k++) {
                    var e = line[k];

                    /*
                    // this must always be perfomed during row redrawing
                    e.part.line = j;
                    if (!e.part.height) {
                        e.part.height = row.eventHeight;
                    }
                    //e.part.height = row.eventHeight;
                    e.part.top = line.top + row.marginTop;
                    //e.part.top = j * (e.part.height + eventMarginTop) + eventMarginTop;
                    e.part.detachedBarTop = e.part.top - eventMarginTop;

                    e.part.right = e.part.left + e.part.width;
                    e.part.fullTop = this.rowlist[e.part.dayIndex].top + e.part.top;
                    e.part.fullBottom = e.part.fullTop + e.part.height;
*/
                    // batch rendering not supported here
                    this._drawEvent(e);
                }
                //lineTop += (line.height || row.eventHeight) * this.eventStackingLineHeight / 100;
            }
            this.divEvents.style.display = '';

            //this._findEventsInViewPort();
            //this.multiselect.redraw();
        };

        this._deleteEvents = function() {
            if (this.elements.events) {
                var length = this.elements.events.length;

                for (var i = 0; i < length; i++) {
                    var div = this.elements.events[i];
                    this._deleteEvent(div);
                }
            }
            this.elements.events = [];
        };

        this._deleteEventsInRow = function(rowIndex) {
            //var count = 0;
            if (this.elements.events) {
                var length = this.elements.events.length;
                var removed = [];

                for (var i = 0; i < length; i++) {
                    var div = this.elements.events[i];
                    if (div.row === rowIndex) {
                        this._deleteEvent(div);
                        removed.push(i);
                        //count += 1;
                    }
                }

                for (var i = removed.length - 1; i >= 0; i--) {
                    this.elements.events.splice(removed[i], 1);
                }
            }

        };

        this._deleteEvent = function(div) {

            // direct event handlers
            div.onclick = null;
            div.oncontextmenu = null;
            div.onmouseover = null;
            div.onmouseout = null;
            div.onmousemove = null;
            div.onmousedown = null;
            div.ondblclick = null;

            if (div.event) {
                if (!div.isBar) {
                    div.event.rendered = null;
                }
                div.event = null;
            }

            if (div.related) {
                DayPilot.de(div.related);
            }
            if (div.parentNode) { div.parentNode.removeChild(div); }
        };

        this._deleteBlock = function(div) {
            if (div.event) {
                div.event.rendered = false;
            }
            div.onclick = null;
            div.onmousedown = null;
            div.event = null;
            if (div.parentNode) {
                div.parentNode.removeChild(div);
            }
        };

        // deletes events that are out of the current view
        // keeps the last "keepPlus" number of events outside of the view
        this._deleteOldEvents = function(keepPlus) {
            if (!keepPlus) {
                keepPlus = 0;
            }

            if (this.dynamicEventRendering !== 'Progressive') {
                return;
            }

            this.divEvents.style.display = 'none';

            var updated = [];

            var deleted = 0;

            var length = this.elements.events.length;
            for (var i = length - 1; i >= 0; i--) {
                var div = this.elements.events[i];
                if (this._oldEvent(div.event)) {
                    if (keepPlus > 0) {
                        keepPlus--;
                        updated.unshift(div);
                    }
                    else {
                        this._deleteEvent(div);
                        deleted++;
                    }
                }
                else {
                    updated.unshift(div);
                }
            }

            this.elements.events = updated;

            this.divEvents.style.display = '';
        };

        this._deleteOldCells = function(keepPlus) {
            var updated = [];

            var deleted = 0;

            var area = this._getDrawArea();

            var length = this.elements.cells.length;
            for (var i = length - 1; i >= 0; i--) {
                var div = this.elements.cells[i];

                var visible = (area.xStart < div.coords.x && div.coords.x <= area.xEnd) && (area.yStart < div.coords.y && div.coords.y <= area.yEnd);

                if (!visible) {
                    if (keepPlus > 0) {
                        keepPlus--;
                        updated.unshift(div);
                    }
                    else {
                        this._deleteCell(div);
                        deleted++;
                    }
                }
                else {
                    updated.unshift(div);
                }
            }

        };

        this._deleteCell = function(div) {
            if (!div) {
                return;
            }
            var x = div.coords.x;
            var y = div.coords.y;

            // remove div
            var index = DayPilot.indexOf(calendar.elements.cells, div);
            calendar.elements.cells.splice(index, 1);
            if (div.parentNode) { div.parentNode.removeChild(div); }

            // remove from cache
            calendar._cache.cells[x + "_" + y] = null;
        };

        this._deleteSeparators = function() {
            if (this.elements.separators) {
                for (var i = 0; i < this.elements.separators.length; i++) {
                    var div = this.elements.separators[i];
                    //DayPilot.pu(div); // not necessary
                    DayPilot.de(div);
                    //div.parentNode.removeChild(div);
                }
            }
            this.elements.separators = [];
        };


        this._hiddenEvents = function() {
            var dynamic = this.dynamicEventRendering === 'Progressive';

            if (!this.nav.scroll) {
                return false;
            }
            var top = this.nav.scroll.scrollTop;
            var bottom = top + this.nav.scroll.clientHeight;

            for (var i = 0; i < this.rowlist.length; i++) {

                var row = this.rowlist[i];

                var rowTop = row.top;
                var rowBottom = row.top + row.height;
                if (dynamic && (bottom <= rowTop || top >= rowBottom)) {
                    continue;
                }

                for (var j = 0; j < row.lines.length; j++) {
                    var line = row.lines[j];
                    for (var k = 0; k < line.length; k++) {
                        var e = line[k];
                        if (this._hiddenEvent(e)) {
                            return true;
                        }
                    }
                }

            }

            return false;
        };

        this._hiddenEvent = function(data) {
            if (data.rendered) {
                return false;
            }

            var dynamic = this.dynamicEventRendering === 'Progressive';
            var left = this.nav.scroll.scrollLeft;
            var right = left + this.nav.scroll.clientWidth;
            var eventLeft = data.Left;
            var eventRight = data.Left + data.Width;
            if (dynamic && (right <= eventLeft || left >= eventRight)) {
                return false;
            }
            return true;
        };

        this._oldEvent = function(ev) {
            if (!ev.rendered) {  // just for the case, these events might not have Top defined
                return true;
            }

            var area = this._getDrawArea();

            var top = area.pixels.top;
            var bottom = area.pixels.bottom;
            var left = area.pixels.left - this.dynamicEventRenderingMargin;
            var right = area.pixels.right + this.dynamicEventRenderingMargin;

            var eventLeft = ev.part.left;
            var eventRight = ev.part.right;
            var eventTop = ev.part.fullTop;
            var eventBottom = ev.part.fullBottom;

            if (right <= eventLeft || left >= eventRight) {
                return true;
            }

            if (bottom <= eventTop || top >= eventBottom) {
                return true;
            }

            return false;
        };

        this._drawBlock = function(block) {
            if (block.rendered) {
                return false;
            }

            var left = block.min;
            var width = block.max - block.min;
            var height = calendar.eventHeight;
            var top = block.row.top;

            var div = document.createElement("div");
            div.style.position = 'absolute';
            div.style.left = left + 'px';
            div.style.top = top + 'px';
            div.style.width = width + 'px';
            div.style.height = height + 'px';

            // temp styling
            div.className = calendar._prefixCssClass("_event_group");
            div.style.cursor = "pointer";

            var args = {};
            args.group = {};
            args.group.count = block.events.length;
            args.group.html = "[+] " + block.events.length + " events";
            if (typeof calendar.onBeforeGroupRender === "function") {
                calendar.onBeforeGroupRender(args);
            }

            var inner = document.createElement("div");
            inner.innerHTML = args.group.html;
            div.appendChild(inner);

            div.onmousedown = function(ev) {
                var ev = ev || window.event;
                ev.cancelBubble = true;
            };

            div.onclick = function(ev) {
                var block = div.event;
                block.expanded = true;

                calendar._deleteBlock(div);
                var elindex = DayPilot.indexOf(calendar.elements.events, div);
                if (elindex !== -1) {
                    calendar.elements.events.splice(elindex, 1);
                }

                //calendar._drawEvents();
                calendar._updateRowHeights();
                calendar._updateRowsNoLoad(block.part.dayIndex);
                calendar._updateHeight();

                var ev = ev || window.event;
                ev.cancelBubble = true;
            };

            // make it compatible with event.part
            // TODO resolve
            block.part = {};
            block.part.left = left;
            block.part.width = width;
            block.part.dayIndex = DayPilot.indexOf(calendar.rowlist, block.row);
            block.part.top = 0;
            block.client = {};
            block.client.html = function() { return args.group.html; };
            div.event = block;

            // add it to the events collection
            this.elements.events.push(div);

            // draw the div
            this.divEvents.appendChild(div);

            block.rendered = true;

            return true;

        };

        // returns true if the event was actually rendered
        this._drawEvent = function(data) {

            if (data.rendered) {
                return false;
            }

            if (calendar.groupConcurrentEvents) {
                var block = data.part.block;
                if (block.events.length > 1 && !block.expanded) {
                    return calendar._drawBlock(block);
                }
            }

            //var dynamic = this.dynamicEventRendering === 'Progressive' && !this.dynamicLoading;
            var dynamic = this.dynamicEventRendering === 'Progressive';

            /*
            var left = this.nav.scroll.scrollLeft - this.dynamicEventRenderingMargin;
            var right = left + this.nav.scroll.clientWidth + 2 * this.dynamicEventRenderingMargin;
            */
            var area = this._getDrawArea();
            var left = area.pixels.left - this.dynamicEventRenderingMargin;
            var right = area.pixels.right + this.dynamicEventRenderingMargin;

            var eventLeft = data.part.left;
            var eventRight = data.part.left + data.part.width;
            if (dynamic && (right <= eventLeft || left >= eventRight)) { // dynamic rendering, event outside of the current view
                return false;
            }

            var rowIndex = data.part.dayIndex;
            var borders = !this.cssOnly && this.eventBorderVisible;
            var width = data.part.width;
            var height = data.part.height;
            if (borders) {
                width -= 2;
                height -= 2;
            }

            var cache = data.cache || data.data;

            // make sure it's not negative
            width = Math.max(0, width);
            height = Math.max(0, height);

            var row = this.rowlist[rowIndex];
            if (row.hidden) {
                return false;
            }

            // prepare the top position
            var rowTop = row.top;
            //var line = data.part.top / this.eventHeight;

            var div = document.createElement("div");

            var barDetached = this._durationBarDetached;

            div.related = [];

            if (barDetached) {
                var bar = document.createElement("div");
                bar.style.position = 'absolute';
                bar.style.left = (data.part.left + data.part.barLeft) + 'px';
                bar.style.top = (rowTop + data.part.detachedBarTop) + 'px';
                bar.style.width = data.part.barWidth + 'px';
                bar.style.height = 5 + 'px';

                bar.style.backgroundColor = "red";
                bar.type = "detachedBar";
                div.related.push(bar);

                // add it to the events collection
                //this.elements.bars.push(bar);

                // draw the div
                this.divEvents.appendChild(bar);
            }

            if (data.data.htmlLeft) {

                var margin = 5;
                var divLeft = document.createElement("div");
                divLeft.style.position = 'absolute';
                divLeft.style.right = -(data.part.left - margin) + 'px';
                divLeft.style.top = (rowTop + data.part.top) + 'px';
                divLeft.style.height = calendar.eventHeight + 'px';
                divLeft.style.boxSizing = "border-box";
                divLeft.innerHTML = data.data.htmlLeft;
                divLeft.className = calendar._prefixCssClass("_event_left");
                divLeft.type = "divLeft";

                div.related.push(divLeft);

                // draw the div
                this.divEvents.appendChild(divLeft);
            }

            if (data.data.htmlRight) {
                var margin = 5;
                var divRight = document.createElement("div");
                divRight.style.position = 'absolute';
                divRight.style.left = (data.part.left + data.part.width + margin) + 'px';
                divRight.style.top = (rowTop + data.part.top) + 'px';
                divRight.style.height = calendar.eventHeight + 'px';
                divRight.style.boxSizing = "border-box";
                divRight.innerHTML = data.data.htmlRight;
                divRight.className = calendar._prefixCssClass("_event_right");
                divRight.type = "divRight";

                div.related.push(divRight);

                // draw the div
                this.divEvents.appendChild(divRight);
            }

            var top = rowTop + data.part.top;

            //div.data = data;
            div.style.position = 'absolute';
            div.style.left = data.part.left + 'px';
            div.style.top = (rowTop + data.part.top) + 'px';
            div.style.width = width + 'px';
            div.style.height = height + 'px';
            if (!this.cssOnly) {
                if (calendar.eventBorderVisible) {
                    div.style.border = '1px solid ' + (cache.borderColor || this.eventBorderColor);
                }
                div.style.backgroundColor = data.client.backColor();
                div.style.fontSize = this.eventFontSize;
                div.style.cursor = 'default';
                div.style.fontFamily = this.eventFontFamily;
                div.style.color = cache.fontColor || this.eventFontColor;  // TODO move inside Event as fontColor()

                if (cache.backImage && !this.durationBarVisible) {
                    div.style.backgroundImage = "url(" + cache.backImage + ")";
                    if (cache.backRepeat) {
                        div.style.backgroundRepeat = cache.backRepeat;
                    }
                }

                if (this._resolved.rounded()) {
                    div.style.MozBorderRadius = "5px";
                    div.style.webkitBorderRadius = "5px";
                    div.style.borderRadius = "5px";
                }

            }
            div.style.whiteSpace = 'nowrap';
            div.style.overflow = 'hidden';
            div.className = this.cssOnly ? this._prefixCssClass("_event") : this._prefixCssClass("event");
            if (data.data.type === "Milestone") {
                DayPilot.Util.addClass(div, calendar._prefixCssClass("_task_milestone"));
            }
            if (data.data.type === "Group") {
                DayPilot.Util.addClass(div, calendar._prefixCssClass("_task_parent"));  // remove
                DayPilot.Util.addClass(div, calendar._prefixCssClass("_task_group"));
            }
            if (cache.cssClass) {
                DayPilot.Util.addClass(div, cache.cssClass);
            }
            var lineClasses = true;
            if (lineClasses) {
                DayPilot.Util.addClass(div, this._prefixCssClass("_event_line" + data.part.line));
            }
            div.setAttribute("unselectable", "on");

            if (this.showToolTip && !this.bubble) {
                div.title = data.client.toolTip();
            }

            div.onmousemove = this._onEventMouseMove;
            div.onmouseout = this._onEventMouseOut;
            div.onmousedown = this._onEventMouseDown;

            div.ontouchstart = touch.onEventTouchStart;
            div.ontouchmove = touch.onEventTouchMove;
            div.ontouchend = touch.onEventTouchEnd;

            if (data.client.clickEnabled()) {
                div.onclick = this._onEventClick;
            }

            if (data.client.doubleClickEnabled()) {
                div.ondblclick = this._eventDoubleClickDispatch;
            }

            div.oncontextmenu = this._eventRightClickDispatch;

            var inside = [];
            var durationBarHeight = 0;

            // inner divs

            if (this.cssOnly) {
                var inner = document.createElement("div");
                inner.setAttribute("unselectable", "on");
                inner.className = calendar._prefixCssClass("_event_inner");
                inner.innerHTML = data.client.innerHTML();

                if (cache.backColor) {
                    inner.style.background = cache.backColor;
                    if (DayPilot.browser.ie9 || DayPilot.browser.ielt9) {
                        inner.style.filter = '';
                    }
                }
                if (cache.fontColor) {
                    inner.style.color = cache.fontColor;
                }
                if (cache.borderColor) {
                    inner.style.borderColor = cache.borderColor;
                }
                if (cache.backImage) {
                    inner.style.backgroundImage = "url(" + cache.backImage + ")";
                    if (cache.backRepeat) {
                        inner.style.backgroundRepeat = cache.backRepeat;
                    }
                }

                div.appendChild(inner);

                var startsHere = data.start().getTime() === data.part.start.getTime();
                var endsHere = data.end().getTime() === data.part.end.getTime();

                if (!startsHere) {
                    DayPilot.Util.addClass(div, this._prefixCssClass("_event_continueleft"));
                }
                if (!endsHere) {
                    DayPilot.Util.addClass(div, this._prefixCssClass("_event_continueright"));
                }

                if (data.client.barVisible() && width > 0) {
                    var barLeft = 100 * data.part.barLeft / (width); // %
                    var barWidth = Math.ceil(100 * data.part.barWidth / (width)); // %

                    if (this.durationBarMode === "PercentComplete") {
                        barLeft = 0;
                        barWidth = cache.complete || 0;
                    }

                    var bar = document.createElement("div");
                    bar.setAttribute("unselectable", "on");
                    bar.className = this._prefixCssClass("_event_bar");
                    bar.style.position = "absolute";

                    if (cache.barBackColor) {
                        bar.style.backgroundColor = cache.barBackColor;
                    }

                    var barInner = document.createElement("div");
                    barInner.setAttribute("unselectable", "on");
                    barInner.className = this._prefixCssClass("_event_bar_inner");
                    barInner.style.left = barLeft + "%";
                    //barInner.setAttribute("barWidth", data.part.barWidth);  // debug
                    if (0 < barWidth && barWidth <= 1) {
                        barInner.style.width = "1px";
                    }
                    else {
                        barInner.style.width = barWidth + "%";
                    }

                    if (cache.barColor) {
                        barInner.style.backgroundColor = cache.barColor;
                    }

                    if (cache.barImageUrl) {
                        barInner.style.backgroundImage = "url(" + cache.barImageUrl + ")";
                    }

                    bar.appendChild(barInner);
                    div.appendChild(bar);
                }
            }
            else {
                if (data.client.barVisible()) {
                    durationBarHeight = calendar.durationBarHeight;

                    // white space left
                    inside.push("<div unselectable='on' style='left:0px;background-color:white;width:");
                    inside.push(data.part.barLeft);
                    inside.push("px;height:2px;font-size:1px;position:absolute'></div>");

                    // white space right
                    inside.push("<div unselectable='on' style='left:");
                    inside.push(data.part.barLeft + data.part.barWidth);
                    inside.push("px;background-color:white;width:");
                    inside.push(width - (data.part.barLeft + data.part.barWidth));
                    inside.push("px;height:2px;font-size:1px;position:absolute'></div>");

                    if (this.durationBarMode === "Duration") {
                        inside.push("<div unselectable='on' style='width:");
                        inside.push(data.part.barWidth);
                        inside.push("px;margin-left:");
                        inside.push(data.part.barLeft);
                        inside.push("px;height:");
                    }
                    else {
                        inside.push("<div unselectable='on' style='width:");
                        inside.push(cache.complete);
                        inside.push("%;margin-left:0px;height:");
                    }
                    inside.push(durationBarHeight - 1);
                    inside.push("px;background-color:");
                    inside.push(data.client.barColor());

                    if (cache.barImageUrl) {
                        inside.push(";background-image:url(");
                        inside.push(cache.barImageUrl);
                        inside.push(")");
                    }

                    inside.push(";font-size:1px;position:relative'></div>");
                    inside.push("<div unselectable='on' style='width:");
                    inside.push(width);
                    inside.push("px;height:1px;background-color:");
                    inside.push((cache.borderColor || this.eventBorderColor));
                    inside.push(";font-size:1px;overflow:hidden;position:relative'></div>");
                }

                inside.push("<div unselectable='on' style='padding-left:1px;width:");
                inside.push(width - 1);
                inside.push("px;height:");
                inside.push(height - durationBarHeight);
                inside.push("px;");
                if (calendar.rtl) {
                    inside.push("direction:rtl;");
                }
                if (cache.backImage && this.durationBarVisible) {
                    inside.push("background-image:url(");
                    inside.push(cache.backImage);
                    inside.push(");");
                    if (cache.backRepeat) {
                        inside.push("background-repeat:");
                        inside.push(cache.backRepeat);
                        inside.push(";");
                    }
                }
                inside.push("'>");
                inside.push(data.client.innerHTML());
                inside.push("</div>");

                div.innerHTML = inside.join('');
            }
            div.row = rowIndex;

            if (cache.areas) {
                for (var i = 0; i < cache.areas.length; i++) {
                    var area = cache.areas[i];
                    var v = area.v || "Visible";
                    if (v !== "Visible") {
                        continue;
                    }
                    var a = DayPilot.Areas.createArea(div, data, area);
                    div.appendChild(a);
                }
            }

            // add it to the events collection
            this.elements.events.push(div);

            // draw the div
            this.divEvents.appendChild(div);

            data.rendered = true;

            // init code for the event
            //div.event = new DayPilotScheduler.Event(div.data, calendar);
            div.event = data;

            if (calendar.multiselect._shouldBeSelected(div.event)) {
                calendar.multiselect.add(div.event, true);
                calendar.multiselect._update(div);
            }

            if (calendar._api2()) {
                if (typeof calendar.onAfterEventRender === 'function') {
                    var args = {};
                    args.e = div.event;
                    args.div = div;

                    calendar.onAfterEventRender(args);
                }
            }
            else {
                if (calendar.afterEventRender) {
                    calendar.afterEventRender(div.event, div);
                }
            }

            return true;
        };

        this._api2 = function() {
            return calendar.api === 2;
        };

        this._updateEventTops = function() {
            for (var i = 0; i < this.elements.events.length; i++) {
                var div = this.elements.events[i];
                var event = div.event;
                var rowIndex = event.part.dayIndex;
                var row = this.rowlist[rowIndex];
                var rowTop = row.top;
                var top = rowTop + event.part.top;
                div.style.top = top + 'px';

                if (DayPilot.isArray(div.related)) {
                    for (var j = 0; j < div.related.length; j++) {
                        var rel = div.related[j];
                        if (rel.type === "divLeft" || rel.type === "divRight") {
                            rel.style.top = top + 'px';
                        }
                    }
                }
                /*
                if (div.bar) {
                    div.bar.style.top = (rowTop + event.part.top + 10) + 'px'; // HACK
                }*/
            }
        };

        this._findEventDiv = function(e) {
            if (!e) {
                return null;
            }
            for (var i = 0; i < calendar.elements.events.length; i++) {
                var div = calendar.elements.events[i];
                if (div.event === e || div.event.data === e.data) {
                    return div;
                }
            }
            return null;
        };

        this._onEventMouseOut = function(ev) {
            var div = this;

            DayPilot.Areas.hideAreas(div, ev);

            if (calendar.cssOnly) {
                DayPilot.Util.removeClass(div, calendar._prefixCssClass("_event_hover"));
            }

            calendar._doEventMouseOut(div);


            if (!linking.source) {
                linktools.hideLinkpointsWithDelay();
            }

            if (calendar.bubble && calendar.eventHoverHandling === 'Bubble') {
                calendar.bubble.hideOnMouseOut();
            }

        };

        this._doEventMouseOver = function(div) {
            if (typeof this.onEventMouseOver === "function") {
                var args = {};
                args.div = div;
                args.e = div.event;

                this.onEventMouseOver(args);
            }
        };

        this._doEventMouseOut = function(div) {
            if (typeof this.onEventMouseOut === "function") {
                var args = {};
                args.div = div;
                args.e = div.event;

                this.onEventMouseOut(args);
            }
        };

        this._onEventMouseMove = function(ev) {
            ev = ev || window.event;

            if (calendar.cellBubble) { calendar.cellBubble.delayedHide(); }

            var div = this;
            while (div && !div.event) { // make sure it's the top event div
                div = div.parentNode;
            }

            calendar._eventUpdateCursor(div, ev);

            if (!div.active) {
                var areas = [];
                if (calendar.eventDeleteHandling !== "Disabled") {
                    var top = calendar.durationBarVisible ? calendar.durationBarHeight : 0;
                    areas.push({"action":"JavaScript","v":"Hover","w":17,"h":17,"top": top + 2,"right":2, "css": calendar._prefixCssClass("_event_delete"),"js":function(e) { calendar._eventDeleteDispatch(e); } });
                }

                var list = div.event.cache ? div.event.cache.areas : div.event.data.areas;
                if (list && list.length > 0) {
                    areas = areas.concat(list);
                }
                DayPilot.Areas.showAreas(div, div.event, null, areas);
                if (calendar.cssOnly) {
                    DayPilot.Util.addClass(div, calendar._prefixCssClass("_event_hover"));
                }

                calendar._doEventMouseOver(div);

            }

            if (calendar.linkCreateHandling !== "Disabled" && !linking.source) {
                linktools.clearHideTimeout();
                linktools.hideLinkpoints();
                if (!DayPilotScheduler.moving && !DayPilotScheduler.resizing) {
                    linktools.showLinkpoint(div);
                }
            }

            if (ev.srcElement) {
                ev.srcElement.insideEvent = true;
            }
            else {
                ev.insideEvent = true;
            }

            // bubbling must be allowed, required for moving and resizing
            //ev.cancelBubble = true;

        };


        this._moving = {};
        var moving = this._moving;

        this._onEventMouseDown = function(ev) {
            calendar._out();
            /*
            calendar._crosshairHide();
            calendar._stopScroll();
            */

            if (typeof DayPilot.Bubble !== 'undefined') {
                DayPilot.Bubble.hideActive();
            }

            ev = ev || window.event;
            var button = DayPilot.Util.mouseButton(ev);

            if (button.left) {
                if (this.style.cursor === 'w-resize' || this.style.cursor === 'e-resize') {

                    // set
                    DayPilotScheduler.resizing = this;
                    DayPilotScheduler.originalMouse = DayPilot.mc(ev);

                    // cursor
                    document.body.style.cursor = this.style.cursor;
                    linktools.hideLinkpoints();
                }
                else if (this.style.cursor === 'move') {
                    //var mv = this;
                    DayPilotScheduler.moving = this;
                    DayPilotScheduler.originalMouse = DayPilot.mc(ev);

                    DayPilotScheduler.moveOffsetX = DayPilot.mo3(this, ev).x;
                    DayPilotScheduler.moveDragStart = calendar.getDate(calendar.coords.x, true);

                    // cursor
                    document.body.style.cursor = 'move';
                    linktools.hideLinkpoints();
                }
                else if (calendar.moveBy === 'Full' && this.event.client.moveEnabled()) {
                    moving.start = true;
                    moving.moving = this;
                    moving.originalMouse = DayPilot.mc(ev);
                    moving.moveOffsetX = DayPilot.mo3(this, ev).x;
                    moving.moveDragStart = calendar.getDate(calendar.coords.x, true);
                    linktools.hideLinkpoints();

                }
            }

            ev.cancelBubble = true;

            return false;
        };

        this._touch = {};
        var touch = calendar._touch;

        touch.active = false;
        touch.start = false;
        touch.timeouts = [];

        touch.onEventTouchStart = function(ev) {
            // iOS
            if (touch.active || touch.start) {
                return;
            }

            touch.clearTimeouts();

            touch.start = true;
            touch.active = false;

            var div = this;

            var holdfor = 500;
            touch.timeouts.push(window.setTimeout(function() {
                touch.active = true;
                touch.start = false;

                calendar.coords = touch.relativeCoords(ev);
                touch.startMoving(div, ev);

                ev.preventDefault();
            }, holdfor));

            // prevent onMainTouchStart
            ev.stopPropagation();

        };

        touch.onEventTouchMove = function(ev) {
            touch.clearTimeouts();
            touch.start = false;
        };

        touch.onEventTouchEnd = function(ev) {
            touch.clearTimeouts();

            // quick tap
            if (touch.start) {
                calendar._eventClickSingle(this, ev);
            }

            window.setTimeout(function() {
                touch.start = false;
                touch.active = false;
            }, 500);
        };

        touch.onMainTouchStart = function(ev) {
            // prevent after-alert firing on iOS
            if (touch.active || touch.start) {
                return;
            }

            touch.clearTimeouts();

            touch.start = true;
            touch.active = false;

            var holdfor = 500;
            touch.timeouts.push(window.setTimeout(function() {
                touch.active = true;
                touch.start = false;

                ev.preventDefault();

                calendar.coords = touch.relativeCoords(ev);
                touch.range = calendar._rangeFromCoords();
            }, holdfor));
        };

        touch.onMainTouchMove = function(ev) {
            touch.clearTimeouts();

            touch.start = false;

            if (touch.active) {
                ev.preventDefault();

                calendar.coords = touch.relativeCoords(ev);

                if (DayPilotScheduler.moving) {
                    touch.updateMoving();
                    return;
                }

                if (touch.range) {
                    var range = touch.range;
                    range.end = {
                        x: Math.floor(calendar.coords.x / calendar.cellWidth)
                    };

                    calendar._drawRange(range);
                }
            }

        };

        touch.onMainTouchEnd = function(ev) {
            touch.clearTimeouts();

            if (touch.active) {
                if (DayPilotScheduler.moving) {
                    ev.preventDefault();

                    var e = DayPilotScheduler.moving.event;

                    var newStart = DayPilotScheduler.movingShadow.start;
                    var newEnd = DayPilotScheduler.movingShadow.end;
                    var newResource = (calendar.viewType !== 'Days') ? DayPilotScheduler.movingShadow.row.id : null;
                    var external = DayPilotScheduler.drag ? true : false;
                    //var line = DayPilotScheduler.movingShadow.line;

                    // clear the moving state
                    DayPilot.de(DayPilotScheduler.movingShadow);
                    calendar._clearShadowHover();
                    DayPilotScheduler.movingShadow.calendar = null;
                    document.body.style.cursor = '';
                    DayPilotScheduler.moving = null;
                    DayPilotScheduler.movingShadow = null;

                    calendar._eventMoveDispatch(e, newStart, newEnd, newResource, external);

                }

                if (touch.range) {
                    var sel = calendar._getSelection(touch.range);
                    touch.range = null;
                    calendar._timeRangeSelectedDispatch(sel.start, sel.end, sel.resource);
                }
            }

            window.setTimeout(function() {
                touch.start = false;
                touch.active = false;
            }, 500);

        };

        touch.clearTimeouts = function() {
            for (var i = 0; i < touch.timeouts.length; i++) {
                clearTimeout(touch.timeouts[i]);
            }
            touch.timeouts = [];
        };

        touch.relativeCoords = function(ev) {
            var ref = calendar._maind;

            var x = ev.touches[0].pageX;
            var y = ev.touches[0].pageY;

            var abs = DayPilot.abs(ref);
            var coords = {x: x - abs.x, y: y - abs.y, toString: function() { return "x: " + this.x + ", y:" + this.y; } };
            return coords;
        };

        // coords - page coords
        touch.startMoving = function(div, ev) {
            var coords = { x: ev.touches[0].pageX, y : ev.touches[0].pageY };

            DayPilotScheduler.moving = div;
            DayPilotScheduler.originalMouse = coords;

            var absE = DayPilot.abs(div);
            DayPilotScheduler.moveOffsetX = coords.x - absE.x;

            //var absR = DayPilot.abs(calendar.maind);
            //var x = coords.x - absR.x;
            DayPilotScheduler.moveDragStart = calendar.getDate(calendar.coords.x, true);

            DayPilotScheduler.movingShadow = calendar._createShadow(div, calendar.shadow);

            // update dimensions
            calendar._moveShadow();

        };

        // coords - relative to maind
        touch.updateMoving = function() {
            if (DayPilotScheduler.movingShadow && DayPilotScheduler.movingShadow.calendar !== calendar) {
                DayPilotScheduler.movingShadow.calendar = null;
                DayPilot.de(DayPilotScheduler.movingShadow);
                DayPilotScheduler.movingShadow = null;
            }
            if (!DayPilotScheduler.movingShadow) {
                var mv = DayPilotScheduler.moving;
                DayPilotScheduler.movingShadow = calendar._createShadow(mv, calendar.shadow);
            }

            DayPilotScheduler.moving.target = calendar; //might not be necessary, the target is in DayPilotScheduler.activeCalendar
            calendar._moveShadow();
        };

        this._eventUpdateCursor = function(div, ev) {

/*
            if (calendar.moveBy === "Disabled" || calendar.moveBy === "None") {
                return;
            }
*/
            // const
            var resizeMargin = this.eventResizeMargin;
            var moveMargin = this.eventMoveMargin;

            var object = div;

            if (typeof (DayPilotScheduler) === 'undefined') {
                return;
            }

            // position
            var offset = DayPilot.mo3(div, ev);
            if (!offset) {
                return;
            }

            calendar.eventOffset = offset;

            if (DayPilotScheduler.resizing) {
                return;
            }

            var isFirstPart = object.event.part.start.toString() === object.event.start().toString();
            var isLastPart = object.event.part.end.toString() === object.event.end().toString();

            // top
            if (calendar.moveBy === 'Top' && offset.y <= moveMargin && object.event.client.moveEnabled() && calendar.eventMoveHandling !== 'Disabled') {  // TODO disabled check not necessary
                if (isFirstPart) {
                    div.style.cursor = 'move';
                }
                else {
                    div.style.cursor = 'not-allowed';
                }
            }
            // left resizing
            else if ((calendar.moveBy === 'Top' || calendar.moveBy === 'Full') && offset.x <= resizeMargin && object.event.client.resizeEnabled() && calendar.eventResizeHandling !== 'Disabled') {  // TODO disabled check not necessary
                if (isFirstPart) {
                    div.style.cursor = "w-resize";
                    div.dpBorder = 'left';
                }
                else {
                    div.style.cursor = 'not-allowed';
                }
            }
            // left moving
            else if (calendar.moveBy === 'Left' && offset.x <= moveMargin && object.event.client.moveEnabled() && calendar.eventMoveHandling !== 'Disabled') {  // TODO disabled check not necessary
                if (isFirstPart) {
                    div.style.cursor = "move";
                }
                else {
                    div.style.cursor = 'not-allowed';
                }
            }
            // right
            else if (div.offsetWidth - offset.x <= resizeMargin && object.event.client.resizeEnabled() && calendar.eventResizeHandling !== 'Disabled') {  // TODO disabled check not necessary
                if (isLastPart) {
                    div.style.cursor = "e-resize";
                    div.dpBorder = 'right';
                }
                else {
                    div.style.cursor = 'not-allowed';
                }
            }
            else if (!DayPilotScheduler.resizing && !DayPilotScheduler.moving) {
                if (object.event.client.clickEnabled() && calendar.eventClickHandling !== 'Disabled') {  // TODO disabled check not necessary
                    div.style.cursor = 'pointer';
                }
                else {
                    div.style.cursor = 'default';
                }
            }


            if (typeof (DayPilotBubble) !== 'undefined' && calendar.bubble && calendar.eventHoverHandling === 'Bubble') {
                if (div.style.cursor === 'default' || div.style.cursor === 'pointer') {
                    // preventing Chrome bug
                    var notMoved = this._lastOffset && offset.x === this._lastOffset.x && offset.y === this._lastOffset.y;
                    if (!notMoved) {
                        this._lastOffset = offset;
                        calendar.bubble.showEvent(div.event);
                    }
                }
                else {
                    /*
                    // disabled, now it is hidden on click
                    DayPilotBubble.hideActive();
                    */
                }
            }
        };

        this._cellCount = function() {
            if (this.viewType !== 'Days') {
                return this.itline.length;

                // TODO broken for nonbusiness
                //return Math.floor(this.days * 24 * 60 / this.cellDuration);
            }
            else {
                return Math.floor(24 * 60 / this.cellDuration);
            }
        };

        this._getSelection = function(range) {
            //var range = DayPilotScheduler.range;

            var range = range || DayPilotScheduler.range || DayPilotScheduler.rangeHold;

            if (!range) {
                return null;
            }

            var row = calendar.rowlist[range.start.y];

            if (!row) {
                return null;
            }

            var resource = row.id;
            var startX = range.end.x > range.start.x ? range.start.x : range.end.x;
            var endX = (range.end.x > range.start.x ? range.end.x : range.start.x);

            var rowOffset = row.start.getTime() - this._visibleStart().getTime();

            var start = this.itline[startX].start.addTime(rowOffset);
            var end = this.itline[endX].end.addTime(rowOffset);

            return new DayPilot.Selection(start, end, resource, calendar);
        };

        this._createEdit = function(object) {
            var parentTd = object.parentNode;

            var edit = document.createElement('textarea');
            edit.style.position = 'absolute';
            edit.style.width = ((object.offsetWidth < 100) ? 100 : (object.offsetWidth - 2)) + 'px';
            edit.style.height = (object.offsetHeight - 2) + 'px'; //offsetHeight
            edit.style.fontFamily = DayPilot.gs(object, 'fontFamily') || DayPilot.gs(object, 'font-family');
            edit.style.fontSize = DayPilot.gs(object, 'fontSize') || DayPilot.gs(object, 'font-size');
            edit.style.left = object.offsetLeft + 'px';
            edit.style.top = object.offsetTop + 'px';
            edit.style.border = '1px solid black';
            edit.style.padding = '0px';
            edit.style.marginTop = '0px';
            edit.style.backgroundColor = 'white';
            edit.value = DayPilot.tr(object.event.text());

            edit.event = object.event;
            parentTd.appendChild(edit);
            return edit;
        };


        this._divEdit = function(object) {
            if (DayPilotScheduler.editing) {
                DayPilotScheduler.editing.blur();
                return;
            }

            var edit = this._createEdit(object);
            DayPilotScheduler.editing = edit;

            edit.onblur = function() {
                //var id = object.event.value();
                //var tag = object.event.tag();
                var oldText = object.event.text();
                var newText = edit.value;

                DayPilotScheduler.editing = null;
                if (edit.parentNode) {
                    edit.parentNode.removeChild(edit);
                }

                if (oldText === newText) {
                    return;
                }

                object.style.display = 'none';
                calendar._eventEditDispatch(object.event, newText);
            };

            edit.onkeypress = function(e) {
                var keynum = (window.event) ? event.keyCode : e.keyCode;

                if (keynum === 13) {
                    this.onblur();
                    return false;
                }
                else if (keynum === 27) {
                    edit.parentNode.removeChild(edit);
                    DayPilotScheduler.editing = false;
                }

                return true;
            };

            edit.select();
            edit.focus();
        };

        this._onResMouseMove = function(ev) {
            var td = this;
            if (typeof (DayPilotBubble) !== 'undefined') {
                if (calendar.cellBubble) {
                    calendar.cellBubble.hideOnMouseOut();
                }
                if (calendar.resourceBubble) {
                    var res = {};
                    res.calendar = calendar;
                    res.id = td.row.id;
                    res.toJSON = function() {
                        var json = {};
                        json.id = this.id;
                        return json;
                    };
                    calendar.resourceBubble.showResource(res);
                }
            }

            /*
            var div = td.firstChild; // rowheader
            if (!div.active) {
                var row = calendar.rowlist[td.index];
                var r = calendar._createRowObject(row);
                r.areas = row.areas;
                DayPilot.Areas.showAreas(div, r);
            }
            */
        };

        this._onResMouseOut = function(ev) {
            var td = this;
            if (typeof (DayPilotBubble) !== 'undefined' && calendar.resourceBubble) {
                calendar.resourceBubble.hideOnMouseOut();
            }

            var div = td.firstChild;

            DayPilot.Areas.hideAreas(div, ev);
            div.data = null;
        };

        this._onResMouseUp = function(ev) {
            if (rowmoving.row) {
                // testing a hack
                rowtools.cancelClick = true;
                setTimeout(function() {
                    rowtools.cancelClick = false;
                }, 100);
            }
        };

        this._drawTimeHeader = function() {

            this._drawTimeHeader2();
            return;
        };

        this._drawTimeHeader2 = function() {

            if (!this.timeHeader) {
                calendar.debug.message("drawTimeHeader: timeHeader not available");
                return; // mvc shortInit
            }

            this._cache.timeHeader = {};

            var header = document.createElement("div");
            header.style.position = "relative";
            this.nav.timeHeader = header;

            for (var y = 0; y < this.timeHeader.length; y++) {
                var row = this.timeHeader[y];
                for (var x = 0; x < row.length; x++) {
                    this._drawTimeHeaderCell2(x, y);
                }
            }

            var north = this.divNorth;
            DayPilot.puc(north);
            north.innerHTML = '';
            north.appendChild(header);
            //north.style.width = (this._cellCount() * this.cellWidth + 5000) + "px";
            var gridwidth = this._getGridWidth();
            north.style.width = (this._getGridWidth() + 5000) + "px";

            var corner = this.divCorner;
            if (!this.cssOnly) {
                corner.style.backgroundColor = this.cornerBackColor;
            }

            if (this.cornerHtml) {
                corner.innerHTML = this.cornerHtml;
            }
            else {
                corner.innerHTML = '';
            }

            var gridwidth = this._getGridWidth();

            if (gridwidth > 0) {
                this.divStretch.style.width = (this._getGridWidth()) + "px";
            }

        };

        this._getGroupName = function(h, cellGroupBy) {
            var html = null;
            var locale = this._resolved.locale();

            var cellGroupBy = cellGroupBy || this.cellGroupBy;

            var from = h.start;
            var to = h.end;
            var locale = this._resolved.locale();

            switch (cellGroupBy) {
                case 'Hour':
                    html = (calendar._resolved.timeFormat() === 'Clock12Hours') ? from.toString("h tt", locale) : from.toString("H", locale);
                    break;
                case 'Day':
                    html = from.toString(locale.datePattern);
                    break;
                case 'Week':
                    html = resolved.weekStarts() === 1 ? from.weekNumberISO() : from.weekNumber(); // TODO format
                    break;
                case 'Month':
                    html = from.toString("MMMM yyyy", locale);
                    break;
                case 'Year':
                    html = from.toString("yyyy");
                    break;
                case 'None':
                    html = '';
                    break;
                case 'Cell':
                    if (this.scale === 'Manual' || this.scale === 'CellDuration') {  // hard-to-guess cell sizes
                        var duration = (h.end.ticks - h.start.ticks) / 60000;
                        html = this._getCellName(from, duration);
                    }
                    else {
                        html = this._getGroupName(h, this.scale);
                    }
                    break;
                default:
                    throw 'Invalid cellGroupBy value';
            }

            return html;
        };

        this._getCellName = function(start, duration) {
            var locale = this._resolved.locale();
            var duration = duration || this.cellDuration;
            if (duration < 60) // smaller than hour, use minutes
            {
                return start.toString("mm"); //String.Format("{0:00}", from.Minute);
            }
            else if (duration < 1440) // smaller than day, use hours
            {
                //return DayPilot.Date.hours(start.d, calendar._resolved.timeFormat() === 'Clock12Hours');
                return calendar._resolved.timeFormat() === 'Clock12Hours' ? start.toString("h tt", locale) : start.toString("H", locale);
            }
            else if (duration < 10080) // use days
            {
                return start.toString("d");
            }
            else if (duration === 10080) {
                return resolved.weekStarts() === 1 ? start.weekNumberISO() : start.weekNumber(); // TODO format
            }
            else
            {
                return start.toString("MMMM yyyy", locale);
            }
        };

        this._addScaleSize = function(from) {
            var scale = this.scale;
            switch (scale) {
                case "Cell":
                    throw "Invalid scale: Cell";
                case "Manual":
                    throw "Internal error (addScaleSize in Manual mode)";
                case "Minute":
                    return from.addMinutes(1);
                case "CellDuration":
                    return from.addMinutes(this.cellDuration);
                default:
                    return this._addGroupSize(from, scale);
            }
        };

        this._addGroupSize = function(from, cellGroupBy) {
            var to;

            var daysHorizontally = this.viewType !== 'Days' ? this.days : 1;
            var endDate = this.startDate.addDays(daysHorizontally);

            var cellGroupBy = cellGroupBy || this.cellGroupBy;
            var cellDuration = 60; // dummy value to make sure it's aligned properly

            switch (cellGroupBy) {
                case 'Hour':
                    to = from.addHours(1);
                    break;
                case 'Day':
                    to = from.addDays(1);
                    break;
                case 'Week':
                    to = from.addDays(1);
                    while (to.dayOfWeek() !== resolved.weekStarts()) {
                        to = to.addDays(1);
                    }
                    break;
                case 'Month':
                    to = from.addMonths(1);
                    to = to.firstDayOfMonth();

                    //var minDiff =
                    var isInt = (DayPilot.Date.diff(to.d, from.d) / (1000.0 * 60)) % cellDuration === 0;
                    while (!isInt) {
                        to = to.addHours(1);
                        isInt = (DayPilot.Date.diff(to.d, from.d) / (1000.0 * 60)) % cellDuration === 0;
                    }
                    break;
                case 'Year':
                    to = from.addYears(1);
                    to = to.firstDayOfYear();

                    var isInt = (DayPilot.Date.diff(to.d, from.d) / (1000.0 * 60)) % cellDuration === 0;
                    while (!isInt) {
                        to = to.addHours(1);
                        isInt = (DayPilot.Date.diff(to.d, from.d) / (1000.0 * 60)) % cellDuration === 0;
                    }
                    break;
                case 'None':
                    to = endDate;
                    break;
                case 'Cell':
                    var cell = this._getItlineCellFromTime(from);
                    if (cell.current)
                    {
                        to = cell.current.end;
                    }
                    else
                    {
                        if (cell.past) {
                            to = cell.previous.end;
                        }
                        else {
                            to = cell.next.start;
                        }
                        /*
                        var cursor = from;
                        while (cell === null)
                        {
                            cursor = cursor.addMinutes(1);
                            cell = this._getItlineCellFromTime(cursor);
                        }
                        to = cell.start;
                                                */
                    }
                    break;
                default:
                    throw 'Invalid cellGroupBy value';
            }
            if (to.getTime() > endDate.getTime()) {
                to = endDate;
            }

            return to;
        };

        this._drawTimeHeaderCell2 = function(x, y) {

            var header = this.nav.timeHeader;

            var p = this.timeHeader[y][x];

            var isGroup = y < this.timeHeader.length - 1;
            var top = y * resolved.headerHeight();
            var left = p.left;
            var width = p.width;
            var height = resolved.headerHeight();

            var cell = document.createElement("div");
            cell.style.position = "absolute";
            cell.style.top = top + "px";
            cell.style.left = left + "px";
            cell.style.width = width + "px";
            cell.style.height = height + "px";
            if (p.toolTip) {
                cell.title = p.toolTip;
            }

            cell.setAttribute("unselectable", "on");
            cell.style.KhtmlUserSelect = 'none';
            cell.style.MozUserSelect = 'none';
            cell.style.webkitUserSelect = 'none';

            cell.oncontextmenu = function() { return false; };
            cell.cell = {};
            cell.cell.start = p.start;
            cell.cell.end = p.end;
            cell.cell.level = y;
            cell.cell.th = p;
            cell.onclick = this._onTimeClick;

            cell.style.overflow = 'hidden';

            if (!this.cssOnly) {
                var isLast = y === this.timeHeader.length - 1;
                cell.style.textAlign = "center";
                cell.style.backgroundColor = (typeof cell.backColor === 'undefined') ? calendar.hourNameBackColor : cell.backColor;
                cell.style.fontFamily = this.hourFontFamily;
                cell.style.fontSize = this.hourFontSize;
                cell.style.color = this.headerFontColor;
                cell.style.cursor = 'default';
                cell.style.border = '0px none';
                if (!isLast) {
                    cell.style.height = (height - 1) + "px";
                    cell.style.borderBottom = "1px solid " + this.borderColor;
                }
                cell.style.width = (width - 1) + "px";
                cell.style.borderRight = "1px solid " + this.hourNameBorderColor;
                cell.style.whiteSpace = 'nowrap';
                cell.className = this._prefixCssClass('timeheadergroup');
            }

            var inner = document.createElement("div");
            inner.setAttribute("unselectable", "on");
            inner.innerHTML = p.innerHTML;

            if (p.backColor) {
                inner.style.background = p.backColor;
            }

            if (this.cssOnly) {
                var cl = this._prefixCssClass("_timeheadercol");
                var cli = this._prefixCssClass("_timeheadercol_inner");
                if (isGroup) {
                    cl = this._prefixCssClass("_timeheadergroup");
                    cli = this._prefixCssClass("_timeheadergroup_inner");
                }
                DayPilot.Util.addClass(cell, cl);
                DayPilot.Util.addClass(inner, cli);
            }

            cell.appendChild(inner);

            this._cache.timeHeader[x + "_" + y] = cell;

            header.appendChild(cell);
        };

        this._updateRowHeights = function() {
            for (var i = 0; i < this.rowlist.length; i++) {
                var row = this.rowlist[i];
                var updated = row.getHeight() + row.marginBottom + row.marginTop;
                if (row.height !== updated  || typeof calendar.onBeforeCellRender === "function") {
                    this._rowsDirty = true;
                }
                row.height = updated;
            }
        };

        this._updateRowHeaderHeights = function() {
            var header = this.divHeader;

            if (!header) {
                return false;
            }

            var len = this.rowlist.length;

            var columns = this.rowHeaderCols ? this.rowHeaderCols.length : 1;
            //var updated = false;

            // row headers
            var j = 0; // real TR index
            //var max = 0;
            for (var i = 0; i < len; i++) {
                var row = this.rowlist[i];
                if (row.hidden) {
                    continue;
                }

                var index = this._resHeaderDivBased ? i : j;

                if (!header.rows[index]) {
                    continue;
                }

                for (var c = 0; c < header.rows[index].cells.length; c++) {
                    var headerCell = header.rows[index].cells[c];

                    if (this._resHeaderDivBased) {
                        headerCell.style.top = row.top + "px";
                    }

                    var newHeight = row.height;

                    if (headerCell && headerCell.firstChild && parseInt(headerCell.firstChild.style.height, 10) !== newHeight) {
                        headerCell.firstChild.style.height = newHeight + "px";
                    }
                }

                j++;
            }

            if (this._resHeaderDivBased) {
                if (calendar.nav.resScrollSpace) {
                    calendar.nav.resScrollSpace.style.top = calendar._innerHeightTree + "px";
                }
            }

        };

        this._drawSeparator = function(index) {
            var s = this.separators[index];


            // fix
            s.location = s.location || s.Location;
            s.color = s.color || s.Color;
            s.layer = s.layer || s.Layer;
            s.width = s.width || s.Width;
            s.opacity = s.opacity || s.Opacity;

            var time = new DayPilot.Date(s.location);
            var color = s.color;
            var width = s.width ? s.width : 1;
            var above = s.layer ? s.layer === 'AboveEvents' : false;
            var opacity = s.opacity ? s.opacity : 100;

            // check the start and end dates of the visible area
            if (time.getTime() < this.startDate.getTime()) {
                return;
            }
            if (time.getTime() >= this.startDate.addDays(this.days).getTime()) {
                return;
            }

            var pixels = this.getPixels(time);

            // check if it's in the hidden area, don't show in that case
            if (pixels.cut) {
                return;
            }

            if (pixels.left < 0) {
                return;
            }
            if (pixels.left > this._cellCount() * this.cellWidth) {
                return;
            }

            var line = document.createElement("div");
            line.style.width = width + 'px';
            line.style.height = calendar._innerHeightTree + 'px';
            line.style.position = 'absolute';
            line.style.left = (pixels.left - 1) + 'px';
            line.style.top = '0px';
            line.style.backgroundColor = color;
            line.style.opacity = opacity / 100;
            line.style.filter = "alpha(opacity=" + opacity + ")";

            if (above) {
                this.divSeparatorsAbove.appendChild(line);
            }
            else {
                this.divSeparators.appendChild(line);
            }

            this.elements.separators.push(line);
        };


        this._onMaindDblClick = function(ev) {
            if (calendar.timeRangeDoubleClickHandling === 'Disabled') {
                return false;
            }

            if (DayPilotScheduler.timeRangeTimeout) {
                clearTimeout(DayPilotScheduler.timeRangeTimeout);
                DayPilotScheduler.timeRangeTimeout = null;
            }

            var range = {};

            // make sure that coordinates are set
            if (!calendar.coords) {
                var ref = calendar._maind;
                calendar.coords = DayPilot.mo3(ref, ev);
            }

            ev = ev || window.event;

            // only process left and right button outside of selection
            if (calendar._isWithinRange(calendar.coords)) {
                var sel = calendar._getSelection(DayPilotScheduler.rangeHold);
                calendar._timeRangeDoubleClickDispatch(sel.start, sel.end, sel.resource);
            }
            else {
                DayPilotScheduler.range = calendar._rangeFromCoords();
                if (DayPilotScheduler.range) {
                    var sel = calendar._getSelection(DayPilotScheduler.range);
                    calendar._timeRangeDoubleClickDispatch(sel.start, sel.end, sel.resource);
                }
            }

            DayPilotScheduler.rangeHold = DayPilotScheduler.range;
            DayPilotScheduler.range = null;

        };

/*
        this._clearDblClickTimeout = function() {
            calendar.dblclick = null;
        };
*/
        // handles:
        // - TimeRangeSelected
        this._onMaindMouseDown = function(ev) {

            if (touch.start) {
                return;
            }

            if (DayPilotScheduler.timeRangeTimeout && false) {
                clearTimeout(DayPilotScheduler.timeRangeTimeout);
                DayPilotScheduler.timeRangeTimeout = null;
            }

            calendar._crosshairHide();
            calendar._stopScroll();

            // make sure that coordinates are set
            if (!calendar.coords) {
                var ref = calendar._maind;
                calendar.coords = DayPilot.mo3(ref, ev);
            }

            ev = ev || window.event;
            var button = DayPilot.Util.mouseButton(ev);

            if (button.middle || (button.right && calendar._isWithinRange(calendar.coords))) {
                return false;
            }

            if (calendar.timeRangeSelectedHandling === 'Disabled') {
                return false;
            }

            var i = calendar._getRow(calendar.coords.y).i;
            var row = calendar.rowlist[i];
            if (row.isNewRow) {
                return false;
            }

            DayPilotScheduler.range = calendar._rangeFromCoords();

            return false; // prevent FF3 bug (?), dragging is otherwise activated and DayPilot.mo2 gives incorrect results
        };

        // creates a single cell range selection at the current position (calendar.coords)
        this._rangeFromCoords = function() {
            var range = {};

            var cx = this._getItlineCellFromPixels(calendar.coords.x).x;

            range.start = {
                y: calendar._getRow(calendar.coords.y).i,
                x: cx
            };

            range.end = {
                x: cx
            };

            if (this._isRowDisabled(calendar._getRow(calendar.coords.y).i)) {
                return null;
            }

            //return false;

            range.calendar = calendar;
            //DayPilotScheduler.range = range;

            calendar._drawRange(range);

            return range;
        };

        this._doEventResizing = function() {
            calendar._updateResizingShadow();

            var shadow = DayPilotScheduler.resizingShadow;
            var ev = DayPilotScheduler.resizing;

            (function() {

                var last = calendar._lastEventResizing;

                // don't fire the event if there is no change
                if (last && last.start.getTime() === shadow.start.getTime() && last.end.getTime() === shadow.end.getTime()) {
                    return;
                }

                var args = {};
                args.start = shadow.start;
                args.end = shadow.end;
                args.e = ev;
                args.left = {};
                args.left.html = args.start.toString(calendar.eventResizingStartEndFormat);
                args.left.enabled = calendar.eventResizingStartEndEnabled;
                args.right = {};
                args.right.html = args.end.toString(calendar.eventResizingStartEndFormat);
                args.right.enabled = calendar.eventResizingStartEndEnabled;

                calendar._lastEventResizing = args;

                if (typeof calendar.onEventResizing === 'function') {
                    calendar.onEventResizing(args);
                }

                calendar._showShadowHover(DayPilotScheduler.resizingShadow, args);
            })();

        };

        // handles:
        // - EventMove (including external)
        // - EventResize
        // - TimeRangeSelected
        //
        // saves calendar.coords
        this._onMaindMouseMove = function(ev) {
            if (touch.active) {
                return;
            }

            DayPilotScheduler.activeCalendar = calendar; // required for moving
            ev = ev || window.event;
            var mousePos = DayPilot.mc(ev);

            calendar.coords = DayPilot.mo3(calendar._maind, ev);

            //ev = ev || window.event;
            ev.insideMainD = true;
            if (window.event) {
                window.event.srcElement.inside = true;
            }

            if (moving.start) {
                if (moving.originalMouse.x !== mousePos.x || moving.originalMouse.y !== mousePos.y) {
                    DayPilot.Util.copyProps(moving, DayPilotScheduler);
                    document.body.style.cursor = 'move';
                    moving = {};
                }
            }

            if (DayPilotScheduler.resizing && DayPilotScheduler.resizing.event.calendar === calendar) {
                if (!DayPilotScheduler.resizingShadow) {
                    DayPilotScheduler.resizingShadow = calendar._createShadow(DayPilotScheduler.resizing, calendar.shadow);
                }
                var _step = DayPilotScheduler.resizing.event.calendar.cellWidth;
                var originalWidth = DayPilotScheduler.resizing.event.part.width;
                var originalLeft = DayPilotScheduler.resizing.event.part.left;
                var _startOffset = 0;
                var delta = (mousePos.x - DayPilotScheduler.originalMouse.x);

                var newLeft, newWidth;

                if (DayPilotScheduler.resizing.dpBorder === 'right') {
                    newLeft = originalLeft;
                    if (calendar.snapToGrid) {
                        //newWidth = Math.ceil(((originalWidth + originalLeft + delta)) / _step) * _step - originalLeft;
                        var itc =  calendar._getItlineCellFromPixels(originalWidth + originalLeft + delta).cell;
                        var newRight = itc.left + itc.width;
                        newWidth = newRight - originalLeft;

                        if (newWidth < _step) {
                            newWidth = _step;
                        }

                    }
                    else {
                        newWidth = originalWidth + delta;
                    }

                    var max = calendar._getGridWidth();

                    if (originalLeft + newWidth > max) {
                        newWidth = max - originalLeft;
                    }

                    DayPilotScheduler.resizingShadow.style.width = (newWidth) + 'px';
                }
                else if (DayPilotScheduler.resizing.dpBorder === 'left') {
                    if (calendar.snapToGrid) {
                        if (delta >= originalWidth) {
                            delta = originalWidth;
                        }
                        newLeft = Math.floor(((originalLeft + delta) + 0) / _step) * _step;
                        if (newLeft < _startOffset) {
                            newLeft = _startOffset;
                        }
                    }
                    else {
                        newLeft = originalLeft + delta;
                    }

                    newWidth = originalWidth - (newLeft - originalLeft);
                    var right = originalLeft + originalWidth;

                    var min = _step;

                    if (!calendar.snapToGrid) {
                        min = 1;
                    }
                    else if (calendar.useEventBoxes === "Never") {
                        if (originalWidth < _step) {
                            min = originalWidth;
                        }
                        else {
                            min = 1;
                        }
                    }

                    if (newWidth < min) {
                        newWidth = min;
                        newLeft = right - newWidth;
                    }

                    DayPilotScheduler.resizingShadow.style.left = newLeft + 'px';
                    DayPilotScheduler.resizingShadow.style.width = (newWidth) + 'px';
                }


                (function checkOverlap() {
                    var ev = DayPilotScheduler.resizing.event;
                    var row = calendar.rowlist[ev.part.dayIndex];
                    var shadow = DayPilotScheduler.resizingShadow;
                    var left = newLeft;
                    var width = newWidth;

                    calendar._overlappingShadow(DayPilotScheduler.resizingShadow, row, left, width, ev.data);
                })();



                calendar._doEventResizing();
            }
            else if (DayPilotScheduler.moving  && (DayPilotScheduler.moving.event.calendar === calendar || DayPilotScheduler.moving.event.calendar.dragOutAllowed)) {
                if (DayPilotScheduler.movingShadow && DayPilotScheduler.movingShadow.calendar !== calendar) {
                    DayPilotScheduler.movingShadow.calendar = null;
                    DayPilot.de(DayPilotScheduler.movingShadow);
                    DayPilotScheduler.movingShadow = null;
                }
                if (!DayPilotScheduler.movingShadow) {
                    var mv = DayPilotScheduler.moving;
                    DayPilotScheduler.movingShadow = calendar._createShadow(mv, calendar.shadow);
                }

                calendar._expandParent();

                DayPilotScheduler.moving.target = calendar; //might not be necessary, the target is in DayPilotScheduler.activeCalendar
                calendar._moveShadow();
            }
            else if (DayPilotScheduler.range && DayPilotScheduler.range.calendar === calendar) {

                var range = DayPilotScheduler.range;
                range.end = {
                    //x: Math.floor(calendar.coords.x / calendar.cellWidth)
                    x: calendar._getItlineCellFromPixels(calendar.coords.x).x
                };

                calendar._drawRange(range);
            }
            else if (linking.source) {
                var src = linking.source;
                //linktools.clear();
                //linktools.drawLinkXy(src.coords, calendar.coords, src.type + "ToStart");
                linktools.drawShadow(src.coords, calendar.coords);
            }
            else if (calendar.crosshairType !== 'Disabled') {  // crosshair
                calendar._updateCrosshairPosition();
            }

            calendar._cellhover();

            var insideEvent = ev.insideEvent;
            if (window.event) {
                insideEvent = window.event.srcElement.insideEvent;
            }

            // cell bubble
            if (calendar.cellBubble && calendar.coords && calendar.rowlist && calendar.rowlist.length > 0 && !insideEvent) {
                var x = Math.floor(calendar.coords.x / calendar.cellWidth);
                var y = calendar._getRow(calendar.coords.y).i;
                if (0 <= y && y < calendar.rowlist.length && 0 <= x && x < calendar.itline.length) {
                    var cell = {};
                    cell.calendar = calendar;
                    cell.start = calendar.itline[x].start;
                    cell.end = calendar.itline[x].end;
                    cell.resource = calendar.rowlist[y].id;
                    cell.toJSON = function() {
                        var json = {};
                        json.start = this.start;
                        json.end = this.end;
                        json.resource = this.resource;
                        return json;
                    };

                    calendar.cellBubble.showCell(cell);
                }
            }

            if (DayPilotScheduler.drag) {
                calendar._crosshairHide();
                if (DayPilotScheduler.gShadow) {
                    document.body.removeChild(DayPilotScheduler.gShadow);
                }
                DayPilotScheduler.gShadow = null;

                if (!DayPilotScheduler.movingShadow && calendar.coords && calendar.rowlist.length > 0) {
                    //if (DayPilotScheduler.movingShadow) { // can be null if the location is forbidden (first two rows in IE)
                    if (!DayPilotScheduler.moving) { // can be null if the location is forbidden (first two rows in IE)
                        DayPilotScheduler.moving = {};

                        var event = DayPilotScheduler.drag.event;
                        if (!event) {
                            //var now = new DayPilot.Date().getDatePart();
                            var now = calendar.itline[0].start;
                            calendar.debug.message("external start:" + now);
                            var ev = { 'id': DayPilotScheduler.drag.id, 'start': now, 'end': now.addSeconds(DayPilotScheduler.drag.duration), 'text': DayPilotScheduler.drag.text };
                            event = new DayPilot.Event(ev);
                            event.calendar = calendar;
                        }
                        DayPilotScheduler.moving.event = event;
                    }
                    //DayPilotScheduler.movingShadow = calendar.createShadow(DayPilotScheduler.drag.duration, calendar.shadow, DayPilotScheduler.drag.shadowType);
                    //DayPilotScheduler.movingShadow = calendar.createShadow(calendar.shadow, DayPilotScheduler.drag.shadowType);
                    DayPilotScheduler.movingShadow = calendar._createShadow(DayPilotScheduler.moving, DayPilotScheduler.drag.shadowType);
                }

                ev.cancelBubble = true;
            }

            // autoscroll
            if (calendar.autoScroll === "Always" ||
                    (calendar.autoScroll === "Drag" && (DayPilotScheduler.moving || DayPilotScheduler.resizing || DayPilotScheduler.range))
                    ) {


                var scrollDiv = calendar.nav.scroll;
                var coords = { x: calendar.coords.x, y: calendar.coords.y };
                coords.x -= scrollDiv.scrollLeft;
                coords.y -= scrollDiv.scrollTop;

                var width = scrollDiv.clientWidth;
                var height = scrollDiv.clientHeight;

                var border = 20;

                var left = coords.x < border;
                var right = width - coords.x < border;

                var top = coords.y < border;
                var bottom = height - coords.y < border;

                var x = 0;
                var y = 0;

                if (left) {
                    x = -5;
                }
                if (right) {
                    x = 5;
                }
                if (top) {
                    y = -5;
                }
                if (bottom) {
                    y = 5;
                }


                if (x || y) {
                    calendar._startScroll(x, y);
                }
                else {
                    calendar._stopScroll();
                }
            }

            // don't cancel the event bubbling here, it will hurt position detection used in DayPilot ContextMenu and DayPilot Bubble
            //ev.cancelBubble = true;
        };


        this._getCurrentCell = function() {
            var x, y;
            if (calendar.coords && calendar.rowlist && calendar.rowlist.length > 0) {
                x = calendar._getItlineCellFromPixels(calendar.coords.x).x;
                y = calendar._getRow(calendar.coords.y).i;
                if (y >= calendar.rowlist.length) {
                    return;
                }
            }
            else {
                return;
            }

            var row = this._getRowByIndex(y);
            var itc = this.itline[x];

            var cell = {};
            cell.x = x;
            cell.y = y;
            cell.start = itc.start;
            cell.end = itc.end;
            cell.resource = row ? row.data.id : null;

        };

        this._cellhover = function() {

            var cell = this._getCurrentCell();

            if (this.hover.cell) {
                if (this.hover.cell.x === cell.x && this.hover.cell.y === cell.y) {
                    return;
                }
                this._cellhoverout();
            }

            this.hover.cell = cell;

            if (typeof this.onCellMouseOver === 'function') {
                var args = {};
                args.cell = cell;
                this.onCellMouseOver(args);
            }

        };

        this._cellhoverout = function() {
            if (typeof this.onCellMouseOut === 'function') {
                var args = {};
                args.cell = this.hover.cell;
                this.onCellMouseOut(args);
            }
            this.hover.cell = null;
        };

        this.hover = {};

        this._updateCrosshairPosition = function() {
            var cell = this._getCurrentCell();
            if (this.hover.cell) {
                if (this.hover.cell.x === cell.x && this.hover.cell.y === cell.y) {
                    return;
                }
            }
            this._crosshair();
        };

        this._crosshairHide = function() {
            this.divCrosshair.innerHTML = '';
            this.crosshairVertical = null;
            this.crosshairHorizontal = null;

            if (this.crosshairTop && this.crosshairTop.parentNode) {
                this.crosshairTop.parentNode.removeChild(this.crosshairTop);
                this.crosshairTop = null;
            }

            if (this.crosshairLeft) {
                for (var i = 0; i < this.crosshairLeft.length; i++) {
                    var ch = this.crosshairLeft[i];
                    if (ch.parentNode) {
                        ch.parentNode.removeChild(ch);
                    }
                }
                this.crosshairLeft = null;
            }

            this._crosshairLastX = -1;
            this._crosshairLastY = -1;
        };

        this._crosshair = function() {
            var x, y;
            if (calendar.coords && calendar.rowlist && calendar.rowlist.length > 0) {
                x = calendar._getItlineCellFromPixels(calendar.coords.x).x;
                y = calendar._getRow(calendar.coords.y).i;
                if (y >= calendar.rowlist.length) {
                    return;
                }
            }
            else {
                return;
            }

            var type = this.crosshairType;

            var row = calendar.rowlist[y];

            if (type === 'Full') {
                // vertical
                var itc = this.itline[x];

                var left = itc.left;

                var line = this.crosshairVertical;
                if (!line) {
                    var line = document.createElement("div");
                    line.style.height = calendar._innerHeightTree + 'px';
                    line.style.position = 'absolute';
                    line.style.top = '0px';
                    line.style.backgroundColor = this.crosshairColor;
                    line.style.opacity = this.crosshairOpacity / 100;
                    line.style.filter = "alpha(opacity=" + this.crosshairOpacity + ")";
                    this.crosshairVertical = line;
                    this.divCrosshair.appendChild(line);
                }

                line.style.left = left + 'px';
                line.style.width = itc.width + 'px';

                // horizontal
                var top = row.top;
                var height = row.height;
                //var width = this._cellCount() * this.cellWidth;
                var width = this._getGridWidth();

                var line = this.crosshairHorizontal;
                if (!line) {
                    var line = document.createElement("div");
                    line.style.width = width + 'px';
                    line.style.height = height + 'px';
                    line.style.position = 'absolute';
                    line.style.top = top + 'px';
                    line.style.left = '0px';
                    line.style.backgroundColor = this.crosshairColor;
                    line.style.opacity = this.crosshairOpacity / 100;
                    line.style.filter = "alpha(opacity=" + this.crosshairOpacity + ")";
                    this.crosshairHorizontal = line;
                    this.divCrosshair.appendChild(line);
                }

                line.style.top = top + 'px';
                line.style.height = height + 'px';

            }

            var thc = this._getTimeHeaderCell(this.coords.x);
            if (thc && this._crosshairLastX !== thc.x) {
                if (this.crosshairTop && this.crosshairTop.parentNode) {
                    this.crosshairTop.parentNode.removeChild(this.crosshairTop);
                    this.crosshairTop = null;
                }

                // top
                var line = document.createElement("div");
                line.style.width = thc.cell.width + "px";
                line.style.height = resolved.headerHeight() + "px";
                line.style.left = '0px';
                line.style.top = '0px';
                line.style.position = 'absolute';
                line.style.backgroundColor = this.crosshairColor;
                line.style.opacity = this.crosshairOpacity / 100;
                line.style.filter = "alpha(opacity=" + this.crosshairOpacity + ")";

                this.crosshairTop = line;
                var north = this.divNorth;
                var lastHeader = this.timeHeader ? this.timeHeader.length - 1 : 1;
                if (this.nav.timeHeader) {
                    this._cache.timeHeader[thc.x + "_" + lastHeader].appendChild(line);
                }
                else {
                    if (north.firstChild.rows[lastHeader].cells[x]) {
                        north.firstChild.rows[lastHeader].cells[x].firstChild.appendChild(line);
                    }
                }
            }

            if (this._crosshairLastY !== y) {

                if (this.crosshairLeft) {
                    for (var i = 0; i < this.crosshairLeft.length; i++) {
                        var ch = this.crosshairLeft[i];
                        if (ch.parentNode) {
                            ch.parentNode.removeChild(ch);
                        }
                    }
                    this.crosshairLeft = null;
                }

                // left
                var columns = this.rowHeaderCols ? this.rowHeaderCols.length : 1;

                this.crosshairLeft = [];
                if (this.divHeader.rows[y]) {
                    for (var i = 0; i < this.divHeader.rows[y].cells.length; i++) {
                        //var width = this.rowHeaderCols ? this.rowHeaderCols[i] : this.rowHeaderWidth;
                        var width = calendar._getOuterRowHeaderWidth();

                        // disabled because of Chrome performance problems (.clientWidth is expensive)
                        //var width = this.divHeader.rows[row.i].cells[i].clientWidth;

                        var line = document.createElement("div");
                        line.style.width = width + "px";
                        line.style.height = row.height + "px";
                        line.style.left = '0px';
                        line.style.top = '0px';
                        line.style.position = 'absolute';
                        line.style.backgroundColor = this.crosshairColor;
                        line.style.opacity = this.crosshairOpacity / 100;
                        line.style.filter = "alpha(opacity=" + this.crosshairOpacity + ")";

                        this.crosshairLeft.push(line);
                        this.divHeader.rows[y].cells[i].firstChild.appendChild(line);
                    }
                }
            }

            if (thc) {
                this._crosshairLastX = thc.x;
            }
            this._crosshairLastY = y;
        };

        this._getTimeHeaderCell = function(pixels) {
            var last = this.timeHeader[this.timeHeader.length - 1];
            for (var i = 0; i < last.length; i++) {
                var cell = last[i];
                if (pixels >= cell.left && pixels < cell.left + cell.width) {
                    var result = {};
                    result.cell = cell;
                    result.x = i;
                    return result;
                }
            }
            return null;
        };

        this._onMaindRightClick = function(ev) {

            ev = ev || window.event;

            if (calendar.timeRangeSelectedHandling === 'Disabled') {
                return false;
            }

            if (!calendar._isWithinRange(calendar.coords)) {
                calendar._onMaindClick(ev);
            }

            if (calendar.contextMenuSelection) {
                var selection = calendar._getSelection(DayPilotScheduler.rangeHold);
                calendar.contextMenuSelection.show(selection);
            }

            ev.cancelBubble = true;

            if (!calendar.allowDefaultContextMenu) {
                return false;
            }
        };

        this._isWithinRange = function(coords) {
            var range = DayPilotScheduler.rangeHold;

            if (!range || !range.start || !range.end) {
                return false;
            }

            var row = this._getRowByIndex(range.start.y);

            var leftToRight = range.start.x < range.end.x;

            var rangeLeft = (leftToRight ? range.start.x : range.end.x) * this.cellWidth;
            var rangeRight = (leftToRight ? range.end.x : range.start.x) * this.cellWidth + this.cellWidth;
            var rangeTop = row.top;
            var rangeBottom = row.bottom;

            if (coords.x >= rangeLeft && coords.x <= rangeRight && coords.y >= rangeTop && coords.y <= rangeBottom) {
                return true;
            }

            return false;
        };

        this._drawRange = function(range) {
            var range = range || DayPilotScheduler.range;

            var startX = range.end.x > range.start.x ? range.start.x : range.end.x;
            var endX = (range.end.x > range.start.x ? range.end.x : range.start.x);


            this._drawRange2(startX, endX, range.start.y);

            /*
            this._deleteRange();
            for (var x = startX; x < endX; x++) {
                this._drawRangeCell(x, range.start.y);
            }
            */

        };

        this._drawRange2 = function(startX, endX, y) {
            var start = this.itline[startX];
            var end = this.itline[endX];

            var left = start.left;
            var right = end.left + end.width;
            var width = right - left;

            var cell = this.elements.range2;

            if (!cell) {
                cell = document.createElement("div");
                cell.style.position = 'absolute';
                cell.setAttribute("unselectable", "on");

                if (this.cssOnly) {
                    cell.className = this._prefixCssClass("_shadow");
                    var inner = document.createElement("div");
                    inner.className = this._prefixCssClass("_shadow_inner");
                    cell.appendChild(inner);
                }
                else {
                    cell.style.backgroundColor = calendar.cellSelectColor;
                }

                this.divRange.appendChild(cell);
            }

            cell.style.left = (start.left) + "px";
            cell.style.top = this.rowlist[y].top + "px";
            cell.style.width = width + "px";
            cell.style.height = (this.rowlist[y].height - 1) + "px";

            this.elements.range2 = cell;

            (function checkOverlap() {
                var row = calendar.rowlist[y];
                var shadow = cell;
                var left = start.left;

                calendar._overlappingShadow(cell, row, left, width, null);

            })();

            (function() {

                var last = calendar._lastRange;

                // don't fire the event if there is no change
                if (last && last.start.getTime() === start.start.getTime() && last.end.getTime() === end.end.getTime() && last.resource === calendar.rowlist[y].id) {
                    return;
                }

                var args = {};
                args.start = start.start;
                args.end = end.end;
                args.resource = calendar.rowlist[y].id;
                args.left = {};
                args.left.html = args.start.toString(calendar.timeRangeSelectingStartEndFormat);
                args.left.enabled = calendar.timeRangeSelectingStartEndEnabled;
                args.right = {};
                args.right.html = args.end.toString(calendar.timeRangeSelectingStartEndFormat);
                args.right.enabled = calendar.timeRangeSelectingStartEndEnabled;

                calendar._lastRange = args;

                if (typeof calendar.onTimeRangeSelecting === 'function') {
                    calendar.onTimeRangeSelecting(args);
                }

                calendar._showShadowHover(cell, args);
            })();


        };

        this._onMaindClick = function(ev) {

            if (calendar.timeRangeSelectedHandling === 'Disabled') {
                return false;
            }

            ev = ev || window.event;
            var button = DayPilot.Util.mouseButton(ev);

            if (DayPilotScheduler.range) { // time range selecting already active
                return;
            }

            if (DayPilotScheduler.rangeHold && calendar._isWithinRange(calendar.coords) && (button.right || button.middle)) {
                return;
            }

            if (calendar._isRowDisabled(calendar._getRow(calendar.coords.y).i)) {
                return;
            }

            var range = {};

            var cx = calendar._getItlineCellFromPixels(calendar.coords.x).x;

            range.start = {
                y: calendar._getRow(calendar.coords.y).i,
                x: cx
            };

            range.end = {
                x: cx
            };

            calendar._drawRange(range);

            var sel = calendar._getSelection(range);
            calendar._timeRangeSelectedDispatch(sel.start, sel.end, sel.resource);

            // TEST the default behavior is now Hold
            DayPilotScheduler.rangeHold = range;

        };

        this.timeouts = {};
        this.timeouts.drawEvents = null;
        this.timeouts.drawCells = null;
        this.timeouts.click = null;

        this._onScroll = function(ev) {
            calendar._clearCachedValues();

            if (calendar.dynamicLoading) {
                calendar._onScrollDynamic();
                return;
            }
            var divScroll = calendar.nav.scroll;

            calendar._scrollPos = divScroll.scrollLeft;
            calendar._scrollTop = divScroll.scrollTop;
            calendar._scrollWidth = divScroll.clientWidth;

            calendar.divTimeScroll.scrollLeft = calendar._scrollPos;
            calendar.divResScroll.scrollTop = calendar._scrollTop;


            if (calendar.timeouts.drawEvents) {
                clearTimeout(calendar.timeouts.drawEvents);
                calendar.timeouts.drawEvents = null;
            }
            if (calendar.scrollDelayEvents > 0) {
                calendar.timeouts.drawEvents = setTimeout(calendar._delayedDrawEvents(), calendar.scrollDelayEvents);
            }
            else {
                var f = calendar._delayedDrawEvents();
                f();
            }


            if (calendar.timeouts.drawCells) {
                clearTimeout(calendar.timeouts.drawCells);
                calendar.timeouts.drawCells = null;
            }
            if (calendar.scrollDelayCells > 0) {
                calendar.timeouts.drawCells = setTimeout(calendar._delayedDrawCells(), calendar.scrollDelayCells);
            }
            else {
                var f = calendar._delayedDrawCells();
                f();
            }

            if (calendar.timeouts.updateFloats) {
                clearTimeout(calendar.timeouts.updateFloats);
                calendar.timeouts.updateFloats = null;
            }
            if (calendar.scrollDelayFloats > 0) {
                calendar.timeouts.updateFloats = setTimeout(function() { calendar._updateFloats(); }, calendar.scrollDelayFloats);
            }
            else {
                calendar._updateFloats();
            }

            calendar.onScrollCalled = true;
        };

        this._delayedRefresh = function() {
            return function() {
                calendar._saveState();
                calendar._drawCells();
                calendar.refreshTimeout = window.setTimeout(calendar._delayedDrawEvents(), 200); // chain update
            };
        };

        this._delayedDrawCells = function() {
            return function() {
                calendar._saveState();
                calendar._drawCells();
            };
        };


        this._delayedDrawEvents = function() {
            var batch = true; // turns on batch rendering
            var deleteOld = calendar.dynamicEventRenderingCacheSweeping;  // deletes old events (outside of the visible area)
            var keepOld = calendar.dynamicEventRenderingCacheSize;  // how many old events should be kept visible (cached)

            return function() {
                if (calendar._hiddenEvents()) {
                    calendar._loadingStart();

                    window.setTimeout(function() {
                        if (deleteOld) calendar._deleteOldEvents(keepOld);
                        window.setTimeout(function() { calendar._drawEvents(batch); }, 50);
                    }, 50);
                }
                else {
                    calendar._findEventsInViewPort();
                }
            };
        };

        this._clearCachedValues = function() {
            this._cache.eventHeight = null;
            this._cache.drawArea = null;
        };

        this.show = function() {
            calendar.nav.top.style.display = '';
            this._resize();
            calendar._onScroll();
        };

        this.hide = function() {
            calendar.nav.top.style.display = 'none';
        };

        this._onScrollDynamic = function() {
            var divScroll = calendar.nav.scroll;

            calendar._scrollPos = divScroll.scrollLeft;
            calendar._scrollTop = divScroll.scrollTop;
            calendar._scrollWidth = divScroll.clientWidth;

            calendar.divTimeScroll.scrollLeft = calendar._scrollPos;
            calendar.divResScroll.scrollTop = calendar._scrollTop;

            if (calendar.refreshTimeout) {
                window.clearTimeout(calendar.refreshTimeout);
            }

            var delay = calendar.scrollDelayDynamic;
            calendar.refreshTimeout = window.setTimeout(calendar._delayedRefreshDynamic(divScroll.scrollLeft, divScroll.scrollTop), delay);

            calendar._updateFloats();
        };
/*
        this._findEventInList = function(e) {
            var eid = e.id;
            var estart = e.start.toString();

            for (var j = 0; j < this.events.list.length; j++) {
                var ex = this.events.list[j];
                var exid = ex.id;
                var exstart = ex.start.toString();

                if (exid === eid && exstart === estart && ex.resource === e.resource) {
                    var result = {};
                    result.ex = ex;
                    result.index = j;
                    result.modified = !calendar._equalObjectFlat(e, ex);
                    return result;
                }
            }
            return null;
        };
*/
        this._findEventInList = function(e) {
            var eid = e.id;

            for (var j = 0; j < this.events.list.length; j++) {
                var ex = this.events.list[j];
                var exid = ex.id;

                if (exid === eid) {
                    var result = {};
                    result.ex = ex;
                    result.index = j;
                    result.modified = !calendar._equalObjectFlat(e, ex);
                    return result;
                }
            }
            return null;
        };

        this._equalObjectFlat = function(first, second) {
            for (var name in first) {
                if (typeof first[name] === 'object') {
                    continue;
                }
                if (first[name] !== second[name]) {
                    return false;
                }
            }

            for (var name in second) {
                if (typeof second[name] === 'object') {
                    continue;
                }
                if (first[name] !== second[name]) {
                    return false;
                }
            }

            return true;
        };

        this._loadEventsDynamic = function(supplied, finished) {
            var updatedRows = [];

            for (var i = 0; i < supplied.length; i++) {
                var e = supplied[i];

                var found = calendar._findEventInList(e);

                var update = found && found.modified;
                var add = !found;

                if (update) {
                    // update it directly in list
                    this.events.list[found.index] = e;

                    // remove it from rows
                    var rows = calendar.events._removeFromRows(found.ex);
                    updatedRows = updatedRows.concat(rows);
                }
                else if (add) {
                    this.events.list.push(e);
                }

                if (update || add) {
                    updatedRows = updatedRows.concat(calendar.events._addToRows(e));
                }
            }

            calendar._loadRows(updatedRows);
            calendar._updateRowHeights();
            calendar._prepareRowTops();
            calendar._updateHeight();

            var useRowBasedUpdate = false;
            if (useRowBasedUpdate) {   // doesn't draw events that were swept
                calendar._updateRowsNoLoad(updatedRows, false, finished);
            }
            else {
                calendar._deleteCells();
                calendar._updateRowHeaderHeights();
                calendar._drawCells();
                calendar._updateEventTops();
                var f = calendar._delayedDrawEvents();
                f();
            }
        };

        this._delayedRefreshDynamic = function(scrollX, scrollY) {
            if (!calendar._serverBased()) {
                return function() {
                    if (typeof calendar.onScroll === 'function') {
                        // make sure the background is rendered immediately
                        calendar._drawCells();

                        var update = function(events) {
                            //var updatedRows = calendar._loadEvents(events, true);
                            var finished = function() {
                                if (calendar._api2()) {
                                    if (typeof calendar.onAfterRender === 'function') {
                                        var args = {};
                                        args.isCallBack = false;
                                        args.isScroll = true;
                                        args.data = null;

                                        calendar.onAfterRender(args);
                                    }
                                }
                            };

                            calendar._loadEventsDynamic(events, finished);
                        };

                        var area = calendar._getArea(scrollX, scrollY);
                        var range = calendar._getAreaRange(area);
                        var res = calendar._getAreaResources(area);

                        var args = {};
                        args.viewport = {};
                        args.viewport.start = range.start;
                        args.viewport.end = range.end;
                        args.viewport.resources = res;
                        args.async = false;
                        args.events = [];
                        args.loaded = function() {
                            if (this.async) {
                                update(this.events);
                            }
                        };

                        calendar.onScroll(args);

                        if (!args.async) {
                            update(args.events);
                        }

                    }
                };
            }
            else {
                return function() {
                    calendar.scrollX = scrollX;
                    calendar.scrollY = scrollY;
                    calendar._callBack2('Scroll');
                };
            }
        };

/*
        this._delayedRefresh = function() {
            return function() {

                // moved to ._onScroll directly for faster feedback
                //calendar._drawCells();
                calendar.refreshTimeout = window.setTimeout(calendar._delayedDrawEvents(), 200); // chain update
            };
        };
*/

        this._drawCellsFull = function() {
            var area = this._getDrawArea();

            var cellLeft = area.xStart;
            var cellWidth = area.xEnd - area.xStart;
            var cellTop = area.yStart;
            var cellHeight = area.yEnd - area.yStart;

            // initialize for client-side processing
            //if (typeof this.onBeforeCellRender === 'function') {
                if (!this.cellProperties) {
                    this.cellProperties = {};
                }
            //}

            //this.elements.cells = [];
            //this.elements.linesVertical = [];
            for (var i = 0; i < cellWidth; i++) {
                var x = cellLeft + i;
                for (var j = 0; j < cellHeight; j++) {
                    var y = cellTop + j;
                    if (!this.rowlist[y].hidden) {
                        this._drawCell(x, y);
                    }
                }
                this._drawLineVertical(x);
            }

            // full height
            var rarea = this._getAreaRowsWithMargin();
            for (var y = rarea.start; y < rarea.end; y++) {
                if (!this.rowlist[y].hidden) {
                    this._drawLineHorizontal(y);
                }
            }

        };

        this._drawCells = function() {
            if (calendar.progressiveRowRendering) {
                this._drawResHeadersProgressive();
            }

            if (this.rowlist !== null && this.rowlist.length > 0) {
                var sweep = this.cellSweeping;
                if (sweep) {
                    var keepOld = this.cellSweepingCacheSize;
                    this._deleteOldCells(keepOld);
                }

                this._drawCellsFull();
                this._drawTimeBreaks();
                rowtools._updateHighlighting();
            }

            var width = this._getGridWidth();

            this._maind.style.height = this._innerHeightTree + "px";
            this._maind.style.width = width + "px";

            if (calendar.cellWidthSpec === "Auto") {
                calendar.nav.scroll.style.overflowX = "hidden";
                calendar.nav.scroll.scrollLeft  = 0;
            }
            else {
                calendar.nav.scroll.style.overflowX = "auto";
            }

            this._rowsDirty = false;

        };

        this._drawTimeBreaks = function() {

            //this.elements.cells = [];  // just to make sure
            var area = this._getDrawArea();

            for (var x = area.xStart; x < area.xEnd; x++) {
                var breaks = (x < this.itline.length - 1) ? this.itline[x + 1].breakBefore : false;
                if (breaks) {
                    this._drawTimeBreak(x);
                }
            }
        };

        this._drawTimeBreak = function(x) {
            var index = "x" + x;
            if (this._cache.breaks[index]) {
                return;
            }

            //var left = x * this.cellWidth + this.cellWidth - 1;
            var left = this.itline[x + 1].left - 1;
            var height = this._innerHeightTree;

            var line = document.createElement("div");
            line.style.left = left + "px";
            line.style.top = "0px";
            line.style.width = "1px";
            line.style.height = height + "px";
            line.style.fontSize = '1px';
            line.style.lineHeight = '1px';
            line.style.overflow = 'hidden';
            line.style.position = 'absolute';
            line.setAttribute("unselectable", "on");
            //cell.className = this._prefixCssClass('cellbackground');

            if (this.cssOnly) {
                line.className = this._prefixCssClass("_matrix_vertical_break");
                //line.className = this._prefixCssClass("_matrix
            }
            else {
                line.style.backgroundColor = this.timeBreakColor;
            }

            this.divBreaks.appendChild(line);
            this.elements.breaks.push(line);

            this._cache.breaks[index] = line;

        };

        this._getDrawArea = function() {

            if (calendar._cache.drawArea) {
                return calendar._cache.drawArea;
            }

            if (!this.nav.scroll) {
                return null;
            }

            // const
            var preCache = 30;
            var scrollTop = calendar._scrollTop;

            var area = {};

            //var divScroll = calendar.divScroll;
            var visibleLeft = Math.floor(calendar._scrollPos / this.cellWidth);  // first visible index column
            var visibleWidth = Math.ceil(calendar._scrollWidth / this.cellWidth) + 1; // number of columns visible
            var totalWidth = this._cellCount();
            var start = visibleLeft - preCache; // pre-caching one screen on each side
            var end = start + 2 * preCache + visibleWidth;
            end = Math.min(end, totalWidth); // make sure it's within the boundaries
            start = Math.max(start, 0); // check the left side

            var cellTop = this._getRow(scrollTop).i;
            var cellBottom = this._getRow(scrollTop + this.nav.scroll.offsetHeight).i;
            if (cellBottom < this.rowlist.length) {
                cellBottom++;
            }
            var cellHeight = cellBottom - cellTop; // unused

            area.xStart = start;
            area.xEnd = end;
            area.yStart = cellTop;
            area.yEnd = cellBottom;

            area.pixels = {};
            area.pixels.left = this.nav.scroll.scrollLeft;
            area.pixels.right = this.nav.scroll.scrollLeft + this.nav.scroll.clientWidth;
            area.pixels.top = this.nav.scroll.scrollTop;
            area.pixels.bottom = this.nav.scroll.scrollTop + this.nav.scroll.clientHeight;
            area.pixels.width = this.nav.scroll.scrollWidth;

            calendar._cache.drawArea = area;

            return area;
        };

        this._getGridWidth = function() {
            var result = 0;
            if (this.viewType === "Days") {
                result = 24*60/this.cellDuration*this.cellWidth;
            }
            else {
                var last = this.itline[this.itline.length - 1];
                if (!last) {
                    result = 0;
                }
                else {
                    result = last.left + last.width;
                }
            }
            if (result < 0 || isNaN(result)) {
                result = 0;
            }
            return result;
        };

        this._drawLineHorizontal = function(y) {
            var index = "y" + y;

            if (this._cache.linesHorizontal[y]) {
                calendar.debug.message("skiping horiz line: " + y);
                return;
            }

            var top = this.rowlist[y].top + this.rowlist[y].height - 1;
            //var width = this._cellCount() * this.cellWidth;
            var width = this._getGridWidth();

            var line = document.createElement("div");
            line.style.left = "0px";
            line.style.top = top + "px";
            line.style.width = width + "px";
            line.style.height = "1px";
            line.style.fontSize = '1px';
            line.style.lineHeight = '1px';
            line.style.overflow = 'hidden';
            line.style.position = 'absolute';
            if (!this.cssOnly) {
                line.style.backgroundColor = this.cellBorderColor;
            }
            line.setAttribute("unselectable", "on");
            if (this.cssOnly) {
                line.className = this._prefixCssClass("_matrix_horizontal_line");
            }

            this.divLines.appendChild(line);
            //this.elements.cells.push(line);

            this._cache.linesHorizontal[index] = line;

        };

        this._drawLineVertical = function(x) {

            var itc = this.itline[x];
            if (!itc) {
                return;
            }

            var index = "x" + x;
            if (this._cache.linesVertical[index]) {
                return;
            }

            //var left = (x + 1) * this.cellWidth - 1;
            var left = itc.left + itc.width - 1;

            var line = document.createElement("div");
            line.style.left = left + "px";
            line.style.top = "0px";
            line.style.width = "1px";
            line.style.height = calendar._innerHeightTree + "px";
            line.style.fontSize = '1px';
            line.style.lineHeight = '1px';
            line.style.overflow = 'hidden';
            line.style.position = 'absolute';
            if (!this.cssOnly) {
                line.style.backgroundColor = this.cellBorderColor;
            }
            line.setAttribute("unselectable", "on");
            if (this.cssOnly) {
                line.className = this._prefixCssClass("_matrix_vertical_line");
            }

            this.divLines.appendChild(line);
            this.elements.linesVertical.push(line);

            this._cache.linesVertical[index] = line;
        };

        this._drawCellColumn = function(x) {
            var index = "x" + x;
            var height = this._innerHeightTree;

            if (this._cache.cells[index]) {
                return;
            }

            // only if not dirty
            var color = this._getColor(x, 0);
            var breaks = (x < this.itline.length - 1) ? this.itline[x + 1].breakBefore : false;

            var cell = document.createElement("div");
            cell.style.left = (x * this.cellWidth) + "px";
            cell.style.top = "0px";
            cell.style.width = (this.cellWidth) + "px";
            cell.style.height = height + "px";
            cell.style.position = 'absolute';
            cell.style.backgroundColor = color;
            cell.setAttribute("unselectable", "on");
            cell.className = this.cssOnly ? this._prefixCssClass('_cellcolumn') : this._prefixCssClass('cellbackground');

            // TEST
            cell.onclick = this._onMaindClick;

            this.divCells.appendChild(cell);
            this.elements.cells.push(cell);

            this._cache.cells[index] = '1';

        };

        this._toggle = function(index) {

            var row = this.rowlist[index];
            var expanded = !row.expanded;

            this.rowlist[index].expanded = expanded;
            var rows = this._updateChildren(index, row.expanded);

            if (!expanded) {
                for (var i = 0; i < rows.length; i++) {
                    var ri = rows[i];
                    this._deleteEventsInRow(ri);
                }
            }

            this._prepareRowTops();

            // the height needs to be updated before drawing cells
            this._drawResHeader();
            //this._drawResHeadersProgressive(); // not sure, should be fired from drawCells
            this._updateHeight();
            this._clearCachedValues();


            if (expanded) {
                for (var i = 0; i < rows.length; i++) {
                    var ri = rows[i];
                    this._drawEventsInRow(ri);
                }
                this._findEventsInViewPort();
            }

            this._updateEventTops();

            linktools.load();

            this._deleteCells(); // don't confuse the cache
            this._drawCells();

            this._saveState();

            var r = this._createRowObject(row, index);
            if (expanded) {
                this._resourceExpandDispatch(r);
            }
            else {
                this._resourceCollapseDispatch(r);
            }

            this._clearCachedValues();
        };

        this._loadNode = function(i) {
            var params = {};
            params.index = i;

            if (typeof this.onLoadNode === 'function') {
                var args = {};
                var resource = this.rowlist[i].resource;
                args.resource = resource;
                args.async = false;
                args.loaded = function() {
                    if (this.async) {
                        resource.dynamicChildren = false;
                        resource.expanded = true;
                        calendar.update();
                    }
                };

                this.onLoadNode(args);

                if (!args.async) {
                    resource.dynamicChildren = false;
                    resource.expanded = true;
                    this.update();
                }
            }
            else {
                this._callBack2('LoadNode', params);
            }

        };

        this._updateChildren = function(i, topExpanded) {
            var row = this.rowlist[i];
            var changed = [];
            //var node = this.tree[i];

            if (row.children === null || row.children.length === 0) {
                return changed;
            }

            for (var k = 0; k < row.children.length; k++) {
                var index = row.children[k];
                this.rowlist[index].hidden = topExpanded ? !row.expanded : true; // show/hide but don't change Expanded state
                if (topExpanded === !this.rowlist[index].hidden) {
                    changed.push(index);
                }
                var uchildren = this._updateChildren(index, topExpanded);
                if (uchildren.length > 0) {
                    changed = changed.concat(uchildren);
                }
            }

            return changed;
        };

        this._startScroll = function(stepX, stepY) {
            //var step = 10;
            this._stopScroll();
            this._scrollabit(stepX, stepY);
        };

        this._scrollabitX = function(step) {
            if (!step) {
                return false;
            }
            var total = this.nav.scroll.scrollWidth;
            var start = this.nav.scroll.scrollLeft;
            var width = this.nav.scroll.clientWidth;
            var right = start + width;

            if (step < 0 && start <= 0) {
                return false;
            }

            if (step > 0 && right >= total) {
                return false;
            }

            this.nav.scroll.scrollLeft += step;
            // this is not necessary, it's linked using nav.scroll.onscroll
            //this.divTimeScroll.scrollLeft = this.nav.scroll.scrollLeft;

            return true;
        };

        this._scrollabitY = function(step) {
            if (!step) {
                return false;
            }
            var total = this.nav.scroll.scrollHeight;
            var start = this.nav.scroll.scrollTop;
            var height = this.nav.scroll.clientHeight;
            var bottom = start + height;

            if (step < 0 && start <= 0) {
                return false;
            }

            if (step > 0 && bottom >= total) {
                return false;
            }

            this.nav.scroll.scrollTop += step;
            // this is not necessary, it's linked using nav.scroll.onscroll
            //this.divTimeScroll.scrollTop = this.nav.scroll.scrollTop;

            return true;
        };

        this._scrollabit = function(stepX, stepY) {

            var moved = this._scrollabitX(stepX) || this._scrollabitY(stepY);
            if (!moved) {
                return;
            }

            var delayed = function(stepX, stepY) {
                return function() {
                    calendar._scrollabit(stepX, stepY);
                };
            };

            this.scrolling = window.setTimeout(delayed(stepX, stepY), 100);

        };

        this._stopScroll = function() {
            if (this.scrolling) {
                window.clearTimeout(this.scrolling);
                this.scrolling = null;
            }
        };

        this._prepareRowTops = function() {
            var top = 0;
            for (var i = 0; i < this.rowlist.length; i++) {
                var row = this.rowlist[i];
                if (!row.hidden) {
                    row.top = top;
                    top += row.height;
                }
            }
            this._innerHeightTree = top;
            //return top; // now it's the bottom of the last visible row
        };

        this._deleteCells = function() {
            this.elements.cells = [];
            this.elements.linesVertical = [];
            this.elements.breaks = [];
            this._cache.cells = [];
            this._cache.linesVertical = [];
            this._cache.linesHorizontal = [];
            this._cache.breaks = [];
            this.divCells.innerHTML = '';
            this.divLines.innerHTML = '';
            this.divBreaks.innerHTML = '';
        };

        this._cellsRendered = 0;

        this._drawCell = function(x, y) {

            if (!this._initialized) {
                return;
            }

            var itc = this.itline[x];
            if (!itc) {
                return;
            }

            var index = x + '_' + y;
            if (this._cache.cells[index]) {
                return;
            }


            var p = this._getCellProperties(x, y);

            if (typeof this.onBeforeCellRender === 'function') {
                var row = calendar.rowlist[y];
                var resource = row.id;
                var rowOffset = row.start.getTime() - this._visibleStart().getTime();
                var start = itc.start.addTime(rowOffset);
                var end = itc.end.addTime(rowOffset);

                var cell = p;

                cell.resource = resource;
                cell.start = start;
                cell.end = end;
                cell.utilization = function(name) {
                    if (!row.sections) {
                        row.calculateUtilization();
                    }
                    return row.sections.forRange(start, end).maxSum(name);
                };
                cell.events = function() {
                    return row.events.forRange(start, end);
                };

                var args = {};
                args.cell = cell;

                this.onBeforeCellRender(args);

            }

            // don't draw cells with no/default properties
            if (!this.drawBlankCells) {
                var isDefault = false;
                if (this.cssOnly && this._isRowParent(y)) {
                    isDefault = false;
                }
                else if (!this._hasProps(p, ['html', 'cssClass', 'backColor', 'backImage', 'backRepeat'])) {
                    isDefault = true;
                }
                if (isDefault) {
                    return;
                }
            }

            var cell = document.createElement("div");
            cell.style.left = (itc.left) + "px";
            cell.style.top = this.rowlist[y].top + "px";
            cell.style.width = (itc.width) + "px";
            cell.style.height = (this.rowlist[y].height) + "px";
            cell.style.position = 'absolute';
            if (p && p.backColor) {
                cell.style.backgroundColor = p.backColor;
            }
            cell.setAttribute("unselectable", "on");
            cell.className = this.cssOnly ? this._prefixCssClass('_cell') : this._prefixCssClass('cellbackground');

            cell.coords = {};
            cell.coords.x = x;
            cell.coords.y = y;

            if (this.cssOnly && this._isRowParent(y)) {
                DayPilot.Util.addClass(cell, this._prefixCssClass("_cellparent"));
            }

            if (p) {
                if (p.cssClass) {
                    if (this.cssOnly) {
                        DayPilot.Util.addClass(cell, p.cssClass);
                    }
                    else {
                        DayPilot.Util.addClass(cell, calendar._prefixCssClass(p.cssClass));
                    }
                }
                if (p.html) {
                    cell.innerHTML = p.html;
                }
                if (p.backImage) {
                    cell.style.backgroundImage = "url(\"" + p.backImage + "\")";
                }
                if (p.backRepeat) {
                    cell.style.backgroundRepeat = p.backRepeat;
                }
                if (p.business) {
                    DayPilot.Util.addClass(cell, calendar._prefixCssClass("_cell_business"));
                }
            }

            // TEST
            cell.onclick = this._onMaindClick;

            this.divCells.appendChild(cell);
            this.elements.cells.push(cell);

            this._cache.cells[index] = cell;

            this._cellsRendered += 1;

        };

        this._hasProps = function(object, props) {
            if (props) {
                for (var i = 0; i < props.length; i++) {
                    if (object[props[i]]) {
                        return true;
                    }
                }
            }
            else {
                for (var name in object) {
                    if (object[name]) {
                        return true;
                    }
                }
            }
            return false;
        };

        this._drawRangeCell = function(x, y) {

            var itc = this.itline[x];

            var cell = document.createElement("div");
            cell.style.left = (itc.left) + "px";
            cell.style.top = this.rowlist[y].top + "px";
            cell.style.width = (itc.width - 1) + "px";
            cell.style.height = (this.rowlist[y].height - 1) + "px";
            cell.style.position = 'absolute';
            cell.style.backgroundColor = calendar.cellSelectColor;
            cell.setAttribute("unselectable", "on");
            //cell.oncontextmenu = function () {return false;};

            this.divRange.appendChild(cell);
            this.elements.range.push(cell);

        };

        this.clearSelection = function() {
            this._deleteRange();
        };

        this.cleanSelection = this.clearSelection;

        this._deleteRange = function() {
            // IE doesn't like the div empty
            this.divRange.innerHTML = '<div style="position:absolute; left:0px; top:0px; width:0px; height:0px;"></div>';
            this.elements.range = [];
            this.elements.range2 = null;

            this._clearShadowHover();

            DayPilotScheduler.rangeHold = null;
        };

        this._resolved = {};
        var resolved = this._resolved;

        resolved.locale = function() {
            return DayPilot.Locale.find(calendar.locale);
        };

        resolved.timeFormat = function() {
            if (calendar.timeFormat !== 'Auto') {
                    return calendar.timeFormat;
            }
            return resolved.locale().timeFormat;
        };

        resolved.weekStarts = function() {
            if (calendar.weekStarts === 'Auto') {
                var locale = resolved.locale();
                if (locale) {
                    return locale.weekStarts;
                }
                else {
                    return 0; // Sunday
                }
            }
            else {
                return calendar.weekStarts;
            }
        };

        resolved.rounded = function() {
            return calendar.eventCorners === 'Rounded';
        };

        resolved.layout = function() {
            var isIE6 = /MSIE 6/i.test(navigator.userAgent);
            if (calendar.layout === 'Auto') {
                if (isIE6) {
                    return 'TableBased';
                }
                else {
                    return 'DivBased';
                }
            }
            return calendar.layout;
        };

        resolved.notifyType = function() {
            var type;
            if (calendar.notifyCommit === 'Immediate') {
                type = "Notify";
            }
            else if (calendar.notifyCommit === 'Queue') {
                type = "Queue";
            }
            else {
                throw "Invalid notifyCommit value: " + calendar.notifyCommit;
            }

            return type;
        };

        resolved.isResourcesView = function() {
            return calendar.viewType !== 'Days';
        };


        resolved.useBox = function(durationTicks) {
            if (calendar.useEventBoxes === 'Always') {
                return true;
            }
            if (calendar.useEventBoxes === 'Never') {
                return false;
            }
            return durationTicks < calendar.cellDuration * 60 * 1000;
        };

        resolved.eventHeight = function() {
            if (calendar._cache.eventHeight) {
                return calendar._cache.eventHeight;
            }
            var height = calendar._getDimensionsFromCss("_event_height").height;
            if (!height) {
                height = calendar.eventHeight;
            }
            calendar._cache.eventHeight = height;
            return height;
        };

        resolved.headerHeight = function() {
            if (calendar._cache.headerHeight) {
                return calendar._cache.headerHeight;
            }
            var height = calendar._getDimensionsFromCss("_header_height").height;
            if (!height) {
                height = calendar.headerHeight;
            }
            calendar._cache.headerHeight = height;
            return height;
        };

        resolved.splitterWidth = function() {
            if (calendar.rowHeaderScrolling) {
                return calendar.rowHeaderSplitterWidth;
            }
            return 1;
        };

        this._getColor = function(x, y) {
            var index = x + '_' + y;
            if (this.cellProperties && this.cellProperties[index]) {
                return this.cellProperties[index].backColor;
            }
            return null;
        };

        this._getCellProperties = function(x, y) {
            var index = x + '_' + y;

            if (!this.cellProperties) {
                this.cellProperties = {};
            }

            if (this.cellProperties[index]) {
                return this.cellProperties[index];
            }

            if (this._cellPropertiesLazyLoading) {
                this.cellProperties[index] = calendar._getExpandedCell(x, y);
            }

            if (!this.cellProperties[index]) {
                var row = calendar.rowlist[y];
                var resource = row.id;
                //var rowOffset = row.start.getTime() - this.startDate.getTime();
                var rowOffset = row.start.getTime() - calendar._visibleStart().getTime();
                var itc = calendar.itline[x];
                var start = itc.start.addTime(rowOffset);
                var end = itc.end.addTime(rowOffset);

                var ibj = {};
                ibj.start = start;
                ibj.end = end;
                ibj.resource = resource;

                var cell = {};
                cell.business = calendar.isBusiness(ibj);
                if (!this.cssOnly) {
                    cell.backColor = cell.business ? this.cellBackColor : this.cellBackColorNonBusiness;
                }
                this.cellProperties[index] = cell;
            }

            return this.cellProperties[index];
        };

        this._copyCellProperties = function(source, x, y) {
            var index = x + '_' + y;
            this.cellProperties[index] = {};
            DayPilot.Util.copyProps(source, this.cellProperties[index], ['html', 'cssClass', 'backColor', 'backImage', 'backRepeat', 'business']);
            //DayPilot.Util.copyProps(source, this.cellProperties[index]);
            return this.cellProperties[index];
        };

        this._getExpandedCell = function(x, y) {
            if (!this.cellConfig) {
                return null;
            }

            var config = this.cellConfig;

            var cell = this.cellProperties[x + "_" + y];

            if (!cell && config.vertical) {
                cell = this.cellProperties[x + "_0"];
            }

            if (!cell && config.horizontal) {
                cell = this.cellProperties["0_" + y];
            }

            if (!cell && config["default"]) {
                cell = config["default"];
            }

            var copy = {};
            return DayPilot.Util.copyProps(cell, copy, ['html', 'cssClass', 'backColor', 'backImage', 'backRepeat', 'business']);

            return copy;
        };

        this._expandCellProperties = function() {

            if (this._cellPropertiesLazyLoading) {
                return;
            }

            if (!this.cellConfig) {
                return;
            }

            var config = this.cellConfig;

            if (config.vertical) {
                for (var x = 0; x < config.x; x++) {
                    var def = this.cellProperties[x + "_0"];
                    if (!def) {
                        continue;
                    }
                    for (var y = 1; y < config.y; y++) {
                        this._copyCellProperties(def, x, y);
                    }
                }
            }

            if (config.horizontal) {
                for (var y = 0; y < config.y; y++) {
                    var def = this.cellProperties["0_" + y];
                    if (!def) {
                        continue;
                    }
                    for (var x = 1; x < config.x; x++) {
                        this._copyCellProperties(def, x, y);
                        //this.cellProperties[x + "_" + y] = def;
                    }
                }
            }

            if (config["default"]) {
                var def = config["default"];
                for (var y = 0; y < config.y; y++) {
                    for (var x = 0; x < config.x; x++) {
                        if (!this.cellProperties[x + "_" + y]) {
                            this._copyCellProperties(def, x, y);
    //                        this.cellProperties[x + "_" + y] = def;
                        }
                    }
                }
            }
        };

        this.isBusiness = function(cell) {

            var start = cell.start;
            var end = cell.end;

            var cellDuration = (end.getTime() - start.getTime()) / (1000 * 60);  // minutes

            if (cellDuration <= 1440) {  // for one day per cell and lower only
                if (cell.start.dayOfWeek() === 0 || cell.start.dayOfWeek() === 6) {
                    return false;
                }
            }
            if (cellDuration < 720) {
                if (cell.start.getHours() < this.businessBeginsHour || cell.start.getHours() >= this.businessEndsHour) {
                    return false;
                }
                else {
                    return true;
                }
            }

            return true;
        };

        this._show = function() {
            if (this.nav.top.style.visibility === 'hidden') {
                this.nav.top.style.visibility = 'visible';
            }
        };

        this._visible = function() {
            var el = calendar.nav.top;
            return el.offsetWidth > 0 && el.offsetHeight > 0;
        };

        this._waitForVisibility = function() {
            var visible = calendar._visible;

            if (!visible()) {
                calendar.debug.message("Not visible during init, starting visibilityInterval");
                calendar._visibilityInterval = setInterval(function() {
                    if (visible()) {
                        calendar.debug.message("Made visible, calling .show()");
                        calendar.show();
                        calendar._autoRowHeaderWidth();
                        clearInterval(calendar._visibilityInterval);
                    }
                }, 100);
            }
        };

        // sets the total height
        this._setHeight = function(pixels) {
            if (this.heightSpec !== "Parent100Pct") {
                this.heightSpec = "Fixed";
            }
            this.height = pixels - (this._getTotalHeaderHeight() + 2);
            this._updateHeight();
        };

        this.setHeight = this._setHeight;

        this._findRowByResourceId = function(id) {
            for (var i = 0; i < this.rowlist.length; i++) {
                if (this.rowlist[i].id === id) {
                    return this.rowlist[i];
                }
            }
            return null;
        };

        this._loadTop = function() {
            //this.nav.top = document.getElementById(id);
            if (this.id && this.id.tagName) {
                this.nav.top = this.id;
            }
            else if (typeof this.id === "string") {
                this.nav.top = document.getElementById(this.id);
                if (!this.nav.top) {
                    throw "DayPilot.Scheduler: The placeholder element not found: '" + id + "'.";
                }
            }
            else {
                throw "DayPilot.Scheduler() constructor requires the target element or its ID as a parameter";
            }

        };

        this._shortInit = function() {
            //this._loadTop();
            this._prepareVariables();
            this._loadResources();
            //this._initPrepareDiv();
            this._resize();
            this._registerGlobalHandlers();
            this._registerDispose();
            DayPilotScheduler.register(this);
            this._fireAfterRenderDetached(this.afterRenderData, false);
            this._registerOnScroll();
            this._waitForVisibility();
            this._startAutoRefresh();
            this._callBack2('Init');
        };

        this.init = function() {
            this._initUpdateBased();
            //this._initOriginal();
        };

        this._initUpdateBased = function() {
            this._loadTop();

            if (this.nav.top.dp) {
                return;
            }

            this._initPrepareDiv();

            var loadFromServer = this._isShortInit();

            if (loadFromServer) {
                this._shortInit();
                this._initialized = true;
                this._clearCachedValues();
                return;
            }

            this._registerGlobalHandlers();
            this._registerDispose();
            DayPilotScheduler.register(this);
            this._registerOnScroll();

            this._update();

            if (calendar.scrollToDate) {
                calendar.scrollTo(calendar.scrollToDate);
            }
            else {
                calendar.setScroll(calendar.scrollX, calendar.scrollY);
            }

            var setScrollY = function() {
                if (calendar.scrollY) {
                    calendar.setScroll(calendar.scrollX, calendar.scrollY);
                }
            };

            window.setTimeout(setScrollY, 200);

            if (this.messageHTML) {
                window.setTimeout(function() { calendar.message(calendar.messageHTML); }, 100);
            }

            this._waitForVisibility();

            this._startAutoRefresh();

            this._initialized = true;

            this._clearCachedValues();
            this._fireAfterRenderDetached(this.afterRenderData, false);
            this.debug.message("Init complete.");

            this._postInit();

        };

        this._initTemp = function() {
            this._loadTop();

            if (this.nav.top.dp) {
                return;
            }
            var loadFromServer = this._isShortInit();
            //var eventsAvailable = this.events.list !== null;

            if (loadFromServer) {
                this._shortInit();
                this._initialized = true;
                this._clearCachedValues();
                return;
            }

            // frame
            this._prepareVariables();
            this._initPrepareDiv();

            this._loadingStart();

            // draw res header, required for width
            this._loadResources();
            this._loadEvents();
            this._prepareRowTops();
            this._drawResHeader();

            // cells and time headers, may be dependent on scroll area width
            this._prepareItline();
            this._expandCellProperties();
            this._drawTimeHeader();

            this._updateHeight();
            this._drawSeparators();

            this._loadSelectedRows(this.selectedRows);

            //this._resize();
            //this._show();
            this._registerGlobalHandlers();
            this._registerDispose();
            DayPilotScheduler.register(this);

            //this.afterRender(null, false);

            this._registerOnScroll();

            this._loadingStop();

            if (calendar.scrollToDate) {
                calendar.scrollTo(calendar.scrollToDate);
            }
            else {
                calendar.setScroll(calendar.scrollX, calendar.scrollY);
            }
            if (!calendar.onScrollCalled) {
                calendar._onScroll();  // renders cells
            }

            var setScrollY = function() {
                if (calendar.scrollY) {
                    calendar.setScroll(calendar.scrollX, calendar.scrollY);
                }
            };

            window.setTimeout(setScrollY, 200);

            if (this.messageHTML) {
                var showMessage = function(msg) {
                    return function() {
                        calendar.message(msg);
                    };
                };
                window.setTimeout(showMessage(this.messageHTML), 100);
                //this.message(this.messageHTML, 5000);
            }

            this._waitForVisibility();

            this._startAutoRefresh();

            this._initialized = true;

            this._clearCachedValues();
            this._fireAfterRenderDetached(this.afterRenderData, false);
            this.debug.message("Init complete.");

            this._postInit();
        };

        this._initOriginal = function() {
            this._loadTop();

            if (this.nav.top.dp) {
                return;
            }
            var loadFromServer = this._isShortInit();
            //var eventsAvailable = this.events.list !== null;

            if (loadFromServer) {
                this._shortInit();
                this._initialized = true;
                this._clearCachedValues();
                return;
            }

            this._prepareVariables();
            this._prepareItline();

            this._loadResources();

            this._expandCellProperties();
            this._initPrepareDiv();

            // resize can't be here because of autocellwidth mode, not all variables are ready

            this._calculateCellWidth();
            this._drawTimeHeader();


            this._loadingStart();

            this._loadEvents();

            this._prepareRowTops();
            this._drawResHeader();
            this._updateHeight();
            this._drawSeparators();

            this._loadSelectedRows(this.selectedRows);

            this._resize();
            this._show();
            this._registerGlobalHandlers();
            this._registerDispose();
            DayPilotScheduler.register(this);

            //this.afterRender(null, false);

            this._registerOnScroll();

            this._loadingStop();

            if (calendar.scrollToDate) {
                calendar.scrollTo(calendar.scrollToDate);
            }
            else {
                calendar.setScroll(calendar.scrollX, calendar.scrollY);
            }
            if (!calendar.onScrollCalled) {
                calendar._onScroll();  // renders cells
            }

            var setScrollY = function() {
                if (calendar.scrollY) {
                    calendar.setScroll(calendar.scrollX, calendar.scrollY);
                }
            };

            window.setTimeout(setScrollY, 200);

            if (this.messageHTML) {
                var showMessage = function(msg) {
                    return function() {
                        calendar.message(msg);
                    };
                };
                window.setTimeout(showMessage(this.messageHTML), 100);
                //this.message(this.messageHTML, 5000);
            }

            this._waitForVisibility();

            this._startAutoRefresh();

            this._initialized = true;

            this._clearCachedValues();
            this._fireAfterRenderDetached(this.afterRenderData, false);
            this.debug.message("Init complete.");

            this._postInit();
        };

        this._specialHandling = null;
        this._loadOptions = function(options) {
            var specialHandling = {
                "events": {
                    "preInit": function() {
                        var events = this.data;
                        if (!events) {
                            return;
                        }
                        if (DayPilot.isArray(events.list)) {
                            calendar.events.list = events.list;
                        }
                        else {
                            calendar.events.list = events;
                        }
                    },
                    "postInit": function() {

                    }
                },
                "scrollTo": {
                    "preInit": function() {

                    },
                    "postInit": function() {
                        if (this.data) {
                            calendar.scrollTo(this.data);
                        }
                    }
                }
            };
            this._specialHandling = specialHandling;

            for (var name in options) {
                if (specialHandling[name]) {
                    var item = specialHandling[name];
                    item.data = options[name];
                    if (item.preInit) {
                        item.preInit();
                    }
                }
                else {
                    calendar[name] = options[name];
                }
            }

        };

        this._postInit = function() {
            var specialHandling = this._specialHandling;
            for (var name in specialHandling) {
                var item = specialHandling[name];
                if (item.postInit) {
                    item.postInit();
                }
            }
        };

        this.temp = {};

        this.temp.getPosition = function() {
            var x = Math.floor(calendar.coords.x / calendar.cellWidth);
            var y = calendar._getRow(calendar.coords.y).i;
            if (y < calendar.rowlist.length) {
                var cell = {};
                cell.start = calendar.itline[x].start;
                cell.end = calendar.itline[x].end;
                cell.resource = calendar.rowlist[y].id;
                return cell;
            }
            else {
                return null;
            }
        };

        // communication between components
        this.internal = {};
        // ASP.NET
        this.internal.initialized = function() {
            return calendar._initialized;
        };

        // DayPilot.Action
        this.internal.invokeEvent = this._invokeEvent;
        // DayPilot.Menu
        this.internal.eventMenuClick = this._eventMenuClick;
        this.internal.timeRangeMenuClick = this._timeRangeMenuClick;
        this.internal.resourceHeaderMenuClick = this._rowMenuClick;
        this.internal.linkMenuClick = this._linkMenuClick;
        // DayPilot.Bubble
        this.internal.bubbleCallBack = this._bubbleCallBack;
        this.internal.findEventDiv = this._findEventDiv;
        this.internal.rowtools = this._rowtools;
        // DayPilot.Gantt
        this.internal.getNodeChildren = this._getNodeChildren;
        this.internal.callback = function() {
            calendar._callBack2.apply(calendar, arguments);
        };
        this.internal.createRowObject = this._createRowObject;

        this.Init = this.init;

        this._loadOptions(options);

    };

    DayPilot.Row = function(row, calendar) {
        if (!row) {
            throw "Now row object supplied when creating DayPilot.Row";
        }
        if (!calendar) {
            throw "No parent control supplied when creating DayPilot.Row";
        }

        var index = DayPilot.indexOf(calendar.rowlist, row);

        var r = this;
        r.isRow = true;
        r.menuType = 'resource';
        r.start = row.start;
        r.name = row.name;
        r.value = row.id;
        r.id = row.id;
        r.index = index;
        r.calendar = calendar;
        r._row = row;
        r.$ = {};
        r.$.row = row;
        r.toJSON = function(key) {
            var json = {};
            json.start = this.start;
            json.name = this.name;
            json.value = this.value;
            json.id = this.id;
            json.index = this.index;

            return json;
        };

        r.events = {};
        r.events.all = function() {
            var list = [];
            for (var i = 0; i < r._row.events.length; i++) {
                list.push(r._row.events[i]);
            }
            return DayPilot.list(list);
        };

        r.events.totalDuration = function() {
            var ticks = 0;
            r.events.all().each(function(item) {
                ticks += item.part.end.getTime() - item.part.start.getTime();
            });
            return new DayPilot.TimeSpan(ticks);
        };

        r.groups = {};
        r.groups.collapseAll = function() {
            for (var i = 0; i < r._row.blocks.length; i++) {
                var block = r._row.blocks[i];
                var group = new EventGroup(block);
                group._collapseDontRedrawRow();
            }

            calendar._updateRowHeights();
            calendar._updateRowsNoLoad(r.index);
            calendar._updateHeight();

        };
        r.groups.expandAll = function() {
            for (var i = 0; i < r._row.blocks.length; i++) {
                var block = r._row.blocks[i];
                var group = new EventGroup(block);
                group._expandDontRedrawRow();
            }

            calendar._updateRowHeights();
            calendar._updateRowsNoLoad(r.index);
            calendar._updateHeight();

        };
        r.groups.expanded = function() {
            var list = [];
            for (var i = 0; i < r._row.blocks.length; i++) {
                var block = r._row.blocks[i];
                if (block.expanded && block.lines.length > calendar.groupConcurrentEventsLimit) {
                    list.push(new EventGroup(block));
                }
            }
            return DayPilot.list(list);
        };
        r.groups.collapsed = function() {
            var list = [];
            for (var i = 0; i < r._row.blocks.length; i++) {
                var block = r._row.blocks[i];
                if (!block.expanded) {
                    list.push(new EventGroup(block));
                }
            }
            return DayPilot.list(list);
        };
        r.groups.all = function() {
            var list = [];
            for (var i = 0; i < r._row.blocks.length; i++) {
                var block = r._row.blocks[i];
                list.push(new EventGroup(block));
            }
            return DayPilot.list(list);
        };

        r.events.collapseGroups = r.groups.collapseAll;
        r.events.expandGroups = r.groups.expandAll;

        r.column = function(i) {
            return new RowHeaderColumn(r, i);
        };

        r.toggle = function() {
            calendar._toggle(r.index);
        };

        var EventGroup = function(block) {

            var findDiv = function(block) {
                for (var i = 0; i < calendar.elements.events.length; i++) {
                    var div = calendar.elements.events[i];
                    if (div.event === block) {
                        return div;
                    }
                }
                return null;
            };

            this._expandDontRedrawRow = function() {
                block.expanded = true;

                var div = findDiv(block);
                if (div) {
                    calendar._deleteBlock(div);
                    var elindex = DayPilot.indexOf(calendar.elements.events, div);
                    if (elindex !== -1) {
                        calendar.elements.events.splice(elindex, 1);
                    }
                }
            };

            this.expand = function() {
                this._expandDontRedrawRow();

                calendar._updateRowHeights();
                calendar._updateRowsNoLoad(block.row.index);
                calendar._updateHeight();
            };

            this._collapseDontRedrawRow = function() {
                if (block.lines.length > calendar.groupConcurrentEventsLimit) {
                    block.expanded = false;
                }
            };

            this.collapse = function() {
                this._collapseDontRedrawRow();

                calendar._updateRowHeights();
                calendar._updateRowsNoLoad(block.row.index);
                calendar._updateHeight();
            };

        };

        var RowHeaderColumn = function(row, i) {
            this.html = function(html) {
                var table = row.calendar.divHeader;
                var cell = table.rows[row.index].cells[i];
                var text = cell.textDiv;
                text.innerHTML = html;
            };
        };

/*
        var eventArray = function(a) {
            var list = [];

            if (DayPilot.isArray(a)) {
                for (var i = 0; i < a.length; i++) {
                    list.push(a[i]);
                }
            }
            else if (typeof a === 'object') {
                list.push(a);
            }

            list.each = function(f) {
                if (!f) {
                    return;
                }
                for (var i = 0; i < list.length; i++) {
                    f(list[i]);
                }
            };

            return list;
        };
*/

    };

    // internal moving
    DayPilotScheduler.moving = null;

    // internal resizing
    DayPilotScheduler.originalMouse = null;
    DayPilotScheduler.resizing = null;

    DayPilotScheduler.globalHandlers = false;
    DayPilotScheduler.timeRangeTimeout = null;

    // selecting
    DayPilotScheduler.selectedCells = null;

    DayPilotScheduler.dragStart = function(element, duration, id, text, type) {
        DayPilot.us(element);

        var drag = DayPilotScheduler.drag = {};
        drag.element = element;
        drag.duration = duration;
        drag.text = text;
        drag.id = id;
        drag.shadowType = type ? type : 'Fill';  // default value

        return false;
    };

    /*
     * options: {
     *      element: dom element,
     *      duration: duration in minutes,
     *      text: event text,
     *      id: id
     * }
     */
    DayPilot.Scheduler.makeDraggable = function(options) {
        var element = options.element;
        var removeElement = options.keepElement ? null : element;

        DayPilot.us(element);  // make it unselectable
        DayPilot.re(element, "mousedown", function(ev) {
            DayPilotScheduler.dragStart(removeElement, options.duration, options.id, options.text);

            var element = (ev.target || ev.srcElement);
            if(element.tagName) {
                var tagname = element.tagName.toLowerCase();
                if(tagname === "textarea" || tagname === "select" || tagname === "input") {
                    return false;
                }
            }
            ev.preventDefault && ev.preventDefault();
            return false;
        });

        element.ontouchstart = function(ev) {

            var holdfor = 0;

            window.setTimeout(function() {
                var drag = DayPilotScheduler.drag = {};

                drag.element = removeElement;

                // TODO create drag.event = new DayPilot.Event() here
                drag.id = options.id;
                drag.text = options.text || "";
                drag.duration = options.duration || 60;
                drag.shadowType = "Fill";

                DayPilotScheduler.gTouchMove(ev);

                ev.preventDefault();
            }, holdfor);

            ev.preventDefault();
        };
    };

    DayPilotScheduler.dragStop = function() {
        if (DayPilotScheduler.gShadow) {
            document.body.removeChild(DayPilotScheduler.gShadow);
            DayPilotScheduler.gShadow = null;
        }
        DayPilotScheduler.drag = null;
    };

    DayPilotScheduler.register = function(calendar) {
        if (!DayPilotScheduler.registered) {
            DayPilotScheduler.registered = [];
        }
        for (var i = 0; i < DayPilotScheduler.registered.length; i++) {
            if (DayPilotScheduler.registered[i] === calendar) {
                return;
            }
        }
        DayPilotScheduler.registered.push(calendar);

    };

    DayPilotScheduler.unregister = function(calendar) {
        var a = DayPilotScheduler.registered;
        if (a) {
            var i = DayPilot.indexOf(a, calendar);
            if (i !== -1) {
                a.splice(i, 1);
            }
            if (a.length === 0) {
                a = null;
            }
        }

        if (!a) {
            DayPilot.ue(document, 'mousemove', DayPilotScheduler.gMouseMove);
            DayPilot.ue(document, 'mouseup', DayPilotScheduler.gMouseUp);
            DayPilot.ue(document, 'touchmove', DayPilotScheduler.gTouchMove);
            DayPilot.ue(document, 'touchend', DayPilotScheduler.gTouchEnd);
            //DayPilot.ue(window, 'unload', DayPilotScheduler.gUnload);
            DayPilotScheduler.globalHandlers = false;
        }
    };

    DayPilotScheduler.gTouchMove = function(ev) {
        if (DayPilotScheduler.drag) {
            ev.preventDefault();

            var x = ev.touches[0].pageX;
            var y = ev.touches[0].pageY;

            var mousePos = {};
            mousePos.x = x;
            mousePos.y = y;

            var calendar = (function() {
                var clientX = ev.touches[0].clientX;
                var clientY = ev.touches[0].clientY;
                var el = document.elementFromPoint(clientX, clientY);
                while (el && el.parentNode) {
                    el = el.parentNode;
                    if (el.daypilotMainD) {
                        return el.calendar;
                    }
                }
                return false;
            })();

            if (calendar) {

                // hide the global shadow
                if (DayPilotScheduler.gShadow) {
                    document.body.removeChild(DayPilotScheduler.gShadow);
                }
                DayPilotScheduler.gShadow = null;

                //var div = ??;
                //DayPilotScheduler.moving = div;
                //DayPilotScheduler.movingShadow = calendar._createShadow(div, calendar.shadow);
                calendar.coords = calendar._touch.relativeCoords(ev);

                if (!DayPilotScheduler.movingShadow && calendar.rowlist.length > 0) {
                    if (!DayPilotScheduler.moving) { // can be null if the location is forbidden (first two rows in IE)
                        DayPilotScheduler.moving = {};

                        var event = DayPilotScheduler.drag.event;
                        if (!event) {
                            //var now = new DayPilot.Date().getDatePart();
                            var now = calendar.itline[0].start;
                            calendar.debug.message("external start/touch:" + now);
                            var ev = { 'id': DayPilotScheduler.drag.id, 'start': now, 'end': now.addSeconds(DayPilotScheduler.drag.duration), 'text': DayPilotScheduler.drag.text };
                            event = new DayPilot.Event(ev);
                            event.calendar = calendar;
                        }
                        DayPilotScheduler.moving.event = event;
                    }
                    DayPilotScheduler.movingShadow = calendar._createShadow(DayPilotScheduler.moving, DayPilotScheduler.drag.shadowType);
                }

                if (DayPilotScheduler.moving) {
                    calendar._touch.updateMoving();
                }
            }
            else {
                // hide the local shadow
                DayPilot.de(DayPilotScheduler.movingShadow);
                DayPilotScheduler.moving = null;
                DayPilotScheduler.movingShadow = null;

                if (!DayPilotScheduler.gShadow) {
                    DayPilotScheduler.gShadow = DayPilotScheduler.createGShadow(DayPilotScheduler.drag.shadowType);
                }

                var shadow = DayPilotScheduler.gShadow;
                shadow.style.left = mousePos.x + 'px';
                shadow.style.top = mousePos.y + 'px';


                /*
                DayPilotScheduler.moving = null;
                // TODO delete moving shadow
                DayPilotScheduler.movingShadow = null;
                shadow.innerHTML = "x:" + x;
                */
            }

        }
    };

    DayPilotScheduler.gTouchEnd = function(ev) {
        DayPilotScheduler.gMouseUp(ev);
    };

    DayPilotScheduler.gMouseMove = function(ev) {
        if (typeof (DayPilotScheduler) === 'undefined') {
            return;
        }

        ev = ev || window.event;

        // quick and dirty inside detection
        // hack, but faster then recursing through the parents
        if (ev.insideMainD) {  // FF
            return;
        }
        else if (ev.srcElement) {  // IE
            if (ev.srcElement.inside) {
                return;
            }
        }

        /*
        if (typeof(DayPilotBubble) != 'undefined') {
        DayPilotBubble.cancelTimeout();
        if (DayPilotBubble.active) {
        DayPilotBubble.active.delayedHide();
        }
        }
        */
        var mousePos = DayPilot.mc(ev);

        if (DayPilotScheduler.drag) {
            document.body.style.cursor = 'move';
            if (!DayPilotScheduler.gShadow) {
                DayPilotScheduler.gShadow = DayPilotScheduler.createGShadow(DayPilotScheduler.drag.shadowType);
            }

            var shadow = DayPilotScheduler.gShadow;
            shadow.style.left = mousePos.x + 'px';
            shadow.style.top = mousePos.y + 'px';

            // it's being moved outside, delete the inside shadow
            DayPilotScheduler.moving = null;
            if (DayPilotScheduler.movingShadow) {
                DayPilotScheduler.movingShadow.calendar = null;
                DayPilot.de(DayPilotScheduler.movingShadow);
                DayPilotScheduler.movingShadow = null;
            }

        }
        else if (DayPilotScheduler.moving && DayPilotScheduler.moving.event.calendar.dragOutAllowed && !DayPilotScheduler.drag) {
            var cal = DayPilotScheduler.moving.event.calendar; // source
            var ev = DayPilotScheduler.moving.event;

            // clear target
            DayPilotScheduler.moving.target = null;

            document.body.style.cursor = 'move';
            if (!DayPilotScheduler.gShadow) {
                DayPilotScheduler.gShadow = DayPilotScheduler.createGShadow(cal.shadow);
            }

            var shadow = DayPilotScheduler.gShadow;
            shadow.style.left = mousePos.x + 'px';
            shadow.style.top = mousePos.y + 'px';

            // it's being moved outside, delete the inside shadow
            DayPilotScheduler.drag = {};
            var drag = DayPilotScheduler.drag;
            drag.element = null;
            drag.duration = (ev.end().getTime() - ev.start().getTime()) / 1000;
            drag.text = ev.text();
            drag.id = ev.value();
            drag.shadowType = cal.shadow;
            drag.event = ev;
            //DayPilotScheduler.moving = null;
            DayPilot.de(DayPilotScheduler.movingShadow);
            DayPilotScheduler.movingShadow.calendar = null;
            DayPilotScheduler.movingShadow = null;
        }

        for (var i = 0; i < DayPilotScheduler.registered.length; i++) {
            if (DayPilotScheduler.registered[i]._out) {
                DayPilotScheduler.registered[i]._out();
            }
        }
    };

    DayPilotScheduler.gUnload = function(ev) {

        if (!DayPilotScheduler.registered) {
            return;
        }
        for (var i = 0; i < DayPilotScheduler.registered.length; i++) {
            var c = DayPilotScheduler.registered[i];
            //c.dispose();

            DayPilotScheduler.unregister(c);
        }
    };

    DayPilotScheduler.gMouseUp = function(ev) {

        if (DayPilotScheduler.resizing) {

            if (!DayPilotScheduler.resizingShadow) {
                document.body.style.cursor = '';
                DayPilotScheduler.resizing = null;
                return;
            }

            var e = DayPilotScheduler.resizing.event;
            var calendar = e.calendar;

            calendar._updateResizingShadow();

            var newStart = DayPilotScheduler.resizingShadow.start;
            var newEnd = DayPilotScheduler.resizingShadow.end;

            var overlapping = DayPilotScheduler.resizingShadow.overlapping;

            // stop resizing on the client
            DayPilot.de(DayPilotScheduler.resizingShadow);
            DayPilotScheduler.resizing = null;
            DayPilotScheduler.resizingShadow = null;

            calendar._clearShadowHover();

            document.body.style.cursor = '';

            if (overlapping) {
                return;
            }

            // action here
            calendar._eventResizeDispatch(e, newStart, newEnd);
        }
        else if (DayPilotScheduler.moving) {
            if (!DayPilotScheduler.movingShadow) {
                document.body.style.cursor = '';
                DayPilotScheduler.moving = null;
                return;
            }

            var e = DayPilotScheduler.moving.event;
            //var calendar = e.calendar;  // doesn't work for drag&drop between two schedulers, this is the source
            var calendar = DayPilotScheduler.moving.target;


            if (!calendar) {
                DayPilot.de(DayPilotScheduler.movingShadow);
                DayPilotScheduler.movingShadow.calendar = null;
                document.body.style.cursor = '';
                DayPilotScheduler.moving = null;
                return;
            }

            var newStart = DayPilotScheduler.movingShadow.start;
            var newEnd = DayPilotScheduler.movingShadow.end;
            var newResource = (calendar.viewType !== 'Days') ? DayPilotScheduler.movingShadow.row.id : null;
            var external = DayPilotScheduler.drag ? true : false;
            var line = DayPilotScheduler.movingShadow.line;
            //var left = DayPilotScheduler.movingShadow.offsetLeft;

            var overlapping = DayPilotScheduler.movingShadow.overlapping;

            // clear the moving state
            DayPilot.de(DayPilotScheduler.movingShadow);
            calendar._clearShadowHover();

            if (DayPilotScheduler.drag) {
                if (!calendar.todo) {
                    calendar.todo = {};
                }
                calendar.todo.del = DayPilotScheduler.drag.element;
                DayPilotScheduler.drag = null;
            }

            DayPilotScheduler.movingShadow.calendar = null;
            document.body.style.cursor = '';
            DayPilotScheduler.moving = null;
            DayPilotScheduler.movingShadow = null;

            if (overlapping) {
                return;
            }

            var ev = ev || window.event;
            calendar._eventMoveDispatch(e, newStart, newEnd, newResource, external, ev, line);
        }
        else if (DayPilotScheduler.range) {

            ev = ev || window.event;
            var button = DayPilot.Util.mouseButton(ev);

            var range = DayPilotScheduler.range;
            var calendar = range.calendar;

            if (DayPilotScheduler.timeRangeTimeout) {
                clearTimeout(DayPilotScheduler.timeRangeTimeout);
                DayPilotScheduler.timeRangeTimeout = null;
                calendar._onMaindDblClick(ev);
                return;
            }

            DayPilotScheduler.rangeHold = range;

            // must be cleared before dispatching
            DayPilotScheduler.range = null;

            var delayed = function(sel) {
                return function() {
                    DayPilotScheduler.timeRangeTimeout = null;

                    var shadow = calendar.elements.range2;
                    if (shadow.overlapping) {
                        calendar.clearSelection();
                        return;
                    }

                    calendar._timeRangeSelectedDispatch(sel.start, sel.end, sel.resource);

                    if (calendar.timeRangeSelectedHandling !== "Hold" && calendar.timeRangeSelectedHandling !== "HoldForever") {
                        doNothing();
                        //calendar.deleteRange();
                    }
                    else {
                        DayPilotScheduler.rangeHold = range;
                    }
                };
            };

            var sel = calendar._getSelection(range);

            if (!button.left) { // only left-click
                DayPilotScheduler.timeRangeTimeout = null;
                return;
            }

            if (calendar.timeRangeDoubleClickHandling === 'Disabled') {
                delayed(sel)();

                var ev = ev || window.event;
                ev.cancelBubble = true;
                return false;  // trying to prevent onmaindclick
            }
            else {
                DayPilotScheduler.timeRangeTimeout = setTimeout(delayed(sel), calendar.doubleClickTimeout);  // 300 ms
            }

        }
        else if (rowmoving.row) {
            var calendar = rowmoving.calendar;
            if (calendar) {
                calendar._rowMoveDispatch();
            }/*
            else {
                calendar._rowtools.resetMoving();
            }*/
        }
        else if (linking.source) {
            var calendar = linking.calendar;
            calendar._linktools.clearShadow();
            calendar._linktools.hideLinkpoints();

            linking.source = null;
            linking.calendar = null;
        }
        else if (DayPilotScheduler.splitting) {
            var splitting = DayPilotScheduler.splitting;
            splitting.cleanup();
            DayPilotScheduler.splitting = null;
        }

        // clean up external drag helpers
        if (DayPilotScheduler.drag) {
            DayPilotScheduler.drag = null;

            document.body.style.cursor = '';
        }

        if (DayPilotScheduler.gShadow) {
            document.body.removeChild(DayPilotScheduler.gShadow);
            DayPilotScheduler.gShadow = null;
        }

        DayPilotScheduler.moveOffsetX = null; // clean for next external drag
        DayPilotScheduler.moveDragStart = null;
    };

    // global shadow, external drag&drop
    DayPilotScheduler.createGShadow = function(type) {

        var shadow = document.createElement('div');
        shadow.setAttribute('unselectable', 'on');
        shadow.style.position = 'absolute';
        shadow.style.width = '100px';
        shadow.style.height = '20px';
        shadow.style.border = '2px dotted #666666';
        shadow.style.zIndex = 101;
        shadow.style.pointerEvents = "none";

        if (type === 'Fill') {       // transparent shadow
            shadow.style.backgroundColor = "#aaaaaa";
            shadow.style.opacity = 0.5;
            shadow.style.filter = "alpha(opacity=50)";
            shadow.style.border = '2px solid #aaaaaa';
        }

        document.body.appendChild(shadow);

        return shadow;
    };

    var rowmoving = {};
    DayPilot.Global.rowmoving = rowmoving;

    var linking = {};
    DayPilot.Global.linking = linking;

    // publish the API

    // (backwards compatibility)
    DayPilot.SchedulerVisible.dragStart = DayPilotScheduler.dragStart;
    DayPilot.SchedulerVisible.dragStop = DayPilotScheduler.dragStop;
    DayPilot.SchedulerVisible.Scheduler = DayPilotScheduler.Scheduler;
    DayPilot.SchedulerVisible.globalHandlers = DayPilotScheduler.globalHandlers;

    // current
    //DayPilot.Scheduler = DayPilotScheduler.Scheduler;


    //  jQuery plugin
    if (typeof jQuery !== 'undefined') {
        (function($) {
            $.fn.daypilotScheduler = function(options) {
                var first = null;
                var j = this.each(function() {
                    if (this.daypilot) { // already initialized
                        return;
                    };

                    var daypilot = new DayPilot.Scheduler(this.id, options);
                    daypilot.init();

                    this.daypilot = daypilot;

                    if (!first) {
                        first = daypilot;
                    }
                });
                if (this.length === 1) {
                    return first;
                }
                else {
                    return j;
                }
            };
        })(jQuery);
    }

    (function registerAngularModule() {
        var app = DayPilot.am();

        if (!app) {
            return;
        }

        app.directive("daypilotScheduler", function() {
            return {
                "restrict": "E",
                "template": "<div id='{{id}}'></div>",
                "compile": function compile(element, attrs) {
                    element.replaceWith(this["template"].replace("{{id}}", attrs["id"]));

                    return function link(scope, element, attrs) {
                        var calendar = new DayPilot.Scheduler(element[0]);
                        calendar._angular.scope = scope;
                        calendar.init();

                        var oattr = attrs["id"];
                        if (oattr) {
                            scope[oattr] = calendar;
                        }

                        var watch = scope["$watch"];

                        watch.call(scope, attrs["daypilotConfig"], function (value) {
                            for (var name in value) {
                                calendar[name] = value[name];
                            }
                            calendar.update();
                        }, true);

                        watch.call(scope, attrs["daypilotEvents"], function(value) {
                            //var calendar = element.data("calendar");
                            calendar.events.list = value;
                            calendar._update({"eventsOnly": true});
                        }, true);

                    };
                }
            };
        });
    })();

    if (typeof Sys !== 'undefined' && Sys.Application && Sys.Application.notifyScriptLoaded) {
        Sys.Application.notifyScriptLoaded();
    }

})();