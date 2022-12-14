import { PageNotFound } from "./components/PageNotFound";
import { Search } from "./components/Search/Search";
import { Groceries } from "./components/Groceries/Groceries";
import { Home } from "./components/Home/Home";
import { Navigate, Outlet } from 'react-router-dom';
import { authenticationService } from "./auth/AuthenticationServce";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";

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
        path: '/search',
        element: <Search />,
        auth: true
    },
    {
        path: '/groceries',
        element: <Groceries />,
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