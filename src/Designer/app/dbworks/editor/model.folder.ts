namespace dbworks.editor.models {

    export class folder {
        folderName: string;
        path: string;
        parentPath: string;
        folders: editor.models.folder[];
        files: editor.models.file[];
        isValidModuleFolder: boolean;
        potentialModuleFolder: boolean;
        _id: number;

        constructor(folder?: Ifolder) {
            if (folder == null || undefined) folder = {};
            this.folderName = folder.folderName;
            this.path = folder.path == null || undefined ? null : folder.path;
            this.parentPath = folder.parentPath == null || undefined ? null : folder.parentPath;
            this.folders = folder.folders == null || undefined ? [] : folder.folders;
            this.files = folder.files == null || undefined ? [] : folder.files;
            this.isValidModuleFolder = folder.isValidModuleFolder == null || undefined ? false : folder.isValidModuleFolder;
            this.potentialModuleFolder = folder.potentialModuleFolder == null || undefined ? false : folder.potentialModuleFolder;
            this._id = dbworksutil.generator.number_id();
        }
    }    

    export interface Ifolder {
        folderName?: string;
        path?: string;
        parentPath?: string,
        folders?: editor.models.folder[];
        files?: editor.models.file[];        
        isValidModuleFolder?: boolean;
        potentialModuleFolder?: boolean;
        _id?: number;
    }
}