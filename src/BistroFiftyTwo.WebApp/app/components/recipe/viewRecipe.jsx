import React from "react";
import { connect } from "react-redux";
import * as actions from "../../actions";
import IngredientList from "./ingredientList";
import StepList from "./stepList";

export default class ViewRecipe extends React.Component {
  render() {
    const recipe = this.props.recipe;

    if(!recipe)
        return (<div>loading...</div>);

    return (
    <div>
        <h1>{recipe.title}</h1>
        <div>{recipe.ingredients.length} Ingredients / {recipe.steps.length} Steps 
            <p>
                {recipe.description}
            </p>
        </div>
        <div className="row">
            <div className="col-md-4">
                <IngredientList list={recipe.ingredients} />
            </div>
            <div className="col-md-8">
                <StepList list={recipe.steps} />            
            </div>
        </div>
    </div>
    )  
  }
}
 
