
/**
 * Funkcja służy do wykonywania uniwersalnych żądań API.
 * @param {string} endpoint - Ścieżka do zasobu (np. '/tasks').
 * @param {Object} options - Opcje żądania (np. metoda HTTP, body, nagłówki).
 * @returns {Promise<any>} - Parsowana odpowiedź JSON (lub null dla HTTP 204).
 * @throws {Error} - Jeśli odpowiedź ma status różny od 2xx.
 */

export const apiRequest = async (endpoint, { method = "GET", body, params = {} } = {}) => {

    const API_BASE_URL = "https://localhost:44335";

    const config = {
        method,
        credentials: "include", // Obsługuje ciasteczka
        headers: { "Content-Type": "application/json" },
        body: body ? JSON.stringify(body) : undefined, // Serializacja body, jeśli istnieje
    };

    // Budowanie ciągu zapytania (query string) na podstawie obiektu `params`
    const queryString = Object.keys(params).length
        ? "?" + new URLSearchParams(params).toString()
        : "";

    // Wykonanie żądania
    const response = await fetch(`${API_BASE_URL}${endpoint}${queryString}`, config);

    // Obsługa braku autoryzacji
    if (response.status === 401) {
        return null;
    }

    // Obsługa błędu HTTP
    if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`HTTP Error: ${response.status}. Details: ${errorText}`);
    }

    // Jeśli brak treści, zwracamy null
    const contentType = response.headers.get("content-type");
    if (contentType && contentType.includes("text/plain")) {
        return response.text();
    }

    if (!contentType || (!contentType.includes("application/json"))) {
        return null; // Brak danych do sparsowania
    }

    try {
        return await response.json(); // Parsowanie JSON
    } catch (error) {
        throw new Error(`Failed to parse JSON: ${error.message}`);
    }
}