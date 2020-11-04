const WebApiCompiler = require('./node-WebApiCompiler');

(async () => {
    try {

        const action = process.argv[2]; // the 1st command line argument
        const target = process.argv[3]; // the 2nd command line argument
        const buildConfiguration = process.argv[4]; // the 3rd command line argument
        const reports = process.argv[5]; // the 4th command line argument
        let compiler = new WebApiCompiler(action, target, buildConfiguration, reports);
        await compiler.build();
    }
    catch (ex) {
        console.error(ex);
        console.log('//------------------------------------------------------------------------------------');
        console.error('- BUILD FAILED -')
        console.log('//------------------------------------------------------------------------------------');
    }
})();
