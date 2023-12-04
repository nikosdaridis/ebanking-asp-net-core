const root = document.querySelector(":root");
const themeIcon = document.querySelector(".theme-icon");

if (
    localStorage.getItem("bank-theme") === "dark" ||
    localStorage.getItem("bank-theme") === "light"
)
    setTheme(localStorage.getItem("bank-theme"));
else
    setTheme(window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light");

function setTheme(theme) {
    if (theme === "light") {
        localStorage.setItem("bank-theme", "light");
        root.style.setProperty("--first-color", "#ffffff");
        root.style.setProperty("--second-color", "#f2f2f2");
        root.style.setProperty("--theme-icon-filter", "invert(0%) sepia(21%) saturate(69%) hue-rotate(155deg) brightness(122%) contrast(74%)");
        root.style.setProperty("--text-color", "#212121");

        themeIcon.setAttribute("src", "/dark.svg");
    } else {
        localStorage.setItem("bank-theme", "dark");
        root.style.setProperty("--first-color", "#1f2023");
        root.style.setProperty("--second-color", "#171717");
        root.style.setProperty("--text-color", "#ffffff");
        root.style.setProperty("--theme-icon-filter", "invert(100%) sepia(0%) saturate(0%) hue-rotate(93deg) brightness(103%) contrast(103%)");

        themeIcon.setAttribute("src", "/light.svg");
    }
}

themeIcon.addEventListener("click", function () {
    localStorage.getItem("bank-theme") === "light"
        ? setTheme("dark")
        : setTheme("light");
});