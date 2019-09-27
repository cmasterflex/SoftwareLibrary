import React, { Component } from 'react';
import { SoftwareSearchComponent } from './components/SoftwareSearch';

export default class App extends Component {
  displayName = App.name

  render() {
      return (
          <div className="container app">
            <h1>Software Library</h1>
              <h3>Search sofware by version number</h3>
              <SoftwareSearchComponent />
          </div>
    );
  }
}
