﻿class AuditHistoryGrid {
    Module: string = 'AuditHistoryGrid';
    apiurl: string = 'api/v1/webauditjson';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let $oldElement = $tr.find('[data-browsedatafield="Json"]');
            let changes = JSON.parse($oldElement.attr('data-originalvalue'));
            let html: Array<string> = [];

            html.push(`<ul class="auditHeader" style="font-size:14px; display:flex;">
                         <span style="font-weight:bold; float:left; width:225px; text-decoration:underline; padding-right:1em;">Field Name</span>
                         <span style="font-weight:bold; float:left; width:200px; text-decoration:underline; padding-right:1em;">Old Value</span>
                         <span style="font-weight:bold; float:left; width:200px; text-decoration:underline;">New Value</span>
                       </ul>`);


            if (changes.length > 0) {
                for (let i = 0; i < changes.length; i++) {
                    if (typeof changes[i].OldValue === 'string') {
                        if (changes[i].OldValue.substring(0, 4) !== 'rgb(') { // For color related fields
                            html.push(`<ul style="font-size:14px; word-wrap:break-word; display:flex;">
                                         <span style="font-weight:bold; float:left; width:225px; padding-right:1em;">${changes[i].FieldName}:</span>
                                         <span style="width:200px; padding-right:1em;">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}</span>
                                         <span style="width:200px; padding-right:4em;">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}</span>
                                         <span class="auditSpacer" style="flex:1 1 0"></span>
                                       </ul>`);
                        } else {
                            html.push(`<ul style="font-size:14px; word-wrap:break-word; display:flex;">
                                         <span style="font-weight:bold; float:left; width:225px; padding-right:1em;">${changes[i].FieldName}:</span>
                                         <span style="width:200px; padding-right:1em;">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}<span style="border-radius:3px;padding-left:18px;margin-left:5px;border:.75px solid black;background-color:${changes[i].OldValue}"></span></span>
                                         <span style="width:200px; padding-right:4em;">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}<span style="border-radius:3px;padding-left:18px;margin-left:5px;border:.75px solid black;background-color:${changes[i].NewValue}"></span></span>
                                         <span class="auditSpacer" style="flex:1 1 0"></span>
                                       </ul>`);
                        }
                    } else {
                        html.push(`<ul style="font-size:14px; word-wrap:break-word; display:flex;">
                                     <span style="font-weight:bold; float:left; width:225px; padding-right:1em;">${changes[i].FieldName}:</span>
                                     <span style="width:200px; padding-right:1em;">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}</span>
                                     <span style="width:200px; padding-right:4em;">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}</span>
                                     <span class="auditSpacer" style="flex:1 1 0"></span>
                                   </ul>`);
                    }
                }
            }
            jQuery($oldElement).replaceWith(html.join(''));
        });
    }
}

var AuditHistoryGridController = new AuditHistoryGrid();
//----------------------------------------------------------------------------------------------