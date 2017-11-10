import React from 'react';
import _ from 'lodash';

const StepList = ({list}) => {
    const renderInstruction = (instruction) => {

        if(!instruction.instructions)
            return;

        return (
            <li key={instruction.id}>            
                <p>
                    {instruction.instructions}
                </p>
            </li>
        )
    };

    const instructions = _.sortBy(list, o => o.ordinal );
    return(
        <div>
        <h6>Steps</h6>
        <ol>
            {instructions.map(renderInstruction)}
        </ol>
        </div>
    );
};

StepList.propTypes = {
    list: React.PropTypes.arrayOf(React.PropTypes.object)
}

export default StepList;