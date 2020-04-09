let webpack = require("webpack");
let { CleanWebpackPlugin } = require("clean-webpack-plugin");
let CopyWebpackPlugin = require("copy-webpack-plugin");
let FriendlyErrorsPlugin = require("friendly-errors-webpack-plugin");
let HtmlWebpackPlugin = require("html-webpack-plugin");
let MiniCssExtractPlugin = require("mini-css-extract-plugin");
let OptimizeCSSPlugin = require("optimize-css-assets-webpack-plugin");
let VueLoaderPlugin = require("vue-loader/lib/plugin");

let rootPath = require("./rootPath");
let staticPath = require("./staticPath");

module.exports = function(isProduction) {
  let forMode = isProduction ? forProduction() : forDevelopment();

  return {
    plugins: [
      ...forMode,
      new webpack.DefinePlugin({
        ENVIRONMENT: JSON.stringify(isProduction ? "Production" : "Development")
      }),
      new CleanWebpackPlugin(),
      new CopyWebpackPlugin([{
        from: staticPath(), // From /static
        to: staticPath(),   // To   /dist/static
        ignore: [".*"]
      }]),
      new VueLoaderPlugin()
    ]
  };
};

function forProduction() {
  return [
    new MiniCssExtractPlugin({
      path: staticPath(),
      filename: staticPath("css/[name].[chunkhash].css"),
      chunkFilename: staticPath("css/[id].[chunkhash].css")
    }),
    new HtmlWebpackPlugin({
      filename: rootPath("dist/index.html"),
      template: "index.html",
      inject: true,
      minify: {
        removeComments: true,
        collapseWhitespace: true,
        removeAttributeQuotes: true
      },
      chunksSortMode: "dependency"
    }),
    new webpack.HashedModuleIdsPlugin(),
    new webpack.optimize.ModuleConcatenationPlugin(),
    new OptimizeCSSPlugin({
      cssProcessorOptions: {
        safe: true,
        map: {
          inline: false
        }
      }
    })
  ];
}

function forDevelopment() {
  return [
    new webpack.HotModuleReplacementPlugin(),
    new HtmlWebpackPlugin({
      filename: "index.html",
      template: "index.html",
      inject: true
    }),
    new FriendlyErrorsPlugin({
      compilationSuccessInfo: {
        messages: [
          "HTTP:                    http://localhost:5000/",
          "HTTPS:                   https://localhost:5001/",
          "Webpack (hot reloading): http://localhost:8080/"
        ],
      },
    })
  ];
}