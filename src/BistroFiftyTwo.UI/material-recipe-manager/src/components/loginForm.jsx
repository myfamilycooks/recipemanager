import React from 'react';
import { Field, reduxForm } from 'redux-form';
import TextField from 'material-ui/TextField';

const renderTextField = (
    {input, label, meta: { touched, error}, ...custom}
) => (
    <TextField 
        hintText={label}
        floatingLabelText={label}
        errorText={touched && error}
        {...input}
        {...custom}
        />
);

const LoginForm = props => {
    const { handleSubmit, pristine, reset, submitting} = props;
    return (
        <div className="loginForm">
        <form onSubmit={handleSubmit}>
            <div>
                <Field 
                    name="email"
                    component={renderTextField}
                    label="Email"
                />
            </div>
            <div>
                <Field name="password" component={renderTextField} label="password" />
            </div>
        </form>
        </div>
    );
};

export default reduxForm({ 
    form: 'LoginForm'
})(LoginForm);