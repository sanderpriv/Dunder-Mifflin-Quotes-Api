import type {Quote} from "../models/Quote";

const baseUrl = "http://localhost:5104";

export async function fetchRandomQuoteFromApi(): Promise<Quote> {
    let endpoint = baseUrl + "/quote";
    const response = await fetch(endpoint);
    return await response.json();
}