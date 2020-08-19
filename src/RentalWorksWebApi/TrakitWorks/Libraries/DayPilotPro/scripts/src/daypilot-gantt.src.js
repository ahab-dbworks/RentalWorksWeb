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

(function() {
    

    if (typeof DayPilot.Gantt !== 'undefined') {
        return;
    }

    var doNothing = function() { };

    DayPilot.Gantt = function(id, options) {
        this.v = '1338';

        var calendar = this;
        this.id = id; // referenced
        this.isGantt = true;
        
        var scheduler = new DayPilot.Scheduler(id);
        this.scheduler = scheduler;

        // scheduler setup
        scheduler.viewType = "Resources";
        //scheduler.taskMoveHandling = "Update";
        //scheduler.linksEnabled = true;

        scheduler.onCallBackHeader = function(args) {
            args.header.taskGroupMode = calendar.taskGroupMode;
            args.header.rowHeaderColumns = calendar.columns;
        };

        scheduler.onGetNodeState = function(args) {

            var getTags = function(task) {
                var result = {};
                if (task.tags) {
                    for (var name in task.tags) {
                        result[name] = "" + task.tags[name];
                    }
                }
                return result;
            };


            var getNode = function(row) {
                var task = row.task;

                var result = {};
                result.start = task.start;
                result.end = task.end;
                result.id = task.id;
                result.complete = task.complete;
                result.text = task.text;
                result.type = task.type;
                result.expanded = row.expanded;
                result.loaded = row.loaded;
                result.tags = getTags(task);

                result.children = scheduler.internal.getNodeChildren(row.children);

                return result;
            };

            args.result = getNode(args.row);
            args.preventDefault();
        };


        scheduler.onCallBackResult = function(args) {
            var result = args.result;
            args.preventDefault();

            if (result.updateType === "None") {
                return;
            }

            var update = function(list) {
                for (var i = 0; i < list.length; i++) {
                    var name = list[i];
                    if (typeof result[name] !== "undefined") {
                        calendar[name] = result[name];
                    }
                }
            };

            // data
            calendar.links.list = result.links;
            calendar.tasks.list = result.tasks;

            // callback-changeable properties
            calendar.startDate = new DayPilot.Date(result.startDate);
            update(['days', 'cellDuration', 'cellGroupBy', 'cellWidth', 'cellWidthSpec', 'cornerHtml', 'separators', 'rowMinHeight', 'rowMarginBottom', 'taskGroupMode']);

            // server-generated properties
            update(['cellProperties', 'cellConfig', 'timeHeader', 'timeHeaders', 'timeline']);
            /*
            calendar.cellProperties = result.cellProperties;
            calendar.cellConfig = result.cellConfig;
            calendar.timeHeader = result.timeHeader;
            calendar.timeHeaders = result.timeHeaders;
            calendar.timeline = result.timeline;
            */

            wrap.translate();
            wrap._loadTasks();
            wrap._loadLinks();
            scheduler.update();
        };

        // default properties
        this.taskGroupMode = "Auto"; // behavior of task groups/parent nodes

        // translated scheduler properties
        this.autoRefreshCommand = "refresh";
        this.autoRefreshEnabled = false;
        this.autoRefreshInterval = 60;
        this.autoRefreshMaxCount = 20;
        this.autoScroll = "Drag";
        this.bubbleTask = null;
        this.bubbleCell = null;
        this.bubbleRow = null;
        this.cellDuration = 1440;
        this.cellGroupBy = "Month";
        this.cellWidth = 40;
        this.cellWidthSpec = "Fixed";
        this.completeBarVisible = true;
        this.completeBarHeight = 3;
        this.contextMenuTask = null;
        this.contextMenuRow = null;
        this.contextMenuLink = null;
        this.cornerHtml = '';
        this.crosshairColor = 'Gray';
        this.crosshairOpacity = 20;
        this.crosshairType = 'Header';
        this.doubleClickTimeout = 300;
        this.progressiveTaskRendering = 'Progressive';
        this.progressiveTaskRenderingMargin = 500;
        this.progressiveTaskRenderingCacheSweeping = false;
        this.progressiveTaskRenderingCacheSize = 200;
        this.floatingTasks = true;
        this.floatingTimeHeaders = true;
        this.headerHeight = 20;
        this.height = 300;
        this.heightSpec = "Max";
        this.linkBottomMargin = 10;
        this.loadingLabelVisible = true;
        this.loadingLabelText = "Loading...";
        this.messageBarPosition = "Top";
        this.messageHideAfter = 5000;
        this.numberFormat = null;
        this.progressiveRowRendering = true;
        this.progressiveRowRenderingPreload = 25;
        this.rowHeaderScrolling = false;
        this.rowHeaderSplitterWidth = 3;
        this.rowHeaderHideIconEnabled = true;
        this.rowHeaderWidth = 80;
        this.rowHeaderWidthAutoFit = true;
        this.rowMarginBottom = 4;
        this.rowMinHeight = 0;
        this.scrollDelayTasks = 200;
        this.scrollDelayCells = 20;
        this.scrollDelayFloats = 0;
        this.scale = "Day";
        this.snapToGrid = true;
        this.syncTasks = true;
        this.syncLinks = true;
        this.taskHeight = 24;
        this.taskResizeMargin = 5;
        this.taskMovingStartEndEnabled = false;
        this.taskMovingStartEndFormat = "MMMM d, yyyy";
        this.taskResizingStartEndEnabled = false;
        this.taskResizingStartEndFormat = "MMMM d, yyyy";
        this.theme = "gantt_default";
        this.treeAutoExpand = true;
        this.treeIndent = 20;
        this.treeImageMarginLeft = 5;
        this.treeImageMarginTop = 5;
        //this.timeHeaders = [ { "groupBy": "Default" }, {"groupBy": "Cell" }];
        this.timeline = null;
        this.timeHeaders = [ { "groupBy": "Month", "format": "MMMM yyyy"}, { "groupBy": "Day", "format": "d"}];
        this.useEventBoxes = "Never";

        // event handling
        this.taskMoveHandling = "Update";
        this.taskClickHandling = "Enabled";
        this.taskResizeHandling = "Update";
        this.linkCreateHandling = "Update";
        this.taskRightClickHandling = "ContextMenu";
        this.taskDoubleClickHandling = "Disabled";
        //this.taskDeleteHandling = "Update";
        this.rowCreateHandling = "Disabled";
        this.rowMoveHandling = "Update";
        this.rowClickHandling = "Disabled";
        this.rowDoubleClickHandling = "Disabled";
        this.rowEditHandling = "Update";
        this.rowSelectHandling = "Update";

        this.separators = [];

        this.members = {};
        this.members.obsolete = [];
        this.members.ignore = [
            "members",
            "scheduler",
            "internal"
        ];
        this.members.noCssOnly = [];

        this.links = {};
        this.links.list = [];
        this.links.add = function(link) {
            if (!link) {
                return;
            }
            var data = link.isLink ? link.data : link;
            calendar.links.list.push(data);

            if (calendar._initialized) {
                wrap._loadLinks();
                scheduler.update();
            }

            calendar._angular.notify();

        };

        this.links.remove = function(link) {
            if (!link || !link.isLink) {
                return;
            }

            var index = DayPilot.indexOf(calendar.links.list, link.data);
            calendar.links.list.splice(index, 1);
            if (calendar._initialized) {
                wrap._loadLinks();
                scheduler.update();
            }

            calendar._angular.notify();
        };

        this.links.find = function(id) {
            if (!DayPilot.isArray(calendar.links.list)) {
                return null;
            }
            for (var i = 0; i < calendar.links.list.length; i++) {
                var link = calendar.links.list[i];
                if (link.id === id) {
                    return new DayPilot.Link(link, calendar);
                }
            }
            return null;
        };

        this.tasks = {};
        this.tasks.list = [];
        this.tasks.add = function(task) {
            if (!task) {
                return;
            }
            if (task instanceof DayPilot.Event) {
                throw "DayPilot.Task object required. You have supplied DayPilot.Event.";
            }
            var data = task.isTask ? task.data : task;
            calendar.tasks.list.push(data);
            if (calendar._initialized) {
                wrap._loadTasks();
                scheduler.update();
            }

            calendar._angular.notify();
        };

        this.tasks.find = function(id) {
            var data = tasktools.findInCache(id);
            if (!data) {
                return null;
            }
            return new DayPilot.Task(data, calendar);
        };

        this.tasks.update = function(task) {
            // commit
            if (!task) {
                return;
            }
            if (!task.isTask) {
                throw "DayPilot.Task object expected";
            }

            task.commit();

            if (calendar._initialized) {
                wrap._loadTasks();
                scheduler.update();
            }

            calendar._angular.notify();

        };

        /**
         * Removes a task, including children.
         * @param task
         */
        this.tasks.remove = function(task) {
            if (!task) {
                return;
            }
            if (!task.isTask) {
                throw "DayPilot.Task object expected";
            }
            var parentArray = tasktools.findParentArray(task.data);
            if (!parentArray) {
                return;
            }

            var sourceIndex = DayPilot.indexOf(parentArray, task.data);
            parentArray.splice(sourceIndex, 1);

            if (calendar._initialized) {
                wrap._loadTasks();
                scheduler.update();
            }

            calendar._angular.notify();
        };

        // events
        this.onAfterRender = null;
        this.onBeforeRowHeaderRender = null;
        this.onBeforeTaskRender = null;
        this.onBeforeTimeHeaderRender = null;
        this.onBeforeCellRender = null;
        this.onTaskClick = null;
        this.onTaskClicked = null;
        this.onTaskDoubleClick = null;
        this.onTaskDoubleClicked = null;
        this.onTaskRightClick = null;
        this.onTaskRightClicked = null;
        this.onRowCreate = null;
        this.onRowCreated = null;
        this.onRowMove = null;
        this.onRowMoved = null;
        this.onRowMoving = null;
        this.onRowClick = null;
        this.onRowClicked = null;
        this.onRowDoubleClick = null;
        this.onRowDoubleClicked = null;
        this.onRowEdit = null;
        this.onRowEdited = null;
        this.onRowSelect = null;
        this.onRowSelected = null;
        this.onTaskMove = null;
        this.onTaskMoved = null;
        this.onTaskMoving = null;
        this.onTaskResize = null;
        this.onTaskResized = null;
        this.onTaskResizing = null;
        this.onLinkCreate = null;
        this.onLinkCreated = null;

        this._serverBased = function() {
            if (this.backendUrl) {  // ASP.NET MVC, Java
                return true;
            }
            if (typeof WebForm_DoCallback === 'function' && this.uniqueID) {  // ASP.NET WebForms
                return true;
            }
            return false;
        };
        
        this._setStartDate = function() {
            if (calendar.startDate && calendar.days) {
                return;
            }

            var startTicks = null;
            var endTicks = null;
            for (var i = 0; i < calendar.tasks.list.length; i++) {
                var e = calendar.tasks.list[i];
                var start = new DayPilot.Date(e.start);
                var end = new DayPilot.Date(e.end);
                if (startTicks === null || start.getTime() < startTicks) {
                    startTicks = start.getTime();
                }
                if (endTicks === null || end.getTime() > endTicks) {
                    endTicks = end.getTime();
                }
            }

            if (startTicks && endTicks) {
                var start = new DayPilot.Date(startTicks).getDatePart();
                var end =  new DayPilot.Date(endTicks).getDatePart().addDays(1);

                scheduler.startDate = calendar.startDate || start;
                scheduler.days = calendar.days || DayPilot.Date.daysDiff(start, end);
            }
            else {
                scheduler.startDate = calendar.startDate;
                scheduler.days = calendar.days;
            }
        };

        this.commandCallBack = function(command, data) {
            scheduler.commandCallBack(command, data);
        };

        this.message = function(html, delay, foreColor, backColor) {
            scheduler.message(html, delay, foreColor, backColor);
        };

        this.setHeight = function(pixels) {
            scheduler.setHeight(pixels);
        };

        this._isShortInit = function() {
            // make sure it has a place to ask
            if (this.backendUrl) {
                return !DayPilot.isArray(calendar.tasks.list) || calendar.tasks.list.length == 0;
            }
            else {
                return false;
            }
        };


        this.init = function() {
            wrap.translate();
            wrap._loadTasks();
            wrap._loadLinks();
            this._setStartDate();

            scheduler.init();
            this._initialized = true;

            this._postInit();

            if (this._isShortInit()) {
                scheduler.internal.callback('Init');
            }
        };

        this.update = function() {
            wrap.translate();
            wrap._loadTasks();
            wrap._loadLinks();
            calendar._setStartDate();
            scheduler.update();
        };

        this.scrollTo = function(target) {
            scheduler.scrollTo(target);
        }

        this._tasktools = {};
        var tasktools = this._tasktools;

        tasktools.cache = {};

        tasktools.clearCache = function() {
            tasktools.cache = {};
        };

        tasktools.addToCache = function(data, parent) {
            // cache
            var key = data.id;

            if (!key) {
                return;
            }

            var wrapper = {};
            wrapper.isTaskWrapper = true;
            wrapper.data = data;
            wrapper.parent = parent;

            if (tasktools.cache[key]) {
                throw "Duplicate task id detected";
            }

            tasktools.cache[key] = wrapper;

        };

        tasktools.findInCache = function(id) {
            if (!id) {
                return null;
            }
            return tasktools.cache[id.toString()];
        };

        tasktools.getProperty = function(task, name) {
            if (task.tags && task.tags[name]) {
                return task.tags[name];
            }
            return task[name];
        };

        tasktools.findParentArray = function(res) {
            return tasktools.findInArray(calendar.tasks.list, res);
        };

        tasktools.findInArray = function(array, res) {
            if (DayPilot.indexOf(array, res) !== -1) {
                return array;
            }
            for(var i = 0; i < array.length; i++) {
                var r = array[i];
                if (r.children && r.children.length > 0) {
                    var parent = tasktools.findInArray(r.children, res);
                    if (parent) {
                        return parent;
                    }
                }
            }
            return null;
        };

        this._wrap = {};
        var wrap = this._wrap;

        var coalesce = function(p, val) {
            if (typeof p !== "undefined") {
                return p;
            }
            return val;
        };

        wrap.translate = function() {
            // settings
            // fixed
            scheduler.durationBarMode = "PercentComplete";
            scheduler.timeRangeSelectedHandling = "Disabled";
            scheduler.treeEnabled = true;

            // variable
            scheduler.autoRefreshCommand = calendar.autoRefreshCommand;
            scheduler.autoRefreshEnabled = calendar.autoRefreshEnabled;
            scheduler.autoRefreshInterval = calendar.autoRefreshInterval;
            scheduler.autoRefreshMaxCount = calendar.autoRefreshMaxCount;
            scheduler.autoScroll = calendar.autoScroll;
            scheduler.backendUrl = calendar.backendUrl;
            scheduler.crosshairColor = calendar.crosshairColor;
            scheduler.crosshairOpacity = calendar.crosshairOpacity;
            scheduler.crosshairType = calendar.crosshairType;
            scheduler.doubleClickTimeout = calendar.doubleClickTimeout;
            scheduler.durationBarVisible = calendar.completeBarVisible;
            scheduler.durationBarHeight = calendar.completeBarHeight;
            scheduler.dynamicEventRendering = calendar.progressiveTaskRendering ? "Progressive" : "Disabled";
            scheduler.dynamicEventRenderingMargin = calendar.progressiveTaskRenderingMargin;
            scheduler.dynamicEventRenderingCacheSweeping = calendar.progressiveTaskRenderingCacheSweeping;
            scheduler.dynamicEventRenderingCacheSize = calendar.progressiveTaskRenderingCacheSize;
            scheduler.startDate = new DayPilot.Date(calendar.startDate);
            scheduler.days = calendar.days;
            scheduler.cellDuration = calendar.cellDuration;
            scheduler.cellGroupBy = calendar.cellGroupBy;
            scheduler.cellWidth = calendar.cellWidth;
            scheduler.cellWidthSpec = calendar.cellWidthSpec;
            scheduler.cornerHtml = calendar.cornerHtml;
            scheduler.eventHeight = calendar.taskHeight;
            scheduler.eventResizeMargin = calendar.taskResizeMargin;
            scheduler.floatingEvents = calendar.floatingTasks;
            scheduler.floatingTimeHeaders = calendar.floatingTimeHeaders;
            scheduler.headerHeight = calendar.headerHeight;
            scheduler.heightSpec = calendar.heightSpec;
            scheduler.height = calendar.height;
            scheduler.linkBottomMargin = calendar.linkBottomMargin;
            scheduler.loadingLabelVisible = calendar.loadingLabelVisible;
            scheduler.loadingLabelText = calendar.loadingLabelText;
            scheduler.messageBarPosition = calendar.messageBarPosition;
            scheduler.messageHideAfter = calendar.messageHideAfter;
            //scheduler.milestoneWidth = calendar.milestoneWidth;
            scheduler.rowCreateHandling = calendar.rowCreateHandling;
            scheduler.numberFormat = calendar.numberFormat;
            scheduler.progressiveRowRendering = calendar.progressiveRowRendering;
            scheduler.progressiveRowRenderingPreload = calendar.progressiveRowRenderingPreload;
            scheduler.scale = calendar.scale;
            scheduler.scrollDelayEvents = calendar.scrollDelayTasks;
            scheduler.scrollDelayCells = calendar.scrollDelayCells;
            scheduler.scrollDelayFloats = calendar.scrollDelayFloats;
            scheduler.separators = calendar.separators;
            scheduler.eventMovingStartEndEnabled = calendar.taskMovingStartEndEnabled;
            scheduler.eventMovingStartEndFormat = calendar.taskMovingStartEndFormat;
            scheduler.eventResizingStartEndEnabled = calendar.taskResizingStartEndEnabled;
            scheduler.eventResizingStartEndFormat = calendar.taskResizingStartEndFormat;
            scheduler.treeIndent = calendar.treeIndent;
            scheduler.treeAutoExpand = calendar.treeAutoExpand;
            scheduler.treeImageMarginLeft = calendar.treeImageMarginLeft;
            scheduler.treeImageMarginTop = calendar.treeImageMarginTop;
            scheduler.timeHeaders = calendar.timeHeaders;
            scheduler.rowHeaderHideIconEnabled = calendar.rowHeaderHideIconEnabled;
            scheduler.rowHeaderScrolling = calendar.rowHeaderScrolling;
            scheduler.rowHeaderSplitterWidth = calendar.rowHeaderSplitterWidth;
            scheduler.rowHeaderWidth = calendar.rowHeaderWidth;
            scheduler.rowHeaderWidthAutoFit = calendar.rowHeaderWidthAutoFit;
            scheduler.rowMarginBottom = calendar.rowMarginBottom;
            scheduler.rowMinHeight = calendar.rowMinHeight;
            scheduler.theme = calendar.theme;
            scheduler.useEventBoxes = calendar.useEventBoxes;
            scheduler.snapToGrid = calendar.snapToGrid;
            scheduler.uniqueID = calendar.uniqueID;
            scheduler.bubble = calendar.bubbleTask;
            scheduler.cellBubble = calendar.bubbleCell;
            scheduler.resourceBubble = calendar.bubbleRow;
            scheduler.contextMenu = calendar.contextMenuTask;
            scheduler.contextMenuResource = calendar.contextMenuRow;
            scheduler.contextMenuLink = calendar.contextMenuLink;
            scheduler.syncResourceTree = calendar.syncTasks;
            scheduler.syncLinks = calendar.syncLinks;

            scheduler.eventMoveHandling = calendar.taskMoveHandling;
            scheduler.eventClickHandling = calendar.taskClickHandling;
            scheduler.eventResizeHandling = calendar.taskResizeHandling;
            scheduler.linkCreateHandling = calendar.linkCreateHandling;
            scheduler.eventRightClickHandling = calendar.taskRightClickHandling;
            scheduler.eventDoubleClickHandling = calendar.taskDoubleClickHandling;
            scheduler.rowMoveHandling = calendar.rowMoveHandling;
            scheduler.rowClickHandling = calendar.rowClickHandling;
            scheduler.rowDoubleClickHandling = calendar.rowDoubleClickHandling;
            scheduler.rowEditHandling = calendar.rowEditHandling;
            scheduler.rowSelectHandling = calendar.rowSelectHandling;

            // temporarily disabled - will require a different mapping
            // scheduler.eventDeleteHandling = calendar.taskDeleteHandling;

            if (DayPilot.isArray(calendar.columns)) {
                scheduler.rowHeaderColumns = [];
                for (var i = 0; i < calendar.columns.length; i++) {
                    var c = calendar.columns[i];
                    var rhc = {};
                    DayPilot.Util.copyProps(c, rhc, ['title', 'width']);
                    //rhc.title = c.title;
                    //rhc.width = c.width;
                    scheduler.rowHeaderColumns.push(rhc);
                }
            }

            // server-based data
            if (calendar._serverBased()) {
                scheduler.timeHeader = calendar.timeHeader;
                scheduler.timeline = calendar.timeline;
                scheduler.cellProperties = calendar.cellProperties;
                scheduler.cellConfig = calendar.cellConfig;
            }

            // event mapping

            scheduler.onRowCreate = function(args) {
                if (typeof calendar.onRowCreate === "function") {
                    calendar.onRowCreate(args);
                }
            };

            scheduler.onRowCreated = function(args) {
                if (typeof calendar.onRowCreated === "function") {
                    calendar.onRowCreated(args);
                }
            };

            scheduler.onAfterRender = function(args) {
                if (typeof calendar.onAfterRender === "function") {
                    calendar.onAfterRender(args);
                }
            };

            scheduler.onRowHeaderResized = function(args) {
                calendar.rowHeaderWidth = scheduler.rowHeaderWidth;
            };

            scheduler.onAjaxError = function(args) {
                if (typeof calendar.onAjaxError === "function") {
                    calendar.onAjaxError(args);
                }
            };

            scheduler.onRowHeaderColumnResized = function(args) {
                var column = args.column;
                var index = DayPilot.indexOf(scheduler.rowHeaderColumns, column);

                for (var i = 0; i < calendar.columns.length; i++) {
                    var rhc = scheduler.rowHeaderColumns[i];
                    var c = calendar.columns[i];
                    c.width = rhc.width;
                }

                if (typeof calendar.onColumnResized === "function") {
                    var cargs = {};
                    cargs.column = calendar.columns[index];
                    calendar.onColumnResized(cargs);
                }

            };

            scheduler.onBeforeRowHeaderRender = function(args) {
                args.task = new DayPilot.Task(args.row.$.row.task, calendar);

                var columns = [];
                if (DayPilot.isArray(calendar.columns)) {
                    for (var i = 0; i < calendar.columns.length; i++) {
                        var column = calendar.columns[i];
                        var property = column.property;

                        var colargs = {};
                        colargs.value = tasktools.getProperty(args.task.data, property);

                        if (args.task.data.row && args.task.data.row.columns && args.task.data.row.columns[i]) {
                            colargs.html = args.task.data.row.columns[i].html;
                        }
                        else {
                            colargs.html = colargs.value;
                        }
                        columns.push(colargs);
                    }
                }

                args.row.columns = columns;

                if (typeof calendar.onBeforeRowHeaderRender === "function") {
                    calendar.onBeforeRowHeaderRender(args);
                }

                if (DayPilot.isArray(calendar.columns)) {
                    for (var i = 0; i < calendar.columns.length; i++) {
                        var html = columns[i].html;
                        if (i === 0) {
                            args.row.html = html;
                        }
                        else {
                            args.row.columns[i - 1].html = html;
                        }
                    }
                }

            };

            /*
            scheduler.onBeforeResHeaderRender = function(args) {
                var columns = [];
                if (DayPilot.isArray(calendar.columns)) {
                    for (var i = 0; i < calendar.columns.length; i++) {
                        var column = calendar.columns[i];
                        var property = column.property;

                        var colargs = {};
                        colargs.value = tasktools.getProperty(args.resource.task, property);
                        colargs.html = colargs.value;
                        columns.push(colargs);
                    }
                }

                if (typeof calendar.onBeforeRowHeaderRender === "function") {
                    var rargs = {};
                    rargs.task = new DayPilot.Task(args.resource.task, calendar);
                    rargs.row = {};
                    DayPilot.Util.copyProps(args.resource, rargs.row, ['html', 'backColor', 'cssClass', 'expanded', 'minHeight', 'marginBottom', 'toolTip', 'contextMenu', 'areas']);
                    rargs.row.columns = columns;
                    calendar.onBeforeRowHeaderRender(rargs);
                    DayPilot.Util.copyProps(rargs.row, args.resource, ['html', 'backColor', 'cssClass', 'expanded', 'minHeight', 'marginBottom', 'toolTip', 'contextMenu', 'areas']);
                }

                if (DayPilot.isArray(calendar.columns)) {
                    for (var i = 0; i < calendar.columns.length; i++) {
                        var html = columns[i].html;
                        if (i === 0) {
                            args.resource.html = html;
                        }
                        else {
                            args.resource.columns[i - 1].html = html;
                        }
                    }
                }

            };
             */


            scheduler.onBeforeCellRender = function(args) {
                args.task = calendar.tasks.find(args.cell.resource);

                delete args.cell.resource;
                if (typeof calendar.onBeforeCellRender === "function") {
                    calendar.onBeforeCellRender(args);
                }
            };

            scheduler.onBeforeTimeHeaderRender = function(args) {
                if (typeof calendar.onBeforeTimeHeaderRender === "function") {
                    calendar.onBeforeTimeHeaderRender(args);
                }
            };

            scheduler.onEventClick = function(args) {
                args.task = new DayPilot.Task(args.e, calendar);
                if (typeof calendar.onTaskClick === "function") {
                    calendar.onTaskClick(args);
                }
            };

            scheduler.onEventClicked = function(args) {
                if (typeof calendar.onTaskClicked === "function") {
                    calendar.onTaskClicked(args);
                }
            };

            scheduler.onEventDelete = function(args) {
                if (typeof calendar.onTaskDelete === "function") {
                    calendar.onTaskDelete(args);
                }
            };

            scheduler.onEventDeleted = function(args) {
                if (typeof calendar.onTaskDeleted === "function") {
                    calendar.onTaskDeleted(args);
                }
            };

            scheduler.onEventDoubleClick = function(args) {
                args.task = new DayPilot.Task(args.e, calendar);
                if (typeof calendar.onTaskDoubleClick === "function") {
                    calendar.onTaskDoubleClick(args);
                }
            };

            scheduler.onEventDoubleClicked = function(args) {
                if (typeof calendar.onTaskDoubleClicked === "function") {
                    calendar.onTaskDoubleClicked(args);
                }
            };

            scheduler.onEventRightClick = function(args) {
                args.task = new DayPilot.Task(args.e, calendar);
                if (typeof calendar.onTaskRightClick === "function") {
                    calendar.onTaskRightClick(args);
                }
            };

            scheduler.onEventRightClicked = function(args) {
                if (typeof calendar.onTaskRightClicked === "function") {
                    calendar.onTaskRightClicked(args);
                }
            };

            scheduler.onRowMoving = function(args) {
                args._source = args.source;
                args._target = args.target;
                args.source = new DayPilot.Task(args._source.$.row.task, calendar);
                args.target = new DayPilot.Task(args._target.$.row.task, calendar);

                if (typeof calendar.onRowMoving === "function") {
                    calendar.onRowMoving(args);
                }
            };

            scheduler.onRowMove = function(args) {
                args._source = args.source;
                args._target = args.target;
                args.source = new DayPilot.Task(args._source.$.row.task, calendar);
                args.target = new DayPilot.Task(args._target.$.row.task, calendar);
                if (typeof calendar.onRowMove === "function") {
                    calendar.onRowMove(args);
                }
                args.source = args._source;
                args.target = args._target;
            };

            scheduler.onRowMoved = function(args) {

                var updateNow = calendar.rowMoveHandling === "Update" || calendar.rowMoveHandling === "Notify";

                if (updateNow) {
                    // update parents
                    var source = args._source.$.row.task;
                    var target = args._target.$.row.task;
                    var position = args.position;

                    // *******************

                    if (position === "forbidden") {
                        return;
                    }

                    // remove from source
                    var sourceParent = tasktools.findParentArray(source);
                    if (!sourceParent) {
                        throw "Cannot find source node parent";
                    }
                    var sourceIndex = DayPilot.indexOf(sourceParent, source);
                    sourceParent.splice(sourceIndex, 1);

                    // move to target
                    var targetParent = tasktools.findParentArray(target);
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

                    //wrap._loadTasks();
                    calendar._angular.notify();

                    calendar.update();

                    // *******************
                }

                args.source = new DayPilot.Task(args._source.$.row.task, calendar);
                args.target = new DayPilot.Task(args._target.$.row.task, calendar);

                if (typeof calendar.onRowMoved === "function") {
                    calendar.onRowMoved(args);
                }
            };

            scheduler.onRowClick = function(args) {
                args.task = new DayPilot.Task(args.resource.$.row.task, calendar);
                if (typeof calendar.onRowClick === "function") {
                    calendar.onRowClick(args);
                }
            };

            scheduler.onRowClicked = function(args) {
                if (typeof calendar.onRowClicked === "function") {
                    calendar.onRowClicked(args);
                }
            };

            scheduler.onRowDoubleClick = function(args) {
                args.task = new DayPilot.Task(args.resource.$.row.task, calendar);
                if (typeof calendar.onRowDoubleClick === "function") {
                    calendar.onRowDoubleClick(args);
                }
            };

            scheduler.onRowDoubleClicked = function(args) {
                if (typeof calendar.onRowDoubleClicked === "function") {
                    calendar.onRowDoubleClicked(args);
                }
            };

            scheduler.onRowEdit = function(args) {
                args.task = new DayPilot.Task(args.resource.$.row.task, calendar);
                if (typeof calendar.onRowEdit === "function") {
                    calendar.onRowEdit(args);
                }
            };

            scheduler.onRowEdited = function(args) {
                if (typeof calendar.onRowEdited === "function") {
                    calendar.onRowEdited(args);
                }
            };

            scheduler.onRowSelect = function(args) {
                args.task = new DayPilot.Task(args.row.$.row.task, calendar);
                if (typeof calendar.onRowSelect === "function") {
                    calendar.onRowSelect(args);
                }
            };

            scheduler.onRowSelected = function(args) {
                if (typeof calendar.onRowSelected === "function") {
                    calendar.onRowSelected(args);
                }
            };

            scheduler.onEventMove = function(args) {
                args._e = args.e;
                args.task = new DayPilot.Task(args.e, calendar);
                delete args.e;
                delete args.position;
                delete args.newResource;
                if (typeof calendar.onTaskMove === "function") {
                    calendar.onTaskMove(args);
                }
            };

            scheduler.onEventMoved = function(args) {

/*
                var updateParent = function(parent) {
                    if (!parent) {
                        return;
                    }

                    var pe = scheduler.events.find(parent.id);
                    var children = wrap.childrenStartEnd(parent);
                    pe.start(children.start);
                    pe.end(children.end);
                    scheduler.events.update(pe);
                    updateParent(pe.data.parent);
                };
*/

                if (calendar.taskGroupMode === "Auto") {
                    var e = args._e;

                    // update source
                    var task = e.data.task;
                    task.start = e.start();
                    task.end = e.end();

                    //updateParent(args.e.data.parent);

                    while (e.data.parent) {
                        var parent = scheduler.events.find(e.data.parent.id);
                        var children = wrap.childrenStartEnd(e.data.parent);
                        parent.start(children.start);
                        parent.end(children.end);
                        scheduler.events.update(parent);
                        e = parent;
                    }

                }

                calendar._angular.notify();

                if (typeof calendar.onTaskMoved === "function") {
                    calendar.onTaskMoved(args);
                }
            };

            scheduler.onEventMoving = function(args) {
                args.task = new DayPilot.Task(args.e, calendar);
                args._e = args.e;

                delete args.position;
                delete args.overlapping;
                delete args.resource;

                if (typeof calendar.onTaskMoving === "function") {
                    calendar.onTaskMoving(args);
                }
            };

            scheduler.onEventResize = function(args) {
                args._e = args.e;
                args.task = new DayPilot.Task(args.e, calendar);
                if (typeof calendar.onTaskResize === "function") {
                    calendar.onTaskResize(args);
                }
            };

            scheduler.onEventResized = function(args) {
                if (calendar.taskGroupMode === "Auto") {

                    var e = args._e;

                    // update source
                    var task = e.data.task;
                    task.start = e.start();
                    task.end = e.end();

                    while (e.data.parent) {
                        var parent = scheduler.events.find(e.data.parent.id);
                        var children = wrap.childrenStartEnd(e.data.parent);
                        parent.start(children.start);
                        parent.end(children.end);
                        scheduler.events.update(parent);
                        e = parent;
                    }
                }
                calendar._angular.notify();

                if (typeof calendar.onTaskResized === "function") {
                    calendar.onTaskResized(args);
                }
            };

            scheduler.onEventResizing = function(args) {
                args.task = new DayPilot.Task(args.e, calendar);
                args._e = args.e;

                if (typeof calendar.onTaskResizing === "function") {
                    calendar.onTaskResizing(args);
                }
            };

            scheduler.onLinkCreate = function(args) {
                args.source = calendar.tasks.find(args.from);
                args.target = calendar.tasks.find(args.to);
                if (typeof calendar.onLinkCreate === "function") {
                    calendar.onLinkCreate(args);
                }
            };

            scheduler.onLinkCreated = function(args) {
                if (typeof calendar.onLinkCreated === "function") {
                    calendar.onLinkCreated(args);
                }
            };

        };

        wrap._rowObjectForTaskData = function(data) {
            for (var i = 0; i < scheduler.rowlist.length; i++) {
                var row = scheduler.rowlist[i];
                if (row.task === data) {
                    return scheduler.internal.createRowObject(row);
                }
            }
            return null;
        };

        wrap._loadLinks = function() {
            scheduler.links = calendar.links.list ? calendar.links.list : [];
        };

        wrap._loadTasks =  function() {
            scheduler.resources = [];
            scheduler.events.list = [];

            tasktools.clearCache();

            wrap._loadChildren(calendar.tasks.list, scheduler.resources, null);
        };

        wrap._doBeforeTaskRender = function(task) {

            var type = task.type || "Task";
            if (task.children && task.children.length) {
                type = "Group";
            }

            /*
            if (typeof calendar.onBeforeTaskRender !== "function") {
                return {
                    "data": task,
                    "type": type
                };
            }
            */

            var data = {};

            // make a copy
            for (var name in task) {
                if (name === "children") {
                    continue;
                }
                data[name] = task[name];
            }

            var args = {};
            args.data = data;
            args.type = type;

            if (!data.box) {
                data.box = {};
            }

            if (typeof data.box.html === "undefined") {
                if (args.type === "Task") {
                    var complete = data.complete || 0;
                    data.box.html = complete + "%";
                }
                else {
                    data.box.html = "";
                }
            }

            if (typeof data.box.htmlRight === "undefined") {
                data.box.htmlRight = data.text;
            }

            if (typeof calendar.onBeforeTaskRender === "function") {
                calendar.onBeforeTaskRender(args);
            }

            // read-only, override the
            args.type = type;

            return args;
        };


        wrap._loadChildren = function(source, target, parent) {
            if (!DayPilot.isArray(source)) {
                return;
            }
            for (var i = 0; i < source.length; i++) {
                var original = source[i];
                tasktools.addToCache(original, parent);
                var args = wrap._doBeforeTaskRender(original);
                var task = args.data;
                var type = args.type;

                var event = {};
                DayPilot.Util.copyProps(task, event, ['id', 'start', 'end', 'text', 'complete']);
                DayPilot.Util.copyProps(task.box, event);
                event.parent = parent;
                event.task = original;
                event.resource = task.id;

                if (type == "Group") {
                    event.type = "Group";
                    event.html = "";
                    //event.html = task.html;
                    //event.barHidden = true;
                    delete event.backColor;
                    if (calendar.taskGroupMode === "Auto") {
                        var children = wrap.childrenStartEnd(original);
                        event.start = children.start;
                        event.end = children.end;
                        event.resizeDisabled = true;
                        event.moveDisabled = true;
                    }
                }
                else if (type === "Milestone") {
                    // TODO modifying the original object (remove?)
                    original.end = original.start;

                    task.end = task.start;

                    event.end = task.start;
                    //event.html = task.html;
                    event.barHidden = true;
                    event.resizeDisabled = true;
                    event.type = "Milestone";
                    event.width = calendar.taskHeight;
                    delete event.backColor;
                }
                else if (type === "Task" ){
                    //event.html = task.box.html;
                    doNothing();
                }

                event.moveVDisabled = true;

                event.htmlRight = task.box.htmlRight;
                event.htmlLeft = task.box.htmlLeft;

                scheduler.events.list.push(event);

                var r = {};

                // source
                r.task = original;

                // server-based only
                DayPilot.Util.copyProps(task.row, r);

                r.name = task.text;
                r.id = task.id;
                r.children = [];
                r.expanded = !task.row || !task.row.collapsed;

                if (original.children && original.children.length) {
                    wrap._loadChildren(original.children, r.children, original);
                }

                target.push(r);

            }
        };

        wrap.childrenStartEnd = function(task) {
            if (!task.children || !task.children.length) {
                var start = task.start;
                var end = task.end;
                if (task.type === "Milestone") {
                    end = start;
                }
                return { "start": new DayPilot.Date(start), "end": new DayPilot.Date(end) };
            }
            var start = null;
            var end = null;
            for (var i = 0; i < task.children.length; i++) {
                var children = wrap.childrenStartEnd(task.children[i]);
                if (!start || children.start.getTime() < start.getTime()) {
                    start = children.start;
                }
                if (!end || children.end.getTime() > end.getTime()) {
                    end = children.end;
                }
            }
            return { "start": start, "end": end};
        };

        this.rows = {};

        this.rows.expand = function(levels) {
            scheduler.rows.expand(levels);
        };

        this.rows.expandAll = function() {
            scheduler.rows.expandAll();
        };

        this.internal = {};
        this.internal.initialized =  function() {
            return calendar._initialized;
        };
        // DayPilot.Task
        this.internal.rowObjectForTaskData = wrap._rowObjectForTaskData;

        this._angular = {};
        this._angular.scope = null;
        this._angular.notify = function() {
            if (calendar._angular.scope) {
                calendar._angular.scope["$apply"]();
            }
        };


        this._specialHandling = null;
        this._loadOptions = function(options) {
            var specialHandling = {
                "tasks": {
                    "preInit": function() {
                        var tasks = this.data;
                        if (!tasks) {
                            return;
                        }
                        if (DayPilot.isArray(tasks.list)) {
                            calendar.tasks.list = tasks.list;
                        }
                        else {
                            calendar.tasks.list = tasks;
                        }
                    }
                },
                "links": {
                    "preInit": function() {

                        var links = this.data;
                        if (!links) {
                            return;
                        }
                        if (DayPilot.isArray(links.list)) {
                            calendar.links.list = links.list;
                        }
                        else {
                            calendar.links.list = links;
                        }
                    }
                },
                "scrollTo": {
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

        this._loadOptions(options);
    };

    // experimental jQuery bindings
    if (typeof jQuery !== 'undefined') {
        (function($) {
            $.fn.daypilotGantt = function(options) {
                var first = null;
                var j = this.each(function() {
                    if (this.daypilot) { // already initialized
                        return;
                    };

                    var daypilot = new DayPilot.Gantt(this.id, options);
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

        app.directive("daypilotGantt", function() {
            return {
                "restrict": "E",
                "template": "<div id='{{id}}'></div>",
                "compile": function compile(element, attrs) {
                    element.replaceWith(this["template"].replace("{{id}}", attrs["id"]));

                    return function link(scope, element, attrs) {
                        var calendar = new DayPilot.Gantt(element[0]);
                        calendar._angular.scope = scope;
                        calendar.init();

                        var oattr = attrs["id"];
                        if (oattr) {
                            scope[oattr] = calendar;
                        }

                        var watch = scope["$watch"];

                        watch.call(scope, attrs["daypilotConfig"], function (value) {
                            calendar._loadOptions(value);
                            calendar.update();
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