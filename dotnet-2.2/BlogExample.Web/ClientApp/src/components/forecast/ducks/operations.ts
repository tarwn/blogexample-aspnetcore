import axios from "axios";

import * as actions from "./actions";

const getForecast = () => {
  return (dispatch: any) => {
    dispatch(actions.getForecast());

    axios.get("/api/SampleData/WeatherForecasts")
      .then((response: any) => {
        dispatch(actions.getForecastSuccess(response.data));
      })
      .catch((err: Error) => {
        console.log(err);
        dispatch(actions.getForecastFailure(err));
      });
  };
};

export {
  getForecast
};
