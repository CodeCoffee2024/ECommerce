
// Utility function to convert string to PascalCase
export function toPascalCase(str: string): string {
    return str.replace(/(^\w|_\w)/g, match => match.replace('_', '').toUpperCase()); 
}

export function toCamelCase(str: string): string {
    return str
      .replace(/([-_]\w)/g, match => match[1].toUpperCase()) // Convert "_x" or "-x" to "X"
      .replace(/^\w/, match => match.toLowerCase()); // Ensure first letter is lowercase
}