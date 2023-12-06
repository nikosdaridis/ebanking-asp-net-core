const root = document.querySelector(":root");
const themeIcon = document.querySelector(".theme-icon");
const localStorageTheme = localStorage.getItem("bank-theme");

if (localStorageTheme === "dark" || localStorageTheme === "light")
    setTheme(localStorageTheme);
else if (window.matchMedia && window.matchMedia("(prefers-color-scheme: dark)").matches)
    setTheme("dark");
else
    setTheme("light");

function setTheme(theme) {
    root.style.setProperty("--first-color", theme === "light" ? "#ffffff" : "#1f2023");
    root.style.setProperty("--second-color", theme === "light" ? "#f2f2f2" : "#171717");
    root.style.setProperty("--text-color", theme === "light" ? "#212121" : "#ffffff");
    root.style.setProperty("--theme-icon-filter", theme === "light" ?
        "invert(0%) sepia(21%) saturate(69%) hue-rotate(155deg) brightness(122%) contrast(74%)" :
        "invert(100%) sepia(0%) saturate(0%) hue-rotate(93deg) brightness(103%) contrast(103%)");

    themeIcon.setAttribute("src", theme === "light" ? "/dark.svg" : "/light.svg");
    localStorage.setItem("bank-theme", theme);
}

themeIcon.addEventListener("click", function () {
    localStorage.getItem("bank-theme") === "light"
        ? setTheme("dark")
        : setTheme("light");
});