const webpack = require('webpack');
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const fs = require('fs');
const srcQuikScanDir = './';
const distQuikScanDir = '../wwwroot/QuikScan';
const FwFrontEndLibraryUri = `${srcQuikScanDir}Libraries/Fw/`;

class WebpackQuikScanCompiler {
    constructor(buildAction, target, buildConfiguration) {
        this.buildAction = buildAction;
        this.target = target;
        this.buildConfiguration = buildConfiguration;
        let mode = buildConfiguration === 'dev' ? 'development' : 'production';
        let devtool = buildConfiguration === 'dev' ? 'inline-source-map' : 'none';
        // generate an entry and HtmlWebpackPlugin for each directory
        let entries = {};
        let plugins = [];
        let appName = 'QuikScan';
        entries[appName] = `${srcQuikScanDir}index.ts`;
        plugins.push(new HtmlWebpackPlugin({
            title: appName,
            filename: `index.html`,
            template: `${srcQuikScanDir}index.html`,
            hash: true,
            chunks: [appName]
        }));
        plugins.push(new webpack.ProvidePlugin({
            Constants: path.resolve(__dirname, `${srcQuikScanDir}scripts/Constants`),
            Modernizr: path.resolve(__dirname, `${FwFrontEndLibraryUri}scripts/modernizr/modernizr-2.6.2`),
            jQuery: path.resolve(__dirname, `${FwFrontEndLibraryUri}scripts/jquery/jquery-3.3.1`),
            //obj: `${FwFrontEndLibraryUri}scripts/jquery/jquery-noconflict`,
            FwConversion: path.resolve(__dirname, `${FwFrontEndLibraryUri}scripts/jquery/jquery.FwConversion`),
            'jquery.FwPopup': path.resolve(__dirname, `${FwFrontEndLibraryUri}scripts/jquery/jquery.FwPopup`),
            mustache: path.resolve(__dirname, `${FwFrontEndLibraryUri}scripts/mustache/mustache-0.7.0`),
            //obj: `${FwFrontEndLibraryUri}scripts/jquery/jquery.signaturepad`,
            //obj: `${FwFrontEndLibraryUri}scripts/jquery/jquery.color`,
            //obj: `${FwFrontEndLibraryUri}scripts/jquery/jquery.RemoveClassPrefix`,
            //obj: `${FwFrontEndLibraryUri}scripts/jquery/datepicker/bootstrap-datepicker`,
            //obj: `${FwFrontEndLibraryUri}scripts/jquery/inputmask/jquery.inputmask`,
            //obj: `${FwFrontEndLibraryUri}scripts/jquery/noUiSlider/nouislider`,
            //obj: `${FwFrontEndLibraryUri}scripts/jquery/timepicker/jquery.timepicker,


            jQuery: 'jquery',
            'window.jQuery': 'jquery',


            FwApplication: path.resolve(__dirname,`${FwFrontEndLibraryUri}scripts/FwApplication`),
            Program: path.resolve(__dirname,`${srcQuikScanDir}scripts/Program`),
            program: path.resolve(__dirname,`${srcQuikScanDir}scripts/Program`),
            application: path.resolve(__dirname,`${srcQuikScanDir}scripts/Program`)
        }));
        this.compiler = webpack({
            mode: mode,
            entry: entries,
            output: {
                path: path.resolve(__dirname, distQuikScanDir),
                filename: '[name]/index.js'
            },
            plugins: plugins,
            devtool: devtool,
            module: {
                rules: [
                    {
                        test: /\.scss$/,
                        use: [
                            { loader: "style-loader" },
                            { loader: "css-loader", options: { sourceMap: true } },
                            { loader: "sass-loader", options: { sourceMap: true } }
                        ]
                    },
                    { test: /\.hbs$/, loader: "handlebars-loader" },
                    { test: /\.ts$/, loader: "ts-loader" }
                ]
            },
            resolve: {
                // Add `.ts` and `.tsx` as a resolvable extension.
                extensions: [".ts", ".js"]
            }
        });
    }
    //------------------------------------------------------------------------------------
    isDirectory(source) {
        return fs.lstatSync(source).isDirectory();
    }
    //------------------------------------------------------------------------------------
    getDirectories(source) {
        return fs.readdirSync(source).map(name => path.join(source, name)).filter(this.isDirectory);
    }
    //------------------------------------------------------------------------------------
    async build() {
        return new Promise((resolve, reject) => {
            this.compiler.run((err, stats) => {
                // reject if fatal webpack errors (wrong configuration, etc)
                if (err) {
                    let error = err.stack || err;
                    if (err.details) {
                        error += '\n' + err.details;
                    }
                    reject(error);
                    return;
                }

                // reject if compilation errors (missing modules, syntax errors, etc)
                const info = stats.toJson();
                if (stats.hasErrors()) {
                    //console.error(info.errors);
                    reject(info.errors);
                    return;
                }

                // resolve and return the stats object
                resolve(stats);
                return;
            });
        });
    }
    //------------------------------------------------------------------------------------
    async watch() {
        return new Promise((resolve, reject) => {
            this.compiler.watch({}, (err, stats) => {
                // reject if fatal webpack errors (wrong configuration, etc)
                if (err) {
                    let error = err.stack || err;
                    if (err.details) {
                        error += '\n' + err.details;
                    }
                    reject(error);
                }

                // reject if compilation errors (missing modules, syntax errors, etc)
                const info = stats.toJson();
                if (stats.hasErrors()) {
                    //console.error(info.errors);
                    reject(info.errors);
                    return
                }

                // resolve and return the stats object
                resolve(stats);
                return
            });
        });
    }
    //------------------------------------------------------------------------------------
}

module.exports = QuikScanCompiler;