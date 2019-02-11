import * as types from "./types";

const getForecast = () => ({
  type: types.GET_FORECAST_STARTED
});

const getForecastSuccess = (response: any) => ({
  type: types.GET_FORECAST_SUCCESS,
  response
});

const getForecastFailure = (error: any) => ({
  type: types.GET_FORECAST_FAILURE,
  error
});

export {
  getForecast,
  getForecastSuccess,
  getForecastFailure
};
