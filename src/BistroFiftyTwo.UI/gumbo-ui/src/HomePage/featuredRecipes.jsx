//@flow
import React from "react";
import RecipeCard from "./recipeCard";

const FeaturedRecipes = (props: { recipes: Array<object> }) => {
  const firstRecipe = props.recipes[0] || {};
  const secondRecipe = props.recipes[1] || {};
  const thirdRecipe = props.recipes[2] || {};

  return (
    <div>
      <div
        className="jumbotron p-3 p-md-5 text-white rounded bg-dark"
        style={{ backgroundImage: `url(${firstRecipe.imageUrl})` }}
      >
        <div className="col-md-6 px-0">
          <h1 className="display-4 font-italic">{firstRecipe.name}</h1>
          <p className="lead my-3">{firstRecipe.shortDescription}</p>
          <p className="lead mb-0">
            <a href="#" className="text-white font-weight-bold">
              Continue reading...
            </a>
          </p>
        </div>
      </div>
      <div className="row mb-2">
        <RecipeCard recipe={secondRecipe} />
        <RecipeCard recipe={thirdRecipe} />
      </div>
    </div>
  );
};

export default FeaturedRecipes;
