import React, { Component } from 'react';
import './Register.css';

class Register extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      email: '',
      password: ''
    };
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleChange = this.handleChange.bind(this);
  }

  handleSubmit(evnt) {
    evnt.preventDefault();
    this.props.handleRegister(this.state);
  }

  handleChange(evnt) {
    const name = evnt.target.name;
    const value = evnt.target.value;
    this.setState({ [name]: value });
  }

  render() {
    return (
      <div className="Register">
        <h1>Register</h1>
        <form onSubmit={this.handleSubmit}>
          <label htmlFor="username">Username</label>
          <input type="text" id="username" name="username" placeholder="Ivan Ivanov" onChange={this.handleChange} />
          <label htmlFor="email">Email</label>
          <input type="text" id="email" name="email" placeholder="ivan@gmail.com" onChange={this.handleChange} />
          <label htmlFor="password">Password</label>
          <input type="password" id="password" name="password" placeholder="******" onChange={this.handleChange} />
          <input type="submit" value="REGISTER" />
        </form>
      </div>
    );
  }
}

export default Register;