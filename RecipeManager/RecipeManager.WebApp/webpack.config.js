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
    devtool: 'eval-source-map',
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
        
        new webpack.DefinePlugin({ // <-- key to reducing React's size
            'process.env': {
                'NODE_ENV': JSON.stringify('production')
            }
        }),

        new webpack.optimize.UglifyJsPlugin({
            compress: {
                warnings: false,
                screw_ie8: true,
                conditionals: true,
                unused: true,
                comparisons: true,
                sequences: true,
                dead_code: true,
                evaluate: true,
                if_return: true,
                join_vars: true
            },
            output: {
                comments: false
            },
            sourceMap: true
        }),
        new webpack.optimize.AggressiveMergingPlugin(),//Merge chunks 
        new webpack.optimize.OccurrenceOrderPlugin(true),
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