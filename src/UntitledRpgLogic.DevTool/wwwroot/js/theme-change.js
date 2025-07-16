/**
 * The theme name for dark mode.
 * @type {string}
 */
const DarkTheme = "dim";

/**
 * The theme name for light mode.
 * @type {string}
 */
const LightTheme = "emerald";

/**
 * Sets the theme to dark or light based on the parameter.
 * @param {boolean} darkTheme - If true, sets dark theme; otherwise, sets light theme.
 */
function setDarkTheme(darkTheme) {
    if (darkTheme === true) {
        setTheme(DarkTheme);
    } else {
        setTheme(LightTheme);
    }
}

/**
 * Applies the specified theme and stores the preference.
 * @param {string} theme - The theme to apply.
 */
function setTheme(theme) {
    document.documentElement.setAttribute("data-theme", theme);
    console.log(`Theme changed to: ${theme}`);
    // Save the theme to local storage
    SaveToStorage("theme", theme);
}

/**
 * Initializes the theme based on stored preference or system setting.
 * Optionally updates a checkbox to reflect the current theme.
 * @param {boolean} [updateCheckbox=false] - Whether to update the theme toggle checkbox.
 */
function initializeTheme(updateCheckbox = false) {
    const theme = LoadStringFromStorage("theme");
    if (theme) {
        setTheme(theme);
    } else {
        const prefersDarkScheme = window.matchMedia("(prefers-color-scheme: dark)").matches;
        setTheme(prefersDarkScheme ? DarkTheme : LightTheme);
    }
    if (updateCheckbox) {
        document.getElementById("theme-toggle-checkbox").checked = theme === DarkTheme;
    }
}

// Initialize theme on script load
initializeTheme();
