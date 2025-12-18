module.exports = function() {
  return {
    node: {
      // Prevent webpack from injecting useless setImmediate polyfill because Vue
      // source contains it (although only uses it if it's native)
      setImmediate: false,
      // Prevent webpack from injecting mocks to Node native modules which does
      // not make sense for the client
      dgram: "empty",
      fs: "empty",
      net: "empty",
      tls: "empty",
      child_process: "empty"
    }
  };
};