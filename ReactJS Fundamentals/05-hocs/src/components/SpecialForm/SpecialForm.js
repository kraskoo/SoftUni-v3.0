import React, { Component } from 'react';

class SpecialForm extends Component {
  constructor(props) {
    super(props);
    this.state = {};
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleChange = this.handleChange.bind(this);
  }

  componentWillMount() {
    this.props.children.forEach(child => {
      if (child.type === 'input' || child.type === 'textarea') {
        this.setState({ [child.props.name]: '' });
      }
    });
  }

  handleSubmit(e) {
    e.preventDefault();
    console.log(this.state);
  }

  handleChange(e) {
    const name = e.target.name;
    const value = e.target.value;
    this.setState({ [name]: value });
    console.log(`${name} => ${this.state[name]}`);
  }

  render() {
    return (
      <form onSubmit={this.handleSubmit}>
        <h1>{this.props.header}</h1>
        {
          React.Children.map(this.props.children, child => {
            if (child.type === 'input' || child.type === 'textarea') {
              return React.cloneElement(child, { onChange: this.handleChange });
            }

            return child;
          })
        }
        <input type="submit" value={this.props.submit} />
      </form>
    );
  }
}

export default SpecialForm;