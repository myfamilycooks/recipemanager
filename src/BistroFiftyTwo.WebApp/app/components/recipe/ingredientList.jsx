import React from 'react';
import _ from 'lodash';

const IngredientList = ({list}) => {
    const renderIngredient = (ingredient) => {
        return (
            <li key={ingredient.id}>
                {ingredient.quantity} {ingredient.units} {ingredient.ingredient} 
            </li>
        )
    };

    const ingredients = _.sortBy(list, o => o.ordinal );
    return(
        <div>
        <h6>Ingredients</h6>
        <ul>
            {ingredients.map(renderIngredient)}
        </ul>
        </div>
    );
};

IngredientList.propTypes = {
    list: React.PropTypes.arrayOf(React.PropTypes.object)
}

export default IngredientList;