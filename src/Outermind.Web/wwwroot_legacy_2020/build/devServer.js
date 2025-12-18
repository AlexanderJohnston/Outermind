let staticPath = require("./staticPath");

module.exports = function(isProduction) {
  return isProduction ? {} : {
    devServer: {
      clientLogLevel: "warning",
      historyApiFallback: {
        rewrites: [{
          from: /.*/,
          to: staticPath("index.html")
        }],
      },
      hot: true,
      contentBase: false,
      compress: true,
      host: "localhost",
      port: "8080",
      overlay: {
        warnings: false,
        errors: true
      },
      publicPath: "/",
      proxy: {
        '/': 'http://localhost:5000',
        "/api": "http://localhost:5000",
        '/hubs': {
          target: "http://localhost:5000",
          ws: true
        },
        '/sockjs-node': "http://localhost:5000"
      },
      quiet: true,
      watchOptions: {
        poll: false
      }
    }
  };
};