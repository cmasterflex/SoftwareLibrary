import React, { Component } from 'react';
import { Table } from 'react-bootstrap';

const API = 'api/library/software';
const reg = /^([0-9]+\.*){1,3}$/;

export class SoftwareSearchComponent extends Component {

    constructor(props) {
        super(props);

        this.state = {
            version: "",
            software: [],
            loading: false,
            invalid: false
        };

        this.loadData();
    }

    buildUrl() {
        return API + "?version=" + this.state.version;
    }

    loadData() {
        let url = this.buildUrl();
        fetch(url)
            .then(response => response.json())
            .then(data => {
                this.setState({ software: data, loading: false });
            });
    }

    handleVersionChange(event) {
        var value = event.target.value;
        if (reg.test(value) || value === "") {
            this.setState({
                version: event.target.value,
                loading: true,
                invalid: false
            }, this.loadData);
        } else {
            this.setState({invalid: true});
        }
    }

    renderSoftwareRow(software, i) {
        return (
            <tr key={i}>
                <td>{software.name}</td>
                <td>{software.version}</td>
            </tr>
        );
    }

    renderSoftwareTable() {
        return (
            this.state.software.length > 0 ? 
            <Table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Version</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.software.map((software, i) => this.renderSoftwareRow(software, i))}
                </tbody>
                </Table>
                : <p>no data</p>
        );
    }

    render() {
        let softwareList = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderSoftwareTable();

        return (
            <div>
                <div id="version">
                    <input
                        onChange={(event) => this.handleVersionChange(event)}
                        value={this.state.version}
                    />
                    { this.state.invalid && <div>
                        <p className="required" >invalid character, only numbers and '.' e.g. 1.23.3</p>
                    </div> }
                </div>
                {softwareList}
            </div>
        );
    }
}