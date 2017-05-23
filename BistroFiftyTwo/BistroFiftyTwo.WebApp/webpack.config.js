module.exports = {
    entry: [
        './app/index.jsx'
    ],
    output: {
        path: __dirname + '/wwwroot/lib',
        publicPath: '/',
        filename: 'bundle.js'
    },
    module: {
        loaders: [{
            exclude: /node_modules/,
            loader: 'babel',
            query: {
                presets: ['react', 'es2015', 'stage-1']
            }
        }]
    },
    resolve: {
        extensions: ['', '.js', '.jsx']
    }
};