//@flow
import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { App } from "./App";
import registerServiceWorker from "./registerServiceWorker";
import { store } from "./_helpers";
import "./_include/bootstrap";

render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById("root")
);

registerServiceWorker();
// based on http://jasonwatmore.com/post/2017/09/16/react-redux-user-registration-and-login-tutorial-example
