import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { AppRoutes, PrivateRoutes } from './AppRoutes';
import Layout from './components/Layout';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Routes>
                    {AppRoutes.map((route, index) => {
                        const { element, auth, ...rest } = route;
                        if (auth) 
                            return <Route key={index} element={<PrivateRoutes />}><Route {...rest} element={element} /></Route>;
                        else
                            return <Route key={index} {...rest} element={element} />;
                    })}
                </Routes>
            </Layout>
        );
    }
}
