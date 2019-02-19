import React from 'react';
import ReactDOMServer from 'react-dom/server';
import contacts from './contacts.json';
import './App.css';
import './index.css';

function Detail(props) {
  return (
    <div id="details">
      <h1>Details</h1>
      <div className="content">
        <div className="info">
          <div className="col">
            <span className="avatar">&#9787;</span>
          </div>
          <div className="col">
            <span className="name">{props.contact.firstName}</span>
            <span className="name">{props.contact.lastName}</span>
          </div>
        </div>
        <div className="info">
          <span className="info-line">&#x260E; {props.contact.phone}</span>
          <span className="info-line">&#9993; {props.contact.email}</span>
        </div>
      </div>
    </div>
  );
}

function mapContact(id) {
  const contact = contacts[id];
  const book = document.getElementById('book');
  const parser = new DOMParser();
  const body = parser.parseFromString(ReactDOMServer.renderToStaticMarkup(<Detail contact={contact} />), 'text/html').body;
  const element = body.firstChild;
  if (book.childElementCount === 2) {
    book.removeChild(book.children[1]);
  }
  
  book.appendChild(element);
}

function makeContact(data) {
  return (
    <div className="contact" data-id={data.id} onClick={() => { mapContact(data.id); }}>
      <span className="avatar small">&#9787;</span>
      <span className="title">{data.firstName} {data.lastName}</span>
    </div>
  ); 
}

function App() {
  contacts.forEach((x, i) => { x.id = i; });
  return (
    <div className="container">
      <header>&#9993; Contact Book</header>
      <div id="book">
        <div id="list">
          <h1>Contacts</h1>
          <div className="content">
            {contacts.map(makeContact)}
          </div>
        </div>
      </div>
      <footer>Contact Book SPA &copy; {new Date().getFullYear()}</footer>
    </div>
  );
}

export default App;