const util = require('util');
var spawn = require('child-process-promise').spawn;
const rmfr = require('rmfr');
const fs = require('fs');
const WebpackReportsCompiler = require('./node-WebpackReportsCompiler');
const UNSUPPORTED_CONFIGURATION = 'Unsupported configuration.';
const path = require('path');

class WebApiCompiler {
    static get TARGET_ALL() { return 'all'; };
    static get TARGET_API() { return 'api' };
    static get TARGET_REPORTS() { return 'reports' };
    static get BUILD_CONFIGURATION_DEVELOPMENT() { return 'dev'; };
    static get BUILD_CONFIGURATION_PRODUCTION() { return 'prod'; };
    static get BUILD_ACTION_BUILD() { return 'build'; };
    static get BUILD_ACTION_RUN() { return 'run'; };
    static get BUILD_ACTION_WATCH() { return 'watch'; };
    //------------------------------------------------------------------------------------
    constructor(buildAction, target, buildMode) {
        this.buildAction = buildAction;
        this.target = target;
        this.buildConfiguration = buildMode;
        this.dotnetConfiguration = this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT ? "Debug" : "Release";
    }
    //------------------------------------------------------------------------------------
    async rmfr_downloads() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Deleting: wwwroot/temp/downloads');
        await rmfr('wwwroot/temp/downloads');
    }
    //------------------------------------------------------------------------------------
    async rmfr_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Deleting: wwwroot/Reports');
        await rmfr('wwwroot/Reports');
    }
    //------------------------------------------------------------------------------------
    async rmfr_publishfolder() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Deleting: ../../build/RentalWorksWebApi');
        await rmfr('../../build/RentalWorksWebApi');
    }
    //------------------------------------------------------------------------------------
    async npm_i() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('npm i');
        console.log('//------------------------------------------------------------------------------------');
        await spawn('npm', ['i'], { stdio: 'inherit' });
    }
    //------------------------------------------------------------------------------------
    async dotnet_restore() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('dotnet restore');
        console.log('//------------------------------------------------------------------------------------');
        await spawn('dotnet', ['restore'], { stdio: 'inherit' });
    }
    //------------------------------------------------------------------------------------
    async clean_api() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Cleaning compiled files...')
        //________________________________________________________
        console.log('Deleting: ../../lib/Fw/src/FwStandard/bin');
        await rmfr('../../lib/Fw/src/FwStandard/bin');
        //________________________________________________________
        console.log('Deleting: ../../lib/Fw/src/FwStandard/obj');
        await rmfr('../../lib/Fw/src/FwStandard/obj');
        //________________________________________________________
        console.log('Deleting: ../../lib/Fw/src/FwCore/bin');
        await rmfr('../../lib/Fw/src/FwCore/bin');
        //________________________________________________________
        console.log('Deleting: ../../lib/Fw/src/FwCore/obj');
        await rmfr('../../lib/Fw/src/FwCore/obj');
        //________________________________________________________
        console.log('Deleting: ../RentalWorksWebLibrary/bin');
        await rmfr('../RentalWorksWebLibrary/bin');
        //________________________________________________________
        console.log('Deleting: ../RentalWorksWebLibrary/obj');
        await rmfr('../RentalWorksWebLibrary/obj');
        //________________________________________________________
        console.log('Deleting: bin');
        await rmfr('bin');
        //________________________________________________________
        console.log('Deleting: obj');
        await rmfr('obj');
    }
    //------------------------------------------------------------------------------------
    async build_webpack_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Building Webpack Reports');
        console.log('//------------------------------------------------------------------------------------');
        let compiler = new WebpackReportsCompiler(this.buildAction, this.target, this.buildConfiguration);
        const stats = await compiler.build();
        console.log(stats.toString({ colors: true }));
    }
    //------------------------------------------------------------------------------------
    async watch_webpack_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Watching Webpack Reports');
        console.log('//------------------------------------------------------------------------------------');
        let compiler = new WebpackReportsCompiler(this.buildAction, this.target, this.buildConfiguration);
        const stats = await compiler.watch();
        console.log(stats.toString({ colors: true }));
    }
    //------------------------------------------------------------------------------------
    async dotnet_build() {
        console.log('//------------------------------------------------------------------------------------');
        console.log(`dotnet build --configuration ${this.dotnetConfiguration}`);
        console.log('//------------------------------------------------------------------------------------');
        await spawn('dotnet', ['build', '--configuration', this.dotnetConfiguration], { stdio: 'inherit' });
    }
    //------------------------------------------------------------------------------------
    async dotnet_publish() {
        console.log('//------------------------------------------------------------------------------------');
        console.log(`dotnet publish -o ../../build/RentalWorksWebApi WebApi.csproj --configuration ${this.dotnetConfiguration} --self-contained -r win-x64`);
        console.log('//------------------------------------------------------------------------------------');
        await spawn('dotnet', ['publish', '-o', '../../build/RentalWorksWebApi', 'WebApi.csproj', '--configuration', this.dotnetConfiguration, '--self-contained', '-r', 'win-x64'], { stdio: 'inherit' });
        console.log('Deleting: appsettings.json');
        await fs.unlink('../../build/RentalWorksWebApi/appsettings.json', function (error) { if (error) { throw error; } console.log('Deleted appsettings.json'); });
        console.log('Adding: wwwroot/temp/downloads');
        await process.chdir('../../build/RentalWorksWebApi/wwwroot');
        await fs.mkdirSync('temp');
        await process.chdir('temp');
        await fs.mkdirSync('downloads');
        console.log('//------------------------------------------------------------------------------------');
    }
    //------------------------------------------------------------------------------------
    async dotnet_run() {
        console.log('//------------------------------------------------------------------------------------');
        console.log(`dotnet run --configuration ${this.dotnetConfiguration} --launch-profile WebApi`);
        console.log('//------------------------------------------------------------------------------------');
        await spawn('dotnet', ['run', '--configuration', this.dotnetConfiguration, '--launch-profile', 'WebApi'], { stdio: 'inherit' });
    }
    //------------------------------------------------------------------------------------
    async build_securitytree() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Building security tree...');
        console.log(`dotnet run --outputfile ../RentalWorksWebApi/ApplicationManager/WebApiApp.Json`);
        console.log('//------------------------------------------------------------------------------------');
        let pathSecurityTreeProject = path.resolve(__dirname, '../SecurityTreeBuilder/').replace(/\\/g, '/');
        let pathOutputFile = path.resolve(__dirname, '../RentalWorksWebApi/ApplicationManager/WebApiApp.Json').replace(/\\/g, '/');
        console.log('pathSecurityTreeProject:', pathSecurityTreeProject);
        console.log('pathOutputFile:', pathOutputFile);
        await spawn('dotnet', ['run', '--outputfile', pathOutputFile], {
            cwd: pathSecurityTreeProject,
            stdio: 'inherit'
        });

    }
    //------------------------------------------------------------------------------------
    async build() {
        try {
            if (this.target === WebApiCompiler.TARGET_ALL) {
                if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
                    if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.rmfr_reports();
                        await this.build_webpack_reports();
                        await this.dotnet_build();
                    } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_RUN) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.rmfr_reports();
                        await this.build_webpack_reports();
                        await this.dotnet_run();
                    } else {
                        throw UNSUPPORTED_CONFIGURATION;
                    }
                } else if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                    await this.clean_api();
                    await this.rmfr_downloads();
                    await this.rmfr_reports();
                    await this.rmfr_publishfolder();
                    await this.npm_i();
                    await this.build_webpack_reports();
                    await this.dotnet_publish();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            } else if (this.target === WebApiCompiler.TARGET_API) {
                if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
                    if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.dotnet_build();
                    } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_RUN) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.dotnet_run();
                    } else {
                        throw UNSUPPORTED_CONFIGURATION;
                    }
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            } else if (this.target === WebApiCompiler.TARGET_REPORTS) {
                if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                    await this.npm_i();
                    await this.rmfr_reports();
                    await this.build_webpack_reports();
                } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_WATCH) {
                    await this.npm_i();
                    await this.rmfr_reports();
                    await this.watch_webpack_reports();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            }
        } catch (ex) {
            if (Array.isArray(ex)) {
                for (let i = 0; i < ex.length; i++) {
                    console.log(ex[i]);
                }
            } else {
                console.log(ex);
            }
        }
    }
    //------------------------------------------------------------------------------------
}

module.exports = WebApiCompiler;