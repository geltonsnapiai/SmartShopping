import React, { Component } from 'react';
import { Container } from 'reactstrap';
import Header from './Header';
import { SideBar } from './SideBar/SideBar';
import { authenticationService } from '../auth/AuthenticationServce';
import { useLocation } from 'react-router-dom';
import { Footer } from './Footer';

const withLocation = (Component) => {
    return (props) => {
        const location = useLocation();
        return <Component location={location} {...props} />;
    };
};

class Layout extends Component {
    static displayName = Layout.name;

    constructor(props) {
        super(props);
        this.state = { open: false, user: authenticationService.userIsLoggedIn()};
        this.toggleSidebar = this.toggleSidebar.bind(this);

        authenticationService.addLoggedInCallback(() => this.setState({user: true}));
        authenticationService.addLoggedOutCallback(() => this.setState({user: false}));
    }

    toggleSidebar() {
        this.setState({ open: !this.state.open });
    }

    render() {
        const currentPath = this.props.location.pathname;
        if (this.state.user){
            return (
                <>
                    <SideBar open={this.state.open}/>
                    <div className={`content ${this.state.open ? "open" : ""}`}>
                        <Header user="true" toggleCallback={this.toggleSidebar}/>
                        <div className="vh-100">
                            {this.props.children}
                        </div>
                        <Footer/>
                    </div>
                </>
            );
        }
        else if (currentPath === '/login' | currentPath === '/register') {
            return (
                <>
                    {this.props.children}
                </>
            );
        }
        else {
            return (
                <>
                    <div className="content open">
                        <Header user="false"/>
                        {this.props.children}
                        <Footer/>
                    </div>
                </>
            );
        }
    }
}

export default withLocation(Layout);