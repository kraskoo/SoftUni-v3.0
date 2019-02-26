import React, { Component } from 'react';
import './Login.css';

class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      password: ''
    };
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleChange = this.handleChange.bind(this);
  }

  handleSubmit(evnt) {
    evnt.preventDefault();
    this.props.handleLogin(this.state);
  }

  handleChange(evnt) {
    const name = evnt.target.name;
    const value = evnt.target.value;
    this.setState({ [name]: value });
  }

  render() {
    return (
      <div className="Login">
        <h1>Login</h1>
        <form onSubmit={this.handleSubmit}>
          <label htmlFor="usernameLogin">Username</label>
          <input type="text" id="usernameLogin" name="username" placeholder="Ivan Ivanov" value={this.state.username} onChange={this.handleChange} />
          <label htmlFor="passwordLogin">Password</label>
          <input type="password" id="passwordLogin" name="password" placeholder="******" value={this.state.password} onChange={this.handleChange} />
          <input type="submit" value="Login" />
        </form>
      </div>
    );
  }
}

export default Login;