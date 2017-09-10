import React from 'react';
import { connect } from 'react-redux';
import * as actions from '../../actions'; 

class Tasks extends React.Component {


  render() {
    return (<div className="card-deck">

    <div className="card">
  <div className="card-header">
    Create Recipe
  </div>
  <div className="card-body">
    <h4 className="card-title">Use the Recipe Builder</h4>
    <p className="card-text">Create a new recipe or make a new version of an existing one.</p>
    <a href="#" className="btn btn-primary">Launch Recipe Builder</a>
  </div>
</div>
         <div className="card">
  <div className="card-header">
    My Recipes
  </div>
  <div className="card-body">
    <h4 className="card-title">View Your Recipes</h4>
    <p className="card-text">Browse your recipes by category.  Manage Recipe your recipes.</p>
    <a href="#" className="btn btn-primary">Browse My Recipes</a>
  </div>
</div>

<div className="card">
  <div className="card-header">
    Advanced Search
  </div>
  <div className="card-body">
    <h4 className="card-title">Search Recipes</h4>
    <p className="card-text">Search Recipes, both yours and other public recipes.  Use the advanced search tools.</p>
    <a href="#" className="btn btn-primary">Search Recipes</a>
  </div>
</div>
    </div>)
  }
}

export default connect(null, actions)(Tasks);
