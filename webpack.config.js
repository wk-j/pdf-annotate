var webpack = require('webpack');
var plugins = [];

module.exports = {
    devtool: 'source-map',
    plugins: plugins,
    entry: './customize.client/index.js',
    output: {
        filename: 'customize.server/PdfAnnotation/wwwroot/index.js',
        library: 'PDFAnnotate',
        libraryTarget: 'umd'
    },
    module: {
        loaders: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: 'babel-loader',
                query: {
                    presets: ['es2015'],
                    plugins: ['add-module-exports']
                }
            }
        ]
    }
};

