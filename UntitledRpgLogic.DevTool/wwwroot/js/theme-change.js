function setTheme(theme) {
    document.documentElement.setAttribute("data-theme", theme);
    console.log(`Theme changed to: ${theme}`);
    // Create a cookie to remember the user's theme choice
    createThemeCookie(theme);
}

// Create a theme cookie to remember the user's choice
function createThemeCookie(theme) {
    const date = new Date();
    date.setTime(date.getTime() + (365 * 24 * 60 * 60 * 1000)); // 1 year expiration
    document.cookie = `theme=${theme}; expires=${date.toUTCString()}; path=/`;
}

// Read the theme cookie to set the initial theme
function readThemeCookie() {
    const name = "theme=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookies = decodedCookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i].trim();
        if (cookie.indexOf(name) === 0) {
            return cookie.substring(name.length, cookie.length);
        }
    }
    return null; // No theme cookie found
}

// Initialize theme based on cookie or system preference
function initializeTheme(updateCheckbox = false) {
    const theme = readThemeCookie();
    if (theme) {
        setTheme(theme);
    } else {
        const prefersDarkScheme = window.matchMedia("(prefers-color-scheme: dark)").matches;
        setTheme(prefersDarkScheme ? "tooling-dark" : "tooling-light");
    }
    if (updateCheckbox) {
        document.getElementById("theme-toggle-checkbox").checked = theme === "tooling-dark";
    }
}

initializeTheme();
