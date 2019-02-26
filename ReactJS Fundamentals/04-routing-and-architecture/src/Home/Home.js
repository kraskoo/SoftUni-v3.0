import React, { Component } from 'react';
import './Home.css'

class Home extends Component {
  constructor(props) {
    super(props);
    this.state = { movies: this.props.movies, trailer: null, storyLine: null };
    this.handleViewTrailer = this.handleViewTrailer.bind(this);
    this.handleViewStoryLine = this.handleViewStoryLine.bind(this);
  }

  componentDidMount() {
    fetch('http://localhost:9999/feed/movies')
      .then(response => response.json())
      .then(body => {
        if (body.movies) {
          this.setState({ movies: body.movies });
        }
      });
  }

  handleViewTrailer(index) {
    this.setState(state => ({ trailer: state.movies[index], storyLine: null }));
  }

  handleViewStoryLine(index) {
    this.setState(state => ({ storyLine: state.movies[index], trailer: null }));
  }

  render() {
    return (
      <div className="Home">
        <div className="Home">
            <h1>All movies</h1>
            {
              this.state.trailer ?
              <div className="trailer" style={{
                  width: '640px',
                  height: '360px'
                }}>
                <div style={{
                    width: '100%',
                    height: '100%'
                  }}>
                  <iframe
                    frameBorder="0"
                    allowFullScreen="1"
                    allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
                    title={this.state.trailer.title}
                    width="100%"
                    height="100%"
                    src={`https://www.youtube.com/embed/${this.state.trailer.trailerUrl.split('=')[1]}?autoplay=1&amp;mute=0&amp;controls=0&amp;origin=http%3A%2F%2Flocalhost%3A3000&amp;playsinline=1&amp;showinfo=0&amp;rel=0&amp;iv_load_policy=3&amp;modestbranding=1&amp;enablejsapi=1&amp;widgetid=1`}
                    id="widget2">
                  </iframe>
                </div>
              </div> : null
            }
            {
              this.state.storyLine ?
                <span>
                  <h2>{this.state.storyLine.title}</h2>
                  <p>{this.state.storyLine.storyLine}</p>
                </span> : null
            }
            <ul className="movies">
            {
              this.state.movies.map((x, i) => (
                <li className="movie" key={x._id}>
                  <h2>{x.title}</h2>
                  <img src={x.poster} alt={x.title} />
                  {
                    this.props.username ?
                      <span>
                        <button onClick={() => this.handleViewTrailer(i)}>View Trailer</button>
                        <button onClick={() => this.handleViewStoryLine(i)}>View Story Line</button>
                      </span> :
                      null
                  }
                </li>
            ))}
            </ul>
          </div>
      </div>
    );
  }
}

export default Home;