
import {apiRequest} from "./apiRequest"

export const cardService = {

    getCards: () => apiRequest("/api/Cards/"),

    //createTask: (task) => apiRequest("/api/MyTasks/", { method: "POST", body: task }),

    //updateTask: (task) =>
    //    apiRequest(`/api/MyTasks/${task.id}`, { method: "PUT", body: task }),

    //deleteTask: (id) => apiRequest(`/api/MyTasks/${id}`, { method: "DELETE" }),
};