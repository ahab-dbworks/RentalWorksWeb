const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const fs = require('fs');
const CleanWebpackPlugin = require('clean-webpack-plugin')
const webpack = require('webpack')
const srcReportDir = 'src';
const distReportDir = '../wwwroot/Reports';

// helper functions
const isDirectory = source => fs.lstatSync(source).isDirectory();
const getDirectories = source => fs.readdirSync(source).map(name => path.join(source, name)).filter(isDirectory);

// generate an entry and HtmlWebPackPlugin for each directory
let entries = {};
let plugins = [
    new CleanWebpackPlugin([path.resolve(__dirname, `${distReportDir}/*`)], {})
];
const reportSourceDirectories = getDirectories(path.resolve(__dirname, srcReportDir));
reportSourceDirectories.forEach((dirPath, index, array) => {
    const dirName = dirPath.substring(dirPath.lastIndexOf('\\') + 1, dirPath.length);
    entries[dirName] = `./${srcReportDir}/${dirName}/index.ts`;
    plugins.push(new HtmlWebpackPlugin({
        title: dirName,
        filename: `${dirName}/index.html`,
        template: `./${srcReportDir}/${dirName}/index.html`,
        hash: true
    }));
});
plugins.push(new webpack.HotModuleReplacementPlugin());

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
            { test: /\.tsx?$/, loader: "ts-loader" }
        ]
    },
    resolve: {
        // Add `.ts` and `.tsx` as a resolvable extension.
        extensions: [".ts", ".tsx", ".js"]
      },
    devServer: {
        contentBase: `./${distReportDir}`,
        //compress: true,
        port: 9000,
        hot: true
    },
};