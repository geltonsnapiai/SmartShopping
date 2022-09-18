class AuthenticationService {
    #loggedIn;
    #callbacks;

    constructor() {
        this.#loggedIn = false;
        this.#callbacks = {loggedIn: null, loggedOut: null};
        console.log("AuthService initialized.");
    }

    setLoggedInCallback(callback) {
        this.#callbacks.loggedIn = callback;
    }
    
    setLoggedOutCallback(callback) {
        this.#callbacks.loggedOut = callback;
    }

    #setLoggedIn(loggedIn) {
        if (this.#loggedIn != loggedIn) {
            this.#loggedIn = loggedIn;
            if (this.#loggedIn & this.#callbacks.loggedIn != null) 
                this.#callbacks.loggedIn();
            else if (this.#callbacks.loggedOut != null)
                this.#callbacks.loggedOut();
        }
    }
    
    userIsLoggedIn() {
        return this.#loggedIn;
    }

    logIn(email, password) {
        this.#setLoggedIn(true); 
    }

    logOut() {
        this.#setLoggedIn(false);
    }
}

export const authenticationService = new AuthenticationService();