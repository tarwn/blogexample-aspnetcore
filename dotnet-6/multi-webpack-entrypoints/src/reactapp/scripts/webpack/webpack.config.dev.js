const ReactRefreshWebpackPlugin = require("@pmmmwh/react-refresh-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const config = require("./webpack.config.base");
const buildConfig = require("../env.config");

config.mode = "development";
config.devtool = "eval-cheap-module-source-map";
config.devServer = {
  // ability to override if Visual Studio hosted so we can proxy through visual studio
  client: {
    webSocketURL: `ws://${buildConfig.clientWebSocketHost}:${buildConfig.clientWebSocketPort}/ws`,
  }
};
config.module.rules.push(
  {
    test: /\.css$/,
    use: ["style-loader", "css-loader"]
  }
);

config.plugins.push(new ReactRefreshWebpackPlugin());
config.plugins.push(new HtmlWebpackPlugin({
  chunks: ["index"],
  title: "Local Development",
  template: "./scripts/webpack/templates/index.html"
}));

module.exports = config;
