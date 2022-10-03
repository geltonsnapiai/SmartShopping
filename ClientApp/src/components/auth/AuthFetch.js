import { authenticationService } from "./AuthenticationServce";

export const authFetch = (url, options = null) => authenticationService.fetch(url, options);