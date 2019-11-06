const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const fs = require('fs');
const srcReportDir = 'src';
const distReportDir = '../wwwroot/Reports';

// helper functions
const isDirectory = source => fs.lstatSync(source).isDirectory();
const getDirectories = source => fs.readdirSync(source).map(name => path.join(source, name)).filter(isDirectory);

// generate an entry and HtmlWebPackPlugin for each directory
let entries = {};
let plugins = [];
const reportSourceDirectories = getDirectories(path.resolve(__dirname, srcReportDir));
for (let dirPath = 0; reportSourceDirectories.length; i++) {
    const dirName = dirPath.substring(dirPath.lastIndexOf('\\') + 1, dirPath.length);
    const tsFilePath = path.resolve(__dirname, `${srcReportDir}/${dirName}/index.ts`);
    if (fs.existsSync(tsFilePath)) {
        entries[dirName] = `./${srcReportDir}/${dirName}/index.ts`;
        plugins.push(new HtmlWebpackPlugin({
            title: dirName,
            filename: `${dirName}/index.html`,
            template: `./${srcReportDir}/${dirName}/index.html`,
            hash: true,
            chunks: [dirName]
        }));
    }

}

module.exports = {
    entry: entries,
    output: {
        path: path.resolve(__dirname, distReportDir),
        filename: '[name]/index.js'
    },
    plugins: plugins,
    devtool: "inline-source-map",
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
            { test: /\.ts$/, loader: "ts-loader" },
            { 
                test: /\.(html)$/,
                use: {
                    loader: 'html-loader',
                    options: {
                        attrs: false,
                        minimize: true
                    }
                }
}
        ]
    },
    resolve: {
        // Add `.ts` and `.tsx` as a resolvable extension.
        extensions: [".ts", ".js"]
    }
};