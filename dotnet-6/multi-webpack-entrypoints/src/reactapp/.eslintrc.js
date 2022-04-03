module.exports = {
  root: true,
  // only will apply to files outside of src
  extends: [
    "eslint:recommended"
  ],
  env: {
    "node": true,
    "es2021": true
  },
  ignorePatterns: [
    "node_modules",
    "build",
    "dist",
    "public"
  ],
  rules: {
    "quotes": ["error", "double"],
    "semi": ["error", "always"]
  },
  overrides: [{
    files: "./src/*",
    parser: "@typescript-eslint/parser",
    plugins: [
      "@typescript-eslint",
    ],
    extends: [
      "eslint:recommended",
      "plugin:@typescript-eslint/recommended",
    ],
    env: {
      "browser": true,
      "node": true,
      "es2021": true,
      "jest": true
    },
    rules: {
      "quotes": ["error", "double"]
    }
  }]
};
