let code = require("./build/code");
let devServer = require("./build/devServer");
let loaders = require("./build/loaders");
let node = require("./build/node");
let plugins = require("./build/plugins");

module.exports = (env, argv) => {
  let isProduction = argv.mode === "production";
  let sections = [code, devServer, loaders, node, plugins];

  let config = {};

  for(let section of sections) {
    Object.assign(config, section(isProduction));
  }

  return config;
};