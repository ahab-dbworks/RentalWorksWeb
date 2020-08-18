const util = require('util');
var spawn = require('child-process-promise').spawn;
const rmfr = require('rmfr');
const fs = require('fs-extra');
const WebpackReportsCompiler = require('./node-WebpackReportsCompiler');
//const WebpackQuikScanCompiler = require('./node-WebpackQuikScanCompiler');
const UNSUPPORTED_CONFIGURATION = 'Unsupported configuration.';
const path = require('path');
var exec = require('child_process').exec;

class WebApiCompiler {
    static get TARGET_ALL() { return 'all'; };
    static get TARGET_API() { return 'api' };
    static get TARGET_WEB() { return 'web' };
    static get TARGET_REPORTS() { return 'reports' };
    static get TARGET_QUIKSCAN() { return 'quikscan' };
    static get BUILD_CONFIGURATION_DEVELOPMENT() { return 'dev'; };
    static get BUILD_CONFIGURATION_PRODUCTION() { return 'prod'; };
    static get BUILD_ACTION_BUILD() { return 'build'; };
    static get BUILD_ACTION_RUN() { return 'run'; };
    static get BUILD_ACTION_WATCH() { return 'watch'; };
    //------------------------------------------------------------------------------------
    constructor(buildAction, target, buildMode, reports) {
        this.buildAction = buildAction;
        this.target = target;
        this.buildConfiguration = buildMode;
        this.dotnetConfiguration = this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT ? "Debug" : "Release";
        this.reports = reports;
        this.appSolutionDir = path.resolve(__dirname, '../../');
    }
    //------------------------------------------------------------------------------------
    async rmfr_downloads() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Deleting: wwwroot/temp/downloads');
        await rmfr('wwwroot/temp/downloads');
    }
    //------------------------------------------------------------------------------------
    async rmfr_web() {
        console.log('//------------------------------------------------------------------------------------');
        const webDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/rentalworks');
        console.log(`Deleting: ${webDir}`);
        await rmfr(webDir);
    }
    //------------------------------------------------------------------------------------
    async rmfr_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Deleting: wwwroot/Reports');
        await rmfr('wwwroot/Reports');
    }
    //------------------------------------------------------------------------------------
    async rmfr_quikscan() {
        console.log('//------------------------------------------------------------------------------------');
        const quikscanDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/quikscan');
        console.log(`Deleting: ${quikscanDir}`);
        await rmfr(quikscanDir);
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
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: ../../lib/Fw/src/FwStandard/bin/Debug');
            await rmfr('../../lib/Fw/src/FwStandard/bin/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: ../../lib/Fw/src/FwStandard/bin/Release');
            await rmfr('../../lib/Fw/src/FwStandard/bin/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: ../../lib/Fw/src/FwStandard/obj/Debug');
            await rmfr('../../lib/Fw/src/FwStandard/obj/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: ../../lib/Fw/src/FwStandard/obj/Release');
            await rmfr('../../lib/Fw/src/FwStandard/obj/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: ../../lib/Fw/src/FwCore/bin/Debug');
            await rmfr('../../lib/Fw/src/FwCore/bin/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: ../../lib/Fw/src/FwCore/bin/Release');
            await rmfr('../../lib/Fw/src/FwCore/bin/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: ../../lib/Fw/src/FwCore/obj/Debug');
            await rmfr('../../lib/Fw/src/FwCore/obj/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: ../../lib/Fw/src/FwCore/obj/Release');
            await rmfr('../../lib/Fw/src/FwCore/obj/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: bin/Debug');
            await rmfr('bin/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: bin/Release');
            await rmfr('bin/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: obj/Release');
            await rmfr('obj/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: obj/Release');
            await rmfr('obj/Release');
        }
    }
    //------------------------------------------------------------------------------------
    async build_webpack_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Building Webpack Reports');
        console.log('//------------------------------------------------------------------------------------');
        await this.rmfr_reports();
        let compiler = new WebpackReportsCompiler(this.buildAction, this.target, this.buildConfiguration, this.reports);
        const stats = await compiler.build();
        console.log(stats.toString({ colors: true }));
    }
    //------------------------------------------------------------------------------------
    async watch_webpack_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Watching Webpack Reports');
        console.log('//------------------------------------------------------------------------------------');
        await this.rmfr_reports();
        let compiler = new WebpackReportsCompiler(this.buildAction, this.target, this.buildConfiguration, this.reports);
        const stats = await compiler.watch();
        console.log(stats.toString({ colors: true }));
    }
    //------------------------------------------------------------------------------------
    async build_web() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Building Web');
        console.log('//------------------------------------------------------------------------------------');

        const jsAppBuilderConfigFile = path.resolve(this.appSolutionDir, 'src/RentalWorksWeb/JSAppBuilder.config');
        const versionFilePath = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/version.txt');
        const version = (await fs.readFile(versionFilePath, 'utf8')).trim();
        const srcDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWeb');
        const destDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/rentalworks');
        let publish = false;
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            publish = false;
        }
        else if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            publish = true;
            await this.rmfr_web();
            const webOutputThemeDir = path.resolve(destDir, 'theme');
            const webOutputLibrariesDir = path.resolve(destDir, 'libraries');
            await fs.mkdir(destDir);
            await fs.mkdir(webOutputThemeDir);
            await fs.mkdir(webOutputLibrariesDir);

            await fs.copy(`${srcDir}/theme/audio`, `${destDir}/theme/audio`);
            await fs.copy(`${srcDir}/theme/fwaudio`, `${destDir}/theme/fwaudio`);
            await fs.copy(`${srcDir}/theme/fwcursors`, `${destDir}/theme/fwcursors`);
            await fs.copy(`${srcDir}/theme/fwfonts`, `${destDir}/theme/fwfonts`);
            await fs.copy(`${srcDir}/theme/fwimages`, `${destDir}/theme/fwimages`);
            await fs.copy(`${srcDir}/theme/images`, `${destDir}/theme/images`);
            //await fs.copy(`${srcDir}/index.htm`, `${destDir}/index.htm`);
            //await fs.copy(`${srcDir}/index.js`, `${destDir}/index.js`);
            await fs.copy(`${srcDir}/ApplicationConfig.sample.js`, `${destDir}/ApplicationConfig.sample.js`);
            await fs.copy(`${srcDir}/web.config`, `${destDir}/web.config`);
            await fs.copy(`./version.txt`, `${destDir}/version.txt`);
        }
        console.log('//------------------------------------------------------------------------------------');
        console.log(`Building RentalWorksWeb TypeScript...`);
        await spawn('npx', ['tsc', '--build', path.resolve(this.appSolutionDir, 'src/RentalWorksWeb/tsconfig.json')], { stdio: 'inherit' });
        console.log(`Finished RentalWorksWeb TypeScript`);
        console.log('//------------------------------------------------------------------------------------');
        console.log(`Running JSAppBuilder for RentalWorksWeb...`);
        await spawn(`${this.appSolutionDir}\\lib\\Fw\\build\\JSAppBuilder\\JSAppBuilder.exe`, ['-ConfigFilePath', jsAppBuilderConfigFile, '-SolutionDir', this.appSolutionDir, '-Version', version, '-UpdateSchema', 'false', '-Publish', publish, '-AttachDebugger', 'false'], { stdio: 'inherit' });
        console.log(`Finished running JSAppBuilder for RentalWorksWeb`);
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            console.log('//------------------------------------------------------------------------------------');
            console.log('Fixing urls on index page...')
            const pathIndexFile = `${srcDir}/index.htm`;
            let fileText = await fs.readFile(pathIndexFile, 'utf8');
            fileText = fileText.replace(/\[appbaseurl\]/g, '/webdev/');
            await fs.writeFile(pathIndexFile, fileText);
        }
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            console.log('//------------------------------------------------------------------------------------');
            console.log('Fixing urls on index page...')
            const pathIndexFile = `${destDir}/index.htm`;
            let fileText = await fs.readFile(pathIndexFile, 'utf8');
            fileText = fileText.replace(/\[appbaseurl\]/g, './');
            fileText = fileText.replace(/\[appvirtualdirectory\]/g, '');
            await fs.writeFile(pathIndexFile, fileText);
            console.log('//------------------------------------------------------------------------------------');
            const pathCssDebug = path.resolve(destDir, `theme/style*-${version}.debug.css`);
            console.log(`Deleting: ${pathCssDebug}`);
            await rmfr(pathCssDebug, { glob: true });
            console.log('//------------------------------------------------------------------------------------');
            const pathScriptDebug = path.resolve(destDir, `script*-${version}.debug.js`);
            console.log(`Deleting: ${pathScriptDebug}`);
            await rmfr(pathScriptDebug, { glob: true });
            console.log('//------------------------------------------------------------------------------------');
        }
    }
    //------------------------------------------------------------------------------------
    async watch_web() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Watch has not been implemented for Web');
        console.log('//------------------------------------------------------------------------------------');
    }
    //------------------------------------------------------------------------------------
    async build_quikscan() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Building QuikScan');
        console.log('//------------------------------------------------------------------------------------');

        const jsAppBuilderConfigFile = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/QuikScan/JSAppBuilder.config');
        const versionFilePath = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/version.txt');
        const version = (await fs.readFile(versionFilePath, 'utf8')).trim();
        const srcDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/QuikScan');
        const destDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/quikScan');
        let publish = false;
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            publish = false;
            await fs.copy(path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/version.txt'), path.resolve(srcDir, 'version.txt'));
            await fs.copy(path.resolve(srcDir, 'theme'), path.resolve(destDir, 'theme'));
            await fs.copy(path.resolve(srcDir, 'views'), path.resolve(destDir, 'views'));
        }
        else if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            publish = true;
            await this.rmfr_quikscan();
            const quikScanOutputThemeDir = path.resolve(destDir, 'theme');
            await fs.mkdir(destDir);
            await fs.mkdir(quikScanOutputThemeDir);

            await fs.copy(`${srcDir}/theme/fwaudio`, `${destDir}/theme/fwaudio`);
            await fs.copy(`${srcDir}/theme/fwcursors`, `${destDir}/theme/fwcursors`);
            await fs.copy(`${srcDir}/theme/fwfonts`, `${destDir}/theme/fwfonts`);
            await fs.copy(`${srcDir}/theme/fwimages`, `${destDir}/theme/fwimages`);
            await fs.copy(`${srcDir}/theme/images`, `${destDir}/theme/images`);

            await fs.copy(`${srcDir}/index.htm`, `${destDir}/index.htm`);
            await fs.copy(`${srcDir}/index.js`, `${destDir}/index.js`);
            await fs.copy(`${srcDir}/web.config`, `${destDir}/web.config`);
            //await fs.copy(`${srcDir}/ApplicationConfig.js`, `${destDir}/ApplicationConfig.js`);
            await fs.copy(`${srcDir}/ApplicationConfig.sample.js`, `${destDir}/ApplicationConfig.sample.js`);
            await fs.copy(`./version.txt`, `${destDir}/version.txt`);
        }
        console.log('//------------------------------------------------------------------------------------');
        console.log(`Building QuikScan TypeScript...`);
        await spawn('npx', ['tsc', '--build', './QuikScan/tsconfig.json'], { stdio: 'inherit' });
        console.log(`Finished QuikScan TypeScript`);
        console.log('//------------------------------------------------------------------------------------');
        console.log(`Running JSAppBuilder for QuikScan...`);
        await spawn(`${this.appSolutionDir}\\lib\\Fw\\build\\JSAppBuilder\\JSAppBuilder.exe`, ['-ConfigFilePath', jsAppBuilderConfigFile, '-SolutionDir', this.appSolutionDir, '-Version', version, '-UpdateSchema', 'false', '-Publish', publish, '-AttachDebugger', 'false'], { stdio: 'inherit' });
        console.log(`Finished running JSAppBuilder for QuikScan`);
        console.log('//------------------------------------------------------------------------------------');
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            const pathCssDebug = path.resolve(destDir, `theme/style*-${version}.debug.css`);
            console.log(`Deleting: ${pathCssDebug}`);
            await rmfr(pathCssDebug, { glob: true });
            //await fs.unlink(pathCssDebug, function (error) { if (error) { throw error; } console.log(`Deleted ${pathCssDebug}`); });
            console.log('//------------------------------------------------------------------------------------');
            const pathScriptDebug = path.resolve(destDir, `script*-${version}.debug.js`);
            console.log(`Deleting: ${pathScriptDebug}`);
            await rmfr(pathScriptDebug, { glob: true });
            //await fs.unlink(pathScriptDebug, function (error) { if (error) { throw error; } console.log(`Deleted ${pathScriptDebug}`); });
            console.log('//------------------------------------------------------------------------------------');
        }

        // build project with webpack
        //let compiler = new WebpackQuikScanCompiler(this.buildAction, this.target, this.buildConfiguration);
        //const stats = await compiler.build();
        //console.log(stats.toString({ colors: true }));
    }
    //------------------------------------------------------------------------------------
    async watch_quikscan() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Watch has not been implemented for QuikScan');
        console.log('//------------------------------------------------------------------------------------');
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
        const appsSrcDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps');
        const appsDestDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWebApi/apps');
        const webSrcDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWebApi/apps/rentalworks');
        const webDestDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWeb');
        console.log('//------------------------------------------------------------------------------------');
        console.log(`dotnet publish -o ../../build/RentalWorksWebApi WebApi.csproj --configuration ${this.dotnetConfiguration} --self-contained -r win-x64`);
        console.log('//------------------------------------------------------------------------------------');
        await spawn('dotnet', ['publish', '-o', '../../build/RentalWorksWebApi', 'WebApi.csproj', '--configuration', this.dotnetConfiguration, '--self-contained', '-r', 'win-x64'], { stdio: 'inherit' });
        console.log('Deleting: appsettings.json');
        await fs.unlink('../../build/RentalWorksWebApi/appsettings.json', function (error) { if (error) { throw error; } console.log('Deleted appsettings.json'); });
        console.log('//------------------------------------------------------------------------------------');
        await fs.mkdirSync('app');
        console.log(`Deploying apps folder from: "${appsSrcDir}" to "${appsDestDir}"`);
        await fs.copy(appsSrcDir, appsDestDir);
        console.log(`Deploying legacy RentalWorksWeb folder from: "${webSrcDir}" to "${webDestDir}"`);
        await fs.copy(webSrcDir, webDestDir);
        console.log('//------------------------------------------------------------------------------------');
        console.log('Adding: wwwroot/temp/downloads');
        await process.chdir('../../build/RentalWorksWebApi/wwwroot');
        await fs.mkdirSync('temp');
        await process.chdir('temp');
        await fs.mkdirSync('downloads');
        await process.chdir('temp');
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
    async build() {
        try {
            if (this.target === WebApiCompiler.TARGET_ALL) {
                if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
                    if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.build_web();
                        await this.build_quikscan();
                        await this.build_webpack_reports();
                        await this.dotnet_build();
                    } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_RUN) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.build_web();
                        await this.build_quikscan();
                        await this.build_webpack_reports();
                        await this.dotnet_run();
                    } else {
                        throw UNSUPPORTED_CONFIGURATION;
                    }
                } else if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                    await this.clean_api();
                    await this.rmfr_downloads();
                    await this.rmfr_publishfolder();
                    await this.npm_i();
                    await this.build_web();
                    await this.build_quikscan();
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
                    await this.build_webpack_reports();
                } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_WATCH) {
                    await this.npm_i();
                    await this.watch_webpack_reports();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            } else if (this.target === WebApiCompiler.TARGET_WEB) {
                if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.build_web();
                } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_WATCH) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.watch_web();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            } else if (this.target === WebApiCompiler.TARGET_QUIKSCAN) {
                if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.build_quikscan();
                } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_WATCH) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.watch_quikscan();
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