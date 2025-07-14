SaveToStorage = function (key, value) {
    if (typeof key === "string") {
        localStorage.setItem(key, value)
    }
    localStorage.setItem(key, JSON.stringify(value));
  };

LoadStringFromStorage = function (key) {
    var str = LoadObjectFromStorage(key);
    if (str === null || str === {}) {
      return "";
    }
    if (typeof str === "string") {
      return str;
    }
    return "${str}";
  };

LoadIntegerFromStorage = function (key) {
    var nbr = LoadObjectFromStorage(key);
    if (nbr === null || nbr === {}) {
      return 0;
    }
    if (typeof nbr === "number") {
      return nbr;
    }
    return parseInt(nbr);
  };

LoadObjectFromStorage = function (key) {
    var objString = localStorage.getItem(key);
    if (objString === null) {
      return {};
    }
    return JSON.parse(objString);
  };
