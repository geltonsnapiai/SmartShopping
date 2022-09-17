class AuthenticationService {
    constructor() {
        console.log("AuthService ctor called");
        this._loggedIn = false;
        this._callbacks = {loggedIn: null, loggedOut: null};
    }

    setLoggedInCallback(callback) {
        this._callbacks.loggedIn = callback;
    }
    
    setLoggedOutCallback(callback) {
        this._callbacks.loggedOut = callback;
    }

    setLoggedIn(loggedIn) {
        if (this._loggedIn != loggedIn) {
            this._loggedIn = loggedIn;
            if (this._loggedIn & this._callbacks.loggedIn != null) 
                this._callbacks.loggedIn();
            else if (this._callbacks.loggedOut != null)
                this._callbacks.loggedOut();
        }
    }
    
    userIsLoggedIn() {
        return this._loggedIn;
    }
}

export const authenticationService = new AuthenticationService();