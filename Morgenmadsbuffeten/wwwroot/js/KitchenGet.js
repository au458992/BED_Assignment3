document.addEventListener("DOMContentLoaded", () => {
    let date = document.getElementById("dateInput");
    if (!date.value) {
        date.valueAsDate = new Date();
    }
});