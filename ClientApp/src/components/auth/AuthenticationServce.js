/*
  checkIfLoggedIn:
    - Check for tokens in local storage;
      |_ Doesn't exist: 
      |  - Logged in = false
      |_ Exist:
         - Try to reach api/auth/user endpoint;
           |_ Success: 
           |  - Logged in = true 
           |  - Load access token to memory
           |_ Fails: 
              - Refresh tokens:
                |_ Success:
                |  - Logged in = true 
                |  - Load access token to memory
                |  - Update tokens in local storage
                |_ Fail:
                   - Logged in = false
                   - Remove tokens from local storage

  login/register:
    - Send credentials to api/auth/login endpoint:
      |_ Fails:
      |  - Return error message
      |_ Success:
         - Logged in = true 
         - Load access token to memory
         - Update tokens in local storage
  
  logout:
    - Send request to api/auth/revoke endpoint
    - Remove tokens from local storage
    - Unload access token from memory
    - Loggen in = false
 */

class AuthenticationService {
    #loggedIn;
    #callbacks;
    #accessToken;

    constructor() {
        this.#loggedIn = false;
        this.#accessToken = null;
        this.#callbacks = {loggedIn: [], loggedOut: []};

        console.log("AuthenticationService initialized.");
    }
    
    async checkIfLoggedIn() {
        let accessToken = localStorage.getItem("accessToken");
        let refreshToken = localStorage.getItem("refreshToken");
        
        if (accessToken === null || refreshToken === null) {
            this.#loggedIn = false;
            this.#accessToken = null;
            return;
        }

        let response = await fetch("api/auth/user", {
            method: 'GET',
            headers: {
                Authorization: `Bearer ${accessToken}`,
            }
        });

        if (response.status === 200) { // success
            let user = await response.json();
            this.#accessToken = accessToken;
            this.#setLoggedIn(true);
            return;
        }
        
        let status = await this.#refreshTokens();
        if (status.ok) {
            this.#setLoggedIn(true);
        }
    }

    async register(credentials) {
        let response = await fetch("api/auth/register", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        }).catch(error => console.error('Error:', error));
        
        if (response.status === 400) { // Bad request
            let body = await response.json();
            return { ok: false, error: body.detail, id: body.type };
        }
        else if (response.status !== 201) { // Other errors
            return { ok: false, error: null };
        }

        let tokens = await response.json();
        localStorage.setItem("accessToken", tokens.accessToken);
        localStorage.setItem("refreshToken", tokens.refreshToken);
        this.#accessToken = tokens.accessToken;

        this.#setLoggedIn(true);

        return { ok: true, error: null };
    }

    async login(credentials) {
        let response = await fetch("api/auth/login", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        }).catch(error => console.error('Error:', error));

        if (response.status === 400) { // Bad request
            let body = await response.json();
            return { ok: false, error: "Error from API" };
        }
        else if (response.status !== 200) { // Other errors
            return { ok: false, error: null };
        }

        let tokens = await response.json();
        localStorage.setItem("accessToken", tokens.accessToken);
        localStorage.setItem("refreshToken", tokens.refreshToken);
        this.#accessToken = tokens.accessToken;

        this.#setLoggedIn(true);

        return { ok: true, error: null };
    }
    
    async logout() {
        // TODO: change to authFetch
        let revokeFetch = this.fetch("api/auth/revoke", {
            method: 'POST'
        });

        this.#accessToken = null;
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        this.#setLoggedIn(false);
    }
    
    /* Fetch wrapper that automaticaly adds authorization token
     * and automatically refreshes tokens if access token is expired. 
     */
    async fetch(url, options = null) {
        if (!this.#loggedIn)
            throw "To use this function user must be logged in";

        let finalOptions;
        if (options !== null) {
            const {
                headers,
                ...extraOpts
            } = options;

            finalOptions = {
                headers: {
                    Authorization: `Bearer ${this.#accessToken}`,
                    ...headers
                },
                ...extraOpts
            }
        }
        else {
            finalOptions = {
                headers: {
                    Authorization: `Bearer ${this.#accessToken}`
                }
            }
        }

        let response = await fetch(url, finalOptions);
        if (response.status !== 401) // unauthorized
            return response;
        
        let status = await this.#refreshTokens();
        if (!status.ok) 
            return response;

        const {
            headers,
            ...extraOpts
        } = finalOptions;

        let refreshedOptions = {
            headers: {
                Authorization: `Bearer ${this.#accessToken}`,
                ...headers
            },
            ...extraOpts
        }
        return fetch(url, refreshedOptions);
    }

    getAccessToken() {
        return this.#accessToken;
    }

    userIsLoggedIn() {
        return this.#loggedIn;
    }
    
    addLoggedInCallback(callback) {
        this.#callbacks.loggedIn.push(callback);
    }
    
    addLoggedOutCallback(callback) {
        this.#callbacks.loggedOut.push(callback);
    }

    deleteLoggedInCallback(name) {
        if (this.#callbacks.loggedIn.hasOwnProperty(name))
            delete this.#callbacks.loggedIn[name];
    }
    
    deleteLoggedOutCallback(name) {
        if (this.#callbacks.loggedOut.hasOwnProperty(name))
            delete this.#callbacks.loggedOut[name];
    }
        
    #setLoggedIn(loggedIn) {
        if (this.#loggedIn !== loggedIn) {
            this.#loggedIn = loggedIn;
            if (this.#loggedIn & this.#callbacks.loggedIn != null) 
                this.#callCallbacks(this.#callbacks.loggedIn);
            else if (this.#callbacks.loggedOut != null)
                this.#callCallbacks(this.#callbacks.loggedOut);
        }
    }
    
    #callCallbacks(callbacks) {
        for (const callback of callbacks) {
            callback();
        }
    }

    async #refreshTokens() {
        console.log("refreshing tokens");
        let accessToken = localStorage.getItem("accessToken");
        let refreshToken = localStorage.getItem("refreshToken");
        
        if (accessToken === null || refreshToken === null) {
            this.#accessToken = null;
            this.#setLoggedIn(false);
            return { ok: false, error: "No tokens in memory" };
        }

        let response = await fetch("api/auth/refresh", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                accessToken: accessToken,
                refreshToken: refreshToken
            })
        }).catch(error => console.error('Error:', error));

        let tokens = await  response.json();
        localStorage.setItem("accessToken", tokens.accessToken);
        localStorage.setItem("refreshToken", tokens.refreshToken);
        this.#accessToken = tokens.accessToken;

        return { ok: true };
    }
}

export const authenticationService = new AuthenticationService();