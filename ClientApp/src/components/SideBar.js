import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { NavLink as RRNavLink, Link } from 'react-router-dom';

export class SideBar extends Component {
    constructor (props) {
        super(props);
    }

    render() {
        return (
            <div className={`sidebar pe-4 pb-3 ${this.props.open ? "open" : ""}`}>
                <Navbar className="navbar bg-secondary navbar-dark">
                    <NavbarBrand className="navbar-brand mx-4 mb-3" tag={Link} to="/">
                        <h4 className="text-primary"><i className="fa fa-user-edit me-2"></i>SmartShopping</h4>
                    </NavbarBrand>
                    <div className="d-flex align-items-center ms-4 mb-4">
                        <div className="position-relative">
                            <img className="rounded-circle" src="assets/img/avatar.jpg" alt="" style={{ width: "40px", height: "40px"}}/>
                            <div className="bg-success rounded-circle border border-2 border-white position-absolute end-0 bottom-0 p-1"></div>
                        </div>
                        <div className="ms-3">
                            <h6 className="mb-0">Jhon Doe</h6>
                            <span>Admin</span>
                        </div>
                    </div>
                    <div className="navbar-nav w-100">
                        <NavItem>
                            <NavLink tag={RRNavLink} to="/"><i className="fa fa-home me-2"/>Home</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={RRNavLink} to="/counter"><i className="fa fa-calculator me-2"/>Counter</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={RRNavLink} to="/fetch-data"><i className="fa fa-cloud me-2"/>Fetch data</NavLink>
                        </NavItem>
                    </div>
                </Navbar>
            </div>
        );
    }
}