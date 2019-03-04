import React, { Component } from 'react';

function withErrorNotification(WrappedComponent) {
  return class WithErrorNotification extends Component {
    componentDidCatch(error, info) {
      console.log(error);
      console.log(info);
    }

    render() {
      return (
        <WrappedComponent />
      );
    }
  }
}

export default withErrorNotification;