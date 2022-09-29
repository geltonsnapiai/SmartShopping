import { Counter } from "./components/Counter";
import { PageNotFound } from "./components/PageNotFound";
import { FetchData } from "./components/FetchData";
import { Search } from "./components/Search";
import { Home } from "./components/Home";
import { Navigate, Outlet } from 'react-router-dom';
import { authenticationService } from "./components/auth/AuthenticationServce";
import Login from "./components/Login";
import Register from "./components/Register";

export const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/login',
        element: <Login />
    },
    {
        path: '/register',
        element: <Register />
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
        path: '/search',
        element: <Search />,
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