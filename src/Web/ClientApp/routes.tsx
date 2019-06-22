import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { TorrentStatus } from './components/TorrentStatus';
import { TorrentSearch } from './components/TorrentSearch';

export const routes = <Layout>
    <Route exact path='/' component={TorrentSearch} />
    <Route path='/torrents' component={TorrentStatus} />
</Layout>;
