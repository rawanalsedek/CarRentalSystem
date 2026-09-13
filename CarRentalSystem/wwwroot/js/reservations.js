const reservationCards = document.getElementById("reservationCards");
const totalReservations = document.getElementById("totalReservations");
const upcomingTrips = document.getElementById("upcomingTrips");
const completedTrips = document.getElementById("completedTrips");

function updateReservationSummary() {
    const cards = [...reservationCards.querySelectorAll(".reservation-card")];
    const upcoming = cards.filter((card) => card.dataset.trip === "upcoming" && !card.classList.contains("is-cancelled"));
    const completed = cards.filter((card) => card.dataset.trip === "completed");

    totalReservations.textContent = cards.length;
    upcomingTrips.textContent = upcoming.length;
    completedTrips.textContent = completed.length;
}

if (reservationCards) {
    reservationCards.addEventListener("click", (event) => {
        const cancelButton = event.target.closest(".cancel-reservation");

        if (!cancelButton) {
            return;
        }

        const card = cancelButton.closest(".reservation-card");
        const status = card.querySelector(".reservation-status");

        card.classList.add("is-cancelled");
        status.className = "reservation-status cancelled";
        status.textContent = "Cancelled";
        updateReservationSummary();
    });

    updateReservationSummary();
}
