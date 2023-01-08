/**
 * Global function for filter table and list
 *
 * @param {Array<{ T }>} array List of objects to filter
                        * @param {string[]} filterFields keywords on apply values
                        * @param {string} text filter value
                        * @returns {Array<{ T }>} List filtered by provide text
                        */
export const search = <T extends {}>(
    array: Array<T>,
    filterFields: Array<string>,
    text: string
): Array<T> => {
    text = text.toLowerCase()
    return array.filter(function (row) {
        return filterFields.some(function (value) {
            return (row as any)[value].toString().toLowerCase().indexOf(text) !== -1
        })
    })
}