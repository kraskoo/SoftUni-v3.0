import React, { Component, Fragment } from 'react';
import './App.css';
import Article from '../Article/Article';
import RegisterForm from '../RegisterForm/RegisterForm';
import Navigation from '../Navigation/Navigation';
import ComponentWithError from '../ComponentWithError/ComponentWithError';
import SpecialForm from '../SpecialForm/SpecialForm';
import withAlert from '../../hocs/withAlert';
import withErrorNotification from '../../hocs/withErrorNotification';

const ArticleWithAlert = withAlert(Article);
const RegisterFormWithAlert = withAlert(RegisterForm);
const NavigationWithAlert = withAlert(Navigation);
const ComponentWithErrorAndErrorNotification = withErrorNotification(ComponentWithError);

class App extends Component {
  render() {
    return (
      <Fragment>
        <Article />
        <RegisterForm />
        <Navigation />
        <ArticleWithAlert />
        <RegisterFormWithAlert />
        <NavigationWithAlert />
        {/* <ComponentWithErrorAndErrorNotification /> - this supposed to work in production */}
        <SpecialForm header="Login Form" submit="Login">
          <label htmlFor="username">Username:</label>
          <input type="text" name="username" id="username" />
          <label htmlFor="password">Password:</label>
          <input type="password" name="password" id="password" />
        </SpecialForm>
      </Fragment>
    );
  }
}

export default App;