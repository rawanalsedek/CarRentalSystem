const reservationCards = document.getElementById("reservationCards");
const totalReservations = document.getElementById("totalReservations");
const upcomingTrips = document.getElementById("upcomingTrips");
const completedTrips = document.getElementById("completedTrips");

function updateReservationSummary() {
    if (!reservationCards || !totalReservations || !upcomingTrips || !completedTrips) {
        return;
    }

    const cards = [...reservationCards.querySelectorAll(".reservation-card")];
    const upcoming = cards.filter((card) => card.dataset.trip === "upcoming");
    const completed = cards.filter((card) => card.dataset.trip === "completed");

    totalReservations.textContent = cards.length;
    upcomingTrips.textContent = upcoming.length;
    completedTrips.textContent = completed.length;
}

if (reservationCards) {
    updateReservationSummary();
}
