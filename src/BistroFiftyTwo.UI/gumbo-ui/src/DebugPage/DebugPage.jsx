import axios from "axios";
import React from "react";
import { connect } from "react-redux";
import config from "../config";

class DebugPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = { response: {}, error: {} };

    this.handleErrors = this.handleErrors.bind(this);
    this.handleResponse = this.handleResponse.bind(this);
  }

  handleErrors(error) {
    this.setState({ error });
    console.log(error);

    return error.response;
  }
  handleResponse(response) {
    this.setState({ response });
    console.log(response);

    if (response.status === 200) return response.data;
    else return Promise.reject(response.statusText);
  }
  componentDidMount() {
    axios
      .post(`${config.apiUrl}/token`, {
        username: "nOtrEaLu$3R",
        password: "N0+R3@lPa$$w0rD"
      })
      .catch(this.handleErrors)
      .then(this.handleResponse)
      .then(user =>
        localStorage.setItem(config.userItem, JSON.stringify(user))
      );
  }

  render() {
    return <div className="col-md-6 col-md-offset-3" />;
  }
}

function mapStateToProps(state) {
  const { users, authentication } = state;
  const { user } = authentication;
  return {
    user,
    users
  };
}

const connectedDebugPage = connect(mapStateToProps)(DebugPage);
export { connectedDebugPage as DebugPage };
