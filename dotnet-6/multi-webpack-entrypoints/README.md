This is an example ASP.Net + React 18 application to show how we can support sharing of CSS assets between the two, with both server-side routes and a SPA default, and CSS assets that are refreshed via HMR in both the React and ASP.Net Razor pages.

# Other Fixes

This repo also includes the following fixes for common ASP.net + UseSpaServices issues:

* Using `UseReactDevelopmentServer` with WebPack 5 + custom configs
* Solving the 2-4s loading latency that pops up between ASP.Net and React occasionally (ipv6 vs ipv4 issue)
* Stop reporting output from Webpack as "fail" errors in ASP.Net console output

And possibly a couple others.

# Also useful for...

Once you have multiple WebPack entry points that share assets live over to ASP.Net, then it also becomes very easy to:

1. Use CopyWebpackPlugin to copy assets from `node_modules` to `dist` and reference them from ASP.Net (manage bootstrap, image libraries, fonts, etc. in npm in one spot, use it in both React + ASP.Net)
2. Embed one-of React components in an entry point that can be used by ASP.Net pages to use small React components 
3. Create complex wrappers for external scripts for use in both React and ASP.Net from a single shared component or script file

