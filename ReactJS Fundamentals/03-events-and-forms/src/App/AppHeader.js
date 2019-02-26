import React, { Component } from 'react';
import AppNav from "./AppNav";

class AppHeader extends Component {
  render() {
    return (
      <AppNav
        user={this.props.user}
        switchForm={this.props.switchForm}
        loginForm={this.props.loginForm}
        logout={this.props.logout}
      />
    )
  }
}

export default AppHeader;