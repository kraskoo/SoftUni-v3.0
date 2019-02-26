import React, { Component } from 'react';
import './register.css';

class RegisterForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      email: '',
      password: ''
    };
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(evnt) {
    const $target = evnt.target;
    const name = $target.name;
    const value = $target.value;
    this.setState({
      [name]: value
    });
  }

  handleSubmit(evnt) {
    evnt.preventDefault();
    this.props.registerUser(this.state);
  }

  render() {
    return (
      <div className="Register">
        <h1>Sign Up</h1>
        <form onSubmit={this.handleSubmit}>
          <label htmlFor="usernameReg">Username</label>
          <input type="text" id="usernameReg" name="username" value={this.state.username} onChange={this.handleChange} />
          <label htmlFor="emailReg">Email</label>
          <input type="text" id="emailReg" name="email" value={this.state.email} onChange={this.handleChange} />
          <label htmlFor="passwordReg">Password</label>
          <input type="password" id="passwordReg" name="password" value={this.state.password} onChange={this.handleChange} />
          <input type="submit" value="Sign Up" />
        </form>
      </div>
    )
  }
}

export default RegisterForm;