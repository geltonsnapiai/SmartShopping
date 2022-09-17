import { Counter } from "./components/Counter";
import { PageNotFound } from "./components/PageNotFound";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { Navigate, Outlet } from 'react-router-dom';
import { authenticationService } from "./components/auth/AuthenticationServce";

export const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/counter',
        element: <Counter />,
        auth: true
    },
    {
        path: '/fetch-data',
        element: <FetchData />,
        auth: true
    },
    {
        path: '*',
        element: <PageNotFound />
    }
];


export const PrivateRoutes = () => {
    const auth = authenticationService.userIsLoggedIn();
    return (
        auth ? <Outlet/> : <Navigate to='/'/>
    );
}