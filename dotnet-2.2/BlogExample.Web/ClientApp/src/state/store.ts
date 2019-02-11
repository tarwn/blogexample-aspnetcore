import { applyMiddleware, combineReducers, createStore } from "redux";
import thunk from "redux-thunk";
import { logger } from "./logger";

import forecast from "../components/forecast/ducks/reducers";

const reducers = {
  ...forecast
};

export default function configureStore(initialState = {}) {
  const rootReducer = combineReducers(reducers);
  return createStore(
    rootReducer,
    initialState,
    applyMiddleware(thunk, logger)
  );
}
