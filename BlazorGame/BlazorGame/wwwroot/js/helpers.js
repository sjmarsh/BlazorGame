export function setFocus(elementId) {
    var el = document.getElementById(elementId);
    if (el) {
        el.focus();
    }
}