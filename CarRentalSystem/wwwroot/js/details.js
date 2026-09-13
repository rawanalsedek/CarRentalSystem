const cars = {
    "bmw-5-series": {
        brand: "BMW", model: "5 Series", price: 95, year: "2024", seats: "5 Seats", transmission: "Automatic", fuel: "Gasoline", status: "Available",
        image: "https://images.unsplash.com/photo-1555215695-3004980ad54e?auto=format&fit=crop&w=1200&q=85",
        shortDescription: "A premium sedan that combines comfort, performance, and modern technology for an exceptional driving experience.",
        fullDescription: "The BMW 5 Series offers a perfect balance between luxury, performance, and everyday practicality. Whether you're planning a business trip or a weekend adventure, this car provides a smooth and comfortable ride."
    },
    "mercedes-c-class": {
        brand: "Mercedes", model: "C Class", price: 120, year: "2024", seats: "5 Seats", transmission: "Automatic", fuel: "Gasoline", status: "Available",
        image: "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?auto=format&fit=crop&w=1200&q=85",
        shortDescription: "An elegant luxury sedan with refined comfort and modern technology for every trip.",
        fullDescription: "The Mercedes C Class brings together sophisticated design, a comfortable interior, and confident performance for a premium driving experience."
    },
    "toyota-rav4": {
        brand: "Toyota", model: "RAV4", price: 65, year: "2024", seats: "5 Seats", transmission: "Automatic", fuel: "Gasoline", status: "Available",
        image: "https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?auto=format&fit=crop&w=1200&q=85",
        shortDescription: "A practical and comfortable SUV, ready for city drives and weekend escapes.",
        fullDescription: "The Toyota RAV4 gives you versatile space, everyday comfort, and dependable performance for every journey."
    },
    "audi-a6": {
        brand: "Audi", model: "A6", price: 110, year: "2023", seats: "5 Seats", transmission: "Automatic", fuel: "Gasoline", status: "Rented",
        image: "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?auto=format&fit=crop&w=1200&q=85",
        shortDescription: "A refined executive sedan with a sleek design, advanced features, and smooth performance.",
        fullDescription: "The Audi A6 blends premium comfort, contemporary design, and confident road handling for business or leisure travel."
    },
    "bmw-x5": {
        brand: "BMW", model: "X5", price: 105, year: "2024", seats: "5 Seats", transmission: "Automatic", fuel: "Gasoline", status: "Available",
        image: "https://images.unsplash.com/photo-1551830820-330a71b99659?auto=format&fit=crop&w=1200&q=85",
        shortDescription: "A spacious luxury SUV that pairs powerful performance with comfort for the whole family.",
        fullDescription: "The BMW X5 offers generous space, premium amenities, and capable performance for comfortable journeys of any length."
    },
    "toyota-camry": {
        brand: "Toyota", model: "Camry", price: 55, year: "2024", seats: "5 Seats", transmission: "Automatic", fuel: "Gasoline", status: "Available",
        image: "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb?auto=format&fit=crop&w=1200&q=85",
        shortDescription: "A reliable and comfortable sedan that makes every daily drive easy and enjoyable.",
        fullDescription: "The Toyota Camry combines excellent comfort, practical features, and dependable efficiency for city and long-distance travel."
    }
};

const selectedCarId = new URLSearchParams(window.location.search).get("car");
const selectedCar = cars[selectedCarId] || cars["bmw-5-series"];
const pickupInput = document.getElementById("detailsPickup");
const dropoffInput = document.getElementById("detailsDropoff");
const totalPrice = document.getElementById("totalPrice");
const daysText = document.getElementById("daysText");
const reserveButton = document.getElementById("reserveButton");

function setCarDetails(car) {
    document.title = `DriveRent | ${car.brand} ${car.model}`;

    const image = document.getElementById("detailsCarImage");
    image.src = car.image;
    image.alt = `${car.brand} ${car.model}`;

    document.getElementById("detailsStatusText").textContent = car.status;
    document.getElementById("detailsBrand").textContent = car.brand;
    document.getElementById("detailsModel").textContent = car.model;
    document.getElementById("detailsShortDescription").textContent = car.shortDescription;
    document.getElementById("detailsDailyPrice").textContent = `$${car.price}`;
    document.getElementById("detailsYear").textContent = car.year;
    document.getElementById("detailsSeats").textContent = car.seats;
    document.getElementById("detailsTransmission").textContent = car.transmission;
    document.getElementById("detailsFuel").textContent = car.fuel;
    document.getElementById("detailsFullDescription").textContent = car.fullDescription;
}

function calculateTotal() {
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

    totalPrice.textContent = `$${days * selectedCar.price}`;
    daysText.textContent = `${days} day${days > 1 ? "s" : ""} × $${selectedCar.price}`;
}

pickupInput.addEventListener("change", () => {
    dropoffInput.min = pickupInput.value;
    calculateTotal();
});

dropoffInput.addEventListener("change", calculateTotal);

reserveButton.addEventListener("click", () => {
    if (!pickupInput.value || !dropoffInput.value) {
        alert("Please select pick-up and drop-off dates.");
        return;
    }

    if (new Date(dropoffInput.value) <= new Date(pickupInput.value)) {
        alert("Drop-off date must be after pick-up date.");
        return;
    }

    window.location.href = "login.html";
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

setCarDetails(selectedCar);
calculateTotal();
