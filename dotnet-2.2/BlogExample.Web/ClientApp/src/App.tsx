import React, { Component } from "react";
import { Route } from "react-router-dom";

import { Counter } from "./components/counter/Counter";
import ForecastContainer from "./components/forecast/ForecastContainer";
import { Home } from "./components/Home";
import { Layout } from "./components/shared/Layout";

export default class App extends Component {
  // static displayName = App.name;

  public render() {
    return (
      <Layout>
        <Route exact={true} path="/" component={Home} />
        <Route path="/counter" component={Counter} />
        <Route path="/forecast" component={ForecastContainer} />
      </Layout>
    );
  }
}
