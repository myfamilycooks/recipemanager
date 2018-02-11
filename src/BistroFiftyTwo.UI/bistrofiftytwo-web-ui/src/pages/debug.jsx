import React from "react"; 
import { Button } from 'reactstrap';

class Debug extends React.Component {
    constructor(props) {
        super(props);

        this.state = {};
    }

    render() { 
        return <div><Button >Click Me!</Button></div>;
    }
}

export default Debug;