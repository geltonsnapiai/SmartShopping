import React, { Component } from "react";
import {
    Navbar,
    NavbarBrand,
    NavItem,
    NavLink,
} from "reactstrap";
import { NavLink as RRNavLink, Link } from "react-router-dom";
import { authFetch } from '../../auth/AuthFetch';
import { NamedLogo } from "../NamedLogo";

export class SideBar extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: null
        };
    }

    componentDidMount() {
        authFetch('api/auth/user')
            .then(response => response.json())
            .then(user => this.populateUser(user));
    }

    populateUser(user) {
        this.setState({ user: user });
    }

    render() {
        return (
            <div className={`sidebar pt-2 pe-4 pb-4 ${this.props.open ? "open" : ""}`} >
                <Navbar className="navbar bg-secondary navbar-dark">
                    <NavbarBrand
                        className="navbar-brand mx-1 mb-3"
                        tag={Link}
                        to="/"
                    >
                        <h3 className="text-primary">
                            <NamedLogo/>
                        </h3>
                    </NavbarBrand>
                    {
                        (this.state.user !== null) && (
                            <div className="d-flex align-items-center ms-4 mb-4">
                                <div className="position-relative">
                                    <img className="rounded-circle" src="assets/img/avatar.jpg" alt="" style={{ width: "40px", height: "40px"}}/>
                                    {/* Notification circle */}
                                    {/* <div className="bg-success rounded-circle border border-2 border-white position-absolute end-0 bottom-0 p-1"></div> */}
                                </div>
                                <div className="ms-3">
                                    <h6 className="mb-0">{this.state.user.name}</h6>
                                    <span>{this.state.user.role}</span>
                                </div>
                            </div>
                        )
                    }
                    <div className="navbar-nav w-100">
                        <NavItem className="w-100">
                            <NavLink tag={RRNavLink} to="/">
                                <i className="fa fa-home me-2" />
                                Home
                            </NavLink>
                        </NavItem>
                        <NavItem className="w-100">
                            <NavLink tag={RRNavLink} to="/upload">
                                <i className="fa fa-shopping-bag me-2" />
                                Upload
                            </NavLink>
                        </NavItem>
                        <NavItem className="w-100">
                            <NavLink tag={RRNavLink} to="/search">
                                <i className="fa fa-search me-2"/>
                                Search
                            </NavLink>
                        </NavItem>
                    </div>
                </Navbar>
            </div>
        );
    }
}
