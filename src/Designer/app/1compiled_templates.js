this["templates"] = this["templates"] || {};
this["templates"]["doc"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    return "<div class=\"row\">\r\n    <div class=\"col s6\">\r\n        <div class=\"card\">\r\n            <p>wpok2e1092091ke1</p>\r\n        </div>\r\n    </div>\r\n    <div class=\"col s6\">\r\n        <div class=\"card\">\r\n            <p>wpok2e1092091ke1</p>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n";
},"useData":true});
this["templates"]["editor"] = this["templates"]["editor"] || {};
this["templates"]["editor"]["designer"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    return "<div class=\"modal-content design_module_section\">\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">dns</i></div>\r\n        <div class=\"caption center\">Container</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">chrome_reader_mode</i></div>\r\n        <div class=\"caption center\">Panel</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">flip_to_front</i></div>\r\n        <div class=\"caption center\">Form</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">view_headline</i></div>\r\n        <div class=\"caption center\">Grid</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">view_week</i></div>\r\n        <div class=\"caption center\">Column</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">view_stream</i></div>\r\n        <div class=\"caption center\">Row</div>\r\n    </div>\r\n</div>    ";
},"useData":true});
this["templates"]["editor"]["filenav"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "<li class=\"nav-item\" title=\"Edit "
    + alias4(((helper = (helper = helpers.folderName || (depth0 != null ? depth0.folderName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"folderName","hash":{},"data":data}) : helper)))
    + "\" data-toggle=\"dropdown\" data-index=\""
    + alias4(((helper = (helper = helpers.index || (data && data.index)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data}) : helper)))
    + "\" style=\"cursor: pointer;\">\r\n    <div class=\"dropdown\">\r\n        <a class=\"file_menu_option nav-link dropdown-toggle\" id=\"dropdownMenuButton2\" data-toggle=\"dropdown\">"
    + alias4(((helper = (helper = helpers.folderName || (depth0 != null ? depth0.folderName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"folderName","hash":{},"data":data}) : helper)))
    + "</a>\r\n        <div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuButton2\">\r\n"
    + ((stack1 = helpers.each.call(alias1,(depth0 != null ? depth0.files : depth0),{"name":"each","hash":{},"fn":container.program(2, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "            <div class=\"dropdown-divider\"></div>\r\n            <a class=\"expand_module_folder dropdown-item\">Expand All</a>\r\n            <!--<a class=\"edit_module_folder dropdown-item\">Edit</a>\r\n            <a class=\"delete_module_folder dropdown-item\">Delete</a>-->\r\n            <div class=\"dropdown-divider\"></div>\r\n            <a class=\"close_full_module dropdown-item\">Close</a>\r\n        </div>\r\n    </div>\r\n</li>\r\n";
},"2":function(container,depth0,helpers,partials,data) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "                <a class=\"editable_file dropdown-item\" title=\"Edit "
    + alias4(((helper = (helper = helpers.fileName || (depth0 != null ? depth0.fileName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"fileName","hash":{},"data":data}) : helper)))
    + "\" data-index=\""
    + alias4(((helper = (helper = helpers.index || (data && data.index)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data}) : helper)))
    + "\">"
    + alias4(((helper = (helper = helpers.fileName || (depth0 != null ? depth0.fileName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"fileName","hash":{},"data":data}) : helper)))
    + "</a>\r\n";
},"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1;

  return ((stack1 = helpers.each.call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? depth0.folders : depth0),{"name":"each","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "");
},"useData":true});
this["templates"]["editor"]["filenav_mode_1"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    var stack1;

  return "\r\n"
    + ((stack1 = helpers.each.call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? depth0.folders : depth0),{"name":"each","hash":{},"fn":container.program(2, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "<div class=\"dropdown-divider\"></div>\r\n<a id=\"expand_collapsed_folders\" class=\"dropdown-item\" href=\"#\">Expand All</a>\r\n";
},"2":function(container,depth0,helpers,partials,data) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "<li class=\"nav-item\">\r\n    <a class=\"editable_file nav-link\" title=\"Edit "
    + alias4(((helper = (helper = helpers.folderName || (depth0 != null ? depth0.folderName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"folderName","hash":{},"data":data}) : helper)))
    + "\" data-index=\""
    + alias4(((helper = (helper = helpers.index || (data && data.index)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data}) : helper)))
    + "\">"
    + alias4(((helper = (helper = helpers.folderName || (depth0 != null ? depth0.folderName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"folderName","hash":{},"data":data}) : helper)))
    + "</a>\r\n</li>\r\n";
},"4":function(container,depth0,helpers,partials,data) {
    return "<a class=\"dropdown-item\">No Folders Found</a>\r\n";
},"6":function(container,depth0,helpers,partials,data) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "        <li class=\"nav-item\" title=\"Edit "
    + alias4(((helper = (helper = helpers.folderName || (depth0 != null ? depth0.folderName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"folderName","hash":{},"data":data}) : helper)))
    + "\" data-toggle=\"dropdown\" data-index=\""
    + alias4(((helper = (helper = helpers.index || (data && data.index)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data}) : helper)))
    + "\" style=\"cursor: pointer;\">\r\n            <div class=\"dropdown\">\r\n                <a class=\"file_menu_option nav-link dropdown-toggle\" id=\"dropdownMenuButton2\" data-toggle=\"dropdown\">"
    + alias4(((helper = (helper = helpers.folderName || (depth0 != null ? depth0.folderName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"folderName","hash":{},"data":data}) : helper)))
    + "</a>\r\n                <div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuButton2\">\r\n"
    + ((stack1 = helpers.each.call(alias1,(depth0 != null ? depth0.files : depth0),{"name":"each","hash":{},"fn":container.program(7, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "                    <div class=\"dropdown-divider\"></div>\r\n                    <a class=\"expand_module_folder dropdown-item\">Expand All</a>\r\n                    <!--<a class=\"edit_module_folder dropdown-item\">Edit</a>\r\n                    <a class=\"delete_module_folder dropdown-item\">Delete</a>-->\r\n                    <div class=\"dropdown-divider\"></div>\r\n                    <a class=\"close_full_module dropdown-item\">Close</a>\r\n                </div>\r\n            </div>\r\n        </li>\r\n";
},"7":function(container,depth0,helpers,partials,data) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "                    <a class=\"editable_file dropdown-item\" title=\"Edit "
    + alias4(((helper = (helper = helpers.fileName || (depth0 != null ? depth0.fileName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"fileName","hash":{},"data":data}) : helper)))
    + "\" data-index=\""
    + alias4(((helper = (helper = helpers.index || (data && data.index)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data}) : helper)))
    + "\">"
    + alias4(((helper = (helper = helpers.fileName || (depth0 != null ? depth0.fileName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"fileName","hash":{},"data":data}) : helper)))
    + "</a>\r\n";
},"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, alias1=depth0 != null ? depth0 : (container.nullContext || {});

  return "<!--"
    + ((stack1 = helpers["if"].call(alias1,(depth0 != null ? depth0.folders : depth0),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.program(4, data, 0),"data":data})) != null ? stack1 : "")
    + "-->\r\n\r\n<!--<h1 style=\"color: white;\">FILENAV 1</h1>-->\r\n\r\n    <ul id=\"file_nav\" class=\"navbar-nav\">\r\n"
    + ((stack1 = helpers.each.call(alias1,(depth0 != null ? depth0.folders : depth0),{"name":"each","hash":{},"fn":container.program(6, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "    </ul>\r\n\r\n";
},"useData":true});
this["templates"]["editor"]["filenav_mode_2"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    var stack1;

  return ((stack1 = helpers.each.call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? depth0.files : depth0),{"name":"each","hash":{},"fn":container.program(2, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "");
},"2":function(container,depth0,helpers,partials,data) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "        <li class=\"nav-item\">\r\n            <a class=\"editable_file nav-link\" title=\"Edit "
    + alias4(((helper = (helper = helpers.fileName || (depth0 != null ? depth0.fileName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"fileName","hash":{},"data":data}) : helper)))
    + "\" data-index=\""
    + alias4(((helper = (helper = helpers.index || (data && data.index)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data}) : helper)))
    + "\">"
    + alias4(((helper = (helper = helpers.fileName || (depth0 != null ? depth0.fileName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"fileName","hash":{},"data":data}) : helper)))
    + "</a>\r\n        </li>\r\n";
},"4":function(container,depth0,helpers,partials,data) {
    return "        <li class=\"nav-item\" style=\"display: none;\">\r\n            <a id=\"active_editable_file\" class=\"nav-link\">No Active Folder Group</a>\r\n        </li>\r\n";
},"6":function(container,depth0,helpers,partials,data) {
    var stack1;

  return ((stack1 = helpers.each.call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? depth0.folders : depth0),{"name":"each","hash":{},"fn":container.program(7, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "            <div class=\"dropdown-divider\"></div>\r\n            <a id=\"expand_collapsed_folders\" class=\"dropdown-item\" href=\"#\">Expand All</a>\r\n";
},"7":function(container,depth0,helpers,partials,data) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "                <a class=\"expand_collapsed_folder nav-link\" title=\"Edit "
    + alias4(((helper = (helper = helpers.folderName || (depth0 != null ? depth0.folderName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"folderName","hash":{},"data":data}) : helper)))
    + "\" data-index=\""
    + alias4(((helper = (helper = helpers.index || (data && data.index)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data}) : helper)))
    + "\">"
    + alias4(((helper = (helper = helpers.folderName || (depth0 != null ? depth0.folderName : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"folderName","hash":{},"data":data}) : helper)))
    + "</a>\r\n";
},"9":function(container,depth0,helpers,partials,data) {
    return "            <a class=\"dropdown-item\">No Folders Found</a>\r\n";
},"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {});

  return "<div style=\"display: inline-block;\" data-index=\""
    + container.escapeExpression(((helper = (helper = helpers.index || (depth0 != null ? depth0.index : depth0)) != null ? helper : helpers.helperMissing),(typeof helper === "function" ? helper.call(alias1,{"name":"index","hash":{},"data":data}) : helper)))
    + "\">\r\n    <ul class=\"navbar-nav\" id=\"expanded_folder_container\" style=\"border-radius: 3px; background-color: #3E4041; margin-left: 10px;\">\r\n"
    + ((stack1 = helpers["if"].call(alias1,(depth0 != null ? depth0.files : depth0),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.program(4, data, 0),"data":data})) != null ? stack1 : "")
    + "    </ul>\r\n</div>   \r\n\r\n    <div id=\"collapsed_folders_container\" class=\"dropdown\">\r\n        <a class=\"btn btn-secondary dropdown-toggle\" href=\"https://example.com\" id=\"dropdownMenuLink\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\" style=\"border-radius: 3px; background-color: #3E4041; border: 1px solid #3E4041; color: rgba(255, 255, 255, 0.5); height: 40px; margin-left: 10px;\">\r\n            Folders\r\n        </a>\r\n        <div id=\"collapsed_folders\" class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuLink\">            \r\n"
    + ((stack1 = helpers["if"].call(alias1,(depth0 != null ? depth0.folders : depth0),{"name":"if","hash":{},"fn":container.program(6, data, 0),"inverse":container.program(9, data, 0),"data":data})) != null ? stack1 : "")
    + "        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n";
},"useData":true});
this["templates"]["editor"]["main"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var helper;

  return "<div id=\"master_loader\"></div>\r\n\r\n<div>\r\n    <nav class=\"navbar navbar-toggleable-md navbar-inverse bg-inverse bg-faded\">\r\n        <!--<button class=\"navbar-toggler navbar-toggler-right\" type=\"button\" data-toggle=\"collapse\" data-target=\"#navbarNav\" aria-controls=\"navbarNav\" aria-expanded=\"false\" aria-label=\"Toggle navigation\">\r\n            <span class=\"navbar-toggler-icon\"></span>\r\n        </button>-->\r\n        <div class=\"collapse navbar-collapse\" id=\"navbarNav\">\r\n            <ul class=\"navbar-nav\">\r\n                <li class=\"nav-item active\" data-toggle=\"dropdown\" style=\"cursor: pointer;\">\r\n                    <div class=\"dropdown\">\r\n                        <a class=\"file_menu_option nav-link dropdown-toggle\" id=\"dropdownMenuButton1\" data-toggle=\"dropdown\">New</a>\r\n                        <div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuButton1\">\r\n                            <a class=\"new_file_option dropdown-item\" data-type=\"structure\">Module</a>\r\n                            <!--<div class=\"dropdown-divider\"></div>\r\n                            <a class=\"new_file_option dropdown-item\" data-type=\"browse\">Browse</a>\r\n                            <a class=\"new_file_option dropdown-item\" data-type=\"form\">Form</a>\r\n                            <a class=\"new_file_option dropdown-item\" data-type=\"ts\">TS</a>\r\n                            <a class=\"new_file_option dropdown-item\" data-type=\"menu\">Menu</a>-->\r\n                        </div>\r\n                    </div>\r\n                </li>\r\n                <li class=\"nav-item\" data-toggle=\"dropdown\" style=\"cursor: pointer;\">\r\n                    <div class=\"dropdown\">\r\n                        <a class=\"file_menu_option nav-link dropdown-toggle\" id=\"dropdownMenuButton2\" data-toggle=\"dropdown\">Open</a>\r\n                        <div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuButton2\">\r\n                            <a class=\"open_file_option dropdown-item\" data-config=\"0\">Module</a>\r\n                            <div class=\"dropdown-divider\"></div>\r\n                            <a class=\"open_file_option dropdown-item\" data-config=\"1\">Scan</a>\r\n                        </div>\r\n                    </div>\r\n                </li>\r\n                <li class=\"nav-item\" style=\"cursor: pointer;\">\r\n                    <div class=\"dropdown\">\r\n                        <a class=\"file_menu_option nav-link dropdown-toggle\" id=\"dropdownMenuButton3\" data-toggle=\"dropdown\">Recent</a>\r\n                        <div id=\"recents_dropdown\" class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuButton3\"></div>\r\n                    </div>\r\n                </li>\r\n                <li class=\"nav-item\"><a class=\"file_save nav-link\" style=\"cursor: pointer;\">Save</a></li>\r\n                <li class=\"nav-item\" id=\"editor_settings\" style=\"cursor: pointer;\"><a class=\"nav-link\"><i class=\"material-icons\">more_vert</i></a></li>\r\n            </ul>\r\n            <ul class=\"navbar-nav\" style=\"border-radius: 3px; background-color: #3E4041;\">\r\n                <li class=\"nav-item\">\r\n                    <a id=\"active_editable_file\" class=\"nav-link\">No Active File</a>\r\n                </li>\r\n            </ul>\r\n            <div id=\"file_management_container\">                \r\n                <!--<ul class=\"navbar-nav\" id=\"expanded_folder_container\" style=\"border-radius: 3px; background-color: #3E4041; margin-left: 10px;\">\r\n                    <li class=\"nav-item\" style=\"display: none;\">\r\n                        <a id=\"active_editable_file\" class=\"nav-link\">No Active Folder Group</a>\r\n                    </li>\r\n                </ul>\r\n                <ul id=\"file_nav\" class=\"navbar-nav\"></ul>\r\n                \r\n                <div id=\"collapsed_folders_container\" class=\"dropdown\" style=\"display: none;\">\r\n                    <a class=\"btn btn-secondary dropdown-toggle\" href=\"https://example.com\" id=\"dropdownMenuLink\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\" style=\"border-radius: 3px; background-color: #3E4041; border: 1px solid #3E4041; color: rgba(255, 255, 255, 0.5); height: 40px; margin-left: 10px;\">\r\n                        Folders\r\n                    </a>\r\n                    <div id=\"collapsed_folders\" class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuLink\">\r\n                        <a class=\"dropdown-item\">No Folders Found</a>\r\n                    </div>\r\n                </div>-->\r\n            </div>\r\n        </div>\r\n    </nav>\r\n</div>\r\n\r\n<div id=\"context_menu_options\" style=\"margin-left: 86px; position: relative; z-index:100; display: none;\">\r\n    \r\n</div>\r\n\r\n<div style=\"display: block; position:absolute; height:auto; bottom:0; top:0; left:0; right:0; margin-top:49px;\">\r\n    <div class=\"navbar navbar-inverse navbar-fixed-left\" style=\"background-color: #3E4041 !important; width: 86px; height: 100%; text-align: center;\">\r\n        <ul class=\"nav navbar-nav\">\r\n            <li class=\"nav-item dev_options\" data-view=\"0\"><a class=\"nav-link\">Edit</a></li>\r\n            <li class=\"nav-item dev_options\" data-view=\"1\"><a class=\"nav-link\">Preview</a></li>\r\n            <!--<li class=\"nav-item\" id=\"design\"><a class=\"nav-link\">Design</a></li>-->\r\n            <li class=\"nav-item\" id=\"context_options\"><a class=\"nav-link\">Options</a></li>\r\n        </ul>\r\n    </div>\r\n    <div id=\"version\">\r\n        <div>v"
    + container.escapeExpression(((helper = (helper = helpers.version || (depth0 != null ? depth0.version : depth0)) != null ? helper : helpers.helperMissing),(typeof helper === "function" ? helper.call(depth0 != null ? depth0 : (container.nullContext || {}),{"name":"version","hash":{},"data":data}) : helper)))
    + "</div>\r\n    </div>\r\n</div>\r\n\r\n<div id=\"main_content_body\" data-activefolderindex=\"\" data-activefileindex=\"\">\r\n    <div id=\"code_view\" class=\"main_editor_view\" data-index=\"\" data-scrolltop=\"\">\r\n        <textarea id=\"code_editor\"></textarea>\r\n    </div>\r\n    <div id=\"preview_view\" class=\"main_editor_view\">\r\n        <strong>Nothing to preview.</strong>\r\n    </div>\r\n</div>\r\n\r\n<!-- Modals -->\r\n<div id=\"design_panel\" style=\"display: none;\">\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">dns</i></div>\r\n        <div class=\"caption center\">Container</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">chrome_reader_mode</i></div>\r\n        <div class=\"caption center\">Panel</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">flip_to_front</i></div>\r\n        <div class=\"caption center\">Form</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">view_headline</i></div>\r\n        <div class=\"caption center\">Grid</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">view_week</i></div>\r\n        <div class=\"caption center\">Column</div>\r\n    </div>\r\n    <div class=\"design_module\">\r\n        <div class=\"icon\"><i class=\"material-icons\">view_stream</i></div>\r\n        <div class=\"caption center\">Row</div>\r\n    </div>\r\n</div>\r\n\r\n<div id=\"module_modal\" class=\"modal fade\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"modal\" aria-hidden=\"true\">\r\n    <div class=\"modal-dialog\" role=\"document\">\r\n        <div class=\"modal-content\">\r\n            <div class=\"modal-header\">\r\n                <h5 class=\"modal-title\">Module</h5>\r\n            </div>\r\n            <div class=\"loader\"></div>\r\n            <div class=\"modal-body\">\r\n                ...\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</button>\r\n                <button type=\"button\" id=\"generate_module\" class=\"btn btn-primary\">Save</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div id=\"scan_modal\" class=\"modal fade\" data-config=\"1\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"scan\" aria-hidden=\"true\">\r\n    <div class=\"modal-dialog\" role=\"document\">\r\n        <div class=\"modal-content\">\r\n            <div class=\"modal-header\">\r\n                <h5 class=\"modal-title\">Scan</h5>\r\n            </div>\r\n            <div class=\"loader\"></div>\r\n            <div class=\"modal-body\">\r\n                ...\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <input type=\"button\" id=\"open_folder\" class=\"btn btn-primary\" style=\"display: none;\" value=\"Open Folder\" />\r\n                <input type=\"button\" id=\"open_file\" class=\"btn btn-primary\" value=\"Open\" />\r\n                <input type=\"button\" id=\"scan_files\" class=\"btn btn-primary\" value=\"Scan\" />\r\n                <!--<input type=\"button\" id=\"scan_rescan\" class=\"btn btn-primary\" value=\"Rescan\" style=\"display: none;\" />-->\r\n                <a class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"modal fade\" id=\"settings_modal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"settings\" aria-hidden=\"true\">\r\n    <div class=\"modal-dialog\" role=\"document\">\r\n        <div class=\"modal-content\">\r\n            <div class=\"modal-header\">\r\n                <h5 class=\"modal-title\">Settings</h5>\r\n            </div>\r\n            <div class=\"loader\"></div>\r\n            <div class=\"modal-body\">\r\n                ...\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</button>\r\n                <button type=\"button\" id=\"save_settings\" class=\"btn btn-secondary\">Save</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n";
},"useData":true});
this["templates"]["editor"]["module"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, alias1=container.lambda, alias2=container.escapeExpression;

  return "<div id=\"structure_input_fields\" class=\"structure_response_container\">\r\n    <div class=\"row\">\r\n        <div class=\"col-12\">\r\n            <div class=\"form-group\">\r\n                <!--<input placeholder=\"My Module\" id=\"structure_name\" type=\"text\" class=\"validate\">\r\n                <label for=\"structure_name\" data-error=\"Not so good.\" data-success=\"Looks good.\">Module Name</label>-->\r\n                <label for=\"module_name\">Module Name</label>\r\n                <input type=\"text\" class=\"form-control\" id=\"module_name\" placeholder=\"My Module\">\r\n                <samll class=\"form-text text-muted\">This defines a major part of the modules name.</samll>\r\n            </div>                        \r\n        </div>\r\n        <div class=\"col-12\">\r\n            <div class=\"form-group\">\r\n                <label for=\"module_path\">Module Path</label>\r\n                <input type=\"text\" class=\"form-control\" id=\"module_path\" placeholder=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultModuleSavePath : stack1), depth0))
    + "\" value=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultModuleSavePath : stack1), depth0))
    + "\">\r\n                <!--<input placeholder=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultModuleSavePath : stack1), depth0))
    + "\" id=\"structure_path\" value=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultModuleSavePath : stack1), depth0))
    + "\" type=\"text\" class=\"validate\">\r\n                <label for=\"structure_path\" data-error=\"Not so good.\" data-success=\"Looks good.\">Module Path</label>-->\r\n                <samll class=\"form-text text-muted\">The location at which you can find your module.</samll>\r\n            </div>\r\n        </div>\r\n        <div class=\"col-12\">\r\n            <div class=\"form-group\">\r\n                <label for=\"module_path\">Menu Path</label>\r\n                <input type=\"text\" class=\"form-control\" id=\"menu_path\" placeholder=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultMenuSavePath : stack1), depth0))
    + "\" value=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultMenuSavePath : stack1), depth0))
    + "\">            \r\n                <samll class=\"form-text text-muted\">The location at which you can find your menu.</samll>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-6\">                        \r\n            <label class=\"custom-control custom-checkbox\">\r\n                <input id=\"check_browse\" type=\"checkbox\" class=\"custom-control-input\" checked>\r\n                <span class=\"custom-control-indicator\"></span>\r\n                <span class=\"custom-control-description\">Include Browse</span>\r\n            </label>\r\n        </div>\r\n        <div class=\"col-6\">                        \r\n            <label class=\"custom-control custom-checkbox\">\r\n                <input id=\"check_form\" type=\"checkbox\" class=\"custom-control-input\" checked>\r\n                <span class=\"custom-control-indicator\"></span>\r\n                <span class=\"custom-control-description\">Include Form</span>\r\n            </label>\r\n        </div>    \r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-6\">\r\n            <label class=\"custom-control custom-checkbox\">\r\n                <input id=\"check_browse\" type=\"checkbox\" class=\"custom-control-input\" checked>\r\n                <span class=\"custom-control-indicator\"></span>\r\n                <span class=\"custom-control-description\">Include TS</span>\r\n            </label>\r\n        </div>\r\n        <div class=\"col-6\">\r\n            <label class=\"custom-control custom-checkbox\">\r\n                <input id=\"check_form\" type=\"checkbox\" class=\"custom-control-input\" checked>\r\n                <span class=\"custom-control-indicator\"></span>\r\n                <span class=\"custom-control-description\">Include Menu</span>\r\n            </label>\r\n        </div>\r\n    </div>\r\n</div>\r\n        ";
},"useData":true});
this["templates"]["editor"]["options_context"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    return "<nav class=\"navbar navbar-toggleable-md navbar-inverse bg-inverse bg-faded\" style=\"background-color: #565a5d !important;\">\r\n    <div class=\"collapse navbar-collapse\">\r\n        <ul class=\"context_menu_nav navbar-nav\" id=\"default_options\" style=\"display: none;\">\r\n            <li>\r\n                <samll class=\"form-text\" style=\"color: white;\">No options found.</samll>\r\n            </li>\r\n        </ul>\r\n        <ul class=\"context_menu_nav navbar-nav\" id=\"edit_options\" style=\"display: none;\">\r\n            <li>\r\n                <samll class=\"form-text\" style=\"color: white;\">No options found.</samll>\r\n            </li>\r\n        </ul>\r\n        <ul class=\"context_menu_nav navbar-nav\" id=\"preview_options\" style=\"display: none;\">\r\n            <li class=\"nav-item active\" data-toggle=\"dropdown\" style=\"cursor: pointer;\">\r\n                <div class=\"form-group\">\r\n                    <samll class=\"form-text text-muted\" style=\"color: white !important;\">Render Style Type</samll>\r\n                    <select class=\"form-control\" id=\"render_style_select\">\r\n                        <option value=\"theme-default\">Default</option>\r\n                        <option value=\"theme-classic\">Classic</option>\r\n                        <option value=\"theme-material\">Material</option>\r\n                    </select>\r\n                </div>\r\n            </li>\r\n            <li>\r\n                <div>\r\n                    <samll class=\"form-text text-muted\" style=\"color: white !important;\">Display Size</samll>\r\n                    <div class=\"form-check form-check-inline\" style=\"margin-top: 6px;\">\r\n                        <label class=\"form-check-label\">\r\n                            <input class=\"form-check-input display_size_option\" type=\"radio\" name=\"displaysizeoption\" id=\"inlineRadio3\" value=\"1\">\r\n                            <i class=\"material-icons\">desktop_windows</i>\r\n                        </label>\r\n                    </div>\r\n                    <div class=\"form-check form-check-inline\" style=\"margin-top: 6px;\">\r\n                        <label class=\"form-check-label\">\r\n                            <input class=\"form-check-input display_size_option\" type=\"radio\" name=\"displaysizeoption\" id=\"inlineRadio2\" value=\"2\">\r\n                            <i class=\"material-icons\">tablet</i>\r\n                        </label>\r\n                    </div>\r\n                    <div class=\"form-check form-check-inline\" style=\"margin-top: 6px;\">\r\n                        <label class=\"form-check-label\">\r\n                            <input class=\"form-check-input display_size_option\" type=\"radio\" name=\"displaysizeoption\" id=\"inlineRadio1\" value=\"3\">\r\n                            <i class=\"material-icons\">smartphone</i>\r\n                        </label>\r\n                    </div>\r\n                </div>\r\n            </li>\r\n        </ul>\r\n    </div>\r\n</nav>";
},"useData":true});
this["templates"]["editor"]["recents"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    var stack1;

  return ((stack1 = helpers.each.call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? depth0.recent : depth0),{"name":"each","hash":{},"fn":container.program(2, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "    <div class=\"dropdown-divider\"></div>\r\n    <a class=\"clear_recent_options dropdown-item\">Clear</a>\r\n";
},"2":function(container,depth0,helpers,partials,data) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=helpers.helperMissing, alias3="function", alias4=container.escapeExpression;

  return "    <a class=\"open_recent_option dropdown-item\" data-path=\""
    + alias4(((helper = (helper = helpers.value || (depth0 != null ? depth0.value : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"value","hash":{},"data":data}) : helper)))
    + "\" data-name=\""
    + alias4(((helper = (helper = helpers.key || (depth0 != null ? depth0.key : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"key","hash":{},"data":data}) : helper)))
    + "\">"
    + alias4(((helper = (helper = helpers.key || (depth0 != null ? depth0.key : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"key","hash":{},"data":data}) : helper)))
    + "</a>\r\n";
},"4":function(container,depth0,helpers,partials,data) {
    return "    <a class=\"dropdown-item disabled\">No recents found.</a>\r\n";
},"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1;

  return ((stack1 = helpers["if"].call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? depth0.recent : depth0),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.program(4, data, 0),"data":data})) != null ? stack1 : "");
},"useData":true});
this["templates"]["editor"]["scan"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, alias1=container.lambda, alias2=container.escapeExpression;

  return "<!--<div id=\"scan_container\">\r\n    <div class=\"modal-content\">-->\r\n<div id=\"scan_fields\" class=\"scan_content\">\r\n    <div class=\"row\">\r\n        <div class=\"col-12\">\r\n            <label for=\"scan_path\">Scan Path</label>\r\n            <input type=\"text\" class=\"form-control form-inline\" id=\"scan_path\" placeholder=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultModuleSavePath : stack1), depth0))
    + "\" value=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultModuleSavePath : stack1), depth0))
    + "\">\r\n            <!--<input id=\"scan_path\" placeholder=\"C:\\DevFolder\\\" value=\"C:\\dbworksprojects\" type=\"text\" class=\"validate\">\r\n            <label for=\"scan_path\" data-error=\"Not so good.\" data-success=\"Path looks good to me.\">Structure Path</label>-->\r\n            <samll class=\"form-text text-muted\">The target path gets scanned for all compatible files.</samll>\r\n        </div>\r\n    </div>\r\n    <div id=\"module_name_container\" class=\"row\">\r\n        <div class=\"col-12\">\r\n            <label for=\"scan_path\">Module Name</label>\r\n            <input type=\"text\" class=\"form-control\" id=\"scan_module_name\" placeholder=\"My Module\" value=\"\">\r\n            <samll class=\"form-text text-muted\">Module folders with matching Module Names provided, will show in the results.</samll>\r\n        </div>\r\n    </div>\r\n    <div id=\"module_menu_container\" class=\"row\">\r\n        <div class=\"col-12\">\r\n            <label for=\"scan_path\">Menu Path</label>\r\n            <input type=\"text\" class=\"form-control\" id=\"scan_module_menu_path\" placeholder=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultMenuSavePath : stack1), depth0))
    + "\" value=\""
    + alias2(alias1(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultMenuSavePath : stack1), depth0))
    + "\">\r\n            <samll class=\"form-text text-muted\">The target path gets the associated menu file. If not provided, no file will be pulled.</samll>\r\n        </div>\r\n    </div>\r\n    <div id=\"module_only_container\" class=\"row\">\r\n        <div class=\"col-12\">\r\n            <label class=\"custom-control custom-checkbox\">\r\n                <input id=\"scan_modules_only\" type=\"checkbox\" class=\"custom-control-input\">\r\n                <span class=\"custom-control-indicator\"></span>\r\n                <span class=\"custom-control-description\">Modules Only</span>\r\n            </label>\r\n            <samll class=\"form-text text-muted\">Scans results only display valid modules.</samll>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div id=\"scanning\" class=\"scan_content\">\r\n    Scanning Path...\r\n</div>\r\n\r\n<div id=\"scan_folder\" class=\"scan_content\">\r\n    \r\n</div>\r\n    <!--</div>\r\n    <div class=\"modal-footer\">\r\n        <a class=\"modal-action modal-close waves-effect waves-light btn-flat\">Close</a>\r\n        <a id=\"scan_rescan\" class=\"modal-action waves-effect waves-light btn-flat\">Rescan</a>        \r\n        <a id=\"scan_back\" class=\"modal-action waves-effect waves-light btn-flat\">Back</a>\r\n    </div>\r\n</div>-->\r\n";
},"useData":true});
this["templates"]["editor"]["scan_folders_files"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    var alias1=container.lambda, alias2=container.escapeExpression;

  return "    <div style=\"display: inline;\">\r\n        <div style=\"display: inline;\" class=\"scan_breadcrumb\" data-path=\""
    + alias2(alias1((depth0 != null ? depth0.path : depth0), depth0))
    + "\">"
    + alias2(alias1((depth0 != null ? depth0.name : depth0), depth0))
    + "</div>\r\n        <div style=\"display: inline;\"><i class=\"material-icons scan_breadcrumb_icons\">keyboard_arrow_right</i></div>    \r\n    </div>    \r\n";
},"3":function(container,depth0,helpers,partials,data) {
    var stack1;

  return "\r\n        <h5>Folders <i class=\"material-icons scan_toggle_icon\">keyboard_arrow_down</i></h5>\r\n        <div class=\"scan_toggle_container\">\r\n"
    + ((stack1 = helpers.each.call(depth0 != null ? depth0 : (container.nullContext || {}),((stack1 = (depth0 != null ? depth0.folder : depth0)) != null ? stack1.folders : stack1),{"name":"each","hash":{},"fn":container.program(4, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "        </div>\r\n";
},"4":function(container,depth0,helpers,partials,data) {
    var stack1;

  return "            <div>\r\n"
    + ((stack1 = (helpers.doesfolderhavefolders || (depth0 && depth0.doesfolderhavefolders) || helpers.helperMissing).call(depth0 != null ? depth0 : (container.nullContext || {}),depth0,{"name":"doesfolderhavefolders","hash":{},"fn":container.program(5, data, 0),"inverse":container.program(8, data, 0),"data":data})) != null ? stack1 : "")
    + "            </div>                        \r\n";
},"5":function(container,depth0,helpers,partials,data) {
    var stack1, alias1=container.lambda, alias2=container.escapeExpression;

  return "                <div class=\"scan_folder_section hover_background_gray\" style=\"display: inline;\" data-path=\""
    + alias2(alias1((depth0 != null ? depth0.path : depth0), depth0))
    + "\" data-node=\""
    + alias2(alias1((depth0 != null ? depth0.folderName : depth0), depth0))
    + "\" data-depth=\""
    + alias2(alias1((depth0 != null ? depth0.depth : depth0), depth0))
    + "\">\r\n                    <div class=\"scan_icon_folder\">\r\n                        <i class=\"material-icons\">folder</i>\r\n                    </div>\r\n                    <a class=\"scan_folder_title\">"
    + alias2(alias1((depth0 != null ? depth0.folderName : depth0), depth0))
    + "</a>\r\n                </div>\r\n                <div style=\"display: inline;\">\r\n"
    + ((stack1 = helpers["if"].call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? depth0.isValidModuleFolder : depth0),{"name":"if","hash":{},"fn":container.program(6, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "                </div>\r\n";
},"6":function(container,depth0,helpers,partials,data) {
    var alias1=container.lambda, alias2=container.escapeExpression;

  return "                    <!--<div style=\"display: inline;\"><i class=\"material-icons scan_open_module\" data-path=\""
    + alias2(alias1((depth0 != null ? depth0.path : depth0), depth0))
    + "\" data-name=\""
    + alias2(alias1((depth0 != null ? depth0.folderName : depth0), depth0))
    + "\">open_in_new</i></div>-->\r\n                    <button type=\"button\" class=\"btn btn-dark btn-sm scan_open_module\" style=\"cursor: pointer;\" data-path=\""
    + alias2(alias1((depth0 != null ? depth0.path : depth0), depth0))
    + "\" data-name=\""
    + alias2(alias1((depth0 != null ? depth0.folderName : depth0), depth0))
    + "\">Open</button>\r\n";
},"8":function(container,depth0,helpers,partials,data) {
    var alias1=container.lambda, alias2=container.escapeExpression;

  return "                <div class=\"no_folders_or_files\" title=\"No supported folders or files found.\" data-path=\""
    + alias2(alias1((depth0 != null ? depth0.path : depth0), depth0))
    + "\" data-node=\""
    + alias2(alias1((depth0 != null ? depth0.folderName : depth0), depth0))
    + "\" data-depth=\""
    + alias2(alias1((depth0 != null ? depth0.depth : depth0), depth0))
    + "\">\r\n                    <div class=\"scan_icon_folder\">\r\n                        <i class=\"material-icons\">folder</i>                        \r\n                    </div>\r\n                    <div style=\"display: inline-block;\">"
    + alias2(alias1((depth0 != null ? depth0.folderName : depth0), depth0))
    + "</div>\r\n                </div>\r\n";
},"10":function(container,depth0,helpers,partials,data) {
    return "        <!--<span>No folders found.</span>-->\r\n";
},"12":function(container,depth0,helpers,partials,data) {
    var stack1;

  return "        <h5>Files<i class=\"material-icons scan_toggle_icon\" data-toggletarget=\".scan_file_section\">keyboard_arrow_down</i></h5>\r\n        <div class=\"scan_toggle_container\">\r\n"
    + ((stack1 = helpers.each.call(depth0 != null ? depth0 : (container.nullContext || {}),((stack1 = (depth0 != null ? depth0.folder : depth0)) != null ? stack1.files : stack1),{"name":"each","hash":{},"fn":container.program(13, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "        </div>\r\n";
},"13":function(container,depth0,helpers,partials,data) {
    return "            <div class=\"scan_file_section hover_background_gray\">\r\n                <div class=\"scan_icon_folder\">\r\n                    <i class=\"material-icons\">description</i>\r\n                </div>\r\n                <a class=\"scan_folder_title\">"
    + container.escapeExpression(container.lambda((depth0 != null ? depth0.fileName : depth0), depth0))
    + "</a>\r\n            </div>\r\n";
},"15":function(container,depth0,helpers,partials,data) {
    return "        <!--<span>No files found.</span>-->\r\n";
},"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, alias1=depth0 != null ? depth0 : (container.nullContext || {});

  return "<style>\r\n    .btn-dark {\r\n        color: #fff !important;\r\n        background-color: #343a40 !important;\r\n        border-color: #343a40 !important;\r\n    }\r\n</style>\r\n\r\n<div style=\"margin-bottom: 10px;\">    \r\n"
    + ((stack1 = helpers.each.call(alias1,(depth0 != null ? depth0.breadcrumbs : depth0),{"name":"each","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + "</div>\r\n\r\n<!--<h5 class=\"truncate\">"
    + container.escapeExpression(container.lambda(((stack1 = (depth0 != null ? depth0.folder : depth0)) != null ? stack1.folderName : stack1), depth0))
    + "<i class=\"material-icons scan_open_module\">open_in_new</i></h5>-->\r\n<div class=\"scan_folder_navigation_content\">\r\n    <div>\r\n"
    + ((stack1 = helpers["if"].call(alias1,((stack1 = (depth0 != null ? depth0.folder : depth0)) != null ? stack1.folders : stack1),{"name":"if","hash":{},"fn":container.program(3, data, 0),"inverse":container.program(10, data, 0),"data":data})) != null ? stack1 : "")
    + "    </div>\r\n    <div style=\"margin-left: 15px;\">\r\n"
    + ((stack1 = helpers["if"].call(alias1,((stack1 = (depth0 != null ? depth0.folder : depth0)) != null ? stack1.files : stack1),{"name":"if","hash":{},"fn":container.program(12, data, 0),"inverse":container.program(15, data, 0),"data":data})) != null ? stack1 : "")
    + "    </div>\r\n</div>";
},"useData":true});
this["templates"]["editor"]["settings"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    return " checked ";
},"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.lambda, alias3=container.escapeExpression;

  return "\r\n<div class=\"container\">\r\n\r\n    <ul id=\"tab_nav\" class=\"nav nav-tabs\" role=\"tablist\">\r\n        <li class=\"nav-item\">\r\n            <a class=\"nav-link active\" href=\"#general\" data-toggle=\"tab\" role=\"tab\" aria-controls=\"general\" aria-selected=\"true\">General</a>\r\n        </li>\r\n        <li class=\"nav-item\">\r\n            <a class=\"nav-link\" href=\"#codemirror\" data-toggle=\"tab\" role=\"tab\" aria-controls=\"codemirror\" aria-selected=\"false\">Code Mirror</a>\r\n        </li>\r\n        <li class=\"nav-item\">\r\n            <a class=\"nav-link\" href=\"#preview\" data-toggle=\"tab\" role=\"tab\" aria-controls=\"preview\" aria-selected=\"false\">Preview</a>\r\n        </li>\r\n        <li class=\"nav-item\">\r\n            <a class=\"nav-link\" href=\"#design\" data-toggle=\"tab\" role=\"tab\" aria-controls=\"design\" aria-selected=\"false\">Design</a>\r\n        </li>\r\n    </ul>\r\n\r\n    <div id=\"general\" class=\"tab-pane fade show active\" role=\"tabpanel\" aria-labelledby=\"general-tab\">\r\n        <div class=\"row\">\r\n            <div class=\"col-12\">\r\n                <div class=\"form-group\">\r\n                    <samll class=\"form-text text-muted\">Auto save all files when changes are made every 10secs.</samll>\r\n                    <label class=\"custom-control custom-radio\">\r\n                        <input id=\"autosave_1\" name=\"radio\" type=\"radio\" class=\"custom-control-input\" "
    + ((stack1 = helpers["if"].call(alias1,((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.isAutoSaveOn : stack1),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + ">\r\n                        <span class=\"custom-control-indicator\"></span>\r\n                        <span class=\"custom-control-description\">Auto Save On</span>\r\n                    </label>\r\n                    <label class=\"custom-control custom-radio\">\r\n                        <input id=\"autosave_2\" name=\"radio\" type=\"radio\" class=\"custom-control-input\" "
    + ((stack1 = helpers["if"].call(alias1,((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.isAutoSaveOff : stack1),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + ">\r\n                        <span class=\"custom-control-indicator\"></span>\r\n                        <span class=\"custom-control-description\">Auto Save Off</span>\r\n                    </label>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-12\">\r\n                <div class=\"form-group\">\r\n                    <samll class=\"form-text text-muted\">Default Module Save Path</samll>\r\n                    <input placeholder=\""
    + alias3(alias2(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultModuleSavePath : stack1), depth0))
    + "\" id=\"default_save_module_path_setting\" class=\"form-control validate\" type=\"text\" value=\""
    + alias3(alias2(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultModuleSavePath : stack1), depth0))
    + "\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-12\">\r\n                <div class=\"form-group\">\r\n                    <samll class=\"form-text text-muted\">Default Security Menu Save Path</samll>\r\n                    <input placeholder=\""
    + alias3(alias2(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultMenuSavePath : stack1), depth0))
    + "\" id=\"default_save_menu_path_setting\" class=\"form-control validate\" type=\"text\" value=\""
    + alias3(alias2(((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.defaultMenuSavePath : stack1), depth0))
    + "\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-12\">\r\n                <div class=\"form-group\">\r\n                    <samll class=\"form-text text-muted\">Onload - Open last known Module</samll>\r\n                    <label class=\"custom-control custom-radio\">\r\n                        <input id=\"open_module_1\" name=\"open_radio\" type=\"radio\" class=\"custom-control-input\" "
    + ((stack1 = helpers["if"].call(alias1,((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.isAutoSaveOn : stack1),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + ">\r\n                        <span class=\"custom-control-indicator\"></span>\r\n                        <span class=\"custom-control-description\">Yes</span>\r\n                    </label>\r\n                    <label class=\"custom-control custom-radio\">\r\n                        <input id=\"open_module_2\" name=\"open_radio\" type=\"radio\" class=\"custom-control-input\" "
    + ((stack1 = helpers["if"].call(alias1,((stack1 = (depth0 != null ? depth0.settings : depth0)) != null ? stack1.isAutoSaveOff : stack1),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data})) != null ? stack1 : "")
    + ">\r\n                        <span class=\"custom-control-indicator\"></span>\r\n                        <span class=\"custom-control-description\">No</span>\r\n                    </label>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-12\">\r\n                <div class=\"form-group\">\r\n                    <samll class=\"form-text text-muted\">Max number of allowed recents to save and display.</samll>\r\n                    <input placeholder=\"5\" id=\"number_allowed_recents\" class=\"col-2 form-control validate\" max=\"20\" type=\"number\" value=\"\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-12\">\r\n                <samll class=\"form-text text-muted\">CM - Enable Code Folding</samll>\r\n                <label class=\"custom-control custom-checkbox\">\r\n                    <input type=\"checkbox\" class=\"custom-control-input\">\r\n                    <span class=\"custom-control-indicator\"></span>\r\n                </label>\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-12\">\r\n                <div class=\"form-group\">\r\n                    <samll class=\"form-text text-muted\">CM - Background Color</samll>\r\n                    <input class=\"form-control col-5\" type=\"color\" value=\"#563d7c\" id=\"example-color-input\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div id=\"codemirror\" class=\"tab-pane fade\" role=\"tabpanel\" aria-labelledby=\"codemirror-tab\">Code Mirror</div>\r\n    <div id=\"preview\" class=\"tab-pane fade\" role=\"tabpanel\" aria-labelledby=\"preview-tab\">Preview</div>\r\n    <div id=\"design\" class=\"tab-pane fade\" role=\"tabpanel\" aria-labelledby=\"design-tab\">Design</div>\r\n</div>";
},"useData":true});
this["templates"]["error"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    return "<div class=\"valign-wrapper\">\r\n    <div class=\"center row\">\r\n        <div class=\"col s12\">\r\n            <div class=\"card\">\r\n                <div class=\"img_error\"></div>\r\n                <p>Something went wrong. Let's go back to the <a href=\"/#home\">beginning</a>.</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n";
},"useData":true});
this["templates"]["home"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    return "HOME";
},"useData":true});
this["templates"]["main"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    return "<div id=\"main_master_body\" class=\"flex_parent\">    \r\n    MAIN\r\n</div>\r\n";
},"useData":true});
this["templates"]["sandbox"] = Handlebars.template({"compiler":[7,">= 4.0.0"],"main":function(container,depth0,helpers,partials,data) {
    return "SANDBOX";
},"useData":true});