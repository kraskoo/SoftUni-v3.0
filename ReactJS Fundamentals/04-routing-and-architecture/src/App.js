import React, { Component } from 'react';
import { Router, Route, Switch } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import createBrowserHistory from 'history/createBrowserHistory';

import Navbar from './Navbar/Navbar';
import Home from './Home/Home';
import Register from './Register/Register';
import Login from './Login/Login';
import Create from './Create/Create';
import './App.css';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: null,
      isAdmin: false,
      movies: [],
      history: createBrowserHistory()
    };
    this.handleLogin = this.handleLogin.bind(this);
    this.handleLogout = this.handleLogout.bind(this);
    this.handleRegister = this.handleRegister.bind(this);
    this.handleCreateMovie = this.handleCreateMovie.bind(this);
  }

  handleCreateMovie(movie) {
    fetch('http://localhost:9999/feed/movie/create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(movie)
    }).then(response => response.json()).then(body => {
      if (!body.movie) {
        toast.error(body.message);
      } else {
        this.setState(state => ({ movies: [...state.movies, body.movie] }));
        this.state.history.push('/');
      }
    });
  }

  handleLogin(user) {
    fetch('http://localhost:9999/auth/signin', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(user)
    }).then(response => response.json()).then(body => {
      if (!body.username) {
        toast.error(body.message);
      } else {
        this.setState({
          username: body.username,
          isAdmin: body.isAdmin
        });
        this.state.history.push('/');
        toast.success(body.message);
      }
    });
  }

  handleRegister(user) {
    fetch('http://localhost:9999/auth/signup', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(user)
    }).then(response => response.json()).then(body => {
      if (!body.username) {
        toast.error(body.message);
      } else {
        this.setState({ username: body.username, isAdmin: body.isAdmin });
        this.state.history.push('/');
        toast.success(body.message);
      }
    });
  }

  handleLogout() {
    this.setState({ username: null, isAdmin: false });
    this.state.history.push('/login');
    toast.success('Successfully logout!');
  }

  render() {
    return (
      <Router history={this.state.history}>
        <div className="App">
          <Navbar handleLogout={this.handleLogout} {...this.state} />
          <ToastContainer autoClose={2500} hideProgressBar={true} closeButton={<span>&#9827;</span>} />
          <Switch>
            <Route path="/" exact render={(props) => <Home {...props} {...this.state} />} />
            <Route path="/register" exact render={(props) => <Register {...props} handleRegister={this.handleRegister} />} />
            <Route path="/login" exact render={(props) => <Login {...props} handleLogin={this.handleLogin} />} />
            <Route path="/create" exact render={(props) => <Create {...props} handleCreateMovie={this.handleCreateMovie} />} />
            <Route render={() => <h1>Page not found.</h1>} />
          </Switch>
        </div>
      </Router>
    );
  }
}

export default App;