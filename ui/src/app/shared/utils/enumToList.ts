/**
 * 
 * Transform a enum type to list of self items named
 * 
 * @param enumType 
 * @returns Array<{ label: string, value: T }>
 */
export const EnumToList = <T>(enumType: T): Array<{ label: string, value: T }> => {
    const stringIsNumber = (value: any) => isNaN(Number(value)) === false;
    let items = Object.keys(enumType as unknown as any).map(key => ({
        label: key,
        value: key as T
    }))
    return items
}