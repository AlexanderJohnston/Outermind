let rootPath = require("./rootPath");
let staticPath = require("./staticPath");

module.exports = function(isProduction) {
  return {
    module: {
      rules: [
        ...getBaseLoaders(isProduction),
        ...getNonVueStyleLoaders(isProduction)
      ]
    }
  };
};

function getBaseLoaders(isProduction) {
  return [
    {
      test: /\.vue$/,
      loader: "vue-loader",
      options: {
        loaders: getVueStyleLoaders(isProduction),
        cssSourceMap: true,
        cacheBusting: true,
        transformToRequire: {
          video: ["src", "poster"],
          source: "src",
          img: "src",
          image: "xlink:href"
        }
      }
    },
    {
      test: /\.js$/,
      loader: "babel-loader",
      include: [rootPath("src"), rootPath("node_modules/webpack-dev-server/client")]
    },
    {
      test: /\.(png|jpe?g|gif|svg)(\?.*)?$/,
      loader: "url-loader",
      options: {
        limit: 10000,
        name: staticPath("img/[name].[hash:7].[ext]")
      }
    },
    {
      test: /\.(woff2?|eot|ttf|otf)(\?.*)?$/,
      loader: "url-loader",
      options: {
        limit: 10000,
        name: staticPath("fonts/[name].[hash:7].[ext]")
      }
    }
  ];
}

function getVueStyleLoaders(isProduction) {
  return {
    css: buildLoaders(),
    postcss: buildLoaders(),
    less: buildLoaders("less"),
    sass: buildLoaders("sass", { indentedSyntax: true }),
    scss: buildLoaders("sass"),
    stylus: buildLoaders("stylus"),
    styl: buildLoaders("stylus")
  }

  function buildLoaders(type) {
    let loaders = [
      isProduction ? MiniCssExtractPlugin.loader : "vue-style-loader",
      {
        loader: "css-loader",
        options: {
          sourceMap: true
        }
      },
      {
        loader: "postcss-loader",
        options: {
          sourceMap: true
        }
      }
    ];

    if(type) {
      loaders.push({
        loader: type + "-loader",
        options: {
          sourceMap: true
        }
      });
    }

    return loaders;
  }
}

function getNonVueStyleLoaders(isProduction) {
  let vueLoaders = getVueStyleLoaders(isProduction);
  let nonVueLoaders = [];

  for(let extension in vueLoaders) {
    nonVueLoaders.push({
      test: new RegExp(`\\.${extension}$`),
      use: vueLoaders[extension]
    });
  }

  return nonVueLoaders;
}