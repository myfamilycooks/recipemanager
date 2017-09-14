import React from "react";
import { connect } from "react-redux";
import * as actions from "../../actions";

class Editor extends React.Component {
  render() {
    return <div>
      <h1>Create a Recipe</h1>
      </div>;
  }
}

export default connect(null, actions)(Editor);
