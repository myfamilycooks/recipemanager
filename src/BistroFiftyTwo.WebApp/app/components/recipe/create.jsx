import React from 'react';
import { connect } from 'react-redux';
import * as actions from '../../actions'; 

class CreateRecipe extends React.Component {
  componentWillMount() {
    this.props.logOutUser();
  }

  render() {
    return (<div>
     Big Form Here To Create Recipe
      
    </div>)
  }
}

export default connect(null, actions)(CreateRecipe);
