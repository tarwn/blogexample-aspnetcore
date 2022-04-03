const TerserPlugin = require("terser-webpack-plugin");
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");

const config = require("./webpack.config.base");
config.mode = "production";

// Use `hidden-source-map`, so that the browser will not load it automatically.
// You can still load it manually.
config.devtool = "hidden-source-map";

config.externals = {
  "classnames": "classnames",
  "history": "History",
  "react": "React",
  "react-dom": "ReactDOM"
};

config.optimization = {
  minimizer: [
    new TerserPlugin({
      extractComments: false,
      terserOptions: {
        format: { comments: false }
      }
    }),
    new CssMinimizerPlugin()
  ]
};

config.module.rules.push(
  {
    test: /\.s?css$/,
    use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"]
  }
);

config.plugins.push(
  new MiniCssExtractPlugin({
    filename: "css/[name].css",
    chunkFilename: "[id].chunk.css"
  })
);
config.plugins.push(new HtmlWebpackPlugin({
  chunks: ["index"],
  title: "Loading...",
  template: "./scripts/webpack/templates/index.html"
}));

module.exports = config;
