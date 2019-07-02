const webpack = require('webpack');
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const fs = require('fs');
const srcReportDir = 'WebpackReports/src';
const distReportDir = 'wwwroot/Reports';

class WebpackReportsCompiler {
    constructor(buildMode) {
        let mode = buildMode === 'dev' ? 'development' : 'production';
        let devtool = buildMode === 'dev' ? 'inline-source-map' : 'none';
        // generate an entry and HtmlWebpackPlugin for each directory
        let entries = {};
        let plugins = [];
        const reportSourceDirectories = this.getDirectories(path.resolve(__dirname, srcReportDir));
        reportSourceDirectories.forEach((dirPath, index, array) => {
            dirPath = dirPath.replace(/\\/g, '/'); // convert Windows backslash to forward slash so paths are the same format as Mac/Linux
            const dirName = dirPath.substring(dirPath.lastIndexOf('/') + 1, dirPath.length);
            const tsFilePath = path.resolve(__dirname, `${srcReportDir}/${dirName}/index.ts`).replace(/\\/g, '/');
            if (fs.existsSync(tsFilePath)) {
                console.log('Found Report at:', tsFilePath);
                entries[dirName] = `./${srcReportDir}/${dirName}/index.ts`;
                plugins.push(new HtmlWebpackPlugin({
                    title: dirName,
                    filename: `${dirName}/index.html`,
                    template: `./${srcReportDir}/${dirName}/index.html`,
                    hash: true,
                    chunks: [dirName]
                }));
            }
        });
        this.compiler = webpack({
            mode: mode,
            entry: entries,
            output: {
                path: path.resolve(__dirname, distReportDir),
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

module.exports = WebpackReportsCompiler;