import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface TorrentSearchResult {
    siteId: string;
    name: string;
    size: string;
    seeds: number;
    leeches: number;
    uploaded: string;
    relativeDetailUrl: string;
}

export class TorrentSearch extends React.Component<RouteComponentProps<{}>, { searchResults: TorrentSearchResult[] }> {

    constructor(props: any) {
        super(props);
        this.state = { searchResults: [] };
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    public render() {
        return <div>
            <h1>Torrent finder</h1>
            <TorrentSearchInput onSubmit={this.handleSubmit} />
            <div className="center-block" style={{ width: '80%' }}>
                <TorrentSearchTable searchResults={this.state.searchResults} />
            </div>
        </div>;
    }

    public handleSubmit(event: any) {
        event.preventDefault();
        var query = (document.getElementById("query") as HTMLInputElement).value;

        fetch('/api/Torrents/Search?query=' + query)
            .then(response => response.json() as Promise<{ results: TorrentSearchResult[] }>)
            .then(data => { this.setState({ searchResults: data.results }); });
    }
}

class TorrentSearchInput extends React.Component<{ onSubmit(event: any): void }, {}> {
    public render() {
        return (
            <div className="input-group col-md-5" role="search">
                <input type="text" id="query" className="col-md-5 form-control" placeholder="Search torrents" />
                <span className="input-group-btn">
                    <button className="btn btn-default" onClick={this.props.onSubmit}>Search</button>
                </span>
            </div>
        );
    }
}

class TorrentSearchTable extends React.Component<{ searchResults: TorrentSearchResult[] }> {
    constructor(props: any) {
        super(props);

        this.downloadClickHandler = this.downloadClickHandler.bind(this);
    }

    private downloadClickHandler(searchResult: TorrentSearchResult) {
        if (searchResult) {
            fetch('/api/Torrents/Download?siteId=' + searchResult.siteId + '&relativeUrl=' + searchResult.relativeDetailUrl, {
                method: 'GET',
            });
        }
    }

    public render() {
        if (this.props.searchResults.length == 0) {
            return (null);
        }

        return (
            <table className="table" role="searchResults">
                <thead>
                    <TorrentSearchTableHead />
                </thead>
                <tbody>
                    {this.props.searchResults.map((searchResult, index) =>
                        <TorrentSearchTableRow searchResult={searchResult} downloadClicked={this.downloadClickHandler} key={index} />
                    )}
                </tbody>
            </table>
        );
    }
}

interface TorrentSearchTableRowProps {
    searchResult: TorrentSearchResult;
    downloadClicked: (searchResult: TorrentSearchResult) => void;
}

class TorrentSearchTableRow extends React.Component<TorrentSearchTableRowProps> {
    constructor(props: any) {
        super(props);

        this.clickHandler = this.clickHandler.bind(this);
    } 

    private clickHandler(event: any) {
        this.props.downloadClicked(this.props.searchResult);
    }

    public render() {
        return (
            <tr>
                <td>{this.props.searchResult.name}</td>
                <td>{this.props.searchResult.size}</td>
                <td>{this.props.searchResult.seeds}</td>
                <td>{this.props.searchResult.leeches}</td>
                <td>
                    {this.props.searchResult.uploaded}
                    <span className="glyphicon glyphicon-save pull-right" role="button" onClick={this.clickHandler} title="Download"></span>
                </td>
            </tr>
        );
    }
}

function TorrentSearchTableHead() {
    return (
        <tr className="table-heading-gray">
            <th className="col-xs-7">Name</th>
            <th className="col-xs-1">Size</th>
            <th className="col-xs-1">Seeds</th>
            <th className="col-xs-1">Leeches</th>
            <th className="col-xs-2">Uploaded</th>
        </tr>
    );
}