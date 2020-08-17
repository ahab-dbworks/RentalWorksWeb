setImmediate(async () => {
    try {
        const QuikScanCompiler = require('./node-QuikScanCompiler');
        const action = process.argv[2]; // the 1st command line argument
        const target = process.argv[3]; // the 2nd command line argument
        const buildConfiguration = process.argv[4]; // the 3rd command line argument
        let compiler = new QuikScanCompiler(action, target, buildConfiguration);
        await compiler.build();
    }
    catch (ex) {
        console.error(ex[0]);
    }
});
