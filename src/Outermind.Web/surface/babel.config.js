
module.exports = {
  presets: [
    '@quasar/babel-preset-app',
    ["@babel/env", {
      "modules": false,
      "targets": {
        "browsers": ["> 1%", "last 2 versions", "not ie <= 8"]
      }
    }],
  ],
  plugins: [
    "@babel/plugin-proposal-class-properties",
    "@babel/plugin-proposal-object-rest-spread",
    "@babel/transform-runtime",
    "transform-vue-jsx",
  ],
}
