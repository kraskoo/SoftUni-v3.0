import React, { Component } from 'react';
import './Alert.css';

function withAlert(WrappedComponent) {
  return class WithAlert extends Component {
    render() {
      return (
        <div className="alert">
          <span className="alert-symbol">&#9888;</span>
          <WrappedComponent {...this.props} />
        </div>
      );
    }
  }
}

export default withAlert;