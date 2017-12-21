var ts;

namespace dbworks.editor.controllers {

    export class ts_to_js_compiler {

        static compile(tscript: string): any {                        

            let result = ts.transpileModule(tscript, { compilerOptions: { module: ts.ModuleKind.CommonJS } });

            return result;
        }

    }

}