import React, { useState } from "react";
import { Link, Navigate, useNavigate } from "react-router-dom";
import { Button, Input, Label } from "reactstrap";
import { authenticationService } from './auth/AuthenticationServce';
import { NamedLogo } from "./NamedLogo";

function Register() {
    const userLoggedIn = authenticationService.userIsLoggedIn();
    const navigate = useNavigate();

    // { id: elementId, msg: error_message }
    const [error, setError] = useState(null);

    function getUserInput() {
        return {
            username: document.getElementById("username").value,
            email: document.getElementById("email").value,
            password: document.getElementById("password").value,
            repeatPassword: document.getElementById("repeatPassword").value
        }
    }

    function validateUserInput(input) {
        if (input.username === "") return { ok: false, err: { id: "username", msg: "Enter username" }};
        if (input.email === "")  return { ok: false, err: { id: "email", msg: "Enter email" }};
        if (input.password === "")  return { ok: false, err: { id: "password", msg: "Enter password" }};
        if (input.repeatPassword === "")  return { ok: false, err: { id: "repeatPassword", msg: "Repeat password" }};
        if (input.password !== input.repeatPassword)  return { ok: false, err: { id: "repeatPassword", msg: "Passwords don't match" }};
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
                            { (error !== null && error.id === "username" ) &&
                            <div className="text-end" style={{color: "var(--primary)"}}><p>{error.msg}</p></div> }
                            <div className="form-floating mb-3">
                                <Input type="text" className="form-control" placeholder="Username" id="username"/>
                                <Label>Username</Label>
                            </div>
                            { (error !== null && error.id === "email" ) &&
                            <div className="text-end" style={{color: "var(--primary)"}}><p>{error.msg}</p></div> }
                            <div className="form-floating mb-3">
                                <Input type="email" className="form-control" placeholder="name@example.com" id="email"/>
                                <Label>Email address</Label>
                            </div>
                            { (error !== null && error.id === "password" ) &&
                            <div className="text-end" style={{color: "var(--primary)"}}><p>{error.msg}</p></div> }
                            <div className="form-floating mb-3">
                                <Input type="password" className="form-control" placeholder="Password" id="password"/>
                                <Label>Password</Label>
                            </div>
                            { (error !== null && error.id === "repeatPassword" ) &&
                            <div className="text-end" style={{color: "var(--primary)"}}><p>{error.msg}</p></div> }
                            <div className="form-floating mb-4">
                                <Input type="password" className="form-control" placeholder="Password" id="repeatPassword"/>
                                <Label>Repeat password</Label>
                            </div>
                            <Button color="primary" className="py-3 w-100 mb-4" onClick={() => {
                                let userData = getUserInput();
                                let valid = validateUserInput(userData);
                                if (valid.ok) {
                                    setError(null);
                                    authenticationService.register({
                                        name: userData.username,
                                        email: userData.email,
                                        password: userData.password
                                    }).then((result) => console.log(result));
                                }
                                else {
                                    setError(valid.err);
                                }
                                
                                //authenticationService.logIn();
                                //navigate('/');
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