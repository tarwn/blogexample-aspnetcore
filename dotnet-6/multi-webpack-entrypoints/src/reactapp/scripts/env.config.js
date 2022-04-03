const fs = require("fs");
const path = require("path");

const isProd = process.env.NODE_ENV === "production";
const isHostedThroughVS = (!!process.env.ASPNET_PORT);
const rootPath = fs.realpathSync(process.cwd());
module.exports = {
  isProd,
  rootPath,

  devProtocol: "http",
  devHost: process.env.HOST || (isHostedThroughVS ? "[::1]" : "localhost"),
  devPort: process.env.PORT || 8881,
  clientWebSocketHost: "0.0.0.0",
  clientWebSocketPort: process.env.ASPNET_PORT || process.env.PORT || 8881,

  buildPath: path.resolve(rootPath, "./dist")
};
