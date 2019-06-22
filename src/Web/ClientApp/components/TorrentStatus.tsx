import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface Torrent {
    id: string;
    name: string;
    seeds: number;
    peers: number;
    downloadSpeed: string;
    uploadSpeed: string;
    progress: number;
    size: string;
}

export class TorrentStatus extends React.Component<RouteComponentProps<{}>> {
    public render() {
        return (
        <div>
            <h1>Torrent status</h1>
            <TorrentStatusTable />
        </div>);
    }
}

class TorrentStatusTable extends React.Component<{}, { torrents: Torrent[], loading: boolean }> {
    private intervalId: number;

    constructor() {
        super();
        this.state = { torrents: [], loading: true };
    }

    public componentWillMount() {
        fetch('api/Torrents/Login')
            .then(response => response.json())
            .then(data => {
                if (data.isSuccess === true) {
                    showSnackbar("Login succesful");

                    this.intervalId = startIntervalAndExecute(() => fetch('api/Torrents/GetAll')
                        .then(response => response.json() as Promise<Torrent[]>)
                        .then(data => {
                            this.setState({ torrents: data, loading: false });
                        }), 5000);
                }
                else if (data.isSuccess === false) {
                    showSnackbar("Login failed");
                }
                else {
                    showSnackbar("Something went wrong while logging-in");
                }
            });
    }

    public componentWillUnmount() {
        clearInterval(this.intervalId);
    }

    public render() {
        return (
            <table className="table table-condensed table-hover" role="torrentStatuses">
                <thead>
                    <TorrentStatusTableHead />
                </thead>
                <tbody>
                    {this.state.torrents.map(torrent =>
                        <TorrentStatusTableRow torrent={torrent} key={torrent.id}/>
                    )}
                </tbody>
            </table>
        );
    }
}

class TorrentStatusTableRow extends React.Component<{ torrent: Torrent }> {
    public render() {
        return (
            <tr>
                <td className="col-xs-5">{this.props.torrent.name}</td>
                <td className="col-xs-2">
                    <div className="progress" style={{ marginBottom: '0px' }}>
                        <div className="progress-bar progress-bar-success" role="progressbar" style={{ width: this.props.torrent.progress + '%' }}>
                            {this.props.torrent.progress + '%'} Complete
				        </div>
                    </div>
                </td>
                <td className="col-xs-1">{this.props.torrent.size}</td>
                <td className="col-xs-1">{this.props.torrent.downloadSpeed}</td>
                <td className="col-xs-1">{this.props.torrent.uploadSpeed}</td>
                <td className="col-xs-1">{this.props.torrent.seeds}</td>
                <td className="col-xs-1">{this.props.torrent.peers}</td>
            </tr>
        );
    }
}

function TorrentStatusTableHead() {
    return (
        <tr className="table-heading-gray">
            <th className="col-xs-5">Torrent name</th>
            <th className="col-xs-2">Progress</th>
            <th className="col-xs-1">Size</th>
            <th className="col-xs-1">Down speed</th>
            <th className="col-xs-1">Up speed</th>
            <th className="col-xs-1">Seeds</th>
            <th className="col-xs-1">Peers</th>
        </tr>
    );
}

const showSnackbar = (message: string) => {
    var x = document.getElementById("snackbar");

    if (x != null) {
        x.className = "show";
        x.innerText = message;
        setTimeout(function () { if (x != null) { x.className = x.className.replace("show", ""); } }, 3000);
    }
}

const startIntervalAndExecute = (callback: () => void, interval: number) => {
    callback();
    return setInterval(callback, interval);
}