const pickupInput = document.getElementById("detailsPickup");
const dropoffInput = document.getElementById("detailsDropoff");
const totalPrice = document.getElementById("totalPrice");
const daysText = document.getElementById("daysText");
const reserveButton = document.getElementById("reserveButton");
const price = Number(document.querySelector("[data-daily-price]")?.dataset.dailyPrice || 0);

function calculateTotal() {
    if (!pickupInput || !dropoffInput || !totalPrice || !daysText) {
        return;
    }

    if (!pickupInput.value || !dropoffInput.value) {
        totalPrice.textContent = "$0";
        daysText.textContent = "Select your dates";
        return;
    }

    const pickup = new Date(pickupInput.value);
    const dropoff = new Date(dropoffInput.value);
    const days = Math.ceil((dropoff - pickup) / (1000 * 60 * 60 * 24));

    if (days <= 0) {
        totalPrice.textContent = "$0";
        daysText.textContent = "Invalid date range";
        return;
    }

    totalPrice.textContent = `$${days * price}`;
    daysText.textContent = `${days} day${days > 1 ? "s" : ""} × $${price}`;
}

function saveDates() {
    if (pickupInput?.value) {
        sessionStorage.setItem("pickupDate", pickupInput.value);
    }

    if (dropoffInput?.value) {
        sessionStorage.setItem("dropoffDate", dropoffInput.value);
    }
}

function datesAreValid() {
    if (!pickupInput?.value || !dropoffInput?.value) {
        alert("Please select pick-up and drop-off dates.");
        return false;
    }

    if (new Date(dropoffInput.value) <= new Date(pickupInput.value)) {
        alert("Drop-off date must be after pick-up date.");
        return false;
    }

    return true;
}

if (pickupInput && dropoffInput) {
    pickupInput.addEventListener("change", () => {
        dropoffInput.min = pickupInput.value;
        saveDates();
        calculateTotal();
    });

    dropoffInput.addEventListener("change", () => {
        saveDates();
        calculateTotal();
    });

    const savedPickup = sessionStorage.getItem("pickupDate");
    const savedDropoff = sessionStorage.getItem("dropoffDate");

    if (savedPickup) {
        pickupInput.value = savedPickup;
        dropoffInput.min = savedPickup;
    }

    if (savedDropoff) {
        dropoffInput.value = savedDropoff;
    }

    calculateTotal();
}

if (reserveButton) {
    reserveButton.addEventListener("click", (event) => {
        if (!datesAreValid()) {
            event.preventDefault();
            return;
        }

        saveDates();
    });
}

const reservationForm = document.getElementById("reservationForm");

if (reservationForm) {
    reservationForm.addEventListener("submit", (event) => {
        if (!datesAreValid()) {
            event.preventDefault();
        }
    });
}
