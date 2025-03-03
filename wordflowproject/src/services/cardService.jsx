
import {apiRequest} from "./apiRequest"

export const cardService = {

    getCards: () => apiRequest("/api/Cards/"),

    getRandomCard: () => apiRequest("/api/Cards/Random"), 

    createCard: (card) => apiRequest("/api/Cards/", { method: "POST", body: card }),

    updateCard: (card) =>
        apiRequest(`/api/Cards/${card.id}`, { method: "PUT", body: card }),

    deleteCard: (id) => apiRequest(`/api/Cards/${id}`, { method: "DELETE" }),
};