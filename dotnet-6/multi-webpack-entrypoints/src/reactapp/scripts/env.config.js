const fs = require("fs");
const path = require("path");

const isProd = process.env.NODE_ENV === "production";
const rootPath = fs.realpathSync(process.cwd());
module.exports = {
  isProd,
  rootPath,

  devProtocol: "http",
  devHost: process.env.HOST || "localhost",
  devPort: process.env.PORT || 8881,
  buildPath: path.resolve(rootPath, "./dist")
};
