//@flow
import React from "react";
import { connect } from "react-redux";
import { recipeActions } from "../_actions";
import FeaturedRecipes from "./featuredRecipes";

type Props = {
  dispatch: Function,
  recipes: Array<Object>
};

class HomePage extends React.Component<Props, State> {
  constructor(props) {
    super(props);
  }
  componentDidMount() {
    this.props.dispatch(recipeActions.getFeatured());
  }

  render() {
    const featured = this.props.recipes.featured || { recipes: [] };

    return (
      <div className="mfc-homepage-main">
        <div className="container">
          <h2>Recipe Manager</h2>
          <div className="mfc-recipes-featured">
            <FeaturedRecipes recipes={featured.recipes} />
            {featured.recipes.map(r => (
              <div key={r.id}>
                <h4>{r.name}</h4>
                <p>{r.description}</p>
              </div>
            ))}
          </div>
        </div>
      </div>
    );
  }
}

function mapStateToProps(state) {
  const { recipes, authentication } = state;
  const { user } = authentication;
  return {
    user,
    recipes
  };
}

const connectedHomePage = connect(mapStateToProps)(HomePage);
export { connectedHomePage as HomePage };
