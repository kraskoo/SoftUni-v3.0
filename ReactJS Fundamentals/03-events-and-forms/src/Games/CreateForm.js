import React from 'react';

const state = {
  title: '',
  description: '',
  imageUrl: ''
};

function handleSubmit(evnt, props) {
  evnt.preventDefault();
  const $target = evnt.target;
  $target.children[1].value = '';
  $target.children[3].value = '';
  $target.children[5].value = '';
  props.createGame(state);
}

function handleChange(evnt) {
  const $target = evnt.target;
  const name = $target.name;
  const value = $target.value;
  state[name] = value;
}

const CreateForm = (props) => {
  return (
    <div className="create-form">
      <h1>Create game</h1>
      <form onSubmit={(e) => handleSubmit(e, props)}>
        <label>Title</label>
        <input type="text" id="title" name="title" onChange={handleChange} />
        <label>Description</label>
        <textarea type="text" id="description" name="description" onChange={handleChange} />
        <label>ImageUrl</label>
        <input type="text" id="imageUrl" name="imageUrl" onChange={handleChange} />
        <input type="submit" value="Create" />
      </form>
    </div>
  )
};

export default CreateForm;