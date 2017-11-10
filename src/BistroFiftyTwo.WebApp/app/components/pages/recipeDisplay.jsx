import React from "react"; 
import axios from 'axios';
import ViewRecipe from "../recipe/viewRecipe"; 

export default class RecipeDisplay extends React.Component {
  constructor(){
    super();

    this.state = {recipe: null};
  }
  componentDidMount() {
    const recipeId = this.props.match.params.recipeId;
    const token = JSON.parse(localStorage.getItem('token'));
 
    axios.get(`api/recipe/${recipeId}`, { headers: { Authorization: `Bearer ${token.access_token}`  } })
      .then(res => {
        this.setState({ recipe: res.data});
      });
  }


  render() {
    if(!this.state.recipe)
      return (<div>Loading...</div>);
      
    return (
      <div className="container">
        <div className=" starter-template" style={{ padding: "3rem 1.5rem" }}>
          <span>&nbsp;</span>
          <ViewRecipe recipe={this.state.recipe} />
        </div>
      </div>
    )
  }
}

