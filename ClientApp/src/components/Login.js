import React, { useState } from "react";
import { Link, Navigate, useNavigate } from "react-router-dom";
import { Button, Input, Label } from "reactstrap";
import { authenticationService } from './auth/AuthenticationServce';
import { NamedLogo } from "./NamedLogo";

function Login() {
    const userLoggedIn = authenticationService.userIsLoggedIn();
    const navigate = useNavigate();

    const [error, setError] = useState(null);

    function getUserInput() {
        return {
            email: document.getElementById("email").value,
            password: document.getElementById("password").value
        }
    }

    function validateUserInput(input) {
        if (input.email === "")  return { ok: false, err: { id: "email", msg: "Enter email" }};
        if (input.password === "")  return { ok: false, err: { id: "password", msg: "Enter password" }};
        return { ok: true };
    }

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
                            { (error !== null && error.id === "email" ) &&
                            <div className="text-end" style={{color: "var(--primary)"}}><p>{error.msg}</p></div> }
                            <div className="form-floating mb-3">
                                <Input type="email" className="form-control" placeholder="name@example.com" id="email"/>
                                <Label>Email address</Label>
                            </div>
                            { (error !== null && error.id === "password" ) &&
                            <div className="text-end" style={{color: "var(--primary)"}}><p>{error.msg}</p></div> }
                            <div className="form-floating mb-4">
                                <Input type="password" className="form-control" placeholder="Password" id="password"/>
                                <Label>Password</Label>
                            </div>
                            {(error !== null && error.id === "loginError") &&
                                <div className="text-end" style={{ color: "var(--primary)" }}><p>{error.msg}</p></div>}

                            {/* TODO: Implement remember me.
                                <div className="d-flex align-items-center justify-content-between mb-4">
                                <div className="form-check">
                                    <Input type="checkbox" className="form-check-input"/>
                                    <Label className="form-check-label">Remember me</Label>
                                </div>
                                <a href="">Forgot Password</a>
                                TODO: Implement forgot password once api supports it
                            </div> */}
                            <Button color="primary" className="py-3 w-100 mb-4" onClick={() => {
                                let userData = getUserInput();
                                let valid = validateUserInput(userData);
                                if (valid.ok) {
                                    setError(null);
                                    authenticationService.login({
                                        email: userData.email,
                                        password: userData.password
                                    }).then(status => {
                                        if (status.ok) {
                                            console.log("Great success!");
                                        }
                                        else {
                                            console.error("Error: ", status.error);
                                            setError({ id: "loginError", msg: status.error});
                                        }
                                    });
                                }
                                else {
                                    setError(valid.err);
                                }
                            }}>Log In</Button>
                            <p className="text-center mb-0">
                                Don't have an Account? <Link to="/register">Register</Link>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Login;