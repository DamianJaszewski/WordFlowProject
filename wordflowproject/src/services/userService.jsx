import { apiRequest } from "./apiRequest"

export const userService = {

    login: (user) => apiRequest("/login", {
        method: "POST",
        body: user,
        params : { useCookies: true }
    }),

    register: (user) => apiRequest("/api/User", {
        method: "POST",
        body: user
    }),

    getUser: () => apiRequest("/api/User"),

    logout: () => apiRequest("/logout", {
        method: "POST"
    })
};