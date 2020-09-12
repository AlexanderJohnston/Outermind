let rootPath = require("./rootPath");

module.exports = function(isProduction) {
  return {
    entry: {
      app: "./src/index.js"
    },
    output: {
      path: rootPath("dist"),
      filename: "[name].js",
      publicPath: "/"
    },
    resolve: {
      extensions: [".js", ".vue", ".json"],
      alias: {
        "vue$": "vue/dist/vue.esm.js"
      }
    },
    devtool: isProduction ?
      "cheap-module-eval-source-map" :
      "#source-map"
  };
};