export function tryParseJSON(jsonString: string) {
    try {
        const result = JSON.parse(jsonString);
        return result;
    } catch {
        return null;
    }
}