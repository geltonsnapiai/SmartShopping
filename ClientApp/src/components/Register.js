import React from "react";
import { Link, Navigate, useNavigate } from "react-router-dom";
import { Button, Input, Label } from "reactstrap";
import { authenticationService } from './auth/AuthenticationServce';
import { NamedLogo } from "./NamedLogo";

function Register() {
    const userLoggedIn = authenticationService.userIsLoggedIn();
    const navigate = useNavigate();

    return (
        <>
            {userLoggedIn && <Navigate to="/" />}
            <div className='container-fluid'>
                <div className="row h-100 align-items-center justify-content-center" style={{minHeight: "100vh"}}>
                    <div className="col-12 col-sm-8 col-md-6 col-lg-5 col-xl-4">
                        <div className="bg-secondary rounded p-4 p-sm-5 my-4 mx-3">
                            <div className="d-flex align-items-center justify-content-between mb-3">
                                <Link to="/" className="">
                                    <h3 className="text-primary mb-0"><NamedLogo/></h3>
                                </Link>
                            </div>
                            <div className="form-floating mb-3">
                                <Input type="text" className="form-control" placeholder="Username"/>
                                <Label>Username</Label>
                            </div>
                            <div className="form-floating mb-3">
                                <Input type="email" className="form-control" placeholder="name@example.com"/>
                                <Label>Email address</Label>
                            </div>
                            <div className="form-floating mb-3">
                                <Input type="password" className="form-control" placeholder="Password"/>
                                <Label>Password</Label>
                            </div>
                            <div className="form-floating mb-4">
                                <Input type="password" className="form-control" placeholder="Password"/>
                                <Label>Repeat password</Label>
                            </div>
                            <Button color="primary" className="py-3 w-100 mb-4" onClick={() => {
                                authenticationService.logIn();
                                navigate('/');
                            }}>Register</Button>
                            <p className="text-center mb-0">
                                Already have an Account? <Link to="/login">Log In</Link>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Register;