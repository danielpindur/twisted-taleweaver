export function getRandomEnumValue<T extends Record<string, string>>(
    enumObject: T
): T[keyof T] {
    const values = Object.values(enumObject) as Array<T[keyof T]>;
    const index = Math.floor(Math.random() * values.length);
    return values[index];
}

export function getEnumValues<T extends Record<string, string>>(
    enumObject: T
): Array<T[keyof T]> {
    return Object.values(enumObject) as Array<T[keyof T]>;
}
