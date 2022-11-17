import React, { useState } from 'react';
import { authenticationService } from '../auth/AuthenticationServce';


export function ProfileButton({ userName }) {
    
    const [state, setstate] = useState(false);
    
    const showDropDown=()=> {
        setstate(true);
    }

    const hideDropDown=()=> {
        setstate(false);
    }
    

    return(
        <div className="nav-item dropdown" onMouseEnter={showDropDown} onMouseLeave={hideDropDown}>
            <a href="#" className="nav-link dropdown-toggle show">
            <img className="rounded-circle me-lg-2" src="assets/img/avatar.jpg" alt="fa fa-user me-lg-2" style={{width: 40, height: 40}}/>
                <span className="d-none d-lg-inline-flex">{userName}</span>
            </a>
            
            {state? (<div className="dropdown-menu dropdown-menu-end bg-secondary border-0 rounded-0 rounded-bottom m-0 show">
                <button className="dropdown-item">My Profile</button>
                <button className="dropdown-item">Settings</button>
                <button className="dropdown-item">Shopping List</button>
                <button className="dropdown-item" onClick={() => authenticationService.logout()}>Log out</button>
            </div>)
            : null}
            
        
        </div>
    )
}
