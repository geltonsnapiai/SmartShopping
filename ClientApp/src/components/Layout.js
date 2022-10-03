import React, { Component } from 'react';
import { Container } from 'reactstrap';
import Header from './Header';
import { SideBar } from './SideBar';
import { authenticationService } from './auth/AuthenticationServce';
import { useLocation } from 'react-router-dom';

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
                        <Container className="container-fuild pt-4 px-4">
                            {this.props.children}
                        </Container>
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
                        <Container className="container-fuild pt-4 px-4">
                            {this.props.children}
                        </Container>
                    </div>
                </>
            );
        }
    }
}

export default withLocation(Layout);