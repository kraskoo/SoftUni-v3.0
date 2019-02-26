import React, { Component } from 'react';
import './App.css';
import AppHeader from "./App/AppHeader";
import AppContent from "./App/AppContent";
import AppFooter from "./App/AppFooter";

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      user: null,
      games: [],
      hasFetched: false,
      loginForm: false,
    };
  }

  registerUser(user) {
    this.fetchUser(user);
  }

  loginUser(user) {
    this.fetchUser(user, false);
  }

  fetchUser(user, signUp = true) {
    const fetchAddress = `http://localhost:9999/auth/sign${signUp ? 'up' : 'in'}`;
    fetch(fetchAddress, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(user)
    }).then(response => response.json()).then(body => {
      if (body.errors) {
        body.errors.forEach(error => {
          console.error(error);
        });
      }
      else {
        sessionStorage.setItem('userId', body.userId);
        sessionStorage.setItem('username', body.username);
        this.setState({ user: body.username });
      }
    });
  }

  logout(event) {
    event.preventDefault();
    sessionStorage.removeItem('userId');
    sessionStorage.removeItem('username');
    this.setState({ user: null });
  }

  componentWillMount() {
    const username = sessionStorage.getItem('username');
    this.setState({ user: username ? username : null });
    fetch('http://localhost:9999/feed/games').then(response => response.json()).then(body => {
      this.setState({
        hasFetched: true,
        games: body.games
      });
    });
  }

  createGame(data) {
    fetch('http://localhost:9999/feed/game/create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    }).then(response => response.json()).then(body => {
      if (body.errors) {
        body.errors.forEach(error => {
          console.error(error);
        });
      } else {
        this.setState({ games: [...this.state.games, body.game] });
      }
    });
  }

  switchForm() {
    this.setState({ loginForm: !this.state.loginForm });
  }

  render() {
    return (
      <main>
        <AppHeader
          user={this.state.user}
          logout={this.logout.bind(this)}
          switchForm={this.switchForm.bind(this)}
          loginForm={this.state.loginForm}
        />
        <AppContent
          registerUser={this.registerUser.bind(this)}
          loginUser={this.loginUser.bind(this)}
          games={this.state.games}
          createGame={this.createGame.bind(this)}
          user={this.state.user}
          loginForm={this.state.loginForm}
        />
        <AppFooter />
      </main>
    )
  }
}

export default App;