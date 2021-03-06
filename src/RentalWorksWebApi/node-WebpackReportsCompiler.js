const webpack = require('webpack');
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const fs = require('fs');
const srcReportDir = 'WebpackReports/src/Reports';
const distReportDir = 'wwwroot/Reports';

class WebpackReportsCompiler {
    constructor(buildAction, target, buildConfiguration, reports) {
        this.buildAction = buildAction;
        this.target = target;
        this.buildConfiguration = buildConfiguration;
        this.reports = reports;
        console.log('reports: ' + reports);
        let mode = buildConfiguration === 'dev' ? 'development' : 'production';
        let devtool = buildConfiguration === 'dev' ? 'inline-source-map' : 'none';
        // generate an entry and HtmlWebpackPlugin for each directory
        let entries = {};
        let plugins = [];
        process.chdir(__dirname);
        const reportCategoryDirs = this.getDirectories(path.resolve(__dirname, srcReportDir));
        let reportsArray = reports.toLowerCase().split(',');
        for (let reportCategoryDir of reportCategoryDirs) {
            reportCategoryDir = reportCategoryDir.replace(/\\/g, '/'); // convert Windows backslash to forward slash so paths are the same format as Mac/Linux
            const reportCategoryName = reportCategoryDir.substring(reportCategoryDir.lastIndexOf('/') + 1, reportCategoryDir.length);
            const reportDirs = this.getDirectories(reportCategoryDir);
            for (let reportDir of reportDirs) {
                reportDir = reportDir.replace(/\\/g, '/'); // convert Windows backslash to forward slash so paths are the same format as Mac/Linux
                const reportName = reportDir.substring(reportDir.lastIndexOf('/') + 1, reportDir.length);
                if (reportsArray.includes('all') || reportsArray.includes(reportName.toLowerCase())) {
                    const tsFilePath = path.resolve(__dirname, srcReportDir, reportCategoryName, reportName, 'index.ts').replace(/\\/g, '/');
                    console.log(tsFilePath);
                    if (fs.existsSync(tsFilePath)) {
                        console.log('Found Report at:', tsFilePath);
                        entries[reportName] = path.resolve(__dirname, srcReportDir, reportCategoryName, reportName, 'index.ts');
                        plugins.push(new HtmlWebpackPlugin({
                            title: reportName,
                            filename: `${reportName}/index.html`,
                            template: path.resolve(__dirname, srcReportDir, reportCategoryName, reportName, 'index.html'),
                            hash: true,
                            chunks: [reportName]
                        }));
                        plugins.push(new CopyPlugin({
                            patterns: [
                                {
                                    from: path.resolve(__dirname, srcReportDir, reportCategoryName, reportName, 'hbReport.hbs'), 
                                    to: `${reportName}/hbReport.hbs`
                                }
                            ]
                        }));
                    }
                }
            }
        }
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