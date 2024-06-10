const root = document.querySelector(":root");
const themeIcon = document.querySelector(".theme-icon");
const localStorageTheme = localStorage.getItem("bank-theme");
const prefersDarkScheme = window.matchMedia && window.matchMedia("(prefers-color-scheme: dark)").matches;

const themes = {
    light: {
        "--first-color": "#ffffff",
        "--second-color": "#f2f2f2",
        "--text-color": "#212121",
        "--theme-icon-filter": "invert(0%) sepia(21%) saturate(69%) hue-rotate(155deg) brightness(122%) contrast(74%)",
        "iconSrc": "/dark.svg"
    },
    dark: {
        "--first-color": "#1f2023",
        "--second-color": "#171717",
        "--text-color": "#ffffff",
        "--theme-icon-filter": "invert(100%) sepia(0%) saturate(0%) hue-rotate(93deg) brightness(103%) contrast(103%)",
        "iconSrc": "/light.svg"
    }
};

const initialTheme = localStorageTheme === "dark" || localStorageTheme === "light" ? localStorageTheme : prefersDarkScheme ? "dark" : "light";
setTheme(initialTheme);

function setTheme(theme) {
    const properties = themes[theme];

    for (const property in properties) {
        if (property !== "iconSrc")
            root.style.setProperty(property, properties[property]);
    }

    themeIcon.setAttribute("src", properties.iconSrc);
    localStorage.setItem("bank-theme", theme);
}

themeIcon.addEventListener("click", () => setTheme(localStorage.getItem("bank-theme") === "light" ? "dark" : "light"));
