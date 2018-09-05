const WebApiCompiler = require('./node-WebApiCompiler');

try {
    const action = process.argv[2]; // the 1st command line argument
    const target = process.argv[3]; // the 2nd command line argument
    const buildConfiguration = process.argv[4]; // the 3rd command line argument
    let compiler = new WebApiCompiler(action, target, buildConfiguration);
    compiler.build();
}
catch (ex) {
    console.log(ex);
}
