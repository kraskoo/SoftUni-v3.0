import React, { Component } from 'react';

class ComponentWithError extends Component {
  render() {
    return (
      <div>
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Et praesentium libero quam ab saepe excepturi tempora corrupti rerum quis dolores ullam laboriosam, delectus, dolorum optio eum, maiores cupiditate officiis voluptatem.Incidunt cum officiis officia eligendi aut, distinctio modi quibusdam saepe animi suscipit tempore qui ducimus ipsam harum dicta. Illum, assumenda dicta. Vero laboriosam perferendis illum. Vero saepe placeat omnis? Eaque.
        {this.nonExistence()}
      </div>
    );
  }
}

export default ComponentWithError;