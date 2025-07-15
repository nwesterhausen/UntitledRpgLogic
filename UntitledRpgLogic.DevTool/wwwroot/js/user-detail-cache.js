const UserDetailCacheKeys = {
    authorName: "authorName",
    authorGuid: "authorGuid",
    authorUrl: "authorUrl",
    moduleName: "moduleName",
    moduleGuid: "moduleGuid",
    moduleDescription: "moduleDescription",
    moduleVersion: "moduleVersion",
    moduleVersionNumber: "moduleVersionNumber",
};

// Create a cookie to store the module i
function updateUserDetailCache(cache, data) {
    const date = new Date();
    date.setTime(date.getTime() + (365 * 24 * 60 * 60 * 1000)); // 1 year expiration
    document.cookie = `${cache}=${data}; expires=${date.toUTCString()}; path=/`;
    console.log(`Cache updated for ${cache}: ${data}`);
}

// Read the cache cookie to load local data
function readCache(cache) {
    const name = `${cache}=`;
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookies = decodedCookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i].trim();
        if (cookie.indexOf(name) === 0) {
            console.log(`Cache read for ${cache}: ${cookie.substring(name.length, cookie.length)}`);
            return cookie.substring(name.length, cookie.length);
        }
    }
    console.log(`No cache found for ${cache}`);
    return null; // No theme cookie found
}
