/**
 * Saves a value to localStorage under the specified key.
 * @param {string} key - The storage key.
 * @param {*} value - The value to store.
 */
SaveToStorage = function (key, value) {
    if (value === null || value === undefined) {
        console.warn(`SaveToStorage: Attempted to save null or undefined value for key "${key}"`);
        return;
    }
    console.log(`SaveToStorage: Saving key "${key}" with value:`, value);
    if (typeof key === "string") {
        localStorage.setItem(key, value)
    }
    localStorage.setItem(key, JSON.stringify(value));
};

/**
 * Loads a string value from localStorage.
 * @param {string} key - The storage key.
 * @returns {string} The loaded string or an empty string if not found.
 */
LoadStringFromStorage = function (key) {
    var str = LoadObjectFromStorage(key);
    if (str === null || str === {} || typeof str === "undefined" || typeof str === "object") {
        return "";
    }
    if (typeof str === "string") {
        return str;
    }
    console.log(`LoadStringFromStorage: Expected string, got ${typeof str}`, str);
    return `${str}`;
};

/**
 * Loads an integer value from localStorage.
 * @param {string} key - The storage key.
 * @returns {number} The loaded integer or 0 if not found.
 */
LoadIntegerFromStorage = function (key) {
    var nbr = LoadObjectFromStorage(key);
    if (nbr === null || nbr === {}) {
        return 0;
    }
    if (typeof nbr === "number") {
        return nbr;
    }
    if (typeof nbr === "string") {
        var parsedNbr = parseInt(nbr, 10);
        if (!isNaN(parsedNbr)) {
            return parsedNbr;
        }
    }
    console.log(`LoadIntegerFromStorage: Expected number, got ${typeof nbr}`, nbr);
    return 1;
};

/**
 * Loads an object from localStorage.
 * @param {string} key - The storage key.
 * @returns {Object} The loaded object or an empty object if not found.
 */
LoadObjectFromStorage = function (key) {
    var objString = localStorage.getItem(key);
    console.log(`LoadObjectFromStorage: Loading key "${key}" with value:`, objString);
    if (objString === null) {
        return {};
    }
    return JSON.parse(objString);
};
