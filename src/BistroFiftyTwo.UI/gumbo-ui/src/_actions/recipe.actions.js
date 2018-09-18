//@flow
import { recipeConstants } from "../_constants";
import { recipeService } from "../_services";

export const recipeActions = {
  getFeatured
};

function getFeatured() {
  return dispatch => {
    dispatch(request());

    recipeService.getFeatured().then(featured => {
      dispatch(success(featured));
    });
  };

  function request() {
    return { type: recipeConstants.FEATURED_REQUEST };
  }
  function success(featured) {
    return { type: recipeConstants.FEATURED_SUCCESS, featured };
  }
}
