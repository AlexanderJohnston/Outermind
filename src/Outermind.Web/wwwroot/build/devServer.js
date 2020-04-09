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
        "/api": "http://localhost:5000"
      },
      quiet: true,
      watchOptions: {
        poll: false
      }
    }
  };
};