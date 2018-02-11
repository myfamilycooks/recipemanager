//@flow

import React from 'react';
import RecipeEditor from '../components/editors/recipeEditor';

class Editor extends React.Component<*,*> {
    constructor(props) {
        super(props);

        this.state = {};
    }

    render(){
        return(<div><RecipeEditor /></div>);
    }
};

export default Editor;