import React, { Component } from "react";
import { IForecast, IForecastProps } from "./types";

import "./ForecastComponent.scss";

export default class ForecastComponent extends Component<IForecastProps> {
  constructor(props: any) {
    super(props);
  }

  public componentDidMount() {
    this.props.getForecasts();
  }

  public renderForecastsTable(forecasts: IForecast[]) {
    const rows = forecasts.map((forecast) => (
      <tr key={forecast.dateFormatted}>
        <td>{forecast.dateFormatted}</td>
        <td>{forecast.temperatureC}</td>
        <td>{forecast.temperatureF}</td>
        <td>{forecast.summary}</td>
      </tr>
    ));

    return (
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {rows}
        </tbody>
      </table>
    );
  }

  public render() {
    const contents = this.props.loading
      ? <p><em>Loading...</em></p>
      : this.renderForecastsTable(this.props.forecasts);

    const error = this.props.loadError != null
      ? <span className="error">{this.props.loadError}</span>
      : <span></span>;

    return (
      <div>
        <h1>Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {error}
        {contents}
      </div>
    );
  }
}
