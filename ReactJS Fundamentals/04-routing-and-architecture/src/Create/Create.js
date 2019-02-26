import React, { Component } from 'react';
import './Create.css';

class Create extends Component {
  constructor(props) {
    super(props);
    this.state = {
      title: '',
      storyLine: '',
      trailerUrl: '',
      poster: ''
    };
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(evnt) {
    const name = evnt.target.name;
    const value = evnt.target.value;
    this.setState({ [name]: value });
  }

  handleSubmit(evnt) {
    evnt.preventDefault();
    this.props.handleCreateMovie(this.state);
  }

  render() {
    return (
      <div className="Create">
        <h1>Create Movie</h1>
        <form onSubmit={this.handleSubmit}>
          <label htmlFor="title">Title</label>
          <input type="text" id="title" name="title" value={this.state.title} placeholder="Titanic" onChange={this.handleChange} />
          <label htmlFor="storyLine">Story Line</label>
          <input type="text" id="storyLine" name="storyLine" value={this.state.storyLine} placeholder="Text" onChange={this.handleChange} />
          <label htmlFor="trailerUrl">Trailer Url</label>
          <input type="text" id="trailerUrl" name="trailerUrl" value={this.state.trailerUrl} placeholder="https://www.youtube.com/watch?v=DNyKDI9pn0Q" onChange={this.handleChange} />
          <label htmlFor="poster">Movie Poster</label>
          <input type="text" id="poster" name="poster" value={this.state.poster} placeholder="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRzg6o0KjhufKFU1iBNr1zuyi0YDNgCUw4Ky5SNATZDVKaIUkiAA" onChange={this.handleChange} />
          <input type="submit" value="Create" />
        </form>
      </div>
    );
  }
}

export default Create;