import { GlobalScope } from '../shared/GlobalScope';
import { NewRecordToCreate } from './ModuleBase';

//---------------------------------------------------------------------------------------
export class GridBase {
    gridName: string;
    //moduleName: string;
    //moduleId: string;
    //moduleCaption: string;
    deleteTimeout: number;
    saveTimeout: number;

    canNew: boolean;
    canDelete: boolean;

    defaultNewRecordToExpect?: any;
    newRecordsToCreate?: NewRecordToCreate[];

    globalScopeRef?= GlobalScope;

    //---------------------------------------------------------------------------------------
    constructor(gridName: string) {
        //this.moduleName = 'UnknownModule';
        //this.moduleId = '99999999-9999-9999-9999-999999999999';
        //this.moduleCaption = 'UnknownModule';
        this.gridName = gridName;
        this.canNew = true;
        this.canDelete = true;
        this.deleteTimeout = 120000; // 120 seconds
        this.saveTimeout = 120000; // 120 seconds
        this.globalScopeRef = GlobalScope;
    }
    //---------------------------------------------------------------------------------------
}