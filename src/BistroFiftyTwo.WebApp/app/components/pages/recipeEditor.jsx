import React from "react";
import { connect } from "react-redux";
import Editor from "../recipe/editor";
import * as actions from "../../actions";

class RecipeEditor extends React.Component {
  render() {
    return (
      <div className="container">
        <div className=" starter-template" style={{ padding: "3rem 1.5rem" }}>
          <span>&nbsp;</span>
          <Editor />
        </div>
      </div>
    )
  }
}

export default connect(null, actions)(RecipeEditor);
