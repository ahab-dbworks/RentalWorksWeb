namespace dbworks.editor.models {

    export class file {

        fileName: string;
        fileContents: string;
        path: string;
        ext: string;
        isEditable: boolean;
        hasChanged: boolean;

        constructor(file?: Ifile) {
            if (file == null || undefined) file = {};
            this.fileName = file.fileName;
            this.fileContents = file.fileContents;
            this.path = file.path;
            this.ext = file.ext;
            this.isEditable = file.isEditable == null ? true : true;
            this.hasChanged = file.hasChanged == null ? true : false;
        }

        update(prop: string, value: any): file {            
            //file.fileContents = this.code_mirror.getValue();
            this.hasChanged = true;            
            return this;
        }

    }

    interface Ifile {
        fileName?: string;
        fileContents?: string;
        path?: string;
        ext?: string;
        isEditable?: boolean;
        hasChanged?: boolean;
    }

}