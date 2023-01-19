export function setFocus(elementId) {
    var el = document.getElementById(elementId);
    if (el) {
        el.focus();
    }
}

export function getDimensions() {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
}