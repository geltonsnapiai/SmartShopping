import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { Header } from './Header';
import { SideBar } from './SideBar';
import { authenticationService } from './auth/AuthenticationServce';

export class Layout extends Component {
    static displayName = Layout.name;

    constructor(props) {
        super(props);
        this.state = { open: false, user: authenticationService.userIsLoggedIn()};
        this.toggleSidebar = this.toggleSidebar.bind(this);

        authenticationService.setLoggedInCallback(() => this.setState({user: true}));
        authenticationService.setLoggedOutCallback(() => {
            this.setState({user: false});
        });
    }

    toggleSidebar() {
        console.log("Toogle toggle gobble gobble");
        this.setState({ open: !this.state.open });
    }

    render() {
        if (this.state.user){
            return (
                <>
                    <SideBar open={this.state.open}/>
                    <div className={`content ${this.state.open && "open"}`}>
                        <Header user="true" toggleCallback={this.toggleSidebar}/>
                        <Container className="container-fuild pt-4 px-4">
                            {this.props.children}
                        </Container>
                    </div>
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
