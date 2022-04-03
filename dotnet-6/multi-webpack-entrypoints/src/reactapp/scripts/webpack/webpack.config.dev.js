const ReactRefreshWebpackPlugin = require("@pmmmwh/react-refresh-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const config = require("./webpack.config.base");

config.mode = "development";
config.devtool = "eval-cheap-module-source-map";
config.devServer = {
  writeToDisk: true
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
