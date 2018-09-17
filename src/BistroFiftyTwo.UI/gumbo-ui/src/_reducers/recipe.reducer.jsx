import { recipeConstants } from "../_constants";

export function recipes(state = {}, action) {
  switch (action.type) {
    case recipeConstants.FEATURED_REQUEST:
      return {
        loading: true
      };
    case recipeConstants.FEATURED_SUCCESS:
      return {
        featured: action.featured
      };

    default:
      return state;
  }
}
