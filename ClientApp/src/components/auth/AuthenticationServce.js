class AuthenticationService {
    #loggedIn;
    #callbacks;

    constructor() {
        this.#loggedIn = false;
        this.#callbacks = {loggedIn: null, loggedOut: null};
        console.log("AuthService initialized.");
    }
    
    async register(credentials) {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        };
        return await fetch("/auth/register", requestOptions)
                        .then(response => response.json());
    }

    login(credentials) {
        // const requestOptions = {
        //     method: 'POST',
        //     headers: {
        //         'Content-Type': 'application/json'
        //     },
        //     body: JSON.stringify(credentials)
        // };
        // fetch("/auth/login", requestOptions)
        //     .then(response => response.json())
        //     .then((result) => {
        //         console.log(result);
        //         localStorage.setItem("jwt", result.token);
        //         localStorage.setItem("refreshToken", result.refreshToken);
        //     });
        this.#setLoggedIn(true); 
    }
    
    logout() {
        this.#setLoggedIn(false);
    }
    
    userIsLoggedIn() {
        return this.#loggedIn;
    }
    
    setLoggedInCallback(callback) {
        this.#callbacks.loggedIn = callback;
    }
    
    setLoggedOutCallback(callback) {
        this.#callbacks.loggedOut = callback;
    }
        
    #setLoggedIn(loggedIn) {
        if (this.#loggedIn !== loggedIn) {
            this.#loggedIn = loggedIn;
            if (this.#loggedIn & this.#callbacks.loggedIn != null) 
                this.#callbacks.loggedIn();
            else if (this.#callbacks.loggedOut != null)
                this.#callbacks.loggedOut();
        }
    }
}

export const authenticationService = new AuthenticationService();