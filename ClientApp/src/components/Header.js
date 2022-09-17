import React, { Component } from 'react';
import { Button, Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { authenticationService } from './auth/AuthenticationServce';

export class Header extends Component {
    static displayName = Header.name;

    constructor (props) {
        super(props);
        this.state = {user: props.user}
        this.toggleCallback = props.toggleCallback;
    }

    render() {
        if (authenticationService.userIsLoggedIn()) {
            return (
                <Navbar className="navbar-expand bg-secondary navbar-dark sticky-top px-4 py-0">
                    <NavbarBrand className="navbar-brand d-flex d-lg-none me-4" tag={Link} to="/">
                        <h2 className="text-primary mb-0"><i className="fa fa-user-edit"></i>SmartShopping</h2>
                    </NavbarBrand>
                    <NavbarToggler className="sidebar-toggler flex-shrink-0" style={{color: "var(--primary)"}} tag="a" onClick={this.toggleCallback}>
                        <i className="fa fa-bars"></i>
                    </NavbarToggler>
                    <Button className="btn-outline-primary m-2" onClick={() => authenticationService.setLoggedIn(false)}>
                        Log out
                    </Button>
                </Navbar>
            );
        }
        else {
            return (
                <Navbar className="navbar-expand bg-secondary navbar-dark sticky-top px-4 py-0">
                    <NavbarBrand className="navbar-brand d-flex me-4" tag={Link} to="/">
                        <h2 className="text-primary mb-0"><i className="fa fa-user-edit"></i>SmartShopping</h2>
                    </NavbarBrand>
                    <Button className="btn-outline-primary m-2" onClick={() => authenticationService.setLoggedIn(true)}>
                        Log in
                    </Button>
                </Navbar>
            );
        }
    }
}
