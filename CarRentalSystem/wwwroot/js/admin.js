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
