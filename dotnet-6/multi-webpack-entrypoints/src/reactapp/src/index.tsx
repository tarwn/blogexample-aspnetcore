import { render } from "react-dom";

import "../styles/site.scss";
import { App } from "./application/App";

render(
  (
    <App />
  ),
  document.getElementById("root")
);
