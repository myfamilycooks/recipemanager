var webpack = require('webpack');
var path = require('path');
var combineLoaders = require('webpack-combine-loaders');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var BUILD_DIR = path.resolve(__dirname, 'wwwroot/lib');
var APP_DIR = path.resolve(__dirname, 'app');
var helpers = require('./helpers');
var config = {
    entry: {
        app: [ 'babel-polyfill', APP_DIR + '/index.jsx'],
    },
    output: {
        path: BUILD_DIR,
        filename: 'app.bundle.js'
    },
    module: {
        loaders: [
            {
                test: /\.jsx$/,
                exclude: [/node_modules/, /scripts/, /lib/],
                include: APP_DIR,
                loader: 'babel-loader',
                query:
                {
                    presets: ['react'],
                    plugins: [
                        'transform-regenerator'
                    ]
                }
            },
            { test: /\.css$/, loader: ExtractTextPlugin.extract({ fallbackLoader: 'style-loader', loader: 'css-loader' }) },
            { test: /\.css$/, include: helpers.root('src', 'app'), loader: 'raw' },
            { test: /\.scss$/, loaders: ['style-loader', 'css-loader', 'sass-loader'] }
        ]
    },
    resolve: {
        extensions: ['.js', '.jsx'],
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin({
            name: 'vendor',
            filename: 'vendor.bundle.js',
            minChunks: ({ resource }) => /node_modules/.test(resource),
        }),
        new ExtractTextPlugin({ filename: 'css/[name].css', disable: false, allChunks: true }),
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery",
            "window.jQuery": "jquery"
        })
    ]
};

module.exports = config;