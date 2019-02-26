import React from 'react';
import './login.css';

class LogInForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
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
    this.props.loginUser(this.state);
  }

  render() {
    return (
      <div className="Login">
        <h1>Login</h1>
        <form onSubmit={this.handleSubmit}>
          <label htmlFor="usernameLogin">Usersname</label>
          <input type="text" id="usernameLogin" name="username" value={this.state.username} onChange={this.handleChange} />
          <label htmlFor="passwordLogin">Password</label>
          <input type="password" id="passwordLogin" name="password" value={this.state.password} onChange={this.handleChange} />
          <input type="submit" value="Login" />
        </form>
      </div>
    )
  }
}

export default LogInForm;