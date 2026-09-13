const adminMenu = document.getElementById("adminMenu");
const adminSidebar = document.getElementById("adminSidebar");

if (adminMenu && adminSidebar) {
    adminMenu.addEventListener("click", () => adminSidebar.classList.toggle("open"));
}

document.querySelectorAll("[data-modal]").forEach((button) => {
    button.addEventListener("click", () => document.getElementById(button.dataset.modal)?.classList.add("open"));
});

document.querySelectorAll("[data-close-modal]").forEach((button) => {
    button.addEventListener("click", () => button.closest(".admin-modal").classList.remove("open"));
});

document.querySelectorAll("[data-admin-search]").forEach((input) => {
    input.addEventListener("input", () => {
        const query = input.value.toLowerCase().trim();
        document.querySelectorAll(input.dataset.adminSearch).forEach((row) => {
            row.style.display = row.textContent.toLowerCase().includes(query) ? "" : "none";
        });
    });
});

document.querySelectorAll("[data-reservation-action]").forEach((button) => {
    button.addEventListener("click", () => {
        const status = button.closest("tr").querySelector(".status");
        const action = button.dataset.reservationAction;
        status.className = `status ${action}`;
        status.textContent = action[0].toUpperCase() + action.slice(1);
    });
});
